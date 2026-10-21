using System;
using System.IO;
using System.Text;
using System.Reflection;
using UnityEditor;
using UnityEngine;
namespace Dc2Pilot.EditorTools
{
    public static class PilotWorldPhaseVerify
    {
        public static void Run()
        {
            string output=Path.Combine(Application.dataPath,"../Reports/DC2-PILOT-001/world-phase-r41");var args=Environment.GetCommandLineArgs();
            for(int i=0;i<args.Length-1;i++)if(args[i]=="-reportDir")output=args[i+1];Directory.CreateDirectory(output);
            string dir=Path.Combine(Path.GetTempPath(),"dc2_phase_"+Guid.NewGuid().ToString("N"));int passed=0,failed=0;var log=new StringBuilder();
            Action<string,bool> check=(n,v)=>{if(v)passed++;else failed++;log.AppendLine((v?"PASS ":"FAIL ")+n);};
            Dc2Game game=null;Camera camera=null;RenderTexture rt=null;Texture2D image=null;
            try
            {
                camera=Bootstrap.MakeCamera();game=Dc2Game.Create(dir);game.cam=camera;game.manualStep=true;game.SkipMissionIntro=true;
                rt=new RenderTexture(1024,768,24);image=new Texture2D(1024,768,TextureFormat.RGB24,false);
                Action<string> shot=name=>{camera.targetTexture=rt;Bootstrap.RenderAll(camera);RenderTexture.active=rt;image.ReadPixels(new Rect(0,0,1024,768),0,0);image.Apply();File.WriteAllBytes(Path.Combine(output,name),image.EncodeToPNG());camera.targetTexture=null;RenderTexture.active=null;};
                Func<int,GameScene> fresh=type=>
                {
                    game.LaunchMission=new NativeMapState.Mission {type=type,count=99,background=7};game.LaunchMission.enemies.Add(1);
                    game.Profile.playerType=0;game.SwitchScene("gameScene");game.Step(0f);var s=(GameScene)game.Current;
                    s.spawningEnabled=false;s.ignorePlayer=true;s.DebugSpawnModeRoll=()=>0;s.heroX=1000;game.Step(0f);return s;
                };
                var scene=fresh(3);scene.rightDown=true;game.Step(1f/60f);
                check("first held input commands next physics step",scene.heroX==1000&&scene.heroVelocityX==300&&scene.heroControlState==1);
                game.Step(1f/60f);check("second tick consumes stored velocity",scene.heroX==1005);
                scene.rightDown=false;game.Step(1f/60f);check("release follows physics then stops",scene.heroX==1010&&scene.heroVelocityX==0&&scene.heroControlState==2);
                game.Step(1f/60f);check("next released tick stays stopped",scene.heroX==1010);
                scene=fresh(3);var before=scene.Rig.GetComponentsInChildren<SpriteRenderer>()[0];float beforeX=before.transform.position.x;float cameraBefore=scene.camLeft;
                scene.leftDown=true;game.Step(1f/60f);var after=scene.Rig.GetComponentsInChildren<SpriteRenderer>()[0];
                check("turn emits preceding face before control",scene.facingLeft&&!after.flipX&&scene.heroX==1000);
                check("same-frame camera uses exact projection delta",Mathf.Abs(after.transform.position.x-beforeX-(cameraBefore-scene.camLeft))<.001f&&cameraBefore-scene.camLeft==10f);
                shot("turn_old_face_new_camera.png");game.Step(1f/60f);
                check("following emission uses new face",scene.heroX==995&&scene.Rig.GetComponentsInChildren<SpriteRenderer>()[0].flipX);
                scene=fresh(3);scene.rightDown=true;scene.OnRollDown();
                for(int tick=0;tick<52;tick++)game.Step(1f/60f);
                check("blocked roll preserves held input and resumes in end update",scene.rightDown&&!scene.rolling&&scene.heroControlState==1&&scene.heroVelocityX==300&&scene.heroX==1568.75f);
                var rollOracle=JsonUtility.FromJson<PilotXmlPoseVerify.Oracle>(File.ReadAllText(Path.Combine(Application.dataPath,"../../Dev/Reports/DC2-PILOT-001/xml_pose_expansion.r35d.json"))).assets[0].frames[25];
                bool finalParts=scene.Rig.Player.DisplayedFrame==25;int visible=0;
                foreach(var renderer in scene.Rig.GetComponentsInChildren<SpriteRenderer>())if(renderer.enabled)
                {
                    int index=renderer.sortingOrder-50;visible++;
                    finalParts&=index>=0&&index<rollOracle.parts.Length&&renderer.sprite.name==rollOracle.parts[index].reference+"Frame";
                    log.AppendLine("FINAL_ROLL_RENDER "+index+" "+renderer.sprite.name);
                }
                check("pending Move preserves every final Roll sprite identity",finalParts&&visible==rollOracle.parts.Length);
                shot("roll_final_then_pending_move.png");game.Step(1f/60f);
                check("pending move physics begins after final roll emission",scene.heroX==1573.75f&&scene.Rig.Player.DisplayedFrame==0);
                scene=fresh(7);scene.heroX=835;game.Step(0f);scene.OnRollDown();for(int tick=0;tick<6;tick++)game.Step(1f/60f);
                check("native actor boundary stops stored roll velocity",scene.heroX==840&&scene.RollVelocity==0);
                scene=fresh(3);scene.DebugSpawnModeRoll=()=>99;var jumper=scene.CreateEnemy(1);jumper.x=scene.heroX+100;jumper.velocityX=0;jumper.hp=10000;
                for(int tick=0;tick<18;tick++)game.Step(1f/60f);
                var fire=typeof(GameScene).GetMethod("HeroAttack",BindingFlags.Instance|BindingFlags.NonPublic);
                Action primeHero=()=>
                {
                    var f=scene.Rig.Player.animation.frames[0];f.key=true;scene.Rig.Player.Play(new NativeCaAnimation {frames=new[]{f}},-1,0,.025f);
                    scene.Rig.Player.keyFrame=n=>fire.Invoke(scene,null); // DEBUG_ONLY controlled CA callback, testing native object IDs1->2.
                };
                primeHero();float hp=jumper.hp;game.Step(1f/60f);
                check("hero scan precedes dinosaur frame9 flag enable",jumper.hp==hp&&jumper.targetable&&jumper.rig.DisplayedFrame==9);
                primeHero();game.Step(1f/60f);check("following hero scan sees enabled target",jumper.hp<hp);
                scene=fresh(3);scene.ignorePlayer=false;var attacker=scene.CreateEnemy(1);attacker.x=scene.heroX+30;attacker.faceRight=false;attacker.velocityX=0;attacker.atkDis=100;
                typeof(GameScene).GetMethod("StartAttack",BindingFlags.Instance|BindingFlags.NonPublic).Invoke(scene,new object[]{attacker});
                int count=attacker.aAttack.frames.Length;for(int tick=0;tick<count*2;tick++)game.Step(1f/60f);
                check("attack end replays in range without Move emission",attacker.state==1&&attacker.rig.CurrentFrame==0&&attacker.rig.DisplayedFrame==count-1&&!attacker.rig.Ended&&attacker.velocityX==0);
                shot("attack_final_then_replay.png");game.Step(1f/60f);
                check("replayed attack begins on following emission",attacker.state==1&&attacker.rig.DisplayedFrame==0);
            }
            catch(Exception e){failed++;log.AppendLine(e.ToString());}
            finally
            {
                if(game!=null)UnityEngine.Object.DestroyImmediate(game.gameObject);if(camera!=null)UnityEngine.Object.DestroyImmediate(camera.gameObject);if(rt!=null)UnityEngine.Object.DestroyImmediate(rt);if(image!=null)UnityEngine.Object.DestroyImmediate(image);
                if(Directory.Exists(dir))Directory.Delete(dir,true);log.AppendLine("SUMMARY passes="+passed+" fails="+failed);File.WriteAllText(Path.Combine(output,"verify.txt"),log.ToString());Debug.Log(log.ToString());PilotExit.Exit(failed==0?0:1);
            }
        }
    }
}
