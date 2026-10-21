# mainScene::EggFull

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0974FA` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x974FA` |
| RVA (EA - 0x1000) | `0x964FA` |
| File offset | `0x964FA` |
| Size | 232 code bytes in 1 chunk(s), 102 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9mainScene7EggFullEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::mainScene::EggFull(void)` |
| Bytes SHA-256 | `8605568cd94c97e0d071b9fbf35636d8069adcd1548f7ce547a6f926a0eadc83` |
| Direct code callers | 2 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameEgg.cs:7`](../../../../../Assets/Scripts/GameEgg.cs#L7) cites `0x974FA`
- [`Assets/Scripts/GameEgg.cs:192`](../../../../../Assets/Scripts/GameEgg.cs#L192) cites `0x974FA`

## Callers and callees (direct code references)

Callers: [`playerClass::UpdateCamera`](../../../functions/dossiers/playerClass/FUN-0BB054.md), [`playerNewAniClass::UpdateCamera`](../../../functions/dossiers/playerNewAniClass/FUN-127342.md)

Callees: [`PlaySound`](../../../functions/dossiers/DinoCap/FUN-01A448.md), [`CompAnimObj::PlayAnimation`](../../../functions/dossiers/CompAnimObj/FUN-119010.md), [`CompAnimObj::SetAnimation`](../../../functions/dossiers/CompAnimObj/FUN-1198AC.md), `ComponentAnimation::UpdateTexture`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`

## Strings it references

- `0x2074CC` "cheMoveAnimation"
- `0x204EB0` "A_car"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x2265C6` GCC_except_table18_1
- `0x22A474` __ZN7DinoCap17g_ca_egg_che_moveE_ptr
- `0x23F5DC` DinoCap::g_ca_egg_che_move

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x974FA   f0b5       PUSH    {R4-R7,LR}
0x974FC   5e46       MOV     R6, R11
0x974FE   5546       MOV     R5, R10
0x97500   4446       MOV     R4, R8
0x97502   70b4       PUSH    {R4-R6}
0x97504   50f1ecec   BLX     ___save_vfp_d8_d15_regs
0x97508   16af       ADD     R7, SP, #arg_38
0x9750A   95b0       SUB     SP, SP, #0x54 ; 'T'
0x9750C   354b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x97516)
0x9750E   6a46       MOV     R2, SP
0x97510   1092       STR     R2, [SP,#0x54+var_14]
0x97512   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x97514   0122       MOVS    R2, #1
0x97516   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x97518   0490       STR     R0, [SP,#0x54+var_44]
0x9751A   06a8       ADD     R0, SP, #0x54+fctx; lpfctx
0x9751C   0c93       STR     R3, [SP,#0x54+fctx.personality]
0x9751E   324b       LDR     R3, =(GCC_except_table18_1 - 0x97526)
0x97520   0e97       STR     R7, [SP,#0x54+fctx.jbuf]
0x97522   7b44       ADD     R3, PC; GCC_except_table18_1
0x97524   0d93       STR     R3, [SP,#0x54+fctx.lsda]
0x97526   314b       LDR     R3, =(loc_97574 - 0x9752C)
0x97528   7b44       ADD     R3, PC; loc_97574
0x9752A   1343       ORRS    R3, R2
```

