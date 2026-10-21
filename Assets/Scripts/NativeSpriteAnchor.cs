using System;
using UnityEngine;
namespace Dc2Pilot
{
    /// <summary>CAObjCallBack::getSpritePos a4d0. Dormant arithmetic vessel:
    /// emitted child lifecycle and native libm identity must close before combat activation.</summary>
    public static class NativeSpriteAnchor
    {
        public sealed class Child
        {
            public string name;
            public bool enabled;
            public Vector2 position,pivot;
            public float sin,cos;
        }
        public static Vector2 GetPoint(Child[] children,string name,bool flipX,Vector2 input)
        {
            foreach(var child in children)
            {
                if(!child.enabled||child.name!=name)continue;
                if(flipX)input.x=N.F(BitConverter.SingleToInt32Bits(input.x)^int.MinValue);
                float cx=N.Add32(child.position.x,child.pivot.x),cy=N.Add32(child.position.y,child.pivot.y);
                float px=N.Add32(input.x,child.position.x),py=N.Add32(input.y,child.position.y);
                float dx=N.Add32(px,-cx),dy=N.Add32(py,-cy);
                float rx=N.Add32(N.Mul32(child.cos,dx),-N.Mul32(child.sin,dy));
                float ry=N.Add32(N.Mul32(child.sin,dx),N.Mul32(child.cos,dy));
                return new Vector2(N.Add32(cx,rx),N.Add32(cy,ry));
            }
            return input;
        }
    }
}
