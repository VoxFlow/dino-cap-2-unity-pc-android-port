namespace Dc2Pilot
{
    /// <summary>Native OnUpdate bb74a caller gate + UpdateControl b8a60; supplied state/input vessel.
    /// No Unity physics or timers. Inactive/dead/special-pose lifecycles belong to their callers.</summary>
    public static class NativeHeroControl
    {
        public struct Result
        {
            public int state;
            public bool facingLeft,turning,hasVelocity,called;
            public float velocity;
        }
        public static Result Decide(int state,bool facingLeft,bool left,bool right,bool active,float speed)
        {
            var r=new Result {state=state,facingLeft=facingLeft};
            if(state==4||state==5||state==7||!active)return r;
            return UpdateControl(state,facingLeft,left,right,speed);
        }
        // Direct callback callers (hurt end bd b02) bypass the OnUpdate state/active gate.
        public static Result UpdateControl(int state,bool facingLeft,bool left,bool right,float speed)
        {
            var r=new Result {state=state,facingLeft=facingLeft};
            r.called=true;
            if(left)
            {
                if(state==0||state==3||state==7||state==9)return r;
                if(!facingLeft){r.facingLeft=true;r.turning=true;}
                if(state!=10){r.state=0;r.hasVelocity=true;r.velocity=-speed;}
            }
            else if(right)
            {
                if(state==1||state==3||state==7||state==9)return r;
                if(facingLeft){r.facingLeft=false;r.turning=true;}
                if(state!=10){r.state=1;r.hasVelocity=true;r.velocity=speed;}
            }
            else if(state!=2&&state!=3&&state!=9&&state!=7&&state!=10)
            {
                r.state=2;r.hasVelocity=true;r.velocity=0f;
            }
            return r;
        }
    }
}
