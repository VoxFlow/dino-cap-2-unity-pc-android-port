using System;
using UnityEngine;

namespace Dc2Pilot
{
    /// <summary>
    /// selectScene m_map_type 2 (hero / "tavern") user interface shell.
    /// Native: showTavernUI 0x1056dc, OnSelectChange 0x100b1e, selectScene ctor icon rects 0xf7190,
    /// OnUIEvent type-2 cases 6..10 / 36 / 37 / 38 (registry dc2.select.scene_modes, dc2.hero.*).
    /// Native OnUIEvent mode 2 (0x10ce02) also handles ids 11, 36/37 (cursor -1/+1 over 0..3 + InitCharacter + OnSelectChange), 40 and 41, but no selectScene code creates
    /// those controls (showTavernUI 0x1056dc / OnSelectChange 0x100b1e / sub_101624 create only 6..10, 106..110, 18, 28, 35, 38, 39, 42..44, 47..51, 57, 58, 63, 90;
    /// the only movs r1,#40/#41 sites are DeleteControl calls): DEAD handlers in v1.3 (r118x; the old "OPEN: go/upgrade/arrow buttons" row is closed).
    /// </summary>
    public partial class SelectScene
    {
        public int heroCursor;                 // native this+14
        public int heroSelected = -1;          // native this+15
        public int heroHpShown, heroLevelShown, heroExpPercent;
        public Rect heroSelectFrameRect;
        public HeroPreview HeroRig { get; private set; }

        // native icon-slot permutation (showTavernUI switch): hero i uses slot Perm[i]
        static readonly int[] HeroSlotPerm = { 0, 3, 1, 2, 4 };
        // selectScene ctor 0xf7190: five icon rects (x per slot, y,w,h shared)
        static readonly int[] SlotXBits = { 1113325568, 1123418112, 1128136704, 1132462080, 1134559232 };

        Rect HeroIconRect(int hero)
        {
            int slot = HeroSlotPerm[hero];
            return N.Layout(N.F(SlotXBits[slot]), N.F(1106247680), N.F(1116864512), N.F(1118699520));
        }

        void MoveRightLabel(UiLabel l, Rect r)
        {
            float cx = r.width * 0.5f + r.x, cy = r.height * 0.5f + r.y;
            l.StartMove(new MoveData(cx + 480f * N.S, cy, r.width, r.height, White),
                        new MoveData(cx + N.F(-1046478848) * N.S, cy, r.width, r.height, White),
                        new MoveData(cx, cy, r.width, r.height, White), 0.2f, 0.05f);
        }

        /// <summary>selectScene::UpdateExp 0xf45dc (callers: showTavernUI, showUpgrade, UpgradeWeapon, AddBullet, showIAP, iapaddmoney, OnUIEvent x2):
        /// loads the global cash (g_prop_Player+8, 0xf461a), clamps a negative value to 0 and STORES it back (0xf4626), then prints it with commas.
        /// DC2-RELEASE-REVIEW-001 REL-12 (Codex): the port clamped only the displayed value, so a negative cash stayed negative (FALSIFIED; fixed).</summary>
        string UpdateExpCash()
        {
            if (Profile.cash < 0) Profile.cash = 0;
            return WithCommas(Profile.cash);
        }

        static string WithCommas(int v)
        {
            if (v < 0) v = 0;                                  // display guard; the native write happens in UpdateExpCash
            string s = v.ToString();
            for (int i = s.Length; i > 3;) { i -= 3; s = s.Insert(i, ","); }
            return s;
        }

