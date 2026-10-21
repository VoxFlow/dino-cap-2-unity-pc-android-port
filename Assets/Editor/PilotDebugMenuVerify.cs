using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>DC2-BUGS-001 O2: the DEBUG_ONLY debug menu (F1). Drives the same public actions the buttons call and checks that each one
    /// lands in the native save file (Profile.Load of dinocap2hd.sav), that the battle cheats work, and that every flag is off by default.</summary>
    public static class PilotDebugMenuVerify
    {
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-BUGS-001/debugmenu-r1");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            int passed = 0, failed = 0; var log = new StringBuilder();
            Action<string, bool> check = (n, v) => { log.AppendLine((v ? "PASS " : "FAIL ") + n); if (v) passed++; else failed++; };
            string dir = Path.Combine(Path.GetTempPath(), "dc2_debugmenu_" + Guid.NewGuid().ToString("N"));
            Camera cam = null; Dc2Game g = null;
            try
            {
                cam = Bootstrap.MakeCamera();
                g = Dc2Game.Create(dir); g.cam = cam; g.manualStep = true; g.SkipMissionIntro = true;
                DebugMenu.Install(g.gameObject, g); var m = g.GetComponent<DebugMenu>();
                check("all debug flags are off by default (inert while closed)", !g.DebugGodMode && !g.DebugInfiniteAmmo && !g.DebugMenuOpen && g.DebugTimeScale == 1f);
                m.SetOpen(true); bool opened = g.DebugMenuOpen; m.SetOpen(false);
                check("r119o: Close (SetOpen false) releases the game input gate", opened && !g.DebugMenuOpen);
                g.SwitchScene("menuScene"); g.Step(0f);

                m.AddCash(100000); m.AddCash(1000);
                check("add cash: 101,000 in the profile and in the saved file", g.Profile.cash == 101000 && Profile.Load(dir).cash == 101000);
                m.SetCash(20000000);
                check("cash clamps at the native cap 9,999,999", g.Profile.cash == 9999999);
                m.ResetCash();
                check("reset cash: 0 saved", Profile.Load(dir).cash == 0);

                m.UnlockAllHeroes();
                var p1 = Profile.Load(dir);
                check("unlock heroes: every hero level >= 0 in the saved file", Array.TrueForAll(p1.level, l => l >= 0));
                g.Profile.playerType = 2; m.AddHeroLevel(10);
                check("hero level +10 for the current hero", g.Profile.level[2] == 10);
                m.AddHeroLevel(500);
                check("hero level caps at 98 (UpdateExp 0xd1f90 levels up only while <= 97)", g.Profile.level[2] == 98);
                m.ResetLevels();
                check("reset levels: owned heroes back to 0, exp cleared", g.Profile.level[2] == 0 && g.Profile.exp[2] == 0 && g.Profile.level[1] == 0);
                m.ResetHeroesBought();
                var p2 = Profile.Load(dir);
                check("reset heroes bought: only hero 1 owned, current hero 1", p2.level[0] == 0 && p2.level[1] == -1 && p2.level[4] == -1 && p2.playerType == 0);

                g.Profile.weaponAmmo[3] = 7; g.Profile.weaponOwned[3] = 1; m.ResetWeapons();
                var fresh = new Profile();
                check("reset weapons: owned / ammo / levels back to the reset defaults", g.Profile.weaponAmmo[3] == fresh.weaponAmmo[3] && g.Profile.weaponOwned[3] == fresh.weaponOwned[3]);
                m.GiveAllJigsawPieces();
                check("give jigsaw pieces: all 80 earned (not placed) in the saved file", Array.TrueForAll(Profile.Load(dir).jigsawGet, v => v == 1));
                m.ResetJigsawPieces();
                var pj = Profile.Load(dir);
                check("reset jigsaw: nothing earned or placed, counts 0", Array.TrueForAll(pj.jigsawGet, v => v == 0) && Array.TrueForAll(pj.jigsawActive, v => v == 0) && pj.jigsaw1Count == 0 && pj.jigsaw5Count == 0);
                g.Profile.jigsawActive[0] = 1;
                int k1 = m.GiveRandomJigsawPiece(), k2 = m.GiveRandomJigsawPiece();
                var pk = Profile.Load(dir); int earned = 0; foreach (var v in pk.jigsawGet) earned += v;
                check("give random jigsaw: two distinct free pieces earned and saved (" + k1 + "," + k2 + ")", k1 >= 0 && k2 >= 0 && k1 != k2 && k1 != 0 && k2 != 0 && earned == 2 && pk.jigsawGet[k1] == 1 && pk.jigsawGet[k2] == 1);
                m.UnlockAllMissions();
                check("unlock missions: mission index raised", g.Profile.mission >= 30);

                // battle cheats
                m.LaunchMission(3, 1, 0); g.Step(0f); g.Step(1f / 60f);
                var s = g.Current as GameScene;
                check("launch mission type 3 / bg 0 enters the game scene", s != null && g.LaunchMission.type == 3 && g.LaunchMission.background == 0);
                if (s != null)
                {
                    s.spawningEnabled = false; s.ignorePlayer = true;
                    m.SpawnDinosaur(1); m.SpawnDinosaur(2);
                    check("spawn adds dinosaurs", s.dinos.Count >= 2);
                    int killed = m.KillAllDinosaurs();
                    check("kill all dinosaurs kills every live one (" + killed + ")", killed >= 2);
                    float hp0 = s.playerHp; var d = s.CreateEnemy(1);
                    g.DebugGodMode = true; s.BeAttack(d);
                    check("god mode: a dinosaur hit leaves HP unchanged but still plays the hit (hurt state, Wudi invincibility)", s.playerHp == hp0 && s.invincible && s.HeroHurting);
                    g.StepFor(2.0f);   // Wudi 1.5 s ends before the next hit
                    g.DebugGodMode = false; s.BeAttack(d);
                    check("god mode off: the same hit costs HP", s.playerHp < hp0);
                    int idx = s.CurrentAmmoIndex; g.Profile.weaponAmmo[idx] = 50;
                    g.DebugInfiniteAmmo = true; s.DebugFireOneShot();
                    check("infinite ammo: a shot keeps the ammo count", g.Profile.weaponAmmo[idx] == 50);
                    g.DebugInfiniteAmmo = false; s.DebugFireOneShot();
                    check("infinite ammo off: a shot spends one", g.Profile.weaponAmmo[idx] == 49);
                }
            }
            catch (Exception e) { failed++; log.AppendLine("EXCEPTION " + e); }
            finally
            {
                if (g != null) UnityEngine.Object.DestroyImmediate(g.gameObject);
                if (cam != null) UnityEngine.Object.DestroyImmediate(cam.gameObject);
                if (Directory.Exists(dir)) Directory.Delete(dir, true);
                log.AppendLine("SUMMARY passes=" + passed + " fails=" + failed);
                File.WriteAllText(Path.Combine(output, "verify.txt"), log.ToString()); Debug.Log(log.ToString());
                PilotExit.Exit(failed == 0 ? 0 : 1);
            }
        }
    }
}
