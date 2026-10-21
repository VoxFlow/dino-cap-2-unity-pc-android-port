using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>DEBUG_ONLY diagnostic (DC2-BUGS-001 r119k item 7, user: "after killing the stomp boss its foot shadow is briefly shown at a fixed position"): jiao boss,
    /// hp set to 0 during its attack, logs sub-state, animation frame, drawn components and position each tick until the death animation starts. Not part of the suite.</summary>
    public static class PilotBossDeathProbe
    {
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-BUGS-001/bossdeath-probe");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            var log = new StringBuilder();
            string dir = Path.Combine(Path.GetTempPath(), "dc2_bossdeath_" + Guid.NewGuid().ToString("N"));
            Camera cam = null; Dc2Game g = null;
            try
            {
                cam = Bootstrap.MakeCamera(); g = Dc2Game.Create(dir); g.cam = cam; g.manualStep = true; g.SkipMissionIntro = true;
                g.LaunchMission = new NativeMapState.Mission { type = 8, count = 1, level = 3, background = 0 };   // boss line 2: hou + jiao
                g.Profile.playerType = 0; g.Profile.level[0] = 2;
                g.SwitchScene("gameScene"); g.StepFor(0.02f);
                var s = (GameScene)g.Current; s.spawningEnabled = false; s.ignorePlayer = true;
                s.DebugBossKindRoll = () => 2;   // second <ani>: jiao
                g.StepFor(2.2f);
                var b = s.Boss;
                string last = "";
                bool zeroed = false;
                for (int t = 0; t < 60 * 40 && !(b.kind == 4 && b.sub == 6); t++)
                {
                    g.Step(1f / 60f);
                    if (!zeroed && b.kind == 2 && b.sub == 4) { b.hp = 0f; zeroed = true; log.AppendLine("tick " + t + " hp := 0 (sub 4)"); }
                    if (!zeroed) continue;
                    string comps = string.Join(",", b.ca.Sprites.Keys.OrderBy(k => k));
                    string st = "kind " + b.kind + " sub " + b.sub + " frame " + b.ca.CurrentFrame + "/" + (b.ca.animation.FrameCount - 1) + " fin " + b.ca.Fin + " x " + b.x + " comps-drawn-now " + DrawnNames(b);
                    if (st != last) log.AppendLine("tick " + t + " " + st);
                    last = st;
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

        static string DrawnNames(BossActor b)
        {
            var a = b.ca.animation; int f = b.ca.CurrentFrame;
            return string.Join(",", a.components.Where(c => f < c.frames.Length && c.frames[f] != null && c.avatar.Length > 0).Select(c => c.avatar + (c.avatar == "boss_yz" || c.avatar.StartsWith("boss_b") ? "[x" + c.frames[f].x + " y" + c.frames[f].y + " s" + c.frames[f].scaleX + " a" + (c.frames[f].rgba >> 24) + "]" : "")));
        }
    }
}
