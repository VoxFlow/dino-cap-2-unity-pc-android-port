using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
namespace Dc2Pilot.EditorTools
{
    public static class PilotCaChannelsVerify
    {
        [Serializable] class Oracle{public Case[] cases;}
        [Serializable] class Case{public int loops,start;public float interval;public Tick[] ticks;}
        [Serializable] class Tick{public int tick,base_ended,advanced_ended;public uint @base,advanced,base_acc_bits,advanced_acc_bits;public Callback[] callbacks;}
        [Serializable] class Callback{public string kind;}
        static uint B(float value){return unchecked((uint)BitConverter.SingleToInt32Bits(value));}
        static NativeCaAnimation Source()
        {
            var a=new NativeCaAnimation{frames=new NativeCaAnimation.Frame[4]};
            for(int i=0;i<4;i++)a.frames[i]=new NativeCaAnimation.Frame{key=i==1,parts=new[]{new NativeCaAnimation.Part{name="part",reference="white",w=4,h=4,rgba=0xffffffff}}};
            return a;
        }
        public static void Run()
        {
            string output=Path.GetFullPath(Path.Combine(Application.dataPath,"../Reports/DC2-PILOT-001/ca-channels-r64"));var args=Environment.GetCommandLineArgs();for(int i=0;i<args.Length-1;i++)if(args[i]=="-reportDir")output=args[i+1];Directory.CreateDirectory(output);
            int pass=0,fail=0;var log=new StringBuilder();GameObject root=null;
            try
            {
                Atlas.EnsureWhite();var o=JsonUtility.FromJson<Oracle>(File.ReadAllText(Path.Combine(Application.dataPath,"../../Dev/Reports/DC2-PILOT-001/ca_channels_oracle.r64.json")));
                foreach(var c in o.cases)
                {
                    root=new GameObject("native_ca_channels_verify");var player=new NativeCaPlayer(root.transform,_=>root.transform,1);var callbacks=new List<string>();
                    player.keyFrame=_=>callbacks.Add("base_key");player.advancedKeyFrame=_=>callbacks.Add("advanced_key");player.animationEnd=()=>callbacks.Add("base_end");player.advancedAnimationEnd=()=>callbacks.Add("advanced_end");
                    player.Play(Source(),0,0,c.interval,1);player.SetAdvanced(Source());player.PlayAdvanced(c.loops,c.start,c.interval,1);
                    foreach(var tick in c.ticks)
                    {
                        callbacks.Clear();player.Step(1f/60f);
                        bool ok=(uint)player.CurrentFrame==tick.@base&&(uint)player.CurrentAdvancedFrame==tick.advanced&&B(player.Accumulator)==tick.base_acc_bits&&B(player.AdvancedAccumulator)==tick.advanced_acc_bits&&player.Ended==(tick.base_ended!=0)&&player.AdvancedEnded==(tick.advanced_ended!=0)&&callbacks.Count==tick.callbacks.Length;
                        if(callbacks.Count==tick.callbacks.Length)for(int i=0;i<callbacks.Count;i++)ok&=callbacks[i]==tick.callbacks[i].kind;
                        log.AppendLine((ok?"PASS ":"FAIL ")+c.loops+"/"+c.start+"/"+c.interval+" tick"+tick.tick+" callbacks="+string.Join(",",callbacks));if(ok)pass++;else fail++;
                    }
                    UnityEngine.Object.DestroyImmediate(root);root=null;
                }
            }
            catch(Exception e){fail++;log.AppendLine("EXCEPTION "+e);}
            if(root!=null)UnityEngine.Object.DestroyImmediate(root);
            log.AppendLine("SUMMARY passes="+pass+" fails="+fail);File.WriteAllText(Path.Combine(output,"verify.txt"),log.ToString());Debug.Log(log.ToString());PilotExit.Exit(fail==0?0:1);
        }
    }
}
