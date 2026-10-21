using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Dc2Pilot
{
    /// <summary>One shipped dinosaur (dinosaurClass). Spec: Dev/Evidence/lifeMode_enemy_spec.json.</summary>
    public sealed class DinoActor
    {
        public int kind, skin, surface;
        public int drawIndex;              // NativeDrawOrder creation index of the pooled dinosaur object
        public int targetSeq;              // order in the mainScene+8 target vector (push_back at creation; shared with the boss, AddBoss 0xd1102)
        public float hp, spd, exp, frq, atk, atkDis;
        public float x, y;                 // native GameObject world origin; independent of rendered art bounds.
        public bool faceRight;             // dino +96 flag
        public int state = 8;              // 8 spawn, 0 move, 1 attack, 4 hit, 3 die (native +180 / +180 values)
        public bool dead;                  // die animation running / finished
        public bool removed;
        public int spawnMode;
        public float velocityX;
        public bool targetable = true;     // native dinosaurClass+172; Jump clears until its marked callback.
        public NativeCaPlayer rig;
        public GameObject root;
        public NativeCaAnimation aMove, aAttack, aBehited, aBefired, aDie, aJump, aBe, aShao;   // aBe/aShao: BEAnim (+224) / BShaoAnim (+228), loaded on first BeDamaged
        public bool legacyHit;             // state 4 set by the pilot hit placeholder (not the native BE state)
        public FxActor burnFx;             // dinosaur +192: DahuoAnim overlay while burning
        public bool advActive;             // dinosaur +104 advanced-animation name non-empty ("BefiredAnim")
        public bool attackHit;             // keyframe 12 handled this attack
        public float hitTimer;
        public NativeCaAnimation aDie2;    // dinosaur +220: headless "die2" animation used by DieTouFei
        public bool headSpawned;           // dinosaur +184: head particle object exists
        public bool disabled;              // GameObject::Enable(false) (blast death): not updated, not drawn
        public bool bodyAsleep;            // b2Body flag 8 (e_sleepFlag): b2World::Solve skips the body
        public float bodySleepTime;        // b2Body +140 m_sleepTime
        /// <summary>GamePhysics::WakeUp 0x16d83c -> b2Body::WakeUp: clear the sleep flag, sleepTime = 0.</summary>
        public void WakeUp() { bodyAsleep = false; bodySleepTime = 0f; }
    }

    /// <summary>Battle logic (partial). NATIVE where marked; PILOT_ASSUMPTION where noted. See showcase_compare.md and the enemy spec.</summary>
    public partial class GameScene
    {
        public const float DinoAnimScale = 1.1f;     // DinoCap::g_scale = 1066192077 on iPad during loadDinoANI
        public readonly List<DinoActor> dinos = new List<DinoActor>();
        int targetSeqCounter;   // mainScene+8: dinosaurs and the boss are push_back'ed into one vector; erase keeps the order, so creation order = walk order
        public int HeroDrawIndex;
        readonly Stack<int> dinoDrawPool = new Stack<int>();
        public int DinoDrawIndex(DinoActor d) { return d.drawIndex; }   // verifier hook
        public float playerHp, playerMaxHp;
        public bool invincible;                      // playerClass+658 ("Wudi" 1.5 s)
        // Invincibility pulse (r119, DC2-BUGS-001 item 9, NATIVE). playerClass: +88 brightness (CAObjCallBack ctor 1.0) and +136 rate (OnLoad 0xbc78e: 4.0).
        // Each update while +658: v += dt * rate; v >= 1.0 -> v = 1.0, rate = -4.0; else v <= 0.5 -> v = 0.5, rate = 4.0 (0xbb700..0xbb744). The CA sprites of this update
        // already used the old v (CAObjCallBack::OnUpdate runs first). Wudi timeout 0xbb550: +658 = 0, v = 1.0 (the rate is kept).
        // Girl (playerNewAniClass): same cycle on +472 / +468 (OnLoad 0x12522c rate 4.0); the colour +240 becomes white at the top bound and 0xFF7F7F7F at the bottom bound
        // (no fade); the timeout sets +472 = 1.0 and the colour white. PILOT_ASSUMPTION: +472 is never initialised before the first Wudi (ctor 0x1260ae leaves it) - the port starts it at 1.0.
        public float wudiBrightness = 1f, wudiRate = 4f, girlWudiValue = 1f, girlWudiRate = 4f;
        void WudiPulse(float dt)
        {
            if (!invincible) return;
            float v = N.Add32(wudiBrightness, N.Mul32(dt, wudiRate));
            if (v >= 1f) { wudiBrightness = 1f; wudiRate = -4f; } else if (v <= 0.5f) { wudiBrightness = 0.5f; wudiRate = 4f; } else wudiBrightness = v;
            if (girl == null) return;
            float g = N.Add32(girlWudiValue, N.Mul32(dt, girlWudiRate));
            if (g >= 1f) { girlWudiValue = 1f; girlWudiRate = -4f; girl.objectColor = new Color32(255, 255, 255, 255); }
            else if (g <= 0.5f) { girlWudiValue = 0.5f; girlWudiRate = 4f; girl.objectColor = new Color32(127, 127, 127, 255); }
            else girlWudiValue = g;
        }
        void WudiEnd() { invincible = false; wudiBrightness = 1f; girlWudiValue = 1f; if (girl != null) girl.objectColor = new Color32(255, 255, 255, 255); }
        float invincibleTimer;
        public bool fireDown;                        // g_b_FireDown
        public bool firing;                          // fire pose active
        bool combatPoseActive { get { return HeroHurting || rolling || meleeing; } }   // heroes 0..3: advanced fire channel overlays the locomotion base (OnFireDown 0xb9990)
        bool CombatMovementAllowed { get { return !heroDead && heroInputEnabled && !HeroHurting && !rolling && !meleeing; } }
        float hitStun;                               // behit pose time
        bool nativeHurting;
        /// <summary>The hero GameObject as a timer owner: playerClass / playerNewAniClass GameObject::SetTimer 0x16c826 "fireup" 2.0, "roll" 0.5, "rollEndTime" 3.0, "Wudi" 1.5.
        /// None of these is ever killed (settimer_calls_r118z: the only GameObject::KillTimer sites are houseClass::OnKeyFrame and itemsClass::OnUpdate), so every
        /// SetTimer is an independent timer and overlapping arms each fire (r118z; the port's old countdowns restarted instead).</summary>
        readonly object heroTimer = new object();
        public bool HeroHurting { get { return nativeHurting||hitStun>0f; } }
        bool UsesRecoveredHeroHurt { get { return UsesRecoveredLifeCamera; } }
        public string LastHurtResumeRequest { get; private set; } // DEBUG_ONLY callback dispatch evidence.
        public bool heroDead;
        public int kills;
        public int weaponSlot;                       // playerClass+592
        public int hitDino;                          // counter of dino hits (test hook)
        /// <summary>Native: Triniti2D::EngineShell::InitApplication 0x165c7a calls srand(time(NULL)) once at start-up and the game draws libc rand() (0..2^31-1 scaled by 2^-31), so the sequence differs on every launch and is shared by all scenes.
        /// The pilot seeds one process-wide generator from the clock; harnesses (Dc2Game.manualStep) keep the fixed seed 12345 for reproducible runs. Supersedes PA-012 (pilot assumption: fixed host seed).</summary>
        public System.Random rng;   // DEBUG_ONLY: verifier-supplied unit source; null in play = the native libc rand() stream (NativeRand)
        void SeedRng() { if (Game.manualStep) NativeRand.Seed(12345); }   // harness reproducibility only; normal play keeps the launch-time seed
        // DEBUG_ONLY: explicit native-input fixtures; null in normal play. No gameplay permission switch.
        public Func<int> DebugSpawnModeRoll;
        public Func<float> DebugSpawnSideUnit;
        Vector2 slot0JumpBounds = new Vector2(1024f,-1024f);
        readonly Dictionary<string, NativeCaAnimation> animCache = new Dictionary<string, NativeCaAnimation>();

        // Spawn timer ids, one slot per kind (mainScene +184..+256, lifeModeScene +448..+496 for kinds 1..13 and +236..+256 for 14..19); OnTimeout compares the
        // fired id with each slot, so a kind listed twice keeps only its last id and the earlier timer fires without effect (shipped behaviour, NATIVE).
        readonly Dictionary<int, NativeTimer.Info> spawnSlots = new Dictionary<int, NativeTimer.Info>();
        public bool spawningEnabled = true;   // DEBUG_ONLY harness switch (verifiers turn spawning off); not a native flag
        public List<int> missionKinds = new List<int> { 1 };

        // PA-012 SUPERSEDED (r118v): one native stream, libc rand() words scaled by 2^-31 in float, RandomRange 0x15de8 endpoint behaviour (NativeRand).
        float Rand01() { return rng != null ? (float)rng.NextDouble() : NativeRand.Unit(); }
        int RandomRange(int lo, int hi) { return rng != null ? lo + (int)(Rand01() * (hi - lo)) : NativeRand.Range(lo, hi); }

        // ---- setup ----
        void CombatLoad()
        {
            if (Game.LaunchMission != null && Game.LaunchMission.enemies.Count > 0) missionKinds = new List<int>(Game.LaunchMission.enemies);
            int firstSkin = Dc2GameData.Dinos()[missionKinds[0]-1].skin;
            if (firstSkin == 1 || firstSkin == 2)
            {
                string firstFamily = firstSkin == 1 ? "k3" : "k1";
                var firstJump = NativeCaAnimation.Load("DC2/Image/a2b/"+firstFamily+"_jump.a2b",false,DinoAnimScale);
                slot0JumpBounds = new Vector2(firstJump.minX,firstJump.maxX);
            } // families3/5 leave the native Jump temporary empty; cached ctor bounds survive Clone.
            int level = Profile.level[heroType];
            var h = Dc2Data.Heroes()[heroType];
            int hp = h.hp - 15 + 15 * (level + 1);                      // selectScene HP label formula
            if (heroType == 4) hp = Dc2Data.GirlHp(hp, level);
            playerMaxHp = playerHp = N.HpWithJigsaw(hp, Profile.jigsaw1Count);
            // StartGame_N 0xe232e: first spawn after table(5.0) * rand01 for each kind in the mission list
            // lifeMode StartGame_N 0xe232e: 5.0*rand; mainScene::StartGame_N 0x94f68 arms each kind with its property FRQ (+12)
            UpdateHpBar(); UpdateExpBar();
            Audio.RegisterAudio("Audio_RoleAudio","DC2/Audio/Audio_Role"); // native loader31 registration6d3bc.
        }

        /// <summary>StartGame_N (runs when the mission intro ends, state 4): one spawn timer per Map.xml kind 1..19 (other ids are skipped by the switch).
        /// mainScene::StartGame_N 0x94f68 arms g_prop_kN.FRQ (+12); lifeModeScene::StartGame_N 0xe232e arms FRQ * (rand() * 2^-31) + 0.
        /// g_prop_kN is filled from Image/Conf/dinosaur.prop by LoadDinosaurProperty 0x5b7a4 (testApp::OnLoad 0xc0d74/0xc0e00), i.e. Dc2GameData.Dinos().frq.
        /// r118z: supersedes "5.0 * rand01" (lifeMode_enemy_spec timers, emulated without the loader - FALSIFIED).</summary>
        void ArmSpawnTimers()
        {
            var props = Dc2GameData.Dinos();
            foreach (int kind in missionKinds)
            {
                if (kind < 1 || kind > 19 || kind > props.Length) continue;
                float frq = props[kind - 1].frq;
                ArmSpawn(kind, MainSceneMission ? frq : N.Add32(N.Mul32(frq, Rand01()), 0f));
            }
        }

        void ArmSpawn(int kind, float seconds)
        {
            var holder = new NativeTimer.Info[1];
            holder[0] = SetTimer(this, "spawn" + kind, seconds, () => SpawnTimeout(kind, holder[0]));
            spawnSlots[kind] = holder[0];
        }

        /// <summary>Spawn re-arm interval, mainScene::OnTimeout 0xb7208.. / lifeModeScene sub_E2F92+sub_E3216: map type 7 -> FRQ + FRQ*(float)(g_time/60)*(-0.1) (0xBDCCCCCD),
        /// type 8 -> FRQ*1.2 (0x3F99999A), otherwise FRQ; binary32 in that operation order. g_time/60 is integer division (__divsi3); from g_time 600 on the
        /// interval is &lt;= 0 and the kind fires on every Timer::Update (shipped).</summary>
        public static float SpawnInterval(float frq, int mapType, int gTime)
        {
            if (mapType == 7) return N.Add32(frq, N.Mul32(N.Mul32(frq, (float)(gTime / 60)), -0.1f));
            if (mapType == 8) return N.Mul32(1.2f, frq);
            return frq;
        }

        /// <summary>OnTimeout spawn branch: the fired id must still be the kind's slot; then CreateEnemy(kind) and the slot is re-armed with SpawnInterval.
        /// No hero-death gate: GameOverStart 0xa6f70 kills no timer and StopCreateEnemy's callers are GameClear, SubTime, UpdateKillDinocap, bossN::BeAttacked,
        /// the run-end camera and OnUnload (r118z; the port's old "!heroDead" freeze was not native).</summary>
        void SpawnTimeout(int kind, NativeTimer.Info fired)
        {
            NativeTimer.Info slot;
            if (!spawnSlots.TryGetValue(kind, out slot) || slot != fired) return;
            if (!spawningEnabled) { spawnSlots.Remove(kind); return; }   // DEBUG_ONLY harness switch
            CreateEnemy(kind);
            ArmSpawn(kind, SpawnInterval(Dc2GameData.Dinos()[kind - 1].frq, missionType, gTime));
        }

        /// <summary>StopCreateEnemy (mainScene 0x9560e: +184..+232; lifeModeScene 0xd0e14: +448..+496): KillTimer and -1 for the slots of kinds 1..13 only.
        /// Kinds 14..19 keep their timers and go on spawning after GameClear / time-out (shipped quirk, NATIVE; both scenes).</summary>
        void StopCreateEnemy()
        {
            for (int k = 1; k <= 13; k++) { NativeTimer.Info t; if (spawnSlots.TryGetValue(k, out t)) { KillTimer(t); spawnSlots.Remove(k); } }
        }

        /// <summary>Test hook: kinds whose spawn slot still holds a timer id.</summary>
        public int LiveSpawnSlots(int fromKind, int toKind) { int n = 0; foreach (var kv in spawnSlots) if (kv.Key >= fromKind && kv.Key <= toKind) n++; return n; }

        void UpdateHpBar()
        {
            if (bloodBar == null) return;
            float ratio = playerMaxHp > 0f ? Mathf.Clamp01(playerHp / playerMaxHp) : 0f;
            // mainScene::UpdateHP 0x95228 (shared: lifeModeScene::OnLoad 0xe7852 / 0xe9d22, UpdateExp, every BeAttack call it) always uses the mainScene table
            // bloodbarRect 0x23d280 = (57,6,161,14). The lifeMode table (161x18, 0x23d4a0) is only the OnLoad placeholder (r119, DC2-BUGS-001 item 4: the port kept 18 in life missions
            // and the red bar overlapped the EXP bar; Showcase.mp4 ~5:10 shows the 14 bar, validation only).
            Rect full = N.Layout(57, 6, 161, 14);
            bloodBar.SetRect(new Rect(full.x, full.y, full.width * ratio, full.height));
        }

        // ---- dinosaur creation (lifeModeScene::CreateEnemy 0xd5308) ----
        public DinoActor CreateEnemy(int kind)
        {
            var props = Dc2GameData.Dinos();
            if (kind < 1 || kind > props.Length) return null;
            var p = props[kind - 1];
            var d = new DinoActor { kind = kind, skin = p.skin, surface = p.surface };
            float s = N.S;
            d.atkDis = N.Add32(N.Mul32(s,p.adis),N.Mul32(s,N.Mul32(Rand01(),20f))); // ReadProperty precedes EXP/mode random draws.
            int L = Profile.level[heroType] + 1;
            int M = Game.LaunchMission != null ? Game.LaunchMission.level : 1;     // lifeModeScene+640 (source to confirm)
            float hpv = p.hp, atk = p.atk, frq = p.frq; int exp = (int)p.exp;
            float difficultyStep = 0f;                                              // g_difficult-1 (static init 1)
            switch (d.skin)
            {
                case 1: case 5:
                    if (L <= 11) hpv += M - 2 + 2 * L;
                    else if (L <= 21) hpv += 20 + (4 * L - 44) + M;
                    else hpv += 20 + 40 + (20 * L - 420) + M;
                    if (d.skin == 1) { atk += 3 * L - 3; exp += 3 * L - 3 + RandomRange(0, 5); frq = Mathf.Max(2f, frq + (L - 1) * -0.25f); }
                    else { atk += 2 * L - 2; exp += -3 + 3 * L + RandomRange(0, 5); frq = Mathf.Max(3f, frq + (L - 1) * -0.3333f); }
                    break;
                case 2:
                    if (L <= 6) hpv += M;
                    else if (L <= 21) hpv += M - 12 + 2 * L;
                    else hpv += M - 54 + 4 * L;
                    atk += L - 1; exp += -2 + 2 * L + RandomRange(0, 5); frq = Mathf.Max(1.5f, frq + (L - 1) * -0.25f);
                    break;
                case 3:
                    if (L <= 11) hpv += M - 4 + 4 * L;
                    else if (L <= 21) hpv += 40 + (6 * L - 66) + M;
                    else hpv += 40 + 60 + (15 * L - 315) + M;
                    atk += 5 * L - 5; exp += -6 + 6 * L + RandomRange(0, 5); frq = Mathf.Max(5f, frq + (L - 1) * -0.2f);
                    break;
            }
            atk *= 1f + 0.1f * difficultyStep; hpv *= 1f + 0.1f * difficultyStep; exp = (int)(exp * (1f - 0.05f * difficultyStep));
            d.hp = hpv; d.atk = atk; d.exp = exp; d.frq = frq; d.spd = p.spd;
            // animation set by SKIN: 1 -> k3_*, 2 -> k1_*, 3 -> k2_* (loadDinoANI 0x17818); K5 (pterodactyl) is built below (loadDinoANI '5' block)
            // loadDinoANI 0x17818 keys on the first digit of the skin: 1 -> k3, 2 -> k1, 3 -> k2, 5 -> an2/k5 (XML-variant CA loader, g_scale 1.0).
            string family = d.skin == 1 ? "k3" : d.skin == 2 ? "k1" : d.skin == 3 ? "k2" : d.skin == 5 ? "k5" : null;
            bool k5 = family == "k5";
            if (family == null) return null;
            d.aMove = DinoAnim(family, "move", d.surface, false, false, false);
            d.aAttack = DinoAnim(family, "attack", d.surface, false, false, false);
            d.aAttack.MarkKeyFrame(k5 ? 18 : 12);
            d.aBehited = DinoAnim(family, "behited", d.surface, false, false, !k5);
            // loadDinoANI 0x17818: befired is DinoChangeImage'd with the hit flag like behited, then SetToAdv (0xeefe); bound to dinosaur+212.
            d.aBefired = DinoAnim(family, "befired", d.surface, false, false, !k5); d.aBefired.SetToAdvanced();
            d.aDie = DinoAnim(family, "die", d.surface, false, true, false);
            d.aDie.MarkKeyFrame(17);
            if (d.skin == 1 || d.skin == 2) { d.aJump = DinoAnim(family,"jump",d.surface,false,false,false); d.aJump.MarkKeyFrame(9); }
            int modeRoll = d.skin == 1 || d.skin == 2 ? (DebugSpawnModeRoll != null ? DebugSpawnModeRoll() : RandomRange(0,99)) : 0;
            d.spawnMode = NativeDinosaurSpawn.Mode(d.skin, Profile.level[heroType], modeRoll);
            float wx = WideCombatExtra;   // ADAPTATION WideView combat (0 = native)
            float viewLeft = camLeft - wx, viewRight = camLeft + WorldViewportW + wx; // native Camera::GetViewport width
            bool right;
            if (d.spawnMode == 2)
            {
                Vector2 spawn = NativeDinosaurSpawn.Jump(viewLeft,WorldViewportW + 2f * wx,slot0JumpBounds,facingLeft);
                d.x = spawn.x; d.velocityX = spawn.y; d.faceRight = facingLeft;
            }
            else
            {
                if (d.spawnMode == 3) right = NativeDinosaurSpawn.Mode3Right(Profile.level[heroType],DebugSpawnSideUnit != null ? DebugSpawnSideUnit() : Rand01());
                else if(UsesRecoveredWorldPhase)right=NativeDinosaurSpawn.Mode1Right(HeroBodyVelocity,DebugSpawnSideUnit??new Func<float>(Rand01));
                else if (leftDown) right = false; else if (rightDown) right = true;
                else right = N.Mul32(DebugSpawnSideUnit != null ? DebugSpawnSideUnit() : Rand01(),100f) < 50f;
                d.x = right ? N.Add32(viewRight,N.Mul32(140f,s)) : N.Add32(viewLeft,N.Mul32(-140f,s));
                d.faceRight = !right; d.velocityX = N.Mul32(d.faceRight ? s : -s,d.spd);
            }
            d.root = new GameObject("dino" + kind + "_" + dinos.Count);
            d.root.transform.SetParent(Background.ActorRoot, false);
            // CAObjCallBack DrawOrder (0xaff4): the dinosaur GameObject pool is LIFO (CreateEnemy reuses back(); Disappear 0x119f6 push_back), the creation index is fixed when the pooled object is first created.
            d.drawIndex = dinoDrawPool.Count > 0 ? dinoDrawPool.Pop() : NativeDrawOrder.Allocate();
            var group = d.root.AddComponent<SortingGroup>(); group.sortingOrder = NativeDrawOrder.CaGroup(NativeDrawOrder.Base(d.drawIndex));
            d.rig = new NativeCaPlayer(d.root.transform, _ => d.root.transform, 50);
            d.rig.objectScale = 1f;
            d.rig.Play(d.spawnMode == 2 ? d.aJump : d.aMove, d.spawnMode == 2 ? 0 : -1, 0, 0.025f);
            d.y = NativeDinoSpawnY(d.skin);
            PlaceDino(d); // native SetPosition precedes CAObj emission; never emit at the default origin.
            d.rig.flipX = d.faceRight;
            d.rig.Step(0f);
            d.state = d.spawnMode == 2 ? 7 : 0;
            d.targetable = d.spawnMode != 2;
            d.rig.keyFrame = frame => OnDinoKeyFrame(d);   // dinosaurClass::OnKeyFrame 0x156d4
            d.rig.animationEnd = () => OnDinosaurAnimationEnd(d);
            d.rig.advancedAnimationEnd = () => OnDinosaurAdvancedEnd(d);
            dinos.Add(d); d.targetSeq = ++targetSeqCounter;
            Audio.PlaySound("Audio_enemyAudio"); // CreateEnemy tail 0xd6466 (all spawn modes)
            return d;
        }

        NativeCaAnimation DinoAnim(string family, string name, int surface, bool die2, bool die, bool hit)
        {
            string key = family + name + surface + die2 + die + hit;
            NativeCaAnimation a;
            if (!animCache.TryGetValue(key, out a))
            {
                bool k2type = family == "k2";
                if (family == "k5")
                {
                    // loadDinoANI '5' block: CombineAnimation::LoadXmlFromFile 0xc4da on Image/an2/k5_*.a2b with g_scale 1.0 (iPad), then
                    // DinoChangeImage(surface, die2, die, hit=0, a6=0) executed from the ARM table.
                    a = NativeCaAnimation.Load("DC2/Image/an2/k5_" + name + ".a2b", true, 1f);
                    NativeDinoChangeK5.Apply(a, surface, die2, die, hit, false);
                    if (name == "be") foreach (var p in new[] { "com1", "com2", "com3", "com4", "com5", "com6", "com7", "com8" }) a.SetSpriteAnimation(p);   // loadDinoANI 0x17e.. LABEL_59: v19 (be)
                    animCache[key] = a;
                    return a.Clone();
                }
                a = NativeCaAnimation.Load("DC2/Image/a2b/" + family + "_" + name + ".a2b", false, DinoAnimScale);
                bool basic = surface == 10 || surface == 20 || surface == 30;
                if (!basic)
                {
                    string pre = "k" + surface;
                    if (!k2type)
                    {
                        a.ChangeImage("z1", pre + "zhua"); a.ChangeImage("z2", pre + "zhua");
                        for (int i = 1; i <= 6; i++) a.ChangeImage("t" + i, pre + "t" + i);
                    }
                    else
                    {
                        a.ChangeImage("zhua1", pre + "zhua"); a.ChangeImage("zhua2", pre + "zhua");
                        for (int i = 1; i <= 4; i++) a.ChangeImage("tui" + i, pre + "tui" + i);
                        a.ChangeImage("tui5 ", pre + "tui5"); a.ChangeImage("tui5", pre + "tui5"); a.ChangeImage("tui6", pre + "tui6");
                    }
                    a.ChangeImage("shen", pre + (die2 ? "shen2" : "shen"));
                    a.ChangeImage("tou", pre + (die ? "tou3" : hit ? "tou2" : "tou"));
                    a.ChangeImage("xiaba", pre + "xiaba");
                }
                if (name == "die2") a.SetSpriteAnimation("com2");   // loadDinoANI 0x17cc2 / 0x17dd6 / 0x180ea: SetSpriteAnim(die2, "com2") for k1/k2/k3 (the xue3 blood-spurt part is an animated sprite)
                if (name == "be")   // loadDinoANI SetSpriteAnim(v19,...): the shock x-ray flash parts
                {
                    string[] parts = k2type ? new[] { "shen", "tou", "tui1", "tui2", "tui3", "tui4", "tui5 ", "tui6", "xiaba", "zhua1", "zhua2" }
                                            : new[] { "z1", "z2", "t1", "t2", "t3", "t4", "t5", "t6", "shen", "tou", "xiaba" };
                    foreach (var p in parts) a.SetSpriteAnimation(p);
                }
                animCache[key] = a;
            }
            return a.Clone();
        }

        public static float NativeDinoSpawnY(int skin)
        {
            // lifeMode d5fd0..d6024 / main a2168..a21bc; iPad terms.
            float y = N.Add32(N.Add32(N.Mul32(2f,160f),GroundY),-45f);
            if (skin == 5) y = N.Add32(N.Add32(y,-35f),22f);
            return y;
        }

        // ---- per-frame ----
        void PrepareCombatTimers(float dt)
        {
            // spawn timers are native Application timers now (ArmSpawn / SpawnTimeout, fired in the Timer::Update stage)
            if (invincible && !UsesRecoveredHeroHurt) { invincibleTimer -= dt; if (invincibleTimer <= 0f) invincible = false; }   // legacy non-recovered path
            if (hitStun > 0f) { hitStun -= dt; if (hitStun <= 0f) { firing = false;heroControlState=2;heroVelocityX=0f; rigState = -1; SetRigState(0); } }
        }

        // Skin 3 (K2 family, types 14..19) shares dinosaurClass Move/Attack/OnUpdate/OnAnimationEnd with skins 1/2 (same ARM bodies; only +196==5 differs at 0x11ba4/0xd6008).
        bool UsesRecoveredGroundDinosaur(DinoActor d) { return d.skin==1||d.skin==2||d.skin==3||d.skin==5; }
        // dinosaurClass::OnUpdate 0x11ba4 state-8 ground reset: skin5 140 (S*125+g_ground_y, +45-45), others 165 (S*160+g_ground_y-45).
        public static float NativeDinoStateY(int skin) { return skin==5 ? N.Add32(N.Add32(N.Add32(N.Mul32(2f,125f),GroundY),45f),-45f) : N.Add32(N.Add32(N.Mul32(2f,160f),GroundY),-45f); }

        void PrepareDinosaurPhysics(float dt)
        {
            foreach(var d in dinos)
            {
                if(d.dead||d.removed||!UsesRecoveredGroundDinosaur(d)||d.bodyAsleep)continue;   // b2World::Solve skips sleeping bodies (flag 8)
                // b2Island::Solve integrates every awake body whatever the dinosaurClass state (r118z2; the old 0/1/2/7 gate dropped the first physics step of a
                // spawned dinosaur, which CreateEnemy leaves in state 8 with its spawn velocity in the Timer stage before Physics, and reaction velocities in 4/5/6).
                d.x=N.Add32(d.x,N.Mul32(d.velocityX,dt));
                StepBodySleep(d,dt);
            }
        }

        /// <summary>b2Island::Solve 0x131b24..0x131bf6 (doSleep world, allowSleep body, no contacts/joints, so each body is its own island).
        /// w = 0 for these bodies, so only the linear test applies: |v|^2 > 0x38D1B717 (1e-4) resets m_sleepTime, otherwise m_sleepTime += dt; at
        /// m_sleepTime >= 0x3F000000 (0.5) the body gets flag 8 and v = w = 0. (Angular tolerance^2 is 0x3901742E = 0.000123456789, not stock Box2D.)</summary>
        static void StepBodySleep(DinoActor d,float dt)
        {
            if(N.Mul32(d.velocityX,d.velocityX)>BitConverter.Int32BitsToSingle(0x38D1B717)) {d.bodySleepTime=0f;return;}
            d.bodySleepTime=N.Add32(d.bodySleepTime,dt);
            if(d.bodySleepTime>=0.5f) {d.bodyAsleep=true;d.velocityX=0f;}
        }

        bool itemsUpdatedThisFrame;
        public Action<DinoActor> DebugDinoUpdated;   // DEBUG_ONLY verifier hook (update order)
        void CombatUpdate(float dt)
        {
            if(!nativeWorldPrepared) {PrepareItemPhysics(dt);PrepareBossPhysics(dt);UpdateId0Fx(dt);PrepareCombatTimers(dt);RollUpdate(dt);MeleeUpdate();UpdateFire(dt);}
            // Scene::Update 0x175e84 walks the GameObject multimap<int id> in key order; equal keys keep insertion order (_M_insert_equal). Every dinosaur is
            // CreateGameObject(.., id 2) (CreateEnemy 0xd543c / mainScene 0xa15da) and a pooled object keeps its map slot, so dinosaurs update in the order their
            // GameObjects were first created (drawIndex), and Disappear pushes them back to the pool in that order (r118z L1; the port iterated newest-first).
            var order = new List<DinoActor>(dinos);
            order.Sort((a, b) => a.drawIndex.CompareTo(b.drawIndex));
            foreach (var d in order)
            {
                if (!dinos.Contains(d)) continue;   // already returned by a Die timer
                UpdateDino(d, dt); if (DebugDinoUpdated != null) DebugDinoUpdated(d);
                // dinosaur +192 DahuoAnim object: GameContainer child (BeDamaged 0x11df4, CreateGameObject 0x16b7a0, id 96), updated right after the
                // dinosaur's own callback and only while the dinosaur is enabled (GameObject::Update 0x16c106 -> GameContainer::Update 0x16be94) (r118z2)
                if (d.burnFx != null && !d.disabled && fxList.Contains(d.burnFx)) StepFx(d.burnFx, dt);
                if (d.removed) { HideBurn(d); UnityEngine.Object.Destroy(d.root); dinoDrawPool.Push(d.drawIndex); dinos.Remove(d); }
            }
            // remaining scene-map ids in key order (the CreateGameObject int is the multimap key, GameObject::Id 0x16bf38 = +0x18 set by Initialize), r118z2:
            // 3 bulletClass shells (GetBullet 0x9881e) -> 5 one walk in insertion order (effects, projectiles, effectClassN, bossN, YanJiang) -> 6 blood particleClass
            // -> 10 house. Ids 96 (dahuo) and 97 (beams) are GameContainer children, updated with the dinosaur / hero above, not scene-map keys.
            // SUPERSEDED: r118z order projectiles -> boss -> effects -> blood + shells -> houses -> beams.
            ShellObjects();
            UpdateId5Objects(dt); fxStagePassed = true;
            BloodObjects();
            UpdateHouses(dt);
            if (!nativeWorldPrepared) UpdateBeams(dt);   // legacy frame only; the recovered phase updates them right after the hero
            fxStagePassed = false; beamsStepped = false;
            UpdateWeather(dt);
            if (!itemsUpdatedThisFrame) UpdateItems(dt);
            itemsUpdatedThisFrame = false;
        }

        /// <summary>dinosaurClass::Disappear 0x119f6 from the "Die" timer: runs in the Timer::Update stage, so the pooled object is back (push_back) before any
        /// CreateEnemy of a later timer in the same update can reuse it.</summary>
        void DisappearDino(DinoActor d)
        {
            if (!dinos.Remove(d)) return;
            d.removed = true; HideBurn(d); UnityEngine.Object.Destroy(d.root); dinoDrawPool.Push(d.drawIndex);
        }

        /// <summary>dinosaurClass::Disappear 0x119f6..0x11aac: the +192 DahuoAnim object is disabled and hidden with its dinosaur. r119j: the port left it alive
        /// after the dinosaur was destroyed (a burning dinosaur killed by the flame), no longer stepped, frozen at a stale screen position - the "flame residue that
        /// slides" the user reported.</summary>
        void HideBurn(DinoActor d) { if (d.burnFx != null) { RemoveFx(d.burnFx); d.burnFx = null; } }

        void UpdateDino(DinoActor d, float dt)
        {
            if (d.disabled) return;   // Enable(false); the "Die" timer removes it (DisappearDino)
            if(nativeWorldPrepared&&UsesRecoveredGroundDinosaur(d)) {UpdateRecoveredDinosaur(d,dt);return;}
            float s = N.S;
            if (d.state == 7 && !d.dead)
            {
                d.x = N.Add32(d.x,N.Mul32(d.velocityX,dt)); // native physics before CA emission/key callback.
                d.rig.flipX = d.faceRight; PlaceDino(d); d.rig.Step(dt);
                if (d.state == 8) // end callback ran inside CA; native OnUpdate resumes here in the same tick.
                {
                    d.y = NativeDinoStateY(d.skin); d.rig.objectScale = 1f;
                    if (CheckAttack(d,heroX)) StartAttack(d);
                    else { d.state = 0; d.rig.Play(d.aMove,-1,0,0.025f); }
                }
                return;
            }
            if (d.dead)
            {
                PlaceDino(d);
                d.rig.Step(dt);
                if (d.rig.Ended) d.removed = true;
                return;
            }
            float dx = heroX - d.x;
            if (d.state == 4)
            {
                d.hitTimer -= dt;
                if (d.hitTimer <= 0f) { d.state = 0; d.rig.Play(d.aMove, -1, 0, 0.025f); }
            }
            else if (d.state == 1)
            {
                // attack animation runs once; keyframe 12 -> BeAttack when still in range (OnKeyFrame 0x156d4)
                if (d.rig.Ended) { d.state = 0; d.rig.Play(d.aMove, -1, 0, 0.025f); }
            }
            else
            {
                // Move 0x11784: face and walk toward the player at SPD*s
                d.faceRight = dx > 0f ? true : dx < 0f ? false : d.faceRight;
                d.x += (d.faceRight ? 1f : -1f) * d.spd * s * dt;
                if (CheckAttack(d, heroX)) StartAttack(d);
            }
            d.rig.flipX = d.faceRight;                     // Move118d8 writes CA flag96=(velocity.x>0); CA draw consumes that flag directly.
            PlaceDino(d);
            d.rig.Step(dt); // physics/world position is established before object callback rendering.
        }

        void MoveRecoveredDinosaur(DinoActor d)
        {
            if(d.state==2)return;
            if(d.state!=0) {d.state=0;d.rig.Play(d.aMove,-1,0,.025f);d.rig.SetAdvanced(null);d.advActive=false;} // Move 0x11784 setAnimation/setAnimationAdv(null)
            if(d.advActive)return; // native compares the advanced-name string with \"BefiredAnim\" and leaves the hit velocity alone
            float dx=N.Add32(heroX,-d.x);
            if(HeroTargetable)   // Move 0x11840: chase only a targetable hero, else keep walking in the facing direction
            {
                if(dx>0f)d.velocityX=N.Mul32(N.S,d.spd);
                else if(dx<0f)d.velocityX=N.Mul32(-N.S,d.spd);
            }
            else d.velocityX=N.Mul32(d.faceRight?N.S:-N.S,d.spd);
            d.faceRight=d.velocityX>0f;
            d.WakeUp();   // Move 0x118e6: WakeUp then SetLinearVelocity on every path past the state-2 / BefiredAnim returns
        }

        void OnDinosaurAnimationEnd(DinoActor d)
        {
            if(d.state==7) {d.state=8;return;}
            if(!nativeWorldPrepared||!UsesRecoveredGroundDinosaur(d))return;
            if(d.state==1)
            {
                if(CheckAttack(d,heroX))d.rig.Play(d.aAttack,0,0,.025f);
                else MoveRecoveredDinosaur(d);
            }
            else if(d.state==2||d.state==6) d.state=8; // OnAnimationEnd 0x11ad6 cases 1/5/6 (state 7 handled above)
            else if(d.state==3) {d.removed=true;d.rig.Hide();}
            else if(d.state==4&&!d.legacyHit)
            {   // r119j2 (user: "laser makes dinos twitch up but not down and freeze, a 2nd firing kills"): OnAnimationEnd 0x11ad6 case 3 (state 4): CAObjCallBack+68 is the
                // CURRENT frame; == 4 (the BEAnim hold bound set by BeDamaged 0x11d84) -> playAnimation(0, 2, 0.025, 4): frames 2..4 loop (the twitch), and every pass of key
                // frame 3 deals the laser record (OnKeyFrame 0x15768, BeAttacked). Otherwise (BeDamagedOver's 4..end tail) -> state 8. The port had no state-4 branch: the
                // dinosaur froze on frame 4 after one key-frame hit. The legacy hit-timer reaction (legacyHit) keeps its own PILOT_ASSUMPTION path.
                if(d.rig.CurrentFrame==4) d.rig.Play(d.rig.animation,0,2,.025f,4);
                else d.state=8;
            }
        }

        /// <summary>dinosaurClass::OnAnimationEndAdv 0x1170e.</summary>
        void OnDinosaurAdvancedEnd(DinoActor d)
        {
            if(d.dead||d.removed)return;
            if(CheckAttack(d,heroX)) {if(d.state==0||d.state==6||d.state==8)StartAttack(d);}
            else d.velocityX=d.velocityX>0f?d.spd:-d.spd; // raw property SPD (no RectScale factor), sign from old vx; zero follows the negative branch
            d.rig.SetAdvanced(null);d.advActive=false;d.rig.SetInterval(.025f);
        }

        /// <summary>NATIVE non-lethal reaction (r94/r96 ARM oracle). Returns false when the weapon/skin is outside the recovered ground-dinosaur path.</summary>
        public void ApplyDamageForTest(DinoActor d){ if(!d.dead) DinoDie(d); } // DEBUG_ONLY verifier hook
        public bool ApplyReactionForTest(DinoActor d,WeaponDef w){return ApplyNativeDinoReaction(d,w);}
        public void StepDinoPhysicsForTest(float dt){PrepareDinosaurPhysics(dt);}   // DEBUG_ONLY verifier hook (physics stage alone)
        public void MoveDinoForTest(DinoActor d){MoveRecoveredDinosaur(d);}         // DEBUG_ONLY verifier hook
        public WeaponDef MeleeWeaponForTest(){return MeleeWeapon();} // DEBUG_ONLY verifier hook
        public bool ApplyMeleeReactionForTest(DinoActor d,WeaponDef w){return ApplyNativeMeleeReaction(d,w);} // DEBUG_ONLY verifier hook // DEBUG_ONLY verifier hook

        bool ApplyNativeDinoReaction(DinoActor d,WeaponDef w)
        {
            if(!UsesRecoveredGroundDinosaur(d))return false;
            var r=NativeDinoReaction.Evaluate(w.type,w.index,d.skin,d.state,d.faceRight);
            if(!r.found)return false;
            if(r.advanced)
            {
                d.rig.SetAdvanced(d.aBefired);d.rig.PlayAdvanced(0,0,r.advInterval,-1);d.advActive=true;
                d.velocityX=N.Mul32(r.velocitySpdScale,d.spd);
                if(r.hasBaseDelta)d.rig.SetInterval(r.baseDelta);
            }
            else if(r.basePose)
            {
                d.rig.Play(d.aBehited,0,0,r.baseInterval);d.rig.SetAdvanced(null);d.advActive=false;
                d.WakeUp();   // BeAttacked 0x15082 / 0x152e2 / 0x153f8: every BehitedAnim branch wakes; BefiredAnim branches do not
                d.velocityX=r.velocityAbsolute;
            }
            if(r.sound!=null)Audio.PlaySound(r.sound);
            for(int b=0;b<r.blast;b++)DinoBlastBlood(d);   // BeAttacked tail: BlastBlood x3 then Blood(0) (qx effect)
            // dinosaurClass::Blood 0x12a46 from the reaction tail (0x15650): every sampled gun hit (types 0-3) calls Blood(0) (the spiky qx effect), types 4/5/6
            // call Blood(1) (dx). DC2-BUGS-001 B5 (r118z8): the table stored the argument and the port tested != 0, so Blood(0) never drew and Blood(1) drew as qx.
            if(r.blood>=0)DinoBlood(d,r.blood);
            d.state=r.stateAfter;
            return true;
        }

        /// <summary>dinosaurClass::BeJinAttacked 0x1442a non-lethal tail 0x14668..0x148a2 (weapon type 6 only), read from ARM:
        /// skin != 3: BehitedAnim played once at 0.05, velocity (facing flag ? -200*S : +200*S), flag160=1, state=2;
        /// skin 3 and state 0: BefiredAnim advanced layer at 0.025, velocity +-0.5*S*SPD (flag ? + : -), base interval 0.1 (state unchanged);
        /// both: BlastBlood x3, sound by subtype (0,1 -> att1; 2,3 -> att2; else none), Blood(1) (dx effect) - visuals implemented in GameBlood / NativeBlood.</summary>
        bool ApplyNativeMeleeReaction(DinoActor d,WeaponDef w)
        {
            if(!UsesRecoveredGroundDinosaur(d)||w.type!=6)return false;
            if(d.skin!=3)
            {
                d.rig.Play(d.aBehited,0,0,0.05f);d.rig.SetAdvanced(null);d.advActive=false;
                d.WakeUp();   // BeJinAttacked 0x146b4 (skin != 3 branch only)
                d.velocityX=N.Mul32(N.S,d.faceRight?-200f:200f);
                d.state=2;
            }
            else if(d.state==0)
            {
                d.rig.SetAdvanced(d.aBefired);d.rig.PlayAdvanced(0,0,0.025f,-1);d.advActive=true;
                d.velocityX=N.Mul32(N.Mul32(N.S,d.spd),d.faceRight?0.5f:-0.5f);
                d.rig.SetInterval(0.1f);
            }
            if(w.index==0||w.index==1)Audio.PlaySound("Audio_att1Audio");
            else if(w.index==2||w.index==3)Audio.PlaySound("Audio_att2Audio");
            return true;
        }

        void UpdateRecoveredDinosaur(DinoActor d,float dt)
        {
            d.rig.flipX=d.faceRight;PlaceDino(d);d.rig.Step(dt);
            if(d.removed||d.dead)return;
            if(d.state==4&&d.legacyHit)
            {
                // Existing PILOT_ASSUMPTION: hit reaction timer/knockback is a separate OPEN lifecycle.
                d.hitTimer-=dt;
                if(d.hitTimer<=0f) {d.state=0;d.rig.Play(d.aMove,-1,0,.025f);}
                return;
            }
            if(d.state>=3&&d.state<=7)return;
            if(d.state==8) {d.y=NativeDinoStateY(d.skin);d.rig.objectScale=1f;}
            if(d.x<N.Add32(camLeft,-500f-WideCombatExtra)||d.x>N.Add32(N.Add32(camLeft,WorldViewportW),500f+WideCombatExtra))   // + WideView combat extra (ADAPTATION; 0 = native)
            {
                // Native Disappear hides/disables and pools. Pool reuse/state retention remains OPEN in this pilot.
                d.removed=true;d.rig.Hide();return;
            }
            if(d.state!=1)
            {
                if(CheckAttack(d,heroX)){if(d.state==0||d.state==6||d.state==8)StartAttack(d);} // Attack 0x109dc state gate
                else MoveRecoveredDinosaur(d);
            }
        }

        void PlaceDino(DinoActor d)
        {
            d.rig.position = new Vector2(d.x - (camLeft + WorldViewportW * 0.5f), d.y);
        }

        /// <summary>dinosaurClass::CheckAttack 0x10b94 / dinosaurClass::Move 0x11840..0x11894: the hero is a target only while its state is not 4 (dead) or 6 and
        /// +657 (girl +356) != 0 and +664 (girl +372) == 0. +664/+372 are only ever written by OnLoad (= 0; exhaustive ARM store scan r118w), state 6 only by the
        /// unreachable EnterHouse, so the live gate is "alive and +657 set"; GameClear / EggFull clear +657.</summary>
        bool HeroTargetable { get { return !heroDead && heroInputEnabled; } }
        public bool ignorePlayer;   // DEBUG_ONLY verifier fixture: dinosaurs never start an attack (not set in play; the native completion gate is heroInputEnabled)
        bool CheckAttack(DinoActor d, float playerX)
        {
            if (!HeroTargetable || ignorePlayer) return false;
            float dx = playerX - d.x;
            if (d.faceRight) return dx < d.atkDis && dx > 0f;
            return dx > -d.atkDis && dx < 0f;
        }

        void StartAttack(DinoActor d)
        {
            d.velocityX=0f; // Attack10a34 stops native physics before the next world step.
            d.state = 1; d.attackHit = false;
            d.rig.Play(d.aAttack, 0, 0, 0.025f);
        }

        /// <summary>playerClass::BeAttack 0xbd166: HP -= (int)dino ATK; invincibility 1.5 s ("Wudi"); behit pose; Die at HP<=0.</summary>
        public void BeAttack(DinoActor d) { HeroHurtFrom(d.atk, d.x, 1.5f); }

        /// <summary>playerClass::BeAttack 0xbd166 / BeAttackByBoss 0xbd3c8 shared hurt stage; the boss variant passes a different damage and Wudi duration.</summary>
        void HeroHurtFrom(float attack, float attackerX, float wudiSeconds)
        {
            // DEBUG_ONLY god mode (debug menu, user): the hit runs normally (knockback, hurt animation, invincibility) but HP is not reduced
            if(UsesRecoveredHeroHurt)
            {
                int state=heroDead?4:rolling?7:nativeHurting?5:meleeing?3:heroControlState;
                var hit=NativeHeroHurt.Hit(state,invincible,playerHp,attack,N.Add32(attackerX,-heroX),facingLeft);
                if(!hit.accepted)return;
                playerHp=Game.DebugGodMode?playerHp:hit.hp;UpdateHpBar();
                EggHeroHit();                                       // BeAttack 0xbd23a/0xbd25c: carried egg breaks on any accepted hit
                if(hit.dead&&!Game.DebugGodMode){HeroDie();return;}
                ClearRageIfFiring();ClearNativeFireChannel();nativeHurting=true;hitStun=0f;firing=false;meleeing=false;heroControlState=5;
                heroVelocityX=hit.velocity;facingLeft=hit.facingLeft;nativeCamera.turning|=hit.turning;
                invincible=true;SetTimer(heroTimer,"Wudi",wudiSeconds,WudiEnd);   // BeAttack: SetTimer("Wudi",1.5); OnTimeout Wudi clears +invincible
                LastHurtResumeRequest=null;
                if (IsGirl)
                {
                    GirlBehit();   // playerNewAniClass::BeAttack 0x1282b8: behit_short/long, no key callback, end -> OnAnimationEnd state 5
                    Audio.PlaySound(RandomRange(0, 100) > 49 ? "p5behit2" : "p5behit1");
                    return;
                }
                Rig.SelectNativeHurt(heroType,weaponSlot);Rig.Player.objectScale=HeroRigScale;
                Rig.Player.keyFrame=f=>{if(nativeHurting&&heroControlState==5)heroVelocityX=0f;};
                Rig.Player.animationEnd=EndNativeHeroHurt;
                Audio.PlaySound("Audio_RoleAudio");
                return;
            }
            if (invincible || heroDead || rolling) return;               // states 4..7 ignore BeAttack (roll = 7)
            if (!Game.DebugGodMode) playerHp -= Mathf.Floor(attack);               // (int)ATK; DEBUG_ONLY god mode keeps HP
            EggHeroHit();
            invincible = true; invincibleTimer = wudiSeconds;
            if (playerHp <= 0f) { playerHp = 0f; UpdateHpBar(); HeroDie(); return; }
            UpdateHpBar();
            ClearRageIfFiring(); ClearNativeFireChannel(); firing = false; hitStun = 0.4f;                 // PILOT_ASSUMPTION: behit pose duration (state 5 end not yet traced)
            heroVelocityX=0f;heroControlState=5; // Existing unmodelled native hurt knockback remains OPEN.
            Rig.SelectPose(heroType, "behit", 0, 0.025f, 7);
            Rig.Player.objectScale = HeroRigScale;
            // FALSIFIED old -150 comment: native bd2ee/bd328 uses +100/-100, key callback stops it.
            // Native hurt binding/end/interrupt replacement remains OPEN; see report.r44-r46.
        }

        void EndNativeHeroHurt()
        {
            if(!nativeHurting||heroControlState!=5)return;
            nativeHurting=false;
            var control=NativeHeroControl.UpdateControl(5,facingLeft,leftDown,rightDown,WalkSpeedFor(heroType));
            facingLeft=control.facingLeft;nativeCamera.turning|=control.turning;
            if(control.hasVelocity)heroVelocityX=control.velocity;
            heroControlState=control.state;rigState=-1;SetRigState(control.state==2?0:1,true);
            LastHurtResumeRequest=hitHeld?"OnHitDown":fireDown?"OnFireDown":"none";
            if(hitHeld)StartMelee(true);
            else if(fireDown)UpdateFire(0f,true); // dispatch into existing pilot firing vessel; full advanced fire remains OPEN.
        }

        void HeroDie()
        {
            nativeHurting=false;hitStun=0f;
            ClearNativeFireChannel(); heroDead = true; firing = false;
            heroVelocityX=0f;heroControlState=4;
            if (IsGirl)
            {
                Vector2 lp; NativeCompObj.SpriteInfo li; bool have = GirlSpriteWorld("lian", out lp, out li);
                Audio.PlaySound("P5dieAudio"); GirlDie();                       // playerNewAniClass::Die 0x126a36
                GirlDieBlood(have ? lp : Vector2.zero);
                return;
            }
            Vector2 bp = BloodHeroPoint();                                       // getSpritePos("yyj6") before the die pose replaces the rig
            Audio.PlaySound("RoledieAudio"); // playerClass::Die 0xbcee4
            Rig.SelectDie(heroType);   // playerClass::Die 0xbcee4: p_die.a2b + PlayerChangeHead(9 + type)
            HeroDieBlood(bp);
            Rig.Player.objectScale = HeroRigScale;
        }

        // ---- weapon fire ----
        public WeaponDef CurrentWeapon()
        {
            var slot = Dc2GameData.PlayerWeapons(heroType)[weaponSlot];
            return Dc2GameData.Weapons()[slot.type][slot.subtype];
        }
        public int CurrentAmmoIndex { get { return heroType * 6 + weaponSlot; } }

        public void FireDown() { fireDown = true; }
        public void FireUp() { fireDown = false; }

        void UpdateFire(float dt,bool preserveEmission=false)
        {
            NativeFireUpdate(dt);   // GameFire.cs: OnFireDown/OnKeyFrameAdv/OnAnimationEndAdv lifecycle (Girl via GameGirl.cs)
        }

        /// <summary>playerClass::Attack 0xbf784 + dinosaurClass::BeAttacked 0x14918 (damage), simplified candidate scan.</summary>
        void HeroAttack()
        {
            var w = CurrentWeapon();
            float range = w.atkDis * N.S;
            var targets = new List<DinoActor>();
            DinoActor nearest = null; float best = float.MaxValue;
            foreach (var d in dinos)
            {
                if (d.dead || !d.targetable) continue;
                float dx = MeleeDx(d);   // r119f: playerClass::Attack 0xbf92c / 0xbf942 measures to the head sprite ("tou", "com6" for skin 5), like JinAttack
                if (!(dx > 0f && dx < range)) continue;   // no viewport filter here: BeAttacked rejects an off-screen target (the nearest still consumes the shot)
                targets.Add(d);
                if (dx < best) { best = dx; nearest = d; }
            }
            float bossDelta; bool bossHit = BossAttackCandidate(w, out bossDelta);   // playerClass::Attack 0xbf784 boss branch
            bool armored = false;   // v47: dinosaurClass+156 (SURFACE) > 100 after BeAttacked
            if (w.group) { foreach (var d in targets) { BeAttacked(d, w, WideFalloffDistance(Mathf.Abs(heroX - d.x))); if (d.surface > 100) armored = true; } if (bossHit) BossBeAttacked(w); }
            else if (bossHit && !BossCheckAttackState(Boss) && (nearest == null || bossDelta < best)) BossBeAttacked(w);
            else if (nearest != null) { BeAttacked(nearest, w, WideFalloffDistance(Mathf.Abs(heroX - nearest.x)));   // R002-B2: selection uses the head (best), damage the origins: BeAttacked 0x1495a..0x14966 / 0x14ef0..0x14efa dino x - hero x
                if (nearest.surface > 100) armored = true; }
            if (armored && (int)(Rand01() * 100f) <= 49) Audio.PlaySound("A_shot_tie");   // playerClass::Attack 0xbfdae..0xbfdd6
        }

        /// <summary>ADAPTATION (WideView combat, user decision "native edge damage"): the type-1 falloff (|d|/ATK_DIS)*-2+3 (dinosaurClass::BeAttacked 0x14918)
        /// reaches 0 at 1.5*ATK_DIS (720 px for the 480 guns) and goes negative beyond; on the iPad no hittable dinosaur is farther than the native view edge
        /// (camLeft .. camLeft + WorldViewportW, the 1024 viewport). In wide view a target beyond that edge takes the damage of a target at the edge. The hard reach stays native:
        /// the candidate scan still drops anything at or beyond ATK_DIS*S (960 px), so very far dinosaurs (e.g. on 48:9) are not hit. 0 extra = native.</summary>
        float WideFalloffDistance(float distance)
        {
            if (WideCombatExtra <= 0f) return distance;
            float edge = facingLeft ? N.Add32(heroX, -camLeft) : N.Add32(N.Add32(camLeft, WorldViewportW), -heroX);   // R002-B5: the native view is the 1024 viewport (0x1498a..0x149ba), not the 960 camera-follow width (FALSIFIED: ViewW)
            return edge > 0f && distance > edge ? edge : distance;
        }
        public bool InViewportForTest(DinoActor d) { return InViewport(d); }   // DEBUG_ONLY verifier probe
        public void JinAttackForTest() { JinAttack(); }                           // DEBUG_ONLY verifier probe
        public float HeadXForTest(DinoActor d) { return DinoPartWorld(d, DinoHeadPart(d), Vector2.zero).x; }   // DEBUG_ONLY verifier probe
        /// <summary>r119f (user item 6, dinos near the left edge not hit): dinosaurClass::BeAttacked 0x1498a..0x149ba and BeJinAttacked 0x144b8..0x144e2 reject a dinosaur
        /// only when x &gt; viewport.x + viewport.w + anim+0x14 or x &lt; viewport.x + anim+0x10, anim = the current CombineAnimation (CAObj+8, set by setAnimation 0xaea0),
        /// i.e. its cached left/right extents; viewport.w is the 1024 camera width. FALSIFIED: the port tested camLeft .. camLeft + 960 on the origin without the
        /// extents, so a dinosaur partly on screen near an edge could not be hit (and the "64 px right-edge dead zone" note was an artefact of that). Plus the
        /// WideView combat extra (ADAPTATION; 0 = native).</summary>
        bool InViewport(DinoActor d)
        {
            float wx = WideCombatExtra;
            var a = d.rig != null ? d.rig.animation : null;
            float minX = a != null ? a.minX : 0f, maxX = a != null ? a.maxX : 0f;
            if (d.x > N.Add32(N.Add32(N.Add32(camLeft, WorldViewportW), maxX), wx)) return false;
            if (d.x < N.Add32(N.Add32(camLeft, minX), -wx)) return false;
            return true;
        }

        /// <summary>Girl level for GetPlayer4Atk (dword_240494[g_player_type]), -1 for the male heroes.</summary>
        int GirlDamageLevel { get { return heroType == 4 ? Profile.level[4] : -1; } }

        /// <summary>Damage per dinosaurClass::BeAttacked 0x14918 (all heroes; the girl's basis goes through GetPlayer4Atk 0x193b8).</summary>
        public float WeaponDamage(WeaponDef w, int level, float distance, bool critRoll)
        {
            float damage=NativeWeaponDamage.Base(w,level,Profile.jigsaw2Count,distance,Rand01(),GirlDamageLevel);
            return NativeWeaponDamage.ApplyCritical(damage,critRoll);
        }

        void BeAttacked(DinoActor d, WeaponDef w, float distance)
        {
            if (!InViewport(d)) return;   // BeAttacked 0x1498a..0x149ba viewport gate (before any rand or effect)
            int level = Profile.weaponLevels[CurrentAmmoIndex];
            float dmg; bool crit;
            {
                // Native 0x14918 consumes the damage rand before the critical RandomRange, for every hero (the old girl branch drew the critical roll first: FALSIFIED order).
                float raw=NativeWeaponDamage.Base(w,level,Profile.jigsaw2Count,distance,Rand01(),GirlDamageLevel);
                crit=NativeWeaponDamage.IsCritical(RandomRange(0,10000),w.crit);
                dmg=NativeWeaponDamage.ApplyCritical(raw,crit);
            }
            hitDino++;
            if (crit) { DinoBlastBlood(d); DinoBlastBlood(d); DinoBlastBlood(d); }   // BeAttacked 0x14f4a: critical hit -> BlastBlood x3
            d.hp=N.Add32(d.hp,-dmg); // native binary32 HP subtraction14f6e.
            if (d.hp <= 0f) { DinoDie(d, w); return; }
            if (w.type == 5 && (w.index == 1 || w.index == 2 || w.index == 4)) return;   // oracle: flame/laser/energy have no hit reaction (BeDamaged owns their states)
            // FALSIFIED generic reaction: native r94 has weapon/field/state branches,
            // advanced Befired or base Behited, retained state or state2, and knockback.
            // Existing PILOT_ASSUMPTION remains until binding/callback/AI composition closes.
            if(ApplyNativeDinoReaction(d,w))return;
            d.state = 4; d.hitTimer = 0.25f; d.legacyHit = true;
            d.velocityX=0f; // FALSIFIED stand-in for skins outside the recovered ground path (OPEN).
            d.rig.Play(d.aBehited, 0, 0, 0.05f);
        }

        NativeCaAnimation shaosiAnim;
        /// <summary>dinosaurClass::Die 0x140fc weapon dispatch (type 5 subtype bit 0x16 = subtypes 1,2,4 -> DieShaoSi 0x10a4c).
        /// Other dispatch targets: DieTouFei 0x10cbc, BlastSuiRou 0x1226c and DieNormal 0x12ca8 are implemented in DinoDie + NativeBlood (r118v: old OPEN note stale).</summary>
        static bool BurnDeath(WeaponDef w) { return w != null && w.type == 5 && ((1 << w.index) & 0x16) != 0 && w.index <= 5; }

        /// <summary>DieShaoSi 0x10a4c: g_ca_shaosiData "ShaoSiAnim" with per-skin objectScale/position: y = S*160*0.8 + g_ground_y (0x10a8a 0x43200000, 0x3F4CCCCD),
        /// iPad branch y -= 70 (0x10b22 0x428C0000). r119w FALSIFIED: the port used 150 and 72 (ash 18 px too low on iPad; user report).</summary>
        void DieShaoSi(DinoActor d)
        {
            float s = N.S;
            float y = N.Add32(N.Mul32(N.Mul32(s, 160f), 0.8f), GroundY);
            switch (d.skin)
            {
                case 1: case 2: d.rig.objectScale = 0.8f; d.y = N.Add32(y, -70f); break;
                case 3: d.rig.objectScale = 1f; break;
                case 5: d.rig.objectScale = 0.8f; d.x = d.faceRight ? N.Add32(d.x, -40f) : N.Add32(d.x, 40f); d.y = N.Add32(y, -70f); break;   // +0x60 flag != 0 -> x - 40
            }
            if (shaosiAnim == null) shaosiAnim = NativeCaAnimation.Load("DC2/Image/a2b/shaosi.a2b", false, 1f);   // loader case 27, LoadFromFile with g_scale 1 (iPad)
            d.rig.SetAdvanced(null); d.advActive = false;
            d.rig.Play(shaosiAnim, 0, 0, 0.025f);
        }

        void DinoDie(DinoActor d, WeaponDef w = null)
        {
            MaybeDropItem(d.x);   // dinosaurClass::Die 0x140fc: arena/boss only
            d.dead = true; d.state = 3; // native death state; interrupts Jump's state7 end callback.
            int dk = DieKindOf(w);
            if (dk == 2 && UsesRecoveredGroundDinosaur(d)) DieShaoSi(d);
            else if (dk == 3) DinoDieBlast(d);
            else if (dk == 1) DinoDieTouFei(d);
            else if (dk == 0)
            {
                // DieNormal 0x12ca8: setAnimation(DieAnim) 0x12d12, then setAnimationAdv(nullptr, "") 0x12d1e - the BefiredAnim layer of a hit reaction is
                // cleared, so the die animation's own head (k?tou3, the meat head in k1/k2/k3_die.a2b) shows. DC2-BUGS-001 B4 (r118z8): the port kept the
                // advanced layer, so a dinosaur killed during its reaction kept the hurt face (tou2) until it despawned.
                d.rig.Play(d.aDie, 0, 0, 0.025f);
                d.rig.SetAdvanced(null); d.advActive = false;
                DinoDieNormalBlood(d);   // then the gore pieces and 3 BlastBlood
            }
            else
            {
                // Die 0x140fc LABEL_25 without a Die* call (type 5/6 subtype outside every mask): playAnimation(0, 0, 0.025) on the CURRENT animation, the
                // advanced layer untouched (shipped quirk; the port played the die animation here).
                d.rig.Play(d.rig.animation, 0, 0, 0.025f);
            }
            DieSound(d, w);
            OnDinoKilled(d);
        }

        /// <summary>dinosaurClass::Die 0x140fc dispatch sounds: DieNormal 0x12ca8 (surface > 100: 50% A_die_tie_qiang else Audio_death1Audio), DieTouFei 0x10cbc Audio_death2Audio;
        /// DieShaoSi / BlastSuiRou (type 4) and weapon records with no matching mask play nothing here (the key-frame death3 sound is in OnDinoKeyFrame).</summary>
        void DieSound(DinoActor d, WeaponDef w)
        {
            int kind;   // 0 normal, 1 toufei, 2 none
            if (w == null) kind = 0;
            else switch (w.type)
            {
                case 4: kind = 2; break;
                case 5: { int m = w.index <= 5 ? (1 << w.index) : 0; kind = (m & 0x16) != 0 ? 2 : (m & 9) != 0 ? 1 : (m & 0x20) != 0 ? 0 : 2; break; }
                case 6: { int m = w.index <= 6 ? (1 << w.index) : 0; kind = (m & 0x43) != 0 ? 0 : (m & 0x3C) != 0 ? 1 : 2; break; }
                default: kind = 0; break;
            }
            if (kind == 0) Audio.PlaySound(d.surface > 100 && RandomRange(0, 100) <= 49 ? "A_die_tie_qiang" : "Audio_death1Audio");
            else if (kind == 1) Audio.PlaySound("Audio_death2Audio");
        }

        public bool DieShaoSiForTest(DinoActor d, WeaponDef w) { bool b = BurnDeath(w); DinoDie(d, w); return b; }   // DEBUG_ONLY
    }
}
