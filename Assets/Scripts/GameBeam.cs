using System.Collections.Generic;
using UnityEngine;

namespace Dc2Pilot
{
    /// <summary>Continuous-damage hero weapons (type 5 sub 1 flame PenHuoQ 0xbeb28, sub 2 laser JiGuangQ 0xbeda6) and the dinosaur BE/burn states they cause:
    /// playerClass::Damage 0xbe65c, dinosaurClass::BeDamaged 0x11c9c / BeDamagedOver 0x10954 / OnKeyFrame 0x156d4, effectClass::OnUpdate 0x167a6 modes 4/5 and
    /// OnAnimationEnd 0x16af0 (Dev/Evidence/beam_weapons_r113.md). YuanLi (sub 4), FeiBiao (sub 3) and JuPan (sub 0) are implemented below (r118v: the old OPEN note was stale). JiGuangQ's Mount branch is unreachable with shipped data (r118x, see JiGuangQ).</summary>
    public partial class GameScene
    {
        /// <summary>effectClass bound to the hero (+0x2bc laser mode 4, +0x2c4 flame mode 5).</summary>
        sealed class HeroBeam
        {
            public int mode; public FxActor fx; public bool enabled;
            public NativeCaAnimation anim;
        }
        HeroBeam flameBeam, laserBeam, yuanliBeam;
        public int BeamDamageCalls, BurnStarts, ShockStarts;   // DEBUG_ONLY
        public bool FlameEnabled { get { return flameBeam != null && flameBeam.enabled; } }
        public bool LaserEnabled { get { return laserBeam != null && laserBeam.enabled; } }
        public bool YuanliEnabled { get { return yuanliBeam != null && yuanliBeam.enabled; } }

        /// <summary>dinosaurClass::OnKeyFrame 0x156d4 (state switch).</summary>
        void OnDinoKeyFrame(DinoActor d)
        {
            float dist = Mathf.Abs(heroX - d.x);
            switch (d.state)
            {
                case 1: if (CheckAttack(d, heroX)) BeAttack(d); break;                               // CheckAttack && skin <= 5 -> BeAttack
                case 3: Audio.PlaySound("Audio_death3Audio"); break;
                case 4: BeAttacked(d, Dc2GameData.Weapons()[5][2], dist); break;                     // weapon manager type 5 + 88 (laser F3)
                case 5: BeAttacked(d, Dc2GameData.Weapons()[5][1], dist); Audio.PlaySound("Audio_att3Audio"); break;   // type 5 + 44 (flame F2)
                case 7: d.velocityX = 0f; d.targetable = true; break;                                // jump landing
            }
        }

        bool InViewportMargin(DinoActor d) { return InViewport(d); }   // r119f: BeDamaged 0x11d14..0x11d5e has the BeAttacked gate (view + current-animation extents +0x10/+0x14), + WideView extra

