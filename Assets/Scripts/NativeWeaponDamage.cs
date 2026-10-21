using System;
namespace Dc2Pilot
{
    /// <summary>dinosaurClass::BeAttacked 0x14918 / bossN::BeAttacked 0x123b70 numeric stage (r90 ARM cases). Girl (g_player_type 4): the per-type basis
    /// passes through GetPlayer4Atk 0x193b8 before the jigsaw bonus (0x14a7e..0x14a8a), everything after it is shared.</summary>
    public static class NativeWeaponDamage
    {
        static readonly int[] coefficients={2,5,3,4,8,5,2};
        public static float RandomUnit(int word)
        {return N.Mul32(N.F32(word),N.F(0x30000000));}
        public static float Base(WeaponDef weapon,int level,int jigsaw2,float distance,float randomUnit,int girlHeroLevel=-1)
        {
            float damage=BaseBeforeFalloff(weapon,level,jigsaw2,randomUnit,girlHeroLevel);
            if(weapon.type==1)damage=N.Mul32(damage,FalloffFactor(distance,weapon.atkDis));
            return damage;
        }
        /// <summary>Type-1 factor (|distance|/ATK_DIS)*-2+3, shared by dinosaurClass::BeAttacked and bossN::BeAttacked 0x123b70.</summary>
        public static float FalloffFactor(float distance,float atkDis)
        {
            float absolute=N.F(BitConverter.SingleToInt32Bits(distance)&0x7fffffff);
            return N.Add32(N.Mul32(N.Div32(absolute,atkDis),N.F(unchecked((int)0xc0000000))),N.F(0x40400000));
        }
        /// <summary>Numeric stage before the type-1 falloff; bossN applies its resistance factor between the two.</summary>
        public static float BaseBeforeFalloff(WeaponDef weapon,int level,int jigsaw2,float randomUnit,int girlHeroLevel=-1)
        {
            int k=level<=0?2:coefficients[weapon.type];
            float basis=N.Add32(N.F32(unchecked(k*level-k)),weapon.atkMin);
            if(girlHeroLevel>=0)basis=Player4Atk(basis,girlHeroLevel);
            float bonus=N.Mul32(N.Mul32(N.F32(jigsaw2),basis),N.F(1008981770));
            float lower=N.Add32(bonus,basis),upper=N.Add32(N.F32(weapon.baseAtk),lower);
            return N.Add32(lower,N.Mul32(randomUnit,N.Add32(upper,-lower)));
        }
        /// <summary>DinoCap::GetPlayer4Atk 0x193b8, level = the girl's stored level (dword_240494[4]): 0..3 -> x * 0.5f (float multiply);
        /// 4..8 / 9..13 / 14..18 / 19..23 -> (float)((double)x * 0.6 / 0.7 / 0.8 / 0.9); 24..28 -> x; above 28 -> (float)((double)x * 1.2).
        /// Doubles 0x3FE3333333333333, 0x3FE6666666666666, 0x3FE999999999999A, 0x3FECCCCCCCCCCCCD, 0x3FF3333333333333 (struct-decoded).</summary>
        public static float Player4Atk(float x,int level)
        {
            uint u=unchecked((uint)level);
            if(u<=3u)return N.Mul32(x,0.5f);
            double f;
            if(unchecked((uint)(level-4))<=4u)f=BitConverter.Int64BitsToDouble(0x3FE3333333333333);
            else if(unchecked((uint)(level-9))<=4u)f=BitConverter.Int64BitsToDouble(0x3FE6666666666666);
            else if(unchecked((uint)(level-14))<=4u)f=BitConverter.Int64BitsToDouble(0x3FE999999999999A);
            else if(unchecked((uint)(level-19))<=4u)f=BitConverter.Int64BitsToDouble(0x3FECCCCCCCCCCCCD);
            else if(unchecked((uint)(level-24))>4u&&level>28)f=BitConverter.Int64BitsToDouble(0x3FF3333333333333);
            else return x;
            return (float)((double)x*f);
        }
        public static bool IsCritical(int randomRangeWord,float threshold)
        {return N.F32(randomRangeWord%100)<threshold;}
        public static float ApplyCritical(float damage,bool critical)
        {return N.Add32(damage,critical?damage:0f);}
    }
}
