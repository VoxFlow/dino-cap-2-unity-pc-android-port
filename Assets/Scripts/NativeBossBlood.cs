using System;
using UnityEngine;
namespace Dc2Pilot
{
    /// <summary>bossN::Blood 0x1236e0 placement (NATIVE). The component is chosen by the boss kind (456): 1 "t2", 2 "Component_05", 3 "w1" (sub-state 3: hero point instead);
    /// the effect x = anchor.x + lerp(lo, hi, rand()*2^-31), y = 0, a second rand() is drawn and discarded, flip = offset &lt; 0. Executed on the shipped bytes by Dev/Tools/emu_bossblood.py
    /// (oracle Dev/Evidence/bossblood_emu_r117.json, checked by PilotBossBloodVerify).</summary>
    public static class NativeBossBlood
    {
        static float Lerp32(float lo, float hi, float r) { return N.Add32(lo, N.Mul32(r, N.Add32(hi, -lo))); }

        /// <summary>Which sprite the native reads for this boss kind/sub: null = the hero point branch (kind 3 sub 3) or no component.</summary>
        public static string ComponentFor(int kind, int sub)
        {
            switch (kind) { case 1: return "t2"; case 2: return "Component_05"; case 3: return sub == 3 ? null : "w1"; default: return null; }
        }

        /// <summary>Returns the effect x and the flip flag (offset &lt; 0). spriteX / spriteW describe the component (ignored for kind 3 sub 3), heroX the hero position, heroFlip the hero flag (+96 or +223 for playerNewAniClass).</summary>
        public static void Place(int kind, int sub, float spriteX, float spriteW, float heroX, bool heroFlip, float s, Func<float> rand01, out float x, out bool flip)
        {
            float off = 0f, anchor = 0f;
            switch (kind)
            {
                case 1: anchor = spriteX; off = Lerp32(N.Mul32(spriteW, -0.2f), N.Mul32(spriteW, 0.3f), rand01()); break;
                case 2: anchor = spriteX; off = Lerp32(N.Mul32(spriteW, -0.2f), N.Mul32(spriteW, 0.2f), rand01()); break;
                case 3:
                    if (sub == 3)
                    {
                        anchor = N.Add32(heroX, N.Mul32(s, heroFlip ? -100f : 100f));
                        float v71 = N.Mul32(s, 100f);
                        off = Lerp32(N.Mul32(v71, -0.1f), N.Mul32(v71, 0.1f), rand01());
                    }
                    else { anchor = spriteX; off = Lerp32(N.Mul32(spriteW, -0.1f), N.Mul32(spriteW, 0.1f), rand01()); }
                    break;
            }
            if (kind >= 1 && kind <= 3) rand01();   // the discarded second rand()
            x = N.Add32(off, anchor);
            flip = off < 0f;
        }

        /// <summary>bossN::BossShao 0x123106 placement: component (same kind/sub choice, "t2" / "Component_05" / "w1"; kind 3 sub 3 uses the hero point (+-140S, -60S)),
        /// offsets from two rand() draws, position mirrored by the boss flag (+223). Oracle: Dev/Tools/emu_bossshao.py.</summary>
        public static void PlaceShao(int kind, int sub, float spriteX, float spriteY, float spriteW, float spriteH, float heroX, float heroY, bool heroFlip, bool bossLeft, float s, Func<float> rand01, out float x, out float y)
        {
            float px = 0f, py = 0f, xo = 0f, yo = 0f;
            switch (kind)
            {
                case 1:
                    px = spriteX; py = spriteY;
                    xo = Lerp32(N.Mul32(spriteW, -0.2f), N.Mul32(spriteW, 0.3f), rand01()); yo = Lerp32(N.Mul32(spriteH, -0.2f), N.Mul32(spriteH, 0.2f), rand01()); break;
                case 2:
                    px = spriteX; py = spriteY;
                    xo = Lerp32(N.Mul32(spriteW, -0.1f), N.Mul32(spriteW, 0.3f), rand01()); yo = Lerp32(N.Mul32(spriteH, -0.1f), N.Mul32(spriteH, 0.2f), rand01()); break;
                case 3:
                    if (sub == 3)
                    {
                        px = N.Add32(heroX, N.Mul32(s, heroFlip ? -140f : 140f)); py = N.Add32(heroY, N.Mul32(s, -60f));
                        float v116 = N.Mul32(s, 100f), v118 = N.Mul32(s, 20f);
                        xo = Lerp32(N.Mul32(v116, -0.3f), N.Mul32(v116, 0.2f), rand01()); yo = Lerp32(N.Mul32(v118, -0.2f), N.Mul32(v118, 0.2f), rand01());
                    }
                    else
                    {
                        px = spriteX; py = spriteY;
                        xo = Lerp32(N.Mul32(spriteW, -0.2f), N.Mul32(spriteW, 0.2f), rand01()); yo = Lerp32(N.Mul32(spriteH, -0.2f), N.Mul32(spriteH, 0.2f), rand01());
                    }
                    break;
            }
            x = bossLeft ? N.Add32(px, -xo) : N.Add32(xo, px); y = N.Add32(py, yo);
        }
    }
}
