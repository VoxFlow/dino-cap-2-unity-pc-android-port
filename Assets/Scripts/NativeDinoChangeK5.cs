using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dc2Pilot
{
    /// <summary>
    /// NATIVE CombineAnimation::DinoChangeImage 0xdb2c for the K5 (pterodactyl) surfaces 50/51/52/501/511/521, executed on the ARM bytes
    /// (Dev/Tools/emu_dinochange.py -> Dev/Recovered/dinochangeimage_emu.json; ChangeImage 0xcc8a and ChangeImageK5 0xc9c4 recorded).
    /// Key: surface_die2 die hit_a6. The K5 branch of loadDinoANI passes hit=0, a6=0 for every animation, die=1 for die, die2=1 for die2.
    /// </summary>
    public static class NativeDinoChangeK5
    {
        [Serializable] class Op { public bool k5; public string part, image; }
        [Serializable] class Entry { public string key; public Op[] ops; }
        [Serializable] class Table { public Entry[] entries; }
        static Dictionary<string, Op[]> map;

        public static void Apply(NativeCaAnimation animation, int surface, bool die2, bool die, bool hit, bool a6)
        {
            if (map == null)
            {
                map = new Dictionary<string, Op[]>();
                foreach (var e in JsonUtility.FromJson<Table>(Resources.Load<TextAsset>("DC2/native_dino_changeimage_k5.json").text).entries) map[e.key] = e.ops;
            }
            Op[] ops;
            string key = surface + "_" + (die2 ? 1 : 0) + (die ? 1 : 0) + (hit ? 1 : 0) + "_" + (a6 ? 1 : 0);
            if (!map.TryGetValue(key, out ops)) throw new Exception("Unrecovered K5 DinoChangeImage case " + key);
            foreach (var op in ops)
            {
                if (op.k5) animation.ChangeImageK5(op.part, op.image);
                else animation.ChangeImage(op.part, op.image);
            }
        }
    }
}
