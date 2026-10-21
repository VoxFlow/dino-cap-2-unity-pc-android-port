using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml.Linq;
using UnityEngine;

namespace Dc2Pilot
{
    /// <summary>Triniti2D::RandomRange ARM0x15de8. Host generator is a PLATFORM_ADAPTER;
    /// the signed float32 conversions and endpoint rounding are the recovered native contract.</summary>
    public static class NativeRandom
    {
        // One process-wide libc rand() stream (NativeRand); the old Unity-based source is SUPERSEDED.
        public static int HostRaw31() { return NativeRand.Raw(); }
        public static int RangeFromRaw(int lo, int hi, int raw)
        {
            float unit = (float)raw * N.F(805306368);
            float width = (float)unchecked(hi - lo);
            return unchecked(lo + (int)(unit * width));
        }
    }

    /// <summary>Shipped Map.xml/point.xml fields consumed by missionClass/mapPosClass.
    /// Source: native_map_trace.r5*.json, native_map_loader_save_arm.r5.json and raw manifests.</summary>
    public sealed class NativeMapState
    {
        public struct Point { public float x, y; public int bg; }
        public sealed class Mission
        {
            public int type, level, background;
            public int info;                      // missionInfo/info value (record +24): egg missions deliver this many eggs
            public float count;
            public string rawType;
            public System.Collections.Generic.List<int> enemies = new System.Collections.Generic.List<int>();   // Map.xml enimy/dianocap (g_map_info +36)
        }
        public struct Choice { public int mapIndex; public Point point; public bool boss; }
        public sealed class BossState { public bool active; public Point point; public int seed, level; }

        public readonly List<Mission> missions = new List<Mission>();
        public readonly List<Point> positions = new List<Point>();
        public readonly List<Choice> choices = new List<Choice>();
        public BossState boss;
        public int bossMapCount;
        public Func<int> rawRandom = NativeRandom.HostRaw31;
        public int Range(int lo, int hi) { return NativeRandom.RangeFromRaw(lo, hi, rawRandom()); }
        static float Float(XAttribute a) { return (float)double.Parse(a.Value, CultureInfo.InvariantCulture); }
        static int Int(XAttribute a) { return int.Parse(a.Value, CultureInfo.InvariantCulture); }

        public NativeMapState()
        {
            var typeIds = new Dictionary<string, int> { {"run",1}, {"time",2}, {"kill",3}, {"specialkill",4}, {"egg",5}, {"timerun",6}, {"arena",7}, {"boss",8} };
            foreach (var e in XDocument.Parse(Resources.Load<TextAsset>("DC2/Image/Conf/Map.xml").text).Root.Elements("mapPoint"))
            {
                string t = e.Attribute("type").Value;
                int id = typeIds.TryGetValue(t, out int found) ? found : 0;
                missions.Add(new Mission { type = id, rawType = t, count = Float(e.Attribute("num")),
                    level = Int(e.Element("missionInfo").Element("level").Attribute("value")), background = Int(e.Attribute("bg")),
                    info = e.Element("missionInfo").Element("info") != null && int.TryParse(e.Element("missionInfo").Element("info").Attribute("value").Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out int infoValue) ? infoValue : 0 });
                var en = e.Element("enimy");
                if (en != null) foreach (var k in en.Elements("dianocap")) missions[missions.Count - 1].enemies.Add(int.Parse(k.Value.Trim(), CultureInfo.InvariantCulture));
                if (id == 8) bossMapCount++;
            }
            foreach (var e in XDocument.Parse(Resources.Load<TextAsset>("DC2/Image/Conf/point.xml").text).Root.Elements("pos"))
                positions.Add(new Point {x=Float(e.Attribute("posx")), y=Float(e.Attribute("posy")), bg=Int(e.Attribute("bg"))});
        }

