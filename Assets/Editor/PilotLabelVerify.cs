using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    [Serializable] public sealed class LabelCaseDef { public string font, text; public float[] rect; public float cs, ls; public int h, v; public float[] glyphs; }
    [Serializable] public sealed class LabelOracleDef { public LabelCaseDef[] cases; }

    /// <summary>Native UILabel layout (UpdateText 0x180040) against the independent Python re-implementation (Dev/Tools/label_oracle.py) and the HUD labels' native attributes (Dev/Evidence/hud_labels_r118.json).</summary>
    public static class PilotLabelVerify
    {
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-PILOT-001/label-r1");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            string dir = Path.Combine(Path.GetTempPath(), "dc2_label_verify_" + Guid.NewGuid().ToString("N"));
            int passed = 0, failed = 0; var log = new StringBuilder();
            Action<string, bool> check = (n, v) => { log.AppendLine((v ? "PASS " : "FAIL ") + n); if (v) passed++; else failed++; };
            Dc2Game game = null; Camera cam = null; RenderTexture rt = null; Texture2D img = null;
            try
            {
                cam = Bootstrap.MakeCamera(); game = Dc2Game.Create(dir); game.manualStep = true; game.SkipMissionIntro = true; game.cam = cam;
                rt = new RenderTexture(1024, 768, 24); img = new Texture2D(1024, 768, TextureFormat.RGB24, false);
                game.LaunchMission = new NativeMapState.Mission { type = 3, count = 10, level = 1, background = 1 };
                game.Profile.cash = 8220622;
                game.SwitchScene("gameScene"); game.StepFor(0.1f);
                var scene = (GameScene)game.Current; scene.spawningEnabled = false;
                var oracle = JsonUtility.FromJson<LabelOracleDef>(File.ReadAllText(Path.Combine(Application.dataPath, "../../Dev/Evidence/label_layout_oracle_r118.json")));
                int n = 0;
                foreach (var c in oracle.cases)
                {
                    var l = scene.CreateLabel(7000 + n++, 4, 30);
                    l.SetRect(new Rect(c.rect[0], c.rect[1], c.rect[2], c.rect[3])); l.SetNative(c.font, c.cs, c.ls, c.h, c.v); l.SetText(c.text);
                    bool ok = l.GlyphCount * 4 <= c.glyphs.Length && true;
                    int expect = c.glyphs.Length / 4, got = l.GlyphCount, spaces = 0; foreach (char ch in c.text) if (ch == ' ' || ch == '\n') spaces++;
                    string why = "";
                    for (int i = 0; i < Math.Min(got, expect) && ok; i++)
                    {
                        var r = l.GlyphRect(i);
                        if (Mathf.Abs(r.x - c.glyphs[i * 4]) > 0.01f || Mathf.Abs(r.y - c.glyphs[i * 4 + 1]) > 0.01f || Mathf.Abs(r.width - c.glyphs[i * 4 + 2]) > 0.01f || Mathf.Abs(r.height - c.glyphs[i * 4 + 3]) > 0.01f) { ok = false; why = " glyph " + i + " got " + r + " want " + c.glyphs[i * 4] + "," + c.glyphs[i * 4 + 1] + "," + c.glyphs[i * 4 + 2] + "," + c.glyphs[i * 4 + 3]; }
                    }
                    check("label '" + c.text.Replace("\n", "\\n") + "' (" + c.font + ", align " + c.h + "," + c.v + ") glyph rects equal the independent oracle" + why, ok && got == expect);
                }
                // HUD labels: native attributes
                UiLabel gold = null, ammo = null;
                foreach (var l in scene.Labels) { if (l.id == 10) gold = l; if (l.id == 97) ammo = l; }
                check("HUD gold label (id 10) uses the native label path and shows the comma-formatted cash", gold != null && gold.IsNative && gold.Text == "8,220,622" && gold.GlyphCount == 9);
                check("HUD ammo label (id 97) uses the native label path", ammo != null && ammo.IsNative && ammo.GlyphCount > 0);
                // lifeModeScene +564 = label 9 (kill counter), +560 = label 27 (clock); OnLoad visibility per mission type (r118z)
                UiLabel kill = null, clock = null;
                foreach (var l in scene.Labels) { if (l.id == 9) kill = l; if (l.id == 27) clock = l; }
                check("kill mission: label 9 (Adventure_16, rect icon.x+S*30, icon.y, S*200, S*30 = 692,42,400,60 (r119; the r118z S*30,0 value is FALSIFIED)) shows 0/total and is visible; label 27 is hidden",
                      kill != null && kill.IsNative && kill.Text == "0/" + scene.killTotal && kill.Visible && kill.CurrentRect == new Rect(692f, 42f, 400f, 60f) && clock != null && !clock.Visible);
                scene.ignorePlayer = true; var dino = scene.CreateEnemy(1); scene.ApplyDamageForTest(dino); game.StepFor(0.1f);
                check("a kill updates label 9 (UpdateKillDinocap)", kill.Text == "1/" + scene.killTotal);
                cam.targetTexture = rt; Bootstrap.RenderAll(cam); RenderTexture.active = rt; img.ReadPixels(new Rect(0, 0, 1024, 768), 0, 0); img.Apply();
                File.WriteAllBytes(Path.Combine(output, "hud_native_labels.png"), img.EncodeToPNG());
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
