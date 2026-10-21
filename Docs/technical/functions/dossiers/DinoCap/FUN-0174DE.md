# CreateFrame

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0174DE` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x174DE` |
| RVA (EA - 0x1000) | `0x164DE` |
| File offset | `0x164DE` |
| Size | 306 code bytes in 1 chunk(s), 138 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11CreateFrameEPKcS1_N9Triniti2D4RectEb` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::CreateFrame(char const*,char const*,Triniti2D::Rect,bool)` |
| Bytes SHA-256 | `56e52c86aca65a1ed70d2abdb637d2b409e6b8ed57a28fe7f4646a5623d27663` |
| Direct code callers | 1077 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.asset.conf.format` (PROVEN, NATIVE): A .conf atlas file is text: '[Frame]', 'Count = N', then N lines 'FrameK = (name x y w h)' with integer x,y,w,h parsed by atoi; each becomes a frame named name+'Frame' with Rect(x,y,w,h) on texture <dir>/<base>Image? loaded as .pvr when flag set else .png. Consumer: DinoCap::loadImageByConf @0x59ad6.

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/Atlas.cs:97`](../../../../../Assets/Scripts/Atlas.cs#L97) cites `0x174DE`

## Callers and callees (direct code references)

Callers: [`loadDinoBMP`](../../../functions/dossiers/DinoCap/FUN-03B8AC.md), `sub_3C854`, `sub_3D764`, `sub_3D76C`, `sub_3E682`, `sub_3F58C`, `sub_3F594`, `sub_40428`, `sub_413B4`, `sub_413BC`, `sub_422C4`, `sub_431DC`, `sub_431E4`, `sub_440E8`, `sub_44F76`, `sub_44F7E`, `sub_45F16`, `sub_46DA6`, `sub_46DAE`, `sub_47D4C`, `sub_48BE2`, `sub_48BEA`, `sub_49BFC`, `sub_4AB54`, `sub_4AB5C`, `sub_4BB20`, `sub_4CA70`, `sub_4CA78`, `sub_4DA4A`, `sub_4E9AE` ... (45)

Callees: `Triniti2D::TextureManager::GetFrameId`, `Triniti2D::TextureManager::CreateFrame`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x223C83` GCC_except_table6_3
- `0x22A124` __ZN9Triniti2D15_TextureManagerE_ptr
- `0x23BBA4` Triniti2D::_TextureManager

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x174DE   82b0       SUB     SP, SP, #8
0x174E0   f0b5       PUSH    {R4-R7,LR}
0x174E2   5e46       MOV     R6, R11
0x174E4   5546       MOV     R5, R10
0x174E6   4446       MOV     R4, R8
0x174E8   70b4       PUSH    {R4-R6}
0x174EA   d0f1faec   BLX     ___save_vfp_d8_d15_regs
0x174EE   16af       ADD     R7, SP, #0x28+arg_30
0x174F0   99b0       SUB     SP, SP, #0x64 ; 'd'
0x174F2   3293       STR     R3, [SP,#0x8C+arg_38.bottom]
0x174F4   359b       LDR     R3, [SP,#0x8C+arg_48]
0x174F6   3192       STR     R2, [SP,#0x8C+arg_38]
0x174F8   6a46       MOV     R2, SP
0x174FA   1292       STR     R2, [SP,#0x8C+var_44]
0x174FC   dbb2       UXTB    R3, R3
0x174FE   0293       STR     R3, [SP,#0x8C+var_84]
0x17500   434b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x1750A)
0x17502   0122       MOVS    R2, #1
0x17504   0490       STR     R0, [SP,#0x8C+var_7C]
0x17506   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x17508   08a8       ADD     R0, SP, #0x8C+fctx; lpfctx
0x1750A   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x1750C   0391       STR     R1, [SP,#0x8C+var_80]
0x1750E   1097       STR     R7, [SP,#0x8C+fctx.jbuf]
```

