using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>DC2-ENGINE-001 L10: native dinocap2hd.sav layout (DinoCap::SaveFile 0x58b9c / ReadFile 0x1995c). Offsets are the writer's; tolerances are the reader's.</summary>
    public static class PilotSaveVerify
    {
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-ENGINE-001/save-r1");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            int passed = 0, failed = 0; var log = new StringBuilder();
            Action<string, bool> check = (n, v) => { log.AppendLine((v ? "PASS " : "FAIL ") + n); if (v) passed++; else failed++; };
            string dir = Path.Combine(Path.GetTempPath(), "dc2_save_" + Guid.NewGuid().ToString("N"));
            try
            {
                var p = new Profile();
                p.Save(dir);   // ensures the lazy fields
                p.cash = 123456; p.mission = 7; p.level = new[] { 3, 1, -1, 2, 4 }; p.exp = new[] { 10, 20, 30, 40, 50 };
                p.playerType = 2; p.sound = 0; p.fx = 1; p.maxBullet = 999; p.jigsawSeed = 9; p.jigsaw1Count = 3; p.jigsaw5Count = 5;
                p.selectedWeaponSlots = new[] { 1, 2, 3, 4, 5 }; p.jigsawActive[0] = 1; p.jigsawActive[70] = 1; p.jigsawGet[63] = 1; p.jigsawGet[79] = 1;
                p.weaponOwned[0] = 1; p.weaponLevels[1] = 7; p.weaponAmmo[2] = 333; p.weaponAmmo[4 * 6 + 5] = 77;
                p.nativeMaxMission = 11; p.nativeMaxMissionRand = 12; p.nativePropPlayer = 55.5f; p.native240480 = 13; p.nativeDifficult = 2; p.nativeCurrentWeapon = 3; p.nativeP4Kill = 14; p.nativeP1Kill = 15;
                p.nativeHired[1] = 1;
                byte[] d = NativeSaveFile.Write(p);
                Func<int, int> I = at => BitConverter.ToInt32(d, at);
                int rec = 8 + 18 * 4;   // 80 bytes per hero record
                check("header: max_mission, rand, mission, cash at 0/4/8/12", I(0) == 11 && I(4) == 12 && I(8) == 7 && I(12) == 123456);
                check("g_prop_Player float bits at 16, dword_240480 at 20", BitConverter.ToSingle(d, 16) == 55.5f && I(20) == 13);
                check("level[0..3] at 24..40, exp[0..3] at 40..56", I(24) == 3 && I(36) == 2 && I(40) == 10 && I(52) == 40);
                check("hero record 0 at 56: hp, df, owned x6, levels x6, ammo x6 (+84, +108, +96 order)", I(56) == p.nativeHeroHp[0] && I(64) == 1 && I(64 + 24 + 4) == 7 && I(64 + 48 + 8) == 333);
                int after = 56 + 4 * rec;
                check("hired flag bytes follow the four records", d[after] == 0 && d[after + 1] == 1);
                int t = after + 5;
                check("g_difficult, sound, fx, player type, current weapon, kills, max bullets", I(t) == 2 && d[t + 4] == 0 && d[t + 5] == 1 && I(t + 6) == 2 && I(t + 10) == 3 && I(t + 14) == 14 && I(t + 18) == 15 && I(t + 22) == 999);
                int s = t + 26;
                check("selected slots[0..3], then jigsaw_active[0..63], jigsaw_get[0..63]", I(s) == 1 && I(s + 12) == 4 && d[s + 16] == 1 && d[s + 16 + 64 + 63] == 1);
                int j = s + 16 + 128;
                check("jigsawSeed and jigsaw1..5 counts", I(j) == 9 && I(j + 4) == 3 && I(j + 20) == 5);
                int r5 = j + 24;
                check("hero record 5 after the counts, then level[4], exp[4]", I(r5 + 8 + 4 * 17) == 77 && I(r5 + rec) == 4 && I(r5 + rec + 4) == 50);
                int tail = r5 + rec + 8;
                check("jigsaw_active[64..79], jigsaw_get[64..79], selected slot[4]; total length", d[tail + 6] == 1 && d[tail + 16 + 15] == 1 && I(tail + 32) == 5 && d.Length == tail + 36);

                Directory.CreateDirectory(dir);
                File.WriteAllBytes(Path.Combine(dir, NativeSaveFile.FileName), d);
                var q = Profile.Load(dir);
                check("round trip restores every written field", q.cash == 123456 && q.mission == 7 && q.level[3] == 2 && q.level[4] == 4 && q.exp[4] == 50 && q.playerType == 2 && q.sound == 0 && q.maxBullet == 999
                      && q.selectedWeaponSlots[4] == 5 && q.jigsawActive[70] == 1 && q.jigsawGet[79] == 1 && q.weaponAmmo[29] == 77 && q.nativePropPlayer == 55.5f && q.nativeHired[1] == 1 && q.nativeDifficult == 2);

                // reader tolerances on a file that stops right after g_fx
                var shortFile = new byte[t + 6]; Array.Copy(d, shortFile, shortFile.Length);
                BitConverter.GetBytes(9).CopyTo(shortFile, t);   // g_difficult out of 1..3
                File.WriteAllBytes(Path.Combine(dir, NativeSaveFile.FileName), shortFile);
                var r = Profile.Load(dir);
                check("short file: g_difficult outside 1..3 -> 1, player type 0, max bullets 500, selected slots 0", r.nativeDifficult == 1 && r.playerType == 0 && r.maxBullet == 500 && r.selectedWeaponSlots[0] == 0 && r.cash == 123456);
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
