using System.Collections.Generic;
using UnityEngine;

namespace Dc2Pilot
{
    /// <summary>
    /// selectScene m_map_type 5 (jigsaw collection).
    /// Native: OnLoad case 5 (0x1064c6), showJigsawCount 0xfec34, showJigsawUI 0xfdf70, showJigsawInfo 0xf47a4,
    /// OnTouchEvent type-5 branch 0xf57bc, OnUpdate type-5 settle 0xf93c4, AlignmentLigsaw 0xf909e, movePicTOLigsaw 0xf8d3e,
    /// jigsawPicClass::OnUpdate 0x124722, ligsawPicCallBack 0xf514e, AddJigsawEff 0xf4f70, ligsawEffCallBack 0xf90de,
    /// OnUIEvent case 5 (0x10be6c).  Registry: dc2.jigsaw.*.
    /// World coordinates (GameObject positions) are origin-at-screen-centre, y up (dc2.engine.camera, LIKELY).
    /// </summary>
    public partial class SelectScene
    {
        sealed class JigPic { public UiControl ctl; public bool active; public Vector2 pos, size; }
        sealed class JigGroup
        {
            public UiControl ctl; public Vector2 pos; public float scale, w, h, tw, th;
            public readonly List<JigPic> pics = new List<JigPic>();
        }

        readonly List<JigGroup> jigGroups = new List<JigGroup>();
        public readonly List<Vector2Int> jigPending = new List<Vector2Int>();   // native vector<Vector2> at this+224 (group, piece)
        public int jigCursor;                  // native this+156
        int jigStartGroup;                     // native this+160
        int jigAlignPiece;                     // native this+164
        float jigMinDist = 100000f;            // native this+152
        Vector2 jigLast, jigStart;             // native this+208/212 and +216/220
        public bool jigSettling;               // native byte this+151
        bool jigIntroOut;                      // native byte this+150
        public string jigInfoText = "", jigCountText = "";
        public int jigEffectsPlayed;
        public Vector2 LastJigsawSlotPos; public UiControl LastJigsawSlotControl;   // DEBUG_ONLY verifier probes (DC2-BUGS-001 B7)

        sealed class JigFly { public UiControl ctl; public Vector2 p0, p1, s0, s1; public float t; }
        JigFly jigFly;
        NativeComponentPlayer jigEffect;
        public NativeComponentPlayer JigsawEffect { get { return jigEffect; } }
        GameObject jigEffectRoot;
        float jigEffectDuration;

        static readonly string[] JigsawInfoFormats =
        {
            "Increase all characters' HP by %d%%. This \ntask can only be finished by SUPER KID.",
            "Increase the attack of all weapons by %d%%. \nThis task can only be finished by FORCE KNIGHT.",
            "Increase the quantity of ammo sold in Armory by\n %d%%.This task can only be finished by SOLDIER.",
            "Increase all characters' invincible time by %d%% when \nbeing attacked. This task can only be finished by NINJA.",
            "Increase cash earned in battle by %d%%. \n Only Warrior Girl can collect this jigsaw!",
        };

        public float JigGroupX(int g) { return jigGroups[g].pos.x; }

        static Rect WorldRect(Vector2 c, Vector2 size)
        {
            return new Rect(N.ScreenW * 0.5f + c.x - size.x * 0.5f, N.ScreenH * 0.5f - c.y - size.y * 0.5f, size.x, size.y);
        }

        static string PieceFrame(int g, int j)
        {
            return j > 8 ? string.Format("jigsaw_map0{0}_{1}Frame", g + 1, j + 1) : string.Format("jigsaw_map0{0}_0{1}Frame", g + 1, j + 1);
        }

        // ---------------------------------------------------------------- OnLoad case 5
        void ShowJigsawScene()
        {
            Atlas.LoadByConf("DC2/Image/ip/jigsaw_h", "DC2/Image/ip/jigsaw_h.conf");
            Atlas.LoadByConf("DC2/Image/ip/jigsaw_h_1", "DC2/Image/ip/jigsaw_h_1.conf");
            Atlas.LoadByConf("DC2/Image/ip/jigeff2x", "DC2/Image/ip/jigeff2x.conf");
            var bg = CreateControl(9100, -3); bg.touchable = false;
            bg.SetTexture("ui_bg_allFrame");
            bg.SetRect(new Rect(0, 0.5f * N.HOffset, 480f * N.S + N.WOffset, 320f * N.S + N.HOffset));
            jigCursor = 0; jigSettling = false; jigIntroOut = false;
            ShowJigsawCount();
        }

