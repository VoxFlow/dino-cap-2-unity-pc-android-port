using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>NATIVE blood / gore particles (NativeBlood.cs, GameBlood.cs) against the ARM-executed oracle Assets/Editor/blood_oracle.json
    /// (Dev/Tools/prepare_blood_oracle_r116.py: dinosaurClass::DieNormal / BlastBlood / BlastSuiRou / DieTouFei and particleClass::OnUpdate run on the shipped bytes with fixed rand sequences),
    /// plus the integration (semi-implicit Euler, persistent force, 2000 px/s clamp) and an end-to-end kill in a mission scene.</summary>
    public static class PilotBloodVerify
    {
        [Serializable] class Rec
        {
            public string frame; public float[] size, vel, force, pos; public float w, rot; public int layer, vis, flip, active, kind, style, sub, surface; public bool enabled;
        }
        [Serializable] class ShellExp { public bool disappear; public float[] pos, vel; public float w; }
        [Serializable] class Case
        {
            public string kind, fn, frameIn; public int skin, surface, flip, k, style, sub, flag, order; public float x, y, @base, w; public float[] vel, posIn, sizeIn; public int[] rands, rr; public Rec[] particles, objects; public Rec rec; public ShellExp exp;
        }
        [Serializable] class Oracle { public Case[] cases; }

        static bool Near(float a, float b) { return Mathf.Abs(a - b) <= 1e-3f + 1e-5f * Mathf.Abs(b); }
        static string F2(float[] a) { return a == null || a.Length != 2 ? "none" : a[0] + "," + a[1]; }
        static bool Near2(float[] e, Vector2 a) { if (e == null || e.Length != 2) return a == Vector2.zero; return Near(e[0], a.x) && Near(e[1], a.y); }

        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-PILOT-001/blood-r1");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            string dir = Path.Combine(Path.GetTempPath(), "dc2_blood_verify_" + Guid.NewGuid().ToString("N"));
            int passed = 0, failed = 0; var log = new StringBuilder();
            Action<string, bool> check = (n, v) => { log.AppendLine((v ? "PASS " : "FAIL ") + n); if (v) passed++; else failed++; };
            Dc2Game game = null; Camera cam = null; RenderTexture rt = null; Texture2D img = null;
            try
            {
                var oracle = JsonUtility.FromJson<Oracle>(File.ReadAllText(Path.Combine(Application.dataPath, "Editor/blood_oracle.json")));
                log.AppendLine("INFO oracle cases " + oracle.cases.Length);
                var mismatches = new List<string>(); var groups = new Dictionary<string, int>();
                int nSpawn = 0, nUpdate = 0, nDanke = 0, nShell = 0;
                foreach (var c in oracle.cases)
                {
                    int ri = 0, rri = 0; var rl = c.rands ?? new int[] { 0 }; var rrl = c.rr ?? new int[] { 0 };
                    Func<float> unit = () => { int r = rl[ri % rl.Length]; ri++; return N.Mul32((float)r, N.F(805306368)); };      // the oracle cycles its rand() list
                    Func<int, int, int> range = (lo, hi) => { int v = rrl[rri % rrl.Length]; rri++; return hi > lo ? lo + v % Math.Max(1, hi - lo) : lo; };
                    var world = new BloodWorld(-110f, unit, range);
                    string tag = c.kind == "spawn" ? c.fn + " skin" + c.skin + " sf" + c.surface + " flip" + c.flip : "update k" + c.k + " st" + c.style + " sub" + c.sub + " sf" + c.surface + " y" + c.y;
                    Action<int, Rec, BloodParticle, bool> cmp = (idx, e, p, isKid) =>
                    {
                        string t = tag + " #" + idx;
                        string ef = string.IsNullOrEmpty(e.frame) ? null : e.frame;
                        if (ef != p.frame) mismatches.Add(t + " frame " + e.frame + " vs " + p.frame);
                        if (!Near2(e.size, p.size)) mismatches.Add(t + " size");
                        if (!(c.kind == "update" && (e.vel == null || e.vel.Length != 2)) && !Near2(e.vel, p.vel)) mismatches.Add(t + " vel " + F2(e.vel) + " vs " + p.vel);
                        if (!Near(e.w, p.angVel)) mismatches.Add(t + " w " + e.w + " vs " + p.angVel);
                        if (!Near2(e.force, p.force)) mismatches.Add(t + " force");
                        bool selfObj = c.kind == "update" && idx == 0;     // the self record's callback block is not the particle struct under test
                        if (!selfObj && e.kind != p.kind) mismatches.Add(t + " kind " + e.kind + " vs " + p.kind);
                        if (!selfObj && p.kind == 2 && e.sub != p.sub) mismatches.Add(t + " sub");
                        if (!selfObj && p.kind == 3 && (e.style != p.style || e.surface != p.surface)) mismatches.Add(t + " style/surface");
                        if (!selfObj && (e.active != 0) != p.active) mismatches.Add(t + " active");
                        if (!selfObj) { if (c.kind == "spawn" && e.layer != p.order) mismatches.Add(t + " layer " + e.layer + " vs " + p.order); if ((e.vis != 0) != p.visible) mismatches.Add(t + " vis"); }   // update-pass children are re-ordered to 7 by their own OnUpdate
                        if (c.kind == "spawn" && ((e.flip != 0) != p.flipX)) mismatches.Add(t + " flip");
                        if (c.kind == "update" && !Near(e.rot, p.rot)) mismatches.Add(t + " rot " + e.rot + " vs " + p.rot);
                        if (e.pos != null && e.pos.Length == 2 && !Near2(e.pos, p.pos)) mismatches.Add(t + " pos " + F2(e.pos) + " vs " + p.pos);
                    };
                    if (c.kind == "spawn")
                    {
                        nSpawn++;
                        switch (c.fn)
                        {
                            case "BlastBlood": world.BlastBlood(Vector2.zero); break;
                            case "BlastSuiRou": world.BlastSuiRou(Vector2.zero); break;
                            case "DieNormal": world.DieNormal(Vector2.zero, c.skin, c.surface); break;
                            case "DieTouFei": world.DieTouFei(Vector2.zero, c.skin, c.surface, c.flip != 0); break;
                            case "HeroDie": world.HeroDie(Vector2.zero); break;
                            case "GirlDie": world.GirlDie(Vector2.zero); break;
                        }
                        if (c.particles.Length != world.Spawned) mismatches.Add(tag + " count " + c.particles.Length + " vs " + world.Spawned);
                        for (int i = 0; i < c.particles.Length && i < world.pool.Length; i++) cmp(i, c.particles[i], world.pool[i], true);
                    }
                    else if (c.kind == "danke")
                    {
                        nDanke++;
                        var sh = world.Danke(new Vector2(c.posIn[0], c.posIn[1]), new Vector2(c.sizeIn[0], c.sizeIn[1]), c.frameIn, c.order, c.flag != 0);
                        var e = c.rec; string t = "danke f" + c.flag + " o" + c.order + " " + c.frameIn;
                        if (e.frame != sh.frame) mismatches.Add(t + " frame");
                        if (!Near2(e.size, sh.size)) mismatches.Add(t + " size");
                        if (!Near2(e.vel, sh.vel)) mismatches.Add(t + " vel " + F2(e.vel) + " vs " + sh.vel);
                        if (!Near(e.w, sh.angVel)) mismatches.Add(t + " w " + e.w + " vs " + sh.angVel);
                        if (!Near2(e.force, sh.force)) mismatches.Add(t + " force");
                        if (!Near2(e.pos, sh.pos)) mismatches.Add(t + " pos");
                        if (e.layer != sh.order) mismatches.Add(t + " order " + e.layer + " vs " + sh.order);
                        if (e.kind != sh.kind) mismatches.Add(t + " kind");
                    }
                    else if (c.kind == "shell")
                    {
                        nShell++;
                        var sh = world.Danke(new Vector2(c.x, c.y), new Vector2(10f, 10f), "danke1Frame", 8, false);
                        sh.pos = new Vector2(c.x, c.y); sh.bodyPos = sh.pos; sh.vel = new Vector2(c.vel[0], c.vel[1]); sh.angVel = c.w; sh.baseY = c.@base;
                        world.ShellObjectsUpdate();   // r118z2: bulletClass (id 3) objects have their own pass
                        var e = c.exp; string t = "shell y" + c.y + " vy" + c.vel[1];
                        if (e.disappear != !sh.enabled) mismatches.Add(t + " disappear");
                        if (e.pos != null && e.pos.Length == 2) { if (!Near2(e.pos, sh.pos)) mismatches.Add(t + " pos " + F2(e.pos) + " vs " + sh.pos); if (!Near2(e.vel, sh.vel)) mismatches.Add(t + " vel"); if (!Near(e.w, sh.angVel)) mismatches.Add(t + " w"); }
                        else if (!e.disappear && (!Near2(new[] { c.x, c.y }, sh.pos) || !Near2(c.vel, sh.vel))) mismatches.Add(t + " changed without landing");
                    }
                    else
                    {
                        nUpdate++;
                        var self = world.pool[0];
                        self.enabled = true; self.visible = true; self.active = true; self.kind = c.k; self.style = c.style; self.sub = c.sub; self.surface = c.surface;
                        self.pos = new Vector2(c.x, c.y); self.bodyPos = self.pos; self.vel = new Vector2(c.vel[0], c.vel[1]); self.baseY = c.@base; world.NextIndex = 1;
                        world.ObjectUpdate();
                        if (c.objects.Length - 1 != world.Spawned) mismatches.Add(tag + " children " + (c.objects.Length - 1) + " vs " + world.Spawned);
                        var es = c.objects[0];
                        // self: the oracle records the object state left by the original
                        if (c.k == 3) { if (world.pool[0].enabled || world.pool[0].active) mismatches.Add(tag + " parent not disabled"); }
                        else
                        {
                            cmp(0, es, self, false);
                            bool flying = c.y >= c.@base;
                            if (flying != self.active) mismatches.Add(tag + " active after update");   // landed particles stop (active = 0), flying ones keep updating
                        }
                        for (int i = 1; i < c.objects.Length && i < world.pool.Length; i++) cmp(i, c.objects[i], world.pool[i], true);
                    }
                }
                log.AppendLine("INFO spawn cases " + nSpawn + " update cases " + nUpdate + " danke cases " + nDanke + " shell cases " + nShell + " mismatches " + mismatches.Count);
                foreach (var m in mismatches)
                {
                    var parts = m.Split(' ');
                    string key = (m.StartsWith("update") ? "update " + parts[1] : parts[0]) + " " + (parts.Length > 4 ? parts[parts.Length > 6 && parts[3].StartsWith("#") ? 4 : 4] : "?");
                    int cnt; groups.TryGetValue(key, out cnt); groups[key] = cnt + 1;
                }
                foreach (var g in groups.OrderByDescending(x => x.Value).Take(40)) log.AppendLine("  GROUP " + g.Key + " x" + g.Value);
                foreach (var m in mismatches.Take(25)) log.AppendLine("  MISMATCH " + m);
                check("all " + nSpawn + " spawner cases (DieNormal / BlastBlood / BlastSuiRou / DieTouFei, 6 skins x 17 surfaces, 2 rand sets) match the ARM oracle", mismatches.All(m => m.StartsWith("update") || m.StartsWith("danke") || m.StartsWith("shell")));
                check("all " + nUpdate + " particleClass::OnUpdate cases (landing of kinds 1-4, kind 3 burst, flight rotation) match the ARM oracle", mismatches.All(m => !m.StartsWith("update")));
                check("all " + nDanke + " playerClass::Danke shell-casing launches match the ARM oracle (velocity, spin, force, size, order, flip)", mismatches.All(m => !m.StartsWith("danke")));
                check("all " + nShell + " bulletClass::OnUpdate shell bounce / Disappear cases match the ARM oracle", mismatches.All(m => !m.StartsWith("shell")));

                // ---- integration: semi-implicit Euler with a persistent force (b2Island::Solve), no gravity in the world ----
                {
                    var w = new BloodWorld(-110f, () => 0.5f, (a, b) => a);
                    var p = w.BlastBlood(new Vector2(0f, 0f));
                    p.vel = new Vector2(400f, 1500f); p.force = new Vector2(0f, -4000f);
                    float dt = 1f / 60f; float vy = 1500f, y = 0f, x = 0f;
                    for (int i = 0; i < 20; i++) { w.PhysicsStep(dt); vy += dt * -4000f; y += dt * vy; x += dt * 400f; }
                    check("20 steps at 1/60 s: velocity and position follow v += dt*F; x += dt*v (persistent force, mass 1)", Near(p.vel.y, vy) && Near(p.pos.y, y) && Near(p.pos.x, x));
                    for (int i = 0; i < 90; i++) w.PhysicsStep(dt);
                    float sp = p.vel.magnitude;
                    check("speed is clamped to b2_maxLinearVelocity 2000 after the fall", Mathf.Abs(sp - 2000f) < 1f);
                }

                // ---- end-to-end: shoot a dinosaur in a mission scene, blood lands as ground splats ----
                cam = Bootstrap.MakeCamera(); game = Dc2Game.Create(dir); game.manualStep = true; game.SkipMissionIntro = true; game.cam = cam;
                rt = new RenderTexture(1024, 768, 24); img = new Texture2D(1024, 768, TextureFormat.RGB24, false);
                Action<string> shot = name => { cam.targetTexture = rt; Bootstrap.RenderAll(cam); RenderTexture.active = rt; img.ReadPixels(new Rect(0, 0, 1024, 768), 0, 0); img.Apply(); File.WriteAllBytes(Path.Combine(output, name), img.EncodeToPNG()); cam.targetTexture = null; RenderTexture.active = null; };
                game.LaunchMission = new NativeMapState.Mission { type = 3, count = 200, level = 1, background = 0 };
                game.Profile.playerType = 0; game.Profile.cash = 5000;
                game.SwitchScene("gameScene"); game.StepFor(0.05f);
                var scene = (GameScene)game.Current; scene.spawningEnabled = false; scene.ignorePlayer = true; scene.DebugSpawnModeRoll = () => 0;
                for (int s = 0; s < 5; s++) { game.Profile.weaponOwned[s] = 0; game.Profile.weaponAmmo[s] = 100; }
                game.StepFor(0.1f);
                check("a blood world exists with a 40-object pool", scene.BloodForTest != null && scene.BloodForTest.pool.Length == 40);
                var dino = scene.CreateEnemy(1); dino.x = scene.heroX + 160f; dino.hp = 99999f; dino.state = 0; dino.velocityX = 0f;
                game.StepFor(0.3f);
                int before = scene.BloodForTest.Spawned, fx0 = scene.FxSpawned;
                scene.weaponSlot = 0; scene.facingLeft = false;
                dino.x = scene.heroX + 160f;   // r119c: head-based melee reach (JinAttack 0xbe170) dino.state = 0; dino.velocityX = 0f; dino.targetable = true;
                float hp0 = dino.hp;
                scene.HitDownForTest(); game.StepFor(1.2f);
                log.AppendLine("INFO melee: dino hp " + hp0 + " -> " + dino.hp + " state " + dino.state);
                int afterHit = scene.BloodForTest.Spawned;
                log.AppendLine("INFO particles spawned by the melee hit: " + (afterHit - before));
                check("a melee hit spawns BlastBlood x3 (+ x3 when critical) and the dx blood effect", (afterHit - before) >= 3 && (afterHit - before) % 3 == 0 && scene.FxSpawned > fx0 && scene.LastFxTag == "blood");
                shot("blood_hit_flight.png");
                game.StepFor(1.5f);
                var drops = scene.BloodForTest.pool.Where(p => p.enabled && p.visible && p.frame == "xue2Frame").ToList();
                check("drops land as flat splats (xue2Frame, rotation 0, resting just above g_ground_y)", drops.Count >= 1 && drops.All(p => !p.active && Near(p.rot, 0f) && p.pos.y >= GameScene.GroundY && p.pos.y < GameScene.GroundY + 4.01f));
                shot("blood_hit_landed.png");
                // pistol shot: shell casing (Danke) ejected, bounces on the ground and disappears
                scene.ignorePlayer = true; dino.x = scene.heroX + 4000f;
                int sh0 = scene.BloodForTest.ShellsSpawned;
                scene.FireDown(); game.StepFor(0.5f); scene.FireUp();
                log.AppendLine("INFO shells after the pistol burst: " + (scene.BloodForTest.ShellsSpawned - sh0));
                check("firing the pistol ejects shell casings (bulletClass pool, danke1Frame)", scene.BloodForTest.ShellsSpawned - sh0 >= 1 && scene.BloodForTest.shells.Any(b => b.frame == "danke1Frame"));
                shot("blood_shell_flight.png");
                game.StepFor(3.0f);
                check("shell casings bounce and then disappear (back in the free list)", scene.BloodForTest.shells.Count > 0 && scene.BloodForTest.shells.All(b => !b.enabled));
                // kill with an armed hit: DieNormal bursts
                int b2 = scene.BloodForTest.Spawned;
                scene.ApplyDamageForTest(dino); game.StepFor(0.05f);
                log.AppendLine("INFO particles spawned by the kill: " + (scene.BloodForTest.Spawned - b2));
                check("DieNormal spawns 4 gore pieces and 3 blood drops (7 particles)", scene.BloodForTest.Spawned - b2 == 7);
                shot("blood_die_flight.png");
                game.StepFor(0.45f); shot("blood_die_mid.png");
                game.StepFor(2.0f);
                var pieces = scene.BloodForTest.pool.Where(p => p.enabled && p.frame != null && (p.frame.StartsWith("xue5") || p.frame.StartsWith("xue4") || p.frame.StartsWith("xue6") || p.frame.StartsWith("xue7"))).ToList();
                check("gore pieces land as xue4..7 splats", pieces.Count >= 1);
                shot("blood_die_landed.png");
            }
            catch (Exception e) { failed++; log.AppendLine("EXCEPTION " + e); }
            finally
            {
                if (rt != null) UnityEngine.Object.DestroyImmediate(rt);
                if (img != null) UnityEngine.Object.DestroyImmediate(img);
                if (game != null) UnityEngine.Object.DestroyImmediate(game.gameObject);
                if (cam != null) UnityEngine.Object.DestroyImmediate(cam.gameObject);
                if (Directory.Exists(dir)) Directory.Delete(dir, true);
                log.AppendLine("SUMMARY passes=" + passed + " fails=" + failed);
                File.WriteAllText(Path.Combine(output, "verify.txt"), log.ToString()); Debug.Log(log.ToString());
                PilotExit.Exit(failed == 0 ? 0 : 1);
            }
        }
    }
}
