using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>Random weather (GameWeather.cs): selection table, overcast overlay, rain emitters (p2d engine), lightning sequence, wind effects.</summary>
    public static class PilotWeatherVerify
    {
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-PILOT-001/weather-r1");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            string dir = Path.Combine(Path.GetTempPath(), "dc2_weather_verify_" + Guid.NewGuid().ToString("N"));
            int passed = 0, failed = 0; var log = new StringBuilder();
            Action<string, bool> check = (n, v) => { log.AppendLine((v ? "PASS " : "FAIL ") + n); if (v) passed++; else failed++; };
            Dc2Game game = null; Camera cam = null; RenderTexture rt = null; Texture2D img = null;
            try
            {
                cam = Bootstrap.MakeCamera(); game = Dc2Game.Create(dir); game.manualStep = true; game.SkipMissionIntro = true; game.cam = cam;
                rt = new RenderTexture(1024, 768, 24); img = new Texture2D(1024, 768, TextureFormat.RGB24, false);
                Action<string> shot = name => { cam.targetTexture = rt; Bootstrap.RenderAll(cam); RenderTexture.active = rt; img.ReadPixels(new Rect(0, 0, 1024, 768), 0, 0); img.Apply(); File.WriteAllBytes(Path.Combine(output, name), img.EncodeToPNG()); cam.targetTexture = null; RenderTexture.active = null; };
                Func<int, int, GameScene> load = (roll, bg) =>
                {
                    GameScene.DebugWeatherRoll = () => roll;
                    game.LaunchMission = new NativeMapState.Mission { type = 3, count = 200, level = 1, background = bg };
                    game.Profile.playerType = 0;
                    game.SwitchScene("gameScene"); game.StepFor(0.05f);
                    var s = (GameScene)game.Current; s.spawningEnabled = false; s.ignorePlayer = true;
                    return s;
                };
                // ---- selection: v = RandomRange(0,6) - 1 ----
                int[] expect = { GameScene.WeatherClear, GameScene.WeatherOvercast, GameScene.WeatherRain, GameScene.WeatherStorm, GameScene.WeatherClear, GameScene.WeatherClear };
                for (int r = 0; r < 6; r++)
                {
                    var s = load(r, 0);
                    check("roll " + r + " -> kind " + expect[r] + " (bg 0)", s.WeatherKind == expect[r]);
                }
                var wind = load(4, 2); check("roll 4 on background 2 -> wind", wind.WeatherKind == GameScene.WeatherWind && wind.YeziTimerForTest > 0f);
                wind = load(4, 3); check("roll 4 on background 3 -> wind", wind.WeatherKind == GameScene.WeatherWind);
                wind = load(4, 1); check("roll 4 on background 1 -> clear", wind.WeatherKind == GameScene.WeatherClear);
                // ---- overcast ----
                var oc = load(1, 0); game.StepFor(0.1f);
                check("overcast: yintian overlay, no rain", oc.HasOvercast && !oc.HasRain);
                shot("weather_overcast.png");
                // ---- rain ----
                var rain = load(2, 0); game.StepFor(0.1f);
                check("rain: overlay + 3 emitters", rain.HasOvercast && rain.HasRain);
                game.StepFor(1.5f);
                check("rain: particles alive (70/s * 0.68 s lifetime ~ 48 per emitter set)", rain.RainAlive > 40);
                log.AppendLine("INFO rain alive " + rain.RainAlive);
                shot("weather_rain.png");
                var cl = load(0, 0); game.StepFor(0.2f);
                check("clear: nothing", !cl.HasOvercast && !cl.HasRain);
                shot("weather_clear.png");
                // ---- storm: lightning sequence ----
                var storm = load(3, 0);
                float t0 = storm.ShanDianTimerForTest;
                check("storm: first lightning timer RandomRange(4,7) seconds (minus the load step)", t0 >= 3.5f && t0 <= 6f);   // r118: the spawn-timer rand draws moved to StartGame_N, so the stream position (and a value of exactly 4) differs
                int sounds = game.Audio.RequestedSounds.FindAll(x => x == "linghtingAudio").Count;
                game.StepFor(t0 + 0.02f);
                check("lightning bolt effect spawned and the thunder sound requested", storm.LightningStrikes == 1 && game.Audio.RequestedSounds.FindAll(x => x == "linghtingAudio").Count == sounds + 1);
                shot("weather_lightning_bolt.png");
                game.StepFor(0.1f);
                check("0.08 s later the white flash mask exists", storm.LightningMaskVisible);
                shot("weather_lightning_flash.png");
                game.StepFor(0.25f);
                check("after maskWhite: brightness set from 255 and decaying", storm.LightningBrightness > 0f && storm.LightningBrightness < 255f);
                shot("weather_lightning_dark.png");
                game.StepFor(1.0f);
                check("brightness reaches 0 and the mask is deleted", !storm.LightningMaskVisible && storm.LightningBrightness == 0f);
                check("next lightning is 50..60 s away", storm.ShanDianTimerForTest > 40f && storm.ShanDianTimerForTest <= 60f);
                // ---- wind ----
                var w = load(4, 2);
                game.StepFor(w.YeziTimerForTest + 0.05f);
                check("wind: effects spawned when the Yezi timer fires, timer re-armed", w.WindSpawns >= 1 && w.YeziTimerForTest > 0f);
                shot("weather_wind.png");
            }
            catch (Exception e) { failed++; log.AppendLine("EXCEPTION " + e); }
            finally
            {
                GameScene.DebugWeatherRoll = null;
                if (rt != null) UnityEngine.Object.DestroyImmediate(rt);
                if (img != null) UnityEngine.Object.DestroyImmediate(img);
                if (game != null) UnityEngine.Object.DestroyImmediate(game.gameObject);
                if (cam != null) UnityEngine.Object.DestroyImmediate(cam.gameObject);
                if (Directory.Exists(dir)) Directory.Delete(dir, true);
                log.AppendLine("SUMMARY passes=" + passed + " fails=" + failed);
                File.WriteAllText(Path.Combine(output, "verify.txt"), log.ToString()); Debug.Log(log.ToString());
                PilotExit.Exit(failed == 0 ? 0 : 1);
            }
        }
    }
}
