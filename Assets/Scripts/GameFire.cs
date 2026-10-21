using System.Collections.Generic;
using UnityEngine;

namespace Dc2Pilot
{
    /// <summary>Native hero fire lifecycle for heroes 0..3 (Girl keeps the legacy stand-in): playerClass::OnFireDown 0xb9990, OnKeyFrameAdv 0xc076a,
    /// OnAnimationEndAdv 0xbc222, KaiQiang 0xc0336, AfterNoBullets 0xbaeac, OnFireUp 0xb8bc6 (Dev/Evidence/fire_lifecycle_r112.md).
    /// The fire pose is the advanced channel (recipe from fire_recipes.json, r59/r66b) overlaying the locomotion base animation.
    /// Implemented weapon functions: KaiQiang (types 0..3). LiuDanQ (4) and the type-5 weapons (PenHuoQ/JiGuangQ/FeiBiao/YuanLi in GameBeam) are implemented; FireImplemented admits every shipped hero slot (Player.xml: type 5 subtypes 1..4 for heroes 0..3; JuPan sub 0 has no hero slot). r118v: the old "OPEN and stay gated" note was stale.</summary>
    public partial class GameScene
    {
        public bool heroInputEnabled = true;                 // playerClass+657 (cleared by GameClear / EggFull: UpdateCamera 0xbb24c, 0xbb430)
        readonly NativeShotAppearance shotApp = new NativeShotAppearance();
        readonly Dictionary<int, NativeCaAnimation> fireAnimCache = new Dictionary<int, NativeCaAnimation>();
        int fireAdvEnd = -1;                                 // resolved end frame of the last PlayAdvanced (CAObjCallBack+72)
        bool fireWasDown;                                    // touch edge for the OnFireDown dispatch
        /// <summary>SetTimer("fireup", 2.0) on the hero GameObject (7 sites, never killed): every arm fires on its own and clears the sprite suffix.</summary>
        void ArmFireup() { SetTimer(heroTimer, "fireup", 2f, FireupTimeout); }
        public int ShotsFired;                               // DEBUG_ONLY
        public string LastFireEvent = "";                    // DEBUG_ONLY
        public bool FireAdvActive { get { return Rig.Player.AdvancedAnimation != null; } }
        public int FireAdvEnd { get { return fireAdvEnd; } }
        public string ShotSuffix { get { return shotApp.Suffix; } }

        /// <summary>Weapon types whose native weapon function is recovered and ported; others are not started (no invented effect).</summary>
        bool FireImplemented(PlayerWeaponSlot slot) { return slot.type <= 4 || (slot.type == 5 && slot.subtype >= 1 && slot.subtype <= 4); }   // sub 0 (JuPan) has no hero slot; code kept in GameBeam

        NativeCaAnimation FireAnimation(int slot)
        {
            int key = heroType * 8 + slot;
            NativeCaAnimation a;
            if (!fireAnimCache.TryGetValue(key, out a)) { a = NativeHeroFireRecipes.Load(heroType, slot); fireAnimCache[key] = a; }
            return a;
        }

        void FirePlayAdv(int loops, int start, float interval, int end)
        {
            var anim = Rig.Player.AdvancedAnimation;
            fireAdvEnd = end < 0 ? (anim != null ? anim.frames.Length - 1 : -1) : end;
            Rig.Player.PlayAdvanced(loops, start, interval, end);
        }

        void FireBeginAdv(NativeCaAnimation anim, int start, float interval, int end)
        {
            Rig.Player.SetAdvanced(anim);
            Rig.Player.advancedKeyFrame = OnFireAdvKey;
            Rig.Player.advancedAnimationEnd = OnFireAdvEnd;
            FirePlayAdv(0, start, interval, end);
            firing = true;
        }

        /// <summary>Called every frame instead of the touch handler's direct OnFireDown call: dispatches on the press edge (or while idle with the button held).</summary>
        void NativeFireUpdate(float dt)
        {
            bool edge = fireDown && !fireWasDown; fireWasDown = fireDown;
            if (heroDead || HeroHurting || rolling || meleeing) return;
            if (fireDown && (edge || !firing)) NativeFireDown();
        }

        void FireupTimeout() { if (IsGirl) { GirlFireupTimeout(); return; } shotApp.FireupTimeout(); Rig.Player.FrameSuffix = shotApp.Suffix; }   // fireup handler bb566 / playerNewAniClass::OnTimeout 0x127dac