        /// <summary>dinosaurClass::BeDamaged 0x11c9c: sub 2/4 -> state 4 ("BEAnim", k*_be played to frame 4); sub 1 -> state 5 (loop "BShaoAnim" k*_shao + DahuoAnim overlay); velocity 0.</summary>
        void BeDamaged(DinoActor d, WeaponDef w)
        {
            if (!InViewportMargin(d)) return;   // native gate 0x11d14..0x11d5e: viewport extended by the current animation's extents (CAObj+8 -> +0x10 / +0x14)
            BeamDamageCalls++;
            if (w.index == 2 || w.index == 4)
            {
                if (d.aBe == null) d.aBe = DinoAnim(DinoFamily(d), "be", d.surface, false, false, false);
                d.aBe.MarkKeyFrame(3);
                d.rig.SetAdvanced(null); d.advActive = false;
                d.rig.Play(d.aBe, 0, 0, 0.025f, 4);
                d.state = 4; d.legacyHit = false; ShockStarts++;
            }
            else if (w.index == 1)
            {
                if (d.aShao == null)
                {
                    string fam = DinoFamily(d);   // loadDinoANI: shao gets no DinoChangeImage (only SetSpriteAnim parts)
                    d.aShao = fam == "k5" ? NativeCaAnimation.Load("DC2/Image/an2/k5_shao.a2b", true, 1f).Clone()
                                          : NativeCaAnimation.Load("DC2/Image/a2b/" + fam + "_shao.a2b", false, DinoAnimScale).Clone();
                    d.aShao.MarkKeyFrame(3);
                    foreach (var p in fam == "k5" ? new[] { "com10", "com11", "com12", "com13", "com14", "com15" } : fam == "k2" ? new[] { "com2", "com3" } : new[] { "com3", "com4" }) d.aShao.SetSpriteAnimation(p);   // loadDinoANI SetSpriteAnim(shao,...): xiaohuo1/xiaohuo2 parts (k1/k3 com3+com4, k2 com2+com3, parts verified in the a2b files; the second call of each pair is the shared tail at 0x1842a). r117 briefly changed this to com3 / zhua2 from an incomplete trace: WRONG, reverted (soak crash xiaohuo2Frame)
                }
                d.rig.SetAdvanced(null); d.advActive = false;
                d.rig.Play(d.aShao, -1, 0, 0.025f);
                if (d.burnFx == null)
                {
                    // BeDamaged 0x11e94..0x11f20 (and the same block per hit at 0x11f70..0x1201c): skins 1, 2, 5 -> objectScale 0.8 (0x3F4CCCCD),
                    // y = S*160*0.8 + g_ground_y (0x43200000 = 160), iPad y -= 70 (0x11f1a 0x428C0000); skin 3 -> scale 1.0, y kept; x = dinosaur x.
                    // r119z2 FALSIFIED: the port used 150 and 72 here (the dahuo fire 18 px too low on iPad), the twin of the DieShaoSi ash error fixed in r119w.
                    float s = N.S;
                    float y = d.y; float scale = 1f;
                    if (d.skin == 1 || d.skin == 2 || d.skin == 5) { scale = 0.8f; y = N.Add32(N.Mul32(N.Mul32(s, 160f), 0.8f), GroundY); y = N.Add32(y, -70f); }
                    d.burnFx = SpawnCaFx("dahuo", FxAnimation("DC2/Image/a2b/dahuo.a2b", false), d.x, y, d.faceRight, 9, scale, -1);
                }
                d.state = 5; d.legacyHit = false; BurnStarts++;
            }
            d.velocityX = 0f;   // LABEL_32: SetLinearVelocity(0,0)
        }

        string DinoFamily(DinoActor d) { return d.skin == 1 ? "k3" : d.skin == 2 ? "k1" : d.skin == 3 ? "k2" : "k5"; }

        /// <summary>dinosaurClass::BeDamagedOver 0x10954.</summary>
        void BeDamagedOver(DinoActor d)
        {
            if (d.state == 4)
            {
                d.rig.Play(d.rig.animation, 0, 4, 0.025f);
                d.state = 6;
            }
            else if (d.state == 5)
            {
                d.state = 6;
                d.rig.Play(d.rig.animation, 0, 0, 0.025f);
                if (d.burnFx != null) { RemoveFx(d.burnFx); d.burnFx = null; }
            }
        }

        /// <summary>playerClass::Damage 0xbe65c: forward scan of every dinosaur/boss with the held beam weapon.</summary>
        void HeroDamage()
        {
            var w = CurrentWeapon();
            float range = N.Mul32(N.S, w.atkDis);
            foreach (var d in dinos)
            {
                if (d.dead && d.state != 3) continue;
                float fwd = facingLeft ? N.Add32(heroX, -d.x) : N.Add32(d.x, -heroX);
                if (fwd > 0f && fwd < range)
                {
                    if (d.state >= 3 && d.state <= 5) continue;
                    if (d.targetable && w.group) BeDamaged(d, w);
                }
                else if (d.state == 4 || d.state == 5) BeDamagedOver(d);
            }
            float bossDelta;
            if (Boss != null && w.group && BossAttackCandidate(w, out bossDelta) && Boss.kind != 4 && Boss.sub != 0) BossBeAttacked(w);
        }

