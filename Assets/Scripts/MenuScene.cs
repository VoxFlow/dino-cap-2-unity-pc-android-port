using System;
using UnityEngine;

namespace Dc2Pilot
{
    /// <summary>
    /// DinoCap::menuScene (splash -> title -> tap -> exit animations -> loading -> selectScene).
    /// Every number below comes from the decompile/ARM of DC2 v1.3 (registry ids dc2.startup.*, dc2.title.*, dc2.ui.*).
    /// </summary>
    public class MenuScene : Dc2Scene
    {
        // native fields: +12 state, +280 ready flag, +268 start control, +284 alpha, +288 direction
        public int state;
        public bool ready;
        public UiControl startCtrl;
        public float alpha = 208f;          // ctor: 0x43500000
        public bool fadingDown;             // ctor: 0
        public bool heroAnimStarted;
        public LoadingAnimation loadingAnimation;
        public int heroPick = -1;
        public bool loadResourceCalled;

        static readonly Color32 White = new Color32(255, 255, 255, 255);
        static readonly Color32 Clear = new Color32(255, 255, 255, 0);

        public override void OnLoad()
        {
            Atlas.Reset();
            Atlas.LoadNativeTable();
            Atlas.EnsureWhite();
            // splash: Image/Default-Landscape.png shown full-screen (iPad: 1024x768), startUI after 5.0 s
            var logoTex = Atlas.LoadTexture("DC2/Image/Default-Landscape");
            Atlas.CreateFrame("LogoFrame", logoTex, 0, 0, 1024, 768);
            SetTimer("startUI", 5.0f);
            var img = CreateControl(35, 5);
            img.consumesTouch = true;   // r118z4: native image left enabled (scan_ui_control_enable.py) - consumes touches in its rect (menuScene::OnLoad 0xcc2a6)
            img.SetTexture("LogoFrame");
            img.SetRect(new Rect(0, 0, 1024, 768));
        }

        void StartUI()
        {
            DeleteControl(35);
            Game.OngameSound = Profile.sound;
            Game.OngameFx = Profile.fx;
            state = 0;                                  // native: g_state = 1 is a global; scene state (+12) stays 0

            var tex = Atlas.LoadTexture("DC2/Image/ip/uimenu_h");
            Atlas.CreateFrame("ui_menu_bgFrame", tex, 0, 0, 960, 640);
            Atlas.CreateFrame("ui_menu_dinoFrame", tex, 0, 640, 846, 322);
            Atlas.CreateFrame("ui_menu_startFrame", tex, 0, 962, 484, 56);
            var load = Atlas.LoadTexture("DC2/Image/ip/loadimg_h");
            Atlas.CreateFrame("yuanliFrame", load, 0, 0, 75, 81);
            Atlas.CreateFrame("wuqi0007Frame", load, 75, 0, 245, 140);
            Atlas.CreateFrame("danshou0003Frame", load, 0, 140, 210, 110);
            // loadingFrame comes from the recovered native table: (707,0,230,44).
            loadingAnimation = LoadingAnimation.Create(transform);

            InitUI();
            Game.Audio.PlayMusic("Music_menuAudio", true, "menu_bgAudio");
        }

        Rect BgRect()
        {
            // InitUI: RectScale(0,0,480,320) -> (0,0,960,640); iPad: y += 32, w += 64, h += 64
            Rect r = N.RectScale(0, 0, 480, 320);
            return new Rect(r.x, r.y + 32f, r.width + 64f, r.height + 64f);
        }

        void InitUI()
        {
            var bg = CreateControl(8, 0);
            bg.touchable = false;                       // vtable+44(0)
            bg.SetTexture("ui_menu_bgFrame");
            bg.SetRect(BgRect());
            SetTimer("LogoEnter", 0.2f);
            SetTimer("NewGameEnter", 0.3f);
        }

