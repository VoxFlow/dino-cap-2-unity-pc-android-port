using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dc2Pilot
{
    /// <summary>Per-mission random weather (mainScene::OnLoad 0xb6862..0xb694e / lifeModeScene::OnLoad 0xf0230..0xf03a6, identical): v = RandomRange(0,6) - 1; case 0 overcast (InitYinTian 0x95f7a),
    /// case 1 overcast + rain (InitRain 0xa2e50), case 2 + lightning (InitShanDian 0xa1310), case 3 wind effects when the background is 2 or 3 (InitFan 0xa12e0), anything else clear.
    /// Positions follow the camera every frame (mainScene::OnUpdate 0x979d2 / lifeModeScene::OnUpdate 0xd41ba). Evidence: Dev/Evidence/weather_decomp_r115.md.</summary>
    public partial class GameScene
    {
        public const int WeatherClear = 0, WeatherOvercast = 1, WeatherRain = 2, WeatherStorm = 3, WeatherWind = 4;
        public int WeatherKind;                       // DEBUG_ONLY classification of the chosen branch
        public static Func<int> DebugWeatherRoll;     // DEBUG_ONLY: replaces RandomRange(0,6)
        public int LightningStrikes, WindSpawns;      // DEBUG_ONLY
        int weatherBackground;
        NativeAffineSprite overcastSprite;
        NativeParticleEmitter rainA, rainB, rainC;    // raineff (layer 15), rainhua (layer 3), raineff2 (layer 7)
        Transform rainRootA, rainRootB, rainRootC;
        // Scene::SetTimer "ShanDian" / "AddShanDian" / "maskWhite" / "Yezi" (never killed); OnTimeout dispatches them by name (lifeModeScene::OnTimeout 0xe2a4c).
        NativeTimer.Info shanDianTimer, yeziTimer;
        float lightningOffset, lightningBrightness; bool lightningActive;
        UiControl lightningMask, lightningGlow;
        readonly Dictionary<string, string> weatherXml = new Dictionary<string, string>();
        public bool HasOvercast { get { return overcastSprite != null; } }
        public bool HasRain { get { return rainA != null; } }
        public int RainAlive { get { return (rainA != null ? rainA.AliveCount : 0) + (rainB != null ? rainB.AliveCount : 0) + (rainC != null ? rainC.AliveCount : 0); } }
        public bool LightningMaskVisible { get { return lightningMask != null; } }
        public float LightningBrightness { get { return lightningBrightness; } }
        float TimerRemaining(NativeTimer.Info t) { return Game.Timer.IsLive(t) ? t.deadline - Game.NativeTimerTime : -1f; }
        public float ShanDianTimerForTest { get { return TimerRemaining(shanDianTimer); } }
        public float YeziTimerForTest { get { return TimerRemaining(yeziTimer); } }

        bool WeatherTimeout(string name)
        {
            if (name == "Yezi") { AddYezi(); return true; }
            if (name == "maskWhite") { MaskWhite(); return true; }
            if (name == "AddShanDian") { AddShanDian(); return true; }
            if (name == "ShanDian") { AddDian(); return true; }
            return false;
        }

        float WeatherViewY { get { return -N.ScreenH * 0.5f; } }   // Camera viewport y: Camera::Initialize -0.5 * screen height (x: camLeft)

        void WeatherLoad()
        {
            weatherBackground = Game.LaunchMission.type == 7 ? 7 : Game.LaunchMission.type == 8 ? 8 : Game.LaunchMission.background;
            // the test harness (manualStep) runs clear weather unless a verifier forces a roll, so existing pixel/RNG assertions stay deterministic
            int roll = DebugWeatherRoll != null ? DebugWeatherRoll() : (Game.manualStep ? 0 : RandomRange(0, 6));
            uint v = unchecked((uint)(roll - 1));
            WeatherKind = WeatherClear;
            switch (v)
            {
                case 0: InitYinTian(); WeatherKind = WeatherOvercast; break;
                case 1: InitYinTian(); InitRain(); WeatherKind = WeatherRain; break;
                case 2: InitYinTian(); InitRain(); InitShanDian(); WeatherKind = WeatherStorm; break;
                case 3: if (unchecked((uint)(weatherBackground - 2)) <= 1u) { InitFan(); WeatherKind = WeatherWind; } break;
            }
        }

        void InitYinTian()
        {
            var root = new GameObject("weather_yintian"); root.transform.SetParent(Background.LayerRoot(13), false);
            overcastSprite = NativeAffineSprite.Create(root.transform);
        }

        string WeatherXml(string name)
        {
            string s;
            if (!weatherXml.TryGetValue(name, out s)) { var a = Resources.Load<TextAsset>("DC2/Image/Conf/" + name + ".xml"); s = a.text; weatherXml[name] = s; }
            return s;
        }

        void InitRain()
        {
            float S = N.S;
            rainA = MakeEmitter("raineff", 15, new Vector2(N.Mul32(S, 0f), N.Mul32(S, 180f)), new Vector2(N.Mul32(S, 525f), N.Mul32(S, 33f)), out rainRootA);
            rainB = MakeEmitter("rainhua", 3, new Vector2(0f, 0f), new Vector2(N.Mul32(S, 525f), N.Mul32(S, 100f)), out rainRootB);
            rainC = MakeEmitter("raineff2", 7, new Vector2(0f, N.Mul32(S, 160f)), new Vector2(N.Mul32(S, 525f), N.Mul32(S, 33f)), out rainRootC);
        }

        NativeParticleEmitter MakeEmitter(string xmlName, int layer, Vector2 pos, Vector2 size, out Transform root)
        {
            var go = new GameObject("rain_" + xmlName); go.transform.SetParent(Background.LayerRoot(layer), false); root = go.transform;
            var e = new NativeParticleEmitter(Rand01) { position = pos, size = size, layer = layer };
            e.LoadXml(WeatherXml(xmlName));
            e.Play();
            return e;
        }

        void InitShanDian() { lightningActive = false; shanDianTimer = SetTimer("ShanDian", RandomRange(4, 7)); }
        void InitFan() { yeziTimer = SetTimer("Yezi", RandomRange(1, 3)); }

        /// <summary>mainScene/lifeModeScene OnUpdate camera-follow block + the weather timers.</summary>
        float sceneUpdateCamLeft; bool sceneUpdateCamValid;
        public float LastWeatherViewX { get; private set; }   // DEBUG_ONLY verifier hook
        /// <summary>lifeModeScene::OnUpdate 0xd41ba places the rain emitters (+660/+664/+668) and the yintian object (+672) from Camera::GetViewport at the start of the
        /// scene callback, i.e. before the hero GameObject (id 1) runs UpdateCamera in the same Scene::Update; they therefore follow the previous tick's camera (r118z L1).</summary>
        void UpdateWeather(float dt)
        {
            float S = N.S, vx = sceneUpdateCamValid ? sceneUpdateCamLeft : camLeft, camCenterX = camLeft + WorldViewportW * 0.5f;
            sceneUpdateCamValid = false; LastWeatherViewX = vx;
            if (overcastSprite != null)
            {
                // yintian object (+396 / +672): centre (S*240 + view.x + 0.5*g_w_offset, S*160 + view.y + 0.5*g_h_offset), size (S*600, S*400)
                float cx = N.Add32(N.Add32(N.Mul32(S, 240f), vx), 0.5f * N.WOffset), cy = N.Add32(N.Add32(N.Mul32(S, 160f), WeatherViewY), 0.5f * N.HOffset);
                var r = Atlas.Get("yintianFrame").rect;
                // ADAPTATION (WideView, user item 7): the native overcast covers S*600 around the 1024 view; widened by the extra width on each side (render only, 0 = native)
                var f = new NativeComponentAnimation.Frame { scaleX = (N.Mul32(S, 600f) + 2f * GameScene.WideCombatExtra) / r.width, scaleY = N.Mul32(S, 400f) / r.height, rgba = 0xFFFFFFFF };
                overcastSprite.Draw("yintianFrame", f, new Vector2(N.ScreenW * 0.5f + (cx - camCenterX), -N.ScreenH * 0.5f + cy), 0, 1f, false, null);
            }
            if (rainA != null)
            {
                float x = N.Add32(N.Mul32(S, 350f), vx);
                rainA.position = new Vector2(x, N.Mul32(S, 160f)); rainC.position = new Vector2(x, N.Mul32(S, 160f));
                rainB.position = new Vector2(x, weatherBackground != 0 ? N.Mul32(S, -100f) : N.Mul32(S, -80f));
                rainA.Step(dt); rainB.Step(dt); rainC.Step(dt);
                rainA.Draw(rainRootA, camCenterX); rainB.Draw(rainRootB, camCenterX); rainC.Draw(rainRootC, camCenterX);
            }
            WeatherTimers(dt);
        }

        void WeatherTimers(float dt)
        {
            if (lightningActive)
            {
                // lifeModeScene::OnUpdate +653: brightness -= dt * 192 * -2 ... (decays 255 -> 0), the afterglow image goes below 240
                lightningBrightness = N.Add32(lightningBrightness, N.Mul32(N.Mul32(dt, 192f), -2f));
                if (lightningBrightness < 0f)
                {
                    lightningBrightness = 0f; DeleteControl(11); lightningMask = null; lightningActive = false;
                }
                else
                {
                    if (!(lightningBrightness >= 240f)) { DeleteControl(42); lightningGlow = null; }
                    if (lightningMask != null) lightningMask.SetColor(new Color32(0, 0, 0, (byte)(uint)lightningBrightness));
                }
            }
        }

        /// <summary>lifeModeScene::AddDian 0xe1304: lightning bolt effect (g_ca_shandianEff, layer 14, mode 10 = x follows the camera by S*offset), then AddShanDian in 0.08 s; linghtingAudio.</summary>
        void AddDian()
        {
            float S = N.S;
            lightningOffset = RandomRange(-200, 200);
            var f = SpawnCaFx("shandian", FxAnimation("DC2/Image/an2/shandian.a2b", true), N.Add32(camLeft, N.Mul32(S, lightningOffset)), N.Mul32(S, 130f), false, 14, 1f);
            f.camOffset = N.Mul32(S, lightningOffset);
            SetTimer("AddShanDian", 0.08f);
            LightningStrikes++;
            Audio.PlaySound("linghtingAudio");
        }

        /// <summary>lifeModeScene::AddShanDian 0xe20c0: full-screen white flash (UIImage id 11 sd_baiFrame), maskWhite in 0.2 s, next ShanDian in S*25 + rand*(S*30 - S*25) seconds.</summary>
        void AddShanDian()
        {
            float S = N.S;
            if (lightningMask == null) { lightningMask = CreateControl(11, 2); lightningMask.SetTexture("sd_baiFrame"); lightningMask.touchable = false; }
            lightningMask.SetRect(new Rect(0f, 0.5f * N.HOffset, N.Add32(N.Mul32(S, 480f), N.WOffset), N.Add32(N.Mul32(S, 320f), N.HOffset)));
            lightningMask.SetColor(new Color32(255, 255, 255, 255));
            float a = N.Mul32(S, 25f);
            SetTimer("maskWhite", 0.2f);
            shanDianTimer = SetTimer("ShanDian", N.Add32(a, N.Mul32(Rand01(), N.Add32(N.Mul32(S, 30f), -a))));
        }

        /// <summary>lifeModeScene::OnTimeout "maskWhite" 0xe2bf2: the mask turns black (alpha 255, then fades with the brightness), brightness := 255, the afterglow image shandian02Frame (id 42) appears.</summary>
        void MaskWhite()
        {
            float S = N.S;
            if (lightningMask != null) lightningMask.SetColor(new Color32(0, 0, 0, 255));
            lightningBrightness = 255f;
            DeleteControl(42);
            lightningGlow = CreateControl(42, 3); lightningGlow.touchable = false; lightningGlow.SetTexture("shandian02Frame");
            lightningGlow.SetRect(new Rect(N.Mul32(S, N.Add32(N.Add32(240f, lightningOffset), -40f)), 0f, N.Mul32(S, 80f), N.Mul32(S, 220f)));
            lightningActive = true;
        }

        /// <summary>lifeModeScene::AddYezi 0xe1040: RandomRange(1,4) wind effects (g_ca_feng1..3Eff = eff_fan1..3, layer 9, mode 11 = static in the world), then re-arm "Yezi" in RandomRange(1,3) s.</summary>
        void AddYezi()
        {
            float S = N.S;
            int n = RandomRange(1, 4);
            for (int i = 0; i < n; i++)
            {
                int type = RandomRange(1, 4);
                float x, y;
                switch (type)
                {
                    case 1: case 2: x = N.Add32(camLeft, N.Mul32(S, 240f)); y = N.Mul32(S, 130f); break;
                    case 3: x = N.Add32(N.Add32(N.Mul32(S, 350f), camLeft), N.Mul32(S, RandomRange(-150, 50))); y = N.Mul32(S, -60f); break;
                    default: continue;
                }
                SpawnCaFx("feng" + type, FxAnimation("DC2/Image/an2/eff_fan" + type + ".a2b", true), x, y, false, 9, 1f);
                WindSpawns++;
            }
            yeziTimer = SetTimer("Yezi", RandomRange(1, 3));
        }
    }
}
