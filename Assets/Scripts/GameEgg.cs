using UnityEngine;

namespace Dc2Pilot
{
    /// <summary>mainScene egg mission (type 5). Evidence: Dev/Evidence/mainScene_decomp_r107.md and egg_decomp_r108.md.
    /// initEgg 0x95dc0, eggClass::OnUpdate 0x11feb6, playerClass::UpdateCamera egg branch 0xbb316, ShowEggCount 0x94e8e,
    /// AddEggOnCar 0x9760a, EggFull 0x974fa, BrokenEgg 0x9725a, effectClassN::OnAnimationEnd 0x11fa80.
    /// Hero body swap while carrying (getEgg 0xb937c / getDownEgg 0xb96f8) is implemented in EggBodyEnsure/EggBodyApply (r118v: old OPEN note stale).</summary>
    public partial class GameScene
    {
        public int EggRemaining, EggDelivered;           // scene+272, +276
        public bool EggCarrying;                         // scene+269
        public bool EggPresent;                          // an "eggClass" object exists (initEgg) and is waiting to be picked up
        public float EggX;                               // initEgg: right + S*-50
        public bool EggFullStarted;                      // EggFull executed (truck cheMoveAnimation playing)
        public int EggBrokenCount;                       // DEBUG_ONLY counter
        bool eggAssetsLoaded;
        GameObject eggWorldRoot;
        SpriteRenderer nestSprite;
        NativeComponentPlayer truck, brokenFx;
        GameObject truckRoot, brokenRoot;
        NativeComponentAnimation truckStandAcb, truckMoveAcb, eggSuiAcb;

        /// <summary>mainScene::OnLoad iPad branch 0xa7cd4: eggs2x conf atlas (HUD icon, truck pieces) and eggbody_h frames.</summary>
        void EggLoadAssets()
        {
            if (eggAssetsLoaded) return;
            eggAssetsLoaded = true;
            Atlas.LoadByConf("DC2/Image/ip/eggs2x", "DC2/Image/ip/eggs2x.conf");
            var body = Atlas.LoadTexture("DC2/Image/ip/eggbody_h");
            // registrations recovered from the OnLoad trace (Dev/Recovered/mainScene/onload_egg_t5.json, pc 0xa78b0..0xa7b2a)
            if (!Atlas.Has("egg_woFrame")) Atlas.CreateFrame("egg_woFrame", body, 0, 0, 247, 157);
            if (!Atlas.Has("shenti0001_eggFrame")) Atlas.CreateFrame("shenti0001_eggFrame", body, 247, 0, 176, 156);
            if (!Atlas.Has("shenti0003_eggFrame")) Atlas.CreateFrame("shenti0003_eggFrame", body, 176, 157, 176, 156);
            if (!Atlas.Has("shenti0004_eggFrame")) Atlas.CreateFrame("shenti0004_eggFrame", body, 0, 157, 176, 156);
            if (!Atlas.Has("shenti0002_eggFrame")) Atlas.CreateFrame("shenti0002_eggFrame", body, 0, 313, 176, 156);
        }

