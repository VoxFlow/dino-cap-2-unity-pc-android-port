using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>DC2-BUGS-001 O1: WideView (ADAPTATION, render only). Checks that ExtraHalf 0 is the native layout, that a 16:9 battle shows more world with the
    /// HUD on the screen edges and touches landing on the drawn buttons, that the background chains cover the whole widened view, that full-width solid
    /// overlays stretch, that menus scale to cover, and that "off" gives the native 4:3 box. Captures battle_16x9.png / battle_21x9.png / menu_16x9.png.</summary>
    public static class PilotWideViewVerify
    {
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-BUGS-001/wideview-r1");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            int passed = 0, failed = 0; var log = new StringBuilder();
            Action<string, bool> check = (n, v) => { log.AppendLine((v ? "PASS " : "FAIL ") + n); if (v) passed++; else failed++; };
            string dir = Path.Combine(Path.GetTempPath(), "dc2_wide_" + Guid.NewGuid().ToString("N"));
            Camera cam = null; Dc2Game g = null; bool wasEnabled = WideView.Enabled;
            try
            {
                WideView.Enabled = true;
                cam = Bootstrap.MakeCamera();
                g = Dc2Game.Create(dir); g.cam = cam; g.manualStep = true; g.SkipMissionIntro = true;
                g.LaunchMission = new NativeMapState.Mission { type = 3, count = 20, level = 1, background = 0 }; g.LaunchMission.enemies.Add(1);
                g.SwitchScene("gameScene"); g.Step(0f); g.Step(1f / 60f);
                var s = (GameScene)g.Current; s.spawningEnabled = false; s.ignorePlayer = true;

                WideView.SetExtraHalf(0f, s);
                check("ExtraHalf 0: touch rects are the native rects", GameScene.LeftRect == GameScene.NativeLeftRect && GameScene.FireRect == GameScene.NativeFireRect && GameScene.StopRect == GameScene.NativeStopRect);
                var hpFrame = s.GetControl(5); var fire = s.GetControl(4); var stop = s.GetControl(44);   // the kill-counter icon (kill mission): right half since r119
                float hp0 = hpFrame.transform.localPosition.x, fire0 = fire.transform.localPosition.x, stop0 = stop.transform.localPosition.x;
                check("ExtraHalf 0: HUD drawn at its native rect", Mathf.Abs(hp0 - (hpFrame.FullRect.x + hpFrame.FullRect.width * 0.5f)) < 1e-3f && Mathf.Abs(fire0 - (fire.FullRect.x + fire.FullRect.width * 0.5f)) < 1e-3f);

                // 16:9 battle
                WideView.Apply(cam, s, 1920, 1080);
                float e = WideView.ExtraHalf;
                log.AppendLine("SAMPLE e=" + e.ToString("R") + " hp " + hp0 + "->" + hpFrame.transform.localPosition.x + " a" + hpFrame.wideAnchor + " fire " + fire0 + "->" + fire.transform.localPosition.x + " a" + fire.wideAnchor + " stop " + stop0 + "->" + stop.transform.localPosition.x
                    + " fireRect " + GameScene.FireRect.x.ToString("R") + " native " + GameScene.NativeFireRect.x + " left " + GameScene.LeftRect.x.ToString("R") + " label0 id" + s.Labels[0].id + " a" + s.Labels[0].wideAnchor + " x" + s.Labels[0].transform.localPosition.x);
                check("16:9 battle: full-screen camera, native height 768, extra " + e.ToString("0.00") + " px per side", cam.rect == new Rect(0, 0, 1, 1) && cam.orthographicSize == 384f && Mathf.Abs(e - (384f * 1920f / 1080f - 512f)) < 1e-3f);
                check("16:9 battle: left HUD moved left by e, right HUD right by e, kill icon right by e",
                      Mathf.Abs(hpFrame.transform.localPosition.x - (hp0 - e)) < 1e-3f && Mathf.Abs(fire.transform.localPosition.x - (fire0 + e)) < 1e-3f && Mathf.Abs(stop.transform.localPosition.x - (stop0 + e)) < 1e-3f);
                check("16:9 battle: HUD hit/restore rects stay native (render-only shift)", fire.FullRect == GameScene.FireDisp);
                var gold = s.Labels[0];
                check("16:9 battle: left label shifted with the HUD", gold.wideAnchor == -1 && Mathf.Abs(gold.transform.localPosition.x + e) < 1e-3f);
                check("16:9 battle: fire touch rect follows the drawn fire button", Mathf.Abs(GameScene.FireRect.x - (GameScene.NativeFireRect.x + e)) < 1e-3f && Mathf.Abs(GameScene.LeftRect.x - (GameScene.NativeLeftRect.x - e)) < 1e-3f && GameScene.StopRect.x > GameScene.NativeStopRect.x);
                s.Touch(0, GameScene.NativeFireRect.center + new Vector2(e, 0f), 7);
                check("16:9 battle: a touch on the drawn fire button fires", s.fireDown);
                s.Touch(2, GameScene.NativeFireRect.center + new Vector2(e, 0f), 7);
                float hx = s.heroX; s.Touch(0, GameScene.NativeRightRect.center - new Vector2(e, 0f), 8); g.StepFor(0.3f); s.Touch(2, GameScene.NativeRightRect.center - new Vector2(e, 0f), 8);
                check("16:9 battle: a touch on the drawn right arrow walks right", s.heroX > hx);
                check("16:9 battle: keyboard fingers press the drawn button centres", (PcPlatform.ActionPoint(4) - GameScene.FireRect.center).sqrMagnitude < 1e-6f && GameScene.NativeTouchInside(GameScene.StopRect, PcPlatform.ActionPoint(6)));
                Coverage(check, s, e, "bg0 16:9");
                check("r119e battle: no menu masks", !WideView.MenuMasksVisible);
                // r119e item 2 (user decision): in wide view the hero walks left to the visible edge; the camera keeps the native bound
                {
                    s.leftDown = true; g.StepFor(12f); s.leftDown = false; g.Step(1f / 60f);
                    check("r119e 16:9: hero stops at the visible left edge (x " + s.heroX + ", bound " + s.BoundLower + ", e " + e + "), camera at the native bound (" + s.camLeft + ")",
                          Mathf.Abs(s.heroX - (s.BoundLower - e)) < 0.01f && s.camLeft == s.BoundLower);
                    WideView.SetExtraHalf(0f, s); s.leftDown = true; g.StepFor(1f); s.leftDown = false; g.Step(1f / 60f);
                    check("r119e 4:3: native bound (" + s.heroX + ")", s.heroX == s.BoundLower);
                    WideView.Apply(cam, s, 1920, 1080);
                }
                // r119f item 7 (user decision): in wide view the drawn ray is clipped at max(native screen edge, hero + S*ATK_DIS); 4:3 unmasked
                {
                    s.heroX = s.camLeft + 100f; s.facingLeft = false; s.DebugCreateLaserBeam(); g.Step(1f / 60f); g.Step(1f / 60f);
                    float reach = N.S * s.CurrentWeapon().atkDis, want = Mathf.Max(s.camLeft + 1024f, s.heroX + reach), got = s.BeamClipRight(4);
                    check("r119f 16:9: laser ray clipped at the reach (right edge " + got + " vs " + want + ")", s.BeamClipActive(4) && Mathf.Abs(got - want) < 0.5f);
                    check("r119i 16:9 facing right: the mask keeps the whole visible strip left of the reach (left edge " + s.BeamClipLeft(4) + " <= " + (s.camLeft - e) + ")", s.BeamClipLeft(4) <= s.camLeft - e + 0.5f);
                    s.facingLeft = true; s.heroX = s.camLeft + 900f; g.Step(1f / 60f); g.Step(1f / 60f);
                    float wantL = Mathf.Max(s.camLeft - e, Mathf.Min(s.camLeft, s.heroX - reach));
                    check("r119i 16:9 facing left: clipped at the reach (left edge " + s.BeamClipLeft(4) + " vs " + wantL + "), right side open (" + s.BeamClipRight(4) + " >= " + (s.camLeft + 1024f + e) + ")",
                          Mathf.Abs(s.BeamClipLeft(4) - wantL) < 0.5f && s.BeamClipRight(4) >= s.camLeft + 1024f + e - 0.5f);
                    s.facingLeft = false;
                    WideView.SetExtraHalf(0f, s); g.Step(1f / 60f);
                    check("r119f 4:3: laser ray not masked (native)", !s.BeamClipActive(4));
                    WideView.Apply(cam, s, 1920, 1080); g.Step(1f / 60f);
                }

                // pause dim (full-width whiteFrame) stretches
                s.PauseGame(); g.Step(1f / 60f);
                var dim = s.GetControl(60);
                float dimW = dim != null ? dim.transform.localScale.x * dim.GetComponent<SpriteRenderer>().sprite.rect.width : 0f;
                check("16:9 battle: pause dim stretched to the widened view (" + dimW.ToString("0.0") + " vs " + (1024f + 2f * e).ToString("0.0") + ")", dim != null && Mathf.Abs(dimW - (1024f + 2f * e)) < 0.5f);
                var resume = s.GetControl(66);
                check("16:9 battle: pause menu items are not anchored (centred dialog)", resume != null && resume.wideAnchor == 0);
                Shot(cam, 1920, 1080, Path.Combine(output, "battle_16x9_paused.png"), null);
                g.StepFor(0.5f);   // children slide in
                resume = s.GetControl(66);
                if (resume != null) { s.Touch(0, resume.FullRect.center); s.Touch(2, resume.FullRect.center); }
                g.StepFor(0.5f);
                check("16:9 battle: the centred pause menu's resume button still works", !s.paused);
                string black = Shot(cam, 1920, 1080, Path.Combine(output, "battle_16x9.png"), true);
                check("16:9 battle capture: no all-black edge columns (" + black + ")", black == "ok");

                // 21:9 + another background (bg 5: period 1700 front layer)
                g.LaunchMission = new NativeMapState.Mission { type = 3, count = 20, level = 1, background = 5 }; g.LaunchMission.enemies.Add(1);
                g.SwitchScene("gameScene"); g.Step(0f); g.Step(1f / 60f);
                s = (GameScene)g.Current; s.spawningEnabled = false; s.ignorePlayer = true;
                WideView.Apply(cam, s, 2560, 1080); e = WideView.ExtraHalf; g.Step(1f / 60f);
                Coverage(check, s, e, "bg5 21:9");
                check("new battle scene re-anchors its HUD for the current width", Mathf.Abs(s.GetControl(4).transform.localPosition.x - (s.GetControl(4).FullRect.center.x + e)) < 1e-3f);
                black = Shot(cam, 2560, 1080, Path.Combine(output, "battle_21x9.png"), true);
                check("21:9 battle capture: no all-black edge columns (" + black + ")", black == "ok");

                // r119d (user item 4): storm darkness (lightning mask id 11 after maskWhite) and overcast at 21:9: captures for edge coverage
                GameScene.DebugWeatherRoll = () => 3;
                g.LaunchMission = new NativeMapState.Mission { type = 3, count = 200, level = 1, background = 0 };
                g.SwitchScene("gameScene"); g.Step(0f); g.Step(1f / 60f);
                s = (GameScene)g.Current; s.spawningEnabled = false; s.ignorePlayer = true;
                WideView.Apply(cam, s, 2560, 1080); g.Step(1f / 60f);
                Shot(cam, 2560, 1080, Path.Combine(output, "storm_overcast_21x9.png"), null);
                g.StepFor(s.ShanDianTimerForTest + 0.12f);
                g.StepFor(0.25f);
                var mask = s.GetControl(11);
                float maskW = mask != null ? mask.transform.localScale.x * mask.GetComponent<SpriteRenderer>().sprite.rect.width : 0f;
                log.AppendLine("SAMPLE storm mask id11 " + (mask != null) + " w " + maskW + " x " + (mask != null ? mask.transform.localPosition.x : 0f) + " anchor " + (mask != null ? mask.wideAnchor : 9) + " e " + WideView.ExtraHalf);
                check("21:9 storm: the darkness mask covers the widened view (" + maskW.ToString("0.0") + " vs " + (1024f + 2f * WideView.ExtraHalf).ToString("0.0") + ")", mask != null && Mathf.Abs(maskW - (1024f + 2f * WideView.ExtraHalf)) < 0.5f && Mathf.Abs(mask.transform.localPosition.x - 512f) < 0.5f);
                Shot(cam, 2560, 1080, Path.Combine(output, "storm_dark_21x9.png"), null);
                // same in a boss mission (lifeModeScene weather roll 0xf0244 also runs there), after the boss has entered
                g.LaunchMission = new NativeMapState.Mission { type = 8, count = 1, level = 1, background = 0 };
                g.SwitchScene("gameScene"); g.Step(0f); g.Step(1f / 60f);
                s = (GameScene)g.Current; s.spawningEnabled = false; s.ignorePlayer = true;
                WideView.Apply(cam, s, 2560, 1080); g.StepFor(2.5f);
                Shot(cam, 2560, 1080, Path.Combine(output, "boss_overcast_21x9.png"), null);
                g.StepFor(Mathf.Max(0f, s.ShanDianTimerForTest) + 0.37f);
                mask = s.GetControl(11);
                maskW = mask != null ? mask.transform.localScale.x * mask.GetComponent<SpriteRenderer>().sprite.rect.width : 0f;
                log.AppendLine("SAMPLE boss storm mask " + (mask != null) + " w " + maskW + " camLeft " + s.camLeft);
                Shot(cam, 2560, 1080, Path.Combine(output, "boss_dark_21x9.png"), null);
                GameScene.DebugWeatherRoll = null;

                // 5:4 (narrower): sides cropped, HUD pulled inwards
                WideView.Apply(cam, s, 1280, 1024);
                check("5:4 battle: height 768, negative extra pulls the HUD in (" + WideView.ExtraHalf.ToString("0.0") + ")", cam.orthographicSize == 384f && WideView.ExtraHalf < 0f && GameScene.FireRect.xMax <= 512f + 384f * 1.25f);

                // menu: scale to cover
                g.SwitchScene("menuScene"); g.Step(0f); g.Step(1f / 60f);
                WideView.Apply(cam, g.Current, 1920, 1080);
                check("16:9 menu: fit (native height 768 shown, extra width on the sides)", cam.orthographicSize == 384f && Mathf.Abs(WideView.ExtraHalf - (384f * 1920f / 1080f - 512f)) < 1e-3f && cam.rect == new Rect(0, 0, 1, 1));
                UiControl menuBg = null; foreach (var c in g.Current.Controls) if (c != null && c.FrameName == "ui_menu_bgFrame") menuBg = c;
                if (menuBg != null)
                {
                    float bw = menuBg.transform.localScale.x * menuBg.GetComponent<SpriteRenderer>().sprite.rect.width;
                    check("16:9 menu: red background widened over the view (" + bw.ToString("0.0") + ")", menuBg.FullRect.x > 0f || Mathf.Abs(bw - (menuBg.FullRect.width + 2f * WideView.ExtraHalf)) < 0.5f);
                }
                Shot(cam, 1920, 1080, Path.Combine(output, "menu_16x9.png"), null);
                check("r119e 16:9 menu: the area past the native screen is masked (title logo parked off screen stays hidden)", WideView.MenuMasksVisible);
                g.MapType = 4; g.SwitchScene("selectScene"); g.Step(0f); g.Step(1f / 60f);
                WideView.Apply(cam, g.Current, 1920, 1080);
                var iap = g.Current.GetControl(9001);
                float iapW = iap != null ? iap.transform.localScale.x * iap.GetComponent<SpriteRenderer>().sprite.rect.width : 0f;
                check("r119e 16:9 bank: ui_iap_bgFrame (cells painted in) keeps its native width (" + iapW.ToString("0.0") + ")", iap != null && Mathf.Abs(iapW - iap.FullRect.width) < 0.5f);
                Shot(cam, 1920, 1080, Path.Combine(output, "bank_16x9.png"), null);
                g.MapType = 0; g.SwitchScene("menuScene"); g.Step(0f); g.Step(1f / 60f); WideView.Apply(cam, g.Current, 1920, 1080);
                WideView.Apply(cam, g.Current, 1280, 1024);
                check("5:4 menu: cover (height 768 shown, sides cropped)", cam.orthographicSize == 384f);

                // off: native 4:3 box
                WideView.Enabled = false;
                WideView.Apply(cam, g.Current, 1920, 1080);
                check("off: 4:3 pillar-box, ortho 384, ExtraHalf 0", Mathf.Abs(cam.rect.width - 0.75f) < 1e-4f && cam.orthographicSize == 384f && WideView.ExtraHalf == 0f);
            }
            catch (Exception ex) { failed++; log.AppendLine("EXCEPTION " + ex); }
            finally
            {
                GameScene.DebugWeatherRoll = null; WideView.Enabled = wasEnabled; WideView.SetExtraHalf(0f, null);
                if (g != null) UnityEngine.Object.DestroyImmediate(g.gameObject);
                if (cam != null) UnityEngine.Object.DestroyImmediate(cam.gameObject);
                if (Directory.Exists(dir)) Directory.Delete(dir, true);
                log.AppendLine("SUMMARY passes=" + passed + " fails=" + failed);
                File.WriteAllText(Path.Combine(output, "verify.txt"), log.ToString()); Debug.Log(log.ToString());
                PilotExit.Exit(failed == 0 ? 0 : 1);
            }
        }

        /// <summary>Every background layer's native children + repeats, as drawn x intervals, cover [-e, 1024+e] with no gap.</summary>
        static void Coverage(Action<string, bool> check, GameScene s, float e, string tag)
        {
            var bg = s.Background; bool all = true; var why = new StringBuilder();
            foreach (var layer in bg.layers)
            {
                var spans = new System.Collections.Generic.List<Vector2>();
                Action<SpriteRenderer> add = r => { if (r == null || !r.enabled) return; float w = r.transform.localScale.x * r.sprite.rect.width, x = r.transform.localPosition.x; spans.Add(new Vector2(x - w * 0.5f, x + w * 0.5f)); };
                foreach (var r in layer.children) add(r);
                foreach (var copy in layer.repeats.Values) foreach (var r in copy) add(r);
                spans.Sort((a, b) => a.x.CompareTo(b.x));
                float reach = float.NegativeInfinity, need = -e, end = N.ScreenW + e; bool ok = spans.Count > 0 && spans[0].x <= need + 0.5f;
                foreach (var sp in spans) { if (reach != float.NegativeInfinity && sp.x > reach + 0.5f && reach < end) ok = false; reach = Mathf.Max(reach, sp.y); }
                if (reach < end - 0.5f) ok = false;
                if (!ok) { all = false; why.Append(" slot" + layer.definition.slot + " [" + (spans.Count > 0 ? spans[0].x : 0f) + ".." + reach + "]"); }
            }
            check(tag + ": every background layer covers [-e, 1024+e]" + why, all);
        }

        /// <summary>Renders at w x h; with checkEdges, reports whether the outermost pixel columns are entirely black (bars).</summary>
        static string Shot(Camera cam, int w, int h, string path, bool? checkEdges)
        {
            var rt = new RenderTexture(w, h, 24); var img = new Texture2D(w, h, TextureFormat.RGB24, false);
            try
            {
                cam.targetTexture = rt; Bootstrap.RenderAll(cam); RenderTexture.active = rt; img.ReadPixels(new Rect(0, 0, w, h), 0, 0); img.Apply();
                File.WriteAllBytes(path, img.EncodeToPNG());
                if (checkEdges != true) return "ok";
                foreach (int x in new[] { 0, w - 1 })
                {
                    bool allBlack = true;
                    for (int y = 0; y < h; y += 4) { var c = img.GetPixel(x, y); if (c.r + c.g + c.b > 0.03f) { allBlack = false; break; } }
                    if (allBlack) return "column " + x + " black";
                }
                return "ok";
            }
            finally { cam.targetTexture = null; RenderTexture.active = null; UnityEngine.Object.DestroyImmediate(rt); UnityEngine.Object.DestroyImmediate(img); }
        }
    }
}
