# CompAnimObj::PlayAnimationAdv

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-118F4C` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x118F4C` |
| RVA (EA - 0x1000) | `0x117F4C` |
| File offset | `0x117F4C` |
| Size | 182 code bytes in 1 chunk(s), 83 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11CompAnimObj16PlayAnimationAdvEjfffb` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::CompAnimObj::PlayAnimationAdv(unsigned int,float,float,float,bool)` |
| Bytes SHA-256 | `b5c553d9a1a7be71747971d6f3d1f9a3fc38f0a98f27762814011de3a6fa0fe5` |
| Direct code callers | 10 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/NativeCompObj.cs:62`](../../../../../Assets/Scripts/NativeCompObj.cs#L62) cites `0x118F4C`
- [`Assets/Scripts/NativeCompObj.cs:211`](../../../../../Assets/Scripts/NativeCompObj.cs#L211) cites `0x118F4C`

## Callers and callees (direct code references)

Callers: [`playerNewAniClass::OnAnimationEndAdv`](../../../functions/dossiers/playerNewAniClass/FUN-1261D2.md), `sub_12A1D6`

Callees: [`CompAnimObj::OnUpdateSprite`](../../../functions/dossiers/CompAnimObj/FUN-1189EC.md), `ComponentAnimation::GetFrameSize`, `ComponentAnimation::GetFrameIndex`, `___divsf3vfp`, `___eqsf2vfp`, `___mulsf3vfp`

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x118F4C  f0b5       PUSH    {R4-R7,LR}
0x118F4E  5646       MOV     R6, R10
0x118F50  4546       MOV     R5, R8
0x118F52  60b4       PUSH    {R5,R6}
0x118F54  05af       ADD     R7, SP, #0x14
0x118F56  9846       MOV     R8, R3
0x118F58  089b       LDR     R3, [SP,#arg_4]
0x118F5A  c569       LDR     R5, [R0,#0x1C]
0x118F5C  041c       MOVS    R4, R0
0x118F5E  0026       MOVS    R6, #0
0x118F60  dbb2       UXTB    R3, R3
0x118F62  9a46       MOV     R10, R3
0x118F64  c823       MOVS    R3, #0xC8
0x118F66  c150       STR     R1, [R0,R3]
0x118F68  101c       MOVS    R0, R2
0x118F6A  a96d       LDR     R1, [R5,#0x58]
0x118F6C  d0f058ef   BLX     ___mulsf3vfp
0x118F70  011c       MOVS    R1, R0
0x118F72  2448       LDR     R0, =0x3F800000
0x118F74  d0f0a6ee   BLX     ___divsf3vfp
0x118F78  b823       MOVS    R3, #0xB8
0x118F7A  2349       LDR     R1, =0
0x118F7C  e050       STR     R0, [R4,R3]
0x118F7E  dd23       MOVS    R3, #0xDD
```