        /// <summary>playerClass::OnFireDown 0xb9990.</summary>
        void NativeFireDown()
        {
            if (heroControlState > 2 || !heroInputEnabled) return;
            var slots = Dc2GameData.PlayerWeapons(heroType);
            var slot = slots[weaponSlot];
            if (Profile.weaponAmmo[CurrentAmmoIndex] <= 0 && !Game.DebugInfiniteAmmo) { AfterNoBullets(slots.Count - 1); return; }   // DEBUG_ONLY infinite ammo also bypasses an empty weapon
            if (IsGirl) { if (GirlFireImplemented(slot)) GirlFireDown(slot); return; }
            if (!FireImplemented(slot)) return;
            var anim = FireAnimation(weaponSlot);
            switch (slot.type)
            {
                case 0: if (!firing) FireBeginAdv(anim, 0, 0.025f, -1); else FirePlayAdv(0, 1, 0.025f, -1); break;
                case 1: if (!firing) FireBeginAdv(anim, 0, 0.025f, -1); break;
                case 2: case 3: if (!firing) { FireBeginAdv(anim, 0, 0.025f, 6); shotApp.AcceptedBurstStart(slot.type); } break;
                case 4: if (!firing) FireBeginAdv(anim, 0, 0.05f, -1); break;
                case 5:
                    if (slot.subtype == 1 || slot.subtype == 2) { if (!firing) FireBeginAdv(anim, 0, 0.05f, 4); }
                    else if (slot.subtype == 3 || slot.subtype == 4) { if (!firing) BaseCastBegin(anim, slot.subtype); }
                    break;
            }
            LastFireEvent = "OnFireDown type " + slot.type;
        }

        /// <summary>playerClass::OnKeyFrameAdv 0xc076a: only outside the roll state.</summary>
        void OnFireAdvKey(int frame)
        {
            if (heroControlState == 7) return;
            var slot = Dc2GameData.PlayerWeapons(heroType)[weaponSlot];
            if (frame == 3) UseRound();
            switch (slot.type)
            {
                case 0: case 1: case 2: case 3: KaiQiang(slot); break;
                case 4: LiuDanQ(slot); break;
                case 5: if (slot.subtype == 1) PenHuoQ(); else if (slot.subtype == 2) JiGuangQ(); break;
            }
        }

        /// <summary>OnKeyFrameAdv 0xc076a frame 3 / base-channel key frames: ammo - 1; at 0 OnFireUp, held flag cleared; mainScene::UpdateBullet.</summary>
        public void DebugFireOneShot() { UseRound(); }   // DEBUG_ONLY verifier hook (debug menu infinite-ammo check)
        void UseRound()
        {
            int idx = CurrentAmmoIndex;
            int ammo = Profile.weaponAmmo[idx] - (Game.DebugInfiniteAmmo ? 0 : 1);   // DEBUG_ONLY infinite ammo (debug menu); native always subtracts 1
            if (ammo <= 0) { NativeOnFireUp(); ammo = 0; fireDown = false; }
            Profile.weaponAmmo[idx] = ammo;
            UpdateBullet();                                                   // mainScene::UpdateBullet
        }

        // ---- base-channel casts: FeiBiao (sub 3, hero state 9) and YuanLi (sub 4, state 10); OnFireDown 0xb9c50.. ----
        bool baseCasting;
        public bool BaseCasting { get { return baseCasting; } }

        void BaseCastBegin(NativeCaAnimation anim, int sub)
        {
            baseCasting = true; firing = true;
            heroControlState = sub == 3 ? 9 : 10; heroVelocityX = 0f;
            rigState = -1;
            Rig.Player.keyFrame = BaseCastKey;
            Rig.Player.animationEnd = BaseCastEnd;
            if (sub == 3) Rig.Player.Play(anim, 0, 0, 0.025f, -1);            // "playerFeibiaoAnimation" playAnimation(0,0,0.025,-1)
            else Rig.Player.Play(anim, 0, 0, 0.02f, 15);                       // "playerYuanliAnimation" playAnimation(0,0,0.02,end 15)
        }

