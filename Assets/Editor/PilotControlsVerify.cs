using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>Touch rects, roll (dash/cooldown/invulnerable), melee, weapon swap, level-up, pause menu.</summary>
    public static class PilotControlsVerify
    {
        static Vector2 Ctr(Rect r) { return r.center; }

        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-PILOT-001/controls-r1");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            string dir = Path.Combine(Path.GetTempPath(), "dc2_controls_verify_" + Guid.NewGuid().ToString("N"));
            int passed = 0, failed = 0; var log = new StringBuilder();
            Action<string, bool> check = (n, v) => { log.AppendLine((v ? "PASS " : "FAIL ") + n); if (v) passed++; else failed++; };
            Dc2Game game = null; Camera cam = null; RenderTexture rt = null; Texture2D img = null;
            try
            {
                cam = Bootstrap.MakeCamera(); game = Dc2Game.Create(dir); game.manualStep = true; game.SkipMissionIntro = true; game.cam = cam;
                game.LaunchMission = new NativeMapState.Mission { type = 3, count = 20, background = 0 }; // explicit test fixture
                rt = new RenderTexture(1024, 768, 24); img = new Texture2D(1024, 768, TextureFormat.RGB24, false);
                Action<string> shot = name =>
                {
                    cam.targetTexture = rt; Bootstrap.RenderAll(cam); RenderTexture.active = rt; img.ReadPixels(new Rect(0, 0, 1024, 768), 0, 0); img.Apply();
                    File.WriteAllBytes(Path.Combine(output, name), img.EncodeToPNG());
                    cam.targetTexture = null; RenderTexture.active = null;
                };
                game.Profile.playerType = 0; game.Profile.cash = 5000;
                game.Profile.weaponOwned[1] = 0;
                GameScene.ResetTouchIdsForTest(); game.SwitchScene("gameScene"); game.StepFor(0.02f);
                var scene = (GameScene)game.Current; scene.spawningEnabled = false; scene.missionType = 9; scene.DebugSpawnModeRoll = () => 0; // edge-mode fixture
                game.StepFor(0.2f);
                shot("controls_hud.png");

                scene.Touch(0, Ctr(GameScene.RightRect)); float x0 = scene.heroX; game.StepFor(0.5f); scene.Touch(2, Ctr(GameScene.RightRect));
                check("right arrow rect walks right at 300 px/s", Mathf.Abs(scene.heroX - x0 - 150f) < 8f);
                scene.Touch(0, Ctr(GameScene.LeftRect)); game.StepFor(0.2f); scene.Touch(2, Ctr(GameScene.LeftRect));
                check("left arrow rect faces left", scene.facingLeft);
                // Triniti2D::Rect::PtInRect 0x16fc4 excludes every edge (r118z4): a press on the arrow's left/top edge is not inside, one pixel in is
                var r0 = GameScene.LeftRect;
                scene.Touch(0, new Vector2(r0.x, r0.y + 50f)); bool onEdge = scene.leftDown; scene.Touch(2, new Vector2(r0.x, r0.y + 50f));
                scene.Touch(0, new Vector2(r0.x + 1f, r0.y + 50f)); bool inside = scene.leftDown; scene.Touch(2, new Vector2(r0.x + 1f, r0.y + 50f));
                check("a press exactly on the left arrow's left edge is outside (strict PtInRect)", !onEdge);
                check("a press one pixel inside the left edge holds the arrow", inside);
                scene.Touch(0, new Vector2(500, 50)); scene.Touch(2, new Vector2(500, 50));
                check("a tap outside every rect does nothing", !scene.paused && !scene.rolling);

                scene.heroX = 1000f; // explicit interior-world fixture: test dash distance without hitting native lower bound -240.
                float rx = scene.heroX;
                scene.Touch(0, Ctr(GameScene.RollRect)); scene.Touch(2, Ctr(GameScene.RollRect));
                check("roll starts (state 7)", scene.rolling && scene.rollCooldown);
                var d1 = scene.CreateEnemy(1); d1.x = scene.heroX - 40f; d1.faceRight = true; d1.state = 0;
                float hp0 = scene.playerHp;
                game.StepFor(0.15f);
                check("rolling is invulnerable (no BeAttack)", scene.playerHp == hp0);
                for (int f = 0; f < 90 && scene.rolling; f++) game.Step(1f / 60f);
                check("roll ended and dashed left by more than 200 px", !scene.rolling && rx - scene.heroX > 200f);
                scene.Touch(0, Ctr(GameScene.RollRect)); scene.Touch(2, Ctr(GameScene.RollRect));
                check("cooldown blocks a second roll for 3 s", !scene.rolling);
                shot("controls_roll_cooldown.png");
                game.StepFor(3.2f);
                scene.Touch(0, Ctr(GameScene.RollRect)); scene.Touch(2, Ctr(GameScene.RollRect));
                check("roll available again after cooldown", scene.rolling);
                for (int f = 0; f < 120 && scene.rolling; f++) game.Step(1f / 60f);
                foreach (var d in scene.dinos) { d.dead = true; d.removed = true; }
                game.StepFor(0.1f);

                scene.facingLeft = false; var d2 = scene.CreateEnemy(1); d2.x = scene.heroX + 90f; d2.faceRight = false; d2.state = 0; d2.hp = 3f;
                float hpm = d2.hp;
                scene.Touch(0, Ctr(GameScene.HitRect));
                game.StepFor(0.8f); scene.Touch(2, Ctr(GameScene.HitRect));
                check("melee (hit button) damages or kills the nearby dino", d2.dead || d2.hp < hpm);
                shot("controls_melee.png");
                game.StepFor(3f);

                int slot0 = scene.weaponSlot;
                scene.Touch(0, Ctr(GameScene.ChangeRect)); scene.Touch(2, Ctr(GameScene.ChangeRect));
                check("swap moves to the next owned slot (1)", scene.weaponSlot == 1 && slot0 == 0);
                scene.Touch(0, Ctr(GameScene.ChangeRect)); scene.Touch(2, Ctr(GameScene.ChangeRect));
                check("swap skips locked weapons and wraps to slot 0", scene.weaponSlot == 0);
                int clicks0 = game.Audio.RequestedSounds.FindAll(s => s == "Audio_Button1Audio").Count;
                scene.Touch(0, Ctr(GameScene.ChangeRect)); scene.Touch(2, Ctr(GameScene.ChangeRect));
                check("swap release plays Audio_Button1Audio (0xd1f30)", game.Audio.RequestedSounds.FindAll(s => s == "Audio_Button1Audio").Count == clicks0 + 1);
                scene.Touch(0, Ctr(GameScene.ChangeRect)); scene.Touch(2, Ctr(GameScene.ChangeRect));

                // PC keyboard adapter: each action point lies inside its native touch rect (PtInRect excludes edges)
                check("pc keys: action points inside their touch rects",
                    GameScene.NativeTouchInside(GameScene.LeftRect, PcPlatform.ActionPoint(0)) && GameScene.NativeTouchInside(GameScene.RightRect, PcPlatform.ActionPoint(1)) &&
                    GameScene.NativeTouchInside(GameScene.HitRect, PcPlatform.ActionPoint(2)) && GameScene.NativeTouchInside(GameScene.RollRect, PcPlatform.ActionPoint(3)) &&
                    GameScene.NativeTouchInside(GameScene.FireRect, PcPlatform.ActionPoint(4)) && GameScene.NativeTouchInside(GameScene.ChangeRect, PcPlatform.ActionPoint(5)) &&
                    GameScene.NativeTouchInside(GameScene.StopRect, PcPlatform.ActionPoint(6)));
                scene.Touch(0, PcPlatform.ActionPoint(4), 104); game.Step(1f / 60f);
                check("pc keys: fire key finger holds fire", scene.fireDown);
                scene.Touch(2, PcPlatform.ActionPoint(4), 104); game.StepFor(0.5f);
                check("pc keys: fire key release clears fire", !scene.fireDown);

                // multi-touch (lifeModeScene::OnTouchEvent 0xd15e4): per-finger control ids
                scene.Touch(0, Ctr(GameScene.RightRect), 1);
                scene.Touch(0, Ctr(GameScene.FireRect), 2);
                check("multitouch: finger 1 right + finger 2 fire held together", scene.rightDown && scene.fireDown);
                scene.Touch(2, Ctr(GameScene.FireRect), 2);
                check("multitouch: releasing the fire finger keeps the right arrow held", scene.rightDown && !scene.fireDown);
                scene.Touch(1, Ctr(GameScene.LeftRect), 2);
                check("multitouch: a non-arrow finger moving onto the left arrow does nothing", scene.rightDown && !scene.leftDown);
                scene.Touch(1, Ctr(GameScene.LeftRect), 1);
                check("multitouch: the arrow finger slides right -> left (0xd1c4e)", scene.leftDown && !scene.rightDown);
                scene.Touch(2, Ctr(GameScene.HitRect), 3);
                check("multitouch: an unknown finger's release changes nothing", scene.leftDown);
                scene.Touch(2, new Vector2(500, 50), 1);
                check("multitouch: the arrow finger's release anywhere clears its arrow", !scene.leftDown && !scene.rightDown);
                game.StepFor(0.5f);

                int lv0 = game.Profile.level[0];
                game.Profile.exp[0] = scene.ExpThreshold - 1;
                var d3 = scene.CreateEnemy(1); d3.x = scene.heroX + 150f; d3.faceRight = false; d3.hp = 0.1f; d3.exp = 5;
                scene.playerHp = 10f;
                scene.FireDown(); for (int f = 0; f < 240 && !d3.dead; f++) game.Step(1f / 60f); scene.FireUp();
                check("exp over the threshold raises the level", game.Profile.level[0] == lv0 + 1 && scene.levelUps == 1);
                check("level-up restores HP to full", scene.playerHp == scene.playerMaxHp);
                check("level-up starts the native effect objects (LevelUpEffect 0xd0024)", scene.LevelUpEffectActive);
                game.StepFor(0.35f); shot("controls_levelup.png");
                game.StepFor(4f);
                check("level-up effects disappear after their animations end", !scene.LevelUpEffectActive);

                var dd = scene.CreateEnemy(1); dd.x = scene.heroX + 600f; dd.faceRight = false; dd.state = 0;
                scene.Touch(0, Ctr(GameScene.StopRect)); scene.Touch(2, Ctr(GameScene.StopRect));
                check("pause icon opens the pause menu", scene.paused && scene.GetControl(66) != null && scene.GetControl(67) != null && scene.GetControl(68) != null);
                game.StepFor(0.6f);
                float px = dd.x; game.StepFor(1.0f);
                check("world frozen while paused (SetTimeFactorEx 0)", dd.x == px);
                shot("controls_pause.png");
                var snd = scene.GetControl(64);
                scene.Touch(0, snd.FullRect.center); scene.Touch(2, snd.FullRect.center);
                check("sound toggle flips g_fx and its frame", game.Profile.fx == 0 && scene.GetControl(64).FrameName == "op_off1Frame");
                var resume = scene.GetControl(66);
                scene.Touch(0, resume.FullRect.center); scene.Touch(2, resume.FullRect.center);
                check("resume click starts pauseCtrl::Leave (still paused, children sliding out)", scene.paused && scene.GetControl(66) != null); game.StepFor(0.1f); check("still paused 0.1 s into the 0.25 s slide-out", scene.paused); game.StepFor(0.3f);
                check("child 0 move end (event 6) unpauses and removes the menu", !scene.paused && scene.GetControl(66) == null);
                game.StepFor(0.5f);
                check("world runs again after resume", dd.x != px);
                scene.OnSystemEvent(1); check("system event 1 does nothing", !scene.paused);
                scene.OnSystemEvent(0); check("system event 0 in state 4 opens the pause menu (OnSystemEvent 0x9636a)", scene.paused && scene.GetControl(66) != null);
                scene.Touch(0, Ctr(GameScene.StopRect)); scene.Touch(2, Ctr(GameScene.StopRect));
                game.StepFor(0.6f);
                var map = scene.GetControl(68);
                scene.Touch(0, map.FullRect.center); scene.Touch(2, map.FullRect.center);
                check("MAP leaves to the main menu (m_map_type 0)", game.MapType == 0);

                // GameClear 0xf0ff8: hero Stand, +657 = 0, held flags cleared except HitDown; the scene stays in state 4 (touches still dispatched)
                game.StepFor(0.1f); GameScene.ResetTouchIdsForTest(); game.SwitchScene("gameScene"); game.StepFor(0.02f);
                var gc = (GameScene)game.Current; gc.spawningEnabled = false; gc.DebugSpawnModeRoll = () => 0; gc.heroX = 1000f; game.StepFor(0.2f);
                gc.Touch(0, Ctr(GameScene.RightRect), 1); game.StepFor(0.05f); gc.hitHeld = true;   // g_b_HitDown fixture (the life-mode melee start itself clears it)
                var near = gc.CreateEnemy(1); near.x = gc.heroX + 40f; near.faceRight = false; near.state = 0;
                gc.Complete();
                check("GameClear: hero input disabled (+657 = 0) and right/fire flags cleared, hit flag kept", !gc.heroInputEnabled && !gc.rightDown && !gc.fireDown && gc.hitHeld);
                // r118z4: touches checked while the GameClear band (id 20, 0.2 + 0.05 s) still moves: state 4 and no full-screen image yet
                float hx = gc.heroX; game.StepFor(0.1f);
                gc.Touch(0, Ctr(GameScene.LeftRect), 3);
                check("GameClear: the scene still takes touches (state 4) but the hero does not move", gc.leftDown && gc.heroX == hx && gc.GetControl(11) == null);
                gc.Touch(2, Ctr(GameScene.LeftRect), 3);
                gc.Touch(0, Ctr(GameScene.StopRect), 4); gc.Touch(2, Ctr(GameScene.StopRect), 4);
                check("pause still opens after a win (scene state stays 4 after GameClear)", gc.paused);
                gc.SetTimer("DieEnd", 0.1f);
                game.StepFor(1.0f);
                check("scene timers are frozen while paused (Timer::Update gets dt * time factor)", game.CurrentName == "gameScene" && game.Current == gc);

                // r118z4: 1 s after GameClear (fresh scene, not paused) - no movement / attack; the band end runs MissCompEnter (state 5) and lifeModeScene::OnUpdate
                // creates image 11 (missionfailbgFrame, layer 3, rect from y 32, enabled: scan_ui_control_enable.py 0xd47f6), which swallows scene touches (UIImage::HandleTouch 0x17f1ac)
                game.StepFor(0.1f); GameScene.ResetTouchIdsForTest(); game.SwitchScene("gameScene"); game.StepFor(0.02f);
                var gc2 = (GameScene)game.Current; gc2.spawningEnabled = false; gc2.DebugSpawnModeRoll = () => 0; gc2.heroX = 1000f; game.StepFor(0.2f);
                var near2 = gc2.CreateEnemy(1); near2.x = gc2.heroX + 40f; near2.faceRight = false; near2.state = 0;
                gc2.Complete();
                float hx2 = gc2.heroX; float hpc2 = gc2.playerHp; game.StepFor(1.0f);
                check("GameClear: no movement and no dinosaur attack afterwards (CheckAttack +657 gate)", gc2.heroX == hx2 && gc2.playerHp == hpc2);
                var cb = gc2.GetControl(11);
                gc2.Touch(0, Ctr(GameScene.LeftRect), 3);
                check("complete screen (state 5): enabled image 11 swallows the left-button touch (no scene OnTouchEvent)", cb != null && cb.consumesTouch && !gc2.leftDown);
                gc2.Touch(2, Ctr(GameScene.LeftRect), 3);
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
