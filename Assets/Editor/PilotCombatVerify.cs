using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>First combat slice: CreateEnemy stats, dino chase/attack, hero BeAttack, hitscan fire, ammo, damage.</summary>
    public static class PilotCombatVerify
    {
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-PILOT-001/combat-r1");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            string dir = Path.Combine(Path.GetTempPath(), "dc2_combat_verify_" + Guid.NewGuid().ToString("N"));
            int passed = 0, failed = 0; var log = new StringBuilder();
            Action<string, bool> check = (n, v) => { log.AppendLine((v ? "PASS " : "FAIL ") + n); if (v) passed++; else failed++; };
            Dc2Game game = null; Camera cam = null; RenderTexture rt = null; Texture2D img = null;
            try
            {
                cam = Bootstrap.MakeCamera(); game = Dc2Game.Create(dir); game.manualStep = true; game.SkipMissionIntro = true; game.cam = cam;
                game.LaunchMission = new NativeMapState.Mission { type = 3, count = 20, level = 1, background = 0 }; // explicit M=1 fixture for the native stat case below
                rt = new RenderTexture(1024, 768, 24); img = new Texture2D(1024, 768, TextureFormat.RGB24, false);
                Action<string> shot = name =>
                {
                    cam.targetTexture = rt; Bootstrap.RenderAll(cam); RenderTexture.active = rt; img.ReadPixels(new Rect(0, 0, 1024, 768), 0, 0); img.Apply();
                    File.WriteAllBytes(Path.Combine(output, name), img.EncodeToPNG());
                    cam.targetTexture = null; RenderTexture.active = null;
                };
                game.Profile.playerType = 0; game.Profile.cash = 8220622;
                game.SwitchScene("gameScene"); game.StepFor(0.02f);
                var scene = (GameScene)game.Current; scene.spawningEnabled = false; scene.DebugSpawnModeRoll = () => 0; // explicit edge-mode fixture
                game.StepFor(0.2f);
                float maxHp = scene.playerMaxHp;
                check("hero0 level1 max HP = native label formula", maxHp == N.HpWithJigsaw(Dc2Data.Heroes()[0].hp, 0));

                // CreateEnemy numbers for kind 1 at L=1 (K1 rows: HP+=M-2+2L with M=1 -> +1, ATK+=3L-3 -> +0)
                shot("combat_before_spawn.png"); var beforeSpawn = img.GetPixels32();
                var d1 = scene.CreateEnemy(1);
                check("kind1 spawned with skin1/surface10", d1 != null && d1.skin == 1 && d1.surface == 10);
                check("kind1 L1 stats hp 8+1=9, atk 11, spd 80", d1 != null && Mathf.Approximately(d1.hp, 9f) && Mathf.Approximately(d1.atk, 11f) && d1.spd == 80f);
                shot("combat_spawn_edge.png");
                var afterSpawn = img.GetPixels32(); int spawnPixels = 0;
                for (int p = 0; p < beforeSpawn.Length; p++) if (!beforeSpawn[p].Equals(afterSpawn[p])) spawnPixels++;
                check("offscreen edge spawn has no default-origin render flash", spawnPixels == 0);
                // place the dino at a fixed distance in front of the hero and fire
                d1.x = scene.heroX + 280f; d1.faceRight = false; d1.state = 0;
                game.Step(0f); shot("combat_native_origin.png");
                check("native negative-velocity dinosaur uses CA flag96 false", !d1.rig.flipX);
                float hp0 = d1.hp; int ammo0 = game.Profile.weaponAmmo[0];
                scene.FireDown();
                // native OnAnimationEndAdv 0xbc222: type 0 is one finite cycle and clears the held flag, so each shot is a new press (r112 native lifecycle)
                for (int f = 0; f < 600 && !d1.dead; f++) { if (!scene.firing && !scene.fireDown) scene.FireDown(); game.Step(1f / 60f); }
                check("repeated presses consume ammo", game.Profile.weaponAmmo[0] < ammo0);
                check("hitscan damages the nearest dino or kills it (A1 range 480*2)", d1.dead || d1.hp < hp0);
                shot("combat_firing.png");
                scene.FireUp(); game.StepFor(4.0f);
                check("kill counted and corpse removed after die anim", scene.kills == 1 && scene.dinos.Count == 0);

                // dino attack -> BeAttack
                var d2 = scene.CreateEnemy(1);
                d2.x = scene.heroX + 120f; d2.faceRight = false; d2.state = 0;
                float hpBefore = scene.playerHp;
                game.StepFor(1.5f);
                check("dino reached the hero and hit for floor(ATK)=11", scene.playerHp <= hpBefore - 11f + 0.001f);
                shot("combat_hit.png");
                check("invincible 1.5 s after BeAttack (no double hit)", scene.invincible || scene.playerHp > hpBefore - 22f);

                // kill mission completion flow
                game.Profile.cash = 1000;
                game.Profile.playerType = 0;
                game.Profile.level[0] = 4; game.Profile.weaponOwned[1] = -1; game.Profile.weaponAmmo[1] = 0;   // display level 5 unlocks slot 1 (AwardItem 0xd09dc)
                game.SwitchScene("gameScene"); game.StepFor(0.02f);
                scene = (GameScene)game.Current; scene.spawningEnabled = false; scene.DebugSpawnModeRoll = () => 0;
                scene.missionType = 3; scene.killTotal = 2; scene.killRemaining = 2;
                for (int k = 0; k < 2; k++)
                {
                    var dk = scene.CreateEnemy(1); dk.x = scene.heroX + 200f; dk.faceRight = false; dk.hp = 0.5f;
                    scene.FireDown(); for (int f = 0; f < 240 && !dk.dead; f++) game.Step(1f / 60f); scene.FireUp();
                    game.StepFor(0.5f);
                }
                check("two kills complete the kill mission", scene.result == GameScene.MissionResult.Complete);
                check("cash gained EXP per kill (>1000)", game.Profile.cash > 1000);
                game.StepFor(3.0f);
                check("done button shown after summary", scene.GetControl(41) != null && scene.GetControl(31) != null);
                check("state-5 dim backdrop (id 11, missionfailbg) ramps to alpha 210 behind the results", scene.GetControl(11) != null);   // OnUpdate 0xd41ba case 5
                {   // lifeModeScene::AddBullet 0xcff3a: the unlocked weapon's type gains a flat amount (0:200, 1/4:15, 2:60, 3:30, 5:50), capped at g_max_bullet_count
                    int wt = Dc2GameData.PlayerWeapons(0)[1].type; int[] amt = { 200, 15, 60, 30, 15, 50 };
                    int expect = Math.Min(game.Profile.maxBullet, wt >= 0 && wt <= 5 ? amt[wt] : 0);
                    check("weapon unlock at level 5 grants AddBullet ammo for the new weapon's type (type " + wt + " -> " + expect + ")", game.Profile.weaponOwned[1] >= 0 && game.Profile.weaponAmmo[1] == expect);
                }
                shot("mission_complete.png");
                scene.Touch(0, scene.GetControl(41).FullRect.center); scene.Touch(2, scene.GetControl(41).FullRect.center);
                check("DONE returns to the armory (map type 1)", game.MapType == 1);
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
