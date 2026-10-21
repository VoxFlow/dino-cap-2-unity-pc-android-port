using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
namespace Dc2Pilot.EditorTools
{
    public static class PilotJumpVerify
    {
        [Serializable] public class ModeCase { public int skin,nativeLevel,roll,mode; }
        [Serializable] public class SpawnCase { public string bounds; public bool playerFacingLeft; public float[] position,velocity; public int flag96; }
        [Serializable] public class SideCase { public int playerLevel,randWord; public float[] position,velocity; }
        [Serializable] public class Oracle { public ModeCase[] modes; public SpawnCase[] spawns; public SideCase[] sides; }
        public static void Run()
        {
            string output=Path.Combine(Application.dataPath,"../Reports/DC2-PILOT-001/jump-r33");
            var args=Environment.GetCommandLineArgs();for(int i=0;i<args.Length-1;i++)if(args[i]=="-reportDir")output=args[i+1];
            Directory.CreateDirectory(output);string dir=Path.Combine(Path.GetTempPath(),"dc2_jump_"+Guid.NewGuid().ToString("N"));
            int passed=0,failed=0;var log=new StringBuilder();Action<string,bool> check=(n,v)=>{log.AppendLine((v?"PASS ":"FAIL ")+n);if(v)passed++;else failed++;};
            Dc2Game game=null;Camera camera=null;RenderTexture rt=null;Texture2D image=null;
            try
            {
                var oracle=JsonUtility.FromJson<Oracle>(File.ReadAllText(Path.Combine(Application.dataPath,"../../Dev/Reports/DC2-PILOT-001/jump_contract_oracle.r32.json")));
                foreach(var c in oracle.modes)check("ARM mode skin"+c.skin+" L"+c.nativeLevel+" roll"+c.roll,NativeDinosaurSpawn.Mode(c.skin,c.nativeLevel-1,c.roll)==c.mode);
                foreach(var c in oracle.spawns)
                {
                    var bounds=c.bounds=="ground"?new Vector2(-153.45001220703125f,154.5500030517578f):new Vector2(1024,-1024);
                    var v=NativeDinosaurSpawn.Jump(-240,1024,bounds,c.playerFacingLeft);
                    check("ARM spawn "+c.bounds+" left"+c.playerFacingLeft,v.x==c.position[0]&&v.y==c.velocity[0]);
                }
                foreach(var c in oracle.sides)check("ARM mode3 L"+c.playerLevel+" word"+c.randWord,NativeDinosaurSpawn.Mode3Right(c.playerLevel,NativeDinosaurSpawn.UnitFromWord(c.randWord))==(c.velocity[0]<0));
                camera=Bootstrap.MakeCamera();game=Dc2Game.Create(dir);game.cam=camera;game.manualStep=true;game.SkipMissionIntro=true;
                rt=new RenderTexture(1024,768,24);image=new Texture2D(1024,768,TextureFormat.RGB24,false);
                Action<string> shot=name=>{camera.targetTexture=rt;Bootstrap.RenderAll(camera);RenderTexture.active=rt;image.ReadPixels(new Rect(0,0,1024,768),0,0);image.Apply();File.WriteAllBytes(Path.Combine(output,name),image.EncodeToPNG());camera.targetTexture=null;RenderTexture.active=null;};
                foreach(int kind in new[]{1,5})
                {
                    game.LaunchMission=new NativeMapState.Mission { type=3,count=99,level=1,background=7 };game.LaunchMission.enemies.Add(kind);
                    game.Profile.playerType=0;game.SwitchScene("gameScene");game.Step(0f);
                    var scene=(GameScene)game.Current;scene.spawningEnabled=false;scene.ignorePlayer=true;scene.DebugSpawnModeRoll=()=>99;scene.heroX=0;
                    var d=scene.CreateEnemy(kind);float x0=d.x;
                    check("jump family"+kind+" native initial state/velocity/flag",d.spawnMode==2&&d.state==7&&d.velocityX==-800&&!d.targetable);
                    check("raw native cached bounds family"+kind,d.aJump.minX==-153.45001220703125f&&d.aJump.maxX==154.5500030517578f);
                    var clone=d.aJump.Clone();clone.ChangeImage("shen","k10shen",2f);
                    check("Clone/image replacement preserves native cached bounds family"+kind,clone.minX==d.aJump.minX&&clone.maxX==d.aJump.maxX);
                    shot("jump_"+kind+"_frame0.png");
                    for(int tick=1;tick<=42;tick++)
                    {
                        game.Step(1f/60f);
                        if(tick==18)check("before marked emission family"+kind,d.rig.CurrentFrame==9&&d.state==7&&!d.targetable&&d.velocityX==-800);
                        if(tick==19)
                        {
                            check("marked frame9 stops after native physics family"+kind,d.rig.DisplayedFrame==9&&d.state==7&&d.targetable&&d.velocityX==0);
                            check("19 moved physics ticks family"+kind,Mathf.Abs((x0-d.x)-800f*19f/60f)<.003f);
                            shot("jump_"+kind+"_frame9.png");
                        }
                        if(tick==20)check("post-marker physics frozen family"+kind,Mathf.Abs((x0-d.x)-800f*19f/60f)<.003f);
                    }
                    check("final emission then same-update AI family"+kind,d.rig.DisplayedFrame==20&&d.state==0&&d.rig.CurrentFrame==0&&d.targetable);
                    shot("jump_"+kind+"_final.png");game.Step(1f/60f);check("move emission follows final Jump family"+kind,d.rig.DisplayedFrame==0&&d.state==0);
                }
                game.LaunchMission=new NativeMapState.Mission {type=3,count=99,background=7};game.LaunchMission.enemies.Add(14);
                game.SwitchScene("gameScene");game.Step(0f);var empty=(GameScene)game.Current;empty.spawningEnabled=false;empty.ignorePlayer=true;empty.DebugSpawnModeRoll=()=>99;
                var quirky=empty.CreateEnemy(1);check("empty first-slot bounds preserve shipped unusual spawn",quirky.x==N.Add32(N.Add32(empty.camLeft,1024f),-2048f)&&quirky.state==7);
                // Independent native candidate gates: player Attack bf784/bfa32 and JinAttack bdfb8/be23a read +172.
                foreach(string attack in new[]{"HeroAttack","JinAttack"})
                {
                    game.LaunchMission=new NativeMapState.Mission {type=3,count=99,background=7};game.LaunchMission.enemies.Add(1);
                    game.SwitchScene("gameScene");game.Step(0f);var scene=(GameScene)game.Current;
                    scene.spawningEnabled=false;scene.ignorePlayer=true;scene.DebugSpawnModeRoll=()=>99;scene.heroX=0;
                    var d=scene.CreateEnemy(1);d.x=160;d.hp=10000;   // r119d: melee reach is measured to the head (JinAttack 0xbe170), +160 keeps it in front
                    var method=typeof(GameScene).GetMethod(attack,System.Reflection.BindingFlags.NonPublic|System.Reflection.BindingFlags.Instance);
                    float hp=d.hp;method.Invoke(scene,null);
                    check(attack+" excludes Jump before marked callback",d.hp==hp&&d.state==7&&!d.targetable);
                    for(int tick=1;tick<=19;tick++)game.Step(1f/60f);
                    d.x=160;method.Invoke(scene,null);
                    check(attack+" accepts Jump after marked callback",d.targetable&&d.hp<hp);
                }
                game.SwitchScene("gameScene");game.Step(0f);var dyingScene=(GameScene)game.Current;
                dyingScene.spawningEnabled=false;dyingScene.ignorePlayer=true;dyingScene.DebugSpawnModeRoll=()=>99;
                var dying=dyingScene.CreateEnemy(1);
                typeof(GameScene).GetMethod("DinoDie",System.Reflection.BindingFlags.NonPublic|System.Reflection.BindingFlags.Instance).Invoke(dyingScene,new object[]{dying,null});
                dying.rig.animationEnd();
                check("death interrupts Jump end transition",dying.dead&&dying.state==3);
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
