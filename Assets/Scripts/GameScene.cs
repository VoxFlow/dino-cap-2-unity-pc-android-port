using System.Collections.Generic;
using UnityEngine;

namespace Dc2Pilot
{
    /// <summary>
    /// First playable battle frame (lifeModeScene / mainScene destination of the mission launch).
    /// NATIVE (registry dc2.lifeMode.*, Evidence/lifeMode_hud_layout.json): HUD controls and rects from the
    /// Unicorn-emulated lifeModeScene::OnLoad 0xe7202 (every RectScale is followed by RectOffset -> N.Layout);
    /// walk velocity playerClass::UpdateControl 0xb8a60 (+/-dword_240478 = iPad scale 2 * 150, hero 3: 2*200 per OnLoad 0xe87ae/0xe9b72);
    /// camera rule playerClass::UpdateCamera 0xbb054 (viewport left = hero x - 0.55*W facing left / - 0.45*W facing right, W=960);
    /// background layer geometry InitGameBackGround 0xdcaa6 (iPad factor 2).
    /// PA-011 remains for main/egg/Girl camera lifecycle, Girl pose families and complete physics/roll ordering.
    /// NativeBackground owns recovered layer geometry; NativeGameCamera covers heroes0..3 life modes2/3/7/8.
    /// </summary>
    public partial class GameScene : Dc2Scene
    {
        public const float WalkSpeed = 300f;          // 2.0 * 150 (iPad)
        public const float NinjaWalkSpeed = 400f;     // 2.0 * 200 (g_player_type == 3)
        public const float ViewW = 960f, ViewH = 640f; // g_width/height_Resolution
        public const float WorldViewportW = 1024f;    // Camera::Initialize uses Application width, independently of g_width_Resolution.
        /// <summary>ADAPTATION (WideView combat, user decision "follow visible view"): extra logical width on each side that the combat / spawn / despawn
        /// windows widen by while the wide view shows more world (0 when wide view is off, at 4:3, or on a narrower screen: every rule is then native).
        /// Native windows keep their own shape, e.g. the hittable window camLeft + anim.minX .. camLeft + WorldViewportW (1024) + anim.maxX (r119f; the old
        /// "960 / 64 px dead zone" wording is FALSIFIED) becomes the same window widened by e on each side.</summary>
        public static float WideCombatExtra { get { return Mathf.Max(0f, WideView.ExtraHalf); } }
        public const float GroundY = -110f;            // g_ground_y (iPad)
        public const float HeroRigScale = 1f;          // NATIVE CAObjCallBack ctor b086; iPad base loader g_scale=1.

        // lifeModeScene e993c..e9ad0: preserve vector component order and arithmetic.
        // These are spawn origins, not an art-bounds/feet alignment calculation.
        public static Vector2 NativeHeroSpawn(int hero)
        {
            float height = hero == 0 ? 2f * 125f - 5f : 2f * 135f - 25f;
            if (hero == 4) height = 5f;
            return new Vector2(2f * -120f, GroundY + height);
        }

        public int heroType;
        bool heroCameraThisTick = true;               // HeroUpdatesCamera sampled in this tick's hero update
        public bool leftDown, rightDown;               // native g_b_LeftDown / g_b_RightDown
        public bool facingLeft;                        // playerClass+96
        public float heroX, camLeft;
        public HeroPreview Rig { get; private set; }
        public int rigState = -1;                      // 0 stand, 1 move

