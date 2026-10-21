using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
namespace Dc2Pilot.EditorTools
{
    public static class PilotMeleePoseVerify
    {
        [Serializable] class Oracle{public Asset[] assets;}
        [Serializable] class Asset{public Frame[] frames;public int frame_count;}
        [Serializable] class Frame{public int index;public Part[] parts;}
        [Serializable] class Part{public string name,reference;public float[] values;public uint color_native;public int layer;}
        static uint B(float v){return unchecked((uint)BitConverter.SingleToInt32Bits(v));}
        public static void Run()
        {
            string output=null;var args=Environment.GetCommandLineArgs();for(int i=0;i<args.Length-1;i++)if(args[i]=="-reportDir")output=args[i+1];Directory.CreateDirectory(output);var log=new StringBuilder();int pass=0,fail=0;GameObject root=null;Camera cam=null;
            Action<string,bool> check=(n,ok)=>{log.AppendLine((ok?"PASS ":"FAIL ")+n);if(ok)pass++;else fail++;};
            try
            {
                Atlas.LoadNativeTable();cam=Bootstrap.MakeCamera();
                for(int hero=0;hero<4;hero++)
                {
                    root=new GameObject("native_melee_pose_verify");var rig=HeroPreview.Create(root.transform);rig.SelectNativeMelee(hero);
                    // DEBUG_ONLY capture fixture; no production placement/scale selected here.
                    rig.Player.position=Vector2.zero;rig.Player.objectScale=1f;
                    var oracle=JsonUtility.FromJson<Oracle>(File.ReadAllText(Path.Combine(Application.dataPath,"../../Dev/Reports/DC2-PILOT-001/xml_pose_expansion."+(hero==3?"r75":"r77")+".json"))).assets[0];
                    var recipe=NativeHeroMelee.Get(hero);var a=rig.Player.animation;
                    check(hero+" native expanded frame count",a.frames.Length==oracle.frame_count);
                    foreach(var f in oracle.frames)
                    {
                        var actual=a.frames[f.index];check(hero+" native key/count "+f.index,actual.key==(f.index==10)&&actual.parts.Length==f.parts.Length);
                        for(int i=0;i<f.parts.Length;i++)
                        {
                            var e=f.parts[i];var p=actual.parts[i];string reference=e.reference;foreach(var op in recipe.events)if(op.action=="image"&&op.part==e.name)reference=op.reference;
                            float[] values={p.x,p.y,p.w,p.h,p.pivotX,p.pivotY,p.radians};bool ok=p.name==e.name&&p.reference==reference&&p.rgba==e.color_native&&p.layerBits==unchecked((uint)e.layer);
                            for(int j=0;j<7;j++)ok&=B(values[j])==B(e.values[j]);check(hero+" native part "+f.index+"/"+e.name,ok);
                        }
                    }
                    int keys=0;rig.Player.keyFrame=f=>{if(f==10)keys++;};
                    for(int tick=1;tick<=38;tick++)
                    {
                        rig.Step(1f/60f);if(tick!=1&&tick!=21&&tick!=35)continue;
                        var rt=new RenderTexture(1024,768,24);var image=new Texture2D(1024,768,TextureFormat.RGB24,false);
                        cam.targetTexture=rt;Bootstrap.RenderAll(cam);RenderTexture.active=rt;image.ReadPixels(new Rect(0,0,1024,768),0,0);image.Apply();File.WriteAllBytes(Path.Combine(output,"hero"+hero+"_tick"+tick+".png"),image.EncodeToPNG());cam.targetTexture=null;RenderTexture.active=null;UnityEngine.Object.DestroyImmediate(rt);UnityEngine.Object.DestroyImmediate(image);
                    }
                    check(hero+" finite playback ended",rig.Player.Ended);check(hero+" native marker once",keys==1);
                    UnityEngine.Object.DestroyImmediate(root);root=null;
                }
            }
            catch(Exception e){fail++;log.AppendLine("EXCEPTION "+e);}
            if(root!=null)UnityEngine.Object.DestroyImmediate(root);if(cam!=null)UnityEngine.Object.DestroyImmediate(cam.gameObject);log.AppendLine("SUMMARY passes="+pass+" fails="+fail);File.WriteAllText(Path.Combine(output,"verify.txt"),log.ToString());Debug.Log(log.ToString());PilotExit.Exit(fail==0?0:1);
        }
    }
}
