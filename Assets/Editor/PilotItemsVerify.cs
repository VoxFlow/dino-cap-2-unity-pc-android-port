using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>itemsClass drops (arena/boss only), ammo tiers, HP kit, bounds (Dev/Evidence/items_decomp_r107.md).</summary>
    public static class PilotItemsVerify
    {
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-PILOT-001/items-r1");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            string dir = Path.Combine(Path.GetTempPath(), "dc2_items_verify_" + Guid.NewGuid().ToString("N"));
            int passed = 0, failed = 0; var log = new StringBuilder();
            Action<string, bool> check = (n, v) => { log.AppendLine((v ? "PASS " : "FAIL ") + n); if (v) passed++; else failed++; };
            Dc2Game game = null; Camera cam = null; RenderTexture rt = null; Texture2D img = null;
            try
            {
                cam = Bootstrap.MakeCamera(); game = Dc2Game.Create(dir); game.manualStep = true; game.SkipMissionIntro = true; game.cam = cam;
                game.LaunchMission = new NativeMapState.Mission { type = 7, count = 60, level = 1, background = 0 };
                rt = new RenderTexture(1024, 768, 24); img = new Texture2D(1024, 768, TextureFormat.RGB24, false);
                Action<string> shot = name => { cam.targetTexture = rt; Bootstrap.RenderAll(cam); RenderTexture.active = rt; img.ReadPixels(new Rect(0, 0, 1024, 768), 0, 0); img.Apply(); File.WriteAllBytes(Path.Combine(output, name), img.EncodeToPNG()); cam.targetTexture = null; RenderTexture.active = null; };
                game.Profile.playerType = 0;
                game.SwitchScene("gameScene"); game.StepFor(0.02f);
                var scene = (GameScene)game.Current; scene.spawningEnabled = false; game.StepFor(0.1f);
                check("frames for the items exist in the atlas", Atlas.Has("Lbullet0Frame") && Atlas.Has("LxuepingFrame") && Atlas.Has("L07Frame") && Atlas.Has("LaddhpFrame"));
                // bounds: arena -240..840, margin S*20
                scene.DropItemForTest(-241f); scene.DropItemForTest(839f + 10f);
                check("drops outside [left+S*20, right-S*20] are refused", scene.ItemCount == 0);
                scene.rng = new System.Random(3);
                scene.DropItemForTest(100f);
                check("DropItem inside the bounds creates an item", scene.ItemCount == 1);
                var it = scene.ItemAt(0);
                var weapons = Dc2GameData.PlayerWeapons(0);
                bool ownedType = false; for (int i = 0; i < 5; i++) if (game.Profile.weaponOwned[i] >= 0 && weapons[i].type == it.kind) ownedType = true;
                check("kind is the type of an owned slot (0..4)", ownedType);
                // pickup: ammo
                int slot = -1; for (int i = 0; i < 5; i++) if (weapons[i].type == it.kind && game.Profile.weaponOwned[i] >= 0) slot = i;
                game.Profile.weaponAmmo[slot] = 100;
                scene.heroX = it.x + 10f; scene.rng = new System.Random(11);
                game.Step(1f / 60f);
                int gained = game.Profile.weaponAmmo[slot] - 100;
                int[][] tiers = { new[] { 24, 36, 48, 60 }, new[] { 8, 16, 24, 48 }, new[] { 30, 60, 90, 120 }, new[] { 40, 80, 120, 20 }, new[] { 5, 10, 15, 30 }, new[] { 10, 20, 30, 40 } };
                var r = new System.Random(11); int roll = (int)((float)r.NextDouble() * 100f); int tier = roll < 40 ? 0 : roll < 70 ? 1 : roll < 90 ? 2 : 3;
                check("ammo pickup within 60 px adds the tier amount for the weapon type", gained == tiers[it.kind][tier]);
                check("ammo item is removed after pickup", scene.ItemCount == 0);
                {   // itemClass::Start 0x97f8 popup (r118): [icon_item{type}Frame] x then the amount digits, rising S*50 px/s
                    string digits = tiers[it.kind][tier].ToString();
                    check("pickup starts the itemClass popup with one digit object per character", scene.PopupActive && scene.PopupDigitCount == digits.Length);
                    check("popup icon frame is icon_item{type}Frame and the x sprite is icon_itemxFrame", scene.PopupIcon.sprite == Atlas.Get("icon_item" + it.kind + "Frame") && scene.PopupMul.sprite == Atlas.Get("icon_itemxFrame"));
                    bool digitsOk = true; for (int di = 0; di < digits.Length; di++) digitsOk &= scene.PopupDigit(di).sprite == Atlas.Get("icon_num" + digits[di] + "Frame") && scene.PopupDigit(di).enabled;
                    check("popup digits are icon_num{d}Frame of the picked amount", digitsOk);
                    shot("item_popup_start.png");
                    float y0 = scene.PopupPosition.y;
                    for (int f = 0; f < 60; f++) game.Step(1f / 60f);
                    check("popup rises S*50 px/s (gravity 0)", Mathf.Abs((scene.PopupPosition.y - y0) - N.S * 50f) < 0.5f);
                    check("iPad popup scale is already 1.0 (S*0.5)", Mathf.Abs(scene.PopupScale - 1f) < 1e-6f);
                    // digit spacing: next digit centre advances by the previous digit's width (v43[0] += size.x)
                    if (digits.Length >= 2)
                    {
                        float dxx = scene.PopupDigit(1).transform.localPosition.x - scene.PopupDigit(0).transform.localPosition.x;
                        float w0 = scene.PopupDigit(0).sprite.rect.width * scene.PopupDigit(0).transform.localScale.x;
                        check("digit i+1 sits one width of digit i to the right", Mathf.Abs(dxx - w0) < 0.01f);
                    }
                }
                // cap
                scene.CreateItemForTest(it.kind, 200f); game.Profile.weaponAmmo[slot] = 495; scene.heroX = 205f; game.Step(1f / 60f);
                check("ammo is capped at g_max_bullet_count", game.Profile.weaponAmmo[slot] == game.Profile.maxBullet);
                // far item stays
                scene.CreateItemForTest(it.kind, 400f); scene.heroX = 100f; game.Step(1f / 60f);
                check("an item farther than 60 px is not picked up", scene.ItemCount == 1 && scene.ItemAt(0).picked == false);
                { var spr = scene.ItemAt(0).sprite; log.AppendLine("INFO item sprite enabled=" + spr.enabled + " sprite=" + (spr.sprite != null) + " pos=" + spr.transform.position + " scale=" + spr.transform.lossyScale + " order=" + spr.sortingOrder + " camLeft=" + scene.camLeft + " heroX=" + scene.heroX + " itemY=" + scene.ItemAt(0).y + " camPos=" + cam.transform.position + " visible=" + spr.isVisible + " bounds=" + spr.bounds + " parent=" + spr.transform.parent.parent.name + " layerRoots=" + spr.transform.parent.parent.GetComponent<UnityEngine.Rendering.SortingGroup>().sortingOrder + " color=" + spr.color); }
                shot("item_ground.png");
                {   // pixel assertion: the item must change pixels around its screen position
                    var sp = scene.ItemAt(0).sprite;
                    Func<Color32[]> grab = () => { cam.targetTexture = rt; Bootstrap.RenderAll(cam); RenderTexture.active = rt; img.ReadPixels(new Rect(0, 0, 1024, 768), 0, 0); img.Apply(); var px = img.GetPixels32(); cam.targetTexture = null; RenderTexture.active = null; return px; };
                    var withItem = grab(); log.AppendLine("INFO after render visible=" + sp.isVisible + " go.active=" + sp.gameObject.activeInHierarchy + " layer=" + sp.gameObject.layer + " cullMask=" + cam.cullingMask + " mat=" + (sp.sharedMaterial != null ? sp.sharedMaterial.name : "null") + " sprTex=" + sp.sprite.texture.name + " rect=" + sp.sprite.rect + " tex=" + sp.sprite.texture.width + "x" + sp.sprite.texture.height); try { var px0 = sp.sprite.texture.GetPixels32(); int opaque = 0; for (int yy = 412; yy < 412 + 74; yy++) for (int xx = 617; xx < 697; xx++) if (px0[yy * 1024 + xx].a > 20) opaque++; log.AppendLine("INFO crate opaque texels=" + opaque); } catch (Exception ex0) { log.AppendLine("INFO texture unreadable " + ex0.Message); }
                    sp.enabled = false; scene.ItemAt(0).glow.enabled = false; var without = grab(); sp.enabled = true;
                    int diff = 0; for (int i2 = 0; i2 < withItem.Length; i2++) if (Mathf.Abs(withItem[i2].r - without[i2].r) + Mathf.Abs(withItem[i2].g - without[i2].g) + Mathf.Abs(withItem[i2].b - without[i2].b) > 30) diff++;
                    log.AppendLine("INFO item pixel diff=" + diff);
                    check("the ammo crate is actually drawn (pixel difference)", diff > 500);
                }
                // bobbing keeps y within base+30..base+50
                float baseY = scene.ItemAt(0).baseY; float lo = 1e9f, hi = -1e9f;
                for (int f = 0; f < 240; f++) { game.Step(1f / 60f); float y = scene.ItemAt(0).y; lo = Mathf.Min(lo, y); hi = Mathf.Max(hi, y); }
                check("upDown bobbing stays inside base+28 .. base+52", lo >= baseY + 28f && hi <= baseY + 52f && hi - lo > 5f);
                // HP kit
                scene.playerHp = scene.playerMaxHp * 0.5f; float before = scene.playerHp;
                scene.CreateItemForTest(6, 100f); scene.heroX = 100f; game.Step(1f / 60f);
                float expected = Mathf.Min(scene.playerMaxHp, before + (float)(int)((int)scene.playerMaxHp * 0.25f));
                check("HP kit heals 25% of max HP (int arithmetic), clamped", Mathf.Abs(scene.playerHp - expected) < 0.01f);
                game.StepFor(5.2f);
                check("HP popup disappears after the 5 s secondtimer", scene.ItemCount == 1);   // the far ammo item remains
                // mission types 3 never drop
                game.LaunchMission = new NativeMapState.Mission { type = 3, count = 10, level = 1, background = 0 };
                game.SwitchScene("gameScene"); game.StepFor(0.02f);
                scene = (GameScene)game.Current; scene.spawningEnabled = false; scene.rng = new System.Random(2);
                for (int k = 0; k < 200; k++) { var d = scene.CreateEnemy(1); scene.ApplyDamageForTest(d); }
                check("kill missions never drop items", scene.ItemCount == 0);
                game.LaunchMission = new NativeMapState.Mission { type = 7, count = 10, level = 1, background = 0 };
                game.SwitchScene("gameScene"); game.StepFor(0.02f);
                scene = (GameScene)game.Current; scene.spawningEnabled = false; scene.rng = new System.Random(2);
                for (int k = 0; k < 400; k++) { var d = scene.CreateEnemy(1); d.x = 100f; scene.ApplyDamageForTest(d); }
                int drops = scene.ItemsDropped;
                check("arena kills drop ~6% (between 10 and 45 of 400)", drops >= 10 && drops <= 45);
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
