# selectScene::startReturnUIRight

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0F6D42` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xF6D42` |
| RVA (EA - 0x1000) | `0xF5D42` |
| File offset | `0xF5D42` |
| Size | 194 code bytes in 1 chunk(s), 88 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene18startReturnUIRightEPNS_15UILabelMoveCtrlEN9Triniti2D4RectE` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::startReturnUIRight(DinoCap::UILabelMoveCtrl *,Triniti2D::Rect)` |
| Bytes SHA-256 | `fd82e55ea35453736fc3ddc4e1f7afcdbec6ce72f075074476be4f4f10eeb2aa` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UI_FLOW / PORTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: selectScene::startReturnUIRight (removeMissionInfo slide), SelectMissionInfo.cs

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: [`selectScene::removeMissionInfo`](../../../functions/dossiers/selectScene/FUN-0FA64C.md)

Callees: [`UILabelMoveCtrl::StartMove`](../../../functions/dossiers/UILabelMoveCtrl/FUN-0CD4B2.md), `___addsf3vfp`, `___mulsf3vfp`

## Named data it references

- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xF6D42   82b0       SUB     SP, SP, #8
0xF6D44   f0b5       PUSH    {R4-R7,LR}
0xF6D46   5e46       MOV     R6, R11
0xF6D48   5546       MOV     R5, R10
0xF6D4A   4446       MOV     R4, R8
0xF6D4C   70b4       PUSH    {R4-R6}
0xF6D4E   06af       ADD     R7, SP, #0x18
0xF6D50   92b0       SUB     SP, SP, #0x48
0xF6D52   1b93       STR     R3, [SP,#0x48+varg_r3]
0xF6D54   1d9b       LDR     R3, [SP,#0x48+arg_4]
0xF6D56   1a92       STR     R2, [SP,#0x48+varg_r2]
0xF6D58   0291       STR     R1, [SP,#0x48+var_40]
0xF6D5A   9846       MOV     R8, R3
0xF6D5C   1b9b       LDR     R3, [SP,#0x48+varg_r3]
0xF6D5E   1c9e       LDR     R6, [SP,#0x48+arg_0]
0xF6D60   1a9c       LDR     R4, [SP,#0x48+varg_r2]
0xF6D62   9b46       MOV     R11, R3
0xF6D64   274b       LDR     R3, =(__ZN7DinoCap8g_iphoneE_ptr - 0xF6D6A)
0xF6D66   7b44       ADD     R3, PC; __ZN7DinoCap8g_iphoneE_ptr
0xF6D68   1b68       LDR     R3, [R3]; DinoCap::g_iphone ...
0xF6D6A   1b78       LDRB    R3, [R3]; DinoCap::g_iphone
0xF6D6C   002b       CMP     R3, #0
0xF6D6E   01d1       BNE     loc_F6D74
0xF6D70   254b       LDR     R3, =0x40000000
```

