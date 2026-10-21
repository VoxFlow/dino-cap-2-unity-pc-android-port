using System;
using UnityEngine;
namespace Dc2Pilot
{
    // Post-ChangeWeapon pointers668/672 and operations recovered r68; native
    // final numeric fields r69. Life InitType/Stand supplied caller r81; active
    // bounded male life2/3/7/8. Native selected-index lifecycle (r118x): ResetFile/ReadFile default, read in scene OnLoad, written only by missionComplete (both scenes), saved by SaveFile.
    public static class NativeHeroLocomotionRecipes
    {
        [Serializable] class Table{public NativeHeroFireRecipes.Recipe[] recipes;}
        static Table table;
        public static NativeHeroFireRecipes.Recipe Get(int hero,int slot,string pose)
        {
            if(table==null)table=JsonUtility.FromJson<Table>(Resources.Load<TextAsset>("DC2/locomotion_recipes.json").text);
            foreach(var r in table.recipes)if(r.hero==hero&&r.slot==slot&&r.pose==pose)return r;
            throw new ArgumentOutOfRangeException("Native locomotion recipe hero/slot/pose");
        }
        public static NativeCaAnimation Load(int hero,int slot,string pose)
        {return NativeHeroFireRecipes.Build(Get(hero,slot,pose));}
    }
}
