# selectScene::startMoveUIRight

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0F6FC8` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xF6FC8` |
| RVA (EA - 0x1000) | `0xF5FC8` |
| File offset | `0xF5FC8` |
| Size | 194 code bytes in 1 chunk(s), 88 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene16startMoveUIRightEPNS_15UILabelMoveCtrlEN9Triniti2D4RectE` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::startMoveUIRight(DinoCap::UILabelMoveCtrl *,Triniti2D::Rect)` |
| Bytes SHA-256 | `3a7a7c762176d396930ed71d8fc97fe905acc4078f52c961017635099d1ec148` |
| Direct code callers | 18 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: [`selectScene::showMissionInfo`](../../../functions/dossiers/selectScene/FUN-0FD0C0.md), [`selectScene::showJigsawUI`](../../../functions/dossiers/selectScene/FUN-0FDF70.md), [`selectScene::showJigsawCount`](../../../functions/dossiers/selectScene/FUN-0FEC34.md), [`selectScene::updateWeaponIcon`](../../../functions/dossiers/selectScene/FUN-0FEF68.md), [`selectScene::OnSelectChange`](../../../functions/dossiers/selectScene/FUN-100B1E.md), [`selectScene::showIAP`](../../../functions/dossiers/selectScene/FUN-101AD2.md), [`selectScene::showUpgrade`](../../../functions/dossiers/selectScene/FUN-10363E.md), [`selectScene::showTavernUI`](../../../functions/dossiers/selectScene/FUN-1056DC.md), [`selectScene::OnLoad`](../../../functions/dossiers/selectScene/FUN-10600A.md)

Callees: [`UILabelMoveCtrl::StartMove`](../../../functions/dossiers/UILabelMoveCtrl/FUN-0CD4B2.md), `___addsf3vfp`, `___mulsf3vfp`

## Named data it references

- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xF6FC8   82b0       SUB     SP, SP, #8
0xF6FCA   f0b5       PUSH    {R4-R7,LR}
0xF6FCC   5e46       MOV     R6, R11
0xF6FCE   5546       MOV     R5, R10
0xF6FD0   4446       MOV     R4, R8
0xF6FD2   70b4       PUSH    {R4-R6}
0xF6FD4   06af       ADD     R7, SP, #0x18
0xF6FD6   92b0       SUB     SP, SP, #0x48
0xF6FD8   1b93       STR     R3, [SP,#0x48+varg_r3]
0xF6FDA   1d9b       LDR     R3, [SP,#0x48+arg_4]
0xF6FDC   1a92       STR     R2, [SP,#0x48+varg_r2]
0xF6FDE   0291       STR     R1, [SP,#0x48+var_40]
0xF6FE0   9846       MOV     R8, R3
0xF6FE2   1b9b       LDR     R3, [SP,#0x48+varg_r3]
0xF6FE4   1c9e       LDR     R6, [SP,#0x48+arg_0]
0xF6FE6   1a9c       LDR     R4, [SP,#0x48+varg_r2]
0xF6FE8   9b46       MOV     R11, R3
0xF6FEA   284b       LDR     R3, =(__ZN7DinoCap8g_iphoneE_ptr - 0xF6FF0)
0xF6FEC   7b44       ADD     R3, PC; __ZN7DinoCap8g_iphoneE_ptr
0xF6FEE   1b68       LDR     R3, [R3]; DinoCap::g_iphone ...
0xF6FF0   1b78       LDRB    R3, [R3]; DinoCap::g_iphone
0xF6FF2   002b       CMP     R3, #0
0xF6FF4   01d1       BNE     loc_F6FFA
0xF6FF6   264b       LDR     R3, =0x40000000
```

