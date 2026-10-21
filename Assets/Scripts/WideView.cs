using UnityEngine;

namespace Dc2Pilot
{
    /// <summary>
    /// ADAPTATION (DC2-BUGS-001 O1, user request: "fullscreen with no black bars at any resolution/aspect including triple/curve monitors";
    /// user choices: battle widens the world view, menus scale to cover and crop, on by default, persistent). Not native: the iPad draws the 1024x768
    /// logical screen only. Render-only: every native rule keeps the 1024x768 logical space (Camera::GetViewport width 1024, touch rects, spawn and
    /// despawn ranges, camera bounds). What changes:
    /// - battle (GameScene): the camera keeps the native height (768) and shows ExtraHalf more logical pixels on each side (negative = sides cropped on a
    ///   screen narrower than 4:3); the HUD anchors to the screen edge of the half its centre is in, and full-width solid overlays (whiteFrame) stretch;
    ///   the native background chains are repeated at their own chain length to fill the extra width (content is periodic with the native wrap period).
    /// - every other scene (r119, user decision, superseding cover-crop): on a wider screen the whole menu shows at native height and only the full-width red
    ///   splatter backgrounds (ui_menu_bg / ui_bg_all / ui_iap_bg) and strip overlays are widened over the extra width; on a narrower screen, cover (sides cropped).
    /// Off (or a 4:3 screen) = the native 4:3 letter/pillar-box, ExtraHalf 0, identical to before.
    /// Setting: PlayerPrefs "DC2_WideView" (default 1), outside the native dinocap2hd.sav. REVERSIBILITY: Enabled = false restores the native view.
    /// </summary>
    public static class WideView
    {
        const string Key = "DC2_WideView";
        static int enabled = -1;

        public static bool Enabled
        {
            get
            {
                if (enabled < 0) { try { enabled = PlayerPrefs.GetInt(Key, 1) != 0 ? 1 : 0; } catch (System.Exception) { enabled = 1; } }
                return enabled == 1;
            }
            set
            {
                enabled = value ? 1 : 0;
                try { PlayerPrefs.SetInt(Key, enabled); PlayerPrefs.Save(); } catch (System.Exception) { }
            }
        }

        /// <summary>Extra logical pixels shown on each side of the native 1024-wide battle view (0 outside battle, when off, or at 4:3).</summary>
        public static float ExtraHalf { get; private set; }
        /// <summary>Bumped whenever ExtraHalf changes; anchored controls re-place themselves.</summary>
        public static int Version { get; private set; }

        /// <summary>Edge anchor of a HUD rect: -1 left half, +1 right half (by its centre). r119: halves, not thirds: with thirds the kill icon (centre 661) stayed
        /// while its counter (692) moved right, and the run-progress line (centre ~775 / mainScene 743) split from the HUD (user items 2 and 10).</summary>
        public static int AnchorOf(Rect r)
        {
            float c = r.x + r.width * 0.5f;
            return c < N.ScreenW * 0.5f ? -1 : 1;
        }

        /// <summary>Render shift of an anchor (-1 / 0 / +1).</summary>
        public const float MaxAspect = 3440f / 1440f;   // ADAPTATION r119k: wide-view cap "21:9" (user decision). Marketed 21:9 panels are 2560x1080 (2.370) and 3440x1440 (2.389), both wider than 21/9 = 2.333, so the cap is the widest common 21:9 panel; anything wider gets black bars
        public static float Shift(int anchor)
        {
            if (ExtraHalf == 0f) return 0f;
            return anchor == -1 ? N.Add32(-ExtraHalf, SafeInsetLeft) : anchor == 1 ? N.Add32(ExtraHalf, -SafeInsetRight) : 0f;
        }

        /// <summary>PLATFORM_ADAPTER (r119u, user decision "inset for notch", DC1 NativeViewport approach): logical pixels by which the edge-anchored battle HUD
        /// (drawing and its touch rects, both through Shift) moves inward so it clears a phone's cutout / rounded corners: the part of Screen.safeArea's left / right
        /// inset not already covered by pillarbox bars. Mobile only; 0 elsewhere, at 4:3 and with wide view off.</summary>
        public static float SafeInsetLeft { get; private set; }
        public static float SafeInsetRight { get; private set; }
        /// <summary>Also used by the verifier to emulate a notch without a device.</summary>
        public static void SetSafeInsets(float left, float right, Dc2Scene scene)
        {
            left = Mathf.Max(0f, left); right = Mathf.Max(0f, right);
            if (left == SafeInsetLeft && right == SafeInsetRight) return;
            SafeInsetLeft = left; SafeInsetRight = right; Version++;
            if (scene != null) scene.ReapplyWide();
        }
        static void UpdateSafeInsets(Camera cam, Dc2Scene scene, int screenW, int screenH)
        {
            if (!Application.isMobilePlatform) return;   // editor / desktop: verifier-controlled (SetSafeInsets), default 0
            Rect safe = Screen.safeArea, px = cam.pixelRect;
            if (px.height <= 0f) return;
            float toLogical = N.ScreenH / px.height;                        // the camera shows 768 logical pixels over its pixel height
            float barL = px.xMin, barR = Mathf.Max(0f, screenW - px.xMax);
            SetSafeInsets(Mathf.Max(0f, safe.xMin - barL) * toLogical, Mathf.Max(0f, (screenW - safe.xMax) - barR) * toLogical, scene);
        }

