using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Dc2Pilot
{
    [Serializable] public sealed class BackgroundChildDef
    {
        public string frame;
        public float width, height;
        public int layer;
    }
    [Serializable] public sealed class BackgroundLayerDef
    {
        public int slot;
        public float x, y, period, containerHeight, walkVelocityFactor;
        public BackgroundChildDef[] children;
    }
    [Serializable] public sealed class BackgroundVariantDef { public int id; public BackgroundLayerDef[] layers; }
    [Serializable] sealed class BackgroundTable { public BackgroundVariantDef[] variants; }
    [Serializable] sealed class BackgroundMusicDef { public int id; public string key, resource; }
    [Serializable] sealed class BackgroundMusicTable { public BackgroundMusicDef[] entries; }

    /// <summary>Native lifeModeScene InitGameBackGround + bglayerClass OnUpdate.
    /// Recovered neutral IR: Dev/Recovered/Backgrounds/background_spec.r19.json.
    /// Geometry comes from ARM construction; signed wrapping and child flow from700c.
    /// Walk velocity factors are bounded to UpdateControl's calls(1,false)/(3,false).
    /// Camera bounds, easing and other caller lifecycles remain OPEN in GameScene.
    /// </summary>
    public sealed class NativeBackground
    {
        public sealed class Layer
        {
            public BackgroundLayerDef definition;
            public float x;
            public readonly List<SpriteRenderer> children = new List<SpriteRenderer>();
            public readonly Dictionary<int, SpriteRenderer[]> repeats = new Dictionary<int, SpriteRenderer[]>();   // ADAPTATION WideView: chain copies keyed by offset k
        }
        public readonly List<Layer> layers = new List<Layer>();
        public readonly int variant;
        Transform[] renderLayers;
        public Transform LayerRoot(int layer) { return renderLayers[layer]; }
        public string MusicResource { get; private set; }
        public Transform ActorRoot { get; private set; }
        public Transform Root { get; private set; }

        public NativeBackground(Transform parent, int background)
        {
            var table = JsonUtility.FromJson<BackgroundTable>(Resources.Load<TextAsset>("DC2/background_spec.json").text);
            BackgroundVariantDef selected = null;
            foreach (var candidate in table.variants) if (candidate.id == background) selected = candidate;
            if (selected == null) throw new ArgumentOutOfRangeException(nameof(background), "Native background variant is not recovered");
            variant = background;
            var music = JsonUtility.FromJson<BackgroundMusicTable>(Resources.Load<TextAsset>("DC2/background_music.json").text);
            foreach (var entry in music.entries) if (entry.id == variant) MusicResource = entry.resource;
            if (MusicResource == null) throw new InvalidOperationException("Native background music pairing is missing");
            var world = new GameObject("native_world"); world.transform.SetParent(parent, false);
            // PLATFORM_ADAPTER: Scene renders world before UI. Nested groups retain native layer0..15 order.
            world.AddComponent<SortingGroup>().sortingOrder = -1; Root = world.transform;
            renderLayers = new Transform[16];
            for (int i = 0; i < renderLayers.Length; i++)
            {
                var go = new GameObject("native_render_layer_" + i); go.transform.SetParent(Root, false);
                go.AddComponent<SortingGroup>().sortingOrder = i; renderLayers[i] = go.transform;
            }
            var actors = new GameObject("actors"); actors.transform.SetParent(renderLayers[10], false);
            // No SortingGroup here: hero, dinosaur, blood and other layer-10 CAObj groups must be siblings so they sort by DrawOrder together (NativeDrawOrder; RenderManager mode 5).
            ActorRoot = actors.transform;
            foreach (var definition in selected.layers)
            {
                var layer = new Layer { definition = definition, x = definition.x };
                foreach (var child in definition.children)
                {
                    var go = new GameObject(child.frame); go.transform.SetParent(renderLayers[child.layer], false);
                    var renderer = go.AddComponent<SpriteRenderer>(); renderer.sprite = Atlas.Get(child.frame);
                    renderer.sortingOrder = child.layer; // PLATFORM_ADAPTER: native draw order by CreateGameObject layer (ground layer2 over sky layer1); Unity did not honour the nested SortingGroup order alone (bgdump a_normal). Equal-layer material sorting remains OPEN.
                    var size = renderer.sprite.rect.size;
                    go.transform.localScale = new Vector3(child.width / size.x, child.height / size.y, 1);
                    layer.children.Add(renderer);
                }
                layers.Add(layer);
            }
        }

        public static float WrappedX(float x, float period, float cameraLeft)
        {
            int delta = unchecked((int)cameraLeft - (int)x), width = (int)period;
            if (delta < 0) delta = unchecked(delta - width);
            int quotient = delta / width; // ARM signed division truncates towards zero, including negative exact-boundary quirk.
            return quotient == 0 ? x : N.Add32(x, N.Mul32(period, N.F32(quotient)));
        }

        public void StepVelocity(float heroVelocity, float dt)
        {
            foreach (var layer in layers)
                layer.x = N.Add32(layer.x, N.Mul32(N.Mul32(heroVelocity, layer.definition.walkVelocityFactor), dt));
        }

        public void Place(float cameraLeft, float cameraWidth)
        {
            foreach (var layer in layers)
            {
                layer.x = WrappedX(layer.x, layer.definition.period, cameraLeft);
                float center = 0, previousHalf = 0;
                for (int i = 0; i < layer.children.Count; i++)
                {
                    float half = N.Mul32(layer.definition.children[i].width, .5f);
                    center = N.Add32(N.Add32(center, i == 0 ? 0 : half), i == 0 ? half : previousHalf);
                    previousHalf = half;
                    float x = N.Add32(layer.x, center);
                    layer.children[i].transform.localPosition = new Vector3(N.ScreenW * .5f + x - (cameraLeft + cameraWidth * .5f),
                        -N.ScreenH * .5f + layer.definition.y, 0);
                }
            }
        }

        /// <summary>ADAPTATION (WideView, DC2-BUGS-001 O1), render only, called after Place: the native chain covers [layer.x, layer.x + chain length) and its
        /// content repeats with the native wrap period (every recovered chain length is a whole multiple of it), so copies at k * chain length show exactly
        /// what the next native wrap would show. extraHalf 0 (off / 4:3 / menus) hides every copy. layer.x and the native children are not touched.</summary>
        public void PlaceWide(float cameraLeft, float cameraWidth, float extraHalf)
        {
            float viewL = cameraLeft - extraHalf, viewR = cameraLeft + cameraWidth + extraHalf;
            foreach (var layer in layers)
            {
                float chain = 0f; foreach (var c in layer.definition.children) chain += c.width;
                int kMin = 0, kMax = 0;
                if (extraHalf > 0f && chain > 0f)
                {
                    kMin = -Mathf.CeilToInt((layer.x - viewL) / chain);
                    kMax = Mathf.CeilToInt((viewR - (layer.x + chain)) / chain);
                    if (kMin > 0) kMin = 0; if (kMax < 0) kMax = 0;
                }
                foreach (var pair in layer.repeats) if (pair.Key < kMin || pair.Key > kMax) foreach (var r in pair.Value) r.enabled = false;
                for (int k = kMin; k <= kMax; k++)
                {
                    if (k == 0) continue;
                    SpriteRenderer[] copy;
                    if (!layer.repeats.TryGetValue(k, out copy))
                    {
                        copy = new SpriteRenderer[layer.children.Count];
                        for (int i = 0; i < copy.Length; i++)
                        {
                            var src = layer.children[i];
                            var go = new GameObject(src.gameObject.name + "_wide" + k); go.transform.SetParent(src.transform.parent, false);
                            var r = go.AddComponent<SpriteRenderer>(); r.sprite = src.sprite; r.sortingOrder = src.sortingOrder; r.color = src.color;
                            go.transform.localScale = src.transform.localScale;
                            copy[i] = r;
                        }
                        layer.repeats[k] = copy;
                    }
                    for (int i = 0; i < copy.Length; i++)
                    {
                        var src = layer.children[i];
                        copy[i].enabled = src.enabled;
                        copy[i].transform.localPosition = src.transform.localPosition + new Vector3(k * chain, 0f, 0f);
                    }
                }
            }
        }
    }
}
