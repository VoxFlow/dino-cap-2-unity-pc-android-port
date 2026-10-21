using UnityEditor;

namespace Dc2Pilot.EditorTools
{
    /// <summary>DEBUG_ONLY: verifier exit hook. Normal batch runs exit as before; the coverage runner (PilotCoverageRun) sets Hold so several verifiers can run in one recording session.</summary>
    public static class PilotExit
    {
        public static bool Hold;
        public static int LastCode;
        public static void Exit(int code) { LastCode = code; if (!Hold) EditorApplication.Exit(code); }
    }
}
