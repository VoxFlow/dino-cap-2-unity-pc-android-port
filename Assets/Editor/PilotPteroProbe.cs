using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>DEBUG_ONLY diagnostic (DC2-BUGS-001 r119i item 2, user: "heroes can't harm pterodactyls"): spawns every skin-5 kind in front of the hero, logs the
    /// animation extents, the viewport gate and the head point, then fires a pistol shot and a melee hit and logs the hp. Not part of the suite.</summary>
    public static class PilotPteroProbe
    {
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-BUGS-001/ptero-probe");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            var log = new StringBuilder();
            string dir = Path.Combine(Path.GetTempPath(), "dc2_ptero_" + Guid.NewGuid().ToString("N"));
            Camera cam = null; Dc2Game g = null;
            try
            {
                cam = Bootstrap.MakeCamera(); g = Dc2Game.Create(dir); g.cam = cam; g.manualStep = true; g.SkipMissionIntro = true;
                var props = Dc2GameData.Dinos();
                for (int kind = 1; kind <= props.Length; kind++)
                {
                    if (props[kind - 1].skin != 5) continue;
                    foreach (float dx in new[] { 150f, 400f })
                    {
                        g.Profile.playerType = 0;
                        g.LaunchMission = new NativeMapState.Mission { type = 3, count = 20, level = 1, background = 0 }; g.LaunchMission.enemies.Add(1);
                        g.SwitchScene("gameScene"); g.Step(0f); g.Step(1f / 60f);
                        var s = (GameScene)g.Current; s.spawningEnabled = false; s.ignorePlayer = true; s.DebugSpawnModeRoll = () => 99;
                        s.heroX = s.camLeft + 300f; s.facingLeft = false;
                        var d = s.CreateEnemy(kind); d.hp = 10000f; d.x = s.heroX + dx; g.Step(1f / 60f);
                        var a = d.rig.animation;
                        log.AppendLine("kind " + kind + " dx " + dx + " x " + d.x + " y " + d.y + " state " + d.state + " targetable " + d.targetable + " dead " + d.dead + " anim " + (a != null ? a.minX + ".." + a.maxX : "null")
                            + " inView " + s.InViewportForTest(d) + " headX " + s.HeadXForTest(d) + " parts " + string.Join(",", d.rig.PartInfos.Keys));
                        float hp0 = d.hp; s.fireDown = true; g.StepFor(0.6f); s.fireDown = false; g.StepFor(0.3f);
                        log.AppendLine("  pistol hp " + hp0 + " -> " + d.hp + " state " + d.state + " x " + d.x + " inView " + s.InViewportForTest(d) + " headX " + s.HeadXForTest(d));
                        hp0 = d.hp; d.x = s.heroX + 60f; g.Step(1f / 60f); s.OnHitDown(); g.StepFor(0.8f);
                        log.AppendLine("  melee hp " + hp0 + " -> " + d.hp + " state " + d.state + " x " + d.x + " headX " + s.HeadXForTest(d));
                    }
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
