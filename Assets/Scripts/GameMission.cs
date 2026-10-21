using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dc2Pilot
{
    /// <summary>
    /// Mission rules for the lifeModeScene types (2 time, 3 kill, 4 specialkill, 7 arena, 8 boss) and the completion/failure screens.
    /// NATIVE: lifeModeTime::initTime 0xc5804, SubTime 0xd0ed6, UpdateKillDinocap 0xe1e62, StopCreateEnemy 0xd0e14,
    /// missionComplete 0xe1704 (partial), MissCompEnter 0xe3c54 / MissCompMove 0xe28ac rects (Unicorn), AwardItem 0xd09dc (weapon unlock by level).
    /// GameClear 0xf0ff8 / mainScene::GameClear 0xb7f5e (r118w): StopCreateEnemy, hero Stand, +657 = 0 (no movement, fire, melee or dinosaur targeting), velocity 0,
    /// g_b_Left/Right/Fire/RollDown = 0 (HitDown kept); the scene stays in state 4. Type 4 (specialkill) and the 0/4/6 fallbacks are not in shipped Map.xml.
    /// EXCLUDED (r118w): g_mission_state / g_total_time / SaveMissionInfo are telemetry-only (consumer SendStatisticsData). OPEN: pause during the completion sequence (native allows it in state 4).
    /// </summary>
    public partial class GameScene
    {
        public int missionType = 3;                 // Map.xml type id (2 time, 3 kill, 4 specialkill, 7 arena, 8 boss)
        public float missionNum = 20f;              // Map.xml num
        public int timeLeft;                        // seconds (types 2/7)
        public int killTotal, killRemaining;        // kill missions
        public int gTime;                           // DinoCap::g_time: ++ on every g_time_id (reset by mainScene/lifeModeScene::OnLoad); drives the type-7 spawn interval
        bool clockRunning;                          // lifeModeScene+652: StartGame_N := 1, SubTime at 00:00 := 0; gates the g_time_id re-arm
        public int elapsedSeconds;                  // g_lifemodetime digits counting up (lifeModeScene::UpdateTime 0xd0608) for types other than 2/7
        public enum MissionResult { Running, Complete, Failed }
        public MissionResult result = MissionResult.Running;
        public int moneyEarned;                     // lifeModeScene+280
        float resultTimer;
        UiLabel killLabel, timeLabel;   // lifeModeScene +564 (label 9) / +560 (label 27)

        void MissionLoad()
        {
            if (Game.LaunchMission != null) { missionType = Game.LaunchMission.type; missionNum = Game.LaunchMission.count; }
            int level0 = Profile.level[heroType];
            switch (missionType)
            {
                case 2: timeLeft = Mathf.Min(200, (int)(10 * level0 + missionNum)); break;
                case 7: timeLeft = Mathf.Min(200, (int)missionNum); break;
                case 3: case 4: killTotal = Mathf.Min(200, (int)(5 * level0 + missionNum)); killRemaining = killTotal; break;
            }
            Atlas.LoadByConf("DC2/Image/missionui", "DC2/Image/missionui.conf");
            bool timed = missionType == 2 || missionType == 7;
            if (!timed) DeleteControl(46);
            if (missionType != 3 && missionType != 4) DeleteControl(44);
            killLabel = timeLabel = null;
            foreach (var l in Labels) { if (l.id == 9) killLabel = l; if (l.id == 27) timeLabel = l; }
            // OnLoad 0xea22e UpdateKillDinocap(0): label 9 gets "x/total" for every type; 0xea25a..0xea374 visibility: types 2/7 clock on, kill off; type 3 clock off,
            // kill on; type 8 both off (sub_F0F44 / sub_EA32C); other types unchanged.
            if (killLabel != null) killLabel.SetText((killTotal - killRemaining) + "/" + killTotal);
            if (missionType == 2 || missionType == 7) { if (killLabel != null) killLabel.SetVisible(false); }
            else if (missionType == 3) { if (timeLabel != null) timeLabel.SetVisible(false); }
            else if (missionType == 8) { if (timeLabel != null) timeLabel.SetVisible(false); if (killLabel != null) killLabel.SetVisible(false); }
            RefreshMissionLabel();
            BossLoad(); EggLoad(); HouseLoad(); WeatherLoad(); BloodLoad();
        }

        void RefreshMissionLabel()
        {
            if (missionType == 2 || missionType == 7) { if (timeLabel != null) timeLabel.SetText(string.Format("{0:00}:{1:00}", timeLeft / 60, timeLeft % 60)); }   // SubTime "%d%d:%d%d" -> +560
            else if (missionType == 3 || missionType == 4) { if (killLabel != null) killLabel.SetText((killTotal - killRemaining) + "/" + killTotal); }        // UpdateKillDinocap "%d/%d" -> +564
        }

        void MissionUpdate(float dt)
        {
            if (IntroActive) return;                  // mission clock and result logic start with StartGame_N
            if (result != MissionResult.Running) { ResultUpdate(dt); return; }
            if (heroDead) { if (IsGirl ? GirlDieEnded : Rig.Player.Ended) Fail(); return; }   // playerClass::OnAnimationEnd state4 -> mainScene::GameOverStart 0xa6f70
            // the mission clock is the native "g_time_id" timer (ArmMissionClock / MissionSecond)
        }

        /// <summary>StartGame_N clock: lifeModeScene 0xe232e sets +652 = 1 and arms "g_time_id" 1.0 unless the map type is 8; mainScene 0x94f68 arms it only for type 6.
        /// Armed before the spawn timers (heap insertion order).</summary>
        void ArmMissionClock()
        {
            if (MainSceneMission) { if (missionType == 6) SetTimer("g_time_id", 1f); return; }
            clockRunning = true;
            if (missionType != 8) SetTimer("g_time_id", 1f);
        }

        /// <summary>OnTimeout "g_time_id". lifeModeScene 0xe2f44..: ++g_time; types 2/7: --g_lifemodetime + SubTime 0xd0ed6 (borrowing digits; at 00:00 StopCreateEnemy,
        /// GameClear (vtbl+56), +652 = 0), other types: ++g_lifemodetime + UpdateTime 0xd0608 (elapsed time, written to the +560 label that OnLoad hides for them);
        /// then re-arm 1.0 if +652. mainScene 0xb714e: ++g_time; type 6 with g_lifemodetime &gt; 0: -- and mainScene::SubTime (not ported, OPEN); re-arm 1.0 always.</summary>
        void MissionSecond()
        {
            gTime++;
            if (MainSceneMission) { SetTimer("g_time_id", 1f); return; }
            if (missionType == 2 || missionType == 7)
            {
                if (timeLeft > 0) timeLeft--;
                RefreshMissionLabel();
                if (timeLeft <= 0) { StopCreateEnemy(); Complete(); clockRunning = false; }
            }
            else elapsedSeconds++;
            if (clockRunning) SetTimer("g_time_id", 1f);
        }

        /// <summary>UpdateKillDinocap 0xe1e62 for the plain kill mission.</summary>
        void OnDinoKilled(DinoActor d)
        {
            kills++;
            AddExp((int)d.exp);                                              // Die: stats exp += EXP field
            Profile.cash = Mathf.Min(9999999, Profile.cash + (int)d.exp);       // dinosaurClass::Die: cash += EXP field (cap 9,999,999)
            moneyEarned += (int)d.exp;
            UpdateGoldLabel();
            if (missionType == 3 || missionType == 4)
            {
                killRemaining--;
                RefreshMissionLabel();
                if (killRemaining <= 0) { killRemaining = 0; RefreshMissionLabel(); Complete(); }
            }
        }

        void UpdateGoldLabel()
        {
            foreach (var l in Labels) if (l.id == 10) l.SetText(Profile.cash.ToString("N0", System.Globalization.CultureInfo.InvariantCulture));
        }


        public bool jigsawGot; public int jigsawGroup, jigsawPiece;        // lifeModeScene +152 / +144 / +148 (getJigsaw result)
        int newWeaponHero = -1, newWeaponSlot = -1;                          // lifeModeScene +584 / +588 (AwardItem)
        int compSub;                                                         // lifeModeScene +644 (31 banner arriving, 32 banner moved up)

        /// <summary>lifeModeScene::GameClear 0xf0ff8: spawning stops, input is cleared, the fail_line band (id 20, RectScale(0,118,480,82), alpha 192) grows from zero height (0.2 s, 0.02 s overshoot 1.1).
        /// Its move end starts MissCompEnter (OnUIEvent id 20), the banner move end runs missionComplete + the "CompTextEnter" 1.0 s timer, which runs MissCompMove, whose banner move end runs MissCompEnd.</summary>
        public void Complete()
        {
            if (result != MissionResult.Running) return;
            result = MissionResult.Complete;
            StopCreateEnemy();
            GameClearHero();
            float s = N.S;
            DeleteControl(20);
            var band = CreateControl(20, 2); band.SetTexture("fail_lineFrame"); band.touchable = false;
            Rect r = new Rect(0f, N.Add32(N.Mul32(s, 118f), 0.5f * N.HOffset), N.Add32(N.Mul32(s, 480f), N.WOffset), N.Mul32(s, 82f));   // RectScale(0,118,480,82): lifeModeScene::GameClear 0xf0ff8 and mainScene::GameClear 0xb7f5e agree (0x42EC0000)
            var black = new Color32(0, 0, 0, 192);
            float cx = 0.5f * r.width + r.x, cy = 0.5f * r.height + r.y;
            band.SetRect(r); band.SetColor(black);
            band.StartMove(new MoveData(cx, cy, r.width, 0f, black), new MoveData(cx, cy, r.width, N.Mul32(r.height, 1.1f), black), new MoveData(cx, cy, r.width, r.height, black), 0.2f, 0.05f);
            compSub = 31; compBand = band;
            resultTimer = 0f;
        }

        UiControl compBand;

        /// <summary>GameClear 0xf1078..0xf117a / mainScene::GameClear 0xb7fdc..0xb8074 hero part: Stand, +657 = 0, velocity 0 (+ UpdateBG), then the four held flags cleared
        /// (not g_b_HitDown). mainScene::GameClear skips the hero part for the girl (0xb7fea: g_player_type != 4 only).</summary>
        void GameClearHero()
        {
            leftDown = rightDown = fireDown = false;
            if (IsGirl && MainSceneMission) return;
            heroInputEnabled = false;
            if (firing) ClearNativeFireChannel();
            bool hiddenRoll = rolling && rollPhase == 1 && !UsesNativeGroundRoll && !IsGirl;   // two-part roll between roll_1 end and the "roll" timer: hero hidden
            firing = false; meleeing = false; rolling = false; heroVelocityX = 0f; rollVelocity = 0f; heroControlState = 2;   // Stand: state 2; a pending "roll" timer still fires (SpecialRollTimeout)
            rigState = -1; SetRigState(0);   // girl: SetRigState -> GirlStand (playerNewAniClass::Stand 0x124f82)
            if (hiddenRoll) Rig.Player.Hide();   // Stand 0xb88b4 does not touch visibility: the hero stays hidden until the "roll" timer (r118x)
        }

        /// <summary>lifeModeScene::missionComplete 0xe1704 (data part, run when the banner arrives) + AwardItem 0xd09dc: jigsaw award, weapon unlocks by level, jigsaw5 cash bonus, save.</summary>
        void MissionCompleteData()
        {
            JigsawSeedStep();
            if (Game.LaunchMission != null && Game.LaunchMission.type == 8) (Game.MapState ?? (Game.MapState = new NativeMapState())).BossCompleted(Game.SaveDir, Profile.mission, RandomRange(4, 6));   // 0xe18fa (r119d item 6)
            Profile.mission++;   // missionComplete 0xe19a2: ++g_mission (the per-map flag vector is cleared at the end of every completion)
            int bonus = (int)N.Mul32(N.F32(moneyEarned), N.Mul32(N.F32(Profile.jigsaw5Count), 0.01f));   // R002-B4 (r119s): 0xe1d0e..0xe1d2e / main twin: (int)(earned_f * ((float)count * 0.01f)) in binary32 (0x3C23D70A); the C# expression ran in double and truncated 19.9999996 to 19
            Profile.cash += bonus;   // R002-B4: missionComplete tails 0xa2c92..0xa2c96 (main) / 0xe1d36..0xe1d3a (life) add and store with no upper compare (FALSIFIED: the port clamped to 9,999,999)
            { int bg = Game.LaunchMission != null ? Game.LaunchMission.background : 0; if ((Game.LaunchMission != null && MainSceneMission) || bg == 2 || bg == 3 || bg == 4) Profile.cash += 100 + Profile.level[heroType]; }   // AwardItem 0xd09dc: mapClass +20 = Map.xml bg (LoadFromXml 0xf3294 v21[5]) in 2/3/4: cash += 100 + level, no cap; mainScene::AwardItem 0x963f0 (run/egg missions) adds it unconditionally
            newWeaponHero = heroType; newWeaponSlot = UnlockWeaponsByLevel(Profile, heroType);
            Game.DebugLevelUnlockPending.Remove(heroType);   // the native unlock just ran for this hero
            if(UsesRecoveredLifeCamera)Profile.selectedWeaponSlots[heroType]=weaponSlot; // native completion e1db2 / mainScene 0xa2cf6 precede SaveFile; no other writer exists (xrefs of the g_prop_Player+0x2c array: ReadFile, ResetFile, SaveFile, both missionComplete, OnLoad reads; r118x): fail/quit/retry keep the previous saved slot
            Game.SaveFile();
        }

        /// <summary>lifeModeScene::AddBullet 0xcff3a / mainScene::AddBullet 0x95384 (identical, called from AwardItem for a newly unlocked weapon): every weapon record of that type (records 0..size-2) gains a flat amount, capped at g_max_bullet_count: type 0 +200, 1 and 4 +15, 2 +60, 3 +30, 5 +50, other types +0 (the cap still applies).</summary>
        /// <summary>The weapon-unlock part of lifeModeScene::AwardItem 0xd09dc: display level (level + 1) &gt; 4 / 9 / 14 / 19 unlocks owned slots 1..4 if still locked (-1),
        /// each new weapon adds its type's bullets (AddBullet). Returns the last unlocked slot (+588), -1 if none. Native caller: mission complete only.</summary>
        public static int UnlockWeaponsByLevel(Profile profile, int hero)
        {
            int displayLevel = profile.level[hero] + 1, last = -1;
            for (int s = 1; s <= 4; s++)
                if (displayLevel > 5 * s - 1 && profile.weaponOwned[hero * 6 + s] < 0) { profile.weaponOwned[hero * 6 + s] = 0; last = s; AwardUnlockBullets(profile, hero, Dc2GameData.PlayerWeapons(hero)[s].type); }
            return last;
        }

        static void AwardUnlockBullets(Profile Profile, int heroType, int type)
        {
            int amount = 0;
            switch (type) { case 0: amount = 200; break; case 1: case 4: amount = 15; break; case 2: amount = 60; break; case 3: amount = 30; break; case 5: amount = 50; break; }
            var weapons = Dc2GameData.PlayerWeapons(heroType);
            for (int i = 0; i < weapons.Count - 1 && i < 5; i++)
                if (weapons[i].type == type)
                {
                    int idx = heroType * 6 + i;
                    Profile.weaponAmmo[idx] = Mathf.Min(Profile.maxBullet, Profile.weaponAmmo[idx] + amount);
                }
        }

        // missionComplete 0xe1704 jigsaw seed logic: every completion either awards a piece (seed == g_mission) or draws the next award mission
        void JigsawSeedStep()
        {
            int g = heroType, unowned = 0;
            for (int i = 0; i < 16; i++) if (Profile.jigsawGet[16 * g + i] == 0) unowned++;
            if (unowned == 16)
            {
                Profile.jigsawSeed = (Profile.mission > 29 ? RandomRange(4, 6) : RandomRange(3, 5)) + Profile.mission;
                GetJigsaw();
            }
            if (Profile.jigsawSeed != 0)
            {
                if (Profile.jigsawSeed == Profile.mission)
                {
                    Profile.jigsawSeed += Profile.jigsawSeed > 29 ? RandomRange(4, 6) : RandomRange(3, 5);
                    GetJigsaw();
                }
            }
            else Profile.jigsawSeed = Profile.mission > 29 ? RandomRange(4, 6) : RandomRange(3, 5);
        }

        /// <summary>lifeModeScene::getJigsaw 0xd0cf6 + getJigsawPicID 0x5a6b0: a random piece (RandomRange(0, count - 1)) of the hero that is not yet owned; the seed is nudged by the average placed level (empty history: +1).</summary>
        void GetJigsaw()
        {
            int g = heroType; var pool = new List<int>();
            for (int i = 0; i < 16; i++) if (Profile.jigsawGet[16 * g + i] == 0) pool.Add(i);
            if (pool.Count == 0) return;
            int piece = pool[RandomRange(0, pool.Count - 1)];
            Profile.jigsawGet[16 * g + piece] = 1;
            float avg = 0f; int sum = 0;   // getJigsaw 0xd0cf6: running sum / (i+1) over g_jigsaw_levels (levels of the last 3 launched missions); empty -> 0
            for (int i = 0; i < Game.JigsawLevels.Count; i++) { sum += Game.JigsawLevels[i]; avg = (float)sum / (float)(i + 1); }
            if (avg < 1.5f) Profile.jigsawSeed += 1; else if (avg > 4.5f) Profile.jigsawSeed -= 1;   // ARM 0xd0db6 literal 0x40900000 = 4.5 (struct-decoded; r118 constants audit; was 5.5)
            jigsawGot = true; jigsawGroup = g; jigsawPiece = piece;
        }

        void Fail()
        {
            if (result != MissionResult.Running) return;
            result = MissionResult.Failed;   // r118z: no StopCreateEnemy - native GameOverStart 0xa6f70 kills no timer, enemies keep spawning behind the fail screen
            resultTimer = 0f;
            GameOverStart();
        }

        // ---- NATIVE fail chain (Dev/Evidence/lifeMode_fail_ui_r103.json) ----
        int sceneMode;                 // lifeModeScene+340: 7 fail started, 8 fail banner shown
        float failAlpha;               // lifeModeScene+260
        UiControl failBg, failLine;

        /// <summary>mainScene::GameOverStart 0xa6f70: missionfailbg fade image (white, alpha 0) and the fail_line UIMoveCtrl id17 expanding from zero height.</summary>
        void GameOverStart()
        {
            float s = N.S;
            DeleteControl(11);
            failBg = CreateControl(11, 4); failBg.SetTexture("missionfailbgFrame");
            failBg.SetRect(new Rect(0f, 0.5f * N.HOffset, N.Add32(N.Mul32(s, 480f), N.WOffset), N.Add32(N.Mul32(s, 320f), N.HOffset)));
            failBg.SetColor(new Color32(255, 255, 255, 0)); failBg.consumesTouch = true;   // r118z4: native image left enabled (scan_ui_control_enable.py) - consumes touches in its rect (GameOverStart 0xa703e)
            failLine = CreateControl(17, 4); failLine.SetTexture("fail_lineFrame"); failLine.touchable = false;
            // RectScale(0,118,480,82); iPad branch 0xa7116: y += 0.5*g_h_offset, w += g_w_offset (x stays 0, h unchanged)
            Rect r = new Rect(0f, N.Add32(N.Mul32(s, 118f), 0.5f * N.HOffset), N.Add32(N.Mul32(s, 480f), N.WOffset), N.Mul32(s, 82f));
            var black = new Color32(0, 0, 0, 192);
            float cx = 0.5f * r.width + r.x, cy = 0.5f * r.height + r.y;
            failLine.SetColor(black);
            failLine.StartMove(new MoveData(cx, cy, r.width, 0f, black), new MoveData(cx, cy, r.width, N.Mul32(r.height, 1.1f), black),
                               new MoveData(cx, cy, r.width, r.height, black), 0.2f, 0.05f);
            sceneMode = 7;
        }

        /// <summary>lifeModeScene::MissionFailEnter 0xe40de (after fail_line move end).</summary>
        void MissionFailEnter()
        {
            var banner = CreateControl(29, 4); banner.SetTexture("mission_failedFrame"); banner.touchable = false;
            Rect r = N.Layout(57f, 108f, 381f, 111f); banner.SetRect(r);
            float cx = 0.5f * r.width + r.x, cy = 0.5f * r.height + r.y; var white = new Color32(255, 255, 255, 255);
            // lifeModeScene::MissionFailEnter 0xe40de: start x + 480, mid x - 20 (no S). mainScene::MissionFailEnter 0xa671e (run/egg missions): start x + S*480, mid x + S*(-20).
            float off0 = MainSceneMission ? N.Mul32(N.S, 480f) : 480f, off1 = MainSceneMission ? N.Mul32(N.S, -20f) : -20f;
            banner.StartMove(new MoveData(N.Add32(cx, off0), cy, r.width, r.height, white), new MoveData(N.Add32(cx, off1), cy, r.width, r.height, white),
                             new MoveData(cx, cy, r.width, r.height, white), 0.2f, 0.05f);
            // ++g_mission_state[..][2], g_total_time, SaveMissionInfo: telemetry-only (SendStatisticsData is the sole consumer), EXCLUDED r118w
            sceneMode = 8;
        }

        /// <summary>lifeModeScene::FailZEnter 0xe3e8e (after the banner move end): fail_z flies in from the top right at 0.7 scale, overshoots to 1.1.</summary>
        void FailZEnter()
        {
            float s = N.S;
            var z = CreateControl(30, 3); z.SetTexture("fail_zFrame"); z.touchable = false;
            Rect r = N.Layout(75f, 23f, 314f, 282f); z.SetRect(r);
            float cx = 0.5f * r.width + r.x, cy = 0.5f * r.height + r.y; var white = new Color32(255, 255, 255, 255);
            z.StartMove(new MoveData(cx + N.Mul32(s, 300f), cy + N.Mul32(s, -300f), N.Mul32(r.width, 0.7f), N.Mul32(r.height, 0.7f), white),
                        new MoveData(cx, cy, N.Mul32(r.width, 1.1f), N.Mul32(r.height, 1.1f), white),
                        new MoveData(cx, cy, r.width, r.height, white), 0.1f, 0.05f);
        }

        protected override void OnTimeout(string name)
        {
            if (BossTimeout(name)) return;   // lifeModeScene "AddBoss" + bossN OnTimeout dispatch
            if (name == "g_time_id") { MissionSecond(); return; }
            if (WeatherTimeout(name)) return;
            if (name == "LevelLeave") { LevelLeave(); return; }   // mainScene/lifeModeScene OnTimeout: the 0.5 s hold after the day card arrived
            if (name == "CompTextEnter") { MissCompMove(); return; }
            if (name == "DieEnd") { Audio.Stop("game_bgAudio"); Game.MapType = 0; Game.SwitchScene("selectScene"); }   // lifeModeScene::OnTimeout DieEnd
        }

        void FailFadeUpdate(float dt)
        {
            if (sceneMode != 8 || failBg == null) return;
            // lifeModeScene::OnUpdate 0xd41ba mode 8: alpha += 192*dt while <= 255 (white missionfailbg); mainScene::OnUpdate 0x979d2 mode 8: the same but the limit literal is 1128792064 = 200.0
            failAlpha = N.Add32(failAlpha, N.Mul32(192f, dt));
            if (failAlpha <= (MainSceneMission ? 200f : 255f)) failBg.SetColor(new Color32(255, 255, 255, (byte)failAlpha));
        }

        // ---- result screens ----
        UiControl compBanner, doneButton;
        void ResultUpdate(float dt)
        {
            resultTimer += dt;
            if (result != MissionResult.Complete) FailFadeUpdate(dt);   // the completion chain is event / timer driven (OnUIEvent, "CompTextEnter")
            else CompleteFadeUpdate(dt);
        }

        UiControl compBg;
        /// <summary>lifeModeScene::OnUpdate 0xd41ba state 5 (after MissCompEnter): +260 += 192*dt; the first frame creates image 11 ("missionfailbgFrame", layer 3, rect (0, 32, S*480 + g_w_offset, S*320 + g_h_offset) on iPad);
        /// colour (0,0,0,(uint)+260) while +260 &lt;= 210, otherwise +260 is clamped to 210 and the colour is left alone (literal 1129447424 = 0x43520000 = 210.0; the earlier 208 was a hand-decode error, fixed r118).</summary>
        void CompleteFadeUpdate(float dt)
        {
            if (sceneMode != 5) return;
            failAlpha = N.Add32(failAlpha, N.Mul32(dt, 192f));
            if (compBg == null)
            {
                float s = N.S;
                DeleteControl(11);
                compBg = CreateControl(11, 3); compBg.SetTexture("missionfailbgFrame"); compBg.consumesTouch = true;   // r118z4: native image left enabled (scan_ui_control_enable.py) - consumes touches in its rect (OnUpdate 0x980c2 / 0xd47f6)
                compBg.SetRect(new Rect(0f, 32f, N.Add32(N.Mul32(s, 480f), N.WOffset), N.Add32(N.Mul32(s, 320f), N.HOffset)));
            }
            if (!(failAlpha > 210f)) compBg.SetColor(new Color32(0, 0, 0, (byte)(uint)failAlpha));
            else failAlpha = 210f;
        }

        /// <summary>lifeModeScene::startMoveUIRight 0xe4356 / 0xd5224: the control slides in from the right: start (cx + 480S, cy), mid (cx - 20S, cy), end (cx, cy) with 0.2 s / 0.05 s (float 0x3D4CCCCD; the earlier 0.02 was a mis-decode, r117t).</summary>
        static void MoveRight(UiControl c, Rect r)
        {
            float cx = 0.5f * r.width + r.x, cy = 0.5f * r.height + r.y, s = N.S; var w = new Color32(255, 255, 255, 255);
            c.SetRect(r);
            c.StartMove(new MoveData(N.Add32(cx, N.Mul32(s, 480f)), cy, r.width, r.height, w), new MoveData(N.Add32(cx, N.Mul32(s, -20f)), cy, r.width, r.height, w), new MoveData(cx, cy, r.width, r.height, w), 0.2f, 0.05f);
        }
        static void MoveRight(UiLabel c, Rect r)
        {
            float cx = 0.5f * r.width + r.x, cy = 0.5f * r.height + r.y, s = N.S; var w = new Color32(255, 255, 255, 255);
            c.SetRect(r);
            c.StartMove(new MoveData(N.Add32(cx, N.Mul32(s, 480f)), cy, r.width, r.height, w), new MoveData(N.Add32(cx, N.Mul32(s, -20f)), cy, r.width, r.height, w), new MoveData(cx, cy, r.width, r.height, w), 0.2f, 0.05f);
        }

        /// <summary>lifeModeScene::MissCompEnter 0xe3c54 (fail_line move end): the mission_completeFrame banner (id 31, RectScale(25,97,426,93)) slides in.</summary>
        void MissCompEnter()
        {
            DeleteControl(31);
            compBanner = CreateControl(31, 5); compBanner.SetTexture("mission_completeFrame"); compBanner.touchable = false;
            Rect br = N.Layout(25f, 97f, 426f, 93f);
            if (MainSceneMission) br.y = N.Add32(br.y, 32f);   // mainScene::MissCompEnter 0xa3ea6: iPad y += 32 (the lifeModeScene copy 0xe3c54 has no such term)
            MoveRight(compBanner, br);
        }

        /// <summary>lifeModeScene::MissCompMove 0xe28ac ("CompTextEnter" timer): the banner and the band move up by 100S; compSub = 32.</summary>
        void MissCompMove()
        {
            float s = N.S; var w = new Color32(255, 255, 255, 255);
            foreach (var c in new[] { compBanner, compBand })
            {
                if (c == null) continue;
                Rect r = c.FullRect; float cx = 0.5f * r.width + r.x, cy = 0.5f * r.height + r.y, cy2 = N.Add32(cy, N.Mul32(s, MainSceneMission ? -110f : -100f));   // mainScene::MissCompMove 0xa3c52 uses -110 (lifeModeScene 0xe28ac: -100)
                c.StartMove(new MoveData(cx, cy, r.width, r.height, w), new MoveData(cx, cy2, r.width, r.height, w), new MoveData(cx, cy2, r.width, r.height, w), 0.2f, 0.05f);
            }
            compSub = 32;
        }

        static string JigsawPieceFrame(int g, int j) { return j > 8 ? string.Format("jigsaw_map0{0}_{1}Frame", g + 1, j + 1) : string.Format("jigsaw_map0{0}_0{1}Frame", g + 1, j + 1); }

        /// <summary>lifeModeScene::MissCompEnd 0xe4438 (banner moved up): the rows slide in; layout variants by (jigsaw obtained, new weapon): none "_72" art, one extra row "_55", both "_48" (rects from the ARM trace Dev/Evidence/misscomp_emu_r116.json).</summary>
        void MissCompEnd()
        {
            bool jig = jigsawGot, wpn = newWeaponSlot > -1 && newWeaponHero > -1;
            var yellow = new Color32(0xF8, 0xC0, 0x20, 255);
            string art; Rect dk, kl, tm, ml;
            if (jig && wpn) { art = "48"; dk = N.Layout(131, 88, 162, 21); kl = N.Layout(300, 75, 200, 40); tm = N.Layout(128, 127, 167, 21); ml = N.Layout(300, 115, 200, 40); }
            else if (jig || wpn) { art = "55"; dk = N.Layout(149, 90, 188, 24); kl = N.Layout(345, 80, 200, 40); tm = N.Layout(144, 149, 192, 23); ml = N.Layout(350, 138, 200, 40); }
            else { art = "72"; dk = N.Layout(85, 103, 241, 29); kl = N.Layout(345, 95, 200, 40); tm = N.Layout(76, 180, 248, 29); ml = N.Layout(350, 170, 200, 40); }
            DeleteControl(33); var i33 = CreateControl(33, 4); i33.SetTexture("dinos_killed_" + art + "Frame"); i33.touchable = false; MoveRight(i33, dk);
            var l38 = CreateLabel(38, 4, 44); l38.consumesTouch = true;   // r118z4: native label left enabled - consumes touches in its rect (MissCompEnd 0xe4604 / mainScene id 32 0xa430a)
            l38.SetColor(new Color32(255, 255, 255, 255)); l38.SetNative("Adventure_22", 0f, 0f, 0, 0); l38.SetText(kills.ToString()); MoveRight(l38, kl);   // lifeModeScene::MissCompEnd 0xe4438 emulation: SetFont Adventure_22 (iPad 44), SetColor(255,255,255,255), no SetAlignment (top-left); the yellow came from Showcase.mp4 (OBSERVED) and is superseded
            DeleteControl(34); var i34 = CreateControl(34, 4); i34.SetTexture("total_money_" + art + "Frame"); i34.touchable = false; MoveRight(i34, tm);
            var l39 = CreateLabel(39, 4, 44); l39.consumesTouch = true;   // r118z4: native label left enabled - consumes touches in its rect (0xe48de / mainScene id 33)
            l39.SetColor(new Color32(255, 255, 255, 255)); l39.SetNative("Adventure_22", 0f, 0f, 0, 0); l39.SetText(Profile.cash.ToString()); MoveRight(l39, ml);
            if (jig)
            {
                Atlas.LoadByConf("DC2/Image/ip/jigsaw_h", "DC2/Image/ip/jigsaw_h.conf"); Atlas.LoadByConf("DC2/Image/ip/jigsaw_h_1", "DC2/Image/ip/jigsaw_h_1.conf");   // piece art (the jigsaw scene loads the same atlases)
                DeleteControl(36); var i36 = CreateControl(36, 4); i36.SetTexture("gain_a_new_jigsaw_55Frame"); i36.touchable = false;
                MoveRight(i36, wpn ? N.Layout(13, 217, 281, 22) : N.Layout(58, 212, 279, 24));
                DeleteControl(49); var i49 = CreateControl(49, 4); i49.SetTexture(JigsawPieceFrame(jigsawGroup, jigsawPiece)); i49.touchable = false;
                MoveRight(i49, wpn ? N.Layout(307, 211, 50, 50) : N.Layout(354, 198, 50, 50));
            }
            if (wpn)
            {
                Atlas.LoadByConf("DC2/Image/weapon_icon1", "DC2/Image/weapon_icon1.conf");   // icon_<id>Frame art: the native DinoCap::LoadImage loads this atlas at start-up and keeps it resident; the pilot loads atlases per scene (engine lifetime difference, no behaviour change). Without it the unlock row threw "Unknown frame: icon_C1Frame" (found r118).
                DeleteControl(35); var i35 = CreateControl(35, 4); i35.SetTexture("new_weapons_unlocked_" + (jig ? "48" : "55") + "Frame"); i35.touchable = false;
                MoveRight(i35, jig ? N.Layout(55, 171, 240, 22) : N.Layout(10, 212, 326, 23));
                var slot = Dc2GameData.PlayerWeapons(newWeaponHero)[newWeaponSlot];
                string identity = Dc2GameData.Weapons()[slot.type][slot.subtype].name;
                if (identity == "B5") identity = "B3"; else if (identity == "D5") identity = "D4"; else if (identity == "E3") identity = "E1";   // shipped icon aliases (showUpgrade)
                DeleteControl(40); var i40 = CreateControl(40, 5); i40.SetTexture("icon_" + identity + "Frame"); i40.touchable = false;
                MoveRight(i40, jig ? N.Layout(306, 152, 86, 56) : N.Layout(348, 186, 108, 70));
            }
            DeleteControl(41); doneButton = CreateControl(41, 4); doneButton.SetTexture("comp_doneFrame"); doneButton.touchable = true;
            MoveRight(doneButton, N.Layout(346, 264, 110, 49));
        }

        protected override void OnUIEvent(UiControl c, int ev)
        {
            if (HandlePauseEvent(c, ev)) return;
            if (HandleIntroEvent(c, ev)) return;
            if (ev == 1 && c.id == 17 && sceneMode == 7) { MissionFailEnter(); return; }     // OnUIEvent 0xe6550 id17
            if (ev == 1 && c.id == 29) { FailZEnter(); return; }                               // id29 move end
            if (ev == 1 && c.id == 30) { SetTimer("DieEnd", 3f); return; }                     // id30 move end
            if (result == MissionResult.Complete && ev == 1 && c.id == 20 && compSub != 32 && compBanner == null) { MissCompEnter(); sceneMode = 5; return; }   // OnUIEvent 0xe6550 id 20
            if (result == MissionResult.Complete && ev == 1 && c.id == 31)
            {
                if (compSub == 31) { SetTimer("CompTextEnter", 1f); MissionCompleteData(); }   // banner arrived: timer + missionComplete
                else if (compSub == 32) MissCompEnd();
                return;
            }
            if (c.id == 41 && ev == 0 && result == MissionResult.Complete && doneButton != null)
            {
                Audio.Stop("game_bgAudio");
                Game.MapType = 1;                       // "Clicking done would bring me back to Armory" (OnUIEvent id 41: m_map_type = 1)
                Game.SwitchScene("selectScene");
            }
        }
    }
}
