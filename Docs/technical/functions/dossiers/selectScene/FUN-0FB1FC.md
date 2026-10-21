# selectScene::startMoveUIUp

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0FB1FC` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xFB1FC` |
| RVA (EA - 0x1000) | `0xFA1FC` |
| File offset | `0xFA1FC` |
| Size | 196 code bytes in 1 chunk(s), 89 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene13startMoveUIUpEPNS_10UIMoveCtrlEN9Triniti2D4RectE` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::startMoveUIUp(DinoCap::UIMoveCtrl *,Triniti2D::Rect)` |
| Bytes SHA-256 | `10bc795d0c3dcd8c58fdc1b75ccd3278def158711d3d41c6d315db02ea23aec9` |
| Direct code callers | 10 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/SelectScene.cs:61`](../../../../../Assets/Scripts/SelectScene.cs#L61) cites `0xFB1FC`

## Callers and callees (direct code references)

Callers: [`selectScene::OnOptionLeave`](../../../functions/dossiers/selectScene/FUN-0FB2E0.md)

Callees: [`UIMoveCtrl::StartMove`](../../../functions/dossiers/UIMoveCtrl/FUN-0CD466.md), `___addsf3vfp`, `___mulsf3vfp`

## Named data it references

- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xFB1FC   82b0       SUB     SP, SP, #8
0xFB1FE   f0b5       PUSH    {R4-R7,LR}
0xFB200   5e46       MOV     R6, R11
0xFB202   5546       MOV     R5, R10
0xFB204   4446       MOV     R4, R8
0xFB206   70b4       PUSH    {R4-R6}
0xFB208   06af       ADD     R7, SP, #0x18
0xFB20A   92b0       SUB     SP, SP, #0x48
0xFB20C   1b93       STR     R3, [SP,#0x48+varg_r3]
0xFB20E   1d9b       LDR     R3, [SP,#0x48+arg_4]
0xFB210   1a92       STR     R2, [SP,#0x48+varg_r2]
0xFB212   0291       STR     R1, [SP,#0x48+var_40]
0xFB214   9846       MOV     R8, R3
0xFB216   1b9b       LDR     R3, [SP,#0x48+varg_r3]
0xFB218   1c9e       LDR     R6, [SP,#0x48+arg_0]
0xFB21A   1a9c       LDR     R4, [SP,#0x48+varg_r2]
0xFB21C   9b46       MOV     R11, R3
0xFB21E   284b       LDR     R3, =(__ZN7DinoCap8g_iphoneE_ptr - 0xFB224)
0xFB220   7b44       ADD     R3, PC; __ZN7DinoCap8g_iphoneE_ptr
0xFB222   1b68       LDR     R3, [R3]; DinoCap::g_iphone ...
0xFB224   1b78       LDRB    R3, [R3]; DinoCap::g_iphone
0xFB226   002b       CMP     R3, #0
0xFB228   01d1       BNE     loc_FB22E
0xFB22A   264b       LDR     R3, =0x40000000
```

