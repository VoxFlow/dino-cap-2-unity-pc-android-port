# jigsawPicClass::StartMove

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-1245EE` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x1245EE` |
| RVA (EA - 0x1000) | `0x1235EE` |
| File offset | `0x1235EE` |
| Size | 90 code bytes in 1 chunk(s), 45 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap14jigsawPicClass9StartMoveENS_14UIMoveCtrlDataES1_f` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::jigsawPicClass::StartMove(DinoCap::UIMoveCtrlData,DinoCap::UIMoveCtrlData,float)` |
| Bytes SHA-256 | `b59687652253c26c97974e291936f2d54c4775cce212a4e07b8653ec06070e47` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UI_FLOW / PORTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: 0x1245ee: stores start/mid/end move keys and durations, +180 = 1; caller selectScene::movePicTOLigsaw 0xf8d3e (SelectJigsaw)

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: [`selectScene::movePicTOLigsaw`](../../../functions/dossiers/selectScene/FUN-0F8D3E.md)

Callees: none.

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x1245EE  83b0       SUB     SP, SP, #0xC
0x1245F0  30b5       PUSH    {R4,R5,LR}
0x1245F2  0391       STR     R1, [SP,#varg_r1]
0x1245F4  0492       STR     R2, [SP,#varg_r2]
0x1245F6  0593       STR     R3, [SP,#varg_r3]
0x1245F8  039a       LDR     R2, [SP,#varg_r1]
0x1245FA  049b       LDR     R3, [SP,#varg_r2]
0x1245FC  011c       MOVS    R1, R0
0x1245FE  8c31       ADDS    R1, #0x8C
0x124600  079c       LDR     R4, [SP,#arg_4]
0x124602  0c9d       LDR     R5, [SP,#arg_18]
0x124604  0a60       STR     R2, [R1]
0x124606  4b60       STR     R3, [R1,#4]
0x124608  089a       LDR     R2, [SP,#arg_8]
0x12460A  099b       LDR     R3, [SP,#arg_C]
0x12460C  0831       ADDS    R1, #8
0x12460E  0a60       STR     R2, [R1]
0x124610  4b60       STR     R3, [R1,#4]
0x124612  059a       LDR     R2, [SP,#varg_r3]
0x124614  069b       LDR     R3, [SP,#arg_0]
0x124616  0831       ADDS    R1, #8
0x124618  0a60       STR     R2, [R1]
0x12461A  4b60       STR     R3, [R1,#4]
0x12461C  0a9a       LDR     R2, [SP,#arg_10]
```

