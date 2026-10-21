namespace Dc2Pilot
{
    /// <summary>The game's single random stream.
    /// NATIVE (shipped binary): Triniti2D::EngineShell::InitApplication 0x165c7a calls srand(time(NULL)) once at start-up; every consumer
    /// (Triniti2D::RandomRange 0x15de8, the direct rand() calls in CreateEnemy / BeAttacked / Blood / bossN / particle emitters, ...) draws from libc rand().
    /// PLATFORM (LIKELY, not in the shipped binary): iOS libSystem rand() is FreeBSD rand.c without USE_WEAK_SEEDING, the Park-Miller "minimal standard"
    /// generator (x = 16807 * x mod (2^31 - 1), Schrage form, a zero state replaced by 123459876), RAND_MAX = 0x7fffffff. Seeded with the Unix time in seconds.
    /// Verification harnesses (Dc2Game.manualStep) reseed with a fixed value so runs are reproducible (DEBUG_ONLY).</summary>
    public static class NativeRand
    {
        static uint next = (uint)System.DateTimeOffset.UtcNow.ToUnixTimeSeconds();   // srand(time(NULL)) at launch
        public static void Seed(uint seed) { next = seed; }
        public static uint State { get { return next; } }   // DEBUG_ONLY

        /// <summary>libc rand(): do_rand(&next).</summary>
        public static int Raw()
        {
            long x = next;
            if (x == 0) x = 123459876;
            long hi = x / 127773, lo = x % 127773;
            x = 16807 * lo - 2836 * hi;
            if (x < 0) x += 0x7fffffff;
            next = (uint)x;
            return (int)(x % 0x80000000L);
        }

        /// <summary>(float)rand() * 2^-31 (literal 0x30000000), as RandomRange and the direct rand() consumers compute it.</summary>
        public static float Unit(int raw) { return (float)raw * N.F(805306368); }
        public static float Unit() { return Unit(Raw()); }

        /// <summary>Triniti2D::RandomRange(lo, hi) 0x15de8: lo + (int)(Unit * (float)(hi - lo)).</summary>
        public static int Range(int lo, int hi) { return NativeRandom.RangeFromRaw(lo, hi, Raw()); }
    }
}
