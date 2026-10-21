# CompAnimObj::SetAdvCompAni

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-117DAE` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x117DAE` |
| RVA (EA - 0x1000) | `0x116DAE` |
| File offset | `0x116DAE` |
| Size | 188 code bytes in 1 chunk(s), 84 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11CompAnimObj13SetAdvCompAniERKSsS2_S2_` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::CompAnimObj::SetAdvCompAni(std::string const&,std::string const&,std::string const&)` |
| Bytes SHA-256 | `d46c1bd4d5277d421b269067cc81453ea205b548d244136cc00ee5651cb5dfa0` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/NativeCompObj.cs:174`](../../../../../Assets/Scripts/NativeCompObj.cs#L174) cites `0x117DAE`

## Callers and callees (direct code references)

Callers: `sub_12A1D6`

Callees: `CALibrary::Anim::UpdateTextureId`, `CALibrary::AnimLib::GetAnim`, `ComponentAnimation::GetComponent`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::~string`

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x2289C4` GCC_except_table35_4
- `0x22A300` __ZStplIcSt11char_traitsIcESaIcEESbIT_T0_T1_ERKS6_S8__ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x117DAE  f0b5       PUSH    {R4-R7,LR}
0x117DB0  5e46       MOV     R6, R11
0x117DB2  5546       MOV     R5, R10
0x117DB4  4446       MOV     R4, R8
0x117DB6  70b4       PUSH    {R4-R6}
0x117DB8  d0f092e8   BLX     ___save_vfp_d8_d15_regs
0x117DBC  16af       ADD     R7, SP, #arg_38
0x117DBE  96b0       SUB     SP, SP, #0x58 ; 'X'
0x117DC0  0193       STR     R3, [SP,#0x58+var_54]
0x117DC2  2a4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x117DCC)
0x117DC4  0292       STR     R2, [SP,#0x58+var_50]
0x117DC6  6a46       MOV     R2, SP
0x117DC8  7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x117DCA  1292       STR     R2, [SP,#0x58+var_10]
0x117DCC  1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x117DCE  0122       MOVS    R2, #1
0x117DD0  0490       STR     R0, [SP,#0x58+var_48]
0x117DD2  0e93       STR     R3, [SP,#0x58+fctx.personality]
0x117DD4  264b       LDR     R3, =(GCC_except_table35_4 - 0x117DDE)
0x117DD6  08a8       ADD     R0, SP, #0x58+fctx; lpfctx
0x117DD8  0391       STR     R1, [SP,#0x58+var_4C]
0x117DDA  7b44       ADD     R3, PC; GCC_except_table35_4
0x117DDC  0f93       STR     R3, [SP,#0x58+fctx.lsda]
0x117DDE  254b       LDR     R3, =(loc_117E26 - 0x117DE6)
```

