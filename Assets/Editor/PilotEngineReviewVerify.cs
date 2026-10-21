using System;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>Codex review r1 (DC2-ENGINE-001, packet r3) blocking findings R1-R4 as regressions. Fixtures follow the reviewer's probes
    /// (Dev/Reviews/DC2-ENGINE-001-r3/test-root/.../EngineReviewProbe.cs); expected values come from the native ARM it cites, not from the port.
    /// R1 EngineShell::RenderScene 0x165bb4: dt = (ms now - ms last) * 0x3A83126F, capped 0x3DCCCCCD, never replaced by 1/60.
    /// R2 Game::Update 0x16a8da applies queued scene commands before Scene::Update 0x175e84 reads the touches.
    /// R3 Scene::Update caches the time factor (0x175ed2) before touch dispatch; timers of the pressing tick use the cached factor.
    /// R4 PhysicsManager::Update 0x17610a precedes every object callback: the hero's attack check sees the boss body's x of this tick.</summary>
    public static class PilotEngineReviewVerify
    {
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-ENGINE-001/engine-review-r1");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            int passed = 0, failed = 0; var log = new StringBuilder();
            Action<string, bool> check = (n, v) => { log.AppendLine((v ? "PASS " : "FAIL ") + n); if (v) passed++; else failed++; };
            Camera cam = null; Dc2Game g = null;
            try
            {
                cam = Bootstrap.MakeCamera();
                g = Dc2Game.Create(Path.Combine(Path.GetTempPath(), "dc2_engine_review_" + Guid.NewGuid().ToString("N"))); g.cam = cam; g.manualStep = true; g.SkipMissionIntro = true;

                // ---- R2: a scene switch queued in the previous frame is applied before this tick's touches are read ----
                g.SwitchScene("menuScene"); g.Step(0f); var old = g.Current;
                g.SwitchScene("selectScene"); g.QueueTouch(0, new Vector2(-9999f, -9999f), 9); g.HostTick(1f / 60f);
                check("R2: a touch queued after a pending SwitchScene reaches the incoming scene (old " + old.DebugSceneTouches + ", new " + g.Current.DebugSceneTouches + ")",
                      old.DebugSceneTouches == 0 && g.Current.DebugSceneTouches == 1 && g.CurrentName == "selectScene");

                // ---- R3: the pause touch does not freeze the tick that delivers it ----
                g.Profile.playerType = 0; g.LaunchMission = new NativeMapState.Mission { type = 3, count = 99, background = 0 };
                g.SwitchScene("gameScene"); g.Step(0f); var s = (GameScene)g.Current; s.spawningEnabled = false; s.ignorePlayer = true;
                g.HostTick(1f / 60f);   // settle the host clock
                float t0 = g.NativeTimerTime; int fired = 0; s.SetTimer(s, "review_pause", 0.001f, () => fired++);
                g.QueueTouch(0, GameScene.StopRect.center, 11); g.HostTick(1f / 60f);
                check("R3: the pressing tick still runs Timer::Update with the entry factor 1 (paused " + s.paused + ", timer delta " + (g.NativeTimerTime - t0) + ", fired " + fired + ")",
                      s.paused && g.NativeTimerTime - t0 > 0f && fired == 1);
                float t1 = g.NativeTimerTime; g.HostTick(1f / 60f);
                check("R3: the following tick is frozen (factor 0 cached at entry)", g.NativeTimerTime == t1);
                typeof(GameScene).GetMethod("ClosePause", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(s, null);
                g.Step(0f);

                // ---- item bodies move in the physics stage with the previous tick's velocity (r118z6, review bound "item / global physics") ----
                s.CreateItemForTest(1, N.Add32(s.camLeft, 300f));
                var item = s.ItemAt(s.ItemCount - 1);
                g.Step(1f / 60f);   // first callback: on-screen activation + upDown chooses vy
                float y0 = item.y, v0 = item.vy;
                check("activation tick: itemsClass::OnUpdate sets y = base + 20*S and upDown only sets the velocity, so the item has not moved yet (" + y0 + ")",
                      y0 == N.Add32(N.Mul32(N.S, 20f), item.baseY) && v0 != 0f);
                g.Step(1f / 60f);
                check("an item's y in a tick is last y + previous vy*dt (body moved before the callback, new vy only next tick): " + y0 + " + " + v0 + "*dt -> " + item.y,
                      v0 != 0f && item.y == N.Add32(y0, N.Mul32(v0, 1f / 60f)));

                // ---- Codex R5: at a bob limit upDown only flips the direction byte (0x115574 / 0x1155ac); the velocity persists one more physics step ----
                float dtT = 1f / 60f;
                item.goingDown = false; item.vy = 20f; item.y = N.Add32(N.Add32(N.Mul32(N.S, 25f), item.baseY), 0.25f);   // just past the upper limit, rising
                g.Step(dtT);
                float yU = item.y;
                check("R5 upper limit: the flip tick sets goingDown and keeps vy +20 (" + item.vy + ")", item.goingDown && item.vy == 20f);
                g.Step(dtT);
                check("R5 upper limit: the next physics step still rises with +20 (" + yU + " -> " + item.y + "), then the callback reverses to -20", item.y == N.Add32(yU, N.Mul32(20f, dtT)) && item.vy == -20f);
                item.goingDown = true; item.vy = -20f; item.y = N.Add32(N.Add32(N.Mul32(N.S, 15f), item.baseY), -0.25f);   // just past the lower limit, falling
                g.Step(dtT);
                float yL = item.y;
                check("R5 lower limit: the flip tick clears goingDown and keeps vy -20 (" + item.vy + ")", !item.goingDown && item.vy == -20f);
                g.Step(dtT);
                check("R5 lower limit: the next physics step still falls with -20 (" + yL + " -> " + item.y + "), then the callback reverses to +20", item.y == N.Add32(yL, N.Mul32(-20f, dtT)) && item.vy == 20f);

                // ---- R1: measured dt, ms-quantized, 0.1 cap ----
                g.HostTick(0.02f);   // warm-up: establishes the last-ms stamp at a 20 ms cadence
                float want = BitConverter.Int32BitsToSingle(0x3CA3D70B);   // (20 ms) * 0x3A83126F, the reviewer's ARM probe result
                int ok = 0; for (int i = 0; i < 49; i++) { g.HostTick(0.02f); if (g.DebugLastStepDt == want) ok++; }
                check("R1: 20 ms host callbacks give Application::Update dt bits 0x3CA3D70B (" + ok + "/49), not 1/60", ok >= 48);
                g.HostTick(0.25f);
                check("R1: a late callback is capped at 0x3DCCCCCD (0.1)", g.DebugLastStepDt == BitConverter.Int32BitsToSingle(0x3DCCCCCD));

                // ---- R4: the boss body is integrated in the physics stage, before the hero's attack check ----
                g.LaunchMission = new NativeMapState.Mission { type = 8, count = 1, level = 3, background = 0 };
                g.SwitchScene("gameScene"); g.Step(0f); s = (GameScene)g.Current; s.spawningEnabled = false; s.ignorePlayer = true; s.DebugBossKindRoll = () => 1;
                typeof(GameScene).GetMethod("AddBoss", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(s, null);
                var boss = s.Boss; boss.kind = 2; boss.sub = 2; boss.left = false; boss.velX = 100f; s.heroX = 0f; s.facingLeft = false;
                var weapon = s.CurrentWeapon();
                boss.x = N.Add32(s.heroX, N.Add32(N.Mul32(N.S, weapon.atkDis), -0.5f));
                float x0 = boss.x, expected = N.Add32(x0, N.Mul32(boss.velX, 1f / 60f)), seen = float.NaN; bool candidate = true;
                s.DebugBeforeBeams = () => { s.DebugBeforeBeams = null; seen = boss.x; float delta; candidate = s.BossCandidateForTest(weapon, out delta); };
                g.Step(1f / 60f);
                check("R4: inside the hero's update the boss x is already this tick's physics position (" + seen + " vs " + expected + ")", seen == expected);
                check("R4: the attack-window boundary uses that position (boss just left the range: not a candidate)", !candidate);
            }
            catch (Exception e) { failed++; log.AppendLine("EXCEPTION " + e); }
            finally
            {
                if (g != null) UnityEngine.Object.DestroyImmediate(g.gameObject);
                if (cam != null) UnityEngine.Object.DestroyImmediate(cam.gameObject);
                log.AppendLine("SUMMARY passes=" + passed + " fails=" + failed);
                File.WriteAllText(Path.Combine(output, "verify.txt"), log.ToString()); Debug.Log(log.ToString());
                PilotExit.Exit(failed == 0 ? 0 : 1);
            }
        }
    }
}
