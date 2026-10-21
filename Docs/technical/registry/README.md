# Source-truth registry (published copy)

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../start/disclaimer.md).*

These are the project's working records of what is known about the original. They are date-sanitized copies made at publication: calendar dates are removed (`tools/scrub_dates.py`), and ids, statuses, supersession links and evidence are unchanged. The working copies continue to evolve.

| File | What it is |
|---|---|
| [`source_contract_registry.json`](source_contract_registry.json) | 203 recovered rules ("contracts"): each has an id, a status (PROVEN, LIKELY, POSSIBLE, UNRESOLVED, FALSIFIED, SUPERSEDED, ...), its provenance (NATIVE, OBSERVED, ...), the proposition and its evidence, plus `supersedes` / `superseded_by` links. Wrong entries stay in the file, marked, so the trail is visible: **read an entry's status, not just its text.** |
| [`pilot_assumptions.json`](pilot_assumptions.json) | Every non-original piece of the port: adaptations approved by the user (AD-*), platform layers (PF-*), debug tools (DBG-*), and the remaining stand-ins for unknown original values (PA-*). Each has its removal condition. |
| [`disarmed_heuristics.json`](disarmed_heuristics.json) | Guessed values that were removed and must never come back as production values or as test answers. |

The machine-readable form used by the dossiers is [`../claims.jsonl`](../claims.jsonl): one record per rule, with the functions whose addresses it cites.
