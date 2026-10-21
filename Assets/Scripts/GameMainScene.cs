using UnityEngine;

namespace Dc2Pilot
{
    /// <summary>mainScene missions: run (type 1) and egg (type 5). Evidence: Dev/Evidence/mainScene_decomp_r107.md.</summary>
    public partial class GameScene
    {
        UiControl runLine, runPoint;
        public float RunTotal { get { return BoundUpper; } }                 // scene+140
        public bool RunMission { get { return Game.LaunchMission != null && Game.LaunchMission.type == 1; } }
        public bool runEnded;                                                 // playerClass::UpdateCamera run-end branch executed

        /// <summary>mainScene::OnLoad HUD additions: run = progress line/marker (ids 37/38); egg = egg icon (47) + count label (48).</summary>
        void BuildMainSceneHud(Color32 yellow)
        {
            if (!MainSceneMission) return;
            if (Game.LaunchMission.type == 5) EggLoadAssets();   // eggs2x atlas must exist before the HUD egg icon
            if (Game.LaunchMission.type == 1)
            {
                runLine = Image(37, 0, "icon_move_lineFrame", 301, 5, 141, 28);
                runPoint = Image(38, 1, "icon_move_pointFrame", 290, 16, 17, 15);
            }
            else
            {
                Image(47, 0, "eggs_egg_aFrame", 300, 5, 24, 30);
                eggLabel = CreateLabel(48, 0, 30); eggLabel.SetRect(N.Layout(330, 5, 200, 30)); eggLabel.SetColor(new Color32(239, 205, 0, 255)); eggLabel.SetNative("Adventure_16", 0f, 0f, 0, 0);
            }
        }
        UiLabel eggLabel;

        /// <summary>mainScene::OnUpdate 0x979d2 progress marker: x = S*((420-290)*hero.x/total, clamped at 0) + RectOffset(RectScale(290,16,17,15)).x.</summary>
        void UpdateRunProgress()
        {
            if (runPoint == null) return;
            float t = N.Div32(N.Mul32(N.Add32(420f, -290f), heroX), RunTotal);
            if (t < 0f) t = 0f;
            Rect r = N.Layout(290f, 16f, 17f, 15f);
            runPoint.SetRect(new Rect(N.Add32(N.Mul32(t, N.S), r.x), r.y, r.width, r.height));
        }

        /// <summary>playerClass::UpdateCamera run branch (mode &lt;= 1), evaluated EVERY frame while hero.x &gt; S*-180 + scene+140 (no once-only guard, bb22e..bb2c6):
        /// +657 = 0, StopCreateEnemy, flip 0 (face right), velocity = walk speed, Move() if the state already is 4, state = 4, scene GameClear (vtbl+0x38; only the first
        /// call acts: control 20 exists afterwards), then OnFireUp if firing and g_b_FireDown = 0. The first frame's GameClear stands the hero and zeroes the velocity,
        /// the second frame restores the walk velocity (state 2 -> no Move), the third frame calls Move(): the walk-out starts two frames late (shipped quirk).</summary>
        // ---- DEBUG_ONLY (debug menu) ----
        /// <summary>Kills the hero through the normal death path (HeroDie: die pose, blood, then the native failed sequence). Ignores god mode on purpose.</summary>
        public void DebugKillHero() { if (heroDead) return; playerHp = 0f; UpdateHpBar(); HeroDie(); }
        /// <summary>Endurance (run, type 1): places the hero 300 px before the finish test (hero.x > S*-180 + total, RunEndCheck) and recentres the camera.</summary>
        public bool DebugTeleportNearRunEnd()
        {
            if (!RunMission || runEnded || heroDead) return false;
            heroX = N.Add32(N.Add32(N.Mul32(N.S, -180f), RunTotal), -300f);
            camLeft = HeroCameraLeft(); nativeCamera.left = camLeft;
            return true;
        }

        void RunEndCheck()
        {
            if (Game.LaunchMission.type != 1 || heroDead) return;   // heroDead: PILOT guard (native has none; a dying hero cannot cross the line in the port)
            if (heroX > N.Add32(N.Mul32(N.S, -180f), RunTotal))   // UpdateCamera bb22e: S*-180 + scene+140 (ARM literal 0xc3340000; r107 note of -100 was a misread)
            {
                runEnded = true;
                heroInputEnabled = false;                                 // bb254: +657 = 0
                StopCreateEnemy();
                facingLeft = false;                                       // bb262: flip (+96) = 0
                SetBodyVelocity(WalkSpeedFor(heroType));                  // bb276..bb286: velocity (g_prop_Player walk speed, 0)
                if (IsGirl)
                {   // playerNewAniClass::UpdateCamera 0x127582..0x12758e: Move() unless the state is 1 or 4 (Move 0x125072 restarts "move" unconditionally)
                    if (heroControlState != 1 && heroControlState != 4) { rigState = 1; GirlMove(); }
                }
                else if (heroControlState == 4) SetRigState(1, true);     // bb28e: Move() only when the state already is 4 (Move keeps a running move animation)
                heroControlState = 4;
                Complete();                                               // bb2a8: vtbl+0x38 = mainScene::GameClear 0xb7f5e (sees the roll: a hidden teleport stays hidden)
                rolling = false;                                          // the single native state field is 4 (girl) / 2 (GameClear Stand): no longer state 7; a pending "roll" timer still fires
                if (IsGirl) firing = false;                               // 0x1275ae..0x1275bc: +357 = 0, g_b_FireDown = 0 (no OnFireUp)
                else if (firing) { ClearNativeFireChannel(); firing = false; } // bb2ae..bb2be: OnFireUp when +656, +656 = 0
                fireDown = false;                                         // bb2c6
            }
        }

        /// <summary>UpdateCamera bb2d2..bb30e (every frame, mode &lt;= 1): hero.x &gt; camLeft + viewportW + S*50 -> velocity 0 (nothing else: the move animation keeps playing off screen).</summary>
        void RunExitWalk()
        {
            if (!runEnded) return;
            // ADAPTATION (r119j, user decision "walk past the wide edge"; wide view only): the stop line moves right by the wide-view extra width, so the
            // hero ends S*50 past the VISIBLE edge as on iPad; with no extra width (4:3) the term is 0 and the native line applies.
            if (heroX > N.Add32(N.Add32(N.Add32(camLeft, WorldViewportW), N.Mul32(N.S, 50f)), WideCombatExtra)) SetBodyVelocity(0f);
        }

        /// <summary>GamePhysics::SetLinearVelocity on the hero body. Native has ONE body velocity; the port splits it into heroVelocityX and rollVelocity (used while
        /// rolling), so a native velocity write must reach both. r119d (user item 11): the run-end walk velocity never reached a rolling hero in the port.</summary>
        void SetBodyVelocity(float v) { heroVelocityX = v; rollVelocity = v; }

        void MainSceneUpdate(float dt)
        {
            if (!MainSceneMission) return;
            UpdateRunProgress();
            if (heroCameraThisTick) { RunEndCheck(); RunExitWalk(); }   // both are UpdateCamera parts (HeroUpdatesCamera)
            EggUpdate(dt);
        }
    }
}
