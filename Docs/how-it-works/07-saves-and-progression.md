# 7. Saves, levels, money and the shop

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../start/disclaimer.md).*

> **In simple terms.** The save file has the original's layout; levels, cash and unlocks follow the original's formulas, including two oddities that are kept.

- **Save file.** `NativeSaveFile.cs` writes and reads the original's `dinocap2hd.sav` layout. The boss map point is saved separately (`dinocap2_boss_point.sav`: key, position, seed, level) and cleared only when a boss mission is completed.
- **Experience.** The threshold is `(L + 1) × 30 × L + base`. Run and Egg missions (the `mainScene` copy of the code) level up when experience **reaches** the threshold. The other missions (the `lifeModeScene` copy) need it to be **exceeded**: one comparison differs between the two copies (`BLT` at `0x985C4` vs `BLE` at `0xD2090`), and the port keeps the difference.
- **Cash.**
  - Mission earnings plus a jigsaw-5 bonus `(int)(earned × count × 0.01)`, computed in binary32, with **no upper cap** on that bonus.
  - Menu cash displays clamp a negative balance to 0 and store it back (`selectScene::UpdateExp`, [dossier](../technical/functions/dossiers/selectScene/FUN-0F45DC.md)).
- **Unlocks.** Weapons unlock by hero level at mission completion. The debug menu's level-up queues the same unlock for the next menu visit (debug only).
- **Shop and bank.** Armory upgrades and ammo use the original prices and caps. The bank's in-app purchases cannot exist outside the App Store, so its cash packs are granted directly (ADAPTATION, PA-009).
