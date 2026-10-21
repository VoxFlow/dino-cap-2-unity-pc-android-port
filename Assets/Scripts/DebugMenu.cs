using System;
using UnityEngine;

namespace Dc2Pilot
{
    /// <summary>
    /// DEBUG_ONLY test menu (DC2-BUGS-001 O2, user request; modelled on DC1's debug panel). F1 toggles it.
    /// Rules: it changes nothing while closed; save edits go through Profile and Dc2Game.SaveFile (the native dinocap2hd.sav layout);
    /// battle cheats are the Dc2Game Debug* flags, read only by their one-line guards (GameCombat.HeroHurtFrom, GameFire / GameGirl ammo).
    /// None of this is native behaviour.
    /// </summary>
    public sealed class DebugMenu : MonoBehaviour
    {
        public Dc2Game game;
        public const KeyCode ToggleKey = KeyCode.F1;
        bool open;
        Vector2 scroll;
        int launchType = 3, launchLevel = 1, launchBg = 0, spawnKind = 1;
        string status = "";

        public static void Install(GameObject host, Dc2Game game) { var m = host.AddComponent<DebugMenu>(); m.game = game; }

        void Update()
        {
            if (Input.GetKeyDown(ToggleKey)) SetOpen(!open);
        }

        // r119o (user report: F1 did not close the menu at 48:9 and player input stayed frozen): the menu now also has a Close button, which works even when
        // the key press does not reach the game (e.g. Unity Game view without keyboard focus after its aspect dropdown was used).
        public void SetOpen(bool value)
        {
            open = value; if (game != null) game.DebugMenuOpen = open;
            // r119x perf: a MonoBehaviour with OnGUI makes Unity run IMGUI layout every frame (garbage even while closed), so the drawing lives in
            // DebugMenuView, which exists only while the menu is open.
            var view = GetComponent<DebugMenuView>();
            if (open && view == null) gameObject.AddComponent<DebugMenuView>().menu = this; else if (!open && view != null) { if (Application.isPlaying) Destroy(view); else DestroyImmediate(view); }
        }

