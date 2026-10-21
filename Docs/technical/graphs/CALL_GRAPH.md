# Call graph

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../start/disclaimer.md).*

- `callgraph_all.jsonl`: **56541 call edges**, one `[caller, call site, callee owner, kind, target]` per line. `target` is the address the call goes to; `callee owner` is the entry of the function whose chunk holds that address (they differ in 1552 edges, for example a call into a tail chunk). Kind `d` (55873): the instruction's encoded operand is the target. Kind `r` (28): IDA resolved the target another way (thunk or indirect); treat `r` edges as IDA-inferred. Kind `u` (640): the call sits in code IDA placed in no function, reached from `caller` (see `unowned_blocks.jsonl`); that is reachability, not ownership.
- `unowned_blocks.jsonl`: per function, the code blocks reached from it that belong to no IDA function (ranges, byte hashes; instructions for core functions), and gaps where the target is not disassembled code.
- `callgraph_core_edges.json`: the 2422 edges among the 416 core functions.

Limits: calls through virtual tables and function pointers are **not** edges. The game dispatches scene, player, dinosaur and UI handlers that way (see [classes](../classes/CLASSES.md)), so a function with no callers here can still run. A missing edge is not proof of absence.

Most-called functions (edges):

- `0x1E9BB0` std::string::~string: 7260
- `0x1EA2D0` _objc_msgSend: 4886
- `0x1E9B8C` std::string::string: 3345
- `0x1E9E20` ___mulsf3vfp: 2947
- `0x1E9A60` __Unwind_SjLj_Resume: 2065
- `0x1E9C70` ___addsf3vfp: 2029
- `0x174DE` CreateFrame: 1051
- `0x1E9A6C` __Unwind_SjLj_Unregister: 946
- `0x1E7EE8` ___restore_vfp_d8_d15_regs: 946
- `0x1E7EE0` ___save_vfp_d8_d15_regs: 944
- `0x1E9A54` __Unwind_SjLj_Register: 944
- `0x1E9D6C` ___floatsisfvfp: 801
- `0x1E9E50` ___subsf3vfp: 596
- `0x17526A` Triniti2D::Scene::GetUIManager: 563
- `0x16BFC8` Triniti2D::GameObject::Sprite: 475
