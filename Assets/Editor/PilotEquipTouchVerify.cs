using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
namespace Dc2Pilot.EditorTools
{
    // Expectations come from actual ARM geometry/touch pairs, not host logic.
    public static class PilotEquipTouchVerify
    {
        [Serializable] class Oracle { public Case[] cases; }
        [Serializable] class Case { public string scene,label;public int hero,switches;public int[] down,up; }
        public static void Run()
        {
            string output=Path.GetFullPath(Path.Combine(Application.dataPath,"../Reports/DC2-PILOT-001/equip-touch-r56"));
            var args=Environment.GetCommandLineArgs();for(int i=0;i<args.Length-1;i++)if(args[i]=="-reportDir")output=args[i+1];
            Directory.CreateDirectory(output);string temp=Path.Combine(Path.GetTempPath(),"dc2_equip_touch_"+Guid.NewGuid().ToString("N"));
            Dc2Game game=null;Camera cam=null;int pass=0,fail=0;var log=new StringBuilder();
            Action<string,bool> check=(name,ok)=>{log.AppendLine((ok?"PASS ":"FAIL ")+name);if(ok)pass++;else fail++;};
            try
            {
                var oracle=JsonUtility.FromJson<Oracle>(File.ReadAllText(Path.Combine(Application.dataPath,"../../Dev/Reports/DC2-PILOT-001/equip_geometry_oracle.r56.json")));
                cam=Bootstrap.MakeCamera();game=Dc2Game.Create(temp);game.manualStep=true;game.SkipMissionIntro=true;game.cam=cam;
                game.LaunchMission=new NativeMapState.Mission{type=3,count=1000,background=0};
                foreach(int hero in new[]{0,4})
                {
                    game.Profile.playerType=hero;for(int slot=0;slot<5;slot++)game.Profile.weaponOwned[hero*6+slot]=0;
                    game.SwitchScene("gameScene");var scene=(GameScene)game.Current;scene.spawningEnabled=false;
                    foreach(var c in oracle.cases)
                    {
                        if(c.scene!="life"||c.hero!=hero)continue;
                        scene.weaponSlot=0;scene.Touch(0,new Vector2(c.down[0],c.down[1]));
                        check(hero+"/"+c.label+" native press keeps slot",scene.weaponSlot==0);
                        scene.Touch(2,new Vector2(c.up[0],c.up[1]));
                        check(hero+"/"+c.label+" native release result",scene.weaponSlot==c.switches);
                    }
                    scene.rolling=true;scene.fireDown=true;scene.hitHeld=true;int before=scene.weaponSlot;scene.SwitchWeapon();
                    check(hero+" native roll switch gate clears held inputs",!scene.fireDown&&!scene.hitHeld);
                    check(hero+" native roll gate preserves slot",scene.weaponSlot==before);scene.rolling=false;
                }
            }
            catch(Exception e){fail++;log.AppendLine("EXCEPTION "+e);}
            finally
            {
                if(game!=null)UnityEngine.Object.DestroyImmediate(game.gameObject);if(cam!=null)UnityEngine.Object.DestroyImmediate(cam.gameObject);
                if(Directory.Exists(temp))Directory.Delete(temp,true);log.AppendLine("SUMMARY passes="+pass+" fails="+fail);
                File.WriteAllText(Path.Combine(output,"verify.txt"),log.ToString());Debug.Log(log.ToString());PilotExit.Exit(fail==0?0:1);
            }
        }
    }
}
