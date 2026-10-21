using UnityEngine;

namespace Dc2Pilot
{
    /// <summary>Creates the camera and the game, then enters menuScene (startup splash first).</summary>
    public static class Bootstrap
    {
        /// <summary>Single camera over the 1024x768 UI/touch space (Application size when OSEnableiPadToiPhone4 is NOT active).
        /// NATIVE: Camera::Initialize 0x161cb0 only calls glViewport(32,64,960,640) when IsGameSupportiPadToiPhone4 (0x1921b4) is set; in that mode
        /// Application size is 480x320 and touches are (x-32,y-64)/2 (Application::TouchEvent 0x15f890). Showcase.mp4 content spans the full iPad width
        /// with ~32 px bars top/bottom, i.e. that mode is not active; the bars are unused screen (menu bg art is 1024x704 at y=32).</summary>
        public static Camera MakeCamera()
        {
            var go = new GameObject("Dc2Camera");
            var cam = go.AddComponent<Camera>();
            cam.orthographic = true;
            cam.orthographicSize = N.ScreenH * 0.5f;
            cam.transform.position = new Vector3(N.ScreenW * 0.5f, -N.ScreenH * 0.5f, -10f);
            cam.clearFlags = CameraClearFlags.SolidColor;
            cam.backgroundColor = Color.black;
            cam.nearClipPlane = 0.1f; cam.farClipPlane = 100f;
            return cam;
        }

        public static void RenderAll(Camera cam) { cam.Render(); }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void Boot()
        {
            foreach (var c in Object.FindObjectsByType<Camera>(FindObjectsSortMode.None)) Object.Destroy(c.gameObject);
            // r119u mobile host (PLATFORM_ADAPTER reproducing the native shell, AGENTS.md section 12 host layer): the iOS shell ticks from a 60 Hz NSTimer
            // (EngineAppDelegate 0x18365c, host_clock_r118z.md); Unity mobile defaults to 30 fps, which would run one native update per frame at dt 1/30 and halve
            // every CAObjCallBack animation (one frame per update). Touches arrive only as touches (no simulated mouse button 0). Multi-touch on (native
            // setMultipleTouchEnabled:). Orientation: landscape left/right only (Info.plist UISupportedInterfaceOrientations; ProjectSettings portrait off).
            if (Application.isMobilePlatform) { Application.targetFrameRate = 60; Input.simulateMouseWithTouches = false; Input.multiTouchEnabled = true; }
            // PLATFORM_ADAPTER (user decisions): "keep awake" (not native: the original most likely lets iOS auto-lock; no setIdleTimerDisabled: in the binary)
            // and Android "immersive" (system bars hidden; Unity's Android fullscreen mode is sticky immersive, androidStartInFullscreen = 1; re-asserted on focus in Dc2Game).
            if (Application.isMobilePlatform) { Screen.sleepTimeout = SleepTimeout.NeverSleep; if (Application.platform == RuntimePlatform.Android) Screen.fullScreen = true; }
            var cam = MakeCamera();
            // PLATFORM_ADAPTER (r119q, user report "F1 menu closed in the log but still on my screen" at 1918x360): when CameraFit pillar/letter-boxes the
            // game camera (rect < full screen: 4:3 mode, or wide view beyond the 21:9 cap) Unity clears only inside that rect, so the bars kept whatever was
            // drawn there last (the debug menu). This camera renders nothing (culling mask 0) and only clears the whole screen black before the game camera.
            var bars = new GameObject("Dc2BarClear").AddComponent<Camera>();
            bars.clearFlags = CameraClearFlags.SolidColor; bars.backgroundColor = Color.black; bars.cullingMask = 0; bars.depth = -100f; bars.rect = new Rect(0f, 0f, 1f, 1f);
            var fit = cam.gameObject.AddComponent<CameraFit>();
            // PLATFORM_ADAPTER (r119b, user report "No audio listeners"): Boot destroys the scene's default camera, which carried Unity's only AudioListener;
            // without one nothing is heard. The listener only receives the 2D music / sfx sources (no positional audio).
            cam.gameObject.AddComponent<AudioListener>();
            var game = Dc2Game.Create(Application.persistentDataPath);
            game.cam = cam; fit.game = game;
            PcPlatform.Install(game.gameObject, game);   // PLATFORM_ADAPTER: window size / fullscreen toggle / keyboard (user request)
            DebugMenu.Install(game.gameObject, game);    // DEBUG_ONLY: F1 test menu (DC2-BUGS-001 O2, user request); inert while closed
            PerfRun.InstallIfRequested(game);            // DEBUG_ONLY: -dc2perf <dir> performance run (user request); inert without the flag
            game.SwitchScene("menuScene");
        }
    }

    /// <summary>Fits the camera to the window: WideView (ADAPTATION, default on) fills the screen; off = the 4:3 design area letter/pillar-boxed.</summary>
    public class CameraFit : MonoBehaviour
    {
        public Dc2Game game;
        void LateUpdate()
        {
            // after Dc2Game.Update (scene switches happen there), before rendering
            WideView.Apply(GetComponent<Camera>(), game != null ? game.Current : null, Screen.width, Screen.height);
        }
    }
}
