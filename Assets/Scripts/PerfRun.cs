using System;
using System.IO;
using System.Text;
using Unity.Profiling;
using UnityEngine;

namespace Dc2Pilot
{
    /// <summary>
    /// DEBUG_ONLY performance runner (user request: "build for PC first and check performance issues"). Inert unless the player is started with
    /// <c>-dc2perf &lt;outputDir&gt;</c>. It plays fixed phases (menu, a real Map.xml kill mission, a boss mission) with the debug god mode / infinite ammo flags,
    /// a synthetic finger holding fire and the hero walking left and right, and writes one line per second: frame-time avg / p99 / max (ms), frames over
    /// 16.7 / 33.3 ms, GC allocated per frame (bytes), GC count, draw calls / batches / SetPass (development players only), managed heap and live
    /// GameObjects. Then it quits. No gameplay rule is changed; the flags are the existing debug-menu cheats.
    /// </summary>
    public sealed class PerfRun : MonoBehaviour
    {
        Dc2Game game; string outDir; bool uncapped; long lastMono, monoAlloc, phaseMonoAlloc; StringBuilder csv = new StringBuilder(); StringBuilder summary = new StringBuilder();
        ProfilerRecorder gcAlloc, drawCalls, batches, setPass, mainThread;
        readonly System.Collections.Generic.List<float> frameMs = new System.Collections.Generic.List<float>();
        readonly System.Collections.Generic.List<float> phaseMs = new System.Collections.Generic.List<float>();
        long gcBytes; int gcFrames, gcCountStart; float secondStart; string phase = "boot"; float phaseStart;
        long phaseGc; int phaseFrames, phaseOver33, phaseOver17, phaseGcCountStart;

        public static void InstallIfRequested(Dc2Game game)
        {
            var args = Environment.GetCommandLineArgs();
            bool uncapped = Array.IndexOf(args, "-dc2perf-uncapped") >= 0;
            for (int i = 0; i < args.Length - 1; i++)
                if (args[i] == "-dc2perf") { var r = game.gameObject.AddComponent<PerfRun>(); r.game = game; r.outDir = args[i + 1]; r.uncapped = uncapped; return; }
        }

        void OnEnable()
        {
            gcAlloc = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "GC Allocated In Frame");
            drawCalls = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Draw Calls Count");
            batches = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Batches Count");
            setPass = ProfilerRecorder.StartNew(ProfilerCategory.Render, "SetPass Calls Count");
            mainThread = ProfilerRecorder.StartNew(ProfilerCategory.Internal, "Main Thread", 15);
        }
        void OnDisable() { gcAlloc.Dispose(); drawCalls.Dispose(); batches.Dispose(); setPass.Dispose(); mainThread.Dispose(); }

        System.Collections.IEnumerator Start()
        {
            Directory.CreateDirectory(outDir);
            if (uncapped) { QualitySettings.vSyncCount = 0; Application.targetFrameRate = -1; }   // headroom: render as fast as possible (the native 60 Hz host clock is unaffected)
            lastMono = UnityEngine.Profiling.Profiler.GetMonoUsedSizeLong();
            csv.AppendLine("t,phase,fps,avg_ms,p99_ms,max_ms,over17,over33,gc_bytes_per_frame,gc_count,draw_calls,batches,setpass,heap_mb,gameobjects,dinos,est_alloc_kb_per_s");
            summary.AppendLine("DC2 perf run " + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + " screen " + Screen.width + "x" + Screen.height + " " + SystemInfo.graphicsDeviceName + " / " + SystemInfo.processorType + " / dev " + Debug.isDebugBuild);
            game.DebugGodMode = true; game.DebugInfiniteAmmo = true;
            gcCountStart = GC.CollectionCount(0); secondStart = Time.realtimeSinceStartup;
            BeginPhase("menu");
            yield return new WaitForSecondsRealtime(8f);
            var map = game.MapState ?? (game.MapState = new NativeMapState());
            foreach (int type in new[] { 3, 8, 1 })
            {
                NativeMapState.Mission best = null;
                foreach (var m in map.missions) if (m.type == type && (best == null || m.level > best.level)) best = m;
                if (best == null) continue;
                game.LaunchMission = best; game.SwitchScene("gameScene");
                BeginPhase("mission" + type + "_L" + best.level);
                yield return new WaitForSecondsRealtime(1f);
                // development players: record ~300 frames of the first battle to a binary profiler log for Editor/PerfProfileAnalyze (deep data when launched with -deepprofiling)
                if (Debug.isDebugBuild && type == 3) StartCoroutine(Capture(Path.Combine(outDir, "battle.raw"), 10f));
                float end = Time.realtimeSinceStartup + 40f; int dir = 1; float flip = Time.realtimeSinceStartup + 3f;
                game.QueueTouch(0, GameScene.FireRect.center, 140);
                game.QueueTouch(0, GameScene.RightRect.center, 141);
                while (Time.realtimeSinceStartup < end && game.Current is GameScene)
                {
                    if (Time.realtimeSinceStartup > flip)
                    {
                        flip = Time.realtimeSinceStartup + 3f;
                        game.QueueTouch(2, dir > 0 ? GameScene.RightRect.center : GameScene.LeftRect.center, 141);
                        dir = -dir;
                        game.QueueTouch(0, dir > 0 ? GameScene.RightRect.center : GameScene.LeftRect.center, 141);
                    }
                    yield return null;
                }
                game.QueueTouch(2, GameScene.FireRect.center, 140); game.QueueTouch(2, GameScene.RightRect.center, 141); game.QueueTouch(2, GameScene.LeftRect.center, 141);
                yield return null;
            }
            BeginPhase("end");
            File.WriteAllText(Path.Combine(outDir, "perf.csv"), csv.ToString());
            File.WriteAllText(Path.Combine(outDir, "summary.txt"), summary.ToString());
            Application.Quit();
        }

