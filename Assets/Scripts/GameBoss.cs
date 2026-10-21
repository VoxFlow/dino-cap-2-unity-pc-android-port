using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Dc2Pilot
{
    /// <summary>bossN state (Dev/Evidence/boss_decomp_r105.md). Field comments give the native offsets.</summary>
    public sealed class BossActor
    {
        public int slot;                        // bossN GameObject (AddBoss 0xd1256, id 5): position in the id-5 walk
        public int targetSeq;                   // AddBoss tail: push_back into mainScene+8 (after every dinosaur alive at that moment)
        public GameObject root;
        public NativeComponentPlayer ca;
        public BossDef def;
        public int lineType;                    // g_boss_line_type
        public int kind;                        // +456: 1 hou, 2 jiao, 3 wei, 4 dying
        public int sub;                         // +460 sub-state
        public int counter;                     // +392 repeat counter
        public bool left;                       // +223 side flag
        public bool visitedIdle;                // +448
        public bool dying;                      // +450
        public float x, y, velX;
        public float hp, maxHp, exp, money;     // g_boss[line].hp/+4/+8 and lifeModeScene+648
        public bool recoil, recoilSaved;        // +424, +432
        public float recoilAcc, recoilLimit, recoilX;   // +436, +428, +440
        public bool shaking; public float shakeAcc, shakeDur;   // +396, +404, +400
    }

    /// <summary>effectClassN instance used by the boss (lava wave, flame hit).</summary>
    public sealed class BossFx
    {
        public GameObject root;
        public NativeComponentPlayer ca;
        public int type;                        // +376: 5 = Yanjiang lava wave (damages the hero), 0 = plain one-shot
        public int slot; public bool skipFirstUpdate;   // YanJiang 0x120234 creates a new id-5 object: it waits in the pending set until next tick
        public float x, y;
        public bool dead;
    }

    /// <summary>Mission type 8: lifeModeScene::AddBoss + bossN (OnIdle/OnUpdate/Attack/MoveOut/BossDie/BeAttacked).</summary>
    public partial class GameScene
    {
        public BossActor Boss { get; private set; }
        readonly List<BossFx> bossFx = new List<BossFx>();
        readonly Dictionary<string, NativeComponentAnimation> bossAnim = new Dictionary<string, NativeComponentAnimation>();
        UiControl bossBloodBg, bossBloodBar;
        bool bossAssetsLoaded;
        public int BossHitsTaken;               // DEBUG_ONLY counter (verifier)
        public int BossHeroHits;                // DEBUG_ONLY counter (verifier)
        public Func<int> DebugBossKindRoll;     // DEBUG_ONLY: replaces RandomRange(1,n) of OnIdle

        static readonly string[] bossProp = { "time1min", "time1max", "time2min", "time2max", "changetimemin", "changetimemax", "speed", "atk", "probase", "prosub", "dinonummin", "dinonummax", "restmin", "restmax" };

        int BossLine { get { return Mathf.Clamp((Game.LaunchMission != null ? Game.LaunchMission.level : 1) - 1, 0, Dc2GameData.Bosses().Count - 1); } }

        float BossP(BossActor b, int index)
        {
            float v;
            return b.def.anis[b.lineType].attr.TryGetValue(bossProp[index], out v) ? v : 0f;   // bossAniProperty float table; absent attributes keep 0
        }
        float BossRnd(float lo, float hi) { return N.Add32(lo, N.Mul32(Rand01(), N.Add32(hi, -lo))); }   // lo+rand()*2^-31*(hi-lo)

        // ---- load (lifeModeScene::OnLoad type 8) ----
        void BossLoad()
        {
            if (missionType != 8) return;
            if (!bossAssetsLoaded)
            {
                // OnLoad e9ebc: Boss2x, Boss2x_1, Boss2x_2 on the iPad branch; huoyan atlas for BossShao.
                Atlas.LoadByConf("DC2/Image/ip/Boss2x", "DC2/Image/ip/Boss2x.conf");
                Atlas.LoadByConf("DC2/Image/ip/Boss2x_1", "DC2/Image/ip/Boss2x_1.conf");
                Atlas.LoadByConf("DC2/Image/ip/Boss2x_2", "DC2/Image/ip/Boss2x_2.conf");
                Atlas.LoadByConf("DC2/Image/ip/huoyan2x", "DC2/Image/ip/huoyan2x.conf");
                bossAssetsLoaded = true;
            }
            // sub_EA32C: ids 50/51 created untouchable; bar rect = bloodBGRect.x+45,y+7, width 110 (full), height 10.
            bossBloodBg = CreateControl(50, 1); bossBloodBg.SetTexture("boss_blood_bgFrame"); bossBloodBg.touchable = false;
            bossBloodBg.SetRect(N.Layout(267f, 2f, 160f, 39f));
            bossBloodBar = CreateControl(51, 1); bossBloodBar.SetTexture("boss_bloodFrame"); bossBloodBar.touchable = false;
            bossBloodBar.SetRect(N.Layout(N.Add32(267f, 45f), N.Add32(2f, 7f), 110f, 10f));
            SetTimer("AddBoss", 2f);            // sub_F0EB6: Scene::SetTimer("AddBoss",2.0,false) when mission type == 8
        }

        NativeComponentAnimation BossAcb(string name)
        {
            NativeComponentAnimation a;
            if (!bossAnim.TryGetValue(name, out a))
            {
                a = NativeComponentAnimation.Load("DC2/Image/acb/ipad/" + name + ".animation.acb");
                bossAnim[name] = a;
            }
            return a;
        }

        void BossSetAnim(BossActor b, string acb)
        {
            var a = BossAcb(acb);
            if (b.ca == null) b.ca = new NativeComponentPlayer(a, b.root.transform, BossRigPos(b), 1f);
            else b.ca.SetAnimation(a);
        }
        void BossPlay(BossActor b, int loops) { b.ca.position = BossRigPos(b); b.ca.Play(loops); }
        Vector2 BossRigPos(BossActor b) { return new Vector2(b.x - (camLeft + WorldViewportW * 0.5f), b.y); }

        /// <summary>AddBoss 0xd105a hp: L&lt;=9 base+10L; 10..14 base+25L+50; &gt;=15 base+30L+50 (50.0 = 0x42480000... literal 1112014848).</summary>
        public static float BossHpFor(BossDef def, int L)
        {
            if (L > 9) return N.Add32(N.Add32(N.F32((uint)(L - 10) > 4u ? 30 * L : 25 * L), def.hp), 50f);
            return N.Add32(def.hp, N.F32(10 * L));
        }

        // DEBUG_ONLY verifier hooks
        public void HeroAttackForTest() { HeroAttack(); }
        public void BossBeAttackedForTest(WeaponDef w) { BossBeAttacked(w); }
        public bool BossCandidateForTest(WeaponDef w, out float delta) { return BossAttackCandidate(w, out delta); }

        // ---- lifeModeScene::AddBoss 0xd105a ----
        void AddBoss()
        {
            if (Boss != null) return;
            int line = BossLine;
            var def = Dc2GameData.Bosses()[line];
            int L = Profile.level[heroType];
            float hp = BossHpFor(def, L);
            var b = new BossActor { def = def, hp = hp, maxHp = hp, recoilLimit = 0.02f, sub = 0 };
            b.exp = N.Add32(def.exp, N.F32(50 * L));
            b.money = N.Add32(def.money, N.F32(15 * L));
            b.root = new GameObject("bossN");
            b.root.transform.SetParent(Background.LayerRoot(5), false);   // AddBoss CompAnimObj layer(+12)=5; the hero/dinosaur actor root sits in render layer 10
            b.root.AddComponent<SortingGroup>().sortingOrder = NativeDrawOrder.CustomDraw;   // CompAnimObj custom draw (DrawOrder +16 = 0): after every layer-5 sprite (houses)
            b.slot = NextObjectSlot(); b.targetSeq = ++targetSeqCounter;   // AddBoss runs from lifeModeScene::OnTimeout (Timer stage): inserted at once, so it updates in this tick's id-5 walk
            Boss = b;
            BossOnIdle();
            UpdateBossBlood();
        }

        // ---- bossN::OnIdle 0x12125e ----
        void BossOnIdle()
        {
            var b = Boss; if (b == null || b.dying) return;
            int coin = RandomRange(0, 100);
            bool side;
            if (b.visitedIdle) side = coin <= 49;
            else { b.visitedIdle = true; side = false; }
            int n = b.def.anis.Count; if (n == 0) return;
            var kinds = new int[n];
            for (int i = 0; i < n; i++) { string t = b.def.anis[i].type; kinds[i] = t == "hou" ? 1 : t == "wei" ? 3 : 2; }
            int pick = DebugBossKindRoll != null ? DebugBossKindRoll() : RandomRange(1, n);
            b.lineType = pick - 1;
            b.kind = kinds[b.lineType];
            if (b.sub == 1) return;
            b.sub = 1; b.velX = 0f;
            float s = N.S;
            if (b.kind == 2)
            {
                BossSetAnim(b, "BossJiao01");
                b.x = side ? N.Mul32(s, -180f) : N.Mul32(s, 420f); b.y = N.Mul32(s, -50f); b.left = side;
                SetTimer("boss:Attack", BossRnd(BossP(b, 0), BossP(b, 1)));
            }
            else if (b.kind == 3)
            {
                BossSetAnim(b, "BossWei01");
                b.x = side ? N.Mul32(s, -10f) : N.Mul32(s, 300f); b.y = N.Mul32(s, -50f); b.left = side;
                b.x = BossWideX(b.x, side);
            }
            else
            {
                BossSetAnim(b, "BossHou03");
                b.x = side ? N.Mul32(s, -120f) : N.Mul32(s, 380f); b.y = N.Mul32(s, -50f); b.left = side;
                b.x = BossWideX(b.x, side);
            }
            BossPlay(b, 1);
        }

        // ---- bossN timers (OnTimeout) ----
        bool BossTimeout(string name)
        {
            if (name == "AddBoss") { AddBoss(); return true; }
            if (!name.StartsWith("boss:")) return false;
            var b = Boss; if (b == null) return true;
            string t = name.Substring(5);
            if (t == "BossDieOntime") BossDieOntime(b);
            else if (t == "MissionEnd") BossMissionEnd(b);
            else if (!b.dying)
            {
                if (t == "Attack") BossAttack(b);
                else if (t == "MoveOut") BossMoveOut(b);
                else if (t == "OnIdle") BossOnIdle();
                else if (t == "AttackRest") BossAttackRest(b);
            }
            return true;
        }

        void BossAttack(BossActor b)    // 0x120ac4
        {
            if (b.kind == 2) { BossSetAnim(b, "BossJiao02"); b.velX = 0f; }
            else if (b.kind == 3) BossSetAnim(b, "BossWei03");
            else if (b.kind == 1) BossSetAnim(b, "BossHou02");
            BossPlay(b, 1); b.sub = 4;
        }
        void BossAttackRest(BossActor b)  // 0x1205b2 (wei)
        {
            if (b.kind == 3) { BossSetAnim(b, "BossWei05"); BossPlay(b, 1); b.sub = 5; }
        }
        void BossMoveOut(BossActor b)   // 0x12041a
        {
            if (b.kind == 2) BossSetAnim(b, "BossJiao04");
            else if (b.kind == 3) BossSetAnim(b, "BossWei06");
            else if (b.kind == 1) BossSetAnim(b, "BossHou04");
            BossPlay(b, 1); b.sub = 7;
        }

        void BossDie(BossActor b)       // 0x12090a (reached once, from OnUpdate sub7 fin with hp<=0)
        {
            if (b.kind == 4) return;
            Audio.PlaySound("A_boss_die");
            b.kind = 4; b.velX = 0f;
            SetTimer("boss:BossDieOntime", 1f);
            b.x = b.left ? N.Mul32(N.S, -120f) : N.Mul32(N.S, 380f); b.y = N.Mul32(N.S, -50f);
            b.x = BossWideX(b.x, b.left);
        }

        /// <summary>ADAPTATION (r119k, user decision "shift + cap 21:9"; wide view only): the hou / wei boss and its death pose stand at a fixed side position
        /// whose art ends at the native 4:3 edge. In wide view the boss itself moves outward by the extra width (capped at 21:9), so the cut meets the visible edge and
        /// its touch / attack zones, the hero's hits and the lava geysers (all measured from b.x) keep their iPad distances. 4:3: unchanged. Jiao walks to the hero: not shifted.</summary>
        float BossWideX(float x, bool leftSide) { return N.Add32(x, leftSide ? -WideCombatExtra : WideCombatExtra); }
        void BossDieOntime(BossActor b) // 0x120804
        {
            b.sub = 6; BossSetAnim(b, "BossDie"); BossPlay(b, 1);
            SetTimer("boss:MissionEnd", 4f);
        }
        void BossMissionEnd(BossActor b)    // 0x120cbe -> lifeModeScene vtbl+56 GameClear (mission-complete UI chain is PILOT: existing Complete())
        {
            if (b.root != null) b.ca.Hide();
            Complete();
        }

        // ---- HouCannotTouch / hero damage ----
        void BossHouCannotTouch(BossActor b)   // 0x1206fc
        {
            float s = N.S;
            bool hit = b.left ? heroX < N.Add32(N.Mul32(s, 115f), b.x) : heroX > N.Add32(N.Mul32(s, -115f), b.x);
            if (hit) BeAttackByBoss(b.x, b);
        }

        /// <summary>playerClass::BeAttackByBoss 0xbd3c8 / playerNewAniClass 0x1286e4: damage (int)(10*(L+1)-10+atk), Wudi jigsaw4*1.5*0.01+1.5.</summary>
        public void BeAttackByBoss(float attackerX, BossActor b)
        {
            if (b == null) b = Boss; if (b == null) return;
            float L = N.F32(10 * (Profile.level[heroType] + 1) - 10);
            float damage = N.Add32(L, BossP(b, 7));
            float wudi = N.Add32(N.Mul32(N.Mul32(N.F32(Profile.jigsaw4Count), 1.5f), N.F(1008981770)), 1.5f);
            float before = playerHp;
            HeroHurtFrom(damage, attackerX, wudi);
            if (playerHp != before || heroDead) BossHeroHits++;
        }

        // ---- per frame: bossN::OnUpdate 0x121a98 ----
        /// <summary>bossN (AddBoss 0xd1256) is an id-5 GameObject: it updates in the id-5 walk after the dinosaurs (UpdateId5Objects), at its insertion slot.
        /// SUPERSEDED (r118z2): "the boss body is id 0 and updates before the hero". The ten id-0 objects bossN::OnLoad 0x1211d2 creates are its disabled
        /// effectClassN pool (GetEff 0x120098, used only by BossShao), not a body.</summary>
        /// <summary>b2World::Step for the bossN body (r118z5, Codex review R4): PhysicsManager::Update 0x17610a runs before every object callback and
        /// GameObject::BeforeUpdate 0x16c136 copies the body position, so the hero's attack check (bossN::CheckAttackDis 0x120f22) already sees this tick's x.
        /// The r118z3 port integrated x inside the id-5 callback, after the hero.</summary>
        void PrepareBossPhysics(float dt)
        {
            var b = Boss;
            if (b == null || b.ca == null) return;
            b.x = N.Add32(b.x, N.Mul32(b.velX, dt));
        }

        void UpdateBossBody(float dt)
        {
            var b = Boss;
            if (b == null || b.ca == null) return;
            BossLogic(b, dt);
            b.ca.position = BossRigPos(b);
            b.ca.flipX = b.left;
            b.ca.StepNative(dt);
            BossShakeAndRecoil(b, dt);
            Background.Root.localPosition = new Vector3(BossShakeOffset.x, BossShakeOffset.y, 0f);   // Camera::SetViewport jitter moves the world, not the UI
        }

        void BossLogic(BossActor b, float dt)
        {
            float s = N.S;
            bool fin = b.ca.Fin;
            if (b.kind == 4)
            {
                if (b.sub == 6 && b.ca.CurAnimationNumber == 23) Audio.PlaySound("A_boss_jiao_attack01");   // shipped quirk: dying frame 23 reuses the jiao attack sound
                return;
            }
            if (b.kind == 1)
            {
                if (!fin)
                {
                    int num = b.ca.CurAnimationNumber;
                    switch (b.sub)
                    {
                        case 1: if (num > 22) BossHouCannotTouch(b); break;
                        case 2: BossHouCannotTouch(b); break;
                        case 4:
                            BossHouCannotTouch(b);
                            if (num == 22) BossYanJiang(b, 1);          // + lifeModeScene::AddDinoCap (empty stub on this build)
                            else if (num == 28) Audio.PlaySound("A_boss_hou_attack");
                            else if (num == 47) BossYanJiang(b, 2);
                            else if (num == 72) BossYanJiang(b, 3);
                            break;
                        case 7: if (num <= 13) BossHouCannotTouch(b); break;
                    }
                }
                else BossFinHouWei(b, true);
            }
            else if (b.kind == 3)
            {
                if (fin) BossFinHouWei(b, false);
                else if (b.sub == 4)
                {
                    int num = b.ca.CurAnimationNumber;
                    if (num == 38) Audio.PlaySound("A_boss_wei");
                    else if (num == 40)
                    {
                        bool hit = b.left ? heroX < N.Add32(N.Mul32(s, 320f), b.x) : heroX > N.Add32(N.Mul32(s, -320f), b.x);
                        if (hit) BeAttackByBoss(b.x, b);
                    }
                }
            }
            else if (b.kind == 2)
            {
                if (fin) BossFinJiao(b);
                else if (b.sub == 1)
                {
                    float speed = BossP(b, 6);
                    float v;
                    if (b.x > N.Add32(heroX, N.Mul32(s, 20f))) v = N.Mul32(-speed, s);
                    else if (b.x < N.Add32(heroX, N.Mul32(s, -20f))) v = N.Mul32(speed, s);
                    else v = 0f;
                    b.velX = v;
                }
                else if (b.sub == 4 && b.ca.CurAnimationNumber == 42)
                {
                    Audio.PlaySound("A_boss_jiao_attack01");
                    if (heroX > N.Add32(N.Mul32(s, -70f), b.x) && heroX < N.Add32(N.Mul32(s, 70f), b.x)) BeAttackByBoss(b.x, b);
                    BossStartShake(b);                                  // OnAttackShakeCamera 0x1206a8 (after AddDinoCap stub)
                }
            }
        }

        /// <summary>Prob chain shared by hou/wei/jiao: min(100,5*L+probase) - prosub*count against RandomRange(0,100).</summary>
        bool BossRepeatRoll(BossActor b)
        {
            float basis = N.Add32(N.F32(5 * Profile.level[heroType]), BossP(b, 8));
            if (basis >= 100f) basis = 100f;
            float p = N.Add32(N.Mul32(-BossP(b, 9), N.F32(b.counter)), basis);
            return p > N.F32(RandomRange(0, 100));
        }

        void BossFinHouWei(BossActor b, bool hou)
        {
            if (b.sub == 1)
            {
                BossSetAnim(b, hou ? "BossHou01" : "BossWei02"); BossPlay(b, -1); b.sub = 2;
                SetTimer("boss:Attack", BossRnd(BossP(b, 0), BossP(b, 1)));
            }
            else if (b.sub == 4)
            {
                if (hou)
                {
                    BossSetAnim(b, "BossHou01"); BossPlay(b, -1); b.sub = 2;
                    BossChain(b);
                }
                else
                {
                    BossSetAnim(b, "BossWei04"); BossPlay(b, -1); b.sub = 3;
                    SetTimer("boss:AttackRest", BossRnd(BossP(b, 12), BossP(b, 13)));
                }
            }
            else if (b.sub == 5 && !hou)
            {
                BossSetAnim(b, "BossWei02"); BossPlay(b, -1);
                BossChain(b);
            }
            else if (b.sub == 7)
            {
                if (b.hp <= 0f) BossDie(b);
                else { b.sub = 0; SetTimer("boss:OnIdle", BossRnd(BossP(b, 4), BossP(b, 5))); }
            }
        }

        void BossChain(BossActor b)
        {
            if (BossRepeatRoll(b)) { b.counter++; SetTimer("boss:Attack", BossRnd(BossP(b, 2), BossP(b, 3))); }
            else { b.counter = 0; SetTimer("boss:MoveOut", BossRnd(BossP(b, 2), BossP(b, 3))); }
        }

        void BossFinJiao(BossActor b)
        {
            if (b.sub == 4)
            {
                BossSetAnim(b, "BossJiao03"); BossPlay(b, -1); b.sub = 2;
                SetTimer("boss:MoveOut", BossRnd(BossP(b, 2), BossP(b, 3)));
            }
            else if (b.sub == 7)
            {
                if (b.hp <= 0f) { BossDie(b); return; }
                if (BossRepeatRoll(b))
                {
                    b.counter++; b.sub = 1; b.velX = 0f;
                    BossSetAnim(b, "BossJiao01"); BossPlay(b, 1);
                    SetTimer("boss:Attack", BossRnd(BossP(b, 0), BossP(b, 1)));
                }
                else
                {
                    b.sub = 0; b.velX = 300f;      // raw unscaled SetLinearVelocity(300,0) at 0x1228a8
                    SetTimer("boss:OnIdle", BossRnd(BossP(b, 4), BossP(b, 5)));
                }
            }
        }

        // ---- camera shake (OnAttackShakeCamera 0x1206a8 + OnUpdate tail 0x122bc2) / recoil (BeAttackedMove 0x1209c0) ----
        void BossStartShake(BossActor b)
        {
            if (b.shaking) return;
            b.shakeDur = 0.4f; b.shakeAcc = 0f; b.shaking = true;
        }
        /// <summary>World-space shake offset applied to the actor/background roots (Camera::SetViewport jitter ±10*S, fresh rand each update while the 0.4 s window runs).</summary>
        public Vector2 BossShakeOffset { get; private set; }

        void BossShakeAndRecoil(BossActor b, float dt)
        {
            float s = N.S;
            if (b.shaking)
            {
                if (b.shakeAcc < b.shakeDur)
                {
                    b.shakeAcc = N.Add32(dt, b.shakeAcc);
                    float lo = N.Mul32(s, -10f), hi = N.Mul32(s, 10f);
                    BossShakeOffset = new Vector2(N.Add32(lo, N.Mul32(Rand01(), N.Add32(hi, -lo))), N.Add32(lo, N.Mul32(Rand01(), N.Add32(hi, -lo))));
                }
                else { b.shaking = false; BossShakeOffset = Vector2.zero; }
            }
            if (b.recoil && !b.dying)
            {
                if (b.recoilAcc <= b.recoilLimit) b.recoilAcc = N.Add32(b.recoilAcc, dt);   // lesf2: acc<=limit keeps accumulating
                else if (Mathf.Abs(N.Add32(b.recoilX, -b.x)) < 1f) { b.recoil = false; b.recoilSaved = false; b.velX = 0f; }
                else b.velX = b.recoilX > b.x ? 120f : -120f;    // raw velocities 0x42f00000 / 0xc2f00000
            }
        }

        void BossBeAttackedMove(BossActor b)   // 0x1209c0
        {
            float s = N.S;
            b.recoil = true; b.recoilAcc = 0f; b.recoilLimit = N.F(1017370378);
            if (!b.recoilSaved) b.recoilX = b.x;
            b.velX = heroX > b.x ? N.Mul32(s, -50f) : N.Mul32(s, 50f);
            b.recoilSaved = true;
        }

        // ---- lava wave (bossN::YanJiang 0x1201a8 + effectClassN::OnUpdate type 5) ----
        void BossYanJiang(BossActor b, int param)
        {
            float s = N.S;
            var fx = new BossFx { type = 5 };
            float dx = b.left ? N.Add32(N.Mul32(s, 50f), N.Mul32(s, N.F32(150 * param))) : N.Add32(N.Mul32(s, -50f), -N.Mul32(s, N.F32(150 * param)));
            fx.x = N.Add32(dx, b.x); fx.y = N.Mul32(s, -50f);
            fx.root = new GameObject("effectClassN_yanjiang");
            fx.root.transform.SetParent(Background.LayerRoot(10), false);   // YanJiang: effectClassN layer(+12)=10
            fx.root.AddComponent<SortingGroup>().sortingOrder = NativeDrawOrder.CustomDraw;   // effectClassN custom draw: after every layer-10 sprite (hero, dinosaurs, blood)
            fx.ca = new NativeComponentPlayer(BossAcb("BossYanjiang"), fx.root.transform, new Vector2(fx.x - (camLeft + WorldViewportW * 0.5f), fx.y), 1f);
            fx.ca.Play(1);
            fx.slot = NextObjectSlot(); fx.skipFirstUpdate = InUpdateStage && !fxStagePassed;
            bossFx.Add(fx);
        }

        void StepBossFx(BossFx fx, float dt)
        {
            if (fx.skipFirstUpdate) { fx.skipFirstUpdate = false; return; }
            UpdateBossFx(fx, dt);
            if (fx.dead) { if (fx.root != null) UnityEngine.Object.Destroy(fx.root); bossFx.Remove(fx); }
        }

        void UpdateBossFx(BossFx fx, float dt)
        {
            float s = N.S;
            if (fx.type == 5)
            {
                if (fx.ca.Fin) { fx.dead = true; return; }                 // Disappear
                int num = fx.ca.CurAnimationNumber;
                if (num == 40) Audio.PlaySound("A_boss_yanjiang");
                else if (num == 42 && heroX > N.Add32(N.Mul32(s, -100f), fx.x) && heroX < N.Add32(N.Mul32(s, 100f), fx.x))
                    BeAttackByBoss(fx.x, Boss);
            }
            else if (fx.ca.Fin) { fx.dead = true; return; }
            fx.ca.position = new Vector2(fx.x - (camLeft + WorldViewportW * 0.5f), fx.y);
            fx.ca.StepNative(dt);
        }

        // ---- bossN::BeAttacked 0x123b70 ----
        bool BossCheckAttackState(BossActor b)    // 0x120c82: true while the jiao boss is invulnerable
        {
            if (b.kind != 2) return false;
            if (b.sub == 4) return b.ca.CurAnimationNumber <= 41;
            return b.sub == 7 || b.sub == 1;
        }

        float BossAttackOffset(BossActor b)        // CheckAttackDis 0x120e38 offsets (also GetBossAttackOffset 0x120018)
        {
            float s = N.S;
            if (b.kind == 3) { if (b.sub == 2 || b.sub == 4 || b.sub == 5) return N.Mul32(s, -60f); if (b.sub == 3) return N.Mul32(s, 305f); }
            else if (b.kind == 1 && (b.sub == 2 || b.sub == 4)) return N.Mul32(s, 90f);
            return 0f;
        }

        bool BossCheckAttackDis(BossActor b, WeaponDef w)
        {
            if (b.kind == 2) return false;
            float s = N.S;
            float off = BossAttackOffset(b);
            if (b.left)
            {
                float a = facingLeft ? N.Add32(N.Add32(off, b.x), N.Mul32(s, w.atkDis)) : N.Add32(off, b.x);   // heroFace flag(+96) set adds S*ATK_DIS
                return a > heroX;
            }
            else
            {
                float v = facingLeft ? N.Add32(b.x, -off) : N.Add32(N.Add32(b.x, -off), -N.Mul32(s, w.atkDis));
                return v < heroX;
            }
        }

        /// <summary>Candidate test of playerClass::Attack for the boss; returns false when the boss cannot be hit.</summary>
        bool BossAttackCandidate(WeaponDef w, out float delta)
        {
            delta = 0f; var b = Boss;
            if (b == null || b.ca == null || b.kind == 4 || b.sub == 0) return false;
            float off = BossCheckAttackDis(b, w) ? BossAttackOffset(b) : 0f;
            delta = facingLeft ? N.Add32(heroX, -N.Add32(b.x, off)) : N.Add32(N.Add32(b.x, -off), -heroX);
            return delta > 0f && delta < N.Mul32(N.S, w.atkDis);
        }

        void BossBeAttacked(WeaponDef w)
        {
            var b = Boss; if (b == null || b.dying) { return; }
            if (b.sub == 7 || b.sub == 1) return;
            if (b.sub == 4 && b.kind == 2 && b.ca.CurAnimationNumber <= 39) return;
            int level = Profile.weaponLevels[CurrentAmmoIndex];
            float dmg = NativeWeaponDamage.BaseBeforeFalloff(w, level, Profile.jigsaw2Count, Rand01(), GirlDamageLevel);   // bossN::BeAttacked 0x123c8c..: GetPlayer4Atk for the girl
            int sub = w.index;
            if (b.kind == 2 || b.kind == 3)
            {
                if (w.type == 5) dmg = N.Mul32(dmg, (uint)(sub - 1) <= 4u ? 0.75f : 0.25f);
                else if (w.type == 6) dmg = (sub == 0 || sub == 2 || sub == 4 || sub == 5 || sub == 6) ? N.Mul32(dmg, 1.75f) : N.Mul32(dmg, 0.25f);
                else { dmg = N.Mul32(dmg, 0.25f); if (w.type == 1) dmg = N.Mul32(dmg, NativeWeaponDamage.FalloffFactor(Mathf.Abs(N.Add32(b.x, -heroX)), w.atkDis)); }
            }
            else if (b.kind == 1)
            {
                if (w.type == 5) { if ((uint)(sub - 1) > 4u) dmg = N.Mul32(dmg, 0.25f); }
                else if (w.type == 6) dmg = (sub == 0 || sub == 2 || sub == 4 || sub == 5 || sub == 6) ? N.Add32(dmg, dmg) : N.Mul32(dmg, 0.25f);
                else { dmg = N.Mul32(dmg, 0.25f); if (w.type == 1) dmg = N.Mul32(dmg, NativeWeaponDamage.FalloffFactor(Mathf.Abs(N.Add32(b.x, -heroX)), w.atkDis)); }
            }
            else if (w.type == 1) dmg = N.Mul32(dmg, NativeWeaponDamage.FalloffFactor(Mathf.Abs(N.Add32(b.x, -heroX)), w.atkDis));
            bool crit = NativeWeaponDamage.IsCritical(RandomRange(0, 10000), w.crit);
            if (crit) BossBlastBlood(b, 3);   // bossN::BeAttacked 0x1241e0: critical hit -> BlastBlood x3
            float total = N.Add32(dmg, crit ? dmg : 0f);
            b.hp = N.Add32(b.hp, -total);
            BossHitsTaken++;
            UpdateBossBlood();
            if (b.hp <= 0f)
            {
                if (b.sub != 7 && !b.dying)
                {
                    BossMoveOut(b);
                    b.dying = true;
                    Profile.cash = (int)N.Add32(N.F32(Profile.cash), b.money);   // dword_24047C (cash) += boss money; UpdateExp vtbl+60
                    moneyEarned = (int)N.Add32(N.F32(moneyEarned), b.money);     // lifeModeScene+280
                    UpdateGoldLabel(); AddExp(0);
                    StopCreateEnemy();
                    // playerClass+664 / playerNewAni+372 hero flag := 1 (meaning not yet traced; OPEN)
                }
            }
            else BossBeAttackedMove(b);
            // BlastBlood/Blood/BossShao particles and the att1/att2 hit sounds (BossHitEffects) follow.
            BossHitEffects(w, b, crit);
        }

        void BossHitEffects(WeaponDef w, BossActor b, bool crit)
        {
            if (b.hp > 0f)
            {
                // bossN::BeAttacked hp>0 branch (0x1243e8): type4 kind3 and type6 play att1/att2 by subtype; Blood/BlastBlood/BossShao visuals follow below.
                int sub = w.index;
                bool sound = (w.type == 4 && b.kind == 3) || w.type == 6;
                if (sound)
                {
                    if (sub == 0 || sub == 1) Audio.PlaySound("Audio_att1Audio");
                    else if (sub == 2 || sub == 3) Audio.PlaySound("Audio_att2Audio");
                }
            }
            // bossN::BeAttacked hp > 0 tail (switch tables 0x1243ec / 0x124450 / 0x124480 / 0x1244d4):
            //   types 0,1,2,3 (and > 6): Blood(0); type 4: BlastBlood x3, kind 3 adds the sound and Blood(1); type 5: sub 0,3 Blood(1), sub 1,2,4 BossShao;
            //   type 6: BlastBlood x3, sound, Blood(1).
            if (b.hp <= 0f) return;
            if (w.type == 4) { BossBlastBlood(b, 3); if (b.kind == 3) BossBlood(b, 1); }
            else if (w.type == 6) { BossBlastBlood(b, 3); BossBlood(b, 1); }
            else if (w.type == 5) { if (w.index == 0 || w.index == 3) BossBlood(b, 1); else if (w.index == 1 || w.index == 2 || w.index == 4) BossShao(b); }
            else BossBlood(b, 0);
        }

        /// <summary>CompAnimObj::GetSprite on the boss rig: world centre / size, or false when the component is not displayed.</summary>
        bool BossSprite(BossActor b, string name, out Vector2 world, out NativeCompObj.SpriteInfo info)
        {
            world = Vector2.zero; info = default(NativeCompObj.SpriteInfo);
            if (b.ca == null || !b.ca.Sprites.TryGetValue(name, out info)) return false;
            world = new Vector2(info.x + camLeft + WorldViewportW * 0.5f, info.y);
            return true;
        }

        static float Lerp32(float lo, float hi, float r) { return N.Add32(lo, N.Mul32(r, N.Add32(hi, -lo))); }

        /// <summary>bossN::Blood 0x1236e0: dx (a2 != 0) / qx CA blood effect (layer 13) at x = sprite.x + random offset, y = 0.</summary>
        void BossBlood(BossActor b, int a2)
        {
            float s = N.S, sx = 0f, sw = 0f; NativeCompObj.SpriteInfo si; Vector2 p;
            string comp = NativeBossBlood.ComponentFor(b.kind, b.sub);
            if (comp != null) { if (!BossSprite(b, comp, out p, out si)) return; sx = p.x; sw = si.w; }
            float x; bool flip;
            NativeBossBlood.Place(b.kind, b.sub, sx, sw, heroX, facingLeft, s, Rand01, out x, out flip);
            SpawnCaFx("blood", BloodEffectAnimation(a2 != 0), x, 0f, flip, 13, 1f);
        }

        NativeComponentAnimation bossShaoAnim;
        /// <summary>bossN::BossShao 0x123106: the "effshaoAnimation" fire effect (g_ca_boss_huoyan, layer 13) at a random point of the hit component.</summary>
        void BossShao(BossActor b)
        {
            Vector2 p = Vector2.zero; NativeCompObj.SpriteInfo si = default(NativeCompObj.SpriteInfo);
            string comp = NativeBossBlood.ComponentFor(b.kind, b.sub);
            if (comp != null && !BossSprite(b, comp, out p, out si)) return;
            float x, y;
            NativeBossBlood.PlaceShao(b.kind, b.sub, p.x, p.y, si.w, si.h, heroX, Rig.Player.position.y, facingLeft, b.left, N.S, Rand01, out x, out y);
            if (bossShaoAnim == null)
            {
                bossShaoAnim = BossAcb("huoyan_huoyan");
                var lib = NativeCaLibraryAnim.Get("DC2/Image/acb/huoyan_huoyan.library.lcb", "huo");   // component "huo" plays the library animation huoyan_xiaohuo0001..0006
                foreach (var c in bossShaoAnim.components) if (c.avatar == "huo") c.libAnim = lib;
            }
            SpawnCompFx("bossShao", bossShaoAnim, x, y, false, 13, 1f);
        }

        /// <summary>bossN::BlastBlood 0x122e38: same drop as the dinosaur one, at the boss GameObject position.</summary>
        void BossBlastBlood(BossActor b, int count)
        {
            if (bloodWorld == null) return;
            for (int i = 0; i < count; i++) bloodWorld.BlastBlood(new Vector2(b.x, b.y));
        }

        void UpdateBossBlood()   // lifeModeScene::UpdateBossBlood 0xd0710
        {
            var b = Boss; if (b == null || bossBloodBar == null) return;
            int ratio = (int)N.Div32(N.Mul32(100f, b.hp), b.maxHp);
            bossBloodBar.SetVisible(ratio >= 0);
            if (ratio < 0) ratio = 0;
            float w = N.Mul32(N.Mul32(N.F32(ratio), N.F(1008981770)), 110f);
            bossBloodBar.SetRect(N.Layout(N.Add32(267f, 45f), N.Add32(2f, 7f), w, 10f));
        }
    }
}
