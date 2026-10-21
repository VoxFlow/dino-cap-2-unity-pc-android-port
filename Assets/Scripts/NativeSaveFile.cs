using System;
using System.Collections.Generic;
using System.IO;

namespace Dc2Pilot
{
    /// <summary>DinoCap::SaveFile 0x58b9c / ReadFile 0x1995c: the native "dinocap2hd.sav" byte layout (little-endian int32 unless noted; DC2-ENGINE-001 L10, r118z).
    /// Writer order:
    ///   0 g_max_mission, 4 g_max_mission_rand, 8 g_mission, 12 cash (dword_24047C), 16 g_prop_Player (float bits), 20 dword_240480,
    ///   24..40 level[0..3] (dword_240494), 40..56 exp[0..3] (dword_240488),
    ///   4 hero records (g_player_vector, stride 120): +8 hp, +12 df, vector +84 (owned x6), vector +108 (levels x6), vector +96 (ammo x6),
    ///   one byte per hired flag (vector&lt;bool&gt; unk_2404F8), g_difficult, byte g_sound, byte g_fx, g_player_type, dword_2404E8, g_p4_kill_dino, g_p1_kill_dino,
    ///   g_max_bullet_count, selected weapon slot[0..3] (dword_2404A0), jigsaw_active[0..63] bytes, jigsaw_get[0..63] bytes, jigsawSeed, jigsaw1..5 counts,
    ///   hero record 5 (same shape), level[4], exp[4], jigsaw_active[64..79], jigsaw_get[64..79], selected slot[4].
    /// Reader tolerances (native): record +8/+12 read as 0 when beyond the file; g_difficult outside 1..3 -> 1; g_player_type, dword_2404E8, kills read as 0 and
    /// g_max_bullet_count as 500 when beyond the file; selected slots 0 when beyond the file. The rest of the reader (0x1a2e0 chunk) mirrors the writer.</summary>
    public static class NativeSaveFile
    {
        public const string FileName = "dinocap2hd.sav";
        const int Heroes4 = 4, Slots = 6;

        public static byte[] Write(Profile p)
        {
            var b = new List<byte>(1024);
            Action<int> I = v => { b.Add((byte)v); b.Add((byte)(v >> 8)); b.Add((byte)(v >> 16)); b.Add((byte)(v >> 24)); };
            Action<int> B = v => b.Add((byte)v);
            I(p.nativeMaxMission); I(p.nativeMaxMissionRand); I(p.mission); I(p.cash); I(BitConverter.ToInt32(BitConverter.GetBytes(p.nativePropPlayer), 0)); I(p.native240480);
            for (int i = 0; i < Heroes4; i++) I(p.level[i]);
            for (int i = 0; i < Heroes4; i++) I(p.exp[i]);
            for (int h = 0; h < Heroes4; h++) WriteRecord(p, h, I);
            for (int i = 0; i < p.nativeHired.Length; i++) B(p.nativeHired[i] != 0 ? 1 : 0);
            I(p.nativeDifficult); B(p.sound); B(p.fx); I(p.playerType); I(p.nativeCurrentWeapon); I(p.nativeP4Kill); I(p.nativeP1Kill); I(p.maxBullet);
            for (int i = 0; i < Heroes4; i++) I(p.selectedWeaponSlots[i]);
            for (int i = 0; i < 64; i++) B(p.jigsawActive[i]);
            for (int i = 0; i < 64; i++) B(p.jigsawGet[i]);
            I(p.jigsawSeed); I(p.jigsaw1Count); I(p.jigsaw2Count); I(p.jigsaw3Count); I(p.jigsaw4Count); I(p.jigsaw5Count);
            WriteRecord(p, 4, I);   // LIKELY g_player_vector[4] (copy-ctor source dropped by Hex-Rays at 0x5918x)
            I(p.level[4]); I(p.exp[4]);
            for (int i = 64; i < 80; i++) B(p.jigsawActive[i]);
            for (int i = 64; i < 80; i++) B(p.jigsawGet[i]);
            I(p.selectedWeaponSlots[4]);
            return b.ToArray();
        }

        static void WriteRecord(Profile p, int h, Action<int> I)
        {
            I(p.nativeHeroHp[h]); I(p.nativeHeroDf[h]);
            for (int s = 0; s < Slots; s++) I(p.weaponOwned[h * Slots + s]);    // +84
            for (int s = 0; s < Slots; s++) I(p.weaponLevels[h * Slots + s]);   // +108
            for (int s = 0; s < Slots; s++) I(p.weaponAmmo[h * Slots + s]);     // +96
        }

        public static void Read(Profile p, byte[] d)
        {
            int at = 0;
            Func<int, bool> fits = n => at + n <= d.Length;
            Func<int> I = () => { int v = fits(4) ? BitConverter.ToInt32(d, at) : 0; at += 4; return v; };
            Func<int> B = () => { int v = fits(1) ? d[at] : 0; at += 1; return v; };
            p.nativeMaxMission = I(); p.nativeMaxMissionRand = I(); p.mission = I(); p.cash = I(); p.nativePropPlayer = BitConverter.ToSingle(BitConverter.GetBytes(I()), 0); p.native240480 = I();
            for (int i = 0; i < Heroes4; i++) p.level[i] = I();
            for (int i = 0; i < Heroes4; i++) p.exp[i] = I();
            for (int h = 0; h < Heroes4; h++) ReadRecord(p, h, I);
            for (int i = 0; i < p.nativeHired.Length; i++) p.nativeHired[i] = B();
            p.nativeDifficult = I(); if (unchecked((uint)(p.nativeDifficult - 1)) > 2u) p.nativeDifficult = 1;
            p.sound = B(); p.fx = B();
            p.playerType = I(); p.nativeCurrentWeapon = I(); p.nativeP4Kill = I(); p.nativeP1Kill = I();
            int mb = at; p.maxBullet = I(); if (mb + 4 > d.Length) p.maxBullet = 500;
            for (int i = 0; i < Heroes4; i++) p.selectedWeaponSlots[i] = I();
            for (int i = 0; i < 64; i++) p.jigsawActive[i] = B();
            for (int i = 0; i < 64; i++) p.jigsawGet[i] = B();
            p.jigsawSeed = I(); p.jigsaw1Count = I(); p.jigsaw2Count = I(); p.jigsaw3Count = I(); p.jigsaw4Count = I(); p.jigsaw5Count = I();
            ReadRecord(p, 4, I);
            p.level[4] = I(); p.exp[4] = I();
            for (int i = 64; i < 80; i++) p.jigsawActive[i] = B();
            for (int i = 64; i < 80; i++) p.jigsawGet[i] = B();
            p.selectedWeaponSlots[4] = I();
        }

        static void ReadRecord(Profile p, int h, Func<int> I)
        {
            p.nativeHeroHp[h] = I(); p.nativeHeroDf[h] = I();
            for (int s = 0; s < Slots; s++) p.weaponOwned[h * Slots + s] = I();
            for (int s = 0; s < Slots; s++) p.weaponLevels[h * Slots + s] = I();
            for (int s = 0; s < Slots; s++) p.weaponAmmo[h * Slots + s] = I();
        }
    }
}
