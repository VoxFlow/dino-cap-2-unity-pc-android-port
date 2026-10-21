using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dc2Pilot
{
    /// <summary>Girl hero (g_player_type 4, playerNewAniClass 0x124f82..0x12bd44): a CompAnimObj component rig, not a CA rig. Base channel = stand/move/hit/behit/roll/die component
    /// animations (g_player_* ComponentAnimations, short family for weapon type 0, long family for types 1..5: ChangeWeapon 0x1298d0), advanced channel = fire animation composed on top
    /// (OnFireDown 0x12a178 / OnKeyFrameAdv 0x12bd44 / OnAnimationEndAdv 0x1261d2 / OnAnimationEnd 0x12aa2c). Evidence: Dev/Evidence/girl_decomp_r114.md.
    /// Implemented: Qianghuo (muzzle flash, 0x12530c, effectClassN, never rotated (no SetRotation on the effectClassN pool)), Danke (shell casing 0x126804), getEgg/getDownEgg body avatar, die blood drops, roll sounds (r118v: the old OPEN list was stale).</summary>
    public partial class GameScene
    {
        public bool IsGirl { get { return heroType == 4; } }
        NativeCompObj girl;
        GameObject girlRoot;
        static readonly Dictionary<string, NativeComponentAnimation> girlAcb = new Dictionary<string, NativeComponentAnimation>();
        NativeComponentAnimation gStand, gMove, gFire, gBehit, gHit, gRollStart, gRollEnd;
        bool girlShortFamily = true;
        string girlBaseName = "";                       // CompAnimObj+32 (animation name): "DieAnim" gates GameOverStart
        public int GirlRollPhase;                       // playerNewAniClass+368
        int girlBurstCounter;                           // +464 (reset by OnFireDown for types 2/3)
        readonly List<KeyValuePair<string, string>> girlAvatars = new List<KeyValuePair<string, string>>();   // +480
        public bool GirlDieEnded { get; private set; }
        // DEBUG_ONLY accessors
        public string GirlBaseName { get { return girlBaseName; } }
        public bool GirlSelectionIsDefault { get { return girl == null || girl.SelectionIsDefault(); } }   // DEBUG_ONLY
        public bool GirlAdvActive { get { return girl != null && girl.AdvActive; } }
        public int GirlFrame { get { return girl != null ? girl.frame : -1; } }
        public int GirlAdvFrame { get { return girl != null ? girl.advFrame : -1; } }
        public bool GirlShortFamily { get { return girlShortFamily; } }
        public float GirlRollVelocity { get { return rollVelocity; } }
        public Vector2 GirlPosition { get { return girl != null ? girl.position : Vector2.zero; } }
        public float GirlObjectScale { get { return girl != null ? girl.objectScale : 0f; } }
        public bool GirlHasSprite(string name) { return girl != null && girl.Sprites.ContainsKey(name); }
        public void SelectSlotForTest(int slot) { SelectWeaponSlot(slot); }
        public void HitDownForTest() { OnHitDown(); }
        public string GirlSelection(string comp) { return girl != null ? girl.SelectionOf(comp) : ""; }

        static NativeComponentAnimation GA(string name)
        {
            NativeComponentAnimation a;
            if (!girlAcb.TryGetValue(name, out a)) { a = NativeComponentAnimation.Load("DC2/Image/acb/ipad/" + name + ".animation.acb"); girlAcb[name] = a; }
            return a;
        }

        void GirlEnsure()
        {
            if (girl != null) return;
            girlRoot = new GameObject("native_girl_rig"); girlRoot.transform.SetParent(Rig.transform, false);
            girl = new NativeCompObj(girlRoot.transform) { objectScale = HeroRigScale };
            girl.onKeyFrame = GirlOnKeyFrame; girl.onKeyFrameAdv = GirlOnKeyFrameAdv;
            girl.onAnimationEnd = GirlOnAnimationEnd; girl.onAnimationEndAdv = GirlOnAnimationEndAdv;
            gRollStart = GA("player_roll_start"); gRollEnd = GA("player_roll_end"); gDie = GA("player_die"); gHit = GA("player_hit");
            GirlChangeWeapon(true);
        }

        NativeComponentAnimation gDie;

        /// <summary>playerNewAniClass::ChangeWeapon 0x1298d0: weapon avatar pair on "bshouqiang", the short/long ComponentAnimation family, re-bind the base/advanced animations for the current state.</summary>
        void GirlChangeWeapon(bool initial = false)
        {
            var slot = Dc2GameData.PlayerWeapons(4)[weaponSlot];
            string avatar; bool? shortFamily = null;
            switch (slot.type)
            {
                case 0: avatar = "newplayer_shouqiang001"; shortFamily = true; break;
                case 1: case 2: avatar = "newplayer_sandan"; shortFamily = false; break;
                case 3: avatar = "newplayer_gelin"; shortFamily = false; break;
                case 4: avatar = "newplayer_liudan"; shortFamily = false; break;
                case 5: avatar = "newplayer_jujiqiang001"; shortFamily = false; break;
                case 6: avatar = ""; shortFamily = true; break;
                default: return;
            }
            if (shortFamily.HasValue) girlShortFamily = shortFamily.Value;
            string fam = girlShortFamily ? "short" : "long";
            gStand = GA("player_stand_" + fam); gMove = GA("player_move_" + fam); gFire = GA("player_fire_" + fam); gBehit = GA("player_behit_" + fam);
            int state = heroControlState;
            if (state == 0 || state == 1) { girl.SetAnimation(gMove); girl.SetAnimationAdv(gFire); }
            else if (state == 2) { girl.SetAnimation(gStand); girl.SetAnimationAdv(gFire); }
            else if (state == 7) girl.SetAnimation(gRollStart);
            if (initial && girl.baseAnim == null) { girl.SetAnimation(gStand); girl.SetAnimationAdv(gFire); }
            girlAvatars.RemoveAll(p => p.Key == "bshouqiang");
            girlAvatars.Add(new KeyValuePair<string, string>("bshouqiang", avatar));
            girl.SetAvatar(girlAvatars);
        }

        void GirlSetAvatar(string comp, string avatar)
        {
            girlAvatars.RemoveAll(p => p.Key == comp);
            girlAvatars.Add(new KeyValuePair<string, string>(comp, avatar));
            girl.SetAvatar(girlAvatars);
        }

        void GirlRemoveAvatar(string comp)
        {
            girlAvatars.RemoveAll(p => p.Key == comp);
            girl.SetAvatar(girlAvatars);
        }

        // ---- playerNewAniClass::Stand 0x124f82 / Move 0x125072 ----
        void GirlStand()
        {
            GirlEnsure();
            girl.SetAnimation(gStand); girlBaseName = "stand";
            girl.PlayAnimation(uint.MaxValue, 30f, 0f, -1f);
        }

        void GirlMove()
        {
            GirlEnsure();
            girl.SetAnimation(gMove); girlBaseName = "move";
            girl.PlayAnimation(uint.MaxValue, 30f, 0f, -1f);
        }

        void GirlPlace()
        {
            if (girl == null) return;
            girl.position = Rig.Player.position;
            girl.flipX = facingLeft;
        }

        void GirlStep(float dt)
        {
            if (girl == null) return;
            GirlPlace();
            girl.Step(dt);
        }

        // ---- hit (OnHitDown 0x127954) ----
        void GirlHit()
        {
            GirlEnsure();
            girl.SetAnimation(gHit); girlBaseName = "hit";
            if (Profile.weaponLevels[4 * 6 + 5] >= Dc2GameData.PlayerMelee(4).maxLevel) GirlSetAvatar("bwuqi", "newplayer_chui2");   // v13 >= v18 (max level hammer)
            girl.keyFrame = 11;
            girl.PlayAnimation(1u, 30f, 0f, -1f);
        }

        void GirlOnKeyFrame(int key)
        {
            if (heroControlState == 3 && key == 11) JinAttack();                        // OnKeyFrame 0x129180
        }

        // ---- behit (BeAttack 0x1282b8) ----
        void GirlBehit()
        {
            GirlEnsure();
            girl.SetAnimation(weaponSlot != 0 ? GA("player_behit_long") : GA("player_behit_short")); girlBaseName = "behit";
            girl.PlayAnimation(1u, 30f, 0f, -1f);
        }

        // ---- roll (Roll 0x126626, OnAnimationEnd state 7, OnTimeout "roll") ----
        void GirlRoll()
        {
            GirlEnsure();
            if (girlBaseName != "roll")
            {
                girl.SetAnimation(gRollStart); girlBaseName = "roll";
                girl.PlayAnimation(1u, 30f, 0f, -1f);
                GirlRollPhase = 0;
            }
        }

        void GirlRollTimeout()
        {
            {
                // OnTimeout "roll" 0x127f78: velocity 0, SetAnimation(roll_end "playerRoll2Animation"), PlayAnimation(1,30,0,-1), phase 2
                heroVelocityX = 0f; rollVelocity = 0f; GirlRollPhase = 2;   // SetLinearVelocity(0) on the single native body velocity
                Audio.PlaySound("p5rollAudio");                                   // OnTimeout "roll" 0x128024
                girl.SetAnimation(gRollEnd); girlBaseName = "roll2";
                girl.PlayAnimation(1u, 30f, 0f, -1f);
            }
        }

        // ---- die (Die 0x126a36) ----
        void GirlDie()
        {
            GirlEnsure();
            girl.ClearAvatar();   // playerNewAniClass::Die 0x126a9c: ClearAvatar before SetAnimation(g_player_die): the death pose has no weapon/skin avatars
            girl.SetAnimation(gDie); girlBaseName = "DieAnim";
            girl.PlayAnimation(1u, 30f, 0f, -1f);
            GirlDieEnded = false;
        }

        // ---- base-channel end (OnAnimationEnd 0x12aa2c) ----
        void GirlOnAnimationEnd()
        {
            switch (heroControlState)
            {
                case 3:
                    if (hitHeld) { girl.keyFrame = 11; girl.PlayAnimation(1u, 30f, 0f, -1f); }   // chained melee: g_b_HitDown -> replay
                    else
                    {
                        meleeing = false; heroControlState = 2; heroVelocityX = 0f; rigState = -1; SetRigState(0);
                        if (fireDown) NativeFireDown();
                    }
                    break;
                case 4:
                    if (girlBaseName == "DieAnim") GirlDieEnded = true;                          // GameOverStart (GameMission polls)
                    break;
                case 5: EndNativeHeroHurt(); break;
                case 7:
                    if (GirlRollPhase == 0)
                    {
                        float dir = facingLeft ? -1f : 1f;
                        rollVelocity = N.Mul32(dir * 3f, WalkSpeedFor(4));                       // +/-3 * walk speed (0x40400000 * dword_240478)
                        GirlRollPhase = 1; SetTimer(heroTimer, "roll", 0.5f, GirlRollTimeout);   // SetTimer("roll", 0.5)
                    }
                    else if (GirlRollPhase == 2)
                    {
                        rollVelocity = 0f; rolling = false; nativeCamera.turning = false;
                        heroControlState = 2; heroVelocityX = 0f; rigState = -1; SetRigState(0);
                    }
                    break;
                case 9:
                    fireDown = false; firing = false;
                    heroControlState = 2; rigState = -1; SetRigState(0);
                    break;
                case 10: break;
            }
        }

        // ---- advanced fire channel ----
        bool GirlFireImplemented(PlayerWeaponSlot slot)
        {
            return slot.type <= 4 || (slot.type == 5 && (slot.subtype == 0 || slot.subtype == 1 || slot.subtype == 2 || slot.subtype == 5));
        }

        /// <summary>OnFireDown 0x12a178 weapon switch (state/gate/ammo already checked by NativeFireDown).</summary>
        void GirlFireDown(PlayerWeaponSlot slot)
        {
            GirlEnsure();
            switch (slot.type)
            {
                case 0:
                    girl.SetAnimationAdv(gFire);
                    if (firing) { girl.advKeyFrame = 1; girl.PlayAnimationAdv(1u, 30f, 1f, -1f); }
                    else { girl.advKeyFrame = 2; girl.PlayAnimationAdv(1u, 30f, 0f, -1f); firing = true; }
                    break;
                case 1:
                    if (firing) return;
                    girl.SetAnimationAdv(gFire); girl.advKeyFrame = 1; girl.PlayAnimationAdv(1u, 30f, 0f, 10f); firing = true;
                    break;
                case 2: case 3:
                    if (firing) return;
                    girl.SetAnimationAdv(gFire);
                    girl.SetAdvCompAni("bshouqiang", "newplayer_gelin", "Animation01");
                    girl.advKeyFrame = 1; girl.PlayAnimationAdv(1u, 30f, 0f, 6f); firing = true; girlBurstCounter = 0;
                    break;
                case 4:
                    if (firing) return;
                    girl.SetAnimationAdv(gFire); girl.advKeyFrame = 1; girl.PlayAnimationAdv(1u, 30f, 0f, 12f); firing = true;
                    break;
                case 5:
                    if (firing) return;
                    girl.SetAnimationAdv(gFire); girl.advKeyFrame = 1;
                    if (slot.subtype == 1 || slot.subtype == 2) girl.PlayAnimationAdv(1u, 25f, 0f, 4f);
                    else if (slot.subtype == 5) girl.PlayAnimationAdv(1u, 30f, 0f, 15f);
                    else girl.PlayAnimationAdv(1u, 30f, 0f, -1f);
                    firing = true;
                    break;
            }
            LastFireEvent = "Girl OnFireDown type " + slot.type;
        }

        /// <summary>OnKeyFrameAdv 0x12bd44: not in states 4/7; types 0..3 KaiQiang, 4 LiuDanQ, 5 sub 5 KaiQiang; then SubBullet 0x125292.</summary>
        void GirlOnKeyFrameAdv(int key)
        {
            if (heroControlState == 7 || heroControlState == 4) return;
            var slot = Dc2GameData.PlayerWeapons(4)[weaponSlot];
            switch (slot.type)
            {
                case 0: case 1: case 2: case 3: GirlKaiQiang(slot); break;
                case 4: GirlLiuDanQ(slot); break;
                case 5: if (slot.subtype == 5) GirlKaiQiang(slot); break;
            }
            GirlSubBullet();
        }

        void GirlSubBullet()
        {
            int idx = CurrentAmmoIndex;
            int ammo = Profile.weaponAmmo[idx] - (Game.DebugInfiniteAmmo ? 0 : 1);   // DEBUG_ONLY infinite ammo (debug menu); native always subtracts 1
            if (ammo <= 0) { fireDown = false; ammo = 0; }
            Profile.weaponAmmo[idx] = ammo;
            UpdateBullet();                                                   // SubBullet 0x125292 -> mainScene::UpdateBullet
        }

        /// <summary>OnAnimationEndAdv 0x1261d2 (states 3/4/7 ignore): per weapon type, single cycle for 0/1/4, held loops for 2/3 and type 5 sub 1/2.</summary>
        void GirlOnAnimationEndAdv()
        {
            if (heroControlState == 3 || heroControlState == 7 || heroControlState == 4) return;
            var slot = Dc2GameData.PlayerWeapons(4)[weaponSlot];
            if (Profile.weaponAmmo[CurrentAmmoIndex] <= 0 && !Game.DebugInfiniteAmmo) fireDown = false;
            switch (slot.type)
            {
                case 1:
                    firing = false; fireDown = false; ArmFireup();
                    if (girl.advEnd == 10) girl.PlayAnimationAdv(1u, 30f, 10f, -1f);
                    break;
                case 2: case 3:
                    if (fireDown) { girl.advKeyFrame = 3; girl.PlayAnimationAdv(1u, 30f, 3f, 6f); }
                    else if (girl.advEnd == 6) girl.PlayAnimationAdv(1u, 30f, 6f, -1f);
                    else { firing = false; fireDown = false; ArmFireup(); }
                    break;
                case 4:
                    firing = false; fireDown = false; ArmFireup();
                    if (girl.advEnd == 12) girl.PlayAnimationAdv(1u, 30f, 12f, -1f);
                    break;
                case 5:
                    switch (slot.subtype)
                    {
                        case 0: case 3: firing = false; fireDown = false; break;
                        case 5:
                            firing = false; fireDown = false; ArmFireup();
                            if (girl.advEnd == 15) girl.PlayAnimationAdv(1u, 30f, 15f, -1f);
                            break;
                        default: firing = false; fireDown = false; break;
                    }
                    break;
                default: firing = false; fireDown = false; break;
            }
        }

        /// <summary>playerNewAniClass::KaiQiang 0x12b65c: sound by type/subtype, lian face avatar for burst weapons (counter +464), Attack (shared scan), Danke/Qianghuo visuals implemented.</summary>
        void GirlKaiQiang(PlayerWeaponSlot slot)
        {
            string lian = "";
            switch (slot.type)
            {
                case 0: Audio.PlaySound("Attack_AAudio"); break;
                case 1: Audio.PlaySound("Attack_BAudio"); break;
                case 2:
                    Audio.PlaySound(slot.subtype <= 1 ? "Attack_DAudio" : "Attack_E1Audio");
                    { int c = girlBurstCounter++; if ((uint)(c - 9) <= 19u || (uint)(c - 29) <= 19u || c + 1 > 49) lian = "newplayer_lian4"; }
                    break;
                case 3:
                    { int c = girlBurstCounter++; if ((uint)(c - 9) <= 19u) lian = "newplayer_lian3"; else if ((uint)(c - 29) <= 19u) lian = "newplayer_lian4"; else if (c + 1 > 49) lian = "newplayer_lian4"; }
                    Audio.PlaySound("Attack_CAudio");
                    break;
                case 5: if (slot.subtype == 5) Audio.PlaySound("Attack_F6Audio"); break;
            }
            // pair ("lian", avatar) replaces any earlier face pair (an empty avatar string resolves to no component and leaves the selection unchanged)
            girlAvatars.RemoveAll(p => p.Key == "lian");
            girlAvatars.Add(new KeyValuePair<string, string>("lian", lian));
            girl.SetAvatar(girlAvatars);
            ShotsFired++;
            HeroAttack();
            Vector2 gp; NativeCompObj.SpriteInfo gi;
            if (GirlSpriteWorld("bshouqiang", out gp, out gi))
            {
                string frame; Vector2 fsize; int order;                                       // KaiQiang 0x12bbb4..0x12bc06: Danke at (x, y + 0.2 * height) of "bshouqiang", then Qianghuo
                if (ShellTable(slot, true, out frame, out fsize, out order) && bloodWorld != null)
                    bloodWorld.Danke(new Vector2(N.Add32(gp.x, 0f), N.Add32(N.Mul32(0.2f, gi.h), gp.y)), fsize, frame, order, facingLeft);
                GirlQianghuo(slot, gp);                                                       // 0x12bc42
            }
        }

        // playerNewAniClass::Qianghuo 0x12530c offsets (A, B) per [type][moving][subtype]: x offset = (int)((S + 0) * A), y += S * B, S = 2 on iPad; unlisted entries: none.
        static readonly Dictionary<int, float[][][]> girlQh = new Dictionary<int, float[][][]>
        {
            { 0, new[] { new[] { new[]{10f,10f}, new[]{20f,15f}, new[]{15f,10f}, new[]{20f,12f}, new[]{5f,7f} }, new[] { new[]{20f,5f}, new[]{30f,10f}, new[]{25f,5f}, new[]{30f,7f}, new[]{20f,7f} } } },
            { 1, new[] { new[] { new[]{40f,12f}, new[]{40f,12f}, new[]{40f,12f}, new[]{30f,12f}, new[]{40f,12f} }, new[] { new[]{50f,12f}, new[]{50f,12f}, new[]{50f,12f}, new[]{40f,5f}, new[]{50f,12f} } } },
            { 2, new[] { new[] { new[]{20f,20f}, new[]{20f,25f}, new[]{20f,10f}, new[]{20f,5f} }, new[] { new[]{30f,12f}, new[]{30f,15f}, new[]{30f,10f}, new[]{30f,5f} } } },
            { 3, new[] { new[] { new[]{40f,8f}, new[]{30f,10f}, new[]{30f,8f}, new[]{35f,5f}, new[]{30f,5f} }, new[] { new[]{50f,5f}, new[]{40f,8f}, new[]{40f,5f}, new[]{45f,0f}, new[]{45f,0f} } } },
        };

        /// <summary>playerNewAniClass::Qianghuo 0x12530c: effectClassN qhEffectAnimation (g_player_fire_eff, layer 9) at pos + offset; objectScale 1.25 (type 0) else 1.5; sniper (5/5) adds g_player_eff_zidan at scale 1.0.</summary>
        void GirlQianghuo(PlayerWeaponSlot slot, Vector2 pos)
        {
            float off = 0f, dy = 0f; float s2 = 2f;
            bool moving = leftDown || rightDown;
            float[] row = null;
            float[][][] t;
            if (girlQh.TryGetValue(slot.type, out t)) { var m = t[moving ? 1 : 0]; if (slot.subtype >= 0 && slot.subtype < m.Length) row = m[slot.subtype]; }
            else if (slot.type == 4 && slot.subtype == 2) row = new[] { moving ? 50f : 40f, 10f };
            else if (slot.type == 5 && slot.subtype == 5) row = new[] { moving ? 70f : 55f, 8f };
            if (row != null) { off = (int)N.Mul32(s2, row[0]); dy = N.Mul32(s2, row[1]); }
            float x = facingLeft ? N.Add32(pos.x, -off) : N.Add32(pos.x, off);
            float y = N.Add32(pos.y, dy);
            SpawnCompFx("qh", GA("player_fire_eff"), x, y, facingLeft, 9, slot.type == 0 ? 1.25f : 1.5f);
            if (slot.type == 5 && slot.subtype == 5) SpawnCompFx("zidan", GA("player_eff_zidan"), x, y, facingLeft, 9, 1f);
        }

        /// <summary>Girl getEgg 0x126e2e / getDownEgg 0x1270fa: the "shenti" component takes the egg-carrying avatar newplayer_shenti2 (selection persists until the next SetAnimation).</summary>
        void GirlGetEgg() { if (girl != null) GirlSetAvatar("shenti", "newplayer_shenti2"); }
        void GirlGetDownEgg() { if (girl != null) GirlRemoveAvatar("shenti"); }

        /// <summary>World position (x relative to camera frame converted back to world x) of a named sprite of the Girl rig.</summary>
        bool GirlSpriteWorld(string name, out Vector2 world, out NativeCompObj.SpriteInfo info)
        {
            world = Vector2.zero; info = default(NativeCompObj.SpriteInfo);
            if (girl == null || !girl.Sprites.TryGetValue(name, out info)) return false;
            world = new Vector2(info.x + camLeft + WorldViewportW * 0.5f, info.y);
            return true;
        }

        /// <summary>playerNewAniClass::LiuDanQ 0x12b1ba: gun sprite by subtype, blast at the sprite x -/+ 2*150, y + 2*102 (iPad v39 = 2), radius 2*180; baozha layer 12; Attack_E2Audio.</summary>
        void GirlLiuDanQ(PlayerWeaponSlot slot)
        {
            string gun = slot.subtype == 0 ? "yyj5" : slot.subtype == 1 ? "com1" : slot.subtype == 2 ? "bshouqiang" : null;
            Vector2 p; NativeCompObj.SpriteInfo info;
            if (gun == null || !GirlSpriteWorld(gun, out p, out info)) return;
            GirlQianghuo(slot, p);                                                           // 0x12b2aa Qianghuo(sprite pos)
            float s2 = 2f;                                                                   // v39: g_iphone ? 1 : 2
            float bx = N.Add32(p.x, N.Mul32(s2, facingLeft ? -150f : 150f));
            float by = N.Add32(p.y, N.Mul32(s2, 102f));
            SpawnCaFx("baozha", FxAnimation("DC2/Image/a2b/baozha.a2b", false), bx, by, facingLeft, 12, 1f);
            var w = CurrentWeapon();
            float radius = N.Mul32(s2, 180f);
            var victims = new List<DinoActor>();
            foreach (var d in dinos)
            {
                if (d.dead || !d.targetable || d.state == 3) continue;
                if (Mathf.Abs(N.Add32(d.x, -bx)) < radius) victims.Add(d);
            }
            bool bossHit = Boss != null && Boss.kind != 4 && Boss.sub != 0 && Mathf.Abs(N.Add32(Boss.x, -bx)) < radius;
            foreach (var d in victims) BeAttacked(d, w, Mathf.Abs(heroX - d.x));
            if (bossHit) BossBeAttacked(w);
            Audio.PlaySound("Attack_E2Audio");
            ShotsFired++;
        }

        /// <summary>OnTimeout "fireup" 0x127dac: removes the lian face pair (the selected avatar stays until the next SetAnimation).</summary>
        void GirlFireupTimeout()
        {
            if (girl != null) GirlRemoveAvatar("lian");
        }
    }
}
