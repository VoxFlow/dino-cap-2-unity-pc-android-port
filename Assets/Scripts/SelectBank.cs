using System.Collections.Generic;
using UnityEngine;

namespace Dc2Pilot
{
    /// <summary>
    /// selectScene m_map_type 4 ("bank" / IAP store).
    /// Native: selectScene::OnLoad case 4 (0x1066aa, iPad branch 0x1066d2..0x1076fe), showIAP 0x101ad2,
    /// OnTouchEvent type-4 branch (0xf57bc), OnUpdate type-4 purchase poll (0xf93c4), iapaddmoney 0xf46d2,
    /// DinoCap::IAPPurchase 0x5a1f0, OnUIEvent case 4 (0x10bf04).  Registry: dc2.bank.*.
    /// ADAPTATION: StoreKit does not exist on PC, so IAPManager::GetPurchaseStatus is supplied by <see cref="iapResult"/>.
    /// </summary>
    public partial class SelectScene
    {
        // IAPManager::GetPurchaseStatus values observed in OnUpdate: 2 = success; 3,4,5 = ended without payment; 0/1 = still waiting.
        public const int IapSuccess = 2, IapCancelled = 4;
        public static readonly string[] IapProductIds =
        {
            "com.trinitigame.dinocap2hd.099centsnew", "com.trinitigame.dinocap2hd.299centsnew",
            "com.trinitigame.dinocap2hd.499centsnew", "com.trinitigame.dinocap2hd.999centsnew",
            "com.trinitigame.dinocap2hd.1999centsnew", "com.trinitigame.dinocap2hd.099cents2",
        };
        // iapaddmoney: cases 0..4 add these amounts to cash (dword_24047C); case 5 sets g_max_bullet_count = 999.
        public static readonly int[] IapCash = { 0x3A98, 0xC350, 0x17318, 0x35B60, 0x7A120 };

        public int bankSelected = -1;      // native this+0x28
        public int iapPending = -1;        // native this+0xF0 (product being purchased)
        public bool iapBusy;               // native this+236
        public bool activityIndicator;     // Triniti2D::OSShowActivityIndicator(…,1001) shown
        public int iapResult = IapCancelled;   // ADAPTATION PA-009: result the (absent) store returns
        public float iapDelay = 0.5f;      // ADAPTATION PA-009: time before the store answers
        public string lastPurchaseProduct = "";
        float iapElapsed;
        public readonly List<Rect> bankRects = new List<Rect>();   // native std::vector<Rect> at this+0x1B4

        static bool PtInRect(Rect r, Vector2 p) { return p.x > r.x && p.y > r.y && p.x < r.x + r.width && p.y < r.y + r.height; }   // Triniti2D::Rect::PtInRect (strict)

        void ShowBankScene()
        {
            var tex = Atlas.LoadTexture("DC2/Image/ip/uiiap_h");
            // CreateFrame calls at 0x1066f0..0x106b10 (iPad): rect = (x, y, w, h) in texture pixels
            Atlas.CreateFrame("ui_iap_bgFrame", tex, 0, 0, 960, 640);
            Atlas.CreateFrame("ui_btn_backFrame", tex, 0, 640, 208, 62);
            Atlas.CreateFrame("ui_goldFrame", tex, 960, 0, 46, 44);
            Atlas.CreateFrame("ui_iap_selectFrame", tex, 208, 640, 300, 208);
            Atlas.CreateFrame("ui_btn_back_imgFrame", tex, 508, 640, 138, 54);

            // six product cells (1x rect 150x104) laid out 3x2; RectScale, iPad pixel nudge, then RectOffset
            float[] xs = { 10, 165, 314, 10, 165, 314 };
            float[] ys = { 54, 54, 54, 161, 161, 161 };
            float[] dx = { -20, 0, 20, -20, 0, 20 };
            float[] dy = { 18, 18, 18, 45, 45, 45 };
            bankRects.Clear();
            for (int i = 0; i < 6; i++)
            {
                Rect r = N.RectScale(xs[i], ys[i], 150f, 104f);
                r = new Rect(r.x + dx[i], r.y + dy[i], r.width, r.height);
                bankRects.Add(N.RectOffset(r));
            }

            // GameObject background: SetSize(960 + g_w_offset, 640 + g_h_offset) at world (0,0).  World origin is the screen centre
            // (dc2.engine.camera, LIKELY) so the 1024x704 sprite spans UI y = 32..736; modelled as the lowest UI layer.
            var bg = CreateControl(9001, -1); bg.touchable = false;
            bg.SetTexture("ui_iap_bgFrame");
            bg.SetRect(new Rect(0, 0.5f * N.HOffset, 480f * N.S + N.WOffset, 320f * N.S + N.HOffset));

            ShowIAP();
            bankSelected = -1; iapPending = -1; iapBusy = false; activityIndicator = false;
        }

