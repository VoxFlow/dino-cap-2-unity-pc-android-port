using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Dc2Pilot
{
    /// <summary>
    /// Gameplay data files, parsed the way the native loaders do.
    /// weapon.prop: weaponManager::LoadFromXml 0xc6232 + weaponClass::ReadProperty 0xc5718.
    /// Player.xml weapons: playerTypeProperty::loadFromXml 0xc5fb6.  dinosaur.prop / lifedinosaur.prop / boss.xml: see registry dc2.data.*.
    /// Native startup and weaponManager select Conf/weapon.prop and Conf/dinosaur.prop on iPad too.
    /// The retained LifeDinos parser has no proven native caller yet; it is not gameplay truth.
    /// </summary>
    public sealed class WeaponDef
    {
        public int type, index;             // native +36 type, +40 index inside the type
        public string name = "";
        public int exp;                     // native +4, XML "exp"; do not infer XP meaning from its name.
        public float atkMin, atkMax, atkDis;
        public bool group;                  // ATK_GROUP != 0
        public float crit;                  // native+24: RandomRange(0,10000)%100 converted float, strict<threshold (r90).
        public int baseAtk;                 // ATK_BASE: atoi. Both fields present in all35 native root records.
    }

    public struct DinoStats { public float hp, spd, exp, frq, atk, adis; public int skin, surface; }

    public sealed class LifeInterval { public float time; public DinoStats[] dinos = new DinoStats[4]; }

    public sealed class PlayerWeaponSlot { public int type, subtype, curLevel, maxLevel, ammoPrice, ammoAdd; }

    public sealed class BossAni { public string type; public Dictionary<string, float> attr = new Dictionary<string, float>(); }
    public sealed class BossDef { public int hp, exp, money, level; public List<BossAni> anis = new List<BossAni>(); }

    public static class Dc2GameData
    {
        static List<List<WeaponDef>> weapons;
        static List<PlayerWeaponSlot>[] playerWeapons;
        static PlayerWeaponSlot[] playerMelee;
        static DinoStats[] dinos;
        static List<LifeInterval> life;
        static List<BossDef> bosses;

        static string Load(string res) { var ta = Resources.Load<TextAsset>(res); if (ta == null) throw new Exception("missing " + res); return ta.text; }
        static float F(string s) { return (float)double.Parse(s.Trim(), CultureInfo.InvariantCulture); } // native atof -> float
        static int I(string s) { int v; return int.TryParse(s.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out v) ? v : 0; }
        static int Atoi(string s)
        {
            var prefix = Regex.Match(s, @"^\s*([+-]?\d+)");
            return prefix.Success ? I(prefix.Groups[1].Value) : 0;
        }

        public static List<List<WeaponDef>> Weapons()
        {
            if (weapons != null) return weapons;
            string t = Load("DC2/Image/Conf/weapon.prop");
            weapons = new List<List<WeaponDef>>();
            foreach (Match w in Regex.Matches(t, "<Weapon type=\"(\\d+)\">(.*?)</Weapon>", RegexOptions.Singleline))
            {
                int type = I(w.Groups[1].Value);
                var list = new List<WeaponDef>();
                foreach (Match s in Regex.Matches(w.Groups[2].Value, "<SubWeapon name=\"([^\"]*)\" exp=\"(\\d+)\">(.*?)</SubWeapon>", RegexOptions.Singleline))
                {
                    string b = s.Groups[3].Value;
                    var d = new WeaponDef { type = type, index = list.Count, name = s.Groups[1].Value, exp = I(s.Groups[2].Value) };
                    d.atkMin = Tag(b, "ATK_MIN", true); d.atkMax = Tag(b, "ATK_MAX", true);
                    d.atkDis = (int)Tag(b, "ATK_DIS", false); d.group = (int)Tag(b, "ATK_GROUP", false) != 0;
                    d.crit = (int)Tag(b, "ATK_CRIT", false); d.baseAtk = (int)Tag(b, "ATK_BASE", false);
                    list.Add(d);
                }
                weapons.Add(list);
            }
            return weapons;
        }

        static float Tag(string body, string tag, bool real)
        {
            var m = Regex.Match(body, "<" + tag + ">(.*?)</" + tag + ">", RegexOptions.Singleline);
            if (!m.Success) return 0f;                      // absent: native GetText(null)/atoi gives 0 (ASSUMPTION PA-011)
            return real ? F(m.Groups[1].Value) : (float)Atoi(m.Groups[1].Value);
        }

        static void LoadPlayers()
        {
            if (playerWeapons != null) return;
            string t = Load("DC2/Image/Conf/Player.xml");
            var ms = Regex.Matches(t, "<player id=\"(\\d+)\".*?</player>", RegexOptions.Singleline);
            playerWeapons = new List<PlayerWeaponSlot>[ms.Count]; playerMelee = new PlayerWeaponSlot[ms.Count];
            for (int i = 0; i < ms.Count; i++)
            {
                playerWeapons[i] = new List<PlayerWeaponSlot>();
                // FirstChildElement("weapon"), then unfiltered NextSiblingElement@0xc607c.
                // The shipped trailing jinweapon therefore becomes slot5 as well as the separate melee record.
                // QueryIntAttribute leaves the destination unchanged when an attribute is absent.
                var values = new int[6];
                string[] names = { "type", "subtype", "curlevel", "maxlevel", "ammoprice", "ammoadd" };
                foreach (Match w in Regex.Matches(ms[i].Value, "<(?:weapon|jinweapon)\\b([^>]*)>"))
                {
                    for (int field = 0; field < names.Length; field++)
                    {
                        var attribute = Regex.Match(w.Groups[1].Value, "\\b" + names[field] + "=\"(\\d+)\"");
                        if (attribute.Success) values[field] = I(attribute.Groups[1].Value);
                    }
                    playerWeapons[i].Add(new PlayerWeaponSlot { type = values[0], subtype = values[1],
                        curLevel = values[2], maxLevel = values[3], ammoPrice = values[4], ammoAdd = values[5] });
                }
                var j = Regex.Match(ms[i].Value, "<jinweapon type=\"(\\d+)\" subtype=\"(\\d+)\" curlevel=\"(\\d+)\" maxlevel=\"(\\d+)\"");
                playerMelee[i] = new PlayerWeaponSlot { type = I(j.Groups[1].Value), subtype = I(j.Groups[2].Value), curLevel = I(j.Groups[3].Value), maxLevel = I(j.Groups[4].Value) };
            }
        }

        public static List<PlayerWeaponSlot> PlayerWeapons(int hero) { LoadPlayers(); return playerWeapons[hero]; }
        public static PlayerWeaponSlot PlayerMelee(int hero) { LoadPlayers(); return playerMelee[hero]; }

        static DinoStats ParseDino(string body, bool nativeIntegerFields = false)
        {
            bool real = !nativeIntegerFields;
            return new DinoStats { hp = Tag(body, "HP", real), spd = Tag(body, "SPD", real), exp = Tag(body, "EXP", real), frq = Tag(body, "FRQ", real), atk = Tag(body, "ATK", real), adis = Tag(body, "ADIS", real),
                skin = nativeIntegerFields ? (int)Tag(body,"SKIN",false) : 0, surface = nativeIntegerFields ? (int)Tag(body,"SURFACE",false) : 0 };
        }

        /// <summary>Native root dinosaur.prop: 19 rows in file order. LoadFromXml@0xc58fc uses atoi for every field (including FRQ).</summary>
        public static DinoStats[] Dinos()
        {
            if (dinos != null) return dinos;
            string t = Load("DC2/Image/Conf/dinosaur.prop");
            var list = new List<DinoStats>();
            foreach (Match m in Regex.Matches(t, "<Dinosaur type=\"(\\d+)\">(.*?)</Dinosaur>", RegexOptions.Singleline)) list.Add(ParseDino(m.Groups[2].Value, true));
            dinos = list.ToArray();
            return dinos;
        }

        /// <summary>DEBUG_ONLY legacy-file parser. Native use/path is UNRESOLVED; no literal lifedinosaur.prop in inspected binary.
        /// Do not use these values as gameplay truth. Retained for archaeology only.</summary>
        public static List<LifeInterval> LifeDinos()
        {
            if (life != null) return life;
            string t = Load("DC2/Image/Conf/iPad/lifedinosaur.prop");
            life = new List<LifeInterval>();
            foreach (Match iv in Regex.Matches(t, "<Interval time=\"(\\d+)\">(.*?)</Interval>", RegexOptions.Singleline))
            {
                var li = new LifeInterval { time = F(iv.Groups[1].Value) };
                foreach (Match m in Regex.Matches(iv.Groups[2].Value, "<Dinosaur type=\"(\\d+)\">(.*?)</Dinosaur>", RegexOptions.Singleline))
                { int k = I(m.Groups[1].Value) - 1; if (k >= 0 && k < 4) li.dinos[k] = ParseDino(m.Groups[2].Value); }
                life.Add(li);
            }
            return life;
        }

        public static List<BossDef> Bosses()
        {
            if (bosses != null) return bosses;
            string t = Load("DC2/Image/Conf/boss.xml");
            bosses = new List<BossDef>();
            foreach (Match b in Regex.Matches(t, "<boss hp=\"(\\d+)\" exp=\"(\\d+)\" money=\"(\\d+)\" level=\"(\\d+)\">(.*?)</boss>", RegexOptions.Singleline))
            {
                var d = new BossDef { hp = I(b.Groups[1].Value), exp = I(b.Groups[2].Value), money = I(b.Groups[3].Value), level = I(b.Groups[4].Value) };
                foreach (Match a in Regex.Matches(b.Groups[5].Value, "<ani ([^>]*)>"))
                {
                    var ani = new BossAni();
                    foreach (Match kv in Regex.Matches(a.Groups[1].Value, "(\\w+)\\s*=\\s*\"([^\"]*)\""))
                    {
                        if (kv.Groups[1].Value == "type") ani.type = kv.Groups[2].Value;
                        else ani.attr[kv.Groups[1].Value] = F(kv.Groups[2].Value);
                    }
                    d.anis.Add(ani);
                }
                bosses.Add(d);
            }
            return bosses;
        }
    }
}
