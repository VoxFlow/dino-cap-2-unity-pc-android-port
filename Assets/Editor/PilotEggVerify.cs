using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>mainScene egg mission (type 5): pickup, carry/roll lock, delivery, count label, truck colours, EggFull, broken egg (Dev/Evidence/egg_decomp_r108.md).</summary>
    public static class PilotEggVerify
    {
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-PILOT-001/egg-r1");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            string dir = Path.Combine(Path.GetTempPath(), "dc2_egg_verify_" + Guid.NewGuid().ToString("N"));
            int passed = 0, failed = 0; var log = new StringBuilder();
            Action<string, bool> check = (n, v) => { log.AppendLine((v ? "PASS " : "FAIL ") + n); if (v) passed++; else failed++; };
            Dc2Game game = null; Camera cam = null; RenderTexture rt = null; Texture2D img = null;
            try
            {
                cam = Bootstrap.MakeCamera(); game = Dc2Game.Create(dir); game.manualStep = true; game.SkipMissionIntro = true; game.cam = cam;
                rt = new RenderTexture(1024, 768, 24); img = new Texture2D(1024, 768, TextureFormat.RGB24, false);
                Action<string> shot = name => { cam.targetTexture = rt; Bootstrap.RenderAll(cam); RenderTexture.active = rt; img.ReadPixels(new Rect(0, 0, 1024, 768), 0, 0); img.Apply(); File.WriteAllBytes(Path.Combine(output, name), img.EncodeToPNG()); cam.targetTexture = null; RenderTexture.active = null; };
                game.LaunchMission = new NativeMapState.Mission { type = 5, count = 1200f, level = 1, background = 1, info = 3 };
                game.LaunchMission.enemies.Add(1);
                game.Profile.playerType = 0;
                game.SwitchScene("gameScene"); game.StepFor(0.05f);
                var scene = (GameScene)game.Current; scene.spawningEnabled = false;
                check("egg: bounds -480 .. num*S = 2400", scene.MainSceneMission && scene.BoundLower == -480f && scene.BoundUpper == 2400f);
                check("egg HUD: exp bar (id 40, sd_bai) survives next to the egg icon (id 47): the lifeMode id 47 collided", scene.GetControl(40) != null && scene.GetControl(40).gameObject != null);
                check("egg HUD: icon id 47 and label id 48 exist, no progress line/time", scene.GetControl(47) != null && scene.GetControl(37) == null && scene.GetControl(27) == null);
                check("eggs to deliver = Map.xml info (3); label reads 0/3", scene.EggRemaining == 3 && scene.EggDelivered == 0 && HasLabel(scene, "0/3"));
                check("initEgg: trigger at right + S*-50 = 2300", scene.EggPresent && Mathf.Abs(scene.EggX - 2300f) < 0.01f);
                check("nest sprite (egg_woFrame) and truck exist", scene.NestVisible && scene.TruckColorAlpha("e1") == 0);
                game.StepFor(0.05f);
                shot("egg_start.png");
                {   // r119g (user: the truck covers the ground and props and is behind the hero): truck root in render layer 10 below every actor group (>= 200).
                    // Edit-mode captures cannot show it: component (mesh) renderers sort behind the background in this batch capture path (bosses too), so this is a structural check.
                    Transform truckT = null; foreach (var tr in scene.GetComponentsInChildren<Transform>()) if (tr.name == "truck") truckT = tr;
                    var tg = truckT != null ? truckT.GetComponent<UnityEngine.Rendering.SortingGroup>() : null;
                    check("r119h egg truck: custom draw on native layer 12 after its sprites (parent " + (truckT != null ? truckT.parent.name : "-") + ", order " + (tg != null ? tg.sortingOrder : -1) + ")",
                          truckT != null && truckT.parent.name == "native_render_layer_12" && tg != null && tg.sortingOrder == NativeDrawOrder.CustomDraw);
                }
                scene.heroX = 2300f - 61f; game.StepFor(0.05f);
                check("no pickup at 61 px", !scene.EggCarrying && scene.EggPresent);
                scene.heroX = 2300f - 59f; game.StepFor(0.05f);
                check("pickup within 60 px: carrying, egg object deleted", scene.EggCarrying && !scene.EggPresent);
                check("carrying masks the roll button (id 39 shown: UnRollDown 0x94c6c SetVisible(1))", scene.GetControl(39) != null && scene.GetControl(39).visible);
                float bw1; string bref1 = scene.EggBodyRefForTest(out bw1);
                check("getEgg: yyj4 body image becomes shenti0001_egg (width x2)", bref1 == "shenti0001_egg" && bw1 > 0f);
                game.StepFor(0.3f);
                float bw2; string bref2 = scene.EggBodyRefForTest(out bw2);
                check("body swap persists across frames without compounding", bref2 == "shenti0001_egg" && Mathf.Abs(bw2 - bw1) < 0.001f);
                shot("egg_carry.png");
                scene.heroX = -170f; game.StepFor(0.05f);
                check("no delivery at hero.x = -170 (> -180)", scene.EggCarrying && scene.EggDelivered == 0);
                scene.heroX = -181f; game.StepFor(0.05f);
                check("delivery at hero.x < -180: 1 delivered, 2 remaining, label 1/3", !scene.EggCarrying && scene.EggDelivered == 1 && scene.EggRemaining == 2 && HasLabel(scene, "1/3"));
                check("delivery: new egg at the nest, roll mask hidden (CanRollDown 0x94c2a SetVisible(0); r118z4: not deleted)", scene.EggPresent && scene.GetControl(39) != null && !scene.GetControl(39).visible);
                check("AddEggOnCar(1): e1 visible, e2 hidden", scene.TruckColorAlpha("e1") == 255 && scene.TruckColorAlpha("e2") == 0);
                shot("egg_delivered1.png");
                scene.heroX = scene.EggX; game.StepFor(0.05f);
                check("second pickup", scene.EggCarrying);
                int broken = scene.EggBrokenCount;
                scene.EggHitForTest();
                { float bw3; string bref3 = scene.EggBodyRefForTest(out bw3); check("getDownEgg restores the body image (shenti0001, width x0.5 of the swapped value)", bref3 == "shenti0001" && Mathf.Abs(bw3 * 2f - bw1) < 0.01f); }
                check("hit while carrying: egg dropped, BrokenEgg effect, egg respawns, counts unchanged", !scene.EggCarrying && scene.EggBrokenCount == broken + 1 && scene.EggPresent && scene.EggDelivered == 1 && scene.EggRemaining == 2);
                game.StepFor(0.05f); shot("egg_broken.png");
                for (int k = 0; k < 2; k++)
                {
                    scene.heroX = scene.EggX; game.StepFor(0.05f);
                    scene.heroX = -181f; game.StepFor(0.05f);
                }
                check("all delivered: EggFull started, truck switches to cheMove, e1..e3 visible", scene.EggDelivered == 3 && scene.EggRemaining == 0 && scene.EggFullStarted && scene.TruckPlaysMove && scene.TruckColorAlpha("e3") == 255 && scene.TruckColorAlpha("e4") == 0);
                check("EggFull runs StopCreateEnemy (kind 1..13 spawn slots killed) and does not complete yet", scene.LiveSpawnSlots(1, 13) == 0 && scene.result == GameScene.MissionResult.Running);
                game.StepFor(0.5f); shot("egg_truck_moving.png");
                float t = 0f; while (scene.result == GameScene.MissionResult.Running && t < 20f) { game.StepFor(0.1f); t += 0.1f; }
                log.AppendLine("INFO truck move finished after " + t.ToString("0.0") + " s");
                check("truck animation end (effectClassN kind 2) -> GameClear -> mission complete", scene.result == GameScene.MissionResult.Complete);
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

        static bool HasLabel(GameScene scene, string text)
        {
            foreach (var l in scene.Labels) if (l.id == 48 && l.Text == text) return true;
            return false;
        }
    }
}
