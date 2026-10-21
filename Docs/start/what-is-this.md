# What is this project?

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](disclaimer.md).*

> **In simple terms.** The iPad game Dino Cap 2 rebuilt for Windows and Android by reading what the original program actually does, then checking the rebuild against it.

- **It is** a rebuild of Dino Cap 2 (iOS 1.3) in Unity 6 (6000.3.8f1). Timings, damage, animation frames, spawn rules and menus come from the original ARM code wherever that code has been read.
- **It is not** a full decompilation or the original source code. Of 418 native game functions in scope, 276 have test proof and 142 are traced or ported without their own check (see [coverage](../technical/binary/COVERAGE.md)).
- **It is not** a remaster. Original quirks are kept on purpose ([quirks and divergences](../fidelity/shipped-quirks-and-divergences.md)). Changes for modern screens and controls are listed one by one and can be told apart from original behaviour.
- **What is in this repository:** the complete Unity project. The original IPA, the reverse-engineering database and the working reports stay private.

Start with [our findings](../findings.md) for the story of how it was taken apart, or [how it works](../how-it-works/01-overview.md) for the engineering.
