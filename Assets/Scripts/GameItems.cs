using System.Collections.Generic;
using UnityEngine;

namespace Dc2Pilot
{
    /// <summary>itemsClass pickup (Dev/Evidence/items_decomp_r107.md): ammo crates / HP kits dropped by dinosaurs in arena (7) and boss (8) missions.</summary>
    public sealed class ItemActor
    {
        public GameObject root;
        public SpriteRenderer sprite, glow;
        public int kind;                 // weapon type 0..5 (ammo) or 6 (HP kit, native +132 = -1)
        public float x, baseY;           // +160, +156
        public bool active, picked, goingDown;   // +165, +166, +164
        public float y, vy, w, h;
    }

    public partial class GameScene
    {
        readonly List<ItemActor> items = new List<ItemActor>();
        public int ItemsDropped, ItemsPicked;   // DEBUG_ONLY counters

        /// <summary>dinosaurClass::Die 0x140fc: only mission types 7/8 (map record +8), RandomRange(0,1000)%100 &lt;= 5.</summary>
        void MaybeDropItem(float x)
        {
            if (missionType != 7 && missionType != 8) return;
            if (RandomRange(0, 1000) % 100 <= 5) DropItem(x);
        }

        /// <summary>lifeModeScene::DropItem 0xe1470.</summary>
        public void DropItem(float x)
        {
            float s = N.S;
            float left = -240f, right = missionType == 7 || missionType == 8 ? 840f : 50000f;   // scene+136 / +140 (lifeMode bounds as used by the port)
            if (x < N.Add32(N.Mul32(s, 20f), left)) return;
            if (x > N.Add32(N.Mul32(s, -20f), right)) return;
            var weapons = Dc2GameData.PlayerWeapons(heroType);
            var types = new List<int>();
            for (int i = 0; i < weapons.Count - 1 && i < 5; i++)
                if (Profile.weaponOwned[heroType * 6 + i] >= 0) types.Add(weapons[i].type);
            int n = types.Count;
            types.Add(6);                                            // v24[v16] = 6 (unreachable with RandomRange(1,n+1) except the rounding quirk)
            int pick = RandomRange(1, n + 1);
            int kind = types[Mathf.Clamp(pick - 1, 0, types.Count - 1)];
            CreateItem(kind, x, N.Add32(N.Mul32(s, 20f), -110f));   // y = S*20 + g_ground_y
        }

        /// <summary>itemsClass::CreatItem 0x11582a + CloneItems 0x1155dc (iPad scale 1.8 for sizes).</summary>
        void CreateItem(int kind, float x, float baseY)
        {
            float k = 1.8f;
            var it = new ItemActor { kind = kind, x = x, baseY = baseY };
            string frame;
            if (kind == 6) { frame = "LxuepingFrame"; it.w = N.Mul32(k, 47f); it.h = N.Mul32(k, 60f); }
            else { frame = "Lbullet" + kind + "Frame"; it.w = N.Mul32(k, 80f); it.h = N.Mul32(k, kind == 2 ? 75f : 74f); }
            it.root = new GameObject("itemsClass");
            it.root.transform.SetParent(Background.LayerRoot(12), false);   // CloneItems: sprite layer field 12
            var go = new GameObject("item"); go.transform.SetParent(it.root.transform, false);
            it.sprite = go.AddComponent<SpriteRenderer>(); it.sprite.sortingOrder = 100;   // PLATFORM_ADAPTER: background children use sortingOrder = layer inside the same group; later-created equal-layer objects draw in front
            if (Atlas.Has(frame)) it.sprite.sprite = Atlas.Get(frame);
            var gg = new GameObject("glow"); gg.transform.SetParent(Background.LayerRoot(11), false);
            it.glow = gg.AddComponent<SpriteRenderer>(); it.glow.sortingOrder = 100;
            if (Atlas.Has("L07Frame")) it.glow.sprite = Atlas.Get("L07Frame");
            it.y = N.Add32(N.Mul32(k, 20f), baseY);
            items.Add(it);
            ItemsDropped++;
            PlaceItem(it);
        }

        void PlaceItem(ItemActor it)
        {
            float cx = N.ScreenW * 0.5f + (it.x - (camLeft + WorldViewportW * 0.5f));
            float cy = -N.ScreenH * 0.5f + it.y;
            Place(it.sprite, cx, cy, it.w, it.h);
            // glow child: CloneItems "L07Frame", size 1.8*130 square at y = 1.8*15 + base (offset relative to the item)
            float gs = N.Mul32(1.8f, 130f);
            Place(it.glow, cx, cy + N.Add32(N.Mul32(1.8f, 15f), it.baseY) - N.Add32(N.Mul32(1.8f, 20f), it.baseY), gs, gs);
            it.glow.enabled = !it.picked || it.kind != 6;
        }