        public void ShowTavernUI()
        {
            Atlas.LoadByConf("DC2/Image/ip/uitavern", "DC2/Image/ip/uitavern.conf");
            Atlas.LoadByConf("DC2/Image/ip/uitavern_1", "DC2/Image/ip/uitavern_1.conf");
            Atlas.LoadByConf("DC2/Image/ip/Player2x", "DC2/Image/ip/Player2x.conf");
            // OnLoad0x109c7e..0x109d2e: world object at (0,0), size (480*S+WOffset,320*S+HOffset).
            var background = CreateControl(9100, -3); background.touchable = false;
            background.SetTexture("ui_bg_allFrame");
            background.SetRect(new Rect(0, 0.5f * N.HOffset, 480f * N.S + N.WOffset, 320f * N.S + N.HOffset));
            heroCursor = Profile.playerType; // OnLoad0x10a1ca..0x10a1d4; new scene owns cursor.
            HeroRig = HeroPreview.Create(transform);
            HeroRig.Select(heroCursor);

            DeleteControl(35);
            var title = CreateControl(35, 0); title.touchable = false;
            title.SetTexture("ui_title_trvernFrame");
            Rect t = N.RectScale(0, 0, 173, 67);                    // taTitleRect
            title.SetRect(new Rect(t.x, t.y + 0.5f * N.HOffset, t.width, t.height));   // iPad: y += 0.5*g_h_offset only
            heroSelected = -1;
            heroLocked = false;

            for (int i = 0; i < 5; i++)
            {
                DeleteControl(i + 6); DeleteControl(i + 106);
                var bg = CreateControl(106 + i, 1); bg.touchable = false;   // vtable+44(0)
                var icon = CreateControl(6 + i, 2);
                Rect ir = HeroIconRect(i);
                icon.SetTexture("ui_p" + (i + 1) + "_icon_gFrame");
                icon.SetRect(ir); MoveLeft(icon, ir, White);
                Rect br = N.Layout(65f + 68f * i, 45f, 61f, 56f);       // playerIconBgRect, x += 68 per slot
                bg.SetTexture("ui_icon_bgFrame");
                bg.SetRect(br); MoveLeft(bg, br, White);
            }

            DeleteControl(50);
            var gold = CreateControl(50, 0); gold.touchable = false;
            gold.SetTexture("ui_goldFrame");
            Rect gr = N.Layout(192, 3, 23, 22); gold.SetRect(gr); MoveRight(gold, gr, White);

            DeleteControl(51); DeleteLabel(51);
            var gl = CreateLabel(51, 0, 30); gl.consumesTouch = true;   // r118z4: native label left enabled - consumes touches in its rect (showTavernUI 0x105b6e)
            gl.SetNative("Adventure_16", 0f, 0f, 0, 0);
            gl.SetColor(new Color32(255, 205, 0, 255));
            Rect glr = N.Layout(220, 0, 200, 30);
            gl.SetRect(glr);
            gl.SetText(UpdateExpCash());                    // UpdateExp: clamp + store, sprintf %d, commas
            MoveRightLabel(gl, glr);

            DeleteControl(49);
            var iap = CreateControl(49, 0);
            iap.SetTexture("ui_btn_iapFrame");
            Rect ar = N.Layout(325, 0, 157, 38); iap.SetRect(ar); MoveRight(iap, ar, White);

            DeleteControl(38);
            var back = CreateControl(38, 1);
            back.SetTexture("ui_btn_backFrame");
            Rect bk = N.Layout(16, 284, 104, 30);
            bk = new Rect(bk.x, bk.y + N.F(1102053376), bk.width, bk.height);     // iPad y += 22
            back.SetRect(bk); MoveRight(back, bk, White);

            DeleteControl(18);
            var backImg = CreateControl(18, 1); backImg.touchable = false;
            backImg.SetTexture("ui_btn_back_imgFrame");
            Rect bi = N.Layout(51, 285, 69, 27);
            bi = new Rect(bi.x, bi.y + N.F(1102053376), bi.width, bi.height);
            backImg.SetRect(bi); MoveRight(backImg, bi, White);

            OnSelectChange();
        }