        /// <summary>A native battle touch rect moved with the HUD art that shows it (the touch then lands where the button is drawn).</summary>
        public static Rect Anchored(Rect r) { float s = Shift(AnchorOf(r)); return s == 0f ? r : new Rect(r.x + s, r.y, r.width, r.height); }

        /// <summary>Camera for the current scene and screen size. The camera stays centred on the native logical screen (512, -384).</summary>
        public static void Apply(Camera cam, Dc2Scene scene, int screenW, int screenH)
        {
            if (cam == null || screenW <= 0 || screenH <= 0) return;
            float target = (float)N.ScreenW / N.ScreenH, cur = (float)screenW / screenH, e = 0f;
            if (!Enabled)
            {   // native 4:3 letter/pillar-box (the earlier CameraFit)
                cam.orthographicSize = N.ScreenH * 0.5f;
                if (cur > target) { float w = target / cur; cam.rect = new Rect((1f - w) * 0.5f, 0f, w, 1f); }
                else { float h = cur / target; cam.rect = new Rect(0f, (1f - h) * 0.5f, 1f, h); }
            }
            else
            {
                // r119k (user decision "shift + cap 21:9"): wide view stops at 21:9; a wider screen gets black bars beyond it (camera rect), so the
                // shifted boss/truck art and the extra width never exceed ExtraHalf = 384 * MaxAspect - 512 (about 405 px) per side.
                float eff = Mathf.Min(cur, MaxAspect);
                if (cur > MaxAspect) { float w = MaxAspect / cur; cam.rect = new Rect((1f - w) * 0.5f, 0f, w, 1f); }
                else cam.rect = new Rect(0f, 0f, 1f, 1f);
                if (scene is GameScene) { cam.orthographicSize = N.ScreenH * 0.5f; e = N.ScreenH * 0.5f * eff - N.ScreenW * 0.5f; }
                else if (cur > target) { cam.orthographicSize = N.ScreenH * 0.5f; e = N.ScreenH * 0.5f * eff - N.ScreenW * 0.5f; }   // r119 menus (user "fit, extend background"): whole menu at native height, the extra width shows the widened background
                else cam.orthographicSize = N.ScreenH * 0.5f;                           // narrower than 4:3: cover (full height, sides cropped)
            }
            SetExtraHalf(e, scene);
            UpdateSafeInsets(cam, scene, screenW, screenH);
            UpdateMenuMasks(cam, scene);
        }

        // r119e (user item 1, "still see the number 2 from the title text"): the title logo parks just past the left edge (LogoLeave, centre x - 480S) and the menu
        // background (whose curtain copies hid it) then leaves upward. Outside battle, everything past the native 1024 px is masked in the camera clear colour,
        // drawn just below the background curtains (UiControl.CurtainOrder): the widened red background still shows; anything else past the edge stays hidden,
        // as on the iPad, whose screen ends there. Same approved ADAPTATION (menus fit, extend background), render-only.
        static SpriteRenderer maskL, maskR; static Sprite maskSprite;
        public const int MenuMaskOrder = UiControl.CurtainOrder - 1;
        public static bool MenuMasksVisible { get { return maskL != null && maskL.enabled; } }   // DEBUG_ONLY verifier probe
        static void UpdateMenuMasks(Camera cam, Dc2Scene scene)
        {
            bool on = Enabled && scene != null && !(scene is GameScene) && ExtraHalf > 0f;
            if (!on) { if (maskL != null) { maskL.enabled = false; maskR.enabled = false; } return; }
            if (maskSprite == null)
            {
                var t = new Texture2D(1, 1); t.SetPixel(0, 0, Color.white); t.Apply();
                maskSprite = Sprite.Create(t, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), 1f);
            }
            if (maskL == null) maskL = MakeMask("wide_menu_mask_l");
            if (maskR == null) maskR = MakeMask("wide_menu_mask_r");
            Color c = cam != null ? cam.backgroundColor : Color.black; c.a = 1f;
            float e = ExtraHalf, h = N.ScreenH + 2f;
            Place(maskL, new Vector3(-e * 0.5f, -N.ScreenH * 0.5f, 0f), e, h, c);
            Place(maskR, new Vector3(N.ScreenW + e * 0.5f, -N.ScreenH * 0.5f, 0f), e, h, c);
        }
        static SpriteRenderer MakeMask(string name)
        {
            var go = new GameObject(name); if (Application.isPlaying) Object.DontDestroyOnLoad(go);
            var r = go.AddComponent<SpriteRenderer>(); r.sprite = maskSprite; r.sortingOrder = MenuMaskOrder; return r;
        }
        static void Place(SpriteRenderer r, Vector3 pos, float w, float h, Color c)
        { r.enabled = true; r.color = c; r.transform.position = pos; r.transform.localScale = new Vector3(w, h, 1f); }

        /// <summary>Also used by the verifier to emulate a screen aspect without a window.</summary>
        public static void SetExtraHalf(float e, Dc2Scene scene)
        {
            if (e == ExtraHalf) return;
            ExtraHalf = e; Version++;
            if (scene != null) scene.ReapplyWide();
        }
    }
}
