# selectScene::startJigsaw

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0F6F00` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xF6F00` |
| RVA (EA - 0x1000) | `0xF5F00` |
| File offset | `0xF5F00` |
| Size | 162 code bytes in 1 chunk(s), 77 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene11startJigsawEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::startJigsaw(void)` |
| Bytes SHA-256 | `5e8027aed440896a237c84a4a44791beffd5b0fed8cef1b9576692d56e248518` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/SelectJigsaw.cs:109`](../../../../../Assets/Scripts/SelectJigsaw.cs#L109) cites `0xF6F00`

## Callers and callees (direct code references)

Callers: [`selectScene::OnTimeout`](../../../functions/dossiers/selectScene/FUN-0FA3D2.md)

Callees: [`RectScale`](../../../functions/dossiers/DinoCap/FUN-017068.md), [`RectOffset`](../../../functions/dossiers/DinoCap/FUN-0170B6.md), [`selectScene::startMoveUIRightOut`](../../../functions/dossiers/selectScene/FUN-0F6E24.md), `___addsf3vfp`

## Named data it references

- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xF6F00   f0b5       PUSH    {R4-R7,LR}
0xF6F02   4646       MOV     R6, R8
0xF6F04   40b4       PUSH    {R6}
0xF6F06   04af       ADD     R7, SP, #0x10
0xF6F08   8ab0       SUB     SP, SP, #0x28
0xF6F0A   264b       LDR     R3, =(__ZN7DinoCap8g_iphoneE_ptr - 0xF6F12)
0xF6F0C   8046       MOV     R8, R0
0xF6F0E   7b44       ADD     R3, PC; __ZN7DinoCap8g_iphoneE_ptr
0xF6F10   1b68       LDR     R3, [R3]; DinoCap::g_iphone ...
0xF6F12   1b78       LDRB    R3, [R3]; DinoCap::g_iphone
0xF6F14   002b       CMP     R3, #0
0xF6F16   01d1       BNE     loc_F6F1C
0xF6F18   234a       LDR     R2, =0x40000000
0xF6F1A   00e0       B       loc_F6F1E
0xF6F1C   234a       LDR     R2, =0x3F800000
0xF6F1E   244b       LDR     R3, =0
0xF6F20   02ac       ADD     R4, SP, #0x28+var_20
0xF6F22   201c       MOVS    R0, R4; this
0xF6F24   0693       STR     R3, [SP,#0x28+var_10]
0xF6F26   234b       LDR     R3, =0x42F80000
0xF6F28   0699       LDR     R1, [SP,#0x28+var_10]; Rect
0xF6F2A   06ad       ADD     R5, SP, #0x28+var_10
0xF6F2C   0793       STR     R3, [SP,#0x28+var_10.bottom]
0xF6F2E   224b       LDR     R3, =0x43F00000
```

