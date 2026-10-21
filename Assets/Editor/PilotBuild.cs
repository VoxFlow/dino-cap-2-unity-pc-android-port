using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dc2Pilot.EditorTools
{
    /// <summary>Builds the Windows x64 and Android players (Bootstrap creates everything at runtime).</summary>
    public static class PilotBuild
    {
        /// <summary>DEBUG_ONLY (r119x perf work): development player with deep-profiling support in Build/WindowsDev (PerfRun writes a profiler log there).</summary>
        public static void BuildWindowsDev() { Build(Path.Combine("Build", "WindowsDev", "Dino Cap 2.exe"), BuildTarget.StandaloneWindows64, BuildOptions.Development | BuildOptions.EnableDeepProfilingSupport); }
        public static void BuildWindows() { Build(Path.Combine("Build", "Windows", "Dino Cap 2.exe"), BuildTarget.StandaloneWindows64, BuildOptions.None); }
        /// <summary>Android APK, ARMv7 + ARM64 (IL2CPP; Google Play requires the 64-bit slice, 32-bit-only devices need ARMv7). Landscape only comes from ProjectSettings (native Info.plist, DC2-MOBILE-001).</summary>
        public static void BuildAndroid() { Build(Path.Combine("Build", "Android", "Dino Cap 2.apk"), BuildTarget.Android, BuildOptions.None); }
        /// <summary>DEBUG_ONLY: development APK (profiler connectable over adb; PerfRun works with -dc2perf through the launch intent extras).</summary>
        public static void BuildAndroidDev() { Build(Path.Combine("Build", "AndroidDev", "Dino Cap 2.apk"), BuildTarget.Android, BuildOptions.Development); }

        // r119x (user request "use the highest res original ipa icon"): iTunesArtwork from the shipped IPA (512x512 JPEG, the largest icon art in the
        // bundle; the CFBundleIconFiles go up to icon@2x.png 114x114), copied to Assets/Icons/AppIcon512.jpg (SHA-256 233cb0cb...; the IPA itself is untouched).
        const string IconPath = "Assets/Icons/AppIcon512.jpg";

        static void Build(string location, BuildTarget target, BuildOptions options)
        {
            string scenePath = "Assets/Scenes/Boot.unity";
            Directory.CreateDirectory("Assets/Scenes");
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            EditorSceneManager.SaveScene(scene, scenePath);

            PlayerSettings.companyName = "Pilot";
            PlayerSettings.productName = "Dino Cap 2";   // user request: shipped name (Windows "Dino Cap 2.exe", Android label "Dino Cap 2")
            PlayerSettings.defaultScreenWidth = 1024;
            PlayerSettings.defaultScreenHeight = 768;
            PlayerSettings.fullScreenMode = FullScreenMode.Windowed;
            PlayerSettings.resizableWindow = true;
            PlayerSettings.runInBackground = true;
            PlayerSettings.SplashScreen.show = false; PlayerSettings.SplashScreen.showUnityLogo = false;   // user request: no "Made with Unity" splash (allowed on every Unity 6 license tier)

            AssetDatabase.ImportAsset(IconPath);
            var importer = (TextureImporter)AssetImporter.GetAtPath(IconPath);
            if (importer != null && (importer.textureCompression != TextureImporterCompression.Uncompressed || importer.mipmapEnabled))
            { importer.textureCompression = TextureImporterCompression.Uncompressed; importer.mipmapEnabled = false; importer.SaveAndReimport(); }
            var icon = AssetDatabase.LoadAssetAtPath<Texture2D>(IconPath);
            if (icon != null)
            {
                PlayerSettings.SetIcons(NamedBuildTarget.Unknown, new[] { icon }, IconKind.Any);   // default icon: every platform and size is generated from it
                if (target == BuildTarget.Android)
                {
                    var kinds = PlayerSettings.GetSupportedIconKinds(NamedBuildTarget.Android);
                    foreach (var kind in kinds)
                    {
                        var icons = PlayerSettings.GetPlatformIcons(NamedBuildTarget.Android, kind);
                        foreach (var pi in icons) { var layers = new Texture2D[pi.maxLayerCount]; for (int l = 0; l < layers.Length; l++) layers[l] = icon; pi.SetTextures(layers); }
                        PlayerSettings.SetPlatformIcons(NamedBuildTarget.Android, kind, icons);
                    }
                }
            }
            else Debug.LogWarning("BUILD icon missing: " + IconPath);

            // user decision: "universal armv7+v8 IL2CPP for Android and Mono for PC"
            if (target == BuildTarget.StandaloneWindows64) PlayerSettings.SetScriptingBackend(NamedBuildTarget.Standalone, ScriptingImplementation.Mono2x);
            if (target == BuildTarget.Android)
            {
                PlayerSettings.SetApplicationIdentifier(NamedBuildTarget.Android, "com.pilot.dc2port");   // neutral identifier (AGENTS.md section 2)
                PlayerSettings.SetScriptingBackend(NamedBuildTarget.Android, ScriptingImplementation.IL2CPP);
                PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARMv7 | AndroidArchitecture.ARM64;   // both: some low-tier Android phones run a 32-bit system (abilist armeabi-v7a only)
                PlayerSettings.defaultInterfaceOrientation = UIOrientation.AutoRotation;
                PlayerSettings.allowedAutorotateToPortrait = false; PlayerSettings.allowedAutorotateToPortraitUpsideDown = false;
                PlayerSettings.allowedAutorotateToLandscapeLeft = true; PlayerSettings.allowedAutorotateToLandscapeRight = true;
                EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
            }

            var args = System.Environment.GetCommandLineArgs();   // -buildOut <file>: absolute output (the Android build runs in a project copy so the live project keeps its Windows platform)
            for (int i = 0; i < args.Length - 1; i++) if (args[i] == "-buildOut") location = args[i + 1];
            Directory.CreateDirectory(Path.GetDirectoryName(location));
            var opts = new BuildPlayerOptions { scenes = new[] { scenePath }, locationPathName = location, target = target, options = options };
            var report = BuildPipeline.BuildPlayer(opts);
            Debug.Log("BUILD " + report.summary.result + " size=" + report.summary.totalSize + " errors=" + report.summary.totalErrors);
            PilotExit.Exit(report.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded ? 0 : 1);
        }
    }
}
