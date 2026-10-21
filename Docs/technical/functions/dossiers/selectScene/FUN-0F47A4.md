# selectScene::showJigsawInfo

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0F47A4` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xF47A4` |
| RVA (EA - 0x1000) | `0xF37A4` |
| File offset | `0xF37A4` |
| Size | 452 code bytes in 1 chunk(s), 196 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene14showJigsawInfoEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::showJigsawInfo(void)` |
| Bytes SHA-256 | `651581921104d01c14d31bb85f3a7cd2fc28d31fefce14c4439db5e1b8c8b7e0` |
| Direct code callers | 5 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.jigsaw.onload_layout` (PROVEN, NATIVE (disasm OnLoad case 5 0x1064c6; decompile showJigsawUI 0xfdf70, showJigsawCount 0xfec34)): iPad (g_iphone==0): loadImageByConf Image/ip/jigsaw_h, jigsaw_h_1, jigeff2x; GameObject background ui_bg_allFrame sized (960+g_w_offset,640+g_h_offset) at world (0,0); cursor this+156=0; TadShow(false); showJigsawCount: ReadFile, pending list = pieces with g_jigsaw_active != g_jigsaw_get and get != 0 (ascending group,piece). Pending => label 599 (Adventure_22, RGB(0xEF,0xCD,0), align (1,1), rect (0,124,480,40) via Re...
- Rule `dc2.jigsaw.info_bonus` (PROVEN, NATIVE (decompile showJigsawInfo 0xf47a4; strings 0x20a060..0x20a1e8)): showJigsawInfo recounts active pieces of the current group n; label 599 text '%d/16'; if n==16 then n=20; groups 0..3 store g_jigsawK_count = 5*n (HP / weapon attack / ammo quantity in Armory / invincible time bonuses) and group 4 stores g_jigsaw5_count = n (cash earned bonus, max 20%); label 598 text = sprintf(format[group], 5*n) for groups 0..3 and sprintf(format[4], n); the five shipped format strings are in Selec...

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/SelectJigsaw.cs:8`](../../../../../Assets/Scripts/SelectJigsaw.cs#L8) cites `0xF47A4`
- [`Assets/Scripts/SelectJigsaw.cs:230`](../../../../../Assets/Scripts/SelectJigsaw.cs#L230) cites `0xF47A4`

## Callers and callees (direct code references)

Callers: [`selectScene::ligsawPicCallBack`](../../../functions/dossiers/selectScene/FUN-0F514E.md), [`selectScene::OnTouchEvent`](../../../functions/dossiers/selectScene/FUN-0F57BC.md), [`selectScene::movePicTOLigsaw`](../../../functions/dossiers/selectScene/FUN-0F8D3E.md), [`selectScene::OnUpdate`](../../../functions/dossiers/selectScene/FUN-0F93C4.md), [`selectScene::showJigsawUI`](../../../functions/dossiers/selectScene/FUN-0FDF70.md)

Callees: `Triniti2D::UILabel::SetText`, `___switch32`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `_sprintf`

## Strings it references

- `0x209E70` "%d/16"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x227F4C` GCC_except_table12_8
- `0x22AB78` __ZN7DinoCap15g_jigsaw_activeE_ptr
- `0x23E0F8` DinoCap::g_jigsaw_active
- `0x22A684` __ZN7DinoCap15g_jigsaw1_countE_ptr
- `0x23E1C8` DinoCap::g_jigsaw1_count
- `0x22A2C8` __ZN7DinoCap15g_jigsaw2_countE_ptr
- `0x23E1CC` DinoCap::g_jigsaw2_count
- `0x22A794` __ZN7DinoCap15g_jigsaw4_countE_ptr
- `0x23E1D4` DinoCap::g_jigsaw4_count
- `0x23E1D0` DinoCap::g_jigsaw3_count
- `0x22AB9C` __ZN7DinoCap15g_jigsaw3_countE_ptr
- `0x22A688` __ZN7DinoCap15g_jigsaw5_countE_ptr
- `0x23E1D8` DinoCap::g_jigsaw5_count

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xF47A4   f0b5       PUSH    {R4-R7,LR}
0xF47A6   5e46       MOV     R6, R11
0xF47A8   5546       MOV     R5, R10
0xF47AA   4446       MOV     R4, R8
0xF47AC   70b4       PUSH    {R4-R6}
0xF47AE   f3f098eb   BLX     ___save_vfp_d8_d15_regs
0xF47B2   16af       ADD     R7, SP, #arg_38
0xF47B4   beb0       SUB     SP, SP, #0xF8
0xF47B6   6c4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xF47C0)
0xF47B8   0122       MOVS    R2, #1
0xF47BA   0190       STR     R0, [SP,#0xF8+var_F4]
0xF47BC   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xF47BE   6946       MOV     R1, SP
0xF47C0   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xF47C2   06a8       ADD     R0, SP, #0xF8+fctx; lpfctx
0xF47C4   0e97       STR     R7, [SP,#0xF8+fctx.jbuf]
0xF47C6   0c93       STR     R3, [SP,#0xF8+fctx.personality]
0xF47C8   684b       LDR     R3, =(GCC_except_table12_8 - 0xF47D0)
0xF47CA   1091       STR     R1, [SP,#0xF8+var_B8]
0xF47CC   7b44       ADD     R3, PC; GCC_except_table12_8
0xF47CE   0d93       STR     R3, [SP,#0xF8+fctx.lsda]
0xF47D0   674b       LDR     R3, =(loc_F4914 - 0xF47D6)
0xF47D2   7b44       ADD     R3, PC; loc_F4914
0xF47D4   1343       ORRS    R3, R2
```

