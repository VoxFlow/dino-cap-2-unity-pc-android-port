# Corrected claims: what was believed, what disproved it, and what is true now

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../start/disclaimer.md).*

Wrong conclusions stay in the record, marked and linked to their successors. Selected cases:

| Topic | Believed | Disproved by | True now | Batch |
|---|---|---|---|---|
| Laser on dinosaurs | A held laser froze a dinosaur after one hit | User report; the ARM of [`dinosaurClass::OnAnimationEnd`](../technical/functions/dossiers/dinosaurClass/FUN-011AD6.md): state 4 tests the *current frame* (+68) == 4 and loops frames 2–4 | The port loops 2–4 and hits on every pass of frame 3 | r119k |
| Electric-shock release | Released from the current frame | A sweep of every animation-end handler: that rule belongs to the flame/laser handler; the shock release restarts from frame 6 | Release from frame 6 | r119k (supersedes r119j) |
| Burnt-dinosaur ash | `S×150×0.8`, iPad −72 | User report; ARM literals `0x43200000` = 160 and `0x428C0000` = 70 in [`DieShaoSi`](../technical/functions/dossiers/dinosaurClass/FUN-010A4C.md) | 160 and 70 (18 px higher) | r119w |
| Burning-dinosaur big fire (dahuo) | `S×150×0.8`, iPad −72 (the ash fix missed this twin) | User report; the same literals `0x43200000` = 160 and `0x428C0000` = 70 in [`BeDamaged`](../technical/functions/dossiers/dinosaurClass/FUN-011C9C.md) (`0x11E94`..`0x11F20`) | 160 and 70 (18 px higher) | r119z2 |
| Weapon icon "jumping" at Mission Complete | A deleted icon was being re-created | The scene's OnLoad: the icon is control 8 on layer 2; the port used id 20, which is also the Mission Complete band's id | Native ids for all HUD controls | r119m (supersedes r119l) |
| Dinosaur hits in states 4–6 | A guessed legacy reaction (0.25 s, velocity 0) | Review R002 (Claude and Codex): the reaction tail branches only on state 0 vs non-zero | The non-zero row; the state is kept unless the row writes 2 | r119s |
| Shotgun falloff | Measured to the head | `BeAttacked` saves the dinosaur's own position for damage; the head is only for target selection | Origin distance | r119s |
| Level-up threshold | One rule for all missions | `BLT` vs `BLE` in the two copies of the code | Run and egg at ≥, others at > | r119s |
| Jigsaw-5 cash bonus | Clamped to 9,999,999; double precision | No upper compare in either copy; binary32 gives 20 where double gave 19 | Uncapped binary32 | r119s |
| Negative cash on menus | Display-only clamp | Codex audit: `selectScene::UpdateExp` stores the clamp back | Stored back as 0 | r119y |
| Egg wide view | Truck drawn further left, delivery line and start shifted | User: walking distance must stay native | The camera stops early instead; native distances | r119r (supersedes r119g, r119k) |
| Beam mask origin | 512 px off | Probe of the clip: the origin is the camera's left edge | Fixed | r119i (supersedes r119f) |
| Float literal `0x3D4CCCCD` | 0.02 | Decoded: 0.05 | Rule: decode every literal with a tool | pilot r117/r118 |

## Things the user reported that were original behaviour

These are kept, and documented so later sessions do not "fix" them: the roll after Mission Complete, the frequent but not constant metal sound, the stomp boss's shadow after death, the boss attack alternation, the red failed screen after a late death, the quieter menu music, and HUD edge pixels. Each is on the [fidelity page](../fidelity/shipped-quirks-and-divergences.md).

## The user's own corrections

Several user reports were withdrawn after a closer look, for example the truck drawn over the hero, the boss ring on the map, and the boss shadow on the iPad. They are recorded as the user's corrections, not as port defects.
