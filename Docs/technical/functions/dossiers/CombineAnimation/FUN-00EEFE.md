# CombineAnimation::SetToAdv

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-00EEFE` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xEEFE` |
| RVA (EA - 0x1000) | `0xDEFE` |
| File offset | `0xDEFE` |
| Size | 250 code bytes in 1 chunk(s), 115 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap16CombineAnimation8SetToAdvEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::CombineAnimation::SetToAdv(void)` |
| Bytes SHA-256 | `cfb40a8b07e0996f1493615fa10197ea902f8c9f58b3d00a8b56129757cf3c0c` |
| Direct code callers | 14 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameCombat.cs:240`](../../../../../Assets/Scripts/GameCombat.cs#L240) cites `0xEEFE`

## Callers and callees (direct code references)

Callers: [`loadDinoANI`](../../../functions/dossiers/DinoCap/FUN-017818.md), `GenComAnim`

Callees: `CAFrame::~CAFrame`, [`CAFrame::Clone`](../../../functions/dossiers/CAFrame/FUN-00C2DA.md), `operator delete`, `___addsf3vfp`, `___gtsf2vfp`, `___ltsf2vfp`, `___subsf3vfp`

## Named data it references

- `0x22A120` __ZNKSt6vectorIPN7DinoCap7CAFrameESaIS2_EE4sizeEv_ptr
- `0x22A154` __ZNKSt6vectorIPN7DinoCap8CASpriteESaIS2_EE4sizeEv_ptr
- `0x22A12C` __ZNSt8_Rb_treeISsSt4pairIKSsPN7DinoCap8CASpriteEESt10_Select1stIS5_ESt4lessISsESaIS5_EE4findERS1__ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xEEFE    f0b5       PUSH    {R4-R7,LR}
0xEF00    5e46       MOV     R6, R11
0xEF02    5546       MOV     R5, R10
0xEF04    4446       MOV     R4, R8
0xEF06    70b4       PUSH    {R4-R6}
0xEF08    06af       ADD     R7, SP, #0x18
0xEF0A    86b0       SUB     SP, SP, #0x18
0xEF0C    0190       STR     R0, [SP,#0x18+var_14]
0xEF0E    0068       LDR     R0, [R0]
0xEF10    0068       LDR     R0, [R0]; this
0xEF12    fdf7e2f9   BL      __ZN7DinoCap7CAFrame5CloneEv; DinoCap::CAFrame::Clone(void)
0xEF16    384b       LDR     R3, =(__ZNKSt6vectorIPN7DinoCap7CAFrameESaIS2_EE4sizeEv_ptr - 0xEF20)
0xEF18    0021       MOVS    R1, #0
0xEF1A    0291       STR     R1, [SP,#0x18+var_10]
0xEF1C    7b44       ADD     R3, PC; __ZNKSt6vectorIPN7DinoCap7CAFrameESaIS2_EE4sizeEv_ptr
0xEF1E    1b68       LDR     R3, [R3]
0xEF20    0093       STR     R3, [SP,#0x18+var_18]
0xEF22    8046       MOV     R8, R0
0xEF24    53e0       B       loc_EFCE
0xEF26    019b       LDR     R3, [SP,#0x18+var_14]
0xEF28    0299       LDR     R1, [SP,#0x18+var_10]
0xEF2A    0026       MOVS    R6, #0
0xEF2C    1a68       LDR     R2, [R3]
0xEF2E    8b00       LSLS    R3, R1, #2
```

