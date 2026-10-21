# Recovering animation and sprite formats

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../start/disclaimer.md).*

- **Recover the format and its runtime meaning before converting anything.** Find the loader in the binary, then the consumer that uses each field.
- **Keep a neutral representation, and prove a byte-exact round trip** before writing engine importers.
- **Don't hand-slice atlases or re-author animations** in the engine's animator. Play the original data with a small player that follows the original's clock rules (for Dino Cap 2: one frame per update, the remainder dropped, clamped at the end bound).
- **Sister games are leads, not facts.** Dino Cap 1's animation grammar parsed some Dino Cap 2 files, and each format was still verified on Dino Cap 2's own loader.
- **Record each file's identity:** original or derived, its source path in the original archive, and its hash ([asset identity map](../../technical/maps/ASSET_IDENTITY_MAP.md)).
