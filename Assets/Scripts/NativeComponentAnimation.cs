using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace Dc2Pilot
{
    /// <summary>Shipped ComponentAnimation binary grammar. See Dev/Tools/acb_ir.py:
    /// the loading ACB and library have lossless neutral IR / byte-exact round trips.
    /// </summary>
    public sealed class NativeComponentAnimation
    {
        public sealed class Frame
        {
            public float number, x, y, shearX, shearY, scaleX, scaleY, radians;
            public uint rgba;
            public int layer;
        }
        public sealed class Component { public string name, avatar; public Frame[] frames; public NativeCaLibraryAnim libAnim; public string Key { get { return name + avatar; } } }
        public Component[] components;
        /// <summary>ComponentAnimation::Load 0x11d3fe keeps every (component, avatar) pair keyed by name+avatar; the first avatar is the default selection per component name.</summary>
        public readonly Dictionary<string, Component> byKey = new Dictionary<string, Component>();
        public readonly Dictionary<string, string> defaultSelection = new Dictionary<string, string>();
        public bool advNormalized;                  // UpdateSpriteDrawOrderAdv 0x118036 rewrites the frames relative to frame 0 (idempotent)
        public int FrameCount { get; private set; }
        public float MaxNumber { get; private set; }
        /// <summary>ComponentAnimation+0x58: computed at load, but code may overwrite it (mainScene::OnLoad 0xaac48 sets g_ca_egg_che_stand+0x58 = 0.5). &lt; 0 = not overwritten.</summary>
        public float frameRateOverride = -1f;
        public float FrameRateFactor { get { return frameRateOverride >= 0f ? frameRateOverride : FrameCount == 0 ? 1f : (float)(FrameCount - 1) / MaxNumber; } }

        public static NativeComponentAnimation Load(string resource)
        {
            var asset = Resources.Load<TextAsset>(resource);
            if (asset == null) throw new Exception("Missing native component animation " + resource);
            using (var stream = new MemoryStream(asset.bytes, false))
            using (var r = new BinaryReader(stream))
            {
                uint version = r.ReadUInt32();
                if (version != 1002) throw new Exception("Unrecovered component version " + version);
                ReadString(r); // library identity; loading defaults use static frame references.
                var result = new NativeComponentAnimation { components = new Component[r.ReadUInt32()] };
                for (int i = 0; i < result.components.Length; i++)
                {
                    string name = ReadString(r);
                    int count = r.ReadInt32();
                    string avatar = ReadString(r);
                    var component = new Component { name = name, avatar = avatar, frames = ReadFrames(r, count) };
                    result.components[i] = component;
                    result.byKey[component.Key] = component; result.defaultSelection[name] = component.Key;
                    int alternatives = r.ReadInt32();
                    for (int j = 0; j < alternatives; j++)
                    {
                        string altAvatar = ReadString(r);
                        var alt = new Component { name = name, avatar = altAvatar, frames = ReadFrames(r, count) };
                        result.byKey[alt.Key] = alt;
                    }
                    result.FrameCount = Math.Max(result.FrameCount, count);
                    foreach (var frame in component.frames) if (frame != null) result.MaxNumber = Math.Max(result.MaxNumber, frame.number);
                }
                if (stream.Position != stream.Length) throw new Exception("Trailing component bytes " + resource);
                return result;
            }
        }

        static Frame[] ReadFrames(BinaryReader r, int count)
        {
            var result = new Frame[count];
            for (int i = 0; i < count; i++)
            {
                float number = r.ReadSingle();
                if (!(number >= 0f)) continue;
                result[i] = new Frame { number = number, x = r.ReadSingle(), y = r.ReadSingle(),
                    shearX = r.ReadSingle(), shearY = r.ReadSingle(), scaleX = r.ReadSingle(), scaleY = r.ReadSingle(),
                    radians = r.ReadSingle(), rgba = r.ReadUInt32(), layer = r.ReadInt32() };
            }
            return result;
        }

        static string ReadString(BinaryReader r)
        {
            var bytes = new List<byte>(); byte b;
            while ((b = r.ReadByte()) != 0) bytes.Add(b);
            return Encoding.GetEncoding(28591).GetString(bytes.ToArray());
        }
    }

    /// <summary>Pure affine render vessel for native Sprite::GetPointer, including shear.
    /// Sprite vertices/UVs are built from native dimensions and atlas metadata only.
    /// </summary>
    public sealed class NativeAffineSprite : MonoBehaviour
    {
        Mesh mesh;
        MeshRenderer rendererComponent;
        Material material;
        Texture texture;
        readonly Vector3[] vertices = new Vector3[4];
        readonly Vector2[] uv = new Vector2[4];
        readonly Color32[] colors = new Color32[4];

        public static NativeAffineSprite Create(Transform parent)
        {
            var go = new GameObject("native_component_part"); go.transform.SetParent(parent, false);
            var result = go.AddComponent<NativeAffineSprite>();
            result.mesh = new Mesh { name = "native_affine_quad" };
            go.AddComponent<MeshFilter>().sharedMesh = result.mesh;
            result.rendererComponent = go.AddComponent<MeshRenderer>();
            return result;
        }

        /// <summary>flipX = CompAnimObj+223 mirror (OnUpdateSprite 0x118c8c): the caller negates frame.x; rotation and shearX are negated here and the texture is mirrored.</summary>
        public void Draw(string identity, NativeComponentAnimation.Frame frame, Vector2 position, int order, float objectScale = 1f, bool flipX = false, Color32? colorOverride = null)
        {
            var sprite = Atlas.Get(identity);
            if (texture != sprite.texture)
            {
                if (material != null) DestroyOwned(material);
                texture = sprite.texture;
                material = new Material(Shader.Find("Sprites/Default")) { mainTexture = texture };
                rendererComponent.sharedMaterial = material;
            }
            float w = sprite.rect.width * (frame.scaleX * objectScale), h = sprite.rect.height * (frame.scaleY * objectScale);
            float radians = flipX ? -frame.radians : frame.radians, shearX = flipX ? -frame.shearX : frame.shearX;
            float c = Mathf.Cos(radians), s = Mathf.Sin(radians);
            var color = new Color32((byte)frame.rgba, (byte)(frame.rgba >> 8), (byte)(frame.rgba >> 16), (byte)(frame.rgba >> 24));
            if (colorOverride.HasValue) color = colorOverride.Value;   // CompAnimObj::SetChangeSpriteColor replaces the colour of sprites with that component name
            for (int i = 0; i < 4; i++)
            {
                float x = (i == 0 || i == 3 ? -0.5f : 0.5f) * w;
                float y = (i < 2 ? 0.5f : -0.5f) * h;
                float hx = x + shearX * y, hy = frame.shearY * x + y;
                vertices[i] = new Vector3(position.x + c * hx - s * hy, position.y + s * hx + c * hy, 0);
                colors[i] = color;
            }
            Rect r = sprite.rect;
            float left = r.x / texture.width, right = (r.x + r.width) / texture.width;
            float bottom = r.y / texture.height, top = (r.y + r.height) / texture.height;
            if (flipX) { float t = left; left = right; right = t; }
            uv[0] = new Vector2(left, top); uv[1] = new Vector2(right, top);
            uv[2] = new Vector2(right, bottom); uv[3] = new Vector2(left, bottom);
            mesh.vertices = vertices; mesh.uv = uv; mesh.colors32 = colors;
            mesh.triangles = new[] { 0, 1, 2, 0, 2, 3 }; mesh.RecalculateBounds();
            rendererComponent.sortingOrder = order; rendererComponent.enabled = true;
        }

        public void Hide() { rendererComponent.enabled = false; }
        static void DestroyOwned(UnityEngine.Object value)
        { if (Application.isPlaying) Destroy(value); else DestroyImmediate(value); }
        void OnDestroy() { if (mesh != null) DestroyOwned(mesh); if (material != null) DestroyOwned(material); }
    }

    /// <summary>CompAnimObj's distinct timing: subtract interval in a loop, then emit.
    /// Loading scope is base avatars at scale=1, rotation=0, white tint, infinite looping.
    /// </summary>
    public sealed class NativeComponentPlayer
    {
        public NativeComponentAnimation animation;
        public Vector2 position;
        public float objectScale = 1f; // CompAnimObj+228 scales local positions and sprite scale; ARM0x118c3a..0x118c86.
        public bool oneShot;                    // jigsaw effect: plays once and reports Finished instead of looping
        public bool Finished { get; private set; }
        public int CurrentFrame { get; private set; }
        public float Accumulator { get; private set; }
        readonly List<NativeAffineSprite> sprites = new List<NativeAffineSprite>();
        public readonly Dictionary<string, NativeCompObj.SpriteInfo> Sprites = new Dictionary<string, NativeCompObj.SpriteInfo>();   // CompAnimObj::GetSprite: centre (relative to the screen centre), size, rotation
        readonly Transform parent;
        float interval;

        public NativeComponentPlayer(NativeComponentAnimation animation, Transform parent, Vector2 position, float objectScale = 1f)
        {
            this.animation = animation; this.parent = parent; this.position = position;
            this.objectScale = objectScale;
            interval = 1f / (30f * animation.FrameRateFactor); // InitPlayer passes 30 fps.
            Render(); // PlayAnimation(..., updateSprite=true) emits initial pose immediately.
        }

        // ---- CompAnimObj::PlayAnimation 0x119010 / OnUpdateFrameNumber 0x11835c (bossN and other CompAnimObj users) ----
        public readonly Dictionary<string, Color32> colorOverrides = new Dictionary<string, Color32>();   // CompAnimObj::SetChangeSpriteColor 0x117f54 (name -> colour)
        public void SetChangeSpriteColor(string name, Color32 color) { colorOverrides[name] = color; Render(); }
        public bool flipX;              // CompAnimObj+223 horizontal mirror (OnUpdateSprite 0x118c8c)
        public int loops = -1;          // CompAnimObj+196: <=1 plays once and raises the end flag, >1 counts down, -1 loops forever
        public bool Fin { get; private set; }   // CompAnimObj+220
        int endFrame;

        /// <summary>CompAnimObj::SetAnimation: swaps the ComponentAnimation (PlayAnimation follows).</summary>
        public void SetAnimation(NativeComponentAnimation next) { animation = next; }

        /// <summary>CompAnimObj::PlayAnimation(loops, 30.0, 0, -1, true): interval=1/(30*FrameRateFactor), frame 0, end flag cleared, initial pose emitted.</summary>
        public void Play(int loopCount)
        {
            loops = loopCount;
            interval = 1f / (30f * animation.FrameRateFactor);
            Fin = false; Finished = false;
            endFrame = animation.FrameCount - 1;
            CurrentFrame = 0; Accumulator = 0f;
            Render();
        }

        /// <summary>CompAnimObj::GetCurAnimationNumber 0x11826a: frame number of the first component that has the current frame, truncated (fixsfsi).</summary>
        public int CurAnimationNumber
        {
            get
            {
                foreach (var component in animation.components)
                    if (CurrentFrame < component.frames.Length && component.frames[CurrentFrame] != null) return (int)component.frames[CurrentFrame].number;
                return 0;
            }
        }

        /// <summary>CompAnimObj::OnUpdate: OnUpdateFrameNumber (accumulate, advance, end/loop handling) then OnUpdateSprite.</summary>
        public void StepNative(float dt, Action onEnd = null)
        {
            Accumulator += dt;
            while (Accumulator >= interval)
            {
                int old = CurrentFrame;
                CurrentFrame = old + 1;
                Accumulator -= interval;
                if (CurrentFrame > endFrame)
                {
                    if (unchecked((uint)loops) <= 1u)
                    {
                        CurrentFrame = endFrame;
                        if (!Fin) { Fin = true; if (onEnd != null) onEnd(); }
                    }
                    else
                    {
                        CurrentFrame = old - endFrame;      // start frame is 0
                        if (loops != -1) loops--;
                    }
                }
            }
            Render();
        }

        public void SetParentVisible(bool visible) { if (!visible) foreach (var s in sprites) s.Hide(); }
        public void Hide() { foreach (var s in sprites) s.Hide(); }
        public void Destroy() { foreach (var s in sprites) if (s != null) UnityEngine.Object.Destroy(s.gameObject); sprites.Clear(); }

        public void Step(float dt)
        {
            if (Finished) return;
            Accumulator += dt;
            while (Accumulator >= interval)
            {
                CurrentFrame++;
                Accumulator -= interval;
                if (CurrentFrame >= animation.FrameCount)
                {
                    if (oneShot) { Finished = true; Accumulator = 0f; CurrentFrame = animation.FrameCount - 1; return; }
                    CurrentFrame -= animation.FrameCount;
                }
            }
            Render();
        }

        void Render()
        {
            var components = new List<NativeComponentAnimation.Component>();
            foreach (var component in animation.components)
                if (CurrentFrame < component.frames.Length && component.frames[CurrentFrame] != null) components.Add(component);
            // SpriteSortLayer: signed layer, then ordinal component-name comparison.
            components.Sort((a, b) => a.frames[CurrentFrame].layer != b.frames[CurrentFrame].layer
                ? a.frames[CurrentFrame].layer.CompareTo(b.frames[CurrentFrame].layer) : string.CompareOrdinal(a.name, b.name));
            int count = 0;
            foreach (var component in components)
            {
                if (component.avatar.Length == 0) continue; // unresolved emptyFrame ids are not rendered natively.
                string identity = NativeReferenceSuffix.FrameName(component.avatar);
                // The shipped loading base avatars are static, not library animations.
                if (!Atlas.Has(identity)) throw new Exception("Unresolved loading component " + identity);
                while (sprites.Count <= count) sprites.Add(NativeAffineSprite.Create(parent));
                var frame = component.frames[CurrentFrame];
                sprites[count].Draw(identity, frame,
                    new Vector2(N.ScreenW * 0.5f + position.x + (flipX ? -frame.x : frame.x) * objectScale, -N.ScreenH * 0.5f + position.y + frame.y * objectScale), count, objectScale, flipX,
                    colorOverrides.TryGetValue(component.name, out Color32 over) ? over : (Color32?)null);
                var rect = Atlas.Get(identity).rect;
                Sprites[component.name] = new NativeCompObj.SpriteInfo { x = position.x + (flipX ? -frame.x : frame.x) * objectScale, y = position.y + frame.y * objectScale, w = rect.width * frame.scaleX * objectScale, h = rect.height * frame.scaleY * objectScale, rot = flipX ? -frame.radians : frame.radians };
                count++;
            }
            for (int i = count; i < sprites.Count; i++) sprites[i].Hide();
        }
    }
}
