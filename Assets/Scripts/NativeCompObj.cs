using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace Dc2Pilot
{
    /// <summary>CALibrary::Anim (Player.library.lcb): numbered texture frames; GetFrameId 0x1162ee returns the last frame with number &lt;= t, looping when the loop byte is set.</summary>
    public sealed class NativeCaLibraryAnim
    {
        public bool loop; public int[] numbers; public string[] refs;
        static Dictionary<string, Dictionary<string, NativeCaLibraryAnim>> cache = new Dictionary<string, Dictionary<string, NativeCaLibraryAnim>>();

        public static NativeCaLibraryAnim Get(string resource, string name)
        {
            Dictionary<string, NativeCaLibraryAnim> lib;
            if (!cache.TryGetValue(resource, out lib))
            {
                lib = new Dictionary<string, NativeCaLibraryAnim>();
                var asset = Resources.Load<TextAsset>(resource);
                if (asset == null) throw new Exception("Missing native CALibrary " + resource);
                using (var r = new BinaryReader(new MemoryStream(asset.bytes, false)))
                {
                    uint version = r.ReadUInt32(); ReadString(r);
                    uint count = r.ReadUInt32();
                    for (uint i = 0; i < count; i++)
                    {
                        string n = ReadString(r); uint frames = r.ReadUInt32();
                        var a = new NativeCaLibraryAnim { numbers = new int[frames], refs = new string[frames] };
                        for (uint f = 0; f < frames; f++) { a.numbers[f] = r.ReadInt32(); a.refs[f] = ReadString(r); }
                        if (version > 1001) a.loop = r.ReadByte() != 0;
                        lib[n] = a;
                    }
                }
                cache[resource] = lib;
            }
            NativeCaLibraryAnim result; lib.TryGetValue(name, out result); return result;
        }

        static string ReadString(BinaryReader r)
        {
            var bytes = new List<byte>(); byte b;
            while ((b = r.ReadByte()) != 0) bytes.Add(b);
            return Encoding.GetEncoding(28591).GetString(bytes.ToArray());
        }

        /// <summary>Returns the texture frame reference name for t, or null (native -1) when t is before the first frame.</summary>
        public string FrameRef(float t)
        {
            if (numbers.Length == 0 || numbers[0] > t) return null;
            int last = numbers[numbers.Length - 1];
            // GetFrameId 0x1162ee: while (t > (float)last) t = t - (float)(last + 1 - first) in binary32; span >= 1 always (the port's old 1000-step guard was invented, r118z L4)
            if (loop) { float span = N.F32(last + 1 - numbers[0]), fl = N.F32(last); while (t > fl) t = N.Add32(t, -span); }
            string prev = refs[0];
            for (int i = 0; i < numbers.Length; i++) { if (t < numbers[i]) return prev; prev = refs[i]; }
            return refs[refs.Length - 1];
        }
    }

    /// <summary>DinoCap::CompAnimObj (CombineAnimation-free component animation object) with its two channels: the base channel (PlayAnimation 0x119010,
    /// +172..+220) and the advanced channel (PlayAnimationAdv 0x118f4c, +176..+221) that is composed onto it per sprite (OnUpdateSprite 0x1189ec):
    /// position and rotation are summed with the advanced frame, scale/shear/colour/layer and the texture come from the advanced component.
    /// OnUpdateFrameNumber 0x11835c: accumulate, step frames, end/loop handling, single-shot key frames (+232 base, +236 advanced).</summary>
    public sealed class NativeCompObj
    {
        const float TwoPi = 6.2831855f, Pi = 3.1415927f;
        public NativeComponentAnimation baseAnim, advAnim;
        public Vector2 position; public float objectScale = 1f; public bool flipX;
        /// <summary>CompAnimObj+240..243 (RGBA): CompAnimObj::OnUpdateSprite 0x1189ec scales each sprite colour channel by byte/255 ((uint)(c * o / 255)).
        /// playerNewAniClass::OnUpdate 0x127840 switches it between white and 0xFF7F7F7F while the girl is invincible (Wudi).</summary>
        public Color32 objectColor = new Color32(255, 255, 255, 255);
        static byte MulChannel(byte c, byte o) { return (byte)(uint)N.Div32(N.Mul32((float)c, (float)o), 255f); }
        /// <summary>Final sprite colour: the component (or SetChangeSpriteColor) colour times objectColor; a white objectColor returns the input unchanged.</summary>
        Color32? ObjectTint(Color32? over, uint rgba)
        {
            if (objectColor.r == 255 && objectColor.g == 255 && objectColor.b == 255 && objectColor.a == 255) return over;
            Color32 c = over ?? new Color32((byte)rgba, (byte)(rgba >> 8), (byte)(rgba >> 16), (byte)(rgba >> 24));
            return new Color32(MulChannel(c.r, objectColor.r), MulChannel(c.g, objectColor.g), MulChannel(c.b, objectColor.b), MulChannel(c.a, objectColor.a));
        }
        public string libraryResource = "DC2/Image/acb/Player.library.lcb";
        readonly Transform parent;
        readonly List<NativeAffineSprite> sprites = new List<NativeAffineSprite>();
        readonly List<KeyValuePair<string, string>> avatarPairs = new List<KeyValuePair<string, string>>();   // +308
        readonly Dictionary<string, string> selection = new Dictionary<string, string>();                      // +40: component name -> key
        readonly Dictionary<string, NativeCaLibraryAnim> advLibrary = new Dictionary<string, NativeCaLibraryAnim>();   // SetAdvCompAni: key -> library animation
        public readonly Dictionary<string, Color32> colorOverrides = new Dictionary<string, Color32>();

        // base channel
        public int frame, start, end; public uint loops = 1; public bool fin; float acc, interval = 1f / 30f;
        // advanced channel
        public int advFrame = -1, advStart, advEnd; public uint advLoops = 1; public bool advFin; float advAcc, advInterval = 1f / 30f;
        public int keyFrame = -1, advKeyFrame = -1;
        public Action<int> onKeyFrame, onKeyFrameAdv;       // vtable +48 / +52 (argument: the key frame index that was set)
        public Action onAnimationEnd, onAnimationEndAdv;    // vtable +40 / +44
        public Action<int> onFrameNumber;                   // per-step object callback (+344/+352) with GetCurAnimationNumber

        /// <summary>CompAnimObjSprite (+12 x, +16 y, +20 w, +24 h, +28 rotation) of the sprites emitted by the last OnUpdateSprite, by component name (CompAnimObj::GetSprite 0x117e7c).</summary>
        public struct SpriteInfo { public float x, y, w, h, rot; }
        public readonly Dictionary<string, SpriteInfo> Sprites = new Dictionary<string, SpriteInfo>();

        public NativeCompObj(Transform parent) { this.parent = parent; }

        public bool AdvActive { get { return advAnim != null && advFrame >= 0; } }
        public bool SelectionIsDefault()   // DEBUG_ONLY verifier hook: no avatar substitution active
        {
            if (baseAnim == null) return true;
            if (selection.Count != baseAnim.defaultSelection.Count) return false;
            foreach (var kv in baseAnim.defaultSelection) { string v; if (!selection.TryGetValue(kv.Key, out v) || v != kv.Value) return false; }
            return true;
        }
        public string SelectionOf(string comp) { string k; return selection.TryGetValue(comp, out k) ? k : ""; }

        // ---- CompAnimObj::SetAnimation 0x1198ac ----
        public void SetAnimation(NativeComponentAnimation anim)
        {
            baseAnim = anim;
            selection.Clear();
            foreach (var kv in anim.defaultSelection) selection[kv.Key] = kv.Value;
            DoSetAvatar();
        }

        // ---- CompAnimObj::SetAnimationAdv 0x119102 (+ UpdateSpriteDrawOrderAdv 0x118036) ----
        public void SetAnimationAdv(NativeComponentAnimation anim)
        {
            advAnim = anim;
            if (anim != null)
            {
                if (!anim.advNormalized) { NormalizeAdv(anim); anim.advNormalized = true; }
                advFrame = -1;
            }
            else advFrame = -1;
        }

        static void NormalizeAdv(NativeComponentAnimation anim)
        {
            foreach (var comp in anim.byKey.Values)
            {
                float x0 = 0, y0 = 0, r0 = 0; bool first = true;
                for (int i = 0; i < comp.frames.Length; i++)
                {
                    var f = comp.frames[i]; if (f == null) continue;
                    if (first) { x0 = f.x; y0 = f.y; r0 = f.radians; first = false; }
                    f.x -= x0; f.y -= y0;
                    float r = f.radians - r0;
                    if (r > Pi) r -= TwoPi; else if (r < -Pi) r += TwoPi;
                    f.radians = r;
                }
            }
        }

        /// <summary>CompAnimObj::SetAvatar 0x11888e / DoSetAvatar 0x1186e6: pairs (component, avatar) select name+avatar when the base animation has that component.</summary>
        public void SetAvatar(IEnumerable<KeyValuePair<string, string>> pairs)
        {
            avatarPairs.Clear(); avatarPairs.AddRange(pairs);
            DoSetAvatar();
        }

        /// <summary>CompAnimObj::ClearAvatar 0x117cfc: drop the avatar pairs (+308) and reset the selection map (+40) to the current base animation's defaults (+24).</summary>
        public void ClearAvatar()
        {
            avatarPairs.Clear();
            selection.Clear();
            if (baseAnim != null) foreach (var kv in baseAnim.defaultSelection) selection[kv.Key] = kv.Value;
        }

        void DoSetAvatar()
        {
            if (baseAnim == null) return;
            foreach (var p in avatarPairs)
                if (baseAnim.byKey.ContainsKey(p.Key + p.Value)) selection[p.Key] = p.Key + p.Value;
        }

        /// <summary>CompAnimObj::SetAdvCompAni 0x117dae: the advanced component (name+avatar) renders through a library animation by its frame number.</summary>
        public void SetAdvCompAni(string name, string avatar, string animName)
        {
            if (advAnim == null) return;
            var lib = NativeCaLibraryAnim.Get(libraryResource, animName);
            NativeComponentAnimation.Component c;
            if (lib != null && advAnim.byKey.TryGetValue(name + avatar, out c)) c.libAnim = lib;
        }

        // ---- ComponentAnimation::GetFrameIndex 0x11cea4 / CAComponent::GetFrameIndex 0x11d90c ----
        public static int GetFrameIndex(NativeComponentAnimation anim, float number)
        {
            var keys = new List<string>(anim.byKey.Keys); keys.Sort(string.CompareOrdinal);
            foreach (var k in keys)
            {
                var frames = anim.byKey[k].frames;
                for (int i = 0; i < frames.Length; i++)
                    if (frames[i] != null && frames[i].number > number) { if (i - 1 != -1) return i - 1; else return -1; }
            }
            return -1;
        }

        static int FrameSize(NativeComponentAnimation anim) { return anim.FrameCount; }

        /// <summary>CompAnimObj::PlayAnimation 0x119010.</summary>
        public void PlayAnimation(uint loopCount, float fps, float startNumber, float endNumber, bool updateSprite = true)
        {
            loops = loopCount; interval = 1f / (fps * baseAnim.FrameRateFactor); fin = false;
            start = startNumber == 0f ? 0 : GetFrameIndex(baseAnim, startNumber);
            end = endNumber == -1f ? -1 : GetFrameIndex(baseAnim, endNumber);
            int size = FrameSize(baseAnim);
            if ((uint)start > (uint)size) start = 0;   // unsigned compare: size < start (negative start counts as large)
            if ((uint)end > (uint)size) end = size - 1;
            frame = start; acc = 0f;
            if (updateSprite) OnUpdateSprite();
        }

        /// <summary>CompAnimObj::PlayAnimationAdv 0x118f4c.</summary>
        public void PlayAnimationAdv(uint loopCount, float fps, float startNumber, float endNumber, bool updateSprite = true)
        {
            advLoops = loopCount; advInterval = 1f / (fps * advAnim.FrameRateFactor); advFin = false;
            advStart = startNumber == 0f ? 0 : GetFrameIndex(advAnim, startNumber);
            advEnd = endNumber == -1f ? -1 : GetFrameIndex(advAnim, endNumber);
            int size = FrameSize(advAnim);
            if ((uint)advStart > (uint)size) advStart = 0;
            if ((uint)advEnd > (uint)size) advEnd = size - 1;
            advFrame = advStart; advAcc = 0f;
            if (updateSprite) OnUpdateSprite();
        }

        /// <summary>CompAnimObj::GetCurAnimationNumber 0x11826a: number of the first drawn component that has the current base frame.</summary>
        public int CurAnimationNumber
        {
            get
            {
                foreach (var c in DrawList())
                    if (frame >= 0 && frame < c.frames.Length && c.frames[frame] != null) return (int)c.frames[frame].number;
                return 0;
            }
        }

        List<NativeComponentAnimation.Component> DrawList()
        {
            var list = new List<NativeComponentAnimation.Component>();
            if (baseAnim == null) return list;
            foreach (var kv in selection)
            {
                NativeComponentAnimation.Component c;
                if (baseAnim.byKey.TryGetValue(kv.Value, out c)) list.Add(c);
            }
            // SpriteSortLayer: signed layer of the current frame, then ordinal component-name comparison.
            int fi = frame;
            list.Sort((a, b) =>
            {
                var fa = fi >= 0 && fi < a.frames.Length ? a.frames[fi] : null; var fb = fi >= 0 && fi < b.frames.Length ? b.frames[fi] : null;
                int la = fa != null ? fa.layer : 0, lb = fb != null ? fb.layer : 0;
                return la != lb ? la.CompareTo(lb) : string.CompareOrdinal(a.name, b.name);
            });
            return list;
        }

        /// <summary>CompAnimObj::OnUpdate 0x1190d4: OnUpdateFrameNumber then OnUpdateSprite.</summary>
        public void Step(float dt)
        {
            if (baseAnim == null) return;
            StepFrames(dt);
            OnUpdateSprite();
        }

        void StepFrames(float dt)
        {
            acc += dt; advAcc += dt;
            while (acc >= interval)
            {
                int old = frame;
                frame = old + 1; acc -= interval;
                if (frame > end)
                {
                    if (loops <= 1u)
                    {
                        frame = end;
                        if (!fin) { fin = true; if (onAnimationEnd != null) onAnimationEnd(); }
                    }
                    else { frame = old - end + start; if (loops != uint.MaxValue) loops--; }
                }
                if (onFrameNumber != null) onFrameNumber(CurAnimationNumber);
            }
            if (advAnim != null)
            {
                if (advFin) advFrame = -1;
                else
                {
                    while (advAcc >= advInterval)
                    {
                        int old = advFrame;
                        advFrame = old + 1; advAcc -= advInterval;
                        if (advFrame > advEnd)
                        {
                            if (advLoops <= 1u)
                            {
                                advFrame = advEnd;
                                if (!advFin) { advFin = true; if (onAnimationEndAdv != null) onAnimationEndAdv(); }
                            }
                            else { advFrame = old - advEnd + advStart; advLoops--; }
                        }
                    }
                }
            }
            if (keyFrame > -1 && frame == keyFrame) { int k = keyFrame; if (onKeyFrame != null) onKeyFrame(k); keyFrame = -1; }
            if (advKeyFrame > -1 && advKeyFrame == advFrame) { int k = advKeyFrame; if (onKeyFrameAdv != null) onKeyFrameAdv(k); advKeyFrame = -1; }
        }

        /// <summary>CompAnimObj::OnUpdateSprite 0x1189ec.</summary>
        public void OnUpdateSprite()
        {
            if (baseAnim == null) return;
            int count = 0;
            Sprites.Clear();
            if (frame >= 0 && frame < baseAnim.FrameCount)
            {
                foreach (var comp in DrawList())
                {
                    if (frame >= comp.frames.Length) continue;
                    var f = comp.frames[frame]; if (f == null) continue;
                    float x = f.x, y = f.y, rot = f.radians;
                    var merged = new NativeComponentAnimation.Frame { number = f.number, shearX = f.shearX, shearY = f.shearY, scaleX = f.scaleX, scaleY = f.scaleY, rgba = f.rgba, layer = f.layer };
                    string avatar = comp.avatar;
                    NativeCaLibraryAnim lib = comp.libAnim;
                    NativeComponentAnimation.Component ac = null;
                    if (advAnim != null && advFrame >= 0 && advFrame < advAnim.FrameCount && advAnim.byKey.TryGetValue(comp.Key, out ac) && advFrame < ac.frames.Length && ac.frames[advFrame] != null)
                    {
                        var a = ac.frames[advFrame];
                        x += a.x; y += a.y;
                        rot += a.radians;
                        if (rot > TwoPi) rot -= TwoPi; else if (rot < 0f) rot += TwoPi;
                        merged.shearX = a.shearX; merged.shearY = a.shearY; merged.scaleX = a.scaleX; merged.scaleY = a.scaleY; merged.rgba = a.rgba; merged.layer = a.layer;
                        merged.number = a.number;
                        avatar = ac.avatar; lib = ac.libAnim;
                    }
                    merged.x = x; merged.y = y; merged.radians = rot;
                    string identity;
                    if (lib != null) { string r = lib.FrameRef(merged.number); if (r == null) continue; identity = NativeReferenceSuffix.FrameName(r); }
                    else { if (avatar.Length == 0) continue; identity = NativeReferenceSuffix.FrameName(avatar); }
                    if (!Atlas.Has(identity)) throw new Exception("Unresolved component avatar " + identity);
                    while (sprites.Count <= count) sprites.Add(NativeAffineSprite.Create(parent));
                    sprites[count].Draw(identity, merged,
                        new Vector2(N.ScreenW * 0.5f + position.x + (flipX ? -merged.x : merged.x) * objectScale, -N.ScreenH * 0.5f + position.y + merged.y * objectScale), count, objectScale, flipX,
                        ObjectTint(colorOverrides.TryGetValue(comp.name, out Color32 over) ? over : (Color32?)null, merged.rgba));
                    var rect = Atlas.Get(identity).rect;
                    Sprites[comp.name] = new SpriteInfo { x = position.x + (flipX ? -merged.x : merged.x) * objectScale, y = position.y + merged.y * objectScale,
                        w = rect.width * merged.scaleX * objectScale, h = rect.height * merged.scaleY * objectScale, rot = flipX ? -merged.radians : merged.radians };
                    count++;
                }
            }
            for (int i = count; i < sprites.Count; i++) sprites[i].Hide();
        }

        public void Hide() { foreach (var s in sprites) s.Hide(); }
        public void Destroy() { foreach (var s in sprites) if (s != null) UnityEngine.Object.Destroy(s.gameObject); sprites.Clear(); }
    }
}
