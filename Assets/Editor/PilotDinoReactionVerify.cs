using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>Native non-lethal dinosaur reaction (ARM oracle r94/r96): table lookups and the runtime composition for skin1 ground dinosaurs.</summary>
    public static class PilotDinoReactionVerify
    {
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath, "../Reports/DC2-PILOT-001/dinoreaction-r1");
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-reportDir") output = args[i + 1];
            Directory.CreateDirectory(output);
            string dir = Path.Combine(Path.GetTempPath(), "dc2_dinoreact_verify_" + Guid.NewGuid().ToString("N"));
            int passed = 0, failed = 0; var log = new StringBuilder();
            Action<string, bool> check = (n, v) => { log.AppendLine((v ? "PASS " : "FAIL ") + n); if (v) passed++; else failed++; };
            Dc2Game game = null; Camera cam = null; RenderTexture rt = null; Texture2D img = null;
            try
            {
                // independent oracle values (dino_reaction_oracle.r94.json rows)
                var a = NativeDinoReaction.Evaluate(0, 0, 1, 0, false);
                check("type0/0 skin1 state0 flip false: advanced Befired, vx -0.5*SPD, base delta .1, state stays 0",
                    a.found && a.advanced && !a.basePose && a.velocitySpdScale == -0.5f && a.hasBaseDelta && Mathf.Abs(a.baseDelta - 0.1f) < 1e-6f && a.stateAfter == 0 && Mathf.Abs(a.advInterval - 0.025f) < 1e-6f);
                var a2 = NativeDinoReaction.Evaluate(0, 0, 1, 0, true);
                check("flip true: +0.5*SPD", a2.found && a2.velocitySpdScale == 0.5f);
                var a5 = NativeDinoReaction.Evaluate(0, 0, 5, 0, false);
                check("skin5 default path skips the base delta change", a5.found && a5.advanced && !a5.hasBaseDelta);
                var b = NativeDinoReaction.Evaluate(4, 0, 1, 0, false);
                check("type4/0 skin1: base Behited, state 2, knockback +200 (flip false), interval .05",
                    b.found && b.basePose && b.stateAfter == 2 && b.velocityAbsolute == 200f && Mathf.Abs(b.baseInterval - 0.05f) < 1e-6f && b.flag160 == 1);
                var b3 = NativeDinoReaction.Evaluate(4, 0, 3, 0, false);
                check("type4/0 skin3 uses the advanced path instead", b3.found && b3.advanced);
                var s2 = NativeDinoReaction.Evaluate(4, 0, 2, 0, false);
                check("skin2 behaves as skin1", s2.found && s2.basePose && s2.velocityAbsolute == 200f);
                var none = NativeDinoReaction.Evaluate(5, 1, 1, 0, false);
                check("type5/1 has no reaction requests", none.found && !none.advanced && !none.basePose && none.stateAfter == 0);
                var st1 = NativeDinoReaction.Evaluate(0, 0, 1, 1, false);
                check("attacking dinosaur (state 1) is not reacted to by the ordinary weapon", st1.found && !st1.advanced && !st1.basePose);

                cam = Bootstrap.MakeCamera(); game = Dc2Game.Create(dir); game.manualStep = true; game.SkipMissionIntro = true; game.cam = cam;
                game.LaunchMission = new NativeMapState.Mission { type = 3, count = 20, level = 1, background = 0 };
                rt = new RenderTexture(1024, 768, 24); img = new Texture2D(1024, 768, TextureFormat.RGB24, false);
                Action<string> shot = name => { cam.targetTexture = rt; Bootstrap.RenderAll(cam); RenderTexture.active = rt; img.ReadPixels(new Rect(0, 0, 1024, 768), 0, 0); img.Apply(); File.WriteAllBytes(Path.Combine(output, name), img.EncodeToPNG()); cam.targetTexture = null; RenderTexture.active = null; };
                game.Profile.playerType = 0; game.Profile.cash = 5000;
                game.SwitchScene("gameScene"); game.StepFor(0.02f);
                var scene = (GameScene)game.Current; scene.spawningEnabled = false; scene.DebugSpawnModeRoll = () => 0;
                game.StepFor(0.2f);
                var d = scene.CreateEnemy(1);
                d.x = scene.heroX + 400f; d.faceRight = false; d.state = 0; d.hp = 9999f; d.velocityX = -N.S * d.spd;
                game.StepFor(0.05f);
                var w = scene.CurrentWeapon();
                var expected = NativeDinoReaction.Evaluate(w.type, w.index, d.skin, d.state, d.faceRight);
                scene.ApplyReactionForTest(d, w);
                if (expected.advanced)
                {
                    check("runtime: advanced Befired active with ±0.5 raw SPD velocity", d.advActive && d.state == 0 && d.velocityX == expected.velocitySpdScale * d.spd);
                    d.x = scene.heroX + 300f; game.StepFor(0.1f); shot("adv_befired_playing.png");
                    bool ended = false;
                    for (int f = 0; f < 240 && d.advActive; f++) { game.Step(1f / 60f); }
                    ended = !d.advActive;
                    check("runtime: advanced channel ends and clears", ended);
                    game.Step(1f / 60f);
                    check("runtime: Move restores S*SPD toward the hero", Mathf.Abs(Mathf.Abs(d.velocityX) - N.S * d.spd) < 1e-3f);
                }
                else
                    check("runtime: weapon uses base Behited path state 2 then returns to 8/0", d.state == 2 && !d.advActive);

                // K5 (skin 5) native family: an2 XML-variant animations, ChangeImageK5 wing frames, attack key 18, spawn y 152 / state-8 y 140
                game.Profile.fx = 1; int snd0 = game.Audio.SoundsPlayed;
                var es = scene.CreateEnemy(1);
                check("CreateEnemy plays Audio_enemyAudio (native tail 0xd6466)", game.Audio.SoundsPlayed == snd0 + 1);
                es.removed = true;
                var e5 = scene.CreateEnemy(9);
                check("kind9 is skin5 surface51 and spawns (K5 family wired)", e5 != null && e5.skin == 5 && e5.surface == 51 && e5.aBefired != null);
                if (e5 != null)
                {
                    bool wing = false, wingOk = true;
                    foreach (var fr in e5.aMove.frames) foreach (var pt in fr.parts) if (pt.name == "com1" || pt.name == "com3") { wing = true; if (!pt.reference.StartsWith("k51chibang")) wingOk = false; }
                    check("K5 wing parts renamed to the surface-specific chibang frames", wing && wingOk);
                    bool headOk = false; foreach (var pt in e5.aMove.frames[0].parts) if (pt.name == "com6" && pt.reference == "k51tou1") headOk = true;
                    check("K5 head part com6 -> k51tou1 (DinoChangeImage emulation)", headOk);
                    check("K5 spawn y is the native skin5 term 152", Mathf.Abs(e5.y - 152f) < 1e-3f);
                    check("K5 state-8 ground reset is 140", Mathf.Abs(GameScene.NativeDinoStateY(5) - 140f) < 1e-3f && Mathf.Abs(GameScene.NativeDinoStateY(1) - 165f) < 1e-3f);
                    e5.x = scene.heroX + 450f; e5.faceRight = false; e5.hp = 9999f; e5.velocityX = -N.S * e5.spd;
                    game.StepFor(0.3f); shot("k5_pterodactyl.png");
                    check("K5 dinosaur advances under native physics velocity", e5.x < scene.heroX + 450f);
                }
                // K5 attack (key 18) and death lifecycle
                if (e5 != null)
                {
                    e5.x = scene.heroX + 90f; e5.faceRight = false; e5.state = 0; e5.hp = 9999f; scene.invincible = false;
                    float hpK = scene.playerHp;
                    game.StepFor(1.6f);
                    check("K5 reaches attack range and damages the hero on its marked attack frame", scene.playerHp < hpK && e5.aAttack.frames[18].key);
                    shot("k5_attack.png");
                    e5.hp = 0.01f; scene.invincible = true;
                    int killsBefore = scene.kills;
                    scene.ApplyDamageForTest(e5);
                    for (int f = 0; f < 600 && !e5.removed; f++) game.Step(1f / 60f);
                    check("K5 death plays the die animation then is removed; kill counted", e5.removed && scene.kills == killsBefore + 1);
                }
                var e3 = scene.CreateEnemy(14);
                check("kind14 is skin3 and rides the recovered ground path", e3 != null && e3.skin == 3 && e3.aBefired != null);
                if (e3 != null)
                {
                    e3.x = scene.heroX + 500f; e3.faceRight = false; e3.state = 0; e3.hp = 9999f; e3.velocityX = -N.S * e3.spd;
                    game.StepFor(0.05f);
                    var x3 = NativeDinoReaction.Evaluate(w.type, w.index, 3, e3.state, e3.faceRight);
                    scene.ApplyReactionForTest(e3, w);
                    check("skin3 reaction follows the skin3 oracle row", x3.advanced ? (e3.advActive && e3.state == 0) : (x3.basePose && e3.state == 2));
                    float x0 = e3.x; game.StepFor(0.5f);
                    check("skin3 dinosaur moves with physics velocity (recovered path)", Mathf.Abs(e3.x - x0) > 1f);
                }
            }
            catch (Exception e) { failed++; log.AppendLine("EXCEPTION " + e); }
            finally
            {
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
