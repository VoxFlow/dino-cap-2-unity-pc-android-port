# mainScene::UnRollDown

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-094C6C` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x94C6C` |
| RVA (EA - 0x1000) | `0x93C6C` |
| File offset | `0x93C6C` |
| Size | 218 code bytes in 1 chunk(s), 100 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9mainScene10UnRollDownEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::mainScene::UnRollDown(void)` |
| Bytes SHA-256 | `b0e968e834b92f3424e069884ed3635c81506a80fcf00127c56f6bb700623ccc` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameEgg.cs:100`](../../../../../Assets/Scripts/GameEgg.cs#L100) cites `0x94C6C`

## Callers and callees (direct code references)

Callers: [`eggClass::OnUpdate`](../../../functions/dossiers/eggClass/FUN-11FEB6.md)

Callees: [`RectScale`](../../../functions/dossiers/DinoCap/FUN-017068.md), [`RectOffset`](../../../functions/dossiers/DinoCap/FUN-0170B6.md)

## Named data it references

- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x23D170` rotatectrlRect

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x94C6C   f0b5       PUSH    {R4-R7,LR}
0x94C6E   5e46       MOV     R6, R11
0x94C70   5546       MOV     R5, R10
0x94C72   4446       MOV     R4, R8
0x94C74   70b4       PUSH    {R4-R6}
0x94C76   06af       ADD     R7, SP, #0x18
0x94C78   86b0       SUB     SP, SP, #0x18
0x94C7A   334b       LDR     R3, =(__ZN7DinoCap8g_iphoneE_ptr - 0x94C82)
0x94C7C   061c       MOVS    R6, R0
0x94C7E   7b44       ADD     R3, PC; __ZN7DinoCap8g_iphoneE_ptr
0x94C80   1b68       LDR     R3, [R3]; DinoCap::g_iphone ...
0x94C82   1b78       LDRB    R3, [R3]; DinoCap::g_iphone
0x94C84   002b       CMP     R3, #0
0x94C86   02d1       BNE     loc_94C8E
0x94C88   3048       LDR     R0, =0x40000000
0x94C8A   8246       MOV     R10, R0
0x94C8C   01e0       B       loc_94C92
0x94C8E   3049       LDR     R1, =0x3F800000
0x94C90   8a46       MOV     R10, R1
0x94C92   86235b00   MOVS    R3, #0x10C
0x94C96   0022       MOVS    R2, #0
0x94C98   f254       STRB    R2, [R6,R3]
0x94C9A   9c22       MOVS    R2, #0x9C
0x94C9C   5300       LSLS    R3, R2, #1
```

