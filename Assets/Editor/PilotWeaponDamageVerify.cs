using System;
using System.IO;
using System.Text;
using System.Reflection;
using UnityEditor;
using UnityEngine;
namespace Dc2Pilot.EditorTools
{
    public static class PilotWeaponDamageVerify
    {
        [Serializable] class Oracle{public Case[] cases;}
        [Serializable] class Case{public int type,level,jigsaw2,random_word,critical_word,blood_requests;public float distance,atk_min,atk_distance;public int atk_base;public uint raw_damage_bits,hp_bits;}
        sealed class Units: System.Random
        {
            readonly double[] values;public int Calls;
            public Units(params double[] supplied){values=supplied;}
            public override double NextDouble(){return values[Calls++%values.Length];}
        }
        static uint B(float v){return unchecked((uint)BitConverter.SingleToInt32Bits(v));}
        public static void Run()
        {
            string output=null;var args=Environment.GetCommandLineArgs();for(int i=0;i<args.Length-1;i++)if(args[i]=="-reportDir")output=args[i+1];Directory.CreateDirectory(output);var log=new StringBuilder();int pass=0,fail=0;Dc2Game game=null;Camera cam=null;string temp=Path.Combine(Path.GetTempPath(),"dc2_weapon_damage_"+Guid.NewGuid().ToString("N"));
            Action<string,bool> check=(n,ok)=>{log.AppendLine((ok?"PASS ":"FAIL ")+n);if(ok)pass++;else fail++;};
            try
            {
                var oracle=JsonUtility.FromJson<Oracle>(File.ReadAllText(Path.Combine(Application.dataPath,"../../Dev/Reports/DC2-PILOT-001/weapon_damage_oracle.r90.json")));
                cam=Bootstrap.MakeCamera();game=Dc2Game.Create(temp);game.cam=cam;game.manualStep=true;game.SkipMissionIntro=true;game.LaunchMission=new NativeMapState.Mission{type=3,count=1000,background=0};game.SwitchScene("gameScene");game.Step(0f);var scene=(GameScene)game.Current;scene.spawningEnabled=false;
                foreach(var row in oracle.cases)
                {
                    var w=new WeaponDef{type=row.type,atkMin=row.atk_min,baseAtk=row.atk_base,atkDis=row.atk_distance,crit=7};float unit=NativeWeaponDamage.RandomUnit(row.random_word);float raw=NativeWeaponDamage.Base(w,row.level,row.jigsaw2,row.distance,unit);string tag=row.type+"/"+row.level+"/"+row.jigsaw2+"/"+row.random_word+"/"+row.critical_word+"/"+row.distance;
                    check(tag+" native raw damage",B(raw)==row.raw_damage_bits);
                    bool critical=NativeWeaponDamage.IsCritical(row.critical_word,w.crit);float hp=N.Add32(10000f,-NativeWeaponDamage.ApplyCritical(raw,critical));check(tag+" native critical/HP write",critical==(row.blood_requests==3)&&B(hp)==row.hp_bits);
                    game.Profile.jigsaw2Count=row.jigsaw2;var rng=new Units(unit);scene.rng=rng;float live=scene.WeaponDamage(w,row.level,row.distance,row.blood_requests==3);check(tag+" active numeric method supplied unit",B(N.Add32(10000f,-live))==row.hp_bits&&rng.Calls==1);
                }
                var one=Array.Find(oracle.cases,r=>r.type==0&&r.level==1&&r.jigsaw2==0&&r.random_word==0x40000000&&r.critical_word==0&&r.distance==0);var weapon=new WeaponDef{type=0,atkMin=one.atk_min,baseAtk=one.atk_base,atkDis=one.atk_distance,crit=7};game.Profile.jigsaw2Count=0;game.Profile.weaponLevels[scene.CurrentAmmoIndex]=1;var target=scene.CreateEnemy(1);target.hp=10000f;target.x=scene.camLeft+500f;/* r119f: BeAttacked 0x1498a viewport gate - the spawn point is off screen */var sequence=new Units(.5,0);scene.rng=sequence;
                typeof(GameScene).GetMethod("BeAttacked",BindingFlags.NonPublic|BindingFlags.Instance).Invoke(scene,new object[]{target,weapon,0f});
                // r118z8 (DC2-BUGS-001 B5): the non-lethal reaction tail then calls dinosaurClass::Blood(0) 0x15650, which draws rand() twice (0x12aa8 / 0x12adc) for the head point.
                // The old expectation (2 + 9) counted the port before it drew Blood(0) at all (B5 port defect), so it encoded the defect.
                check("active attack consumes damage unit before critical draw (a critical hit then draws 3 BlastBlood x 3 rand() values: BeAttacked 0x14f4a), then Blood(0) draws 2 (0x12a46)",sequence.Calls==2+(one.blood_requests==3?9:0)+2&&B(target.hp)==one.hp_bits);
            }
            catch(Exception e){fail++;log.AppendLine("EXCEPTION "+e);}
            if(game!=null)UnityEngine.Object.DestroyImmediate(game.gameObject);if(cam!=null)UnityEngine.Object.DestroyImmediate(cam.gameObject);if(Directory.Exists(temp))Directory.Delete(temp,true);log.AppendLine("SUMMARY passes="+pass+" fails="+fail);File.WriteAllText(Path.Combine(output,"verify.txt"),log.ToString());Debug.Log(log.ToString());PilotExit.Exit(fail==0?0:1);
        }
    }
}
