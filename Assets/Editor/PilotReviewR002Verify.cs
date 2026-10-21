using System;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;
namespace Dc2Pilot.EditorTools {
 /// <summary>R002 review regressions (DC2-REVIEW-002 B1-B5): fixtures adopted from the Codex probe codex/snapshot/.../CodexR002Probe.cs (cases unchanged) plus
 /// builder additions (B1 BehitedAnim row in a non-zero state, B1 table-level collapse).</summary>
 public static class PilotReviewR002Verify {
  static BindingFlags flags=BindingFlags.Instance|BindingFlags.NonPublic|BindingFlags.Public;
  public static void Run() {
   string output="";var args=Environment.GetCommandLineArgs();for(int i=0;i<args.Length-1;i++)if(args[i]=="-reportDir")output=args[i+1];
   int pass=0,fail=0;var log=new StringBuilder();Dc2Game g=null;Camera cam=null;
   Action<string,bool> check=(n,v)=>{log.AppendLine((v?"PASS ":"FAIL ")+n);if(v)pass++;else fail++;};
   string dir=Path.Combine(Path.GetTempPath(),"dc2_r002_"+Guid.NewGuid().ToString("N"));
   try {
    cam=Bootstrap.MakeCamera();g=Dc2Game.Create(dir);g.manualStep=true;g.SkipMissionIntro=true;g.cam=cam;
    Func<int,GameScene> scene=type=>{g.LaunchMission=new NativeMapState.Mission {type=type,count=999,background=0};g.SwitchScene("gameScene");g.Step(0f);var s=(GameScene)g.Current;s.spawningEnabled=false;s.ignorePlayer=true;return s;};
    Action<GameScene,string,object[]> call=(s,name,a)=>typeof(GameScene).GetMethod(name,flags).Invoke(s,a);
    var s0=scene(3);s0.heroX=0f;s0.camLeft=-100f;
    var gun=new WeaponDef {type=0,index=0,atkDis=480f,atkMin=1f,atkMax=1f,baseAtk=1,crit=0f};
    foreach(int state in new[]{4,5,6}) {
     var d=s0.CreateEnemy(1);d.x=300f;d.hp=1000000f;d.state=state;d.velocityX=37f;d.legacyHit=false;var anim=d.rig.animation;s0.rng=new System.Random(123);
     call(s0,"BeAttacked",new object[]{d,gun,300f});
     check("B1 state "+state+" gun hit retains state/animation/velocity (actual state="+d.state+", legacy="+d.legacyHit+", velocity="+d.velocityX+")",d.state==state&&!d.legacyHit&&d.velocityX==37f&&d.rig.animation==anim);
     d.dead=true;
    }
    // builder additions: table-level collapse (B1)
    var nz=NativeDinoReaction.Evaluate(0,0,1,5,false); var one=NativeDinoReaction.Evaluate(0,0,1,1,false);
    check("B1 table: gun state 5 uses the non-zero row and keeps state 5 (found="+nz.found+", after="+nz.stateAfter+", blood="+nz.blood+")",nz.found&&nz.stateAfter==5&&nz.blood==one.blood&&!nz.advanced&&!nz.basePose);
    var hb=NativeDinoReaction.Evaluate(4,0,1,6,false);
    check("B1 table: BehitedAnim row (type 4 skin 1) in state 6 writes 2 like states 1/7/8 (after="+hb.stateAfter+", base="+hb.basePose+")",hb.found&&hb.basePose&&hb.stateAfter==2);
    check("B1 table: state 0 has no collapse (its own advanced row)",NativeDinoReaction.Evaluate(0,0,1,0,false).advanced);
    // Actual HeroAttack target selection; expected damage uses the independently recovered origin operand.
    g.Profile.playerType=1;g.Profile.level[1]=0;s0=scene(3);s0.heroX=0f;s0.camLeft=-100f;
    int shotgun=-1;var slots=Dc2GameData.PlayerWeapons(s0.heroType);for(int i=0;i<slots.Count-1;i++)if(slots[i].type==1){shotgun=i;break;}
    if(shotgun<0)throw new Exception("No shipped shotgun slot");s0.weaponSlot=shotgun;
    var target=s0.CreateEnemy(1);target.x=400f;target.hp=4096f;target.state=1;target.faceRight=false;target.rig.Play(target.aMove,0,0,0.05f);
    float origin=Mathf.Abs(s0.heroX-target.x),head=s0.HeadXForTest(target)-s0.heroX;
    var w=s0.CurrentWeapon();int level=g.Profile.weaponLevels[s0.CurrentAmmoIndex];var rng=new System.Random(321);
    float random=(float)rng.NextDouble();int roll=(int)((float)rng.NextDouble()*10000f);bool crit=NativeWeaponDamage.IsCritical(roll,w.crit);
    float expected=NativeWeaponDamage.ApplyCritical(NativeWeaponDamage.Base(w,level,g.Profile.jigsaw2Count,origin,random),crit);
    float want=N.Add32(target.hp,-expected);s0.rng=new System.Random(321);call(s0,"HeroAttack",new object[0]);
    check("B2 shotgun uses origin for damage; head="+head+", origin="+origin+", hp="+target.hp+", expected="+want,Mathf.Abs(target.hp-want)<0.0001f);
    foreach(int type in new[]{1,5,3}) {
     var s=scene(type);g.Profile.level[s.heroType]=0;g.Profile.exp[s.heroType]=0;int threshold=s.ExpThreshold;
     call(s,"AddExp",new object[]{threshold});bool main=type==1||type==5;
     check("B3 mission "+type+" at threshold="+threshold+" level="+g.Profile.level[s.heroType]+" expected="+(main?1:0),g.Profile.level[s.heroType]==(main?1:0));
    }
    var cashScene=scene(3);for(int i=0;i<g.Profile.jigsawGet.Length;i++)g.Profile.jigsawGet[i]=1;
    g.Profile.jigsawSeed=int.MaxValue;g.Profile.jigsaw5Count=2;g.Profile.cash=9999990;
    typeof(GameScene).GetField("moneyEarned",flags).SetValue(cashScene,1000);
    call(cashScene,"MissionCompleteData",new object[0]);var reloaded=Profile.Load(dir);
    check("B4 completion saves uncapped cash; actual="+g.Profile.cash+", reload="+reloaded.cash+", expected=10000010",g.Profile.cash==10000010&&reloaded.cash==10000010);
    // R002-N2: HUD insertion order inside each layer follows the native OnLoad streams
    {
     var json=File.ReadAllText(Path.Combine(Application.dataPath,"../../Dev/Evidence/mainscene_hud_emulated_r118.json"));
     foreach(int type in new[]{1,5,3})
     {
      var s=scene(type);
      var order=new System.Collections.Generic.Dictionary<int,int>();
      foreach(var c in s.Controls) if(c!=null) order[c.id]=c.UiOrderForTest;
      foreach(var l in s.Labels) if(l!=null) order[l.id]=l.UiOrderForTest;
      // the set for this mission type: ids and layers in native creation order
      int at=json.IndexOf("\"type\": "+type+","); int end=json.IndexOf("\"type\":",at+5); if(end<0)end=json.Length;
      var ids=new System.Collections.Generic.List<int>(); var layers=new System.Collections.Generic.List<int>();
      foreach(System.Text.RegularExpressions.Match m in System.Text.RegularExpressions.Regex.Matches(json.Substring(at,end-at),"\"id\":[^0-9]*([0-9]+),[^0-9]*\"layer\":[^0-9]*([0-9]+)")){ids.Add(int.Parse(m.Groups[1].Value));layers.Add(int.Parse(m.Groups[2].Value));}
      int bad=0,pairs=0; string first="";
      for(int i=0;i<ids.Count;i++)for(int j=i+1;j<ids.Count;j++)
      {
       if(layers[i]!=layers[j]||ids[i]==11||ids[j]==11||!order.ContainsKey(ids[i])||!order.ContainsKey(ids[j]))continue;
       pairs++; if(order[ids[i]]>order[ids[j]]){bad++; if(first=="")first=ids[i]+" after "+ids[j];}
      }
      check("N2 mission "+type+" HUD same-layer creation order = native ("+pairs+" pairs, "+bad+" out of order"+(first!=""?", first: "+first:"")+")",ids.Count>0&&pairs>0&&bad==0);
     }
    }
    var wideScene=scene(3);wideScene.camLeft=0f;wideScene.heroX=432f;wideScene.facingLeft=false;
    // Set only the derived adapter state in this disposable harness; avoid writing PlayerPrefs.
    typeof(WideView).GetProperty("ExtraHalf",BindingFlags.Static|BindingFlags.Public).GetSetMethod(true).Invoke(null,new object[]{170f});
    float distance=1000f-wideScene.heroX;float adapted=(float)typeof(GameScene).GetMethod("WideFalloffDistance",flags).Invoke(wideScene,new object[]{distance});
    check("B5 in-native-view distance stays unchanged in wide mode; actual="+adapted+", expected="+distance,adapted==distance);
    typeof(WideView).GetProperty("ExtraHalf",BindingFlags.Static|BindingFlags.Public).GetSetMethod(true).Invoke(null,new object[]{0f});
    float native=(float)typeof(GameScene).GetMethod("WideFalloffDistance",flags).Invoke(wideScene,new object[]{distance});
    check("B5 E=0 control preserves distance",native==distance);
   }catch(Exception ex){fail++;log.AppendLine("EXCEPTION "+ex);}
   finally{if(g!=null)UnityEngine.Object.DestroyImmediate(g.gameObject);if(cam!=null)UnityEngine.Object.DestroyImmediate(cam.gameObject);Directory.CreateDirectory(output);log.AppendLine("SUMMARY passes="+pass+" fails="+fail);File.WriteAllText(Path.Combine(output,"verify.txt"),log.ToString());Debug.Log(log.ToString());PilotExit.Exit(fail==0?0:1);}
  }
 }
}
