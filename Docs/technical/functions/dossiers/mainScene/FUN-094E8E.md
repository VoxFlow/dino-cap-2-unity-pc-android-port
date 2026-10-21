# mainScene::ShowEggCount

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-094E8E` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x94E8E` |
| RVA (EA - 0x1000) | `0x93E8E` |
| File offset | `0x93E8E` |
| Size | 200 code bytes in 1 chunk(s), 87 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9mainScene12ShowEggCountEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::mainScene::ShowEggCount(void)` |
| Bytes SHA-256 | `943132101a7f5f90ebaf1a08af63feaa9601939e55e1e2058ff55606d23ab98e` |
| Direct code callers | 3 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameEgg.cs:6`](../../../../../Assets/Scripts/GameEgg.cs#L6) cites `0x94E8E`
- [`Assets/Scripts/GameEgg.cs:84`](../../../../../Assets/Scripts/GameEgg.cs#L84) cites `0x94E8E`

## Callers and callees (direct code references)

Callers: `sub_ABBEE`, [`playerClass::UpdateCamera`](../../../functions/dossiers/playerClass/FUN-0BB054.md), [`playerNewAniClass::UpdateCamera`](../../../functions/dossiers/playerNewAniClass/FUN-127342.md)

Callees: `Triniti2D::UILabel::SetText`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `_sprintf`

## Strings it references

- `0x207360` "%d/%d"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x226550` GCC_except_table3_6

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x94E8E   f0b5       PUSH    {R4-R7,LR}
0x94E90   5e46       MOV     R6, R11
0x94E92   5546       MOV     R5, R10
0x94E94   4446       MOV     R4, R8
0x94E96   70b4       PUSH    {R4-R6}
0x94E98   53f122e8   BLX     ___save_vfp_d8_d15_regs
0x94E9C   16af       ADD     R7, SP, #arg_38
0x94E9E   97b0       SUB     SP, SP, #0x5C ; '\'
0x94EA0   2d4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x94EAA)
0x94EA2   0290       STR     R0, [SP,#0x5C+var_54]
0x94EA4   0121       MOVS    R1, #1
0x94EA6   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x94EA8   6846       MOV     R0, SP
0x94EAA   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x94EAC   0e90       STR     R0, [SP,#0x5C+var_24]
0x94EAE   04a8       ADD     R0, SP, #0x5C+fctx; lpfctx
0x94EB0   0a93       STR     R3, [SP,#0x5C+fctx.personality]
0x94EB2   2a4b       LDR     R3, =(GCC_except_table3_6 - 0x94EBA)
0x94EB4   0c97       STR     R7, [SP,#0x5C+fctx.jbuf]
0x94EB6   7b44       ADD     R3, PC; GCC_except_table3_6
0x94EB8   0b93       STR     R3, [SP,#0x5C+fctx.lsda]
0x94EBA   294b       LDR     R3, =(loc_94F12 - 0x94EC0)
0x94EBC   7b44       ADD     R3, PC; loc_94F12
0x94EBE   0b43       ORRS    R3, R1
```