        string BossPath(string dir) { return Path.Combine(dir, "dinocap2_boss_point.sav"); }
        public void ResetBoss(int mission)
        { boss = new BossState {active=false, point=new Point(), seed=unchecked(Range(4,6)+mission), level=-1}; }
        /// <summary>lifeModeScene::missionComplete 0xe18fa..0xe1946 (r119d, user item 6): after a completed boss map (mapClass type 8) the boss point goes back to the
        /// position pool (push_back g_boss_point_pos), is cleared (mapPosProp::Init), g_boss_point_key = 0 (inactive), g_boss_seed = RandomRange(4,6) + g_mission
        /// (before ++g_mission) and BossPointSaveFile. The boss therefore reappears only when g_mission reaches the new seed (4..6 more completed missions);
        /// a failed or quit mission never runs missionComplete. The port never cleared it, so the boss point stayed on the map.</summary>
        public void BossCompleted(string dir, int mission, int seedDraw)
        {
            if (boss == null) ReadBoss(dir, mission);
            positions.Add(boss.point); boss.point = new Point(); boss.active = false;
            boss.seed = unchecked(seedDraw + mission);
            SaveBoss(dir);
        }
        public void SaveBoss(string dir)
        {
            Directory.CreateDirectory(dir);
            using (var w = new BinaryWriter(File.Create(BossPath(dir))))
            { w.Write(boss.active); w.Write(boss.point.bg); w.Write(boss.point.x); w.Write(boss.point.y); w.Write(boss.seed); w.Write(boss.level); }
        }
        public void ReadBoss(string dir, int mission)
        {
            string path = BossPath(dir);
            if (!File.Exists(path)) { ResetBoss(mission); SaveBoss(dir); return; }
            using (var r = new BinaryReader(File.OpenRead(path)))
            {
                boss = new BossState { active=r.ReadByte()!=0, point=new Point {bg=r.ReadInt32(),x=r.ReadSingle(),y=r.ReadSingle()} };
                boss.seed = r.BaseStream.Length > 16 ? r.ReadInt32() : Range(4,6);
                boss.level = r.BaseStream.Length > 20 ? r.ReadInt32() : -1;
            }
        }

        static bool Allowed(Mission m, int level) { return !(m.level>3 && level<=5 || m.level<=2 && level>6); }
        int ChooseMission(int lo, int hi, int level, int exclude0=-1, int exclude1=-1)
        {
            int m = Range(lo,hi);
            // Native rejection loop; no substitution/clamping changes the shipped filter.
            while (m==exclude0 || m==exclude1 || !Allowed(missions[m],level)) m=Range(lo,hi);
            return m;
        }
        Point RemoveAtSwapBack(int i)
        { Point p=positions[i]; positions[i]=positions[positions.Count-1]; positions.RemoveAt(positions.Count-1); return p; }

        /// <summary>showMapPoint0x10a69a, getBossPoint0xf4992, showBossPoint0x10a2e8.
        /// Scope: fresh menu generation (isskip0); retry/skip reuse remains a separate open row.</summary>
        public void Generate(string dir, int mission, int level)
        {
            choices.Clear(); ReadBoss(dir,mission);
            if (boss.active)
            {
                int found=-1;
                for (int i=0;i<positions.Count;i++) if (positions[i].x==boss.point.x && positions[i].y==boss.point.y) found=i;
                if (found!=-1) RemoveAtSwapBack(found);
            }
            if (mission==boss.seed)
            {
                if (boss.active && mission>1) { positions.Add(boss.point); boss.point=new Point(); boss.active=false; }
                if (!boss.active && mission>1)
                {
                    boss.seed=unchecked(Range(4,6)+mission); boss.active=true;
                    boss.point=RemoveAtSwapBack(Range(0,positions.Count-1));
                    int m=ChooseMission(missions.Count-bossMapCount,missions.Count-1,level);
                    boss.level=missions[m].level; SaveBoss(dir);
                }
            }
            if (boss.active)
            {
                int m=ChooseMission(missions.Count-bossMapCount,missions.Count-1,level);
                choices.Add(new Choice {mapIndex=m,point=boss.point,boss=true});
            }
            int normalCount=missions.Count-bossMapCount;
            int upper=normalCount-1;
            if (level<=8) upper=normalCount-11;
            else if (unchecked((uint)(level-9))<=9) upper=normalCount-6;
            int count=Range(1,3);
            int[] selectedMaps={0,0,0},selectedPositions={0,0,0};
            for (int i=0;i<count;i++)
            {
                // Native consumes initial map and position draws before rejecting the map.
                int m=Range(0,upper),p=Range(0,positions.Count-1);
                while ((i>0 && m==selectedMaps[0]) || (i>1 && m==selectedMaps[1]) || !Allowed(missions[m],level)) m=Range(0,upper);
                while ((i>0 && p==selectedPositions[0]) || (i>1 && p==selectedPositions[1])) p=Range(0,positions.Count-1);
                selectedMaps[i]=m; selectedPositions[i]=p;
                missions[m].background=Range(0,10000)%6;
                choices.Add(new Choice {mapIndex=m,point=positions[p],boss=false});
            }
        }
    }
}
