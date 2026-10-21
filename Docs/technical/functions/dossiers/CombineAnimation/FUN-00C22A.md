# CombineAnimation::Clone

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-00C22A` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xC22A` |
| RVA (EA - 0x1000) | `0xB22A` |
| File offset | `0xB22A` |
| Size | 162 code bytes in 1 chunk(s), 76 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap16CombineAnimation5CloneEPKS0_` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::CombineAnimation::Clone(DinoCap::CombineAnimation const*)` |
| Bytes SHA-256 | `5d57d1fb1f71ed3b0a23f6198ac47f95702b6b94e3f12c8745408da634fc1e01` |
| Direct code callers | 328 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/NativeCaAnimation.cs:93`](../../../../../Assets/Scripts/NativeCaAnimation.cs#L93) cites `0xC22A`

## Callers and callees (direct code references)

Callers: `sub_18430`, `GenComAnim`, `sub_69DE6`, [`playerClass::ChangeWeapon`](../../../functions/dossiers/playerClass/FUN-0B9DE8.md), `sub_BA20A`, [`selPlayerClass::ChangeWeapon`](../../../functions/dossiers/selPlayerClass/FUN-10E4F8.md), `sub_10E930`

Callees: `CAFrame::CAFrame`, `CAFrame::~CAFrame`, [`CAFrame::Clone`](../../../functions/dossiers/CAFrame/FUN-00C100.md), `operator delete`, `operator new`

## Named data it references

- `0x22A120` __ZNKSt6vectorIPN7DinoCap7CAFrameESaIS2_EE4sizeEv_ptr
- `0x22A228` __ZNSt6vectorIPN7DinoCap7CAFrameESaIS2_EE9push_backERKS2__ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xC22A    f0b5       PUSH    {R4-R7,LR}
0xC22C    5e46       MOV     R6, R11
0xC22E    5546       MOV     R5, R10
0xC230    4446       MOV     R4, R8
0xC232    70b4       PUSH    {R4-R6}
0xC234    06af       ADD     R7, SP, #0x18
0xC236    81b0       SUB     SP, SP, #4
0xC238    244b       LDR     R3, =(__ZNKSt6vectorIPN7DinoCap7CAFrameESaIS2_EE4sizeEv_ptr - 0xC242)
0xC23A    061c       MOVS    R6, R0
0xC23C    8a46       MOV     R10, R1
0xC23E    7b44       ADD     R3, PC; __ZNKSt6vectorIPN7DinoCap7CAFrameESaIS2_EE4sizeEv_ptr
0xC240    0025       MOVS    R5, #0
0xC242    1b68       LDR     R3, [R3]
0xC244    9846       MOV     R8, R3
0xC246    0be0       B       loc_C260
0xC248    3368       LDR     R3, [R6]
0xC24A    a800       LSLS    R0, R5, #2
0xC24C    c458       LDR     R4, [R0,R3]
0xC24E    002c       CMP     R4, #0
0xC250    05d0       BEQ     loc_C25E
0xC252    201c       MOVS    R0, R4; this
0xC254    fff70bf9   BL      __ZN7DinoCap7CAFrameD2Ev; DinoCap::CAFrame::~CAFrame()
0xC258    201c       MOVS    R0, R4; void *
0xC25A    ddf1f2ec   BLX     __ZdlPv; operator delete(void *)
```