        public NativeBackground Background { get; private set; }
        readonly NativeGameCamera nativeCamera = new NativeGameCamera();
        public float heroVelocityX;
        public int heroControlState=2;
        bool nativeWorldPrepared;
        // r118z: the heroes 1/3 special roll no longer drops the whole frame (dinosaurs included) to the legacy port order; its body velocity is integrated in the
        // physics position like every other roll (playerClass::Roll 0xb95f8 sets it with SetLinearVelocity). The legacy branch is only reachable by a mission type
        // outside Map.xml's 1/2/3/5/7/8.
        bool UsesRecoveredWorldPhase { get { return UsesRecoveredLifeCamera; } }
        float HeroBodyVelocity { get { return rolling ? rollVelocity : heroVelocityX; } }
        public bool RecoveredWorldPhaseForTest { get { return UsesRecoveredWorldPhase; } }   // DEBUG_ONLY verifier hook   // hero b2Body linear velocity (roll states write rollVelocity)
        bool UsesRecoveredLifeCamera { get { int t = Game.LaunchMission.type; return (t == 1 || t == 2 || t == 3 || t == 5 || t == 7 || t == 8); } }
        /// <summary>mainScene missions (run=1, egg=5): scene+136 = -480, scene+140 = min(20000, num*S) (mainScene::OnLoad trace); lifeMode: -240 / 840 (arena, boss) / unbounded.</summary>
        public bool MainSceneMission { get { int t = Game.LaunchMission.type; return t == 1 || t == 5; } }
        public float BoundLower { get { return MainSceneMission ? -480f : -240f; } }
        /// <summary>ADAPTATION (r119e, user decision: "in wide view, let the hero walk left until they reach the visible screen edge; 4:3 stays native").
        /// Natively the hero and the camera share the lower bound (UpdateCamera 0xbb19e / 0xbb4c0), so at the bound the hero stands on the screen's left edge.
        /// In wide view the visible edge is WideCombatExtra further left; only the hero clamp moves there, the camera keeps the native bound. 0 extra = native.</summary>
        public float HeroBoundLower { get { return EggWideCamera ? BoundLower : N.Add32(BoundLower, -WideCombatExtra); } }   // r119r: egg missions keep the native bound (the wide camera ends there)
        bool EggWideCamera { get { return Game.LaunchMission != null && Game.LaunchMission.type == 5; } }
        /// <summary>ADAPTATION (r119t, user decision "mirror it"; wide view only): the right-edge twin of HeroBoundLower. Natively the hero clamp equals the camera
        /// bound, so in arena / boss missions (scene+140 = 840, camera stops at 840 - 1024) the hero stops on the right screen edge; in wide view the visible edge is
        /// WideCombatExtra further right, so the clamp moves there (and the hero keeps the iPad distance to a right-side boss shifted by BossWideX). Egg missions keep the
        /// native bound (their camera clamp puts it on the visible edge, AD-005). 0 extra = native.</summary>
        public float HeroBoundUpper { get { return EggWideCamera ? BoundUpper : N.Add32(BoundUpper, WideCombatExtra); } }
        public float BoundUpper
        {
            get
            {
                int t = Game.LaunchMission.type;
                if (MainSceneMission) return Mathf.Min(20000f, N.Mul32(Game.LaunchMission.count, N.S));
                return t == 7 || t == 8 ? 840f : 50000f;
            }
        }

        UiControl bloodBar, expBar;
        UiLabel ammoLabel;

        public override void OnLoad()
        {
            Atlas.LoadNativeTable();
            if (!Atlas.Has("newplayer_toufaFrame")) Atlas.LoadByConf("DC2/Image/ip/Player2x", "DC2/Image/ip/Player2x.conf");
            SeedRng();
            heroType = Mathf.Clamp(Profile.playerType, 0, 4);
            if(UsesRecoveredLifeCamera)weaponSlot=Profile.selectedWeaponSlots[heroType]; // native life e9b1e..e9b34; no preview weapon override.
            NativeDrawOrder.Reset();   // before BuildBackground: houses (InitHouse) are CAObj objects too
            BuildBackground();
            BuildHud();
            AnchorHud();
            HeroDrawIndex = NativeDrawOrder.Allocate();   // playerClass is created by OnLoad before any dinosaur (CAObjCallBack 0xaff4 DrawOrerIndex += 50)
            Rig = HeroPreview.Create(Background.ActorRoot, HeroDrawIndex);
            SetRigState(0);
            facingLeft = false;
            heroX = NativeHeroSpawn(heroType).x;
            camLeft = HeroCameraLeft();
            // r119r: the r119k egg start shift is SUPERSEDED (the egg wide camera keeps native positions; see UpdateNativeCamera).
            if (UsesRecoveredLifeCamera) UpdateNativeCamera(0f);
            Audio.Stop("menu_bgAudio");
            Audio.RegisterAudio("Music_Bg_Audio", Background.MusicResource);
            Audio.PlayMusic("Music_Bg_Audio", true, "game_bgAudio"); // InitGameBackGround CreateAudio/PlayMusic e0ff8
            Place();
            CombatLoad();
            MissionLoad();
            UpdateBullet();                   // mainScene::OnLoad refreshes the ammo HUD (icon_none when the selected weapon has no ammo)
            IntroLoad();                      // OnLoad tail: cover, state 1, LevelBGEnter (NATIVE); +180 := 1 and the spawn timers follow in StartGame_N when the intro ends
        }

