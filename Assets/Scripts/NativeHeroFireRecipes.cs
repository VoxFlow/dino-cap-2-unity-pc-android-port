using System;
using UnityEngine;
namespace Dc2Pilot
{
    // Native GenComAnim/ChangeWeapon operation ordering r59; every represented
    // final part independently recovered through ARM loader/transforms r66b.
    // Gameplay firing activation and its complete lifecycle remain OPEN.
    public static class NativeHeroFireRecipes
    {
        [Serializable] public class Operation {public string action,part,reference;public float scale;public int frame;}
        [Serializable] public class Recipe {public int hero,slot,weapon_type,weapon_subtype;public string source,pose;public Operation[] events;}
        [Serializable] class Table {public Recipe[] recipes;}
        static Table table;
        public static Recipe Get(int hero,int slot)
        {
            if(table==null)table=JsonUtility.FromJson<Table>(Resources.Load<TextAsset>("DC2/fire_recipes.json").text);
            foreach(var r in table.recipes)if(r.hero==hero&&r.slot==slot)return r;
            throw new ArgumentOutOfRangeException("Native fire recipe hero/slot");
        }
        public static NativeCaAnimation Load(int hero,int slot)
        {
            return Build(Get(hero,slot));
        }
        public static NativeCaAnimation Build(Recipe r)
        {
            var result=NativeCaAnimation.Load("DC2/"+r.source,true).Clone();
            foreach(var op in r.events)
            {
                switch(op.action)
                {
                    case "set_to_adv":result.SetToAdvanced();break;
                    case "image":result.ChangeImage(op.part,op.reference,op.scale);break;
                    case "key":result.MarkKeyFrame(op.frame);break;
                    case "sprite_animation":result.SetSpriteAnimation(op.part);break;
                    default:throw new InvalidOperationException("Unrecovered native fire transform "+op.action);
                }
            }
            return result;
        }
    }
}
