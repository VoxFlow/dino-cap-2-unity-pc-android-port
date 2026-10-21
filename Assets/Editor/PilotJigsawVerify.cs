using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>Jigsaw (m_map_type 5): native layout, drag/swipe/settle, new-piece animation, info text and bonus counters.</summary>
    public static class PilotJigsawVerify
    {
        static string Label(Dc2Scene s, int id) { foreach (var l in s.Labels) if (l.id == id) return l.Text; return null; }
        static void Click(Dc2Scene s, Vector2 p) { s.Touch(0, p); s.Touch(2, p); }

        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-PILOT-001/jigsaw-r1");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            string dir = Path.Combine(Path.GetTempPath(), "dc2_jig_verify_" + Guid.NewGuid().ToString("N"));
            int passed = 0, failed = 0; var log = new StringBuilder();
            Action<string, bool> check = (n, v) => { log.AppendLine((v ? "PASS " : "FAIL ") + n); if (v) passed++; else failed++; };
            Dc2Game game = null; Camera cam = null;
            RenderTexture rt = null; Texture2D img = null;
            try
            {
                cam = Bootstrap.MakeCamera(); game = Dc2Game.Create(dir); game.manualStep = true; game.SkipMissionIntro = true; game.cam = cam;
                rt = new RenderTexture(1024, 768, 24); img = new Texture2D(1024, 768, TextureFormat.RGB24, false);
                Action<string> shot = name =>
                {
                    cam.targetTexture = rt; Bootstrap.RenderAll(cam); RenderTexture.active = rt; img.ReadPixels(new Rect(0, 0, 1024, 768), 0, 0); img.Apply();
                    File.WriteAllBytes(Path.Combine(output, name), img.EncodeToPNG());
                    cam.targetTexture = null; RenderTexture.active = null;
                };
                game.SwitchScene("selectScene");
                var scene = (SelectScene)game.Current;
                game.StepFor(1.0f);
                // a few pieces already placed so the capture shows art
                foreach (int k in new[] { 0, 1, 2, 5, 6, 9, 10, 15 }) game.Profile.jigsawActive[k] = 1;
                foreach (int k in new[] { 16, 17, 20, 31 }) game.Profile.jigsawActive[k] = 1;
                Click(scene, scene.GetControl(95).FullRect.center);
                check("map jigsaw button queues map type 5", game.MapType == 5);
                game.StepFor(0.05f); scene = (SelectScene)game.Current; game.StepFor(1.5f);

                check("background centred 1024x704 (0,32)", scene.GetControl(9100) != null && scene.GetControl(9100).FullRect == new Rect(0, 32, 1024, 704) && scene.GetControl(9100).FrameName == "ui_bg_allFrame");
                check("title jigsaw_titleFrame (0,32,390,144)", scene.GetControl(35).FullRect == new Rect(0, 32, 390, 144) && scene.GetControl(35).FrameName == "jigsaw_titleFrame");
                check("group0 panel (262,74,500,500)", scene.GetControl(9110).FullRect == new Rect(262, 74, 500, 500));
                check("group1 panel scaled 0.8: (872,124,400,400)", scene.GetControl(9111).FullRect == new Rect(872, 124, 400, 400));
                check("group0 piece0 (312,124,100,100) frame jigsaw_map01_01", scene.GetControl(9200).FullRect == new Rect(312, 124, 100, 100) && scene.GetControl(9200).FrameName == "jigsaw_map01_01Frame");
                check("group0 piece5 in row1 col1 (412,224)", scene.GetControl(9205).FullRect == new Rect(412, 224, 100, 100));
                check("piece 10 frame name uses two digits", scene.GetControl(9209).FrameName == "jigsaw_map01_10Frame");
                check("counter shows 8/16 at (790,84,400,80)", Label(scene, 599) == "8/16");
                check("info text group0 HP 40%", Label(scene, 598) == "Increase all characters' HP by 40%. This \ntask can only be finished by SUPER KID.");
                check("g_jigsaw1_count := 5*8 = 40", game.Profile.jigsaw1Count == 40);
                check("back button + image present", scene.GetControl(17) != null && scene.GetControl(18) != null && !scene.GetControl(18).touchable);
                shot("jigsaw_ui.png");

                // drag: press 500, move to 300 then 100, release at 100
                scene.Touch(0, new Vector2(500, 400));
                scene.Touch(1, new Vector2(300, 400));
                check("first drag step moves every group by -200", Mathf.Abs(scene.JigGroupX(0) - -200f) < 0.01f && Mathf.Abs(scene.JigGroupX(1) - 360f) < 0.01f);
                scene.Touch(1, new Vector2(100, 400));
                check("second drag step moves again by -200", Mathf.Abs(scene.JigGroupX(0) - -400f) < 0.01f && Mathf.Abs(scene.JigGroupX(1) - 160f) < 0.01f);
                shot("jigsaw_drag.png");
                scene.Touch(2, new Vector2(100, 400));
                check("swipe left over 100 px selects group 1 and shows its text", scene.jigCursor == 1 && scene.jigSettling && Label(scene, 598).StartsWith("Increase the attack of all weapons by "));
                game.StepFor(2.0f);
                check("settle brings group 1 to the centre (|x|<=1)", Mathf.Abs(scene.JigGroupX(1)) <= 1f && !scene.jigSettling);
                check("group 1 count 4/16 and bonus 4*5=20%", Label(scene, 599) == "4/16" && game.Profile.jigsaw2Count == 20 && Label(scene, 598).Contains("by 20%"));
                shot("jigsaw_group2.png");

                // tap without horizontal movement does nothing
                int cursorBefore = scene.jigCursor;
                scene.Touch(0, new Vector2(500, 400)); scene.Touch(2, new Vector2(500, 400));
                check("tap without movement keeps group", scene.jigCursor == cursorBefore && !scene.jigSettling);

                // new pieces: pending list processed from the end with fly-in and effect
                game.Profile.jigsawGet[16 * 2 + 5] = 1; game.Profile.jigsawGet[3] = 1;
                game.Profile.jigsawActive[3] = 0;
                Click(scene, scene.GetControl(17).FullRect.center);
                check("back leaves for the map", game.MapType == 0);
                game.StepFor(0.05f); scene = (SelectScene)game.Current; game.StepFor(1.0f);
                Click(scene, scene.GetControl(95).FullRect.center);
                game.StepFor(0.05f); scene = (SelectScene)game.Current;
                game.StepFor(0.2f);
                check("banner YOU HAVE 2 JIGSAWS", Label(scene, 599) == "YOU HAVE 2 JIGSAWS" && scene.jigPending.Count == 2);
                game.StepFor(1.0f);
                game.StepFor(0.5f);
                check("UI built after the banner leaves; back not yet available", scene.GetControl(9110) != null && scene.GetControl(17) == null);
                for (int frame = 0; frame < 12*60 && scene.JigsawEffect == null; frame++) game.Step(1f/60f);
                check("piece callback creates native scaled effect within bounded12s", scene.JigsawEffect != null && scene.JigsawEffect.objectScale == 2f);
                if (scene.JigsawEffect != null)
                {
                    // DC2-BUGS-001 B7 diagnostic: ligsawPicCallBack 0xf514e passes the board slot GameObject's position; the effect object origin must sit on
                    // that slot. Compare the effect origin (jigsaw_effect root + NativeComponentPlayer mapping) with the slot control's rendered centre.
                    var rootField = typeof(SelectScene).GetField("jigEffectRoot", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    var effRoot = rootField != null ? rootField.GetValue(scene) as GameObject : null;
                    var slotPos = scene.LastJigsawSlotPos; var slotCtl = scene.LastJigsawSlotControl;
                    if (effRoot != null && slotCtl != null)
                    {
                        Vector3 origin = effRoot.transform.TransformPoint(new Vector3(N.ScreenW * 0.5f + slotPos.x, -N.ScreenH * 0.5f + slotPos.y, 0f));
                        var sr = slotCtl.GetComponentInChildren<SpriteRenderer>(true);
                        Vector3 slotCentre = sr != null ? sr.bounds.center : slotCtl.transform.position;
                        log.AppendLine("SAMPLE B7 effect origin " + origin + " slot centre " + slotCentre + " effect root " + effRoot.transform.position + " slot parent " + slotCtl.transform.parent.position);
                        check("B7 the placement flash's object origin is on its slot (|d| <= 1 px)", Mathf.Abs(origin.x - slotCentre.x) <= 1f && Mathf.Abs(origin.y - slotCentre.y) <= 1f);
                    }
                    else check("B7 diagnostic fixture: effect root and slot control found", false);
                    var sg = effRoot != null ? effRoot.GetComponent<UnityEngine.Rendering.SortingGroup>() : null;
                    int panelMax = int.MinValue, pieceMin = int.MaxValue;
                    foreach (var c in scene.Controls) if (c != null && c.id >= 9100 && c.id < 9400)
                    { int o = c.GetComponent<SpriteRenderer>().sortingOrder; if (c.id < 9200) panelMax = Mathf.Max(panelMax, o); else pieceMin = Mathf.Min(pieceMin, o); }
                    check("r119d: the placement flash draws above the background / panels and under the pieces (group " + (sg != null ? sg.sortingOrder : -1) + ", panels max " + panelMax + ", pieces min " + pieceMin + ")",
                          sg != null && sg.sortingOrder > panelMax && sg.sortingOrder < pieceMin);
                    game.StepFor(0.15f);
                    shot("jigsaw_effect_scale2.png");
                    scene.JigsawEffect.objectScale = 1f; scene.JigsawEffect.Step(0f);
                    shot("jigsaw_effect_old_scale1.png");
                    scene.JigsawEffect.objectScale = 2f; scene.JigsawEffect.Step(0f);
                }
                game.StepFor(14.0f);
                check("both pieces placed", game.Profile.jigsawActive[16 * 2 + 5] == 1 && game.Profile.jigsawActive[3] == 1 && scene.jigPending.Count == 0);
                check("two effects played", scene.jigEffectsPlayed == 2);
                check("back button appears after the last effect", scene.GetControl(17) != null);
                check("saved after completion", File.Exists(Path.Combine(dir, "dinocap2hd.sav")));
                Click(scene, scene.GetControl(17).FullRect.center);
                check("back to map type 0", game.MapType == 0);
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
