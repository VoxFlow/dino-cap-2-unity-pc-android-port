# Our findings: how this game was taken apart

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](start/disclaimer.md).*

> **In simple terms.** We read the original iPad program, rebuilt the mechanics from what its code does, and checked the rebuild against it, wrong turns included. The few values not recovered yet are labelled stand-ins with a removal condition ([open questions](technical/OPEN_QUESTIONS.md)).

## What the original is

Dino Cap 2 for iOS (1.3) is a single 32-bit ARM executable, mostly Thumb code, with its C++ symbols still in it, plus one data archive (`DinoCapData.dat`).
- The game code (`DinoCap::`) runs on Triniti's small 2D engine (`Triniti2D::`): scenes, game objects with callbacks, a UI manager, OpenAL audio, and Box2D for movement.
- Because the symbols survived, functions have their real names (`dinosaurClass::BeAttacked`, `selectScene::UpdateExp`, ...). That made a faithful rebuild possible without guessing what a routine is.

## How we worked

1. **Map everything.** All 8,503 functions were exported. The 1,229 game functions were classified one by one; 418 are in scope ([coverage](technical/binary/COVERAGE.md)).
2. **Read instructions, not impressions.** The pseudocode is a guide; the ARM instructions are the truth. Constants are decoded with tools: a misread `0.02` that was really `0.05` had once spread to eight places.
3. **Record rules with evidence.** 203 rules in the [registry](technical/registry/README.md), each with a status, evidence and its successor when it turned out wrong.
4. **Rebuild exactly that.** No tuned constants, no "looks right" offsets: any change made only to match a video is a workaround, and needs the user's explicit approval and a label.
5. **Test against the original.** 46 verifier programs assert recovered values. The user compared features on a real iPad and reported every difference, and each report was traced back to the code.
6. **Review independently.** Codex audited the work twice and found defects that tests had missed. They were fixed, re-checked, and recorded.

## What we got wrong, and how we found out

The [corrected claims](history/corrected-claims.md) page lists them. The pattern that recurred most: reading a field or a number by its *assumed* meaning instead of checking how the original's code *uses* it. Examples include the animation field misread as a loop bound, the wrong HUD control id, and two misread literals in the burn death. Several "bugs" the user reported also turned out to be original behaviour; those are now [documented quirks](fidelity/shipped-quirks-and-divergences.md).

## Where to go next

- [How it works](how-it-works/01-overview.md), chapter by chapter.
- [The technical record](technical/README.md): every core function with its address, instructions, callers and the Unity code that cites it.
- [Testing and validation](quality/testing-and-validation.md): what the checks show, and what they cannot.
