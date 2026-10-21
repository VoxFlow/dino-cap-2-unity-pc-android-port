using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
namespace Dc2Pilot.EditorTools
{
    public static class PilotActiveLocomotionVerify
    {
        [Serializable] class Oracle{public Recipe[] recipes;}
        [Serializable] class Recipe{public int hero,slot;public string pose,source;public Frame[] frames;}
        [Serializable] class Frame{public int index;public bool key;public Part[] parts;}
        [Serializable] class Part{public string name,reference;public uint[] words;public uint layer,rgba;public bool sprite_animation;}
        static uint B(float v){return unchecked((uint)BitConverter.SingleToInt32Bits(v));}
        public static void Run()
        {
            string output=null;var args=Environment.GetCommandLineArgs();for(int i=0;i<args.Length-1;i++)if(args[i]=="-reportDir")output=args[i+1];Directory.CreateDirectory(output);var log=new StringBuilder();int pass=0,fail=0;Dc2Game game=null;Camera cam=null;string temp=Path.Combine(Path.GetTempPath(),"dc2_active_locomotion_"+Guid.NewGuid().ToString("N"));
            Action<string,bool> check=(n,ok)=>{log.AppendLine((ok?"PASS ":"FAIL ")+n);if(ok)pass++;else fail++;};
            Action<GameScene,Recipe> pose=(scene,truth)=>
            {
                var a=scene.Rig.Player.animation;string tag=truth.hero+"/"+truth.slot+"/"+truth.pose;
                check(tag+" native selected source/count",scene.Rig.Source==truth.source&&a.frames.Length==truth.frames.Length);
                foreach(var f in truth.frames)
                {
                    var actual=a.frames[f.index];check(tag+" key/count "+f.index,actual.key==f.key&&actual.parts.Length==f.parts.Length);
                    for(int i=0;i<f.parts.Length;i++)
                    {
                        var e=f.parts[i];var p=actual.parts[i];uint[] bits={B(p.x),B(p.y),B(p.w),B(p.h),B(p.pivotX),B(p.pivotY),B(p.radians)};bool ok=p.name==e.name&&p.reference==e.reference&&p.rgba==e.rgba&&p.layerBits==e.layer&&p.spriteAnimation==e.sprite_animation;
                        for(int j=0;j<7;j++)ok&=bits[j]==e.words[j];check(tag+" native fields "+f.index+"/"+e.name,ok);
                    }
                }
            };
            Action<string> capture=name=>
            {
                var rt=new RenderTexture(1024,768,24);var image=new Texture2D(1024,768,TextureFormat.RGB24,false);cam.targetTexture=rt;Bootstrap.RenderAll(cam);RenderTexture.active=rt;image.ReadPixels(new Rect(0,0,1024,768),0,0);image.Apply();File.WriteAllBytes(Path.Combine(output,name+".png"),image.EncodeToPNG());cam.targetTexture=null;RenderTexture.active=null;UnityEngine.Object.DestroyImmediate(rt);UnityEngine.Object.DestroyImmediate(image);
            };
            try
            {
                var oracle=JsonUtility.FromJson<Oracle>(File.ReadAllText(Path.Combine(Application.dataPath,"../../Dev/Reports/DC2-PILOT-001/locomotion_numeric_oracle.r69.json")));
                cam=Bootstrap.MakeCamera();game=Dc2Game.Create(temp);game.cam=cam;game.manualStep=true;game.SkipMissionIntro=true;
                for(int i=0;i<game.Profile.weaponOwned.Length;i++)game.Profile.weaponOwned[i]=0; // DEBUG_ONLY ownership fixture for all configured slots.
                for(int hero=0;hero<4;hero++)
                {
                    game.LaunchMission=new NativeMapState.Mission{type=3,count=1000,background=0};game.Profile.playerType=hero;game.SwitchScene("gameScene");game.Step(0f);var scene=(GameScene)game.Current;scene.spawningEnabled=false;
                    for(int slot=0;slot<5;slot++)
                    {
                        check(hero+" selected slot "+slot,scene.weaponSlot==slot);pose(scene,Array.Find(oracle.recipes,r=>r.hero==hero&&r.slot==slot&&r.pose=="stand"));
                        game.Step(1f/60f);if(slot==0||slot==4)capture("hero"+hero+"_slot"+slot+"_stand");
                        scene.rightDown=true;game.Step(1f/60f);pose(scene,Array.Find(oracle.recipes,r=>r.hero==hero&&r.slot==slot&&r.pose=="move"));
                        scene.rightDown=false;game.Step(1f/60f);game.Step(1f/60f);
                        int frame=scene.Rig.Player.CurrentFrame;uint acc=B(scene.Rig.Player.Accumulator);int emitted=scene.Rig.Player.DisplayedFrame;
                        scene.SwitchWeapon();check(hero+" native switch retains clock/emission "+slot,scene.Rig.Player.CurrentFrame==frame&&B(scene.Rig.Player.Accumulator)==acc&&scene.Rig.Player.DisplayedFrame==emitted);
                    }
                }
            }
            catch(Exception e){fail++;log.AppendLine("EXCEPTION "+e);}
            if(game!=null)UnityEngine.Object.DestroyImmediate(game.gameObject);if(cam!=null)UnityEngine.Object.DestroyImmediate(cam.gameObject);if(Directory.Exists(temp))Directory.Delete(temp,true);log.AppendLine("SUMMARY passes="+pass+" fails="+fail);File.WriteAllText(Path.Combine(output,"verify.txt"),log.ToString());Debug.Log(log.ToString());PilotExit.Exit(fail==0?0:1);
        }
    }
}
