# 1. Overview: what was built and how

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../start/disclaimer.md).*

> **In simple terms.** Read what the original program does, write it down as a rule with its evidence, rebuild the rule in Unity, then check the rebuild against the rule (and against the real iPad where code alone cannot decide).

## The pipeline

1. **Original files, untouched.** The shipped IPA (SHA-256 `6cec1c94…940038`) is only read through hash-checked copies. Its executable is analysed in IDA ([binary identity](../technical/binary/ADDRESS_CONVENTIONS.md)); its data comes from `DinoCapData.dat`, a ZIP of images, atlases, animations, audio and configuration.
2. **Native first.**
   - The ARM/Thumb instructions outrank the decompiler's pseudocode.
   - Numeric literals are decoded with tools, never read by eye.
   - Where a decision depended on runtime values, small CPU emulations of the original functions were run (Unicorn) on supplied inputs.
3. **Written rules.** Each recovered behaviour becomes a registry entry with a confidence label and its evidence ([registry](../technical/registry/README.md)). Wrong entries are kept, marked FALSIFIED or SUPERSEDED, with their successor.
4. **Assets in their own formats.** Sprite atlases (`.conf` + `.png`) and the game's animation formats (`.a2b`, `.an2`, `.acb`, `.animation.acb`) are parsed and played by a small engine-neutral player inside Unity, instead of being re-made in Unity's animator ([chapter 2](02-how-the-game-stores-its-art.md)).
5. **The host too.**
   - The iOS shell ticks 60 times per second (an `NSTimer`) and caps the time step at 0.1 s; the port reproduces that ([chapter 8](08-pause-and-time.md)).
   - Touches are queued and handled at the start of the scene update, as on iOS.
6. **Checks.** 46 verifier programs run the game headless at 60 Hz and assert recovered values ([testing](../quality/testing-and-validation.md)). The user compared features on a real iPad.
7. **Reviews.** Independent review rounds by Claude and Codex; every confirmed defect gets its own regression test ([corrected claims](../history/corrected-claims.md)).

## Code layout

| Path | What it holds |
|---|---|
| `Assets/Scripts/Bootstrap.cs`, `Dc2Scene.cs` | Boot, the native host clock, the scene base (controls, labels, touch queue, timers) |
| `GameScene.cs` + `Game*.cs` (partial class) | The battle scene: combat, fire, beams, boss, egg, run, weather, blood, HUD, missions |
| `MenuScene.cs`, `SelectScene.cs` + `Select*.cs` | Title, map, hero, armory, bank, jigsaw, options |
| `Native*.cs` | Engine-neutral players for the original formats (animations, fonts, sprites, particles, map data, save file) |
| `WideView.cs`, `PcPlatform.cs`, `DebugMenu.cs` | Adaptations, the platform layer and the debug tool, each labelled |
| `Assets/Editor/Pilot*Verify.cs` | The verifier programs; `PilotBuild.cs` makes the shipped builds |
| `Assets/Resources/DC2/` | The converted game data ([asset map](../technical/maps/ASSET_IDENTITY_MAP.md)) |
