using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
namespace Dc2Pilot.EditorTools
{
    public static class PilotMeleeInputVerify
    {
        public static void Run()
        {
            string output=Path.GetFullPath(Path.Combine(Application.dataPath,"../Reports/DC2-PILOT-001/melee-input-r72"));var args=Environment.GetCommandLineArgs();for(int i=0;i<args.Length-1;i++)if(args[i]=="-reportDir")output=args[i+1];Directory.CreateDirectory(output);
            int pass=0,fail=0;var log=new StringBuilder();Dc2Game game=null;Camera cam=null;string temp=Path.Combine(Path.GetTempPath(),"dc2_melee_input_"+Guid.NewGuid().ToString("N"));
            Action<string,bool> check=(n,ok)=>{log.AppendLine((ok?"PASS ":"FAIL ")+n);if(ok)pass++;else fail++;};
            try
            {
                cam=Bootstrap.MakeCamera();game=Dc2Game.Create(temp);game.manualStep=true;game.SkipMissionIntro=true;game.cam=cam;game.LaunchMission=new NativeMapState.Mission{type=3,count=1000,background=0};
                for(int hero=0;hero<4;hero++)
                {
                    // Explicit mission per case; drain the native queued scene
                    // transition before retaining or touching its new scene.
                    game.LaunchMission=new NativeMapState.Mission{type=3,count=1000,background=0};
                    game.Profile.playerType=hero;game.SwitchScene("gameScene");game.Step(0f);var s=(GameScene)game.Current;s.spawningEnabled=false;
                    s.Rig.Player.SetAdvanced(s.Rig.Player.animation.Clone());
                    s.Touch(0,GameScene.HitRect.center);check(hero+" native first press consumed",s.meleeing&&!s.hitHeld);
                    check(hero+" native melee clears advanced channel",s.Rig.Player.AdvancedAnimation==null);
                    for(int i=0;i<180&&s.meleeing;i++)game.Step(1f/60f);
                    check(hero+" holding initial press does not repeat",!s.meleeing&&!s.hitHeld);s.Touch(2,GameScene.HitRect.center);
                    s.Touch(0,GameScene.HitRect.center);game.Step(1f/60f);s.Touch(0,GameScene.HitRect.center);
                    check(hero+" blocked second press remains latched",s.meleeing&&s.hitHeld);
                    var selected=s.Rig.Player.animation;bool restarted=false;
                    int prior=s.Rig.Player.CurrentFrame;
                    for(int i=0;i<100;i++){game.Step(1f/60f);int current=s.Rig.Player.CurrentFrame;if(current<prior){restarted=true;break;}prior=current;}
                    check(hero+" native held replay retains selected pose",restarted&&s.meleeing&&s.hitHeld&&ReferenceEquals(selected,s.Rig.Player.animation));
                    s.Touch(2,GameScene.HitRect.center);check(hero+" release clears second press",!s.hitHeld);
                    for(int i=0;i<180&&s.meleeing;i++)game.Step(1f/60f);
                    check(hero+" release allows finite attack end",!s.meleeing);
                }
            }
            catch(Exception e){fail++;log.AppendLine("EXCEPTION "+e);}
            finally
            {
                if(game!=null)UnityEngine.Object.DestroyImmediate(game.gameObject);if(cam!=null)UnityEngine.Object.DestroyImmediate(cam.gameObject);if(Directory.Exists(temp))Directory.Delete(temp,true);
                log.AppendLine("SUMMARY passes="+pass+" fails="+fail);File.WriteAllText(Path.Combine(output,"verify.txt"),log.ToString());Debug.Log(log.ToString());PilotExit.Exit(fail==0?0:1);
            }
        }
    }
}
