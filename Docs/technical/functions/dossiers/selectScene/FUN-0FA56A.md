# selectScene::startReturnUIRight

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0FA56A` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xFA56A` |
| RVA (EA - 0x1000) | `0xF956A` |
| File offset | `0xF956A` |
| Size | 194 code bytes in 1 chunk(s), 88 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene18startReturnUIRightEPNS_10UIMoveCtrlEN9Triniti2D4RectE` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::startReturnUIRight(DinoCap::UIMoveCtrl *,Triniti2D::Rect)` |
| Bytes SHA-256 | `8bd24b50344a47b2ff65a2fba34a94269aa2de5d968c98903955297035809652` |
| Direct code callers | 6 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UI_FLOW / PORTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: selectScene::startReturnUIRight (removeMissionInfo slide), SelectMissionInfo.cs

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: [`selectScene::removeMissionInfo`](../../../functions/dossiers/selectScene/FUN-0FA64C.md)

Callees: [`UIMoveCtrl::StartMove`](../../../functions/dossiers/UIMoveCtrl/FUN-0CD466.md), `___addsf3vfp`, `___mulsf3vfp`

## Named data it references

- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xFA56A   82b0       SUB     SP, SP, #8
0xFA56C   f0b5       PUSH    {R4-R7,LR}
0xFA56E   5e46       MOV     R6, R11
0xFA570   5546       MOV     R5, R10
0xFA572   4446       MOV     R4, R8
0xFA574   70b4       PUSH    {R4-R6}
0xFA576   06af       ADD     R7, SP, #0x18
0xFA578   92b0       SUB     SP, SP, #0x48
0xFA57A   1b93       STR     R3, [SP,#0x48+varg_r3]
0xFA57C   1d9b       LDR     R3, [SP,#0x48+arg_4]
0xFA57E   1a92       STR     R2, [SP,#0x48+varg_r2]
0xFA580   0291       STR     R1, [SP,#0x48+var_40]
0xFA582   9846       MOV     R8, R3
0xFA584   1b9b       LDR     R3, [SP,#0x48+varg_r3]
0xFA586   1c9e       LDR     R6, [SP,#0x48+arg_0]
0xFA588   1a9c       LDR     R4, [SP,#0x48+varg_r2]
0xFA58A   9b46       MOV     R11, R3
0xFA58C   274b       LDR     R3, =(__ZN7DinoCap8g_iphoneE_ptr - 0xFA592)
0xFA58E   7b44       ADD     R3, PC; __ZN7DinoCap8g_iphoneE_ptr
0xFA590   1b68       LDR     R3, [R3]; DinoCap::g_iphone ...
0xFA592   1b78       LDRB    R3, [R3]; DinoCap::g_iphone
0xFA594   002b       CMP     R3, #0
0xFA596   01d1       BNE     loc_FA59C
0xFA598   254b       LDR     R3, =0x40000000
```

