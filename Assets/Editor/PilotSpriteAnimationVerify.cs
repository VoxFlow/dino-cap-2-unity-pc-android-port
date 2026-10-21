using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
namespace Dc2Pilot.EditorTools
{
    public static class PilotSpriteAnimationVerify
    {
        [Serializable] class Oracle{public Case[] cases;}
        [Serializable] class Case{public int frame_count;public bool looping,paused;public Tick[] ticks;}
        [Serializable] class Tick{public int tick,dt_bits,time_bits;public uint frame;public int ended,texture;}
        public static void Run()
        {
            string output=null;var args=Environment.GetCommandLineArgs();for(int i=0;i<args.Length-1;i++)if(args[i]=="-reportDir")output=args[i+1];Directory.CreateDirectory(output);var log=new StringBuilder();int pass=0,fail=0;
            try
            {
                var oracle=JsonUtility.FromJson<Oracle>(File.ReadAllText(Path.Combine(Application.dataPath,"../../Dev/Reports/DC2-PILOT-001/sprite_clock_oracle.r88.json")));
                foreach(string name in new[]{"D4Anim","yuanli20001Anim","yuanli0001Anim"})foreach(var row in oracle.cases)
                {
                    if(row.frame_count!=(name=="D4Anim"?2:3))continue;
                    var a=new NativeSpriteAnimation{Paused=row.paused};a.Play(name,row.looping,30f);
                    foreach(var tick in row.ticks)
                    {
                        a.Step(N.F(tick.dt_bits));string frame=(name=="D4Anim"?"D4-":name=="yuanli20001Anim"?"yuanli2000":"yuanli000")+(tick.frame+1)+"Frame";
                        bool ok=BitConverter.SingleToInt32Bits(a.Time)==tick.time_bits&&a.FrameIndex==tick.frame&&a.Ended==(tick.ended!=0)&&a.FrameName==frame;
                        log.AppendLine((ok?"PASS ":"FAIL ")+name+" loop="+row.looping+" pause="+row.paused+" tick="+tick.tick);if(ok)pass++;else fail++;
                    }
                }
            }
            catch(Exception e){fail++;log.AppendLine("EXCEPTION "+e);}
            log.AppendLine("SUMMARY passes="+pass+" fails="+fail);File.WriteAllText(Path.Combine(output,"verify.txt"),log.ToString());Debug.Log(log.ToString());PilotExit.Exit(fail==0?0:1);
        }
    }
}
