# CombineAnimation::SetSpriteAnim

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-00EDBC` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xEDBC` |
| RVA (EA - 0x1000) | `0xDDBC` |
| File offset | `0xDDBC` |
| Size | 292 code bytes in 1 chunk(s), 133 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap16CombineAnimation13SetSpriteAnimEPKc` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::CombineAnimation::SetSpriteAnim(char const*)` |
| Bytes SHA-256 | `2e75a829e329431e6de9c97b4ffef98584a65bf1b31c35df9a4085381b390e7f` |
| Direct code callers | 134 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/NativeCaAnimation.cs:142`](../../../../../Assets/Scripts/NativeCaAnimation.cs#L142) cites `0xEDBC`

## Callers and callees (direct code references)

Callers: [`loadDinoANI`](../../../functions/dossiers/DinoCap/FUN-017818.md), `GenComAnim`, `sub_69DE6`, `sub_6AB6A`, [`playerClass::ChangeWeapon`](../../../functions/dossiers/playerClass/FUN-0B9DE8.md), `sub_BA20A`, [`menuScene::InitAnim`](../../../functions/dossiers/menuScene/FUN-0C9CE0.md), [`selPlayerClass::ChangeWeapon`](../../../functions/dossiers/selPlayerClass/FUN-10E4F8.md), `sub_10E930`

Callees: `Triniti2D::TextureManager::GetAnimationId`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::append`, `std::string::string`, `std::string::string`, `std::string::~string`

## Strings it references

- `0x20117C` "Anim"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x223AB6` GCC_except_table24
- `0x22A120` __ZNKSt6vectorIPN7DinoCap7CAFrameESaIS2_EE4sizeEv_ptr
- `0x22A12C` __ZNSt8_Rb_treeISsSt4pairIKSsPN7DinoCap8CASpriteEESt10_Select1stIS5_ESt4lessISsESaIS5_EE4findERS1__ptr
- `0x22A124` __ZN9Triniti2D15_TextureManagerE_ptr
- `0x23BBA4` Triniti2D::_TextureManager

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xEDBC    f0b5       PUSH    {R4-R7,LR}
0xEDBE    5e46       MOV     R6, R11
0xEDC0    5546       MOV     R5, R10
0xEDC2    4446       MOV     R4, R8
0xEDC4    70b4       PUSH    {R4-R6}
0xEDC6    d9f18ce8   BLX     ___save_vfp_d8_d15_regs
0xEDCA    16af       ADD     R7, SP, #arg_38
0xEDCC    97b0       SUB     SP, SP, #0x5C ; '\'
0xEDCE    444b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xEDD8)
0xEDD0    0122       MOVS    R2, #1
0xEDD2    0390       STR     R0, [SP,#0x5C+var_50]
0xEDD4    7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xEDD6    0291       STR     R1, [SP,#0x5C+var_54]
0xEDD8    1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xEDDA    6946       MOV     R1, SP
0xEDDC    07a8       ADD     R0, SP, #0x5C+fctx; lpfctx
0xEDDE    0d93       STR     R3, [SP,#0x5C+fctx.personality]
0xEDE0    404b       LDR     R3, =(GCC_except_table24 - 0xEDEA)
0xEDE2    1191       STR     R1, [SP,#0x5C+var_18]
0xEDE4    0f97       STR     R7, [SP,#0x5C+fctx.jbuf]
0xEDE6    7b44       ADD     R3, PC; GCC_except_table24
0xEDE8    0e93       STR     R3, [SP,#0x5C+fctx.lsda]
0xEDEA    3f4b       LDR     R3, =(loc_EE7A - 0xEDF0)
0xEDEC    7b44       ADD     R3, PC; loc_EE7A
```

