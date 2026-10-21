using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dc2Pilot
{
    /// <summary>
    /// Touch pad, roll, melee, weapon swap, pause menu and level-up.
    /// NATIVE: lifeModeScene::OnTouchEvent 0xd15e4 (iPad rects), playerClass::OnRollDown 0xbd668/Roll 0xb95f8/OnKeyFrame 0xbf406/OnAnimationEnd 0xbd978,
    /// lifeModeScene::RollDown 0xd0850, OnHitDown 0xbd7c8, JinAttack 0xbdfb8, BeJinAttacked 0x1442a, SwitchWeapon 0xbac3c, UpdateExp 0xd1f90,
    /// pauseCtrl ctor 0xce670 / Enter 0xced0c / Initialize 0xcf05e / SendUIEvent 0xce5c0, lifeModeScene::OnUIEvent 0xe6550.
    /// DC1 pause/combat overlap is documented in Dev/DevDocs/DC1_DC2_COMBAT_OVERLAP.md (leads only).
    /// Touch: per-finger ids as native (mainScene::OnTouchEvent 0x96914 / lifeModeScene::OnTouchEvent 0xd15e4, r118w); UI controls ignore the finger id natively as well (UIMoveCtrl::HandleTouch 0xcd542: one pressed flag, any finger's end clicks; UIManager::HandleUITouch per touch, r118x).
    /// </summary>
    public partial class GameScene
    {
        public bool paused;
        public bool rolling, rollCooldown;            // state 7 / +141
        float rollVelocity;
        public float RollVelocity { get { return rollVelocity; } }
        public int rollPhase;
        bool UsesNativeGroundRoll { get { return heroType==0||heroType==2||heroType==4; } }
        public float rollFill;                        // lifeModeScene+608 / mainScene+380 clip height (88 px, -28 px/s on iPad: dt * -20 * 2 * 0.7)
        public bool rollFillOn;                       // scene +268: drain active (RollDown sets, UnRollDown / empty clear)
        public bool meleeing, hitHeld;                // state 3 / g_b_HitDown
        // File-scope statics leftCtrlID .. changeCtrlID: one set per native scene class (mainScene 0x23b924.., lifeModeScene 0x23ba28..), only written by
        // that class's OnTouchEvent, never reset on scene load (process lifetime). Index: 0 left 1 right 2 hit 3 rota 4 fire 5 weapon 6 change.
        static readonly int[] mainCtrlIds = { -1, -1, -1, -1, -1, -1, -1 }, lifeCtrlIds = { -1, -1, -1, -1, -1, -1, -1 };
        int[] CtrlIds { get { int t = Game.LaunchMission.type; return t == 1 || t == 5 ? mainCtrlIds : lifeCtrlIds; } }   // run/egg missions run in mainScene
        /// <summary>Roll fill control: mainScene (run/egg missions) id 39 (+312), lifeModeScene id 45 (+572).</summary>
        public int RollFillId { get { int t = Game.LaunchMission.type; return t == 1 || t == 5 ? 39 : 45; } }
        public static void ResetTouchIdsForTest() { for (int i = 0; i < 7; i++) mainCtrlIds[i] = lifeCtrlIds[i] = -1; }   // DEBUG_ONLY
        public int levelUps;

        // ---- touch: OnTouchEvent(finger_id, type 0 begin / 1 move / 2 end, x, y) ----
        protected override void OnTouchEvent(int type, Vector2 p) { OnTouchEvent(type, p, 0); }
        protected override void OnTouchEvent(int type, Vector2 p, int finger)
        {
            if (IntroActive) return;                       // OnTouchEvent 0xd163c / 0x96940: state != 4 -> nothing
            if (paused || sceneMode == 7 || sceneMode == 8) return;   // native: paused = state 6, GameOverStart = 7 (MissionFailEnter: 8); after GameClear the scene stays in state 4 and keeps taking touches
            var ids = CtrlIds;
            if (type == 0) TouchBegin(p, finger, ids);
            else if (type == 1)
            {
                // move (0xd1c36 / 0x96e0c): only a finger owning one arrow can take over the other arrow; nothing else reacts.
                // Every OnTouchEvent rect test is Triniti2D::Rect::PtInRect 0x16fc4 (all four edges excluded), r118z4: the port used the inclusive Rect.Contains.
                if (NativeTouchInside(LeftRect,p)) { if (ids[1] == finger) { ids[1] = -1; ids[0] = finger; rightDown = false; leftDown = true; } }
                else if (NativeTouchInside(RightRect,p)) { if (ids[0] == finger) { ids[0] = -1; ids[1] = finger; leftDown = false; rightDown = true; } }
            }
            else if (type == 2) TouchEnd(p, finger, ids);
        }

        void TouchBegin(Vector2 p, int finger, int[] ids)
        {
            bool mainScene = ids == mainCtrlIds;
            if (NativeTouchInside(LeftRect,p)) { ids[0] = finger; leftDown = true; }
            else if (NativeTouchInside(RightRect,p)) { ids[1] = finger; rightDown = true; }
            else if (NativeTouchInside(HitRect,p)) { ids[2] = finger; hitHeld = true; OnHitDown(); }
            else if (NativeTouchInside(RollRect,p))
            {
                if (mainScene && EggCarrying) return;     // mainScene 0x96b84: carried egg (scene+269): no id, no flag, no roll
                ids[3] = finger; OnRollDown();   // g_b_RollDown = 1 is mirrored by the roll state
            }
            else if (NativeTouchInside(FireRect,p)) { ids[4] = finger; fireDown = true; }
            // Native d1afc/d1b0e records the finger at press; d1d3e/d1eb2 switches only on release inside the corresponding release rect.
            else if (NativeTouchInside(WeaponRect,p)) ids[5] = finger;
            else if (NativeTouchInside(ChangeRect,p)) ids[6] = finger;
            else if (NativeTouchInside(StopRect,p)) PauseGame();
        }

        // ARM Rect::PtInRect 16fc4: all four edges excluded. The icon release
        // uses the unscaled static rect, despite its scaled press rect (r56).
        public static bool NativeTouchInside(Rect r,Vector2 p)
        { return p.x>r.x && p.y>r.y && p.x<N.Add32(r.x,r.width) && p.y<N.Add32(r.y,r.height); }
        public static Rect WeaponReleaseRect { get { return new Rect(0f,0f,80f,80f); } }

        /// <summary>End (0xd1cc0 / 0x96e78): the first control whose id equals the finger (left, right, hit, rota, fire, weapon, change) is released; only its flag changes.
        /// The rota release clears the id only (g_b_RollDown stays until the roll ends). Weapon/change release inside the release rect: SwitchWeapon + Audio_Button1Audio.</summary>
        void TouchEnd(Vector2 p, int finger, int[] ids)
        {
            if (ids[0] == finger) { ids[0] = -1; leftDown = false; }
            else if (ids[1] == finger) { ids[1] = -1; rightDown = false; }
            else if (ids[2] == finger) { ids[2] = -1; hitHeld = false; }
            else if (ids[3] == finger) ids[3] = -1;
            else if (ids[4] == finger) { ids[4] = -1; fireDown = false; }
            else if (ids[5] == finger) { if (NativeTouchInside(WeaponReleaseRect,p)) { SwitchWeapon(); Audio.PlaySound("Audio_Button1Audio"); } ids[5] = -1; }
            else if (ids[6] == finger) { if (NativeTouchInside(ChangeRect,p)) { SwitchWeapon(); Audio.PlaySound("Audio_Button1Audio"); } ids[6] = -1; }
        }

        // ---- roll ----
        string RollPoseFor(int hero) { return hero == 3 ? "p4_roll_1" : hero == 1 ? "p2_roll_1" : "p_roll"; }

        public void OnRollDown()
        {
            if (rolling || rollCooldown || heroDead || paused || heroControlState == 4) return;       // state 7/4 or +141 (3 s "rollEndTime")
            // r119j (user: "endurance end: I can still press S to roll-dash"): playerClass::OnRollDown 0xbd674..0xbd688 / playerNewAniClass 0x128168..0x128174 return when
            // the state field is 4, which UpdateCamera's run end (bb2a0, RunEndCheck) and the egg end write every frame; the port only tested heroDead (FALSIFIED gate).
            nativeHurting=false;hitStun=0f; // native OnRollDown permits state5 and replaces it with state7.
            rolling = true; rollCooldown = true; SetTimer(heroTimer, "rollEndTime", 3.0f, () => { rollCooldown = false; });   // OnRollDown: SetTimer("rollEndTime",3.0)
            heroControlState=7;heroVelocityX=0f;
            Audio.PlaySound(heroType == 4 ? "p5rollAudio" : heroType == 3 ? "p4rollAudio" : heroType == 1 ? "p2rollAudio" : "p1rollAudio");   // playerClass/playerNewAniClass::OnRollDown 0xbd6ee / 0x1281e2
            ClearRageIfFiring(); ClearNativeFireChannel(); firing = false; fireDown = false; meleeing = false;
            // lifeModeScene::RollDown 0xd0850: fill indicator at the iPad rect (830,537,88,88), height drains
            rollFill = RollFillRect.height; rollFillOn = true;
            // The fill is created by the scene OnLoad (lifeModeScene::OnLoad 0xe950c CreateImage(45, 1) -> +572; mainScene::OnLoad 0xaa288 CreateImage(39, 1) -> +312),
            // SetRect (830,537,88,88), SetColor 0xB0000000 = black alpha 176 (0xe96a2 / 0xaa4b8). r118z4: the port used white alpha 140 and id 45 in both scenes (FALSIFIED).
            var fill = GetControl(RollFillId);   // created hidden by the scene OnLoad (GameScene.BuildHud, layer 1)
            if (fill != null) { fill.SetVisible(true); fill.SetClip(RollFillRect); }   // RollDown 0xd0900 / mainScene 0x94e00: SetVisible(1), clip = the same iPad rect (830,537,88,88) via vtable+0x34 (UIControlVisible::SetClip)
            float dir = facingLeft ? -1f : 1f;
            rollPhase=0;
            // r119d (user item 10): playerNewAniClass::OnRollDown 0x128238 sets the body velocity to (0,0) and Roll 0x126626 sets none, so the girl does not move
            // during roll_start; the port gave her the playerClass ground-roll start velocity (0.2 * walk) as well.
            if (IsGirl) { rollVelocity = 0f; GirlRollPhase = 0; GirlRoll(); return; }   // end/timer callbacks run in GameGirl
            rollVelocity = N.Mul32(dir*0.2f,WalkSpeedFor(heroType)); // native ground Roll initial velocity
            string pose = RollPoseFor(heroType);
            if(UsesNativeGroundRoll)Rig.SelectNativeGroundRoll(heroType);
            else { StartSpecialRoll(); return; }   // heroes 1 and 3: playerClass::Roll 0xb95f8 else-branch (two-part roll), PA-011 SUPERSEDED for the roll
            Rig.Player.objectScale = HeroRigScale;
            Rig.Player.keyFrame = f =>
            {
                float d = facingLeft ? -1f : 1f;
                if (f == 7) { rollVelocity = N.Mul32(d*5f,WalkSpeedFor(heroType));rollPhase=1; }
                else if (f == 18) { rollVelocity = N.Mul32(d*(UsesNativeGroundRoll ? .05f : .2f),WalkSpeedFor(heroType));rollPhase=2; }
            };
            Rig.Player.animationEnd=()=>
            {
                if(!rolling||!UsesNativeGroundRoll||rollPhase!=2)return;
                rolling=false;rollVelocity=0f;nativeCamera.turning=false;
                heroControlState=2;heroVelocityX=0f;
                rigState=0;
                if(UsesRecoveredLifeCamera)Rig.SelectNativeLocomotion(heroType,weaponSlot,"stand");else Rig.Select(heroType,true);
                Rig.Player.objectScale=HeroRigScale;
            }; // native callback switches pose after emitting final Roll, preserving that emission.
        }

        /// <summary>Heroes 1 and 3 (playerClass::Roll 0xb95f8, OnAnimationEnd 0xbdb8c state 7, OnTimeout "roll" 0xbb59e): roll_1 plays once without velocity (phase +161 = 0);
        /// at its end the hero is hidden (setVisible(false), +659 = 0), hero 3 shows the p4 roll effect (GetEffect, g_ca_p4_RollEff "rollEffectAnimation", y - 50, layer 9, flip not set),
        /// the body travels at (flip ? -3 : 3) * walk speed and SetTimer("roll", 0.5); the timer makes the hero visible, stops it and plays roll_2 once (phase 2);
        /// the end of roll_2 stops the hero, state = 2, g_b_RollDown = 0, Stand.</summary>
        void StartSpecialRoll()
        {
            rollPhase = 0; rollVelocity = 0f;
            Rig.SelectNamedPose(heroType, heroType == 3 ? "p4_roll_1" : "p2_roll_1", 0, 0.025f, new int[0]);
            Rig.Player.objectScale = HeroRigScale;
            Rig.Player.keyFrame = null;
            Rig.Player.animationEnd = () =>
            {
                if (!rolling || rollPhase != 0) return;
                Rig.Player.Hide();
                if (heroType == 3)
                {
                    SpawnCaFxInheritFlip("rolleff", FxAnimation("DC2/Image/an2/p4_roll_eff.a2b", true), heroX, N.Add32(Rig.Player.position.y, -50f), 9, 1f);
                }
                rollVelocity = N.Mul32(facingLeft ? N.F(unchecked((int)0xC0400000)) : N.F(0x40400000), WalkSpeedFor(heroType));
                rollPhase = 1; SetTimer(heroTimer, "roll", 0.5f, SpecialRollTimeout);
            };
        }
        void SpecialRollTimeout()
        {
            // playerClass::OnTimeout "roll" 0xbb59e has no state check: even after GameClear stood the hero (state 2) it makes the hero visible, stops it and plays
            // roll_2 once; OnAnimationEnd then only acts in state 7, so the hero stays on roll_2's last frame (shipped quirk, r118x).
            rollVelocity = 0f; heroVelocityX = 0f; rollPhase = 2;
            Rig.SelectNamedPose(heroType, heroType == 3 ? "p4_roll_2" : "p2_roll_2", 0, 0.025f, new int[0]);   // also makes the hero visible again (setVisible(true))
            Rig.Player.objectScale = HeroRigScale; Rig.Player.keyFrame = null;
            Rig.Player.animationEnd = () =>
            {
                if (!rolling || rollPhase != 2) return;
                rolling = false; rollVelocity = 0f; heroControlState = 2; heroVelocityX = 0f; rigState = -1; SetRigState(0);
            };
        }
        public int SpecialRollPhase { get { return rollPhase; } }   // DEBUG_ONLY verifier hook
        public bool HeroVisible { get { return Rig != null && Rig.Player != null && Rig.Player.active; } }   // DEBUG_ONLY

        void RollPhysics(float dt)
        {
            if(!rolling||!UsesNativeGroundRoll)return;
            heroX=N.Add32(heroX,N.Mul32(rollVelocity,dt));
            Background.StepVelocity(rollVelocity,dt);
        }

        void RollUpdate(float dt)
        {
            var fill = GetControl(RollFillId);
            if (fill != null && rollFillOn)
            {
                // lifeModeScene::OnUpdate 0xd45b0..0xd461a (mainScene 0x97de4/0x97df8 identical): s = 2.0 on iPad, *0.7 (0x3F333333);
                // clip height += dt * -20.0 (0xC1A00000, struct-decoded) * s; the sprite keeps its 88x88 rect and is CLIPPED (UISprite::GetPointer), not
                // resized. r118z4: the port drained at -25 and resized the control (squashing the texture) - both FALSIFIED.
                rollFill = N.Add32(rollFill, N.Mul32(N.Mul32(dt, -20f), N.Mul32(2f, 0.7f)));
                fill.SetClip(new Rect(RollFillRect.x, RollFillRect.y, RollFillRect.width, rollFill));   // vtable+0x34 every tick while +268 is set
                if (rollFill <= 0f) rollFillOn = false;   // native clears +268 and leaves the empty clip (nothing drawn); the control stays (r118z4)
            }
            if (!rolling || UsesNativeGroundRoll || IsGirl || nativeWorldPrepared) return;   // recovered phase integrates HeroBodyVelocity in the physics position
            heroX = N.Add32(heroX, N.Mul32(rollVelocity, dt));
            Background.StepVelocity(rollVelocity, dt); // UpdateBG(1,false)/(3,false) after every velocity change
        }

        // ---- melee (OnHitDown / JinAttack) ----
        public WeaponDef MeleeWeapon()
        {
            var m = Dc2GameData.PlayerMelee(heroType);
            return Dc2GameData.Weapons()[m.type][m.subtype];
        }

        public void OnHitDown()
        {
            StartMelee(false);
        }
        void StartMelee(bool preserveEmission)
        {
            if (rolling || meleeing || heroDead || paused || HeroHurting || !heroInputEnabled) return;   // OnHitDown 0xbd810: +657 gate
            ClearRageIfFiring(); ClearNativeFireChannel(); meleeing = true; firing = false;
            heroVelocityX=0f;heroControlState=3; // native OnHitDown stops physics before melee playback.
            if (IsGirl) { GirlHit(); hitHeld = false; return; }   // playerNewAniClass::OnHitDown 0x127954 keeps the advanced channel
            if(UsesRecoveredLifeCamera){Rig.SelectNativeMelee(heroType);Rig.Player.SetAdvanced(null);} // native bd890 clears advanced channel on successful melee entry.
            else Rig.SelectNamedPose(heroType, heroType == 3 ? "p4_hit" : "p_hit", 0, 0.025f, new[] { 10 },preserveEmission);
            Rig.Player.objectScale = HeroRigScale;
            Rig.Player.keyFrame = f => { if (f == 10) JinAttack(); };
            // Native OnHitDown bd906 consumes the initial held-hit flag. A new
            // press during state3 may remain latched for the end callback.
            // Girl/other mode input composition remains outside this bound.
            if(UsesRecoveredLifeCamera)hitHeld=false;
        }

        void MeleeUpdate()
        {
            if (!meleeing || IsGirl) return;
            if (Rig.Player.Ended)                                                    // OnAnimationEnd state 3: held -> replay, else stand
            {
                if (hitHeld) { if(UsesRecoveredLifeCamera)Rig.Player.Play(Rig.Player.animation,0,0,.025f); else Rig.SelectNamedPose(heroType, heroType == 3 ? "p4_hit" : "p_hit", 0, 0.025f, new[] { 10 }); Rig.Player.objectScale = HeroRigScale; Rig.Player.keyFrame = f => { if (f == 10) JinAttack(); }; }
                else { meleeing = false;heroControlState=2;heroVelocityX=0f; rigState = -1; SetRigState(0); }
            }
        }

        /// <summary>playerClass::JinAttack 0xbdfb8: the reach test measures from the hero x to the dinosaur's head sprite (getSpritePos "tou", "com6" for skin 5;
        /// 0xbe15a / 0xbe170), not to the body origin (r119c, user: melee through a head sometimes did not hit). A missing part yields (0,0), as native a4d0.</summary>
        float MeleeDx(DinoActor d) { float hx = DinoPartWorld(d, DinoHeadPart(d), Vector2.zero).x; return facingLeft ? heroX - hx : hx - heroX; }

        void JinAttack()
        {
            var w = MeleeWeapon();
            float range = w.atkDis * N.S;
            bool armored = false;     // v35: dinosaurClass+156 (SURFACE) > 100 after BeJinAttacked
            // r119d (user item 13, boss leg not meleeable during the stomp): playerClass::JinAttack 0xbdfb8 also walks the "boss" object: distance from the hero to
            // the boss position (0xbe1bc), forced to 10.0 when bossN::CheckAttackDis 0x120e38 is true (0xbe204..0xbe20a); in reach when 0 < d < S*ATK_DIS;
            // skipped when +456 == 4 or +460 == 0 (0xbe25a / 0xbe266). Group weapons hit it at once (0xbe2a0); otherwise it competes for the nearest target.
            // The port's melee never considered the boss.
            // The walk follows the vector order (dinosaurs and the boss by creation). The kept candidate (v33) is re-measured each time with the raw distance
            // (0xbe2aa..0xbe3f6: head for a dinosaur, GameObject position for the boss - the forced 10.0 is not kept) and replaced when that is > the new one.
            var bb = Boss;
            DinoActor bestDino = null; bool bestBoss = false;
            int bossSeq = bb != null ? bb.targetSeq : int.MaxValue;
            bool bossDone = bb == null;
            float BossRawDx() { return facingLeft ? N.Add32(heroX, -bb.x) : N.Add32(bb.x, -heroX); }
            void Consider(DinoActor d)
            {
                float dx;
                if (d == null)
                {
                    dx = BossCheckAttackDis(bb, w) ? 10f : BossRawDx();   // 0xbe204..0xbe20a
                    if (!(dx > 0f)) return;
                    if (!(range > dx)) return;
                    if (bb.ca == null || bb.kind == 4 || bb.sub == 0) return;   // 0xbe25a / 0xbe266
                    if (w.group) { BossBeAttacked(w); return; }                 // 0xbe2a0
                }
                else
                {
                    dx = MeleeDx(d);
                    if (!(dx > 0f) || !(range > dx)) return;
                    if (d.dead || !d.targetable) return;                        // 0xbe234 (+180 == 3) / 0xbe266 (+172)
                    if (w.group) { BeJinAttacked(d, w); if (d.surface > 100) armored = true; return; }
                }
                if (bestDino == null && !bestBoss) { bestDino = d; bestBoss = d == null; return; }
                float kept = bestBoss ? BossRawDx() : MeleeDx(bestDino);
                if (kept > dx) { bestDino = d; bestBoss = d == null; }
            }
            foreach (var d in dinos.ToArray())
            {
                if (!bossDone && bossSeq < d.targetSeq) { bossDone = true; Consider(null); }
                Consider(d);
            }
            if (!bossDone) Consider(null);
            if (bestBoss) BossBeAttacked(w);
            else if (bestDino != null) { BeJinAttacked(bestDino, w); if (bestDino.surface > 100) armored = true; }
            MeleeSound(armored);
        }

        /// <summary>playerClass::JinAttack 0xbe542..0xbe5ae / playerNewAniClass::JinAttack 0x12903a..0x1290ca: the swing sound is played on every JinAttack by the melee weapon subtype.</summary>
        void MeleeSound(bool armored)
        {
            int sub = Dc2GameData.PlayerMelee(heroType).subtype;
            string n;
            switch (sub)
            {
                case 0: n = armored ? "A_hit_tie_mugun" : "Attack_GAudio"; break;
                case 2: n = "Audio_G4Audio"; break;
                case 4: n = armored ? "A_hit_tie_guangjian" : "p2hitAudio"; break;
                case 5: n = armored ? "A_hit_tie_tie" : "Attack_GAudio"; break;
                case 6:
                    if (IsGirl) n = Profile.weaponLevels[heroType * 6 + 5] >= Dc2GameData.PlayerMelee(4).maxLevel ? "p5hit_tieAudio" : "p5hit_muAudio";   // level vs max level (0x129090)
                    else n = armored ? "A_hit_tie_tie" : "p5hitAudio";
                    break;
                default: n = "Attack_GAudio"; break;
            }
            Audio.PlaySound(n);
        }

        /// <summary>dinosaurClass::BeJinAttacked 0x1442a: melee damage uses 2*(level-1)+ATK_MIN + rand01*ATK_BASE (no jigsaw bonus); level<=0 uses ATK_MIN..ATK_MAX.</summary>
        /// <summary>dinosaurClass::BeJinAttacked 0x1442a damage, binary32 as on ARM: level > 0 and type 6: basis = (float)(2L - 2) + atkMin (girl: GetPlayer4Atk 0x14586),
        /// upper = (float)baseAtk + basis, dmg = basis + unit * (upper - basis); level <= 0: atkMin + unit * (atkMax - atkMin); other types 0. The critical roll comes after (0x145f8).</summary>
        public float MeleeDamage(WeaponDef w, int level)
        {
            if (level <= 0) { float u = Rand01(); return N.Add32(w.atkMin, N.Mul32(u, N.Add32(w.atkMax, -w.atkMin))); }
            if (w.type != 6) return 0f;
            float basis = N.Add32(N.F32(2 * level - 2), w.atkMin);
            if (heroType == 4) basis = NativeWeaponDamage.Player4Atk(basis, Profile.level[4]);
            float upper = N.Add32(N.F32((int)w.baseAtk), basis);
            float unit = Rand01();
            return N.Add32(basis, N.Mul32(unit, N.Add32(upper, -basis)));
        }
        public float MeleeDamage(WeaponDef w, int level, bool crit) { float d = MeleeDamage(w, level); return crit ? N.Add32(d, d) : d; }   // test helper

        void BeJinAttacked(DinoActor d, WeaponDef w)
        {
            if (!InViewport(d)) return;
            int level = Profile.weaponLevels[heroType * 6 + 5];
            float dmg = MeleeDamage(w, level);                               // damage rand first (0x1459c / 0x145ce) ...
            bool crit = NativeWeaponDamage.IsCritical(RandomRange(0, 10000), w.crit);   // ... then the critical RandomRange (0x145f8)
            if (crit) dmg = NativeWeaponDamage.ApplyCritical(dmg, true);
            hitDino++;
            if (crit) { DinoBlastBlood(d); DinoBlastBlood(d); DinoBlastBlood(d); }   // BeJinAttacked 0x14624: critical hit -> BlastBlood x3
            d.hp -= dmg;
            if (d.hp <= 0f) { DinoDie(d, w); return; }   // dinosaurClass::Die(weaponClass*) dispatches on the weapon (melee type 6: DieNormal / DieTouFei)
            bool recovered = ApplyNativeMeleeReaction(d, w);
            if (!recovered)
            {
                d.state = 4; d.hitTimer = 0.25f; d.legacyHit = true; d.rig.Play(d.aBehited, 0, 0, 0.05f);
                d.velocityX=0f; // Existing pilot hurt/knockback lifecycle remains OPEN.
                d.x += (facingLeft ? -1f : 1f) * 200f * N.S * 0.1f;                       // knockback velocity ±200*s for a short time (state 2)
            }
            DinoBlastBlood(d); DinoBlastBlood(d); DinoBlastBlood(d);   // BeJinAttacked 0x14712 / 0x14850: BlastBlood x3 on both reaction branches, then Blood(1) (dx effect)
            DinoBlood(d, 1);
        }

        // ---- weapon swap ----
        public void SwitchWeapon()
        {
            if (heroDead || paused) return;
            fireDown = false; hitHeld = false;
            if (rolling) return; // native clears held flags before its state7 gate (r55).
            if (firing) ClearNativeFireChannel();
            var weapons = Dc2GameData.PlayerWeapons(heroType);
            int size = weapons.Count;                                                   // includes the trailing melee record
            for (int guard = 0; guard < 8; guard++)
            {
                weaponSlot++;
                if (size - 1 <= weaponSlot) weaponSlot = 0;                              // ++slot; if (size-1 <= slot) slot = 0
                var s = weapons[weaponSlot];
                if (s.type <= 5 && Profile.weaponOwned[heroType * 6 + weaponSlot] >= 0) break;   // owned (>=0), else recurse
            }
            AfterWeaponSlotChanged();
        }

        void AfterWeaponSlotChanged()
        {
            RefreshWeaponHud();
            if (IsGirl) { GirlChangeWeapon(); firing = false; if (fireDown) NativeFireDown(); return; }   // ChangeWeapon 0x1298d0 tail
            if(nativeHurting)Rig.RebindNativeHurt(heroType,weaponSlot);
            else if(UsesRecoveredLifeCamera&&!meleeing&&!rolling&&(uint)heroControlState<=2)
                Rig.RebindNativeLocomotion(heroType,weaponSlot,heroControlState==2?"stand":"move");
        }

        /// <summary>AfterNoBullets 0xbaeac slot change (GenComAnim + ChangeWeapon + HUD).</summary>
        void SelectWeaponSlot(int slot) { weaponSlot = slot; AfterWeaponSlotChanged(); }

        UiControl ammoIcon;   // the scene pointer to the weapon icon created by OnLoad (control 8, layer 2; lifeModeScene::OnLoad 0xe7b5c)
        void RefreshWeaponHud()
        {
            var slot = Dc2GameData.PlayerWeapons(heroType)[weaponSlot];
            // mainScene::SwitchWeapon 0xb7db0 updates the icon through the pointer stored at load (control 8, see GameScene.BuildHud), never by id. r119l blamed a
            // deleted icon; SUPERSEDED r119m: the icon is control 8 and GameClear's band is control 20, so the icon stays and keeps animating on switches (native
            // SwitchWeapon 0xbac3c has no +657 gate). The user's shifted icon came from the port's wrong id (20), not from the switch itself.
            var c = ammoIcon;
            if (c != null)
            {
                c.SetTexture("icon_bullet" + Mathf.Min(slot.type, 5) + "Frame");
                // r119e (user item 5): mainScene::SwitchWeapon(int) 0xb7db0, called by playerClass::ChangeWeapon 0xba5f2 / playerNewAniClass::ChangeWeapon 0x129f94 for
                // every non-melee weapon: iconbulletRect centre, start size 0.5 * (w, h), then (w, h) twice, colour -1, durations 0.2 / 0.1 (0x3F000000, 0x3E4CCCCD, 0x3DCCCCCD).
                Rect r = N.Layout(0, 1, 61, 54);
                float cx = N.Add32(N.Mul32(r.width, 0.5f), r.x), cy = N.Add32(N.Mul32(r.height, 0.5f), r.y);
                var white = new Color32(255, 255, 255, 255);
                c.StartMove(new MoveData(cx, cy, N.Mul32(r.width, 0.5f), N.Mul32(r.height, 0.5f), white), new MoveData(cx, cy, r.width, r.height, white),
                            new MoveData(cx, cy, r.width, r.height, white), 0.2f, 0.1f);
            }
            UpdateBullet();
        }

        public bool bulletSoundArmed;                 // mainScene+180: set once the mission has started (OnUIEvent 0xa6a9e), cleared in OnLoad
        public int NoBulletSounds;                    // DEBUG_ONLY

        /// <summary>mainScene::UpdateBullet 0x9546c: ammo label text; ammo &lt;= 0 (and started) plays Audio_NobulletAudio; icon_none (id 21) visible when ammo &lt;= 0.</summary>
        void UpdateBullet()
        {
            int ammo = Profile.weaponAmmo[CurrentAmmoIndex];
            if (ammoLabel != null) ammoLabel.SetText(ammo.ToString());
            if (ammo <= 0 && bulletSoundArmed && !Game.DebugInfiniteAmmo) { Audio.PlaySound("Audio_NobulletAudio"); NoBulletSounds++; }   // DEBUG_ONLY: infinite ammo keeps firing an empty gun silently (r119e item 6)
            var none = GetControl(21); if (none != null) none.SetVisible(ammo <= 0);
        }

        // ---- experience / level-up (UpdateExp 0xd1f90) ----
        public int HeroExp { get { return Profile.exp[heroType]; } }
        public int ExpThreshold { get { int L = Profile.level[heroType]; return (L + 1) * 30 * L + Dc2Data.Heroes()[heroType].baseExp; } }

        /// <summary>DEBUG_ONLY (r119e item 7): the held gun's ammo set to 0 (HUD refreshed, no sound: the native UpdateBullet sound is for real fire).</summary>
        public void DebugEmptyHeldGun() { if (heroDead) return; bool armed = bulletSoundArmed; bulletSoundArmed = false; Profile.weaponAmmo[CurrentAmmoIndex] = 0; UpdateBullet(); bulletSoundArmed = armed; }
        /// <summary>DEBUG_ONLY (r119e item 10): EXP up to one past the next threshold through the normal AddExp path (level-up effect, HP refill, EXP bar).</summary>
        public void DebugLevelUp() { if (heroDead) return; AddExp(ExpThreshold - Profile.exp[heroType] + 1); Game.DebugLevelUnlockPending.Add(heroType); }   // DEBUG_ONLY: weapon unlock applied on the next menu load (SelectScene.OnLoad)

        void AddExp(int amount)
        {
            Profile.exp[heroType] += amount;
            int thr = ExpThreshold;
            // R002-B3: mainScene 0x985c2 CMP / 0x985c4 BLT levels up at exp >= threshold; lifeModeScene 0xd208e CMP / 0xd2090 BLE only at exp > threshold (shipped twin difference)
            if (MainSceneMission ? Profile.exp[heroType] >= thr : Profile.exp[heroType] > thr)
            {
                if (Profile.level[heroType] <= 97)
                {
                    Profile.level[heroType]++; Profile.exp[heroType] = 0; levelUps++; StartLevelUpEffect(); // lifeModeScene::LevelUpEffect 0xd0024
                    int L0 = Profile.level[heroType] - 1;
                    int hp = 15 * L0 + Dc2Data.Heroes()[heroType].hp;
                    if (heroType == 4) hp = Dc2Data.GirlHp(hp, Profile.level[heroType]);
                    playerMaxHp = playerHp = N.HpWithJigsaw(hp, Profile.jigsaw1Count);   // leveling up restores HP to full (Visuals Description)
                    UpdateHpBar();
                }
                else Profile.exp[heroType] = thr;
            }
            UpdateExpBar();
        }

        void UpdateExpBar()
        {
            if (expBar == null) return;
            int thr = ExpThreshold; int e = Profile.exp[heroType];
            Rect full = N.Layout(57, 21, 158, 4);
            if (e > 0 && thr > 0) { expBar.SetVisible(true); expBar.SetRect(new Rect(full.x, full.y, full.width * ((100 * e / thr) * 0.01f), full.height)); }
            else expBar.SetVisible(false);
        }

        // ---- pause ----
        UiControl pauseDim;
        readonly List<UiControl> pauseItems = new List<UiControl>();
        public bool pauseMenuReady;

        /// <summary>mainScene::OnSystemEvent 0x9636a / lifeModeScene::OnSystemEvent 0xd0958 (identical): event 0 while the scene state is 4 (playing) stops time (SetTimeFactorEx 0), enters state 6, TadShow, creates pauseCtrl id 16 and Enter()s it, clears the five g_b_*Down flags. Any other event or state: nothing. Trigger source on iOS is the application interruption; the desktop mapping lives in Dc2Game.pauseOnFocusLoss (PLATFORM_ADAPTER, off).</summary>
        public void OnSystemEvent(int ev) { if (ev == 0 && levelState == 4) PauseGame(false); }   // state 4 only (0x9636a); no boss-hp check on this path

        public void PauseGame() { PauseGame(true); }
        void PauseGame(bool fromStopButton)
        {
            // Native gate = scene state 4 (OnTouchEvent stop rect / OnSystemEvent): the intro (state 1/3), GameOverStart (7) and the fail banner (8) block it, but
            // GameClear leaves the state at 4 and the hero's die animation runs in state 4 too, so pausing after a win or while dying is possible (shipped quirk, r118x).
            if (paused || IntroActive || sceneMode == 7 || sceneMode == 8) return;
            if (fromStopButton && missionType == 8 && Boss != null && Boss.hp < 0f) return;      // lifeModeScene::OnTouchEvent 0xd1ba4 (stop button only): the stop button does nothing in a boss mission once g_boss[line].hp < 0 (killed, mission end pending)
            paused = true; pauseMenuReady = false;
            fireDown = hitHeld = leftDown = rightDown = false;                       // g_b_*Down cleared on entry
            // pauseCtrl::Initialize 0xcf05e: whiteFrame backdrop colour 0xC0000000 (black, alpha 192)
            pauseDim = CreateControl(60, 6); pauseDim.SetTexture("whiteFrame");
            pauseDim.SetRect(new Rect(0, 0, N.ScreenW, N.ScreenH)); pauseDim.SetColor(new Color32(0, 0, 0, 192)); pauseDim.touchable = false;
            pauseItems.Clear();
            // child rects (480x320 space) from the pauseCtrl ctor 0xce670 literal table; children slide in per Enter 0xced0c
            pauseLeaving = false;                                                       // pauseCtrl+208 = 0 (Enter 0xced0c)
            AddPauseItem(61, 0, "op_titleFrame", 169, 34, 151, 34, false);
            AddPauseItem(62, 1, "op_soundFrame", 106, 99, 101, 25, false);
            AddPauseItem(63, 2, "op_musicFrame", 106, 143, 101, 25, false);
            AddPauseItem(64, 4, Profile.fx != 0 ? "op_on1Frame" : "op_off1Frame", 319, 95, 55, 29, true);     // g_fx toggle (native child 4, OnFx)
            AddPauseItem(65, 3, Profile.sound != 0 ? "op_on1Frame" : "op_off1Frame", 319, 142, 55, 29, true); // g_sound toggle (native child 3, OnMusic)
            AddPauseItem(66, 5, "ui_btn_resumeFrame", 366, 236, 110, 48, true);
            AddPauseItem(67, 6, "ui_btn_retryFrame", 182, 197, 123, 45, true);
            AddPauseItem(68, 7, "ui_btn_mapFrame", 5, 236, 109, 48, true);
        }

        bool pauseLeaving;   // pauseCtrl+208 (0 after Enter, 1 after Leave)

        /// <summary>pauseCtrl::Enter 0xced0c: per child, centre-based keys start (cx + 480S), mid (cx - 48S), end (cx), 0.2 s then 0.05 s (floats 0x3E4CCCCD / 0x3D4CCCCD). iPad: the resume and map buttons (children 5 and 7) get +38 px width (0x42180000 added to the width read back from the control; the centre is recomputed from it).</summary>
        void AddPauseItem(int id, int index, string frame, float x, float y, float w, float h, bool interactive)
        {
            var c = CreateControl(id, 7);
            c.SetTexture(frame);
            Rect r = N.Layout(x, y, w, h);
            c.SetRect(r); c.touchable = interactive;
            float s = N.S, cw = (index == 5 || index == 7) ? N.Add32(r.width, 38f) : r.width;
            float cx = N.Add32(N.Mul32(cw, 0.5f), r.x), cy = N.Add32(N.Mul32(r.height, 0.5f), r.y);
            Color32 white = new Color32(255, 255, 255, 255);
            c.StartMove(new MoveData(N.Add32(cx, N.Mul32(s, 480f)), cy, cw, r.height, white), new MoveData(N.Add32(cx, N.Mul32(s, -48f)), cy, cw, r.height, white),
                        new MoveData(cx, cy, cw, r.height, white), 0.2f, 0.05f);
            pauseItems.Add(c);
        }

        /// <summary>pauseCtrl::Leave 0xce93c (scene event 3, raised by the resume click): every child slides out: start = current centre, mid (cx + 48S), end (cx - 480S), 0.05 s then 0.2 s; +208 = 1. The move end of child 0 then raises scene event 6 (resume).</summary>
        void LeavePause()
        {
            float s = N.S; var white = new Color32(255, 255, 255, 255);
            foreach (var c in pauseItems)
            {
                if (c == null) continue;
                Rect r = c.FullRect;
                float cx = N.Add32(N.Mul32(r.width, 0.5f), r.x), cy = N.Add32(N.Mul32(r.height, 0.5f), r.y);
                c.StartMove(new MoveData(cx, cy, r.width, r.height, white), new MoveData(N.Add32(cx, N.Mul32(s, 48f)), cy, r.width, r.height, white),
                            new MoveData(N.Add32(cx, N.Mul32(s, -480f)), cy, r.width, r.height, white), 0.05f, 0.2f);
            }
            pauseLeaving = true;
        }

        void ClosePause()
        {
            foreach (var c in pauseItems) if (c != null) DeleteControl(c.id);
            pauseItems.Clear(); DeleteControl(60);
            paused = false;                                                           // SetTimeFactor(1.0), state 4
        }

        bool HandlePauseEvent(UiControl c, int ev)
        {
            if (!paused) return false;
            if (ev == 1 && c.id == 61) { if (pauseLeaving) ClosePause(); return true; }   // child 0 move end: state 1 -> scene event 6 (DeleteControl 16, SetTimeFactor 1, mode 4); state 0 -> event 5 (no handler)
            if (ev != 0) return false;
            switch (c.id)
            {
                case 65:   // pauseCtrl::OnMusic 0xce384: g_sound ^= 1, g_ongame_sound = g_sound; on -> PlayMusic("Music_Bg_Audio", loop, "game_bgAudio"), off -> AudioManager::Stop("game_bgAudio"). No SaveFile.
                    Profile.sound = Profile.sound != 0 ? 0 : 1; Game.OngameSound = Profile.sound; c.SetTexture(Profile.sound != 0 ? "op_on1Frame" : "op_off1Frame");
                    if (Profile.sound != 0) Audio.PlayMusic("Music_Bg_Audio", true, "game_bgAudio"); else Audio.Stop("game_bgAudio");
                    return true;
                case 64:   // pauseCtrl::OnFx 0xce1ec: g_fx ^= 1, g_ongame_fx = g_fx, texture only
                    Profile.fx = Profile.fx != 0 ? 0 : 1; Game.OngameFx = Profile.fx; c.SetTexture(Profile.fx != 0 ? "op_on1Frame" : "op_off1Frame");
                    return true;
                case 66: if (!pauseLeaving) LeavePause(); return true;                                                                  // child 5 click -> scene event 3 (Leave); the resume itself runs on event 6
                case 67: Audio.Stop("game_bgAudio"); ClosePause(); Game.MapType = 6; Game.SwitchScene("selectScene"); return true;      // child 6 -> event 7: Stop game_bgAudio, SetTimeFactor 1, m_map_type 6 (mission-state counter [3]++ / SaveMissionInfo: telemetry-only, EXCLUDED r118w)
                case 68: Audio.Stop("game_bgAudio"); ClosePause(); Game.MapType = 0; Game.SwitchScene("selectScene"); return true;      // child 7 -> event 4: Stop game_bgAudio, m_map_type 0 (mission-state counter [2]++ / SaveMissionInfo: telemetry-only, EXCLUDED r118w)
            }
            return false;
        }
    }
}
