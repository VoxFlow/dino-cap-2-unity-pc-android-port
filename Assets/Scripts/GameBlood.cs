using System.Collections.Generic;
using UnityEngine;

namespace Dc2Pilot
{
    /// <summary>Scene glue for the NATIVE blood / gore particle pool (NativeBlood.cs): frame order follows Scene::Update (PhysicsManager::Update first, then BeforeUpdate sync,
    /// the scene callbacks that spawn particles, then every enabled object's OnUpdate), rendering draws the pooled GameObjects in render layer 6 (CreateGameObject(.., 6, ..)).
    /// Evidence: Dev/Evidence/blood_decomp_r116.md.</summary>
    public partial class GameScene
    {
        BloodWorld bloodWorld;
        public BloodWorld BloodForTest { get { return bloodWorld; } }    // DEBUG_ONLY

        void BloodLoad() { bloodWorld = new BloodWorld(GroundY, Rand01, RandomRange); }

        /// <summary>Scene::Update: PhysicsManager::Update(dt) runs before any object callback of the frame.</summary>
        void BloodPhysics(float dt) { if (bloodWorld != null) bloodWorld.PhysicsStep(dt); }

        /// <summary>GameObject::Update of the bulletClass shell objects (id 3).</summary>
        void ShellObjects() { if (bloodWorld != null) bloodWorld.ShellObjectsUpdate(); }

        /// <summary>GameObject::Update of the pooled particleClass objects (after the scene callbacks) and their render.</summary>
        void BloodObjects()
        {
            if (bloodWorld == null) return;
            bloodWorld.ObjectUpdate();
            BloodDraw();
        }

        void BloodDraw()
        {
            float camCenterX = camLeft + WorldViewportW * 0.5f;
            for (int i = 0; i < bloodWorld.pool.Length; i++)
            {
                var p = bloodWorld.pool[i];
                bool draw = p.enabled && p.visible && p.frame != null && Atlas.Has(p.frame);
                if (!draw) { if (p.sprite != null) p.sprite.Hide(); continue; }
                if (p.sprite == null) p.sprite = NativeAffineSprite.Create(Background.LayerRoot(6));
                var r = Atlas.Get(p.frame).rect;
                var f = new NativeComponentAnimation.Frame
                {
                    scaleX = N.Div32(p.size.x, r.width), scaleY = N.Div32(p.size.y, r.height),
                    radians = p.flipX ? -p.rot : p.rot, rgba = 0xFFFFFFFF      // flipX only mirrors the texture (NativeAffineSprite negates the angle for its CA flip)
                };
                p.sprite.Draw(p.frame, f, new Vector2(N.ScreenW * 0.5f + (p.pos.x - camCenterX), -N.ScreenH * 0.5f + p.pos.y), p.order * 100 + i, 1f, p.flipX, null);
            }
            // bulletClass shell casings: CreateGameObject layer 3, sprite order 8 / 12
            for (int i = 0; i < bloodWorld.shells.Count; i++)
            {
                var p = bloodWorld.shells[i];
                bool draw = p.enabled && p.visible && p.frame != null && Atlas.Has(p.frame);
                if (!draw) { if (p.sprite != null) p.sprite.Hide(); continue; }
                if (p.sprite == null) p.sprite = NativeAffineSprite.Create(Background.LayerRoot(3));
                var r = Atlas.Get(p.frame).rect;
                var f = new NativeComponentAnimation.Frame { scaleX = N.Div32(p.size.x, r.width), scaleY = N.Div32(p.size.y, r.height), radians = p.rot, rgba = 0xFFFFFFFF };
                p.sprite.Draw(p.frame, f, new Vector2(N.ScreenW * 0.5f + (p.pos.x - camCenterX), -N.ScreenH * 0.5f + p.pos.y), p.order * 100 + i, 1f, false, null);
            }
        }