        float HeroCameraLeft() { return heroX - (facingLeft ? 0.55f : 0.45f) * ViewW; }   // UpdateCamera 0xbb0b0..0xbb15a

        /// <summary>r119d (user item 11): whether the hero's OnUpdate reaches UpdateCamera this tick (camera, actor clamp, run-end / exit-walk / egg-delivery tests).
        /// playerClass::OnUpdate 0xbb6e6: hidden (+659 == 0) -> UpdateCamera only in state 7. playerNewAniClass::OnUpdate 0x127866: +356 (control) == 0 -> return before
        /// UpdateCamera. So a teleport-dash hero stood by GameClear while hidden stays where it is until the "roll" timer shows it again, and the girl keeps her body
        /// velocity after the run-end frame (stopped only by a pending "roll" timer; otherwise she walks on, the exit-walk stop never runs).</summary>
        bool HeroUpdatesCamera
        {
            get
            {
                if (IsGirl) return heroInputEnabled;
                return HeroVisible || rolling;   // state 7 = rolling in the port
            }
        }

        void UpdateNativeCamera(float dt)
        {
            int type = Game.LaunchMission.type;
            // ADAPTATION (r119r, user: "egg nest should sit near the right edge and the truck-to-nest walk distance should stay native"; wide view only):
            // in egg missions the camera stops where its visible edge (WideCombatExtra beyond the native 1024 view) meets the native bounds, so the truck (left end)
            // and the nest (right end) sit on the visible edges at their native world positions and every walk distance is native. 0 extra = native.
            // Supersedes the r119g truck drawing shift and the r119k delivery line / start shift. Egg arenas are 2880..3480 wide (Map.xml num 1200..1500, S 2).
            float e = EggWideCamera ? WideCombatExtra : 0f;
            nativeCamera.Step(heroX, facingLeft, dt, N.Add32(BoundLower, e), N.Add32(BoundUpper, -e));
            camLeft = nativeCamera.left;
        }

        public override void OnUnload()
        {
            Audio.Stop("game_bgAudio"); Audio.DeleteAudio("Music_Bg_Audio"); // lifeModeScene OnUnload d3b06/d3b36
        }

        void BuildBackground()
        {
            // lifeModeScene OnLoadea25a..ea266 sets arena7; boss branch sets8 atea714..ea716.
            int background = Game.LaunchMission.type == 7 ? 7 : Game.LaunchMission.type == 8 ? 8 : Game.LaunchMission.background;
            Background = new NativeBackground(transform, background);
            BuildScenery(background);   // mainScene::InitBackGround 0x9aff2 / lifeModeScene::InitBackGround 0xd6a0e (bg 1/3/4/5)
        }

        /// <summary>ADAPTATION (WideView, DC2-BUGS-001 O1): every control / label OnLoad built is battle HUD; anchor each to the screen edge of the half its
        /// centre is in (render only). Controls created later (pause menu, banners, result dialogs) stay centred on the native screen.</summary>
        void AnchorHud()
        {
            foreach (var c in Controls) if (c != null) c.wideAnchor = WideView.AnchorOf(c.FullRect);
            foreach (var l in Labels) if (l != null) l.wideAnchor = WideView.AnchorOf(new Rect(l.AlignX, l.CurrentRect.y, 0f, l.CurrentRect.height));
            ReapplyWide();
        }

