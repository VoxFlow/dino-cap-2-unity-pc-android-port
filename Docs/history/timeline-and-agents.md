# Timeline: phases of the project and which agent did what

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../start/disclaimer.md).*

The phases are listed in the order they happened, taken from the project's reports and rule files. Calendar dates are left out on purpose.

| # | Phase | Main work | Agents |
|---:|---|---|---|
| 1 | Asset formats (DC2-ASSET-001, -002) | Testing whether Dino Cap 1's animation grammar parses Dino Cap 2's files; the unparsed formats and their consumers | Claude, Codex |
| 2 | Pilot starts (DC2-PILOT-001) | First Unity project; batch-mode run and rendering verified; the user allows labelled stand-ins where the native mechanism is still unknown | Claude |
| 3 | Pilot revisions r1 – r118 | Menus, battle, combat, weapons, bosses, eggs, weather, HUD; the "no unapproved workarounds" rule; parallel test runs and the code map | Claude, Codex (builder and reviewer, sequentially) |
| 4 | Engine review (DC2-ENGINE-001) | Engine layers (host clock, timers, Box2D, rendering, UI, audio, saves); review rounds R1 – R5 confirmed by Codex; PC controls and wide view (user requests) | Claude builder, Codex reviewer |
| 5 | User-reported differences (DC2-BUGS-001, batches r119 – r119y) | Each report traced to native code: fixed, documented as an original quirk, or recorded open | Claude, the user on the iPad |
| 6 | Mobile, performance and builds (DC2-MOBILE-001, DC2-PERF-001) | Touch path, orientation and interruption; Windows and Android builds; a profiling pass; a test on a low-tier Android phone | Claude, the user |
| 7 | Reviews and release preparation (DC2-REVIEW-002, DC2-RELEASE-REVIEW-001, DC2-RELEASE-001) | Whole-game reviews by Claude and Codex; a pre-publication audit by Codex; this documentation | Claude, Codex |

**Roles.** The user set the goals, made every decision about adaptations and publication, and did the device comparisons. Claude did most of the analysis, implementation and writing. Codex worked as an independent builder in some rounds and as reviewer in others, always one agent at a time. Per-agent shares are not measured, so none are claimed.
