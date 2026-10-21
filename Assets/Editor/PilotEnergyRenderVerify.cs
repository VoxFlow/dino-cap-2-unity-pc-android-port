using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
namespace Dc2Pilot.EditorTools
{
    public static class PilotEnergyRenderVerify
    {
        [Serializable] class Oracle{public Case[] cases;}
        [Serializable] class Case{public bool looping,paused;public Tick[] ticks;}
        [Serializable] class Tick{public int dt_bits;public uint frame;}
        public static void Run()
        {
            string output=null;var args=Environment.GetCommandLineArgs();for(int i=0;i<args.Length-1;i++)if(args[i]=="-reportDir")output=args[i+1];Directory.CreateDirectory(output);var log=new StringBuilder();int pass=0,fail=0;GameObject root=null;Camera cam=null;
            Action<string,bool> check=(n,ok)=>{log.AppendLine((ok?"PASS ":"FAIL ")+n);if(ok)pass++;else fail++;};
            try
            {
                Atlas.LoadNativeTable();root=new GameObject("native_energy_render_verify");var rig=HeroPreview.Create(root.transform);rig.Player.objectScale=1;
                // Native loading effect has com15; Knight stand/move lack com1.
                var native=NativeCaAnimation.Load("DC2/Image/an2/yuanli.a2b",true);native.SetSpriteAnimation("com15");rig.Player.Play(native,-1);
                var oracle=JsonUtility.FromJson<Oracle>(File.ReadAllText(Path.Combine(Application.dataPath,"../../Dev/Reports/DC2-PILOT-001/sprite_clock_oracle.r82b.json")));Case truth=null;foreach(var c in oracle.cases)if(c.looping&&!c.paused)truth=c;
                for(int tick=0;tick<truth.ticks.Length;tick++)
                {
                    rig.Step(N.F(truth.ticks[tick].dt_bits));int index=Array.FindIndex(native.frames[rig.Player.DisplayedFrame].parts,p=>p.name=="com15");
                    var sprites=root.GetComponentsInChildren<SpriteRenderer>();check("loading native energy family tick"+tick,sprites[index].sprite==Atlas.Get("yuanli000"+(truth.ticks[tick].frame+1)+"Frame"));
                }
                // DEBUG_ONLY supplied identity transition; does not assert native
                // Knight stand/move contain a matching part or use this transition.
                var changed=native.Clone();changed.ChangeImage("com15","yuanli20001");rig.Player.Play(changed,-1);rig.Step(1f/60f);
                int part=Array.FindIndex(changed.frames[rig.Player.DisplayedFrame].parts,p=>p.name=="com15");check("identity change resets native energy clock",root.GetComponentsInChildren<SpriteRenderer>()[part].sprite==Atlas.Get("yuanli20001Frame"));
                foreach(var f in changed.frames)foreach(var p in f.parts)if(p.name=="com15")p.spriteAnimation=false;
                rig.Step(1f/60f);foreach(var f in changed.frames)foreach(var p in f.parts)if(p.name=="com15")p.spriteAnimation=true;
                rig.Step(1f/60f);part=Array.FindIndex(changed.frames[rig.Player.DisplayedFrame].parts,p=>p.name=="com15");check("static to animation resets native clock",root.GetComponentsInChildren<SpriteRenderer>()[part].sprite==Atlas.Get("yuanli20001Frame"));
                // DEBUG_ONLY fixture position0/scale1, original numeric pose unchanged.
                rig.Player.Play(native,-1);rig.Step(1f/60f);cam=Bootstrap.MakeCamera();var rt=new RenderTexture(1024,768,24);var image=new Texture2D(1024,768,TextureFormat.RGB24,false);
                cam.targetTexture=rt;Bootstrap.RenderAll(cam);RenderTexture.active=rt;image.ReadPixels(new Rect(0,0,1024,768),0,0);image.Apply();File.WriteAllBytes(Path.Combine(output,"loading_energy_fixture.png"),image.EncodeToPNG());cam.targetTexture=null;RenderTexture.active=null;UnityEngine.Object.DestroyImmediate(rt);UnityEngine.Object.DestroyImmediate(image);
            }
            catch(Exception e){fail++;log.AppendLine("EXCEPTION "+e);}
            if(root!=null)UnityEngine.Object.DestroyImmediate(root);if(cam!=null)UnityEngine.Object.DestroyImmediate(cam.gameObject);log.AppendLine("SUMMARY passes="+pass+" fails="+fail);File.WriteAllText(Path.Combine(output,"verify.txt"),log.ToString());Debug.Log(log.ToString());PilotExit.Exit(fail==0?0:1);
        }
    }
}
