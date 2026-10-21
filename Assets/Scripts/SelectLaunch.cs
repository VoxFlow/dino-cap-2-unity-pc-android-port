using UnityEngine;

namespace Dc2Pilot
{
    /// <summary>
    /// selectScene m_map_type 6 (mission launch loading screen). Native: OnLoad case 6 0x1060bc (iPad branch),
    /// OnUIEvent case 6 0x10b9b8 (control 597 event 1). Registry dc2.launch.mode6_trace.r15, dc2.mission.types_dispatch.
    /// Destination scenes (mainScene / lifeModeScene) are NOT implemented: the dispatch result is recorded and the
    /// loading screen stays up (no fabricated gameplay).
    /// </summary>
    public partial class SelectScene
    {
        // selectScene ctor 0xf7190 loading tips, ctor order assumed = string-table order (LIKELY)
        public static readonly string[] LoadingTips =
        {
            "TIP: When EGG ROBBERY, you can't \nroll while carrying the egg on your back.",
            "TIP: You will get the best effect, if \nyou attack the boss's head with rifle.",
            "TIP: You will get very good effect, if \nyou attack the boss's tail and \nfoot with melee weapons.",
            "TIP: Pterosaur's attack range is a little \nbit farther than melee weapons'.\n Avoid using melee weapons as much as possible \nwhile pterosaurs are attacking.",
            "TIP: Gather jigsaws to increase characters' \nattributes, but each character can only \nget 1 predetermined jigsaw.",
            "TIP: Warrior Girl's melee weapon will \ngain a special effect when fully upgraded!",
            "TIP: Unlock a new weapon every 5 levels.",
        };
        public string launchTip = "";
        public string launchDestination = "";   // "mainScene" / "lifeModeScene" once control 597's move finishes
        public bool launchLifeMode;
        public bool launchInitTime;
        public bool launchReloadWeaponFirst;     // types 7/8 reload weapon.prop before the scene switch; 2/3/4 after

        void ShowLaunchScene()
        {
            var img = CreateControl(597, 0);
            img.SetTexture("loadingFrame");
            Rect r = N.Layout(340, 257, 115, 22); r = new Rect(r.x, r.y + 54f, r.width, r.height);   // iPad y += 54
            img.SetRect(r); MoveLeft(img, r, White);
            DeleteLabel(598);
            var l = CreateLabel(598, 4, 24); l.SetNative("Adventure_12", 0f, -2f * N.S, 1, 0);   // selectScene::OnLoad 0x106308..0x10645a (ARM): Adventure_12, colour 0xFF00CDEF, SetLineSpacing(-2*S), SetAlignment(1,0)
            l.SetColor(new Color32(0xEF, 0xCD, 0, 255));
            Rect lr = N.Layout(0, 120, 480, 80); lr = new Rect(lr.x, lr.y + 22f, lr.width, lr.height);
            l.SetRect(lr);
            launchTip = LoadingTips[NativeRand.Range(0, LoadingTips.Length - 1)];   // selectScene::OnLoad 0x106484: RandomRange(0, size - 1): the last tip only appears on the float-rounding edge (shipped quirk)
            l.SetText(launchTip);
            MoveRightLabel(l, lr);
        }

        /// <summary>OnUIEvent case 6: only control 597 / event 1 acts. Ordering per registry dc2.mission.types_dispatch.</summary>
        bool HandleLaunchEvent(UiControl c, int ev)
        {
            if (Game.MapType != 6) return false;
            if (c.id != 597 || ev != 1) return true;
            if (Game.LaunchMission != null)   // 0x10b9f2..0x10ba1c: size <= 2 -> push_back(level), else insert at begin and drop the last (sliding window of 3)
            {
                if (Game.JigsawLevels.Count <= 2) Game.JigsawLevels.Add(Game.LaunchMission.level);
                else { Game.JigsawLevels.Insert(0, Game.LaunchMission.level); Game.JigsawLevels.RemoveAt(Game.JigsawLevels.Count - 1); }
            }
            int type = Game.LaunchMissionType;
            if (type >= 1 && type <= 8)
            {
                if (Profile.missionState == null || Profile.missionState.Length < 8) System.Array.Resize(ref Profile.missionState, 8);
                Profile.missionState[type - 1]++;                       // ++g_mission_state[type-1]
                Game.SaveFile();                                       // SaveMissionInfo (JSON adapter)
            }
            launchLifeMode = type == 2 || type == 3 || type == 4 || type == 7 || type == 8;
            launchDestination = launchLifeMode ? "lifeModeScene" : "mainScene";
            launchInitTime = type != 1 && type != 5;                   // 1/5 bypass lifeModeTime::initTime
            launchReloadWeaponFirst = type == 7 || type == 8;
            Audio.Stop("menu_bgAudio");
            lastAction = "launch:" + launchDestination;
            Game.SwitchScene("gameScene");                             // PILOT: mainScene and lifeModeScene share the first battle scene
            return true;
        }
    }
}
