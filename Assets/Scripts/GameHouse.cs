using System.Collections.Generic;
using UnityEngine;

namespace Dc2Pilot
{
    /// <summary>mainScene::InitHouse 0x9aae8 / lifeModeScene::InitHouse 0xd6504 (identical code): animated "houseClass" buildings and
    /// "zhuangshi" decor sprites along the street, plus houseClass::OnLoad 0x72dc (random house1/2/3).
    /// Recovered live behaviour is decorative only: houseClass::OnKeyFrame -> EnterHouse(false) is gated by playerClass+659 which the constructor
    /// sets to 1 (0xbc0ac), EnterHouse(true) has no caller, and no code starts the "close" timer (Dev/Evidence/egg_decomp_r108.md, house section).
    /// DEAD (PROVEN, dc2.house.door_frames.dead.r118): the pickup path (houseClass::OnTimeout "close" -> itemClass::Start) is never started: no SetTimer("close").</summary>
    public partial class GameScene
    {
        sealed class HouseActor { public GameObject root; public NativeCaPlayer ca; public float x, y; }
        sealed class DecorActor { public SpriteRenderer sr; public float x, y, w, h; }
        readonly List<HouseActor> houses = new List<HouseActor>();
        readonly List<DecorActor> decor = new List<DecorActor>();
        public int HouseCount { get { return houses.Count; } }
        public int DecorCount { get { return decor.Count; } }
        public Vector2 HousePosition(int i) { return new Vector2(houses[i].x, houses[i].y); }
        public Vector2 DecorPosition(int i) { return new Vector2(decor[i].x, decor[i].y); }
        public int HouseKind(int i) { return houseKinds[i]; }
        readonly List<int> houseKinds = new List<int>();

        void HouseLoad()
        {
            if (Game.LaunchMission == null) return;
            // InitHouse is reached only from the background-0 branch of InitGameBackGround: mainScene 0xb3332, lifeModeScene 0xddaf6. Bounded emulation of
            // mainScene::OnLoad and lifeModeScene::InitGameBackGround 0xdcaa6 for bg 0..8: no other background calls it (DC2-BUGS-001 B1, r118z8; the port
            // built houses and zhuangshi, and drew their RandomRange values, on every map).
            int bg = Game.LaunchMission.type == 7 ? 7 : Game.LaunchMission.type == 8 ? 8 : Game.LaunchMission.background;
            if (bg != 0) return;
            float s = N.S;
            float right = BoundUpper;
            int count = (int)N.Div32(N.Add32((float)(int)right, N.Mul32(s, -380f)), N.Mul32(s, 960f));
            float houseY = N.Mul32(s, -36f);
            for (int i = 0; i < count; i++)
            {
                var h = new HouseActor();
                float x = N.Add32(N.Add32(N.Mul32(s, (float)RandomRange(0, 200)), 380f), N.Mul32(s, N.Mul32((float)i, 960f)));
                h.x = x; h.y = houseY;
                h.root = new GameObject("houseClass"); h.root.transform.SetParent(Background.LayerRoot(5), false);   // CAObjCallBack layer field = 5
                // houseClass::OnLoad: roll in [0,100): <33 house1 (508x210, start frame 10), <66 house2 (466x210, start 12), else house3 (614x210, start 12)
                float roll = (float)(int)N.Mul32(Rand01(), 100f);
                int kind = roll < 33f ? 1 : roll < 66f ? 2 : 3;
                houseKinds.Add(kind);
                // r119 (DC2-BUGS-001 item 11): Resources now hold DC2's own Image/a2b/houseN.a2b (r119b; the port had imported the unused a2b/iPad/ files, byte-identical to DC1's iPad houses, 782x470 bodies). The loader sets g_scale = 2.0 on iPad (sub_6D068 0x6d31c..0x6d334, literal 0x40000000; iPhone 1.0) right before
                // CombineAnimation::LoadFromFile 0xc7ba (house1/2/3.a2b) -> CAFrame -> CASprite::LoadFromFile 0xbb98, which multiplies part x, y, w and h by g_scale
                // (no frame-size substitution: that is the XML loader). The port loaded them through the XML-named path at scale 1: every house was half size.
                var anim = NativeCaAnimation.Load("DC2/Image/a2b/house" + kind + ".a2b", false, 2f);
                h.root.AddComponent<UnityEngine.Rendering.SortingGroup>().sortingOrder = NativeDrawOrder.CaGroup(NativeDrawOrder.Base(NativeDrawOrder.Allocate()));   // houseClass is a CAObj (layer 5, mode 5: DrawOrder = creation base)
                h.ca = new NativeCaPlayer(h.root.transform, _ => h.root.transform, 50);
                // houseClass::OnLoad 0x72dc: +108 = "_a" (CAObjCallBack::OnUpdate frame suffix: fangziN_a / fangziN_md_a are drawn, the door leaves without an _a frame keep
                // their own). r119, DC2-BUGS-001 item 11: the port drew the plain fangziN frames, whose painted doors do not line up with the door parts.
                h.ca.FrameSuffix = "_a";
                h.ca.position = new Vector2(x - (camLeft + WorldViewportW * 0.5f), houseY);
                int start = kind == 1 ? 10 : 12;   // houseClass::OnLoad playAnimation(0, 10/12, 0.025, -1)
                h.ca.Play(anim, 0, Mathf.Min(start, anim.frames.Length - 1), 0.025f);   // r119b: DC2's own house2/3.a2b have 13 frames (house1 11), so the start frames 12 / 10 are the last frames: the house shows its final door pose (the r118 'start 12 past the 11-frame end' note came from the DC1 iPad files the port had imported, SUPERSEDED)
                houses.Add(h);
            }
            int decorCount = (int)N.Mul32(s, N.Div32(right, 360f));
            for (int j = 0; j < decorCount; j++)
            {
                var d = new DecorActor();
                float fr = RandomRange(0, 99);
                string frame; float w, hgt;
                if (fr < 33f) { frame = "zhuangshi1Frame"; w = N.Mul32(s, 86f); hgt = N.Mul32(s, 208f); }
                else if (fr < 66f) { frame = "zhuangshi2Frame"; w = N.Mul32(s, 24f); hgt = N.Mul32(s, 161f); }
                else { frame = "zhuangshi3Frame"; w = N.Mul32(s, 37f); hgt = N.Mul32(s, 57f); }
                d.w = w; d.h = hgt;
                d.x = N.Add32(N.Add32(N.Mul32(s, (float)RandomRange(0, 40)), -100f), N.Mul32(s, N.Mul32((float)j, 320f)));
                d.y = N.Add32(N.Add32(N.Mul32(0.5f, hgt), GroundY), N.Mul32(s, 20f));
                var go = new GameObject("zhuangshi"); go.transform.SetParent(Background.LayerRoot(4), false);   // sprite layer field = 4
                d.sr = go.AddComponent<SpriteRenderer>(); d.sr.sprite = Atlas.Has(frame) ? Atlas.Get(frame) : null; d.sr.sortingOrder = 100;   // PLATFORM_ADAPTER: see CreateItem
                decor.Add(d);
            }
            UpdateHouses(0f);
        }

        void UpdateHouses(float dt)
        {
            float half = camLeft + WorldViewportW * 0.5f;
            foreach (var h in houses) { h.ca.position = new Vector2(h.x - half, h.y); h.ca.Step(dt); }
            foreach (var d in decor)
            {
                if (d.sr.sprite == null) { d.sr.enabled = false; continue; }
                Place(d.sr, N.ScreenW * 0.5f + (d.x - half), -N.ScreenH * 0.5f + d.y, d.w, d.h);
            }
        }
    }
}
