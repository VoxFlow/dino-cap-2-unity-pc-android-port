using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>DC2-BUGS-001 r119g (user: "try the forced switch from pistol scale bug for all other weapons on all heroes").
    /// playerClass::AfterNoBullets 0xbaeac = GenComAnim + ChangeWeapon(gun) + ChangeWeapon(melee), the same work as a manual switch, so after an empty-gun switch
    /// the hero must look exactly like after a manual switch to the same gun. For every hero (girl included), every ordered pair of owned guns (from, to) and three
    /// entries (tap on an empty gun / last round then press / last round then press while walking right), the forced switch is compared with a manual switch on
    /// (a) the rig description (classic: pose source, frame-0 parts with image and size, object scale; girl: avatars, base animation, object scale) and
    /// (b) the largest rendered size of the hero over 1 s (all enabled sprite renderers), which catches a scale change whatever the animation phase.</summary>
    public static class PilotForcedSwitchVerify
    {
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-BUGS-001/forced-switch");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            int passed = 0, failed = 0; var log = new StringBuilder();
            Action<string, bool> check = (n, v) => { log.AppendLine((v ? "PASS " : "FAIL ") + n); if (v) passed++; else failed++; };
            string dir = Path.Combine(Path.GetTempPath(), "dc2_fsw_" + Guid.NewGuid().ToString("N"));
            Camera cam = null; Dc2Game g = null;
            var sel = typeof(GameScene).GetMethod("SelectWeaponSlot", BindingFlags.NonPublic | BindingFlags.Instance);
            var girlField = typeof(GameScene).GetField("girlAvatars", BindingFlags.NonPublic | BindingFlags.Instance);
            var girlObjField = typeof(GameScene).GetField("girl", BindingFlags.NonPublic | BindingFlags.Instance);
            try
            {
                cam = Bootstrap.MakeCamera(); g = Dc2Game.Create(dir); g.cam = cam; g.manualStep = true; g.SkipMissionIntro = true;
                string[] modes = { "tap-empty", "last-round", "last-round-walking" };
                for (int hero = FirstHero(); hero <= 4; hero++)
                {
                    var weapons = Dc2GameData.PlayerWeapons(hero);
                    var guns = new List<int>();
                    for (int s = 0; s < weapons.Count - 1 && s < 5; s++) if (weapons[s].type <= 5) guns.Add(s);
                    foreach (string mode in modes)
                        foreach (int a in guns)
                            foreach (int b in guns)
                            {
                                if (a == b) continue;
                                bool walking = mode == "last-round-walking";
                                // manual reference
                                var m = Load(g, hero, a, b);
                                sel.Invoke(m, new object[] { a }); g.StepFor(0.4f);
                                if (walking) m.rightDown = true;
                                g.StepFor(0.2f);
                                sel.Invoke(m, new object[] { b }); g.StepFor(2.5f);
                                string manualSig = Sig(m, hero, girlField, girlObjField); Vector2 manualMax = MaxBounds(g, m, girlObjField);
                                // forced switch
                                var f = Load(g, hero, a, b);
                                sel.Invoke(f, new object[] { a }); g.StepFor(0.4f);
                                if (walking) f.rightDown = true;
                                g.StepFor(0.2f);
                                g.Profile.weaponAmmo[hero * 6 + a] = mode == "tap-empty" ? 0 : 1;
                                f.fireDown = true; g.Step(1f / 60f); f.fireDown = false;
                                if (mode != "tap-empty") { g.StepFor(1.5f); f.fireDown = true; g.Step(1f / 60f); f.fireDown = false; }
                                g.StepFor(2.5f);   // the new gun fires once if fire is still held (ChangeWeapon tail, native); compare after that shot and the 2 s fireup timer
                                string forcedSig = Sig(f, hero, girlField, girlObjField); Vector2 forcedMax = MaxBounds(g, f, girlObjField);
                                bool same = f.weaponSlot == b && forcedSig == manualSig && Mathf.Abs(forcedMax.x - manualMax.x) <= 1f && Mathf.Abs(forcedMax.y - manualMax.y) <= 1f;
                                string name = "hero " + hero + " " + mode + " slot " + a + " (" + weapons[a].type + "/" + weapons[a].subtype + ") -> " + b + " (" + weapons[b].type + "/" + weapons[b].subtype + "): landed " + f.weaponSlot
                                            + ", max size forced " + forcedMax.x.ToString("0.0") + "x" + forcedMax.y.ToString("0.0") + " manual " + manualMax.x.ToString("0.0") + "x" + manualMax.y.ToString("0.0");
                                check(name, same);
                                if (!same) log.AppendLine("  manual " + manualSig + "\n  forced " + forcedSig);
                            }
                }
            }
            catch (Exception e) { failed++; log.AppendLine("EXCEPTION " + e); }
            finally
            {
                if (g != null) UnityEngine.Object.DestroyImmediate(g.gameObject);
                if (cam != null) UnityEngine.Object.DestroyImmediate(cam.gameObject);
                if (Directory.Exists(dir)) Directory.Delete(dir, true);
                log.AppendLine("SUMMARY passes=" + passed + " fails=" + failed);
                File.WriteAllText(Path.Combine(output, "verify.txt"), log.ToString()); Debug.Log(log.ToString());
                PilotExit.Exit(failed == 0 ? 0 : 1);
            }
        }

        static int FirstHero() { var a = Environment.GetCommandLineArgs(); for (int i = 0; i < a.Length - 1; i++) if (a[i] == "-firstHero") return int.Parse(a[i + 1]); return 0; }

        static GameScene Load(Dc2Game g, int hero, int a, int b)
        {
            g.Profile.playerType = hero;
            for (int s = 0; s < 6; s++) { g.Profile.weaponOwned[hero * 6 + s] = s == a || s == b || s == 5 ? 0 : -1; g.Profile.weaponAmmo[hero * 6 + s] = 50; }
            g.LaunchMission = new NativeMapState.Mission { type = 3, count = 20, level = 1, background = 0 }; g.LaunchMission.enemies.Add(1);
            g.SwitchScene("gameScene"); g.Step(0f); g.Step(1f / 60f);
            var s2 = (GameScene)g.Current; s2.spawningEnabled = false; s2.ignorePlayer = true; s2.heroX = s2.camLeft + 300f;
            return s2;
        }

        static string Sig(GameScene s, int hero, FieldInfo girlField, FieldInfo girlObjField)
        {
            var sb = new StringBuilder();
            if (hero == 4)
            {
                var av = (List<KeyValuePair<string, string>>)girlField.GetValue(s);
                var obj = (NativeCompObj)girlObjField.GetValue(s);
                sb.Append("girl base " + s.GirlBaseName + " scale " + (obj != null ? obj.objectScale.ToString("R") : "-") + " |");
                foreach (var p in av) if (p.Value != "") sb.Append(" " + p.Key + "=" + p.Value);   // an empty avatar string selects no component (no visual change): the KaiQiang "lian" pair until the fireup timer
                return sb.ToString();
            }
            var pl = s.Rig.Player;
            sb.Append("scale " + pl.objectScale.ToString("R") + " src " + s.Rig.Source + " |");
            foreach (var pt in pl.animation.frames[0].parts) sb.Append(" " + pt.name + ":" + pt.reference + ":" + pt.w.ToString("0.##") + "x" + pt.h.ToString("0.##"));
            return sb.ToString();
        }

        static Vector2 MaxBounds(Dc2Game g, GameScene s, FieldInfo girlObjField)
        {
            Vector2 max = Vector2.zero;
            for (int t = 0; t < 60; t++)
            {
                g.Step(1f / 60f);
                var obj = girlObjField.GetValue(s) as NativeCompObj;
                if (obj != null && obj.Sprites.Count > 0)
                {   // the girl draws through NativeCompObj: extents of its emitted sprites (centre +/- size/2, rotation ignored)
                    float x0 = float.MaxValue, x1 = float.MinValue, y0 = float.MaxValue, y1 = float.MinValue;
                    foreach (var sp in obj.Sprites.Values) { x0 = Mathf.Min(x0, sp.x - sp.w * 0.5f); x1 = Mathf.Max(x1, sp.x + sp.w * 0.5f); y0 = Mathf.Min(y0, sp.y - sp.h * 0.5f); y1 = Mathf.Max(y1, sp.y + sp.h * 0.5f); }
                    max.x = Mathf.Max(max.x, x1 - x0); max.y = Mathf.Max(max.y, y1 - y0);
                    continue;
                }
                bool any = false; Bounds bb = new Bounds();
                foreach (var r in s.Rig.GetComponentsInChildren<SpriteRenderer>())
                {
                    if (!r.enabled || r.sprite == null || !r.gameObject.activeInHierarchy) continue;
                    if (!any) { bb = r.bounds; any = true; } else bb.Encapsulate(r.bounds);
                }
                if (any) { max.x = Mathf.Max(max.x, bb.size.x); max.y = Mathf.Max(max.y, bb.size.y); }
            }
            return max;
        }
    }
}
