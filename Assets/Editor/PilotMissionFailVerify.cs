using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>Native hero-death -> mission-fail chain (GameOverStart 0xa6f70, MissionFailEnter 0xe40de, FailZEnter 0xe3e8e, OnTimeout DieEnd).</summary>
    public static class PilotMissionFailVerify
    {
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-PILOT-001/missionfail-r1");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            string dir = Path.Combine(Path.GetTempPath(), "dc2_fail_verify_" + Guid.NewGuid().ToString("N"));
            int passed = 0, failed = 0; var log = new StringBuilder();
            Action<string, bool> check = (n, v) => { log.AppendLine((v ? "PASS " : "FAIL ") + n); if (v) passed++; else failed++; };
            Dc2Game game = null; Camera cam = null; RenderTexture rt = null; Texture2D img = null;
            try
            {
                cam = Bootstrap.MakeCamera(); game = Dc2Game.Create(dir); game.manualStep = true; game.SkipMissionIntro = true; game.cam = cam;
                game.LaunchMission = new NativeMapState.Mission { type = 3, count = 20, level = 1, background = 0 };
                rt = new RenderTexture(1024, 768, 24); img = new Texture2D(1024, 768, TextureFormat.RGB24, false);
                Action<string> shot = name => { cam.targetTexture = rt; Bootstrap.RenderAll(cam); RenderTexture.active = rt; img.ReadPixels(new Rect(0, 0, 1024, 768), 0, 0); img.Apply(); File.WriteAllBytes(Path.Combine(output, name), img.EncodeToPNG()); cam.targetTexture = null; RenderTexture.active = null; };
                game.Profile.playerType = 0; game.SwitchScene("gameScene"); game.StepFor(0.02f);
                var scene = (GameScene)game.Current; scene.spawningEnabled = false; game.StepFor(0.2f);
                scene.BeAttack(new DinoActor { x = scene.heroX + 5f, atk = 5000f });
                check("lethal hit starts the hero death", scene.heroDead);
                for (int f = 0; f < 600 && scene.result == GameScene.MissionResult.Running; f++) game.Step(1f / 60f);
                check("mission fails only after the die animation ends (GameOverStart)", scene.result == GameScene.MissionResult.Failed && scene.Rig.Player.Ended);
                check("GameOverStart creates the fail_line move (id17) and the missionfailbg fade image (id11)", scene.GetControl(17) != null && scene.GetControl(11) != null);
                game.StepFor(0.1f); shot("fail_line.png");
                game.StepFor(0.35f);
                check("fail_line move end -> MissionFailEnter banner (id29)", scene.GetControl(29) != null);
                game.StepFor(0.1f); shot("fail_banner.png");
                game.StepFor(0.5f);
                check("banner move end -> FailZEnter (id30)", scene.GetControl(30) != null);
                game.StepFor(0.5f); shot("fail_z.png");
                check("still in the game scene before the DieEnd timer", game.CurrentName == "gameScene");
                game.StepFor(3.3f);
                check("DieEnd timer 3.0 s after fail_z: map type 0, selectScene", game.CurrentName == "selectScene" && game.MapType == 0);
            }
            catch (Exception e) { failed++; log.AppendLine("EXCEPTION " + e); }
            finally
            {
                if (rt != null) UnityEngine.Object.DestroyImmediate(rt);
                if (img != null) UnityEngine.Object.DestroyImmediate(img);
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
