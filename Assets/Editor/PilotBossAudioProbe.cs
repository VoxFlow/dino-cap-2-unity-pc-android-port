using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>DEBUG_ONLY diagnostic (DC2-BUGS-001 r119d item 3): runs boss fights for every boss line and logs each boss sound request with its host tick,
    /// so repeated requests (same sound on consecutive ticks) can be counted. Not part of the suite.</summary>
    public static class PilotBossAudioProbe
    {
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-BUGS-001/bossaudio-probe");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            var log = new StringBuilder();
            string dir = Path.Combine(Path.GetTempPath(), "dc2_bossaudio_" + Guid.NewGuid().ToString("N"));
            Camera cam = null; Dc2Game g = null;
            try
            {
                cam = Bootstrap.MakeCamera(); g = Dc2Game.Create(dir); g.cam = cam; g.manualStep = true; g.SkipMissionIntro = true;
                foreach (int level in new[] { 1, 2, 3, 4, 5 })
                {
                    g.LaunchMission = new NativeMapState.Mission { type = 8, count = 1, level = level, background = 0 };
                    g.SwitchScene("gameScene"); g.Step(0f);
                    var s = (GameScene)g.Current; s.spawningEnabled = false; g.DebugGodMode = true;
                    int start = g.Audio.RequestedSounds.Count; string last = null; int lastTick = -10, total = 0, consecutive = 0;
                    for (int tick = 0; tick < 60 * 40; tick++)
                    {
                        int before = g.Audio.RequestedSounds.Count;
                        g.Step(1f / 60f);
                        for (int k = before; k < g.Audio.RequestedSounds.Count; k++)
                        {
                            string n = g.Audio.RequestedSounds[k];
                            if (!n.StartsWith("A_boss") && !n.StartsWith("Audio_att")) continue;
                            total++;
                            if (n == last && tick - lastTick <= 1) consecutive++;
                            log.AppendLine("level " + level + " tick " + tick + " " + n);
                            last = n; lastTick = tick;
                        }
                    }
                    log.AppendLine("SUMMARY level " + level + ": boss sounds " + total + ", repeats on the next tick " + consecutive);
                    g.DebugGodMode = false;
                }
            }
            catch (Exception e) { log.AppendLine("EXCEPTION " + e); }
            finally
            {
                if (g != null) UnityEngine.Object.DestroyImmediate(g.gameObject);
                if (cam != null) UnityEngine.Object.DestroyImmediate(cam.gameObject);
                if (Directory.Exists(dir)) Directory.Delete(dir, true);
                File.WriteAllText(Path.Combine(output, "verify.txt"), log.ToString()); Debug.Log(log.ToString());
                PilotExit.Exit(0);
            }
        }
    }
}
