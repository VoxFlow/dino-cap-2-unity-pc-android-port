using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>DEBUG_ONLY diagnostic (DC2-BUGS-001 r119k, user: "check all weapons audio behaviour: loop speed, cadence, cancelling"): every hero, every owned weapon slot,
    /// fire held 2.0 s then released for 1.0 s; logs each sound request with its time, the mean interval between the weapon's attack sounds and what plays after release.
    /// Also one melee press per hero. Not part of the suite.</summary>
    public static class PilotWeaponAudioProbe
    {
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-BUGS-001/weaponaudio-probe");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            var log = new StringBuilder();
            string dir = Path.Combine(Path.GetTempPath(), "dc2_wpnaudio_" + Guid.NewGuid().ToString("N"));
            Camera cam = null; Dc2Game g = null;
            var sel = typeof(GameScene).GetMethod("SelectWeaponSlot", BindingFlags.NonPublic | BindingFlags.Instance);
            try
            {
                cam = Bootstrap.MakeCamera(); g = Dc2Game.Create(dir); g.cam = cam; g.manualStep = true; g.SkipMissionIntro = true;
                for (int hero = 0; hero <= 4; hero++)
                {
                    var ws = Dc2GameData.PlayerWeapons(hero);
                    for (int slot = 0; slot < ws.Count && slot < 6; slot++)
                    {
                        g.Profile.playerType = hero;
                        for (int k = 0; k < 6; k++) { g.Profile.weaponOwned[hero * 6 + k] = 0; g.Profile.weaponAmmo[hero * 6 + k] = 500; }
                        g.LaunchMission = new NativeMapState.Mission { type = 3, count = 20, level = 1, background = 0 }; g.LaunchMission.enemies.Add(1);
                        g.SwitchScene("gameScene"); g.Step(0f); g.Step(1f / 60f);
                        var s = (GameScene)g.Current; s.spawningEnabled = false; s.ignorePlayer = true;
                        bool melee = slot == 5;
                        if (!melee) { sel.Invoke(s, new object[] { slot }); g.StepFor(2.5f); }
                        else g.StepFor(0.5f);
                        g.Audio.RequestedSounds.Clear();
                        var events = new List<KeyValuePair<float, string>>();
                        int seen = 0; float t = 0f;
                        if (melee) s.OnHitDown(); else s.fireDown = true;
                        for (int i = 0; i < 180; i++)
                        {
                            if (i == 120) { if (melee) { } else s.fireDown = false; events.Add(new KeyValuePair<float, string>(t, "<release>")); }
                            g.Step(1f / 60f); t += 1f / 60f;
                            for (; seen < g.Audio.RequestedSounds.Count; seen++) events.Add(new KeyValuePair<float, string>(t, g.Audio.RequestedSounds[seen]));
                        }
                        var attack = events.FindAll(e => e.Value.StartsWith("Attack_") && e.Key <= 2.0f);
                        string iv = "";
                        if (attack.Count > 1) iv = " mean interval " + ((attack[attack.Count - 1].Key - attack[0].Key) / (attack.Count - 1)).ToString("0.000") + " s over " + attack.Count + " shots";
                        var sb = new StringBuilder();
                        foreach (var e in events) sb.Append(e.Key.ToString("0.000")).Append(':').Append(e.Value).Append(' ');
                        log.AppendLine("hero " + hero + " slot " + slot + " (" + (melee ? "melee" : ws[slot].type + "/" + ws[slot].subtype) + ")" + iv);
                        log.AppendLine("   " + sb);
                    }
                }
            }
            catch (Exception e) { log.AppendLine("EXCEPTION " + e); }
            finally
            {
                if (g != null) UnityEngine.Object.DestroyImmediate(g.gameObject);
                if (cam != null) UnityEngine.Object.DestroyImmediate(cam.gameObject);
                if (Directory.Exists(dir)) Directory.Delete(dir, true);
                File.WriteAllText(Path.Combine(output, "verify.txt"), log.ToString()); Debug.Log(log.ToString());
                PilotExit.Exit(0);
            }
        }
    }
}