        void BuildPending()
        {
            jigPending.Clear();
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 16; j++)
                {
                    int a = Profile.jigsawActive[16 * i + j], g = Profile.jigsawGet[16 * i + j];
                    if (a != g && g != 0) jigPending.Add(new Vector2Int(i, j));
                }
        }

        /// <summary>showJigsawCount 0xfec34 (ReadFile is a no-op here: the profile is already in memory).</summary>
        void ShowJigsawCount()
        {
            BuildPending();
            if (jigPending.Count != 0)
            {
                Rect r = N.Layout(0, 124f, 480f, 40f); r = new Rect(r.x, r.y + 22f, r.width, r.height);   // iPad: y += 22
                DeleteLabel(599);
                var l = CreateLabel(599, 4, 44); l.consumesTouch = true;   // r118z4: native label left enabled - consumes touches in its rect (showJigsawCount 0xfed9e; the showJigsawUI 599 is disabled)
                l.SetNative("Adventure_22", 0f, 0f, 1, 1);   // showJigsawCount 0xfec34 (ARM/Hex-Rays): Adventure_22, colour (239,205,0,255), SetAlignment(1,1)
                l.SetColor(new Color32(0xEF, 0xCD, 0, 255));
                l.SetRect(r);
                jigCountText = jigPending.Count == 1 ? string.Format("YOU HAVE {0} JIGSAW", jigPending.Count) : string.Format("YOU HAVE {0} JIGSAWS", jigPending.Count);
                l.SetText(jigCountText);
                MoveRightLabel(l, r);
                jigIntroOut = false;
                SetTimer("startJigsaw", 1.0f);
            }
            else ShowJigsawUI();
        }

        /// <summary>Timer "startJigsaw" (0xf6f00): the banner leaves (startMoveUIRightOut), then showJigsawUI runs when it stops.</summary>
        void StartJigsaw()
        {
            Rect r = N.Layout(0, 124f, 480f, 40f); r = new Rect(r.x, r.y + 32f, r.width, r.height);
            foreach (var l in Labels)
                if (l.id == 599)
                {
                    float cx = r.width * 0.5f + r.x, cy = r.height * 0.5f + r.y;
                    l.StartMove(new MoveData(cx + 20f * N.S, cy, r.width, r.height, White),
                                new MoveData(cx - 480f * N.S, cy, r.width, r.height, White),
                                new MoveData(cx - 480f * N.S, cy, r.width, r.height, White), 0.2f, 0.05f);
                    break;
                }
            jigIntroOut = true;
        }

        void JigsawIntroPoll()
        {
            if (!jigIntroOut) return;
            foreach (var l in Labels) if (l.id == 599 && l.IsMoving) return;
            jigIntroOut = false;                       // OnUIEvent case 5, event 1: this+150 := 0, this+149 := 1
            ShowJigsawUI();
        }

