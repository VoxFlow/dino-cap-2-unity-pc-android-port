using System;
using System.Reflection;
using UnityEditor;
using UnityEditor.TestTools.CodeCoverage;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>DEBUG_ONLY: runs the verifiers named by -coverageMethods (comma separated, e.g. PilotLabelVerify,PilotJumpVerify) inside one Code Coverage recording.
    /// Start with -enableCodeCoverage -coverageResultsPath <dir> -coverageOptions "assemblyFilters:+Assembly-CSharp". Exit code = number of failing verifiers.</summary>
    public static class PilotCoverageRun
    {
        public static void Run()
        {
            string list = "";
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-coverageMethods") list = args[i + 1];
            PilotExit.Hold = true; int failed = 0;
            CodeCoverage.StartRecording();
            foreach (var name in list.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var t = Type.GetType("Dc2Pilot.EditorTools." + name.Trim());
                if (t == null) { Debug.LogError("coverage: no verifier " + name); failed++; continue; }
                PilotExit.LastCode = 0;
                try { t.GetMethod("Run", BindingFlags.Public | BindingFlags.Static).Invoke(null, null); }
                catch (Exception e) { Debug.LogError("coverage: " + name + " threw " + e); PilotExit.LastCode = 1; }
                if (PilotExit.LastCode != 0) failed++;
                Debug.Log("coverage: " + name + " exit " + PilotExit.LastCode);
            }
            CodeCoverage.StopRecording();
            PilotExit.Hold = false;
            EditorApplication.Exit(failed);
        }
    }
}