        /// <summary>Key frame 9 (FeiBiao) / 11 (YuanLi): ammo - 1, then the weapon function.</summary>
        void BaseCastKey(int frame)
        {
            if (!baseCasting || heroControlState == 7) return;
            var slot = Dc2GameData.PlayerWeapons(heroType)[weaponSlot];
            if (slot.subtype == 3 && frame == 9) { UseRound(); ShotsFired++; FeiBiao(); }
            else if (slot.subtype == 4 && frame == 11) { UseRound(); ShotsFired++; YuanLi(); }
        }

        void BaseCastEnd()
        {
            if (!baseCasting) return;
            var slot = Dc2GameData.PlayerWeapons(heroType)[weaponSlot];
            if (Profile.weaponAmmo[CurrentAmmoIndex] <= 0 && !Game.DebugInfiniteAmmo) fireDown = false;
            if (slot.subtype == 4 && fireDown) { Rig.Player.Play(FireAnimation(weaponSlot), 0, 11, 0.05f, 15); return; }   // held: loop frames 11..15
            if (slot.subtype == 4 && yuanliBeam != null && yuanliBeam.enabled) yuanliBeam.fx.ca.Play(yuanliBeam.anim, 0, 6, 0.025f);   // released: playerClass::OnAnimationEnd 0xbde1e case 10 (hero frame 15): effect +176 playAnimation(0, 6, 0.025, -1). r119j2: SUPERSEDES r119j "current frame" (that rule is OnAnimationEndAdv's, for the flame/laser)
            baseCasting = false; firing = false; fireDown = false;
            Rig.Player.keyFrame = null; Rig.Player.animationEnd = null;
            heroControlState = 2; heroVelocityX = 0f; rigState = -1; SetRigState(0);   // Stand()
        }

        /// <summary>playerClass::KaiQiang 0xc0336: sound by type/subtype, burst counter suffix (types 2,3), then Attack(slot).
        /// Shell casing (Danke 0xbcbb8, GameBlood.cs) and muzzle flash (Qianghuo 0xb8c90).</summary>
        void KaiQiang(PlayerWeaponSlot slot)
        {
            switch (slot.type)
            {
                case 0: Audio.PlaySound("Attack_AAudio"); break;
                case 1: Audio.PlaySound("Attack_BAudio"); break;
                case 2: Audio.PlaySound(slot.subtype <= 1 ? "Attack_DAudio" : "Attack_E1Audio"); break;   // subtypes 2,3 -> E1; native default arm leaves the sound out of range, none exist
                case 3: Audio.PlaySound("Attack_CAudio"); break;
            }
            if (slot.type == 2 || slot.type == 3) { shotApp.AfterKaiQiang(slot.type); Rig.Player.FrameSuffix = shotApp.Suffix; }
            ShotsFired++;
            HeroAttack();
            // KaiQiang gun sprite by weapon: type 0 and type 2 subtypes 0,1 -> "com1"; type 1, type 2 subtypes 2,3 and type 3 -> "yyj5" (0xc0336 switch; type 2 sub 0/1 was wrongly "yyj5" before r116)
            var gun = (slot.type == 0 || (slot.type == 2 && slot.subtype <= 1)) ? "com1" : "yyj5";
            Vector2 p; Vector2 size; float rot;
            if (HeroSpritePoint(gun, Vector2.zero, out p, out size, out rot))
            {
                Vector2 dp, dsz; float drot;                                                   // getSpritePos(name, (0, 0.2 * sprite height)) -> Danke 0xbcbb8
                string frame; Vector2 fsize; int order;
                if (ShellTable(slot, false, out frame, out fsize, out order) && HeroSpritePoint(gun, new Vector2(0f, N.Mul32(0.2f, size.y)), out dp, out dsz, out drot) && bloodWorld != null)
                    bloodWorld.Danke(dp, fsize, frame, order, facingLeft);
                Qianghuo(slot, p);                                                             // KaiQiang tail 0xc064a: getSpritePos(name,(0,0)) -> Qianghuo
            }
        }

        /// <summary>CAObjCallBack::getSprite + getSpritePos a4d0 for a hero rig part: world point of a local offset on that child sprite.</summary>
        bool HeroSpritePoint(string part, Vector2 local, out Vector2 world, out Vector2 size, out float rotation)
        {
            world = Vector2.zero; size = Vector2.zero; rotation = 0f;
            NativeCaPlayer.PartInfo info;
            if (!Rig.Player.PartInfos.TryGetValue(part, out info)) return false;
            float heroY = Rig.Player.position.y;
            var child = new NativeSpriteAnchor.Child { name = part, enabled = true, position = new Vector2(heroX + info.localPos.x, heroY + info.localPos.y), pivot = info.pivot, sin = info.sin, cos = info.cos };
            world = NativeSpriteAnchor.GetPoint(new[] { child }, part, facingLeft, local);
            size = info.size; rotation = Mathf.Atan2(info.sin, info.cos);
            return true;
        }

