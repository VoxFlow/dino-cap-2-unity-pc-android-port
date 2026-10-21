using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>DEBUG_ONLY diagnostic (DC2-BUGS-001 r119l, user: "boss mission level 1 with the Girl, spamming weapon switch: the ammo icon shifts out of place"):
    /// taps the change button every 3 ticks for 4 s, logs control 20's rect / visibility and the transform position, captures the HUD; 4:3 and 16:9; Girl and hero 0;
    /// boss and kill missions. Not part of the suite.</summary>
    public static class PilotAmmoIconProbe
    {
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-BUGS-001/ammoicon-probe");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            var log = new StringBuilder();
            string dir = Path.Combine(Path.GetTempPath(), "dc2_ammoicon_" + Guid.NewGuid().ToString("N"));
            Camera cam = null; Dc2Game g = null; bool wasWide = WideView.Enabled;
            var rt = new RenderTexture(1024, 768, 24); var img = new Texture2D(1024, 768, TextureFormat.RGB24, false);
            try
            {
                WideView.Enabled = true;
                cam = Bootstrap.MakeCamera(); g = Dc2Game.Create(dir); g.cam = cam; g.manualStep = true; g.SkipMissionIntro = true;
                Action<string> shot = name => { cam.targetTexture = rt; Bootstrap.RenderAll(cam); RenderTexture.active = rt; img.ReadPixels(new Rect(0, 0, 1024, 768), 0, 0); img.Apply(); File.WriteAllBytes(Path.Combine(output, name), img.EncodeToPNG()); cam.targetTexture = null; RenderTexture.active = null; };
                foreach (int hero in new[] { 4, 0 })
                foreach (int type in new[] { 8, 3 })
                foreach (bool wide in new[] { false, true })
                {
                    g.Profile.playerType = hero; if (g.Profile.level[hero] < 0) g.Profile.level[hero] = 0;
                    for (int k = 0; k < 6; k++) { g.Profile.weaponOwned[hero * 6 + k] = 0; g.Profile.weaponAmmo[hero * 6 + k] = 300; }
                    g.LaunchMission = new NativeMapState.Mission { type = type, count = 20, level = 1, background = 0 }; g.LaunchMission.enemies.Add(1);
                    g.SwitchScene("gameScene"); g.Step(0f); g.Step(1f / 60f);
                    var s = (GameScene)g.Current; s.spawningEnabled = false; s.ignorePlayer = true;
                    if (wide) WideView.Apply(cam, s, 1366, 768); else WideView.Apply(cam, s, 1024, 768);
                    g.StepFor(1f);
                    var c = s.GetControl(20);
                    string tag = "h" + hero + "_t" + type + (wide ? "_wide" : "_43");
                    log.AppendLine(tag + " before: rect " + c.CurrentRect + " full " + c.FullRect + " pos " + c.transform.localPosition + " anchor " + c.wideAnchor);
                    var p = GameScene.ChangeRect.center;
                    for (int t = 0; t < 240; t++)
                    {
                        if (t % 3 == 0) s.Touch(0, p, 9); else if (t % 3 == 1) s.Touch(2, p, 9);
                        g.Step(1f / 60f);
                        if (t % 40 == 0) log.AppendLine("   t" + t + " slot " + s.weaponSlot + " rect " + c.CurrentRect + " pos " + c.transform.localPosition);
                    }
                    g.StepFor(1f);
                    log.AppendLine(tag + " after: rect " + c.CurrentRect + " full " + c.FullRect + " pos " + c.transform.localPosition);
                    shot(tag + ".png");
                }
            }
            catch (Exception e) { log.AppendLine("EXCEPTION " + e); }
            finally
            {
                WideView.Enabled = wasWide;
                if (g != null) UnityEngine.Object.DestroyImmediate(g.gameObject);
                if (cam != null) UnityEngine.Object.DestroyImmediate(cam.gameObject);
                if (Directory.Exists(dir)) Directory.Delete(dir, true);
                File.WriteAllText(Path.Combine(output, "verify.txt"), log.ToString()); Debug.Log(log.ToString());
                PilotExit.Exit(0);
            }
        }
    }
}
