# 10. Mission types

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../start/disclaimer.md).*

> **In simple terms.** Every mission type in the shipped map file works as on the iPad; the wide-view adjustments are listed per type.

| Type (Map.xml) | Scene | Goal | Notes |
|---|---|---|---|
| 1 run (endurance) | `mainScene` | reach the end of the stage | The run end writes the hero state 4 every frame (no roll after the finish). In wide view the hero walks out past the wide edge (AD-006). |
| 2 time | `lifeModeScene` | time-based mission (goal text from `Map.xml`) | Time label and digits from the original's rects and update code. |
| 3 kill | `lifeModeScene` | kill N dinosaurs | Kill counter label from the original's rect. |
| 5 egg robbery | `mainScene` | carry eggs from the nest to the truck | Delivery line at `S × 150` from the left bound; truck drawn over the hero (layer 12 custom draw). In wide view the camera stops early so the truck and nest sit on the screen edges with native walking distances (AD-005). |
| 7 arena | `lifeModeScene` | arena mission (goal text from `Map.xml`) | Closed stage: right bound 840, the camera barely moves. |
| 8 boss | `lifeModeScene` | defeat the boss | Boss lines, lava geysers and the death sequence from `bossN`. In wide view two boss poses stand further out (AD-004). |

Types 4 (special kill) and 6 (time run) exist in the code but not in the shipped `Map.xml`.
