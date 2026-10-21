using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>Mission start sequence (NATIVE ARM, Dev/Evidence/yuanli_awarditem_r117.md "r118 MISSION INTRO"): OnLoad tail -> state 1 + black cover + LevelBGEnter -> LevelEnter -> 0.5 s hold -> LevelLeave (state 3) -> LevelBGLeave -> StartGame_N (state 4).
    /// Logic only (state, controls, timings, input gating, spawn arming); it does not judge visuals.</summary>
    public static class PilotIntroVerify
    {
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-PILOT-001/intro-r1");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            string dir = Path.Combine(Path.GetTempPath(), "dc2_intro_verify_" + Guid.NewGuid().ToString("N"));
            int passed = 0, failed = 0; var log = new StringBuilder();
            Action<string, bool> check = (n, v) => { log.AppendLine((v ? "PASS " : "FAIL ") + n); if (v) passed++; else failed++; };
            Dc2Game game = null; Camera cam = null; RenderTexture rt = null; Texture2D img = null;
            try
            {
                rt = new RenderTexture(1024, 768, 24); img = new Texture2D(1024, 768, TextureFormat.RGB24, false);
                Action<string> shot = name =>
                {
                    cam.targetTexture = rt; Bootstrap.RenderAll(cam); RenderTexture.active = rt; img.ReadPixels(new Rect(0, 0, 1024, 768), 0, 0); img.Apply();
                    File.WriteAllBytes(Path.Combine(output, name), img.EncodeToPNG()); cam.targetTexture = null; RenderTexture.active = null;
                };
                cam = Bootstrap.MakeCamera(); game = Dc2Game.Create(dir); game.manualStep = true; game.cam = cam;   // SkipMissionIntro stays false
                var missions = new NativeMapState().missions;
                foreach (int type in new[] { 3, 1 })   // a lifeMode mission and a run mission (mainScene)
                {
                    NativeMapState.Mission m = null; foreach (var x in missions) if (x.type == type) { m = x; break; }
                    if (m == null) { log.AppendLine("INFO no mission of type " + type); continue; }
                    game.Profile.playerType = 0; game.Profile.mission = 7; game.LaunchMission = m; game.LaunchMissionType = m.type;
                    game.SwitchScene("gameScene"); game.StepFor(0.02f);
                    var scene = (GameScene)game.Current;
                    string t = "type " + type + ": ";
                    check(t + "scene starts in intro state 1", scene.levelState == 1 && scene.IntroActive);
                    check(t + "black cover id 11 present", scene.GetControl(11) != null);
                    check(t + "band id 12 present, day card not yet", scene.GetControl(12) != null && scene.GetControl(13) == null);
                    // r119 (DC2-BUGS-001 item 12): OnLoad sets g_b_RightDown = 1 (mainScene 0xaa9f2 / lifeModeScene 0xe9b58): the hero walks in to the right during the intro.
                    float x0 = scene.heroX; scene.Touch(0, GameScene.LeftRect.center); game.StepFor(0.2f);
                    check(t + "walk-in: g_b_RightDown set by OnLoad, hero walks right during the intro (" + x0 + " -> " + scene.heroX + ")", scene.rightDown && scene.heroX > x0);
                    check(t + "touch input ignored in state 1 (OnTouchEvent state != 4): a left press does not turn the hero", !scene.leftDown && scene.rightDown); scene.Touch(2, GameScene.LeftRect.center);
                    game.StepFor(0.5f);   // band 0.25 s -> LevelEnter at its move end
                    check(t + "band move end creates the day card (id 13) and the number label (id 14)", scene.GetControl(13) != null);
                    string num = null; foreach (var l in scene.Labels) if (l.id == 14) num = l.Text;
                    check(t + "label shows g_mission (7)", num == "7");
                    game.StepFor(0.2f); shot("intro_type" + type + "_card.png");
                    check(t + "still state 1 and no enemies yet", scene.levelState == 1 && scene.dinos.Count == 0);
                    game.StepFor(0.38f);   // card 0.25 s + hold 0.5 s -> LevelLeave at ~1.0 s (0.2 s spent on the capture step above)
                    check(t + "after the hold LevelLeave ran: state 3", scene.levelState == 3);
                    game.StepFor(1.0f);
                    check(t + "StartGame_N: state 4, cover and band deleted", scene.levelState == 4 && scene.GetControl(11) == null && scene.GetControl(12) == null && scene.GetControl(13) == null);
                    check(t + "walk-in ends: g_b_RightDown cleared at StartGame (0xa6ab8 / 0xe666e)", !scene.rightDown);
                    float x1 = scene.heroX; scene.Touch(0, GameScene.RightRect.center); game.StepFor(0.3f); scene.Touch(2, GameScene.RightRect.center);
                    check(t + "input works again in state 4", scene.heroX > x1);
                }
            }
            catch (Exception e) { failed++; log.AppendLine("EXCEPTION " + e); }
            finally
            {
                if (rt != null) UnityEngine.Object.DestroyImmediate(rt); if (img != null) UnityEngine.Object.DestroyImmediate(img);
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
