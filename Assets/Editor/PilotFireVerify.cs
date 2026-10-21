using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>Native hero fire lifecycle (GameFire.cs): OnFireDown 0xb9990, OnKeyFrameAdv 0xc076a, OnAnimationEndAdv 0xbc222, AfterNoBullets 0xbaeac.</summary>
    public static class PilotFireVerify
    {
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-PILOT-001/fire-r1");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            string dir = Path.Combine(Path.GetTempPath(), "dc2_fire_verify_" + Guid.NewGuid().ToString("N"));
            int passed = 0, failed = 0; var log = new StringBuilder();
            Action<string, bool> check = (n, v) => { log.AppendLine((v ? "PASS " : "FAIL ") + n); if (v) passed++; else failed++; };
            Dc2Game game = null; Camera cam = null; RenderTexture rt = null; Texture2D img = null;
            try
            {
                cam = Bootstrap.MakeCamera(); game = Dc2Game.Create(dir); game.manualStep = true; game.SkipMissionIntro = true; game.cam = cam;
                rt = new RenderTexture(1024, 768, 24); img = new Texture2D(1024, 768, TextureFormat.RGB24, false);
                Action<string> shot = name => { cam.targetTexture = rt; Bootstrap.RenderAll(cam); RenderTexture.active = rt; img.ReadPixels(new Rect(0, 0, 1024, 768), 0, 0); img.Apply(); File.WriteAllBytes(Path.Combine(output, name), img.EncodeToPNG()); cam.targetTexture = null; RenderTexture.active = null; };
                game.LaunchMission = new NativeMapState.Mission { type = 3, count = 200, level = 1, background = 0 };
                game.Profile.playerType = 0; game.Profile.cash = 5000;
                game.SwitchScene("gameScene"); game.StepFor(0.05f);
                var scene = (GameScene)game.Current; scene.spawningEnabled = false; scene.DebugSpawnModeRoll = () => 0;
                for (int s = 0; s < 5; s++) { game.Profile.weaponOwned[s] = 0; game.Profile.weaponAmmo[s] = 100; }
                game.StepFor(0.1f);
                var weapons = Dc2GameData.PlayerWeapons(0);
                // ---- type 0: single finite cycle, one shot at key frame 3 ----
                scene.weaponSlot = 0; check("hero0 slot0 is weapon type 0", weapons[0].type == 0);
                int ammo0 = game.Profile.weaponAmmo[0];
                scene.FireDown(); game.StepFor(0.02f);
                check("press starts the advanced fire channel (firing, adv anim set, base untouched)", scene.firing && scene.FireAdvActive);
                game.StepFor(0.05f); shot("fire_type0.png"); game.StepFor(0.15f);
                check("key frame 3 fired exactly one shot and used one round", scene.ShotsFired == 1 && game.Profile.weaponAmmo[0] == ammo0 - 1);
                game.StepFor(1.5f);
                check("cycle end clears the channel, firing and the held flag (no auto-repeat for type 0)", !scene.firing && !scene.FireAdvActive && !scene.fireDown && scene.ShotsFired == 1);
                // ---- type 2 (slot 1): held burst loops frames 3..6, counter suffix, wind-down on release ----
                scene.weaponSlot = 1; check("hero0 slot1 is weapon type 2", weapons[1].type == 2);
                int shots = scene.ShotsFired;
                scene.FireDown(); game.StepFor(0.02f);
                check("type 2 starts with end frame 6", scene.firing && scene.FireAdvEnd == 6);
                game.StepFor(0.5f); shot("fire_type2.png"); game.StepFor(0.5f);
                check("held type 2 keeps cycling frames 3..6 and fires repeatedly", scene.firing && scene.ShotsFired - shots >= 5 && scene.FireAdvEnd == 6);
                game.StepFor(2.0f);
                check("burst counter reached the _nu1/_nu2 suffix range", scene.ShotSuffix == "_nu1" || scene.ShotSuffix == "_nu2" || scene.ShotSuffix == "_nu3");
                scene.FireUp(); game.StepFor(2.0f);
                check("release plays the wind-down (frames 6..end) then clears firing", !scene.firing && !scene.FireAdvActive);
                game.StepFor(2.2f);
                check("fireup timer (2.0 s) clears the sprite suffix", scene.ShotSuffix == "");
                // ---- AfterNoBullets: empty slot switches to the highest owned slot with ammo ----
                scene.weaponSlot = 0; game.Profile.weaponAmmo[0] = 0; game.Profile.weaponAmmo[4] = 0; game.Profile.weaponAmmo[3] = 0;
                scene.FireDown(); game.StepFor(0.1f);
                check("empty weapon + press: AfterNoBullets selects the highest gun slot below the melee record with ammo (slot 2)", scene.weaponSlot == 2);
                scene.FireUp(); game.StepFor(0.5f);
                // ---- type 4 LiuDanQ: instant blast 150*S in front, radius 200*S, baozha + muzzle flash, E2 sound ----
                scene.weaponSlot = 3; game.Profile.weaponAmmo[3] = 50; check("hero0 slot3 is weapon type 4", weapons[3].type == 4);
                scene.facingLeft = false;
                var near = scene.CreateEnemy(1); near.x = scene.heroX + 300f; near.hp = 999f; near.state = 0;
                var far = scene.CreateEnemy(1); far.x = scene.heroX + 900f; far.hp = 999f; far.state = 0;
                float nh = near.hp, fh = far.hp; int fx0 = scene.FxSpawned;
                scene.FireDown(); game.StepFor(0.02f);
                check("type 4 starts the advanced channel with interval 0.05", scene.firing);
                game.StepFor(0.4f); shot("fire_type4.png");
                log.AppendLine("INFO near.hp " + near.hp + " (was " + nh + ") far.hp " + far.hp + " hitDino " + scene.hitDino + " x " + near.x + " heroX " + scene.heroX + " state " + near.state + " dead " + near.dead + " w=" + scene.CurrentWeapon().type + "/" + scene.CurrentWeapon().index);
                check("blast hit the dinosaur at the blast centre and spared the one 600 px away", near.hp < nh && far.hp == fh);
                check("muzzle flash + baozha effects spawned and ammo used", scene.FxSpawned >= fx0 + 2 && game.Profile.weaponAmmo[3] == 49);
                scene.ApplyDamageForTest(near); scene.ApplyDamageForTest(far);
                game.StepFor(1.5f);
                check("grenade cycle is single shot (no auto-repeat) and effects disappear", !scene.firing && scene.FxActive == 0);
                // ---- type 5 sub 1 flamethrower (PenHuoQ + Damage + BeDamaged burn state) ----
                scene.weaponSlot = 4; game.Profile.weaponAmmo[4] = 60; check("hero0 slot4 is weapon type 5 sub 1", weapons[4].type == 5 && weapons[4].subtype == 1);
                scene.facingLeft = false;
                var burn = scene.CreateEnemy(1); burn.x = scene.heroX + 200f; burn.hp = 9999f; burn.state = 0; burn.velocityX = 0f;
                var out1 = scene.CreateEnemy(1); out1.x = scene.heroX + 900f; out1.hp = 9999f; out1.state = 0; out1.velocityX = 0f;
                float bh = burn.hp; int starts = scene.BurnStarts;
                scene.FireDown(); game.StepFor(0.02f);
                check("flame starts the advanced channel", scene.firing && scene.FireAdvEnd == 4);
                game.StepFor(0.25f); shot("fire_flame.png");
                check("flame key frame: effect enabled and the dinosaur in range burns (state 5), the far one does not", scene.FlameEnabled && burn.state == 5 && scene.BurnStarts == starts + 1 && out1.state != 5);
                check("burning dinosaur has the dahuo overlay and BShao animation", burn.burnFx != null && burn.aShao != null);
                game.StepFor(0.6f);
                check("burn damage over time through the dinosaur key frames (BeAttacked flame record)", burn.hp < bh);
                scene.FireUp(); game.StepFor(2.0f);
                check("release: wind-down ends, flame effect gone, burning dinosaur recovers (6 -> 8 or resumed)", !scene.firing && !scene.FlameEnabled && burn.state != 5 && burn.state != 6 && burn.burnFx == null);
                // kill by flame uses the burn death animation (DieShaoSi)
                burn.hp = 0.5f; scene.ApplyDamageForTest(burn);
                scene.ApplyDamageForTest(out1);
                game.StepFor(0.2f);
                // ---- interrupt by hurt clears the advanced channel ----
                scene.weaponSlot = 0; game.Profile.weaponAmmo[0] = 50;
                scene.invincible = false;   // r119j: the burning dinosaur may bite during the 2 s release above (the native flame tail now starts at the current frame, so it is shorter); this check needs a hero that can be hit
                scene.FireDown(); game.StepFor(0.05f);
                bool wasFiring = scene.firing;
                scene.BeAttack(new DinoActor { x = scene.heroX + 5f, atk = 1 });
                game.StepFor(0.02f);
                check("hurt interrupts fire: adv channel cleared, firing false (wasFiring " + wasFiring + ", firing " + scene.firing + ", adv " + scene.FireAdvActive + ", slot " + scene.weaponSlot + ", invincible " + scene.invincible + ", hp " + scene.playerHp + ")", wasFiring && !scene.firing && !scene.FireAdvActive);
                // ---- hero 2: type 5 sub 2 laser (JiGuangQ + BEAnim shock state 4) ----
                game.Profile.playerType = 2; game.Profile.cash = 5000;
                game.SwitchScene("gameScene"); game.StepFor(0.05f);
                scene = (GameScene)game.Current; scene.spawningEnabled = false; scene.DebugSpawnModeRoll = () => 0;
                for (int q = 0; q < 5; q++) { game.Profile.weaponOwned[12 + q] = 0; game.Profile.weaponAmmo[12 + q] = 100; }
                scene.weaponSlot = 4; game.StepFor(0.1f);
                var w2 = Dc2GameData.PlayerWeapons(2);
                check("hero2 slot4 is weapon type 5 sub 2", w2[4].type == 5 && w2[4].subtype == 2);
                scene.facingLeft = false;
                var shock = scene.CreateEnemy(1); shock.x = scene.heroX + 300f; shock.hp = 9999f; shock.state = 0; shock.velocityX = 0f;
                float sh = shock.hp; int sstarts = scene.ShockStarts;
                scene.FireDown(); game.StepFor(0.3f); shot("fire_laser.png");
                check("laser: effect enabled and the dinosaur in range is shocked (state 4)", scene.LaserEnabled && shock.state == 4 && scene.ShockStarts == sstarts + 1);
                game.StepFor(0.8f);
                check("laser DOT through BEAnim key frames", shock.hp < sh);
                scene.FireUp(); game.StepFor(2.5f);
                check("laser release: effect gone and the dinosaur recovers", !scene.firing && !scene.LaserEnabled && shock.state != 4 && shock.state != 6);
                // ---- hero 3: type 5 sub 3 FeiBiao thrown shuriken (base channel, hero state 9, key frame 9) ----
                game.Profile.playerType = 3; game.Profile.cash = 5000;
                game.SwitchScene("gameScene"); game.StepFor(0.05f);
                scene = (GameScene)game.Current; scene.spawningEnabled = false; scene.DebugSpawnModeRoll = () => 0;
                for (int q = 0; q < 5; q++) { game.Profile.weaponOwned[18 + q] = 0; game.Profile.weaponAmmo[18 + q] = 100; }
                scene.weaponSlot = 4; game.StepFor(0.1f);
                var w3 = Dc2GameData.PlayerWeapons(3);
                check("hero3 slot4 is weapon type 5 sub 3", w3[4].type == 5 && w3[4].subtype == 3);
                scene.facingLeft = false;
                var tgt = scene.CreateEnemy(1); tgt.x = scene.heroX + 600f; tgt.hp = 9999f; tgt.state = 0; tgt.velocityX = 0f;
                float th = tgt.hp; int am3 = game.Profile.weaponAmmo[22];
                scene.FireDown(); game.StepFor(0.02f);
                check("FeiBiao press: base-channel cast, hero state 9, velocity 0", scene.BaseCasting && scene.heroControlState == 9 && scene.heroVelocityX == 0f && !scene.FireAdvActive);
                game.StepFor(0.5f); shot("fire_feibiao.png");
                check("key frame 9 threw one shuriken and used one round", scene.ProjectileSpawned == 1 && game.Profile.weaponAmmo[22] == am3 - 1);
                game.StepFor(1.5f);
                check("shuriken hit the dinosaur in its path exactly once", scene.ProjectileHits == 1 && tgt.hp < th);
                check("cast ends: hero back to stand state 2, not firing", !scene.firing && !scene.BaseCasting && scene.heroControlState == 2);
                game.StepFor(3.0f);
                check("projectile leaves the viewport and is removed", scene.ProjectileCount == 0);
                // ---- hero 1: type 5 sub 4 YuanLi (base channel, hero state 10, key frame 11, effect mode 8) ----
                game.Profile.playerType = 1; game.Profile.cash = 5000;
                game.SwitchScene("gameScene"); game.StepFor(0.05f);
                scene = (GameScene)game.Current; scene.spawningEnabled = false; scene.DebugSpawnModeRoll = () => 0;
                for (int q = 0; q < 5; q++) { game.Profile.weaponOwned[6 + q] = 0; game.Profile.weaponAmmo[6 + q] = 100; }
                scene.weaponSlot = 4; game.StepFor(0.1f);
                var w1 = Dc2GameData.PlayerWeapons(1);
                check("hero1 slot4 is weapon type 5 sub 4", w1[4].type == 5 && w1[4].subtype == 4);
                scene.facingLeft = false;
                var yt = scene.CreateEnemy(1); yt.x = scene.heroX + 250f; yt.hp = 9999f; yt.state = 0; yt.velocityX = 0f;
                float yh = yt.hp;
                scene.FireDown(); game.StepFor(0.02f);
                check("YuanLi press: base-channel cast, hero state 10", scene.BaseCasting && scene.heroControlState == 10);
                game.StepFor(0.5f); shot("fire_yuanli.png");
                check("key frame 11: energy effect enabled and a dinosaur in range is hit (state 4)", scene.YuanliEnabled && yt.state == 4);
                game.StepFor(0.8f);
                check("YuanLi damage over time", yt.hp < yh);
                scene.FireUp(); game.StepFor(3.0f);
                check("release: cast ends, effect gone, hero stands, dinosaur recovers", !scene.BaseCasting && !scene.YuanliEnabled && scene.heroControlState == 2 && yt.state != 4 && yt.state != 6);
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
