using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Dc2Pilot
{
    /// <summary>
    /// Persistent profile. Native container: dinocap2hd.sav in the native binary layout (NativeSaveFile, DinoCap::SaveFile 0x58b9c / ReadFile 0x1995c; r118z L10).
    /// Fields that the native file does not contain (weaponLastCost, upgrade / purchase counters, missionState, iapTime) are not persisted, as in the original.
    /// PLATFORM_ADAPTER: a legacy pilot "dinocap2hd.sav.json" is read once when no native file exists; every save writes the native file.
    /// </summary>
    [Serializable]
    public class Profile
    {
        public int sound = 1;          // native Init0x171e6; absent-save epilogue retains this.
        public int fx = 1;             // native Init0x171ec.
        public int mission = 1;        // native Init0x171d4.
        public int cash = 0;           // dword_24047C
        public int playerType = 0;     // g_player_type
        public int[] level = new[] {0,-1,-1,-1,-1}; // native XML loader0xc5fb6/0xc6108.
        public int[] exp = new int[5];     // one zero per shipped Player.xml row.
        public int jigsaw1Count = 0;       // g_jigsaw1_count (HP bonus 1% each)
        public int jigsaw2Count, jigsaw3Count, jigsaw4Count, jigsaw5Count;   // g_jigsaw2..5_count
        public int[] jigsawActive = new int[80];   // g_jigsaw_active (0x23e0f8), 5 groups x 16 pieces
        public int[] jigsawGet = new int[80];      // g_jigsaw_get (0x23e0a8): pieces earned in battle, not yet placed
        public int jigsawSeed;             // DinoCap::jigsawSeed (0x23b8e0): mission number at which the next jigsaw piece is awarded
        public int[] missionState = new int[8];   // g_mission_state (incremented at mission launch)
        public int[] iapTime = new int[6]; // g_iap_time[6] (0x240a84), ++ per completed IAP product
        public int maxBullet = 500;        // g_max_bullet_count (static init 500; IAP product 5 sets 999)
        // playerTypeClass vectors +84 ownership, +96 ammo, +108 upgrade levels. Five heroes x six records.
        public int[] weaponOwned = WeaponDefaults(0, -1);
        public int[] weaponAmmo = WeaponDefaults(200, 0);
        public int[] weaponLevels = WeaponDefaults(1, 1);
        public int[] weaponLastCost = new int[30]; // subWeaponClass+24 live last calculated purchase price
        public int[] selectedWeaponSlots = new int[5]; // native vector2404a0: XML/reset0; missionComplete copies current slot, entry consumes it (r86b).
        public int[] weaponUpgradeCount = new int[5], weaponUpgradeSpent = new int[5]; // player stats+56/+60
        public int[] ammoPurchaseCount = new int[5], ammoPurchaseSpent = new int[5]; // +64/+68
        // Native save fields the port keeps for an exact round trip (written by SaveFile, read by ReadFile).
        public int nativeMaxMission, nativeMaxMissionRand;      // g_max_mission / g_max_mission_rand: only Init/Read/Save/Reset touch them (inert)
        public float nativePropPlayer = 100f;                    // g_prop_Player (ResetFile 100.0)
        public int native240480;                                 // dword_240480 (GameOverEnd label, menuScene::startUI zeroes it without a save)
        public int[] nativeHired = new int[5];                   // vector<bool> unk_2404F8: only Read/Save/Reset touch it (inert); one byte per Player.xml row (LIKELY 5)
        public int nativeDifficult = 1;                          // g_difficult (static init 1; ReadFile clamps to 1..3)
        public int nativeCurrentWeapon;                          // dword_2404E8 (current weapon index)
        public int nativeP4Kill, nativeP1Kill;                   // g_p4_kill_dino / g_p1_kill_dino
        public int[] nativeHeroHp, nativeHeroDf;                 // playerTypeClass +8 / +12 (Player.xml values until a save overwrites them; filled lazily, Resources is not callable during deserialization)
        static int[] HeroField(Func<Dc2Data.Hero, int> f) { var v = new int[5]; try { var hs = Dc2Data.Heroes(); for (int i = 0; i < 5 && i < hs.Length; i++) v[i] = f(hs[i]); } catch (Exception) { } return v; }

        static int[] WeaponDefaults(int first, int rest)
        {
            var values = new int[30];
            for (int i=0; i<values.Length; i++) values[i] = i%6 == 0 ? first : rest;
            return values;
        }

        internal void EnsureNativeFields()
        {
            if (nativeHeroHp == null || nativeHeroHp.Length != 5) nativeHeroHp = HeroField(h => h.hp);
            if (nativeHeroDf == null || nativeHeroDf.Length != 5) nativeHeroDf = HeroField(h => h.df);
            if (nativeHired == null || nativeHired.Length != 5) nativeHired = new int[5];
        }

        void EnsureWeaponArrays()
        {
            EnsureNativeFields();
            // PLATFORM_ADAPTER: old pilot JSON files had no weapon vectors.
            if (weaponOwned == null || weaponOwned.Length != 30) weaponOwned = WeaponDefaults(0,-1);
            if (weaponAmmo == null || weaponAmmo.Length != 30) weaponAmmo = WeaponDefaults(200,0);
            if (weaponLevels == null || weaponLevels.Length != 30) weaponLevels = WeaponDefaults(1,1);
            if (weaponLastCost == null || weaponLastCost.Length != 30) weaponLastCost = new int[30];
            if (selectedWeaponSlots == null) selectedWeaponSlots = new int[5]; // JSON adapter migration; native XML defaults0.
            else if(selectedWeaponSlots.Length<5)Array.Resize(ref selectedWeaponSlots,5); // preserve existing values; native short fifth read defaults only that slot0.
            if (weaponUpgradeCount == null || weaponUpgradeCount.Length != 5) weaponUpgradeCount = new int[5];
            if (weaponUpgradeSpent == null || weaponUpgradeSpent.Length != 5) weaponUpgradeSpent = new int[5];
            if (ammoPurchaseCount == null || ammoPurchaseCount.Length != 5) ammoPurchaseCount = new int[5];
            if (ammoPurchaseSpent == null || ammoPurchaseSpent.Length != 5) ammoPurchaseSpent = new int[5];
        }

        static string PathFor(string dir) { return System.IO.Path.Combine(dir, "dinocap2hd.sav.json"); }
        static string NativePathFor(string dir) { return System.IO.Path.Combine(dir, NativeSaveFile.FileName); }

        public static Profile Load(string dir)
        {
            try
            {
                string n = NativePathFor(dir);
                if (File.Exists(n)) { var np = new Profile(); np.EnsureWeaponArrays(); NativeSaveFile.Read(np, File.ReadAllBytes(n)); return np; }   // FileManager::CheckSave + ReadFile
                string p = PathFor(dir);
                if (File.Exists(p))
                {
                    var profile=JsonUtility.FromJson<Profile>(File.ReadAllText(p));
                    // PLATFORM_ADAPTER: preserve legacy pilot JSON values while adding its missing fifth slot.
                    if(profile.level.Length==4) {Array.Resize(ref profile.level,5);profile.level[4]=-1;}
                    if(profile.exp.Length==4)Array.Resize(ref profile.exp,5);
                    profile.EnsureWeaponArrays();
                    return profile;
                }
            }
            catch (Exception) { /* fall through to defaults like a missing native save */ }
            return new Profile();
        }

        public void Save(string dir)
        {
            Directory.CreateDirectory(dir);
            EnsureWeaponArrays();
            File.WriteAllBytes(NativePathFor(dir), NativeSaveFile.Write(this));   // FileManager::WriteSave("dinocap2hd.sav")
        }

        /// <summary>DinoCap::ResetFile: audio flags are NOT touched (registry dc2.save.reset_defaults).</summary>
        public void ResetFileKeepAudio()
        {
            cash = 0; playerType = 0; mission = 1;
            level = new[] {0,-1,-1,-1,-1}; exp = new int[5]; jigsaw1Count = 0;
            weaponOwned = WeaponDefaults(0,-1); weaponAmmo = WeaponDefaults(200,0); weaponLevels = WeaponDefaults(1,1);
            weaponLastCost = new int[30]; weaponUpgradeCount = new int[5]; weaponUpgradeSpent = new int[5];
            selectedWeaponSlots = new int[5]; // native Reset596a4..596b8, XML per-player zero push.
            ammoPurchaseCount = new int[5]; ammoPurchaseSpent = new int[5];
            iapTime = new int[6]; // g_iap_time: telemetry-only (read only by Read/SaveMissionInfo and SendStatisticsData, r118w); PA-009 counter-reset question closed as EXCLUDED
            // NATIVE ResetFile5969a restores cap500;596c0/596d4 clear5x16 jigsaw arrays;596ec..59704 clear bonuses.
            maxBullet = 500; jigsawActive = new int[80]; jigsawGet = new int[80]; jigsaw2Count = jigsaw3Count = jigsaw4Count = jigsaw5Count = 0;
            // ResetFile 0x5960a: g_max_mission = g_max_mission_rand = 0, g_prop_Player = 100.0, dword_240480 = 0, hired flags cleared, kills 0, Player.xml reloaded (+8/+12).
            nativeMaxMission = nativeMaxMissionRand = 0; nativePropPlayer = 100f; native240480 = 0; nativeHired = new int[5]; nativeP4Kill = nativeP1Kill = 0;
            nativeHeroHp = HeroField(h => h.hp); nativeHeroDf = HeroField(h => h.df);
        }
    }

    public static class Util
    {
        /// <summary>Destroy in play mode, DestroyImmediate in edit mode (verification harness runs in the editor).</summary>
        public static void Kill(GameObject go)
        {
            if (Application.isPlaying) UnityEngine.Object.Destroy(go);
            else UnityEngine.Object.DestroyImmediate(go);
        }
    }

    public static class Dc2Data
    {
        public struct Hero { public int hire, hp, df, baseExp; }
        static Hero[] heroes;

        /// <summary>Player.xml: hire (+4), hp (+8), df (+12), baseexp (+16).</summary>
        public static Hero[] Heroes()
        {
            if (heroes != null) return heroes;
            var ta = Resources.Load<TextAsset>("DC2/Image/Conf/Player.xml");
            var list = new List<Hero>();
            var rx = new System.Text.RegularExpressions.Regex("<player id=\"(\\d+)\" hire=\"(\\d+)\" hp=\"(\\d+)\" df=\"(\\d+)\" baseexp=\"(\\d+)\"");
            foreach (System.Text.RegularExpressions.Match m in rx.Matches(ta.text))
                list.Add(new Hero { hire = int.Parse(m.Groups[2].Value), hp = int.Parse(m.Groups[3].Value), df = int.Parse(m.Groups[4].Value), baseExp = int.Parse(m.Groups[5].Value) });
            heroes = list.ToArray();
            return heroes;
        }

        /// <summary>GetPlayer4Hp@0x1946c. Native reads the saved/current player level,
        /// even when called for a different Heroes cursor. Double multiply then signed truncation.</summary>
        public static int GirlHp(int hp, int currentPlayerLevel)
        {
            int level = currentPlayerLevel;
            if (level < 0) return hp;
            double factor = level <= 3 ? 0.5 : level <= 8 ? 0.6 : level <= 13 ? 0.7 :
                level <= 18 ? 0.8 : level <= 23 ? 0.9 : level <= 28 ? 1.0 : 1.2;
            return (int)((double)hp * factor);
        }
    }

    /// <summary>DinoCap::PlayMusic / PlaySound gating (registry dc2.audio.music_gate).</summary>
    public class AudioMgr : MonoBehaviour
    {
        public Profile profile;
        AudioSource music, sfx;
        string musicKey;
        public string LastMusicStarted { get; set; }   // DEBUG_ONLY probe (settable by verifiers)
        public string LastMusicResource { get; private set; }
        public int SoundsPlayed { get; private set; }

        static readonly Dictionary<string, string> clips = new Dictionary<string, string>
        {
            { "Music_menuAudio", "DC2/Audio/Music_Bg_Menu" },          // LIKELY pairing (dc2.audio.menu_music_file)
            { "Audio_buttonAudio", "DC2/Audio/Audio_button" },
            { "Audio_Button1Audio", "DC2/Audio/Audio_Button1" },
            { "Audio_button2Audio", "DC2/Audio/Audio_button2" },
            { "A_jigsaw_touchend", "DC2/Audio/A_jigsaw_touchend" },
            { "A_jigsaw_moveend", "DC2/Audio/A_jigsaw_moveend" },
        };

        // Awake is not called when the harness drives the game in edit mode, so create sources lazily.
        [Serializable] class AudioEntry { public string name, resource; }
        [Serializable] class AudioTable { public AudioEntry[] entries; }
        static bool nativeTableLoaded;
        /// <summary>Native CreateAudio name -> Audio/ file pairs recovered by emulating the loader (Dev/Recovered/emu_loader_*.json).</summary>
        static void LoadNativeAudioTable()
        {
            if (nativeTableLoaded) return;
            nativeTableLoaded = true;
            var data = Resources.Load<TextAsset>("DC2/native_audio_table.json");
            if (data == null) return;
            foreach (var e in JsonUtility.FromJson<AudioTable>(data.text).entries) if (!clips.ContainsKey(e.name)) clips[e.name] = e.resource;
        }
        public void RegisterAudio(string key, string resource) { clips[key] = resource; }
        public void DeleteAudio(string key) { clips.Remove(key); }
        void Ensure()
        {
            if (music == null) { music = gameObject.AddComponent<AudioSource>(); music.playOnAwake = false; }
            if (sfx == null) { sfx = gameObject.AddComponent<AudioSource>(); sfx.playOnAwake = false; }
        }

        /// <summary>Native plays only when g_sound != 0 (and the device is not already playing iPod music).</summary>
        public void PlayMusic(string key, bool loop, string stopKey)
        {
            Ensure();
            if (profile.sound == 0) return;
            AudioClip c = Resources.Load<AudioClip>(clips[key]);
            if (c == null) return;
            music.clip = c; music.loop = loop; music.Play();
            musicKey = stopKey; LastMusicStarted = key;
            LastMusicResource = clips[key];
        }

        public void Stop(string stopKey)
        {
            Ensure();
            if (musicKey == stopKey) { music.Stop(); musicKey = null; }
        }

        public bool MusicPlaying { get { return music != null && music.isPlaying; } }
        /// <summary>Triniti2D::AudioManager::CheckSession 0x1606f0 stand-in: the music session with this stop key is current and playing.</summary>
        public bool SessionPlaying(string stopKey) { return musicKey == stopKey && music != null && music.isPlaying; }

        /// <summary>Native PlaySound0x1a448 gates by g_fx.</summary>
        public readonly List<string> RequestedSounds = new List<string>();      // DEBUG_ONLY: every PlaySound request in order (capped)
        public readonly HashSet<string> MissingSounds = new HashSet<string>();   // DEBUG_ONLY: requested names that are not registered (silent no-op natively would be a CreateAudio gap)
        public void PlaySound(string key)
        {
            Ensure(); LoadNativeAudioTable();
            if (RequestedSounds.Count < 4096) RequestedSounds.Add(key);
            if (profile.fx == 0) return;
            string resource;
            if (!clips.TryGetValue(key, out resource)) { MissingSounds.Add(key); return; }
            AudioClip c = Resources.Load<AudioClip>(resource);
            if (c == null) return;
            sfx.PlayOneShot(c); SoundsPlayed++;
        }
    }

    /// <summary>Equivalent of Triniti2D::Scene + CppSceneCallBack as used by DinoCap::menuScene/selectScene.</summary>
    public abstract class Dc2Scene : MonoBehaviour
    {
        public Dc2Game Game;
        public AudioMgr Audio { get { return Game.Audio; } }
        public Profile Profile { get { return Game.Profile; } }

        readonly Dictionary<int, UiControl> controls = new Dictionary<int, UiControl>();
        readonly List<UiLabel> labels = new List<UiLabel>();
        int seq;

        public int NextSequence() { return seq++; }

        // ---- UIManager layer vectors (NATIVE, r118z L6): layers +944 (count +940), each a std::vector<UIControl*> in insertion order; labels are UIControls too.
        // CreateControl/CreateLabel push_back, DeleteControl 0x181460 erases at once; DoUpdate 0x1811f0 walks layers 0..n and indices 0..size (size re-read: a
        // control deleted during the pass shifts the rest, one created in the current or a later layer is updated in the same pass); HandleUITouch 0x18102e
        // walks layers n..0 and indices size-1..0. Draw order = layer, then index (UIManager::DoRender), here sortingOrder = base + layer * stride + per-layer count.
        public const int UiLayerStride = 2800;
        // keyed by layer; negative keys are the port's below-UI stand-ins for world GameObject sprites (SelectBank/SelectHero/SelectJigsaw backgrounds)
        readonly SortedDictionary<int, List<Component>> uiLayers = new SortedDictionary<int, List<Component>>();
        readonly Dictionary<int, int> uiLayerCount = new Dictionary<int, int>();
        internal int RegisterUi(Component c, int layer)
        {
            List<Component> list;
            if (!uiLayers.TryGetValue(layer, out list)) { list = new List<Component>(); uiLayers[layer] = list; uiLayerCount[layer] = 0; }
            list.Add(c);
            int stride = layer < 0 ? 100 : UiLayerStride;   // negative (below-UI world stand-in) layers keep the established 100 stride so they stay in 700..999
            if (uiLayerCount[layer] >= stride - 1)
            {
                for (int i = 0; i < list.Count; i++) ApplyUiOrder(list[i], UiControl.UiOrderBase + layer * stride + i);   // keep the relative order, restart the counter
                uiLayerCount[layer] = list.Count;
                return UiControl.UiOrderBase + layer * stride + list.Count - 1;
            }
            return UiControl.UiOrderBase + layer * stride + uiLayerCount[layer]++;
        }
        static void ApplyUiOrder(Component c, int order) { var uc = c as UiControl; if (uc != null) uc.SetUiOrder(order); else { var ul = c as UiLabel; if (ul != null) ul.SetUiOrder(order); } }
        internal void UnregisterUi(Component c) { foreach (var l in uiLayers.Values) if (l.Remove(c)) return; }
        /// <summary>True between the pending-object merge and the end of the object updates (L1): a GameObject created now joins the map next tick.</summary>
        public bool InUpdateStage { get; private set; }
        public IEnumerable<UiControl> Controls { get { return controls.Values; } }
        public IList<UiLabel> Labels { get { return labels; } }
        /// <summary>ADAPTATION (WideView): re-place every control and label after the wide-view extra width changed (render only).</summary>
        public virtual void ReapplyWide() { foreach (var c in controls.Values) if (c != null) c.ReapplyWide(); foreach (var l in labels) if (l != null) l.ReapplyWide(); }

        public UiControl CreateControl(int id, int layer)
        {
            DeleteControl(id);
            var c = UiControl.Create(this, id, layer);   // registers itself in the layer vector
            controls[id] = c;
            return c;
        }

        public UiControl GetControl(int id)
        {
            UiControl c;
            return controls.TryGetValue(id, out c) ? c : null;
        }

        public void DeleteControl(int id)
        {
            UiControl c;
            if (controls.TryGetValue(id, out c))
            {
                controls.Remove(id); UnregisterUi(c);
                Util.Kill(c.gameObject);
            }
        }

        public UiLabel CreateLabel(int id, int layer, int pixelSize)
        {
            var l = UiLabel.Create(this, id, layer, Resources.Load<Font>("DC2/Font/Adventure"), pixelSize);
            labels.Add(l);
            return l;
        }

        public void DeleteLabel(int id)
        {
            for (int i = labels.Count - 1; i >= 0; i--)
                if (labels[i].id == id) { UnregisterUi(labels[i]); Util.Kill(labels[i].gameObject); labels.RemoveAt(i); }
        }

        /// <summary>Scene::SetTimer 0x175648 (data, seconds, repeat=false) on the Application timer (NativeTimer): the deadline is now + seconds and the timer joins
        /// the heap at the start of the next Timer::Update. The returned info is the native timer id the scene keeps for its OnTimeout compare / KillTimer.</summary>
        public NativeTimer.Info SetTimer(string name, float seconds) { return Game.Timer.Set(this, name, seconds, false, null); }
        /// <summary>Owner-held timer id with its own handler (GameObject::SetTimer 0x16c826 / scene timers whose id is compared in OnTimeout); fires in the Timer::Update stage.</summary>
        public NativeTimer.Info SetTimer(object owner, string name, float seconds, Action handler) { return Game.Timer.Set(this, owner, name, seconds, false, handler); }
        /// <summary>Scene::KillTimer 0x175694 / GameObject::KillTimer 0x16c8e2: queued on the kill deque, applied at the start of the next Timer::Update.</summary>
        public void KillTimer(NativeTimer.Info t) { Game.Timer.Kill(t); }
        internal void FireTimer(NativeTimer.Info t) { if (t.action != null) t.action(); else if (t.owner == (object)this) OnTimeout(t.name); }

        public virtual void OnLoad() { }
        public virtual void OnUnload() { }
        protected virtual void OnUpdate(float dt) { }
        protected virtual void OnObjectUpdate(float dt) { }
        protected virtual void OnTimeout(string name) { }
        protected virtual void OnUIEvent(UiControl c, int ev) { }
        protected virtual void OnTouchEvent(int type, Vector2 p) { }
        /// <summary>Scene::OnTouchEvent(finger_id, type, x, y): scenes that track fingers override this; the default drops the id.</summary>
        protected virtual void OnTouchEvent(int type, Vector2 p, int finger) { OnTouchEvent(type, p); }
        public virtual bool CanTouch(UiControl c) { return true; }

        public void OnUIEventInternal(UiControl c, int ev) { OnUIEvent(c, ev); }

        /// <summary>Scene::Update@0x175e84: timers, scene callback, game objects, UI updates.
        /// World rendering occurs before UI rendering; Unity supplies the final renderer.
        /// </summary>
        public void Step(float dt)
        {
            // Application Timer is shared by scenes; native Scene::Update scales its dt before physics/objects.
            var battle=this as GameScene;
            // Scene::Update 0x175ed0/0x175ed2 caches the time factor (Scene+0x82C) BEFORE it reads the touches (GetTouchs 0x175eea); timers (0x175f5c..0x175f7c),
            // physics (0x17610a) and object callbacks (0x176254) all use that cached factor, so a pause / resume touch takes effect on the next tick (Codex
            // review R3, r118z5; the port froze the pressing tick). Pause (SetTimeFactorEx 0) freezes timers too; the UI keeps raw dt (UIManager::DoUpdate).
            TickPaused = battle != null && battle.paused;
            Game.DrainTouches(this);   // iPhoneInput::GetTouchs + dispatch inside Scene::Update, after Game::Update applied any queued scene switch (Codex review R2)
            float timerDt = TickPaused ? 0f : dt;
            Game.Timer.Update(timerDt, FireTimer);   // one Application timer (TimerProxy), exact PriorityQueueT order (NativeTimer, DC2-ENGINE-001 L2)
            // Scene::Update 0x175e84 merges the pending GameObject set (+1996) right after Timer::Update; an object created later in the tick (scene OnUpdate,
            // physics callbacks, another object's OnUpdate) is only inserted at the next Scene::Update and gets its first Update then.
            InUpdateStage = true;
            OnUpdate(dt);
            OnObjectUpdate(dt);
            InUpdateStage = false;
            // UIManager::DoUpdate 0x1811f0 over the live layer vectors (r118z L6; supersedes the sorted snapshot that still updated deleted controls and skipped new ones).
            foreach (var list in new List<List<Component>>(uiLayers.Values))   // layer order; a layer created during the pass joins next tick (native layers are fixed)
            {
                for (int j = 0; j < list.Count; j++)
                {
                    var uc = list[j] as UiControl;
                    if (uc != null) uc.UpdateMove(dt); else { var ul = list[j] as UiLabel; if (ul != null) ul.UpdateMove(dt); }
                }
            }
        }

        /// <summary>Native UIManager reverse layer/insertion dispatch, then scene fallback
        /// only when no UI control returned nonzero. Press is not click consumption.
        /// </summary>
        public void Touch(int type, Vector2 p) { Touch(type, p, 0); }
        /// <summary>True when a visible, touchable UI control covers p (PLATFORM_ADAPTER helper: battle left click = UI click there, shoot elsewhere).</summary>
        public bool UiButtonAt(Vector2 p)
        {
            foreach (var c in controls.Values)
                if (c != null && c.touchable && c.visible && c.FullRect.Contains(p)) return true;
            return false;
        }
        /// <summary>The time-factor snapshot of this Scene::Update (true = factor 0, paused at tick entry).</summary>
        public bool TickPaused { get; private set; }
        public int DebugSceneTouches;
        public void Touch(int type, Vector2 p, int finger)
        {
            // UIManager::HandleUITouch 0x18102e: layers top-down, indices last-to-first, live vectors; the first control that handles the touch ends it.
            var layerLists = new List<List<Component>>(uiLayers.Values);
            for (int L = layerLists.Count - 1; L >= 0; L--)
            {
                var list = layerLists[L];
                for (int j = list.Count - 1; j >= 0; j--)
                {
                    if (j >= list.Count) continue;   // a handler deleted controls of this layer (native reads stale slots: UB, not reproduced)
                    var lb = list[j] as UiLabel;
                    if (lb != null) { if (lb.ConsumesTouch(p)) return; continue; }   // UILabel::HandleTouch 0x17fd98 (r118z4)
                    var c = list[j] as UiControl;
                    if (c == null || !CanTouch(c)) continue;
                    if (c.HandleTouch(type, p)) return;
                }
            }
            DebugSceneTouches++;   // DEBUG_ONLY counter (PilotUiTouchVerify)
            OnTouchEvent(type, p, finger);
        }

        protected Rect Px(float x, float y, float w, float h) { return N.Layout(x, y, w, h); }
    }

    /// <summary>Game::SwitchScene + app loop. Drives scenes with a fixed or real dt.</summary>
    public class Dc2Game : MonoBehaviour
    {
        public Profile Profile;
        public AudioMgr Audio;
        public Dc2Scene Current;
        public string CurrentName;
        public string SaveDir;
        public int OngameSound, OngameFx;   // native g_ongame_sound / g_ongame_fx
        public int MapType;                 // native DinoCap::m_map_type
        public NativeMapState.Mission LaunchMission;   // selected Map.xml record (enemy kind list, level, count)
        public readonly List<int> JigsawLevels = new List<int>();   // DinoCap::g_jigsaw_levels (0x240a6c): level of the last 3 launched missions (selectScene OnUIEvent 0x10b93a); not saved (no SaveFile xref)
        public int LaunchMissionType;       // selected map record type (+8), carried across the selectScene reload
        public int LastMapType;             // native dword_23E0A4: screen the bank returns to (0 map, 1 armory, 2 hero)
        public NativeMapState MapState;     // native global map/position vectors live across menu scenes.
        public bool manualStep;           // verification harness steps time explicitly
        // DEBUG_ONLY (DC2-BUGS-001 O2 debug menu, F1): inert unless set from the menu; never saved, never read by native-path code otherwise.
        public bool DebugGodMode, DebugInfiniteAmmo, DebugMenuOpen;
        /// <summary>DEBUG_ONLY (DC2-BUGS-001 r119i, user: debug level-up should unlock weapons at once, on the reload of any menu tab): heroes levelled by the
        /// debug button since their last native unlock. Native unlocks only at mission complete (AwardItem 0xd09dc); this set lets SelectScene.OnLoad run the same unlock
        /// early for those heroes only, so normal play keeps the native rule. Not saved.</summary>
        public readonly HashSet<int> DebugLevelUnlockPending = new HashSet<int>();
        public float DebugTimeScale = 1f;
        public bool SkipMissionIntro;     // DEBUG_ONLY: verification harnesses start missions in state 4 (skips the native OnLoad intro: LevelBGEnter .. StartGame_N)
        /// <summary>PLATFORM_ADAPTER, OFF by default (needs the user's approval): maps losing window focus onto the native system event 0 (iOS interruption). The native handler itself is GameScene.OnSystemEvent (NATIVE, mainScene 0x9636a / lifeModeScene 0xd0958).</summary>
        public bool pauseOnFocusLoss;
        void OnApplicationFocus(bool focus)
        {
            // r119u NATIVE on mobile: -[EngineAppDelegate applicationWillResignActive:] 0x182ec4 calls the shell vtable+0x1C (EngineShell::SystemEvent 0x165b9a, vtable 0x22e1c8)
            // with 0 -> Application/Game/Scene SystemEvent -> OnSystemEvent(0), then clears _run (the update loop stops until applicationDidBecomeActive). Unity reports
            // resign-active as focus loss on iOS/Android. On desktop the same mapping stays the opt-in PLATFORM_ADAPTER pauseOnFocusLoss.
            if (!focus && (pauseOnFocusLoss || Application.isMobilePlatform) && !manualStep) { var gs = Current as GameScene; if (gs != null) gs.OnSystemEvent(0); }
            if (focus && Application.platform == RuntimePlatform.Android) Screen.fullScreen = true;   // PLATFORM_ADAPTER (user "immersive"): bars hidden again after returning
        }
        /// <summary>The Application Timer shared by every scene and game object (TimerProxy singleton); its clock is NativeTimerTime.</summary>
        public readonly NativeTimer Timer = new NativeTimer();
        public float NativeTimerTime { get { return Timer.Now; } }
        string pendingScene;

        public static Dc2Game Create(string saveDir)
        {
            var go = new GameObject("Dc2Game");
            var g = go.AddComponent<Dc2Game>();
            g.SaveDir = saveDir;
            g.Profile = Profile.Load(saveDir);
            g.Audio = go.AddComponent<AudioMgr>();
            g.Audio.profile = g.Profile;
            return g;
        }

        public void SaveFile() { Profile.Save(SaveDir); }

        public void SwitchScene(string name)
        {
            // Game::SwitchScene@0x16a74a queues; Game::Update drains before Scene::Update.
            if (Current == null) EnterScene(name);
            else pendingScene = name;
        }

        void EnterScene(string name)
        {
            // Game::Update 0x16a8da SwitchScene: Scene::Unload 0x175cce kills every scene timer (TimerProxy::SceneKillTimer) and unloads/deletes every GameObject
            // (GameObject::Unload 0x16c494 kills its timers), then calls the scene callback's OnUnload (+20); the new scene is created, loaded and updated in the same tick.
            if (Current != null) { Timer.KillOwnersOf(Current); Current.OnUnload(); Util.Kill(Current.gameObject); }
            var go = new GameObject("scene_" + name);
            go.transform.SetParent(transform, false);
            Dc2Scene s;
            switch (name)
            {
                case "menuScene": s = go.AddComponent<MenuScene>(); break;
                case "selectScene": s = go.AddComponent<SelectScene>(); break;
                case "gameScene": s = go.AddComponent<GameScene>(); break;
                default: throw new Exception("Unknown scene " + name);
            }
            s.Game = this;
            Current = s; CurrentName = name;
            s.OnLoad();
        }

        /// <summary>True after the first Application::Update (HostTick or a harness Step); see HostTick.</summary>
        public bool firstUpdateDone;
        public float DebugLastStepDt;   // DEBUG_ONLY verifier probe
        public void Step(float dt)
        {
            firstUpdateDone = true; DebugLastStepDt = dt;
            if (pendingScene != null)
            {
                string next = pendingScene;
                pendingScene = null;
                EnterScene(next);
            }
            Current.Step(dt);
        }

        // ---- input mapping: screen -> logical 1024x768 ----
        public Camera cam;

        public static Vector2 ScreenToLogical(Camera cam, Vector3 screen)
        {
            Vector3 w = cam.ScreenToWorldPoint(new Vector3(screen.x, screen.y, 0f));
            return new Vector2(w.x, -w.y);
        }

        bool down;
        public PcPlatform pc;   // PLATFORM_ADAPTER keyboard / window (null in verification harnesses)
        void Update()
        {
            if (manualStep || Current == null) return;
            if (!DebugMenuOpen)   // DEBUG_ONLY: the debug menu (F1) owns the mouse and keys while open
            {
                // r119u: the PC keyboard / mouse layer is off on phones and tablets. Unity simulates mouse button 0 from the first touch, so PumpMouse took every touch
                // outside a UI button as the shoot finger and GatherPointerInput then skipped the real touches (port defect; touch is the native input there).
                bool pcInput = pc != null && !Application.isMobilePlatform;
                if (pcInput) pc.PumpKeys(Current);
                bool mouseTaken = cam != null && pcInput && pc.PumpMouse(Current, ScreenToLogical(cam, Input.mousePosition));
                GatherPointerInput(mouseTaken);
            }
            HostTick(Time.unscaledDeltaTime * DebugTimeScale);   // DEBUG_ONLY game speed (1 unless changed in the debug menu): scales the real time fed to the native host clock
        }

        // ---- native host clock (PLATFORM_ADAPTER reproducing the iOS shell, r118z) ----
        // EngineAppDelegate applicationDidFinishLaunching 0x18365c: NSTimer scheduledTimerWithTimeInterval 1/60 (0x3F91111111111111) -> update ->
        // EngineShell::RenderScene 0x165bb4: dt = (PVRShellGetTime() - last) * 0.001, capped at 0.1 s -> Application::Update. Touches are queued by iOS
        // and handled at the start of Scene::Update. The game logic is not frame-rate independent at the animation level (CAObjCallBack drops the
        // accumulator residue, so a 0.025 s animation runs at 30 fps when ticked at 60 Hz), so ticking once per monitor frame made a 144/240 Hz PC
        // animate faster (port defect, PilotFrameRateVerify). The host now ticks at the native 60 Hz rate with dt = 1/60; when the PC falls behind
        // (two or more ticks due) it coalesces into one tick with the elapsed time capped at 0.1 s, as the NSTimer-driven shell does.
        public const float NativeTickInterval = 1f / 60f;
        double hostTime;                                  // real seconds since this game object started receiving host ticks
        double nextFire = NativeTickInterval;             // NSTimer fire date (first callback one interval after scheduling)
        long lastMs;                                      // EngineShell+0x10: PVRShellGetTime() of the previous RenderScene
        struct QueuedTouch { public int type, finger; public Vector2 p; }
        readonly List<QueuedTouch> touchQueue = new List<QueuedTouch>();
        /// <summary>iPhoneInput::AddTouch 0x16e7f4: appends {touch, type, x, y}; only when the last queued entry is the same touch with the same type and that type is 2
        /// (ended, -[EventEAGLView touchesEnded:] 0x183954) is it overwritten. Moves (1) are never merged. EventEAGLView has no touchesCancelled handler (iOS only).</summary>
        public void QueueTouch(int type, Vector2 p, int finger)
        {
            int last = touchQueue.Count - 1;
            if (last >= 0 && touchQueue[last].finger == finger && touchQueue[last].type == type && type == 2) { touchQueue[last] = new QueuedTouch { type = type, p = p, finger = finger }; return; }
            touchQueue.Add(new QueuedTouch { type = type, p = p, finger = finger });
        }

        /// <summary>Scene::Update 0x175eea..0x175f32: the queued touches go to the scene that is current after Game::Update's command loop (r118z5).</summary>
        public void DrainTouches(Dc2Scene scene)
        {
            if (touchQueue.Count == 0) return;
            var q = new List<QueuedTouch>(touchQueue); touchQueue.Clear();
            foreach (var t in q) scene.Touch(t.type, t.p, t.finger);
        }

        /// <summary>Host clock (r118z5, Codex review R1; supersedes the r118z fixed-1/60 dt).
        /// Scheduling (PLATFORM_ADAPTER): an NSTimer-like fire date on the 1/60 grid decides WHEN Application::Update runs; a late timer fires once and its
        /// next date is the first future grid point (10 % tolerance for vsync jitter).
        /// dt (NATIVE, EngineShell::RenderScene 0x165bb4): (unsigned ms now - ms last) converted to float * 0x3A83126F (0.001), capped at 0x3DCCCCCD (0.1);
        /// the first update gets 0.1 because EngineShell::InitView 0x165c30 stamps last = now - 1000. The elapsed time is never replaced by 1/60.</summary>
        public void HostTick(float realDt)
        {
            hostTime += realDt;
            if (hostTime < nextFire - NativeTickInterval * 0.1) return;
            do nextFire += NativeTickInterval; while (nextFire <= hostTime);
            long nowMs = (long)Math.Floor(hostTime * 1000.0);
            float dt;
            if (!firstUpdateDone) dt = 0.1f;
            else
            {
                float d = N.Mul32((float)(nowMs - lastMs), BitConverter.Int32BitsToSingle(0x3A83126F));
                dt = d > BitConverter.Int32BitsToSingle(0x3DCCCCCD) ? BitConverter.Int32BitsToSingle(0x3DCCCCCD) : d;   // __gtsf2 at 0x165bd6
            }
            lastMs = nowMs;
            Step(dt);
        }

        void GatherPointerInput(bool mouseTaken)
        {
            if (cam != null && !mouseTaken)
            {
                // PLATFORM_ADAPTER: iOS delivers one OnTouchEvent(finger_id, type 0 begin / 1 move / 2 end, x, y) per UITouch; Unity touches carry the
                // same per-finger stream (fingerId). Without a touch screen the mouse is finger 0.
                if (Input.touchCount > 0)
                {
                    for (int i = 0; i < Input.touchCount; i++)
                    {
                        var t = Input.GetTouch(i);
                        int type = t.phase == TouchPhase.Began ? 0 : t.phase == TouchPhase.Moved ? 1 : (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled) ? 2 : -1;
                        if (type >= 0) QueueTouch(type, ScreenToLogical(cam, t.position), t.fingerId);
                    }
                }
                else
                {
                    if (Input.GetMouseButtonDown(0)) { down = true; QueueTouch(0, ScreenToLogical(cam, Input.mousePosition), 0); }
                    else if (down && Input.GetMouseButton(0)) QueueTouch(1, ScreenToLogical(cam, Input.mousePosition), 0);
                    if (down && !Input.GetMouseButton(0)) { down = false; QueueTouch(2, ScreenToLogical(cam, Input.mousePosition), 0); }   // R002-N1: level-checked, so a release made while the F1 menu gated input is not lost
                }
            }
        }

        /// <summary>Verification harness: advance logical time in fixed steps.</summary>
        public void StepFor(float seconds, float step = 1f / 60f)
        {
            float t = 0f;
            while (t < seconds - 1e-6f) { Step(step); t += step; }
        }
    }
}
