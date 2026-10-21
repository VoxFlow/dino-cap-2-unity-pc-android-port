using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>InitHouse (0x9aae8): house count, positions, decor count/positions, house animation load (Dev/Evidence/egg_decomp_r108.md house section).</summary>
    public static class PilotHouseVerify
    {
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-PILOT-001/house-r1");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            string dir = Path.Combine(Path.GetTempPath(), "dc2_house_verify_" + Guid.NewGuid().ToString("N"));
            int passed = 0, failed = 0; var log = new StringBuilder();
            Action<string, bool> check = (n, v) => { log.AppendLine((v ? "PASS " : "FAIL ") + n); if (v) passed++; else failed++; };
            Dc2Game game = null; Camera cam = null; RenderTexture rt = null; Texture2D img = null;
            try
            {
                cam = Bootstrap.MakeCamera(); game = Dc2Game.Create(dir); game.manualStep = true; game.SkipMissionIntro = true; game.cam = cam;
                rt = new RenderTexture(1024, 768, 24); img = new Texture2D(1024, 768, TextureFormat.RGB24, false);
                Action<string> shot = name => { cam.targetTexture = rt; Bootstrap.RenderAll(cam); RenderTexture.active = rt; img.ReadPixels(new Rect(0, 0, 1024, 768), 0, 0); img.Apply(); File.WriteAllBytes(Path.Combine(output, name), img.EncodeToPNG()); cam.targetTexture = null; RenderTexture.active = null; };
                foreach (bool named in new[] { true, false })
                    for (int k = 1; k <= 3; k++)
                    {
                        try { var an = NativeCaAnimation.Load("DC2/Image/a2b/house" + k + ".a2b", named, 1f); log.AppendLine("INFO house" + k + " named=" + named + " frames=" + an.frames.Length + " parts0=" + an.frames[0].parts.Length + " ref0=" + (an.frames[0].parts.Length > 0 ? an.frames[0].parts[0].reference : "")); }
                        catch (Exception ex) { log.AppendLine("INFO house" + k + " named=" + named + " load failed " + ex.Message); }
                    }
                // InitHouse runs only for background 0 (mainScene 0xb3332 / lifeModeScene 0xddaf6; DC2-BUGS-001 B1): the run case uses bg 0 (was bg 1, port defect).
                game.LaunchMission = new NativeMapState.Mission { type = 1, count = 4500f, level = 2, background = 0 };
                game.LaunchMission.enemies.Add(2);
                game.Profile.playerType = 0;
                game.SwitchScene("gameScene"); game.StepFor(0.05f);
                var scene = (GameScene)game.Current; scene.spawningEnabled = false;
                // right = 9000: houses = (int)((9000 - 380*2)/(960*2)) = 4; decor = (int)(2*9000/360) = 50
                check("run 4500: 4 houses, 50 decor sprites", scene.HouseCount == 4 && scene.DecorCount == 50);
                bool pos = true;
                for (int i = 0; i < scene.HouseCount; i++)
                {
                    var p = scene.HousePosition(i);
                    float lo = 380f + 1920f * i, hi = lo + 398f;
                    pos &= p.x >= lo && p.x <= hi && Mathf.Abs(p.y + 72f) < 0.01f;
                }
                check("house x = S*rand(0..199) + 380 + S*960*i, y = S*-36", pos);
                bool dpos = true;
                for (int j = 0; j < scene.DecorCount; j++)
                {
                    var p = scene.DecorPosition(j);
                    float lo = -100f + 640f * j, hi = lo + 78f;
                    dpos &= p.x >= lo && p.x <= hi;
                }
                check("decor x = S*rand(0..39) - 100 + S*320*j", dpos);
                for (int i = 0; i < scene.HouseCount; i++) log.AppendLine("INFO house " + i + " kind " + scene.HouseKind(i) + " at " + scene.HousePosition(i));
                scene.heroX = scene.HousePosition(0).x; game.StepFor(0.5f);
                shot("house0.png");
                scene.heroX = 700f; game.StepFor(0.5f);
                shot("decor.png");
                // Arena (type 7 -> background 7) and every background other than 0: no InitHouse call, so no houses and no zhuangshi.
                // FALSIFIED (r118z8): the old expectation "arena: 4 decor sprites" came from the port building decor on every map.
                game.LaunchMission = new NativeMapState.Mission { type = 7, count = 60, level = 1, background = 0 };
                game.SwitchScene("gameScene"); game.StepFor(0.05f);
                scene = (GameScene)game.Current;
                check("arena (background 7): no houses and no zhuangshi", scene.HouseCount == 0 && scene.DecorCount == 0);
                bool none = true; string seen = "";
                foreach (int bgk in new[] { 1, 2, 3, 4, 5 })
                {
                    game.LaunchMission = new NativeMapState.Mission { type = 1, count = 4500f, level = 2, background = bgk }; game.LaunchMission.enemies.Add(2);
                    game.SwitchScene("gameScene"); game.StepFor(0.05f); scene = (GameScene)game.Current;
                    seen += " bg" + bgk + ":" + scene.HouseCount + "/" + scene.DecorCount; none &= scene.HouseCount == 0 && scene.DecorCount == 0;
                }
                check("run backgrounds 1..5: no houses and no zhuangshi (" + seen.Trim() + ")", none);
                game.LaunchMission = new NativeMapState.Mission { type = 3, count = 20, level = 1, background = 0 }; game.LaunchMission.enemies.Add(2);
                game.SwitchScene("gameScene"); game.StepFor(0.05f); scene = (GameScene)game.Current;
                check("life-mode kill mission on background 0 builds them (lifeModeScene::InitHouse 0xd6504)", scene.HouseCount + scene.DecorCount > 0);
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
