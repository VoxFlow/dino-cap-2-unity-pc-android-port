using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace Dc2Pilot
{
    /// <summary>Binary A2B vessel. Native consumers: CAFrame/CASprite Load[Xml]FromFile.
    /// Raw assets and lossless neutral IR are hash/roundtrip verified before import.
    /// Runtime semantics are DC2 contracts, not the reused DC1 parser's advisory labels.
    /// </summary>
    public sealed class NativeCaAnimation
    {
        public sealed class Part
        {
            public string name, reference;
            public float x, y, w, h, pivotX, pivotY, radians;
            public uint rgba, layerBits;
            public bool spriteAnimation;
        }
        public sealed class Frame { public uint ordinal; public Part[] parts; public bool key; }
        public Frame[] frames;
        // CombineAnimation cached bounds (+0x10/+0x14, ctor 0x2380: 1024 / -1024): computed by both loaders, then copied by Clone.
        // Image replacement does not recompute them. r119i: the XML-named loader computes them too (CombineAnimation::LoadXmlFromFile 0xc60a..0xc640 from
        // CAFrame::LoadXmlFromFile 0xc46a..0xc4b0: per sprite x -/+ 0.5*w after CASprite::LoadXmlFromFile); the port skipped them, which left K5 at 1024/-1024
        // and made the r119f viewport gate reject every pterodactyl (user "heroes can't harm pterodactyls").
        public float minX = 1024f, maxX = -1024f;

        public static NativeCaAnimation Load(string resource, bool xmlNamedLoader, float scale = 1f)
        {
            var asset = Resources.Load<TextAsset>(resource);
            if (asset == null) throw new Exception("Missing native animation " + resource);
            using (var stream = new MemoryStream(asset.bytes, false))
            using (var reader = new BinaryReader(stream))
            {
                var animation = new NativeCaAnimation { frames = new Frame[reader.ReadUInt32()] };
                for (int i = 0; i < animation.frames.Length; i++)
                {
                    var frame = new Frame { ordinal = reader.ReadUInt32(), parts = new Part[reader.ReadUInt32()] };
                    for (int p = 0; p < frame.parts.Length; p++)
                    {
                        var part = new Part();
                        part.name = ReadString(reader, checked((int)reader.ReadUInt32()));
                        part.x = reader.ReadSingle() * scale; part.y = reader.ReadSingle() * scale;
                        part.w = reader.ReadSingle(); part.h = reader.ReadSingle();
                        part.pivotX = reader.ReadSingle(); part.pivotY = reader.ReadSingle();
                        part.radians = reader.ReadSingle(); part.rgba = reader.ReadUInt32();
                        part.layerBits = reader.ReadUInt32();
                        int length = reader.ReadInt32();
                        part.reference = length > 0 ? ReadString(reader, length) : "";
                        string identity = NativeReferenceSuffix.FrameName(part.reference);
                        if (!xmlNamedLoader)
                        {
                            part.w *= scale; part.h *= scale;
                        }
                        else if (Atlas.Has(identity))
                        {
                            // CASprite::LoadXmlFromFile@0xb8ec: threshold branch verified on ARM.
                            if (part.w < 10f && part.h < 10f)
                            {
                                var size = Atlas.FrameSize(identity);
                                if (part.reference == "yingzi") { part.w = size.x * scale; part.h = size.y * scale; }
                                else { part.w *= size.x; part.h *= size.y; }
                            }
                            else { part.w *= scale; part.h *= scale; }
                        }
                        frame.parts[p] = part;
                        {   // both loaders (binary: literal load; XML: CAFrame::LoadXmlFromFile 0xc46c: R0 = 0.5, R1 = sprite+8, R2 unused by __mulsf3vfp)
                            float half = N.Mul32(part.w, .5f);
                            float left = N.Add32(part.x, -half), right = N.Add32(part.x, half);
                            if (left < animation.minX) animation.minX = left;
                            if (right > animation.maxX) animation.maxX = right;
                        }
                    }
                    // Native binary loaders append in file order. No inferred layer re-sort.
                    animation.frames[i] = frame;
                }
                if (stream.Position != stream.Length) throw new Exception("Trailing A2B bytes: " + resource);
                return animation;
            }
        }

        static string ReadString(BinaryReader r, int count)
        {
            byte[] bytes = r.ReadBytes(count);
            if (bytes.Length != count) throw new EndOfStreamException();
            return Encoding.GetEncoding(28591).GetString(bytes);
        }

        public void MarkKeyFrame(int frame) { if ((uint)frame < (uint)frames.Length) frames[frame].key = true; }

        // CombineAnimation::Clone@0xc22a owns independent frames and sprites.
        public NativeCaAnimation Clone()
        {
            var copy = new NativeCaAnimation { frames = new Frame[frames.Length], minX = minX, maxX = maxX };
            for (int i = 0; i < frames.Length; i++)
            {
                var source = frames[i];
                var frame = new Frame { ordinal = source.ordinal, key = source.key, parts = new Part[source.parts.Length] };
                for (int p = 0; p < source.parts.Length; p++)
                {
                    var a = source.parts[p];
                    frame.parts[p] = new Part { name = a.name, reference = a.reference, x = a.x, y = a.y,
                        w = a.w, h = a.h, pivotX = a.pivotX, pivotY = a.pivotY, radians = a.radians,
                        rgba = a.rgba, layerBits = a.layerBits, spriteAnimation = a.spriteAnimation };
                }
                copy.frames[i] = frame;
            }
            return copy;
        }

        // ChangeImageK5@0xc9c4: per frame, the named part's current image decides the replacement suffix:
        // "chibang2" -> <base minus last char>"2", "chibang1" -> "1", anything else -> "3". Geometry is untouched.
        public void ChangeImageK5(string partName, string imageBase)
        {
            string prefix = imageBase.Substring(0, imageBase.Length - 1);
            foreach (var frame in frames)
                foreach (var part in frame.parts)
                    if (part.name == partName)
                    {
                        part.reference = prefix + (part.reference == "chibang2" ? "2" : part.reference == "chibang1" ? "1" : "3");
                        break;
                    }
        }

        // ChangeImage@0xcc8a: named part, existing width/height multiplied by argument;
        // replacement atlas dimensions do not change the recovered pose geometry.
        public bool eggBodySwapped;   // playerClass::getEgg/getDownEgg 0xb937c/0xb96f8 body swap applied to this animation instance
        public void ChangeImage(string partName, string reference, float sizeMultiplier = 1f)
        {
            foreach (var frame in frames)
                foreach (var part in frame.parts)
                    if (part.name == partName)
                    {
                        part.reference = reference;
                        part.w *= sizeMultiplier; part.h *= sizeMultiplier;
                        break; // native frame map has one entry per name
                    }
        }

        // CombineAnimation::SetSpriteAnim@0xedbc identifies parts by NAME, not atlas reference.
        public void SetSpriteAnimation(string partName)
        {
            foreach (var frame in frames)
                foreach (var part in frame.parts) if (part.name == partName) part.spriteAnimation = true;
        }

        static Part CopyPartInto(Part a,Part r)
        { r.name=a.name;r.reference=a.reference;r.x=a.x;r.y=a.y;r.w=a.w;r.h=a.h;r.pivotX=a.pivotX;r.pivotY=a.pivotY;r.radians=a.radians;r.rgba=a.rgba;r.layerBits=a.layerBits;r.spriteAnimation=a.spriteAnimation;return r; }
        static Part CopyPart(Part a)
        { return new Part{name=a.name,reference=a.reference,x=a.x,y=a.y,w=a.w,h=a.h,pivotX=a.pivotX,pivotY=a.pivotY,radians=a.radians,rgba=a.rgba,layerBits=a.layerBits,spriteAnimation=a.spriteAnimation}; }

        // ARM SetToAdv eefe: snapshot frame0, subtract named x/y/rotation, then
        // a single strict +/-pi wrap. Sizes, pivots, colors and references stay.
        // This vessel is not yet activated by gameplay firing (r58/r59).
        public void SetToAdvanced()
        {
            var first=frames[0];var snapshot=new Part[first.parts.Length];
            for(int i=0;i<snapshot.Length;i++)snapshot[i]=CopyPart(first.parts[i]);
            float pi=N.F(0x40490fdb),tau=N.F(0x40c90fdb);
            foreach(var frame in frames)foreach(var part in frame.parts)
                foreach(var origin in snapshot)if(origin.name==part.name)
                {
                    part.x=N.Add32(part.x,-origin.x);part.y=N.Add32(part.y,-origin.y);
                    float angle=N.Add32(part.radians,-origin.radians);
                    if(angle>pi)angle=N.Add32(angle,-tau);else if(angle<-pi)angle=N.Add32(angle,tau);
                    part.radians=angle;break;
                }
        }

        // CA OnUpdate a7ce..a89e: merge by part name into base geometry.
        // Advanced replaces identity/color/pivot/sprite mode, but base w/h and
        // order remain. A single strict 0..tau adjustment is not modulo.
        public static Part MergeAdvancedPart(Part basis,Frame advanced){return MergeAdvancedPart(basis,advanced,null);}
        /// <summary>r119x perf: writes into <paramref name="into"/> when given (one scratch Part per player and part slot) instead of allocating a copy per part per step.</summary>
        public static Part MergeAdvancedPart(Part basis,Frame advanced,Part into)
        {
            var result=into==null?CopyPart(basis):CopyPartInto(basis,into);
            if(advanced==null||basis.reference.Length==0)return result;
            foreach(var overlay in advanced.parts)if(overlay.name==basis.name)
            {
                result.x=N.Add32(basis.x,overlay.x);result.y=N.Add32(basis.y,overlay.y);
                float angle=N.Add32(basis.radians,overlay.radians),tau=N.F(0x40c90fdb);
                if(angle>tau)angle=N.Add32(angle,-tau);else if(angle<0f)angle=N.Add32(angle,tau);
                result.radians=angle;result.reference=overlay.reference;result.rgba=overlay.rgba;
                result.pivotX=overlay.pivotX;result.pivotY=overlay.pivotY;result.spriteAnimation=overlay.spriteAnimation;
                break;
            }
            return result;
        }
    }

    /// <summary>CAObjCallBack frame emission / callbacks / reset-to-zero timing.
    /// No Unity Animator, animation interpolation or independent Update clock.
    /// </summary>
    public sealed class NativeCaPlayer
    {
        public NativeCaAnimation animation;
        public Vector2 position;
        public bool flipX;             // CAObjCallBack+96: negates part x, pivot x and rotation (OnUpdate 0xa926)
        public float objectScale = 1f; // CAObjCallBack+84; local pose scaled before adding object position.
        /// <summary>CAObjCallBack+88 (ctor 0xaff4: 1.0): CAObjCallBack::OnUpdate 0xa6cc multiplies each part colour's R, G, B bytes by it ((uint)(byte * v), alpha untouched).
        /// playerClass::OnUpdate 0xbb6cc pulses it 0.5..1.0 while the hero is invincible (Wudi).</summary>
        public float brightness = 1f;
        public float objectRotation;   // GameObject rotation: CAObjCallBack::OnUpdate 0xa9c0 adds GetRotation() to every (flip-signed) part rotation; positions are not rotated
        public int layer = 10, baseOrder;
        public bool active;
        public string FrameSuffix=""; // Native +108 frame suffix; set live by GameFire (KaiQiang AfterKaiQiang / fireup timeout), PilotFireVerify (r118x: old OPEN note stale); default empty.
        public int CurrentFrame { get; private set; }
        public int DisplayedFrame { get; private set; } = -1;
        public bool Ended { get; private set; }
        public float Accumulator { get; private set; }
        public Action<int> keyFrame;
        public Action animationEnd;
        // NATIVE separate CA advanced channel. Gameplay firing activation is
        // still OPEN; these methods are exercised against native oracles first.
        public NativeCaAnimation AdvancedAnimation { get; private set; }
        public int CurrentAdvancedFrame { get; private set; } = -1;
        public bool AdvancedEnded { get; private set; } = true;
        public float AdvancedAccumulator { get; private set; }
        public Action<int> advancedKeyFrame;
        public Action advancedAnimationEnd;
        int advancedLoops,advancedStart,advancedEnd,previousAdvancedKey=-1;
        float advancedInterval;
        int loops, startFrame, endFrame, previousKey = -1;
        float interval;
        readonly Transform parent;
        readonly Func<int, Transform> layerParent;
        readonly List<SpriteRenderer> sprites = new List<SpriteRenderer>();
        readonly List<NativeSpriteAnimation> spriteAnimations = new List<NativeSpriteAnimation>();
        readonly List<bool> spriteAnimating = new List<bool>();

        public NativeCaPlayer(Transform parent, Func<int, Transform> layerParent, int baseOrder)
        { this.parent = parent; this.layerParent = layerParent; this.baseOrder = baseOrder; }

        public void Play(NativeCaAnimation source, int loops, int start = 0, float interval = 0.025f, int end = -1)
        {
            animation = source; this.loops = loops; startFrame = start;
            CurrentFrame = start; endFrame = end < 0 ? source.frames.Length - 1 : end;
            this.interval = interval; Accumulator = start * interval;
            Ended = false; previousKey = -1; active = true;
        }

        // CAObjCallBack::setAnimationDelta a00c: base frame interval; the accumulator is not touched.
        public void SetInterval(float value) { interval = value; }

        public void Hide()
        {
            active = false;
            foreach (var sprite in sprites) if (sprite != null) sprite.enabled = false;
        }

        // setAnimationAdv a298 resets the cursor/ended flag, not the accumulator.
        public void SetAdvanced(NativeCaAnimation source)
        { AdvancedAnimation=source;CurrentAdvancedFrame=-1;AdvancedEnded=true; }
        public void PlayAdvanced(int loops,int start=0,float interval=.025f,int end=-1)
        {
            advancedLoops=loops;advancedStart=start;advancedEnd=end<0?AdvancedAnimation.frames.Length-1:end;
            advancedInterval=interval;CurrentAdvancedFrame=start;AdvancedAccumulator=N.Mul32(start,interval);
            AdvancedEnded=false;previousAdvancedKey=-1;
        }

        /// <summary>Per displayed frame part transforms (CAObjCallBack child sprites): local position relative to the object origin, pivot, sin/cos of the rotation and size.
        /// Source for CAObjCallBack::getSpritePos a4d0 / getSprite (NativeSpriteAnchor).</summary>
        public struct PartInfo { public Vector2 localPos, pivot, size; public float sin, cos; }
        public readonly Dictionary<string, PartInfo> PartInfos = new Dictionary<string, PartInfo>();
        readonly List<NativeCaAnimation.Part> mergeScratch = new List<NativeCaAnimation.Part>();   // r119x perf: reused merge targets (one per part slot)

        public void Step(float dt)
        {
            if (!active || animation == null) return;
            PartInfos.Clear();
            int frameIndex = CurrentFrame;
            var frame = animation.frames[frameIndex];
            var advanced=AdvancedAnimation!=null&&CurrentAdvancedFrame>=0?AdvancedAnimation.frames[CurrentAdvancedFrame]:null;
            bool usedAdvanced=false;
            DisplayedFrame = frameIndex;
            for (int i = 0; i < frame.parts.Length; i++)
            {
                while (sprites.Count <= i)
                {
                    var go = new GameObject("native_ca_part"); go.transform.SetParent(parent, false);
                    sprites.Add(go.AddComponent<SpriteRenderer>());
                    spriteAnimations.Add(new NativeSpriteAnimation()); spriteAnimating.Add(false);
                }
                var part = frame.parts[i]; var sprite = sprites[i];
                if (part.reference.Length == 0) { sprite.enabled = false; continue; }
                if(advanced!=null){while(mergeScratch.Count<=i)mergeScratch.Add(new NativeCaAnimation.Part());part=NativeCaAnimation.MergeAdvancedPart(part,advanced,mergeScratch[i]);usedAdvanced=true;}
                sprite.enabled = true;
                string identity = NativeReferenceSuffix.FrameName(part.reference);
                // Native CA part animation uses independent GameSprite time, 30 fps.
                if (part.spriteAnimation)
                {
                    var state=spriteAnimations[i];string name=NativeReferenceSuffix.AnimName(part.reference);
                    // Native aa94..ab72: static mode or changed animation name
                    // requests PlayAnimation(loop=true,30); same identity keeps its clock.
                    if(!spriteAnimating[i]||state.Name!=name)state.Play(name,true,30f);
                    spriteAnimating[i]=true;state.Step(dt);identity=state.FrameName;
                }
                else {spriteAnimating[i] = false;identity=NativeReferenceSuffix.Frame(part.reference,FrameSuffix,Atlas.Has);}
                sprite.sprite = Atlas.Get(identity);
                sprite.transform.SetParent(layerParent(layer), false);
                float width = part.w * objectScale, height = part.h * objectScale;
                float sx = flipX ? -1f : 1f, radians = N.Add32(objectRotation, part.radians * sx);
                if (!string.IsNullOrEmpty(part.name)) PartInfos[part.name] = new PartInfo { localPos = new Vector2(sx * part.x * objectScale, part.y * objectScale), pivot = new Vector2(sx * width * part.pivotX * 0.5f, height * part.pivotY * 0.5f), size = new Vector2(width, height), sin = Mathf.Sin(radians), cos = Mathf.Cos(radians) };
                Vector2 center = SpriteCenter(new Vector2(sx * part.x * objectScale, part.y * objectScale) + position,
                    new Vector2(sx * width * part.pivotX * 0.5f, height * part.pivotY * 0.5f), radians);
                sprite.transform.localPosition = new Vector3(N.ScreenW * 0.5f + center.x, -N.ScreenH * 0.5f + center.y, 0);
                sprite.transform.localRotation = Quaternion.Euler(0, 0, radians * Mathf.Rad2Deg);
                sprite.flipX = flipX;
                var size = sprite.sprite.rect.size;
                sprite.transform.localScale = new Vector3(width / size.x, height / size.y, 1);
                sprite.color = brightness == 1f ? new Color32((byte)part.rgba, (byte)(part.rgba >> 8), (byte)(part.rgba >> 16), (byte)(part.rgba >> 24))
                    : new Color32((byte)(uint)N.Mul32((float)(byte)part.rgba, brightness), (byte)(uint)N.Mul32((float)(byte)(part.rgba >> 8), brightness), (byte)(uint)N.Mul32((float)(byte)(part.rgba >> 16), brightness), (byte)(part.rgba >> 24));
                sprite.sortingOrder = baseOrder + i;
            }
            for (int i = frame.parts.Length; i < sprites.Count; i++) sprites[i].enabled = false;
            // CAObjCallBack emits marked callbacks before its accumulator/frame advance.
            if(usedAdvanced)
            {
                if(advanced.key&&previousAdvancedKey!=CurrentAdvancedFrame)advancedKeyFrame?.Invoke(CurrentAdvancedFrame);
                previousAdvancedKey=CurrentAdvancedFrame;
            }
            if (frame.key && previousKey != frameIndex) keyFrame?.Invoke(frameIndex);
            previousKey = frameIndex;
            Accumulator += dt;
            if (Accumulator >= interval) { CurrentFrame++; Accumulator = 0f; }
            // ARM adce..ae3c: base clock advances, then advanced clock/end,
            // then base end callback. Each channel drops accumulated residue.
            if(AdvancedAnimation==null)CurrentAdvancedFrame=-1;
            else if(!AdvancedEnded)
            {
                AdvancedAccumulator=N.Add32(AdvancedAccumulator,dt);
                if(AdvancedAccumulator>=advancedInterval){CurrentAdvancedFrame++;AdvancedAccumulator=0f;}
                if(CurrentAdvancedFrame>advancedEnd)
                {
                    if(advancedLoops!=0){CurrentAdvancedFrame=advancedStart;if(advancedLoops!=-1)advancedLoops--;}
                    else{CurrentAdvancedFrame=advancedEnd;AdvancedEnded=true;advancedAnimationEnd?.Invoke();}
                }
            }
            if (CurrentFrame > endFrame)
            {
                if (loops != 0) { CurrentFrame = startFrame; if (loops != -1) loops--; }
                else
                {
                    CurrentFrame = endFrame;
                    if (!Ended) { Ended = true; animationEnd?.Invoke(); }
                }
            }
        }

        public static Vector2 SpriteCenter(Vector2 position, Vector2 pivot, float radians)
        {
            // Sprite::GetPointer: T(position)*T(pivot)*R*T(-pivot)*S for zero shear.
            float c = Mathf.Cos(radians), s = Mathf.Sin(radians);
            return new Vector2(position.x + pivot.x - (c * pivot.x - s * pivot.y),
                               position.y + pivot.y - (s * pivot.x + c * pivot.y));
        }
    }
}