        /// <summary>mainScene::OnLoad egg branch (truck effectClassN 0xaaa76, nest object 0xac1b8, initEgg, ShowEggCount).</summary>
        void EggLoad()
        {
            if (Game.LaunchMission == null || Game.LaunchMission.type != 5) return;
            EggLoadAssets();
            float s = N.S;
            EggRemaining = Game.LaunchMission.info; EggDelivered = 0; EggCarrying = false; EggFullStarted = false;
            eggWorldRoot = new GameObject("egg_world"); eggWorldRoot.transform.SetParent(Background.LayerRoot(0), false);
            // nest: GameObject (egg_woFrame, sprite layer 12) size (248, S*79) at (right + S*-50, 5)
            float nestX = N.Add32(N.Mul32(s, -50f), BoundUpper);
            var nestGo = new GameObject("nest"); nestGo.transform.SetParent(Background.LayerRoot(12), false);
            nestSprite = nestGo.AddComponent<SpriteRenderer>(); nestSprite.sprite = Atlas.Get("egg_woFrame"); nestSprite.sortingOrder = 100;   // PLATFORM_ADAPTER: see CreateItem
            nestSize = new Vector2(248f, N.Mul32(s, 79f)); nestPos = new Vector2(nestX, EggObjectY());
            // truck: effectClassN size (S*157, S*119) at (S*-160, 5), "cheStandAnimation" played (PlayAnimation loops -1), effectClassN kind 2
            truckStandAcb = NativeComponentAnimation.Load("DC2/Image/acb/ipad/eggs_che1.animation.acb");   // g_ca_egg_che_stand (LIKELY: symbol name + 1162 B)
            truckStandAcb.frameRateOverride = 0.5f;   // r119e: mainScene::OnLoad 0xaac48 g_ca_egg_che_stand+0x58 = 0.5 (0x3F000000) before PlayAnimation(-1, 30): half-rate idle
            truckMoveAcb = NativeComponentAnimation.Load("DC2/Image/acb/ipad/eggs_che2.animation.acb");    // g_ca_egg_che_move
            eggSuiAcb = NativeComponentAnimation.Load("DC2/Image/acb/ipad/eggs_sui.animation.acb");        // g_ca_egg_sui
            // r119h NATIVE (user decision "follow the native code"): mainScene::OnLoad 0xac15c..0xac160 sets the truck's CompAnimObj+12 (its CustomDraw layer,
            // registered by CompAnimObj::OnUpdate 0x1190fc) to 12; RenderManager::DoRender 0x172d90 draws each layer's sprites, then its custom draws, layers 0..15
            // ascending. So the truck covers the ground and props (layers 1..3), the hero and dinosaurs (layer 10) and the nest (layer-12 sprite). Supersedes the
            // r119g observed order (between props and actors) and the original port parent (layer 0).
            truckRoot = new GameObject("truck"); truckRoot.transform.SetParent(Background.LayerRoot(12), false); truckRoot.AddComponent<UnityEngine.Rendering.SortingGroup>().sortingOrder = NativeDrawOrder.CustomDraw;   // after the layer-12 sprites
            truckPos = new Vector2(N.Add32(N.Mul32(s, 80f), BoundLower), EggObjectY());   // 0xaab8e..0xaabf6: (S*80 + scene+136, ...) = (-320, -50) on iPad
            truck = new NativeComponentPlayer(truckStandAcb, truckRoot.transform, TruckScreenPos(), 1f);
            truck.Play(-1);
            for (int i = 1; i <= 6; i++) truck.SetChangeSpriteColor("e" + i, new Color32(1, 1, 1, 0));   // OnLoad: six SetChangeSpriteColor, alpha 0
            InitEgg();
            ShowEggCount();
            PlaceEggWorld();
        }

        Vector2 nestSize, nestPos, truckPos;
        /// <summary>r119e (user item 3, nest and truck too high): mainScene::OnLoad 0xaab44..0xaabc0: y = S*10 + (S*20 + g_ground_y) (literals 0x41A00000 / 0x41200000,
        /// g_ground_y 0x23b910); the nest (0xac3a4) reuses the same stack value. iPad g_ground_y = -110 gives -50. FALSIFIED: the port's y = 5 came from the static
        /// iPhone g_ground_y -55 (the B2 scenery error).</summary>
        static float EggObjectY() { float s = N.S; return N.Add32(N.Mul32(s, 10f), N.Add32(N.Mul32(s, 20f), GroundY)); }
        /// <summary>NATIVE screen position of the truck: world x minus the camera centre. The truck art (eggs_truck_1) is the rear half of the pickup, cut so its end lies
        /// on the iPad screen's left edge at the native position (S*80 + scene+136, body quad -6..308). In wide view the egg camera clamp (AD-005, GameScene.UpdateNativeCamera)
        /// puts that native bound on the visible edge. History: r119g drew the truck WideCombatExtra further left (ADAPTATION), SUPERSEDED in r119r (R002-N5 docstring fix).</summary>
        Vector2 TruckScreenPos() { return new Vector2(truckPos.x - (camLeft + WorldViewportW * 0.5f), truckPos.y); }

        /// <summary>mainScene::initEgg 0x95dc0: invisible "eggClass" trigger object at x = right + S*-50.</summary>
        void InitEgg() { EggPresent = true; EggX = N.Add32(N.Mul32(N.S, -50f), BoundUpper); }

        /// <summary>mainScene::ShowEggCount 0x94e8e: "%d/%d" = delivered / (delivered + remaining).</summary>
        void ShowEggCount() { if (eggLabel != null) eggLabel.SetText(EggDelivered + "/" + (EggDelivered + EggRemaining)); }

