using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
namespace Dc2Pilot.EditorTools
{
    public static class PilotShotAppearanceVerify
    {
        [Serializable] class Shots{public Shot[] cases;}
        [Serializable] class Shot{public int type,counter_before,counter_after;public string suffix_before,suffix_after;}
        [Serializable] class Starts{public Start[] cases;}
        [Serializable] class Start{public int weapon_type,counter_before,counter_after;public bool already_firing;}
        [Serializable] class Timeouts{public Timeout[] cases;}
        [Serializable] class Timeout{public int counter;public string suffix_after;}
        [Serializable] class Manifest{public Frame[] frames;}
        [Serializable] class Frame{public string name;public int[] rect;}
        public static void Run()
        {
            string output=null;var args=Environment.GetCommandLineArgs();for(int i=0;i<args.Length-1;i++)if(args[i]=="-reportDir")output=args[i+1];Directory.CreateDirectory(output);var log=new StringBuilder();int pass=0,fail=0;GameObject root=null;Camera cam=null;
            Action<string,bool> check=(n,ok)=>{log.AppendLine((ok?"PASS ":"FAIL ")+n);if(ok)pass++;else fail++;};
            string report=Path.Combine(Application.dataPath,"../../Dev/Reports/DC2-PILOT-001");
            try
            {
                foreach(var row in JsonUtility.FromJson<Shots>(File.ReadAllText(Path.Combine(report,"shot_dispatch_oracle.r91.json"))).cases)
                {
                    var a=new NativeShotAppearance{Counter=row.counter_before,Suffix=row.suffix_before};a.AfterKaiQiang(row.type);check("native shot counter/suffix "+row.type+"/"+row.counter_before,a.Counter==row.counter_after&&a.Suffix==row.suffix_after);
                }
                foreach(var row in JsonUtility.FromJson<Starts>(File.ReadAllText(Path.Combine(report,"counter_start_oracle.r93.json"))).cases)
                {
                    var a=new NativeShotAppearance{Counter=row.counter_before};if(!row.already_firing)a.AcceptedBurstStart(row.weapon_type);check("native accepted burst counter "+row.weapon_type+"/"+row.already_firing,a.Counter==row.counter_after);
                }
                foreach(var row in JsonUtility.FromJson<Timeouts>(File.ReadAllText(Path.Combine(report,"fire_timeout_oracle.r91.json"))).cases)
                {
                    var a=new NativeShotAppearance{Counter=row.counter,Suffix="_nu3"};a.FireupTimeout();check("native fireup suffix/counter "+row.counter,a.Counter==row.counter&&a.Suffix==row.suffix_after);
                }
                Atlas.LoadNativeTable();var manifest=JsonUtility.FromJson<Manifest>(File.ReadAllText(Path.Combine(report,"face_asset_manifest.r92.json")));
                foreach(var frame in manifest.frames)
                {
                    var s=Atlas.Get(frame.name);check("native facial rect "+frame.name,s.rect.x==frame.rect[0]&&s.rect.y==s.texture.height-frame.rect[1]-frame.rect[3]&&s.rect.width==frame.rect[2]&&s.rect.height==frame.rect[3]);
                }
                cam=Bootstrap.MakeCamera();
                for(int hero=0;hero<4;hero++)
                {
                    root=new GameObject("native_face_suffix_fixture");var rig=HeroPreview.Create(root.transform);rig.SelectNativeLocomotion(hero,0,"stand");rig.Player.objectScale=1;
                    // DEBUG_ONLY supplied suffix, no live firing/timer activation.
                    for(int stage=1;stage<=3;stage++)
                    {
                        rig.Player.FrameSuffix="_nu"+stage;rig.Step(1f/60f);var basis=rig.Player.animation.frames[rig.Player.DisplayedFrame];var sprites=root.GetComponentsInChildren<SpriteRenderer>();int changed=0;
                        for(int i=0;i<basis.parts.Length;i++)
                        {
                            var p=basis.parts[i];if(p.reference.Length==0)continue;string alt=p.reference+"_nu"+stage+"Frame";bool present=Array.Exists(manifest.frames,f=>f.name==alt);string expected=present?alt:p.reference+"Frame";
                            check("bounded registered static suffix "+hero+"/"+stage+"/"+p.name,sprites[i].sprite==Atlas.Get(expected));if(present)changed++;
                        }
                        check("one recovered head variant "+hero+"/"+stage,changed==1);
                    }
                    var rt=new RenderTexture(1024,768,24);var image=new Texture2D(1024,768,TextureFormat.RGB24,false);cam.targetTexture=rt;Bootstrap.RenderAll(cam);RenderTexture.active=rt;image.ReadPixels(new Rect(0,0,1024,768),0,0);image.Apply();File.WriteAllBytes(Path.Combine(output,"hero"+hero+"_nu3.png"),image.EncodeToPNG());cam.targetTexture=null;RenderTexture.active=null;UnityEngine.Object.DestroyImmediate(rt);UnityEngine.Object.DestroyImmediate(image);UnityEngine.Object.DestroyImmediate(root);root=null;
                }
                check("supplied failed lookup retains original frame",NativeReferenceSuffix.Frame("tou0001","_missing",_=>false)=="tou0001Frame");
            }
            catch(Exception e){fail++;log.AppendLine("EXCEPTION "+e);}
            if(root!=null)UnityEngine.Object.DestroyImmediate(root);if(cam!=null)UnityEngine.Object.DestroyImmediate(cam.gameObject);log.AppendLine("SUMMARY passes="+pass+" fails="+fail);File.WriteAllText(Path.Combine(output,"verify.txt"),log.ToString());Debug.Log(log.ToString());PilotExit.Exit(fail==0?0:1);
        }
    }
}
