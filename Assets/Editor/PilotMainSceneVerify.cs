using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{

    [Serializable] public sealed class HudControl { public int id, layer; public string texture; }
    [Serializable] public sealed class HudSet { public int type; public string scene; public HudControl[] controls; }
    [Serializable] public sealed class HudSets { public HudSet[] sets; }
    /// <summary>mainScene missions: bounds, HUD variant, run progress marker and run end (Dev/Evidence/mainScene_decomp_r107.md).</summary>
    public static class PilotMainSceneVerify
    {
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-PILOT-001/mainscene-r1");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            string dir = Path.Combine(Path.GetTempPath(), "dc2_main_verify_" + Guid.NewGuid().ToString("N"));
            int passed = 0, failed = 0; var log = new StringBuilder();
            Action<string, bool> check = (n, v) => { log.AppendLine((v ? "PASS " : "FAIL ") + n); if (v) passed++; else failed++; };
            Dc2Game game = null; Camera cam = null; RenderTexture rt = null; Texture2D img = null;
            try
            {
                cam = Bootstrap.MakeCamera(); game = Dc2Game.Create(dir); game.manualStep = true; game.SkipMissionIntro = true; game.cam = cam;
                rt = new RenderTexture(1024, 768, 24); img = new Texture2D(1024, 768, TextureFormat.RGB24, false);
                Action<string> shot = name => { cam.targetTexture = rt; Bootstrap.RenderAll(cam); RenderTexture.active = rt; img.ReadPixels(new Rect(0, 0, 1024, 768), 0, 0); img.Apply(); File.WriteAllBytes(Path.Combine(output, name), img.EncodeToPNG()); cam.targetTexture = null; RenderTexture.active = null; };
                float S = N.S;
                // ---- run mission ----
                game.LaunchMission = new NativeMapState.Mission { type = 1, count = 4500f, level = 2, background = 1, info = 0 };
                game.LaunchMission.enemies.Add(2);
                game.Profile.playerType = 0;
                game.SwitchScene("gameScene"); game.StepFor(0.02f);
                var scene = (GameScene)game.Current; scene.spawningEnabled = false;
                check("run: mainScene bounds -480 .. num*S", scene.MainSceneMission && scene.BoundLower == -480f && scene.BoundUpper == 9000f);
                check("run HUD: progress line (37) and marker (38) exist, time icon/label do not", scene.GetControl(37) != null && scene.GetControl(38) != null && scene.GetControl(46) == null && scene.GetControl(27) == null && scene.Labels.Count > 0);
                check("run HUD: exp bar is id 40 (mainScene::OnLoad), lifeMode id 47 unused", scene.GetControl(40) != null && scene.GetControl(47) == null);
                check("run HUD: stop icon is id 15 (layer 0); r119m roll 22, change 41, gold 9 (mainScene emulation)", scene.GetControl(15) != null && scene.GetControl(15).FrameName == "icon_stopFrame" && scene.GetControl(22) != null && scene.GetControl(22).FrameName == "ui_btn_rollFrame" && scene.GetControl(41) != null && scene.GetControl(9) != null);
                game.StepFor(0.2f);
                Rect start = N.Layout(290f, 16f, 17f, 15f);
                var marker = scene.GetControl(38);
                check("marker at hero.x<0 clamps t to 0 (x = RectOffset(RectScale(290,..)).x)", marker != null && Mathf.Abs(marker.CurrentRect.x - start.x) < 0.01f);
                scene.heroX = 4500f; game.StepFor(0.1f);
                float expectedX = N.Add32(N.Mul32(N.Div32(N.Mul32(130f, 4500f), 9000f), S), start.x);
                check("marker at half distance: x += S*65", Mathf.Abs(marker.CurrentRect.x - expectedX) < 0.5f);
                shot("run_half.png");
                // lower bound clamp
                scene.heroX = -2000f; game.StepFor(0.1f);
                check("hero clamps at the -480 left bound", scene.heroX >= -480f - 1f && scene.heroX <= -470f);
                // run end: UpdateCamera bb22e threshold is right + S*-180 = 8640 (ARM literal 0xc3340000)
                scene.heroX = 8639f; game.StepFor(0.05f);
                check("hero at 8639 (< right-180*S) does not end the run", !scene.runEnded);
                scene.heroX = 8641f; game.Step(1f / 60f);
                check("run end frame 1: GameClear stands the hero, velocity 0 (mainScene::GameClear 0xb801a)", scene.runEnded && scene.heroVelocityX == 0f && scene.heroControlState == 2);
                game.Step(1f / 60f);
                check("run end frame 2: per-frame UpdateCamera restores walk velocity and state 4 (no once-only guard, bb22e)", scene.heroVelocityX > 0f && scene.heroControlState == 4);
                game.StepFor(0.1f);
                check("hero past right-100*S ends the run (GameClear -> mission complete)", scene.runEnded && scene.result == GameScene.MissionResult.Complete);
                check("run end locks hero control (state 4) and facing right", scene.heroControlState == 4 && !scene.facingLeft);
                float exitStart = scene.heroX;
                game.StepFor(0.5f);
                check("after GameClear the hero keeps walking right at walk speed (no right clamp)", scene.heroX > exitStart + 100f && scene.heroVelocityX > 0f);
                game.StepFor(8f);
                check("hero stops once x > camLeft + 1024 + S*50 (UpdateCamera bb2d2)", scene.heroVelocityX == 0f && scene.heroX > scene.camLeft + 1024f + 100f - 1f);
                shot("run_end.png");
                // first spawn at FRQ
                game.LaunchMission = new NativeMapState.Mission { type = 1, count = 4500f, level = 2, background = 1 };
                game.LaunchMission.enemies.Add(2);
                game.SwitchScene("gameScene"); game.StepFor(0.02f);
                scene = (GameScene)game.Current;
                float frq = Dc2GameData.Dinos()[1].frq;
                int before = scene.dinos.Count;
                game.StepFor(frq - 0.3f);
                bool none = scene.dinos.Count == before;
                game.StepFor(0.6f);
                check("mainScene StartGame_N: first spawn at the kind's FRQ (" + frq + " s), not 5*rand", none && scene.dinos.Count == before + 1);
                // lifeMode HUD unchanged
                game.LaunchMission = new NativeMapState.Mission { type = 3, count = 10, level = 1, background = 0 };
                game.SwitchScene("gameScene"); game.StepFor(0.02f);
                scene = (GameScene)game.Current;
                check("r119m kill mission HUD: stop icon is id 15 layer 0 (lifeModeScene::OnLoad 0xe7e6a), change 48, roll 28, gold 43", scene.GetControl(15) != null && scene.GetControl(48) != null && scene.GetControl(28) != null && scene.GetControl(43) != null && scene.GetControl(30) == null && scene.GetControl(22) == null);
                check("kill mission with bg 0 has no scenery", scene.SceneryCount == 0);
                {   // HUD parity with the native control list of mainScene::OnLoad (bounded emulation, Dev/Evidence/mainscene_hud_emulated_r118.json): every native texture must exist in the port HUD
                    var hud = JsonUtility.FromJson<HudSets>(File.ReadAllText(Path.Combine(Application.dataPath, "../../Dev/Evidence/mainscene_hud_emulated_r118.json")));
                    foreach (var set in hud.sets)
                    {
                        game.LaunchMission = new NativeMapState.Mission { type = set.type, count = set.type == 1 || set.type == 5 ? 1500f : 20f, level = 1, background = 1 };
                        game.LaunchMission.enemies.Add(2);
                        game.SwitchScene("gameScene"); game.StepFor(0.1f);
                        scene = (GameScene)game.Current;
                        var have = new System.Collections.Generic.Dictionary<string, int>();
                        foreach (var c in scene.Controls) { string f = c.FrameName; if (f != null) { int n; have.TryGetValue(f, out n); have[f] = n + 1; } }
                        var need = new System.Collections.Generic.Dictionary<string, int>();
                        foreach (var c in set.controls) if (!string.IsNullOrEmpty(c.texture)) { int n; need.TryGetValue(c.texture, out n); need[c.texture] = n + 1; }
                        var missing = new StringBuilder();
                        foreach (var kv in need)
                        {
                            string frame = kv.Key.EndsWith("Frame") ? kv.Key : kv.Key + "Frame";
                            int n; have.TryGetValue(frame, out n); if (n == 0) have.TryGetValue(kv.Key, out n);
                            if (n < 1) missing.Append(kv.Key + " (need " + kv.Value + ", have " + n + ") ");
                        }
                        log.AppendLine("INFO HUD parity type " + set.type + " missing: " + (missing.Length == 0 ? "none" : missing.ToString()));
                        string tolerated = missing.ToString().Replace("whiteFrame (need 1, have 0) ", "");
                        // The port prunes the clock icon (ui_game_time_icon) for non-timed and the kill icon (Lkill_h) for non-kill lifeMode missions (user description: kill = counter, time = clock);
                        // whether native hides them per type was not recovered (OnLoad creates both for every type; no DeleteControl / GetControl call by id 44/46 in OnLoad or MissCompMove / GameClear / LevelLeave).
                        if (set.scene == "lifeModeScene" && set.type != 2 && set.type != 7) tolerated = tolerated.Replace("ui_game_time_iconFrame (need 1, have 0) ", "");
                        if (set.scene == "lifeModeScene" && set.type != 3 && set.type != 4) tolerated = tolerated.Replace("Lkill_hFrame (need 1, have 0) ", "");
                        check(set.scene + " type " + set.type + " HUD: every native OnLoad texture exists in the port HUD (cover and per-type clock/kill icons excepted)", tolerated.Trim().Length == 0);
                    }
                }
                // ---- mainScene::InitBackGround 0x9aff2 scenery (r118): expectations come from direct emulation of the ARM (Dev/Tools/explore_main_bg.py), not from the port's table ----
                Func<int, int, float, int> sceneryObjects = (type, bg, num) =>
                {
                    game.LaunchMission = new NativeMapState.Mission { type = type, count = num, level = 1, background = bg };
                    game.LaunchMission.enemies.Add(2);
                    game.SwitchScene("gameScene"); game.StepFor(0.02f);
                    scene = (GameScene)game.Current; scene.spawningEnabled = false;
                    return scene.SceneryCount;
                };
                int runObjs = (int)sceneryObjects(1, 1, 4500f);   // total 9000 -> int(9380/960)+1 = 10 groups of 4 (patterns 0..2) or 5 (pattern 3, rounding quirk only) objects
                check("run bg 1: 10 groups of 4 objects", runObjs >= 40 && runObjs <= 50);
                bool framesOk = true, layerOk = true; float minX = 1e9f, maxX = -1e9f;
                for (int i = 0; i < scene.SceneryCount; i++)
                {
                    var r = scene.SceneryRenderer(i);
                    framesOk &= r.sprite != null && r.sprite.name.Length > 0 && r.gameObject.name.StartsWith("scenery_beijing2_");
                    layerOk &= r.transform.parent.name == "native_render_layer_2";
                    float x = scene.SceneryPosition(i).x; minX = Mathf.Min(minX, x); maxX = Mathf.Max(maxX, x);
                }
                check("run bg 1 scenery: beijing2_* frames loaded, all on render layer 2", framesOk && layerOk);
                check("run bg 1 scenery spans the run (first group near -344..288, last group beyond 8000)", minX < 0f && maxX > 8000f);
                game.StepFor(0.1f); shot("run_scenery_bg1.png");
                check("egg bg 3, total 4000: groups A,B,C,B = 4+3+5+3 objects (B variant 1 is unreachable: RandomRange(0,1) = 0)", sceneryObjects(5, 3, 2000f) == 15);
                check("egg bg 4, total 2000: 2 groups of 2 objects", sceneryObjects(5, 4, 1000f) == 4);
                check("run bg 5, total 2000: 2 groups of 2 objects", sceneryObjects(1, 5, 1000f) == 4);
                check("bg 0 and 2 have no scenery (no InitBackGround call)", sceneryObjects(1, 0, 1000f) == 0 && sceneryObjects(1, 2, 1000f) == 0);
                int killObjs = sceneryObjects(3, 1, 10f);   // lifeModeScene::InitBackGround 0xd6a0e, scene+140 = 50000: int(50380/960)+1 = 53 groups of 4 (5 only through the rounding quirk)
                check("kill bg 1: lifeMode scenery, 53 groups of 4 objects (scene+140 = 50000)", killObjs >= 212 && killObjs <= 265);
                game.StepFor(0.1f); shot("kill_scenery_bg1.png");
                {   // pixel assertion: the kill-counter icon (id 44) must change pixels inside its rect (layer-0 HUD art was hidden by world sprites before UiControl.UiOrderBase)
                    var kc = scene.GetControl(44); var srK = kc.GetComponent<SpriteRenderer>();
                    Func<Color32[]> grab = () => { cam.targetTexture = rt; Bootstrap.RenderAll(cam); RenderTexture.active = rt; img.ReadPixels(new Rect(0, 0, 1024, 768), 0, 0); img.Apply(); return img.GetPixels32(); };
                    var withIcon = grab(); srK.enabled = false; var withoutIcon = grab(); srK.enabled = true;
                    Rect kr = kc.CurrentRect; int diff = 0;
                    for (int yy = (int)kr.y; yy < (int)(kr.y + kr.height); yy++) for (int xx = (int)kr.x; xx < (int)(kr.x + kr.width); xx++)
                    { int idx = (768 - 1 - yy) * 1024 + xx; if (Mathf.Abs(withIcon[idx].r - withoutIcon[idx].r) + Mathf.Abs(withIcon[idx].g - withoutIcon[idx].g) + Mathf.Abs(withIcon[idx].b - withoutIcon[idx].b) > 30) diff++; }
                    check("the kill-counter icon (id 44) is actually drawn (pixel difference inside its rect)", diff > 200);
                }
                check("arena (variant 7) and boss (variant 8) have no scenery", sceneryObjects(7, 1, 10f) == 0 && sceneryObjects(8, 3, 10f) == 0);
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
