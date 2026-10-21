namespace Dc2Pilot
{
    // playerClass::UpdateCamera bb054..bb1dc. Physics/mission-end consumers follow
    // this block separately; this vessel models the recovered camera block only.
    public sealed class NativeGameCamera
    {
        public float left = -512f; // Camera::Initialize: -0.5*Application screen width.
        public bool turning;
        public bool clamped;

        public void Step(float heroX, bool facingLeft, float dt, float lower, float upper)
        {
            float target = N.Add32(heroX, -N.Mul32(facingLeft ? .55f : .45f, 960f));
            if (turning)
            {
                left = N.Add32(left, N.Mul32(facingLeft ? -600f : 600f, dt));
                float difference = N.Add32(left, -target);
                if (facingLeft ? difference < 0f : difference > 0f) turning = false;
                // Native keeps the overshoot this tick; snap occurs next tick.
            }
            else left = target;
            clamped = false;
            if (left < lower) { left = lower; clamped = true; }
            else if (N.Add32(left, 1024f) > upper) { left = N.Add32(upper, -1024f); clamped = true; }
        }
    }
}
