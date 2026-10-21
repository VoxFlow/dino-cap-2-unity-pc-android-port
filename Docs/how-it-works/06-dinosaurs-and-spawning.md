# 6. Dinosaurs, bosses and spawning

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../start/disclaimer.md).*

> **In simple terms.** Which dinosaurs appear, how strong they are, how they move, attack, react and die, all from the original's code and tables.

- **Stats** come from `dinosaur.prop` / `lifedinosaur.prop`, scaled by mission level with the original formulas.
- **States.** `dinosaurClass` keeps a state field:
  - 0 move, 1 attack, 2 hit;
  - 3 dying, 4/5/6 beam and burn reactions;
  - 7 jump, 8 spawn.
- **Transitions** happen in `OnAnimationEnd` ([dossier](../technical/functions/dossiers/dinosaurClass/FUN-011AD6.md)) and in the hit handlers.
- **Physics.** The original uses Box2D with no shapes, no gravity and no contacts: bodies only integrate their velocity. Bodies at rest go to sleep after 0.5 s, which is why a resting dinosaur ignores some reactions until something wakes it. The port reproduces the sleep test with its exact literals.
- **Deaths.**
  - Normal death, head-flying death and burn death (`DieShaoSi`, [dossier](../technical/functions/dossiers/dinosaurClass/FUN-010A4C.md)), each with its blood and gore particles.
  - Burnt dinosaurs of skins 1, 2 and 5 leave ash at `S × 160 × 0.8 + ground − 70` on iPad (scale 0.8). Skin 3 only gets scale 1.0 and keeps its position: its branch skips the position write.
- **Bosses** (`bossN`) choose attack lines with the random-range formula. A line with three attacks nearly always alternates between the first two, and the third is possible but rare. A dead boss's last frame keeps its foot shadow on screen for one second.
- **Spawning** follows each mission's timers and enemy list from `Map.xml`. Spawning stops at Mission Complete; dinosaurs already alive stay.
