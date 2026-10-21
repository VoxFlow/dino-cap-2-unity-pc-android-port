using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>DEBUG_ONLY diagnostic (DC2-BUGS-001 r119w, user: "bullet casings despawn faster on iPad"): one shot per hero, then logs the first casing every tick
    /// (spawn position, velocity, bounces, lifetime until bulletClass::Disappear). Not part of the suite.</summary>
    public static class PilotShellLifeProbe
    {
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-BUGS-001/shell-probe");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            var log = new StringBuilder();
            string dir = Path.Combine(Path.GetTempPath(), "dc2_shell_" + Guid.NewGuid().ToString("N"));
            Camera cam = null; Dc2Game g = null;
            try
            {
                cam = Bootstrap.MakeCamera(); g = Dc2Game.Create(dir); g.cam = cam; g.manualStep = true; g.SkipMissionIntro = true;
                foreach (int hero in new[] { 0, 1, 2, 3 })
                {
                    g.Profile.playerType = hero; if (g.Profile.level[hero] < 0) g.Profile.level[hero] = 0;
                    for (int k = 0; k < 6; k++) g.Profile.weaponAmmo[hero * 6 + k] = 300;
                    g.LaunchMission = new NativeMapState.Mission { type = 3, count = 20, level = 1, background = 0 }; g.LaunchMission.enemies.Add(1);
                    g.SwitchScene("gameScene"); g.Step(0f); g.Step(1f / 60f);
                    var s = (GameScene)g.Current; s.spawningEnabled = false; s.ignorePlayer = true;
                    g.StepFor(0.5f);
                    var bw = s.BloodForTest; int before = bw.ShellsSpawned;
                    g.QueueTouch(0, GameScene.FireRect.center, 7); g.Step(1f / 60f); g.QueueTouch(2, GameScene.FireRect.center, 7);
                    BloodParticle p = null; int ticks = 0, bounces = 0; float lastVy = 0f; float spawnY = 0f, peakY = float.MinValue;
                    for (int t = 0; t < 600; t++)
                    {
                        g.Step(1f / 60f);
                        if (p == null && bw.ShellsSpawned > before) { foreach (var q in bw.shells) if (q.enabled) { p = q; break; } if (p != null) { spawnY = p.pos.y; lastVy = p.vel.y; } }
                        if (p == null) continue;
                        if (!p.enabled) break;
                        ticks++; if (p.pos.y > peakY) peakY = p.pos.y;
                        if (lastVy < 0f && p.vel.y > 0f) bounces++;
                        lastVy = p.vel.y;
                    }
                    log.AppendLine("SAMPLE hero " + hero + ": shells spawned " + (bw.ShellsSpawned - before) + (p == null ? " (none)" : ", spawn y " + spawnY + ", peak y " + peakY + ", base y " + p.baseY + ", bounces " + bounces + ", lifetime " + ticks + " ticks = " + (ticks / 60f).ToString("0.000") + " s"));
                }
                log.AppendLine("SUMMARY passes=0 fails=0");
            }
            catch (Exception ex) { log.AppendLine("EXCEPTION " + ex); }
            finally
            {
                if (g != null) UnityEngine.Object.DestroyImmediate(g.gameObject); if (cam != null) UnityEngine.Object.DestroyImmediate(cam.gameObject);
                File.WriteAllText(Path.Combine(output, "verify.txt"), log.ToString()); Debug.Log(log.ToString()); PilotExit.Exit(0);
            }
        }
    }
}
