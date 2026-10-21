using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>Gameplay data tables parse to the shipped counts/values.</summary>
    public static class PilotDataVerify
    {
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-PILOT-001/data-r1");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            int passed = 0, failed = 0; var log = new StringBuilder();
            Action<string, bool> check = (n, v) => { log.AppendLine((v ? "PASS " : "FAIL ") + n); if (v) passed++; else failed++; };
            try
            {
                var w = Dc2GameData.Weapons();
                check("7 weapon types", w.Count == 7);
                int[] counts = { 5, 5, 4, 5, 3, 6, 7 }; bool ok = true;
                for (int i = 0; i < 7; i++) ok &= w[i].Count == counts[i];
                check("native root sub-weapon counts 5,5,4,5,3,6,7", ok);
                check("native root A1 exp300 atk2..5 dis480 group0 crit4/base3", w[0][0].name == "A1" && w[0][0].exp == 300 && w[0][0].atkMin == 2 && w[0][0].atkMax == 5 && w[0][0].atkDis == 480 && !w[0][0].group && w[0][0].crit == 4 && w[0][0].baseAtk == 3);
                check("native root E2 exp4800 atk32..40 dis200 group", w[4][1].name == "E2" && w[4][1].exp == 4800 && w[4][1].atkMin == 32 && w[4][1].atkMax == 40 && w[4][1].atkDis == 200 && w[4][1].group);
                check("native root G7 hammer dis140 atk9..14", w[6][6].name == "G7" && w[6][6].atkDis == 140 && w[6][6].atkMin == 9 && w[6][6].atkMax == 14);
                var d = Dc2GameData.Dinos();
                check("native root19 dinosaurs, first hp8/spd80/atk11/adis100", d.Length == 19 && d[0].hp == 8 && d[0].spd == 80 && d[0].atk == 11 && d[0].adis == 100);
                check("native atoi truncates second-row FRQ3.2 to3; raw skin/surface retained", d[1].frq == 3 && d[0].skin == 1 && d[0].surface == 10);
                var l = Dc2GameData.LifeDinos();
                check("DEBUG_ONLY legacy file has6 intervals; native usage UNRESOLVED", l.Count == 6 && l[0].time == 0 && l[1].time == 30 && l[2].time == 60 && l[3].time == 190 && l[4].time == 150 && l[5].time == 200);
                check("DEBUG_ONLY legacy raw interval0; not a gameplay oracle", l[0].dinos[0].hp == 13 && l[0].dinos[0].spd == 50 && Mathf.Approximately(l[0].dinos[0].frq, 1.3f));
                var b = Dc2GameData.Bosses();
                check("5 bosses, boss1 hp40 money500 one jiao ani speed110", b.Count == 5 && b[0].hp == 40 && b[0].money == 500 && b[0].anis.Count == 1 && b[0].anis[0].type == "jiao" && b[0].anis[0].attr["speed"] == 110f);
                check("boss5 has hou, wei, jiao", b[4].anis.Count == 3 && b[4].anis[0].type == "hou" && b[4].anis[1].type == "wei" && b[4].anis[2].type == "jiao");
                var p0 = Dc2GameData.PlayerWeapons(0);
                check("native unfiltered sibling loop includes melee as slot5", p0.Count == 6 && p0[0].type == 0 && p0[1].type == 2 && p0[2].subtype == 1 && p0[4].type == 5 && p0[5].type == 6 && p0[5].subtype == 0);
                check("missing melee ammo attributes retain previous record values", p0[5].ammoPrice == 400 && p0[5].ammoAdd == 40 && Dc2GameData.PlayerWeapons(4)[5].ammoPrice == 900 && Dc2GameData.PlayerWeapons(4)[5].ammoAdd == 40);
                check("hero4 melee subtype 6, hero1 first weapon sub 1 ammoPrice 200 add 30", Dc2GameData.PlayerMelee(4).subtype == 6 && Dc2GameData.PlayerWeapons(1)[0].ammoPrice == 200 && Dc2GameData.PlayerWeapons(1)[0].ammoAdd == 30);
            }
            catch (Exception e) { failed++; log.AppendLine("EXCEPTION " + e); }
            finally
            {
                log.AppendLine("SUMMARY passes=" + passed + " fails=" + failed);
                File.WriteAllText(Path.Combine(output, "verify.txt"), log.ToString()); Debug.Log(log.ToString());
                PilotExit.Exit(failed == 0 ? 0 : 1);
            }
        }
    }
}