        // ---- menuScene::startScaleUI (0xccdfe): centre-based grow 0.1 -> 1.1 (0.2 s) -> 1.0 (0.05 s) ----
        void StartScaleUI(UiControl c, Rect r)
        {
            float cx = r.width * 0.5f + r.x, cy = r.height * 0.5f + r.y;
            var s = new MoveData(cx, cy, r.width * 0.1f, r.height * 0.1f, White);
            var m = new MoveData(cx, cy, r.width * 1.1f, r.height * 1.1f, White);
            var e = new MoveData(cx, cy, r.width, r.height, White);
            c.StartMove(s, m, e, 0.2f, 0.05f);
        }

        // startMoveUIUp (0xcc900): mid y = cy + 20s, end y = cy - 320s, (0.05, 0.2)
        void StartMoveUIUp(UiControl c, Rect r)
        {
            float cx = r.width * 0.5f + r.x, cy = r.height * 0.5f + r.y;
            var s = new MoveData(cx, cy, r.width, r.height, White);
            var m = new MoveData(cx, cy + 20f * N.S, r.width, r.height, White);
            var e = new MoveData(cx, cy - 320f * N.S, r.width, r.height, White);
            c.StartMove(s, m, e, 0.05f, 0.2f);
        }

        // startMoveUILeft (0xccb6e): slides in from the left: start x = cx - 480s, mid x = cx + 20s, end = cx, (0.2, 0.05)
        void StartMoveUILeft(UiControl c, Rect r)
        {
            float cx = r.width * 0.5f + r.x, cy = r.height * 0.5f + r.y;
            var s = new MoveData(cx - 480f * N.S, cy, r.width, r.height, White);
            var m = new MoveData(cx + 20f * N.S, cy, r.width, r.height, White);
            var e = new MoveData(cx, cy, r.width, r.height, White);
            c.StartMove(s, m, e, 0.2f, 0.05f);
        }

        protected override void OnTimeout(string name)
        {
            switch (name)
            {
                case "startUI": StartUI(); break;
                case "LogoEnter": LogoEnter(); break;
                case "NewGameEnter": NewGameEnter(); break;
                case "LogoLeave": LogoLeave(); break;
                case "NewGameLeave": NewGameLeave(); break;
                case "OnBgLeave": OnBgLeave(); break;
                case "LoadingEnter": LoadingEnter(); break;
            }
        }

        static Rect Ipad(float x, float y, float w, float h)
        {
            // RectScale then the hand-coded iPad (+32,+76) used by the title rects
            Rect r = N.RectScale(x, y, w, h);
            return new Rect(r.x + 32f, r.y + 76f, r.width, r.height);
        }

        void LogoEnter()
        {
            Rect r = Ipad(28, 5, 423, 161);
            var c = CreateControl(0, 0);
            c.touchable = false;
            c.SetTexture("ui_menu_dinoFrame");
            StartScaleUI(c, r);
        }

        void NewGameEnter()
        {
            Rect r = Ipad(119, 235, 242, 28);
            var c = CreateControl(1, 1);
            c.SetTexture("ui_menu_startFrame");
            startCtrl = c;
            StartScaleUI(c, r);
        }

        // ---- exit sequence (registry dc2.title.exit_sequence) ----
        void LogoLeave()
        {
            // start rect is hard-coded and differs from the displayed logo (native quirk, kept)
            Rect r = Ipad(240, 68, 448, 137);
            var c = GetControl(0);
            if (c == null) return;
            var s = new MoveData(r.x, r.y, r.width, r.height, White);
            var m = new MoveData(r.x + 20f * N.S, r.y, r.width, r.height, White);
            var e = new MoveData(r.x - 480f * N.S, r.y, r.width, r.height, White);
            c.StartMove(s, m, e, 0.05f, 0.2f);
        }

        void NewGameLeave()
        {
            Rect r = Ipad(240, 235, 242, 28);
            var c = GetControl(1);
            startCtrl = c;
            if (c == null) return;
            var s = new MoveData(r.x, r.y, r.width, r.height, White);
            var m = new MoveData(r.x + N.F(-1035468800) * N.S, r.y, r.width, r.height, White);   // -50 * s
            var e = new MoveData(r.x + 480f * N.S, r.y, r.width, r.height, White);
            c.StartMove(s, m, e, 0.05f, 0.2f);
        }

