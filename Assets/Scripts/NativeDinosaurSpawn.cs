using UnityEngine;
namespace Dc2Pilot
{
    // lifeModeScene CreateEnemy d5a06/d5bdc/d5ee8/d61f4/d62da. Numeric vessel;
    // the host RNG seed/word source remains PA-012, distinct from these recovered terms.
    public static class NativeDinosaurSpawn
    {
        public static int Mode(int skin, int playerLevel, int integerRoll)
        {
            if (skin != 1 && skin != 2) return 1;
            float threshold = N.Add32(75f, N.Mul32(-.5f, (float)(playerLevel + 1)));
            return (float)integerRoll < threshold ? 3 : 2;
        }
        public static float UnitFromWord(int word) { return N.Mul32((float)word, 1f / 2147483648f); }
        public static bool Mode3Right(int playerLevel, float unit)
        { return N.Mul32(unit,100f) < N.Add32(55f,N.Mul32(-.5f,(float)playerLevel)); }
        public static bool Mode1Right(float heroPhysicsVelocity,System.Func<float> unit)
        {
            if(heroPhysicsVelocity<0f)return false;
            if(heroPhysicsVelocity>0f)return true;
            return N.Mul32(unit(),100f)<50f; // zero velocity alone consumes the native random draw.
        }
        public static Vector2 Jump(float cameraLeft, float cameraWidth, Vector2 slot0Bounds, bool playerFacingLeft)
        {
            return playerFacingLeft
                ? new Vector2(N.Add32(cameraLeft,-N.Mul32(2f,slot0Bounds.y)),800f)
                : new Vector2(N.Add32(N.Add32(cameraLeft,cameraWidth),N.Mul32(2f,-slot0Bounds.x)),-800f);
        }
    }
}
