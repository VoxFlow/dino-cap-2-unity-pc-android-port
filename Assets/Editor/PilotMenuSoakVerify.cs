using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>Menu monkey test (logic only, DEBUG_ONLY): starts every menu scene (menuScene, selectScene map types 0..6) and taps random touchable controls / random points with seeded randomness,
    /// with several cash / progress profiles, for -menuSeconds simulated seconds each (default 60). Scene switches into gameScene run a few seconds then return to the map.
    /// Fails on any exception or Unity error log. Args: -menuSeed N (default 1).</summary>
    public static class PilotMenuSoakVerify
    {
        static int errorLogs; static string firstError;
        static void OnLog(string msg, string stack, LogType t)
        {
            if ((t == LogType.Error || t == LogType.Exception || t == LogType.Assert) && !msg.StartsWith("Destroy may not be called from edit mode")) { errorLogs++; if (firstError == null) firstError = msg + "\n" + stack; }
        }

        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-PILOT-001/menusoak-r1");
            var args = Environment.GetCommandLineArgs(); float seconds = 60f; int seed = 1;
            for (int i = 0; i < args.Length - 1; i++)
            {
                if (args[i] == "-reportDir") output = args[i + 1];
                if (args[i] == "-menuSeconds") seconds = float.Parse(args[i + 1]);
                if (args[i] == "-menuSeed") seed = int.Parse(args[i + 1]);
            }
            Directory.CreateDirectory(output);
            string dir = Path.Combine(Path.GetTempPath(), "dc2_menusoak_" + Guid.NewGuid().ToString("N"));
            int passed = 0, failed = 0; var log = new StringBuilder();
            Action<string, bool> check = (n, v) => { log.AppendLine((v ? "PASS " : "FAIL ") + n); if (v) passed++; else failed++; };
            Dc2Game game = null; Camera cam = null;
            Application.logMessageReceived += OnLog;
            try
            {
                cam = Bootstrap.MakeCamera(); game = Dc2Game.Create(dir); game.manualStep = true; game.cam = cam;
                var missions = new NativeMapState().missions;
                var profiles = new[] { new { name = "poor", cash = 50, level = 0 }, new { name = "rich", cash = 5000000, level = 30 }, new { name = "mid", cash = 4000, level = 8 } };
                var scenes = new List<string[]>();
                scenes.Add(new[] { "menuScene", "-1" });
                for (int mt = 0; mt <= 6; mt++) scenes.Add(new[] { "selectScene", mt.ToString() });
                foreach (var prof in profiles)
                foreach (var sc in scenes)
                {
                    string tag = prof.name + " " + sc[0] + " maptype " + sc[1];
                    errorLogs = 0; firstError = null; string failure = null; int steps = 0; int switches = 0;
                    try
                    {
                        game.Profile.cash = prof.cash;
                        for (int h = 0; h < 5; h++) if (game.Profile.level[h] >= 0) game.Profile.level[h] = Math.Max(game.Profile.level[h], prof.level);
                        game.Profile.playerType = seed % 5;
                        game.LaunchMission = missions[(seed * 7) % missions.Count]; game.LaunchMissionType = game.LaunchMission.type;
                        if (sc[1] != "-1") game.MapType = int.Parse(sc[1]);
                        game.SwitchScene(sc[0]); game.StepFor(0.1f);
                        var rnd = new System.Random(seed * 1000 + tag.GetHashCode() % 997);
                        float t = 0f, next = 0f, gameUntil = 0f;
                        while (t < seconds)
                        {
                            game.Step(1f / 30f); t += 1f / 30f; steps++;
                            if (errorLogs > 0) { failure = "error log at t=" + t.ToString("F1") + ": " + firstError; break; }
                            var cur = game.Current; if (cur == null) continue;
                            if (cur is GameScene)
                            {
                                if (gameUntil == 0f) { gameUntil = t + 4f; switches++; }
                                if (t >= gameUntil) { gameUntil = 0f; game.MapType = 0; game.SwitchScene("selectScene"); }
                                continue;
                            }
                            if (t < next) continue;
                            next = t + 0.15f + (float)rnd.NextDouble() * 0.5f;
                            Vector2 p; var list = cur.Controls.Where(c => c != null && c.touchable).ToList();
                            if (list.Count > 0 && rnd.NextDouble() < 0.85) p = list[rnd.Next(list.Count)].FullRect.center;
                            else p = new Vector2((float)rnd.NextDouble() * 1024f, (float)rnd.NextDouble() * 768f);
                            bool drag = rnd.NextDouble() < 0.2;
                            cur.Touch(0, p);
                            if (game.Current != cur || cur == null) continue;   // the press switched scene: the old scene is gone and must not get the release
                            if (drag) { for (int k = 1; k <= 3 && game.Current == cur; k++) { cur.Touch(1, p + new Vector2(k * 40f, 0f)); game.Step(1f / 30f); t += 1f / 30f; steps++; } if (game.Current == cur) cur.Touch(2, p + new Vector2(120f, 0f)); }
                            else cur.Touch(2, p);
                        }
                        if (failure == null && errorLogs > 0) failure = "error log: " + firstError;
                        log.AppendLine("INFO " + tag + " steps=" + steps + " gameSwitches=" + switches);
                    }
                    catch (Exception e) { failure = "exception at step " + steps + ": " + e; }
                    check(tag + " survives " + steps + " random-tap steps", failure == null);
                    if (failure != null) log.AppendLine("  " + failure.Replace("\n", " | "));
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
