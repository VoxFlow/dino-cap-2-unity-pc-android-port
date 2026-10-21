using System;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>DEBUG_ONLY diagnostic (DC2-BUGS-001 r119i item 1, user: "facing right, the FK Electric Shock beam is cut short on screen only; facing left is fine; the laser may have the same problem").
    /// 16:9 wide view: every hero with a laser (type 5 sub 2) or electric (type 5 sub 4) weapon fires facing right and facing left from several x; logs the mask span,
    /// the beam sprite bounds and captures the frame. Not part of the suite.</summary>
    public static class PilotBeamClipProbe
    {
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-BUGS-001/beamclip-probe");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            var log = new StringBuilder();
            string dir = Path.Combine(Path.GetTempPath(), "dc2_beamclip_" + Guid.NewGuid().ToString("N"));
            Camera cam = null; Dc2Game g = null; bool wasEnabled = WideView.Enabled;
            var sel = typeof(GameScene).GetMethod("SelectWeaponSlot", BindingFlags.NonPublic | BindingFlags.Instance);
            var rt = new RenderTexture(1920, 1080, 24); var img = new Texture2D(1920, 1080, TextureFormat.RGB24, false);
            try
            {
                WideView.Enabled = true;
                cam = Bootstrap.MakeCamera(); g = Dc2Game.Create(dir); g.cam = cam; g.manualStep = true; g.SkipMissionIntro = true;
                Action<string> shot = name => { cam.targetTexture = rt; Bootstrap.RenderAll(cam); RenderTexture.active = rt; img.ReadPixels(new Rect(0, 0, 1920, 1080), 0, 0); img.Apply(); File.WriteAllBytes(Path.Combine(output, name), img.EncodeToPNG()); cam.targetTexture = null; RenderTexture.active = null; };
                for (int hero = 0; hero <= 3; hero++)
                {
                    var weapons = Dc2GameData.PlayerWeapons(hero);
                    for (int slot = 0; slot < weapons.Count && slot < 6; slot++)
                    {
                        var w = weapons[slot];
                        if (w.type != 5 || (w.subtype != 2 && w.subtype != 4)) continue;
                        foreach (bool left in new[] { false, true })
                            foreach (float hx in new[] { 100f, 300f, 600f })
                            {
                                g.Profile.playerType = hero;
                                for (int k = 0; k < 6; k++) { g.Profile.weaponOwned[hero * 6 + k] = k == slot || k == 5 ? 0 : -1; g.Profile.weaponAmmo[hero * 6 + k] = 50; }
                                g.LaunchMission = new NativeMapState.Mission { type = 3, count = 20, level = 1, background = 0 }; g.LaunchMission.enemies.Add(1);
                                g.SwitchScene("gameScene"); g.Step(0f); g.Step(1f / 60f);
                                var s = (GameScene)g.Current; s.spawningEnabled = false; s.ignorePlayer = true;
                                WideView.Apply(cam, s, 1920, 1080);
                                sel.Invoke(s, new object[] { slot }); g.StepFor(0.4f);
                                s.heroX = s.camLeft + hx; s.facingLeft = left; g.Step(1f / 60f);
                                s.fireDown = true;
                                string tag = "h" + hero + "_s" + slot + "_" + (left ? "L" : "R") + "_x" + hx;
                                for (int t = 0; t < 90; t++)
                                {
                                    g.Step(1f / 60f);
                                    if (t == 40 || t == 70)
                                    {
                                        foreach (var m in UnityEngine.Object.FindObjectsByType<SpriteMask>(FindObjectsSortMode.None))
                                        {
                                            if (!m.enabled) continue;
                                            var root = m.transform.parent; Bounds bb = new Bounds(); bool any = false;
                                            foreach (var r in root.GetComponentsInChildren<SpriteRenderer>()) { if (!r.enabled || r.sprite == null) continue; if (!any) { bb = r.bounds; any = true; } else bb.Encapsulate(r.bounds); }
                                            log.AppendLine(tag + " t" + t + " " + root.name + " rootPos " + root.position.x.ToString("0.0") + " parentPos " + (root.parent != null ? root.parent.position.x.ToString("0.0") : "-") + " mask world x " + m.bounds.min.x.ToString("0.0") + ".." + m.bounds.max.x.ToString("0.0")
                                                + " sprites x " + (any ? bb.min.x.ToString("0.0") + ".." + bb.max.x.ToString("0.0") : "-") + " hero(screen) " + (s.heroX - s.camLeft) + " camPos " + cam.transform.position.x.ToString("0.0"));
                                        }
                                        shot(tag + "_t" + t + ".png");
                                    }
                                }
                                s.fireDown = false;
                            }
                    }
                }
            }
            catch (Exception e) { log.AppendLine("EXCEPTION " + e); }
            finally
            {
                WideView.Enabled = wasEnabled;
                if (g != null) UnityEngine.Object.DestroyImmediate(g.gameObject);
                if (cam != null) UnityEngine.Object.DestroyImmediate(cam.gameObject);
                if (Directory.Exists(dir)) Directory.Delete(dir, true);
                File.WriteAllText(Path.Combine(output, "verify.txt"), log.ToString()); Debug.Log(log.ToString());
                PilotExit.Exit(0);
            }
        }
    }
}
