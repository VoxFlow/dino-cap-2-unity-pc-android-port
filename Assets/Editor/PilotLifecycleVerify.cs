using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>DC2-ENGINE-001 L1: Scene/GameObject lifecycle order (Scene::Update 0x175e84, Game::Update 0x16a8da, Scene::Unload 0x175cce).
    /// - GameObjects update in multimap&lt;id&gt; key order, equal ids in insertion order: dinosaurs (id 2) in creation order after the hero (id 1), items (id 0) first.
    /// - Objects created after the pending merge (scene OnUpdate / object updates) get their first Update next tick; pooled objects are already in the map.
    /// - lifeModeScene::OnUpdate (scene callback) reads the camera before the hero's UpdateCamera.
    /// - SwitchScene: Scene::Unload kills every scene and object timer before the scene callback's OnUnload.</summary>
    public static class PilotLifecycleVerify
    {
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-ENGINE-001/lifecycle-r1");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            int passed = 0, failed = 0; var log = new StringBuilder();
            Action<string, bool> check = (n, v) => { log.AppendLine((v ? "PASS " : "FAIL ") + n); if (v) passed++; else failed++; };
            string dir = Path.Combine(Path.GetTempPath(), "dc2_lifecycle_" + Guid.NewGuid().ToString("N"));
            Camera cam = null; Dc2Game game = null;
            try
            {
                cam = Bootstrap.MakeCamera();
                game = Dc2Game.Create(dir); game.manualStep = true; game.SkipMissionIntro = true; game.cam = cam;
                game.LaunchMission = new NativeMapState.Mission { type = 3, count = 99, background = 1 }; game.LaunchMission.enemies.Add(1);
                game.SwitchScene("gameScene"); game.Step(0f);
                var s = (GameScene)game.Current; s.spawningEnabled = false; s.ignorePlayer = true; s.DebugSpawnModeRoll = () => 99; s.heroX = 1000f;
                game.Step(1f / 60f);

                // ---- dinosaurs update in creation order (multimap id 2, insertion order) ----
                var a = s.CreateEnemy(1); var b = s.CreateEnemy(1); var c = s.CreateEnemy(1);
                var seen = new List<DinoActor>(); s.DebugDinoUpdated = d => seen.Add(d);
                game.Step(1f / 60f);
                s.DebugDinoUpdated = null;
                check("dinosaurs are updated oldest-first (a, b, c)", seen.Count == 3 && seen[0] == a && seen[1] == b && seen[2] == c);

                // ---- objects created after the pending merge wait a tick; outside the update stage they join this tick ----
                check("an effect created outside the object-update stage (touch / timer) is updated in its first tick", !s.DebugSpawnFx());
                bool deferred = false; bool once = true;
                s.DebugDinoUpdated = d => { if (once) { once = false; deferred = s.DebugSpawnFx(); } };
                game.Step(1f / 60f);
                s.DebugDinoUpdated = null;
                check("a new effect GameObject created during another object's update gets its first Update next tick", deferred);

                // ---- GameContainer children (r118z4): CreateGameObject 0x16b7a0 -> pending set +36, merged by the parent's BeforeUpdate 0x16bb90 ----
                check("a hero beam created outside the object-update stage is updated in its first tick", !s.DebugCreateLaserBeam());
                bool beamDeferred = false, beamLate = true;
                s.DebugBeforeBeams = () => { s.DebugBeforeBeams = null; beamDeferred = s.DebugCreateLaserBeam(); };
                game.Step(1f / 60f);
                check("a hero beam created inside the hero's update (before its container pass) waits for the next tick", beamDeferred);
                once = true;
                s.DebugDinoUpdated = d => { if (once) { once = false; beamLate = s.DebugCreateLaserBeam(); } };
                game.Step(1f / 60f);
                s.DebugDinoUpdated = null;
                check("a hero beam created after the hero's container pass is updated in the next tick without a further skip", !beamLate);

                // ---- Box2D sleep (b2Island::Solve 0x131b24): rest >= 0.5 s -> asleep; a non-waking velocity is ignored until Move wakes the body ----
                var z = s.CreateEnemy(1); z.state = 1; z.velocityX = 0f;
                int steps = 0; while (!z.bodyAsleep && steps < 200) { s.StepDinoPhysicsForTest(1f / 60f); steps++; }
                float acc = 0f; int expect = 0; while (acc < 0.5f) { acc = N.Add32(acc, 1f / 60f); expect++; }   // binary32 sum, as m_sleepTime
                check("a resting dinosaur body sleeps once m_sleepTime reaches 0.5 (" + steps + " steps at 60 Hz)", z.bodyAsleep && steps == expect);
                float zx = z.x; z.state = 0; z.velocityX = 300f;   // e.g. OnAnimationEndAdv / BefiredAnim reaction: SetLinearVelocity without WakeUp
                s.StepDinoPhysicsForTest(1f / 60f);
                check("a sleeping body is not integrated after a non-waking SetLinearVelocity", z.x == zx && z.bodyAsleep);
                s.MoveDinoForTest(z);
                check("dinosaurClass::Move wakes the body (0x118e6)", !z.bodyAsleep && z.bodySleepTime == 0f);
                s.StepDinoPhysicsForTest(1f / 60f);
                check("the woken body moves on the next physics step", z.x != zx);
                var w = s.CreateEnemy(1); w.state = 1; w.velocityX = 0.01f;   // |v|^2 = 1e-4 (binary32) is not > 0x38D1B717, so it still accumulates
                for (int i = 0; i < steps; i++) s.StepDinoPhysicsForTest(1f / 60f);
                check("|v| = 0.01 is inside the linear sleep tolerance (sleeps)", w.bodyAsleep && w.velocityX == 0f);

                // ---- r118z2: every awake body integrates, including state 8 (CreateEnemy leaves a spawn velocity before the first OnUpdate) ----
                var sp = s.CreateEnemy(1); sp.state = 8; sp.velocityX = 120f; float sx = sp.x;
                s.StepDinoPhysicsForTest(1f / 60f);
                check("a state-8 dinosaur with a spawn velocity moves in the physics step (no 0/1/2/7 gate)", sp.x == N.Add32(sx, N.Mul32(120f, 1f / 60f)));

                // ---- r118z2: id-5 objects update in one walk in insertion order; a reused pooled effect keeps the slot of its first creation ----
                var ea = s.DebugSpawnLoopFx(); var eb = s.DebugSpawnLoopFx();
                var eaRecord = ea.pooled; s.DebugRemoveFx(ea);
                var ec = s.DebugSpawnLoopFx();   // LIFO pool: takes ea's record
                var walk = new List<object>(); s.DebugId5Stepped = o => { if (o == eb || o == ec) walk.Add(o); };
                game.Step(1f / 60f); s.DebugId5Stepped = null;
                bool cFirst = eaRecord.slot < eb.pooled.slot;
                check("a reused pooled effect keeps its first slot, and the id-5 walk follows slots (" + eaRecord.slot + " vs " + eb.pooled.slot + ")",
                      ec.pooled == eaRecord && walk.Count == 2 && walk[0] == (cFirst ? (object)ec : eb) && walk[1] == (cFirst ? (object)eb : ec));
                s.DebugRemoveFx(eb); s.DebugRemoveFx(ec);

                // ---- scene callback reads the viewport before the hero's camera update ----
                s.Touch(0, GameScene.RightRect.center, 1);
                float before = 0f; bool moved = false;
                for (int i = 0; i < 90; i++)
                {
                    before = s.camLeft; game.Step(1f / 60f);
                    if (s.camLeft != before) { moved = true; break; }
                }
                s.Touch(2, GameScene.RightRect.center, 1);
                log.AppendLine("SAMPLE camLeft before " + before + " after " + s.camLeft + " weather view " + s.LastWeatherViewX);
                check("camera moved while walking (fixture)", moved);
                check("weather/overcast placement uses the viewport from before the hero's UpdateCamera (previous tick)", moved && s.LastWeatherViewX == before);

                // ---- SwitchScene kills the outgoing scene's timers before OnUnload; none fires in the next scene ----
                int fired = 0;
                s.SetTimer(s, "lifecycle_probe", 0.05f, () => fired++);
                game.Step(0f);
                game.SwitchScene("selectScene");
                for (int i = 0; i < 30; i++) game.Step(1f / 60f);
                check("a pending scene/object timer of the unloaded scene never fires (Scene::Unload kills it)", fired == 0);

                // ---- hero 1 special roll keeps the native stage order (r118z: it used to drop the frame, dinosaurs included, to the legacy port path) ----
                game.Profile.playerType = 1; game.SwitchScene("gameScene"); game.Step(0f);
                var r = (GameScene)game.Current; r.spawningEnabled = false; r.ignorePlayer = true; r.heroX = 1000f;
                game.Step(1f / 60f);
                r.OnRollDown();
                bool phaseOk = true, stepOk = false; int guard = 0;
                while (r.SpecialRollPhase != 1 && guard++ < 240) { game.Step(1f / 60f); phaseOk &= r.RecoveredWorldPhaseForTest; }
                if (r.SpecialRollPhase == 1)
                {
                    float x0 = r.heroX, v = r.GirlRollVelocity; game.Step(1f / 60f); phaseOk &= r.RecoveredWorldPhaseForTest;
                    stepOk = v != 0f && r.heroX == N.Add32(x0, N.Mul32(v, 1f / 60f));
                    log.AppendLine("SAMPLE special roll v " + v + " x " + x0 + " -> " + r.heroX);
                }
                check("hero 1 special roll runs the recovered world phase on every frame", phaseOk && r.SpecialRollPhase >= 1);
                check("its body moves exactly one rollVelocity*dt per physics step (no second integration in RollUpdate)", stepOk);
            }
            catch (Exception e) { failed++; log.AppendLine("EXCEPTION " + e); }
            finally
            {
                if (game != null) UnityEngine.Object.DestroyImmediate(game.gameObject);
                if (cam != null) UnityEngine.Object.DestroyImmediate(cam.gameObject);
                log.AppendLine("SUMMARY passes=" + passed + " fails=" + failed);
                File.WriteAllText(Path.Combine(output, "verify.txt"), log.ToString()); Debug.Log(log.ToString());
                PilotExit.Exit(failed == 0 ? 0 : 1);
            }
        }
    }
}
