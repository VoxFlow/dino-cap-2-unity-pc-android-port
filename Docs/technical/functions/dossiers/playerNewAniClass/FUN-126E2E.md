# playerNewAniClass::getEgg

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-126E2E` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x126E2E` |
| RVA (EA - 0x1000) | `0x125E2E` |
| File offset | `0x125E2E` |
| Size | 628 code bytes in 1 chunk(s), 292 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap17playerNewAniClass6getEggEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerNewAniClass::getEgg(void)` |
| Bytes SHA-256 | `189c0b5578417e361fdf8e11cfd5cdbb7e5f3a2d9e7ea4fc5051fe4fa1770ef4` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameEgg.cs:120`](../../../../../Assets/Scripts/GameEgg.cs#L120) cites `0x126E2E`
- [`Assets/Scripts/GameGirl.cs:385`](../../../../../Assets/Scripts/GameGirl.cs#L385) cites `0x126E2E`

## Callers and callees (direct code references)

Callers: [`eggClass::OnUpdate`](../../../functions/dossiers/eggClass/FUN-11FEB6.md)

Callees: [`PlaySound`](../../../functions/dossiers/DinoCap/FUN-01A448.md), [`CompAnimObj::SetAvatar`](../../../functions/dossiers/CompAnimObj/FUN-11888E.md), `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::compare`, `std::string::string`, `std::string::string`, `std::string::~string`

## Strings it references

- `0x20B250` "shenti"
- `0x20B258` "newplayer_shenti2"
- `0x204F5C` "A_get_egg"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x228DFE` GCC_except_table28_7
- `0x22A2B8` __ZN7DinoCap13g_player_typeE_ptr
- `0x23B86C` DinoCap::g_player_type
- `0x22AF2C` __ZSt8_DestroyIPSt4pairISsSsEEvT_S3__ptr
- `0x22AFAC` __ZNKSt6vectorISt4pairISsSsESaIS1_EE4sizeEv_ptr
- `0x22AF68` __ZNSt4pairISsSsEC2ERKS0__ptr
- `0x22B154` __ZNSt6vectorISt4pairISsSsESaIS1_EE9push_backERKS1__ptr
- `0x22AFA8` __ZNSt4pairISsSsED2Ev_ptr
- `0x22AF80` __ZNSt6vectorISt4pairISsSsESaIS1_EEaSERKS3__ptr
- `0x22B0B0` __ZNSt4pairISsSsEC2ERKSsS2__ptr
- `0x22AEB0` __ZNSt6vectorISt4pairISsSsESaIS1_EED2Ev_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x126E2E  f0b5       PUSH    {R4-R7,LR}
0x126E30  5e46       MOV     R6, R11
0x126E32  5546       MOV     R5, R10
0x126E34  4446       MOV     R4, R8
0x126E36  70b4       PUSH    {R4-R6}
0x126E38  c1f052e8   BLX     ___save_vfp_d8_d15_regs
0x126E3C  16af       ADD     R7, SP, #arg_38
0x126E3E  9fb0       SUB     SP, SP, #0x7C ; '|'
0x126E40  984b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x126E4A)
0x126E42  6a46       MOV     R2, SP
0x126E44  1092       STR     R2, [SP,#0x7C+var_3C]
0x126E46  7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x126E48  0122       MOVS    R2, #1
0x126E4A  1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x126E4C  0490       STR     R0, [SP,#0x7C+var_6C]
0x126E4E  06a8       ADD     R0, SP, #0x7C+fctx; lpfctx
0x126E50  0c93       STR     R3, [SP,#0x7C+fctx.personality]
0x126E52  954b       LDR     R3, =(GCC_except_table28_7 - 0x126E5A)
0x126E54  0e97       STR     R7, [SP,#0x7C+fctx.jbuf]
0x126E56  7b44       ADD     R3, PC; GCC_except_table28_7
0x126E58  0d93       STR     R3, [SP,#0x7C+fctx.lsda]
0x126E5A  944b       LDR     R3, =(loc_126EB2 - 0x126E60)
0x126E5C  7b44       ADD     R3, PC; loc_126EB2
0x126E5E  1343       ORRS    R3, R2
```

