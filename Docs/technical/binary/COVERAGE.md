# Coverage

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../start/disclaimer.md).*

The numbers come from the data files in this folder (`coverage_stats.json`) and from the project's function ledger. No figure here is an estimate from counting files or strings.

| Measure | Count |
|---|---:|
| Functions in the analysis database (game, engine, libraries) | 8,503 |
| Functions of the game's own C++ code (`DinoCap::`) in the ledger | 1,229 rows |
| In-scope rows (verdict UNSET / GAMEPLAY / UI_FLOW): the **core** | 418 |
| Core rows that are defined functions in the analysis database | 416 (see note 1) |
| Core functions with test proof (ledger VERIFIED or TEST_EXECUTED) | 276 of 418 |
| Core functions traced or ported without a dedicated test | 142 of 418 |
| Core functions untraced | 0 |
| Core functions cited by at least one recovered rule | 100 of 416 |
| Core functions cited at least once by the Unity code | 314 of 416 (102 **unmapped**; see note 2) |
| Call edges (whole binary) / among core functions | 56,541 / 2,422 |
| Functions with more than one code chunk (whole binary) / core | 205 / 6 (see note 3) |
| Core functions that reach code IDA placed in no function | 4 (127 blocks, 1 gaps; see note 3) |
| Virtual tables | 116 |

Out-of-scope rows were classified as data, trivial (constructors, destructors, factories), excluded (networking, ads, analytics, Game Center), dead (no caller and no virtual-table slot), or engine glue. Each row records its reason in the ledger.

**Note 1.** Two ledger rows are symbols that IDA has not defined as functions in the analysis database: `LoadImageHight` at `0x6F89C` (a very large data-loading routine) and `mainScene::OnTimeout` at `0xB6C94`. They have no dossier; the ledger still records their status.

**Note 2.** "Unmapped" means no comment in the Unity code cites the function by address or name. It is not proof that the behaviour is missing: much of the port cites the *caller* or the *rule* instead. A cited function is not proof of a complete port either.

**Note 3.** IDA can split a function into chunks (an entry chunk and tails elsewhere in the code). Every chunk is exported with its range and byte hash, and an address counts as part of a function only if one of its chunks holds it. Code that a function reaches by a branch or fall-through, but that IDA placed in no function, is listed under that function in `graphs/unowned_blocks.jsonl` and in its dossier: that shows reachability, not ownership.

**What "test proof" means.** A verifier program exercised the port of the function while asserting values recovered from the original (a native oracle) or the port's own stated adaptation rule. Executing a function's port is not, by itself, proof that it matches the original in every branch. The pre-publication audit sampled ten of the 142 untested rows; some port mappings in that group still need reconciling (see [open questions](../OPEN_QUESTIONS.md)).
