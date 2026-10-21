# Export tools

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../start/disclaimer.md).*

These scripts produced the data in this folder. They are the reproduction record, not game code. Each file is a byte-identical copy of the working script; `generate_docs.py` re-copies them on every run.

| Script | Produces |
|---|---|
| `export_ida.py` | `functions.jsonl` (with every chunk of each function), `callgraph_all.jsonl`, `unowned_blocks.jsonl`, the core disassembly, classes, strings and per-function references. Symbol names and virtual-table file words are read from the executable itself, not from IDA. It needs IDA Pro with `idalib` (9.4 was used) and the analysis database of the binary in [ADDRESS_CONVENTIONS](../binary/ADDRESS_CONVENTIONS.md). |
| `generate_docs.py` | Every generated page under `Docs/technical` (dossiers, address index, class pages, maps, call-graph and data pages), from the export, the function ledger, the registry and the Unity code. |
| `scrub_dates.py` | Removes calendar dates from the published text (only lines that hold a date are touched; code and raw data copies stay byte-identical). `generate_docs.py` runs it on its output. |
| `ledger_report.py` | The coverage counts (in-scope, tested, traced or ported without test). |
| `check_float_claims.py` | Re-decodes every "integer bits = float value" claim in the evidence, the reports and the code with `struct`, so that no constant is trusted from reading by eye. |

**Safety.** `export_ida.py` opens the database with auto-analysis off and closes it without saving. That is a safeguard, not an enforced read-only mode, so keep the database's SHA-256 as the snapshot the data is tied to.

**Environment variables** (no machine paths are built in):
- `DC2_DB`, `DC2_BIN` (the executable), `DC2_OUT`, `DC2_LEDGER` for the export;
- `DC2_EXPORT`, `DC2_DEV`, `DC2_UNITY`, `DC2_BINREC` and `DC2_DAT` (the game data container `DinoCapData.dat`, for the asset identity map) for the generator;
- `DC2_ROOT` (the folder that holds `Dev/` and `DC2-Dev/`) for `ledger_report.py` and `check_float_claims.py`; without it they use the folder two levels above the script.

The function ledger and the working reports the generator reads are private working files; their published results are the files in this folder.
