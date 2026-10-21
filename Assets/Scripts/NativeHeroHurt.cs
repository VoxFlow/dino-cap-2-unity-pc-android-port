using System;
using UnityEngine;
namespace Dc2Pilot
{
    // Bounded playerClass contracts bd166/bf406/bd978 and recovered native recipes r49.
    // Girl/boss/egg/death and complete firing composition retain their separate OPEN rows.
    public static class NativeHeroHurt
    {
        [Serializable] public class Image { public string part,reference; public float scale; }
        [Serializable] public class Recipe { public int hero,slot; public string source; public Image[] images; public int[] keys; public string[] sprite_animations; }
        [Serializable] public class Table { public Recipe[] recipes; }
        static Table table;
        public struct Result { public bool accepted,dead,facingLeft,turning; public float hp,velocity; }
        public static Result Hit(int state,bool invincible,float hp,float attack,float attackerDx,bool facingLeft)
        {
            var r=new Result {hp=hp,facingLeft=facingLeft};
            if(invincible||unchecked((uint)(state-4))<=3u)return r;
            r.accepted=true;r.hp=N.Add32(hp,-(float)(int)attack); // native fixsfsivfp truncates toward zero.
            if(r.hp<=0f){r.hp=0f;r.dead=true;return r;}
            r.facingLeft=attackerDx<0f;r.turning=r.facingLeft!=facingLeft;
            r.velocity=r.facingLeft?100f:-100f;return r;
        }
        public static Recipe Get(int hero,int slot)
        {
            if(table==null)table=JsonUtility.FromJson<Table>(Resources.Load<TextAsset>("DC2/hurt_recipes.json").text);
            foreach(var r in table.recipes)if(r.hero==hero&&r.slot==slot)return r;
            throw new ArgumentOutOfRangeException("Native hurt recipe hero/slot");
        }
        public static NativeCaAnimation Load(int hero,int slot)
        {
            var r=Get(hero,slot);var a=NativeCaAnimation.Load("DC2/"+r.source,true).Clone();
            foreach(var image in r.images)a.ChangeImage(image.part,image.reference,image.scale);
            foreach(int key in r.keys)a.MarkKeyFrame(key);
            foreach(string part in r.sprite_animations)a.SetSpriteAnimation(part);
            return a;
        }
        public static float Deadline(float time) { return N.Add32(time,1.5f); }
        public static float AdvanceClock(float time,float dt) { return dt>0f?N.Add32(time,dt):time; }
        public static bool TimerDue(float time,float deadline,float dt) { return dt>0f&&deadline<=time; }
    }
}
