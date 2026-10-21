using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>Frame-rate independence. Native EngineShell::RenderScene 0x165bb4 feeds Application::Update the measured elapsed time (ms * 0.001,
    /// capped at 0.1 s) from a 60 Hz NSTimer, and PhysicsManager::Update steps Box2D with that dt, so game speed does not depend on the frame rate.
    /// The same scenario is run with 60, 144 and 240 Hz steps for the same total time; positions, animation frames and dinosaur progress must agree.</summary>
    public static class PilotFrameRateVerify
    {
        struct Sample { public float heroX, dinoX, camLeft; public int heroFrame, dinoFrame, dinos; }

        static Sample RunScenario(float hz, string dir, Camera cam, bool hostClock)
        {
            var game = Dc2Game.Create(dir); game.manualStep = true; game.SkipMissionIntro = true; game.cam = cam;
            try
            {
                game.LaunchMission = new NativeMapState.Mission { type = 3, count = 99, background = 0 }; game.LaunchMission.enemies.Add(1);
                game.Profile.playerType = 0;
                game.SwitchScene("gameScene"); game.Step(0f);
                var s = (GameScene)game.Current; s.spawningEnabled = false; s.ignorePlayer = true; s.heroX = 1000f; s.DebugSpawnModeRoll = () => 0;
                game.Step(0f);
                var d = s.CreateEnemy(1); d.x = s.heroX + 700f; d.faceRight = false; d.state = 0;
                float step = 1f / hz;
                s.Touch(0, GameScene.RightRect.center, 1);
                for (float t = 0f; t < 1.5f - 1e-5f; t += step) { if (hostClock) game.HostTick(step); else game.Step(step); }
                s.Touch(2, GameScene.RightRect.center, 1);
                for (float t = 0f; t < 0.5f - 1e-5f; t += step) { if (hostClock) game.HostTick(step); else game.Step(step); }
                return new Sample { heroX = s.heroX, dinoX = d.x, camLeft = s.camLeft, heroFrame = s.Rig.Player.CurrentFrame, dinoFrame = d.rig.CurrentFrame, dinos = s.dinos.Count };
            }
            finally { UnityEngine.Object.DestroyImmediate(game.gameObject); }
        }

        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-PILOT-001/framerate-r1");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            string dir = Path.Combine(Path.GetTempPath(), "dc2_framerate_" + Guid.NewGuid().ToString("N"));
            int passed = 0, failed = 0; var log = new StringBuilder();
            Action<string, bool> check = (n, v) => { log.AppendLine((v ? "PASS " : "FAIL ") + n); if (v) passed++; else failed++; };
            Camera cam = null;
            try
            {
                cam = Bootstrap.MakeCamera();
                // ---- first Application::Update after launch (EngineShell::InitView 0x165c30 stamps now - 1000 ms; RenderScene caps dt at 0.1) ----
                {
                    var g0 = Dc2Game.Create(dir + "first"); g0.manualStep = true; g0.cam = cam;
                    try
                    {
                        g0.SwitchScene("menuScene");
                        g0.HostTick(1f / 60f); float first = g0.DebugLastStepDt;
                        g0.HostTick(1f / 60f); float second = g0.DebugLastStepDt;
                        check("the first host tick after launch runs Application::Update with dt 0.1 (InitView stamp)", first == 0.1f);
                        // RenderScene 0x165bb4: (ms now - ms last) * 0x3A83126F; host times 16.67 ms and 33.33 ms -> PVRShellGetTime 16 and 33 -> 17 ms (Codex review R1, r118z5)
                        check("later host ticks run with the measured ms-quantized dt (17 ms here), not a fixed 1/60", second == N.Mul32(17f, BitConverter.Int32BitsToSingle(0x3A83126F)) && second != 1f / 60f);
                    }
                    finally { UnityEngine.Object.DestroyImmediate(g0.gameObject); }
                }
                var a = RunScenario(60f, dir + "a", cam, false);   // reference: ideal 60 Hz ticks (dt exactly 1/60)
                var b = RunScenario(144f, dir + "b", cam, true); var c = RunScenario(240f, dir + "c", cam, true); var e = RunScenario(60f, dir + "e", cam, true);
                var raw = RunScenario(240f, dir + "r", cam, false);  // diagnostic: one tick per 240 Hz frame (the old host loop)
                foreach (var x in new[] { a, b, c, e, raw }) log.AppendLine("SAMPLE heroX=" + x.heroX + " dinoX=" + x.dinoX + " cam=" + x.camLeft + " heroFrame=" + x.heroFrame + " dinoFrame=" + x.dinoFrame + " dinos=" + x.dinos);
                Func<float, float, bool> near = (p, q) => Mathf.Abs(p - q) <= 6f;   // one 60 Hz step of walking at 300 px/s is 5 px
                foreach (var kv in new[] { new { n = "144 Hz display", v = b }, new { n = "240 Hz display", v = c }, new { n = "60 Hz display", v = e } })
                {
                    check("host clock " + kv.n + ": hero walk distance matches native 60 Hz", near(a.heroX, kv.v.heroX));
                    check("host clock " + kv.n + ": dinosaur travel matches", near(a.dinoX, kv.v.dinoX));
                    // With the native measured dt (r118z5) the tick spacing follows the display (13.9/20.8 ms at 144 Hz, 16/17 ms at 60 Hz) and CAObjCallBack
                    // drops its residue per advance, so the frame can differ by one from ideal 1/60 ticks, as on a device with a jittery run loop. The guard is
                    // against the old defect (one tick per display frame, animating up to 4x faster), not an exact native oracle.
                    check("host clock " + kv.n + ": hero animation frame within one of ideal 60 Hz", Math.Abs(a.heroFrame - kv.v.heroFrame) <= 1);
                    check("host clock " + kv.n + ": dinosaur animation frame within one of ideal 60 Hz", Math.Abs(a.dinoFrame - kv.v.dinoFrame) <= 1);
                }
                check("diagnostic: ticking once per 240 Hz frame animates faster (the defect the host clock removes)", raw.heroFrame != a.heroFrame || raw.dinoFrame != a.dinoFrame);
            }
            catch (Exception e) { failed++; log.AppendLine("EXCEPTION " + e); }
            finally
            {
                if (cam != null) UnityEngine.Object.DestroyImmediate(cam.gameObject);
                log.AppendLine("SUMMARY passes=" + passed + " fails=" + failed);
                File.WriteAllText(Path.Combine(output, "verify.txt"), log.ToString()); Debug.Log(log.ToString());
                PilotExit.Exit(failed == 0 ? 0 : 1);
            }
        }
    }
}
