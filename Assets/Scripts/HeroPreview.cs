using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Dc2Pilot
{
    /// <summary>Bounded selectScene A2B stand contract. Evidence: native_hero_rig*.r11.json.
    /// Girl uses the separate ComponentAnimation short-weapon stand path.</summary>
    public sealed class HeroPreview : MonoBehaviour
    {
        public NativeCaPlayer Player { get; private set; }
        public int HeroType { get; private set; }
        public string Source { get; private set; }
        public NativeComponentPlayer ComponentPlayer { get; private set; }
        GameObject componentRoot;

        public static HeroPreview Create(Transform parent, int drawIndex = -1)
        {
            var go = new GameObject("native_hero_preview"); go.transform.SetParent(parent, false);
            go.AddComponent<SortingGroup>().sortingOrder = drawIndex >= 0 ? NativeDrawOrder.CaGroup(NativeDrawOrder.Base(drawIndex)) : -1; // native world before UI; gameplay: CAObj creation index (NativeDrawOrder)
            var result = go.AddComponent<HeroPreview>();
            result.Player = new NativeCaPlayer(go.transform, _ => go.transform, 50);
            return result;
        }

        /// <summary>Gameplay locomotion pose (playerClass::Move 0xb8900 plays "playerMoveAnimation" loop interval 0.025).
        /// Move a2b names from the loader manifest (p_move/pd_move/p4_move); hero1 re-skins the p_ rig (PlayerChangeImage case 2).
        /// Girl (4): never requested - gameplay routes the girl through GameGirl (playerNewAniClass CompAnimObj rig) before any HeroPreview pose call, and the tavern preview only stands (r118x: old OPEN row closed).</summary>
        public void SelectMove(int type, bool preserveEmission = false)
        {
            if (type == 4) { Select(type); return; }
            HeroType = type;
            if (!preserveEmission) Player.Hide();
            string[] moves = { "p_move", "p_move", "pd_move", "p4_move" };
            Source = moves[type];
            var animation = NativeCaAnimation.Load("DC2/Image/an2/" + Source + ".a2b", true).Clone();
            if (type == 1) Skin(animation, "0002", false, false);
            if (type == 2) Skin(animation, "0003", true, false);
            if (type == 3) Skin(animation, "0004", false, true);
            if (type == 0) animation.ChangeImage("com1", "A1");
            if (type == 2) animation.ChangeImage("yyj5", "D3");
            if (type == 3) animation.ChangeImage("com15", "shoulijian01");
            Player.Play(animation, -1, 0, 0.025f);
        }

        /// <summary>Gameplay pose from a named a2b family: fire (key frame 3 per GenComAnim SetKeyFrame(3)), behit (key 7), roll etc.
        /// Hero 1 re-skins the p_ rig; Girl (4) poses are never requested here (GameGirl owns them; r118x).</summary>
        public void SelectPose(int type, string pose, int loops, float interval, int keyFrame, int endFrame = -1, bool preserveEmission = false)
        {
            if (type == 4) { Select(type); return; }
            HeroType = type;
            if(!preserveEmission)Player.Hide();
            string[] prefix = { "p_", "p_", "pd_", "p4_" };
            Source = prefix[type] + pose;
            var animation = NativeCaAnimation.Load("DC2/Image/an2/" + Source + ".a2b", true).Clone();
            if (type == 1) Skin(animation, "0002", false, false);
            if (type == 2) Skin(animation, "0003", true, false);
            if (type == 3) Skin(animation, "0004", false, true);
            if (type == 0) animation.ChangeImage("com1", "A1");
            if (type == 2) animation.ChangeImage("yyj5", "D3");
            if (type == 3) animation.ChangeImage("com15", "shoulijian01");
            if (keyFrame >= 0) animation.MarkKeyFrame(keyFrame);
            Player.Play(animation, loops, 0, interval, endFrame);
        }

        /// <summary>Pose from an explicit a2b file name (e.g. p_roll, p_hit) with several key frames marked.</summary>
        public void SelectNamedPose(int type, string file, int loops, float interval, int[] keyFrames, bool preserveEmission = false)
        {
            if (type == 4) { Select(type); return; }
            HeroType = type;
            if(!preserveEmission)Player.Hide();
            Source = file;
            var animation = NativeCaAnimation.Load("DC2/Image/an2/" + Source + ".a2b", true).Clone();
            if (file.StartsWith("p2_roll_") || file.StartsWith("p4_roll_")) RollSkin(animation, file);
            else
            {
                if (type == 1) Skin(animation, "0002", false, false);
                if (type == 2) Skin(animation, "0003", true, false);
                if (type == 3) Skin(animation, "0004", false, true);
                if (type == 0) animation.ChangeImage("com1", "A1");
                if (type == 2) animation.ChangeImage("yyj5", "D3");
                if (type == 3) animation.ChangeImage("com15", "shoulijian01");
            }
            if (keyFrames != null) foreach (int k in keyFrames) animation.MarkKeyFrame(k);
            Player.Play(animation, loops, 0, interval, -1);
        }

        /// <summary>Loader cases 18 / 20 (sub_69DE6 0x6a4b8 / 0x6a612): p4_roll_1/2 (g_ca_4pBaseRollData / Roll2Data) are used as loaded; p2_roll_1/2
        /// (g_ca_2pBaseRollData / Roll2Data) get PlayerChangeImage(10) 0xce12 (com1 tou0002, com2 shenti0002, com3/com4 tui0002, com5/com6 koushou0002) and
        /// SetSpriteAnim("com8"). r119d (user item 9): the raw p2 files carry the Ninja images (tou0004 ...) and the port skinned them with the hero-1 yyj recipe,
        /// which matches no part, so Force Knight rolled with the Ninja's art.</summary>
        static void RollSkin(NativeCaAnimation animation, string file)
        {
            if (!file.StartsWith("p2_roll_")) return;
            animation.ChangeImage("com1", "tou0002"); animation.ChangeImage("com2", "shenti0002");
            animation.ChangeImage("com3", "tui0002"); animation.ChangeImage("com4", "tui0002");
            animation.ChangeImage("com5", "koushou0002"); animation.ChangeImage("com6", "koushou0002");
            animation.SetSpriteAnimation("com8");
        }

        public void Select(int type, bool preserveEmission = false)
        {
            HeroType = type;
            if (!preserveEmission) Player.Hide();
            if (componentRoot != null) Util.Kill(componentRoot);
            ComponentPlayer = null;
            if (type == 4)
            {
                // InitNewCharacter: slot0 type0 -> ChangeWeapon short family, bshouqiang default avatar.
                Source = "player_stand_short";
                componentRoot = new GameObject("native_component_hero"); componentRoot.transform.SetParent(transform, false);
                var stand = NativeComponentAnimation.Load("DC2/Image/acb/ipad/player_stand_short.animation.acb");
                ComponentPlayer = new NativeComponentPlayer(stand, componentRoot.transform, new Vector2(-232f, -162f), 1.5f);
                return;
            }
            if ((uint)type > 3) throw new ArgumentOutOfRangeException(nameof(type));
            string[] sources = { "p_stand", "p_stand_j", "pd_stand", "p4_stand" };
            Source = sources[type];
            var animation = NativeCaAnimation.Load("DC2/Image/an2/" + Source + ".a2b", true).Clone();
            if (type == 1) Skin(animation, "0002", false, false);
            if (type == 2) Skin(animation, "0003", true, false);
            if (type == 3) Skin(animation, "0004", false, true);
            // InitType: record0 normally, record2 for Soldier, record4 for Ninja.
            // Hero1's special p_stand_j override bypasses ChangeWeapon's stand clone.
            if (type == 0) animation.ChangeImage("com1", "A1");
            if (type == 2) animation.ChangeImage("yyj5", "D3");
            if (type == 3) animation.ChangeImage("com15", "shoulijian01");
            Player.position = new Vector2(-240f, type == 0 ? 191f : 190.75f);
            Player.objectScale = 1.5f;
            Player.Play(animation, -1, 0, 0.025f);
        }

        public void SelectNativeGroundRoll(int type)
        {
            if(type!=0&&type!=2)throw new ArgumentOutOfRangeException(nameof(type));
            HeroType=type;Source="p_roll.an2 Expand1.5";
            var animation=NativeXmlCaAnimation.Load("DC2/Image/an2/p_roll.an2",1.5f);
            if(type==2)
            {
                // Native PlayerChangeImage5, not the ordinary second-layer/weapon pose recipe.
                animation.ChangeImage("yyj1","tui0003");animation.ChangeImage("yyj2","tui0003");
                animation.ChangeImage("yyj3","shou20003");animation.ChangeImage("yyj4","shenti0003");
                animation.ChangeImage("yyj5","koushou0003");animation.ChangeImage("yyj6","tou0003");
                animation.ChangeImage("roll","p3move");
            }
            animation.MarkKeyFrame(7);animation.MarkKeyFrame(18);
            Player.Play(animation,0,0,.025f);
        }

        public void SelectNativeHurt(int type,int slot)
        {
            HeroType=type;Source=NativeHeroHurt.Get(type,slot).source;
            Player.Play(NativeHeroHurt.Load(type,slot),0,0,.025f); // native next emission; keep already emitted sprites.
        }
        public void SelectNativeNinjaMelee()
        {
            SelectNativeMelee(3);
        }
        public void SelectNativeMelee(int type)
        {
            HeroType=type;Source=NativeHeroMelee.Get(type).source;
            Player.Play(NativeHeroMelee.Load(type),0,0,.025f);
        }
        public void SelectNativeLocomotion(int type,int slot,string pose)
        {
            HeroType=type;Source=NativeHeroLocomotionRecipes.Get(type,slot,pose).source;
            Player.Play(NativeHeroLocomotionRecipes.Load(type,slot,pose),-1,0,.025f);
        }
        public void RebindNativeLocomotion(int type,int slot,string pose)
        {
            HeroType=type;Source=NativeHeroLocomotionRecipes.Get(type,slot,pose).source;
            Player.animation=NativeHeroLocomotionRecipes.Load(type,slot,pose); // setAnimation changes pointer, retains clock/cursor/end.
        }
        public void RebindNativeHurt(int type,int slot)
        {
            HeroType=type;Source=NativeHeroHurt.Get(type,slot).source;
            Player.animation=NativeHeroHurt.Load(type,slot); // embedded animation480 updates in place; preserve cursor/accumulator/last key.
        }

        /// <summary>playerClass::Die 0xbcee4: g_ca_pDieData / 2p / 3p / 4p per g_player_type, played (0, 0, 0.025, -1) with no advanced layer. All four are
        /// Image/an2/p_die.a2b (CombineAnimation::LoadXmlFromFile 0x6a8dc, clones 0x6a904..0x6a954) with PlayerChangeHead(9 + type) 0xd4a8 (r119: the port asked
        /// for pd_die / p4_die, which do not exist, and HeroDie threw for heroes 3 and 4, so no failed screen followed).</summary>
        public void SelectDie(int type)
        {
            HeroType = type; Player.Hide(); Source = "p_die";
            var animation = NativeCaAnimation.Load("DC2/Image/an2/p_die.a2b", true).Clone();
            string n = "000" + (type + 1);
            string[] dieHead = { "tou0012", "tou0009", "tou0008", "tou0010" };   // PlayerChangeHead cases 9..12
            animation.ChangeImage("yyj1", "tui" + n); animation.ChangeImage("yyj2", "tui" + n);
            animation.ChangeImage("yyj3", "shou2" + n); animation.ChangeImage("yyj4", "shenti" + n);
            animation.ChangeImage("yyj5", "koushou" + n); animation.ChangeImage("yyj6", dieHead[type]);
            Player.Play(animation, 0, 0, 0.025f);
        }

        static void Skin(NativeCaAnimation animation, string suffix, bool twoHands, bool ninjaStand)
        {
            // PlayerChangeImage cases2,6,11. Replacements leave native pose dimensions intact.
            animation.ChangeImage("yyj1", "tui" + suffix);
            animation.ChangeImage("yyj2", "tui" + suffix);
            animation.ChangeImage("yyj3", (ninjaStand ? "koushou" : "shou2") + suffix);
            animation.ChangeImage("yyj4", "shenti" + suffix);
            if (twoHands) animation.ChangeImage("com1", "shou" + suffix);
            else animation.ChangeImage("yyj5", "koushou" + suffix);
            animation.ChangeImage("yyj6", "tou" + suffix);
        }

        public void Step(float dt) { Player.Step(dt); ComponentPlayer?.Step(dt); }
    }
}
