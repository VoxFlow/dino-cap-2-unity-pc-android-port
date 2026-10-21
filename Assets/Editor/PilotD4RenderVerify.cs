using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
namespace Dc2Pilot.EditorTools
{
    public static class PilotD4RenderVerify
    {
        [Serializable] class Oracle{public Case[] cases;}
        [Serializable] class Case{public int frame_count;public bool looping,paused;public Tick[] ticks;}
        [Serializable] class Tick{public uint frame;}
        public static void Run()
        {
            string output=null;var args=Environment.GetCommandLineArgs();for(int i=0;i<args.Length-1;i++)if(args[i]=="-reportDir")output=args[i+1];Directory.CreateDirectory(output);var log=new StringBuilder();int pass=0,fail=0;GameObject root=null;Camera cam=null;
            Action<string,bool> check=(n,ok)=>{log.AppendLine((ok?"PASS ":"FAIL ")+n);if(ok)pass++;else fail++;};
            try
            {
                Atlas.LoadNativeTable();root=new GameObject("native_d4_render_verify");var rig=HeroPreview.Create(root.transform);rig.SelectNativeLocomotion(1,2,"stand");rig.Player.objectScale=1;
                rig.Player.SetAdvanced(NativeHeroFireRecipes.Load(1,2));rig.Player.PlayAdvanced(0,0,.025f);rig.Player.advancedAnimationEnd=()=>rig.Player.SetAdvanced(null);
                // Isolated native recipe/channel/render fixture; no input, ammo,
                // target or damage acceptance follows from this composition.
                var oracle=JsonUtility.FromJson<Oracle>(File.ReadAllText(Path.Combine(Application.dataPath,"../../Dev/Reports/DC2-PILOT-001/sprite_clock_oracle.r88.json")));Case clock=null;foreach(var c in oracle.cases)if(c.frame_count==2&&c.looping&&!c.paused)clock=c;
                cam=Bootstrap.MakeCamera();int marked=0;
                for(int tick=0;tick<24;tick++)
                {
                    bool overlay=rig.Player.AdvancedAnimation!=null;rig.Step(1f/60f);var basis=rig.Player.animation.frames[rig.Player.DisplayedFrame];int index=Array.FindIndex(basis.parts,p=>p.name=="yyj5");var sprite=root.GetComponentsInChildren<SpriteRenderer>()[index];
                    string expected=overlay?"D4-"+(clock.ticks[tick].frame+1)+"Frame":basis.parts[index].reference+"Frame";
                    check("native D4 overlay/static identity tick"+tick,sprite.sprite==Atlas.Get(expected));if(overlay)marked++;
                    if(tick!=7&&tick!=17)continue;
                    var rt=new RenderTexture(1024,768,24);var image=new Texture2D(1024,768,TextureFormat.RGB24,false);cam.targetTexture=rt;Bootstrap.RenderAll(cam);RenderTexture.active=rt;image.ReadPixels(new Rect(0,0,1024,768),0,0);image.Apply();File.WriteAllBytes(Path.Combine(output,"d4_tick"+(tick+1)+".png"),image.EncodeToPNG());cam.targetTexture=null;RenderTexture.active=null;UnityEngine.Object.DestroyImmediate(rt);UnityEngine.Object.DestroyImmediate(image);
                }
                check("finite advanced channel cleared and emitted both sprite families",rig.Player.AdvancedAnimation==null&&marked>0&&marked<24);
            }
            catch(Exception e){fail++;log.AppendLine("EXCEPTION "+e);}
            if(root!=null)UnityEngine.Object.DestroyImmediate(root);if(cam!=null)UnityEngine.Object.DestroyImmediate(cam.gameObject);log.AppendLine("SUMMARY passes="+pass+" fails="+fail);File.WriteAllText(Path.Combine(output,"verify.txt"),log.ToString());Debug.Log(log.ToString());PilotExit.Exit(fail==0?0:1);
        }
    }
}
