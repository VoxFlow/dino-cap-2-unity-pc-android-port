using UnityEngine;

namespace Dc2Pilot
{
    /// <summary>
    /// Mission start sequence (NATIVE, ARM): at the end of mainScene::OnLoad (0xb63c0..0xb6426) and lifeModeScene::OnLoad (0xf0100..0xf0142) the scene sets
    /// state +340 = 1, the cover image (id 11) to opaque black, +260 = 255 and calls mainScene::LevelBGEnter 0xa3a40. The state machine then runs
    /// 1 (band + "DAY n" card) -> LevelLeave (state 3) -> StartGame_N (mainScene 0x94f68 / lifeModeScene 0xe232e, state 4 = playing).
    /// Controls: id 11 black cover (OnUpdate states 0/1/3: +260 -= 192 dt, alpha = (byte)+260), id 12 band (LevelBGEnter / LevelBGLeave), id 13 dayFrame (LevelEnter / LevelLeave),
    /// id 14 mission number label (font Adventure_33). Evidence: Dev/Evidence/yuanli_awarditem_r117.md "r118 MISSION INTRO". Provenance NATIVE.
    /// lifeModeScene::LevelEnter 0xf13b4 / LevelLeave 0xf17e6 were read in full (r118): identical to the mainScene versions (own static copies of level_dayRect / level_textRect at 0x23d470 / 0x23d480; static initializer 0x54ac gives the same values (152,147,79,37) / (256,134,400,50) as the mainScene copies).
    /// </summary>
    public partial class GameScene
    {
        /// <summary>mainScene +340: 1 = intro (band entering / card holding), 3 = leaving, 4 = playing.</summary>
        public int levelState = 4;
        public bool IntroActive { get { return levelState != 4; } }
        float introFade;                  // scene +260
        UiControl introCover;             // image 11 (scene +296)

        static readonly Color32 IntroBlack192 = new Color32(0, 0, 0, 192);
        static readonly Color32 IntroWhite = new Color32(255, 255, 255, 255);

        /// <summary>End of OnLoad: cover opaque, state 1, LevelBGEnter, +260 = 255. With Dc2Game.SkipMissionIntro (DEBUG_ONLY, verification harnesses) the scene starts in state 4.</summary>
        void IntroLoad()
        {
            // r119 (DC2-BUGS-001 item 12): mainScene::OnLoad 0xaa9f2 / lifeModeScene::OnLoad 0xe9b58 set g_b_RightDown = 1 right after playerClass::Stand, so UpdateControl walks
            // the hero in to the right for the whole intro; the intro-end UI event (0xa6ab8 / 0xe666e, StartGame below) clears it. The port never set it (no walk-in).
            rightDown = true;
            if (Game.SkipMissionIntro) { StartGame(); return; }
            levelState = 1; introFade = 255f;
            EnsureIntroCover(); introCover.SetColor(new Color32(0, 0, 0, 255));
            LevelBGEnter();
            bulletSoundArmed = false;
        }

        void EnsureIntroCover()
        {
            if (introCover != null) return;
            introCover = CreateControl(11, 4); introCover.SetTexture("whiteFrame"); introCover.consumesTouch = true;   // CreateImage(11, 4) + whiteFrame; r118z4: no SetEnable(0) (mainScene 0xb6298 / lifeModeScene 0xeffbc), consumes touches in its rect
            introCover.SetRect(new Rect(0f, 32f, N.Add32(N.Mul32(N.S, 480f), N.WOffset), N.Add32(N.Mul32(N.S, 320f), N.HOffset)));   // OnUpdate 0x97f98..: (0, 32, S*480 + g_w_offset, S*320 + g_h_offset) on iPad
        }

        /// <summary>mainScene::OnUpdate 0x97f08 cases 0/1/3: the black cover fades 255 -> 0 at 192 per second; below 0 it is pinned to 0 and stays until StartGame_N deletes it.</summary>
        void IntroUpdate(float dt)
        {
            if (!IntroActive) return;
            introFade = N.Add32(introFade, N.Mul32(dt, -192f));   // float literal 0xC3400000 = -192.0 (python-decoded)
            if (introFade >= 0f)
            {
                EnsureIntroCover();
                introCover.SetColor(new Color32(0, 0, 0, (byte)Mathf.Clamp((int)introFade, 0, 255)));
            }
            else introFade = 0f;
        }

        Rect IntroBandRect()
        {   // LevelBGEnter 0xa3a40: RectScale(level_bgRect (0,126,480,80)), width + g_w_offset on iPad (no RectOffset)
            return new Rect(0f, N.Mul32(N.S, 126f), N.Add32(N.Mul32(N.S, 480f), N.WOffset), N.Mul32(N.S, 80f));
        }

        /// <summary>mainScene::LevelBGEnter 0xa3a40: id 12 black band (alpha 192) grows from zero height through 1.1h to h in 0.2 s / 0.05 s; state = 1.</summary>
        void LevelBGEnter()
        {
            DeleteControl(12);
            var band = CreateControl(12, 3); band.SetTexture("whiteFrame"); band.touchable = false;
            Rect r = IntroBandRect(); band.SetRect(r); band.SetColor(IntroBlack192);
            float cx = N.Add32(N.Mul32(r.width, 0.5f), r.x), cy = N.Add32(N.Mul32(r.height, 0.5f), r.y);
            band.StartMove(new MoveData(cx, cy, r.width, 0f, IntroBlack192), new MoveData(cx, cy, r.width, N.Mul32(r.height, 1.1f), IntroBlack192),
                           new MoveData(cx, cy, r.width, r.height, IntroBlack192), 0.2f, 0.05f);
            levelState = 1;
        }

