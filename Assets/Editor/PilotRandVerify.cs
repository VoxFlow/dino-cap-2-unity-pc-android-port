using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>NativeRand: libc rand() minimal-standard sequence, the RandomRange 0x15de8 float mapping and its rounded upper endpoint, and the loading-tip range (selectScene::OnLoad 0x106484).</summary>
    public static class PilotRandVerify
    {
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-PILOT-001/rand-r1");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            int passed = 0, failed = 0; var log = new StringBuilder();
            Action<string, bool> check = (n, v) => { log.AppendLine((v ? "PASS " : "FAIL ") + n); if (v) passed++; else failed++; };
            try
            {
                // Park-Miller minimal standard, seed 1: 16807, 282475249, 1622650073, 984943658, 1144108930 (Park & Miller 1988 reference values; state 10000 after 10000 draws = 1043618065).
                NativeRand.Seed(1);
                int[] want = { 16807, 282475249, 1622650073, 984943658, 1144108930 };
                bool seqOk = true; foreach (var w in want) seqOk &= NativeRand.Raw() == w;
                check("libc rand() minimal-standard sequence from seed 1", seqOk);
                NativeRand.Seed(1); int last = 0; for (int i = 0; i < 10000; i++) last = NativeRand.Raw();
                check("10000th value from seed 1 is 1043618065 (Park-Miller check value)", last == 1043618065);
                NativeRand.Seed(0); check("zero state is replaced by 123459876 (FreeBSD do_rand): first value 520932930", NativeRand.Raw() == 520932930);
                check("RandomRange maps raw 0 to lo", NativeRandom.RangeFromRaw(3, 10, 0) == 3);
                check("RandomRange float rounding lets the largest words reach hi (shipped endpoint quirk)", NativeRandom.RangeFromRaw(0, 10, 0x7fffffff) == 10 && NativeRandom.RangeFromRaw(0, 10, 0x7fffff00) == 9);
                check("Unit uses float(raw) * 2^-31", Mathf.Approximately(NativeRand.Unit(0x40000000), 0.5f));
                int n = SelectScene.LoadingTips.Length, hitsLast = 0; NativeRand.Seed(7);
                for (int i = 0; i < 20000; i++) if (NativeRand.Range(0, n - 1) == n - 1) hitsLast++;
                check("loading tip RandomRange(0, size-1) practically never picks the last tip (" + hitsLast + "/20000)", hitsLast == 0);
                // GetPlayer4Atk 0x193b8 (expected bit patterns from an independent Python struct model of the ARM float/double path)
                Func<string, float> h = x => BitConverter.ToSingle(new byte[] { Convert.ToByte(x.Substring(0, 2), 16), Convert.ToByte(x.Substring(2, 2), 16), Convert.ToByte(x.Substring(4, 2), 16), Convert.ToByte(x.Substring(6, 2), 16) }, 0);
                object[][] p4 = { new object[] { 37.3f, 0, "33339541" }, new object[] { 37.3f, 3, "33339541" }, new object[] { 37.3f, 4, "3d0ab341" }, new object[] { 37.3f, 12, "47e1d041" },
                                  new object[] { 37.3f, 18, "52b8ee41" }, new object[] { 37.3f, 20, "ae470642" }, new object[] { 37.3f, 26, "33331542" }, new object[] { 37.3f, 29, "3d0a3342" }, new object[] { 123.45f, 7, "d7239442" } };
                bool p4ok = true; foreach (var c in p4) p4ok &= NativeWeaponDamage.Player4Atk((float)c[0], (int)c[1]) == h((string)c[2]);
                check("GetPlayer4Atk level bands 0.5 / 0.6 / 0.7 / 0.8 / 0.9 / 1.0 / 1.2 bit-exact", p4ok);
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
