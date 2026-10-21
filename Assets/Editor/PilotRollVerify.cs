using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
namespace Dc2Pilot.EditorTools
{
    public static class PilotRollVerify
    {
        [Serializable] public class Tick {public int tick,phase,displayed;public float x,velocity;public bool rolling;}
        [Serializable] public class Case {public bool left;public Tick[] ticks;}
        [Serializable] public class Oracle {public Case[] cases;}
        public static void Run()
        {
            string output=Path.Combine(Application.dataPath,"../Reports/DC2-PILOT-001/roll-r38");var args=Environment.GetCommandLineArgs();
            for(int i=0;i<args.Length-1;i++)if(args[i]=="-reportDir")output=args[i+1];Directory.CreateDirectory(output);
            string dir=Path.Combine(Path.GetTempPath(),"dc2_roll_"+Guid.NewGuid().ToString("N"));
            int passed=0,failed=0;var log=new StringBuilder();Action<string,bool> check=(n,v)=>{if(v)passed++;else failed++;log.AppendLine((v?"PASS ":"FAIL ")+n);};
            Dc2Game game=null;Camera camera=null;RenderTexture rt=null;Texture2D image=null;
            try
            {
                var oracle=JsonUtility.FromJson<Oracle>(File.ReadAllText(Path.Combine(Application.dataPath,"../../Dev/Reports/DC2-PILOT-001/roll_travel_oracle.r38.json")));
                camera=Bootstrap.MakeCamera();game=Dc2Game.Create(dir);game.cam=camera;game.manualStep=true;game.SkipMissionIntro=true;
                rt=new RenderTexture(1024,768,24);image=new Texture2D(1024,768,TextureFormat.RGB24,false);
                Action<string> shot=name=>{camera.targetTexture=rt;Bootstrap.RenderAll(camera);RenderTexture.active=rt;image.ReadPixels(new Rect(0,0,1024,768),0,0);image.Apply();File.WriteAllBytes(Path.Combine(output,name),image.EncodeToPNG());camera.targetTexture=null;RenderTexture.active=null;};
                foreach(int hero in new[]{0,2})foreach(var c in oracle.cases)
                {
                    game.LaunchMission=new NativeMapState.Mission {type=3,count=99,background=7};game.LaunchMission.enemies.Add(1);
                    game.Profile.playerType=hero;game.SwitchScene("gameScene");game.Step(0f);
                    var scene=(GameScene)game.Current;scene.spawningEnabled=false;scene.ignorePlayer=true;scene.heroX=1000;scene.facingLeft=c.left;
                    // Update projection for the explicit interior fixture before native touch starts the roll.
                    game.Step(0f);scene.OnRollDown();string key="roll_"+hero+"_"+(c.left?"left":"right");
                    check(key+" native26-frame XML/initial velocity",scene.Rig.Player.animation.frames.Length==26&&scene.RollVelocity==(c.left?-60:60)&&scene.rollPhase==0);
                    bool skin=true;foreach(var frame in scene.Rig.Player.animation.frames)foreach(var part in frame.parts)
                    {
                        if(part.name=="com1")skin&=part.reference=="yingzi";
                        if(part.name=="roll")skin&=part.reference==(hero==2?"p3move":"p1move");
                    }
                    check(key+" native shadow/roll skin preserved",skin);
                    foreach(var t in c.ticks)
                    {
                        game.Step(1f/60f);
                        check(key+" tick"+t.tick+" ARM position/velocity/phase/emission",scene.heroX==t.x&&scene.RollVelocity==t.velocity&&scene.rollPhase==t.phase&&scene.rolling==t.rolling&&scene.Rig.Player.DisplayedFrame==t.displayed);
                        if(t.tick==1||t.tick==15||t.tick==37||t.tick==52)shot(key+"_tick"+t.tick+".png");
                    }
                    check(key+" final emission remains rendered",scene.Rig.GetComponentsInChildren<SpriteRenderer>().Length>0&&Array.Exists(scene.Rig.GetComponentsInChildren<SpriteRenderer>(),r=>r.enabled));
                    game.Step(1f/60f);check(key+" Stand follows final emission",scene.Rig.Player.DisplayedFrame==0&&!scene.rolling&&scene.Rig.Source!="p_roll.an2 Expand1.5");
                }
                // heroes 1 and 3: two-part roll (playerClass::Roll 0xb95f8 / OnAnimationEnd state 7 / OnTimeout "roll")
                foreach(int hero in new[]{1,3})foreach(bool left in new[]{false,true})
                {
                    game.LaunchMission=new NativeMapState.Mission {type=3,count=99,background=7};game.LaunchMission.enemies.Add(1);
                    game.Profile.playerType=hero;game.SwitchScene("gameScene");game.Step(0f);
                    var scene=(GameScene)game.Current;scene.spawningEnabled=false;scene.ignorePlayer=true;scene.heroX=1000;scene.facingLeft=left;
                    game.Step(0f);scene.OnRollDown();string key="roll2part_"+hero+"_"+(left?"left":"right");
                    check(key+" phase 0: roll_1 without velocity",scene.rolling&&scene.SpecialRollPhase==0&&scene.RollVelocity==0f&&scene.Rig.Source==(hero==3?"p4_roll_1":"p2_roll_1"));
                    int guard=0;while(scene.SpecialRollPhase==0&&guard++<600)game.Step(1f/60f);
                    float v=scene.WalkSpeedFor(hero)*(left?-3f:3f);
                    check(key+" roll_1 end: hidden, travel at +-3 x walk speed",scene.SpecialRollPhase==1&&!scene.HeroVisible&&scene.RollVelocity==v);
                    if(hero==3)check(key+" hero 3 roll effect spawned (GetEffect, layer 9)",scene.LastFxTag=="rolleff");
                    float x0=scene.heroX;guard=0;while(scene.SpecialRollPhase==1&&guard++<120)game.Step(1f/60f);
                    check(key+" 0.5 s roll timer: visible again, roll_2, stopped",scene.SpecialRollPhase==2&&scene.HeroVisible&&scene.RollVelocity==0f&&scene.Rig.Source==(hero==3?"p4_roll_2":"p2_roll_2")&&guard>=28&&guard<=32);
                    check(key+" travelled while hidden",left?scene.heroX<x0:scene.heroX>x0);
                    guard=0;while(scene.rolling&&guard++<600)game.Step(1f/60f);
                    check(key+" roll_2 end: stand, state 2",!scene.rolling&&scene.heroControlState==2);
                }
                // GameClear while hidden in the two-part roll (r118x): Stand keeps the hero hidden, the pending "roll" timer still shows it and plays roll_2
                {
                    game.LaunchMission=new NativeMapState.Mission {type=3,count=99,background=7};game.LaunchMission.enemies.Add(1);
                    game.Profile.playerType=1;game.SwitchScene("gameScene");game.Step(0f);
                    var scene=(GameScene)game.Current;scene.spawningEnabled=false;scene.ignorePlayer=true;scene.heroX=1000;
                    game.Step(0f);scene.OnRollDown();
                    int guard=0;while(scene.SpecialRollPhase==0&&guard++<600)game.Step(1f/60f);
                    scene.Complete();
                    check("gameclear_hidden_roll: hero still hidden and stopped right after GameClear",!scene.HeroVisible&&!scene.rolling&&scene.heroControlState==2&&scene.RollVelocity==0f);
                    game.StepFor(0.6f);
                    check("gameclear_hidden_roll: the roll timer still fires: visible, roll_2, no stand-up (state stays 2)",scene.HeroVisible&&scene.SpecialRollPhase==2&&scene.Rig.Source=="p2_roll_2");
                }
                // roll fill indicator (r118z4): lifeModeScene::RollDown 0xd0900 clip = (830,537,88,88); OnUpdate 0xd45b0 clip height += dt * -20.0 * (2.0 * 0.7); the 88x88 sprite is clipped, not resized
                {
                    game.LaunchMission=new NativeMapState.Mission {type=3,count=99,background=7};game.LaunchMission.enemies.Add(1);
                    game.Profile.playerType=0;game.SwitchScene("gameScene");game.Step(0f);
                    var scene=(GameScene)game.Current;scene.spawningEnabled=false;scene.ignorePlayer=true;scene.heroX=1000;
                    game.Step(0f);var pre=scene.GetControl(45);check("roll fill exists hidden from OnLoad (SetVisible(0) 0xe96c2) before the first roll",pre!=null&&!pre.visible);scene.OnRollDown();
                    float expect=88f;for(int i=0;i<60;i++){game.Step(1f/60f);expect=N.Add32(expect,N.Mul32(N.Mul32(1f/60f,-20f),N.Mul32(2f,0.7f)));}
                    var fill=scene.GetControl(45);Rect vis;
                    check("roll fill: 1 s after the press the clip height is 88 - 28 (native -20 * 1.4 per second, binary32): "+scene.rollFill,fill!=null&&scene.rollFill==expect&&Mathf.Abs(expect-60f)<0.01f);
                    check("roll fill: the control keeps its 88x88 rect (clipped, not resized)",fill!=null&&fill.CurrentRect==GameScene.RollFillRect);
                    Color32 fc=fill!=null?(Color32)fill.GetComponent<SpriteRenderer>().color:default(Color32);
                    check("roll fill: lifeMode control 45 is black alpha 176 (lifeModeScene::OnLoad SetColor 0xB0000000)",scene.RollFillId==45&&fc.r==0&&fc.g==0&&fc.b==0&&fc.a==176);
                    check("roll fill: the drawn part is the top "+expect+" px of the button rect",fill!=null&&fill.DebugClipped(out vis)&&Mathf.Abs(vis.y-537f)<0.01f&&Mathf.Abs(vis.height-expect)<0.01f&&Mathf.Abs(vis.width-88f)<0.01f);
                    game.StepFor(2.2f);
                    var after=scene.GetControl(45);Rect vis2;
                    check("roll fill: after the clip height reaches 0 (88 / 28 = 3.14 s) the drain stops and nothing is drawn; the OnLoad control stays (r118z4)",after!=null&&!scene.rollFillOn&&!after.DebugClipped(out vis2));
                    int order=after!=null?after.GetComponent<SpriteRenderer>().sortingOrder:-1;
                    check("roll fill lives in UI layer 1 (lifeModeScene::OnLoad 0xe950c CreateImage(45, 1)): order "+order,after!=null&&(order-UiControl.UiOrderBase)/Dc2Scene.UiLayerStride==1);
                }
            }
            catch(Exception e){failed++;log.AppendLine(e.ToString());}
            finally
            {
                if(game!=null)UnityEngine.Object.DestroyImmediate(game.gameObject);if(camera!=null)UnityEngine.Object.DestroyImmediate(camera.gameObject);if(rt!=null)UnityEngine.Object.DestroyImmediate(rt);if(image!=null)UnityEngine.Object.DestroyImmediate(image);
                if(Directory.Exists(dir))Directory.Delete(dir,true);log.AppendLine("SUMMARY passes="+passed+" fails="+failed);File.WriteAllText(Path.Combine(output,"verify.txt"),log.ToString());Debug.Log(log.ToString());PilotExit.Exit(failed==0?0:1);
            }
        }
    }
}
