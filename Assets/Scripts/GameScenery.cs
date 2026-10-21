using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dc2Pilot
{
    [Serializable] public sealed class SceneryObject { public string frame; public float w, h, x, y; public int layer, order; }
    [Serializable] public sealed class SceneryTemplate { public SceneryObject[] objects; public float[] jitter; }
    [Serializable] public sealed class SceneryTheme
    {
        public int arg; public float stride, countOffset, countDivisor; public int countPlus, jitterRange, patternRange;
        public SceneryTemplate[] templates;
    }
    [Serializable] sealed class SceneryTable { public SceneryTheme[] themes; }

    /// <summary>
    /// mainScene::InitBackGround 0x9aff2 (run / egg missions): decorative scenery GameObjects laid out along the run in addition to the parallax layers.
    /// NATIVE, recovered by bounded static emulation of the ARM (Dev/Tools/recover_backgrounds_main.py, explore_main_bg.py, derive_main_bg.py; spec
    /// Dev/Recovered/Backgrounds/mainscene_scenery_spec.r118.json, validated against direct emulation with random draws: 112 trials, 0 mismatches).
    /// mainScene::OnLoad calls InitBackGround(this, bg) for the mission backgrounds 1, 3, 4 and 5 (the only four call sites, xrefs of 0x9aff2: 0xb29d8 / 0xb4688 / 0xb5114 / 0xb5d46;
    /// confirmed by bounded emulation of mainScene::OnLoad per bg: bg 0 and 2 make no call). lifeModeScene::InitGameBackGround calls its own copy lifeModeScene::InitBackGround 0xd6a0e
    /// (same size, 20/20 emulation comparisons identical to the mainScene one) with the same arguments 1, 3, 4, 5 (call sites 0xdd45a / 0xdea54 / 0xdf334 / 0xdfdea); variants 7 and 8 (arena, boss) have none.
    /// scene+140 at that time: mainScene num*S (min 20000), lifeModeScene 50000.0 (OnLoad emulation: +136 = -240.0, +140 = 50000.0 when InitGameBackGround is called).
    /// Group count = int((offset + scene+140) / divisor) + plus (offset / divisor: theme 1 380 / 960, theme 4 380 / 1360, theme 5 0 / 1360; theme 3: 380 / 960 without +1);
    /// the draws are the same Triniti2D::RandomRange(lo, hi) calls in the same order as the native loop (the port's RandomRange has the same exclusive-upper quirk as the native one).
    /// Objects are GameObjects fixed in world space (they scroll 1:1 with the camera); positions are sprite centres in the port's world frame (y up from the screen centre).
    /// Draw order: layers 1/2/4 are RenderManager layer mode 0 (Scene::SetLayerMode(5,10,12 -> mode 5 DrawOrder sort); all other layers mode 0): per layer ascending (texture id, blend), then AddSprite order = creation order (std::set of GameObject pointers: LIKELY).
    /// </summary>
    public partial class GameScene
    {
        sealed class SceneryActor { public SpriteRenderer renderer; public float x, y, w, h; }
        readonly List<SceneryActor> scenery = new List<SceneryActor>();
        static SceneryTable sceneryTable;

        public int SceneryCount { get { return scenery.Count; } }
        public SpriteRenderer SceneryRenderer(int i) { return scenery[i].renderer; }
        public Vector2 SceneryPosition(int i) { return new Vector2(scenery[i].x, scenery[i].y); }

        void BuildScenery(int background)
        {
            if (sceneryTable == null) sceneryTable = JsonUtility.FromJson<SceneryTable>(Resources.Load<TextAsset>("DC2/mainscene_scenery_spec.json").text);
            SceneryTheme theme = null;
            foreach (var t in sceneryTable.themes) if (t.arg == background) theme = t;
            if (theme == null) return;
            int count = (int)N.Div32(N.Add32(theme.countOffset, BoundUpper), theme.countDivisor) + theme.countPlus;
            for (int g = 0; g < count; g++)
            {
                float baseX = N.Mul32(N.F32(g), theme.stride);
                if (theme.arg == 3)
                {   // cycle A, B, C, B; draws per group: A (0,1),(0,0)  B (0,1),(0,0),(1,3)  C (0,1),(0,0)
                    int type = g % 4, index;
                    if (type == 0 || type == 2) { RandomRange(0, 1); RandomRange(0, 0); index = type == 0 ? 0 : 1; }
                    else { int v = RandomRange(0, 1); RandomRange(0, 0); int s = RandomRange(1, 3); index = 2 + Mathf.Clamp(v, 0, 1) * 3 + Mathf.Clamp(s, 1, 3) - 1; }
                    AddSceneryGroup(theme.templates[index], baseX, 0f, 0);
                }
                else
                {
                    int p = Mathf.Clamp(RandomRange(0, theme.patternRange - 1), 0, theme.patternRange - 1);
                    int j = RandomRange(0, theme.jitterRange);
                    AddSceneryGroup(theme.templates[p], baseX, j, 1);
                }
            }
        }

        void AddSceneryGroup(SceneryTemplate t, float baseX, float jitterUnits, int useJitter)
        {
            for (int i = 0; i < t.objects.Length; i++)
            {
                var o = t.objects[i];
                float x = N.Add32(o.x, baseX);
                if (useJitter != 0 && t.jitter != null && i < t.jitter.Length) x = N.Add32(x, N.Mul32(t.jitter[i], jitterUnits));
                var go = new GameObject("scenery_" + o.frame); go.transform.SetParent(Background.LayerRoot(o.layer), false);
                var r = go.AddComponent<SpriteRenderer>();
                if (Atlas.Has(o.frame)) r.sprite = Atlas.Get(o.frame);
                // RenderManager::DoRenderSprite mode 0 (layers other than 5 / 10 / 12): sprites of a layer are drawn per Material (texture id, blend) in ascending key order, then in AddSprite order.
                r.sortingOrder = 100 + TextureRank(r.sprite) * 1000 + scenery.Count;
                scenery.Add(new SceneryActor { renderer = r, x = x, y = o.y, w = o.w, h = o.h });
            }
        }

        // Texture creation order of the background atlases (bounded emulation of the lifeModeScene::OnLoad registration phase, Dev/Tools/recover_backgrounds.py --registrations):
        // 1: beijing2_h, beijing2_sub_h; 3: beijing3_h, beijing3_sub_h, beijing3_sub1_h; 4: beijing4_h, beijing4_sub_h. The Material key is the frame's texture id (GL name, assumed ascending in creation order: LIKELY).
        static readonly string[] textureCreationOrder = { "beijing2_h", "beijing2_sub_h", "beijing3_h", "beijing3_sub_h", "beijing3_sub1_h", "beijing4_h", "beijing4_sub_h", "beijing5_h", "beijing5_h_1" };
        static int TextureRank(Sprite s)
        {
            if (s == null || s.texture == null) return 0;
            int i = System.Array.IndexOf(textureCreationOrder, s.texture.name);
            return i < 0 ? 0 : i;
        }

        void PlaceScenery()
        {
            if (scenery.Count == 0) return;
            float camCenterX = N.Add32(camLeft, N.Mul32(WorldViewportW, 0.5f));
            foreach (var a in scenery)
            {   // far objects are switched off (render cull only: positions are not changed)
                float dx = N.Add32(a.x, -camCenterX);
                if (Mathf.Abs(dx) > 2000f + Mathf.Max(0f, WideView.ExtraHalf)) { a.renderer.enabled = false; continue; }   // + WideView extra width (ADAPTATION, render only)
                Place(a.renderer, N.Add32(N.Mul32(N.ScreenW, 0.5f), dx), N.Add32(N.Mul32(N.ScreenH, -0.5f), a.y), a.w, a.h);
            }
        }
    }
}
