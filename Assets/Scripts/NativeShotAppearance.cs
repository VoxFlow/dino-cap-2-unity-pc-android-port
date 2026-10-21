namespace Dc2Pilot
{
    /// <summary>Dormant native counter/suffix vessel. KaiQiang216/fireup64 ARM
    /// cases r91; accepted type2/3 burst start resets counter, not suffix.
    /// Live firing/timeout lifecycle: wired in GameFire (AfterKaiQiang after type 2/3 shots, FireupTimeout), checked by PilotFireVerify (r118x: old OPEN note stale).</summary>
    public sealed class NativeShotAppearance
    {
        public int Counter;
        public string Suffix="";
        public void AcceptedBurstStart(int weaponType)
        {if(weaponType==2||weaponType==3)Counter=0;}
        public void AfterKaiQiang(int weaponType)
        {
            if(weaponType!=2&&weaponType!=3)return;
            int before=Counter;Counter=unchecked(before+1);
            if(unchecked((uint)(before-9))<=19)Suffix="_nu1";
            else if(unchecked((uint)(before-29))<=19)Suffix="_nu2";
            else if(Counter>49)Suffix="_nu3";
        }
        public void FireupTimeout(){Suffix="";} // native handler bb566..bb576; no counter/firing gate write.
    }
}
