using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dc2Pilot
{
    public sealed class PooledEffect { public int drawBase; public float rotation; public bool flip; public int slot; }   // a pooled effectClass / baozhaClass GameObject: keeps its DrawOrder base, its rotation and its multimap slot (first creation) between uses
    public sealed class FxActor { public int objectId = 5; public bool skipFirstUpdate; public int drawBase = -1; public Stack<int> drawPool; public PooledEffect pooled; public Stack<PooledEffect> pool; public GameObject root; public NativeCaPlayer ca; public float x, y; public string tag; public bool persistent; public float? camOffset; public DinoActor mount; public Vector2 mountDelta; }
    /// <summary>effectClassN: a CompAnimObj world effect (mainScene::GetEffectN 0x989a8) playing one ComponentAnimation once.</summary>
    public sealed class CompFxActor { public GameObject root; public NativeCompObj obj; public float x, y; public string tag; public bool ended; public int slot; public bool skipFirstUpdate; public bool id0; }

    /// <summary>One-shot CombineAnimation world effects (effectClass / baozhaClass pooled objects: mainScene::GetEffect 0x956b6, GetBaozha 0x98880).
    /// The effect GameObject rotation (Qianghuo sets hero rotation -/+ 0.1) is applied through NativeCaPlayer.objectRotation and kept by the pooled object (r118v).</summary>
    public partial class GameScene
    {
        readonly List<FxActor> fxList = new List<FxActor>();
        bool fxStagePassed;   // the id-5 object pass of this tick is over (L1)
        int objectSlotSeq;    // Scene multimap insertion counter for the id-5 family (effects, projectiles, effectClassN, bossN, YanJiang): equal ids update in insertion order
        int NextObjectSlot() { return ++objectSlotSeq; }
        readonly Stack<int> effectNSlots = new Stack<int>();   // GetEffectN pool: a reused effectClassN keeps its slot
        readonly Stack<PooledEffect> effectPool = new Stack<PooledEffect>(), baozhaPool = new Stack<PooledEffect>();
        /// <summary>dinosaurClass::Blood 0x12bd0..0x12bda: layer 10 and DrawOrder base := dinosaur base + 20 (kept by the pooled object).</summary>
        void SetFxDrawBase(FxActor f, int drawBase) { f.drawBase = drawBase; if (f.pooled != null) f.pooled.drawBase = drawBase; var g = f.root.GetComponent<UnityEngine.Rendering.SortingGroup>(); if (g != null) g.sortingOrder = NativeDrawOrder.CaGroup(drawBase); }
        /// <summary>A GetEffect user that never writes the CAObj flip flag (+96) keeps the pooled object's previous value (playerClass::OnAnimationEnd roll effect 0xbdc0a..0xbdc8e).</summary>
        FxActor SpawnCaFxInheritFlip(string tag, NativeCaAnimation anim, float x, float y, int layer, float objectScale)
        {
            bool flip = effectPool.Count > 0 && effectPool.Peek().flip;
            return SpawnCaFx(tag, anim, x, y, flip, layer, objectScale);
        }
        /// <summary>GameObject::SetRotation on the effect object (kept by the pooled object).</summary>
        void SetFxRotation(FxActor f, float radians) { f.ca.objectRotation = radians; if (f.pooled != null) f.pooled.rotation = radians; }
        void ReleaseFxDraw(FxActor f) { if (f.pool != null && f.pooled != null) f.pool.Push(f.pooled); f.pool = null; }
        /// <summary>LevelUpEffect 0x957e0: three GetEffect objects with SetRotation(0) (the port's level-up players are separate; their pool records keep the shared pool order and reset the rotation).</summary>
        PooledEffect TakeEffectRecord() { var p = effectPool.Count > 0 ? effectPool.Pop() : new PooledEffect { drawBase = NativeDrawOrder.Base(NativeDrawOrder.Allocate()), slot = NextObjectSlot() }; p.rotation = 0f; return p; }
        void ReturnEffectRecord(PooledEffect p) { if (p != null) effectPool.Push(p); }
        public float FxRotation(string tag) { for (int i = fxList.Count - 1; i >= 0; i--) if (fxList[i].tag == tag) return fxList[i].ca.objectRotation; return float.NaN; }   // verifier hook
        public int FxDrawBase(string tag) { for (int i = fxList.Count - 1; i >= 0; i--) if (fxList[i].tag == tag) return fxList[i].drawBase; return -1; }   // verifier hook
        readonly Dictionary<string, NativeCaAnimation> fxAnims = new Dictionary<string, NativeCaAnimation>();
        public int FxSpawned;                      // DEBUG_ONLY
        public FxActor DebugSpawnLoopFx() { return SpawnCaFx("dbg", FxAnimation("DC2/Image/an2/shandian.a2b", true), heroX, 0f, false, 14, 1f, -1); }   // DEBUG_ONLY
        public void DebugRemoveFx(FxActor f) { RemoveFx(f); }   // DEBUG_ONLY
        public Action<object> DebugId5Stepped;   // DEBUG_ONLY verifier hook: each object of the id-5 walk, in order
        public bool DebugSpawnFx() { var f = SpawnCaFx("dbg", FxAnimation("DC2/Image/an2/shandian.a2b", true), heroX, 0f, false, 14, 1f); return f.skipFirstUpdate; }   // DEBUG_ONLY: true when the new effect waits for the next tick
        readonly List<CompFxActor> compFxList = new List<CompFxActor>();
        public int FxActive { get { return fxList.Count + compFxList.Count; } }
        public string LastFxTag = "";              // DEBUG_ONLY

        NativeCaAnimation FxAnimation(string resource, bool xmlNamed)
        {
            NativeCaAnimation a;
            if (!fxAnims.TryGetValue(resource, out a)) { a = NativeCaAnimation.Load(resource, xmlNamed, 1f); fxAnims[resource] = a; }
            return a;
        }

        /// <summary>effectClass/baozhaClass: setAnimation + playAnimation(0,0,0.025,-1) once, layer field, CA flip flag, objectScale; the object is disabled at animation end.</summary>
        FxActor SpawnCaFx(string tag, NativeCaAnimation anim, float x, float y, bool flip, int layer, float objectScale, int loops = 0)
        {
            var f = new FxActor { x = x, y = y, tag = tag };
            f.root = new GameObject("fx_" + tag); f.root.transform.SetParent(Background.LayerRoot(layer), false);
            f.ca = new NativeCaPlayer(f.root.transform, _ => f.root.transform, 100);
            // effectClass objects come from the LIFO pool scene+60 (GetEffect 0x956b6, effectClass::Disappear 0x15eea), baozhaClass from scene+84 (GetBaozha, baozhaClass::Disappear 0x15e8a);
            // dahuo (per-dinosaur object) and the beam objects are created directly and are not pooled. A pooled object keeps its DrawOrder base and its GameObject rotation:
            // only Qianghuo 0xb9230 and LevelUpEffect 0x958d4 set the rotation, every other GetEffect user (blood, wind, lightning, FeiBiao/JuPan, boss blood, roll effect) inherits it (shipped quirk).
            f.pool = tag == "baozha" ? baozhaPool : (tag == "dahuo" || tag.StartsWith("beam")) ? null : effectPool;
            bool reused = f.pool != null && f.pool.Count > 0;
            f.pooled = reused ? f.pool.Pop() : new PooledEffect { drawBase = NativeDrawOrder.Base(NativeDrawOrder.Allocate()), slot = NextObjectSlot() };
            f.objectId = tag == "dahuo" ? 96 : tag.StartsWith("beam") ? 97 : 5;   // dahuo / beams: GameContainer children of the dinosaur / hero (updated with their parent)
            // A new effect GameObject created during the update stage waits in the pending set and gets its first Update next tick; a re-enabled pooled
            // object (id 5, after the dinosaurs and the hero in the map) is updated in this tick (Scene::Update 0x175e84, r118z L1).
            // r118z2: only a NEW object created before the end of this tick's id-5 pass is held back (it sits in the pending set until next tick). A reused
            // pooled object is already in the map: the slot-ordered pass reaches it this tick only if its slot is still ahead (UpdateId5Objects). The
            // r118z rule also skipped objects created or reused after the pass, which delayed their first update by a second tick.
            f.skipFirstUpdate = InUpdateStage && !reused && !fxStagePassed;
            f.drawBase = f.pooled.drawBase; f.ca.objectRotation = f.pooled.rotation; f.pooled.flip = flip;
            if (layer == 5 || layer == 10 || layer == 12)   // DrawOrder-sorted layers; material-sorted layers order equal-material sprites by GameContainer std::set address order (0x16b878): not statically recoverable, left as is
                f.root.AddComponent<UnityEngine.Rendering.SortingGroup>().sortingOrder = NativeDrawOrder.CaGroup(f.drawBase);
            f.ca.flipX = flip; f.ca.objectScale = objectScale;
            f.ca.position = new Vector2(x - (camLeft + WorldViewportW * 0.5f), y);
            f.ca.Play(anim, loops, 0, 0.025f);
            fxList.Add(f); FxSpawned++; LastFxTag = tag;
            return f;
        }

        /// <summary>effectClassN setup: SetAnimation + PlayAnimation(1,30,0,-1), layer field, flip flag, objectScale (CompAnimObj+228); disabled at animation end.</summary>
        CompFxActor SpawnCompFx(string tag, NativeComponentAnimation anim, float x, float y, bool flip, int layer, float objectScale)
        {
            var f = new CompFxActor { x = x, y = y, tag = tag };
            if (tag == "bossShao") f.id0 = true;   // bossN::GetEff 0x120098: one of the 10 id-0 effectClassN objects bossN::OnLoad 0x1211d2 creates (pre-created, so never pending)
            else { bool reusedN = effectNSlots.Count > 0; f.slot = reusedN ? effectNSlots.Pop() : NextObjectSlot(); f.skipFirstUpdate = InUpdateStage && !reusedN && !fxStagePassed; }   // GetEffectN 0x989a8 pool (id 5)
            f.root = new GameObject("compfx_" + tag); f.root.transform.SetParent(Background.LayerRoot(layer), false);
            f.root.AddComponent<UnityEngine.Rendering.SortingGroup>().sortingOrder = NativeDrawOrder.CustomDraw;   // effectClassN (CompAnimObj) custom draw: after every sprite of its layer
            f.obj = new NativeCompObj(f.root.transform) { flipX = flip, objectScale = objectScale };
            f.obj.position = new Vector2(x - (camLeft + WorldViewportW * 0.5f), y);
            f.obj.SetAnimation(anim);
            f.obj.onAnimationEnd = () => f.ended = true;
            f.obj.PlayAnimation(1u, 30f, 0f, -1f);
            compFxList.Add(f); FxSpawned++; LastFxTag = tag;
            return f;
        }

        void RemoveFx(FxActor f) { if (f == null) return; ReleaseFxDraw(f); f.ca.Hide(); if (f.root != null) UnityEngine.Object.Destroy(f.root); fxList.Remove(f); }

        void StepFx(FxActor f, float dt)
        {
            if (f.skipFirstUpdate) { f.skipFirstUpdate = false; return; }
            if (f.mount != null) { f.x = N.Add32(f.mount.x, f.mountDelta.x); f.y = N.Add32(f.mount.y, f.mountDelta.y); }   // Scene::Mount follow
            if (f.camOffset.HasValue) f.x = N.Add32(camLeft, f.camOffset.Value);   // effectClass mode 10: x = viewport.x + offset every update
            f.ca.position = new Vector2(f.x - (camLeft + WorldViewportW * 0.5f), f.y);
            f.ca.Step(dt);
            if (f.ca.Ended) { ReleaseFxDraw(f); f.ca.Hide(); UnityEngine.Object.Destroy(f.root); fxList.Remove(f); }
        }

        void StepCompFx(CompFxActor f, float dt)
        {
            if (f.skipFirstUpdate) { f.skipFirstUpdate = false; return; }
            f.obj.position = new Vector2(f.x - (camLeft + WorldViewportW * 0.5f), f.y);
            f.obj.Step(dt);
            if (f.ended) { f.obj.Hide(); UnityEngine.Object.Destroy(f.root); compFxList.Remove(f); if (!f.id0) effectNSlots.Push(f.slot); }
        }

        /// <summary>Id-0 effectClassN objects (the boss's GetEff pool): they update with the other id-0 objects, before the hero (r118z2).</summary>
        void UpdateId0Fx(float dt)
        {
            foreach (var f in compFxList.ToArray()) if (f.id0 && compFxList.Contains(f)) StepCompFx(f, dt);
            StepLevelUpComp(dt);   // lifeModeScene::LevelUpEffect effectClassN (CreateGameObject id 0)
        }

        /// <summary>Scene::Update id-5 pass (r118z2): every id-5 object (effectClass / baozhaClass pool, FeiBiao / JuPan projectiles, effectClassN, bossN,
        /// YanJiang waves) in one multimap walk, insertion order. A pooled object keeps the slot of its first creation; an object re-enabled during the walk is
        /// reached in this tick only if its slot is still ahead of the walk, as in _Rb_tree iteration. Replaces the r118z fixed order projectiles -> boss -> effects.</summary>
        void UpdateId5Objects(float dt)
        {
            int cursor = int.MinValue;
            while (true)
            {
                int best = int.MaxValue; object pick = null;
                foreach (var f in fxList)
                    if (f.objectId == 5 && f.pooled != null && f.pooled.slot > cursor && f.pooled.slot < best && (!f.persistent || ProjectileOf(f) != null)) { best = f.pooled.slot; pick = f; }
                foreach (var c in compFxList) if (!c.id0 && c.slot > cursor && c.slot < best) { best = c.slot; pick = c; }
                foreach (var bf in bossFx) if (bf.slot > cursor && bf.slot < best) { best = bf.slot; pick = bf; }
                if (Boss != null && Boss.ca != null && Boss.slot > cursor && Boss.slot < best) { best = Boss.slot; pick = Boss; }
                if (levelUpFx != null) for (int k = 0; k < 3; k++) { var r = levelUpFx.records[k]; if (!levelUpFx.caDone[k] && r != null && r.slot > cursor && r.slot < best) { best = r.slot; pick = k; } }
                if (pick == null) break;
                cursor = best;
                if (DebugId5Stepped != null) DebugId5Stepped(pick);
                var fx = pick as FxActor;
                if (fx != null) { var pr = ProjectileOf(fx); if (pr != null) StepProjectile(pr, dt); else StepFx(fx, dt); }
                else if (pick is CompFxActor) StepCompFx((CompFxActor)pick, dt);
                else if (pick is BossFx) StepBossFx((BossFx)pick, dt);
                else if (pick is int) StepLevelUpCa((int)pick, dt);
                else UpdateBossBody(dt);
            }
        }
    }
}
