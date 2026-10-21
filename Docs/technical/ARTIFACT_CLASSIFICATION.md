# Artifact classification

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../start/disclaimer.md).*

| Artifact | Class | Note |
|---|---|---|
| `Assets/Resources/DC2/**` (converted game art, audio, data) | ORIGINAL-DERIVED | Needed for a clean checkout to build. Rights remain with the original rights holders. |
| `Assets/Icons/AppIcon512.jpg` | ORIGINAL (the IPA's `iTunesArtwork`) | The app icon of the builds. |
| `Assets/Scripts/**`, `Assets/Editor/**` | PORT CODE | Written for this project; the comments cite native addresses. |
| `Docs/technical/functions/**`, `classes/**`, `data/**`, `graphs/**` | ORIGINAL-DERIVED | Instruction excerpts, names and strings come from the original binary. They are quoted to substantiate the analysis. |
| `Docs/technical/registry/**`, `claims.jsonl` | PROJECT RECORDS | Recovered rules and port decisions. |
| `Docs/evidence/figures/**` | PORT CAPTURES containing original art | See the [figure index](maps/FIGURE_INDEX.md). |
| Original IPA, IDA database, analysis copies, working reports, logs | PRIVATE, not in this repository | Referenced by hash or by name only. |

The original binary's strings (`data/strings.jsonl`) include build paths of the original developer's machine (`/Users/apple/...`, e.g. in assert messages). They are part of the shipped game, quoted as found; they do not identify anyone involved in this project.
