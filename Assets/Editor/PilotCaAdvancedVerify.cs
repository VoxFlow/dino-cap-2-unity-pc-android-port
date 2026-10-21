using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
namespace Dc2Pilot.EditorTools
{
    public static class PilotCaAdvancedVerify
    {
        [Serializable] class Oracle {public TransformCase[] transforms;public MergeCase[] merges;}
        [Serializable] class TransformCase {public bool found;public uint input_angle_bits;public uint[] first,second;}
        [Serializable] class MergeCase {public bool active,found;public uint input_angle_bits,angle,rgba,sprite_animation;public uint[] xy,wh,pivot;}
        static float F(uint b){return BitConverter.Int32BitsToSingle(unchecked((int)b));}
        static uint B(float v){return unchecked((uint)BitConverter.SingleToInt32Bits(v));}
        static NativeCaAnimation.Part Part(string name,float x,float y,float angle)
        {return new NativeCaAnimation.Part{name=name,reference="advanced",x=x,y=y,w=37,h=41,pivotX=.25f,pivotY=-.75f,radians=angle,rgba=0x12345678,layerBits=0x87654321,spriteAnimation=true};}
        public static void Run()
        {
            string output=Path.GetFullPath(Path.Combine(Application.dataPath,"../Reports/DC2-PILOT-001/ca-advanced-r59"));var args=Environment.GetCommandLineArgs();for(int i=0;i<args.Length-1;i++)if(args[i]=="-reportDir")output=args[i+1];Directory.CreateDirectory(output);
            int pass=0,fail=0;var log=new StringBuilder();Action<string,bool> check=(n,ok)=>{log.AppendLine((ok?"PASS ":"FAIL ")+n);if(ok)pass++;else fail++;};
            try
            {
                var o=JsonUtility.FromJson<Oracle>(File.ReadAllText(Path.Combine(Application.dataPath,"../../Dev/Reports/DC2-PILOT-001/ca_advanced_oracle.r58.json")));
                foreach(var c in o.transforms)
                {
                    var first=Part(c.found?"same":"first",11.25f,-17.5f,0);var second=Part(c.found?"same":"other",-33.75f,44.5f,F(c.input_angle_bits));
                    // The missing-map oracle supplies no first-frame match either.
                    var a=new NativeCaAnimation{frames=new[]{new NativeCaAnimation.Frame{parts=new[]{first}},new NativeCaAnimation.Frame{parts=new[]{second}}}};
                    if(!c.found){ // test the actual name-lookup absence against an empty first-frame map
                        a.frames[0].parts=Array.Empty<NativeCaAnimation.Part>();
                    }
                    a.SetToAdvanced();
                    bool ok=B(second.x)==c.second[0]&&B(second.y)==c.second[1]&&B(second.radians)==c.second[6]&&B(second.w)==c.second[2]&&B(second.h)==c.second[3]&&B(second.pivotX)==c.second[4]&&B(second.pivotY)==c.second[5]&&second.rgba==c.second[9]&&second.layerBits==c.second[7]&&second.spriteAnimation&&second.reference=="advanced";
                    if(c.found)ok&=B(first.x)==c.first[0]&&B(first.y)==c.first[1]&&B(first.radians)==c.first[6];
                    check("native transform "+c.found+"/"+c.input_angle_bits,ok);
                }
                foreach(var c in o.merges)
                {
                    var basis=Part("same",11.25f,-17.5f,0);basis.w=111;basis.h=222;basis.pivotX=.5f;basis.pivotY=.75f;basis.rgba=0xaabbccdd;basis.reference="base";basis.spriteAnimation=false;
                    var overlay=Part(c.found?"same":"other",-33.75f,44.5f,F(c.input_angle_bits));
                    var merged=NativeCaAnimation.MergeAdvancedPart(basis,c.active?new NativeCaAnimation.Frame{parts=new[]{overlay}}:null);
                    bool ok=B(merged.x)==c.xy[0]&&B(merged.y)==c.xy[1]&&B(merged.w)==c.wh[0]&&B(merged.h)==c.wh[1]&&B(merged.radians)==c.angle&&merged.rgba==c.rgba&&B(merged.pivotX)==c.pivot[0]&&B(merged.pivotY)==c.pivot[1]&&merged.spriteAnimation==(c.sprite_animation!=0)&&merged.reference==(c.active&&c.found?"advanced":"base")&&merged.layerBits==basis.layerBits&&basis.x==11.25f&&basis.reference=="base";
                    check("native merge "+c.active+"/"+c.found+"/"+c.input_angle_bits,ok);
                }
            }
            catch(Exception e){fail++;log.AppendLine("EXCEPTION "+e);}
            log.AppendLine("SUMMARY passes="+pass+" fails="+fail);File.WriteAllText(Path.Combine(output,"verify.txt"),log.ToString());Debug.Log(log.ToString());PilotExit.Exit(fail==0?0:1);
        }
    }
}
