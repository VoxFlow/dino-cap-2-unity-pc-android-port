using System;
using System.IO;
using System.Text;
using UnityEngine;
namespace Dc2Pilot.EditorTools
{
    /// <summary>r118z4: native UI touch consumption (dc2.ui.image_touch_consume.r118z4). UIManager::HandleUITouch 0x18102e tests only the enable flag;
    /// UIImage::HandleTouch 0x17f1ac / UILabel::HandleTouch 0x17fd98 return UIControl::PtInRect 0x17e70c (half-open, plus the clip rect), so an enabled
    /// plain image or label swallows every touch phase inside its rect, visible or not, and Scene::Update 0x175f22 then skips the scene OnTouchEvent.
    /// The flags at the native sites come from Dev/Tools/scan_ui_control_enable.py.</summary>
    public static class PilotUiTouchVerify
    {
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-PILOT-001/ui-touch-r118z4"); var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1]; Directory.CreateDirectory(output);
            string dir = Path.Combine(Path.GetTempPath(), "dc2_uitouch_" + Guid.NewGuid().ToString("N"));
            int passed = 0, failed = 0; var log = new StringBuilder(); Action<string, bool> check = (n, v) => { if (v) passed++; else failed++; log.AppendLine((v ? "PASS " : "FAIL ") + n); };
            Dc2Game game = null; Camera camera = null;
            try
            {
                camera = Bootstrap.MakeCamera(); game = Dc2Game.Create(dir); game.cam = camera; game.manualStep = true; game.SkipMissionIntro = true;
                game.LaunchMission = new NativeMapState.Mission { type = 3, count = 99, background = 7 }; game.LaunchMission.enemies.Add(1);
                game.Profile.playerType = 0; game.SwitchScene("gameScene"); game.Step(0f); game.Step(1f / 60f);
                var scene = (GameScene)game.Current; scene.spawningEnabled = false; scene.ignorePlayer = true;
                Atlas.EnsureWhite();
                var r = new Rect(100f, 100f, 50f, 50f);
                var button = scene.CreateControl(990, 1); button.SetTexture("whiteFrame"); button.SetRect(r);
                var img = scene.CreateControl(991, 2); img.SetTexture("whiteFrame"); img.SetRect(r); img.consumesTouch = true;
                Func<Vector2, int> sceneTouches = p => { int n0 = scene.DebugSceneTouches; scene.Touch(0, p, 0); scene.Touch(2, p, 0); return scene.DebugSceneTouches - n0; };
                Func<bool> buttonPressed = () => button.CurrentRect != button.FullRect;
                check("enabled plain image consumes a touch inside: no scene touch", sceneTouches(new Vector2(110f, 110f)) == 0);
                scene.Touch(0, new Vector2(110f, 110f), 0); check("the image above swallows the press, the button below is not pressed", !buttonPressed()); scene.Touch(2, new Vector2(110f, 110f), 0);
                check("half-open: the left/top edge (100,100) is inside the image (consumed)", sceneTouches(new Vector2(100f, 100f)) == 0);
                check("half-open: x = left+w (150) is outside the image; the strict button rect also excludes it -> scene touch", sceneTouches(new Vector2(150f, 110f)) == 2);
                img.SetVisible(false);
                check("an invisible enabled image still consumes (HandleUITouch tests enable only)", sceneTouches(new Vector2(120f, 120f)) == 0);
                img.SetVisible(true);
                img.SetClip(new Rect(100f, 100f, 10f, 50f));
                scene.Touch(0, new Vector2(120f, 110f), 0);
                check("outside the clip rect the image does not consume: the button below gets the press", buttonPressed());
                scene.Touch(2, new Vector2(120f, 110f), 0);
                img.ClearClip(); img.touchable = false;
                scene.Touch(0, new Vector2(120f, 110f), 0);
                check("a disabled image (SetEnable 0) does not consume: the button gets the press", buttonPressed());
                scene.Touch(2, new Vector2(120f, 110f), 0);
                scene.DeleteControl(991); scene.DeleteControl(990);
                var label = scene.CreateLabel(992, 3, 30); label.SetRect(new Rect(300f, 300f, 40f, 20f));
                check("a label without the native-enabled flag passes the touch to the scene", sceneTouches(new Vector2(310f, 305f)) == 2);
                label.consumesTouch = true;
                check("an enabled label consumes a touch inside its rect", sceneTouches(new Vector2(310f, 305f)) == 0);
                check("label half-open: x = left+w is outside", sceneTouches(new Vector2(340f, 305f)) == 2);
                scene.DeleteLabel(992);

                // native sites (scan_ui_control_enable.py): menuScene::OnLoad 0xcc2a6 logo id 35 enabled
                game.SwitchScene("menuScene"); game.Step(0f);
                var logo = game.Current.GetControl(35);
                check("menuScene splash logo (id 35, layer 5) is a native-enabled image: consumes", logo != null && logo.consumesTouch && logo.touchable);
                if (logo != null) { int n0 = game.Current.DebugSceneTouches; game.Current.Touch(0, new Vector2(512f, 384f), 0); check("splash logo swallows the touch (no menuScene OnTouchEvent)", game.Current.DebugSceneTouches == n0); }
            }
            catch (Exception e) { failed++; log.AppendLine(e.ToString()); }
            finally
            {
                if (game != null) UnityEngine.Object.DestroyImmediate(game.gameObject); if (camera != null) UnityEngine.Object.DestroyImmediate(camera.gameObject);
                if (Directory.Exists(dir)) Directory.Delete(dir, true); log.AppendLine("SUMMARY passes=" + passed + " fails=" + failed);
                File.WriteAllText(Path.Combine(output, "verify.txt"), log.ToString()); Debug.Log(log.ToString()); PilotExit.Exit(failed == 0 ? 0 : 1);
            }
        }
    }
}
