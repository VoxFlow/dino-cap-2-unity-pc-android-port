using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>bossN::Blood 0x1236e0 placement (NativeBossBlood.cs) against the ARM-executed oracle Assets/Editor/bossblood_oracle.json (Dev/Tools/emu_bossblood.py).
    /// The oracle gives x (SetPosition), the flip byte and the dx/qx animation name for every kind / sub / rand combination; y is always 0.</summary>
    public static class PilotBossBloodVerify
    {
        [Serializable] class Case { public int kind, sub, a2, hero_flip, iphone, flip, rand_draws, bleft, positions; public float[] sprite, hero, pos; public int[] rands; public string[] anim; }
        [Serializable] class Oracle { public Case[] cases; }

        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-PILOT-001/bossblood-r1");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            int passed = 0, failed = 0; var log = new StringBuilder();
            Action<string, bool> check = (n, v) => { log.AppendLine((v ? "PASS " : "FAIL ") + n); if (v) passed++; else failed++; };
            try
            {
                var oracle = JsonUtility.FromJson<Oracle>(File.ReadAllText(Path.Combine(Application.dataPath, "Editor/bossblood_oracle.json")));
                log.AppendLine("INFO oracle cases " + oracle.cases.Length);
                int bad = 0, drawBad = 0, n = 0;
                foreach (var c in oracle.cases)
                {
                    int ri = 0, draws = 0; var rl = c.rands;
                    Func<float> unit = () => { int r = rl[ri % rl.Length]; ri++; draws++; return N.Mul32((float)r, N.F(805306368)); };
                    float s = c.iphone != 0 ? 1f : 2f;
                    float sx = 0f, sw = 0f;
                    if (NativeBossBlood.ComponentFor(c.kind, c.sub) != null) { sx = c.sprite[0]; sw = c.sprite[2]; }
                    float x; bool flip;
                    NativeBossBlood.Place(c.kind, c.sub, sx, sw, c.hero[0], c.hero_flip != 0, s, unit, out x, out flip);
                    float ex = c.pos != null && c.pos.Length == 2 ? c.pos[0] : 0f;
                    n++;
                    if (Mathf.Abs(x - ex) > 1e-3f + 1e-5f * Mathf.Abs(ex) || (flip ? 1 : 0) != c.flip) { bad++; if (bad <= 5) log.AppendLine("MISMATCH kind " + c.kind + " sub " + c.sub + " rands " + string.Join(",", c.rands) + " got " + x + "/" + flip + " expected " + ex + "/" + c.flip); }
                    if (draws != c.rand_draws) drawBad++;
                }
                check("bossN::Blood placement x and flip match the ARM-executed oracle (" + n + " cases)", bad == 0);
                check("bossN::Blood draws the same number of rand() values as the original", drawBad == 0);

                var shao = JsonUtility.FromJson<Oracle>(File.ReadAllText(Path.Combine(Application.dataPath, "Editor/bossshao_oracle.json")));
                int sBad = 0, sDraw = 0, sn = 0;
                foreach (var c in shao.cases)
                {
                    int ri = 0, draws = 0; var rl = c.rands;
                    Func<float> unit = () => { int r = rl[ri % rl.Length]; ri++; draws++; return N.Mul32((float)r, N.F(805306368)); };
                    float s = c.iphone != 0 ? 1f : 2f, x, y;
                    bool comp = NativeBossBlood.ComponentFor(c.kind, c.sub) != null;
                    NativeBossBlood.PlaceShao(c.kind, c.sub, comp ? c.sprite[0] : 0f, comp ? c.sprite[1] : 0f, comp ? c.sprite[2] : 0f, comp ? c.sprite[3] : 0f, c.hero[0], c.hero[1], c.hero_flip != 0, c.bleft != 0, s, unit, out x, out y);
                    sn++;
                    if (Mathf.Abs(x - c.pos[0]) > 1e-3f + 1e-5f * Mathf.Abs(c.pos[0]) || Mathf.Abs(y - c.pos[1]) > 1e-3f + 1e-5f * Mathf.Abs(c.pos[1])) { sBad++; if (sBad <= 5) log.AppendLine("MISMATCH shao kind " + c.kind + " sub " + c.sub + " got " + x + "," + y + " expected " + c.pos[0] + "," + c.pos[1]); }
                    if (draws != c.rand_draws) sDraw++;
                }
                check("bossN::BossShao placement matches the ARM-executed oracle (" + sn + " cases)", sBad == 0);
                check("bossN::BossShao draws the same number of rand() values as the original", sDraw == 0);
            }
            catch (Exception e) { failed++; log.AppendLine("EXCEPTION " + e); }
            finally
            {
                log.AppendLine("SUMMARY passes=" + passed + " fails=" + failed);
                File.WriteAllText(Path.Combine(output, "verify.txt"), log.ToString());
                EditorApplicationExit(failed == 0 ? 0 : 1);
            }
        }

        static void EditorApplicationExit(int code) { Dc2Pilot.EditorTools.PilotExit.Exit(code); }
    }
}