        // ---- hero-bound effect objects ----
        HeroBeam EnsureBeam(ref HeroBeam b, int mode, string resource, bool xml, int endFrame)
        {
            if (b == null)
            {
                if (mode == 5) Atlas.LoadByConf("DC2/Image/ip/huoyan2x", "DC2/Image/ip/huoyan2x.conf");   // flame frames (huoyan atlas, also used by the boss)
                b = new HeroBeam { mode = mode, anim = FxAnimation(resource, xml) };
                if (mode == 5) b.anim.SetSpriteAnimation("com1");   // loader case 28: SetSpriteAnim(g_ca_huoyanData, "com1")
                b.fx = SpawnCaFx("beam" + mode, b.anim, heroX, Rig.Player.position.y, facingLeft, 13, 1f, 0);   // layer 13 (+23*4)
                // GameContainer::CreateGameObject 0x16b7a0 puts the new child in the container's pending set (+36); GameContainer::BeforeUpdate 0x16bb90 merges it
                // into the id multimap only at the hero's next BeforeUpdate (scene stage 5), so a beam created during this tick's object updates, before the hero's
                // container pass, gets its first effectClass update next tick (r118z4 L1 closure; the port stepped it in the creation tick).
                b.fx.skipFirstUpdate = InUpdateStage && !beamsStepped;
                b.fx.persistent = true; b.enabled = true;
                b.fx.ca.Play(b.anim, 0, 0, 0.025f, endFrame);
            }
            return b;
        }

        void BeamEnable(HeroBeam b, int endFrame)
        {
            if (b.enabled) return;
            b.enabled = true;
            b.fx.ca.Play(b.anim, 0, 0, 0.025f, endFrame);
        }

        /// <summary>playerClass::PenHuoQ 0xbeb28: flame effect (a2b huoyan, mode 5, layer 13) at the gun, held-end frame 5, sound F2, then Damage().</summary>
        void PenHuoQ()
        {
            NativeCaPlayer.PartInfo info;
            if (!Rig.Player.PartInfos.TryGetValue("yyj5", out info)) return;
            Vector2 p, size; float rot;
            HeroSpritePoint("yyj5", new Vector2(N.Mul32(0.4f, info.size.x), N.Mul32(N.Mul32(0.4f, info.size.y), 0.1f)), out p, out size, out rot);
            int end = fireDown ? 5 : -1;
            EnsureBeam(ref flameBeam, 5, "DC2/Image/a2b/huoyan.a2b", false, end);
            BeamEnable(flameBeam, end);
            flameBeam.fx.x = p.x; flameBeam.fx.y = p.y;
            Audio.PlaySound("Attack_F2Audio");
            HeroDamage();
        }

        /// <summary>playerClass::JiGuangQ 0xbeda6: laser effect (a2b jiguang, mode 4, layer 13), held-end frame 6, sound F3, then Damage().
        /// Native also positions the effect at getSpritePos("yyj5", (3w, 3h * -0.35)) and, only when its key-frame argument is 6, Mounts it to the hero once
        /// (MountManager then overrides effectClass::OnUpdate's gun follow every frame). OnKeyFrameAdv passes the key frame, and the laser fire animation is keyed
        /// only at frame 3 (GenComAnim SetKeyFrame(3), fire_recipes; frame keys come only from SetKeyFrame): the Mount branch is unreachable with shipped data.
        /// The per-key SetPosition is overwritten by effectClass::OnUpdate in the same frame unless the effect updates before the hero (GameContainer address order,
        /// not statically recoverable): not ported (r118x).</summary>
        void JiGuangQ()
        {
            int end = fireDown ? 6 : -1;
            EnsureBeam(ref laserBeam, 4, "DC2/Image/a2b/jiguang.a2b", false, end);
            BeamEnable(laserBeam, end);
            Audio.PlaySound("Attack_F3Audio");
            HeroDamage();
        }