        void OnBgLeave()
        {
            Rect r = N.RectScale(0, 0, 480, 320);
            r = new Rect(r.x, r.y, r.width + 64f, r.height + 64f);   // no +32 y here (native)
            var c = CreateControl(8, 0);
            c.SetRect(r);
            c.SetTexture("ui_menu_bgFrame");
            c.touchable = false;
            StartMoveUIUp(c, r);
        }

        void LoadingEnter()
        {
            // RectScale(340,257,115,22) + RectOffset; iPad: y += 56
            Rect r = N.RectOffset(N.RectScale(340, 257, 115, 22));
            r = new Rect(r.x, r.y + 56f, r.width, r.height);
            DeleteControl(14);
            var c = CreateControl(14, 0);
            c.SetTexture("loadingFrame");
            c.touchable = false;
            StartMoveUILeft(c, r);
        }

        // ---- InitAnim / OnResume ----
        void InitAnim()
        {
            // menuScene::InitAnim 0xc9d18: libc rand() from the shared native stream; native float conversion preserved, including the rounded upper case4/hero5 branch.
            int raw = NativeRand.Raw();
            heroPick = LoadingAnimation.SelectionFromNativeRand(raw);
            loadingAnimation.Begin(heroPick);
            heroAnimStarted = true;
        }

        void OnResume()
        {
            SetTimer("LogoLeave", 0.1f);
            SetTimer("NewGameLeave", 0.2f);
            SetTimer("OnBgLeave", 0.3f);
        }

        // ---- button effect: cloned overlay (0xcc6f0) ----
        void ButtonEffect(string frame, UiControl src)
        {
            Rect r = src.CurrentRect;
            var c = GetControl(23);
            if (c == null) { c = CreateControl(23, 3); c.touchable = false; }
            c.SetTexture(frame);
            c.SetVisible(true);
            float cx = r.x + r.width * 0.5f, cy = r.y + r.height * 0.5f;
            var s = new MoveData(cx, cy, r.width, r.height, Clear);
            var m = new MoveData(cx, cy, r.width, r.height, White);
            var e = new MoveData(cx, cy, r.width * 1.5f, r.height * 1.5f, White);
            c.StartMove(s, m, e, 0.05f, 0.2f);
        }

        protected override void OnTouchEvent(int type, Vector2 p)
        {
            // native ignores the touch type/position: any touch while ready (and state not 1/4)
            if (state != 1 && state != 4 && ready)
            {
                ready = false;
                ButtonEffect("ui_menu_startFrame", GetControl(1));
                state = 1;
            }
        }

        protected override void OnUIEvent(UiControl c, int ev)
        {
            switch (c.id)
            {
                case 1:
                    if (ev == 1 && state == 0) ready = true;
                    break;
                case 8:
                    if (ev == 1 && state == 1) SetTimer("LoadingEnter", 0.2f);
                    break;
                case 14:
                    if (ev == 1 && state == 1) state = 4;
                    break;
                case 23:
                    if (ev == 1)
                    {
                        c.SetVisible(false);
                        if (state == 1) { InitAnim(); OnResume(); }
                    }
                    break;
            }
        }

        protected override void OnUpdate(float dt)
        {
            if (startCtrl != null)
            {
                // ARM-verified (0xcbc5c-0xcbd0a): +-288/s, flip below 80 / above 250, unclamped
                if (fadingDown)
                {
                    alpha += dt * 192f * -1.5f;
                    if (alpha < 80f) fadingDown = false;
                }
                else
                {
                    alpha += dt * 192f * 1.5f;
                    if (alpha > 250f) fadingDown = true;
                }
                startCtrl.SetColor(new Color32(255, 255, 255, (byte)Mathf.Clamp(alpha, 0f, 255f)));
            }
            if (state == 4 && !loadResourceCalled)
            {
                loadResourceCalled = true;
                Game.SwitchScene("selectScene");        // LoadResource (non-life mode): m_map_type := 0, SwitchScene
            }
        }

        protected override void OnObjectUpdate(float dt)
        {
            if (heroAnimStarted) loadingAnimation.Step(dt);
        }
    }
}
