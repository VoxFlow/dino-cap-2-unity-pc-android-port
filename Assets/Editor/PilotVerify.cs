using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>
    /// Headless logic/state verification of the startup -> title -> menu -> options slice
    /// (editor batchmode: -executeMethod Dc2Pilot.EditorTools.PilotVerify.Run -reportDir <dir>).
    /// It proves logic, timing and layout numbers; it does NOT prove visual correctness or feel.
    /// </summary>
    public static class PilotVerify
    {
        static StringBuilder sb;
        static int fails, passes;
        static string reportDir;

        static void Check(string name, bool ok, string detail = "")
        {
            if (ok) passes++; else fails++;
            sb.AppendLine((ok ? "PASS  " : "FAIL  ") + name + (detail.Length > 0 ? "  [" + detail + "]" : ""));
        }

        static bool Near(float a, float b, float eps = 0.6f) { return Mathf.Abs(a - b) <= eps; }
        static bool RectNear(Rect r, float x, float y, float w, float h, float eps = 0.6f)
        { return Near(r.x, x, eps) && Near(r.y, y, eps) && Near(r.width, w, eps) && Near(r.height, h, eps); }
        static string Fmt(Rect r) { return string.Format("({0:F1},{1:F1},{2:F1},{3:F1})", r.x, r.y, r.width, r.height); }

        static void Shot(Camera cam, string name)
        {
            try
            {
                string dir = Path.Combine(reportDir, "shots");
                Directory.CreateDirectory(dir);
                var rt = new RenderTexture(1024, 768, 24);
                cam.targetTexture = rt; Bootstrap.RenderAll(cam);
                RenderTexture.active = rt;
                var tex = new Texture2D(1024, 768, TextureFormat.RGB24, false);
                tex.ReadPixels(new Rect(0, 0, 1024, 768), 0, 0); tex.Apply();
                File.WriteAllBytes(Path.Combine(dir, name + ".png"), tex.EncodeToPNG());
                cam.targetTexture = null; RenderTexture.active = null;
                UnityEngine.Object.DestroyImmediate(rt); UnityEngine.Object.DestroyImmediate(tex);
                sb.AppendLine("SHOT  " + name + ".png");
            }
            catch (Exception e) { sb.AppendLine("SHOT-FAILED " + name + ": " + e.Message); }
        }

        static void Click(Dc2Scene s, UiControl c)
        {
            Vector2 p = c.FullRect.center;
            s.Touch(0, p); s.Touch(2, p);
        }

        static string Arg(string key)
        {
            var a = Environment.GetCommandLineArgs();
            for (int i = 0; i < a.Length - 1; i++) if (a[i] == key) return a[i + 1];
            return null;
        }

        public static void Run()
        {
            sb = new StringBuilder();
            reportDir = Arg("-reportDir") ?? Path.Combine(Application.dataPath, "..", "VerifyReport");
            Directory.CreateDirectory(reportDir);
            int code = 0;
            try { Execute(); }
            catch (Exception e) { sb.AppendLine("EXCEPTION " + e); fails++; }
            sb.AppendLine(string.Format("\nSUMMARY passes={0} fails={1}", passes, fails));
            File.WriteAllText(Path.Combine(reportDir, "verify.txt"), sb.ToString());
            Debug.Log(sb.ToString());
            code = fails == 0 ? 0 : 1;
            PilotExit.Exit(code);
        }

        static void Execute()
        {
            string tmp = Path.Combine(Path.GetTempPath(), "dc2pilot_verify_" + Guid.NewGuid().ToString("N"));
            var cam = Bootstrap.MakeCamera();
            var game = Dc2Game.Create(tmp);
            game.manualStep = true; game.SkipMissionIntro = true; game.cam = cam;
            Atlas.Reset();

            // ---------------- splash + title ----------------
            game.SwitchScene("menuScene");
            var menu = (MenuScene)game.Current;
            var splash = menu.GetControl(35);
            Check("splash control id35 exists", splash != null);
            Check("splash rect = (0,0,1024,768)", splash != null && RectNear(splash.CurrentRect, 0, 0, 1024, 768));
            Shot(cam, "01_splash");
            game.StepFor(4.9f);
            Check("splash still shown at 4.9 s", menu.GetControl(35) != null);
            game.StepFor(0.12f);
            Check("splash removed after 5.0 s (startUI)", menu.GetControl(35) == null);
            var bg = menu.GetControl(8);
            Check("title bg id8 rect = (0,32,1024,704)", bg != null && RectNear(bg.CurrentRect, 0, 32, 1024, 704), bg != null ? Fmt(bg.CurrentRect) : "null");
            Check("menu music started (default Music ON)", game.Audio.LastMusicStarted == "Music_menuAudio");
            game.StepFor(0.25f);   // >0.2 s after InitUI -> LogoEnter fired
            var dino = menu.GetControl(0);
            Check("dino logo created at +0.2 s and tweening", dino != null && dino.IsMoving);
            game.StepFor(0.30f);
            dino = menu.GetControl(0);
            Check("dino logo final rect = (88,86,846,322)", dino != null && RectNear(dino.CurrentRect, 88, 86, 846, 322), dino != null ? Fmt(dino.CurrentRect) : "null");
            game.StepFor(0.30f);
            var start = menu.GetControl(1);
            Check("tap-to-start final rect = (270,546,484,56)", start != null && RectNear(start.CurrentRect, 270, 546, 484, 56), start != null ? Fmt(start.CurrentRect) : "null");
            Check("ready flag set after pop-in completes", menu.ready);
            float minA = 999, maxA = -999;
            for (int i = 0; i < 360; i++) { game.Step(1f / 60f); minA = Mathf.Min(minA, menu.alpha); maxA = Mathf.Max(maxA, menu.alpha); }
            Check("pulse alpha range ~80..250 (+overshoot < 1 frame step)", minA > 74f && minA <= 80f && maxA >= 250f && maxA < 256f, "min=" + minA + " max=" + maxA);
            Shot(cam, "02_title");

            // ---------------- tap flow ----------------
            Vector2 sp = start.FullRect.center;
            menu.Touch(0, sp);
            Check("tap starts ButtonEffect overlay id23 and state=1", menu.GetControl(23) != null && menu.state == 1);
            float elapsed = 0f; bool switched = false;
            for (int i = 0; i < 60 * 6; i++)
            {
                game.Step(1f / 60f); elapsed += 1f / 60f;
                if (i == 20) Shot(cam, "03_exit_anim");
                if (game.CurrentName == "selectScene") { switched = true; break; }
            }
            Check("loading finished -> selectScene", switched);
            Check("tap -> selectScene takes about 1.25 s", elapsed > 1.15f && elapsed < 1.45f, elapsed.ToString("F3"));
            menu = null;

            // ---------------- map screen ----------------
            var sel = (SelectScene)game.Current;
            game.StepFor(1.0f);
            // Native initialized offset64 (0x23b918), RectOffset adds half (0x170b6).
            // Earlier y606/76 expectations encoded the falsified PA-001 guess128.
            Check("hero button rect = (52,574,220,96)", RectNear(sel.GetControl(54).CurrentRect, 52, 574, 220, 96), Fmt(sel.GetControl(54).CurrentRect));
            Check("bank button rect = (282,574,220,96)", RectNear(sel.GetControl(93).CurrentRect, 282, 574, 220, 96), Fmt(sel.GetControl(93).CurrentRect));
            Check("jigsaw button rect = (522,574,220,96)", RectNear(sel.GetControl(95).CurrentRect, 522, 574, 220, 96), Fmt(sel.GetControl(95).CurrentRect));
            Check("armory button rect = (754,574,220,96)", RectNear(sel.GetControl(55).CurrentRect, 754, 574, 220, 96), Fmt(sel.GetControl(55).CurrentRect));
            Check("player panel rect = (52,44,436,126)", RectNear(sel.GetControl(59).CurrentRect, 52, 44, 436, 126), Fmt(sel.GetControl(59).CurrentRect));
            Check("level label text = 1 (level 0 + 1)", FindLabel(sel, 61) == "1");
            Check("hp label text = 100", FindLabel(sel, 62) == "100", FindLabel(sel, 62));
            Check("exp percent = 0", sel.ExpPercent == 0);
            Shot(cam, "04_map");

            // ---------------- options ----------------
            Click(sel, sel.GetControl(64));
            Check("gear opens options", sel.optionOpen);
            game.StepFor(0.6f);
            Check("dim overlay id78 colour alpha 208", sel.GetControl(78) != null);
            Check("op controls present (79,83,81,84,85,87,88)", sel.GetControl(79) != null && sel.GetControl(83) != null && sel.GetControl(81) != null && sel.GetControl(84) != null && sel.GetControl(85) != null && sel.GetControl(87) != null && sel.GetControl(88) != null);
            Shot(cam, "05_options");
            Click(sel, sel.GetControl(93));
            Check("map buttons are blocked while options open", sel.lastAction != "bank", sel.lastAction);
            int sound0 = game.Profile.sound, fx0 = game.Profile.fx;
            var mc = sel.GetControl(81);
            sb.AppendLine("DEBUG ctrl81 rect=" + Fmt(mc.FullRect) + " touchable=" + mc.touchable + " visible=" + mc.visible + " moving=" + mc.IsMoving + " canTouch=" + sel.CanTouch(mc) + " optionOpen=" + sel.optionOpen + " " + mc.DebugTween);
            Click(sel, sel.GetControl(81));
            sb.AppendLine("DEBUG after click sound=" + game.Profile.sound + " lastAction=" + sel.lastAction);
            Check("Music toggle flips g_sound and saves", game.Profile.sound == 1 - sound0 && File.Exists(Path.Combine(tmp, "dinocap2hd.sav")));
            Check("Music OFF stops menu music", game.Profile.sound == 0 ? !game.Audio.MusicPlaying : true);
            Click(sel, sel.GetControl(83));
            Check("Sound toggle flips g_fx", game.Profile.fx == 1 - fx0);
            Click(sel, sel.GetControl(84)); Click(sel, sel.GetControl(85));
            Check("Support/Review clicks registered", sel.supportClicks == 1 && sel.reviewClicks == 1);
            Click(sel, sel.GetControl(87));
            Check("Reset Profile opens confirm", sel.confirmOpen);
            Click(sel, sel.GetControl(901));
            Check("No keeps data and closes confirm", !sel.confirmOpen && sel.resetCount == 0);
            game.Profile.cash = 1234; Click(sel, sel.GetControl(87)); Click(sel, sel.GetControl(902));
            game.Step(1f / 60f); // queued native scene switch is applied on the next Game::Update
            var sel2 = (SelectScene)game.Current;
            Check("Yes resets cash but keeps audio flags", sel2.resetCount == 0 /* new scene */ && game.Profile.cash == 0 && game.Profile.fx == 1 - fx0 && game.Profile.sound == 1 - sound0);
            game.StepFor(0.6f);
            Click(sel2, sel2.GetControl(64)); game.StepFor(0.6f);
            Click(sel2, sel2.GetControl(88));
            Check("Back starts leave (state 5)", sel2.optionState == 5);
            game.StepFor(0.6f);
            Check("option controls removed after leave tween", sel2.GetControl(78) == null && sel2.GetControl(88) == null && !sel2.optionOpen);

            // persistence: a new Game instance reads the saved flags
            var game2 = Dc2Game.Create(tmp);
            Check("flags persist across a restart", game2.Profile.fx == game.Profile.fx && game2.Profile.sound == game.Profile.sound);
            UnityEngine.Object.DestroyImmediate(game2.gameObject);
            UnityEngine.Object.DestroyImmediate(game.gameObject);
            UnityEngine.Object.DestroyImmediate(cam.gameObject);
            try { Directory.Delete(tmp, true); } catch (Exception) { }
        }

        static string FindLabel(Dc2Scene s, int id)
        {
            foreach (var l in s.Labels) if (l.id == id) return l.Text;
            return null;
        }
    }
}