        /// <summary>effectClass::OnUpdate 0x167a6 modes 4/5 (follow the gun every frame, flip = player flag) + effect clock.</summary>
        /// <summary>Hero weapon effect objects: CreateGameObject id 97 (PenHuoQ 0xbebfa / JiGuangQ 0xbee7a / YuanLi 0xbf198) - the last ids in the scene map.</summary>
        void UpdateBeams(float dt)
        {
            StepBeam(flameBeam, dt);
            StepBeam(laserBeam, dt);
            StepBeam(yuanliBeam, dt);
            beamsStepped = true;   // the hero's GameContainer::Update 0x16be94 pass of this tick is over (reset at the end of CombatUpdate)
        }
        bool beamsStepped;
        public System.Action DebugBeforeBeams;   // DEBUG_ONLY verifier hook
        /// <summary>DEBUG_ONLY verifier probe: (re)creates the laser beam now and reports whether its first update waits for the next tick.</summary>
        public bool DebugCreateLaserBeam() { laserBeam = null; return EnsureBeam(ref laserBeam, 4, "DC2/Image/a2b/jiguang.a2b", false, -1).fx.skipFirstUpdate; }

        /// <summary>ADAPTATION (r119f, user decision "keep the native 960 px damage reach and clip the drawn ray to that reach; render-only, wide view only").
        /// The ray is a fixed-length effect (native); damage stops at S*ATK_DIS of the held weapon (HeroDamage). In wide view the art can run into the extra strip over
        /// dinosaurs it cannot hurt. A SpriteMask on the beam root keeps the native screen (camLeft .. camLeft + 1024) and anything within heroX +/- S*ATK_DIS visible
        /// and hides the rest; with no extra width the mask is off and every renderer is unmasked (native).</summary>
        static Sprite beamMaskSprite;
        public bool BeamClipActive(int mode) { var b = mode == 4 ? laserBeam : mode == 5 ? flameBeam : yuanliBeam; var m = b != null && b.fx != null && b.fx.root != null ? b.fx.root.GetComponentInChildren<SpriteMask>() : null; return m != null && m.enabled; }   // DEBUG_ONLY probe
        public float BeamClipRight(int mode) { var b = mode == 4 ? laserBeam : mode == 5 ? flameBeam : yuanliBeam; var m = b != null && b.fx != null && b.fx.root != null ? b.fx.root.GetComponentInChildren<SpriteMask>() : null; return m != null && m.enabled ? N.Add32(m.bounds.max.x, camLeft) : float.NaN; }   // DEBUG_ONLY probe: rendered mask right edge as a world x (layer-root units are screen x)
        public float BeamClipLeft(int mode) { var b = mode == 4 ? laserBeam : mode == 5 ? flameBeam : yuanliBeam; var m = b != null && b.fx != null && b.fx.root != null ? b.fx.root.GetComponentInChildren<SpriteMask>() : null; return m != null && m.enabled ? N.Add32(m.bounds.min.x, camLeft) : float.NaN; }   // DEBUG_ONLY probe (world x)
        void ClipBeamToReach(HeroBeam b)
        {
            if (b.fx == null || b.fx.root == null) return;
            var mask = b.fx.root.GetComponentInChildren<SpriteMask>(true);
            float e = WideCombatExtra;
            if (e <= 0f)
            {
                if (mask != null && mask.enabled) { mask.enabled = false; foreach (var r in b.fx.root.GetComponentsInChildren<SpriteRenderer>(true)) r.maskInteraction = SpriteMaskInteraction.None; }
                return;
            }
            if (mask == null)
            {
                if (beamMaskSprite == null) { var t = new Texture2D(1, 1); t.SetPixel(0, 0, Color.white); t.Apply(); beamMaskSprite = Sprite.Create(t, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), 1f); }
                var go = new GameObject("beam_reach_mask"); go.transform.SetParent(b.fx.root.transform, false);
                mask = go.AddComponent<SpriteMask>(); mask.sprite = beamMaskSprite;
            }
            mask.enabled = true;
            float reach = N.Mul32(N.S, CurrentWeapon().atkDis);
            float lo = N.Add32(camLeft, -e), hi = N.Add32(N.Add32(camLeft, WorldViewportW), e);   // visible world span
            if (facingLeft) lo = Mathf.Max(lo, Mathf.Min(camLeft, N.Add32(heroX, -reach)));
            else hi = Mathf.Min(hi, Mathf.Max(N.Add32(camLeft, WorldViewportW), N.Add32(heroX, reach)));
            float origin = camLeft;   // r119i: the beam sprites are drawn in screen x (0..1024 across the native view) under the layer root, so root-local x = world x - camLeft
                                      // (r119f used camLeft + 512, which put the mask 512 px left and cut the right-facing ray at screen x 548..683; user report)
            mask.transform.localPosition = new Vector3((lo + hi) * 0.5f - origin, -N.ScreenH * 0.5f, 0f);
            mask.transform.localScale = new Vector3(Mathf.Max(0f, hi - lo), 4f * N.ScreenH, 1f);
            foreach (var r in b.fx.root.GetComponentsInChildren<SpriteRenderer>(true)) r.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        }

