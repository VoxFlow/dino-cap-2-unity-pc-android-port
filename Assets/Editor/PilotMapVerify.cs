using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>Independent native-data, endpoint, boss-save and 60Hz map-render checks.</summary>
    public static class PilotMapVerify
    {
        public static void Run()
        {
            string output=Path.Combine(Application.dataPath,"../Reports/DC2-PILOT-001/map-r5");
            var args=Environment.GetCommandLineArgs();
            for(int i=0;i<args.Length-1;i++) if(args[i]=="-reportDir") output=args[i+1];
            Directory.CreateDirectory(output);
            string dir=Path.Combine(Path.GetTempPath(),"dc2_map_verify_"+Guid.NewGuid().ToString("N"));
            int passed=0,failed=0; var log=new StringBuilder();
            Action<string,bool> check=(n,v)=>{log.AppendLine((v?"PASS ":"FAIL ")+n);if(v)passed++;else failed++;};
            Dc2Game game=null; Camera camera=null;
            try
            {
                var fresh=new Profile();
                check("native five hero levels0/-1/-1/-1/-1",fresh.level.Length==5 && fresh.level[0]==0 && fresh.level[1]==-1 && fresh.level[2]==-1 && fresh.level[3]==-1 && fresh.level[4]==-1);
                check("native five zero exp slots",fresh.exp.Length==5 && fresh.exp[4]==0);
                fresh.level[2]=9;fresh.sound=0;fresh.ResetFileKeepAudio();
                check("reset reloads native locked levels and keeps audio",fresh.level[2]==-1 && fresh.level[4]==-1 && fresh.sound==0);
                check("RandomRange lower endpoint",NativeRandom.RangeFromRaw(4,6,0)==4);
                check("RandomRange float rounding upper endpoint",NativeRandom.RangeFromRaw(4,6,int.MaxValue)==6);
                check("RandomRange midpoint",NativeRandom.RangeFromRaw(4,6,1073741824)==5);
                var state=new NativeMapState();state.rawRandom=()=>0;
                check("shipped Map.xml30 records",state.missions.Count==30);
                check("shipped point.xml25 records",state.positions.Count==25);
                check("five configured boss records",state.bossMapCount==5);
                check("first mission kill20 difficulty1",state.missions[0].type==3 && state.missions[0].count==20 && state.missions[0].level==1);
                check("last mission boss20 difficulty5",state.missions[29].type==8 && state.missions[29].count==20 && state.missions[29].level==5);
                check("first point19/88/bg0",state.positions[0].x==19 && state.positions[0].y==88 && state.positions[0].bg==0);
                state.Generate(dir,1,0);
                check("missing boss file resets inactive seed5 level-1",!state.boss.active && state.boss.seed==5 && state.boss.level==-1);
                check("native boss save21 bytes",new FileInfo(Path.Combine(dir,"dinocap2_boss_point.sav")).Length==21);
                check("raw0 generates one normal mission0 at point0",state.choices.Count==1 && state.choices[0].mapIndex==0 && state.choices[0].point.x==19);
                state.Generate(dir,5,0);
                check("mission threshold creates reserved boss point",state.boss.active && state.boss.seed==9 && state.boss.level==1 && state.boss.point.x==19);
                check("boss removes one position via swap back",state.positions.Count==24 && state.positions[0].x==420);
                var restored=new NativeMapState(); restored.rawRandom=()=>0;restored.ReadBoss(dir,6);
                check("boss read restores x/y/bg/seed/level",restored.boss.active && restored.boss.point.x==19 && restored.boss.point.y==88 && restored.boss.point.bg==0 && restored.boss.seed==9 && restored.boss.level==1);
                using(var r=new BinaryReader(File.OpenRead(Path.Combine(dir,"dinocap2_boss_point.sav"))))
                    check("native field widths/order in save",r.ReadByte()==1 && r.ReadInt32()==0 && r.ReadSingle()==19 && r.ReadSingle()==88 && r.ReadInt32()==9 && r.ReadInt32()==1);
                restored.Generate(dir,6,0);
                check("reload keeps boss reserved position",restored.positions.Count==24 && restored.choices[0].boss && restored.choices[0].point.x==19);
                // Reset native boss state before the fresh production menu render.
                state=new NativeMapState();state.rawRandom=()=>0;state.ResetBoss(1);state.SaveBoss(dir);
                camera=Bootstrap.MakeCamera();game=Dc2Game.Create(dir);game.manualStep=true;game.SkipMissionIntro=true;game.cam=camera;game.MapState=state;
                game.SwitchScene("selectScene");
                var scene=(SelectScene)game.Current;
                check("point waits for bg56 completion",scene.MapChoices.Count==0);
                game.StepFor(0.6f);
                check("bg56 completion creates point300",scene.MapChoices.Count==1 && scene.GetControl(300)?.FrameName=="ui_map_point_type3Frame");
                Rect p=scene.GetControl(300).FullRect;
                check("native point rect70/208/60/70",p==new Rect(70,208,60,70));
                check("ring310 uses native point background",scene.GetControl(310)?.FrameName=="ui_map_point_bgFrame");
                var rt=new RenderTexture(1024,768,24);var image=new Texture2D(1024,768,TextureFormat.RGB24,false);
                camera.targetTexture=rt;camera.Render();RenderTexture.active=rt;image.ReadPixels(new Rect(0,0,1024,768),0,0);image.Apply();
                File.WriteAllBytes(Path.Combine(output,"map_point_native.png"),image.EncodeToPNG());
                camera.targetTexture=null;RenderTexture.active=null;UnityEngine.Object.DestroyImmediate(rt);UnityEngine.Object.DestroyImmediate(image);
                game.StepFor(4.2f);
                check("ring native completion restarts pulse",scene.GetControl(310).IsMoving);
                Vector2 hit=scene.GetControl(300).FullRect.center;scene.Touch(0,hit);scene.Touch(2,hit);game.StepFor(0.5f);
                check("point300 opens mission popup",scene.MissionOpen && scene.SelectedMapSlot==0);
                check("native popup background0/32/1024/704",scene.GetControl(67).FullRect==new Rect(0,32,1024,704));
                check("kill mission title sprite",scene.GetControl(68).FrameName=="ui_maptype3Frame");
                check("native target kill20",scene.MissionTarget==20);
                // Independently decoded IEEE word1113063424=54, not the earlier mistaken56.
                check("native popup back86/580/208/62",scene.GetControl(72).FullRect==new Rect(86,580,208,62));
                check("native popup start724/558/218/98",scene.GetControl(73).FullRect==new Rect(724,558,218,98));
                check("native difficulty claw400/402/82/94",scene.GetControl(471).FullRect==new Rect(400,402,82,94));
                check("popup removes bottom/map toolbar buttons",scene.GetControl(54)==null && scene.GetControl(64)==null);
                // Pixel differential: state/text assertions alone missed disappearing TextMesh glyphs.
                UiLabel countLabel=null;foreach(var l in scene.Labels)if(l.id==69)countLabel=l;
                rt=new RenderTexture(1024,768,24);image=new Texture2D(1024,768,TextureFormat.RGB24,false);
                camera.targetTexture=rt;camera.Render();RenderTexture.active=rt;image.ReadPixels(new Rect(0,0,1024,768),0,0);image.Apply();
                var withNumber=image.GetPixels32();countLabel.gameObject.SetActive(false);
                camera.Render();image.ReadPixels(new Rect(0,0,1024,768),0,0);image.Apply();var withoutNumber=image.GetPixels32();
                int changedPixels=0;for(int pidx=0;pidx<withNumber.Length;pidx++)if(!withNumber[pidx].Equals(withoutNumber[pidx]))changedPixels++;
                countLabel.gameObject.SetActive(true);
                check("mission number contributes visible rendered pixels",changedPixels>0);log.AppendLine("Number rendered differential pixels="+changedPixels);
                camera.targetTexture=null;RenderTexture.active=null;UnityEngine.Object.DestroyImmediate(rt);UnityEngine.Object.DestroyImmediate(image);
                rt=new RenderTexture(1024,768,24);image=new Texture2D(1024,768,TextureFormat.RGB24,false);
                camera.targetTexture=rt;camera.Render();RenderTexture.active=rt;image.ReadPixels(new Rect(0,0,1024,768),0,0);image.Apply();
                File.WriteAllBytes(Path.Combine(output,"mission_popup_native.png"),image.EncodeToPNG());
                camera.targetTexture=null;RenderTexture.active=null;UnityEngine.Object.DestroyImmediate(rt);UnityEngine.Object.DestroyImmediate(image);
                hit=scene.GetControl(72).FullRect.center;scene.Touch(0,hit);scene.Touch(2,hit);
                check("back clears native popup flag immediately",!scene.MissionOpen && scene.MissionLeaving);
                game.StepFor(0.7f);
                check("start73 completion removes popup",!scene.MissionLeaving && scene.GetControl(67)==null && scene.GetControl(73)==null);
                check("map toolbar and points restored",scene.GetControl(54)!=null && scene.GetControl(64)!=null && scene.GetControl(300)!=null);
                check("native leave label/unit lifetime preserved",scene.GetControl(70)!=null);
            }
            catch(Exception e){failed++;log.AppendLine("EXCEPTION "+e);}
            finally
            {
                if(game!=null)UnityEngine.Object.DestroyImmediate(game.gameObject);
                if(camera!=null)UnityEngine.Object.DestroyImmediate(camera.gameObject);
                if(Directory.Exists(dir))Directory.Delete(dir,true);
                log.AppendLine("SUMMARY passes="+passed+" fails="+failed);
                File.WriteAllText(Path.Combine(output,"verify.txt"),log.ToString());Debug.Log(log.ToString());PilotExit.Exit(failed==0?0:1);
            }
        }
    }
}
