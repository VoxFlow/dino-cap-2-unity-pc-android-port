using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>DEBUG_ONLY diagnostic (DC2-BUGS-001 r119k item 8, user: "the boss switches from tail to head roar/lava, never seen on iPad"): boss missions at several
    /// levels, the hero invulnerable, logs the attack kind chosen at every OnIdle over 120 s. Not part of the suite.</summary>
    public static class PilotBossKindProbe
    {
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-BUGS-001/bosskind-probe");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            var log = new StringBuilder();
            string dir = Path.Combine(Path.GetTempPath(), "dc2_bosskind_" + Guid.NewGuid().ToString("N"));
            Camera cam = null; Dc2Game g = null;
            try
            {
                cam = Bootstrap.MakeCamera(); g = Dc2Game.Create(dir); g.cam = cam; g.manualStep = true; g.SkipMissionIntro = true; g.DebugGodMode = true;
                foreach (int level in new[] { 1, 3, 5, 8, 12, 20 })
                {
                    g.LaunchMission = new NativeMapState.Mission { type = 8, count = 1, level = level, background = 0 };
                    g.Profile.playerType = 0;
                    g.SwitchScene("gameScene"); g.StepFor(0.02f);
                    var s = (GameScene)g.Current; s.spawningEnabled = false;
                    var seq = new StringBuilder(); int lastKind = -1, lastSub = -1;
                    for (int t = 0; t < 60 * 120; t++)
                    {
                        g.Step(1f / 60f);
                        var b = s.Boss; if (b == null) continue;
                        if (b.sub == 1 && lastSub != 1) seq.Append(b.kind == 1 ? "hou " : b.kind == 2 ? "jiao " : b.kind == 3 ? "wei " : "k" + b.kind + " ");
                        lastSub = b.sub; lastKind = b.kind;
                    }
                    var bb = s.Boss;
                    log.AppendLine("level " + level + " anis [" + (bb != null ? string.Join(",", bb.def.anis.ConvertAll(a => a.type)) : "-") + "] OnIdle kinds: " + seq);
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