        System.Collections.IEnumerator Capture(string path, float delaySeconds)
        {
            yield return new WaitForSecondsRealtime(delaySeconds);
            UnityEngine.Profiling.Profiler.logFile = path; UnityEngine.Profiling.Profiler.enableBinaryLog = true; UnityEngine.Profiling.Profiler.enabled = true;
            for (int i = 0; i < 300; i++) yield return null;
            UnityEngine.Profiling.Profiler.enabled = false; UnityEngine.Profiling.Profiler.enableBinaryLog = false; UnityEngine.Profiling.Profiler.logFile = "";
            summary.AppendLine("CAPTURE " + path + " (300 frames)");
        }

        void BeginPhase(string name)
        {
            if (phaseFrames > 0)
            {
                phaseMs.Sort();
                float avg = 0f; foreach (var v in phaseMs) avg += v; avg /= phaseMs.Count;
                summary.AppendLine(string.Format("PHASE {0}: {1} frames, avg {2:0.00} ms ({3:0} fps), p99 {4:0.00} ms, max {5:0.00} ms, >16.7 ms {6}, >33.3 ms {7}, GC alloc/frame {8:0} B, GC collections {9}, est. managed alloc {10:0} KB/s",
                    phase, phaseFrames, avg, 1000f / avg, phaseMs[(int)(phaseMs.Count * 0.99f)], phaseMs[phaseMs.Count - 1], phaseOver17, phaseOver33, (double)phaseGc / phaseFrames, GC.CollectionCount(0) - phaseGcCountStart, phaseMonoAlloc / 1024.0 / Math.Max(0.001, Time.realtimeSinceStartup - phaseStart)));
            }
            phase = name; phaseStart = Time.realtimeSinceStartup; phaseMs.Clear(); phaseGc = 0; phaseFrames = 0; phaseOver33 = 0; phaseOver17 = 0; phaseGcCountStart = GC.CollectionCount(0); phaseMonoAlloc = 0;
        }

        void Update()
        {
            float ms = Time.unscaledDeltaTime * 1000f;
            frameMs.Add(ms); phaseMs.Add(ms); phaseFrames++;
            if (ms > 16.7f) phaseOver17++; if (ms > 33.3f) phaseOver33++;
            long a = gcAlloc.Valid ? gcAlloc.LastValue : 0; gcBytes += a; gcFrames++; phaseGc += a;
            long mono = UnityEngine.Profiling.Profiler.GetMonoUsedSizeLong(); if (mono > lastMono) { monoAlloc += mono - lastMono; phaseMonoAlloc += mono - lastMono; } lastMono = mono;   // lower bound: growth between frames (a GC inside a frame hides part of it)
            if (Time.realtimeSinceStartup - secondStart >= 1f)
            {
                frameMs.Sort();
                float avg = 0f; foreach (var v in frameMs) avg += v; avg /= frameMs.Count;
                var gs = game.Current as GameScene;
                csv.AppendLine(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:0.0},{1},{2},{3:0.00},{4:0.00},{5:0.00},{6},{7},{8:0},{9},{10},{11},{12},{13:0.0},{14},{15}",
                    Time.realtimeSinceStartup, phase, frameMs.Count, avg, frameMs[(int)(frameMs.Count * 0.99f)], frameMs[frameMs.Count - 1],
                    frameMs.FindAll(v => v > 16.7f).Count, frameMs.FindAll(v => v > 33.3f).Count, gcFrames > 0 ? (double)gcBytes / gcFrames : 0, GC.CollectionCount(0) - gcCountStart,
                    drawCalls.Valid ? drawCalls.LastValue : -1, batches.Valid ? batches.LastValue : -1, setPass.Valid ? setPass.LastValue : -1,
                    GC.GetTotalMemory(false) / 1048576.0, FindObjectsByType<Transform>(FindObjectsSortMode.None).Length, gs != null ? gs.dinos.Count : 0) + "," + (monoAlloc / 1024));
                monoAlloc = 0; frameMs.Clear(); gcBytes = 0; gcFrames = 0; secondStart = Time.realtimeSinceStartup;
            }
        }
    }
}