        public void OnSelectChange()
        {
            var heroes = Dc2Data.Heroes();
            int type = heroCursor;
            int level = Profile.level[type];
            var h = heroes[type];

            DeleteControl(28);
            var sel = CreateControl(28, 3); sel.touchable = false;
            sel.SetTexture("ui_selectFrame");
            int slot = HeroSlotPerm[type];
            // playerIconBgRect (65,45) + 68*slot, minus (4,7), 70x70
            heroSelectFrameRect = N.Layout(65f + 68f * slot - N.F(1082130432), 45f - N.F(1088421888), 70f, N.F(1116471296));
            sel.SetRect(heroSelectFrameRect);

            if (level <= -1) { ShowLockedHero(type); return; }         // native sub_101624 tail-call: nothing else is built
            heroSelected = type;                                       // native this+15 = cursor (unlocked path only)
            DeleteControl(39); DeleteControl(47); DeleteControl(48); DeleteControl(44);

            var go = CreateControl(44, 0); go.touchable = false;
            go.SetTexture("ui_go_bgFrame");
            Rect info = N.Layout(214, 140, 68, 121);                   // taInfoTxtRect
            go.SetRect(info); MoveRight(go, info, White);

            int lvl = level + 1;
            heroLevelShown = lvl;
            int hp = h.hp - 15 + 15 * lvl;
            if (type == 4) hp = Dc2Data.GirlHp(hp, Profile.level[Profile.playerType]);
            heroHpShown = N.HpWithJigsaw(hp,Profile.jigsaw1Count);
            DeleteLabel(42);
            var hpl = CreateLabel(42, 1, 30); hpl.consumesTouch = true;   // r118z4: native label left enabled - consumes touches in its rect (OnSelectChange 0x100e04)
            hpl.SetNative("Adventure_16", 0f, 0f, 0, 0);   // OnSelectChange 0x100b1e (emulation)
            Rect hpr = N.Layout(N.F(1135214592), N.F(1128136704), 200f, 30f);
            hpl.SetColor(new Color32(255, 205, 0, 255)); hpl.SetRect(hpr); hpl.SetText(heroHpShown.ToString());
            MoveRightLabel(hpl, hpr);
            DeleteLabel(43);
            var lvl43 = CreateLabel(43, 1, 30); lvl43.consumesTouch = true;   // r118z4: native label left enabled - consumes touches in its rect (0x10100c)
            lvl43.SetNative("Adventure_16", 0f, 0f, 0, 0);
            Rect lr = N.Layout(N.F(1136001024), N.F(1125056512), 200f, 30f);
            lvl43.SetColor(new Color32(255, 205, 0, 255)); lvl43.SetRect(lr); lvl43.SetText(lvl.ToString());
            MoveRightLabel(lvl43, lr);

            // EXP bar (taExpProgRect 294,238,106,28): percent / fill computed in 1x then scaled
            DeleteControl(57);
            var eb = CreateControl(57, 0); eb.touchable = false;
            eb.SetTexture("ui_exp_bgFrame");
            Rect er = N.Layout(294, 238, 106, 28); eb.SetRect(er); MoveRight(eb, er, White);
            int needed = (level + 1) * 30 * level + h.baseExp;
            heroExpPercent = needed > 0 ? 100 * Profile.exp[type] / needed : 0;
            int fill = (int)((float)heroExpPercent * N.F(1064514355));          // 0.95, 1x units
            DeleteControl(58);
            var cover = CreateControl(58, 1); cover.touchable = false;
            cover.SetTexture("whiteFrame");
            Rect cr = N.Layout((float)fill + 294f + N.F(1088421888), 238f + N.F(1082130432), (float)(95 - fill), N.F(1100480512));
            cover.SetRect(cr);
            var cov = new Color32(0, 0, 0, 224);
            float cx = cr.x + cr.width * 0.5f, cy = cr.y + cr.height * 0.5f;
            cover.StartMove(new MoveData(cx + 480f * N.S, cy, cr.width, cr.height, cov),
                            new MoveData(cx + N.F(-1046478848) * N.S, cy, cr.width, cr.height, cov),
                            new MoveData(cx, cy, cr.width, cr.height, cov), 0.2f, 0.05f);

            DeleteLabel(63);
            var pl = CreateLabel(63, 1, 24); pl.consumesTouch = true;   // r118z4: native label left enabled - consumes touches in its rect (0x101414)
            pl.SetNative("Adventure_12", 0f, 0f, 1, 2);   // SetAlignment(1, 2)
            Rect pr = N.Layout(294f + 30f, 238f - N.F(1092616192), 50f, 30f);
            pl.SetColor(new Color32(0x97, 0x4A, 0x01, 255)); 
            pl.SetRect(pr); pl.SetText(heroExpPercent.ToString());
            MoveRightLabel(pl, pr);
            DeleteControl(90);
            var pct = CreateControl(90, 1); pct.touchable = false;
            pct.SetTexture("%Frame");
            Rect pp = N.Layout(294f + 30f + N.F(1107820544), 238f - N.F(1092616192) + N.F(1099956224), 11f, N.F(1093664768));
            pct.SetRect(pp);
            pct.StartMove(new MoveData(pp.x + pp.width * 0.5f + 480f * N.S, pp.y + pp.height * 0.5f, pp.width, pp.height, White),
                          new MoveData(pp.x + pp.width * 0.5f + N.F(-1046478848) * N.S, pp.y + pp.height * 0.5f, pp.width, pp.height, White),
                          new MoveData(pp.x + pp.width * 0.5f, pp.y + pp.height * 0.5f, pp.width, pp.height, White), 0.2f, 0.05f);
        }