        static void Place(SpriteRenderer r, float cx, float cy, float w, float h)
        {
            if (r.sprite == null) { r.enabled = false; return; }
            r.enabled = true;
            r.transform.localPosition = new Vector3(cx, cy, 0f);
            var size = r.sprite.rect.size;
            r.transform.localScale = new Vector3(w / size.x, h / size.y, 1f);
        }

        /// <summary>itemsClass::OnUpdate 0x115cc4 (+ upDown 0x115508).</summary>
        void UpdateItems(float dt)
        {
            UpdateItemPopup(dt);
            float s = N.S;
            for (int i = items.Count - 1; i >= 0; i--)
            {
                var it = items[i];
                if (it.picked)
                {
                    if (it.kind == 6) PlaceItem(it);   // "+HP" popup: its body rises in the physics stage (PrepareItemPhysics) until "secondtimer" (5.0) deletes it
                    continue;
                }
                float dxCam = Mathf.Abs(N.Add32(camLeft, -it.x));
                if (dxCam > N.Mul32(s, 1000f))
                {
                    it.active = false; it.vy = 0f; it.y = N.Add32(N.Mul32(s, 20f), it.baseY);
                    PlaceItem(it); continue;
                }
                if (!it.active) { it.active = true; it.y = N.Add32(N.Mul32(s, 20f), it.baseY); }
                if (Mathf.Abs(N.Add32(heroX, -it.x)) < 60f)
                {
                    Pickup(it);
                    if (it.kind != 6) { Object.Destroy(it.root); Object.Destroy(it.glow.gameObject); items.RemoveAt(i); }
                    continue;
                }
                // upDown: bobbing between base+30 and base+50 at 20 px/s (raw units)
                if (!it.goingDown)
                {
                    if (N.Add32(N.Mul32(s, 25f), it.baseY) > it.y) it.vy = 20f; else it.goingDown = true;   // 0x115574: direction byte only, no SetLinearVelocity (Codex R5)
                }
                else
                {
                    if (N.Add32(N.Mul32(s, 15f), it.baseY) < it.y) it.vy = -20f; else it.goingDown = false;   // 0x1155ac: direction byte only; the body keeps its velocity (Codex R5)
                }
                PlaceItem(it);   // the new velocity moves the body in the NEXT physics stage (PrepareItemPhysics), as SetLinearVelocity does natively
            }
        }

        /// <summary>b2World::Step for the item bodies (r118z6; Codex review r1 bound "item / global physics"). PhysicsManager::Update 0x17610a runs before
        /// every callback and GameObject::BeforeUpdate 0x16c136 copies the body position, so itemsClass::OnUpdate 0x115cc4 / upDown 0x115508 read the position
        /// moved with the PREVIOUS tick's velocity and only set a new one (SetLinearVelocity 0x1155a6; pickup (0, 50*S) 0x115fd4). The port integrated inside
        /// the callback with the velocity it had just chosen, one tick ahead.</summary>
        void PrepareItemPhysics(float dt)
        {
            float s = N.S;
            foreach (var it in items)
            {
                if (it.picked) { if (it.kind == 6) it.y = N.Add32(it.y, N.Mul32(N.Mul32(s, 50f), dt)); }
                else it.y = N.Add32(it.y, N.Mul32(it.vy, dt));
            }
        }

        /// <summary>itemsClass::OnTimeout "secondtimer" 0x115afc: Enable(false) + DeleteGameObject, in the Timer::Update stage.</summary>
        void ItemSecondTimer(ItemActor it)
        {
            if (!items.Remove(it)) return;
            Object.Destroy(it.root); Object.Destroy(it.glow.gameObject);
        }

        void Pickup(ItemActor it)
        {
            Audio.PlaySound("Audio_itemAudio");
            ItemsPicked++;
            if (it.kind == 6)
            {
                // heal: (int)((int)(maxHP + jigsaw1 bonus) * 0.25), then clamp to GetPlayerHP
                int add = (int)N.Mul32(N.F32((int)playerMaxHp), 0.25f);
                playerHp = Mathf.Min(playerMaxHp, N.Add32(playerHp, N.F32(add)));
                UpdateHpBar();
                it.picked = true; SetTimer(it, "secondtimer", 5f, () => ItemSecondTimer(it));   // itemsClass::OnUpdate 0x115e9e: GameObject::SetTimer("secondtimer",5.0), once (+166)
                it.sprite.sprite = Atlas.Has("LaddhpFrame") ? Atlas.Get("LaddhpFrame") : it.sprite.sprite;
                it.w = N.Mul32(N.S, 112f); it.h = N.Mul32(N.S, 36f);
                return;
            }
            int gainedAmmo = AddBulletZhuan(it.kind);
            if (it.kind <= 5) StartItemPopup(it.kind, it.x, it.y, gainedAmmo);   // itemsClass::AddBulletZhuan 0x115344 -> itemClass::Start 0x97f8 (a4 = 1 from itemsClass::OnUpdate 0x1160d2)
        }

