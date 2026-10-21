# Dino Cap 2: Unity port (PC and Android)

> **Disclaimer.** Dino Cap 1, 2 and 3 were made by Triniti Interactive. Everything else here is AI-reviewed, may change at any time and should not be fully trusted. See [the disclaimer](Docs/start/disclaimer.md).

## Play it first

- **[Download guide](Docs/start/download-guide.md)**: get the Windows or Android build from the **Releases** section of this GitHub page, not from the *Code* button.
- **[Controls for PC](Docs/start/controls-pc.md)**:
  - <kbd>A</kbd> / <kbd>D</kbd> walk;
  - <kbd>K</kbd> or left click shoots, <kbd>J</kbd> or right click is melee;
  - <kbd>S</kbd> rolls;
  - <kbd>L</kbd> or the mouse wheel switches weapon;
  - <kbd>Esc</kbd> pauses.
- **[Controls for phones](Docs/start/controls-mobile.md)**: the iPad's touch controls, landscape, with multi-touch.
- **Curious how this was figured out?** We took the original iPad game apart, rebuilt the mechanics from what its code really does, and checked them against a real iPad. The few values not recovered yet are labelled stand-ins with a removal condition ([open questions](Docs/technical/OPEN_QUESTIONS.md)). [Here's the whole trail](Docs/findings.md), wrong turns included.

## What this is

A faithful rebuild of Dino Cap 2 (iOS, version 1.3) for modern PC and Android, made by studying the original game rather than guessing.
- **Partial decompilation:** of 418 native game functions in scope, 276 have test proof and 142 are traced or ported without their own check ([coverage](Docs/technical/binary/COVERAGE.md)).
- **Original quirks kept:** odd original behaviour stays as it was.
- **Every intentional difference listed:** wide screens, PC controls and the bank are all [on one page](Docs/fidelity/shipped-quirks-and-divergences.md).

**Status:**
- 46 / 46 automated checks pass.
- Windows and Android builds are made, the Android build running at 60 fps on a mid-range phone.
- Two independent reviews are done and their findings fixed; the final re-check of the pre-publication audit is pending.
- Open items are in the [validation matrix](Docs/release/build-validation-matrix.md) and the [open questions](Docs/technical/OPEN_QUESTIONS.md).

## Build from source

Open this folder in **Unity 6000.3.8f1**. The project creates its scene at runtime (`Assets/Scripts/Bootstrap.cs`). The shipped builds use the editor methods in `Assets/Editor/PilotBuild.cs`:
- `BuildWindows`: Windows x64, Mono;
- `BuildAndroid`: Android, IL2CPP, ARMv7 + ARM64 in one APK.

For example, in PowerShell (quote every path: the output name contains spaces, and `-buildOut` takes exactly the next argument):

```powershell
& "C:\Program Files\Unity\Hub\Editor\6000.3.8f1\Editor\Unity.exe" -batchmode -quit -projectPath "D:\src\DC2-Dev" -executeMethod Dc2Pilot.EditorTools.PilotBuild.BuildWindows -buildOut "D:\out\Windows\Dino Cap 2.exe"
```

Without `-buildOut` the output goes to `Build/Windows` or `Build/Android` inside the project (both git-ignored). Build Android from a project copy or with `-buildTarget Android`, so the project's active platform is not switched under you. See [the clean-checkout check](Docs/release/clean-checkout-build.md) for what was reproduced.

The original IPA, the reverse-engineering database and the working reports stay private.

## Run the checks

```
Unity.exe -batchmode -projectPath <this folder> -executeMethod Dc2Pilot.EditorTools.PilotBugsVerify.Run -reportDir <folder>
```

Each `Assets/Editor/Pilot*Verify.cs` program writes `verify.txt` with `PASS` / `FAIL` lines. They prove logic, timing and layout numbers, not original-device equality, feel or audio ([testing and validation](Docs/quality/testing-and-validation.md)).

## Documentation

| Section | Pages |
|---|---|
| Start here | [What is this?](Docs/start/what-is-this.md), [findings](Docs/findings.md), [glossary](Docs/start/glossary.md), [troubleshooting](Docs/start/troubleshooting.md), [credits](Docs/start/credits.md) |
| How it works | [1 Overview](Docs/how-it-works/01-overview.md), [2 Art and animation](Docs/how-it-works/02-how-the-game-stores-its-art.md), [3 Rendering and wide view](Docs/how-it-works/03-how-the-picture-reaches-your-screen.md), [4 Sound](Docs/how-it-works/04-sound.md), [5 Combat](Docs/how-it-works/05-combat-controls.md), [6 Dinosaurs and bosses](Docs/how-it-works/06-dinosaurs-and-spawning.md), [7 Saves and progression](Docs/how-it-works/07-saves-and-progression.md), [8 Time and pause](Docs/how-it-works/08-pause-and-time.md), [9 Menus](Docs/how-it-works/09-menu-and-screens.md), [10 Missions](Docs/how-it-works/10-missions.md) |
| Fidelity | [Quirks kept and every divergence](Docs/fidelity/shipped-quirks-and-divergences.md) |
| History | [Timeline and agents](Docs/history/timeline-and-agents.md), [corrected claims](Docs/history/corrected-claims.md), [task index](Docs/history/task-index.md) |
| Quality | [Testing](Docs/quality/testing-and-validation.md), [lessons learned](Docs/quality/lessons-learned.md), [working with AI agents](Docs/quality/working-with-ai-agents.md), [failure ledger](Docs/quality/failure-ledger.md), [policy history](Docs/quality/policy-history.md) |
| Release | [Build and validation matrix](Docs/release/build-validation-matrix.md), [clean-checkout build](Docs/release/clean-checkout-build.md), [tools and release notes](Docs/release/tools-and-release-notes.md) |
| Technical | [Technical index](Docs/technical/README.md): binary identity, [address index](Docs/technical/functions/ADDRESS_INDEX.md) of the core functions with a dossier each, call graph, classes, [source-to-Unity map](Docs/technical/maps/SOURCE_TO_UNITY_MAP.md), [asset identity](Docs/technical/maps/ASSET_IDENTITY_MAP.md), registry, tools |
| Reusable notes | [Notes for other games](Docs/knowledge/README.md) |