        void BuildHud()
        {
            Image(5, 0, "bg_bloodFrame", 50, 5, 168, 25);
            bloodBar = Image(6, 1, "bar_bloodFrame", 57, 6, 161, MainSceneMission ? 14 : 18);   // mainScene::OnLoad trace: 161x14
            expBar = Image(MainSceneMission ? 40 : 47, 1, "sd_baiFrame", 57, 21, 158, 4);   // id: lifeModeScene::OnLoad 47, mainScene::OnLoad 40 (id 47 is the egg icon there; r118 OnLoad emulation: the port had the egg icon replace the exp bar)
            // DC2-BUGS-001 B3 (r118z8): OnLoad tints the white sd_baiFrame green right after its rect, SetColor (137,236,149,255) = literal 0xFF95EC89
            // (mainScene::OnLoad 0xa8382, emulated, literal 0xa85bc; lifeModeScene::OnLoad literal 0xe7d2c, its only use, twin LIKELY). The port left it white (grey bar).
            expBar.SetColor(new Color32(137, 236, 149, 255));
            Image(7, 2, "bg_blood1Frame", 49, 4, 174, 49);
            // r119m: the weapon icon is CreateControl<UIMoveCtrl>(8, 2) with icon_bullet0Frame (lifeModeScene::OnLoad 0xe7b5c..0xe7b8c, stored in the scene for
            // SwitchWeapon; mainscene_hud_emulated_r118.json shows id 8 / layer 2 for every mission type). The port used id 20 / layer 3 (FALSIFIED): GameClear's band
            // (id 20) then deleted the icon, and a later switch turned the band into the icon. Natively the icon stays on screen during the complete sequence.
            ammoIcon = Image(8, 2, "icon_bullet" + Mathf.Min(Dc2GameData.PlayerWeapons(heroType)[0].type, 5) + "Frame", 0, 1, 61, 54);   // weapon-type icon (scene pointer used by SwitchWeapon)
            var iconNone = Image(21, 3, "icon_noneFrame", 0, 1, 61, 54); iconNone.SetVisible(false);   // shown by mainScene::UpdateBullet when the ammo is 0
            // r119m HUD ids (mainscene_hud_emulated_r118.json, ARM-checked: lifeModeScene::OnLoad 0xe7e6a CreateControl(15, 0) stop, 0xe7b5c (8, 2) weapon icon):
            // stop 15/0 in both scenes, gold icon 9 (mainScene) / 43 (lifeMode), roll button 22 / 28, change button 41 / 48. The port used stop 22/3 in lifeMode, gold 43
            // and change 30 everywhere and roll 28 everywhere (FALSIFIED ids: the fail banner (30) deleted the change button; GameClear's band (20) the weapon icon).
            var stop = CreateControl(15, 0); stop.SetTexture("icon_stopFrame"); stop.SetRect(N.Layout(445, 0, 34, 33)); stop.touchable = false;
            // R002-N2: creation order follows the native OnLoad streams (Dev/Evidence/mainscene_hud_emulated_r118.json): mainScene 15, 37/38 | 47/48, 9, 10, 97, 1..4, 22, 39, 41;
            // lifeModeScene 15, 43, 46, 10, 44, 9, 27, 97, 1..4, 28, 45, 48 (insertion order decides draw order inside a layer). FALSIFIED: labels and the run/egg icons after the controls.
            BuildMainSceneHud(new Color32(239, 205, 0, 255));   // mainScene: 37/38 (run) or 47/48 (egg) right after the stop icon
            Image(MainSceneMission ? 9 : 43, 0, "ui_game_goldFrame", 126, 29, 23, 22);
            if (!MainSceneMission) Image(46, 0, "ui_game_time_iconFrame", 275, -3, 30, 35);   // mainScene HUD has no time icon/label
            // Labels: rects, fonts, colours, spacing and layers from the bounded OnLoad emulation (Dev/Evidence/hud_labels_r119.json; UILabel ctor defaults: alignment (0,0) = top-left, no wrap). Text content NATIVE
            // (UpdateExp 0xd1f90 inserts "," every 3 digits). The earlier colours / sizes came from Showcase.mp4 (OBSERVED) and are superseded by the native values.
            var yellow = new Color32(239, 205, 0, 255);                                  // id 10 / 9 / 48: SetColor(239, 205, 0, 255)
            var gold = CreateLabel(10, MainSceneMission ? 0 : 2, 30); gold.SetRect(N.Layout(151, 25, 200, 50)); gold.SetColor(yellow); gold.SetNative("Adventure_16", 0f, 0f, 0, 0);
            gold.SetText(Profile.cash.ToString("N0", System.Globalization.CultureInfo.InvariantCulture));
            if (!MainSceneMission) Image(44, 0, "Lkill_hFrame", 300, 5, 29, 26);   // lifeModeScene::OnLoad creates the kill-counter icon (id 44, layer 0, uikillRect 300,5,29,26; r118 OnLoad emulation: the port never created it); MissionLoad prunes it for non-kill missions
            if (!MainSceneMission)
            {
                // lifeModeScene::OnLoad 0xe86ca: label 9 (layer 0, Adventure_16, 0xff00cdef = (239,205,0,255), no spacing calls) is the kill counter (+564, UpdateKillDinocap "%d/%d").
                // Rect (r119, DC2-BUGS-001 item 2): the code reuses the kill icon's rect temporary at SP+0xE10 (uikillRect after RectScale + RectOffset = (632,42,58,52), set 0xe86b6):
                // 0xe8778 h = S*30, 0xe8798 x = icon.x + S*30, 0xe87aa w = S*200, y stays icon.y -> SetRect 0xe87d8 = (692, 42, 400, 60). The r118z value (S*30, 0, S*200, S*30) came from an
                // emulation in which S read 0 and the icon offset was misattributed (FALSIFIED; Showcase.mp4 ~5:10 shows the counter starting at x ~693, validation only).
                Rect killIcon = N.Layout(300, 5, 29, 26);
                var kills = CreateLabel(9, 0, 30); kills.SetRect(new Rect(N.Add32(killIcon.x, N.Mul32(N.S, 30f)), killIcon.y, N.Mul32(N.S, 200f), N.Mul32(N.S, 30f)));
                kills.SetColor(yellow); kills.SetNative("Adventure_16", 0f, 0f, 0, 0);
                var time = CreateLabel(27, 2, 30); time.SetRect(N.Layout(312, -3, 200, 30)); time.SetColor(new Color32(250, 205, 0, 250)); time.SetNative("HARLOWSI-16", 2f, 2f, 0, 0); time.SetText("00:00");
            }
            var ammo = CreateLabel(97, 3, 30); ammoLabel = ammo; ammo.SetRect(N.Layout(75, 19, MainSceneMission ? 50 : 200, 30)); ammo.SetColor(new Color32(0, 0, 0, 255)); ammo.SetNative("HARLOWSI-16", 2f, 2f, 0, 0);
            ammo.SetText(Profile.weaponAmmo[heroType * 6].ToString());
            NativeControl(1, 1, "iconleftFrame", LeftDisp);
            NativeControl(2, 1, "iconrightFrame", RightDisp);
            NativeControl(3, 1, "iconhitFrame", HitDisp);
            NativeControl(4, 1, "iconfireFrame", FireDisp);
            NativeControl(MainSceneMission ? 22 : 28, 1, "ui_btn_rollFrame", RollRect_Disp);
            // r118z4: roll-fill image created here, after the roll button, in layer 1 (mainScene::OnLoad 0xaa288 CreateImage(39, 1) -> +312; lifeModeScene::OnLoad
            // 0xe950c CreateImage(45, 1) -> +572): SetEnable(0), ui_btn_rollFrame, rect (830,537,88,88), colour 0xB0000000, SetVisible(0) (0xaa4da / 0xe96c2).
            // The port created it at the first roll in layer 3 and deleted it when drained (FALSIFIED: layer, insertion order and lifetime).
            var rollFillImg = NativeControl(RollFillId, 1, "ui_btn_rollFrame", RollFillRect); rollFillImg.SetColor(new Color32(0, 0, 0, 176)); rollFillImg.SetVisible(false);
            NativeControl(MainSceneMission ? 41 : 48, 1, "iconchangeFrame", ChangeDisp);
        }

