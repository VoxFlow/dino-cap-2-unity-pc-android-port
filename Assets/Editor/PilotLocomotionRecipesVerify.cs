using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
namespace Dc2Pilot.EditorTools
{
    public static class PilotLocomotionRecipesVerify
    {
        [Serializable] class Oracle{public Recipe[] recipes;}
        [Serializable] class Recipe{public int hero,slot;public string pose;public Frame[] frames;}
        [Serializable] class Frame{public int index;public bool key;public Part[] parts;}
        [Serializable] class Part{public string name,reference;public uint[] words;public uint layer,rgba;public bool sprite_animation;}
        static uint B(float value){return unchecked((uint)BitConverter.SingleToInt32Bits(value));}
        public static void Run()
        {
            string output=Path.GetFullPath(Path.Combine(Application.dataPath,"../Reports/DC2-PILOT-001/locomotion-recipes-r70"));var args=Environment.GetCommandLineArgs();for(int i=0;i<args.Length-1;i++)if(args[i]=="-reportDir")output=args[i+1];Directory.CreateDirectory(output);
            int pass=0,fail=0;var log=new StringBuilder();Action<string,bool> check=(n,ok)=>{log.AppendLine((ok?"PASS ":"FAIL ")+n);if(ok)pass++;else fail++;};
            try
            {
                Atlas.LoadNativeTable();var oracle=JsonUtility.FromJson<Oracle>(File.ReadAllText(Path.Combine(Application.dataPath,"../../Dev/Reports/DC2-PILOT-001/locomotion_numeric_oracle.r69.json")));
                foreach(var r in oracle.recipes)
                {
                    var a=NativeHeroLocomotionRecipes.Load(r.hero,r.slot,r.pose);check(r.pose+"/"+r.hero+"/"+r.slot+" native frame count",a.frames.Length==r.frames.Length);
                    foreach(var f in r.frames)
                    {
                        var actual=a.frames[f.index];check(r.pose+"/"+r.hero+"/"+r.slot+"/"+f.index+" native key/part count",actual.key==f.key&&actual.parts.Length==f.parts.Length);
                        for(int i=0;i<f.parts.Length;i++)
                        {
                            var e=f.parts[i];var p=actual.parts[i];uint[] words={B(p.x),B(p.y),B(p.w),B(p.h),B(p.pivotX),B(p.pivotY),B(p.radians)};
                            bool ok=p.name==e.name&&p.reference==e.reference&&p.layerBits==e.layer&&p.rgba==e.rgba&&p.spriteAnimation==e.sprite_animation;
                            for(int j=0;j<7;j++)ok&=words[j]==e.words[j];check(r.pose+"/"+r.hero+"/"+r.slot+"/"+f.index+"/"+e.name+" native final fields",ok);
                        }
                    }
                }
            }
            catch(Exception e){fail++;log.AppendLine("EXCEPTION "+e);}
            log.AppendLine("SUMMARY passes="+pass+" fails="+fail);File.WriteAllText(Path.Combine(output,"verify.txt"),log.ToString());Debug.Log(log.ToString());PilotExit.Exit(fail==0?0:1);
        }
    }
}
