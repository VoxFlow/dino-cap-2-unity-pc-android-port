# Dino Cap 2 in one page

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../start/disclaimer.md).*

- **Binary:** thin Mach-O, 32-bit ARM (header subtype 6), mostly Thumb, imagebase `0x1000`, with C++ symbols ([identity](../../technical/binary/ADDRESS_CONVENTIONS.md)).
- **Data:** `DinoCapData.dat` (ZIP): `.conf` atlases, `.a2b` / `.an2` / `.acb` animations, `.wav` audio, XML and `.prop` tables.
- **Engine:** `Triniti2D`: scenes with callbacks, `GameObject`s with physics (Box2D without shapes or gravity; persistent forces; bodies sleep after 0.5 s), a UI manager with id-addressed controls in 16 layers, OpenAL audio with one session per sound and no gain control, and a 60 Hz `NSTimer` host with the time step capped at 0.1 s.
- **Game code:** `menuScene`, `selectScene`, `mainScene` (run and egg) and `lifeModeScene` (time, kill, arena, boss); `playerClass` and `playerNewAniClass` (the Girl); `dinosaurClass`; `bossN`; `CAObjCallBack` (composite animation).
- **Units:** logical 1024×768; `S = 2` on iPad; `g_ground_y = -110`.
- **Gotchas:**
  - Animations advance one frame per update, so the game is frame-rate dependent.
  - Two copies of some code differ (`mainScene` vs `lifeModeScene` level-up).
  - The random range excludes the top value except in a binary32 rounding case.
- **Port:** this repository; [how it works](../../how-it-works/01-overview.md).
