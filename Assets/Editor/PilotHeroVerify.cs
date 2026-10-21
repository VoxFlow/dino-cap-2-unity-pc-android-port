using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>Hero (m_map_type 2) UI shell: native rect/permutation/formula checks and a rendered capture.</summary>
    public static class PilotHeroVerify
    {
        static string Label(Dc2Scene s, int id) { foreach (var l in s.Labels) if (l.id == id) return l.Text; return null; }

        static void Capture(Camera cam, string path)
        {
            var rt = new RenderTexture(1024, 768, 24);
            var img = new Texture2D(1024, 768, TextureFormat.RGB24, false);
            cam.targetTexture = rt; Bootstrap.RenderAll(cam); RenderTexture.active = rt;
            img.ReadPixels(new Rect(0, 0, 1024, 768), 0, 0); img.Apply();
            File.WriteAllBytes(path, img.EncodeToPNG());
            cam.targetTexture = null; RenderTexture.active = null;
            UnityEngine.Object.DestroyImmediate(rt); UnityEngine.Object.DestroyImmediate(img);
        }

        static string Part(NativeCaAnimation animation, string name)
        {
            foreach (var part in animation.frames[0].parts) if (part.name == name) return part.reference;
            return null;
        }

        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-PILOT-001/hero-r8");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            string dir = Path.Combine(Path.GetTempPath(), "dc2_hero_verify_" + Guid.NewGuid().ToString("N"));
            int passed = 0, failed = 0; var log = new StringBuilder();
            Action<string, bool> check = (n, v) => { log.AppendLine((v ? "PASS " : "FAIL ") + n); if (v) passed++; else failed++; };
            Dc2Game game = null; Camera cam = null;
            try
            {
                cam = Bootstrap.MakeCamera(); game = Dc2Game.Create(dir); game.manualStep = true; game.SkipMissionIntro = true; game.cam = cam;
                game.SwitchScene("selectScene");
                var scene = (SelectScene)game.Current;
                game.StepFor(1.0f);
                Vector2 hit = scene.GetControl(54).FullRect.center;
                scene.Touch(0, hit); scene.Touch(2, hit);
                check("hero button queues map type 2", game.MapType == 2);
                game.StepFor(0.05f);                                   // Game::Update drains the queued SwitchScene
                scene = (SelectScene)game.Current;
                game.StepFor(1.0f);
                check("title id35 rect = (0,32,346,134)", scene.GetControl(35).FullRect == new Rect(0, 32, 346, 134));
                check("title frame ui_title_trvernFrame", scene.GetControl(35).FrameName == "ui_title_trvernFrame");
                check("hero0 icon slot0 rect = (142,92,146,174)", scene.GetControl(6).FullRect == new Rect(142, 92, 146, 174));
                check("hero1 icon uses slot3 rect = (544,92,146,174)", scene.GetControl(7).FullRect == new Rect(544, 92, 146, 174));
                check("hero2 icon uses slot1 rect = (278,92,146,174)", scene.GetControl(8).FullRect == new Rect(278, 92, 146, 174));
                check("hero3 icon uses slot2 rect = (412,92,146,174)", scene.GetControl(9).FullRect == new Rect(412, 92, 146, 174));
                check("hero4 icon uses slot4 rect = (672,92,146,174)", scene.GetControl(10).FullRect == new Rect(672, 92, 146, 174));
                check("icon frame names ui_p%d_icon_gFrame", scene.GetControl(8).FrameName == "ui_p3_icon_gFrame");
                check("icon bg0 rect = (162,122,122,112)", scene.GetControl(106).FullRect == new Rect(162, 122, 122, 112));
                check("icon bg1 steps 68*2 px", scene.GetControl(107).FullRect.x == 298);
                check("select frame hero0 = (154,108,140,140)", scene.GetControl(28).FullRect == new Rect(154, 108, 140, 140));
                check("hero0 HP label 100 and level label 1", Label(scene, 42) == "100" && Label(scene, 43) == "1");
                check("hero0 exp 0 percent with cover (634,516,190,38)", scene.heroExpPercent == 0 && scene.GetControl(58).FullRect == new Rect(634, 516, 190, 38));
                check("gold label 0", Label(scene, 51) == "0");
                check("native controls present (49 iap, 38 back, 18 back image)", scene.GetControl(49) != null && scene.GetControl(38) != null && scene.GetControl(18) != null);
                check("hero background native world rect", scene.GetControl(9100).FullRect == new Rect(0,32,1024,704));
                check("hero0 stand A1 at native scale and position", scene.HeroRig.Source == "p_stand" && Part(scene.HeroRig.Player.animation,"com1") == "A1" && scene.HeroRig.Player.objectScale == 1.5f && scene.HeroRig.Player.position == new Vector2(-240,191));
                var rt = new RenderTexture(1024, 768, 24); var img = new Texture2D(1024, 768, TextureFormat.RGB24, false);
                cam.targetTexture = rt; Bootstrap.RenderAll(cam); RenderTexture.active = rt; img.ReadPixels(new Rect(0, 0, 1024, 768), 0, 0); img.Apply();
                File.WriteAllBytes(Path.Combine(output, "hero_ui.png"), img.EncodeToPNG());
                cam.targetTexture = null; RenderTexture.active = null; UnityEngine.Object.DestroyImmediate(rt); UnityEngine.Object.DestroyImmediate(img);

                for (int hero = 1; hero <= 3; hero++)
                {
                    hit = scene.GetControl(6+hero).FullRect.center; scene.Touch(0,hit); scene.Touch(2,hit);
                    game.StepFor(1f);
                    Capture(cam, Path.Combine(output, "hero"+hero+"_stand.png"));
                    var animation = scene.HeroRig.Player.animation;
                    check("hero"+hero+" native stand source", scene.HeroRig.Source == new[] {"", "p_stand_j", "pd_stand", "p4_stand"}[hero]);
                    check("hero"+hero+" skin", Part(animation,"yyj6") == "tou000"+(hero+1));
                    if (hero == 2) check("Soldier holds D3 in yyj5", Part(animation,"yyj5") == "D3" && Part(animation,"com1") == "shou0003");
                    if (hero == 3) check("Ninja shuriken and stand-specific hand", Part(animation,"com15") == "shoulijian01" && Part(animation,"yyj3") == "koushou0004");
                    int frame = scene.HeroRig.Player.CurrentFrame;
                    game.StepFor(2f/60f);
                    check("hero"+hero+" advances at deterministic60Hz", scene.HeroRig.Player.CurrentFrame == (frame+1)%20);
                    Capture(cam, Path.Combine(output, "hero"+hero+"_next.png"));
                }
                hit = scene.GetControl(10).FullRect.center; scene.Touch(0,hit); scene.Touch(2,hit); game.StepFor(1f);
                check("girl uses short stand component rig with native position/scale", scene.HeroRig.Source == "player_stand_short" && scene.HeroRig.ComponentPlayer.position == new Vector2(-232,-162) && scene.HeroRig.ComponentPlayer.objectScale == 1.5f && !scene.HeroRig.Player.active);
                Capture(cam, Path.Combine(output,"hero4_stand.png"));
                int girlFrame = scene.HeroRig.ComponentPlayer.CurrentFrame;
                game.StepFor(2f/60f);
                check("girl component animation advances at30fps", scene.HeroRig.ComponentPlayer.CurrentFrame == (girlFrame+1)%21);
                Capture(cam, Path.Combine(output,"hero4_next.png"));
                int[] levels = { -1,0,3,4,8,9,13,14,18,19,23,24,28,29,59 };
                int[] hp = { 101,50,50,60,60,70,70,80,80,90,90,101,101,121,121 };
                bool hpBoundaries = true;
                for (int i=0; i<levels.Length; i++) hpBoundaries &= Dc2Data.GirlHp(101,levels[i]) == hp[i];
                check("native Girl HP unsigned bands/double truncation", hpBoundaries);
                game.Profile.level[4] = 0; scene.OnSelectChange(); game.StepFor(0.6f);
                check("unlocked girl HP uses recovered half factor at current level0", scene.heroHpShown == 50);
                Capture(cam, Path.Combine(output,"hero4_unlocked.png"));
                game.Profile.level[4] = -1;
                hit = scene.GetControl(6).FullRect.center; scene.Touch(0,hit); scene.Touch(2,hit); game.StepFor(0.6f);

                hit = scene.GetControl(8).FullRect.center; scene.Touch(0, hit); scene.Touch(2, hit);
                game.StepFor(0.6f);
                check("selecting locked hero2 moves frame to slot1 (290,108)", scene.GetControl(28).FullRect == new Rect(290, 108, 140, 140));
                check("locked hero2 removes stats and keeps this+15 (heroSelected still 0)", Label(scene, 42) == null && scene.GetControl(44) == null && scene.GetControl(58) == null && scene.heroSelected == 0);
                check("locked hero2 hire button (804,580,176,78) frame ui_btn_hiredFrame", scene.GetControl(39) != null && scene.GetControl(39).FrameName == "ui_btn_hiredFrame" && scene.GetControl(39).FullRect == new Rect(804, 580, 176, 78));
                check("locked hero2 info art ui_p3_infoFrame (448,270,504,362)", scene.GetControl(47) != null && scene.GetControl(47).FrameName == "ui_p3_infoFrame" && scene.GetControl(47).FullRect == new Rect(448, 270, 504, 362));
                check("locked hero2 gold strip (434,576,362,70)", scene.GetControl(48) != null && scene.GetControl(48).FullRect == new Rect(434, 576, 362, 70));
                hit = scene.GetControl(39).FullRect.center; scene.Touch(0, hit); scene.Touch(2, hit); game.StepFor(0.1f);
                check("hire with cash 0 opens short-on-cash dialog and does not hire", scene.confirmOpen && game.Profile.level[2] == -1);
                check("hire dialog has native short cash message", Label(scene,900)=="You're short on cash. Want to buy some more?");
                hit = scene.GetControl(901).FullRect.center; scene.Touch(0, hit); scene.Touch(2, hit);
                check("dialog No closes and keeps hero locked", !scene.confirmOpen && game.Profile.level[2] == -1);
                game.Profile.cash = 15000;
                hit = scene.GetControl(39).FullRect.center; scene.Touch(0, hit); scene.Touch(2, hit); game.StepFor(0.6f);
                check("hire accepts equal cash: level 0, cash 0", game.Profile.level[2] == 0 && game.Profile.cash == 0);
                check("after hire stats appear (HP 120, level 1) and hire button gone", scene.GetControl(39) == null && Label(scene, 42) == "120" && Label(scene, 43) == "1" && scene.heroSelected == 2);
                check("gold label updated to 0", Label(scene, 51) == "0");
                hit = scene.GetControl(38).FullRect.center; scene.Touch(0, hit); scene.Touch(2, hit);
                check("back commits the hired selection (playerType 2)", game.MapType == 0 && game.Profile.playerType == 2);
                game.StepFor(0.05f); scene = (SelectScene)game.Current; game.StepFor(1.0f);
                check("map restored after back", scene.GetControl(54) != null && scene.GetControl(64) != null);
                hit = scene.GetControl(54).FullRect.center; scene.Touch(0,hit); scene.Touch(2,hit); game.StepFor(0.6f);
                scene = (SelectScene)game.Current;
                check("reenter Heroes restores saved hero2 cursor and preview", scene.heroCursor == 2 && scene.heroSelected == 2 && scene.HeroRig.Source == "pd_stand");
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