        // iPad DISPLAY rects of the OnLoad controls (NATIVE): lifeModeScene::OnLoad overrides the RectScale/RectOffset result with
        // hard-coded values when the iPhone flag is 0 (leftctrl 0xe8e2c, right, hit, fire, roll, change; Dev/Recovered/lifeMode_onload_events_ipad2.json).
        public static readonly Rect LeftDisp = new Rect(30, 619, 84, 103), RightDisp = new Rect(172, 619, 84, 103), HitDisp = new Rect(774, 631, 88, 88),
            FireDisp = new Rect(903, 613, 88, 88), RollRect_Disp = new Rect(830, 537, 88, 88), ChangeDisp = new Rect(23, 514, 84, 84);
        // iPad absolute touch rects, lifeModeScene::OnTouchEvent 0xd15e4 (NATIVE).
        public static readonly Rect NativeLeftRect = new Rect(11, 608, 127, 117), NativeRightRect = new Rect(146, 608, 127, 117),
            NativeFireRect = new Rect(892, 626, 132, 100), NativeHitRect = new Rect(738, 625, 140, 100), NativeRollRect = new Rect(818, 526, 106, 97),
            NativeChangeRect = new Rect(13, 507, 107, 97), RollFillRect = new Rect(830, 537, 88, 88);
        /// <summary>r119d (user item 14, pause sometimes not taken on the first click): static stopRect = (414, 0, 66, 52) (lifeModeScene init 0x55d8..0x55f2, mainScene
        /// init 0x4a44..0x4a5c, literals 0x43CF0000 / 0x42840000 / 0x42500000), OnTouchEvent iPad: RectScale + (g_w_offset, g_h_offset - 32.0) (0xd1706 / 0x969fa,
        /// literal 0x42000000) = (892, 32, 132, 104), which covers the whole drawn icon (922, 32, 68, 66). FALSIFIED: the port used the icon layout (445, 0, 34, 33)
        /// with g_h_offset - 8, a 68x66 rect 32 px right of and 24 px below the icon, so clicks on its upper-left part did nothing.</summary>
        public static Rect NativeStopRect { get { var r = N.RectScale(414, 0, 66, 52); return new Rect(r.x + N.WOffset, r.y + (N.HOffset - 32f), r.width, r.height); } }
        public static Rect NativeWeaponRect { get { return N.RectScale(0, 0, 80, 80); } }
        // Hit-test rects = the native rects moved with the HUD art that shows them (ADAPTATION WideView; identical to the native rects when ExtraHalf is 0).
        public static Rect LeftRect { get { return WideView.Anchored(NativeLeftRect); } }
        public static Rect RightRect { get { return WideView.Anchored(NativeRightRect); } }
        public static Rect FireRect { get { return WideView.Anchored(NativeFireRect); } }
        public static Rect HitRect { get { return WideView.Anchored(NativeHitRect); } }
        public static Rect RollRect { get { return WideView.Anchored(NativeRollRect); } }
        public static Rect ChangeRect { get { return WideView.Anchored(NativeChangeRect); } }
        public static Rect StopRect { get { return WideView.Anchored(NativeStopRect); } }
        public static Rect WeaponRect { get { return WideView.Anchored(NativeWeaponRect); } }
        UiControl NativeControl(int id, int layer, string frame, Rect rect)
        {
            var c = CreateControl(id, layer); c.SetTexture(frame); c.SetRect(rect); c.touchable = false; return c;
        }