        // Qianghuo 0xb8c90 offsets: (A, B) per [type][moving][subtype]: x offset = (int)((0.6 + S) * A) (iPad), y += S * B; default subtype/type: none.
        static readonly float[][][][] qhTable =
        {
            new[] { new[] { new[]{10f,10f}, new[]{20f,15f}, new[]{15f,10f}, new[]{20f,12f} }, new[] { new[]{20f,5f}, new[]{30f,10f}, new[]{25f,5f}, new[]{30f,7f} } },
            new[] { new[] { new[]{40f,12f}, new[]{40f,12f}, new[]{40f,12f}, new[]{30f,12f} }, new[] { new[]{50f,5f}, new[]{50f,5f}, new[]{50f,5f}, new[]{40f,5f} } },
            new[] { new[] { new[]{20f,20f}, new[]{20f,25f}, new[]{20f,10f}, new[]{20f,5f} }, new[] { new[]{30f,12f}, new[]{30f,15f}, new[]{30f,10f}, new[]{30f,5f} } },
            new[] { new[] { new[]{40f,8f}, new[]{30f,10f}, new[]{30f,8f}, new[]{35f,5f} }, new[] { new[]{50f,5f}, new[]{40f,8f}, new[]{40f,5f}, new[]{45f,0f} } },
        };

        /// <summary>playerClass::Qianghuo 0xb8c90: muzzle flash effect (an2/qh2 "qhEffectAnimation", effect layer 9) at the gun point + per weapon offset; type 0 uses objectScale 0.6.</summary>
        void Qianghuo(PlayerWeaponSlot slot, Vector2 pos)
        {
            float off = 0f, dy = 0f;
            if (slot.type >= 0 && slot.type <= 3 && slot.subtype >= 0 && slot.subtype <= 3)
            {
                bool moving = leftDown || rightDown;
                var row = qhTable[slot.type][moving ? 1 : 0][slot.subtype];
                off = (int)N.Mul32(N.Add32(0.6f, N.S), row[0]);
                dy = N.Mul32(N.S, row[1]);
            }
            float x = facingLeft ? N.Add32(pos.x, -off) : N.Add32(pos.x, off);
            var qh = SpawnCaFx("qh", FxAnimation("DC2/Image/an2/qh2.a2b", true), x, N.Add32(pos.y, dy), facingLeft, 9, slot.type == 0 ? 0.6f : 1f);
            // 0xb9212..0xb9230: SetRotation(heroRotation +/- 0.1) (0x3DCCCCCD), + when the effect flip flag (= hero +96) is set; KaiQiang passes the hero GameObject rotation (0xc0650), which is 0.
            SetFxRotation(qh, facingLeft ? N.Add32(0f, N.F(1036831949)) : N.Add32(0f, -N.F(1036831949)));
        }

        /// <summary>playerClass::LiuDanQ 0xbfe94 (type 4, instant area blast): muzzle flash, baozha explosion at hero.x -/+ 150*S, then BeAttacked for every dinosaur/boss within 200*S of the blast; Attack_E2Audio.</summary>
        void LiuDanQ(PlayerWeaponSlot slot)
        {
            float s = N.S;
            var gun = slot.subtype == 0 ? "yyj5" : slot.subtype == 1 ? "com1" : null;
            if (gun != null)
            {
                Vector2 p, size; float rot;
                // input point (0.5w, 0.05h) on the gun sprite (LiuDanQ 0xbff74..0xbff96), then Qianghuo
                if (Rig.Player.PartInfos.TryGetValue(gun, out var info) && HeroSpritePoint(gun, new Vector2(N.Mul32(0.5f, info.size.x), N.Mul32(N.Mul32(0.5f, info.size.y), 0.1f)), out p, out size, out rot)) Qianghuo(slot, p);
            }
            float bx = N.Add32(heroX, N.Mul32(s, facingLeft ? -150f : 150f));
            float by = Rig.Player.position.y;                                   // iPad: y offset 0
            SpawnCaFx("baozha", FxAnimation("DC2/Image/a2b/baozha.a2b", false), bx, by, facingLeft, 12, 1f);
            var w = CurrentWeapon();
            float radius = N.Mul32(s, 200f);
            bool bossHit = false;
            var victims = new List<DinoActor>();
            foreach (var d in dinos)
            {
                if (d.dead || !d.targetable || d.state == 3) continue;
                if (Mathf.Abs(N.Add32(d.x, -bx)) < radius) victims.Add(d);
            }
            if (Boss != null && Boss.kind != 4 && Boss.sub != 0 && Mathf.Abs(N.Add32(Boss.x, -bx)) < radius) bossHit = true;   // bossN +456 != 4 && +460 != 0
            foreach (var d in victims) BeAttacked(d, w, Mathf.Abs(heroX - d.x));
            if (bossHit) BossBeAttacked(w);
            Audio.PlaySound("Attack_E2Audio");
            ShotsFired++;
        }

