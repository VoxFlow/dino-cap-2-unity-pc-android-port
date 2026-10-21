using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using UnityEngine;

namespace Dc2Pilot
{
    /// <summary>p2dGraphField (0x110c64..): a list of (time, value) keys. Initialize(def) leaves the single key (0, def); LoadXml clears and replaces the keys.</summary>
    public sealed class P2dGraph
    {
        struct Key { public float time, value; }
        readonly List<Key> keys = new List<Key>();
        public P2dGraph(float def) { keys.Add(new Key { time = 0f, value = def }); }
        public void Clear() { keys.Clear(); }
        public void Add(float time, float value) { keys.Add(new Key { time = time, value = value }); }

        /// <summary>getGraphValue 0x110c88: t &lt; 1e-4 or a single key -> first key; t &gt;= last key time -> last key value; otherwise linear between the keys around t.</summary>
        public float Value(float t)
        {
            if ((double)t < 0.0001 || keys.Count <= 1) return keys[0].value;
            var last = keys[keys.Count - 1];
            if (t >= last.time) return last.value;
            int i = 0;
            while (i < keys.Count) { if (t <= keys[i].time) break; i++; }
            if (i >= keys.Count) i = keys.Count - 1;
            var cur = keys[i];
            if (Mathf.Abs(cur.time - t) < 0.0001f) return cur.value;
            var prev = keys[i - 1];
            float f = (t - prev.time) / (cur.time - prev.time);
            return (1f - f) * prev.value + f * cur.value;
        }

        /// <summary>calcGraphBV 0x110dc4: base(t) + uniform(-var(t)/2, +var(t)/2).</summary>
        public static float BaseVar(P2dGraph b, P2dGraph v, float t, Func<float> rand01)
        {
            float half = v.Value(t) * 0.5f;
            return b.Value(t) + (-half + rand01() * (half - (-half)));
        }
    }

    /// <summary>p2dParticleEmitter (0x111230..0x1137d8): the Power2D particle engine used by the rain (raineff / rainhua / raineff2 .xml). Region: POINT / LINEX / LINEY / AREA from the
    /// emitter GameObject position and size; per-particle life curves; fixed force direction (cos, sin of fixforcangle taken as radians); ALIGNED orientation = atan2(v);
    /// quantity(t) = (base + var) * t cumulative count (Play resets time and count).</summary>
    public sealed class NativeParticleEmitter
    {
        sealed class Node
        {
            public float x, y, sx, sy, rot, life, age, vx, vy, baseSx, baseSy, speed, spin, fixedForce, randomMotion; public byte color; public bool fresh;
        }

        readonly Dictionary<string, P2dGraph> fields = new Dictionary<string, P2dGraph>();
        readonly List<Node> nodes = new List<Node>();
        readonly Func<float> rand01;
        int orientation;       // 0 FIXED, 1 ALIGNED, 2 RANDOM (+692)
        int type;              // 0 POINT, 1 LINEX, 2 LINEY, 3 AREA (+696)
        float fixAngle;        // +716 (radians)
        bool fixAspect;        // +720
        float forceCos, forceSin;   // +708 / +712
        public string textureFrame = "";
        float time; int spawned; bool playing;   // +724 / +728 / +733
        public Vector2 position, size;           // emitter GameObject position / size
        public int layer;
        public int AliveCount { get { return nodes.Count; } }
        public int SpawnedCount { get { return spawned; } }

        static readonly string[] names =
        {
            "particlelife_base","particlelife_var","quantity_base","quantity_var","sizex_base","sizex_var","sizex_life","sizey_base","sizey_var","sizey_life",
            "speed_base","speed_var","speed_life","spin_base","spin_var","spin_life","fixedforce_base","fixedforce_var","fixedforce_life","randommotion_base","randommotion_var","randommotion_life",
            "emissionforce_base","emissionforce_var","emissionangle_base","emissionangle_var","emissionarc_base","emissionarc_var","red_life","green_life","blue_life","visibility_life"
        };
        static readonly float[] defaults = { 2f,0f,10f,0f,20f,0f,1f,20f,0f,1f,10f,0f,1f,0f,0f,1f,0f,0f,1f,0f,0f,1f,5f,0f,0f,0f,360f,0f,1f,1f,1f,1f };   // Initialize 0x112220

        public NativeParticleEmitter(Func<float> rand01)
        {
            this.rand01 = rand01;
            for (int i = 0; i < names.Length; i++) fields[names[i]] = new P2dGraph(defaults[i]);
        }