        UiControl Image(int id, int layer, string frame, float x, float y, float w, float h)
        {
            var c = CreateControl(id, layer);
            c.SetTexture(frame);
            c.SetRect(N.Layout(x, y, w, h));
            c.touchable = false;                       // HUD touches are scene-level (g_b_*Down), not UIMoveCtrl clicks
            return c;
        }

        void SetRigState(int s, bool preserveEmission = false)
        {
            if (rigState == s) return;
            rigState = s;
            if (IsGirl) { if (s == 0) GirlStand(); else GirlMove(); return; }   // playerNewAniClass::Stand 0x124f82 / Move 0x125072
            if(UsesRecoveredLifeCamera)Rig.SelectNativeLocomotion(heroType,weaponSlot,s==0?"stand":"move");
            else if (s == 0) Rig.Select(heroType,preserveEmission);
            else Rig.SelectMove(heroType,preserveEmission);
            Rig.Player.objectScale = HeroRigScale;
        }

        public float WalkSpeedFor(int hero) { return hero == 3 ? NinjaWalkSpeed : WalkSpeed; }

        protected override void OnUpdate(float rawDt)
        {
            IntroUpdate(rawDt);
            float dt = TickPaused ? 0f : rawDt;  // Scene::SetTimeFactorEx(0): timers/objects/physics frozen, UI keeps raw dt; the factor is the tick-entry snapshot (R3)
            Rig.transform.localPosition=Vector3.zero;
            BloodPhysics(dt);   // Scene::Update: PhysicsManager::Update precedes every object callback
            if(UsesRecoveredWorldPhase) { UpdateRecoveredWorldPhase(dt);return; }
            heroCameraThisTick = true;   // legacy path: no hero-update gating
            RollPhysics(dt); // native roll physics precedes camera/projection and CA key/end emission.
            // playerClass::UpdateControl 0xb8a60: left has priority over right
            float v = 0f;
            if (!CombatMovementAllowed) { leftDown = rightDown = false; }
            bool wasFacingLeft = facingLeft;
            if (leftDown) { facingLeft = true; v = -WalkSpeedFor(heroType); }
            else if (rightDown) { facingLeft = false; v = WalkSpeedFor(heroType); }
            if (UsesRecoveredLifeCamera && wasFacingLeft != facingLeft) nativeCamera.turning = true; // UpdateControl b8aa0/b8b04
            if (!combatPoseActive) SetRigState(v != 0f ? 1 : 0);
            heroX += v * dt;
            if (UsesRecoveredLifeCamera)
            {
                UpdateNativeCamera(dt);
                // UpdateCamera bb4b2..bb514, life-mode branch: clamp actor after camera calculation.
                float upper = HeroBoundUpper;   // r119t: mirrored wide clamp (AD-002)
                if (heroX < HeroBoundLower) heroX = HeroBoundLower;
                else if (heroX > upper) heroX = upper;
            }
            else camLeft = HeroCameraLeft(); // PA-011: main-scene bounds/Girl callback lifecycle remain OPEN.
            Background.StepVelocity(UsesRecoveredLifeCamera && nativeCamera.clamped ? 0f : v, dt);
            Place();
            Rig.Player.flipX = facingLeft;
            CombatUpdate(dt);
            MissionUpdate(dt);
            MainSceneUpdate(dt);
            EggBodyEnsure();Rig.Player.brightness=wudiBrightness;Rig.Step(dt);GirlStep(dt);
            WudiPulse(dt);
        }