        /// <summary>mainScene::LevelBGLeave 0xa38c2: the band collapses h -> 1.1h -> 0 in 0.05 s / 0.2 s.</summary>
        void LevelBGLeave()
        {
            var band = GetControl(12); if (band == null) return;
            Rect r = IntroBandRect();
            float cx = N.Add32(N.Mul32(r.width, 0.5f), r.x), cy = N.Add32(N.Mul32(r.height, 0.5f), r.y);
            band.touchable = false;
            band.StartMove(new MoveData(cx, cy, r.width, r.height, IntroBlack192), new MoveData(cx, cy, r.width, N.Mul32(r.height, 1.1f), IntroBlack192),
                           new MoveData(cx, cy, r.width, 0f, IntroBlack192), 0.05f, 0.2f);
        }

        Rect IntroDayRect()
        {   // LevelEnter 0xa348e / LevelLeave: level_dayRect (152,147,79,37) RectScale + RectOffset; iPad: y -= 10, w and h x 0.7
            Rect r = N.Layout(152f, 147f, 79f, 37f);
            return new Rect(r.x, N.Add32(r.y, -10f), N.Mul32(r.width, 0.7f), N.Mul32(r.height, 0.7f));
        }

        Rect IntroTextRect() { return N.Layout(256f, 134f, 400f, 50f); }   // level_textRect

        /// <summary>mainScene::LevelEnter 0xa348e / lifeModeScene::LevelEnter 0xf13b4: id 13 "dayFrame" and id 14 (Adventure_33, 0xFF00CDFE-ish gold) slide in from cx + 480S through cx - 20S, 0.2 s / 0.05 s.</summary>
        void LevelEnter()
        {
            DeleteControl(13);
            var day = CreateControl(13, 3); day.SetTexture("dayFrame"); day.touchable = false;
            MoveRight(day, IntroDayRect());
            DeleteLabel(14);
            var label = CreateLabel(14, 3, 46);
            label.SetColor(new Color32(0xFE, 0xCD, 0x00, 0xFF));          // SetColor(-16724482 = 0xFF00CDFE): bytes r=FE g=CD b=00 a=FF
            label.SetNative("Adventure_33", 0f, 0f, 0, 0);                  // SetFont("Adventure_33") (iPad FT size 46), UILabel defaults: top-left, no spacing
            label.SetText(Profile.mission.ToString());                    // sprintf("%d", g_mission)
            MoveRight(label, IntroTextRect());
        }

        /// <summary>mainScene::LevelLeave 0xb69e4 / lifeModeScene::LevelLeave 0xf17e6: ids 13 and 14 slide out: start (cx, cy), mid cx + 50S, end cx - 480S, 0.05 s / 0.2 s; state = 3.</summary>
        void LevelLeave()
        {
            var day = GetControl(13);
            if (day != null) IntroMoveOut(day, IntroDayRect());
            UiLabel label = null; foreach (var l in Labels) if (l.id == 14) label = l;
            if (label != null) IntroMoveOut(label, IntroTextRect());
            levelState = 3;
        }

        static void IntroMoveOut(UiControl c, Rect r)
        {
            float s = N.S, cx = N.Add32(N.Mul32(r.width, 0.5f), r.x), cy = N.Add32(N.Mul32(r.height, 0.5f), r.y);
            c.StartMove(new MoveData(cx, cy, r.width, r.height, IntroWhite), new MoveData(N.Add32(cx, N.Mul32(s, 50f)), cy, r.width, r.height, IntroWhite),
                        new MoveData(N.Add32(cx, N.Mul32(s, -480f)), cy, r.width, r.height, IntroWhite), 0.05f, 0.2f);
        }

        static void IntroMoveOut(UiLabel c, Rect r)
        {
            float s = N.S, cx = N.Add32(N.Mul32(r.width, 0.5f), r.x), cy = N.Add32(N.Mul32(r.height, 0.5f), r.y);
            c.StartMove(new MoveData(cx, cy, r.width, r.height, IntroWhite), new MoveData(N.Add32(cx, N.Mul32(s, 50f)), cy, r.width, r.height, IntroWhite),
                        new MoveData(N.Add32(cx, N.Mul32(s, -480f)), cy, r.width, r.height, IntroWhite), 0.05f, 0.2f);
        }

        /// <summary>OnUIEvent 0xe6550 / 0xa69ae ids 12 and 13 (event type is not tested: the controls are not touchable, so only move ends arrive).</summary>
        bool HandleIntroEvent(UiControl c, int ev)
        {
            if (c.id == 12)
            {
                if (levelState == 1) { LevelEnter(); return true; }
                if (levelState == 3)
                {
                    DeleteControl(12);
                    StartGame();                       // +180 = 1, g_b_RightDown = 0, StartGame_N
                    return true;
                }
                return false;
            }
            if (c.id == 13)
            {
                if (levelState == 1) { SetTimer("LevelLeave", 0.5f); return true; }
                if (levelState == 3) { DeleteControl(13); DeleteLabel(14); LevelBGLeave(); return true; }
                return false;
            }
            return false;
        }

        /// <summary>mainScene::StartGame_N 0x94f68 / lifeModeScene::StartGame_N 0xe232e: delete the cover, state 4, arm the enemy spawn timers (and the g_time_id clock).</summary>
        void StartGame()
        {
            DeleteControl(11); introCover = null;
            levelState = 4;
            bulletSoundArmed = true;           // mainScene+180 := 1 (OnUIEvent 0xa6a9e)
            rightDown = false;                 // g_b_RightDown = 0
            ArmMissionClock();                 // g_time_id before the spawn timers (StartGame_N order)
            ArmSpawnTimers();
        }
    }
}
