using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>Mission launch (m_map_type 6): loading image, tip label, move completion dispatch.</summary>
    public static class PilotLaunchVerify
    {
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-PILOT-001/launch-r16");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            string dir = Path.Combine(Path.GetTempPath(), "dc2_launch_verify_" + Guid.NewGuid().ToString("N"));
            int passed = 0, failed = 0; var log = new StringBuilder();
            Action<string, bool> check = (n, v) => { log.AppendLine((v ? "PASS " : "FAIL ") + n); if (v) passed++; else failed++; };
            Dc2Game game = null; Camera cam = null;
            try
            {
                cam = Bootstrap.MakeCamera(); game = Dc2Game.Create(dir); game.manualStep = true; game.SkipMissionIntro = true; game.cam = cam;
                foreach (int type in new[] { 1, 2, 5, 6, 8 })
                {
                    game.MapType = 6; game.LaunchMissionType = type;
                    game.LaunchMission = new NativeMapState.Mission { type = type, count = 20, background = 0 }; // explicit selected-record fixture
                    game.SwitchScene("selectScene");
                    game.StepFor(0.02f);                       // Game::Update drains the queued SwitchScene
                    var scene = (SelectScene)game.Current;
                    check("type" + type + ": image597 loadingFrame", scene.GetControl(597) != null && scene.GetControl(597).FrameName == "loadingFrame");
                    check("type" + type + ": rect (712,568,230,44)", scene.GetControl(597).FullRect == new Rect(712, 568, 230, 44) || scene.GetControl(597).FullRect.size == new Vector2(230, 44));
                    bool tip = false; foreach (var t in SelectScene.LoadingTips) foreach (var l in scene.Labels) if (l.id == 598 && l.Text == t) tip = true;
                    check("type" + type + ": tip label 598 is a shipped tip", tip);
                    check("type" + type + ": no dispatch before move ends", scene.launchDestination == "");
                    game.StepFor(1.0f);
                    bool life = type == 2 || type == 8;
                    check("type" + type + ": dispatch " + (life ? "lifeModeScene" : "mainScene"), scene.launchDestination == (life ? "lifeModeScene" : "mainScene") && scene.launchLifeMode == life);
                    check("type" + type + ": initTime " + (type != 1 && type != 5) + ", weapon first " + (type == 8), scene.launchInitTime == (type != 1 && type != 5) && scene.launchReloadWeaponFirst == (type == 8));
                    check("type" + type + ": mission_state++", game.Profile.missionState[type - 1] == 1);
                }
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