        void UpdateRecoveredWorldPhase(float dt)
        {
            nativeWorldPrepared=true;
            Rig.transform.localPosition=Vector3.zero;
            // Touch handlers precede native Timer/Physics; this existing flag bridge starts fire before CA.
            UpdateFire(dt);
            PrepareCombatTimers(dt);
            RollUpdate(dt);MeleeUpdate();
            float velocity=HeroBodyVelocity;
            heroX=N.Add32(heroX,N.Mul32(velocity,dt));
            Background.StepVelocity(nativeCamera.clamped?0f:velocity,dt);
            PrepareDinosaurPhysics(dt);
            PrepareItemPhysics(dt);   // item bodies: same physics stage (r118z6)
            PrepareBossPhysics(dt);   // bossN body in the same PhysicsManager::Update 0x17610a, before every object callback (Codex review R4)
            // Item GameObjects are id 0 (DropItem 0xe15c8 / itemClass): they update before the hero (id 1, lifeModeScene::OnLoad 0xe9874) and therefore see the
            // camera viewport and hero position before the hero's own OnUpdate moves the camera this tick (r118z L1).
            sceneUpdateCamLeft = camLeft; sceneUpdateCamValid = true;   // lifeModeScene::OnUpdate 0xd41ba reads the viewport here, before the hero (id 1) moves the camera
            UpdateId0Fx(dt);   // id 0 objects: the boss's effectClassN pool (BossShao), items. bossN itself is id 5 (r118z2; the r118z boss-body-first order is SUPERSEDED)
            UpdateItems(dt); itemsUpdatedThisFrame = true;
            float previousCamera=camLeft;
            Place();Rig.Player.flipX=facingLeft;
            EggBodyEnsure();Rig.Player.brightness=wudiBrightness;Rig.Step(dt);GirlStep(dt); // native playerID1: CA emission/callback before control and camera.
            WudiPulse(dt);   // playerClass::OnUpdate 0xbb6cc / playerNewAniClass::OnUpdate 0x127840: after the CA update, while invincible
            int state=rolling?7:heroDead?4:HeroHurting?5:meleeing?3:heroControlState;
            var control=NativeHeroControl.Decide(state,facingLeft,leftDown,rightDown,heroInputEnabled,WalkSpeedFor(heroType));   // OnUpdate 0xbb75c: UpdateControl only while +657 != 0 (state 4 = dead is gated separately)
            facingLeft=control.facingLeft;nativeCamera.turning|=control.turning;
            if(control.hasVelocity)heroVelocityX=control.velocity;
            heroControlState=control.state;
            if(control.hasVelocity&&!combatPoseActive)SetRigState(control.state==2?0:1,true);
            bool heroCamera=HeroUpdatesCamera;   // sampled before the camera step; MainSceneUpdate's UpdateCamera tests use the same value
            heroCameraThisTick=heroCamera;
            if(heroCamera)
            {
                UpdateNativeCamera(dt);
                float upper=HeroBoundUpper;   // r119t: mirrored wide clamp (AD-002)
                if(heroX< HeroBoundLower) { heroX=HeroBoundLower;heroVelocityX=0f;rollVelocity=0f; }
                else if(heroX>upper&&!RunMission) { heroX=upper;heroVelocityX=0f;rollVelocity=0f; }   // UpdateCamera mode<=1 has no right clamp (run end walks off screen)
            }
            Place();
            // Exact projection identity: emitted worldX-oldLeft -> worldX-newLeft after camera update.
            // This is a camera transform, not an art placement/size correction.
            Rig.transform.localPosition=new Vector3(N.Add32(previousCamera,-camLeft),0f,0f);
            if (DebugBeforeBeams != null) DebugBeforeBeams();   // DEBUG_ONLY verifier hook: inside the hero's update, before its container pass
            UpdateBeams(dt);   // id-97 beam objects are GameContainer children of the hero (PenHuoQ 0xbebfa / JiGuangQ 0xbee7a / YuanLi 0xbf198 -> GameContainer::CreateGameObject 0x16b7a0):
                               // GameObject::Update 0x16c106 runs the hero callback, then GameContainer::Update 0x16be94 -> children, before any dinosaur (r118z2)
            CombatUpdate(dt);
            MissionUpdate(dt);
            MainSceneUpdate(dt);
            nativeWorldPrepared=false;
        }

        void Place()
        {
            Background.Place(camLeft, WorldViewportW);
            Background.PlaceWide(camLeft, WorldViewportW, WideView.ExtraHalf);   // ADAPTATION WideView: render-only repeats of the chains for the extra width
            PlaceScenery();
            Rig.Player.position = new Vector2(heroX - (camLeft + WorldViewportW * 0.5f), NativeHeroSpawn(heroType).y);
            GirlPlace();
            if (Rig.ComponentPlayer != null)
            {
                // CompAnimObj ctor119690 scale1; OnUpdateSprite118c3a..118d52:
                // scaled local component coordinates + owning GameObject position.
                // Menu preview (-232,-162)/1.5 is a separate selectScene contract.
                Rig.ComponentPlayer.position = Rig.Player.position;
                Rig.ComponentPlayer.objectScale = 1f;
            }
        }

    }
}