        /// <summary>playerClass::OnAnimationEndAdv 0xbc222.</summary>
        void OnFireAdvEnd()
        {
            var slot = Dc2GameData.PlayerWeapons(heroType)[weaponSlot];
            if (Profile.weaponAmmo[CurrentAmmoIndex] <= 0 && !Game.DebugInfiniteAmmo) fireDown = false;
            switch (slot.type)
            {
                case 2: case 3:
                    if (fireDown) FirePlayAdv(0, 3, 0.025f, 6);
                    else if (fireAdvEnd != 6) { Rig.Player.SetAdvanced(null); firing = false; fireDown = false; ArmFireup(); }
                    else FirePlayAdv(0, 6, 0.025f, -1);
                    break;
                case 5:
                    if (slot.subtype == 1 || slot.subtype == 2) BeamAdvEnd(slot.subtype);
                    else { Rig.Player.SetAdvanced(null); firing = false; fireDown = false; }
                    break;
                default:   // types 0,1,4: single cycle then clear
                    Rig.Player.SetAdvanced(null); firing = false; fireDown = false;
                    break;
            }
        }

        /// <summary>playerClass::OnFireUp 0xb8bc6: stops the flame/laser effect objects (+0x2bc/+0x2c0/+0x2c4); none exist for the ported types.</summary>
        void NativeOnFireUp() { StopBeams(); }

        /// <summary>r119f (user item 8, rage face): playerClass::BeAttack 0xbd196..0xbd1ac, OnHitDown 0xbd8xx and OnRollDown 0xbd71c..0xbd73a: only while +656 (firing) is set,
        /// OnFireUp, +656 = 0 and the CAObj frame suffix +108 ("_nu1".."_nu3", the rage face) is emptied. The KaiQiang counter and a pending "fireup" timer are untouched, so a
        /// hit taken after the burst ended keeps the face until that timer. The port never cleared the suffix on these interrupts.</summary>
        void ClearRageIfFiring() { if (!firing || heroType == 4) return; shotApp.Suffix = ""; if (Rig != null && Rig.Player != null) Rig.Player.FrameSuffix = ""; }

        /// <summary>Interrupts (hurt, roll, melee, death, weapon switch, game clear): OnFireUp, advanced channel cleared, firing flag dropped.</summary>
        void ClearNativeFireChannel()
        {
            if (heroType == 4) { firing = false; return; }   // playerNewAniClass has no OnFireUp; BeAttack/OnHitDown/OnRollDown only clear +357 (the advanced channel keeps running)
            NativeOnFireUp();
            if (baseCasting) { baseCasting = false; if (Rig != null && Rig.Player != null) { Rig.Player.keyFrame = null; Rig.Player.animationEnd = null; } }
            if (Rig != null && Rig.Player != null && Rig.Player.AdvancedAnimation != null) Rig.Player.SetAdvanced(null);
            firing = false;
        }

        /// <summary>playerClass::AfterNoBullets 0xbaeac: from the highest gun slot below a2, switch to the first owned slot with ammo.</summary>
        void AfterNoBullets(int a2)
        {
            int v = a2 - 1;
            if (v < 0) return;
            if (Profile.weaponOwned[heroType * 6 + v] >= 0 && Profile.weaponAmmo[heroType * 6 + v] > 0) SelectWeaponSlot(v);
            else AfterNoBullets(v);
        }

        public void AfterNoBulletsForTest(int a2) { AfterNoBullets(a2); }   // DEBUG_ONLY
    }
}