        // ---------------------------------------------------------------- showJigsawUI
        void ShowJigsawUI()
        {
            foreach (var g in jigGroups) { Util.Kill(g.ctl.gameObject); foreach (var p in g.pics) Util.Kill(p.ctl.gameObject); }
            jigGroups.Clear(); jigPending.Clear();
            DeleteControl(35);
            var title = CreateControl(35, 2);
            title.SetTexture("jigsaw_titleFrame");
            Rect tr = N.RectScale(0, 0, 195, 72);
            title.SetRect(new Rect(tr.x, tr.y + 32f, tr.width, tr.height));

            float s = N.S;
            for (int g = 0; g < 5; g++)
            {
                var grp = new JigGroup();
                grp.ctl = CreateControl(9110 + g, -2); grp.ctl.touchable = false;
                grp.ctl.SetTexture("ui_bg_jigsawFrame");
                grp.scale = g != 0 ? 0.8f : 1.0f;
                Rect rr = N.RectScale(280f * g, 10f, 200f, 200f);       // g_iphone==0 => y 10
                grp.tw = s * 250f; grp.th = s * 250f;                    // state+16/+20
                grp.w = rr.width; grp.h = rr.height;                     // state+8/+12
                grp.pos = new Vector2(rr.x, rr.y + s * 20f);
                grp.ctl.SetRect(WorldRect(grp.pos, new Vector2(grp.scale * grp.tw, grp.scale * grp.th)));
                float sw = grp.scale * grp.w, sh = grp.scale * grp.h;
                int pw = (int)(sw * 0.25f), ph = (int)(sh * 0.25f);
                for (int j = 0; j < 16; j++)
                {
                    var pic = new JigPic();
                    pic.ctl = CreateControl(9200 + 16 * g + j, -1); pic.ctl.touchable = false;
                    pic.ctl.SetTexture(PieceFrame(g, j));
                    pic.active = Profile.jigsawActive[16 * g + j] != 0;
                    pic.ctl.SetVisible(pic.active);
                    if (pic.active && jigCursor == g) { }
                    if (Profile.jigsawActive[16 * g + j] != Profile.jigsawGet[16 * g + j] && Profile.jigsawGet[16 * g + j] != 0)
                        jigPending.Add(new Vector2Int(g, j));
                    pic.size = new Vector2(pw, ph);
                    pic.pos = PiecePos(grp.pos, sw, sh, pw, ph, j);
                    pic.ctl.SetRect(WorldRect(pic.pos, pic.size));
                    grp.pics.Add(pic);
                }
                jigGroups.Add(grp);
            }
            int active = CountActive(jigCursor);

            Rect cr = N.Layout(379, 10, 200, 40); cr = new Rect(cr.x, cr.y + 32f, cr.width, cr.height);
            DeleteLabel(599);
            var cl = CreateLabel(599, 4, 44); cl.SetNative("Adventure_22", 0f, 0f, 0, 0);   // showJigsawUI 0xfdf70 (emulation): Adventure_22, colour (239,205,0,255), default alignment
            cl.SetColor(new Color32(0xEF, 0xCD, 0, 255)); cl.SetRect(cr);
            jigCountText = active + "/16"; cl.SetText(jigCountText);
            MoveRightLabel(cl, cr);

            Rect ir = N.Layout(0, 250, 480, 60); ir = new Rect(ir.x, ir.y + 22f, ir.width, ir.height);
            DeleteLabel(598);
            var il = CreateLabel(598, 4, 24); il.SetNative("Adventure_12", 0f, -4f, 1, 0);   // Adventure_12, SetAlignment(1, 0), SetLineSpacing(-4)
            il.SetColor(new Color32(0xEF, 0xCD, 0, 255)); il.SetRect(ir);   // SetAlignment(1,0): centred text block, lines left-aligned, top (UILabel::UpdateText 0x180040)
            il.SetText("");
            MoveRightLabel(il, ir);
            ShowJigsawInfo();

            if (jigPending.Count != 0)
            {
                var last = jigPending[jigPending.Count - 1];
                AlignmentJigsaw(last.x, last.y);
            }
            else CreateJigsawBack(false);
        }

        void CreateJigsawBack(bool scaleIn)
        {
            DeleteControl(17);
            var back = CreateControl(17, 1);
            back.SetTexture("ui_btn_backFrame");
            Rect bk = N.Layout(16, 284, 104, 30); bk = new Rect(bk.x, bk.y + 22f, bk.width, bk.height);
            back.SetRect(bk);
            DeleteControl(18);
            var bi = CreateControl(18, 1); bi.touchable = false;
            bi.SetTexture("ui_btn_back_imgFrame");
            Rect br = N.Layout(51, 285, 69, 27); br = new Rect(br.x, br.y + 22f, br.width, br.height);
            bi.SetRect(br);
            if (scaleIn) { ScaleIn(back, bk); ScaleIn(bi, br); }
            else { MoveRight(back, bk, White); MoveRight(bi, br, White); }
        }

        static Vector2 PiecePos(Vector2 gpos, float sw, float sh, int pw, int ph, int j)
        {
            float x = -0.5f * sw + gpos.x + (float)pw * 0.5f + (j % 4) * pw;
            float y = 0.5f * sh + gpos.y + (float)ph * -0.5f - (j / 4) * ph;
            return new Vector2(x, y);
        }

        int CountActive(int group)
        {
            int n = 0;
            for (int j = 0; j < 16; j++) if (Profile.jigsawActive[16 * group + j] != 0) n++;
            return n;
        }

        /// <summary>showJigsawInfo 0xf47a4: counter label, per-group bonus variable and description text.</summary>
        void ShowJigsawInfo()
        {
            int n = CountActive(jigCursor);
            jigCountText = n + "/16";
            foreach (var l in Labels) if (l.id == 599) l.SetText(jigCountText);
            if (n == 16) n = 20;
            switch (jigCursor)
            {
                case 0: Profile.jigsaw1Count = 5 * n; break;
                case 1: Profile.jigsaw2Count = 5 * n; break;
                case 2: Profile.jigsaw3Count = 5 * n; break;
                case 3: Profile.jigsaw4Count = 5 * n; break;
                case 4: Profile.jigsaw5Count = n; break;
            }
            int value = jigCursor == 4 ? n : 5 * n;
            jigInfoText = JigsawInfoFormats[jigCursor].Replace("%d", value.ToString()).Replace("%%", "%");
            foreach (var l in Labels) if (l.id == 598) l.SetText(jigInfoText);
        }

