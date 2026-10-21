# Troubleshooting

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](disclaimer.md).*

| Problem | What to do |
|---|---|
| Windows: "Windows protected your PC" | The build is unsigned. Choose *More info → Run anyway* only if you trust where you downloaded it. |
| Windows: the game does not start | Keep `Dino Cap 2.exe`, `Dino Cap 2_Data`, `MonoBleedingEdge`, `UnityPlayer.dll` and `UnityCrashHandler64.exe` together in one folder. |
| Windows: the window is small or large | Drag the window edge; the picture refits. <kbd>F11</kbd> toggles fullscreen. |
| Black bars at the sides | Your screen is wider than 21:9, or wide view is off (developer menu, <kbd>F1</kbd>). This is expected. |
| Android: "App not installed" | Allow installs from your browser or file manager. One APK covers 32-bit and 64-bit phones. |
| Android: the system bars stay visible | Report your phone model: immersive mode is not yet verified on every device. |
| A save from an older test build is missing (Windows) | Builds named "DC2 Pilot" stored saves under `AppData\LocalLow\Pilot\DC2 Pilot`; the shipped build uses `...\Pilot\Dino Cap 2`. |
| Something behaves differently from the iPad | Please report it with a recording of the original if you can ([how to report](../release/tools-and-release-notes.md#reporting-a-difference)). |
