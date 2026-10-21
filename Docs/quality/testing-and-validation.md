# Testing and validation: what was checked, and what the checks cannot show

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../start/disclaimer.md).*

> **In simple terms.** 46 automated programs check the game's rules against values taken from the original; the user checked how it looks and feels on a real iPad. Neither replaces the other.

## The verifier programs

`Assets/Editor/Pilot*Verify.cs`: 46 programs plus a few diagnostic probes that are not part of the suite. Each runs the game headless in Unity batch mode at the native 60 Hz and writes `PASS` / `FAIL` lines.

What they assert:
- damage and falloff formulas;
- reaction tables;
- animation frame rules (end handlers, loops, key frames);
- spawn and camera bounds;
- HUD control ids, layers and creation order;
- label rects;
- saves;
- timers and the host clock at non-60 Hz rates;
- input order;
- the wide-view rules and their equivalence at 4:3.

**Native oracles.** Most answers come from the original code, sometimes by emulating an original function on fixed inputs. Tests of adaptations check the adaptation's own stated rule instead. When a test and the original disagreed, the original won and the test changed, and guessed values are never used as native answers.

**Running them.** The suite runs as parallel Unity instances, each on its own copy of the project (`run_suite_parallel.ps1` in the research tools). Last full run: **46 / 46 passing** (r119x, after the performance changes). Targeted runs follow each later fix (for example Bank 31/31 and Bugs 71/71 after the cash fix).

## What they cannot show

- **How it looks.** Headless runs prove logic, state and timing, not pictures. Visual matches come from captures compared with references and from the user's iPad A/B.
- **Every branch.** A test that executes a function's port is not proof that it matches the original everywhere. 142 in-scope functions have no dedicated check ([coverage](../technical/binary/COVERAGE.md)).
- **Device behaviour** they do not simulate: real multi-touch, interruptions, immersive mode, audio output.

## Reviews and device checks

| Check | Result |
|---|---|
| Codex engine review rounds R1 – R5 | Confirmed and closed |
| Whole-game review R002 (Claude, then Codex) | 5 blocking defects, fixed with tests, confirmed by Codex |
| Pre-publication audit (Codex) | 2 privacy blockers and 1 rule difference, fixed; wording corrected; re-check pending |
| User iPad A/B | Feature by feature, recorded per batch |
| Windows build | Automated play pass at 1920×1080 |
| Android build | Installed and run on a low-tier Android phone (60 fps in battle) |
