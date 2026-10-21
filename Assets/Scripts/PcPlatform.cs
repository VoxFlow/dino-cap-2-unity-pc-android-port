using UnityEngine;

namespace Dc2Pilot
{
    /// <summary>
    /// PLATFORM_ADAPTER (user-requested: "I need PC controls to play normally, integrate DC1 controls and add space key as the dash-roll key").
    /// No gameplay semantics of its own:
    /// - Window: the game keeps its native 1024x768 logical space (iPad 4 shows the same 1024x768 points on a 2048x1536 retina panel, which is why a
    ///   1024x768-pixel PC window looks small). The first windowed launch opens the largest 4:3 window that fits 85% of the desktop height; the window is
    ///   resizable (CameraFit letter/pillar-boxes), F11 or Alt+Enter toggles borderless fullscreen, and the windowed size + fullscreen state are remembered.
    ///   Same scheme as DC1's DC1PlatformIntegration (DC1 read-only reference, not copied code).
    /// - Keyboard: every key is a synthetic finger pressing the centre of the matching native battle touch rect, so the recovered OnTouchEvent rules
    ///   (per-finger ids, arrow takeover, roll/egg/pause gates, swap on release) decide everything. User layout (final): A left, D right,
    ///   S dash-roll, left click / K shoot, right click / J melee, mouse wheel or L switch weapon (one change-button tap per notch / press; L added r119k, user), Esc pause.
    ///   In battle a left click on a clickable UI control (pause menu, Done button) is still a normal touch; anywhere else it is the shoot finger.
    /// REVERSIBILITY: delete this file and the PcPlatform line in Bootstrap.Boot.
    /// </summary>
    public sealed class PcPlatform : MonoBehaviour
    {
        public Dc2Game game;
        const string FsKey = "DC2_Fullscreen", WKey = "DC2_Window_W", HKey = "DC2_Window_H";

        struct Binding { public KeyCode key; public int action; }
        // action: 0 left 1 right 2 melee 3 roll 4 fire 5 switch weapon 6 pause
        static readonly Binding[] Bindings =
        {
            new Binding { key = KeyCode.A, action = 0 },
            new Binding { key = KeyCode.D, action = 1 },
            new Binding { key = KeyCode.J, action = 2 },
            new Binding { key = KeyCode.S, action = 3 },
            new Binding { key = KeyCode.K, action = 4 },
            new Binding { key = KeyCode.L, action = 5 },   // r119k (user decision): weapon switch = a finger on the ChangeRect (the weapon icon touch area)
            new Binding { key = KeyCode.Escape, action = 6 },
        };
        const int FingerBase = 100;   // synthetic finger ids 100.. (finger 0 = mouse, real touches use Unity fingerIds 0..9)

        public static Vector2 ActionPoint(int action)
        {
            switch (action)
            {
                case 0: return GameScene.LeftRect.center;
                case 1: return GameScene.RightRect.center;
                case 2: return GameScene.HitRect.center;
                case 3: return GameScene.RollRect.center;
                case 4: return GameScene.FireRect.center;
                case 5: return GameScene.ChangeRect.center;
                default: return GameScene.StopRect.center;
            }
        }

        /// <summary>Called by Dc2Game.Update before the scene step (same order as the native touch dispatch at the top of Scene::Update).</summary>
        public void PumpKeys(Dc2Scene scene)
        {
            if (!(scene is GameScene)) { System.Array.Clear(keyHeld, 0, keyHeld.Length); return; }   // menus stay mouse/touch driven
            for (int i = 0; i < Bindings.Length; i++)
            {
                var b = Bindings[i];
                Vector2 pt = ActionPoint(b.action);
                if (b.action == 6) { Vector2 r; if (ResumePoint(scene as GameScene, out r)) pt = r; }   // paused: Esc presses the pause menu's resume button
                if (Input.GetKeyDown(b.key)) { escPoint = pt; keyHeld[i] = true; game.QueueTouch(0, pt, FingerBase + i); }
                else if (keyHeld[i] && !Input.GetKey(b.key)) { keyHeld[i] = false; game.QueueTouch(2, b.action == 6 ? escPoint : pt, FingerBase + i); }
            }
        }

