# How the working rules evolved, and why

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../start/disclaimer.md).*

| Stage | Rule | Why it was added |
|---|---|---|
| From the start | Native evidence first; machine instructions beat the decompiler | Pseudocode misreads had produced wrong mechanics in the earlier Dino Cap 1 work |
| From the start | Evidence labels and confidence levels; superseded claims kept and marked | To stop inferences from silently becoming "facts" |
| From the start | Dino Cap 1 findings are leads only, never Dino Cap 2 facts | The games share an engine but not their numbers |
| Pilot start | Pilot stand-ins allowed only when labelled, with a removal condition | To keep the pilot moving without disguising guesses |
| Pilot start | "Keep working": a checkpoint is not a stopping point | Sessions had ended at checkpoints with open work |
| Pilot revisions | No unapproved workarounds: any value chosen to match a screenshot needs the user's explicit approval | HUD art had been placed at a guessed size because it matched a video |
| Pilot revisions | Quota reserve 5 % (was 15 %) | Stops too early with the higher reserve |
| Pilot revisions | Long test runs in parallel project copies | Sequential full-suite runs took too long |
| Pilot revisions | A code map (graphify) is consulted before searching, as a location aid only | Faster navigation without treating a graph hit as evidence |
| Pilot r118 | Decode every float literal with a tool; quantifier audit (ONLY / NEVER / ALWAYS) before finalizing | Misread constants spread to several places; overconfident absolute claims |
| Pilot r118z | The host layer is in scope | The iOS shell's 60 Hz timer had never been compared with the Unity loop |
| Release preparation | Scan built artifacts and the exact commit set before publishing | A personal path inside the Windows DLL, found by the audit |

The current rule file is short on purpose: detailed techniques and case histories live in these pages and in the knowledge notes, loaded only when needed.
