using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dc2Pilot
{
    /// <summary>
    /// UIImage + DinoCap::UIMoveCtrl (tween + press/release button behaviour).
    /// Contracts: dc2.ui.UIMoveCtrl.tween (ARM-verified timers), dc2.ui.UIMoveCtrl.touch.
    /// Logical coordinates: pixels, origin top-left, y down (1024x768 iPad layout).
    /// </summary>
    public class UiControl : MonoBehaviour
    {
        /// <summary>The native UIManager draws after every scene object, so all UI renderers sit above any world renderer order (world sprites use orders below 1000; in the editor render path nested SortingGroups are not honoured, so a world sprite of order 50 covered a layer-0 HUD icon of order 9).</summary>
        public const int UiOrderBase = 1000;
        public int id;
        public Dc2Scene scene;
        public bool touchable = true;      // native vtable+44(false) turns this off for non-buttons
        /// <summary>r118z4: the native control is a plain UIImage (UIManager::CreateImage) left enabled. UIImage::HandleTouch 0x17f1ac returns
        /// UIControl::PtInRect 0x17e70c, so every touch phase inside its rect (and clip) is consumed with no event, visible or not
        /// (UIManager::HandleUITouch 0x18102e tests only the enable flag). Set only at sites scan_ui_control_enable.py lists as enabled.</summary>
        public bool consumesTouch;
        public bool visible = true;
        /// <summary>ADAPTATION (WideView, DC2-BUGS-001 O1): render-only edge anchor of a battle HUD control (-1 left, +1 right, 0 none). Rects, hit tests and tweens stay native.</summary>
        public int wideAnchor;

        SpriteRenderer sr;
        Rect rect;                         // currently drawn rect
        Rect fullRect;                     // native +148: rect restored after press
        bool pressed;                      // native +144

        // tween state (native +64..+141)
        MoveData start, mid, end;
        float d1, d2;                      // native +124 (a5) / +128 (a6)
        float t;                           // native +136
        int phase;                         // native +140: 1 then 2
        bool active;                       // native +132

        public bool IsMoving { get { return active; } }
        public string DebugTween { get { return "t=" + t + " phase=" + phase + " d1=" + d1 + " d2=" + d2; } }
        public Rect CurrentRect { get { return rect; } }
        public Rect FullRect { get { return fullRect; } }

        public static UiControl Create(Dc2Scene scene, int id, int layer)
        {
            var go = new GameObject("ui" + id);
            go.transform.SetParent(scene.transform, false);
            var c = go.AddComponent<UiControl>();
            c.id = id; c.scene = scene;
            c.sr = go.AddComponent<SpriteRenderer>();
            c.sr.sortingOrder = scene.RegisterUi(c, layer);   // layer, then insertion index (r118z L6; was base + layer*100 + scene-wide sequence, which let a late low-layer control draw over an early higher-layer one)
            return c;
        }

        public void SetUiOrder(int order) { sr.sortingOrder = order; }
        public int UiOrderForTest { get { return sr.sortingOrder; } }   // DEBUG_ONLY verifier probe (R002-N2)

        public void SetTexture(string frameName)
        {
            sr.sprite = Atlas.Get(frameName); baseSprite = null;
            ApplyRect();
        }

        public string FrameName { get { var s = baseSprite != null ? baseSprite : sr.sprite; return s != null ? s.name : null; } }

        public void SetColor(Color32 c) { sr.color = c; }
        public Color32 CurrentColor { get { return sr.color; } }   // DEBUG_ONLY verifier probe

        public void SetVisible(bool v) { visible = v; sr.enabled = v; if (clipOn) ApplyRect(); }

        /// <summary>UIControl::SetRect (vtable+12): also becomes the restore rect while not pressed.</summary>
        public void SetRect(Rect r)
        {
            rect = r;
            fullRect = r;
            ApplyRect();
        }

        /// <summary>UIMoveCtrl::SetRectRange (0xcd448): sets the hit/restore rect and draws it.</summary>
        public void SetRectRange(Rect r) { SetRect(r); }

        void ApplyRect()
        {
            if (sr.sprite == null) return;
            if (clipOn) { ApplyClipped(); return; }
            if (baseSprite != null) { sr.sprite = baseSprite; baseSprite = null; }
            Vector2 sz = sr.sprite.rect.size;
            transform.localPosition = new Vector3(rect.x + rect.width * 0.5f, -(rect.y + rect.height * 0.5f), 0f);
            transform.localScale = new Vector3(rect.width / sz.x, rect.height / sz.y, 1f);
            ApplyWide(sz.x);
        }

        /// <summary>ADAPTATION (WideView): shift an anchored HUD control; stretch a full-width battle overlay drawn from a strip texture (<= 64 px wide: whiteFrame 4,
        /// missionfailbgFrame 30, fail_lineFrame 6; the native code already stretches these across 1024, so they vary only vertically) over the widened view.
        /// r119 (user item 7): the result backdrop / band and the fail backdrop were missed by the whiteFrame-only rule.</summary>
        // r119d (user item 1: "DC2 number 2 visible on the outer left during the title animation"): natively anything outside the 1024-wide screen is simply
        // not visible, and title / menu pieces leave by moving past the edge. With the widened menu background the space past the edge is shown, so the
        // background's two outer strips are also drawn ABOVE all menu content (same art, same place): whatever lies outside the native screen stays hidden.
        SpriteRenderer curtainL, curtainR; Sprite curtainSpriteL, curtainSpriteR;
        public const int CurtainOrder = 32000;
        void HideCurtains() { if (curtainL != null) curtainL.enabled = false; if (curtainR != null) curtainR.enabled = false; }
        void ApplyCurtains(float e)
        {
            Sprite src = sr.sprite; if (src == null) { HideCurtains(); return; }
            Rect tr = src.textureRect; float total = rect.width + 2f * e, f = e / total;
            if (curtainSpriteL != null) Destroy(curtainSpriteL); if (curtainSpriteR != null) Destroy(curtainSpriteR);
            curtainSpriteL = Sprite.Create(src.texture, new Rect(tr.x, tr.y, tr.width * f, tr.height), new Vector2(0.5f, 0.5f), src.pixelsPerUnit, 0, SpriteMeshType.FullRect);
            curtainSpriteR = Sprite.Create(src.texture, new Rect(tr.x + tr.width * (1f - f), tr.y, tr.width * f, tr.height), new Vector2(0.5f, 0.5f), src.pixelsPerUnit, 0, SpriteMeshType.FullRect);
            if (curtainL == null) { var go = new GameObject("wide_curtain_l"); go.transform.SetParent(transform.parent, false); curtainL = go.AddComponent<SpriteRenderer>(); }
            if (curtainR == null) { var go = new GameObject("wide_curtain_r"); go.transform.SetParent(transform.parent, false); curtainR = go.AddComponent<SpriteRenderer>(); }
            float cy = -(rect.y + rect.height * 0.5f);
            Place(curtainL, curtainSpriteL, new Vector3(rect.x - e * 0.5f, cy, 0f), e, rect.height);
            Place(curtainR, curtainSpriteR, new Vector3(rect.xMax + e * 0.5f, cy, 0f), e, rect.height);
        }
        void Place(SpriteRenderer r, Sprite s, Vector3 pos, float w, float h)
        {
            r.sprite = s; r.color = sr.color; r.sortingOrder = CurtainOrder; r.enabled = sr.enabled;
            r.transform.localPosition = pos; r.transform.localScale = new Vector3(w / s.rect.width, h / s.rect.height, 1f);
        }
        void OnDestroy() { if (curtainL != null) Destroy(curtainL.gameObject); if (curtainR != null) Destroy(curtainR.gameObject); if (curtainSpriteL != null) Destroy(curtainSpriteL); if (curtainSpriteR != null) Destroy(curtainSpriteR); }

        void ApplyWide(float spriteW)
        {
            float e = WideView.ExtraHalf;
            if (e <= 0f) HideCurtains();
            if (e == 0f) return;
            if (wideAnchor != 0) { var p = transform.localPosition; p.x += WideView.Shift(wideAnchor); transform.localPosition = p; return; }
            // menus (r119, user "fit, extend background"): the full-width red splatter backgrounds are widened too (approved stretch of that art only; buttons,
            // panels, the splash logo and the map are not touched)
            // r119e (user item 9, bank misaligned): ui_iap_bgFrame is not a plain splatter background: the bank title, the six product cells, coins and the ammo
            // pack are painted into it, so widening it slid every cell away from the native selection frame and touch rects. It stays at its native size; the
            // sides show the widened ui_bg_allFrame (id 9100) that selectScene::OnLoad puts under every select screen.
            bool menuBackground = !(scene is GameScene) && (FrameName == "ui_menu_bgFrame" || FrameName == "ui_bg_allFrame");
            if (e > 0f && rect.x <= 0f && rect.xMax >= N.ScreenW && spriteW > 0f && (spriteW <= 64f || menuBackground))
            {
                var k = transform.localScale; k.x = (rect.width + 2f * e) / spriteW; transform.localScale = k;
                if (menuBackground) ApplyCurtains(e); else HideCurtains();
            }
            else HideCurtains();
        }

        /// <summary>Re-place after WideView.ExtraHalf changed (window resize, scene switch).</summary>
        public void ReapplyWide() { ApplyRect(); }

        // ---- UIControlVisible::SetClip 0x17f248 / ClearClip 0x17f208 -> UISprite::SetClip 0x17e638 (+80 = 1, rect +84..+96) / ClearClip 0x17e64e ----
        bool clipOn; Rect clip; Sprite baseSprite, clipSprite;
        /// <summary>UIControl vtable+0x34 (SetClip): the image keeps its rect and scale; UISprite::GetPointer 0x17e85e cuts the quad to the clip rect and
        /// interpolates the texture coordinates linearly (binary32), so the visible part shows the matching part of the texture unscaled. A quad entirely
        /// outside the clip collapses to a point (nothing drawn).</summary>
        public void SetClip(Rect r) { clipOn = true; clip = r; ApplyRect(); }
        /// <summary>UIControl::PtInRect 0x17e70c: half-open (left <= x < left+w, top <= y < top+h, binary32 sums) and, with a clip set, the same test against the clip rect.</summary>
        public bool NativeControlPtInRect(Vector2 p) { return UiHit.HalfOpen(rect, p) && (!clipOn || UiHit.HalfOpen(clip, p)); }
        public void ClearClip() { clipOn = false; ApplyRect(); }

        void ApplyClipped()
        {
            var src = baseSprite != null ? baseSprite : sr.sprite;
            baseSprite = src;
            float l = rect.x, t = rect.y, w = rect.width, h = rect.height, r = N.Add32(l, w), b = N.Add32(t, h);
            float cl = clip.x, ct = clip.y, cr = N.Add32(clip.x, clip.width), cb = N.Add32(clip.y, clip.height);
            // GetPointer: no quad when the sprite lies outside the clip on either axis (strict > tests)
            if (cl > r || l > cr || ct > b || t > cb) { sr.enabled = false; return; }
            float vl = l < cl ? cl : l, vr = r > cr ? cr : r, vt = t < ct ? ct : t, vb = b > cb ? cb : b;
            float vw = N.Add32(vr, -vl), vh = N.Add32(vb, -vt);
            sr.enabled = visible && vw > 0f && vh > 0f;
            if (!sr.enabled) return;
            Rect tr = src.textureRect;
            float fx0 = N.Div32(N.Add32(vl, -l), w), fx1 = N.Div32(N.Add32(vr, -l), w), fy0 = N.Div32(N.Add32(vt, -t), h), fy1 = N.Div32(N.Add32(vb, -t), h);
            var sub = new Rect(tr.x + fx0 * tr.width, tr.y + (1f - fy1) * tr.height, (fx1 - fx0) * tr.width, (fy1 - fy0) * tr.height);   // texture y is up, UI y is down
            if (clipSprite != null) Destroy(clipSprite);
            clipSprite = Sprite.Create(src.texture, sub, new Vector2(0.5f, 0.5f), src.pixelsPerUnit, 0, SpriteMeshType.FullRect);
            sr.sprite = clipSprite;
            transform.localPosition = new Vector3(vl + vw * 0.5f + WideView.Shift(wideAnchor), -(vt + vh * 0.5f), 0f);
            transform.localScale = new Vector3(vw / sub.width, vh / sub.height, 1f);
        }
        public bool DebugClipped(out Rect visible) { visible = sr.enabled && clipOn ? new Rect(transform.localPosition.x - transform.localScale.x * sr.sprite.rect.width * 0.5f, -transform.localPosition.y - transform.localScale.y * sr.sprite.rect.height * 0.5f, transform.localScale.x * sr.sprite.rect.width, transform.localScale.y * sr.sprite.rect.height) : Rect.zero; return clipOn && sr.enabled; }   // DEBUG_ONLY

        // ---- UIMoveCtrl::StartMove / Update (native 0xcd466 / 0xcd796) ----
        public void StartMove(MoveData s, MoveData m, MoveData e, float dur1, float dur2)
        {
            start = s; mid = m; end = e; d1 = dur1; d2 = dur2;
            active = true; t = 0f; phase = 1;
        }

        public void UpdateMove(float dt)
        {
            if (!active) return;
            t += dt;
            float r;
            MoveData from, to;
            if (phase == 1)
            {
                r = t / d1;
                if (r >= 1f) { phase = 2; r = 1f; t -= d1; }
                from = start; to = mid;
            }
            else
            {
                r = t / d2;
                if (r >= 1f) { r = 1f; active = false; }
                from = mid; to = end;
            }
            float cx = Mathf.LerpUnclamped(from.cx, to.cx, r);
            float cy = Mathf.LerpUnclamped(from.cy, to.cy, r);
            float w = Mathf.LerpUnclamped(from.w, to.w, r);
            float h = Mathf.LerpUnclamped(from.h, to.h, r);
            // native lerps each colour byte as float then truncates (fixunssfsi)
            var col = new Color32(
                (byte)(from.col.r + r * (to.col.r - from.col.r)),
                (byte)(from.col.g + r * (to.col.g - from.col.g)),
                (byte)(from.col.b + r * (to.col.b - from.col.b)),
                (byte)(from.col.a + r * (to.col.a - from.col.a)));
            rect = new Rect(cx - w * 0.5f, cy - h * 0.5f, w, h);
            fullRect = rect;
            ApplyRect();
            sr.color = col;
            if (!active) scene.OnUIEventInternal(this, 1);
        }

        /// <summary>Triniti2D::Rect::PtInRect 0x16fc4: all four edges excluded (Unity Rect.Contains includes the left and top edge).</summary>
        static bool PtInRect(Rect r, Vector2 p) { return p.x > r.x && p.y > r.y && p.x < N.Add32(r.x, r.width) && p.y < N.Add32(r.y, r.height); }

        // ---- UIMoveCtrl::HandleTouch (native 0xcd542) ----
        /// <summary>type: 0 down, 1 move, 2 up. Returns true when consumed (native returns 1 on click).</summary>
        public bool HandleTouch(int type, Vector2 p)
        {
            if (consumesTouch) return touchable && NativeControlPtInRect(p);   // UIImage::HandleTouch 0x17f1ac (r118z4)
            if (active || !touchable || !visible) return false;
            if (type == 1)
            {
                if (pressed && !PtInRect(fullRect, p))
                {
                    pressed = false;
                    rect = fullRect; ApplyRect();
                }
            }
            else if (type == 2)
            {
                if (pressed)
                {
                    pressed = false;
                    rect = fullRect; ApplyRect();
                    scene.Audio.PlaySound("Audio_buttonAudio");
                    scene.OnUIEventInternal(this, 0);
                    return true;
                }
            }
            else if (type == 0 && PtInRect(fullRect, p))
            {
                pressed = true;
                // native: x += 0.25*w, y += 0.25*h, w *= 0.5, h *= 0.5 (centred 50% size)
                rect = new Rect(fullRect.x + 0.25f * fullRect.width, fullRect.y + 0.25f * fullRect.height,
                                0.5f * fullRect.width, 0.5f * fullRect.height);
                ApplyRect();
            }
            return false;
        }
    }

    /// <summary>UIControl::PtInRect 0x17e70c rect test (r118z4).</summary>
    public static class UiHit
    {
        public static bool HalfOpen(Rect r, Vector2 p) { return p.x >= r.x && p.x < N.Add32(r.x, r.width) && p.y >= r.y && p.y < N.Add32(r.y, r.height); }
    }

    /// <summary>UILabel: text drawn with the original Adventure TTF.</summary>
    public class UiLabel : MonoBehaviour
    {
        public int id;
        /// <summary>r118z4: native UILabel / UILabelMoveCtrl left enabled: UILabel::HandleTouch 0x17fd98 consumes every touch inside its rect
        /// (UIControl::PtInRect 0x17e70c, half-open). Set only at sites scan_ui_control_enable.py lists as enabled.</summary>
        public bool consumesTouch;
        public bool ConsumesTouch(Vector2 p) { return consumesTouch && UiHit.HalfOpen(rect, p); }
        TextMesh tm;
        MeshRenderer mr;
        Rect rect;
        public int hAlign, vAlign;
        /// <summary>ADAPTATION (WideView, DC2-BUGS-001 O1): render-only edge anchor of a battle HUD label (-1 left, +1 right, 0 none).</summary>
        public int wideAnchor;
        /// <summary>Re-place after WideView.ExtraHalf changed: glyphs are children at absolute UI coordinates, so only the label object moves.</summary>
        /// <summary>The x the text is aligned to (left edge / centre / right edge of the rect): WideView anchors a label by this, not by the rect centre (a left-aligned
        /// HUD label such as the cash counter has a 400 px rect that reaches the middle third).</summary>
        public float AlignX
        {
            get
            {
                int h = nfont != null ? nH : (int)tm.anchor % 3;
                return h == 0 ? rect.x : h == 2 ? rect.xMax : rect.x + rect.width * 0.5f;
            }
        }
        public void ReapplyWide() { if (nfont != null) transform.localPosition = new Vector3(WideView.Shift(wideAnchor), 0f, 0f); else Place(); }   // native SetAlignment(1,2) = right/centre in DC2 usage (PILOT: mapped below)

        // ---- native text mode (Triniti2D::UILabel::UpdateText 0x180040): glyph sprites placed from the baked FreeType font tables; default alignment (0,0) = top-left, no wrap (overflow truncates) ----
        NativeFont nfont; float charSpacing, lineSpacing, scale = 1f; int nH, nV; Color32 ncolor = new Color32(255, 255, 255, 255); string ntext = "";
        readonly List<SpriteRenderer> glyphs = new List<SpriteRenderer>();
        public bool IsNative { get { return nfont != null; } }
        public int GlyphCount { get { return glyphs.Count; } }
        public Rect GlyphRect(int i) { var r = glyphs[i]; return new Rect(r.transform.localPosition.x - r.transform.localScale.x * r.sprite.rect.width * 0.5f, -r.transform.localPosition.y - r.transform.localScale.y * r.sprite.rect.height * 0.5f, r.transform.localScale.x * r.sprite.rect.width, r.transform.localScale.y * r.sprite.rect.height); }
        /// <summary>UILabel::SetFont + SetCharacterSpacing + SetLineSpacing + SetAlignment(h, v) (0 left/top, 1 centre, 2 right/bottom, 3 justify horizontally).</summary>
        public void SetNative(string fontName, float charSp, float lineSp, int h, int v)
        {
            if (nfont == null && tm.text.Length > 0) ntext = tm.text;   // text set before the native font was chosen
            nfont = NativeFont.Get(fontName); charSpacing = charSp; lineSpacing = lineSp; nH = h; nV = v;
            tm.text = ""; mr.enabled = false; UpdateNative(); fontAtlasDirty = true;
        }

        public static UiLabel Create(Dc2Scene scene, int id, int layer, Font font, int pixelSize)
        {
            var go = new GameObject("label" + id);
            go.transform.SetParent(scene.transform, false);
            var l = go.AddComponent<UiLabel>();
            l.id = id;
            l.tm = go.AddComponent<TextMesh>();
            l.tm.font = font;
            l.tm.fontSize = pixelSize * 4;
            l.tm.characterSize = 2.5f;   // TextMesh world height = fontSize * characterSize * 0.1
            l.tm.anchor = TextAnchor.MiddleLeft;   // native label rects are vertically centred on the art rows (LV/HP rows verified in render)
            l.mr = go.GetComponent<MeshRenderer>();
            l.mr.sharedMaterial = font.material;
            l.mr.sortingOrder = scene.RegisterUi(l, layer);
            return l;
        }

        public void SetUiOrder(int order) { mr.sortingOrder = order; foreach (var g in glyphs) if (g != null) g.sortingOrder = order; }
        public int UiOrderForTest { get { return mr.sortingOrder; } }   // DEBUG_ONLY verifier probe (R002-N2)
        /// <summary>UIControl vtbl+0x24 SetVisible on a label: hides the text without deleting it (glyphs built later stay hidden).</summary>
        public bool Visible { get; private set; } = true;
        public void SetVisible(bool v) { Visible = v; if (nfont == null) mr.enabled = v; foreach (var g in glyphs) if (g != null) g.enabled = v; }

        public void SetText(string s)
        {
            if (nfont != null) { ntext = s ?? ""; UpdateNative(); return; }
            if (tm.font != null) tm.font.RequestCharactersInTexture(s, tm.fontSize, tm.fontStyle);
            if (tm.text != s) fontAtlasDirty = true;
            tm.text = s;
        }
        public void SetColor(Color32 c) { tm.color = c; ncolor = c; foreach (var g in glyphs) g.color = c; }

        public void SetRect(Rect r)
        {
            rect = r;
            if (nfont != null) { UpdateNative(); return; }
            Place();
        }

        public void SetAlignment(TextAnchor a)
        {
            if (nfont != null)
            {   // native mode: map the anchor to UILabel::SetAlignment(h, v) (0 left/top, 1 centre/middle, 2 right/bottom)
                int col = (int)a % 3, row = (int)a / 3; nH = col; nV = row; UpdateNative(); return;
            }
            tm.anchor = a; Place();
        }

        void Place()
        {
            float x = rect.x, y = rect.y;
            switch (tm.anchor)
            {
                case TextAnchor.UpperCenter: case TextAnchor.MiddleCenter: case TextAnchor.LowerCenter: x = rect.x + rect.width * 0.5f; break;
                case TextAnchor.UpperRight: case TextAnchor.MiddleRight: case TextAnchor.LowerRight: x = rect.x + rect.width; break;
            }
            switch (tm.anchor)
            {
                case TextAnchor.MiddleLeft: case TextAnchor.MiddleCenter: case TextAnchor.MiddleRight: y = rect.y + rect.height * 0.5f; break;
                case TextAnchor.LowerLeft: case TextAnchor.LowerCenter: case TextAnchor.LowerRight: y = rect.y + rect.height; break;
            }
            transform.localPosition = new Vector3(x + WideView.Shift(wideAnchor), -y, 0f);
        }

        public string Text { get { return nfont != null ? ntext : tm.text; } }
        public Rect CurrentRect { get { return rect; } }

        /// <summary>UILabel::UpdateText 0x180040. Per glyph: size = (int)(w*S) x (int)(h*S); centre x = rectX + pen + xoff*S + w/2; centre y = lineY + rectY - yoff*S - h/2 (y down), both truncated to integers (non-retina font);
        /// pen += (adv + charSpacing)*S; a glyph that would pass the rect width truncates the text (wrap flag +96 is 0); lines advance by (lineHeight + lineSpacing)*S; horizontal alignment 1 / 2 shifts by
        /// (int)(w - widest line) halved / full, 3 centres each line; vertical alignment 1 / 2 shifts by (int)(h - total height) halved / full.</summary>
        void UpdateNative()
        {
            transform.localPosition = new Vector3(WideView.Shift(wideAnchor), 0f, 0f);   // ADAPTATION WideView shift (0 = native); glyph positions are absolute UI coordinates (the legacy Place() may have moved the label object)
            // edit-mode verifiers have no frame end, so a deferred Destroy left every earlier glyph layout in the captures (ghost trail of a sliding label)
            foreach (var g in glyphs) if (g != null) { if (Application.isPlaying) Destroy(g.gameObject); else DestroyImmediate(g.gameObject); }
            glyphs.Clear();
            if (nfont == null || ntext.Length == 0) return;
            float lineH = nfont.LineHeight * scale;
            if (lineH > rect.height) return;                                     // first line must fit the rect height
            float penX = 0f, lineY = lineH, maxW = 0f;
            var lineWidths = new List<float>(); var glyphLine = new List<int>(); var items = new List<Vector4>(); var itemChars = new List<NativeFontChar>();
            for (int i = 0; i < ntext.Length; i++)
            {
                int ch = ntext[i];
                if (ch == 10 || ch == 13)
                {
                    if (maxW < penX) maxW = penX; lineWidths.Add(penX); penX = 0f; lineY += (nfont.LineHeight + lineSpacing) * scale;
                    if (lineY > rect.height) break; continue;
                }
                NativeFontChar f; if (!nfont.chars.TryGetValue(ch, out f)) break;   // a missing glyph ends the text
                if (penX + f.adv > rect.width) break;                              // no wrap: truncate
                float gw = (int)(f.w * scale), gh = (int)(f.h * scale);
                float cx = rect.x + penX + f.xoff * scale + 0.5f * gw;
                float cy = lineY + rect.y - f.yoff * scale - 0.5f * gh;
                cx = (int)cx; cy = (int)cy;                                         // retina flag false: whole pixels
                items.Add(new Vector4(cx, cy, gw, gh)); itemChars.Add(f); glyphLine.Add(lineWidths.Count);
                penX += (f.adv + charSpacing) * scale;
            }
            if (maxW < penX) maxW = penX; lineWidths.Add(penX);
            float totalH = lineY;
            float dxFull = (int)(rect.width - maxW), dxHalf = (int)(dxFull * 0.5f);
            float dyFull = (int)(rect.height - totalH), dyHalf = (int)(dyFull * 0.5f);
            for (int i = 0; i < items.Count; i++)
            {
                float x = items[i].x, y = items[i].y;
                if (nH == 1) x += dxHalf; else if (nH == 2) x += dxFull; else if (nH == 3) x += (int)((rect.width - lineWidths[glyphLine[i]]) * 0.5f);
                if (nV == 1) y += dyHalf; else if (nV == 2) y += dyFull;
                var spr = nfont.SpriteFor(itemChars[i]); if (spr == null) continue;
                var go = new GameObject("glyph"); go.transform.SetParent(transform, false);
                go.transform.localPosition = new Vector3(x, -y, 0f);
                var sr = go.AddComponent<SpriteRenderer>(); sr.sprite = spr; sr.color = ncolor; sr.sortingOrder = mr.sortingOrder; sr.enabled = Visible;
                go.transform.localScale = new Vector3(items[i].z / itemChars[i].w, items[i].w / itemChars[i].h, 1f);
                glyphs.Add(sr);
            }
        }

        // UILabelMoveCtrl tween (same phase logic as UiControl; colour not animated)
        MoveData start, mid, end;
        float d1, d2, t;
        int phase;
        bool active;

        // PLATFORM_ADAPTER: Unity dynamic font atlases can rebuild when another label requests
        // glyphs. Rebuild this TextMesh's UVs against the current host atlas; no gameplay clock
        // is introduced. Native labels use UiLabel.SetNative (UILabel::UpdateText + baked FreeType fonts); this legacy TextMesh path only serves labels without a native counterpart (abandon dialog 900-902, PILOT).
        static readonly List<UiLabel> renderLabels = new List<UiLabel>();
        // r119x perf: the full glyph request + UV refresh ran before every camera cull (a new Dictionary with boxed Tuple keys, string concatenation and a
        // forced TextMesh rebuild of every label each frame: ~30 % of battle garbage). It now runs only after a TextMesh text / font / label-set change;
        // atlas rebuilds caused elsewhere still reach every label through Font.textureRebuilt (FontRebuilt), as before.
        static bool fontAtlasDirty = true;
        static readonly Dictionary<(Font, int, FontStyle), string> fontRequests = new Dictionary<(Font, int, FontStyle), string>();
        void OnEnable()
        {
            fontAtlasDirty = true;
            Font.textureRebuilt += FontRebuilt;
            if(renderLabels.Count==0)Camera.onPreCull+=PrepareFontAtlas;
            renderLabels.Add(this);
        }
        void OnDisable()
        {
            Font.textureRebuilt -= FontRebuilt;renderLabels.Remove(this); fontAtlasDirty = true;
            if(renderLabels.Count==0)Camera.onPreCull-=PrepareFontAtlas;
        }
        static void PrepareFontAtlas(Camera camera)
        {
            // Request the complete active glyph set before rebuilding UVs. Per-label requests
            // can evict another label's glyphs in manual editor renders or after enable/disable.
            if(!fontAtlasDirty)return;
            fontAtlasDirty=false;
            var requests=fontRequests; requests.Clear();
            foreach(var l in renderLabels)
            {
                if(l.tm==null || l.tm.font==null || string.IsNullOrEmpty(l.tm.text))continue;
                var key=(l.tm.font,l.tm.fontSize,l.tm.fontStyle);
                requests.TryGetValue(key,out string value);requests[key]=value+l.tm.text;
            }
            foreach(var p in requests)p.Key.Item1.RequestCharactersInTexture(p.Value,p.Key.Item2,p.Key.Item3);
            foreach(var l in renderLabels)if(l.tm!=null)l.FontRebuilt(l.tm.font);
        }
        void FontRebuilt(Font changed)
        {
            if (tm == null || changed != tm.font) return;
            string value = tm.text;
            tm.text = ""; tm.text = value;
            mr.sharedMaterial = changed.material;
        }
        public bool IsMoving { get { return active; } }

        public void StartMove(MoveData s, MoveData m, MoveData e, float dur1, float dur2)
        {
            start = s; mid = m; end = e; d1 = dur1; d2 = dur2; t = 0f; phase = 1; active = true;
        }

        public void UpdateMove(float dt)
        {
            if (!active) return;
            t += dt;
            float r; MoveData from, to;
            if (phase == 1)
            {
                r = t / d1;
                if (r >= 1f) { phase = 2; r = 1f; t -= d1; }
                from = start; to = mid;
            }
            else
            {
                r = t / d2;
                if (r >= 1f) { r = 1f; active = false; }
                from = mid; to = end;
            }
            float cx = Mathf.LerpUnclamped(from.cx, to.cx, r), cy = Mathf.LerpUnclamped(from.cy, to.cy, r);
            float w = Mathf.LerpUnclamped(from.w, to.w, r), h = Mathf.LerpUnclamped(from.h, to.h, r);
            SetRect(new Rect(cx - w * 0.5f, cy - h * 0.5f, w, h));
        }
    }
}
