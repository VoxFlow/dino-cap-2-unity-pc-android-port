using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
namespace Dc2Pilot.EditorTools
{
    public static class PilotSelectedWeaponVerify
    {
        [Serializable] class Oracle{public Case[] cases;public int[] reset_vector;}
        [Serializable] class Case{public int hero,selected,entry_value;public int[] after_completion;}
        static bool Same(int[] a,int[] b){if(a==null||a.Length!=b.Length)return false;for(int i=0;i<a.Length;i++)if(a[i]!=b[i])return false;return true;}
        public static void Run()
        {
            string output=null;var args=Environment.GetCommandLineArgs();for(int i=0;i<args.Length-1;i++)if(args[i]=="-reportDir")output=args[i+1];Directory.CreateDirectory(output);var log=new StringBuilder();int pass=0,fail=0;Dc2Game game=null;Camera cam=null;string temp=Path.Combine(Path.GetTempPath(),"dc2_selected_weapon_"+Guid.NewGuid().ToString("N"));
            Action<string,bool> check=(n,ok)=>{log.AppendLine((ok?"PASS ":"FAIL ")+n);if(ok)pass++;else fail++;};
            try
            {
                var oracle=JsonUtility.FromJson<Oracle>(File.ReadAllText(Path.Combine(Application.dataPath,"../../Dev/Reports/DC2-PILOT-001/selected_slot_oracle.r86b.json")));
                cam=Bootstrap.MakeCamera();
                foreach(var row in oracle.cases)
                {
                    if(row.hero==4)continue; // Girl initial weapon/avatar composition remains outside active bounded male scope.
                    string dir=Path.Combine(temp,"h"+row.hero+"s"+row.selected);game=Dc2Game.Create(dir);game.cam=cam;game.manualStep=true;game.SkipMissionIntro=true;game.Profile.playerType=row.hero;game.Profile.selectedWeaponSlots=new[]{4,3,2,1,0};
                    for(int i=0;i<game.Profile.weaponOwned.Length;i++)game.Profile.weaponOwned[i]=0; // DEBUG_ONLY supplied ownership.
                    game.LaunchMission=new NativeMapState.Mission{type=3,count=1000,background=0};game.SwitchScene("gameScene");game.Step(0f);var scene=(GameScene)game.Current;scene.spawningEnabled=false;
                    for(int i=0;i<5&&scene.weaponSlot!=row.selected;i++)scene.SwitchWeapon();
                    check(row.hero+" current selection separate from stored vector "+row.selected,Same(game.Profile.selectedWeaponSlots,new[]{4,3,2,1,0}));
                    {int m0=game.Profile.mission;scene.Complete();for(int k=0;k<600&&game.Profile.mission==m0;k++)game.Step(1f/60f);} // MissionCompleteData runs when the banner arrives (bounded 10 s)
                    check(row.hero+" completion captures native vector "+row.selected,Same(game.Profile.selectedWeaponSlots,row.after_completion));
                    var saved=Profile.Load(dir);check(row.hero+" JSON adapter reload preserves vector "+row.selected,Same(saved.selectedWeaponSlots,row.after_completion));
                    UnityEngine.Object.DestroyImmediate(game.gameObject);game=Dc2Game.Create(dir);game.cam=cam;game.manualStep=true;game.SkipMissionIntro=true;game.LaunchMission=new NativeMapState.Mission{type=3,count=1000,background=0};game.SwitchScene("gameScene");game.Step(0f);scene=(GameScene)game.Current;scene.spawningEnabled=false;
                    check(row.hero+" native entry consumes stored slot "+row.selected,scene.weaponSlot==row.entry_value&&scene.Rig.Source==NativeHeroLocomotionRecipes.Get(row.hero,row.selected,"stand").source);
                    scene.SwitchWeapon();int live=scene.weaponSlot;check(row.hero+" later live switch leaves captured vector "+row.selected,Same(game.Profile.selectedWeaponSlots,row.after_completion));
                    game.SwitchScene("gameScene");game.Step(0f);scene=(GameScene)game.Current;scene.spawningEnabled=false;
                    check(row.hero+" transition without Complete restores captured selection "+row.selected,live!=row.selected&&scene.weaponSlot==row.entry_value);
                    game.Profile.ResetFileKeepAudio();check(row.hero+" native reset vector "+row.selected,Same(game.Profile.selectedWeaponSlots,oracle.reset_vector));
                    UnityEngine.Object.DestroyImmediate(game.gameObject);game=null;
                }
                string legacy=Path.Combine(temp,"legacy");Directory.CreateDirectory(legacy);File.WriteAllText(Path.Combine(legacy,"dinocap2hd.sav.json"),"{\"level\":[7,-1,-1,-1,-1],\"exp\":[0,0,0,0,0]}");var old=Profile.Load(legacy);check("legacy JSON adapter adds native XML zero vector without discarding profile",old.level[0]==7&&Same(old.selectedWeaponSlots,oracle.reset_vector));
                File.WriteAllText(Path.Combine(legacy,"dinocap2hd.sav.json"),"{\"level\":[7,-1,-1,-1,-1],\"exp\":[0,0,0,0,0],\"selectedWeaponSlots\":[4,3,2,1]}");old=Profile.Load(legacy);check("short JSON vector preserves first four and defaults fifth like native short tail",old.level[0]==7&&Same(old.selectedWeaponSlots,new[]{4,3,2,1,0}));
            }
            catch(Exception e){fail++;log.AppendLine("EXCEPTION "+e);}
            if(game!=null)UnityEngine.Object.DestroyImmediate(game.gameObject);if(cam!=null)UnityEngine.Object.DestroyImmediate(cam.gameObject);if(Directory.Exists(temp))Directory.Delete(temp,true);log.AppendLine("SUMMARY passes="+pass+" fails="+fail);File.WriteAllText(Path.Combine(output,"verify.txt"),log.ToString());Debug.Log(log.ToString());PilotExit.Exit(fail==0?0:1);
        }
    }
}