        /// <summary>LoadXml 0x111d38: Emitter attributes and the DataKey/Key lists.</summary>
        public void LoadXml(string xml)
        {
            var doc = new XmlDocument(); doc.LoadXml(xml);
            var emitter = (XmlElement)doc.DocumentElement.SelectSingleNode("Emitter");
            textureFrame = emitter.GetAttribute("texture");
            fixAspect = emitter.GetAttribute("fixaspect") != "0";
            float a;
            if (float.TryParse(emitter.GetAttribute("fixangle"), NumberStyles.Float, CultureInfo.InvariantCulture, out a)) fixAngle = a;
            fixAngle = fixAngle * 0.017453292f;        // 1016003125 (pi/180)
            float force = 0f;
            float.TryParse(emitter.GetAttribute("fixforcangle"), NumberStyles.Float, CultureInfo.InvariantCulture, out force);
            forceCos = Mathf.Cos(force); forceSin = Mathf.Sin(force);   // used as radians (shipped quirk: cosf/sinf of the raw attribute)
            string o = emitter.GetAttribute("orientaion");
            if (o == "FIXED") orientation = 0; else if (o == "ALIGNED") orientation = 1; else if (o == "RANDOM") orientation = 2;
            string t = emitter.GetAttribute("type");
            if (t == "POINT") type = 0; else if (t == "LINEX") type = 1; else if (t == "LINEY") type = 2; else if (t == "AREA") type = 3;
            foreach (XmlElement dk in emitter.SelectNodes("DataKey"))
            {
                P2dGraph g;
                if (!fields.TryGetValue(dk.GetAttribute("name"), out g)) { g = new P2dGraph(0f); fields[dk.GetAttribute("name")] = g; }
                g.Clear();
                foreach (XmlElement k in dk.SelectNodes("Key"))
                {
                    float kt = 0f, kv = 0f;
                    float.TryParse(k.GetAttribute("Time"), NumberStyles.Float, CultureInfo.InvariantCulture, out kt);
                    float.TryParse(k.GetAttribute("Value"), NumberStyles.Float, CultureInfo.InvariantCulture, out kv);
                    g.Add(kt, kv);
                }
            }
        }

        public void Play() { playing = true; time = 0f; spawned = 0; }

        float U(float lo, float hi) { return lo + rand01() * (hi - lo); }

        void Gen()
        {
            var n = new Node();
            float hw = size.x * 0.5f, hh = size.y * 0.5f;
            switch (type)
            {
                case 0: n.x = position.x; n.y = position.y; break;
                case 1: n.y = position.y; n.x = U(position.x - size.x * 0.5f, position.x + size.x * 0.5f); break;
                case 2: n.x = position.x; n.y = U(position.y - size.y * 0.5f, position.y + size.y * 0.5f); break;
                default: n.x = U(position.x - size.x * 0.5f, position.x + size.x * 0.5f); n.y = U(position.y - size.y * 0.5f, position.y + size.y * 0.5f); break;
            }
            n.age = 0f;
            n.life = P2dGraph.BaseVar(fields["particlelife_base"], fields["particlelife_var"], time, rand01);
            n.baseSx = P2dGraph.BaseVar(fields["sizex_base"], fields["sizex_var"], time, rand01);
            n.baseSy = P2dGraph.BaseVar(fields["sizey_base"], fields["sizey_var"], time, rand01);
            n.speed = P2dGraph.BaseVar(fields["speed_base"], fields["speed_var"], time, rand01);
            n.spin = P2dGraph.BaseVar(fields["spin_base"], fields["spin_var"], time, rand01);
            n.fixedForce = P2dGraph.BaseVar(fields["fixedforce_base"], fields["fixedforce_var"], time, rand01);
            n.randomMotion = P2dGraph.BaseVar(fields["randommotion_base"], fields["randommotion_var"], time, rand01);
            float emissionForce = P2dGraph.BaseVar(fields["emissionforce_base"], fields["emissionforce_var"], time, rand01);
            float emissionAngle = P2dGraph.BaseVar(fields["emissionangle_base"], fields["emissionangle_var"], time, rand01);
            float arc = P2dGraph.BaseVar(fields["emissionarc_base"], fields["emissionarc_var"], time, rand01);
            float halfArc = arc * 0.5f;
            float theta = (emissionAngle + (-halfArc + rand01() * (halfArc - (-halfArc)))) * 0.017453292f;
            n.vx = emissionForce * Mathf.Cos(theta); n.vy = emissionForce * Mathf.Sin(theta);
            n.sx = n.baseSx; n.sy = n.baseSy;
            if (orientation == 0) n.rot = fixAngle; else if (orientation == 2) n.rot = rand01() * 6.2831855f;
            float vis = fields["visibility_life"].Value(0f);
            n.color = (byte)(uint)(vis * 255f);
            n.fresh = true;   // spawned after this tick's render list was built (OnUpdate 0x111c10): first drawn next tick
            nodes.Add(n); spawned++;
        }