        /// <summary>Danke table of playerClass::KaiQiang 0xc0336 / playerNewAniClass::KaiQiang 0x12b8a0: frame, size (S units) and sprite order by weapon; false = no casing.</summary>
        static bool ShellTable(PlayerWeaponSlot slot, bool girlRig, out string frame, out Vector2 size, out int order)
        {
            float s = N.S; frame = "danke1Frame"; size = new Vector2(N.Mul32(s, 14f), N.Mul32(s, 6f)); order = 8;
            switch (slot.type)
            {
                case 0: return true;
                case 1: frame = "danke2Frame"; size = new Vector2(N.Mul32(s, 16f), N.Mul32(s, 7f)); order = 12; return true;
                case 2: if (slot.subtype <= 1) return true; if (slot.subtype <= 3) { order = 12; return true; } return false;
                case 3: order = 12; return true;
                case 5: return girlRig && slot.subtype == 5;
            }
            return false;
        }

        // ------------------------------------------------------------ dinosaur sprite anchors (CAObjCallBack::getSpritePos a4d0 on a dinosaur rig)
        static string DinoHeadPart(DinoActor d) { return d.skin == 5 ? "com6" : "tou"; }

        /// <summary>World position of a local point on a named child sprite of the dinosaur's CA rig; the input is returned unchanged when the child does not exist (native a4d0).</summary>
        Vector2 DinoPartWorld(DinoActor d, string part, Vector2 local)
        {
            NativeCaPlayer.PartInfo info;
            if (!d.rig.PartInfos.TryGetValue(part, out info)) return local;
            var child = new NativeSpriteAnchor.Child { name = part, enabled = true, position = new Vector2(N.Add32(d.x, info.localPos.x), N.Add32(d.y, info.localPos.y)), pivot = info.pivot, sin = info.sin, cos = info.cos };
            return NativeSpriteAnchor.GetPoint(new[] { child }, part, d.rig.flipX, local);
        }

        /// <summary>dinosaurClass::BlastBlood 0x12756.</summary>
        void DinoBlastBlood(DinoActor d) { if (bloodWorld != null) bloodWorld.BlastBlood(DinoPartWorld(d, DinoHeadPart(d), Vector2.zero)); }

        /// <summary>dinosaurClass::Blood(int) 0x12a46: a "qx" (a2 == 0) / "dx" (a2 != 0) CA blood effect on a random point of the head sprite, mounted on the dinosaur (effect layer 10).</summary>
        void DinoBlood(DinoActor d, int a2)
        {
            string part = DinoHeadPart(d);
            NativeCaPlayer.PartInfo info;
            if (!d.rig.PartInfos.TryGetValue(part, out info)) return;       // getSprite == null: nothing happens
            float hw = N.Mul32(0.5f, info.size.x), hh = N.Mul32(0.5f, info.size.y);
            float x0 = N.Mul32(hw, N.F(-1097229926));                        // -0.3
            float x = N.Add32(x0, N.Mul32(Rand01(), N.Add32(hw, -x0)));
            float y0 = N.Mul32(hh, N.F(-1085485875));                        // -0.8
            float y = N.Add32(y0, N.Mul32(Rand01(), N.Add32(N.Mul32(hh, 0.5f), -y0)));
            bool flip = x < 0f;
            if (a2 != 0 && d.skin == 3)
            {
                x = N.Mul32(hw, N.F(1061997773));                            // 0.8
                y = N.Mul32(hh, N.F(-1110651699));                           // -0.1
                flip = d.rig.flipX;                                          // dinosaur flag96
            }
            var pos = DinoPartWorld(d, part, new Vector2(x, y));
            var fx = SpawnCaFx("blood", BloodEffectAnimation(a2 != 0), pos.x, pos.y, flip, 10, 1f);
            SetFxDrawBase(fx, NativeDrawOrder.Base(d.drawIndex) + 20);   // dinosaurClass::Blood 0x12bd4: LDR +76; ADDS #0x14; STR
            fx.mount = d; fx.mountDelta = new Vector2(N.Add32(pos.x, -d.x), N.Add32(pos.y, -d.y));    // Scene::Mount(effect, dinosaur, follow, delta)
        }

