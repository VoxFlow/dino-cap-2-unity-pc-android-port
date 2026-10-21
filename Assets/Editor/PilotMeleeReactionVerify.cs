using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>dinosaurClass::BeJinAttacked 0x1442a non-lethal tail (ARM read, Dev/Evidence/melee_reaction_r111.md).</summary>
    public static class PilotMeleeReactionVerify
    {
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-PILOT-001/melee-r1");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            string dir = Path.Combine(Path.GetTempPath(), "dc2_melee_verify_" + Guid.NewGuid().ToString("N"));
            int passed = 0, failed = 0; var log = new StringBuilder();
            Action<string, bool> check = (n, v) => { log.AppendLine((v ? "PASS " : "FAIL ") + n); if (v) passed++; else failed++; };
            Dc2Game game = null; Camera cam = null;
            try
            {
                cam = Bootstrap.MakeCamera(); game = Dc2Game.Create(dir); game.manualStep = true; game.SkipMissionIntro = true; game.cam = cam;
                game.LaunchMission = new NativeMapState.Mission { type = 3, count = 20, level = 1, background = 0 };
                game.Profile.playerType = 0;
                game.SwitchScene("gameScene"); game.StepFor(0.02f);
                var scene = (GameScene)game.Current; scene.spawningEnabled = false; scene.DebugSpawnModeRoll = () => 0;
                game.StepFor(0.2f);
                WeaponDef melee = scene.MeleeWeaponForTest();
                check("last weapon record is the melee type 6", melee.type == 6);
                DinoActor plain = null, skin3 = null;
                for (int k = 1; k <= 20 && (plain == null || skin3 == null); k++)
                {
                    DinoActor d; try { d = scene.CreateEnemy(k); } catch { continue; }
                    if (d == null) continue;
                    log.AppendLine("INFO kind " + k + " skin " + d.skin);
                    if (d.skin == 3) skin3 = d; else if (plain == null && (d.skin == 1 || d.skin == 5)) plain = d;
                }
                if (plain != null)
                {
                    plain.faceRight = true; plain.state = 0; plain.hp = 9999f;
                    bool ok = scene.ApplyMeleeReactionForTest(plain, melee);
                    check("skin " + plain.skin + " facing right: knocked back -200*S, state 2, no advanced layer", ok && plain.state == 2 && Mathf.Abs(plain.velocityX + 200f * N.S) < 0.01f && !plain.advActive);
                    plain.faceRight = false; plain.state = 0;
                    scene.ApplyMeleeReactionForTest(plain, melee);
                    check("facing left: +200*S", Mathf.Abs(plain.velocityX - 200f * N.S) < 0.01f);
                }
                else check("a non-skin-3 ground dinosaur exists for the test", false);
                if (skin3 != null)
                {
                    skin3.faceRight = true; skin3.state = 0; skin3.hp = 9999f;
                    scene.ApplyMeleeReactionForTest(skin3, melee);
                    check("skin 3 state 0: advanced Befired, velocity +0.5*S*SPD, state unchanged", skin3.advActive && skin3.state == 0 && Mathf.Abs(skin3.velocityX - 0.5f * N.S * skin3.spd) < 0.01f);
                    skin3.advActive = false; skin3.state = 1; float v = skin3.velocityX;
                    scene.ApplyMeleeReactionForTest(skin3, melee);
                    check("skin 3 state != 0: no animation/velocity change", !skin3.advActive && skin3.velocityX == v && skin3.state == 1);
                }
                else log.AppendLine("INFO no skin-3 kind among 1..8 in this mission set");
                // Die(weapon) dispatch 0x140fc: type 5 subtypes 1,2,4 burn (DieShaoSi), others do not
                bool burn1 = scene.DieShaoSiForTest(plain, new WeaponDef { type = 5, index = 1 });
                float sc1 = plain.rig.objectScale; string a1 = plain.rig.animation != null ? "set" : "none";
                check("type 5 subtype 1 kills via DieShaoSi: objectScale 0.8, state 3", burn1 && plain.state == 3 && Mathf.Abs(sc1 - 0.8f) < 1e-6f && a1 == "set");
                float ashY = N.Add32(N.Add32(N.Mul32(N.Mul32(N.S, 160f), 0.8f), GameScene.GroundY), -70f);   // r119w: DieShaoSi 0x10a8a 160, 0x10b22 iPad -70
                check("r119w burn death y (skin " + plain.skin + ") = S*160*0.8 + ground - 70 = " + ashY + " (actual " + plain.y + ")", plain.skin == 3 || Mathf.Abs(plain.y - ashY) < 0.01f);
                game.StepFor(1.5f);   // the ShaoSiAnim plays through rendering without exceptions; state 3 removes the actor at its end
                check("burn death animation completes and removes the actor", plain.removed);
                var plain2 = scene.CreateEnemy(1);
                bool burn0 = scene.DieShaoSiForTest(plain2, new WeaponDef { type = 0, index = 0 });
                check("type 0 weapon is not a burn death", !burn0 && plain2.state == 3 && Mathf.Abs(plain2.rig.objectScale - 0.8f) > 1e-6f);
                var plain3 = scene.CreateEnemy(1);
                check("type 5 subtype 3 (head-fly) and 5 (normal) are not burn deaths", !scene.DieShaoSiForTest(plain3, new WeaponDef { type = 5, index = 3 }) && !scene.DieShaoSiForTest(scene.CreateEnemy(1), new WeaponDef { type = 5, index = 5 }));
                var ranged = scene.CurrentWeapon();
                check("non-melee weapon type is not handled by the melee tail", !scene.ApplyMeleeReactionForTest(plain != null ? plain : skin3, ranged));
            }
            catch (Exception e) { failed++; log.AppendLine("EXCEPTION " + e); }
            finally
            {
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
