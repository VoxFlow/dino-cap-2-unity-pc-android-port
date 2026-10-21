using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>Bank (m_map_type 4) UI: native rects, selection/purchase flow, iapaddmoney amounts, back routing.</summary>
    public static class PilotBankVerify
    {
        static string Label(Dc2Scene s, int id) { foreach (var l in s.Labels) if (l.id == id) return l.Text; return null; }

        static void Click(Dc2Scene s, Vector2 p) { s.Touch(0, p); s.Touch(2, p); }

        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-PILOT-001/bank-r1");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            string dir = Path.Combine(Path.GetTempPath(), "dc2_bank_verify_" + Guid.NewGuid().ToString("N"));
            int passed = 0, failed = 0; var log = new StringBuilder();
            Action<string, bool> check = (n, v) => { log.AppendLine((v ? "PASS " : "FAIL ") + n); if (v) passed++; else failed++; };
            Dc2Game game = null; Camera cam = null;
            try
            {
                cam = Bootstrap.MakeCamera(); game = Dc2Game.Create(dir); game.manualStep = true; game.SkipMissionIntro = true; game.cam = cam;
                game.SwitchScene("selectScene");
                var scene = (SelectScene)game.Current;
                game.StepFor(1.0f);
                Vector2 hit = scene.GetControl(93).FullRect.center;
                Click(scene, hit);
                check("bank button queues map type 4 with return type 0", game.MapType == 4 && game.LastMapType == 0);
                game.StepFor(0.05f);
                scene = (SelectScene)game.Current;
                game.StepFor(1.0f);

                check("background centred 1024x704 (0,32)", scene.GetControl(9001) != null && scene.GetControl(9001).FullRect == new Rect(0, 32, 1024, 704));
                check("background frame ui_iap_bgFrame", scene.GetControl(9001).FrameName == "ui_iap_bgFrame");
                check("gold icon rect (760,60,46,44)", scene.GetControl(50).FullRect == new Rect(760, 60, 46, 44) && scene.GetControl(50).FrameName == "ui_goldFrame");
                check("gold label text 0", Label(scene, 51) == "0");
                check("back button rect (64,622,208,60)", scene.GetControl(17).FullRect == new Rect(64, 622, 208, 60));
                check("back image rect (134,624,138,54) not touchable", scene.GetControl(18).FullRect == new Rect(134, 624, 138, 54) && !scene.GetControl(18).touchable);
                check("six product cells", scene.bankRects.Count == 6);
                check("cell0 = (32,158,300,208)", scene.bankRects[0] == new Rect(32, 158, 300, 208));
                check("cell1 = (362,158,300,208)", scene.bankRects[1] == new Rect(362, 158, 300, 208));
                check("cell2 = (680,158,300,208)", scene.bankRects[2] == new Rect(680, 158, 300, 208));
                check("cell3 = (32,399,300,208)", scene.bankRects[3] == new Rect(32, 399, 300, 208));
                check("cell4 = (362,399,300,208)", scene.bankRects[4] == new Rect(362, 399, 300, 208));
                check("cell5 = (680,399,300,208)", scene.bankRects[5] == new Rect(680, 399, 300, 208));

                var rt = new RenderTexture(1024, 768, 24); var img = new Texture2D(1024, 768, TextureFormat.RGB24, false);
                Action<string> shot = name =>
                {
                    cam.targetTexture = rt; Bootstrap.RenderAll(cam); RenderTexture.active = rt; img.ReadPixels(new Rect(0, 0, 1024, 768), 0, 0); img.Apply();
                    File.WriteAllBytes(Path.Combine(output, name), img.EncodeToPNG());
                    cam.targetTexture = null; RenderTexture.active = null;
                };
                shot("bank_ui.png");

                // press selects (highlight id 94); release in the same cell starts the purchase
                Vector2 c4 = scene.bankRects[4].center;
                scene.Touch(0, c4);
                check("press selects cell 4 and draws ui_iap_selectFrame", scene.bankSelected == 4 && scene.GetControl(94) != null && scene.GetControl(94).FrameName == "ui_iap_selectFrame" && scene.GetControl(94).FullRect == scene.bankRects[4]);
                shot("bank_selected.png");
                scene.Touch(2, c4);
                check("release starts purchase of the 1999 product with the shade overlay", scene.iapBusy && scene.iapPending == 4 && scene.lastPurchaseProduct == "com.trinitigame.dinocap2hd.1999centsnew" && scene.GetControl(10) != null && scene.activityIndicator);
                check("shade rect (0,0,1024,736) black alpha 160", scene.GetControl(10).FullRect == new Rect(0, 0, 1024, 736));
                check("selection cleared after release", scene.bankSelected == -1);
                shot("bank_busy.png");
                game.StepFor(0.6f);
                check("store cancel path removes overlay and grants nothing", !scene.iapBusy && scene.GetControl(10) == null && game.Profile.cash == 0 && !scene.activityIndicator);

                // press in one cell, release in another: no purchase
                scene.Touch(0, scene.bankRects[0].center); scene.Touch(2, scene.bankRects[1].center);
                check("press/release in different cells does not purchase", !scene.iapBusy && scene.bankSelected == -1);
                // release outside any cell
                scene.Touch(0, scene.bankRects[0].center); scene.Touch(2, new Vector2(5, 5));
                check("release outside cells does not purchase", !scene.iapBusy);

                // successful store results (adapter): iapaddmoney amounts
                scene.iapResult = SelectScene.IapSuccess;
                int[] expect = { 15000, 50000, 95000, 220000, 500000 };
                int cash = 0; bool amounts = true;
                for (int i = 0; i < 5; i++)
                {
                    Click(scene, scene.bankRects[i].center);
                    game.StepFor(0.6f);
                    cash += expect[i];
                    amounts &= game.Profile.cash == cash && game.Profile.iapTime[i] == 1;
                }
                check("iapaddmoney adds 15000/50000/95000/220000/500000", amounts && game.Profile.cash == 880000);
                check("gold label shows 880,000", Label(scene, 51) == "880,000");
                Click(scene, scene.bankRects[5].center); game.StepFor(0.6f);
                check("product 5 sets max bullets 999 and leaves cash alone", game.Profile.maxBullet == 999 && game.Profile.cash == 880000 && game.Profile.iapTime[5] == 1);
                check("purchases are saved", File.Exists(Path.Combine(dir, "dinocap2hd.sav")) && Profile.Load(dir).cash.ToString().Contains("880000"));

                // back returns to the map (return type 0)
                Click(scene, scene.GetControl(17).FullRect.center);
                check("back queues the return screen (type 0)", game.MapType == 0);
                game.StepFor(0.05f); scene = (SelectScene)game.Current; game.StepFor(1.0f);
                check("map restored", scene.GetControl(54) != null && scene.GetControl(93) != null);

                // hero -> IAP button -> back returns to hero screen
                Click(scene, scene.GetControl(54).FullRect.center);
                game.StepFor(0.05f); scene = (SelectScene)game.Current; game.StepFor(1.0f);
                Click(scene, scene.GetControl(49).FullRect.center);
                check("hero IAP button queues bank with return type 2", game.MapType == 4 && game.LastMapType == 2);
                game.StepFor(0.05f); scene = (SelectScene)game.Current; game.StepFor(1.0f);
                Click(scene, scene.GetControl(17).FullRect.center);
                check("back from hero-origin bank returns to hero screen", game.MapType == 2);
                game.StepFor(0.05f); scene = (SelectScene)game.Current; game.StepFor(1.0f);
                check("hero screen present again", scene.GetControl(6) != null && scene.GetControl(38) != null);
                // REL-12 (Codex DC2-RELEASE-REVIEW-001): selectScene::UpdateExp 0xf461a..0xf4626 stores the clamped cash back (negative -> 0)
                game.Profile.cash = -5;
                string shown = (string)typeof(SelectScene).GetMethod("UpdateExpCash", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).Invoke(scene, null);
                check("REL-12 UpdateExp writes negative cash back as 0 (shown \"" + shown + "\", cash " + game.Profile.cash + ")", shown == "0" && game.Profile.cash == 0);
                UnityEngine.Object.DestroyImmediate(rt); UnityEngine.Object.DestroyImmediate(img);
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