        void PlaceEggWorld()
        {
            if (nestSprite != null)
            {
                float cx = N.ScreenW * 0.5f + (nestPos.x - (camLeft + WorldViewportW * 0.5f)), cy = -N.ScreenH * 0.5f + nestPos.y;
                Place(nestSprite, cx, cy, nestSize.x, nestSize.y);
            }
            if (truck != null) truck.position = TruckScreenPos();
            if (brokenFx != null) brokenFx.position = brokenPos();
        }
        Vector2 brokenWorld;
        Vector2 brokenPos() { return new Vector2(brokenWorld.x - (camLeft + WorldViewportW * 0.5f), brokenWorld.y); }

        /// <summary>mainScene::UnRollDown 0x94c6c: roll button masked (id 39, black alpha 176) while carrying. r118z4: the native body (0x94d00..0x94d36) clears the
        /// drain flag +268, SetVisible(1) and resets the clip of the same control to the full (830,537,88,88) rect, so a running roll-fill drain stops at full.</summary>
        void UnRollDown()
        {
            var mask = GetControl(39);   // the OnLoad roll-fill image (+312, GameScene.BuildHud)
            if (mask == null) return;
            rollFillOn = false; mask.SetVisible(true); mask.SetClip(RollFillRect);
        }
        /// <summary>mainScene::CanRollDown 0x94c2a (called by getDownEgg): mask hidden.</summary>
        void CanRollDown() { var m = GetControl(39); if (m != null) m.SetVisible(false); }   // 0x94c2a: +312 vtable+36 SetVisible(0) (r118z4: the port deleted it)

        /// <summary>eggClass::OnUpdate 0x11feb6 (pickup) + playerClass::UpdateCamera mode 4 delivery 0xbb3c8.</summary>
        void EggUpdate(float dt)
        {
            if (Game.LaunchMission.type != 5) return;
            if (EggPresent && !EggCarrying && Mathf.Abs(N.Add32(EggX, -heroX)) < 60f)
            {
                EggPresent = false;           // GameObject::Delete
                EggCarrying = true;           // scene+269 = 1
                UnRollDown();
                if (IsGirl) GirlGetEgg();      // playerNewAniClass::getEgg 0x126e2e body avatar
                Audio.PlaySound("A_get_egg"); // playerClass::getEgg 0xb937c tail (hero 0..3 body swap: EggBodyEnsure)
            }
            if (heroCameraThisTick && EggCarrying && heroX < N.Add32(N.Mul32(N.S, 150f), BoundLower)) DeliverEgg();   // native line (r119r: the r119k wide shift is superseded by the egg wide camera)   // UpdateCamera part (HeroUpdatesCamera)
            if (truck != null)
            {
                truck.position = TruckScreenPos();
                truck.StepNative(dt, () => { if (EggFullStarted) { Complete(); truck.Hide(); } });
            }
            if (brokenFx != null)
            {
                brokenFx.position = brokenPos();
                brokenFx.StepNative(dt, () => { brokenFx.Destroy(); if (brokenRoot != null) Object.Destroy(brokenRoot); brokenFx = null; brokenRoot = null; });
            }
            PlaceEggWorld();
        }

        void DeliverEgg()
        {
            EggCarrying = false;
            EggRemaining--; EggDelivered++;
            ShowEggCount();
            if (EggRemaining > 0)
            {
                GetDownEgg(); InitEgg(); AddEggOnCar(EggDelivered);
                return;
            }
            StopCreateEnemy();
            GetDownEgg(); InitEgg(); AddEggOnCar(EggDelivered);
            EggFull();
            heroVelocityX = 0f;               // SetLinearVelocity(0,0)
            heroInputEnabled = false;         // 0xbb430: +657 = 0 (before StopCreateEnemy)
            if (firing || fireDown) { fireDown = false; ClearNativeFireChannel(); firing = false; }   // OnFireUp + g_b_FireDown = 0
            SetRigState(0, true);             // playerClass::Stand
        }

        /// <summary>playerClass::getDownEgg 0xb96f8: CanRollDown, original body images restored (ChangeImage(part, shenti000N, 0.5) undoes the x2 of getEgg).</summary>
        void GetDownEgg() { CanRollDown(); if (IsGirl) GirlGetDownEgg(); else EggBodyEnsure(); }

