using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace Dc2Pilot.EditorTools
{
    public static class PilotHurtVerify
    {
        [Serializable] public class HitCase {public int state;public bool wudi,initial_left;public float attacker_dx,atk,hp;public int result_state,result_wudi,left,turn;public float[] velocity;public Effect[] effects;}
        [Serializable] public class Effect {public string call;}
        [Serializable] public class Hits {public HitCase[] cases;}
        [Serializable] public class Callback {public string stage;public int input,state;public bool initial_left,left,turn,has_velocity;public float velocity;public string[] requests;}
        [Serializable] public class Callbacks {public Callback[] cases;}
        [Serializable] public class Tick {public int tick,displayed;public float x,velocity;public bool hurting;}
        [Serializable] public class Travel {public bool marked,attacker_left;public Tick[] ticks;}
        [Serializable] public class Travels {public Travel[] cases;}
        [Serializable] public class ClockTick {public int tick;public float dt,time;public bool due;}
        [Serializable] public class Clock {public float initial,deadline;public ClockTick[] ticks;}
        [Serializable] public class Clocks {public Clock[] cases;}
        static T Read<T>(string name) {return JsonUtility.FromJson<T>(File.ReadAllText(Path.Combine(Application.dataPath,"../../Dev/Reports/DC2-PILOT-001/"+name)));}
        public static void Run()
        {
            string output=Path.Combine(Application.dataPath,"../Reports/DC2-PILOT-001/hurt-r50");var args=Environment.GetCommandLineArgs();
            for(int i=0;i<args.Length-1;i++)if(args[i]=="-reportDir")output=args[i+1];Directory.CreateDirectory(output);
            string dir=Path.Combine(Path.GetTempPath(),"dc2_hurt_"+Guid.NewGuid().ToString("N"));int passed=0,failed=0;var log=new StringBuilder();
            Action<string,bool> check=(n,v)=>{if(v)passed++;else failed++;log.AppendLine((v?"PASS ":"FAIL ")+n);};
            Dc2Game game=null;Camera camera=null;RenderTexture rt=null;Texture2D image=null;
            try
            {
                foreach(var row in Read<Hits>("player_hurt_oracle.r44.json").cases)
                {
                    var hit=NativeHeroHurt.Hit(row.state,row.wudi,100f,row.atk,row.attacker_dx,row.initial_left);
                    bool accepted=row.effects.Length>0,dead=Array.Exists(row.effects,e=>e.call=="Die");
                    check("ARM hit state="+row.state+" wudi="+row.wudi+" dx="+row.attacker_dx+" atk="+row.atk,hit.accepted==accepted&&hit.dead==dead&&hit.hp==row.hp&&(!accepted||dead||hit.velocity==row.velocity[0]&&hit.facingLeft==(row.left!=0)&&hit.turning==(row.turn!=0)));
                }
                foreach(var row in Read<Callbacks>("hurt_callbacks_oracle.r45.json").cases)if(row.stage=="end")
                {
                    var c=NativeHeroControl.UpdateControl(5,row.initial_left,(row.input&1)!=0,(row.input&2)!=0,300f);
                    check("ARM direct end control input="+row.input+" face="+row.initial_left,c.state==row.state&&c.facingLeft==row.left&&c.turning==row.turn&&c.hasVelocity==row.has_velocity&&c.velocity==row.velocity);
                }
                foreach(var row in Read<Clocks>("hurt_timer_oracle.r50b.json").cases)
                {
                    float now=row.initial,deadline=NativeHeroHurt.Deadline(now);bool exact=deadline==row.deadline;
                    foreach(var tick in row.ticks){now=NativeHeroHurt.AdvanceClock(now,tick.dt);exact&=now==tick.time&&NativeHeroHurt.TimerDue(now,deadline,tick.dt)==tick.due;}
                    check("ARM accumulated clock/deadline dt0 initial="+row.initial,exact);
                }
                camera=Bootstrap.MakeCamera();game=Dc2Game.Create(dir);game.cam=camera;game.manualStep=true;game.SkipMissionIntro=true;game.Profile.fx=1;
                rt=new RenderTexture(1024,768,24);image=new Texture2D(1024,768,TextureFormat.RGB24,false);
                Action<string> shot=name=>{camera.targetTexture=rt;Bootstrap.RenderAll(camera);RenderTexture.active=rt;image.ReadPixels(new Rect(0,0,1024,768),0,0);image.Apply();File.WriteAllBytes(Path.Combine(output,name),image.EncodeToPNG());camera.targetTexture=null;RenderTexture.active=null;};
                Func<int,GameScene> fresh=hero=>
                {
                    game.LaunchMission=new NativeMapState.Mission{type=3,count=99,background=7};game.LaunchMission.enemies.Add(1);game.Profile.playerType=hero;game.SwitchScene("gameScene");game.Step(0f);
                    var s=(GameScene)game.Current;s.spawningEnabled=false;s.ignorePlayer=true;s.heroX=1000;s.playerHp=1000;game.Step(0f);return s;
                };
                var recipes=Read<NativeHeroHurt.Table>("hurt_recipes.r49.json");
                foreach(var recipe in recipes.recipes)
                {
                    var pose=NativeHeroHurt.Load(recipe.hero,recipe.slot);var original=NativeCaAnimation.Load("DC2/"+recipe.source,true);bool exact=pose.frames.Length==14;
                    for(int f=0;f<pose.frames.Length;f++)
                    {
                        exact&=pose.frames[f].key==Array.Exists(recipe.keys,k=>k==f);
                        for(int i=0;i<pose.frames[f].parts.Length;i++)
                        {
                            var part=pose.frames[f].parts[i];var before=original.frames[f].parts[i];string expected=before.reference;
                            foreach(var replace in recipe.images)if(replace.part==part.name)expected=replace.reference;
                            exact&=part.reference==expected&&part.x==before.x&&part.y==before.y&&part.w==before.w&&part.h==before.h&&part.radians==before.radians;
                        }
                    }
                    check("native source/skin/key recipe hero="+recipe.hero+" slot="+recipe.slot,exact);
                }
                foreach(var travel in Read<Travels>("hurt_travel_oracle.r50.json").cases)
                {
                    var scene=fresh(travel.marked?0:1);scene.weaponSlot=travel.marked?0:4;int sounds=game.Audio.SoundsPlayed;
                    scene.BeAttack(new DinoActor {x=1000+(travel.attacker_left?-1:1),atk=1.9f});
                    check("hurt starts without dropping emitted sprites",scene.HeroHurting&&scene.playerHp==999&&scene.heroControlState==5&&Array.Exists(scene.Rig.GetComponentsInChildren<SpriteRenderer>(),r=>r.enabled));
                    check("native nonlethal sound once",game.Audio.SoundsPlayed==sounds+1);
                    foreach(var t in travel.ticks)
                    {
                        game.Step(1f/60f);check("travel marked="+travel.marked+" attackerLeft="+travel.attacker_left+" tick="+t.tick,scene.heroX==t.x&&scene.heroVelocityX==t.velocity&&scene.HeroHurting==t.hurting&&scene.Rig.Player.DisplayedFrame==t.displayed);
                        if(t.tick==1||t.tick==15||t.tick==28)shot("hurt_"+(travel.marked?"marked":"electric")+"_"+travel.attacker_left+"_tick"+t.tick+".png");
                    }
                    game.Step(1f/60f);check("Stand follows final hurt emission",scene.Rig.Player.DisplayedFrame==0&&!scene.HeroHurting);
                }
                var s0=fresh(0);s0.BeAttack(new DinoActor{x=999,atk=1});s0.rightDown=true;s0.fireDown=true;s0.hitHeld=true;
                for(int tick=0;tick<28;tick++)game.Step(1f/60f);
                check("hurt end resumes held control then melee priority",s0.LastHurtResumeRequest=="OnHitDown"&&s0.meleeing&&s0.heroControlState==3&&s0.heroVelocityX==0&&s0.Rig.Player.DisplayedFrame==13);
                s0=fresh(0);s0.BeAttack(new DinoActor{x=999,atk=1});s0.rightDown=true;s0.fireDown=true;
                for(int tick=0;tick<28;tick++)game.Step(1f/60f);
                check("hurt end fire dispatch preserves final emission",s0.LastHurtResumeRequest=="OnFireDown"&&s0.firing&&s0.heroControlState==1&&s0.heroVelocityX==300&&s0.Rig.Player.DisplayedFrame==13);
                s0=fresh(0);s0.BeAttack(new DinoActor{x=999,atk=1});for(int tick=0;tick<5;tick++)game.Step(1f/60f);int frame=s0.Rig.Player.CurrentFrame;float accumulator=s0.Rig.Player.Accumulator;
                game.Profile.weaponOwned[1]=0;s0.SwitchWeapon();check("equip during hurt rebinds data without cursor reset",s0.HeroHurting&&s0.Rig.Player.CurrentFrame==frame&&s0.Rig.Player.Accumulator==accumulator&&s0.Rig.Player.animation.frames[0].parts.Length>0);
                s0.OnRollDown();check("native Roll interrupts hurt state5",s0.rolling&&!s0.HeroHurting&&s0.heroControlState==7);
                s0=fresh(0);s0.BeAttack(new DinoActor{x=999,atk=1});float hp=s0.playerHp;s0.BeAttack(new DinoActor{x=999,atk=100});check("Wudi rejects repeat hit",s0.playerHp==hp);
                float clock=game.NativeTimerTime;s0.paused=true;game.Step(2f);check("pause freezes native clock and hurt physics",game.NativeTimerTime==clock&&s0.heroX==1000&&s0.HeroHurting&&s0.invincible);
                s0.paused=false;game.Step(1.5f);check("absolute Wudi timeout clears before object phase",!s0.invincible);
                s0=fresh(0);game.Profile.fx=0;int mutedSounds=game.Audio.SoundsPlayed;s0.BeAttack(new DinoActor{x=999,atk=1});check("FX gate suppresses native hurt audio",game.Audio.SoundsPlayed==mutedSounds);game.Profile.fx=1;
                s0=fresh(0);s0.BeAttack(new DinoActor{x=999,atk=2000});check("lethal branch bypasses nonlethal Wudi/hurt",s0.heroDead&&!s0.HeroHurting&&!s0.invincible);
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