        /// <summary>tParticleNode::OnUpdate 0x11149c; returns true when the node died.</summary>
        bool UpdateNode(Node n, float dt)
        {
            n.fresh = false;
            n.age += dt;
            if (n.age > n.life || n.life == 0f) return true;
            float t = n.age / n.life;
            float sizeX = fields["sizex_life"].Value(t), sizeY = fields["sizey_life"].Value(t);
            float speedLife = fields["speed_life"].Value(t), spinLife = fields["spin_life"].Value(t);
            float ff = n.fixedForce * fields["fixedforce_life"].Value(t);
            float rm = n.randomMotion * fields["randommotion_life"].Value(t);
            float vis = fields["visibility_life"].Value(t);
            if (Mathf.Abs(rm) > 0.0001f)
            {
                float half = rm * 0.5f;
                float ax = -half + rand01() * (half - (-half)), ay = -half + rand01() * (half - (-half));
                n.vx += dt * ax; n.vy += dt * ay;
            }
            if (Mathf.Abs(ff) > 0.0001f)
            {
                float d = dt * ff;
                n.vx += d * forceCos; n.vy += d * forceSin;
            }
            float step = dt * (n.speed * speedLife);
            n.x += step * n.vx; n.y += step * n.vy;
            n.sx = n.baseSx * sizeX; n.sy = n.baseSy * sizeY;
            if (fixAspect) n.sy = n.sx;
            n.color = (byte)(uint)(vis * 255f);   // NATIVE quirk: red/green/blue_life are evaluated and discarded, all four bytes = visibility (0x11149c +48..+51)
            if (orientation == 1) n.rot = (float)Math.Atan2(n.vy, n.vx);
            else n.rot += dt * (n.spin * spinLife);
            return false;
        }

        /// <summary>One emitter OnUpdate 0x111c10: update/erase the nodes, then spawn so that the cumulative count follows (quantity_base(t) + var) * t.</summary>
        public void Step(float dt)
        {
            // std::list<tParticleNode> front-to-back (oldest first), erase in place: the random-motion rand() draws follow that order (r118z L5; the port iterated newest-first)
            for (int i = 0; i < nodes.Count; ) { if (UpdateNode(nodes[i], dt)) nodes.RemoveAt(i); else i++; }
            if (playing)
            {
                time += dt;
                float q = fields["quantity_base"].Value(time);
                float v = fields["quantity_var"].Value(time) * 0.5f;
                float total = (q + (-v + rand01() * (v - (-v)))) * time;
                int target = (int)total;
                for (int i = 0; i < target - spawned; i++) Gen();
            }
        }

        // ---- drawing: one quad per node (RenderMesh::Make + DrawTriangles) ----
        readonly List<NativeAffineSprite> sprites = new List<NativeAffineSprite>();

        public void Draw(Transform parent, float camCenterX)
        {
            int count = 0;
            if (Atlas.Has(textureFrame))
            {
                float rw = Atlas.Get(textureFrame).rect.width, rh = Atlas.Get(textureFrame).rect.height;
                foreach (var n in nodes)
                {
                    if (n.fresh) continue;   // native render vector (+756) holds only the nodes updated this tick
                    while (sprites.Count <= count) sprites.Add(NativeAffineSprite.Create(parent));
                    var frame = new NativeComponentAnimation.Frame { scaleX = n.sx / rw, scaleY = n.sy / rh, radians = n.rot, rgba = (uint)(n.color | (n.color << 8) | (n.color << 16) | (n.color << 24)) };
                    sprites[count].Draw(textureFrame, frame, new Vector2(N.ScreenW * 0.5f + (n.x - camCenterX), -N.ScreenH * 0.5f + n.y), count, 1f, false, null);
                    count++;
                }
            }
            for (int i = count; i < sprites.Count; i++) sprites[i].Hide();
        }

        public void Hide() { foreach (var s in sprites) s.Hide(); }
        public void Destroy() { foreach (var s in sprites) if (s != null) UnityEngine.Object.Destroy(s.gameObject); sprites.Clear(); nodes.Clear(); }
    }
}
