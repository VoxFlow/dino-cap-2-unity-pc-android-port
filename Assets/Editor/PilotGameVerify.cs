using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>First battle frame: native HUD layout, walk speed, camera rule, background wrap.</summary>
    public static class PilotGameVerify
    {
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-PILOT-001/game-r1");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            string dir = Path.Combine(Path.GetTempPath(), "dc2_game_verify_" + Guid.NewGuid().ToString("N"));
            int passed = 0, failed = 0; var log = new StringBuilder();
            Action<string, bool> check = (n, v) => { log.AppendLine((v ? "PASS " : "FAIL ") + n); if (v) passed++; else failed++; };
            Dc2Game game = null; Camera cam = null; RenderTexture rt = null; Texture2D img = null;
            try
            {
                cam = Bootstrap.MakeCamera(); game = Dc2Game.Create(dir); game.manualStep = true; game.SkipMissionIntro = true; game.cam = cam;
                game.LaunchMission = new NativeMapState.Mission { type = 3, count = 20, background = 0 }; // explicit test fixture
                rt = new RenderTexture(1024, 768, 24); img = new Texture2D(1024, 768, TextureFormat.RGB24, false);
                Action<string> shot = name =>
                {
                    cam.targetTexture = rt; Bootstrap.RenderAll(cam); RenderTexture.active = rt; img.ReadPixels(new Rect(0, 0, 1024, 768), 0, 0); img.Apply();
                    File.WriteAllBytes(Path.Combine(output, name), img.EncodeToPNG());
                    cam.targetTexture = null; RenderTexture.active = null;
                };
                for (int hero = 0; hero < 5; hero++)
                {
                    game.Profile.playerType = hero; game.Profile.cash = 8220622;
                    game.Profile.level[hero] = Math.Max(game.Profile.level[hero], 0);
                    game.SwitchScene("gameScene"); game.StepFor(0.02f);
                    var scene = (GameScene)game.Current;
                    check("native spawn vector hero" + hero, GameScene.NativeHeroSpawn(hero) == new Vector2(-240f, hero == 4 ? -105f : 135f));
                    if (hero < 4) check("native CA callback scale hero" + hero, scene.Rig.Player.objectScale == 1f);
                    else check("native Girl component scale/origin replaces menu preview", scene.GirlObjectScale == 1f && scene.GirlPosition.y == -105f);
                    game.StepFor(0.3f);
                    if (hero == 0)
                    {
                        check("HUD left arrow at native iPad display rect (30,619,84,103)", scene.GetControl(1).FullRect == new Rect(30, 619, 84, 103));
                        check("HUD fire at (903,613,88,88)", scene.GetControl(4).FullRect == new Rect(903, 613, 88, 88));
                        check("HUD roll at (830,537,88,88)", scene.GetControl(28).FullRect == new Rect(830, 537, 88, 88));
                        check("HUD hit at (774,631,88,88)", scene.GetControl(3).FullRect == new Rect(774, 631, 88, 88));
                        check("native camera left boundary at spawn", scene.camLeft == -240f);
                        shot("game_stand_hero0.png");
                        check("native hero0 object origin y135 (not fitted feet)", scene.Rig.Player.position.y == 135f);
                        float x0 = scene.heroX;
                        scene.rightDown = true; game.StepFor(1.0f);
                        check("hero0 walks right 300 px/s", Mathf.Abs(scene.heroX - x0 - 300f) < 6f);
                        check("move pose selected", scene.rigState == 1);
                        shot("game_walk_right.png");
                        scene.rightDown = false; scene.leftDown = true; game.StepFor(0.5f);
                        check("turn left: native boundary remains active", scene.facingLeft && scene.camLeft == -240f);
                        shot("game_walk_left.png");
                        scene.leftDown = false; game.StepFor(0.1f);
                        check("stand pose after release", scene.rigState == 0);
                    }
                    if (hero == 3)
                    {
                        float x0 = scene.heroX; scene.rightDown = true; game.StepFor(1.0f);
                        check("Ninja walks 400 px/s", Mathf.Abs(scene.heroX - x0 - 400f) < 8f);
                    }
                    shot("game_hero" + hero + ".png");
                }
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
