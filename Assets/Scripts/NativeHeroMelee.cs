using System;
using UnityEngine;

namespace Dc2Pilot
{
    /// <summary>Configured male hero melee poses. Native loader + GenComAnim6/ChangeWeapon6,
    /// melee_recipes.r77.json; XML schedule and numeric evidence r75/r77. Target/damage separate.</summary>
    public static class NativeHeroMelee
    {
        [Serializable] public sealed class Recipe
        {
            public int hero;
            public string source;
            public Operation[] events;
        }
        [Serializable] public sealed class Operation
        {
            public string action, part, reference;
            public int bits;
            public int frame;
            public float scale;
        }
        [Serializable] sealed class Recipes { public Recipe[] recipes; }
        static Recipes data;
        public static Recipe Get(int hero)
        {
            if((uint)hero>3)throw new ArgumentOutOfRangeException(nameof(hero));
            if(data==null)data=JsonUtility.FromJson<Recipes>(Resources.Load<TextAsset>("DC2/melee_recipes.json").text);
            return data.recipes[hero];
        }
        public static NativeCaAnimation Load(int hero)
        {
            var recipe=Get(hero);NativeCaAnimation a=null;
            foreach(var op in recipe.events)
            {
                if(op.action=="expand")a=NativeXmlCaAnimation.Load("DC2/"+recipe.source,N.F(op.bits));
                else if(op.action=="image")a.ChangeImage(op.part,op.reference,op.scale);
                else if(op.action=="key")a.MarkKeyFrame(op.frame);
                else throw new InvalidOperationException("Unrecovered melee operation "+op.action);
            }
            return a;
        }
    }
}
