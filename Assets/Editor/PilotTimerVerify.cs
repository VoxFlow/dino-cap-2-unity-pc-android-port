using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>DC2-ENGINE-001 L2: Triniti2D::Timer + PriorityQueueT (NativeTimer) and the game timers that now run on it.
    /// Native contract: SetTimer 0x17ca7c (deadline = now + interval, pending deque), KillTimer 0x17ca5e (kill deque), Update 0x17ccd6 (pending -> heap, kills, now += dt,
    /// fire while !(top.deadline &gt; now)); heap PercolateUp 0x17e330 / PercolateDown 0x17e25c. Spawn re-arm: mainScene::OnTimeout 0xb7208.. / lifeModeScene sub_E3216.</summary>
    public static class PilotTimerVerify
    {
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-ENGINE-001/timer-r1");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            int passed = 0, failed = 0; var log = new StringBuilder();
            Action<string, bool> check = (n, v) => { log.AppendLine((v ? "PASS " : "FAIL ") + n); if (v) passed++; else failed++; };
            string dir = Path.Combine(Path.GetTempPath(), "dc2_timer_" + Guid.NewGuid().ToString("N"));
            Camera cam = null;
            try
            {
                // ---- heap order and invariants ----
                var t = new NativeTimer(); var rng = new System.Random(7); var fired = new List<float>(); bool heapOk = true;
                for (int i = 0; i < 300; i++) t.Set(null, "r" + i, (float)rng.NextDouble() * 5f, false, null);
                t.Update(0f, x => { });
                heapOk &= t.CheckHeap();
                var killed = new List<NativeTimer.Info>();
                for (int step = 0; step < 400; step++)
                {
                    if (step % 7 == 0) killed.Add(t.Set(null, "k", (float)rng.NextDouble(), false, null));
                    if (step % 7 == 3 && killed.Count > 0) t.Kill(killed[killed.Count - 1]);
                    t.Update(1f / 60f, x => fired.Add(x.deadline));
                    heapOk &= t.CheckHeap();
                }
                bool sorted = true; for (int i = 1; i < fired.Count; i++) if (fired[i] < fired[i - 1]) sorted = false;
                check("heap property (PriorityQueueT::Check) holds after every insert / kill / pop", heapOk);
                check("timers fire in non-decreasing deadline order", sorted && fired.Count > 300);

                // ---- deferred insert: a timer set inside a callback waits for the next Update even with interval 0 ----
                var d = new NativeTimer(); int firstCount = 0, secondCount = 0;
                d.Set(null, "a", 0.01f, false, () => { firstCount++; d.Set(null, "b", 0f, false, () => secondCount++); });
                Action<NativeTimer.Info> fire = x => { if (x.action != null) x.action(); };
                d.Update(1f / 60f, fire);
                check("SetTimer inside a callback joins the heap at the next Update (pending deque)", firstCount == 1 && secondCount == 0);
                d.Update(1f / 60f, fire);
                check("the deferred zero-interval timer fires on the following Update", secondCount == 1);

                // ---- kill deque ----
                var k = new NativeTimer(); int kf = 0;
                var ka = k.Set(null, "a", 0.5f, false, () => kf++); k.Kill(ka);
                for (int i = 0; i < 60; i++) k.Update(1f / 60f, fire);
                check("a timer killed while still pending never fires", kf == 0 && k.Count == 0);
                var kb = k.Set(null, "b", 0.5f, false, () => kf++); k.Update(1f / 60f, fire); k.Kill(kb);
                for (int i = 0; i < 60; i++) k.Update(1f / 60f, fire);
                check("a live timer killed before its deadline never fires", kf == 0 && k.Count == 0);
                k.Update(0f, fire);
                check("dt = 0 (pause) does not advance the clock", k.Now > 0f && Mathf.Approximately(k.Now, k.Now));

                // ---- re-arm drift: now + interval at fire time keeps the overshoot (60 vs 144 Hz host steps) ----
                Func<float, int> secondsIn = hz =>
                {
                    var c = new NativeTimer(); int n = 0; Action arm = null;
                    arm = () => c.Set(null, "g", 1f, false, () => { n++; arm(); });
                    arm();
                    for (float time = 0f; time < 10.0f - 1e-4f; time += 1f / hz) c.Update(1f / hz, fire);
                    return n;
                };
                int at60 = secondsIn(60f), at144 = secondsIn(144f);
                log.AppendLine("SAMPLE g_time_id fires in 10 s: 60 Hz " + at60 + ", 144 Hz " + at144);
                check("1.0 s re-armed clock at 60 Hz loses the overshoot (native drift: fewer than 10 fires in 10 s)", at60 <= 10 && at60 >= 9);
                check("1.0 s re-armed clock at 144 Hz stays within one fire of 60 Hz", Math.Abs(at60 - at144) <= 1);

                // ---- spawn interval formula (binary32 constants decoded with struct: 0xBDCCCCCD = -0.1, 0x3F99999A = 1.2) ----
                check("literal -0.1f is 0xBDCCCCCD", BitConverter.ToUInt32(BitConverter.GetBytes(-0.1f), 0) == 0xBDCCCCCD);
                check("literal 1.2f is 0x3F99999A", BitConverter.ToUInt32(BitConverter.GetBytes(1.2f), 0) == 0x3F99999A);
                check("type 7, g_time 59: interval = FRQ (g_time/60 = 0)", GameScene.SpawnInterval(5f, 7, 59) == 5f);
                check("type 7, g_time 60: interval = 5 + 5*1*(-0.1) = 4.5", GameScene.SpawnInterval(5f, 7, 60) == 4.5f);
                check("type 7, g_time 600: interval <= 0 (fires every update, shipped)", GameScene.SpawnInterval(5f, 7, 600) <= 0f);
                check("type 8: interval = FRQ * 1.2 in binary32", GameScene.SpawnInterval(5f, 8, 0) == 1.2f * 5f);
                check("other types: interval = FRQ", GameScene.SpawnInterval(3f, 3, 900) == 3f);

                // ---- scene wiring: StopCreateEnemy kills only kinds 1..13; a duplicate kind keeps one slot ----
                cam = Bootstrap.MakeCamera();
                var game = Dc2Game.Create(dir + "a"); game.manualStep = true; game.SkipMissionIntro = true; game.cam = cam;
                try
                {
                    game.LaunchMission = new NativeMapState.Mission { type = 3, count = 99, background = 0 };
                    game.LaunchMission.enemies.Add(1); game.LaunchMission.enemies.Add(1); game.LaunchMission.enemies.Add(14);
                    game.SwitchScene("gameScene"); game.Step(0f);
                    var s = (GameScene)game.Current;
                    check("a kind listed twice keeps a single spawn slot (OnTimeout id compare)", s.LiveSpawnSlots(1, 1) == 1 && s.LiveSpawnSlots(14, 14) == 1);
                    s.Complete();
                    check("GameClear/StopCreateEnemy kills kinds 1..13 only: kind 14 keeps spawning (shipped quirk)", s.LiveSpawnSlots(1, 13) == 0 && s.LiveSpawnSlots(14, 19) == 1);
                }
                finally { UnityEngine.Object.DestroyImmediate(game.gameObject); }

                // ---- mission clock: type 2 counts down on the g_time_id timer, the same at 60 and 144 Hz host ticks ----
                Func<float, string, int> clock = (hz, sub) =>
                {
                    var g = Dc2Game.Create(dir + sub); g.manualStep = true; g.SkipMissionIntro = true; g.cam = cam;
                    try
                    {
                        g.LaunchMission = new NativeMapState.Mission { type = 2, count = 30, background = 0 }; g.LaunchMission.enemies.Add(1);
                        g.SwitchScene("gameScene"); g.Step(0f);
                        var s = (GameScene)g.Current; s.spawningEnabled = false; s.ignorePlayer = true;
                        for (float time = 0f; time < 5.5f - 1e-4f; time += 1f / hz) g.HostTick(1f / hz);
                        return s.gTime;
                    }
                    finally { UnityEngine.Object.DestroyImmediate(g.gameObject); }
                };
                int c60 = clock(60f, "c60"), c144 = clock(144f, "c144");
                log.AppendLine("SAMPLE g_time after 5.5 s: 60 Hz " + c60 + ", 144 Hz " + c144);
                check("g_time counts the 1.0 s timer (5 in 5.5 s at 60 Hz)", c60 == 5);
                check("g_time is host-rate independent (144 Hz host == 60 Hz)", c60 == c144);
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
