using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
namespace Dc2Pilot.EditorTools
{
    public static class PilotSpriteAnchorVerify
    {
        [Serializable] class Oracle{public Case[] cases;}
        [Serializable] class Case{public string selection;public Child[] children;public bool flip;public float[] point;public uint[] result_bits;public Trig[] trig;}
        [Serializable] class Child{public string name;public bool enabled;public float[] position,pivot;public float rotation;}
        [Serializable] class Trig{public string function;public int value_bits;}
        public static void Run()
        {
            string output=null;var args=Environment.GetCommandLineArgs();for(int i=0;i<args.Length-1;i++)if(args[i]=="-reportDir")output=args[i+1];Directory.CreateDirectory(output);var log=new StringBuilder();int pass=0,fail=0;
            try
            {
                var oracle=JsonUtility.FromJson<Oracle>(File.ReadAllText(Path.Combine(Application.dataPath,"../../Dev/Reports/DC2-PILOT-001/sprite_anchor_oracle.r80.json")));
                foreach(var row in oracle.cases)
                {
                    var children=new NativeSpriteAnchor.Child[row.children.Length];float sin=0,cos=1;
                    foreach(var trig in row.trig){if(trig.function=="sin")sin=N.F(trig.value_bits);else cos=N.F(trig.value_bits);}
                    for(int i=0;i<children.Length;i++){var c=row.children[i];children[i]=new NativeSpriteAnchor.Child{name=c.name,enabled=c.enabled,position=new Vector2(c.position[0],c.position[1]),pivot=new Vector2(c.pivot[0],c.pivot[1]),sin=sin,cos=cos};}
                    var actual=NativeSpriteAnchor.GetPoint(children,"tou",row.flip,new Vector2(row.point[0],row.point[1]));
                    bool ok=unchecked((uint)BitConverter.SingleToInt32Bits(actual.x))==row.result_bits[0]&&unchecked((uint)BitConverter.SingleToInt32Bits(actual.y))==row.result_bits[1];
                    log.AppendLine((ok?"PASS ":"FAIL ")+row.selection+" flip="+row.flip+" point="+actual);if(ok)pass++;else fail++;
                }
            }
            catch(Exception e){fail++;log.AppendLine("EXCEPTION "+e);}
            log.AppendLine("SUMMARY passes="+pass+" fails="+fail);File.WriteAllText(Path.Combine(output,"verify.txt"),log.ToString());Debug.Log(log.ToString());PilotExit.Exit(fail==0?0:1);
        }
    }
}