        static readonly int[][] ammoTiers =
        {   // [type] -> amounts for roll<40, <70, <90, >=90 (itemsClass::AddBulletZhuan 0x115344 decoded tiers)
            new[] { 24, 36, 48, 60 }, new[] { 8, 16, 24, 48 }, new[] { 30, 60, 90, 120 },
            new[] { 40, 80, 120, 20 }, new[] { 5, 10, 15, 30 }, new[] { 10, 20, 30, 40 }
        };

        /// <summary>itemsClass::AddBulletZhuan + mainScene::AddBullet 0x97110: every slot (0..4) of that weapon type gains the amount, capped at g_max_bullet_count.</summary>
        public int AddBulletZhuan(int type)
        {
            if (type > 5) return 0;
            int roll = (int)N.Mul32(N.Mul32(Rand01(), 100f), 1f);        // trunc(rand01*100)
            int tier = roll < 40 ? 0 : roll < 70 ? 1 : roll < 90 ? 2 : 3;
            int amount = ammoTiers[type][tier];
            var weapons = Dc2GameData.PlayerWeapons(heroType);
            for (int i = 0; i < weapons.Count - 1 && i < 5; i++)
                if (weapons[i].type == type)
                {
                    int idx = heroType * 6 + i;
                    Profile.weaponAmmo[idx] = Mathf.Min(Profile.maxBullet, Profile.weaponAmmo[idx] + amount);
                }
            UpdateBullet();
            return amount;
        }

        // ---- itemClass pickup popup: [ammo icon] x<amount>, rising at S*50 px/s (Dev/Evidence/item_popup_r118.md; NATIVE ARM, floats struct-decoded) ----
        sealed class ItemPopup
        {
            public GameObject root;
            public SpriteRenderer icon, mul;
            public readonly List<SpriteRenderer> digits = new List<SpriteRenderer>();
            public readonly List<Vector2> digitSize = new List<Vector2>();   // itemClass +76 vector: startScale * (digit width, 31)
            public float x, y, vy, scale, iconW, iconH, iconOffX, iconOffY;
            public int type, amount;
        }
        ItemPopup popup;   // the single itemClass object (scene +168); Start re-uses it on every pickup

        // itemClass::Start 0x97f8 per-type tables (width, height, offX, offY; each multiplied by S): ARM literal pairs at 0x98b2.. / 0x98e4.. / 0x991e.. / 0x9950.. / 0x9982.. / 0x99b4.. (types 0..5)
        static readonly float[][] popupIconTable =
        {
            new[] { 31f, 35f, -18f, 20f }, new[] { 53f, 56f, -20f, 25f }, new[] { 43f, 42f, -20f, 25f },
            new[] { 51f, 59f, -22f, 28f }, new[] { 51f, 56f, -25f, 25f }, new[] { 49f, 46f, -27f, 25f }
        };
        // digit width by digit (switch at 0x9b94: 0x9bcc 24, 0x9c78 14, 0x9c94 22, 0x9c7c 22, 0x9c82 24, 0x9c86 23, 0x9c8a 23, 0x9c8e 20, 0x9c94 22, 0x9bc8 26); height 31 (0x41F80000)
        static readonly float[] popupDigitWidth = { 24f, 14f, 22f, 22f, 24f, 23f, 23f, 20f, 22f, 26f };

        /// <summary>itemClass::Start: scale = S*0.5, position = pickup position, linear velocity (0, S*50) (gravity (0,0), damping 0), frame icon_item{type}Frame, digits icon_num{d}Frame of sprintf("%d", amount).</summary>
        void StartItemPopup(int type, float x, float y, int amount)
        {
            float s = N.S;
            if (popup == null)
            {   // itemClass::OnLoad 0x7988: icon object, icon_itemxFrame object (size S*10 square), sprite layer field 13
                popup = new ItemPopup();
                popup.root = new GameObject("itemClass"); popup.root.transform.SetParent(Background.LayerRoot(13), false);
                popup.icon = NewPopupSprite("icon"); popup.mul = NewPopupSprite("x");
            }
            popup.type = type; popup.amount = amount;
            popup.x = x; popup.y = y; popup.vy = N.Mul32(s, 50f);
            popup.scale = N.Mul32(s, 0.5f);                                  // +32
            var t = popupIconTable[type];
            popup.iconW = N.Mul32(s, t[0]); popup.iconH = N.Mul32(s, t[1]);       // +60 / +64
            popup.iconOffX = N.Mul32(s, t[2]); popup.iconOffY = N.Mul32(s, t[3]); // +36 / +40
            string iconFrame = "icon_item" + type + "Frame";
            if (Atlas.Has(iconFrame)) popup.icon.sprite = Atlas.Get(iconFrame);
            if (Atlas.Has("icon_itemxFrame")) popup.mul.sprite = Atlas.Get("icon_itemxFrame");
            popup.digitSize.Clear();
            string digits = amount.ToString();
            for (int i = 0; i < digits.Length; i++)
            {
                if (i >= popup.digits.Count) popup.digits.Add(NewPopupSprite("digit" + i));
                int d = digits[i] - '0';
                var r = popup.digits[i];
                string frame = "icon_num" + d + "Frame";
                r.sprite = Atlas.Has(frame) ? Atlas.Get(frame) : null;
                popup.digitSize.Add(new Vector2(N.Mul32(popup.scale, popupDigitWidth[d]), N.Mul32(popup.scale, 31f)));
            }
            for (int i = digits.Length; i < popup.digits.Count; i++) popup.digits[i].enabled = false;   // surplus digit objects are disabled (0x9cf8)
            PlacePopup();
        }

