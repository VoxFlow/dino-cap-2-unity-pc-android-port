using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>Monkey / soak test (logic only, DEBUG_ONLY): every hero x every mission type of Map.xml is played with seeded random inputs (move, fire, roll, melee, weapon swap, pause) for up to 150 simulated seconds
    /// at 60 Hz. Fails on any exception or Unity error log, a non-finite hero position / HP, or runaway GameObject growth. It does not judge visuals or balance.</summary>
    public static class PilotSoakVerify
    {
        static int errorLogs; static string firstError;
        static void OnLog(string msg, string stack, LogType t)
        {
            if ((t == LogType.Error || t == LogType.Exception || t == LogType.Assert) && !msg.StartsWith("Destroy may not be called from edit mode")) { errorLogs++; if (firstError == null) firstError = msg + "\n" + stack; }
        }

        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-PILOT-001/soak-r1");
            var args = Environment.GetCommandLineArgs(); float simSeconds = 150f; int heroFilter = -1; bool all30 = false; int startLevel = 24;
            for (int i = 0; i < args.Length - 1; i++)
            {
                if (args[i] == "-reportDir") output = args[i + 1];
                if (args[i] == "-soakSeconds") simSeconds = float.Parse(args[i + 1]);
                if (args[i] == "-soakHero") heroFilter = int.Parse(args[i + 1]);
                if (args[i] == "-soakLevel") startLevel = int.Parse(args[i + 1]);   // < 24 re-locks weapon slots 1..4 so mission completion exercises AwardItem unlocks
            }
            foreach (var a in args) if (a == "-soakAll") all30 = true;
            Directory.CreateDirectory(output);
            string dir = Path.Combine(Path.GetTempPath(), "dc2_soak_verify_" + Guid.NewGuid().ToString("N"));
            int passed = 0, failed = 0; var log = new StringBuilder();
            Action<string, bool> check = (n, v) => { log.AppendLine((v ? "PASS " : "FAIL ") + n); if (v) passed++; else failed++; };
            Dc2Game game = null; Camera cam = null;
            Application.logMessageReceived += OnLog;
            try
            {
                cam = Bootstrap.MakeCamera(); game = Dc2Game.Create(dir); game.manualStep = true; game.cam = cam;
                var all = new NativeMapState().missions;
                // one record per mission type (first and last record of each type to cover different enemy lists / levels)
                var picks = new List<NativeMapState.Mission>();
                foreach (var g in all.GroupBy(m => m.type).OrderBy(g => g.Key)) { picks.Add(g.First()); if (g.Count() > 1) picks.Add(g.Last()); }
                if (all30) picks = new List<NativeMapState.Mission>(all);
                log.AppendLine("INFO missions " + all.Count + " picked " + picks.Count);
                for (int hero = 0; hero < 5; hero++)
                {
                    if (heroFilter >= 0 && hero != heroFilter) continue;
                    foreach (var m in picks)
                    {
                        string tag = "hero" + hero + " #" + picks.IndexOf(m) + " type" + m.type + " level" + m.level;
                        errorLogs = 0; firstError = null; string failure = null; int steps = 0; int maxObjects = 0; string outcome = "timeout";
                        try
                        {
                            game.Profile.playerType = hero;
                            for (int h = 0; h < game.Profile.level.Length; h++) if (h == hero) game.Profile.level[h] = startLevel < 24 ? startLevel : Math.Max(game.Profile.level[h], 24);
                            if (startLevel < 24) for (int sl = 1; sl <= 4; sl++) game.Profile.weaponOwned[hero * 6 + sl] = -1;
                            for (int i = 0; i < game.Profile.weaponOwned.Length; i++) game.Profile.weaponOwned[i] = Math.Max(game.Profile.weaponOwned[i], 0);
                            for (int i = 0; i < game.Profile.weaponAmmo.Length; i++) game.Profile.weaponAmmo[i] = Math.Max(game.Profile.weaponAmmo[i], 200);
                            game.Profile.cash = 5000;
                            game.LaunchMission = m; game.LaunchMissionType = m.type;
                            game.SwitchScene("gameScene"); game.StepFor(0.05f);
                            var scene = (GameScene)game.Current;
                            var rnd = new System.Random(1000 * hero + 17 * m.type + m.level);
                            bool left = false, right = false, fire = false; float nextChange = 0f, t = 0f, after = 0f; bool paused = false; float pauseUntil = 0f;
                            while (t < simSeconds)
                            {
                                game.Step(1f / 60f); t += 1f / 60f; steps++; if (scene.result == GameScene.MissionResult.Running) scene.playerHp = scene.playerMaxHp;   // soak: survive so the whole mission loop is exercised
                                if (!(float.IsFinite(scene.heroX)) || !(float.IsFinite(scene.playerHp))) { failure = "non-finite hero state at t=" + t.ToString("F1"); break; }
                                if (steps % 300 == 0) maxObjects = Math.Max(maxObjects, UnityEngine.Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None).Length);
                                if (errorLogs > 0) { failure = "error log at t=" + t.ToString("F1") + ": " + firstError; break; }
                                if (scene.result != GameScene.MissionResult.Running) { outcome = scene.result.ToString(); after += 1f / 60f; if (after > 8f) break; continue; }
                                if (paused) { if (t >= pauseUntil) { var resume = scene.GetControl(66); if (resume != null) { scene.Touch(0, resume.FullRect.center); scene.Touch(2, resume.FullRect.center); } paused = false; } continue; }
                                if (t >= nextChange)
                                {
                                    nextChange = t + 0.3f + (float)rnd.NextDouble() * 1.7f;
                                    if (left) { scene.Touch(2, GameScene.LeftRect.center); left = false; }
                                    if (right) { scene.Touch(2, GameScene.RightRect.center); right = false; }
                                    int mv = rnd.Next(3);
                                    if (mv == 1) { scene.Touch(0, GameScene.LeftRect.center); left = true; } else if (mv == 2) { scene.Touch(0, GameScene.RightRect.center); right = true; }
                                    bool wantFire = rnd.NextDouble() < 0.7; if (wantFire && !fire) scene.FireDown(); else if (!wantFire && fire) scene.FireUp(); fire = wantFire;
                                    double r = rnd.NextDouble();
                                    if (r < 0.10) { scene.Touch(0, GameScene.RollRect.center); scene.Touch(2, GameScene.RollRect.center); }
                                    else if (r < 0.22) { scene.Touch(0, GameScene.HitRect.center); game.StepFor(0.15f); t += 0.15f; scene.Touch(2, GameScene.HitRect.center); }
                                    else if (r < 0.30) { scene.Touch(0, GameScene.ChangeRect.center); scene.Touch(2, GameScene.ChangeRect.center); }
                                    else if (r < 0.32) { scene.Touch(0, GameScene.StopRect.center); scene.Touch(2, GameScene.StopRect.center); paused = true; pauseUntil = t + 0.5f; }
                                }
                            }
                            if (failure == null) failure = errorLogs > 0 ? "error log: " + firstError : null;
                            log.AppendLine("INFO " + tag + " steps=" + steps + " outcome=" + outcome + " hp=" + scene.playerHp.ToString("F0") + " kills=" + scene.kills + " maxObjects=" + maxObjects);
                            if (maxObjects > 6000 && failure == null) failure = "GameObject count " + maxObjects;
                        }
                        catch (Exception e) { failure = "exception at step " + steps + ": " + e; }
                        check(tag + " plays " + steps + " steps without error", failure == null);
                        if (failure != null) log.AppendLine("  " + failure.Replace("\n", " | "));
                    }
                }
            }
            catch (Exception e) { failed++; log.AppendLine("EXCEPTION " + e); }
            finally
            {
                Application.logMessageReceived -= OnLog;
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