        /// <summary>playerClass::getEgg 0xb937c: ChangeImage(yyj4 / yuanjian4, shenti000N_egg, 2.0) on the hero's pose animations (the stored members are modified in place;
        /// the port clones per pose selection, so the swap is applied to the live animation and re-applied after each selection while carrying).
        /// Hero 1/3 also swap com2/com4 on their roll animations, unreachable while carrying (roll masked). Parts absent from an animation are skipped (native frame-map find).</summary>
        void EggBodyEnsure()
        {
            if (IsGirl || Rig == null || Rig.Player == null) return;
            EggBodyApply(Rig.Player.animation);
            EggBodyApply(Rig.Player.AdvancedAnimation);
        }

        void EggBodyApply(NativeCaAnimation a)
        {
            if (a == null) return;
            string n = (heroType + 1).ToString("0000");
            if (EggCarrying && !a.eggBodySwapped)
            {
                a.ChangeImage("yyj4", "shenti" + n + "_egg", 2f); a.ChangeImage("yuanjian4", "shenti" + n + "_egg", 2f);
                a.eggBodySwapped = true;
            }
            else if (!EggCarrying && a.eggBodySwapped)
            {
                a.ChangeImage("yyj4", "shenti" + n, 0.5f); a.ChangeImage("yuanjian4", "shenti" + n, 0.5f);
                a.eggBodySwapped = false;
            }
        }

        /// <summary>mainScene::AddEggOnCar 0x9760a: e1..e<n> white, e(n+1)..e6 alpha 0, then A_get_down_egg.</summary>
        void AddEggOnCar(int n)
        {
            for (int i = 1; i <= 6; i++) truck.SetChangeSpriteColor("e" + i, i <= n ? new Color32(255, 255, 255, 255) : new Color32(255, 255, 255, 0));
            Audio.PlaySound("A_get_down_egg");
        }

        /// <summary>mainScene::EggFull 0x974fa: truck plays cheMoveAnimation once; its end (effectClassN kind 2) is GameClear.</summary>
        void EggFull()
        {
            EggFullStarted = true;
            truck.SetAnimation(truckMoveAcb);
            truck.Play(1);
            for (int i = 1; i <= 6; i++) truck.SetChangeSpriteColor("e" + i, i <= EggDelivered ? new Color32(255, 255, 255, 255) : new Color32(255, 255, 255, 0));
            Audio.PlaySound("A_car");
        }

        /// <summary>playerClass::BeAttack/BeAttackByBoss 0xbd23a/0xbd25c/0xbd4ca: a hit while carrying drops the egg: flag cleared, getDownEgg, initEgg, BrokenEgg.</summary>
        void EggHeroHit()
        {
            if (!EggCarrying) return;
            EggCarrying = false;
            GetDownEgg(); InitEgg(); BrokenEgg();
        }

        /// <summary>mainScene::BrokenEgg 0x9725a: effectClassN layer 3 at (hero.x, g_ground_y + 2S) playing the egg_sui animation once; A_egg_broken.</summary>
        void BrokenEgg()
        {
            EggBrokenCount++;
            if (brokenFx != null) { brokenFx.Destroy(); if (brokenRoot != null) Object.Destroy(brokenRoot); }
            brokenRoot = new GameObject("eggBroken"); brokenRoot.transform.SetParent(Background.LayerRoot(3), false);
            brokenWorld = new Vector2(heroX, N.Add32(GroundY, N.Mul32(2f, N.S)));
            brokenFx = new NativeComponentPlayer(eggSuiAcb, brokenRoot.transform, brokenPos(), 1f);
            brokenFx.flipX = facingLeft;                  // CompAnimObj+223 = hero facing flag (+0x60)
            brokenFx.Play(1);
            Audio.PlaySound("A_egg_broken");
        }

        public int TruckColorAlpha(string name) { Color32 c; return truck != null && truck.colorOverrides.TryGetValue(name, out c) ? c.a : -1; }   // DEBUG_ONLY
        public bool TruckPlaysMove { get { return truck != null && truck.animation == truckMoveAcb; } }                                         // DEBUG_ONLY
        public bool NestVisible { get { return nestSprite != null && nestSprite.enabled && nestSprite.sprite != null; } }                         // DEBUG_ONLY
        public string EggBodyRefForTest(out float w)
        {
            w = 0f;
            var a = Rig.Player.animation; if (a == null) return "";
            foreach (var part in a.frames[0].parts) if (part.name == "yyj4") { w = part.w; return part.reference; }
            return "";
        }
        public void EggHitForTest() { EggHeroHit(); }                                        // DEBUG_ONLY
        public void EggPlaceHeroAtEggForTest() { heroX = EggX; }                              // DEBUG_ONLY
    }
}
