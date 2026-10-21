# selectScene::showTavernUI

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-1056DC` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x1056DC` |
| RVA (EA - 0x1000) | `0x1046DC` |
| File offset | `0x1046DC` |
| Size | 2334 code bytes in 1 chunk(s), 991 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene12showTavernUIEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::showTavernUI(void)` |
| Bytes SHA-256 | `592e4ee7dbf3113acaca8c8ee5bcfafa42c58e07b70e08a0f5a19466ebaf088a` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.hero.ui_shell` (PROVEN, NATIVE (decompiler; rect constants decoded from IEEE bits)): selectScene m_map_type 2 UI (showTavernUI@0x1056dc + OnSelectChange@0x100b1e): id35 title 'ui_title_trvernFrame' = RectScale(taTitleRect 0,0,173,67) with only y += 0.5*g_h_offset (no x offset); five hero icons id 6+i (layer 2, frame ui_p<i+1>_icon_gFrame, rect from ctor icon table x={55,123,190,256,320},y=30,w=73,h=87 in 1x, hero i uses slot perm {0,3,1,2,4}) and backgrounds id 106+i (layer 1, 'ui_icon_bgFrame', rect...

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/SelectHero.cs:8`](../../../../../Assets/Scripts/SelectHero.cs#L8) cites `0x1056DC`
- [`Assets/Scripts/SelectHero.cs:11`](../../../../../Assets/Scripts/SelectHero.cs#L11) cites `0x1056DC`
- [`Assets/Scripts/SelectHero.cs:98`](../../../../../Assets/Scripts/SelectHero.cs#L98) cites `0x105B6E`

## Callers and callees (direct code references)

Callers: `sub_109870`

Callees: [`RectScale`](../../../functions/dossiers/DinoCap/FUN-017068.md), [`RectOffset`](../../../functions/dossiers/DinoCap/FUN-0170B6.md), [`selectScene::UpdateExp`](../../../functions/dossiers/selectScene/FUN-0F45DC.md), [`selectScene::startMoveUIRight`](../../../functions/dossiers/selectScene/FUN-0F6FC8.md), [`selectScene::startMoveUIRight`](../../../functions/dossiers/selectScene/FUN-0FCFDC.md), [`selectScene::OnSelectChange`](../../../functions/dossiers/selectScene/FUN-100B1E.md), [`selectScene::startMoveUILeft`](../../../functions/dossiers/selectScene/FUN-1020A8.md), `Triniti2D::Scene::GetUIManager`, `Triniti2D::UIImage::SetTexture`, `Triniti2D::UILabel::SetFont`, `Triniti2D::UILabel::SetColor`, `Triniti2D::UILabel::SetText`, `Triniti2D::UIManager::CreateImage`, `Triniti2D::UIManager::DeleteControl`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___floatsisfvfp`, `___mulsf3vfp`, `_sprintf`

## Strings it references

- `0x20A8E8` "ui_title_trvernFrame"
- `0x20A900` "ui_p%d_icon_gFrame"
- `0x20A914` "ui_icon_bgFrame"
- `0x20A798` "ui_goldFrame"
- `0x2065B8` "Adventure_16"
- `0x20111C` "%d"
- `0x20A8AC` "ui_btn_iapFrame"
- `0x20A550` "ui_btn_backFrame"
- `0x20A564` "ui_btn_back_imgFrame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x228382` GCC_except_table55
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x23D810` taTitleRect
- `0x22A5F8` __ZN7DinoCap10g_h_offsetE_ptr
- `0x23B918` DinoCap::g_h_offset
- `0x22A6C8` __ZN9Triniti2D9UIManager13CreateControlIN7DinoCap10UIMoveCtrlEEEPT_ii_ptr
- `0x23D8F0` playerIconBgRect
- `0x23D8B0` taGoldRect
- `0x22A654` __ZN9Triniti2D9UIManager13CreateControlIN7DinoCap15UILabelMoveCtrlEEEPT_ii_ptr
- `0x23D8D0` taGoldTxtRect
- `0x22A2DC` __ZN7DinoCap13g_prop_PlayerE_ptr
- `0x240474` DinoCap::g_prop_Player
- `0x23D890` taBuyRect
- `0x23D700` aBackRect
- `0x23D710` aBackImgRect

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x1056DC  f0b5       PUSH    {R4-R7,LR}
0x1056DE  5e46       MOV     R6, R11
0x1056E0  5546       MOV     R5, R10
0x1056E2  4446       MOV     R4, R8
0x1056E4  70b4       PUSH    {R4-R6}
0x1056E6  e2f0fceb   BLX     ___save_vfp_d8_d15_regs
0x1056EA  16af       ADD     R7, SP, #arg_38
0x1056EC  e5b0       SUB     SP, SP, #0x194
0x1056EE  e24b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x1056F8)
0x1056F0  1a90       STR     R0, [SP,#0x194+var_12C]
0x1056F2  0122       MOVS    R2, #1
0x1056F4  7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x1056F6  6846       MOV     R0, SP
0x1056F8  1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x1056FA  3990       STR     R0, [SP,#0x194+var_B0]
0x1056FC  2fa8       ADD     R0, SP, #0x194+fctx; lpfctx
0x1056FE  3593       STR     R3, [SP,#0x194+fctx.personality]
0x105700  de4b       LDR     R3, =(GCC_except_table55 - 0x105708)
0x105702  3797       STR     R7, [SP,#0x194+fctx.jbuf]
0x105704  7b44       ADD     R3, PC; GCC_except_table55
0x105706  3693       STR     R3, [SP,#0x194+fctx.lsda]
0x105708  dd4b       LDR     R3, =(loc_105EC8 - 0x10570E)
0x10570A  7b44       ADD     R3, PC; loc_105EC8
0x10570C  1343       ORRS    R3, R2
```

**Around `0x105B6E` (cited by the Unity code)**

```
0x105B60  6ff083fb   BL      __ZN9Triniti2D5Scene12GetUIManagerEv; Triniti2D::Scene::GetUIManager(void)
0x105B64  c54b       LDR     R3, =(__ZN9Triniti2D9UIManager13CreateControlIN7DinoCap15UILabelMoveCtrlEEEPT_ii_ptr - 0x105B6E)
0x105B66  3321       MOVS    R1, #0x33 ; '3'
0x105B68  0022       MOVS    R2, #0
0x105B6A  7b44       ADD     R3, PC; __ZN9Triniti2D9UIManager13CreateControlIN7DinoCap15UILabelMoveCtrlEEEPT_ii_ptr
0x105B6C  1b68       LDR     R3, [R3]
0x105B6E  9847       BLX     R3   <==
0x105B70  1a9c       LDR     R4, [SP,#0x194+var_12C]
0x105B72  f823       MOVS    R3, #0xF8
0x105B74  c249       LDR     R1, =(aAdventure16 - 0x105B7C); "Adventure_16"
0x105B76  c34a       LDR     R2, =0x18F
0x105B78  7944       ADD     R1, PC; "Adventure_16"
0x105B7A  6a44       ADD     R2, SP
```

