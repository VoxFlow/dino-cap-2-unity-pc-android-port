using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Dc2Pilot
{
    /// <summary>menuObj/loadEffClass loading A2B playback and keyframe effects.
    /// Sources: InitAnim, InitPlayer, menuObj::OnKeyFrame, GetBaozha and loadEffClass.
    /// This vessel has no Unity Update; the recovered scene/object clock drives Step.
    /// </summary>
    public sealed class LoadingAnimation : MonoBehaviour
    {
        sealed class Effect
        {
            public NativeCaPlayer player;
            public float velocityX;
            public int id = 8;
            public int insertion;
        }

        public NativeCaPlayer Hero { get; private set; }
        public NativeComponentPlayer ComponentHero { get; private set; }
        public int HeroType { get; private set; }
        public int EffectsSpawned { get; private set; }
        public int ActiveEffectCount { get { int n = 0; foreach (var e in effects) if (e.player.active) n++; return n; } }
        readonly List<Effect> effects = new List<Effect>();
        readonly Stack<Effect> pool = new Stack<Effect>();
        readonly Dictionary<int, Transform> layers = new Dictionary<int, Transform>();
        NativeCaAnimation effectAnimation;
        int drawOrder;

        public static LoadingAnimation Create(Transform parent)
        {
            var go = new GameObject("native_loading_animation");
            go.transform.SetParent(parent, false);
            // Native Scene renders the entire game world before UIManager renders UI.
            // Nested groups preserve native world layers and per-part draw-order values.
            go.AddComponent<SortingGroup>().sortingOrder = -1;
            var result = go.AddComponent<LoadingAnimation>();
            // startUI preallocates five effects; GetBaozha pops from the back (LIFO).
            for (int i = 0; i < 5; i++) result.pool.Push(result.NewEffect(8));
            return result;
        }

        Transform Layer(int layer)
        {
            if (layers.TryGetValue(layer, out var result)) return result;
            var go = new GameObject("native_layer_" + layer); go.transform.SetParent(transform, false);
            go.AddComponent<SortingGroup>().sortingOrder = layer;
            layers[layer] = go.transform;
            return go.transform;
        }

        NativeCaPlayer NewPlayer()
        {
            drawOrder += 50; // CAObjCallBack ctor increments native DrawOrerIndex by 50.
            return new NativeCaPlayer(transform, Layer, drawOrder);
        }

        Effect NewEffect(int id)
        {
            var result = new Effect { player = NewPlayer(), id = id, insertion = effects.Count };
            result.player.Hide();
            result.player.animationEnd = () => Disappear(result);
            effects.Add(result);
            return result;
        }

        void Disappear(Effect effect)
        {
            if (!effect.player.active) return;
            effect.player.Hide(); pool.Push(effect);
        }

        public void Begin(int type)   // not named Start: MonoBehaviour.Start() may not take parameters (Player.log "Script error")
        {
            if (type != 4) { StartA2B(type); return; }
            HeroType = type;
            Atlas.LoadByConf("DC2/Image/ip/Player2x", "DC2/Image/ip/Player2x.conf");
            var animation = NativeComponentAnimation.Load("DC2/Image/acb/ipad/player_fire_long.animation.acb");
            ComponentHero = new NativeComponentPlayer(animation, Layer(10), new Vector2(-100f, -120f));
            // menuObjN::OnKeyFrame dispatch has cases1..4; type5 falls through.
        }

        public void StartA2B(int type)
        {
            if (type < 0 || type > 3) throw new ArgumentOutOfRangeException(nameof(type));
            HeroType = type;
            string[] heroAssets = { "load_p1_fire", "load_p2_fire", "load_p3_fire", "p4_fire" };
            string[] effectAssets = { "a2b/baozha", "an2/yuanli", "an2/qh2", "an2/feibiao" };
            int[] keys = { 3, 6, 3, 3 };
            var hero = NativeCaAnimation.Load("DC2/Image/an2/" + heroAssets[type] + ".a2b", true);
            hero.MarkKeyFrame(keys[type]);
            effectAnimation = NativeCaAnimation.Load("DC2/Image/" + effectAssets[type] + ".a2b", type != 0);
            if (type == 1) effectAnimation.SetSpriteAnimation("com15");
            Hero = NewPlayer(); Hero.position = new Vector2(-100f, 100f); // InitPlayer iPad local scale 2.
            Hero.keyFrame = OnKeyFrame;
            Hero.Play(hero, -1);
        }

        void OnKeyFrame(int frame)
        {
            Effect effect = pool.Count != 0 ? pool.Pop() : NewEffect(5);
            EffectsSpawned++;
            // menuObj::OnKeyFrame uses fixed world positions, not hero part locations.
            switch (HeroType)
            {
                case 0: effect.player.position = new Vector2(200f, 100f); break;
                case 1: effect.player.position = new Vector2((-125f * 2f) * 0.8f, 100f); break;
                default: effect.player.position = new Vector2(0f, -60f * 2f + 30f); break;
            }
            if (HeroType == 3) effect.velocityX = (300f * 2f) * 1.5f;
            effect.player.layer = HeroType == 1 ? 10 : 12;
            effect.player.Play(effectAnimation, HeroType == 3 ? -1 : 0);
        }

        public void Step(float dt)
        {
            if (ComponentHero != null) { ComponentHero.Step(dt); return; }
            if (Hero == null) return;
            // Menu's unshaped bodies: gravity=0, damping=0, physics scale=1.
            // The native shuriken velocity 900 is below Box2D's proven 2000 cap.
            // Physics runs before callbacks; a newly spawned projectile moves next step.
            foreach (var effect in effects)
                if (effect.player.active)
                    effect.player.position = new Vector2(effect.player.position.x + dt * effect.velocityX, effect.player.position.y);
            var snapshot = new List<Effect>(effects);
            snapshot.Sort((a, b) => a.id != b.id ? a.id.CompareTo(b.id) : a.insertion.CompareTo(b.insertion));
            // Hero object id=1 precedes pooled effects id=8 and overflow effects id=5.
            Hero.Step(dt);
            foreach (var effect in snapshot)
                if (effect.player.active)
                {
                    effect.player.Step(dt);
                    // loadEffClass checks the strict world-x threshold AFTER animation update.
                    if (effect.player.active && effect.player.position.x > 1000f) Disappear(effect);
                }
        }

        public static int SelectionFromNativeRand(int raw)
        {
            float f = (float)raw;
            f *= N.F(805306368); // 0x30000000 = 2^-31, separate native float operations.
            f *= 4f;
            return (int)f;
        }
    }
}
