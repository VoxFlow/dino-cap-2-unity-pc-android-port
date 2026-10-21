using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>Girl hero (playerNewAniClass) gameplay lifecycle on the CompAnimObj rig: stand/move, fire per weapon type, melee, hurt, roll, die (GameGirl.cs).</summary>
    public static class PilotGirlVerify
    {
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-PILOT-001/girl-r1");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            string dir = Path.Combine(Path.GetTempPath(), "dc2_girl_verify_" + Guid.NewGuid().ToString("N"));
            int passed = 0, failed = 0; var log = new StringBuilder();
            Action<string, bool> check = (n, v) => { log.AppendLine((v ? "PASS " : "FAIL ") + n); if (v) passed++; else failed++; };
            Dc2Game game = null; Camera cam = null; RenderTexture rt = null; Texture2D img = null;
            try
            {
                cam = Bootstrap.MakeCamera(); game = Dc2Game.Create(dir); game.manualStep = true; game.SkipMissionIntro = true; game.cam = cam;
                rt = new RenderTexture(1024, 768, 24); img = new Texture2D(1024, 768, TextureFormat.RGB24, false);
                Action<string> shot = name => { cam.targetTexture = rt; Bootstrap.RenderAll(cam); RenderTexture.active = rt; img.ReadPixels(new Rect(0, 0, 1024, 768), 0, 0); img.Apply(); File.WriteAllBytes(Path.Combine(output, name), img.EncodeToPNG()); cam.targetTexture = null; RenderTexture.active = null; };
                game.LaunchMission = new NativeMapState.Mission { type = 3, count = 200, level = 1, background = 0 };
                game.Profile.playerType = 4; game.Profile.cash = 5000;
                game.SwitchScene("gameScene"); game.StepFor(0.05f);
                var scene = (GameScene)game.Current; scene.spawningEnabled = false; scene.DebugSpawnModeRoll = () => 0;
                for (int s = 0; s < 5; s++) { game.Profile.weaponOwned[24 + s] = 0; game.Profile.weaponAmmo[24 + s] = 100; }
                game.StepFor(0.2f);
                var weapons = Dc2GameData.PlayerWeapons(4);
                check("Girl weapon slots: types 0,1,3,4,5(sub5)", weapons[0].type == 0 && weapons[1].type == 1 && weapons[2].type == 3 && weapons[3].type == 4 && weapons[4].type == 5 && weapons[4].subtype == 5);
                check("Girl uses the recovered camera/world phase and the component rig stand", scene.IsGirl && scene.GirlBaseName == "stand" && scene.heroControlState == 2 && scene.GirlHasSprite("shenti"));
                log.AppendLine("INFO girl sprites ok; bshouqiang sel=" + scene.GirlSelection("bshouqiang") + " lian sel=" + scene.GirlSelection("lian"));
                check("pistol avatar selected for weapon slot 0 (short family)", scene.GirlSelection("bshouqiang") == "bshouqiangnewplayer_shouqiang001" && scene.GirlShortFamily);
                shot("girl_stand.png");
                // ---- locomotion ----
                float x0 = scene.heroX; scene.rightDown = true; game.StepFor(0.5f);
                check("right input: move animation and hero advances", scene.GirlBaseName == "move" && scene.heroX > x0 + 50f && scene.heroControlState == 1);
                shot("girl_move.png");
                scene.rightDown = false; game.StepFor(0.2f);
                check("release: back to stand, state 2", scene.GirlBaseName == "stand" && scene.heroControlState == 2);
                // ---- type 0 pistol: re-press restarts at frame 1; fresh press key 2 ----
                scene.weaponSlot = 0; int shots = scene.ShotsFired; int am = game.Profile.weaponAmmo[24];
                scene.FireDown(); game.StepFor(0.016f);
                check("press starts the advanced channel", scene.firing && scene.GirlAdvActive);
                game.StepFor(0.25f); shot("girl_fire_pistol.png");
                check("muzzle flash effect (g_player_fire_eff) spawned at the gun sprite", scene.FxSpawned >= 1 && scene.LastFxTag == "qh");
                check("pistol fired one shot at adv key 2 and used a round", scene.ShotsFired == shots + 1 && game.Profile.weaponAmmo[24] == am - 1);
                game.StepFor(1.2f);
                check("single cycle ends: firing and held flag cleared", !scene.firing && !scene.fireDown && !scene.GirlAdvActive);
                // ---- type 1 (sandan) long family ----
                scene.SelectSlotForTest(1);
                check("slot 1 switches to the long family with the sandan avatar", !scene.GirlShortFamily && scene.GirlSelection("bshouqiang") == "bshouqiangnewplayer_sandan");
                shots = scene.ShotsFired;
                scene.FireDown(); game.StepFor(0.5f);
                check("shotgun fires once at key 1", scene.ShotsFired == shots + 1);
                game.StepFor(1.5f);
                check("shotgun cycle ends", !scene.firing);
                // ---- type 3 gatling: held burst ----
                scene.SelectSlotForTest(2); shots = scene.ShotsFired; scene.fireDown = false;
                scene.FireDown(); game.StepFor(0.3f);
                shot("girl_fire_gatling.png");
                game.StepFor(0.7f);
                check("held gatling fires repeatedly", scene.firing && scene.ShotsFired - shots >= 4);
                scene.FireUp(); game.StepFor(2.0f);
                check("release winds down and clears firing", !scene.firing);
                // ---- type 4 grenade launcher: blast at sprite x +/- 300, radius 360 ----
                scene.SelectSlotForTest(3); scene.facingLeft = false;
                var near = scene.CreateEnemy(1); near.x = scene.heroX + 420f; near.hp = 999f; near.state = 0; near.velocityX = 0f;
                float nh = near.hp; int fx0 = scene.FxSpawned;
                scene.FireDown(); game.StepFor(0.6f);
                shot("girl_fire_grenade.png");
                check("grenade blast hit a dinosaur near the blast point and spawned the explosion", near.hp < nh && scene.FxSpawned > fx0);
                scene.ApplyDamageForTest(near);
                game.StepFor(1.5f);
                // ---- type 5/5 sniper ----
                scene.SelectSlotForTest(4); shots = scene.ShotsFired; scene.fireDown = false;
                scene.FireDown(); game.StepFor(0.8f);
                check("sniper fires one shot", scene.ShotsFired == shots + 1);
                game.StepFor(1.5f);
                // ---- melee ----
                scene.SelectSlotForTest(0); scene.fireDown = false;
                var m = scene.CreateEnemy(1); m.x = scene.heroX + 100f; m.hp = 9999f; m.state = 0; m.velocityX = 0f;
                float mh = m.hp;
                scene.HitDownForTest(); game.StepFor(0.02f);
                check("melee: state 3, hit animation", scene.heroControlState == 3 && scene.GirlBaseName == "hit");
                game.StepFor(0.3f); shot("girl_melee.png");
                game.StepFor(1.0f);
                check("melee key frame 11 hit the dinosaur and the animation returned to stand", m.hp < mh && scene.heroControlState == 2 && scene.GirlBaseName == "stand");
                scene.ApplyDamageForTest(m);
                // ---- hurt ----
                float hp0 = scene.playerHp;
                scene.invincible = false;
                scene.BeAttack(new DinoActor { x = scene.heroX + 5f, atk = 5 });
                game.StepFor(0.05f);
                check("hurt: state 5, behit animation, hp reduced", scene.heroControlState == 5 && scene.GirlBaseName == "behit" && scene.playerHp < hp0);
                game.StepFor(1.5f);
                check("hurt ends back to stand", scene.heroControlState == 2 && scene.GirlBaseName == "stand");
                scene.invincible = false;
                // ---- roll ----
                scene.facingLeft = false; float rx = scene.heroX;
                scene.OnRollDown(); game.StepFor(0.02f);
                check("roll: state 7, roll_start animation", scene.rolling && scene.heroControlState == 7 && scene.GirlBaseName == "roll");
                check("r119d roll_start: velocity 0, the girl stays in place (OnRollDown 0x128238) (moved " + (scene.heroX - rx) + ")", scene.GirlRollVelocity == 0f && scene.heroX == rx);
                game.StepFor(0.3f); shot("girl_roll_start.png");
                game.StepFor(1.2f);
                log.AppendLine("INFO after roll heroX moved " + (scene.heroX - rx) + " phase=" + scene.GirlRollPhase + " base=" + scene.GirlBaseName + " state=" + scene.heroControlState);
                check("roll phases: moved forward during the 0.5 s travel, then ends in stand", scene.heroX > rx + 100f);
                game.StepFor(1.5f);
                check("roll ends: not rolling, stand, state 2", !scene.rolling && scene.heroControlState == 2 && scene.GirlBaseName == "stand");
                // ---- die ----
                typeof(GameScene).GetMethod("GirlGetEgg", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(scene, null);   // shenti -> newplayer_shenti2 avatar
                bool avatarBefore = !scene.GirlSelectionIsDefault;
                log.AppendLine("INFO before die: avatar substitution active = " + avatarBefore);
                scene.invincible = false; scene.playerHp = 1f;
                scene.BeAttack(new DinoActor { x = scene.heroX + 5f, atk = 50 });
                game.StepFor(0.1f); shot("girl_die.png");
                check("die: heroDead, DieAnim", scene.heroDead && scene.GirlBaseName == "DieAnim" && scene.heroControlState == 4);
                check("die: ClearAvatar drops the weapon/skin avatars (Die 0x126a9c)", avatarBefore && scene.GirlSelectionIsDefault);
                game.StepFor(6.0f);
                check("die animation end triggers game over (mission failed)", scene.GirlDieEnded);
                // ---- egg mission: carrying avatar ----
                game.LaunchMission = new NativeMapState.Mission { type = 5, count = 1200f, level = 1, background = 1, info = 3 };
                game.LaunchMission.enemies.Add(1);
                game.Profile.playerType = 4;
                game.SwitchScene("gameScene"); game.StepFor(0.05f);
                scene = (GameScene)game.Current; scene.spawningEnabled = false;
                check("egg mission as Girl: body avatar default before pickup", scene.GirlSelection("shenti") == "shentinewplayer_shenti");
                scene.heroX = scene.EggX - 59f; game.StepFor(0.1f);
                check("pickup: carrying and the shenti component takes newplayer_shenti2", scene.EggCarrying && scene.GirlSelection("shenti") == "shentinewplayer_shenti2");
                shot("girl_egg_carry.png");
                scene.heroX = -170f; game.StepFor(0.05f);
                scene.EggHitForTest(); game.StepFor(0.05f);
                check("drop: egg dropped; the shenti selection persists until the next SetAnimation (native DoSetAvatar only adds)", !scene.EggCarrying && scene.GirlSelection("shenti") == "shentinewplayer_shenti2");
                scene.rightDown = true; game.StepFor(0.3f); scene.rightDown = false;
                check("after the next animation switch the default body avatar returns", scene.GirlSelection("shenti") == "shentinewplayer_shenti");
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
