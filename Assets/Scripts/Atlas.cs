using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace Dc2Pilot
{
    /// <summary>
    /// Frame registry equivalent to Triniti2D::TextureManager::CreateFrame plus DinoCap::loadImageByConf.
    /// Frame names are "<name>Frame" exactly as the native loader composes them.
    /// </summary>
    public static class Atlas
    {
        [Serializable] class NativeTable { public NativeFrame[] frames; }
        [Serializable] class NativeFrame
        {
            public string name, texture, nativeCall;
            public int x, y, w, h;
        }
        struct FrameDef { public Texture2D tex; public RectInt rect; }

        static readonly Dictionary<string, FrameDef> frames = new Dictionary<string, FrameDef>();
        static readonly Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
        static readonly Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

        public static void Reset()
        {
            frames.Clear(); sprites.Clear(); textures.Clear();
        }

        /// <summary>SOURCE_FAITHFUL iPad registrations recovered from LoadImageHight ARM.
        /// Dev/Reports/DC2-PILOT-001/native_frame_table.r3.json records every call and raw float word.
        /// Resource generation: Dev/Tools/prepare_loading_assets.py, no pixel-derived rectangles.
        /// </summary>
        public static void LoadNativeTable()
        {
            LoadNativeTable("DC2/native_frames.json");
            // Retained native loader-call manifest, Dev/Recovered/native_frames_game.json.
            // Registration order preserves prior definitions, as loadImageByConf does.
            LoadNativeTable("DC2/native_frames_game.json");
            // loadDinoBMP 0x3b8ac per-surface atlases recovered by emulation (Dev/Recovered/dino_textures_ipad.json)
            LoadNativeTable("DC2/native_frames_dino.json");
            LoadNativeTable("DC2/native_frames_background.json");
            LoadByConf("DC2/Image/ip/body_face","DC2/Image/ip/body_face.conf"); // Native iPad loader73b1e, twelve exact facial frames r92.
        }

        static void LoadNativeTable(string resource)
        {
            var data = Resources.Load<TextAsset>(resource);
            if (data == null) throw new Exception("Native frame resource is missing; run prepare_loading_assets.py");
            foreach (var f in JsonUtility.FromJson<NativeTable>(data.text).frames)
                if (!Has(f.name)) CreateFrame(f.name, LoadTexture(f.texture), f.x, f.y, f.w, f.h);

        }

        public static void LoadArmoryTable() { LoadNativeTable("DC2/native_frames_armory.json"); }

        public static Texture2D LoadTexture(string resourcePath)
        {
            Texture2D t;
            if (textures.TryGetValue(resourcePath, out t)) return t;
            t = Resources.Load<Texture2D>(resourcePath);
            if (t == null) throw new Exception("Texture not found in Resources: " + resourcePath);
            t.wrapMode = TextureWrapMode.Repeat;   // r118z4: native GL_REPEAT (TextureManager::LoadFromPng 0x1782f0)
            textures[resourcePath] = t;
            return t;
        }

        /// <summary>TextureManager::CreateFrame(frameName, image, Rect). Rect is top-left origin (x,y,w,h) in texture pixels.</summary>
        public static void CreateFrame(string frameName, Texture2D tex, int x, int y, int w, int h)
        {
            frames[frameName] = new FrameDef { tex = tex, rect = new RectInt(x, y, w, h) };
            sprites.Remove(frameName);
        }

        /// <summary>DinoCap::loadImageByConf(path): "[Frame]\nCount = N\nFrameK = (name x y w h)" lines, ints via atoi.</summary>
        public static int LoadByConf(string imageResourcePath, string confResourcePath)
        {
            var tex = LoadTexture(imageResourcePath);
            var ta = Resources.Load<TextAsset>(confResourcePath);
            if (ta == null) throw new Exception("Conf not found in Resources: " + confResourcePath);
            return ParseConf(ta.text, tex);
        }

        public static int ParseConf(string text, Texture2D tex)
        {
            int created = 0;
            foreach (var raw in text.Split('\n'))
            {
                string line = raw.Trim('\r', ' ', '\t');
                int a = line.IndexOf('(');
                int b = line.LastIndexOf(')');
                if (a < 0 || b <= a) continue;
                string body = line.Substring(a + 1, b - a - 1);
                string[] parts = body.Split(' ');
                if (parts.Length < 5) continue;
                // DinoCap::CreateFrame@0x174de returns without registering when the name exists.
                // Player2x.conf contains duplicate newplayer_chui2; preserve the first definition.
                string name = parts[0] + "Frame";
                if (!Has(name))
                {
                    CreateFrame(name, tex, Atoi(parts[1]), Atoi(parts[2]), Atoi(parts[3]), Atoi(parts[4]));
                    created++;
                }
            }
            return created;
        }

        static int Atoi(string s)
        {
            int v;
            return int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out v) ? v : 0;
        }

        public static bool Has(string frameName) { return frames.ContainsKey(frameName); }

        public static Vector2Int FrameSize(string frameName)
        {
            FrameDef d;
            if (!frames.TryGetValue(frameName, out d)) throw new Exception("Unknown frame: " + frameName);
            return new Vector2Int(d.rect.width, d.rect.height);
        }

        /// <summary>Sprite with centre pivot, 1 unit == 1 pixel.</summary>
        public static Sprite Get(string frameName)
        {
            Sprite s;
            if (sprites.TryGetValue(frameName, out s)) return s;
            FrameDef d;
            if (!frames.TryGetValue(frameName, out d)) throw new Exception("Unknown frame: " + frameName);
            var r = new Rect(d.rect.x, d.tex.height - d.rect.y - d.rect.height, d.rect.width, d.rect.height);
            s = Sprite.Create(d.tex, r, new Vector2(0.5f, 0.5f), 1f, 0, SpriteMeshType.FullRect);
            s.name = frameName;
            sprites[frameName] = s;
            return s;
        }

        static Texture2D white;
        /// <summary>The native "whiteFrame" (solid white, tinted by SetColor).</summary>
        public static void EnsureWhite()
        {
            if (Has("whiteFrame")) return;
            white = new Texture2D(4, 4, TextureFormat.RGBA32, false);
            var px = new Color32[16];
            for (int i = 0; i < px.Length; i++) px[i] = new Color32(255, 255, 255, 255);
            white.SetPixels32(px); white.Apply(false, false);
            CreateFrame("whiteFrame", white, 0, 0, 4, 4);
        }
    }
}
