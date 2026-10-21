# selectScene::startMoveUIRight

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0FCEF6` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xFCEF6` |
| RVA (EA - 0x1000) | `0xFBEF6` |
| File offset | `0xFBEF6` |
| Size | 198 code bytes in 1 chunk(s), 90 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene16startMoveUIRightEPNS_10UIMoveCtrlEN9Triniti2D4RectENS3_7Color32E` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::startMoveUIRight(DinoCap::UIMoveCtrl *,Triniti2D::Rect,Triniti2D::Color32)` |
| Bytes SHA-256 | `1fce99396d6c7ad020b99a5ef07bc9ad46773425020896a994e3658a2a59e440` |
| Direct code callers | 4 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: [`selectScene::OnSelectChange`](../../../functions/dossiers/selectScene/FUN-100B1E.md), [`selectScene::showMapUI`](../../../functions/dossiers/selectScene/FUN-10218C.md), [`selectScene::showUpgrade`](../../../functions/dossiers/selectScene/FUN-10363E.md)

Callees: [`UIMoveCtrl::StartMove`](../../../functions/dossiers/UIMoveCtrl/FUN-0CD466.md), `___addsf3vfp`, `___mulsf3vfp`

## Named data it references

- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xFCEF6   82b0       SUB     SP, SP, #8
0xFCEF8   f0b5       PUSH    {R4-R7,LR}
0xFCEFA   5e46       MOV     R6, R11
0xFCEFC   5546       MOV     R5, R10
0xFCEFE   4446       MOV     R4, R8
0xFCF00   70b4       PUSH    {R4-R6}
0xFCF02   06af       ADD     R7, SP, #0x18
0xFCF04   92b0       SUB     SP, SP, #0x48
0xFCF06   1b93       STR     R3, [SP,#0x48+varg_r3]
0xFCF08   1d9b       LDR     R3, [SP,#0x48+arg_4]
0xFCF0A   1a92       STR     R2, [SP,#0x48+varg_r2]
0xFCF0C   0291       STR     R1, [SP,#0x48+var_40]
0xFCF0E   9846       MOV     R8, R3
0xFCF10   1b9b       LDR     R3, [SP,#0x48+varg_r3]
0xFCF12   1c9e       LDR     R6, [SP,#0x48+arg_0]
0xFCF14   1a9c       LDR     R4, [SP,#0x48+varg_r2]
0xFCF16   9b46       MOV     R11, R3
0xFCF18   284b       LDR     R3, =(__ZN7DinoCap8g_iphoneE_ptr - 0xFCF1E)
0xFCF1A   7b44       ADD     R3, PC; __ZN7DinoCap8g_iphoneE_ptr
0xFCF1C   1b68       LDR     R3, [R3]; DinoCap::g_iphone ...
0xFCF1E   1b78       LDRB    R3, [R3]; DinoCap::g_iphone
0xFCF20   002b       CMP     R3, #0
0xFCF22   01d1       BNE     loc_FCF28
0xFCF24   264b       LDR     R3, =0x40000000
```