        void StepBeam(HeroBeam b, float dt)
        {
            if (b == null || !b.enabled) return;
            if (b.fx.skipFirstUpdate) { b.fx.skipFirstUpdate = false; return; }   // still in the hero container's pending set (see EnsureBeam)
            NativeCaPlayer.PartInfo info; Vector2 p, size; float rot;
            if (Rig.Player.PartInfos.TryGetValue("yyj5", out info))
            {
                if (b.mode == 5)
                {
                    if (HeroSpritePoint("yyj5", new Vector2(N.Mul32(0.41f, info.size.x), N.Mul32(N.Mul32(0.41f, info.size.y), 0.02f)), out p, out size, out rot)) { b.fx.x = p.x; b.fx.y = p.y; }
                }
                else if (b.mode == 8) { }
                else if (b.mode == 4)
                {
                    if (HeroSpritePoint("yyj5", new Vector2(N.Mul32(0.3f, info.size.x), 0f), out p, out size, out rot))
                    { float off = N.Mul32(140f, N.S); b.fx.x = facingLeft ? N.Add32(p.x, -off) : N.Add32(p.x, off); b.fx.y = p.y; }
                }
            }
            if (b.mode == 8 && Rig.Player.PartInfos.TryGetValue("com2", out info) && HeroSpritePoint("com2", new Vector2(-info.size.x, N.Mul32(info.size.y, 16f)), out p, out size, out rot)) { b.fx.x = p.x; b.fx.y = p.y; }   // effectClass mode 8: com2 point (-w, 16h)
            b.fx.ca.flipX = facingLeft;
            b.fx.ca.position = new Vector2(b.fx.x - (camLeft + WorldViewportW * 0.5f), b.fx.y);
            b.fx.ca.Step(dt);
            ClipBeamToReach(b);
            if (b.fx.ca.Ended) BeamAnimationEnd(b);
        }

        /// <summary>effectClass::OnAnimationEnd 0x16af0 for the hero beams. CAObjCallBack+68 is the CURRENT frame (playAnimation 0xa064 seeds it with the start,
        /// OnUpdate 0xa6cc advances it and clamps it to the end +52 before calling OnAnimationEnd), so on a held clip it equals the held end bound:
        /// mode 5 (flame) frame 5 -> return, the flame stays visible on frame 5 (0x16dd6); mode 4 (laser) frame 6 -> playAnimation(loops -1, 4, 0.025, 6), frames
        /// 4..6 loop (0x16b84); mode 8 (yuanli) frame 6 with fire held -> playAnimation(loops -1, 0, 0.05, 6) (0x16cb2); otherwise hide + BeDamagedOver.
        /// r119j: FALSIFIED the old reading "+68 = recorded start, never 5/6": the port hid the held flame at frame 5 and PenHuoQ restarted it from frame 0 on the
        /// next key frame, drawing the flame as bursts (user report); the held laser / yuanli were hidden at frame 6 the same way.</summary>
        public int BeamFrameForTest(int mode) { var b = mode == 4 ? laserBeam : mode == 5 ? flameBeam : yuanliBeam; return b != null && b.enabled ? b.fx.ca.CurrentFrame : -1; }   // DEBUG_ONLY probe (-1 = hidden)
        public int BurnFxCountForTest() { int n = 0; foreach (var f in fxList) if (f.tag == "dahuo") n++; return n; }   // DEBUG_ONLY probe
        public Vector2 BurnFxPosForTest(DinoActor d) { return d.burnFx != null ? new Vector2(d.burnFx.x, d.burnFx.y) : new Vector2(float.NaN, float.NaN); }   // DEBUG_ONLY probe
        public float BurnFxScaleForTest(DinoActor d) { return d.burnFx != null && d.burnFx.ca != null ? d.burnFx.ca.objectScale : float.NaN; }   // DEBUG_ONLY probe
        void BeamAnimationEnd(HeroBeam b)
        {
            int f = b.fx.ca.CurrentFrame;
            if (b.mode == 5 && f == 5) return;
            if (b.mode == 4 && f == 6) { b.fx.ca.Play(b.anim, -1, 4, 0.025f, 6); return; }
            if (b.mode == 8 && f == 6 && fireDown) { b.fx.ca.Play(b.anim, -1, 0, 0.05f, 6); return; }
            BeamEnded(b);
        }