        // R002-N1: releases are level-checked (pressed earlier, no longer held) instead of GetKeyUp, so a release that happened while the F1 menu gated
        // input (or while the window had no focus) still ends that synthetic finger on the next pumped frame. No gameplay reset is added.
        readonly bool[] keyHeld = new bool[Bindings.Length];
        bool meleeHeld;
        Vector2 escPoint;
        /// <summary>While the pause menu is up (pauseCtrl children in place, not leaving), Esc taps the drawn resume button (control 66): the native stop rect does nothing in
        /// the paused state, so without this Esc could pause but never resume (user report). PLATFORM_ADAPTER: the click itself goes through the native touch path.</summary>
        static bool ResumePoint(GameScene s, out Vector2 p)
        {
            p = default(Vector2);
            if (s == null || !s.paused) return false;
            var c = s.GetControl(66);
            if (c == null || c.IsMoving) return false;
            p = c.CurrentRect.center; return true;
        }

        const int MouseFireFinger = 120, MouseMeleeFinger = 121, WheelFinger = 122;
        bool leftIsFire;

        /// <summary>Battle mouse: returns true when the left button belongs to the shoot finger this frame (Dc2Game then skips its finger-0 touch).</summary>
        public bool PumpMouse(Dc2Scene scene, Vector2 logical)
        {
            if (!(scene is GameScene)) { leftIsFire = false; meleeHeld = false; return false; }
            if (Input.GetMouseButtonDown(1)) { meleeHeld = true; game.QueueTouch(0, ActionPoint(2), MouseMeleeFinger); }
            else if (meleeHeld && !Input.GetMouseButton(1)) { meleeHeld = false; game.QueueTouch(2, ActionPoint(2), MouseMeleeFinger); }   // R002-N1 level-checked release
            float wheel = Input.mouseScrollDelta.y;
            if (wheel != 0f) { game.QueueTouch(0, ActionPoint(5), WheelFinger); game.QueueTouch(2, ActionPoint(5), WheelFinger); }
            // a click on a UI button or on the native stop (pause) rect is a normal touch, not the shoot finger (the stop icon is a plain image; its rect lives in OnTouchEvent)
            if (Input.GetMouseButtonDown(0)) leftIsFire = !scene.UiButtonAt(logical) && !GameScene.NativeTouchInside(GameScene.StopRect, logical);
            if (!leftIsFire) return false;
            if (Input.GetMouseButtonDown(0)) game.QueueTouch(0, ActionPoint(4), MouseFireFinger);
            if (!Input.GetMouseButtonDown(0) && !Input.GetMouseButton(0)) { game.QueueTouch(2, ActionPoint(4), MouseFireFinger); leftIsFire = false; }   // R002-N1 level-checked release
            return true;
        }

        public static void Install(GameObject host, Dc2Game game)
        {
            var p = host.AddComponent<PcPlatform>(); p.game = game; game.pc = p;
            if (!Application.isEditor && !Application.isMobilePlatform) p.RestoreWindow();
        }

        void RestoreWindow()
        {
            if (PlayerPrefs.GetInt(FsKey, 0) == 1)
            {
                var r = Screen.currentResolution;
                Screen.SetResolution(r.width, r.height, FullScreenMode.FullScreenWindow);
                return;
            }
            int w = PlayerPrefs.GetInt(WKey, 0), h = PlayerPrefs.GetInt(HKey, 0);
            if (w <= 0 || h <= 0)
            {
                var r = Screen.currentResolution;
                h = Mathf.Max(N.ScreenH / 2, Mathf.FloorToInt(r.height * 0.85f));
                w = Mathf.RoundToInt(h * (float)N.ScreenW / N.ScreenH);
                if (w > r.width * 0.95f) { w = Mathf.FloorToInt(r.width * 0.95f); h = Mathf.RoundToInt(w * (float)N.ScreenH / N.ScreenW); }
            }
            Screen.SetResolution(w, h, FullScreenMode.Windowed);
        }

        int lastW, lastH;
        void Update()
        {
            if (Application.isEditor || Application.isMobilePlatform) return;
            bool alt = Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt);
            if (Input.GetKeyDown(KeyCode.F11) || (alt && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))) ToggleFullscreen();
            if (!Screen.fullScreen && (Screen.width != lastW || Screen.height != lastH))
            {
                lastW = Screen.width; lastH = Screen.height;
                PlayerPrefs.SetInt(WKey, lastW); PlayerPrefs.SetInt(HKey, lastH); PlayerPrefs.Save();
            }
        }

        void ToggleFullscreen()
        {
            if (Screen.fullScreen)
            {
                PlayerPrefs.SetInt(FsKey, 0); PlayerPrefs.Save();
                RestoreWindow();
            }
            else
            {
                PlayerPrefs.SetInt(FsKey, 1); PlayerPrefs.Save();
                var r = Screen.currentResolution;
                Screen.SetResolution(r.width, r.height, FullScreenMode.FullScreenWindow);
            }
        }
    }
}