        // ---- actions (public so PilotDebugMenuVerify drives the same code as the buttons) ----
        public void AddCash(int amount) { var p = game.Profile; p.cash = (int)Math.Min(9999999L, Math.Max(0L, (long)p.cash + amount)); Save("cash " + p.cash); }   // 9999999: the native cash cap (dinosaurClass::Die)
        public void SetCash(int value) { game.Profile.cash = Mathf.Clamp(value, 0, 9999999); Save("cash " + game.Profile.cash); }
        public void ResetCash() { SetCash(0); }
        public void ResetLevels()
        {
            var p = game.Profile;
            for (int i = 0; i < p.level.Length; i++) if (p.level[i] >= 0) p.level[i] = 0;   // owned heroes back to level 0, unowned stay -1
            p.exp = new int[p.exp.Length];
            Save("levels reset");
        }
        public void ResetHeroesBought()
        {
            var p = game.Profile;
            p.level = new[] { 0, -1, -1, -1, -1 }; p.exp = new int[5];   // the ResetFile hero state (Dc2Scene.Profile.ResetFileKeepAudio)
            if (p.playerType != 0) p.playerType = 0;
            Save("heroes reset (hero 1 only)");
        }
        public void ResetWeapons()
        {
            var fresh = new Profile();   // the reset weapon vectors (WeaponDefaults), taken from a fresh profile rather than restated here
            var p = game.Profile; p.weaponOwned = fresh.weaponOwned; p.weaponAmmo = fresh.weaponAmmo; p.weaponLevels = fresh.weaponLevels;
            p.selectedWeaponSlots = new int[5];
            Save("weapons reset");
        }
        public void ResetWholeSave() { game.Profile.ResetFileKeepAudio(); Save("whole save reset (audio kept)"); }
        public void UnlockAllHeroes() { var p = game.Profile; for (int i = 0; i < p.level.Length; i++) if (p.level[i] < 0) p.level[i] = 0; Save("all heroes owned"); }
        public void AddHeroLevel(int delta)
        {
            var p = game.Profile; int h = Mathf.Clamp(p.playerType, 0, p.level.Length - 1);
            if (p.level[h] < 0) p.level[h] = 0;
            p.level[h] = Mathf.Clamp(p.level[h] + delta, 0, 98);   // lifeModeScene::UpdateExp 0xd1f90 levels up only while level <= 97
            p.exp[h] = 0;
            if (delta > 0) game.DebugLevelUnlockPending.Add(h);   // r119j: the menu "+1" queues the level-weapon unlock too (applied by the next SelectScene.OnLoad, i.e. any tab)
            Save("hero " + (h + 1) + " level " + p.level[h]);
        }
        public void UnlockAllMissions()
        {
            int n = game.MapState != null && game.MapState.missions != null && game.MapState.missions.Count > 0 ? game.MapState.missions.Count : 30;
            game.Profile.mission = Mathf.Max(game.Profile.mission, n);
            Save("missions unlocked: " + game.Profile.mission);
        }
        public void GiveAllJigsawPieces()
        {
            var p = game.Profile; for (int i = 0; i < p.jigsawGet.Length; i++) p.jigsawGet[i] = 1;   // earned but not placed: the jigsaw screen places them natively
            Save("all 80 jigsaw pieces earned (place them in the jigsaw screen)");
        }
        static readonly System.Random debugRng = new System.Random();   // not the native rand stream (NativeRand stays untouched)
        /// <summary>Earns one random piece that is neither earned nor placed (-1 when all 80 are taken); the jigsaw screen then places it natively (fly-in + flash).</summary>
        public int GiveRandomJigsawPiece()
        {
            var p = game.Profile; var free = new System.Collections.Generic.List<int>();
            for (int i = 0; i < p.jigsawGet.Length; i++) if (p.jigsawGet[i] == 0 && p.jigsawActive[i] == 0) free.Add(i);
            if (free.Count == 0) { status = "no free jigsaw piece"; return -1; }
            int k = free[debugRng.Next(free.Count)]; p.jigsawGet[k] = 1;
            Save("jigsaw piece " + (k / 16 + 1) + "-" + (k % 16 + 1) + " earned (open the jigsaw screen to place it)");
            return k;
        }
        /// <summary>Clears every earned and placed piece and the five jigsaw bonus counts (g_jigsaw1..5_count follow the placed pieces); jigsawSeed is kept.</summary>
        public void ResetJigsawPieces()
        {
            var p = game.Profile;
            p.jigsawGet = new int[p.jigsawGet.Length]; p.jigsawActive = new int[p.jigsawActive.Length];
            p.jigsaw1Count = p.jigsaw2Count = p.jigsaw3Count = p.jigsaw4Count = p.jigsaw5Count = 0;
            Save("all jigsaw pieces reset");
        }
        public void LaunchMission(int type, int level, int background)
        {
            var m = new NativeMapState.Mission { type = type, level = Mathf.Max(1, level), background = background, count = type == 1 || type == 5 ? 1500f : 20f, info = 0 };
            m.enemies.Add(1);
            game.LaunchMission = m;
            game.SwitchScene("gameScene");
            status = "launching type " + type + " level " + m.level + " bg " + background;
        }
        public int KillAllDinosaurs()
        {
            var s = game.Current as GameScene; if (s == null) return 0;
            int n = 0;
            foreach (var d in s.dinos.ToArray()) if (!d.dead && !d.removed) { s.ApplyDamageForTest(d); n++; }
            status = "killed " + n; return n;
        }
        public void KillHero() { var s = game.Current as GameScene; if (s == null) return; s.DebugKillHero(); status = "hero killed"; }
        public void TeleportNearRunEnd() { var s = game.Current as GameScene; status = s != null && s.DebugTeleportNearRunEnd() ? "teleported 300 px before the finish" : "not an endurance (run) mission"; }
        public void SpawnDinosaur(int kind)
        {
            var s = game.Current as GameScene; if (s == null) return;
            var d = s.CreateEnemy(kind); status = d != null ? "spawned kind " + kind : "no kind " + kind;
        }

        void Save(string what) { game.SaveFile(); status = what; }

        // ---- IMGUI ----
        internal void DrawGui()
        {
            if (!open || game == null) return;
            float k = Mathf.Max(1f, Screen.height / 768f);
            var old = GUI.matrix; GUI.matrix = Matrix4x4.Scale(new Vector3(k, k, 1f));
            GUILayout.BeginArea(new Rect(10, 10, 520, Screen.height / k - 20), "DC2 DEBUG (F1)  -  DEBUG_ONLY, not native", GUI.skin.window);
            if (GUILayout.Button("Close (F1)")) { SetOpen(false); GUILayout.EndArea(); GUI.matrix = old; return; }
            scroll = GUILayout.BeginScrollView(scroll);
            var p = game.Profile;
            GUILayout.Label("Scene: " + game.CurrentName + "   cash " + p.cash + "   hero " + (p.playerType + 1) + " level " + (p.playerType < p.level.Length ? p.level[p.playerType] : 0) + "   mission " + p.mission);

            bool wide = GUILayout.Toggle(WideView.Enabled, " Wide view: fill the screen, no black bars (ADAPTATION, remembered; off = native 4:3)");
            if (wide != WideView.Enabled) { WideView.Enabled = wide; status = wide ? "wide view on" : "wide view off (native 4:3)"; }

            GUILayout.Label("Cash");
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("+1,000")) AddCash(1000);
            if (GUILayout.Button("+10,000")) AddCash(10000);
            if (GUILayout.Button("+100,000")) AddCash(100000);
            if (GUILayout.Button("+1,000,000")) AddCash(1000000);
            if (GUILayout.Button("Max")) SetCash(9999999);
            GUILayout.EndHorizontal();