        readonly Dictionary<bool, NativeCaAnimation> bloodEffects = new Dictionary<bool, NativeCaAnimation>();
        /// <summary>g_ca_dxEffData / g_ca_qxEffData: Image/a2b/dx.a2b and qx.a2b, loaded by loader case 27 followed by SetSpriteAnim("com1") (the "com1" part plays daoxue / qiangxue sprite frames).</summary>
        NativeCaAnimation BloodEffectAnimation(bool dx)
        {
            NativeCaAnimation a;
            if (!bloodEffects.TryGetValue(dx, out a)) { a = FxAnimation(dx ? "DC2/Image/a2b/dx.a2b" : "DC2/Image/a2b/qx.a2b", false); a.SetSpriteAnimation("com1"); bloodEffects[dx] = a; }
            return a;
        }

        // ------------------------------------------------------------ deaths (dinosaurClass::Die 0x140fc dispatch)
        /// <summary>0 DieNormal, 1 DieTouFei, 2 DieShaoSi, 3 BlastSuiRou + 3 BlastBlood (weapon type 4), 4 no handler.</summary>
        static int DieKindOf(WeaponDef w)
        {
            if (w == null) return 0;
            switch (w.type)
            {
                case 0: return 0;
                case 4: return 3;
                case 5: { if (w.index > 5) return 4; int m = 1 << w.index; return (m & 0x16) != 0 ? 2 : (m & 9) != 0 ? 1 : (m & 0x20) != 0 ? 0 : 4; }
                case 6: { if (w.index > 6) return 4; int m = 1 << w.index; return (m & 0x43) != 0 ? 0 : (m & 0x3C) != 0 ? 1 : 4; }
                default: return 0;
            }
        }

        void DinoDieNormalBlood(DinoActor d)
        {
            if (bloodWorld == null) return;
            bloodWorld.DieNormal(DinoPartWorld(d, DinoHeadPart(d), Vector2.zero), d.skin, d.surface);
        }

        /// <summary>DieTouFei 0x10cbc: head particle (once per dinosaur), then the headless "die2" animation.</summary>
        void DinoDieTouFei(DinoActor d)
        {
            if (!d.headSpawned && bloodWorld != null)
            {
                d.headSpawned = true;
                bloodWorld.DieTouFei(DinoPartWorld(d, DinoHeadPart(d), Vector2.zero), d.skin, d.surface, d.rig.flipX);
            }
            if (d.aDie2 == null) { d.aDie2 = DinoAnim(DinoFamily(d), "die2", d.surface, true, false, false); d.aDie2.MarkKeyFrame(17); }
            d.rig.SetAdvanced(null); d.advActive = false;
            d.rig.Play(d.aDie2, 0, 0, 0.025f);
        }

        /// <summary>weapon type 4 (explosion) death: BlastSuiRou + 3 BlastBlood, the dinosaur object is disabled and removed by its "Die" timer after 1.0 s.</summary>
        void DinoDieBlast(DinoActor d)
        {
            if (bloodWorld != null) bloodWorld.BlastSuiRou(DinoPartWorld(d, d.skin == 5 ? "com2" : "shen", Vector2.zero));
            DinoBlastBlood(d); DinoBlastBlood(d); DinoBlastBlood(d);
            d.rig.Hide(); d.disabled = true; SetTimer(d, "Die", 1f, () => DisappearDino(d));   // dinosaurClass GameObject::SetTimer("Die",1.0) -> OnTimeout -> Disappear 0x119f6
        }

        // ------------------------------------------------------------ heroes
        /// <summary>playerClass::Die 0xbcdea particle loop (position read before the die animation replaces the pose).</summary>
        Vector2 BloodHeroPoint()
        {
            Vector2 p, size; float rot;
            return HeroSpritePoint("yyj6", Vector2.zero, out p, out size, out rot) ? p : Vector2.zero;
        }
        void HeroDieBlood(Vector2 at) { if (bloodWorld != null) bloodWorld.HeroDie(at); }

        void GirlDieBlood(Vector2 at) { if (bloodWorld != null) bloodWorld.GirlDie(at); }
    }
}
