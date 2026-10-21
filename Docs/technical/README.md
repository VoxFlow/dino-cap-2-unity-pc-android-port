# Technical index

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../start/disclaimer.md).*

This folder holds the code-level evidence for the findings:
- which binary was analysed;
- where every function is and what its instructions are;
- which recovered rules cite it;
- where the port's code cites it.

This is a partial decompilation and port, not a full source recovery; see [COVERAGE](binary/COVERAGE.md) for the counts.

| Folder | What is in it |
|---|---|
| [`binary/`](binary/ADDRESS_CONVENTIONS.md) | Identity of the analysed binary (`binaries.jsonl`), its segment table, address conventions (EA, RVA, file offset, Thumb), and [coverage](binary/COVERAGE.md) |
| [`functions/`](functions/ADDRESS_INDEX.md) | The [address index](functions/ADDRESS_INDEX.md) of the 416 core functions, [functions by class](functions/CLASSES_INDEX.md), all 8,503 function records (`functions.jsonl`), and one dossier per core function (12 of them deep) |
| [`classes/`](classes/CLASSES.md) | C++ classes and their virtual tables, with slot targets |
| [`data/`](data/GLOBALS.md) | [Named globals](data/GLOBALS.md) and [notable strings](data/NOTABLE_STRINGS.md) referenced by core functions; all strings (`strings.jsonl`) |
| [`graphs/`](graphs/CALL_GRAPH.md) | The call graph (56,541 edges, with each call's target and the function that owns it), the core-function subgraph, and code reached outside every IDA function |
| [`maps/`](maps/SOURCE_TO_UNITY_MAP.md) | [Original to Unity](maps/SOURCE_TO_UNITY_MAP.md), [asset identity](maps/ASSET_IDENTITY_MAP.md), [figures](maps/FIGURE_INDEX.md) |
| [`registry/`](registry/README.md) | Published copy of the recovered-rules registry, the list of non-original pieces, and the disarmed guesses |
| [`tools/`](tools/README.md) | The scripts that produced this data |
| [`ARTIFACT_CLASSIFICATION.md`](ARTIFACT_CLASSIFICATION.md) | The publication class of every artifact here |
| [`claims.jsonl`](claims.jsonl), [OPEN_QUESTIONS](OPEN_QUESTIONS.md) | One record per recovered rule, and what is still not known |

**How to read a dossier.**
- Instruction listings are exact bytes from the analysed binary in IDA 9.4's rendering.
- The "what it does" text comes from the registry rules that cite the function, or, for the 12 deep dossiers, from a curated note naming the batch that established it.
- Unity links are a mechanical scan of code comments. A comment that cites an address is a reference, not proof that the code implements the whole function.

**Names.** The binary keeps its C++ symbols, so most names are real (`DinoCap::dinosaurClass::DieShaoSi`). Names like `sub_12345` are IDA's automatic names for code without a symbol.
