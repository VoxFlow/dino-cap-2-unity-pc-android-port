using System;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>DEBUG_ONLY diagnostic (DC2-BUGS-001 r119j item 3, user: "check the gun muzzle flash for all applicable weapons and their positions"): every hero and every
    /// owned gun slot fires one shot standing facing right (and walking for slot 0); the frame after the muzzle flash spawns is captured (4:3) and the flash
    /// position/scale is logged. Not part of the suite.</summary>
    public static class PilotMuzzleProbe
    {
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-BUGS-001/muzzle-probe");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            var log = new StringBuilder();
            string dir = Path.Combine(Path.GetTempPath(), "dc2_muzzle_" + Guid.NewGuid().ToString("N"));
            Camera cam = null; Dc2Game g = null;
            var sel = typeof(GameScene).GetMethod("SelectWeaponSlot", BindingFlags.NonPublic | BindingFlags.Instance);
            var rt = new RenderTexture(1024, 768, 24); var img = new Texture2D(1024, 768, TextureFormat.RGB24, false);
            try
            {
                cam = Bootstrap.MakeCamera(); g = Dc2Game.Create(dir); g.cam = cam; g.manualStep = true; g.SkipMissionIntro = true;
                Action<string> shot = name => { cam.targetTexture = rt; Bootstrap.RenderAll(cam); RenderTexture.active = rt; img.ReadPixels(new Rect(0, 0, 1024, 768), 0, 0); img.Apply(); File.WriteAllBytes(Path.Combine(output, name), img.EncodeToPNG()); cam.targetTexture = null; RenderTexture.active = null; };
                for (int hero = 0; hero <= 4; hero++)
                {
                    var ws = Dc2GameData.PlayerWeapons(hero);
                    for (int slot = 0; slot < 5 && slot < ws.Count - 1; slot++)
                    {
                        if (ws[slot].type > 4) continue;
                        foreach (bool walking in slot == 0 ? new[] { false, true } : new[] { false })
                        {
                            g.Profile.playerType = hero;
                            for (int k = 0; k < 6; k++) { g.Profile.weaponOwned[hero * 6 + k] = k == slot || k == 5 ? 0 : -1; g.Profile.weaponAmmo[hero * 6 + k] = 200; }
                            g.LaunchMission = new NativeMapState.Mission { type = 3, count = 20, level = 1, background = 0 }; g.LaunchMission.enemies.Add(1);
                            g.SwitchScene("gameScene"); g.Step(0f); g.Step(1f / 60f);
                            var s = (GameScene)g.Current; s.spawningEnabled = false; s.ignorePlayer = true;
                            sel.Invoke(s, new object[] { slot }); g.StepFor(2.5f);
                            s.facingLeft = false; if (walking) s.rightDown = true;
                            int fx0 = s.FxSpawned; s.fireDown = true;
                            string tag = "h" + hero + "_s" + slot + "_t" + ws[slot].type + "-" + ws[slot].subtype + (walking ? "_walk" : "");
                            bool got = false;
                            for (int t = 0; t < 60 && !got; t++)
                            {
                                g.Step(1f / 60f);
                                if (s.LastFxTag == "qh" && s.FxSpawned > fx0) { g.Step(1f / 60f); shot(tag + ".png"); got = true; log.AppendLine(tag + " flash at tick " + t + " heroX " + s.heroX); }
                            }
                            if (!got) log.AppendLine(tag + " NO muzzle flash within 1 s (last fx " + s.LastFxTag + ")");
                            s.fireDown = false; s.rightDown = false;
                        }
                    }
                }
            }
            catch (Exception e) { log.AppendLine("EXCEPTION " + e); }
            finally
            {
                if (g != null) UnityEngine.Object.DestroyImmediate(g.gameObject);
                if (cam != null) UnityEngine.Object.DestroyImmediate(cam.gameObject);
                if (Directory.Exists(dir)) Directory.Delete(dir, true);
                File.WriteAllText(Path.Combine(output, "verify.txt"), log.ToString()); Debug.Log(log.ToString());
                PilotExit.Exit(0);
            }
        }
    }
}
