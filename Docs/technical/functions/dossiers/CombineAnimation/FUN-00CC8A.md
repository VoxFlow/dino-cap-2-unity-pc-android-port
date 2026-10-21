# CombineAnimation::ChangeImage

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-00CC8A` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xCC8A` |
| RVA (EA - 0x1000) | `0xBC8A` |
| File offset | `0xBC8A` |
| Size | 356 code bytes in 1 chunk(s), 162 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap16CombineAnimation11ChangeImageEPKcS2_f` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::CombineAnimation::ChangeImage(char const*,char const*,float)` |
| Bytes SHA-256 | `83d9c700b54ef66bdad4f8a8b981a6af04acd3647ff3ab14c42163e36292481a` |
| Direct code callers | 365 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.animation.ca_scale_change_image.r11` (PROVEN, NATIVE): CAObjCallBack OnUpdate0xa8a0..0xa920 multiplies local x/y/w/h by callback+84 before pivot and object position. ChangeImage0xcc8a per-frame named-part lookup changes reference/frame id and multiplies existing w/h by argument; no replacement-size normalization.

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/NativeCaAnimation.cs:127`](../../../../../Assets/Scripts/NativeCaAnimation.cs#L127) cites `0xCC8A`
- [`Assets/Scripts/NativeDinoChangeK5.cs:9`](../../../../../Assets/Scripts/NativeDinoChangeK5.cs#L9) cites `0xCC8A`

## Callers and callees (direct code references)

Callers: `CombineAnimation::PlayerChangeImage`, `CombineAnimation::PlayerChangeHead`, [`CombineAnimation::DinoChangeImage`](../../../functions/dossiers/CombineAnimation/FUN-00DB2C.md), `sub_E656`, `sub_E73E`, `sub_E828`, `sub_E8C2`, `sub_E95C`, `sub_EBBC`, `sub_EC56`, [`playerClass::getEgg`](../../../functions/dossiers/playerClass/FUN-0B937C.md), [`playerClass::getDownEgg`](../../../functions/dossiers/playerClass/FUN-0B96F8.md), [`playerClass::ChangeWeapon`](../../../functions/dossiers/playerClass/FUN-0B9DE8.md), `sub_BA20A`, [`selPlayerClass::ChangeWeapon`](../../../functions/dossiers/selPlayerClass/FUN-10E4F8.md), `sub_10E930`

Callees: `Triniti2D::TextureManager::GetFrameId`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::append`, `std::string::assign`, `std::string::string`, `std::string::~string`, `___mulsf3vfp`

## Strings it references

- `0x201184` "Frame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x223AAA` GCC_except_table23
- `0x22A120` __ZNKSt6vectorIPN7DinoCap7CAFrameESaIS2_EE4sizeEv_ptr
- `0x22A12C` __ZNSt8_Rb_treeISsSt4pairIKSsPN7DinoCap8CASpriteEESt10_Select1stIS5_ESt4lessISsESaIS5_EE4findERS1__ptr
- `0x22A124` __ZN9Triniti2D15_TextureManagerE_ptr
- `0x23BBA4` Triniti2D::_TextureManager

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xCC8A    f0b5       PUSH    {R4-R7,LR}
0xCC8C    5e46       MOV     R6, R11
0xCC8E    5546       MOV     R5, R10
0xCC90    4446       MOV     R4, R8
0xCC92    70b4       PUSH    {R4-R6}
0xCC94    dbf124e9   BLX     ___save_vfp_d8_d15_regs
0xCC98    16af       ADD     R7, SP, #arg_38
0xCC9A    9db0       SUB     SP, SP, #0x74 ; 't'
0xCC9C    0293       STR     R3, [SP,#0x74+var_6C]
0xCC9E    544b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xCCA8)
0xCCA0    0392       STR     R2, [SP,#0x74+var_68]
0xCCA2    0122       MOVS    R2, #1
0xCCA4    7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xCCA6    0590       STR     R0, [SP,#0x74+var_60]
0xCCA8    1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xCCAA    0491       STR     R1, [SP,#0x74+var_64]
0xCCAC    0da8       ADD     R0, SP, #0x74+fctx; lpfctx
0xCCAE    1393       STR     R3, [SP,#0x74+fctx.personality]
0xCCB0    504b       LDR     R3, =(GCC_except_table23 - 0xCCBA)
0xCCB2    6946       MOV     R1, SP
0xCCB4    1791       STR     R1, [SP,#0x74+var_18]
0xCCB6    7b44       ADD     R3, PC; GCC_except_table23
0xCCB8    1493       STR     R3, [SP,#0x74+fctx.lsda]
0xCCBA    4f4b       LDR     R3, =(loc_CD84 - 0xCCC2)
```

