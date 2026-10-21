# selectScene::startMoveUILeft

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-101FC2` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x101FC2` |
| RVA (EA - 0x1000) | `0x100FC2` |
| File offset | `0x100FC2` |
| Size | 198 code bytes in 1 chunk(s), 90 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene15startMoveUILeftEPNS_10UIMoveCtrlEN9Triniti2D4RectENS3_7Color32E` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::startMoveUILeft(DinoCap::UIMoveCtrl *,Triniti2D::Rect,Triniti2D::Color32)` |
| Bytes SHA-256 | `ac56d5dcf413c5dd971a52363d13722574a33f1f3d9d41b87a5b514ce0368155` |
| Direct code callers | 3 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UI_FLOW / PORTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: selectScene::startMoveUILeft (showMapUI / showUpgrade slide), SelectScene.cs / SelectArmory.cs

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: [`selectScene::showMapUI`](../../../functions/dossiers/selectScene/FUN-10218C.md), [`selectScene::showUpgrade`](../../../functions/dossiers/selectScene/FUN-10363E.md), `sub_10479C`

Callees: [`UIMoveCtrl::StartMove`](../../../functions/dossiers/UIMoveCtrl/FUN-0CD466.md), `___addsf3vfp`, `___mulsf3vfp`

## Named data it references

- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x101FC2  82b0       SUB     SP, SP, #8
0x101FC4  f0b5       PUSH    {R4-R7,LR}
0x101FC6  5e46       MOV     R6, R11
0x101FC8  5546       MOV     R5, R10
0x101FCA  4446       MOV     R4, R8
0x101FCC  70b4       PUSH    {R4-R6}
0x101FCE  06af       ADD     R7, SP, #0x18
0x101FD0  92b0       SUB     SP, SP, #0x48
0x101FD2  1b93       STR     R3, [SP,#0x48+varg_r3]
0x101FD4  1d9b       LDR     R3, [SP,#0x48+arg_4]
0x101FD6  1a92       STR     R2, [SP,#0x48+varg_r2]
0x101FD8  0291       STR     R1, [SP,#0x48+var_40]
0x101FDA  9846       MOV     R8, R3
0x101FDC  1b9b       LDR     R3, [SP,#0x48+varg_r3]
0x101FDE  1c9e       LDR     R6, [SP,#0x48+arg_0]
0x101FE0  1a9c       LDR     R4, [SP,#0x48+varg_r2]
0x101FE2  9b46       MOV     R11, R3
0x101FE4  284b       LDR     R3, =(__ZN7DinoCap8g_iphoneE_ptr - 0x101FEA)
0x101FE6  7b44       ADD     R3, PC; __ZN7DinoCap8g_iphoneE_ptr
0x101FE8  1b68       LDR     R3, [R3]; DinoCap::g_iphone ...
0x101FEA  1b78       LDRB    R3, [R3]; DinoCap::g_iphone
0x101FEC  002b       CMP     R3, #0
0x101FEE  01d1       BNE     loc_101FF4
0x101FF0  264b       LDR     R3, =0x40000000
```

