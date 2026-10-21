# Fidelity: shipped quirks kept, and every place the port differs

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../start/disclaimer.md).*

> **In simple terms.** Odd original behaviour stays. Every intentional difference is listed here, with the switch or condition that restores the original.

## Original quirks that were kept

Each was reported as a "bug", checked in the original's code, and found to be what the iPad game does:

- **Rolling after Mission Complete** still works in every mission except the run mission. The roll gate ([`playerClass::OnRollDown`](../technical/functions/dossiers/playerClass/FUN-0BD668.md)) blocks only states 7 and 4, and only the run end writes state 4.
- **Metal hit sound** on armoured dinosaurs plays on a 50 % roll per shot ([`playerClass::Attack`](../technical/functions/dossiers/playerClass/FUN-0BF784.md)).
- **A hero killed after Mission Complete** gets the red failed screen on top. [`GameOverStart`](../technical/functions/dossiers/mainScene/FUN-0A6F70.md) has no completion guard, and the hit gate only skips invincible or state 4–7 heroes.
- **The stomp boss's shadow** stays for a second after its death (the finished clip keeps drawing its last frame).
- **Three-attack boss lines** nearly always alternate between the first two attacks. The random-range formula excludes the top value except in a rare binary32 rounding case.
- **Level-up comparison differs by scene:** run and egg level up at the threshold, the other missions above it.
- **Menu music is quieter** than most battle music because of the original files.
- **Edge pixels** on some HUD icons at non-integer scales come from the original's bilinear filtering.
- **Casing life** comes from physics (5 bounces, about 1.1 s), not a timer. The user's memory of shorter casings is an [open question](../technical/OPEN_QUESTIONS.md).

## Every place the port differs

Non-original pieces are labelled in the code and listed in [`pilot_assumptions.json`](../technical/registry/pilot_assumptions.json):

| Id | Kind | Difference | Original restored by |
|---|---|---|---|
| AD-001 | Adaptation | Wide view: more world left and right, capped at 21:9; menus keep the native height with a widened background | wide view off, or a 4:3 screen |
| AD-002 | Adaptation | The hero can walk to the visible wide edge on both sides | wide view off |
| AD-003 | Adaptation | Beams are *drawn* across the visible width, clipped to the weapon's native reach; damage eligibility and reach stay native | wide view off |
| AD-004 | Adaptation | Two boss poses stand further out so the art meets the screen edge. Rules measured from the boss keep their values, but relative to the screen its reach and falloff change. Accepted side effect: on very wide screens the tail sweep covers less of the screen | wide view off |
| AD-005 | Adaptation | Egg Robbery: the camera stops early so the truck and nest sit on the screen edges; walking distances stay native | wide view off |
| AD-006 | Adaptation | Endurance finish: the hero walks out past the wide edge | wide view off |
| AD-007 | Adaptation | Wide view widens the hit, spawn, despawn and projectile windows; a target beyond the iPad edge takes edge damage | wide view off |
| PA-009 | Adaptation | The bank grants its cash packs directly (no App Store) | none (no store) |
| PF-001 | Platform | PC keys and mouse press the native buttons; window and fullscreen handling | not used on mobile |
| PF-002 | Platform | Letterbox and pillarbox bars repainted every frame | none needed |
| PF-003 | Platform | Mobile host: 60 Hz, touch only, multi-touch | not applicable |
| PF-004 | Platform | Mobile: notch inset, screen kept awake, Android bars hidden (not yet device-verified) | desktop and 4:3 have no inset |
| PA-013 | Stand-in | Girl invincibility flash start value (unrecovered); 1.0 used | when the value is recovered |
| PA-005, PA-011 | Stand-in (bounded) | Early pilot residue: UI update-order details, hero/girl placement and physics residue | each entry's removal condition |
| DBG-001 | Debug | <kbd>F1</kbd> developer menu | not opened |

Historical entries that turned out wrong (PA-001, PA-002, PA-006, ...) remain in the registry, marked SUPERSEDED, with their successors.