        /// <summary>effectClass::OnAnimationEnd 0x16af0 modes 5/4: effect hidden, dinosaurs in state 5/4 get BeDamagedOver. Reached from BeamAnimationEnd when the current frame is not a hold frame (r119j).</summary>
        void BeamEnded(HeroBeam b)
        {
            b.enabled = false; b.fx.ca.Hide();
            int want = b.mode == 5 ? 5 : 4;   // laser (4) and yuanli (8) both leave state-4 dinosaurs
            foreach (var d in dinos) if (!d.dead && d.state == want) BeDamagedOver(d);
        }

        /// <summary>playerClass::OnFireUp 0xb8bc6 for the bound effects: current frame (+68) := 0, then OnAnimationEnd, which hides them (frame 0 is never a hold frame).</summary>
        void StopBeams()
        {
            if (flameBeam != null && flameBeam.enabled) BeamEnded(flameBeam);
            if (laserBeam != null && laserBeam.enabled) BeamEnded(laserBeam);
            if (yuanliBeam != null && yuanliBeam.enabled) BeamEnded(yuanliBeam);
        }

        /// <summary>OnAnimationEndAdv type 5 sub 1/2 branches 0xbc382 / 0xbc426: held -> loop frames 3..4 at 0.2 (sub 1) / 0.2 (sub 2: playAdv(0,3,0.2,4)); released: wind-down.</summary>
        void BeamAdvEnd(int sub)
        {
            var beam = sub == 1 ? flameBeam : laserBeam;
            if (fireDown) { FirePlayAdv(0, 3, 0.2f, 4); return; }
            if (fireAdvEnd != 4) { Rig.Player.SetAdvanced(null); firing = false; fireDown = false; return; }
            if (beam != null && beam.fx != null && beam.enabled) beam.fx.ca.Play(beam.anim, 0, beam.fx.ca.CurrentFrame, 0.025f);   // OnAnimationEndAdv 0xbc3ca / 0xbc482: playAnimation(0, +68 current frame, 0.025, -1): the tail runs on from where it is
            FirePlayAdv(0, 4, 0.025f, -1);
            firing = false; fireDown = false;
        }

        /// <summary>playerClass::YuanLi 0xbf0ea: energy effect (an2/yuanli, mode 8, layer 13) near the com2 part, held-end frame 6, sound F3, then Damage() (weapon sub 4).</summary>
        void YuanLi()
        {
            int end = fireDown ? 6 : -1;
            EnsureBeam(ref yuanliBeam, 8, "DC2/Image/an2/yuanli.a2b", true, end);
            BeamEnable(yuanliBeam, end);
            Audio.PlaySound("Attack_F3Audio");
            HeroDamage();
        }

        // ---- thrown projectiles: JuPan (effectClass mode 3, weapon type 5 sub 0) and FeiBiao (mode 7, sub 3) ----
        sealed class HeroProjectile { public FxActor fx; public int mode; public float vx; public readonly HashSet<DinoActor> hit = new HashSet<DinoActor>(); public bool bossHit; }
        readonly List<HeroProjectile> projectiles = new List<HeroProjectile>();
        public int ProjectileSpawned, ProjectileHits;   // DEBUG_ONLY
        public int ProjectileCount { get { return projectiles.Count; } }

