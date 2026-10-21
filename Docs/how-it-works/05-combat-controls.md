# 5. Combat: controls, weapons and the hit rules

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../start/disclaimer.md).*

> **In simple terms.** Buttons, weapons and damage follow the original's code: who gets hit, how hard, how far, and how the dinosaur reacts.

- **Touch.** `lifeModeScene::OnTouchEvent` ([dossier](../technical/functions/dossiers/lifeModeScene/FUN-0D15E4.md)) keeps one finger id per control. Sliding a finger from one arrow to the other takes it over; the weapon and change buttons act on release inside their release area; pause works only while a mission is being played.
- **Guns.** `playerClass::Attack` ([dossier](../technical/functions/dossiers/playerClass/FUN-0BF784.md)) picks the nearest target by its **head** position. Damage, including shotgun falloff, is measured between the dinosaur's and the hero's **origins** in `dinosaurClass::BeAttacked` ([dossier](../technical/functions/dossiers/dinosaurClass/FUN-014918.md)). Group weapons hit every candidate in range.
- **Reactions.** A non-lethal hit selects its reaction by weapon, skin and whether the state is zero. A table recovered from the ARM code drives the port.
- **Beams.** The laser and electric shock hold, loop and release on frame rules from the animation-end handlers. A held laser makes a dinosaur loop frames 2–4 and take a hit on each pass.
- **Melee and roll.**
  - Melee has its own reaction table.
  - The roll is refused only while already rolling, in state 4, or during its 3-second cooldown ([dossier](../technical/functions/dossiers/playerClass/FUN-0BD668.md)).
- **Ammo and switching.** A gun that runs dry switches weapons automatically (`AfterNoBullets`). The ammo icon is HUD control 8 on layer 2.
- **Metal hits** on armoured dinosaurs roll a 50 % chance per shot to play the metal sound.
- **Numbers.** Damage uses binary32 arithmetic like the original, and critical hits roll on the original's random-range formula `lo + (int)(rand × 2⁻³¹ × (hi − lo))`.