        /// <summary>selectScene::showIAP 0x101ad2.</summary>
        void ShowIAP()
        {
            DeleteControl(50);
            var gold = CreateControl(50, 0); gold.touchable = false;
            gold.SetTexture("ui_goldFrame");
            Rect gr = N.Layout(364, 14, 23, 22); gold.SetRect(gr); MoveRight(gold, gr, White);

            DeleteControl(51); DeleteLabel(51);
            var gl = CreateLabel(51, 0, 30); gl.consumesTouch = true;   // r118z4: native label left enabled - consumes touches in its rect (showIAP 0x101c64)
            gl.SetNative("Adventure_16", 0f, 0f, 0, 0);   // showIAP 0x101ad2 (emulation)
            gl.SetColor(new Color32(255, 205, 0, 255));
            Rect glr = N.Layout(387, 10, 200, 30);
            gl.SetRect(glr);
            gl.SetText(UpdateExpCash());   // selectScene::UpdateExp (clamp + store)
            MoveRightLabel(gl, glr);

            DeleteControl(17);
            var back = CreateControl(17, 1);
            back.SetTexture("ui_btn_backFrame");
            Rect bk = N.Layout(16, 284, 104, 30);
            bk = new Rect(bk.x, bk.y + 22f, bk.width, bk.height);
            back.SetRect(bk); ScaleIn(back, bk);

            DeleteControl(18);
            var backImg = CreateControl(18, 1); backImg.touchable = false;
            backImg.SetTexture("ui_btn_back_imgFrame");
            Rect bi = N.Layout(51, 285, 69, 27);
            bi = new Rect(bi.x, bi.y + 22f, bi.width, bi.height);
            backImg.SetRect(bi); ScaleIn(backImg, bi);
        }

        /// <summary>OnUIEvent case 4: any click leaves the bank for dword_23E0A4's screen.</summary>
        bool HandleBankEvent(UiControl c, int ev)
        {
            if (Game.MapType != 4) return false;
            if (ev != 0) return true;
            lastAction = "bank:back";
            Game.MapType = Game.LastMapType;       // native also clears the byte at 0x23b8a4 here
            Game.SwitchScene("selectScene");
            return true;
        }

        /// <summary>selectScene::OnTouchEvent type-4 branch.</summary>
        void BankTouch(int type, Vector2 p)
        {
            if (type == 0)
            {
                for (int i = 0; i < bankRects.Count; i++)
                {
                    if (!PtInRect(bankRects[i], p)) continue;
                    bankSelected = i;
                    DeleteControl(94);
                    var sel = CreateControl(94, 0); sel.touchable = false;
                    sel.SetRect(bankRects[bankSelected]);
                    sel.SetTexture("ui_iap_selectFrame");
                }
            }
            else if (type == 2)
            {
                for (int i = 0; i < bankRects.Count; i++)
                {
                    if (!PtInRect(bankRects[i], p)) continue;
                    if (bankSelected == i) StartPurchase(i);
                }
                bankSelected = -1;
            }
        }

        void StartPurchase(int product)
        {
            activityIndicator = true;                         // OSShowActivityIndicator(…, 1001)
            var shade = CreateControl(10, 3);
            shade.SetTexture("sd_baiFrame");
            // native: w = 960 + g_w_offset; h = (640 + 0.5*g_h_offset) + g_h_offset (the intermediate store is overwritten)
            shade.SetRect(new Rect(0, 0, 480f * N.S + N.WOffset, 320f * N.S + 0.5f * N.HOffset + N.HOffset));
            shade.SetColor(new Color32(0, 0, 0, 160));          // 0xA0000000
            shade.consumesTouch = true;   // r118z4: plain UIImage with SetEnable(1) (selectScene::OnTouchEvent 0xf5d46 / 0xf5e30): swallows touches, no click event or sound
            iapBusy = true;
            iapPending = bankSelected;
            iapElapsed = 0f;
            lastPurchaseProduct = IapProductIds[product];       // IAPPurchase(i): product id string
        }

        void BankUpdate(float dt)
        {
            if (!iapBusy) return;
            iapElapsed += dt;
            if (iapElapsed < iapDelay) return;                 // status 0/1: nothing happens
            int status = iapResult;
            if (status == IapSuccess)
            {
                DeleteControl(10); iapBusy = false;
                if (iapPending > -1) IapAddMoney(iapPending);
                iapPending = -1; bankSelected = -1; activityIndicator = false;
            }
            else if (status >= 3 && status <= 5)
            {
                DeleteControl(10); iapBusy = false;
                iapPending = -1; bankSelected = -1; activityIndicator = false;
            }
        }

        /// <summary>selectScene::iapaddmoney 0xf46d2.  (g_total_time / SaveMissionInfo bookkeeping is not modelled.)</summary>
        void IapAddMoney(int i)
        {
            if (i >= 0 && i <= 4) Profile.cash += IapCash[i];
            else if (i == 5) Profile.maxBullet = 999;
            Profile.iapTime[i]++;
            UpdateGoldLabel();
            Game.SaveFile();
        }
    }
}
