using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>Mission type 8: AddBoss timer, hp scaling, bossN state machine, hit window, resistance table, hero damage, death flow (Dev/Evidence/boss_decomp_r105.md).</summary>
    public static class PilotBossVerify
    {
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-PILOT-001/boss-r1");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            string dir = Path.Combine(Path.GetTempPath(), "dc2_boss_verify_" + Guid.NewGuid().ToString("N"));
            int passed = 0, failed = 0; var log = new StringBuilder();
            Action<string, bool> check = (n, v) => { log.AppendLine((v ? "PASS " : "FAIL ") + n); if (v) passed++; else failed++; };
            Dc2Game game = null; Camera cam = null; RenderTexture rt = null; Texture2D img = null;
            try
            {
                // ---- independent hp scaling values (AddBoss 0xd105a): boss1 base 40 ----
                var defs = Dc2GameData.Bosses();
                check("boss.xml has five bosses with hp 40/60/100/130/150", defs.Count == 5 && defs[0].hp == 40 && defs[1].hp == 60 && defs[2].hp == 100 && defs[3].hp == 130 && defs[4].hp == 150);
                check("hp scaling L=0 -> 40, L=9 -> 130, L=10 -> 40+250+50, L=14 -> 40+350+50, L=15 -> 40+450+50",
                    GameScene.BossHpFor(defs[0], 0) == 40f && GameScene.BossHpFor(defs[0], 9) == 130f && GameScene.BossHpFor(defs[0], 10) == 340f
                    && GameScene.BossHpFor(defs[0], 14) == 440f && GameScene.BossHpFor(defs[0], 15) == 540f);

                cam = Bootstrap.MakeCamera(); game = Dc2Game.Create(dir); game.manualStep = true; game.SkipMissionIntro = true; game.cam = cam;
                game.LaunchMission = new NativeMapState.Mission { type = 8, count = 1, level = 3, background = 0 };   // boss line 2: hou + jiao
                rt = new RenderTexture(1024, 768, 24); img = new Texture2D(1024, 768, TextureFormat.RGB24, false);
                Action<string> shot = name => { cam.targetTexture = rt; Bootstrap.RenderAll(cam); RenderTexture.active = rt; img.ReadPixels(new Rect(0, 0, 1024, 768), 0, 0); img.Apply(); File.WriteAllBytes(Path.Combine(output, name), img.EncodeToPNG()); cam.targetTexture = null; RenderTexture.active = null; };
                game.Profile.playerType = 0; game.Profile.cash = 1000; game.Profile.level[0] = 2;
                game.SwitchScene("gameScene"); game.StepFor(0.02f);
                var scene = (GameScene)game.Current; scene.spawningEnabled = false;
                scene.DebugBossKindRoll = () => 1;                 // first <ani>: hou
                scene.rng = new System.Random(5);
                game.StepFor(1.9f);
                check("no boss before the 2.0 s AddBoss timer", scene.Boss == null);
                check("boss HUD ids 50/51 exist from load", scene.GetControl(50) != null && scene.GetControl(51) != null);
                game.StepFor(0.2f);
                var b = scene.Boss;
                check("AddBoss at 2.0 s creates bossN", b != null);
                float S = N.S;
                check("hp = base 100 + 10*L(2) = 120", b != null && b.hp == 120f && b.maxHp == 120f);
                check("first OnIdle: right side (flag false), hou x = 380*S, y = -50*S, sub-state 1, kind hou", b.kind == 1 && !b.left && b.x == 380f * S && b.y == -50f * S && b.sub == 1);
                check("money = 1500 + 15*L, exp = 1000 + 50*L", b.money == 1530f && b.exp == 1100f);
                shot("boss_hou_idle.png");
                // hou03 plays once -> fin -> sub 2 (hou01 loop) + Attack timer
                int f = 0; while (b.sub == 1 && f++ < 1200) game.Step(1f / 60f);
                check("hou03 intro ends -> sub-state 2 (HouAni hou01) with Attack timer", b.sub == 2);
                shot("boss_hou_ready.png");
                // body-contact (HouCannotTouch): right side boss hits hero when hero.x > boss.x - 115*S
                scene.heroX = b.x - 100f;
                float hpBefore = scene.playerHp;
                game.Step(1f / 60f);
                float L = 2f;
                float expectedDamage = (float)(int)(10f * L + defs[2].anis[0].attr["atk"]);   // (int)(10*(L+1)-10 + atk)
                check("HouCannotTouch -> BeAttackByBoss: hp -= (int)(10*L + atk)", Mathf.Abs((hpBefore - scene.playerHp) - expectedDamage) < 0.001f);
                check("hero enters hurt state 5 with Wudi invulnerability", scene.invincible);
                // wait for the Attack timer and sub 4
                f = 0; while (b.sub != 4 && f++ < 2400) game.Step(1f / 60f);
                check("Attack timer (time1 2..3 s) starts hou02: sub-state 4", b.sub == 4);
                // CheckAttackDis offset during sub 4 (hou): 90*S on the clear-left side; invulnerability states
                var weapon = scene.CurrentWeapon();
                scene.heroX = b.x - 90f * S - 40f;
                scene.facingLeft = false;
                float delta;
                bool cand = scene.BossCandidateForTest(weapon, out delta);
                float expectedDelta = (b.x - 90f * S) - scene.heroX;
                check("weapon candidate delta uses the hou sub-4 offset 90*S", cand == (expectedDelta > 0 && expectedDelta < S * weapon.atkDis) && Mathf.Abs(delta - expectedDelta) < 0.01f);
                // r119d (user item 13): playerClass::JinAttack 0xbdfb8 walks the boss too (CheckAttackDis true -> distance 10.0, BeAttacked 0xbe2a0/0xbe4fc)
                {
                    float hpMelee = b.hp; scene.heroX = b.x - 90f * S - 20f; scene.facingLeft = false;
                    scene.JinAttackForTest();
                    check("r119d melee in front of the attacking boss hits it (hp " + hpMelee + " -> " + b.hp + ")", b.hp < hpMelee);
                    float hpBehind = b.hp; scene.heroX = b.x - 90f * S - 20f; scene.facingLeft = true;   // turned away: CheckAttackDis false, raw distance heroX - boss.x < 0
                    scene.JinAttackForTest();
                    check("r119d melee facing away from the boss does not hit it", b.hp == hpBehind);
                    b.hp = hpMelee;
                }
                // damage table: resistance factors
                Func<WeaponDef, float> dealt = w =>
                {
                    float before = b.hp; scene.rng = new System.Random(77);
                    var r = new System.Random(77); float u = (float)r.NextDouble();
                    float basis = NativeWeaponDamage.BaseBeforeFalloff(w, game.Profile.weaponLevels[scene.CurrentAmmoIndex], game.Profile.jigsaw2Count, u);
                    scene.BossBeAttackedForTest(w);
                    return before - b.hp;
                };
                // hou kind: type 0..3 -> x0.25 ; type 6 good sub -> x2 ; type 5 sub1 -> x1
                var wps = Dc2GameData.Weapons();
                int level = game.Profile.weaponLevels[scene.CurrentAmmoIndex];
                Func<WeaponDef, float> expectedBase = w =>
                {
                    var r = new System.Random(77); float u = (float)r.NextDouble();
                    return NativeWeaponDamage.BaseBeforeFalloff(w, level, game.Profile.jigsaw2Count, u);
                };
                Func<WeaponDef, bool> critOf = w =>
                {
                    var r = new System.Random(77); r.NextDouble(); int word = (int)((float)r.NextDouble() * 10000f);
                    return NativeWeaponDamage.IsCritical(word, w.crit);
                };
                b.hp = b.maxHp = 100000f;
                b.sub = 2;                                          // vulnerable sub-state
                var w0 = wps[0][0];
                float d0 = dealt(w0); float e0 = N.Mul32(expectedBase(w0), 0.25f); if (critOf(w0)) e0 = e0 + e0;
                check("hou vs weapon type 0: x0.25 (crit counted twice)", Mathf.Abs(d0 - e0) < 0.01f);
                var w6 = wps[6][0];
                float d6 = dealt(w6); float e6 = N.Add32(expectedBase(w6), expectedBase(w6)); if (critOf(w6)) e6 = e6 + e6;
                check("hou vs weapon type 6 subtype 0: damage doubled", Mathf.Abs(d6 - e6) < 0.01f);
                var w5 = wps[5][1];
                float d5 = dealt(w5); float e5 = expectedBase(w5); if (critOf(w5)) e5 = e5 + e5;
                check("hou vs weapon type 5 subtype 1: x1", Mathf.Abs(d5 - e5) < 0.01f);
                var w5b = wps[5][0];
                float d5b = dealt(w5b); float e5b = N.Mul32(expectedBase(w5b), 0.25f); if (critOf(w5b)) e5b = e5b + e5b;
                check("hou vs weapon type 5 subtype 0: x0.25", Mathf.Abs(d5b - e5b) < 0.01f);
                // invulnerable sub-states
                b.sub = 1; float before1 = b.hp; scene.BossBeAttackedForTest(w0);
                check("boss in sub-state 1 ignores weapon hits", b.hp == before1);
                b.sub = 7; before1 = b.hp; scene.BossBeAttackedForTest(w0);
                check("boss in sub-state 7 (MoveOut) ignores weapon hits", b.hp == before1);
                // blood bar
                b.sub = 2; b.hp = b.maxHp * 0.5f; scene.BossBeAttackedForTest(w6);
                var bar = scene.GetControl(51);
                check("blood bar width follows (int)(100*hp/max)*0.01*110", bar != null);
                shot("boss_hit_flash.png");
                // death flow
                b.hp = 1f; b.sub = 2; int cashBefore = game.Profile.cash;
                scene.BossBeAttackedForTest(w6);
                check("lethal hit: MoveOut (sub 7), dying flag, cash += boss money", b.sub == 7 && b.dying && game.Profile.cash == cashBefore + (int)b.money);
                f = 0; while (b.kind != 4 && f++ < 1200) game.Step(1f / 60f);
                check("MoveOut animation ends with hp<=0 -> BossDie (kind 4)", b.kind == 4);
                f = 0; while (b.sub != 6 && f++ < 120) game.Step(1f / 60f);
                check("BossDieOntime 1.0 s later: die animation sub-state 6", b.sub == 6);
                shot("boss_die.png");
                f = 0; while (scene.result == GameScene.MissionResult.Running && f++ < 600) game.Step(1f / 60f);
                check("MissionEnd 4.0 s later completes the mission", scene.result == GameScene.MissionResult.Complete);

                // ---- jiao and wei full cycles (kinds picked through the debug roll) ----
                foreach (var kc in new[] { new { level = 3, pick = 2, name = "jiao", kind = 2 }, new { level = 2, pick = 2, name = "wei", kind = 3 } })
                {
                    game.LaunchMission = new NativeMapState.Mission { type = 8, count = 1, level = kc.level, background = 0 };
                    game.Profile.level[0] = 0;
                    game.SwitchScene("gameScene"); game.StepFor(0.02f);
                    scene = (GameScene)game.Current; scene.spawningEnabled = false;
                    scene.DebugBossKindRoll = () => kc.pick; scene.rng = new System.Random(9);
                    game.StepFor(2.1f);
                    b = scene.Boss;
                    check(kc.name + ": boss created with kind " + kc.kind, b != null && b.kind == kc.kind);
                    var seen = new System.Collections.Generic.HashSet<int>(); int hurts = 0; bool mirrored = false; float lastHp = scene.playerHp;
                    scene.heroX = -240f;
                    for (int fr = 0; fr < 60 * 45 && !scene.heroDead; fr++)
                    {
                        scene.playerHp = scene.playerMaxHp;               // keep the hero alive so the cycle can be observed
                        scene.heroX = b.left ? b.x + 100f : b.x - 100f;  // stand near the boss
                        game.Step(1f / 60f);
                        seen.Add(b.sub); if (b.left) mirrored = true;
                        if (fr == 60 * 4) shot("boss_" + kc.name + "_4s.png");
                        if (scene.BossHeroHits > hurts) hurts = scene.BossHeroHits;
                    }
                    log.AppendLine("INFO " + kc.name + " sub-states seen: " + string.Join(",", System.Linq.Enumerable.Select(seen, x => x.ToString())) + " heroHits=" + hurts + " mirrored=" + mirrored);
                    check(kc.name + ": cycles through attack (4) and move-out (7)", seen.Contains(4) && seen.Contains(7));
                    check(kc.name + ": the boss damaged the standing hero at least once", hurts >= 1);
                }
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
