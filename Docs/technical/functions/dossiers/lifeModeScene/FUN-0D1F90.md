# lifeModeScene::UpdateExp

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0D1F90` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xD1F90` |
| RVA (EA - 0x1000) | `0xD0F90` |
| File offset | `0xD0F90` |
| Size | 636 code bytes in 1 chunk(s), 289 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap13lifeModeScene9UpdateExpEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::lifeModeScene::UpdateExp(void)` |
| Bytes SHA-256 | `64336a0d617e75408f6b45842054cc5d214a5ddd46ad93dccfdba01b2d2a396c` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/DebugMenu.cs:70`](../../../../../Assets/Scripts/DebugMenu.cs#L70) cites `0xD1F90`
- [`Assets/Scripts/GameControls.cs:10`](../../../../../Assets/Scripts/GameControls.cs#L10) cites `0xD1F90`
- [`Assets/Scripts/GameControls.cs:410`](../../../../../Assets/Scripts/GameControls.cs#L410) cites `0xD1F90`
- [`Assets/Scripts/GameControls.cs:423`](../../../../../Assets/Scripts/GameControls.cs#L423) cites `0xD208E`
- [`Assets/Scripts/GameScene.cs:187`](../../../../../Assets/Scripts/GameScene.cs#L187) cites `0xD1F90`

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: [`RectScale`](../../../functions/dossiers/DinoCap/FUN-017068.md), [`RectOffset`](../../../functions/dossiers/DinoCap/FUN-0170B6.md), [`GetPlayer4Hp`](../../../functions/dossiers/DinoCap/FUN-01946C.md), [`mainScene::UpdateHP`](../../../functions/dossiers/mainScene/FUN-095228.md), [`lifeModeScene::LevelUpEffect`](../../../functions/dossiers/lifeModeScene/FUN-0D0024.md), `Triniti2D::UILabel::SetText`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::insert`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___divsi3`, `___fixsfsivfp`, `___floatsisfvfp`, `___mulsf3vfp`, `_sprintf`

## Strings it references

- `0x20111C` "%d"
- `0x207598` ","

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x227596` GCC_except_table18_6
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A2DC` __ZN7DinoCap13g_prop_PlayerE_ptr
- `0x240474` DinoCap::g_prop_Player
- `0x22A2B8` __ZN7DinoCap13g_player_typeE_ptr
- `0x23B86C` DinoCap::g_player_type
- `0x22A24C` __ZN7DinoCap15g_player_vectorE_ptr
- `0x2404AC` DinoCap::g_player_vector
- `0x22A684` __ZN7DinoCap15g_jigsaw1_countE_ptr
- `0x23E1C8` DinoCap::g_jigsaw1_count
- `0x23D4B0` levelbarRect

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xD1F90   f0b5       PUSH    {R4-R7,LR}
0xD1F92   5e46       MOV     R6, R11
0xD1F94   5546       MOV     R5, R10
0xD1F96   4446       MOV     R4, R8
0xD1F98   70b4       PUSH    {R4-R6}
0xD1F9A   15f1a2ef   BLX     ___save_vfp_d8_d15_regs
0xD1F9E   16af       ADD     R7, SP, #arg_38
0xD1FA0   adb0       SUB     SP, SP, #0xB4
0xD1FA2   9a4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xD1FAC)
0xD1FA4   0c90       STR     R0, [SP,#0xB4+var_84]
0xD1FA6   0122       MOVS    R2, #1
0xD1FA8   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xD1FAA   6846       MOV     R0, SP
0xD1FAC   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xD1FAE   1c90       STR     R0, [SP,#0xB4+var_44]
0xD1FB0   12a8       ADD     R0, SP, #0xB4+fctx; lpfctx
0xD1FB2   1893       STR     R3, [SP,#0xB4+fctx.personality]
0xD1FB4   964b       LDR     R3, =(GCC_except_table18_6 - 0xD1FBC)
0xD1FB6   1a97       STR     R7, [SP,#0xB4+fctx.jbuf]
0xD1FB8   7b44       ADD     R3, PC; GCC_except_table18_6
0xD1FBA   1993       STR     R3, [SP,#0xB4+fctx.lsda]
0xD1FBC   954b       LDR     R3, =(loc_D21CA - 0xD1FC2)
0xD1FBE   7b44       ADD     R3, PC; loc_D21CA
0xD1FC0   1343       ORRS    R3, R2
```

**Around `0xD208E` (cited by the Unity code)**

```
0xD2082   db00       LSLS    R3, R3, #3
0xD2084   5b18       ADDS    R3, R3, R1
0xD2086   0f98       LDR     R0, [SP,#0xB4+var_78]
0xD2088   1b69       LDR     R3, [R3,#0x10]
0xD208A   e418       ADDS    R4, R4, R3
0xD208C   1094       STR     R4, [SP,#0xB4+var_74]
0xD208E   a042       CMP     R0, R4   <==
0xD2090   41dd       BLE     loc_D2116
0xD2092   612a       CMP     R2, #0x61 ; 'a'
0xD2094   3cdc       BGT     loc_D2110
0xD2096   6146       MOV     R1, R12
0xD2098   0d60       STR     R5, [R1]
0xD209A   0a9a       LDR     R2, [SP,#0xB4+var_8C]
```

