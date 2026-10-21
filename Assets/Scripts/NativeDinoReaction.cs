using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dc2Pilot
{
    /// <summary>
    /// NATIVE dinosaurClass::BeAttacked 0x14918 non-lethal reaction tail 14f8e..1566c, evaluated on the actual ARM bytes
    /// (Dev/Reports/DC2-PILOT-001/dino_reaction_oracle.r94.json + r96_skin2; table built by Dev/Tools/prepare_dino_reaction_r96.py).
    /// Rows are keyed by weapon type/subtype, actor field196 (= SKIN, copied from property +152 by lifeModeScene::CreateEnemy 0xd5308), actor state
    /// and facing flag. Skins 2 and 4 are identical to skin 1 in every sampled case. Blood (argument, -1 = not called), BlastBlood and PlaySound
    /// requests are carried and rendered by GameCombat.ApplyNativeDinoReaction.
    /// </summary>
    public static class NativeDinoReaction
    {
        [Serializable] public class Row
        {
            public int t, s, k, st; public bool fl; public int sa, f160; public string m;
            public float ai, bi, vs, vx, bd; public int blood, blast; public string snd;
        }
        [Serializable] class Table { public Row[] rows; }
        public struct Result
        {
            public bool found, advanced, basePose, hasVelocity, hasBaseDelta;
            public int stateAfter, flag160;
            public float advInterval, baseInterval, velocitySpdScale, velocityAbsolute, baseDelta;
            public int blood, blast; public string sound;
        }
        static Dictionary<long, Row> map;

        static long Key(int t, int s, int k, int st, bool fl) { return ((((long)t * 16 + s) * 8 + k) * 16 + st) * 2 + (fl ? 1 : 0); }
        static void Load()
        {
            map = new Dictionary<long, Row>();
            var data = Resources.Load<TextAsset>("DC2/native_dino_reaction.json");
            if (data == null) throw new Exception("native_dino_reaction.json missing; run Dev/Tools/prepare_dino_reaction_r96.py");
            foreach (var r in JsonUtility.FromJson<Table>(data.text).rows) map[Key(r.t, r.s, r.k, r.st, r.fl)] = r;
        }

        public static Result Evaluate(int type, int subtype, int skin, int state, bool faceRight)
        {
            if (map == null) Load();
            int k = skin == 2 || skin == 4 ? 1 : skin;
            Row r;
            var res = new Result { blood = -1 };   // blood: -1 = no Blood call, else its argument (0 = qx, 1 = dx); DC2-BUGS-001 B5
            // R002-B1: the native non-lethal tail (BeAttacked 0x14f8e..0x1566c) branches on state == 0 vs != 0 only. The sampled table has states 0/1/2/7/8;
            // its 1/7/8 rows are identical except the post-state (no-write rows keep the incoming state, BehitedAnim rows write 2). States 4/5/6 (BeDamaged /
            // BeDamagedOver) take the same non-zero row and keep their own state on no-write rows. PROVEN for the no-write rows (Codex native probe, 54 cases:
            // state kept, Blood only); LIKELY for the BehitedAnim rows (state-invariance of the sampled 1/7/8 rows). FALSIFIED: the miss fell into the guessed
            // legacy reaction (0.25 s, velocity 0, state 4).
            bool collapsed = false;
            if (!map.TryGetValue(Key(type, subtype, k, state, faceRight), out r))
            {
                if (state == 0 || !map.TryGetValue(Key(type, subtype, k, 1, faceRight), out r)) return res;
                collapsed = true;
            }
            res.found = true; res.stateAfter = collapsed && r.m != "base" ? state : r.sa; res.flag160 = r.f160;
            res.advanced = r.m == "adv"; res.basePose = r.m == "base";
            res.advInterval = r.ai; res.baseInterval = r.bi;
            if (res.advanced) { res.hasVelocity = true; res.velocitySpdScale = r.vs; }
            else if (res.basePose) { res.hasVelocity = true; res.velocityAbsolute = r.vx; }
            if (r.m == "adv" && r.bd != 0f) { res.hasBaseDelta = true; res.baseDelta = r.bd; }
            res.blood = r.blood; res.blast = r.blast; res.sound = r.snd;
            return res;
        }
    }
}
