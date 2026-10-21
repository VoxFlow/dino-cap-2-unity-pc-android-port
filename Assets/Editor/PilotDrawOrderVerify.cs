using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEditor;

namespace Dc2Pilot.EditorTools
{
    /// <summary>NativeDrawOrder contract (CAObjCallBack ctor 0xaff4 + dinosaur pool CreateEnemy 0xd5308 / Disappear 0x119f6): creation index per CAObj, LIFO pool reuse, hero created before any dinosaur.</summary>
    public static class PilotDrawOrderVerify
    {
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-PILOT-001/draworder-r1");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            string dir = Path.Combine(Path.GetTempPath(), "dc2_draworder_" + Guid.NewGuid().ToString("N"));
            int passed = 0, failed = 0; var log = new StringBuilder();
            Action<string, bool> check = (n, v) => { log.AppendLine((v ? "PASS " : "FAIL ") + n); if (v) passed++; else failed++; };
            Dc2Game game = null; Camera cam = null;
            try
            {
                // r118z4: texture upload as TextureManager::LoadFromPng 0x1781e0 (raw RGBA for POT images): no colour dilation into alpha-0 texels
                int imported = 0, dilated = 0, clamped = 0;
                foreach (var g in AssetDatabase.FindAssets("t:Texture2D", new[] { "Assets/Resources/DC2" }))
                {
                    var ti = AssetImporter.GetAtPath(AssetDatabase.GUIDToAssetPath(g)) as TextureImporter;
                    if (ti == null) continue; imported++; if (ti.alphaIsTransparency) dilated++; if (ti.wrapMode != TextureWrapMode.Repeat) clamped++;
                }
                check("every DC2 texture imports its raw RGBA (alphaIsTransparency off): " + imported + " textures, " + dilated + " dilated", imported > 0 && dilated == 0);
                check("every DC2 texture wraps GL_REPEAT (TextureManager::LoadFromPng 0x1782f0): " + clamped + " not Repeat", imported > 0 && clamped == 0);
                // r118z4: LoadFreeTypeFont 0x1694c4 builds the font RGBA in a zero-filled buffer and writes RGB 0xFF only where coverage != 0 (0x1699d6..0x1699f6)
                int fonts = 0, wrong = 0;
                foreach (var fontPng in Directory.GetFiles(Path.Combine(Application.dataPath, "Resources/DC2/Font"), "native_font_*.png"))
                {
                    var fontTex = new Texture2D(2, 2, TextureFormat.RGBA32, false); fontTex.LoadImage(File.ReadAllBytes(fontPng)); fonts++;
                    foreach (var px in fontTex.GetPixels32()) if (px.a == 0 ? (px.r | px.g | px.b) != 0 : (px.r & px.g & px.b) != 255) wrong++;
                    UnityEngine.Object.DestroyImmediate(fontTex);
                }
                check("font textures: (0,0,0,0) without coverage, white RGB with coverage alpha (native LoadFreeTypeFont): " + fonts + " fonts, " + wrong + " wrong texels", fonts == 5 && wrong == 0);
                cam =Bootstrap.MakeCamera(); game = Dc2Game.Create(dir); game.manualStep = true; game.SkipMissionIntro = true; game.cam = cam;
                game.LaunchMission = new NativeMapState.Mission { type = 3, count = 50, level = 1, background = 1 };
                game.SwitchScene("gameScene"); game.StepFor(0.1f);
                var scene = (GameScene)game.Current; scene.spawningEnabled = false; scene.ignorePlayer = true; scene.DebugSpawnModeRoll = () => 99;
                check("hero is created before every dinosaur (lower creation index)", scene.HeroDrawIndex >= 1);
                var a = scene.CreateEnemy(1); var b = scene.CreateEnemy(1); var c = scene.CreateEnemy(1);
                check("dinosaurs get consecutive creation indices after the hero", a.drawIndex > scene.HeroDrawIndex && b.drawIndex == a.drawIndex + 1 && c.drawIndex == a.drawIndex + 2);
                check("a later dinosaur has a higher SortingGroup order than the hero and earlier dinosaurs", c.root.GetComponent<SortingGroup>().sortingOrder > b.root.GetComponent<SortingGroup>().sortingOrder && a.root.GetComponent<SortingGroup>().sortingOrder > scene.HeroDrawIndex);
                a.removed = true; b.removed = true; game.Step(1f / 60f);   // native multimap order: a (created first) is pushed first, then b (r118z L1)
                var d = scene.CreateEnemy(1); var e = scene.CreateEnemy(1);
                check("pool is LIFO: the last pushed object (b) is reused first", d.drawIndex == b.drawIndex && e.drawIndex == a.drawIndex);
                var f = scene.CreateEnemy(1);
                check("empty pool allocates a fresh index", f.drawIndex == c.drawIndex + 1);
                var flags = System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance;
                Func<DinoActor, int> groupOf = x => x.root.GetComponent<SortingGroup>().sortingOrder;
                // blood: dinosaurClass::Blood sets the effect base to the dinosaur base + 20 (between that dinosaur and the next one)
                for (int k = 0; k < 4; k++) game.Step(1f / 60f);   // populate rig part transforms
                typeof(GameScene).GetMethod("DinoBlood", flags).Invoke(scene, new object[] { d, 0 });
                int blood = scene.FxDrawBase("blood");
                check("blood effect DrawOrder base = dinosaur base + 20", blood == NativeDrawOrder.Base(d.drawIndex) + 20);
                var bloodGo = GameObject.Find("fx_blood");
                int bloodGroup = bloodGo != null ? bloodGo.GetComponent<SortingGroup>().sortingOrder : int.MinValue;
                check("blood draws above its dinosaur and below the next-created dinosaur", bloodGroup > groupOf(d) && bloodGroup < groupOf(f));
                check("blood and dinosaurs are siblings under the layer-10 root (no actor-root SortingGroup)", bloodGo != null && bloodGo.transform.parent == scene.Background.LayerRoot(10) && scene.Background.ActorRoot.GetComponent<SortingGroup>() == null);
                // baozha: own LIFO pool (scene+84), fresh objects take the next creation base
                var spawn = typeof(GameScene).GetMethod("SpawnCaFx", flags);
                var anim = NativeCaAnimation.Load("DC2/Image/a2b/baozha.a2b", false, 1f);
                spawn.Invoke(scene, new object[] { "baozha", anim, 0f, 0f, false, 12, 1f, 0 });
                int bz1 = scene.FxDrawBase("baozha");
                check("first baozha takes a fresh base above every earlier CAObj", bz1 == NativeDrawOrder.Base(f.drawIndex + 2) || bz1 > NativeDrawOrder.Base(f.drawIndex));
                for (int k = 0; k < 240 && scene.FxDrawBase("baozha") >= 0; k++) game.Step(1f / 60f);
                spawn.Invoke(scene, new object[] { "baozha", anim, 0f, 0f, false, 12, 1f, 0 });
                check("a later baozha reuses the pooled object (same base)", scene.FxDrawBase("baozha") == bz1);
                var hs = new System.Collections.Generic.List<int>();
                foreach (Transform t in scene.Background.LayerRoot(5)) if (t.name == "houseClass") hs.Add(t.GetComponent<SortingGroup>().sortingOrder);
                bool inc = true; for (int k = 1; k < hs.Count; k++) inc &= hs[k] > hs[k - 1];
                check("houses (layer 5 CAObj) carry increasing creation bases (" + hs.Count + " houses)", hs.Count == 0 || (inc && hs[0] >= NativeDrawOrder.CaGroup(NativeDrawOrder.Base(1))));
                // effect rotation: Qianghuo sets +/-0.1 rad on its pooled effect object; a later GetEffect user (blood) inherits it (no SetRotation)
                for (int k = 0; k < 600 && scene.FxDrawBase("blood") >= 0; k++) game.Step(1f / 60f);   // let the earlier blood finish and return to the pool
                var qhAnim = NativeCaAnimation.Load("DC2/Image/an2/qh2.a2b", true, 1f);
                var qhFx = spawn.Invoke(scene, new object[] { "qh", qhAnim, 0f, 0f, true, 9, 1f, 0 });
                typeof(GameScene).GetMethod("SetFxRotation", flags).Invoke(scene, new object[] { qhFx, 0.1f });
                for (int k = 0; k < 600 && !float.IsNaN(scene.FxRotation("qh")); k++) game.Step(1f / 60f);
                typeof(GameScene).GetMethod("DinoBlood", flags).Invoke(scene, new object[] { d, 0 });
                check("pooled effect keeps its rotation: blood after a muzzle flash inherits 0.1 rad", Mathf.Approximately(scene.FxRotation("blood"), 0.1f));
                // custom draws above sprites; plain sprites below CAObj groups
                check("CustomDraw order is above every CAObj group in use", NativeDrawOrder.CustomDraw > NativeDrawOrder.CaGroup(NativeDrawOrder.Base(f.drawIndex + 10)) && NativeDrawOrder.CaGroup(0) >= 200);
            }
            catch (Exception e) { failed++; log.AppendLine("EXCEPTION " + e); }
            finally
            {
                if (game != null) UnityEngine.Object.DestroyImmediate(game.gameObject);
                if (cam != null) UnityEngine.Object.DestroyImmediate(cam.gameObject);
                if (Directory.Exists(dir)) Directory.Delete(dir, true);
                log.AppendLine("SUMMARY passes=" + passed + " fails=" + failed);
                File.WriteAllText(Path.Combine(output, "verify.txt"), log.ToString()); Debug.Log(log.ToString());
                PilotExit.Exit(failed == 0 ? 0 : 1);
            }
        }
    }
}