        void SpawnProjectile(int mode, string gunPart, NativeCaAnimation anim, Vector2 localInput)
        {
            Vector2 p, size; float rot;
            if (!HeroSpritePoint(gunPart, localInput, out p, out size, out rot)) return;
            float v = N.Mul32(N.S, facingLeft ? -500f : 500f);
            var f = SpawnCaFx(mode == 3 ? "jupan" : "feibiao", anim, p.x, p.y, facingLeft, 11, 1f, -1);   // playAnimation(-1,0,.025,-1) loops, layer 11
            f.persistent = true;
            projectiles.Add(new HeroProjectile { fx = f, mode = mode, vx = v });
            ProjectileSpawned++;
            Audio.PlaySound("Attack_F1Audio");
        }

        /// <summary>playerClass::JuPan 0xbca10: disc from the yyj5 gun point (0.5w*0.15, 0.5h*0.04 (r118 audit: ARM 0xbca6c literal 0x3D23D70A = 0.04; was 0.043)), a2b jupan.</summary>
        void JuPan()
        {
            NativeCaPlayer.PartInfo info;
            if (!Rig.Player.PartInfos.TryGetValue("yyj5", out info)) return;
            SpawnProjectile(3, "yyj5", FxAnimation("DC2/Image/a2b/jupan.a2b", false), new Vector2(N.Mul32(N.Mul32(0.5f, info.size.x), 0.15f), N.Mul32(N.Mul32(0.5f, info.size.y), 0.04f)));
        }

        /// <summary>playerClass::FeiBiao 0xbc868: shuriken from the com15 part (0.5w*0.15, 0.5h*-0.7), an2/feibiao.</summary>
        void FeiBiao()
        {
            NativeCaPlayer.PartInfo info;
            if (!Rig.Player.PartInfos.TryGetValue("com15", out info)) return;
            SpawnProjectile(7, "com15", FxAnimation("DC2/Image/an2/feibiao.a2b", true), new Vector2(N.Mul32(N.Mul32(0.5f, info.size.x), 0.15f), N.Mul32(N.Mul32(0.5f, info.size.y), -0.7f)));
        }

        /// <summary>effectClass::OnUpdate + Attack 0x16478 (|dx| &lt; 30*S... 60 px, weapon type 5 sub 0 / sub 3) + UpdateWorld 0x16248 (outside viewport +-30: hit flags cleared, disappears).</summary>
        HeroProjectile ProjectileOf(FxActor f) { foreach (var p in projectiles) if (p.fx == f) return p; return null; }

        /// <summary>One FeiBiao / JuPan effectClass object in the id-5 walk (UpdateId5Objects). A new object waits for its first update like any pending object.</summary>
        void StepProjectile(HeroProjectile pr, float dt)
        {
            if (pr.fx.skipFirstUpdate) { pr.fx.skipFirstUpdate = false; return; }
            pr.fx.x = N.Add32(pr.fx.x, N.Mul32(pr.vx, dt));
            pr.fx.ca.position = new Vector2(pr.fx.x - (camLeft + WorldViewportW * 0.5f), pr.fx.y);
            pr.fx.ca.Step(dt);
            var w = Dc2GameData.Weapons()[5][pr.mode == 3 ? 0 : 3];
            float reach = N.Mul32(N.S, 30f);
            foreach (var d in dinos.ToArray())
            {
                if (d.dead || d.state == 3 || pr.hit.Contains(d)) continue;
                if (Mathf.Abs((int)N.Add32(d.x, -pr.fx.x)) < reach) { pr.hit.Add(d); ProjectileHits++; BeAttacked(d, w, Mathf.Abs(heroX - d.x)); }
            }
            if (Boss != null && !pr.bossHit && Boss.kind != 4 && Boss.sub != 0 && Mathf.Abs((int)N.Add32(Boss.x, -pr.fx.x)) < reach) { pr.bossHit = true; ProjectileHits++; BossBeAttacked(w); }
            if (pr.fx.x < N.Add32(camLeft, -30f - WideCombatExtra) || pr.fx.x > N.Add32(N.Add32(camLeft, WorldViewportW), 30f + WideCombatExtra))   // + WideView combat extra (ADAPTATION)
            { RemoveFx(pr.fx); projectiles.Remove(pr); }
        }
    }
}
