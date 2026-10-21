using System;
using System.Collections.Generic;

namespace Dc2Pilot
{
    /// <summary>Triniti2D::Timer (one per Application, reached through TimerProxy) with its PriorityQueueT&lt;TimerInfo,float&gt; heap. NATIVE (r118z/DC2-ENGINE-001 L2):
    /// SetTimer 0x17ca7c builds TimerInfo{callback, data, interval, repeat, deadline = now + interval} and pushes it on the pending deque (+64);
    /// KillTimer 0x17ca5e pushes the info on the kill deque (+104). Update 0x17ccd6: (1) every pending info is Insert-ed into the heap and the live set,
    /// (2) every kill whose info is still in the live set is Remove-d and deleted, (3) if dt &gt; 0 then now += dt (float), (4) while the heap is not empty and
    /// !(top.deadline &gt; now): call the callback; a repeat timer gets deadline = now + interval and heap Update, otherwise Pop and delete.
    /// Timers set inside a callback wait in the pending deque until the next Update, so a re-arm never fires twice in one Update.
    /// PriorityQueueT: vector of {info, priority} plus map info-&gt;index. Insert 0x17e394 push_back + PercolateUp; Remove 0x17e4f0 swaps with the last node
    /// (when not last), shrinks, then PercolateUp and PercolateDown at the hole; Update 0x17e450 sets the priority then PercolateUp/PercolateDown; Pop 0x17e5c8 = Remove(top).
    /// PercolateUp 0x17e330 swaps while child &lt; parent; PercolateDown 0x17e25c takes the right child only when left &gt; right (__gtsf2 at 0x17e2c6) and swaps while child &lt; node (__ltsf2 at 0x17e2e2).</summary>
    public sealed class NativeTimer
    {
        public sealed class Info
        {
            public object owner;        // TimerCallBack target (scene or game object)
            public object scene;        // scene that created it (port teardown bookkeeping)
            public string name;         // port-side label for the native timer id compare in OnTimeout
            public Action action;       // port-side dispatch for owner-held timer ids (same call position as the native callback)
            public float interval, deadline;
            public bool repeat;
            internal bool live;
        }

        struct Node { public Info info; public float priority; }
        readonly List<Node> nodes = new List<Node>();
        readonly Dictionary<Info, int> index = new Dictionary<Info, int>();
        readonly Queue<Info> pending = new Queue<Info>();
        readonly Queue<Info> kills = new Queue<Info>();
        public float Now { get; private set; }
        public int Count { get { return nodes.Count; } }
        public int PendingCount { get { return pending.Count; } }

        public Info Set(object owner, string name, float interval, bool repeat, Action action) { return Set(owner, owner, name, interval, repeat, action); }
        public Info Set(object scene, object owner, string name, float interval, bool repeat, Action action)
        {
            var t = new Info { scene = scene, owner = owner, name = name, action = action, interval = interval, repeat = repeat, deadline = N.Add32(Now, interval) };
            pending.Enqueue(t);
            return t;
        }

        public void Kill(Info t) { if (t != null) kills.Enqueue(t); }

        /// <summary>Port helper for scene/object teardown: kills every queued or live timer of one owner through the same kill deque.</summary>
        public void KillOwner(object owner)
        {
            foreach (var n in nodes) if (n.info.owner == owner) kills.Enqueue(n.info);
            foreach (var p in pending) if (p.owner == owner) kills.Enqueue(p);
        }

        /// <summary>Scene switch: the outgoing scene's timers (its own and its game objects') are killed through the kill deque. NATIVE: Scene::Unload 0x175cce
        /// (TimerProxy::SceneKillTimer for each id in the scene's timer map) and GameObject::Unload 0x16c494 (GameObjectKillTimer for each object timer).</summary>
        public void KillOwnersOf(object scene)
        {
            foreach (var n in nodes) if (n.info.scene == scene) kills.Enqueue(n.info);
            foreach (var p in pending) if (p.scene == scene) kills.Enqueue(p);
        }

        public bool IsLive(Info t) { return t != null && (t.live || pending.Contains(t)); }

        public void Update(float dt, Action<Info> fire)
        {
            while (pending.Count > 0) { var t = pending.Dequeue(); t.live = true; Insert(t, t.deadline); }
            while (kills.Count > 0) { var t = kills.Dequeue(); if (t.live) { Remove(t); t.live = false; } }
            if (dt > 0f) Now = N.Add32(Now, dt);
            while (nodes.Count > 0 && !(nodes[0].info.deadline > Now))
            {
                var t = nodes[0].info;
                fire(t);
                if (!t.live || nodes.Count == 0 || nodes[0].info != t) continue;   // defensive: native callbacks only queue kills, never touch the heap
                if (t.repeat) { t.deadline = N.Add32(Now, t.interval); UpdatePriority(t, t.deadline); }
                else { Remove(t); t.live = false; }
            }
        }

        // ---- PriorityQueueT<TimerInfo,float> ----
        void Insert(Info t, float p)
        {
            nodes.Add(new Node { info = t, priority = p });
            index[t] = nodes.Count - 1;
            PercolateUp(nodes.Count - 1);
        }

        void Remove(Info t)
        {
            int i;
            if (!index.TryGetValue(t, out i)) return;
            int last = nodes.Count - 1;
            if (i != last)
            {
                Swap(i, last);
                nodes.RemoveAt(last); index.Remove(t);
                PercolateUp(i); PercolateDown(i);
            }
            else { nodes.RemoveAt(last); index.Remove(t); }
        }

        void UpdatePriority(Info t, float p)
        {
            int i = index[t];
            var n = nodes[i]; n.priority = p; nodes[i] = n;
            PercolateUp(i); PercolateDown(index[t]);
        }

        void Swap(int a, int b)
        {
            var na = nodes[a]; var nb = nodes[b];
            nodes[a] = nb; nodes[b] = na;
            index[nb.info] = a; index[na.info] = b;
        }

        void PercolateUp(int i)
        {
            while (i != 0)
            {
                int parent = (i - 1) / 2;
                if (!(nodes[i].priority < nodes[parent].priority)) break;
                Swap(i, parent); i = parent;
            }
        }

        void PercolateDown(int i)
        {
            while (true)
            {
                int l = 2 * i + 1;
                if (l >= nodes.Count) break;
                int c = l; float cp = nodes[l].priority;
                int r = 2 * i + 2;
                if (r < nodes.Count && cp > nodes[r].priority) { c = r; cp = nodes[r].priority; }
                if (!(cp < nodes[i].priority)) break;
                Swap(i, c); i = c;
            }
        }

        /// <summary>Test hook: heap property of PriorityQueueT::Check 0x17d2ac (parent priority &lt;= each child).</summary>
        public bool CheckHeap()
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                int l = 2 * i + 1, r = l + 1;
                if (l < nodes.Count && nodes[i].priority > nodes[l].priority) return false;
                if (r < nodes.Count && nodes[i].priority > nodes[r].priority) return false;
            }
            foreach (var kv in index) if (nodes[kv.Value].info != kv.Key) return false;
            return true;
        }
    }
}
