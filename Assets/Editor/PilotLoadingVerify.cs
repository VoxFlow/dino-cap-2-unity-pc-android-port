using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>Focused native atlas/A2B regression checks and deterministic 60 Hz captures.
    /// Expected facts come from ARM and shipped raw bytes; captures do not prove original equality.
    /// </summary>
    public static class PilotLoadingVerify
    {
        static readonly StringBuilder log = new StringBuilder();
        static int passed, failed;
        static string output;
        static void Check(string name, bool ok)
        { log.AppendLine((ok ? "PASS " : "FAIL ") + name); if (ok) passed++; else failed++; }

        static void Capture(Camera cam, string name)
        {
            var rt = new RenderTexture(1024, 768, 24);
            var image = new Texture2D(1024, 768, TextureFormat.RGB24, false);
            try
            {
                cam.targetTexture = rt; Bootstrap.RenderAll(cam); RenderTexture.active = rt;
                image.ReadPixels(new Rect(0, 0, 1024, 768), 0, 0); image.Apply();
                File.WriteAllBytes(Path.Combine(output, name + ".png"), image.EncodeToPNG());
            }
            finally
            {
                cam.targetTexture = null; RenderTexture.active = null;
                UnityEngine.Object.DestroyImmediate(rt); UnityEngine.Object.DestroyImmediate(image);
            }
        }

        public static void Run()
        {
            var args = Environment.GetCommandLineArgs();
            output = Path.Combine(Application.dataPath, "..", "Reports", "DC2-PILOT-001", "loading-r3");
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            Dc2Game game = null; Camera cam = null;
            string save = Path.Combine(Path.GetTempPath(), "dc2_loading_verify_" + Guid.NewGuid().ToString("N"));
            try
            {
                cam = Bootstrap.MakeCamera();
                game = Dc2Game.Create(save); game.manualStep = true; game.SkipMissionIntro = true; game.cam = cam;
                game.SwitchScene("menuScene"); game.StepFor(5.8f);
                Check("native loading frame dimensions 230x44 (ARM 0x7ee2e)", Atlas.FrameSize("loadingFrame") == new Vector2Int(230, 44));
                Check("native options title dimensions 332x91 (ARM 0x80356)", Atlas.FrameSize("op_titleFrame") == new Vector2Int(332, 91));
                Check("native Sound and Music sprites recovered", Atlas.FrameSize("op_soundFrame") == new Vector2Int(212, 59) && Atlas.FrameSize("op_musicFrame") == new Vector2Int(212, 59));
                Check("native percent sprite 22x23 (ARM 0x7eb72)", Atlas.FrameSize("%Frame") == new Vector2Int(22, 23));
                Check("float rand conversion preserves upper case4", LoadingAnimation.SelectionFromNativeRand(int.MaxValue) == 4);
                Check("float rand conversion lower cases", LoadingAnimation.SelectionFromNativeRand(0) == 0 && LoadingAnimation.SelectionFromNativeRand(1073741824) == 2);
                var pivot = NativeCaPlayer.SpriteCenter(new Vector2(10, 20), new Vector2(3, 4), Mathf.PI * 0.5f);
                Check("native rotation is CCW around pivot, not position offset", Vector2.Distance(pivot, new Vector2(17, 21)) < 0.0001f);
                Check("zero rotation ignores pivot", NativeCaPlayer.SpriteCenter(new Vector2(10, 20), new Vector2(3, 4), 0) == new Vector2(10, 20));
                foreach (var control in game.Current.Controls) control.SetVisible(false);
                foreach (var label in game.Current.Labels) label.gameObject.SetActive(false);
                int[] frameCounts = { 10, 20, 9, 15 }, keys = { 3, 6, 3, 3 };
                for (int type = 0; type < 4; type++)
                {
                    var animation = LoadingAnimation.Create(game.Current.transform);
                    animation.StartA2B(type);
                    Check("hero " + type + " shipped frame count", animation.Hero.animation.frames.Length == frameCounts[type]);
                    Check("hero " + type + " native object position", animation.Hero.position == new Vector2(-100, 100));
                    if (type == 0)
                    {
                        var part = animation.Hero.animation.frames[0].parts[0];
                        Check("yingzi normalized A2B dimensions resolve native atlas 98x14", part.reference == "yingzi" && part.w == 98 && part.h == 14);
                    }
                    // Native render-before-advance: frame k first emitted on tick 2*k+1.
                    for (int i = 0; i < keys[type] * 2; i++) animation.Step(1f / 60f);
                    Check("hero " + type + " no early effect callback", animation.EffectsSpawned == 0);
                    animation.Step(1f / 60f);
                    Check("hero " + type + " keyframe emits effect before advancing", animation.EffectsSpawned == 1 && animation.Hero.DisplayedFrame == keys[type]);
                    Capture(cam, "hero" + (type + 1) + "_keyframe");
                    for (int i = 0; i < frameCounts[type] * 2; i++) animation.Step(1f / 60f);
                    Check("hero " + type + " callback repeats once per native loop", animation.EffectsSpawned == 2);
                    UnityEngine.Object.DestroyImmediate(animation.gameObject);
                }
                var girl = LoadingAnimation.Create(game.Current.transform); girl.Begin(4);
                Check("rare fifth loading route preserves shipped 22-frame ACB", girl.ComponentHero.animation.FrameCount == 22 && girl.ComponentHero.animation.MaxNumber == 21f);
                Check("fifth hero native object position (-100,-120)", girl.ComponentHero.position == new Vector2(-100f, -120f));
                Check("ACB frame-rate factor is 21/21", girl.ComponentHero.animation.FrameRateFactor == 1f);
                girl.Step(0.2f);
                Check("ACB subtracts interval and advances multiple frames", girl.ComponentHero.CurrentFrame == 6 && girl.ComponentHero.Accumulator < 1f / 30f);
                Capture(cam, "hero5_component_frame6");
                UnityEngine.Object.DestroyImmediate(girl.gameObject);
                var slow = LoadingAnimation.Create(game.Current.transform); slow.StartA2B(0); slow.Step(0.2f);
                Check("long dt advances at most one CA frame and discards overshoot", slow.Hero.CurrentFrame == 1 && slow.Hero.Accumulator == 0f && slow.Hero.DisplayedFrame == 0);
                UnityEngine.Object.DestroyImmediate(slow.gameObject);
                // Drive the production tap path separately from isolated animation captures.
                game.SwitchScene("menuScene"); game.Step(1f / 60f); game.StepFor(5.8f);
                var menu = (MenuScene)game.Current;
                menu.Touch(0, new Vector2(512, 580));
                for (int i = 0; i < 42; i++) game.Step(1f / 60f);
                Check("production tap starts recovered loading animation", menu.heroAnimStarted && menu.loadingAnimation != null);
                Capture(cam, "production_loading_at_0p7s");
                game.SwitchScene("selectScene"); game.Step(1f / 60f); game.StepFor(0.5f);
                var select = (SelectScene)game.Current;
                // Independent source: initialized floats 0x23b914/18 and RectOffset ARM 0x170b6.
                Check("native offsets are64 in both axes", N.WOffset == 64f && N.HOffset == 64f);
                int[] buttonIds = {54, 93, 95, 55};
                foreach (int id in buttonIds) Check("native map button " + id + " y574", Mathf.Abs(select.GetControl(id).CurrentRect.y - 574f) < 0.01f);
                Check("native player panel y44", Mathf.Abs(select.GetControl(59).CurrentRect.y - 44f) < 0.01f);
                Capture(cam, "map_native_offsets");
                select.OnOptionEnter(); game.StepFor(0.5f);
                Check("native percent is separate id90 sprite", select.GetControl(90)?.FrameName == "%Frame");
                string percentage = null; foreach (var label in select.Labels) if (label.id == 63) percentage = label.Text;
                Check("percent number label excludes sprite glyph", percentage == "0");
                Check("options use native Sound/Music controls", select.GetControl(82)?.FrameName == "op_soundFrame" && select.GetControl(80)?.FrameName == "op_musicFrame");
                Capture(cam, "options_native_frames");
            }
            catch (Exception e) { failed++; log.AppendLine("EXCEPTION " + e); }
            finally
            {
                if (game != null) UnityEngine.Object.DestroyImmediate(game.gameObject);
                if (cam != null) UnityEngine.Object.DestroyImmediate(cam.gameObject);
                if (Directory.Exists(save)) Directory.Delete(save, true);
                log.AppendLine("SUMMARY passes=" + passed + " fails=" + failed);
                File.WriteAllText(Path.Combine(output, "verify.txt"), log.ToString());
                Debug.Log(log.ToString());
                PilotExit.Exit(failed == 0 ? 0 : 1);
            }
        }
    }
}
