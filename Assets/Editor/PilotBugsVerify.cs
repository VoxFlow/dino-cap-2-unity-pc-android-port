using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>DC2-BUGS-001 (user-reported differences, Dev/Reports/DC2-BUGS-001/report.md): regressions with natively recovered expectations.
    /// B1 lives in PilotHouseVerify. B3: the EXP bar (sd_baiFrame) is tinted (137,236,149,255) by OnLoad (mainScene 0xa8382 / literal 0xa85bc,
    /// lifeModeScene literal 0xe7d2c).</summary>
    public static class PilotBugsVerify
    {
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-BUGS-001/bugs-r1");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            int passed = 0, failed = 0; var log = new StringBuilder();
            Action<string, bool> check = (n, v) => { log.AppendLine((v ? "PASS " : "FAIL ") + n); if (v) passed++; else failed++; };
            Camera cam = null; Dc2Game g = null;
            try
            {
                cam = Bootstrap.MakeCamera();
                g = Dc2Game.Create(Path.Combine(Path.GetTempPath(), "dc2_bugs_" + Guid.NewGuid().ToString("N"))); g.cam = cam; g.manualStep = true; g.SkipMissionIntro = true;
                var green = new Color32(137, 236, 149, 255);
                foreach (var m in new[] { new { type = 3, id = 47, name = "lifeModeScene" }, new { type = 1, id = 40, name = "mainScene" } })
                {
                    g.LaunchMission = new NativeMapState.Mission { type = m.type, count = m.type == 1 ? 1500f : 20f, level = 1, background = 0 }; g.LaunchMission.enemies.Add(1);
                    g.SwitchScene("gameScene"); g.Step(0f); g.Step(1f / 60f);
                    var bar = g.Current.GetControl(m.id);
                    var c = bar != null ? bar.CurrentColor : default(Color32);
                    check("B3 " + m.name + ": EXP bar id " + m.id + " is tinted (137,236,149,255), got (" + c.r + "," + c.g + "," + c.b + "," + c.a + ")",
                          bar != null && c.r == green.r && c.g == green.g && c.b == green.b && c.a == green.a);
                }

                // B4: DieNormal clears the advanced (BefiredAnim) layer (setAnimationAdv(nullptr) 0x12d1e) - a dinosaur killed during its hit reaction shows the die head
                g.LaunchMission = new NativeMapState.Mission { type = 3, count = 20, level = 1, background = 0 }; g.LaunchMission.enemies.Add(1);
                g.SwitchScene("gameScene"); g.Step(0f); g.Step(1f / 60f);
                var gs = (GameScene)g.Current; gs.spawningEnabled = false; gs.ignorePlayer = true;
                var dino = gs.CreateEnemy(1); g.Step(1f / 60f);
                dino.rig.SetAdvanced(dino.aBefired); dino.rig.PlayAdvanced(0, 0, 0.025f, -1); dino.advActive = true;   // as a BefiredAnim reaction leaves it
                gs.ApplyDamageForTest(dino); g.Step(1f / 60f);
                check("B4 DieNormal: advanced layer cleared (" + (dino.rig.AdvancedAnimation == null) + ") and the die animation plays (" + (dino.rig.animation == dino.aDie) + ")",
                      dino.rig.AdvancedAnimation == null && !dino.advActive && dino.rig.animation == dino.aDie);

                // B5: a gun hit (weapon type 1) calls dinosaurClass::Blood(0) (qx) from the reaction tail 0x15650 in every sampled row
                var d5 = gs.CreateEnemy(1); g.Step(1f / 60f); d5.state = 0;
                int fx0 = gs.FxSpawned;
                bool found = gs.ApplyReactionForTest(d5, Dc2GameData.Weapons()[1][0]);
                check("B5 gun hit: reaction found (" + found + ") and the qx blood effect is spawned (fx +" + (gs.FxSpawned - fx0) + ", last '" + gs.LastFxTag + "')",
                      found && gs.FxSpawned > fx0 && gs.LastFxTag == "blood");

                // B2 (r119): mainScene/lifeModeScene::InitBackGround prop y = 0.5*h + g_ground_y + k, with g_ground_y = -110 on iPad (DinoCap::Init 0x171c6, literal 0xC2DC0000).
                // Expected values are the iPad emulation of the ARM (derive_main_bg.py --ground-y -110 -> spec r119): theme 3 group A rock (beijing3_shitouFrame) y -4, tree
                // (beijing3_shu_3Frame) y 136. The r118 table (static g_ground_y -55, the iPhone value) had 51 / 191: every ground-relative prop 55 px too high.
                g.LaunchMission = new NativeMapState.Mission { type = 3, count = 20, level = 1, background = 3 }; g.LaunchMission.enemies.Add(1);
                g.SwitchScene("gameScene"); g.Step(0f); g.Step(1f / 60f);
                var g3 = (GameScene)g.Current; float rockY = float.NaN, treeY = float.NaN;
                for (int i = 0; i < g3.SceneryCount; i++)
                {
                    string n = g3.SceneryRenderer(i).gameObject.name;
                    if (float.IsNaN(rockY) && n == "scenery_beijing3_shitouFrame") rockY = g3.SceneryPosition(i).y;
                    if (float.IsNaN(treeY) && n == "scenery_beijing3_shu_3Frame") treeY = g3.SceneryPosition(i).y;
                }
                check("B2 bg 3 scenery on the iPad ground: rock y " + rockY + " (expect -4), tree y " + treeY + " (expect 136)", rockY == -4f && treeY == 136f);

                // ---- r119 user items ----
                g.LaunchMission = new NativeMapState.Mission { type = 3, count = 20, level = 1, background = 0 }; g.LaunchMission.enemies.Add(1);
                g.SwitchScene("gameScene"); g.Step(0f); g.Step(1f / 60f);
                var k = (GameScene)g.Current; k.spawningEnabled = false; k.ignorePlayer = true;
                var hpBar = k.GetControl(6);
                check("item 4: life-mode HP bar uses the shared mainScene::UpdateHP rect (161x14 -> 28 high), not the OnLoad placeholder 18 (" + (hpBar != null ? hpBar.FullRect.height : -1f) + ")",
                      hpBar != null && Mathf.Abs(hpBar.FullRect.height - 28f) < 1e-3f);
                UiLabel kl = null; foreach (var l in k.Labels) if (l.id == 9) kl = l;
                check("item 2: kill counter rect = kill icon x + S*30, icon y, S*200 x S*30 = (692,42,400,60) (got " + (kl != null ? kl.CurrentRect.ToString() : "none") + ")",
                      kl != null && kl.CurrentRect == new Rect(692f, 42f, 400f, 60f));
                int aFrames = 0, plain = 0;
                foreach (var sr in k.Background.LayerRoot(5).GetComponentsInChildren<SpriteRenderer>(true))
                {
                    if (sr.sprite == null || !sr.sprite.name.StartsWith("fangzi")) continue;
                    string n = sr.sprite.name;
                    if (n.Contains("_aFrame")) aFrames++;
                    else if (n == "fangzi1Frame" || n == "fangzi2Frame" || n == "fangzi3Frame" || n.EndsWith("_mdFrame")) plain++;
                }
                check("item 11: houses draw the _a building / door-frame frames (houseClass::OnLoad +108 = \"_a\"): " + aFrames + " _a, " + plain + " plain", aFrames > 0 && plain == 0);
                var hd = k.CreateEnemy(1); g.Step(1f / 60f);
                k.BeAttack(hd); float minB = 1f;
                for (int f = 0; f < 30; f++) { g.Step(1f / 60f); minB = Mathf.Min(minB, k.Rig.Player.brightness); }
                check("item 9: invincibility pulse (playerClass::OnUpdate 0xbb6cc): CA brightness falls below 1 while Wudi (min " + minB + ", >= 0.5)", k.invincible && minB < 1f && minB >= 0.5f);
                g.StepFor(2.0f);
                check("item 9: Wudi timeout resets the brightness to 1.0 (0xbb550)", !k.invincible && k.wudiBrightness == 1f);
                WideView.SetExtraHalf(170f, k);
                float hdMin = hd.rig.animation.minX, hdMax = hd.rig.animation.maxX;
                hd.x = k.camLeft + hdMin - 100f; bool wideHit = k.InViewportForTest(hd);
                WideView.SetExtraHalf(0f, k);
                bool nativeHit = k.InViewportForTest(hd);
                check("item 6: wide view widens the hittable window (dino 100 px beyond the native left extent: wide " + wideHit + ", native " + nativeHit + ")", wideHit && !nativeHit);
                // r119f item 6: native window = view + the current animation's extents (BeAttacked 0x1498a..0x149ba): a dino partly on screen at an edge is hittable
                hd.x = k.camLeft + hdMin + 2f; bool leftIn = k.InViewportForTest(hd);
                hd.x = k.camLeft + 1024f + hdMax - 2f; bool rightIn = k.InViewportForTest(hd);
                hd.x = k.camLeft + 1024f + hdMax + 2f; bool rightOut = k.InViewportForTest(hd);
                check("r119f 4:3: dinos partly visible at the left / right edges are hittable, beyond the extents not (minX " + hdMin + ", maxX " + hdMax + ")", leftIn && rightIn && !rightOut);
                g.QueueTouch(0, GameScene.RollRect.center, 130); g.HostTick(1f / 60f);
                check("item 8: a roll press is handled on the next 60 Hz host tick (rolling " + k.rolling + ")", k.rolling);
                g.QueueTouch(2, GameScene.RollRect.center, 130); g.HostTick(1f / 60f);

                // r119b: every classic hero dies with p_die + PlayerChangeHead(9 + type) and the failed screen follows (heroes 3 / 4 threw on pd_die / p4_die before)
                for (int ht = 0; ht < 4; ht++)
                {
                    g.Profile.playerType = ht; if (g.Profile.level[ht] < 0) g.Profile.level[ht] = 0;
                    g.LaunchMission = new NativeMapState.Mission { type = 3, count = 20, level = 1, background = 0 }; g.LaunchMission.enemies.Add(1);
                    g.SwitchScene("gameScene"); g.Step(0f); g.Step(1f / 60f);
                    var hs = (GameScene)g.Current; hs.spawningEnabled = false; hs.ignorePlayer = true;
                    var killer = hs.CreateEnemy(1); g.Step(1f / 60f);
                    string err = null;
                    try { hs.playerHp = 1f; hs.BeAttack(killer); g.StepFor(6f); } catch (Exception ex) { err = ex.Message; }
                    check("hero " + (ht + 1) + " death: die pose " + hs.Rig.Source + ", no exception (" + (err ?? "none") + "), failed screen (" + hs.result + ")",
                          err == null && hs.heroDead && hs.Rig.Source == "p_die" && hs.result == GameScene.MissionResult.Failed);
                }
                g.Profile.playerType = 0;

                // r119c: wide-view gun damage at the edge (native-edge falloff) and native reach; melee reach to the head sprite
                g.Profile.playerType = 0;
                g.LaunchMission = new NativeMapState.Mission { type = 3, count = 20, level = 1, background = 0 }; g.LaunchMission.enemies.Add(1);
                g.SwitchScene("gameScene"); g.Step(0f); g.Step(1f / 60f);
                var rs = (GameScene)g.Current; rs.spawningEnabled = false; rs.ignorePlayer = true;
                WideView.SetExtraHalf(400f, rs);
                var far = rs.CreateEnemy(1); g.Step(1f / 60f); far.state = 0; far.hp = 100000f;
                rs.heroX = rs.camLeft + 0.45f * GameScene.ViewW;   // hero mid-view (HeroCameraLeft facing right), no step in between
                far.x = rs.camLeft + GameScene.ViewW + 200f;   // 728 px: beyond the native view edge and the native zero-damage point (720), inside the reach (960)
                rs.facingLeft = false; bool facedRight = true; float hp0r = far.hp; rs.HeroAttackForTest();
                check("r119c wide view: a gun hit beyond the native edge (" + (far.x - rs.heroX) + " px) still does damage (" + (hp0r - far.hp) + ")", facedRight && far.hp < hp0r);
                far.x = rs.heroX + 990f; far.x = N.Add32(far.x, N.Add32(far.x, -rs.HeadXForTest(far))); hp0r = far.hp; rs.HeroAttackForTest();   // r119f: the reach is measured to the head (Attack 0xbf942): head at 990 px
                check("r119c wide view: beyond the native reach ATK_DIS*S (990 px) a gun does not hit", far.hp == hp0r);
                WideView.SetExtraHalf(0f, rs);
                var md = rs.CreateEnemy(1); g.Step(1f / 60f); md.state = 0; md.hp = 100000f;
                float headOff = rs.HeadXForTest(md) - md.x;
                check("r119c melee reach is measured to the head sprite (head offset " + headOff + ")", Mathf.Abs(headOff) > 0.5f);

                // B6 'red background' (r119c): selectScene::OnLoad creates ui_bg_allFrame for every select screen, the world map included
                g.MapType = 0; g.SwitchScene("selectScene"); g.Step(0f); g.Step(1f / 60f);
                var red = g.Current.GetControl(9100);
                check("r119c world map has the red ui_bg_allFrame background (id 9100)", red != null && red.FrameName == "ui_bg_allFrame");

                // B6: world-map (selectScene::showMapUI 0x10218c) EXP cover: w = S*95 - fill, h = S*19, fill = (int)(S * pct * 0.95)
                g.SwitchScene("selectScene"); g.Step(0f); g.Step(1f / 60f);
                var sel = g.Current as SelectScene;
                if (sel != null)
                {
                    float fill = (float)(int)(N.S * ((float)sel.ExpPercent * N.F(1064514355)));
                    var r = sel.ExpFillRect;
                    check("B6 showMapUI EXP cover: width S*95 - fill (" + r.width + " vs " + (N.F(0x42BE0000) * N.S - fill) + "), height S*19 (" + r.height + ")",
                          Mathf.Abs(r.width - (N.F(0x42BE0000) * N.S - fill)) < 1e-3f && Mathf.Abs(r.height - N.F(0x41980000) * N.S) < 1e-3f);
                }
                else check("B6: selectScene loaded", false);

                // r119d item 9: Force Knight's roll (p2_roll_1/2) gets PlayerChangeImage(10) + SetSpriteAnim("com8") at load; the raw files carry the Ninja images
                {
                    g.LaunchMission = new NativeMapState.Mission { type = 3, count = 20, level = 1, background = 0 }; g.LaunchMission.enemies.Add(1);
                    g.SwitchScene("gameScene"); g.Step(0f);
                    var ks = (GameScene)g.Current;
                    // every frame's part of that name (null when the frames disagree, "" when absent)
                    string Img(string part) { string r = ""; foreach (var fr in ks.Rig.Player.animation.frames) foreach (var pt in fr.parts) if (pt.name == part) { if (r == "") r = pt.reference; else if (r != pt.reference) return null; } return r; }
                    bool Anim(string part) { bool any = false; foreach (var fr in ks.Rig.Player.animation.frames) foreach (var pt in fr.parts) if (pt.name == part) { if (!pt.spriteAnimation) return false; any = true; } return any; }
                    ks.Rig.SelectNamedPose(1, "p2_roll_1", 0, 0.025f, new int[0]);
                    check("r119d Force Knight roll_1 uses its own images (com1 " + Img("com1") + ", com3 " + Img("com3") + ", com4 " + Img("com4") + ", com6 " + Img("com6") + ", com8 anim " + Anim("com8") + ")", Img("com1") == "tou0002" && Img("com2") == "shenti0002" && Img("com3") == "tui0002" && Img("com6") == "koushou0002" && Anim("com8"));
                    ks.Rig.SelectNamedPose(1, "p2_roll_2", 0, 0.025f, new int[0]);
                    check("r119d Force Knight roll_2 uses its own images", Img("com1") == "tou0002" && Img("com4") == "tui0002");
                    ks.Rig.SelectNamedPose(3, "p4_roll_1", 0, 0.025f, new int[0]);
                    check("r119d Ninja roll_1 is used as loaded (com1 " + Img("com1") + ")", Img("com1") == "tou0004");
                }

                // r119d item 14: the native stop rect (414,0,66,52)*S + (g_w_offset, g_h_offset - 32) covers the whole drawn pause icon
                {
                    g.LaunchMission = new NativeMapState.Mission { type = 3, count = 20, level = 1, background = 0 }; g.LaunchMission.enemies.Add(1);
                    g.SwitchScene("gameScene"); g.Step(0f); g.Step(1f / 60f);
                    var ps = (GameScene)g.Current; ps.spawningEnabled = false; ps.ignorePlayer = true;
                    var icon = ps.GetControl(15);   // r119m: lifeMode stop icon id 15 (0xe7e6a)
                    Rect ir = icon != null ? icon.FullRect : new Rect();
                    check("r119d stop touch rect is (892, 32, 132, 104) (" + GameScene.NativeStopRect + ")", GameScene.NativeStopRect == new Rect(892f, 32f, 132f, 104f));
                    check("r119d the drawn pause icon " + ir + " lies inside the stop touch rect", icon != null && GameScene.NativeTouchInside(GameScene.NativeStopRect, new Vector2(ir.x + 2f, ir.y + 2f)) && GameScene.NativeTouchInside(GameScene.NativeStopRect, new Vector2(ir.xMax - 2f, ir.yMax - 2f)));
                    ps.Touch(0, new Vector2(ir.x + 4f, ir.y + 4f)); ps.Touch(2, new Vector2(ir.x + 4f, ir.y + 4f)); g.Step(1f / 60f);
                    check("r119d a click on the icon's upper-left corner pauses", ps.paused);
                }

                // r119e item 8: the forced switch on an empty gun (AfterNoBullets 0xbaeac) must leave the rig exactly as a manual switch to the same slot
                foreach (int fh in new[] { 0, 1, 2, 3 })
                {
                    g.Profile.playerType = fh;
                    g.LaunchMission = new NativeMapState.Mission { type = 3, count = 20, level = 1, background = 0 }; g.LaunchMission.enemies.Add(1);
                    g.SwitchScene("gameScene"); g.Step(0f); g.StepFor(0.5f);
                    var fs = (GameScene)g.Current; fs.spawningEnabled = false; fs.ignorePlayer = true;
                    for (int wk = 0; wk < 5; wk++) { g.Profile.weaponOwned[fh * 6 + wk] = wk == 0 || wk == 1 ? 0 : -1; g.Profile.weaponAmmo[fh * 6 + wk] = 50; }   // only slots 0 and 1 owned: the forced switch from an empty slot 1 can only land on 0
                    string Sig() { var pl = fs.Rig.Player; var sb = new System.Text.StringBuilder(); sb.Append("scale " + pl.objectScale + " adv " + (pl.AdvancedAnimation != null) + " src " + fs.Rig.Source + " |");
                        foreach (var pt in pl.animation.frames[0].parts) sb.Append(" " + pt.name + ":" + pt.reference + ":" + pt.w.ToString("0.##") + "x" + pt.h.ToString("0.##"));
                        return sb.ToString(); }
                    typeof(GameScene).GetMethod("SelectWeaponSlot", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(fs, new object[] { 0 }); g.StepFor(0.3f);
                    string manual = Sig();
                    typeof(GameScene).GetMethod("SelectWeaponSlot", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(fs, new object[] { 1 }); g.StepFor(0.3f);
                    g.Profile.weaponAmmo[fh * 6 + 1] = 0;
                    fs.fireDown = true; g.Step(1f / 60f); fs.fireDown = false; g.StepFor(0.3f);
                    string forced = Sig();
                    log.AppendLine("SAMPLE hero " + fh + " slot after forced " + fs.weaponSlot + " | manual " + manual + " | forced " + forced);
                    check("r119e hero " + fh + ": the empty-ammo switch lands on slot 0 with the same rig as a manual switch", fs.weaponSlot == 0 && forced == manual);
                }

                // r119e item 9 probe: labels on the bank screen (garbled cash counter in captures)
                {
                    g.Profile.cash = 1234567; g.MapType = 4; g.SwitchScene("selectScene"); g.Step(0f); g.StepFor(1.5f);
                    foreach (var lb in g.Current.Labels) if (lb != null) log.AppendLine("SAMPLE bank label " + lb.id + " rect " + lb.CurrentRect + " text '" + lb.Text + "' glyphs " + lb.GlyphCount + " pos " + lb.transform.localPosition + " scale " + lb.transform.localScale);
                    g.MapType = 0;
                }

                // r119e item 11: leaving a mission for the map restarts the menu music (selectScene::OnLoad tail 0x10a1dc)
                {
                    int snd = g.Profile.sound; g.Profile.sound = 1;
                    g.LaunchMission = new NativeMapState.Mission { type = 3, count = 20, level = 1, background = 0 }; g.LaunchMission.enemies.Add(1);
                    g.SwitchScene("gameScene"); g.Step(0f); g.Step(1f / 60f);
                    g.Audio.LastMusicStarted = null;
                    g.Audio.Stop("game_bgAudio"); g.MapType = 0; g.SwitchScene("selectScene"); g.Step(0f); g.Step(1f / 60f);
                    check("r119e back on the map after a mission: menu music started (" + g.Audio.LastMusicStarted + ")", g.Audio.LastMusicStarted == "Music_menuAudio");
                    g.Profile.sound = snd;
                }

                // r119e item 12 probe: delay from the end trigger to the result banners (complete: band 0.2 + 0.05 s, then the banner; fail: die animation, then the line)
                {
                    g.LaunchMission = new NativeMapState.Mission { type = 3, count = 20, level = 1, background = 0 }; g.LaunchMission.enemies.Add(1);
                    g.SwitchScene("gameScene"); g.Step(0f); g.StepFor(0.5f);
                    var rs2 = (GameScene)g.Current; rs2.spawningEnabled = false; rs2.ignorePlayer = true;
                    rs2.Complete(); int ticks = 0; while (rs2.GetControl(31) == null && ticks < 600) { g.Step(1f / 60f); ticks++; }
                    log.AppendLine("SAMPLE complete -> banner 31 after " + ticks + " ticks (" + (ticks / 60f).ToString("0.000") + " s)");
                    check("r119e mission complete banner appears 0.25 s after GameClear (" + ticks + " ticks)", ticks >= 14 && ticks <= 17);
                    g.LaunchMission = new NativeMapState.Mission { type = 3, count = 20, level = 1, background = 0 }; g.LaunchMission.enemies.Add(1);
                    g.SwitchScene("gameScene"); g.Step(0f); g.StepFor(0.5f);
                    var fs2 = (GameScene)g.Current; fs2.spawningEnabled = false; fs2.ignorePlayer = true;
                    fs2.DebugKillHero(); int t17 = -1, t29 = -1; for (int dk = 0; dk < 900 && t29 < 0; dk++) { g.Step(1f / 60f); if (t17 < 0 && fs2.GetControl(17) != null) t17 = dk + 1; if (fs2.GetControl(29) != null) t29 = dk + 1; }
                    log.AppendLine("SAMPLE death -> fail line 17 after " + t17 + " ticks, banner 29 after " + t29 + " ticks; die frames " + fs2.Rig.Player.animation.frames.Length);
                }

                // r119f item 4: Force Knight melee sounds (JinAttack 0xbe542: subtype 4 -> p2hitAudio, A_hit_tie_guangjian when a struck dino has SURFACE > 100)
                {
                    g.Profile.playerType = 1;
                    var kinds = Dc2GameData.Dinos(); int armoredKind = -1, basicKind = -1;
                    for (int k2 = 0; k2 < kinds.Length; k2++) { if (kinds[k2].surface > 100 && armoredKind < 0) armoredKind = k2 + 1; if (kinds[k2].surface <= 100 && basicKind < 0) basicKind = k2 + 1; }
                    foreach (int kind in new[] { 0, armoredKind, basicKind })
                    {
                        g.LaunchMission = new NativeMapState.Mission { type = 3, count = 20, level = 1, background = 0 }; g.LaunchMission.enemies.Add(1);
                        g.SwitchScene("gameScene"); g.Step(0f); g.StepFor(0.5f);
                        var ks2 = (GameScene)g.Current; ks2.spawningEnabled = false; ks2.ignorePlayer = true; ks2.facingLeft = false;
                        if (kind > 0) { var dd = ks2.CreateEnemy(kind); dd.x = ks2.heroX + 160f; dd.hp = 100000f; dd.state = 0; dd.targetable = true; dd.velocityX = 0f; g.Step(1f / 60f); }
                        int before = g.Audio.RequestedSounds.Count; ks2.JinAttackForTest();
                        string snd = g.Audio.RequestedSounds.Count > before ? string.Join(",", g.Audio.RequestedSounds.GetRange(before, g.Audio.RequestedSounds.Count - before)) : "none";
                        log.AppendLine("SAMPLE FK melee kind " + kind + " surface " + (kind > 0 ? kinds[kind - 1].surface : -1) + " -> " + snd);
                        string want = kind == armoredKind ? "A_hit_tie_guangjian" : "p2hitAudio";
                        check("r119f Force Knight melee " + (kind == 0 ? "into air" : "on kind " + kind) + " plays " + want + " (" + snd + ")", snd.Contains(want));
                    }
                }

                // r119f item 5: Force Knight, pistol (slot 0) empties while firing -> forced switch to slot 1, compared with a manual switch to slot 1
                foreach (bool holdFire in new[] { false, true })
                {
                    g.Profile.playerType = 1;
                    g.LaunchMission = new NativeMapState.Mission { type = 3, count = 20, level = 1, background = 0 }; g.LaunchMission.enemies.Add(1);
                    g.SwitchScene("gameScene"); g.Step(0f); g.StepFor(0.5f);
                    var f5 = (GameScene)g.Current; f5.spawningEnabled = false; f5.ignorePlayer = true;
                    for (int wk = 0; wk < 5; wk++) { g.Profile.weaponOwned[6 + wk] = wk <= 1 ? 0 : -1; g.Profile.weaponAmmo[6 + wk] = 50; }
                    var sel5 = typeof(GameScene).GetMethod("SelectWeaponSlot", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    string Sig5() { var pl = f5.Rig.Player; var sb = new System.Text.StringBuilder(); sb.Append("scale " + pl.objectScale + " src " + f5.Rig.Source + " adv " + (pl.AdvancedAnimation != null ? pl.AdvancedAnimation.frames.Length.ToString() : "-") + " |");
                        foreach (var pt in pl.animation.frames[0].parts) sb.Append(" " + pt.name + ":" + pt.reference + ":" + pt.w.ToString("0.##") + "x" + pt.h.ToString("0.##"));
                        return sb.ToString(); }
                    sel5.Invoke(f5, new object[] { 1 }); g.StepFor(0.5f); string manual = Sig5();
                    sel5.Invoke(f5, new object[] { 0 }); g.StepFor(0.5f);
                    g.Profile.weaponAmmo[6] = holdFire ? 1 : 0;   // holdFire: the last pistol round is fired first, then the next press finds the gun empty
                    f5.fireDown = true; g.StepFor(holdFire ? 1.5f : 1f / 60f); f5.fireDown = false; g.StepFor(1.0f);
                    if (holdFire) log.AppendLine("SAMPLE FK after held fire: slot " + f5.weaponSlot + " ammo " + g.Profile.weaponAmmo[6] + " state " + f5.heroControlState + " firing " + f5.firing + " fireDown " + f5.fireDown + " src " + f5.Rig.Source);
                    if (holdFire && f5.weaponSlot == 0) { f5.fireDown = true; g.Step(1f / 60f); log.AppendLine("SAMPLE FK tap after empty: slot " + f5.weaponSlot + " state " + f5.heroControlState + " firing " + f5.firing); f5.fireDown = false; g.StepFor(1.0f); }
                    string forced = Sig5();
                    log.AppendLine("SAMPLE FK " + (holdFire ? "last round, then press" : "tap") + " slot " + f5.weaponSlot + " | manual " + manual + " | forced " + forced);
                    check("r119f Force Knight pistol empties (" + (holdFire ? "last round, then press" : "tap") + "): forced switch to slot 1 matches a manual switch", f5.weaponSlot == 1 && forced == manual);
                }

                // r119f item 8: rage face (frame suffix) on a bite: cleared only while firing (playerClass::BeAttack 0xbd196..0xbd1ac)
                foreach (bool firingNow in new[] { true, false })
                {
                    g.Profile.playerType = 0;
                    g.LaunchMission = new NativeMapState.Mission { type = 3, count = 20, level = 1, background = 0 }; g.LaunchMission.enemies.Add(1);
                    g.SwitchScene("gameScene"); g.Step(0f); g.StepFor(0.5f);
                    var r8 = (GameScene)g.Current; r8.spawningEnabled = false;
                    var biter = r8.CreateEnemy(1); g.Step(1f / 60f); biter.x = r8.heroX + 100f;
                    var appField = typeof(GameScene).GetField("shotApp", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    ((NativeShotAppearance)appField.GetValue(r8)).Suffix = "_nu2"; r8.Rig.Player.FrameSuffix = "_nu2"; r8.firing = firingNow;
                    r8.BeAttack(biter);
                    check("r119f bite while " + (firingNow ? "firing clears" : "not firing keeps") + " the rage face (suffix '" + r8.ShotSuffix + "')", firingNow ? r8.ShotSuffix == "" : r8.ShotSuffix == "_nu2");
                }

                // r119d item 6: lifeModeScene::missionComplete 0xe18fa clears the boss point after a completed boss map and draws the next seed (+4..6 missions)
                {
                    var ms = g.MapState ?? (g.MapState = new NativeMapState());
                    ms.ReadBoss(g.SaveDir, g.Profile.mission);
                    ms.boss.active = true; ms.boss.point = new NativeMapState.Point { bg = 0, x = 123f, y = 45f }; ms.SaveBoss(g.SaveDir);
                    int poolBefore = ms.positions.Count, m0 = g.Profile.mission;
                    g.LaunchMission = new NativeMapState.Mission { type = 8, count = 1, level = 1, background = 0 };
                    g.SwitchScene("gameScene"); g.Step(0f);
                    var bsc = (GameScene)g.Current; bsc.spawningEnabled = false;
                    typeof(GameScene).GetMethod("MissionCompleteData", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(bsc, null);
                    ms = g.MapState; var saved = new NativeMapState(); saved.ReadBoss(g.SaveDir, g.Profile.mission);
                    check("r119d boss win: boss point inactive and saved (seed " + ms.boss.seed + ", mission " + m0 + ")", !ms.boss.active && !saved.boss.active && saved.boss.seed == ms.boss.seed);
                    check("r119d boss win: next boss seed = mission + RandomRange(4,6)", ms.boss.seed >= m0 + 4 && ms.boss.seed <= m0 + 6);
                    check("r119d boss win: the boss point returns to the position pool", ms.positions.Count == poolBefore + 1 && ms.positions[ms.positions.Count - 1].x == 123f);
                }
                // r119i item 2 (user: "heroes can't harm pterodactyls"): the XML-named CA loader computes the CombineAnimation extents too (0xc60a..0xc640), so a K5
                // pterodactyl is inside the BeAttacked viewport gate and takes a pistol shot
                if (g != null)
                {
                    g.Profile.playerType = 0;
                    g.LaunchMission = new NativeMapState.Mission { type = 3, count = 20, level = 1, background = 0 }; g.LaunchMission.enemies.Add(1);
                    g.SwitchScene("gameScene"); g.Step(0f); g.Step(1f / 60f);
                    var sp = (GameScene)g.Current; sp.spawningEnabled = false; sp.ignorePlayer = true; sp.DebugSpawnModeRoll = () => 99;
                    int k5 = Array.FindIndex(Dc2GameData.Dinos(), p => p.skin == 5) + 1;
                    sp.heroX = sp.camLeft + 300f; sp.facingLeft = false;
                    var pd = sp.CreateEnemy(k5); pd.hp = 10000f; pd.x = sp.heroX + 400f; g.Step(1f / 60f);
                    float pMin = pd.rig.animation.minX, pMax = pd.rig.animation.maxX, php = pd.hp;
                    sp.fireDown = true; g.StepFor(0.6f); sp.fireDown = false;
                    check("r119i pterodactyl (kind " + k5 + ") extents " + pMin + ".." + pMax + " computed, in view, pistol hits (hp " + php + " -> " + pd.hp + ")", pMin < pMax && sp.InViewportForTest(pd) && pd.hp < php);
                }
                // r119i item 3 (user decision): a debug level-up unlocks the level weapons on the next menu load (DEBUG_ONLY); an ordinary level gain still waits for
                // mission complete (AwardItem 0xd09dc, native)
                if (g != null)
                {
                    int dh = 1; g.Profile.playerType = dh; g.Profile.level[dh] = 3; g.Profile.exp[dh] = 0;
                    for (int sl = 1; sl <= 4; sl++) g.Profile.weaponOwned[dh * 6 + sl] = -1;
                    g.LaunchMission = new NativeMapState.Mission { type = 3, count = 20, level = 1, background = 0 }; g.LaunchMission.enemies.Add(1);
                    g.SwitchScene("gameScene"); g.Step(0f); g.Step(1f / 60f);
                    var ds = (GameScene)g.Current; ds.spawningEnabled = false; ds.ignorePlayer = true;
                    ds.DebugLevelUp(); g.Step(1f / 60f);
                    bool before = g.Profile.weaponOwned[dh * 6 + 1] < 0;
                    g.MapType = 2; g.SwitchScene("selectScene"); g.Step(0f); g.Step(1f / 60f);
                    check("r119i debug level-up (display level " + (g.Profile.level[dh] + 1) + "): slot 1 locked in battle, unlocked on the next menu load", before && g.Profile.weaponOwned[dh * 6 + 1] == 0 && g.Profile.weaponOwned[dh * 6 + 2] < 0);
                    g.Profile.level[dh] = 9; g.MapType = 1; g.SwitchScene("selectScene"); g.Step(0f); g.Step(1f / 60f);
                    check("r119i a level gained without the debug button does not unlock on menu load (native: mission complete only)", g.Profile.weaponOwned[dh * 6 + 2] < 0);
                    var dbgHost = new GameObject("dbg_probe"); DebugMenu.Install(dbgHost, g); var dbg = dbgHost.GetComponent<DebugMenu>();
                    g.Profile.level[dh] = 8; g.Profile.playerType = dh; dbg.AddHeroLevel(1);   // menu "+1" -> display level 10
                    g.MapType = 1; g.SwitchScene("selectScene"); g.Step(0f); g.Step(1f / 60f);   // open Armory
                    check("r119j menu debug '+1' (display level " + (g.Profile.level[dh] + 1) + ") unlocks slot 2 on the next tab load", g.Profile.weaponOwned[dh * 6 + 2] == 0);
                    UnityEngine.Object.DestroyImmediate(dbgHost);
                }
                // r119j item 2: held flame stays on frame 5 / held laser loops 4..6 (effectClass::OnAnimationEnd 0x16af0, +68 = current frame); a dinosaur killed while
                // burning takes its Dahuo with it (dinosaurClass::Disappear 0x119f6)
                if (g != null)
                {
                    foreach (int sub in new[] { 1, 2 })
                    {
                        int fh = -1, fs = -1;
                        for (int hh = 0; hh <= 3 && fh < 0; hh++) { var ws = Dc2GameData.PlayerWeapons(hh); for (int wk2 = 0; wk2 < 5 && wk2 < ws.Count; wk2++) if (ws[wk2].type == 5 && ws[wk2].subtype == sub) { fh = hh; fs = wk2; break; } }
                        if (fh < 0) { check("r119j hero with a type-5 sub-" + sub + " weapon exists", false); continue; }
                        g.Profile.playerType = fh;
                        for (int wk3 = 0; wk3 < 6; wk3++) { g.Profile.weaponOwned[fh * 6 + wk3] = wk3 == fs || wk3 == 5 ? 0 : -1; g.Profile.weaponAmmo[fh * 6 + wk3] = 200; }
                        g.LaunchMission = new NativeMapState.Mission { type = 3, count = 20, level = 1, background = 0 }; g.LaunchMission.enemies.Add(1);
                        g.SwitchScene("gameScene"); g.Step(0f); g.Step(1f / 60f);
                        var fsc = (GameScene)g.Current; fsc.spawningEnabled = false; fsc.ignorePlayer = true; fsc.DebugSpawnModeRoll = () => 99;
                        typeof(GameScene).GetMethod("SelectWeaponSlot", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(fsc, new object[] { fs }); g.StepFor(0.4f);
                        int mode = sub == 1 ? 5 : 4; var frames = new System.Collections.Generic.List<int>(); bool hidden = false;
                        fsc.fireDown = true; g.StepFor(0.5f);
                        for (int t = 0; t < 90; t++) { g.Step(1f / 60f); int fr = fsc.BeamFrameForTest(mode); if (fr < 0) hidden = true; else if (!frames.Contains(fr)) frames.Add(fr); }
                        fsc.fireDown = false;
                        bool ok = !hidden && (mode == 5 ? frames.Count == 1 && frames[0] == 5 : frames.TrueForAll(x => x >= 4 && x <= 6));
                        check("r119j held " + (mode == 5 ? "flame stays visible on frame 5" : "laser stays visible looping frames 4..6") + " for 1.5 s (frames seen " + string.Join(",", frames) + ", hidden " + hidden + ")", ok);
                        if (mode == 4)
                        {   // r119j2 item 1: a dinosaur in the held laser loops BEAnim frames 2..4 and takes the laser record on every key-frame-3 pass (OnAnimationEnd 0x11ad6 case 3)
                            var ld = fsc.CreateEnemy(1); ld.hp = 100000f; ld.x = fsc.heroX + 200f; ld.state = 0; ld.velocityX = 0f; ld.targetable = true; fsc.facingLeft = false;
                            int hits0 = fsc.hitDino; var lframes = new System.Collections.Generic.List<int>();
                            fsc.fireDown = true; g.StepFor(0.6f);
                            for (int t = 0; t < 120; t++) { g.Step(1f / 60f); if (ld.state == 4 && !lframes.Contains(ld.rig.CurrentFrame)) lframes.Add(ld.rig.CurrentFrame); }
                            fsc.fireDown = false; lframes.Sort();
                            check("r119j2 held laser: dinosaur twitch loops BEAnim frames (" + string.Join(",", lframes) + ") and is hit repeatedly (" + (fsc.hitDino - hits0) + " hits in 2.6 s, state " + ld.state + ")", lframes.Contains(2) && lframes.Contains(4) && fsc.hitDino - hits0 >= 4);
                        }
                        if (mode == 5)
                        {
                            var bd = fsc.CreateEnemy(1); bd.hp = 100000f; bd.x = fsc.heroX + 200f; bd.state = 0; bd.velocityX = 0f; bd.targetable = true; fsc.facingLeft = false;
                            fsc.fireDown = true; g.StepFor(1f); int burning = fsc.BurnFxCountForTest(); log.AppendLine("INFO burn fixture: dino state " + bd.state + " hp " + bd.hp + " x " + bd.x + " hero " + fsc.heroX + " flame frame " + fsc.BeamFrameForTest(5) + " firing " + fsc.firing + " slot " + fsc.weaponSlot + " inView " + fsc.InViewportForTest(bd));
                            bd.hp = 0.5f; g.StepFor(4f); fsc.fireDown = false; g.StepFor(3f);
                            check("r119j burning dinosaur killed by the flame: Dahuo shown while burning (" + burning + "), none left after the dinosaur is gone (" + fsc.BurnFxCountForTest() + ", dino alive " + fsc.dinos.Contains(bd) + ")", burning == 1 && !fsc.dinos.Contains(bd) && fsc.BurnFxCountForTest() == 0);
                        }
                    }
                }
                // r119j item 4: after the Endurance run end the roll is refused (OnRollDown state-4 gate); the hero stops S*50 past the native edge (4:3) or past the
                // visible edge in wide view (ADAPTATION)
                if (g != null)
                {
                    foreach (float wideE in new[] { 0f, 384f * 1920f / 1080f - 512f })
                    {
                        g.Profile.playerType = 0;
                        g.LaunchMission = new NativeMapState.Mission { type = 1, count = 1500, level = 1, background = 0 };
                        g.SwitchScene("gameScene"); g.Step(0f); g.Step(1f / 60f);
                        var runS = (GameScene)g.Current; runS.spawningEnabled = false; runS.ignorePlayer = true;
                        WideView.SetExtraHalf(wideE, runS);
                        runS.DebugTeleportNearRunEnd(); runS.rightDown = true;
                        for (int t = 0; t < 600 && !runS.runEnded; t++) g.Step(1f / 60f);
                        runS.rightDown = false; g.StepFor(0.5f);
                        bool rolledBefore = runS.rolling; runS.OnRollDown(); g.Step(1f / 60f);
                        check("r119j run end (extra " + wideE.ToString("0.0") + "): S roll refused (state " + runS.heroControlState + ", rolling " + runS.rolling + ")", runS.runEnded && !rolledBefore && !runS.rolling);
                        g.StepFor(8f);
                        float stop = runS.camLeft + 1024f + N.S * 50f + wideE;
                        check("r119j run end (extra " + wideE.ToString("0.0") + "): hero stops just past x " + stop + " (hero " + runS.heroX + ", cam " + runS.camLeft + ")", runS.heroX > stop && runS.heroX < stop + 40f);
                        WideView.SetExtraHalf(0f, runS);
                    }
                }
                // r119r (user, supersedes r119k items 4/5): egg missions keep the native start, delivery line and positions; the wide camera stops where its
                // visible edge meets the native bounds, so the truck and the nest sit on the visible edges
                if (g != null)
                {
                    foreach (float wideE in new[] { 0f, 384f * 1920f / 1080f - 512f })
                    {
                        WideView.SetExtraHalf(wideE, null);
                        g.Profile.playerType = 0;
                        g.LaunchMission = new NativeMapState.Mission { type = 5, count = 1500, level = 1, background = 0, info = 3 };
                        g.SwitchScene("gameScene"); g.Step(0f); g.Step(1f / 60f);
                        var es = (GameScene)g.Current; es.spawningEnabled = false; es.ignorePlayer = true;
                        float start = es.heroX;
                        check("r119r egg start (extra " + wideE.ToString("0.0") + "): hero x " + start + " = native " + GameScene.NativeHeroSpawn(0).x, Mathf.Abs(start - GameScene.NativeHeroSpawn(0).x) < 0.01f);
                        check("r119r egg camera at the start (extra " + wideE.ToString("0.0") + "): visible left edge " + (es.camLeft - wideE) + " = native left bound " + es.BoundLower, Mathf.Abs(es.camLeft - wideE - es.BoundLower) < 0.01f);
                        es.EggPlaceHeroAtEggForTest(); g.StepFor(0.3f);
                        int d0 = es.EggDelivered; float line = N.S * 150f + es.BoundLower;
                        es.heroX = line + 5f; g.StepFor(0.2f); bool early = es.EggDelivered > d0;
                        es.heroX = line - 5f; g.StepFor(0.2f);
                        check("r119r egg delivery line, native (extra " + wideE.ToString("0.0") + ") at x " + line + ": not delivered 5 px right of it, delivered 5 px left", !early && es.EggDelivered == d0 + 1);
                        es.heroX = es.BoundUpper - 10f; g.StepFor(0.3f);
                        check("r119r egg camera at the nest end (extra " + wideE.ToString("0.0") + "): visible right edge " + (es.camLeft + 1024f + wideE) + " = native right bound " + es.BoundUpper + " (nest S*50 inside it, as on the iPad)", Mathf.Abs(es.camLeft + 1024f + wideE - es.BoundUpper) < 0.01f);
                    }
                    WideView.SetExtraHalf(0f, null);
                }
                // r119u (user decision "inset for notch"): edge HUD touch rects move inward by the uncovered safe-area inset; none at E = 0
                {
                    WideView.SetExtraHalf(321f, null); WideView.SetSafeInsets(40f, 30f, null);
                    bool okL = Mathf.Abs(GameScene.LeftRect.x - (GameScene.NativeLeftRect.x - 321f + 40f)) < 0.01f, okR = Mathf.Abs(GameScene.FireRect.x - (GameScene.NativeFireRect.x + 321f - 30f)) < 0.01f;
                    WideView.SetExtraHalf(0f, null);
                    bool okZ = GameScene.LeftRect == GameScene.NativeLeftRect && GameScene.FireRect == GameScene.NativeFireRect;
                    WideView.SetSafeInsets(0f, 0f, null);
                    check("r119u notch inset: left rect x " + (GameScene.NativeLeftRect.x - 321f + 40f) + ", fire rect x " + (GameScene.NativeFireRect.x + 321f - 30f) + " at E 321 / insets 40, 30; native at E 0", okL && okR && okZ);
                }
                // r119t (user decision "mirror it"): arena / boss right hero clamp = native bound + extra width (0 extra = native 840)
                if (g != null)
                {
                    foreach (float wideE in new[] { 0f, 384f * 1920f / 1080f - 512f })
                    {
                        WideView.SetExtraHalf(wideE, null);
                        g.Profile.playerType = 0; g.LaunchMission = new NativeMapState.Mission { type = 8, count = 1, level = 1, background = 0 };
                        g.SwitchScene("gameScene"); g.StepFor(0.02f);
                        var rcs = (GameScene)g.Current; rcs.spawningEnabled = false; rcs.ignorePlayer = true;
                        rcs.heroX = 2000f; g.StepFor(0.1f);
                        check("r119t boss right clamp (extra " + wideE.ToString("0.0") + "): hero x " + rcs.heroX + " = 840 + extra; visible right edge " + (rcs.camLeft + 1024f + wideE), Mathf.Abs(rcs.heroX - (840f + wideE)) < 0.01f && Mathf.Abs(rcs.camLeft + 1024f + wideE - rcs.heroX) < 0.01f);
                    }
                    WideView.SetExtraHalf(0f, null);
                }
                // r119k item 8 (user decision, ADAPTATION): wide view capped at 21:9 (black bars beyond); the hou / wei boss stands outward by the extra width
                if (g != null && cam != null)
                {
                    bool wasWide = WideView.Enabled; WideView.Enabled = true;
                    g.Profile.playerType = 0; g.LaunchMission = new NativeMapState.Mission { type = 8, count = 1, level = 3, background = 0 };
                    g.SwitchScene("gameScene"); g.StepFor(0.02f);
                    var bs = (GameScene)g.Current; bs.spawningEnabled = false; bs.ignorePlayer = true; bs.DebugBossKindRoll = () => 1;
                    WideView.Apply(cam, bs, 4800, 900);   // 48:9
                    float capE = 384f * WideView.MaxAspect - 512f;
                    check("r119k 48:9: wide view capped at 21:9 (extra " + WideView.ExtraHalf + " vs " + capE + ", viewport width " + cam.rect.width + ")", Mathf.Abs(WideView.ExtraHalf - capE) < 0.01f && Mathf.Abs(cam.rect.width - WideView.MaxAspect / (48f / 9f)) < 1e-4f);
                    WideView.Apply(cam, bs, 1920, 1080); float e169 = WideView.ExtraHalf;
                    g.StepFor(2.3f);
                    var hb = bs.Boss;
                    float wantX = hb != null ? (hb.left ? -120f * N.S - e169 : 380f * N.S + e169) : float.NaN;
                    check("r119k 16:9 hou boss stands outward by the extra width (x " + (hb != null ? hb.x : float.NaN) + " vs " + wantX + ")", hb != null && hb.kind == 1 && Mathf.Abs(hb.x - wantX) < 0.01f);
                    WideView.SetExtraHalf(0f, bs); WideView.Enabled = wasWide;
                }
                // r119l (user repro): weapon switches during the Mission Complete start-up must not turn the complete band (new control 20) into the ammo icon
                if (g != null)
                {
                    g.Profile.playerType = 4; if (g.Profile.level[4] < 0) g.Profile.level[4] = 0;
                    for (int wk4 = 0; wk4 < 6; wk4++) { g.Profile.weaponOwned[4 * 6 + wk4] = 0; g.Profile.weaponAmmo[4 * 6 + wk4] = 300; }
                    g.LaunchMission = new NativeMapState.Mission { type = 3, count = 20, level = 1, background = 0 }; g.LaunchMission.enemies.Add(1);
                    g.SwitchScene("gameScene"); g.Step(0f); g.Step(1f / 60f);
                    var cs = (GameScene)g.Current; cs.spawningEnabled = false; cs.ignorePlayer = true;
                    g.StepFor(0.5f);
                    cs.Complete(); g.Step(1f / 60f);
                    var band = cs.GetControl(20); string bandFrame = band != null ? band.FrameName : "-";
                    Rect bandRect = new Rect(0f, N.Add32(N.Mul32(N.S, 118f), 0.5f * N.HOffset), N.Add32(N.Mul32(N.S, 480f), N.WOffset), N.Mul32(N.S, 82f));   // GameClear band final rect
                    var cp = GameScene.ChangeRect.center;
                    for (int t = 0; t < 60; t++) { if (t % 3 == 0) cs.Touch(0, cp, 9); else if (t % 3 == 1) cs.Touch(2, cp, 9); cs.fireDown = t % 2 == 0; g.Step(1f / 60f); } g.StepFor(0.3f);
                    var after = cs.GetControl(20);
                    check("r119l switches after Complete leave the band alone (frame " + bandFrame + " -> " + (after != null ? after.FrameName : "-") + ", rect " + bandRect + " -> " + (after != null ? after.FullRect.ToString() : "-") + ")",
                          after == band && after != null && after.FrameName == bandFrame && after.FullRect == bandRect);
                    var icon8 = cs.GetControl(8);
                    check("r119m ammo icon is control 8 (layer 2, lifeModeScene::OnLoad 0xe7b5c), still shown after Complete and back at its rect (" + (icon8 != null ? icon8.FrameName + " " + icon8.FullRect : "-") + ")",
                          icon8 != null && icon8.FrameName != null && icon8.FrameName.StartsWith("icon_bullet") && icon8.FullRect == N.Layout(0, 1, 61, 54));
                }
            }
            catch (Exception e) { failed++; log.AppendLine("EXCEPTION " + e); }
            finally
            {
                if (g != null) UnityEngine.Object.DestroyImmediate(g.gameObject);
                if (cam != null) UnityEngine.Object.DestroyImmediate(cam.gameObject);
                log.AppendLine("SUMMARY passes=" + passed + " fails=" + failed);
                File.WriteAllText(Path.Combine(output, "verify.txt"), log.ToString()); Debug.Log(log.ToString());
                PilotExit.Exit(failed == 0 ? 0 : 1);
            }
        }
    }
}