        // ---------------------------------------------------------------- drag / settle
        void JigsawApply(float dx, bool dragging)
        {
            jigMinDist = 100000f;
            float lo = N.S * -200f, hi = N.S * 200f;
            for (int k = 0; k < jigGroups.Count; k++)
            {
                var g = jigGroups[k];
                float x0 = g.pos.x;
                float ax = Mathf.Abs(x0);
                float sc = (x0 < lo || x0 > hi) ? 0.8f : (ax * -0.001f) / N.S + 1f;
                float sw = sc * g.w, sh = sc * g.h;
                g.pos = new Vector2(x0 + dx, g.pos.y);
                g.ctl.SetRect(WorldRect(g.pos, new Vector2(sc * g.tw, sc * g.th)));
                if (dragging && ax < jigMinDist) { jigMinDist = ax; jigCursor = k; }
                int pw = (int)(sw * 0.25f), ph = (int)(sh * 0.25f);
                for (int j = 0; j < g.pics.Count; j++)
                {
                    var p = g.pics[j];
                    p.pos = PiecePos(g.pos, sw, sh, pw, ph, j);
                    p.size = new Vector2(pw, ph);
                    p.ctl.SetRect(WorldRect(p.pos, p.size));
                }
            }
        }

        protected override void OnTouchEvent(int type, Vector2 p)
        {
            if (Game.MapType == 4) { BankTouch(type, p); return; }
            if (Game.MapType != 5) return;
            if (GetControl(18) == null) return;                 // back image exists once the UI is interactive
            if (type == 0)
            {
                jigLast = p; jigStart = p; jigSettling = false; jigStartGroup = jigCursor;
            }
            else if (type == 1)
            {
                if (jigLast.x != p.x)
                {
                    JigsawApply(p.x - jigLast.x, true);
                    jigLast = p;
                }
            }
            else if (type == 2)
            {
                if (jigStart.x == p.x) return;
                jigSettling = true;
                float S = jigStart.x, E = p.x;
                if (S > E + 100f && jigCursor == jigStartGroup) { if (jigStartGroup <= 3) jigCursor = jigStartGroup + 1; }
                else if (S < E - 100f && jigCursor == jigStartGroup && jigStartGroup > 0) jigCursor = jigStartGroup - 1;
                ShowJigsawInfo();
                Audio.PlaySound("A_jigsaw_touchend");
            }
        }

        void JigsawUpdate(float dt)
        {
            JigsawIntroPoll();
            if (jigFly != null) UpdateFly(dt);
            if (jigEffect != null) UpdateEffect(dt);
            if (!jigSettling || jigGroups.Count == 0) return;
            float x = jigGroups[jigCursor].pos.x;
            if (Mathf.Abs(x) > 1f)
            {
                float f = 0.05f * N.S;
                float delta = (x + f * (0f - x)) - x;
                JigsawApply(delta, false);
            }
            else
            {
                jigMinDist = 100000f; jigSettling = false;
                ShowJigsawInfo();
                if (jigPending.Count != 0) MovePicToJigsaw(jigAlignPiece);
            }
        }

        // ---------------------------------------------------------------- new piece animation
        void AlignmentJigsaw(int group, int piece)
        {
            jigAlignPiece = piece;
            if (jigCursor == group) MovePicToJigsaw(piece);
            else { jigCursor = group; jigSettling = true; }
        }

        void MovePicToJigsaw(int piece)
        {
            ShowJigsawInfo();
            var g = jigGroups[jigCursor];
            int pw = (int)(g.w * 0.25f), ph = (int)(g.h * 0.25f);
            float x = g.w * -0.5f + g.pos.x + (float)pw * 0.5f + (piece % 4) * pw;
            float y = g.h * 0.5f + g.pos.y + (float)ph * -0.5f - (piece / 4) * ph;
            var fly = new JigFly();
            fly.ctl = CreateControl(9300, -1); fly.ctl.touchable = false;
            fly.ctl.SetTexture(PieceFrame(jigCursor, piece));
            fly.p0 = new Vector2(0f, N.S * -140f); fly.s0 = new Vector2(2 * pw, 2 * ph);
            fly.p1 = new Vector2(x, y); fly.s1 = new Vector2(pw, ph);
            fly.ctl.SetColor(new Color32(255, 255, 255, 0));
            fly.ctl.SetRect(WorldRect(fly.p0, fly.s0));
            jigFly = fly;
        }

