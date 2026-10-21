using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor.Profiling;
using UnityEditorInternal;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>DEBUG_ONLY (r119x perf work): loads a player profiler log (-profileRaw path) and writes the top GC-allocating and CPU-heavy samples
    /// (self values, summed over all frames, aggregated by sample name) to -reportDir/profile_top.txt. Not part of the suite.</summary>
    public static class PerfProfileAnalyze
    {
        public static void Run()
        {
            string raw = null, output = "Reports/perf-analyze";
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++) { if (args[i] == "-profileRaw") raw = args[i + 1]; if (args[i] == "-reportDir") output = args[i + 1]; }
            Directory.CreateDirectory(output);
            var log = new StringBuilder();
            try
            {
                if (!ProfilerDriver.LoadProfile(raw, false)) throw new Exception("LoadProfile failed: " + raw);
                int first = ProfilerDriver.firstFrameIndex, last = ProfilerDriver.lastFrameIndex;
                var gc = new Dictionary<string, double>(); var cpu = new Dictionary<string, double>(); var calls = new Dictionary<string, long>();
                double totalGc = 0, totalMs = 0; int frames = 0;
                for (int f = first; f <= last; f++)
                {
                    using (var view = ProfilerDriver.GetHierarchyFrameDataView(f, 0, HierarchyFrameDataView.ViewModes.Default, HierarchyFrameDataView.columnGcMemory, false))
                    {
                        if (view == null || !view.valid) continue;
                        frames++; totalMs += view.frameTimeMs;
                        var stack = new Stack<int>(); stack.Push(view.GetRootItemID()); var children = new List<int>();
                        var parentOf = new Dictionary<int, int>();
                        while (stack.Count > 0)
                        {
                            int id = stack.Pop();
                            string name = view.GetItemName(id);
                            if (name == "GC.Alloc" && parentOf.TryGetValue(id, out int par))   // credit the allocation to the calling method (and its caller)
                            {
                                string caller = view.GetItemName(par);
                                int up = par; for (int depth = 0; depth < 3 && parentOf.TryGetValue(up, out int gp); depth++) { caller = caller + "  <-  " + view.GetItemName(gp); up = gp; }
                                double bytes = view.GetItemColumnDataAsFloat(id, HierarchyFrameDataView.columnGcMemory);
                                gc[caller] = (gc.TryGetValue(caller, out var gb) ? gb : 0) + bytes; totalGc += bytes;
                                calls[caller] = (calls.TryGetValue(caller, out var cb) ? cb : 0) + (long)view.GetItemColumnDataAsFloat(id, HierarchyFrameDataView.columnCalls);
                                continue;
                            }
                            children.Clear(); view.GetItemChildren(id, children);
                            foreach (int c in children) { parentOf[c] = id; stack.Push(c); }
                            double selfMs = view.GetItemColumnDataAsFloat(id, HierarchyFrameDataView.columnSelfTime);
                            cpu[name] = (cpu.TryGetValue(name, out var m) ? m : 0) + selfMs;
                            calls[name] = (calls.TryGetValue(name, out var n) ? n : 0) + (long)view.GetItemColumnDataAsFloat(id, HierarchyFrameDataView.columnCalls);
                        }
                    }
                }
                log.AppendLine("frames " + frames + " (" + first + ".." + last + "), avg frame " + (totalMs / Math.Max(1, frames)).ToString("0.00") + " ms, GC total " + (totalGc / 1024).ToString("0") + " KB = " + (totalGc / Math.Max(1, frames)).ToString("0") + " B/frame");
                log.AppendLine("== top GC allocators (self bytes per frame, calls per frame)");
                foreach (var kv in gc.OrderByDescending(k => k.Value).Take(40))
                    log.AppendLine(string.Format("{0,10:0} B/f  {1,7:0.0} calls/f  {2}", kv.Value / frames, (calls.TryGetValue(kv.Key, out var c1) ? c1 : 0) / (double)frames, kv.Key));
                log.AppendLine("== top CPU (self ms per frame)");
                foreach (var kv in cpu.OrderByDescending(k => k.Value).Take(40))
                    log.AppendLine(string.Format("{0,8:0.000} ms/f  {1,7:0.0} calls/f  {2}", kv.Value / frames, (calls.TryGetValue(kv.Key, out var c2) ? c2 : 0) / (double)frames, kv.Key));
                log.AppendLine("SUMMARY passes=0 fails=0");
            }
            catch (Exception ex) { log.AppendLine("EXCEPTION " + ex); }
            finally { File.WriteAllText(Path.Combine(output, "profile_top.txt"), log.ToString()); File.WriteAllText(Path.Combine(output, "verify.txt"), log.ToString()); PilotExit.Exit(0); }
        }
    }
}
