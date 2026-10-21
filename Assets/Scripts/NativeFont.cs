using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dc2Pilot
{
    [Serializable] public sealed class NativeFontChar { public int c, w, h, xoff, adv, yoff, ax, ay; }
    [Serializable] public sealed class NativeFontDef { public string name, path, atlas; public int size, atlasW, atlasH, lineHeight; public float ppem; public NativeFontChar[] chars; }
    [Serializable] sealed class NativeFontTable { public NativeFontDef[] fonts; }

    /// <summary>
    /// Triniti2D font as built by FontManager::CreateFont 0x16778c / LoadFreeTypeFont 0x1694c4 (baked offline by Dev/Tools/bake_fonts.py: FT_Set_Char_Size(size*64, size*64, 96, 96),
    /// FT_Load_Glyph(0) + FT_Render_Glyph(0) for codes 0..127, bitmaps padded to even size, xoff = (horiBearingX>>6) - width%2, adv = horiAdvance>>6, yoff = (horiBearingY>>6) - paddedRows,
    /// line height = vertAdvance>>6 of the last glyph). iPad sizes (LoadImageHight 0x6f89c): HARLOWSI-16 30, Adventure_16 30, Adventure_22 44, Adventure_12 24, Adventure_33 46.
    /// Provenance NATIVE except the FreeType build: the game ships its own FreeType (2.3/2.4 era, v35 interpreter forced in the bake); a different hinting build could shift a glyph by one pixel (LIKELY none).
    /// </summary>
    public sealed class NativeFont
    {
        public readonly NativeFontDef def;
        public readonly Dictionary<int, NativeFontChar> chars = new Dictionary<int, NativeFontChar>();
        readonly Dictionary<int, Sprite> sprites = new Dictionary<int, Sprite>();
        Texture2D atlas;
        static Dictionary<string, NativeFont> cache;

        NativeFont(NativeFontDef d) { def = d; foreach (var c in d.chars) chars[c.c] = c; }

        public static NativeFont Get(string name)
        {
            if (cache == null)
            {
                cache = new Dictionary<string, NativeFont>();
                var table = JsonUtility.FromJson<NativeFontTable>(Resources.Load<TextAsset>("DC2/Font/native_fonts.json").text);
                foreach (var d in table.fonts) cache[d.name] = new NativeFont(d);
            }
            NativeFont f; return cache.TryGetValue(name, out f) ? f : null;
        }

        public float LineHeight { get { return def.lineHeight; } }

        public Sprite SpriteFor(NativeFontChar c)
        {
            Sprite s;
            if (sprites.TryGetValue(c.c, out s)) return s;
            if (atlas == null) atlas = Resources.Load<Texture2D>("DC2/Font/" + def.atlas);
            if (c.w == 0 || c.h == 0 || atlas == null) { sprites[c.c] = null; return null; }
            // atlas rows are stored top-down; the Unity texture origin is bottom-left
            s = Sprite.Create(atlas, new Rect(c.ax, def.atlasH - c.ay - c.h, c.w, c.h), new Vector2(0.5f, 0.5f), 1f, 0, SpriteMeshType.FullRect);
            sprites[c.c] = s; return s;
        }
    }
}
