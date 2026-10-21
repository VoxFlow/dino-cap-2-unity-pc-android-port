using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dc2Pilot
{
    /// <summary>
    /// DinoCap::selectScene, map screen (m_map_type 0) and the Options panel.
    /// Registry: dc2.select.scene_modes, dc2.select.map_ui_layout, dc2.options.*, dc2.ui.rect_tables.
    /// Not yet ported: map points / mission popup, hero, armory, bank, jigsaw screens (other m_map_type values).
    /// </summary>
    public partial class SelectScene : Dc2Scene
    {
        public int optionState;             // native this+420: 0 = option panel open/opening, 5 = leaving
        public bool optionOpen;
        public bool confirmOpen;
        public int resetCount;
        public int supportClicks, reviewClicks;
        public string lastAction = "";

        protected override void OnObjectUpdate(float dt)
        {
            if (HeroRig != null) HeroRig.Step(dt);
        }

        static readonly Color32 White = new Color32(255, 255, 255, 255);

        public override void OnLoad()
        {
            Atlas.Reset();
            Atlas.LoadNativeTable();
            Atlas.EnsureWhite();
            // r119c (DC2-BUGS-001 B6 'missing red background', user: "the red big one in uicontrol_h.png"): selectScene::OnLoad creates, before the
            // per-screen switch, a world GameObject with ui_bg_allFrame (uicontrol_h 480x320) at (0,0), size (480S + g_w_offset, 320S + g_h_offset), layer 0
            // (0x1065f8..0x106654) for every select screen. The port only created it inside the tavern / armory / jigsaw builders, so the world map had none.
            // Same stand-in as those builders (id 9100, below-UI layer -3, UI rect (0, 32, 1024, 704)); a builder that creates 9100 again replaces it.
            if (Game.DebugLevelUnlockPending.Count > 0)
            {   // DEBUG_ONLY (r119i, user request): apply the AwardItem weapon unlock for heroes levelled by the debug button, then save (see Dc2Game.DebugLevelUnlockPending)
                foreach (int h in Game.DebugLevelUnlockPending) GameScene.UnlockWeaponsByLevel(Profile, h);
                Game.DebugLevelUnlockPending.Clear(); Game.SaveFile();
            }
            var redBg = CreateControl(9100, -3); redBg.touchable = false; redBg.SetTexture("ui_bg_allFrame");
            redBg.SetRect(new Rect(0, 0.5f * N.HOffset, 480f * N.S + N.WOffset, 320f * N.S + N.HOffset));
            if (Game.MapType == 1) ShowArmoryScene();
            else if (Game.MapType == 4) ShowBankScene();
            else if (Game.MapType == 5) ShowJigsawScene();
            else if (Game.MapType == 6) ShowLaunchScene();
            else
            {
                Atlas.LoadByConf("DC2/Image/ip/uimap_h", "DC2/Image/ip/uimap_h.conf");
                Atlas.LoadByConf("DC2/Image/ip/uimap_sub_h", "DC2/Image/ip/uimap_sub_h.conf");
                Atlas.LoadByConf("DC2/Image/ip/uimap_sub1_h", "DC2/Image/ip/uimap_sub1_h.conf");
                if (Game.MapType == 2) ShowTavernUI(); else ShowMapUI();
            }
            // r119e (user item 11, no menu music after leaving a mission): every selectScene::OnLoad path ends at 0x10a1dc (branches 0x1066a6, 0x107702, 0x107a1e,
            // 0x109a88): if AudioManager::CheckSession("menu_bgAudio") is false, PlayMusic("Music_menuAudio", loop, "menu_bgAudio") (g_sound gate inside PlayMusic).
            // The game stops game_bgAudio when it leaves; the port never restarted the menu music (only the title did).
            if (!Audio.SessionPlaying("menu_bgAudio")) Audio.PlayMusic("Music_menuAudio", true, "menu_bgAudio");
        }

        // ---- selectScene tween helpers (0xfcfdc right, 0x1020a8 left, 0xf77dc scale, 0xfc056 down, 0xfb1fc up) ----
        void MoveRight(UiControl c, Rect r, Color32 col)
        {
            float cx = r.width * 0.5f + r.x, cy = r.height * 0.5f + r.y;
            c.StartMove(new MoveData(cx + 480f * N.S, cy, r.width, r.height, col),
                        new MoveData(cx + N.F(-1046478848) * N.S, cy, r.width, r.height, col),
                        new MoveData(cx, cy, r.width, r.height, col), 0.2f, 0.05f);
        }

        void MoveLeft(UiControl c, Rect r, Color32 col)
        {
            float cx = r.width * 0.5f + r.x, cy = r.height * 0.5f + r.y;
            c.StartMove(new MoveData(cx - 480f * N.S, cy, r.width, r.height, col),
                        new MoveData(cx + 20f * N.S, cy, r.width, r.height, col),
                        new MoveData(cx, cy, r.width, r.height, col), 0.2f, 0.05f);
        }

        void LabelMoveLeft(UiLabel l, Rect r)
        {
            float cx = r.width * 0.5f + r.x, cy = r.height * 0.5f + r.y;
            l.StartMove(new MoveData(cx - 480f * N.S, cy, r.width, r.height, White),
                        new MoveData(cx + 20f * N.S, cy, r.width, r.height, White),
                        new MoveData(cx, cy, r.width, r.height, White), 0.2f, 0.05f);
        }

        void ScaleIn(UiControl c, Rect r)
        {
            float cx = r.width * 0.5f + r.x, cy = r.height * 0.5f + r.y;
            c.StartMove(new MoveData(cx, cy, r.width * 0.1f, r.height * 0.1f, White),
                        new MoveData(cx, cy, r.width * 1.1f, r.height * 1.1f, White),
                        new MoveData(cx, cy, r.width, r.height, White), 0.2f, 0.05f);
        }

        void MoveDown(UiControl c, Rect r)
        {
            float cx = r.width * 0.5f + r.x, cy = r.height * 0.5f + r.y;
            c.StartMove(new MoveData(cx, cy + N.F(-1012924416) * N.S, r.width, r.height, White),
                        new MoveData(cx, cy + 20f * N.S, r.width, r.height, White),
                        new MoveData(cx, cy, r.width, r.height, White), 0.2f, 0.05f);
        }

        void MoveUp(UiControl c, Rect r)
        {
            float cx = r.width * 0.5f + r.x, cy = r.height * 0.5f + r.y;
            c.StartMove(new MoveData(cx, cy, r.width, r.height, White),
                        new MoveData(cx, cy + 20f * N.S, r.width, r.height, White),
                        new MoveData(cx, cy + N.F(-1012924416) * N.S, r.width, r.height, White), 0.05f, 0.2f);
        }

        // ---- showMapUI (0x10218c) ----
        public Rect ExpFillRect;
        public int ExpPercent;
        public int HpShown;

        void ShowMapUI()
        {
            var heroes = Dc2Data.Heroes();
            int type = Profile.playerType;
            int level = Profile.level[type];
            var h = heroes[type];

            // map background + player panel
            var bg = CreateControl(56, 0); bg.touchable = false;
            bg.SetTexture("ui_map_bgFrame");
            Rect bgR = Px(11, 73, 458, 196); bg.SetRect(bgR); MoveRight(bg, bgR, White);

            var info = CreateControl(59, 0); info.touchable = false;
            info.SetTexture("ui_map_player_infoFrame");
            Rect infoR = Px(10, 6, 218, 63); info.SetRect(infoR); MoveLeft(info, infoR, White);

            // Lv label (Adventure 16), colour 0xFF00CDFF = (255,205,0)
            var lv = CreateLabel(61, 1, 30); lv.consumesTouch = true;   // r118z4: native label left enabled - consumes touches in its rect (showMapUI 0x10269c)
            lv.SetNative("Adventure_16", 0f, 0f, 0, 0);   // showMapUI 0x10218c (emulation): Adventure_16, colour (255,205,0,255), default alignment
            Rect lvR = Px(171, 2, 200, 30);
            lv.SetColor(new Color32(255, 205, 0, 255)); lv.SetText((level + 1).ToString()); lv.SetRect(lvR); LabelMoveLeft(lv, lvR);

            // HP label: (int)(hp + jigsaw1*hp*0.01), hp = base - 15 + 15*(level+1)
            int hpBase = h.hp - 15 + 15 * (level + 1);
            if (type == 4) hpBase = Dc2Data.GirlHp(hpBase, Profile.level[Profile.playerType]);
            HpShown = N.HpWithJigsaw(hpBase,Profile.jigsaw1Count);
            var hp = CreateLabel(62, 1, 30); hp.consumesTouch = true;   // r118z4: native label left enabled - consumes touches in its rect (0x1027e6)
            hp.SetNative("Adventure_16", 0f, 0f, 0, 0);
            Rect hpR = Px(147, 21, 200, 30);
            hp.SetColor(new Color32(255, 205, 0, 255)); hp.SetText(HpShown.ToString()); hp.SetRect(hpR); LabelMoveLeft(hp, hpR);

            // EXP bar
            var expBg = CreateControl(57, 1); expBg.touchable = false;
            expBg.SetTexture("ui_exp_bgFrame");
            Rect expR = Px(120, 43, 106, 28); expBg.SetRect(expR); MoveLeft(expBg, expR, White);

            int needed = (level + 1) * 30 * level + h.baseExp;
            ExpPercent = needed > 0 ? 100 * Profile.exp[type] / needed : 0;
            float fill = Mathf.Floor(N.S * ((float)ExpPercent * N.F(1064514355)));   // 0.95
            var cover = CreateControl(58, 2); cover.touchable = false;
            cover.SetTexture("whiteFrame");
            // showMapUI 0x102aa8..0x102afc: x = bar.x + fill + S*7 (0x40E00000), y = bar.y + S*4, w = S*95 (0x42BE0000) - fill, h = S*19 (0x41980000).
            // DC2-BUGS-001 B6 (r118z8): the port used 6 and 80, leaving part of the yellow fill uncovered and the cover shifted.
            ExpFillRect = new Rect(fill + expR.x + N.F(0x40E00000) * N.S, expR.y + N.F(0x40800000) * N.S, N.F(0x42BE0000) * N.S - fill, N.F(0x41980000) * N.S);
            cover.SetRect(ExpFillRect);
            var cov = new Color32(0, 0, 0, 224);
            float ccx = ExpFillRect.x + ExpFillRect.width * 0.5f, ccy = ExpFillRect.y + ExpFillRect.height * 0.5f;
            cover.StartMove(new MoveData(ccx - 480f * N.S, ccy, ExpFillRect.width, ExpFillRect.height, cov),
                            new MoveData(ccx + 20f * N.S, ccy, ExpFillRect.width, ExpFillRect.height, cov),
                            new MoveData(ccx, ccy, ExpFillRect.width, ExpFillRect.height, cov), 0.2f, 0.05f);

            var pct = CreateLabel(63, 3, 24); pct.consumesTouch = true;   // r118z4: native label left enabled - consumes touches in its rect (0x102bee)
            pct.SetNative("Adventure_12", 0f, 0f, 1, 2);   // Adventure_12, colour (151,74,1,255), SetAlignment(1, 2)
            Rect pctR = new Rect(expR.x + N.F(1104674816) * N.S, expR.y + N.F(-1054867456) * N.S, N.F(1112014848) * N.S, N.F(1106247680) * N.S);
            pct.SetColor(new Color32(0x97, 0x4A, 0x01, 255));
            pct.SetText(ExpPercent.ToString());
            pct.SetRect(pctR); LabelMoveLeft(pct, pctR);
            // showMapUI ARM@0x102cbe..0x102e00: separate id90 percent sprite.
            // Its right-enter tween intentionally differs from the number's left-enter tween.
            var percent = CreateControl(90, 3); percent.touchable = false;
            percent.SetTexture("%Frame");
            Rect percentR = new Rect(pctR.x + 34f * N.S, pctR.y + 18f * N.S, 11f * N.S, 11f * N.S);
            percent.SetRect(percentR); MoveRight(percent, percentR, new Color32(0x97, 0x4A, 0x01, 255));

            // top-right icons: achievements (66), ranking (65), options (64), stepped 49 px (1x) apart
            string[] icon = { "ui_btn_chengjiuFrame", "ui_btn_paimingFrame", "ui_btn_optionFrame" };
            int[] ids = { 66, 65, 64 };
            for (int k = 0; k < 3; k++)
            {
                var b = CreateControl(ids[k], 0);
                b.SetTexture(icon[k]);
                Rect r = Px(331 + 49 * k, 35, 37, 32);
                b.SetRect(r); MoveRight(b, r, White);
            }

            // hero head icon (id 60)
            var head = CreateControl(60, 0); head.touchable = false;
            head.SetTexture("ui_p" + (type + 1) + "_iconFrame");
            // r119d (user item 15): showMapUI 0x102ffa: the girl (g_player_type 4) uses RectScale(-8, -6, 73, 87) (literals 0xC1000000 / 0xC0C00000 / 0x42920000 /
            // 0x42AE0000), every other hero mapPlayerIconRect; the port used the shared rect for her too (smaller icon).
            Rect headR = type == 4 ? Px(-8, -6, 73, 87) : Px(11, 13, 56, 53); head.SetRect(headR); MoveLeft(head, headR, White);

            // bottom row: hero(54), armory(55), bank(93), jigsaw(95) pop in with startScaleUI
            MakeScaleButton(54, "ui_btn_heroFrame", Px(10, 271, 110, 48));
            MakeScaleButton(55, "ui_btn_armoryFrame", Px(361, 271, 110, 48));
            MakeScaleButton(93, "bankFrame", Px(125, 271, 110, 48));
            MakeScaleButton(95, "jigsaw_buttonFrame", Px(245, 271, 110, 48));
        }

        void MakeScaleButton(int id, string frame, Rect r)
        {
            var c = CreateControl(id, 0);
            c.SetTexture(frame); c.SetRect(r); ScaleIn(c, r);
        }

        // ---- Options (0xfc138 enter, 0xfb2e0 leave) ----
        struct OptItem { public int id; public string frame; public Rect enter, leave; }
        List<OptItem> optItems;

        Rect OptRect(float x, float y, float w, float h)
        {
            Rect r = N.RectOffset(N.RectScale(x, y, w, h));
            return new Rect(r.x, r.y + N.F(1113063424), r.width, r.height);     // iPad: y +=54 (raw IEEE word).
        }

        List<OptItem> BuildOptItems()
        {
            var L = new List<OptItem>();
            Func<int, string, float[], float[], OptItem> mk = (id, fr, e, l) => new OptItem
            { id = id, frame = fr, enter = OptRect(e[0], e[1], e[2], e[3]), leave = OptRect(l[0], l[1], l[2], l[3]) };
            Func<int, float> F = b => N.F(b);
            L.Add(mk(79, "op_titleFrame", new[] { F(1126760448), 2f, 151f, F(1107820544) }, new[] { F(1126760448), 2f, 151f, F(1107820544) }));
            L.Add(mk(82, "op_soundFrame", new[] { F(1121189888), F(1116078080), F(1120534528), F(1103626240) }, new[] { F(1121189888), F(1120272384), 101f, F(1103626240) }));
            L.Add(mk(83, Profile.fx != 0 ? "op_on1Frame" : "op_off1Frame", new[] { F(1134526464), F(1115422720), F(1113325568), F(1105723392) }, new[] { F(1134526464), F(1119748096), 55f, F(1105723392) }));
            L.Add(mk(80, "op_musicFrame", new[] { F(1121189888), F(1121845248), F(1120534528), F(1103626240) }, new[] { F(1121189888), F(1125056512), 101f, F(1103626240) }));
            L.Add(mk(81, Profile.sound != 0 ? "op_on1Frame" : "op_off1Frame", new[] { F(1134526464), F(1121714176), F(1113325568), F(1105723392) }, new[] { F(1134526464), F(1124990976), 55f, F(1105723392) }));
            L.Add(mk(84, "op_supportFrame", new[] { F(1118044160), F(1126367232), F(1121320960), F(1101004800) }, new[] { F(1118044160), F(1128464384), 107f, F(1101004800) }));
            L.Add(mk(85, "op_reviewFrame", new[] { F(1133641728), F(1126367232), F(1120534528), F(1101004800) }, new[] { F(1133641728), F(1128464384), 101f, F(1101004800) }));
            L.Add(mk(87, "op_reset_peofileFrame", new[] { F(1125449728), F(1129054208), F(1127546880), F(1100480512) }, new[] { F(1125449728), F(1131151360), 181f, F(1100480512) }));
            return L;
        }

        public void OnOptionEnter()
        {
            optionOpen = true; optionState = 0;
            var dim = CreateControl(78, 5);
            dim.SetTexture("whiteFrame"); dim.consumesTouch = true;   // r118z4: native image left enabled (scan_ui_control_enable.py) - consumes touches in its rect (OnOptionEnter 0xfc1c0)
            // RectScale(0,0,480,320); iPad: y += 0.5*h_offset, w += w_offset, h += h_offset
            Rect d = N.RectScale(0, 0, 480, 320);
            dim.SetRect(new Rect(d.x, d.y + 0.5f * N.HOffset, d.width + N.WOffset, d.height + N.HOffset));
            dim.SetColor(new Color32(0, 0, 0, 208));      // -805306368 = 0xD0000000
            dim.touchable = false;                         // PILOT: native CreateImage is not a button, blocks nothing itself

            optItems = BuildOptItems();
            foreach (var it in optItems)
            {
                var c = CreateControl(it.id, 6);
                c.SetTexture(it.frame);
                c.SetRect(it.enter);
                if (it.id == 79 || it.id == 82 || it.id == 80) c.touchable = false;
                MoveDown(c, it.enter);
            }
            // back button (88) + image (89, hidden)
            Rect back = BackRect(false);
            var b = CreateControl(88, 6);
            b.SetTexture("ui_btn_backFrame"); b.SetRect(back); MoveDown(b, back);
            Rect backImg = BackImgRect();
            var bi = CreateControl(89, 6); bi.touchable = false;
            bi.SetTexture("ui_btn_back_imgFrame"); bi.SetRect(backImg); bi.SetVisible(false); MoveDown(bi, backImg);
        }

        // aBackRect from static initializer. Enter:y-=32, iPad y+=54. Leave:no-32, iPad adds raw1118568448.
        Rect BackRect(bool leave)
        {
            float x = N.F(1098907648), y = N.F(1133379584), w = N.F(1120927744), h = N.F(1106247680);
            if (!leave) return OptRect(x, y - N.F(1107296256), w, h);
            Rect r = N.RectOffset(N.RectScale(x, y, w, h));
            return new Rect(r.x, r.y + N.F(1118568448), r.width, r.height);
        }

        Rect BackImgRect()
        {
            Rect r = OptRect(N.F(1112276992), N.F(1133412352) - N.F(1107296256), N.F(1116340224), N.F(1104674816));
            return r;
        }

        public void OnOptionLeave()
        {
            optionState = 5;
            foreach (var it in optItems)
            {
                var c = GetControl(it.id);
                if (c != null) MoveUp(c, it.leave);
            }
            var b = GetControl(88); if (b != null) MoveUp(b, BackRect(true));
            var bi = GetControl(89); if (bi != null) MoveUp(bi, BackImgRect());
        }

        void DeleteOptionControls()
        {
            for (int id = 78; id <= 89; id++) { DeleteControl(id); DeleteLabel(id); }
            optionOpen = false;
        }

        // ---- toggles (0xf4128 sound = g_fx, 0xf433a music = g_sound), each saves immediately ----
        void OnSound()
        {
            Profile.fx ^= 1; Game.OngameFx = Profile.fx;
            var c = GetControl(83); if (c != null) c.SetTexture(Profile.fx != 0 ? "op_on1Frame" : "op_off1Frame");
            Game.SaveFile();
        }

        void OnMusic()
        {
            Profile.sound ^= 1; Game.OngameSound = Profile.sound;
            var c = GetControl(81); if (c != null) c.SetTexture(Profile.sound != 0 ? "op_on1Frame" : "op_off1Frame");
            if (Profile.sound != 0) Audio.PlayMusic("Music_menuAudio", true, "menu_bgAudio");
            else Audio.Stop("menu_bgAudio");
            Game.SaveFile();
        }

        // ---- reset profile confirm (native OSMessageBox 'ARE YOU SURE YOU WANT TO DELETE ALL SAVED DATA ?' No/Yes) ----
        Action confirmYes;
        string confirmTag = "reset";

        void OpenConfirm()
        {
            OpenDialog("ARE YOU SURE YOU WANT TO DELETE\nALL SAVED DATA ?", "reset", () =>
            {
                Profile.ResetFileKeepAudio(); resetCount++; Game.SaveFile();
                Game.MapState.ResetBoss(Profile.mission); Game.MapState.SaveBoss(Game.SaveDir);
                Game.MapType = 0; Game.SwitchScene("selectScene");
            });
        }

        /// <summary>PLATFORM_ADAPTER for Triniti2D::OSMessageBox (No/Yes): in-scene overlay.</summary>
        void OpenDialog(string text, string tag, Action onYes)
        {
            confirmYes = onYes; confirmTag = tag;
            confirmOpen = true;
            var dim = CreateControl(900, 8); dim.SetTexture("whiteFrame");
            dim.SetRect(new Rect(0, 0, 1024, 768)); dim.SetColor(new Color32(0, 0, 0, 200)); dim.touchable = false;
            var q = CreateLabel(900, 9, 20);
            q.SetColor(new Color32(255, 255, 255, 255));
            q.SetText(text);
            q.SetRect(new Rect(200, 280, 624, 100));
            var no = CreateControl(901, 9); no.SetTexture("whiteFrame"); no.SetRect(new Rect(260, 420, 200, 70)); no.SetColor(new Color32(90, 90, 90, 255));
            var yes = CreateControl(902, 9); yes.SetTexture("whiteFrame"); yes.SetRect(new Rect(564, 420, 200, 70)); yes.SetColor(new Color32(150, 30, 30, 255));
            var ln = CreateLabel(901, 10, 24); ln.SetText("No"); ln.SetColor(new Color32(255, 255, 255, 255)); ln.SetRect(new Rect(330, 438, 120, 40));
            var ly = CreateLabel(902, 10, 24); ly.SetText("Yes"); ly.SetColor(new Color32(255, 255, 255, 255)); ly.SetRect(new Rect(630, 438, 120, 40));
        }

        void CloseConfirm()
        {
            confirmOpen = false;
            for (int id = 900; id <= 902; id++) { DeleteControl(id); DeleteLabel(id); }
        }

        // PILOT: observed (Visuals Description) - while Options is open only its controls react
        public override bool CanTouch(UiControl c)
        {
            if (confirmOpen) return c.id >= 900;
            if (optionOpen) return c.id >= 78 && c.id <= 89;
            return true;
        }

        protected override void OnUpdate(float dt)
        {
            if (Game.MapType == 4) BankUpdate(dt);
            else if (Game.MapType == 5) JigsawUpdate(dt);
        }

        protected override void OnTimeout(string name)
        {
            if (name == "startJigsaw" && Game.MapType == 5) StartJigsaw();
        }

        protected override void OnUIEvent(UiControl c, int ev)
        {
            if (HandleArmoryEvent(c, ev)) return;
            if (HandleBankEvent(c, ev)) return;
            if (HandleJigsawEvent(c, ev)) return;
            if (HandleLaunchEvent(c, ev)) return;
            if (HandleHeroEvent(c, ev)) return;
            if (HandleMapPointEvent(c, ev)) return;
            if (confirmOpen)
            {
                if (ev != 0) return;
                if (c.id == 901) { CloseConfirm(); lastAction = confirmTag + ":no"; }
                else if (c.id == 902)
                {
                    CloseConfirm();
                    lastAction = confirmTag + ":yes";
                    if (confirmYes != null) confirmYes();
                }
                return;
            }
            if (ev == 0)
            {
                switch (c.id)
                {
                    case 64: if (!optionOpen) OnOptionEnter(); break;
                    case 65: lastAction = "ranking(GameCenter leaderboard)"; break;
                    case 66: lastAction = "achievements(GameCenter)"; break;
                    case 81: if (optionOpen) OnMusic(); break;
                    case 83: if (optionOpen) OnSound(); break;
                    case 84: if (optionOpen) { supportClicks++; lastAction = "support:mailto"; } break;
                    case 85: if (optionOpen) { reviewClicks++; lastAction = "review:appstore"; } break;
                    case 87: if (optionOpen) OpenConfirm(); break;
                    case 88: if (optionOpen) OnOptionLeave(); break;
                    case 54: lastAction = "hero"; heroCursor = Profile.playerType; Game.MapType = 2; Game.SwitchScene("selectScene"); break;
                    case 55: lastAction = "armory"; Game.MapType = 1; Game.SwitchScene("selectScene"); break;
                    case 93: lastAction = "bank"; Game.MapType = 4; Game.LastMapType = 0; Game.SwitchScene("selectScene"); break;
                    case 95: lastAction = "jigsaw"; Game.MapType = 5; Game.SwitchScene("selectScene"); break;
                }
            }
            else if (ev == 1 && c.id == 88 && optionState == 5)
            {
                DeleteOptionControls();
            }
        }
    }
}