        SpriteRenderer NewPopupSprite(string name)
        {
            var go = new GameObject(name); go.transform.SetParent(popup.root.transform, false);
            var r = go.AddComponent<SpriteRenderer>(); r.sortingOrder = 100; r.enabled = false;   // same equal-layer ordering stand-in as ItemActor
            return r;
        }

        /// <summary>itemClass::OnUpdate 0x7bbe: scale += 5 dt (capped at 1.0), then icon / "x" / digits are placed from the object position; physics then moves the object up at S*50 px/s.</summary>
        void UpdateItemPopup(float dt)
        {
            if (popup == null) return;
            popup.scale = N.Add32(popup.scale, N.Mul32(dt, 5f));
            if (popup.scale > 1f) popup.scale = 1f;
            PlacePopup();
            popup.y = N.Add32(popup.y, N.Mul32(popup.vy, dt));
        }

        void PlacePopup()
        {
            float s = N.S, k = popup.scale;
            float camCenterX = camLeft + WorldViewportW * 0.5f;
            System.Func<float, float> sx = wx => N.ScreenW * 0.5f + (wx - camCenterX);
            System.Func<float, float> sy = wy => -N.ScreenH * 0.5f + wy;
            // obj+8 icon: position = p + k*(+36,+40), size = k*(+60,+64)
            Place(popup.icon, sx(N.Add32(popup.x, N.Mul32(k, popup.iconOffX))), sy(N.Add32(popup.y, N.Mul32(k, popup.iconOffY))), N.Mul32(k, popup.iconW), N.Mul32(k, popup.iconH));
            // obj+12 "x": position = p + k*(S*-5, S*10), size = k*(S*10, S*10); hidden for type 6 (not reachable here)
            float xs = N.Mul32(s, 10f);
            Place(popup.mul, sx(N.Add32(popup.x, N.Mul32(k, N.Mul32(s, -5f)))), sy(N.Add32(popup.y, N.Mul32(k, xs))), N.Mul32(k, xs), N.Mul32(k, xs));
            // digits: first at p + k*(S*5, S*15); each next advances by the previous digit's width (v43[0] += size.x)
            float dx = N.Add32(popup.x, N.Mul32(k, N.Mul32(s, 5f))), dy = N.Add32(popup.y, N.Mul32(k, N.Mul32(s, 15f)));
            for (int i = 0; i < popup.digitSize.Count; i++)
            {
                float w = N.Mul32(k, popup.digitSize[i].x), h = N.Mul32(k, popup.digitSize[i].y);
                Place(popup.digits[i], sx(dx), sy(dy), w, h);
                dx = N.Add32(dx, w);
            }
        }

        public bool PopupActive { get { return popup != null && popup.digitSize.Count > 0; } }   // DEBUG_ONLY (verifier)
        public SpriteRenderer PopupIcon { get { return popup != null ? popup.icon : null; } }
        public SpriteRenderer PopupMul { get { return popup != null ? popup.mul : null; } }
        public SpriteRenderer PopupDigit(int i) { return popup.digits[i]; }
        public int PopupDigitCount { get { return popup == null ? 0 : popup.digitSize.Count; } }
        public Vector2 PopupPosition { get { return new Vector2(popup.x, popup.y); } }
        public float PopupScale { get { return popup.scale; } }

        public int ItemCount { get { return items.Count; } }
        public ItemActor ItemAt(int i) { return items[i]; }
        public void DropItemForTest(float x) { DropItem(x); }   // DEBUG_ONLY
        public void CreateItemForTest(int kind, float x) { CreateItem(kind, x, N.Add32(N.Mul32(N.S, 20f), -110f)); }   // DEBUG_ONLY
    }
}