            GUILayout.Label("Reset");
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Cash")) ResetCash();
            if (GUILayout.Button("Levels")) ResetLevels();
            if (GUILayout.Button("Heroes bought")) ResetHeroesBought();
            if (GUILayout.Button("Weapons")) ResetWeapons();
            GUILayout.EndHorizontal();
            if (GUILayout.Button("Reset whole save (keeps sound settings)")) ResetWholeSave();

            GUILayout.Label("Unlock");
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("All heroes")) UnlockAllHeroes();
            if (GUILayout.Button("All missions")) UnlockAllMissions();
            if (GUILayout.Button("All jigsaw pieces")) GiveAllJigsawPieces();
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Give random jigsaw piece")) GiveRandomJigsawPiece();
            if (GUILayout.Button("Reset all jigsaw pieces")) ResetJigsawPieces();
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Current hero level", GUILayout.Width(140));
            if (GUILayout.Button("-1")) AddHeroLevel(-1);
            if (GUILayout.Button("+1")) AddHeroLevel(1);
            if (GUILayout.Button("+10")) AddHeroLevel(10);
            if (GUILayout.Button("Max 98")) AddHeroLevel(98);
            GUILayout.EndHorizontal();

            GUILayout.Label("Battle");
            game.DebugGodMode = GUILayout.Toggle(game.DebugGodMode, " God mode (no HP loss; knockback and hurt still play)");
            game.DebugInfiniteAmmo = GUILayout.Toggle(game.DebugInfiniteAmmo, " Infinite ammo");
            GUILayout.BeginHorizontal();
            GUILayout.Label("Game speed x" + game.DebugTimeScale.ToString("0.##"), GUILayout.Width(140));
            if (GUILayout.Button("x0.25")) game.DebugTimeScale = 0.25f;
            if (GUILayout.Button("x0.5")) game.DebugTimeScale = 0.5f;
            if (GUILayout.Button("x1")) game.DebugTimeScale = 1f;
            if (GUILayout.Button("x2")) game.DebugTimeScale = 2f;
            if (GUILayout.Button("x4")) game.DebugTimeScale = 4f;
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Kill hero (failed screen)")) KillHero();
            if (GUILayout.Button("Empty held gun")) { var gs = game.Current as GameScene; if (gs != null) gs.DebugEmptyHeldGun(); }
            if (GUILayout.Button("Level up (in battle)")) { var gs = game.Current as GameScene; if (gs != null) gs.DebugLevelUp(); }
            if (GUILayout.Button("Endurance: TP near finish")) TeleportNearRunEnd();
            GUILayout.EndHorizontal();
            if (GUILayout.Button("Kill all dinosaurs")) KillAllDinosaurs();
            if (GUILayout.Button("-", GUILayout.Width(28))) spawnKind = Mathf.Max(1, spawnKind - 1);
            GUILayout.Label("kind " + spawnKind, GUILayout.Width(60));
            if (GUILayout.Button("+", GUILayout.Width(28))) spawnKind = Mathf.Min(19, spawnKind + 1);
            if (GUILayout.Button("Spawn")) SpawnDinosaur(spawnKind);
            GUILayout.EndHorizontal();

            GUILayout.Label("Launch a mission (1 run, 2 time, 3 kill, 5 egg, 7 arena, 8 boss; background 0 = City Grind)");
            GUILayout.BeginHorizontal();
            launchType = Picker("type", launchType, new[] { 1, 2, 3, 5, 7, 8 });
            launchLevel = Stepper("level", launchLevel, 1, 30);
            launchBg = Stepper("bg", launchBg, 0, 5);
            if (GUILayout.Button("Go")) LaunchMission(launchType, launchLevel, launchBg);
            GUILayout.EndHorizontal();

            if (status.Length > 0) GUILayout.Label("> " + status);
            GUILayout.EndScrollView();
            GUILayout.EndArea();
            GUI.matrix = old;
        }

        static int Stepper(string name, int v, int lo, int hi)
        {
            if (GUILayout.Button("-", GUILayout.Width(24))) v = Mathf.Max(lo, v - 1);
            GUILayout.Label(name + " " + v, GUILayout.Width(62));
            if (GUILayout.Button("+", GUILayout.Width(24))) v = Mathf.Min(hi, v + 1);
            return v;
        }
        static int Picker(string name, int v, int[] values)
        {
            int i = Array.IndexOf(values, v); if (i < 0) i = 0;
            if (GUILayout.Button("<", GUILayout.Width(24))) i = (i + values.Length - 1) % values.Length;
            GUILayout.Label(name + " " + values[i], GUILayout.Width(62));
            if (GUILayout.Button(">", GUILayout.Width(24))) i = (i + 1) % values.Length;
            return values[i];
        }
    }

    /// <summary>DEBUG_ONLY: the IMGUI host of the F1 menu (present only while it is open).</summary>
    public sealed class DebugMenuView : MonoBehaviour { internal DebugMenu menu; void OnGUI() { if (menu != null) menu.DrawGui(); } }
}
