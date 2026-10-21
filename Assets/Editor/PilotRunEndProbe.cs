using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>DEBUG_ONLY diagnostic (DC2-BUGS-001 r119d item 11): every hero rolls across the Endurance (run) finish test; logs x, body velocity, state,
    /// visibility and animation per tick so the result can be compared with the user's iPad description. Not part of the suite.</summary>
    public static class PilotRunEndProbe
    {
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-BUGS-001/runend-probe");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            var log = new StringBuilder();
            string dir = Path.Combine(Path.GetTempPath(), "dc2_runend_" + Guid.NewGuid().ToString("N"));
            Camera cam = null; Dc2Game g = null;
            try
            {
                cam = Bootstrap.MakeCamera(); g = Dc2Game.Create(dir); g.cam = cam; g.manualStep = true; g.SkipMissionIntro = true;
                foreach (int hero in new[] { 0, 1, 2, 3, 4 })
                    foreach (float rollAt in new[] { 60f, 200f })   // distance before the finish test at which the roll starts
                    {
                        g.Profile.playerType = hero;
                        g.LaunchMission = new NativeMapState.Mission { type = 1, count = 1500, level = 1, background = 0 };
                        g.SwitchScene("gameScene"); g.Step(0f); g.Step(1f / 60f);
                        var s = (GameScene)g.Current; s.spawningEnabled = false; s.ignorePlayer = true; g.DebugGodMode = true;
                        s.DebugTeleportNearRunEnd(); g.Step(1f / 60f);
                        float line = N.Add32(N.Mul32(N.S, -180f), s.RunTotal);
                        s.rightDown = true;
                        log.AppendLine("== hero " + hero + " roll " + rollAt + " px before the line (line x " + line + ", total " + s.RunTotal + ")");
                        bool rolled = false; string last = null;
                        for (int tick = 0; tick < 60 * 6; tick++)
                        {
                            if (!rolled && s.heroX > line - rollAt) { s.OnRollDown(); rolled = true; log.AppendLine("tick " + tick + " ROLL at x " + s.heroX); }
                            g.Step(1f / 60f);
                            string st = "x " + s.heroX.ToString("0.0") + " cam " + s.camLeft.ToString("0.0") + " st " + s.heroControlState + " roll " + s.rolling + " vis " + (hero == 4 ? s.GirlBaseName : (s.HeroVisible ? "1" : "0") + " " + s.Rig.Source) + " end " + s.runEnded;
                            string key = st.Substring(st.IndexOf(" st "));
                            if (key != last || tick % 15 == 0) log.AppendLine("tick " + tick + " " + st);
                            last = key;
                        }
                        log.AppendLine("FINAL hero " + hero + " x " + s.heroX + " screenX " + (s.heroX - s.camLeft) + " (native view right edge 1024)");
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
