using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
namespace Dc2Pilot.EditorTools
{
    public static class PilotMode1Verify
    {
        [Serializable] public class Case { public float hero_velocity; public int randWord,rand_calls; public bool right; }
        [Serializable] public class Oracle { public Case[] cases; }
        public static void Run()
        {
            string output=Path.Combine(Application.dataPath,"../Reports/DC2-PILOT-001/mode1-r43");var args=Environment.GetCommandLineArgs();
            for(int i=0;i<args.Length-1;i++)if(args[i]=="-reportDir")output=args[i+1];Directory.CreateDirectory(output);
            string dir=Path.Combine(Path.GetTempPath(),"dc2_mode1_"+Guid.NewGuid().ToString("N"));int passed=0,failed=0;var log=new StringBuilder();
            Action<string,bool> check=(n,v)=>{if(v)passed++;else failed++;log.AppendLine((v?"PASS ":"FAIL ")+n);};
            Dc2Game game=null;Camera camera=null;
            try
            {
                var oracle=JsonUtility.FromJson<Oracle>(File.ReadAllText(Path.Combine(Application.dataPath,"../../Dev/Reports/DC2-PILOT-001/mode1_side_oracle.r42.json")));
                foreach(var row in oracle.cases)
                {
                    int calls=0;bool right=NativeDinosaurSpawn.Mode1Right(row.hero_velocity,()=>{calls++;return NativeDinosaurSpawn.UnitFromWord(row.randWord);});
                    check("ARM side and lazy RNG v="+row.hero_velocity+" word="+row.randWord,right==row.right&&calls==row.rand_calls);
                }
                camera=Bootstrap.MakeCamera();game=Dc2Game.Create(dir);game.cam=camera;game.manualStep=true;game.SkipMissionIntro=true;
                Func<GameScene> fresh=()=>
                {
                    game.LaunchMission=new NativeMapState.Mission {type=3,count=99,background=7};game.LaunchMission.enemies.Add(14);
                    game.Profile.playerType=0;game.SwitchScene("gameScene");game.Step(0f);var s=(GameScene)game.Current;
                    s.spawningEnabled=false;s.ignorePlayer=true;s.heroX=1000;game.Step(0f);return s;
                };
                int draws=0;var scene=fresh();scene.DebugSpawnSideUnit=()=>{draws++;return 1f;};scene.rightDown=true;var d=scene.CreateEnemy(14);
                check("first held input still uses zero physics and random side",d.spawnMode==1&&d.x==scene.camLeft-280f&&draws==1);
                draws=0;scene=fresh();scene.DebugSpawnSideUnit=()=>{draws++;return 0f;};scene.rightDown=true;game.Step(1f/60f);d=scene.CreateEnemy(14);
                check("stored positive command selects right without RNG",scene.heroVelocityX==300f&&d.x==scene.camLeft+1024f+280f&&draws==0);
                draws=0;scene=fresh();scene.DebugSpawnSideUnit=()=>{draws++;return 1f;};scene.leftDown=true;scene.OnRollDown();d=scene.CreateEnemy(14);
                check("blocked opposite input cannot override Roll velocity",scene.RollVelocity==60f&&d.x==scene.camLeft+1024f+280f&&draws==0);
                draws=0;scene=fresh();scene.DebugSpawnSideUnit=()=>{draws++;return 0f;};scene.heroX=-240;scene.leftDown=true;game.Step(0f);game.Step(1f/60f);d=scene.CreateEnemy(14);
                check("clamped zero velocity returns to random side",scene.heroVelocityX==0f&&d.x==scene.camLeft+1024f+280f&&draws==1);
            }
            catch(Exception e){failed++;log.AppendLine(e.ToString());}
            finally
            {
                if(game!=null)UnityEngine.Object.DestroyImmediate(game.gameObject);if(camera!=null)UnityEngine.Object.DestroyImmediate(camera.gameObject);
                if(Directory.Exists(dir))Directory.Delete(dir,true);log.AppendLine("SUMMARY passes="+passed+" fails="+failed);File.WriteAllText(Path.Combine(output,"verify.txt"),log.ToString());Debug.Log(log.ToString());PilotExit.Exit(failed==0?0:1);
            }
        }
    }
}