        /// <summary>sub_101624: locked hero => hire button, info art and gold strip; stats are removed.</summary>
        void ShowLockedHero(int type)
        {
            foreach (int id in new[] { 39, 40, 41, 44, 45, 46, 58, 57, 63, 90 }) DeleteControl(id);
            foreach (int id in new[] { 42, 43, 63 }) DeleteLabel(id);
            var hire = CreateControl(39, 0);
            hire.SetTexture("ui_btn_hiredFrame");
            Rect hr = N.Layout(386, 274, 88, 39); hire.SetRect(hr);
            ScaleIn(hire, hr);
            DeleteControl(47);
            var info = CreateControl(47, 1); info.touchable = false;
            info.SetTexture("ui_p" + (type + 1) + "_infoFrame");
            Rect ir = N.Layout(208, 119, 252, 181); info.SetRect(ir); MoveRight(info, ir, White);
            DeleteControl(48);
            var gb = CreateControl(48, 0); gb.touchable = false;
            gb.SetTexture("ui_gold_bgFrame");
            Rect gr = N.Layout(201, 272, 181, 35); gb.SetRect(gr); MoveRight(gb, gr, White);
            heroLocked = true;
        }

        public bool heroLocked;

        void UpdateGoldLabel()
        {
            foreach (var l in Labels) if (l.id == 51) l.SetText(UpdateExpCash());
        }

        /// <summary>case 39: hire. cash >= cost (ARM: equality accepted) else the 'short on cash' dialog.</summary>
        void HireHero()
        {
            int h = heroCursor;
            int cost = Dc2Data.Heroes()[h].hire;
            if (Profile.cash < cost)
            {
                Audio.PlaySound("Audio_button2Audio");
                OpenDialog("You're short on cash. Want to buy some more?", "nomoney", () => { Game.MapType = 4; Game.LastMapType = 2; lastAction = "bank(from nomoney)"; Game.SwitchScene("selectScene"); });
                return;
            }
            Profile.level[h] = 0;
            Profile.cash -= cost;
            DeleteControl(39);
            foreach (int id in new[] { 40, 44, 45, 46 }) DeleteControl(id);
            foreach (int id in new[] { 42, 43 }) DeleteLabel(id);
            heroLocked = false;
            OnSelectChange();                                           // OnHire -> OnSelectChange
            UpdateGoldLabel();
            Game.SaveFile();
            lastAction = "hero:hired" + h;
        }

        /// <summary>Type-2 events: ids 6..10 select a hero, 38 = back (commit selection).</summary>
        bool HandleHeroEvent(UiControl c, int ev)
        {
            if (Game.MapType != 2) return false;
            if (ev == 0)
            {
                if (c.id >= 6 && c.id <= 10) { heroCursor = c.id - 6; HeroRig.Select(heroCursor); OnSelectChange(); lastAction = "hero:select" + heroCursor; return true; }
                if (c.id == 38)
                {
                    // native: if (selected != g_player_type) dword_2404E8 = 0; g_player_type = selected; MapType = 0; SaveFile
                    // native: this+15 is only updated for unlocked heroes (OnSelectChange tail-calls sub_101624 for locked ones),
                    // so Back commits the last unlocked selection. PA-008 resolved for this part.
                    if (heroSelected >= 0) Profile.playerType = heroSelected;
                    Game.MapType = 0; Game.SaveFile(); lastAction = "hero:back";
                    Game.SwitchScene("selectScene");
                    return true;
                }
                if (c.id == 39) { HireHero(); return true; }
                if (c.id == 49) { lastAction = "bank(from hero)"; Game.MapType = 4; Game.LastMapType = 2; Game.SwitchScene("selectScene"); return true; }
            }
            return false;
        }
    }
}
