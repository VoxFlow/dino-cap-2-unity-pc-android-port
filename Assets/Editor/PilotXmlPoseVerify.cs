using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
namespace Dc2Pilot.EditorTools
{
    public static class PilotXmlPoseVerify
    {
        [Serializable] public class Part {public string name,reference;public float[] values;public uint color_native;public int layer;}
        [Serializable] public class Frame {public int index,segment;public float ratio;public Part[] parts;}
        [Serializable] public class Asset {public string name;public int frame_count,part_count;public Frame[] frames;}
        [Serializable] public class Oracle {public Asset[] assets;}
        public static void Run()
        {
            string output=Path.Combine(Application.dataPath,"../Reports/DC2-PILOT-001/xml-r37");var args=Environment.GetCommandLineArgs();
            for(int i=0;i<args.Length-1;i++)if(args[i]=="-reportDir")output=args[i+1];Directory.CreateDirectory(output);
            int passed=0,failed=0;var log=new StringBuilder();Action<string,bool> check=(n,v)=>{if(v)passed++;else failed++;log.AppendLine((v?"PASS ":"FAIL ")+n);};
            try
            {
                Atlas.LoadNativeTable();
                var oracle=JsonUtility.FromJson<Oracle>(File.ReadAllText(Path.Combine(Application.dataPath,"../../Dev/Reports/DC2-PILOT-001/xml_pose_expansion.r35d.json")));
                foreach(var asset in oracle.assets)
                {
                    var animation=NativeXmlCaAnimation.Load("DC2/Image/an2/"+asset.name+".an2",asset.name=="p_roll"?1.5f:1f);
                    check(asset.name+" native expanded frame count",animation.frames.Length==asset.frame_count);
                    foreach(var frame in asset.frames)
                    {
                        var f=animation.frames[frame.index];bool same=f.parts.Length==frame.parts.Length;
                        for(int i=0;same&&i<f.parts.Length;i++)
                        {
                            var p=f.parts[i];var expected=frame.parts[i];
                            same=p.name==expected.name&&p.reference==expected.reference&&p.rgba==expected.color_native&&unchecked((int)p.layerBits)==expected.layer;
                            var values=new[]{p.x,p.y,p.w,p.h,p.pivotX,p.pivotY,p.radians};
                            for(int j=0;same&&j<values.Length;j++)same=BitConverter.SingleToInt32Bits(values[j])==BitConverter.SingleToInt32Bits(expected.values[j]);
                        }
                        check(asset.name+" frame"+frame.index+" every part/order/ARM numeric word",same);
                    }
                }
            }
            catch(Exception e){failed++;log.AppendLine(e.ToString());}
            log.AppendLine("SUMMARY passes="+passed+" fails="+failed);File.WriteAllText(Path.Combine(output,"verify.txt"),log.ToString());Debug.Log(log.ToString());PilotExit.Exit(failed==0?0:1);
        }
    }
}
