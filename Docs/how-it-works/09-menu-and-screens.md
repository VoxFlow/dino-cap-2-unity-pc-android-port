# 9. Menus and other screens

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../start/disclaimer.md).*

> **In simple terms.** Title, map, heroes, armory, bank, jigsaw and options are rebuilt from the original's scene code, including their slide-in movements and sounds.

- **Title** (`menuScene`): the logo and "tap to start" pulse, at 288 alpha units per second, from `menuScene::OnUpdate`.
- **Map** (`selectScene`): mission points from `point.xml` / `Map.xml`.
  - Each point has a pulsing ring, the boss point included.
  - The boss map and its attack set are re-rolled on each map visit, constrained by hero level; the boss point position, seed and level persist.
- **Heroes, armory, bank, jigsaw:** each screen's controls, rects, movements (`UIMoveCtrl` start/mid/end timings) and labels come from that screen's code.
- **Options:** music and effects switches with the original's save behaviour.
- **Mission Complete and Failed:** the complete band (control 20), the failed background (control 11) and the fail line (control 17), with the original timings. A boss mission waits for the boss's move-out clip, then 1 s, then 4 s before the complete screen.
- **Movement easing** of UI elements uses the original's `UIMoveCtrl` data (start, mid and end rects, colours, two durations).
