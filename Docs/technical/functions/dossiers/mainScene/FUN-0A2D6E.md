# mainScene::startMoveUIRight

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0A2D6E` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xA2D6E` |
| RVA (EA - 0x1000) | `0xA1D6E` |
| File offset | `0xA1D6E` |
| Size | 194 code bytes in 1 chunk(s), 88 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9mainScene16startMoveUIRightEPNS_15UILabelMoveCtrlEN9Triniti2D4RectE` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::mainScene::startMoveUIRight(DinoCap::UILabelMoveCtrl *,Triniti2D::Rect)` |
| Bytes SHA-256 | `1b56acbc3bb5ac1a06afb7f9a7a3a574f8a472b0a9fe616834fb7fcd6fee00d7` |
| Direct code callers | 6 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | GAMEPLAY / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: r118 0xa4042: identical to lifeModeScene::startMoveUIRight 0xe4356 (0.2/0.05); ported (MoveRight)

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: `sub_A41CC`, `sub_A46E2`, `sub_A5254`

Callees: [`UILabelMoveCtrl::StartMove`](../../../functions/dossiers/UILabelMoveCtrl/FUN-0CD4B2.md), `___addsf3vfp`, `___mulsf3vfp`

## Named data it references

- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xA2D6E   82b0       SUB     SP, SP, #8
0xA2D70   f0b5       PUSH    {R4-R7,LR}
0xA2D72   5e46       MOV     R6, R11
0xA2D74   5546       MOV     R5, R10
0xA2D76   4446       MOV     R4, R8
0xA2D78   70b4       PUSH    {R4-R6}
0xA2D7A   06af       ADD     R7, SP, #0x18
0xA2D7C   92b0       SUB     SP, SP, #0x48
0xA2D7E   1b93       STR     R3, [SP,#0x48+varg_r3]
0xA2D80   1d9b       LDR     R3, [SP,#0x48+arg_4]
0xA2D82   1a92       STR     R2, [SP,#0x48+varg_r2]
0xA2D84   0291       STR     R1, [SP,#0x48+var_40]
0xA2D86   9846       MOV     R8, R3
0xA2D88   1b9b       LDR     R3, [SP,#0x48+varg_r3]
0xA2D8A   1c9e       LDR     R6, [SP,#0x48+arg_0]
0xA2D8C   1a9c       LDR     R4, [SP,#0x48+varg_r2]
0xA2D8E   9b46       MOV     R11, R3
0xA2D90   274b       LDR     R3, =(__ZN7DinoCap8g_iphoneE_ptr - 0xA2D96)
0xA2D92   7b44       ADD     R3, PC; __ZN7DinoCap8g_iphoneE_ptr
0xA2D94   1b68       LDR     R3, [R3]; DinoCap::g_iphone ...
0xA2D96   1b78       LDRB    R3, [R3]; DinoCap::g_iphone
0xA2D98   002b       CMP     R3, #0
0xA2D9A   01d1       BNE     loc_A2DA0
0xA2D9C   254b       LDR     R3, =0x40000000
```

