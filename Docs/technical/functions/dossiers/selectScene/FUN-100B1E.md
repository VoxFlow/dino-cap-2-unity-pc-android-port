# selectScene::OnSelectChange

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-100B1E` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x100B1E` |
| RVA (EA - 0x1000) | `0xFFB1E` |
| File offset | `0xFFB1E` |
| Size | 2822 code bytes in 1 chunk(s), 1166 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene14OnSelectChangeEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::OnSelectChange(void)` |
| Bytes SHA-256 | `1f5339dc9dfce66eb59475d6c2784b8032fc1010e4ff561fd790503b185d7496` |
| Direct code callers | 9 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.hero.ui_shell` (PROVEN, NATIVE (decompiler; rect constants decoded from IEEE bits)): selectScene m_map_type 2 UI (showTavernUI@0x1056dc + OnSelectChange@0x100b1e): id35 title 'ui_title_trvernFrame' = RectScale(taTitleRect 0,0,173,67) with only y += 0.5*g_h_offset (no x offset); five hero icons id 6+i (layer 2, frame ui_p<i+1>_icon_gFrame, rect from ctor icon table x={55,123,190,256,320},y=30,w=73,h=87 in 1x, hero i uses slot perm {0,3,1,2,4}) and backgrounds id 106+i (layer 1, 'ui_icon_bgFrame', rect...
- Rule `dc2.hero.locked_branch_and_hire` (PROVEN, NATIVE (decompiler)): OnSelectChange@0x100b1e builds the selection frame then, if dword_240494[cursor] <= -1 (locked), tail-calls sub_101624 and ends: this+15 (selected) is NOT updated for locked heroes, so Back (id38) commits the last UNLOCKED selection. sub_101624 deletes ids 39..46,58,57,63,90 and creates id39 'ui_btn_hiredFrame' (taHireRect 386,274,88,39, startScaleUI), id47 'ui_p<cursor+1>_infoFrame' (taPlayerInfoRect 208,119,252,181...

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/SelectHero.cs:8`](../../../../../Assets/Scripts/SelectHero.cs#L8) cites `0x100B1E`
- [`Assets/Scripts/SelectHero.cs:11`](../../../../../Assets/Scripts/SelectHero.cs#L11) cites `0x100B1E`
- [`Assets/Scripts/SelectHero.cs:158`](../../../../../Assets/Scripts/SelectHero.cs#L158) cites `0x100E04`
- [`Assets/Scripts/SelectHero.cs:159`](../../../../../Assets/Scripts/SelectHero.cs#L159) cites `0x100B1E`
- [`Assets/Scripts/SelectHero.cs:164`](../../../../../Assets/Scripts/SelectHero.cs#L164) cites `0x10100C`
- [`Assets/Scripts/SelectHero.cs:190`](../../../../../Assets/Scripts/SelectHero.cs#L190) cites `0x101414`

## Callers and callees (direct code references)

Callers: `sub_101624`, [`selectScene::OnHire`](../../../functions/dossiers/selectScene/FUN-101A7C.md), [`selectScene::showTavernUI`](../../../functions/dossiers/selectScene/FUN-1056DC.md), [`selectScene::OnUIEvent`](../../../functions/dossiers/selectScene/FUN-10B93A.md)

Callees: [`RectScale`](../../../functions/dossiers/DinoCap/FUN-017068.md), [`RectOffset`](../../../functions/dossiers/DinoCap/FUN-0170B6.md), [`GetPlayer4Hp`](../../../functions/dossiers/DinoCap/FUN-01946C.md), [`selectScene::startMoveUIRight`](../../../functions/dossiers/selectScene/FUN-0F6FC8.md), [`selectScene::startMoveUIRight`](../../../functions/dossiers/selectScene/FUN-0FCEF6.md), [`selectScene::startMoveUIRight`](../../../functions/dossiers/selectScene/FUN-0FCFDC.md), `sub_101624`, `Triniti2D::Scene::GetUIManager`, `Triniti2D::UIImage::SetTexture`, `Triniti2D::UILabel::SetAlignment`, `Triniti2D::UILabel::SetFont`, `Triniti2D::UILabel::SetColor`, `Triniti2D::UILabel::SetText`, `Triniti2D::UIManager::CreateImage`, `Triniti2D::UIManager::DeleteControl`, `___save_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___divsi3`, `___fixsfsivfp`, `___floatsisfvfp`, `___mulsf3vfp`, `___subsf3vfp`, `_sprintf`

## Strings it references

- `0x20A730` "ui_selectFrame"
- `0x20A740` "ui_go_bgFrame"
- `0x2065B8` "Adventure_16"
- `0x20111C` "%d"
- `0x20A750` "ui_exp_bgFrame"
- `0x2064B4` "whiteFrame"
- `0x2065EC` "Adventure_12"
- `0x206F5C` "%Frame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x228252` GCC_except_table51
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x23D8F0` playerIconBgRect
- `0x22A2DC` __ZN7DinoCap13g_prop_PlayerE_ptr
- `0x240474` DinoCap::g_prop_Player
- `0x22A6C8` __ZN9Triniti2D9UIManager13CreateControlIN7DinoCap10UIMoveCtrlEEEPT_ii_ptr
- `0x23D7F0` taInfoTxtRect
- `0x22A654` __ZN9Triniti2D9UIManager13CreateControlIN7DinoCap15UILabelMoveCtrlEEEPT_ii_ptr
- `0x22A24C` __ZN7DinoCap15g_player_vectorE_ptr
- `0x2404AC` DinoCap::g_player_vector
- `0x22A684` __ZN7DinoCap15g_jigsaw1_countE_ptr
- `0x23E1C8` DinoCap::g_jigsaw1_count
- `0x23D800` taExpProgRect

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x100B1E  f0b5       PUSH    {R4-R7,LR}
0x100B20  5e46       MOV     R6, R11
0x100B22  5546       MOV     R5, R10
0x100B24  4446       MOV     R4, R8
0x100B26  70b4       PUSH    {R4-R6}
0x100B28  e7f0dae9   BLX     ___save_vfp_d8_d15_regs
0x100B2C  c24c       LDR     R4, =0xFFFFFDA8
0x100B2E  16af       ADD     R7, SP, #arg_38
0x100B30  a544       ADD     SP, R4
0x100B32  c24b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x100B3C)
0x100B34  2b90       STR     R0, [SP,#0x258+var_1AC]
0x100B36  0122       MOVS    R2, #1
0x100B38  7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x100B3A  6846       MOV     R0, SP
0x100B3C  1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x100B3E  5290       STR     R0, [SP,#0x258+var_110]
0x100B40  48a8       ADD     R0, SP, #0x258+fctx; lpfctx
0x100B42  4e93       STR     R3, [SP,#0x258+fctx.personality]
0x100B44  be4b       LDR     R3, =(GCC_except_table51 - 0x100B4C)
0x100B46  5097       STR     R7, [SP,#0x258+fctx.jbuf]
0x100B48  7b44       ADD     R3, PC; GCC_except_table51
0x100B4A  4f93       STR     R3, [SP,#0x258+fctx.lsda]
0x100B4C  bd4b       LDR     R3, =(loc_10190A - 0x100B52)
0x100B4E  7b44       ADD     R3, PC; loc_10190A
```

**Around `0x100E04` (cited by the Unity code)**

```
0x100DF6  74f038fa   BL      __ZN9Triniti2D5Scene12GetUIManagerEv; Triniti2D::Scene::GetUIManager(void)
0x100DFA  274b       LDR     R3, =(__ZN9Triniti2D9UIManager13CreateControlIN7DinoCap15UILabelMoveCtrlEEEPT_ii_ptr - 0x100E04)
0x100DFC  2a21       MOVS    R1, #0x2A ; '*'
0x100DFE  0122       MOVS    R2, #1
0x100E00  7b44       ADD     R3, PC; __ZN9Triniti2D9UIManager13CreateControlIN7DinoCap15UILabelMoveCtrlEEEPT_ii_ptr
0x100E02  1b68       LDR     R3, [R3]
0x100E04  9847       BLX     R3   <==
0x100E06  0e9c       LDR     R4, [SP,#0x258+var_220]
0x100E08  2449       LDR     R1, =(aAdventure16 - 0x100E12); "Adventure_16"
0x100E0A  254a       LDR     R2, =0x255
0x100E0C  1823       MOVS    R3, #0x18
0x100E0E  7944       ADD     R1, PC; "Adventure_16"
0x100E10  6a44       ADD     R2, SP
```

**Around `0x10100C` (cited by the Unity code)**

```
0x100FFE  74f034f9   BL      __ZN9Triniti2D5Scene12GetUIManagerEv; Triniti2D::Scene::GetUIManager(void)
0x101002  c04b       LDR     R3, =(__ZN9Triniti2D9UIManager13CreateControlIN7DinoCap15UILabelMoveCtrlEEEPT_ii_ptr - 0x10100C)
0x101004  2b21       MOVS    R1, #0x2B ; '+'
0x101006  0122       MOVS    R2, #1
0x101008  7b44       ADD     R3, PC; __ZN9Triniti2D9UIManager13CreateControlIN7DinoCap15UILabelMoveCtrlEEEPT_ii_ptr
0x10100A  1b68       LDR     R3, [R3]
0x10100C  9847       BLX     R3   <==
0x10100E  0c9c       LDR     R4, [SP,#0x258+var_228]
0x101010  bd49       LDR     R1, =(aAdventure16 - 0x10101A); "Adventure_16"
0x101012  be4a       LDR     R2, =0x253
0x101014  1423       MOVS    R3, #0x14
0x101016  7944       ADD     R1, PC; "Adventure_16"
0x101018  6a44       ADD     R2, SP
```

**Around `0x101414` (cited by the Unity code)**

```
0x101406  73f030ff   BL      __ZN9Triniti2D5Scene12GetUIManagerEv; Triniti2D::Scene::GetUIManager(void)
0x10140A  d84b       LDR     R3, =(__ZN9Triniti2D9UIManager13CreateControlIN7DinoCap15UILabelMoveCtrlEEEPT_ii_ptr - 0x101414)
0x10140C  3f21       MOVS    R1, #0x3F ; '?'
0x10140E  0122       MOVS    R2, #1
0x101410  7b44       ADD     R3, PC; __ZN9Triniti2D9UIManager13CreateControlIN7DinoCap15UILabelMoveCtrlEEEPT_ii_ptr
0x101412  1b68       LDR     R3, [R3]
0x101414  9847       BLX     R3   <==
0x101416  089c       LDR     R4, [SP,#0x258+var_238]
0x101418  d549       LDR     R1, =(aAdventure12 - 0x101422); "Adventure_12"
0x10141A  d64a       LDR     R2, =0x24F
0x10141C  0c23       MOVS    R3, #0xC
0x10141E  7944       ADD     R1, PC; "Adventure_12"
0x101420  6a44       ADD     R2, SP
```

