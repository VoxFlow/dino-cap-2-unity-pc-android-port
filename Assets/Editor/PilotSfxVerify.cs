using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>SFX triggers recovered from the PlaySound call-site scan (Dev/Reports/DC2-PILOT-001/playsound_scan.r114.json): armored-surface shot/hit/die sounds,
    /// melee swing table, roll sounds, no-bullet sound, level-up registration; every requested name must be registered (MissingSounds empty).</summary>
    public static class PilotSfxVerify
    {
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-PILOT-001/sfx-r1");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            string dir = Path.Combine(Path.GetTempPath(), "dc2_sfx_verify_" + Guid.NewGuid().ToString("N"));
            int passed = 0, failed = 0; var log = new StringBuilder();
            Action<string, bool> check = (n, v) => { log.AppendLine((v ? "PASS " : "FAIL ") + n); if (v) passed++; else failed++; };
            Dc2Game game = null; Camera cam = null;
            try
            {
                cam = Bootstrap.MakeCamera(); game = Dc2Game.Create(dir); game.manualStep = true; game.SkipMissionIntro = true; game.cam = cam;
                game.LaunchMission = new NativeMapState.Mission { type = 3, count = 200, level = 1, background = 0 };
                game.Profile.playerType = 0; game.Profile.cash = 5000;
                game.SwitchScene("gameScene"); game.StepFor(0.05f);
                var scene = (GameScene)game.Current; scene.spawningEnabled = false; scene.DebugSpawnModeRoll = () => 0;
                for (int s = 0; s < 5; s++) { game.Profile.weaponOwned[s] = 0; game.Profile.weaponAmmo[s] = 100; }
                game.StepFor(0.1f);
                var audio = game.Audio;
                Func<string, int> count = n => audio.RequestedSounds.Count(x => x == n);
                // ---- shots at an armored dinosaur (kind 3, SURFACE 101): A_shot_tie 50% per Attack ----
                scene.weaponSlot = 0; scene.facingLeft = false; scene.ignorePlayer = true;   // dinosaurs ignore the hero: the test measures sounds, not survival
                var armor = scene.CreateEnemy(3); armor.x = scene.heroX + 200f; armor.hp = 99999f; armor.state = 0; armor.velocityX = 0f;
                log.AppendLine("INFO armored dino surface " + armor.surface);
                check("kind 3 is armored (SURFACE > 100)", armor.surface > 100);
                for (int i = 0; i < 30; i++) { scene.FireDown(); game.StepFor(0.9f); scene.FireUp(); game.StepFor(0.05f); armor.x = scene.heroX + 200f; armor.state = 0; armor.velocityX = 0f; }
                int tie = count("A_shot_tie"), shots = count("Attack_AAudio");
                log.AppendLine("INFO shots " + shots + " A_shot_tie " + tie);
                check("30 pistol shots at an armored dinosaur played A_shot_tie several times (50% each) but not every time", tie >= 5 && tie < shots);
                // ---- melee swing: armored -> A_hit_tie_mugun (hero 0 melee subtype 0), else Attack_GAudio ----
                int g0 = count("Attack_GAudio"), m0 = count("A_hit_tie_mugun");
                armor.x = scene.heroX + 160f;   // r119c: melee reach is measured to the head (~64 px nearer than the origin, JinAttack 0xbe170); 160 keeps the head in front of the hero during the swing armor.state = 0;
                scene.HitDownForTest(); game.StepFor(1.2f);
                check("melee on an armored dinosaur plays A_hit_tie_mugun", count("A_hit_tie_mugun") == m0 + 1);
                scene.ApplyDamageForTest(armor); game.StepFor(0.5f);
                var soft = scene.CreateEnemy(1); soft.x = scene.heroX + 160f; soft.hp = 99999f; soft.state = 0; soft.velocityX = 0f;
                scene.HitDownForTest(); game.StepFor(1.2f);
                check("melee on a normal dinosaur plays Attack_GAudio", count("Attack_GAudio") == g0 + 1);
                scene.HitDownForTest(); game.StepFor(0.0f);
                // melee with no target still plays the swing sound (native plays it at the end of every JinAttack)
                soft.x = scene.heroX + 5000f; game.StepFor(0.1f);
                int g1 = count("Attack_GAudio");
                scene.HitDownForTest(); game.StepFor(1.2f);
                check("melee swing with no target still plays the swing sound", count("Attack_GAudio") == g1 + 1);
                // ---- death sounds ----
                int d1 = count("Audio_death1Audio") + count("A_die_tie_qiang");
                scene.ApplyDamageForTest(soft); game.StepFor(0.1f);
                check("killing a dinosaur plays the DieNormal sound (death1 or A_die_tie_qiang)", count("Audio_death1Audio") + count("A_die_tie_qiang") > d1);
                // ---- roll ----
                int r0 = count("p1rollAudio");
                scene.OnRollDown(); game.StepFor(0.05f);
                check("roll plays p1rollAudio (hero 0)", count("p1rollAudio") == r0 + 1);
                game.StepFor(4.0f);
                // ---- no bullet ----
                scene.weaponSlot = 0; game.Profile.weaponAmmo[0] = 1; scene.SelectSlotForTest(0);
                int n0 = count("Audio_NobulletAudio");
                scene.FireDown(); game.StepFor(0.6f); scene.FireUp(); game.StepFor(0.6f);
                check("firing the last round updates the HUD and plays Audio_NobulletAudio; icon_none visible", game.Profile.weaponAmmo[0] == 0 && count("Audio_NobulletAudio") > n0 && scene.GetControl(21) != null && scene.GetControl(21).visible);
                // ---- Girl ----
                game.Profile.playerType = 4;
                game.SwitchScene("gameScene"); game.StepFor(0.05f);
                scene = (GameScene)game.Current; scene.spawningEnabled = false; scene.DebugSpawnModeRoll = () => 0;
                for (int s = 0; s < 5; s++) { game.Profile.weaponOwned[24 + s] = 0; game.Profile.weaponAmmo[24 + s] = 100; }
                game.StepFor(0.2f);
                int gr = count("p5rollAudio");
                scene.OnRollDown(); game.StepFor(2.5f);
                check("Girl roll plays p5rollAudio at OnRollDown and again at the roll timeout", count("p5rollAudio") >= gr + 2);
                int gm = count("p5hit_muAudio");
                scene.HitDownForTest(); game.StepFor(1.5f);
                check("Girl melee below max level plays p5hit_muAudio", count("p5hit_muAudio") == gm + 1);
                // level-up audio registered
                check("levelupAudio, roll sounds registered (no missing sound names requested in the whole run)", audio.MissingSounds.Count == 0);
                log.AppendLine("INFO missing: " + string.Join(",", audio.MissingSounds.ToArray()));
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