        void UpdateFly(float dt)
        {
            var f = jigFly;
            f.t += dt;
            float r = f.t / 1.0f;
            f.ctl.SetRect(WorldRect(Vector2.LerpUnclamped(f.p0, f.p1, r), Vector2.LerpUnclamped(f.s0, f.s1, r)));
            f.ctl.SetColor(new Color32(255, 255, 255, (byte)Mathf.Clamp(255f * r, 0f, 255f)));
            if (r > 1f)
            {
                DeleteControl(9300); jigFly = null;
                JigsawPicCallBack();
            }
        }

        /// <summary>ligsawPicCallBack 0xf514e.</summary>
        void JigsawPicCallBack()
        {
            var last = jigPending[jigPending.Count - 1];
            var pic = jigGroups[last.x].pics[last.y];
            pic.active = true; pic.ctl.SetVisible(true);
            Audio.PlaySound("A_jigsaw_moveend");
            LastJigsawSlotPos = pic.pos; LastJigsawSlotControl = pic.ctl;   // DEBUG_ONLY verifier probe
            AddJigsawEff(pic.pos);
            Profile.jigsawActive[16 * last.x + last.y] = 1;
            jigPending.RemoveAt(jigPending.Count - 1);
            ShowJigsawInfo();
        }

        void AddJigsawEff(Vector2 worldPos)
        {
            jigEffectsPlayed++;
            Atlas.LoadByConf("DC2/Image/ip/jigeff2x", "DC2/Image/ip/jigeff2x.conf");
            var anim = NativeComponentAnimation.Load("DC2/Image/acb/jigsaw_1.animation.acb");
            if (jigEffectRoot != null) Util.Kill(jigEffectRoot);
            jigEffectRoot = new GameObject("jigsaw_effect"); jigEffectRoot.transform.SetParent(transform, false);
            // r119c (user: "the orange flash is drawn under the jigsaw background"): the board (bg 9100, panels 9110+, pieces 9200+, fly 9300) are world
            // GameObjects natively, as is the flash (AddJigsawEff 0xf4ffa, layer 0, created after them; its jigeff2x texture is created after the jigsaw_h ones, and
            // RenderManager mode 0 sorts by texture, then creation), so the flash draws over the board. The port's board stand-ins use orders 700..999 and the flash
            // sprites 0..n; one sorting group at 999 puts the flash above every stand-in and still under the UI (1000+).
            // r119d (user, iPad: "the flash is drawn under the jigsaw piece"): above the board background (layer -3, 700..) and the group panels
            // (layer -2, 800..), under the pieces and the fly-in piece (layer -1, 900..). OBSERVED ordering (the native order between these world objects is
            // texture / creation based and not fully recovered); supersedes the r119c 'above everything' placement.
            jigEffectRoot.AddComponent<UnityEngine.Rendering.SortingGroup>().sortingOrder = UiControl.UiOrderBase - 101;
            // AddJigsawEff ARM0xf4fb8/0xf50f6 writes iPad scale2 to CompAnimObj+228.
            jigEffect = new NativeComponentPlayer(anim, jigEffectRoot.transform, worldPos, N.S) { oneShot = true };
        }

        void UpdateEffect(float dt)
        {
            jigEffect.Step(dt);
            if (jigEffect.Finished)
            {
                jigEffect = null; Util.Kill(jigEffectRoot); jigEffectRoot = null;
                JigsawEffCallBack();
            }
        }

        /// <summary>ligsawEffCallBack 0xf90de.</summary>
        void JigsawEffCallBack()
        {
            if (jigPending.Count != 0)
            {
                var last = jigPending[jigPending.Count - 1];
                AlignmentJigsaw(last.x, last.y);
            }
            else
            {
                CreateJigsawBack(true);
                Game.SaveFile();
            }
        }

        bool HandleJigsawEvent(UiControl c, int ev)
        {
            if (Game.MapType != 5) return false;
            if (ev == 0 && c.id == 17)
            {
                lastAction = "jigsaw:back";
                Game.MapType = 0; Game.SwitchScene("selectScene");
            }
            return true;
        }
    }
}
