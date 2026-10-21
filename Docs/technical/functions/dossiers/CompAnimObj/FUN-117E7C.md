# CompAnimObj::GetSprite

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-117E7C` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x117E7C` |
| RVA (EA - 0x1000) | `0x116E7C` |
| File offset | `0x116E7C` |
| Size | 202 code bytes in 1 chunk(s), 92 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11CompAnimObj9GetSpriteERKSs` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::CompAnimObj::GetSprite(std::string const&)` |
| Bytes SHA-256 | `0309814c7dc261f4dfefd91551d77eaf378dd4be9fa141e0fefc251d51e6d292` |
| Direct code callers | 9 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameBoss.cs:609`](../../../../../Assets/Scripts/GameBoss.cs#L609) (by name)
- [`Assets/Scripts/NativeCompObj.cs:98`](../../../../../Assets/Scripts/NativeCompObj.cs#L98) cites `0x117E7C`
- [`Assets/Scripts/NativeComponentAnimation.cs:165`](../../../../../Assets/Scripts/NativeComponentAnimation.cs#L165) (by name)

## Callers and callees (direct code references)

Callers: [`bossN::BossShao`](../../../functions/dossiers/bossN/FUN-123106.md), [`bossN::Blood`](../../../functions/dossiers/bossN/FUN-1236E0.md), [`playerNewAniClass::Die`](../../../functions/dossiers/playerNewAniClass/FUN-126A36.md), [`playerNewAniClass::LiuDanQ`](../../../functions/dossiers/playerNewAniClass/FUN-12B1BA.md), [`playerNewAniClass::KaiQiang`](../../../functions/dossiers/playerNewAniClass/FUN-12B65C.md)

Callees: `ComponentAnimation::GetComponentIndexFromSeq`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x2289CA` GCC_except_table36_3

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x117E7C  f0b5       PUSH    {R4-R7,LR}
0x117E7E  5e46       MOV     R6, R11
0x117E80  5546       MOV     R5, R10
0x117E82  4446       MOV     R4, R8
0x117E84  70b4       PUSH    {R4-R6}
0x117E86  d0f02ce8   BLX     ___save_vfp_d8_d15_regs
0x117E8A  16af       ADD     R7, SP, #arg_38
0x117E8C  96b0       SUB     SP, SP, #0x58 ; 'X'
0x117E8E  2e4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x117E98)
0x117E90  0122       MOVS    R2, #1
0x117E92  0390       STR     R0, [SP,#0x58+var_4C]
0x117E94  7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x117E96  0291       STR     R1, [SP,#0x58+var_50]
0x117E98  1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x117E9A  6946       MOV     R1, SP
0x117E9C  08a8       ADD     R0, SP, #0x58+fctx; lpfctx
0x117E9E  0e93       STR     R3, [SP,#0x58+fctx.personality]
0x117EA0  2a4b       LDR     R3, =(GCC_except_table36_3 - 0x117EAA)
0x117EA2  1097       STR     R7, [SP,#0x58+fctx.jbuf]
0x117EA4  1291       STR     R1, [SP,#0x58+var_10]
0x117EA6  7b44       ADD     R3, PC; GCC_except_table36_3
0x117EA8  0f93       STR     R3, [SP,#0x58+fctx.lsda]
0x117EAA  294b       LDR     R3, =(loc_117F02 - 0x117EB0)
0x117EAC  7b44       ADD     R3, PC; loc_117F02
```

