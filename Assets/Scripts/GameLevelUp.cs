using UnityEngine;

namespace Dc2Pilot
{
    /// <summary>
    /// lifeModeScene::LevelUpEffect 0xd0024 (NATIVE): levelupAudio plus three hero-following effectClass objects (type 12, layer 14; CA
    /// g_ca_levelup_1/2/3_EFF = Image/an2/levelup_eff1..3.a2b, "eff1Animation", one shot, playAnimation(0,0,.025)) and one effectClassN
    /// (ComponentAnimation g_ca_levelup_4_EFF = Image/acb/level_up.animation.acb, "eff4Animation", CompAnimObj scale S).
    /// Each frame effectClass::OnUpdate (case 12) sets the object to the hero position plus its offset; OnAnimationEnd disappears it.
    /// PILOT_ASSUMPTION: the CA files are loaded here on demand with the loader's XML-variant semantics and g_scale 1.0.
    /// </summary>
    public partial class GameScene
    {
        sealed class LevelUpFx
        {
            public NativeCaPlayer[] ca = new NativeCaPlayer[3];
            public Vector2[] caOffset = new Vector2[3];
            public bool[] caDone = new bool[3];
            public bool[] caSkip = new bool[3];   // a NEW effectClass object waits in the pending set until next tick (r118z2)
            public PooledEffect[] records = new PooledEffect[3];
            public NativeComponentPlayer comp;
            public Vector2 compOffset;
            public GameObject root;
        }
        LevelUpFx levelUpFx;
        public bool LevelUpEffectActive { get { return levelUpFx != null; } }

        void StartLevelUpEffect()
        {
            Audio.PlaySound("levelupAudio");
            if (levelUpFx != null) { for (int k = 0; k < 3; k++) ReturnEffectRecord(levelUpFx.records[k]); Object.Destroy(levelUpFx.root); }
            float s = N.S; bool girl = heroType == 4;
            var fx = new LevelUpFx { root = new GameObject("levelup_fx") };
            fx.root.transform.SetParent(Background.LayerRoot(14), false);
            Vector2[] offsets = girl
                ? new[] { new Vector2(-10f, N.Mul32(s, 150f)), new Vector2(-10f, N.Mul32(s, 155f)), new Vector2(N.Mul32(s, -40f), N.Mul32(s, 155f)) }
                : new[] { new Vector2(0f, N.Mul32(s, -5f)), Vector2.zero, new Vector2(N.Mul32(s, -30f), N.Mul32(s, 35f)) };
            for (int i = 0; i < 3; i++)
            {
                int index = i;
                var animation = NativeCaAnimation.Load("DC2/Image/an2/levelup_eff" + (i + 1) + ".a2b", true, 1f);
                var player = new NativeCaPlayer(fx.root.transform, _ => fx.root.transform, 0);
                player.Play(animation, 0, 0, .025f);
                bool reusedRecord = effectPool.Count > 0;
                var record = TakeEffectRecord(); fx.records[i] = record;
                fx.caSkip[i] = InUpdateStage && !reusedRecord && !fxStagePassed;   // GetEffect + SetRotation(0) (0x958d4/0x959b4/0x95a8c): shares the effectClass pool with blood/muzzle flash/etc.
                player.animationEnd = () => { fx.caDone[index] = true; player.Hide(); ReturnEffectRecord(fx.records[index]); fx.records[index] = null; };   // effectClass::OnAnimationEnd default -> Disappear (pool push)
                fx.ca[i] = player; fx.caOffset[i] = offsets[i];
            }
            fx.compOffset = girl ? new Vector2(N.Mul32(s, 5f), N.Mul32(s, 72f)) : new Vector2(0f, N.Mul32(s, heroType == 0 ? -50f : -60f));
            var comp = NativeComponentAnimation.Load("DC2/Image/acb/level_up.animation.acb");
            fx.comp = new NativeComponentPlayer(comp, fx.root.transform, Rig.Player.position + fx.compOffset, s) { oneShot = true };
            levelUpFx = fx;
            UpdateLevelUpEffect(0f);
        }

        /// <summary>Initial placement at creation (SetPosition before the first update).</summary>
        void UpdateLevelUpEffect(float dt)
        {
            var fx = levelUpFx; if (fx == null) return;
            for (int i = 0; i < 3; i++) if (!fx.caDone[i]) { fx.ca[i].position = Rig.Player.position + fx.caOffset[i]; fx.ca[i].Step(dt); }
            if (!fx.comp.Finished) { fx.comp.position = Rig.Player.position + fx.compOffset; fx.comp.Step(dt); }
        }

        /// <summary>One of the three GetEffect objects (id 5): stepped by the id-5 walk at its pool record's slot (r118z2; was the start of CombatUpdate).</summary>
        void StepLevelUpCa(int i, float dt)
        {
            var fx = levelUpFx; if (fx == null || fx.caDone[i]) return;
            if (fx.caSkip[i]) { fx.caSkip[i] = false; return; }
            fx.ca[i].position = Rig.Player.position + fx.caOffset[i];   // effectClass::OnUpdate case 12: hero position + offset
            fx.ca[i].Step(dt);
            ReleaseLevelUpIfDone();
        }

        /// <summary>The effectClassN object (id 0, new at each LevelUpEffect): stepped in the pre-hero id-0 pass, so it reads the hero position before the hero moves.</summary>
        void StepLevelUpComp(float dt)
        {
            var fx = levelUpFx; if (fx == null || fx.comp.Finished) return;
            fx.comp.position = Rig.Player.position + fx.compOffset;
            fx.comp.Step(dt);
            ReleaseLevelUpIfDone();
        }

        void ReleaseLevelUpIfDone()
        {
            var fx = levelUpFx; if (fx == null) return;
            for (int i = 0; i < 3; i++) if (!fx.caDone[i]) return;
            if (!fx.comp.Finished) return;
            Object.Destroy(fx.root); levelUpFx = null;
        }
    }
}
