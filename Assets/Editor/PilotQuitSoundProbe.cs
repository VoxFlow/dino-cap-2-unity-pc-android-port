using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>DEBUG_ONLY diagnostic (DC2-BUGS-001 r119k item 3, user: "dino spawn roar heard 1-2 times after quitting to the menu mid-match"): plays a kill mission with
    /// spawning, pauses, presses the pause menu's quit control (68) and logs every sound request made after the quit touch, per trial. Not part of the suite.</summary>
    public static class PilotQuitSoundProbe
    {
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-BUGS-001/quitsound-probe");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            var log = new StringBuilder();
            string dir = Path.Combine(Path.GetTempPath(), "dc2_quitsnd_" + Guid.NewGuid().ToString("N"));
            Camera cam = null; Dc2Game g = null;
            try
            {
                cam = Bootstrap.MakeCamera(); g = Dc2Game.Create(dir); g.cam = cam; g.manualStep = true; g.SkipMissionIntro = true;
                for (int trial = 0; trial < 12; trial++)
                {
                    g.Profile.playerType = trial % 4;
                    g.LaunchMission = new NativeMapState.Mission { type = 3, count = 40, level = 3, background = 0 }; g.LaunchMission.enemies.Add(1); g.LaunchMission.enemies.Add(2);
                    g.SwitchScene("gameScene"); g.Step(0f); g.Step(1f / 60f);
                    var s = (GameScene)g.Current; s.ignorePlayer = true;
                    g.StepFor(3f + trial * 0.37f);
                    s.PauseGame(); g.StepFor(0.6f);
                    var quit = s.GetControl(68);
                    if (quit == null) { log.AppendLine("trial " + trial + " no quit control"); continue; }
                    int before = g.Audio.RequestedSounds.Count;
                    var p = quit.FullRect.center; s.Touch(0, p, 3); g.Step(1f / 60f); s.Touch(2, p, 3);
                    int atSwitchTick = -1; var after = new StringBuilder();
                    for (int t = 0; t < 600; t++)
                    {
                        g.Step(1f / 60f);
                        if (atSwitchTick < 0 && g.CurrentName != "gameScene") atSwitchTick = t;
                    }
                    for (int k = before; k < g.Audio.RequestedSounds.Count; k++) after.Append(g.Audio.RequestedSounds[k]).Append(' ');
                    log.AppendLine("trial " + trial + " scene " + g.CurrentName + " (switched at tick " + atSwitchTick + "), sounds requested after the quit touch: " + after);
                    if (g.Audio.RequestedSounds.Count > 3500) g.Audio.RequestedSounds.Clear();
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
