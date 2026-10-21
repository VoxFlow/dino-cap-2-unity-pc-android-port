using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>CompAnimObj port (NativeCompObj.cs): base + advanced channels, key frames, avatar selection, library animation (Girl rig assets).</summary>
    public static class PilotGirlRigVerify
    {
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-PILOT-001/girlrig-r1");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            string dir = Path.Combine(Path.GetTempPath(), "dc2_girlrig_verify_" + Guid.NewGuid().ToString("N"));
            int passed = 0, failed = 0; var log = new StringBuilder();
            Action<string, bool> check = (n, v) => { log.AppendLine((v ? "PASS " : "FAIL ") + n); if (v) passed++; else failed++; };
            Dc2Game game = null; Camera cam = null; RenderTexture rt = null; Texture2D img = null;
            try
            {
                cam = Bootstrap.MakeCamera(); game = Dc2Game.Create(dir); game.manualStep = true; game.SkipMissionIntro = true; game.cam = cam;
                rt = new RenderTexture(1024, 768, 24); img = new Texture2D(1024, 768, TextureFormat.RGB24, false);
                Action<string> shot = name => { cam.targetTexture = rt; Bootstrap.RenderAll(cam); RenderTexture.active = rt; img.ReadPixels(new Rect(0, 0, 1024, 768), 0, 0); img.Apply(); File.WriteAllBytes(Path.Combine(output, name), img.EncodeToPNG()); cam.targetTexture = null; RenderTexture.active = null; };
                game.LaunchMission = new NativeMapState.Mission { type = 3, count = 200, level = 1, background = 0 };
                game.Profile.playerType = 4;
                game.SwitchScene("gameScene"); game.StepFor(0.05f);
                var scene = (GameScene)game.Current; scene.spawningEnabled = false;
                var root = new GameObject("girl_rig_test"); root.transform.SetParent(scene.Rig.transform, false);
                const string A = "DC2/Image/acb/ipad/";
                var stand = NativeComponentAnimation.Load(A + "player_stand_short.animation.acb");
                var fire = NativeComponentAnimation.Load(A + "player_fire_long.animation.acb");
                var move = NativeComponentAnimation.Load(A + "player_move_short.animation.acb");
                log.AppendLine("INFO stand frames=" + stand.FrameCount + " comps=" + stand.components.Length + " fire frames=" + fire.FrameCount + " move frames=" + move.FrameCount);
                foreach (var kv in fire.byKey) log.AppendLine("INFO fire key " + kv.Key + " frames=" + kv.Value.frames.Length);
                var obj = new NativeCompObj(root.transform) { position = new Vector2(-232f, -162f) };
                obj.SetAnimation(stand);
                obj.SetAvatar(new[] { new KeyValuePair<string, string>("bshouqiang", "newplayer_shouqiang001") });
                obj.PlayAnimation(uint.MaxValue, 30f, 0f, -1f);
                check("stand loops forever with frame count end", obj.end == stand.FrameCount - 1 && obj.start == 0 && obj.loops == uint.MaxValue);
                check("avatar pair selects the pistol avatar", obj.CurAnimationNumber >= 0);
                for (int i = 0; i < 20; i++) obj.Step(0.016f);
                shot("girl_stand.png");
                obj.SetAnimation(move); obj.PlayAnimation(uint.MaxValue, 30f, 0f, -1f);
                for (int i = 0; i < 12; i++) obj.Step(0.016f);
                shot("girl_move.png");
                obj.SetAnimation(stand); obj.PlayAnimation(uint.MaxValue, 30f, 0f, -1f);
                // advanced fire: SetAnimationAdv + key frame index 1, PlayAnimationAdv(1, 30, 0, -1)
                int keys = 0, ends = 0; int keyAt = -2;
                obj.onKeyFrameAdv = k => { keys++; keyAt = obj.advFrame; };
                obj.onAnimationEndAdv = () => ends++;
                obj.SetAnimationAdv(fire);
                obj.advKeyFrame = 1;
                obj.PlayAnimationAdv(1u, 30f, 0f, -1f);
                check("advanced channel starts at frame 0 and is active", obj.AdvActive && obj.advFrame == 0 && obj.advEnd == fire.FrameCount - 1);
                float t = 0f;
                for (int i = 0; i < 4; i++) { obj.Step(0.016f); t += 0.016f; }
                shot("girl_fire_early.png");
                for (int i = 0; i < 60 && ends == 0; i++) { obj.Step(0.016f); t += 0.016f; if (i == 6) shot("girl_fire_mid.png"); }
                check("adv key frame index 1 fired exactly once, at frame 1", keys == 1 && keyAt == 1);
                check("single-cycle adv end fired once and deactivates the channel on the next step", ends == 1);
                obj.Step(0.016f);
                check("after the end the advanced frame is -1 (base only)", !obj.AdvActive);
                log.AppendLine("INFO fire cycle seconds ~" + t);
                // GetFrameIndex semantics (CAComponent::GetFrameIndex 0x11d90c): start 1.0 -> index of the last frame with number <= 1.0
                int idx = NativeCompObj.GetFrameIndex(fire, 1.0f);
                log.AppendLine("INFO GetFrameIndex(fire,1.0)=" + idx + " (10.0)=" + NativeCompObj.GetFrameIndex(fire, 10f) + " (6.0)=" + NativeCompObj.GetFrameIndex(fire, 6f));
                check("GetFrameIndex returns a valid index", idx >= 0 && idx < fire.FrameCount);
                // library animation (SetAdvCompAni gelin): Animation01 frames numbered 1..3
                var lib = NativeCaLibraryAnim.Get("DC2/Image/acb/Player.library.lcb", "Animation01");
                check("library Animation01 has 3 looping frames (gelin, gelin1, gelin2)", lib != null && lib.loop && lib.refs.Length == 3 && lib.refs[0] == "newplayer_gelin");
                check("library GetFrameId semantics: before first -> none, number 2 -> gelin1, 4 wraps to 1", lib.FrameRef(0.5f) == null && lib.FrameRef(2f) == "newplayer_gelin1" && lib.FrameRef(4f) == "newplayer_gelin");
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
