using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    public static class PilotBackgroundVerify
    {
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-PILOT-001/background-r19");
            var args = Environment.GetCommandLineArgs(); for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            string dir = Path.Combine(Path.GetTempPath(), "dc2_background_" + Guid.NewGuid().ToString("N"));
            int passed = 0, failed = 0; var log = new StringBuilder();
            Action<string, bool> check = (name, ok) => { log.AppendLine((ok ? "PASS " : "FAIL ") + name); if (ok) passed++; else failed++; };
            Dc2Game game = null; Camera camera = null; RenderTexture target = null; Texture2D pixels = null;
            try
            {
                camera = Bootstrap.MakeCamera(); game = Dc2Game.Create(dir); game.manualStep = true; game.SkipMissionIntro = true; game.cam = camera;
                target = new RenderTexture(1024, 768, 24); pixels = new Texture2D(1024, 768, TextureFormat.RGB24, false);
                Action<string> shot = name => { camera.targetTexture = target; camera.Render(); RenderTexture.active = target;
                    pixels.ReadPixels(new Rect(0, 0, 1024, 768), 0, 0); pixels.Apply(); File.WriteAllBytes(Path.Combine(output, name), pixels.EncodeToPNG());
                    camera.targetTexture = null; RenderTexture.active = null; };
                int[] variants = { 0, 1, 2, 3, 4, 5, 7, 8 };
                string[] first = { "beijing_1Frame", "beijing2_1Frame", "back_endFrame", "beijing3_1Frame", "beijing4_1Frame", "beijing5_1Frame", "beijing7_sub1Frame", "beijing8_sub2Frame" };
                int[] layerCounts = { 2, 3, 2, 3, 3, 3, 2, 2 };
                float[] firstWidths = { 512, 960, 852, 960, 960, 960, 960, 1024 };
                string[] musicNames = { "City", "Shamo", "Conglin", "Renzhe", "Xingzhan", "Shendan", "Conglin", "Boss" };
                for (int index = 0; index < variants.Length; index++)
                {
                    int variant = variants[index];
                    game.LaunchMission = new NativeMapState.Mission { type = variant == 7 ? 7 : variant == 8 ? 8 : 2, count = 180, background = variant >= 7 ? 1 : variant };
                    game.SwitchScene("gameScene"); game.Step(1f / 60f);
                    var scene = (GameScene)game.Current; scene.spawningEnabled = false;
                    var bg = scene.Background;
                    check("variant" + variant + " selected from mission and native layer count", bg.variant == variant && bg.layers.Count == layerCounts[index]);
                    check("variant" + variant + " native first frame/width", bg.layers[0].children[0].sprite.name == first[index] && bg.layers[0].definition.children[0].width == firstWidths[index]);
                    check("variant" + variant + " native background audio resource loaded", game.Audio.LastMusicStarted == "Music_Bg_Audio" && game.Audio.LastMusicResource == "DC2/Audio/Music_Bg_" + musicNames[index]);
                    bool flow = true;
                    foreach (var layer in bg.layers)
                    {
                        for (int i = 1; i < layer.children.Count; i++)
                        {
                            float delta = layer.children[i].transform.localPosition.x - layer.children[i - 1].transform.localPosition.x;
                            float expected = (layer.definition.children[i].width + layer.definition.children[i - 1].width) * .5f;
                            flow &= Mathf.Abs(delta - expected) < .001f;
                        }
                    }
                    check("variant" + variant + " native half-width child flow", flow);
                    shot("background_" + variant + ".png");
                    if (variant == 2)
                    {
                        check("arena foreground has three children on native layer12", bg.layers[1].children.Count == 3 && bg.layers[1].definition.children[0].layer == 12);
                        var shown = pixels.GetPixels32(); foreach (var sprite in bg.layers[1].children) sprite.enabled = false;
                        shot("arena_without_foreground.png"); var hidden = pixels.GetPixels32(); int changed = 0;
                        for (int p = 0; p < shown.Length; p++) if (!shown[p].Equals(hidden[p])) changed++;
                        check("arena foreground contributes rendered pixels", changed > 0); log.AppendLine("ARENA_FOREGROUND_PIXELS " + changed);
                        foreach (var sprite in bg.layers[1].children) sprite.enabled = true;
                    }
                    if (variant == 5)
                    {
                        var front = bg.layers[2];
                        check("variant5 native period1700 and alternating952/748 widths", front.definition.period == 1700 && front.definition.children[0].width == 952 && front.definition.children[1].width == 748);
                        check("variant5 native frame order3_2 then3_1", front.children[0].sprite.name == "beijing5_3_2Frame" && front.children[1].sprite.name == "beijing5_3_1Frame");
                    }
                    float before0 = bg.layers[0].x;
                    float beforeLast = bg.layers[bg.layers.Count - 1].x;
                    bg.StepVelocity(300, 1);
                    check("variant" + variant + " walk caller slot velocity factors", Mathf.Abs(bg.layers[0].x - before0 - (variant == 2 || variant >= 7 ? 0 : variant == 4 ? 300 : 225)) < .001f &&
                        Mathf.Abs(bg.layers[bg.layers.Count - 1].x - beforeLast - (variant == 0 ? 0 : -60)) < .001f);
                    bg.Place(-480, 960); shot("background_" + variant + "_scroll.png");
                }
                check("negative exact-width boundary preserves native signed division quirk", NativeBackground.WrappedX(0, 1024, -1024) == -2048);
                check("zero boundary has no native wrap", NativeBackground.WrappedX(-480, 1024, -480) == -480);
                check("positive two-period boundary wraps twice", NativeBackground.WrappedX(-480, 1024, 1568) == 1568);
                check("frame registrations come from native arena crop", Atlas.FrameSize("back_endFrame") == new Vector2Int(850, 639) && Atlas.FrameSize("back_frontFrame") == new Vector2Int(850, 252));
            }
            catch (Exception ex) { failed++; log.AppendLine("EXCEPTION " + ex); }
            finally
            {
                if (target != null) UnityEngine.Object.DestroyImmediate(target); if (pixels != null) UnityEngine.Object.DestroyImmediate(pixels);
                if (game != null) UnityEngine.Object.DestroyImmediate(game.gameObject); if (camera != null) UnityEngine.Object.DestroyImmediate(camera.gameObject);
                if (Directory.Exists(dir)) Directory.Delete(dir, true);
                log.AppendLine("SUMMARY passes=" + passed + " fails=" + failed); File.WriteAllText(Path.Combine(output, "verify.txt"), log.ToString()); Debug.Log(log.ToString());
                PilotExit.Exit(failed == 0 ? 0 : 1);
            }
        }
    }
}
