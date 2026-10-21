using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dc2Pilot
{
    /// <summary>One pooled particleClass object: its GameObject (position, rotation, size, sprite frame, enable/visible flags), its Triniti2D GamePhysics body
    /// (a b2Body with mass 1, inertia 3, no damping, no shapes) and the particleClass state (active +12, kind +16, style +20, sub kind +24, surface code +28, base y +8).</summary>
    public sealed class BloodParticle
    {
        // GameObject
        public bool enabled, visible, flipX;
        public Vector2 pos, size;
        public float rot;
        public string frame;
        public int order;                       // GameSprite +20
        // b2Body (physics scale factor 1.0 in every scene: pixels == metres)
        public Vector2 bodyPos, vel, force;
        public float bodyAngle, angVel;
        // particleClass
        public bool active;
        public int kind = 1, style, sub, surface;
        public float baseY;
        public NativeAffineSprite sprite;       // render vessel (Unity)
    }

    /// <summary>NATIVE blood / gore particle system. Evidence: Dev/Evidence/blood_decomp_r116.md (ARM-executed oracle Dev/Evidence/blood_emu_r116.json).
    /// particleClass::OnUpdate 0x7dbc, mainScene::GetParticle 0x94c3e (40 pooled objects created by mainScene/lifeModeScene::OnLoad, round robin),
    /// dinosaurClass::BlastBlood 0x12756 / BlastSuiRou 0x1226c / DieTouFei 0x10cbc / DieNormal 0x12ca8, bossN::BlastBlood 0x122e38,
    /// playerClass::Die 0xbcdea and playerNewAniClass::Die 0x126a36 particle loops. Physics: PhysicsManager::CreateWorld gravity (0,0), b2World::Step(dt, 1 iteration)
    /// with b2Island::Solve semi-implicit Euler; GamePhysics::ApplyForce stores (not accumulates) the force, so a force persists until it is reset.</summary>
    public sealed class BloodWorld
    {
        public const int PoolSize = 40;
        public readonly BloodParticle[] pool = new BloodParticle[PoolSize];
        public int NextIndex;                   // mainScene+108 (GetParticle round-robin cursor)
        readonly float groundY;
        readonly Func<float> unit;              // rand() * 2^-31 (as a float)
        readonly Func<int, int, int> range;     // Triniti2D::RandomRange(lo, hi)
        public int Spawned;                     // DEBUG_ONLY
        public readonly List<BloodParticle> shells = new List<BloodParticle>();   // bulletClass objects (mainScene::GetBullet 0x9877c: free list at scene +48, created on demand)
        readonly List<BloodParticle> freeShells = new List<BloodParticle>();
        public int ShellsSpawned;               // DEBUG_ONLY

        public BloodWorld(float groundY, Func<float> unit, Func<int, int, int> range)
        {
            this.groundY = groundY; this.unit = unit; this.range = range;
            for (int i = 0; i < PoolSize; i++) pool[i] = new BloodParticle { baseY = groundY };   // particleClass ctor: +8 = g_ground_y, active 0, kind 1
        }

        static float S { get { return N.S; } }
        static float F32(float v) { return v; }

        // ------------------------------------------------------------ GameObject / GamePhysics vessels
        BloodParticle Get() { var p = pool[NextIndex]; NextIndex++; if (NextIndex >= PoolSize) NextIndex = 0; Spawned++; return p; }   // mainScene::GetParticle
        static void SetPosition(BloodParticle p, Vector2 v) { p.pos = v; p.bodyPos = v; p.bodyAngle = p.rot; }  // GameObject::SetPosition -> GamePhysics::SetTransform(pos, rot)
        static void SetRotation(BloodParticle p, float r) { p.rot = r; p.bodyPos = p.pos; p.bodyAngle = r; }    // GameObject::SetRotation -> SetTransform(pos, r)
        static void Begin(BloodParticle p, Vector2 pos, int order = 11)
        {
            p.enabled = true; p.visible = true;                      // Enable(true); Sprite()+92 = 1
            SetPosition(p, pos);
        }

        // ------------------------------------------------------------ Box2D-lite: b2World::Step -> b2Island::Solve for a free mass-1 body
        /// <summary>PhysicsManager::Update inside Scene::Update, before every object's BeforeUpdate/Update. dt is the (time-scaled) frame time.</summary>
        public void PhysicsStep(float dt)
        {
            if (!(dt > 0f)) return;
            Integrate(pool, dt);
            Integrate(shells, dt);
            // GameObject::BeforeUpdate (enabled objects only): position / rotation := body
            foreach (var p in pool) if (p.enabled) { p.pos = p.bodyPos; p.rot = p.bodyAngle; }
            foreach (var p in shells) if (p.enabled) { p.pos = p.bodyPos; p.rot = p.bodyAngle; }
        }

        static void Integrate(IList<BloodParticle> bodies, float dt)
        {
            foreach (var p in bodies)
            {
                // v += dt * (invMass * F + gravity), gravity = (0,0), invMass = 1
                float dy = N.Mul32(dt, N.Add32(N.Mul32(1f, p.force.y), 0f));
                float dx = N.Mul32(dt, N.Add32(N.Mul32(1f, p.force.x), 0f));
                p.vel.x = N.Add32(p.vel.x, dx); p.vel.y = N.Add32(p.vel.y, dy);
                // damping factor 1 - dt*0 = 1 (no change); velocity clamp b2_maxLinearVelocity 2000 (|v|^2 > 4,000,000)
                float sq = N.Add32(N.Mul32(p.vel.x, p.vel.x), N.Mul32(p.vel.y, p.vel.y));
                if (sq > 4000000f)
                {
                    float len = N.F32(Math.Sqrt((double)sq));
                    if (len >= 1.1920929e-7f) { float inv = N.Div32(1f, len); p.vel.x = N.Mul32(p.vel.x, inv); p.vel.y = N.Mul32(p.vel.y, inv); }
                    else { p.vel.x = 0f; p.vel.y = 0f; }
                    p.vel.x = N.Mul32(p.vel.x, 2000f); p.vel.y = N.Mul32(p.vel.y, 2000f);
                }
                if (N.Mul32(p.angVel, p.angVel) > 62500f) p.angVel = p.angVel < 0f ? -250f : 250f;
                // x += dt * v ; angle += dt * w
                float mx = N.Mul32(dt, p.vel.x), my = N.Mul32(dt, p.vel.y);
                p.bodyPos = new Vector2(N.Add32(p.bodyPos.x, mx), N.Add32(p.bodyPos.y, my));
                p.bodyAngle = N.Add32(p.bodyAngle, N.Mul32(dt, p.angVel));
            }
        }

        // ------------------------------------------------------------ particleClass::OnUpdate 0x7dbc
        public void ObjectUpdate()
        {
            foreach (var p in pool)
            {
                if (!p.enabled || !p.active) continue;
                float s = S;
                Vector2 pos = p.pos, vel0 = p.vel;
                p.order = 7;
                float diff = N.Add32(pos.y, -p.baseY);
                if (!(diff < 0f))
                {
                    if (p.kind == 1) SetRotation(p, (float)Math.Atan2((double)vel0.y, (double)vel0.x));   // 0x9716: rotation follows the velocity
                    continue;
                }
                // landed: one rand() decides the resting height above the base (v85 = base + 4 * unit)
                float pointY = N.Add32(p.baseY, N.Mul32(unit(), 4f));
                var point = new Vector2(pos.x, pointY);
                switch (p.kind)
                {
                    case 1: Land(p); p.frame = "xue2Frame"; p.size = new Vector2(N.Mul32(s, 29f), N.Mul32(s, 3f)); LandTail(p, point); break;
                    case 2:
                        Land(p);
                        switch (p.sub)
                        {
                            case 0: p.frame = "xue5Frame"; p.size = new Vector2(N.Mul32(s, 42f), N.Mul32(s, 11f)); break;
                            case 1: p.frame = "xue4Frame"; p.size = new Vector2(N.Mul32(s, 35f), N.Mul32(s, 10f)); break;
                            case 2: p.frame = "xue6Frame"; p.size = new Vector2(N.Mul32(s, 42f), N.Mul32(s, 8f)); break;
                            case 3: p.frame = "xue7Frame"; p.size = new Vector2(N.Mul32(s, 42f), N.Mul32(s, 8f)); break;
                            default: break;
                        }
                        LandTail(p, point); break;
                    case 4: Land(p); p.frame = "suirou1Frame"; p.size = new Vector2(N.Mul32(s, 79f), N.Mul32(s, 19f)); LandTail(p, point); break;
                    case 3: LandBurst(p, pos, point); break;
                }
            }
        }

        /// <summary>bulletClass GameObjects (id 3): updated before every id-5/id-6 object of the scene map (r118z2; they used to follow the blood).</summary>
        public void ShellObjectsUpdate()
        {
            for (int i = 0; i < shells.Count; i++) { var b = shells[i]; if (b.enabled && b.kind == 2) ShellUpdate(b); }
        }

        /// <summary>bulletClass::OnUpdate 0x77a4 kind 2 (shell casing): below the ground plane it bounces (y mirrored about the base, vy *= -0.5, w += 2) until |vy| < 50, then Disappear.</summary>
        void ShellUpdate(BloodParticle p)
        {
            Vector2 pos = p.pos, vel = p.vel; float w = p.angVel;
            float d = N.Add32(pos.y, -p.baseY);
            if (!(d < 0f)) return;
            float ny = N.Add32(p.baseY, -d);
            float vy = N.Mul32(-0.5f, vel.y);
            if (Mathf.Abs(vy) < 50f) { ShellDisappear(p); return; }
            SetPosition(p, new Vector2(pos.x, ny));
            p.vel = new Vector2(vel.x, vy);
            p.angVel = N.Add32(w, 2f);
        }

        /// <summary>bulletClass::Disappear 0x7566: Enable(false), force/velocity/spin cleared, rotation 0, back to the scene's free list.</summary>
        void ShellDisappear(BloodParticle p)
        {
            p.enabled = false; p.force = Vector2.zero; p.vel = Vector2.zero; p.angVel = 0f; SetRotation(p, 0f);
            freeShells.Add(p);
        }

        BloodParticle GetShell()
        {
            ShellsSpawned++;
            if (freeShells.Count > 0) { var p = freeShells[freeShells.Count - 1]; freeShells.RemoveAt(freeShells.Count - 1); p.enabled = true; return p; }
            var n = new BloodParticle { baseY = groundY, enabled = true, visible = true, kind = 0 };
            shells.Add(n); return n;
        }

        /// <summary>playerClass::Danke 0xbcbb8 / playerNewAniClass::Danke 0x126804: eject a shell casing at pos (size, frame, sprite order from the weapon table).
        /// vx = lerp(-150S, -200S, r) (mirrored when the CA flip flag is set), vy = 400S, spin 10 + 4r, persistent force (0, -2000S).</summary>
        public BloodParticle Danke(Vector2 pos, Vector2 size, string frame, int order, bool flip)
        {
            float s = S;
            var p = GetShell();
            SetPosition(p, pos); p.size = size; p.order = order; p.frame = frame; p.visible = true;
            float x0 = N.Mul32(s, -150f), x1 = N.Mul32(s, -200f);
            float x = N.Add32(x0, N.Mul32(unit(), N.Add32(x1, -x0)));
            if (flip) x = N.F(BitConverter.SingleToInt32Bits(x) ^ int.MinValue);
            p.vel = new Vector2(x, N.Mul32(s, 400f));
            p.angVel = N.Add32(N.Mul32(unit(), 4f), 10f);
            p.force = new Vector2(0f, N.Mul32(s, -2000f));
            p.kind = 2;
            return p;
        }

        // kind 1/2/4 landing preamble: SetLinearVelocity(0), SetAngularVelocity(0), ApplyForce(0, point), active = 0
        static void Land(BloodParticle p) { p.vel = Vector2.zero; p.angVel = 0f; p.force = Vector2.zero; p.active = false; }
        // tail 0x8252: SetRotation(0) then SetPosition(x, base + 4 * unit)
        static void LandTail(BloodParticle p, Vector2 point) { SetRotation(p, 0f); SetPosition(p, point); }

        /// <summary>kind 3 (the flying head of DieTouFei) landing 0x826a..0x93e8: four kind-2 gore pieces at (x, base y); the head object is disabled.</summary>
        void LandBurst(BloodParticle parent, Vector2 pos, Vector2 point)
        {
            float s = S;
            for (int i = 0; i < 4; i++)
            {
                var c = Get();
                c.enabled = true; c.visible = true; SetPosition(c, new Vector2(pos.x, parent.baseY)); c.order = 9;
                if (i == 0) { c.frame = "s3Frame"; c.size = new Vector2(N.Mul32(s, 40f), N.Mul32(s, 33f)); }
                else if (i == 1) { c.frame = "s4Frame"; c.size = new Vector2(N.Mul32(s, 33f), N.Mul32(s, 28f)); }
                else { string f; Vector2 sz; if (PieceFrame(parent.style, parent.surface, i - 2, out f, out sz)) { c.frame = f; c.size = sz; } }
                float lo = N.Mul32(s, -400f), width = N.Add32(N.Mul32(s, 400f), -lo);
                float y0 = N.Mul32(s, 500f), yw = N.Add32(N.Mul32(s, 700f), -y0);
                float rx = unit(), ry = unit();
                c.vel = new Vector2(N.Add32(lo, N.Mul32(rx, width)), N.Add32(y0, N.Mul32(ry, yw)));
                c.force = new Vector2(0f, N.Mul32(s, -2000f));
                float r3 = unit();
                c.angVel = (float)((int)N.Add32(r3, r3) + 4);
                c.active = true; c.kind = 2; c.sub = i;
            }
            SetRotation(parent, 0f);
            parent.vel = Vector2.zero; parent.angVel = 0f; parent.force = Vector2.zero;
            parent.enabled = false; parent.visible = false; parent.active = false;
        }

        // ------------------------------------------------------------ gore piece frames: SKIN (style) x SURFACE code (DieNormal chunk 0x12e16, particleClass kind 3 landing)
        /// <summary>index 0 = "s1" piece, 1 = "s2" piece. false when the style has no frame assignment (the pooled object keeps its previous frame and size).</summary>
        public static bool PieceFrame(int style, int surface, int index, out string frame, out Vector2 size)
        {
            float s = S; string suffix = index == 0 ? "s1Frame" : "s2Frame"; string shard = index == 0 ? "suipian1Frame" : "suipian2Frame";
            frame = null; size = Vector2.zero;
            switch (style)
            {
                case 1:
                    frame = surface == 11 ? "k11" + suffix : surface == 101 ? "k101" + suffix : surface == 111 ? "k111" + suffix : "k3" + suffix;
                    size = index == 0 ? new Vector2(N.Mul32(s, 20f), N.Mul32(s, 32f)) : new Vector2(N.Mul32(s, 23f), N.Mul32(s, 21f)); return true;
                case 2:
                    frame = surface == 21 ? "k21" + suffix : surface == 211 ? "k211" + suffix : "k4" + suffix;
                    size = index == 0 ? new Vector2(N.Mul32(s, 20f), N.Mul32(s, 32f)) : new Vector2(N.Mul32(s, 23f), N.Mul32(s, 21f)); return true;
                case 3:
                    frame = surface == 31 ? "k31" + suffix : surface == 32 ? "k32" + suffix : surface == 301 ? "k301" + suffix : surface == 311 ? "k311" + suffix : surface == 321 ? "k321" + suffix : "k2" + suffix;
                    size = index == 0 ? new Vector2(N.Mul32(s, 29f), N.Mul32(s, 47f)) : new Vector2(N.Mul32(s, 34f), N.Mul32(s, 32f)); return true;
                case 5:
                    frame = (surface == 51 || surface == 52 || surface == 501 || surface == 511 || surface == 521) ? "k" + surface + shard : shard;
                    size = index == 0 ? new Vector2(N.Mul32(s, 23f), N.Mul32(s, 22f)) : new Vector2(N.Mul32(s, 25f), N.Mul32(s, 13f)); return true;
            }
            return false;
        }

        // ------------------------------------------------------------ spawners
        /// <summary>dinosaurClass::BlastBlood 0x12756 / bossN::BlastBlood 0x122e38 (position differs only). One kind-1 blood drop.</summary>
        public BloodParticle BlastBlood(Vector2 pos)
        {
            float s = S;
            var p = Get(); Begin(p, pos); p.frame = "xueFrame"; p.order = 11;
            float hw = N.Mul32(0.5f, N.Mul32(s, 37f)), hh = N.Mul32(0.5f, N.Mul32(s, 17f));
            float k = N.Add32(unit(), 1f);
            p.size = new Vector2(N.Mul32(k, hw), N.Mul32(k, hh));
            float x0 = N.Mul32(s, -400f), y0 = N.Mul32(s, 700f);
            float rx = unit(), ry = unit();
            p.vel = new Vector2(N.Add32(x0, N.Mul32(rx, N.Add32(N.Mul32(s, 400f), -x0))), N.Add32(y0, N.Mul32(ry, N.Add32(N.Mul32(s, 900f), -y0))));
            p.force = new Vector2(0f, N.Mul32(s, -2000f));
            p.active = true; p.kind = 1;
            return p;
        }

        /// <summary>dinosaurClass::BlastSuiRou 0x1226c: four kind-4 flesh chunks (suirou2..5Frame) at the "shen" (skin 5: "com2") sprite.</summary>
        public void BlastSuiRou(Vector2 pos)
        {
            float s = S;
            float a = N.Mul32(s, 46f), b = N.Mul32(s, 25f), c = N.Mul32(s, 50f), d = N.Mul32(s, 30f), e = N.Mul32(s, 47f), f = N.Mul32(s, 42f);
            float x0 = N.Mul32(s, -400f), xw = N.Add32(N.Mul32(s, 400f), -x0), y0 = N.Mul32(s, 700f), yw = N.Add32(N.Mul32(s, 900f), -y0);
            for (int i = 4; i < 8; i++)
            {
                var p = Get(); Begin(p, pos);
                Vector2 sz;
                switch (i)
                {
                    case 4: p.frame = "suirou2Frame"; sz = new Vector2(a, b); break;
                    case 5: p.frame = "suirou3Frame"; sz = new Vector2(c, d); break;
                    case 6: p.frame = "suirou4Frame"; sz = new Vector2(e, a); break;
                    default: p.frame = "suirou5Frame"; sz = new Vector2(f, f); break;
                }
                p.order = 11;
                float k = N.Add32(unit(), 1f);
                p.size = new Vector2(N.Mul32(k, N.Mul32(0.5f, sz.x)), N.Mul32(k, N.Mul32(0.5f, sz.y)));
                float rx = unit(), ry = unit();
                p.vel = new Vector2(N.Add32(x0, N.Mul32(rx, xw)), N.Add32(y0, N.Mul32(ry, yw)));
                float w = (float)range(3, 6);
                if (p.vel.x > 0f) w = -w;
                p.angVel = w;
                p.force = new Vector2(0f, N.Mul32(s, -2000f));
                p.active = true; p.kind = 4;
            }
        }

        /// <summary>dinosaurClass::DieTouFei 0x10cbc: the head flies off as a kind-3 particle. Returns null when the dinosaur already has a head object (+184).
        /// On iPad skins 1, 2 and 5 use the literal 1.6 as scale (v109 reassigned in those cases) for size, velocity and force; skins 3, 4, 6 keep S.</summary>
        public BloodParticle DieTouFei(Vector2 pos, int skin, int surface, bool flip)
        {
            float s = S, se = s;
            var p = Get(); Begin(p, pos); p.order = 11;
            switch (skin)
            {
                case 1:
                    p.frame = surface == 11 ? "k11toufeiFrame" : surface == 101 ? "k101toufeiFrame" : surface == 111 ? "k111toufeiFrame" : "k3toufeiFrame";
                    se = N.S == 2f ? 1.6f : s; p.size = new Vector2(N.Mul32(se, 75f), N.Mul32(se, 78f)); break;
                case 2:
                    p.frame = surface == 21 ? "k21toufeiFrame" : surface == 211 ? "k211toufeiFrame" : "k4toufeiFrame";
                    se = N.S == 2f ? 1.6f : s; p.size = new Vector2(N.Mul32(se, 75f), N.Mul32(se, 78f)); break;
                case 3:
                    p.frame = surface == 31 ? "k31toufeiFrame" : surface == 32 ? "k32toufeiFrame" : surface == 301 ? "k301toufeiFrame" : surface == 311 ? "k311toufeiFrame" : surface == 321 ? "k321toufeiFrame" : "k2toufeiFrame";
                    p.size = new Vector2(N.Mul32(s, 111f), N.Mul32(s, 107f)); break;
                case 5:
                    p.frame = (surface == 51 || surface == 52 || surface == 501 || surface == 511 || surface == 521) ? "k" + surface + "teshutou0003Frame" : "teshutou0003Frame";
                    se = N.S == 2f ? 1.6f : s; p.size = new Vector2(N.Mul32(se, 71f), N.Mul32(se, 99f)); break;
            }
            var v = new Vector2(N.Mul32(se, -100f), N.Mul32(se, 300f));
            float w;
            if (flip) { v.x = N.F(BitConverter.SingleToInt32Bits(v.x) ^ int.MinValue); w = -4f; } else w = 4f;
            p.angVel = w; p.vel = v;
            p.force = new Vector2(0f, N.Mul32(se, -2000f));
            p.flipX = flip;                                      // Sprite()+84 := dinosaur flag96
            p.active = true; p.kind = 3; p.style = skin; p.surface = surface;
            return p;
        }

        /// <summary>dinosaurClass::DieNormal 0x12ca8 (chunk 0x12e16): four kind-2 gore pieces, then three BlastBlood drops, all at the head sprite ("tou", skin 5 "com6").</summary>
        public void DieNormal(Vector2 pos, int skin, int surface)
        {
            float s = S;
            float lo = N.Mul32(s, -200f), width = N.Add32(N.Mul32(s, 200f), -lo), y0 = N.Mul32(s, 500f), yw = N.Add32(N.Mul32(s, 700f), -y0);
            for (int i = 0; i < 4; i++)
            {
                var p = Get(); Begin(p, pos); p.order = 11;
                if (i == 0) { p.frame = "s3Frame"; p.size = new Vector2(N.Mul32(s, 40f), N.Mul32(s, 33f)); }
                else if (i == 1) { p.frame = "s4Frame"; p.size = new Vector2(N.Mul32(s, 33f), N.Mul32(s, 28f)); }
                else { string f; Vector2 sz; if (PieceFrame(skin, surface, i - 2, out f, out sz)) { p.frame = f; p.size = sz; } }
                float rx = unit(), ry = unit();
                p.vel = new Vector2(N.Add32(lo, N.Mul32(rx, width)), N.Add32(y0, N.Mul32(ry, yw)));
                float w = (float)range(3, 6);
                if (!(p.vel.x < 0f)) w = -w;               // DieNormal chunk: __unordsf2 || __gesf2(vx, 0) -> negate (vx >= 0), unlike BlastSuiRou's vx > 0
                p.angVel = w;
                p.force = new Vector2(0f, N.Mul32(s, -2000f));
                p.active = true; p.kind = 2; p.sub = i;
            }
            BlastBlood(pos); BlastBlood(pos); BlastBlood(pos);
        }

        /// <summary>playerClass::Die 0xbcdea: six blood drops at the "yyj6" sprite. Every constant is the unscaled (iPhone) literal: size 0.5*(37,17)*(1+r),
        /// velocity (int)(r*800)-400, (int)(r*200)+700, force -2000.</summary>
        public void HeroDie(Vector2 pos)
        {
            for (int i = 0; i < 6; i++)
            {
                var p = Get(); Begin(p, pos); p.frame = "xueFrame"; p.order = 11;
                float k = N.Add32(unit(), 1f);
                p.size = new Vector2(N.Mul32(k, N.Mul32(0.5f, 37f)), N.Mul32(k, N.Mul32(0.5f, 17f)));
                float r1 = unit(), r2 = unit();
                p.vel = new Vector2((float)((int)N.Mul32(r1, 800f) - 400), (float)((int)N.Mul32(r2, 200f) + 700));
                p.force = new Vector2(0f, -2000f);
                p.active = true; p.kind = 1;
            }
        }

        /// <summary>playerNewAniClass::Die 0x126a36: six blood drops at the "lian" sprite; velocity from RandomRange(-400,400) / RandomRange(700,900); unscaled constants.</summary>
        public void GirlDie(Vector2 pos)
        {
            for (int i = 0; i < 6; i++)
            {
                var p = Get(); Begin(p, pos); p.frame = "xueFrame"; p.order = 11;
                float k = N.Add32(unit(), 1f);
                p.size = new Vector2(N.Mul32(k, N.Mul32(0.5f, 37f)), N.Mul32(k, N.Mul32(0.5f, 17f)));
                int vx = range(-400, 400), vy = range(700, 900);
                p.vel = new Vector2((float)vx, (float)vy);
                p.force = new Vector2(0f, -2000f);
                p.active = true; p.kind = 1;
            }
        }
    }
}
