# selectScene::showMissionInfo

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0FD0C0` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xFD0C0` |
| RVA (EA - 0x1000) | `0xFC0C0` |
| File offset | `0xFC0C0` |
| Size | 3686 code bytes in 1 chunk(s), 1532 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene15showMissionInfoEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::showMissionInfo(void)` |
| Bytes SHA-256 | `98c9f868e930b2473995036130ae9cd656430a1c8da6665b60c7978bf3c5a60a` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.map.mission_popup.r5` (PROVEN, NATIVE): showMissionInfo0xfd0c0 uses selectedmap type and boss savedlevel forboss claws; entry static rectangles initialized0x6444..649c. iPad extra raw1113063424 is54, not56. Targets:run min(base+400*heroLevel,20000);time min(base+10*heroLevel,200);kill min(base+5*heroLevel,200);arena min(base,200). Type1..4 and7 draw numeric labels; type5/8 use atlastext. Back recreates leave controls with type6->1,5->2,7/8->3 remap and raw...

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/SelectMissionInfo.cs:51`](../../../../../Assets/Scripts/SelectMissionInfo.cs#L51) cites `0xFD38C`

## Callers and callees (direct code references)

Callers: [`selectScene::showMissionInfo`](../../../functions/dossiers/selectScene/FUN-0FD0C0.md), [`selectScene::OnUIEvent`](../../../functions/dossiers/selectScene/FUN-10B93A.md)

Callees: [`RectScale`](../../../functions/dossiers/DinoCap/FUN-017068.md), [`RectOffset`](../../../functions/dossiers/DinoCap/FUN-0170B6.md), `TadShow`, [`selectScene::startMoveUIRight`](../../../functions/dossiers/selectScene/FUN-0F6FC8.md), [`selectScene::startMoveUIRight`](../../../functions/dossiers/selectScene/FUN-0FCFDC.md), [`selectScene::showMissionInfo`](../../../functions/dossiers/selectScene/FUN-0FD0C0.md), `sub_FDF26`, `Triniti2D::Scene::GetUIManager`, `Triniti2D::UIImage::SetTexture`, `Triniti2D::UILabel::SetFont`, `Triniti2D::UILabel::SetText`, `Triniti2D::UIManager::DeleteControl`, `___switch32`, `___save_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `std::string::insert`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___fixsfsivfp`, `___floatsisfvfp`, `___gtsf2vfp`, `___subsf3vfp`, `_sprintf`

## Strings it references

- `0x20A610` "ui_map_popbgFrame"
- `0x20A624` "ui_maptype%dFrame"
- `0x2065B8` "Adventure_16"
- `0x20111C` "%d"
- `0x207598` ","
- `0x20A638` "ui_maptype%d_endFrame"
- `0x20A650` "ui_img_levelFrame"
- `0x20A550` "ui_btn_backFrame"
- `0x20A664` "ui_btn_startFrame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x228188` GCC_except_table46_0
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A25C` __ZN7DinoCap14g_cur_map_dataE_ptr
- `0x23B87C` DinoCap::g_cur_map_data
- `0x22A270` __ZN7DinoCap10g_curMapIDE_ptr
- `0x23B878` DinoCap::g_curMapID
- `0x22A280` __ZN7DinoCap10g_map_infoE_ptr
- `0x2404EC` DinoCap::g_map_info
- `0x22AC04` __ZN7DinoCap12g_boss_levelE_ptr
- `0x23B8E8` DinoCap::g_boss_level
- `0x22A2DC` __ZN7DinoCap13g_prop_PlayerE_ptr
- `0x240474` DinoCap::g_prop_Player
- `0x22A2B8` __ZN7DinoCap13g_player_typeE_ptr
- `0x23B86C` DinoCap::g_player_type
- `0x22A6C8` __ZN9Triniti2D9UIManager13CreateControlIN7DinoCap10UIMoveCtrlEEEPT_ii_ptr
- `0x23D980` mappopBgRect
- `0x22A634` __ZN7DinoCap10g_w_offsetE_ptr
- `0x23B914` DinoCap::g_w_offset
- `0x22A5F8` __ZN7DinoCap10g_h_offsetE_ptr
- `0x23B918` DinoCap::g_h_offset
- `0x23D990` mappopInfoRect
- `0x22A654` __ZN9Triniti2D9UIManager13CreateControlIN7DinoCap15UILabelMoveCtrlEEEPT_ii_ptr
- `0x22A49C` __ZN7DinoCap15g_iphone_retinaE_ptr
- `0x23B905` DinoCap::g_iphone_retina
- `0x23D9C0` mappopLevelRect
- `0x22A7C8` __ZN7DinoCap11g_boss_lineE_ptr
- `0x23B870` DinoCap::g_boss_line

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xFD0C0   f0b5       PUSH    {R4-R7,LR}
0xFD0C2   5e46       MOV     R6, R11
0xFD0C4   5546       MOV     R5, R10
0xFD0C6   4446       MOV     R4, R8
0xFD0C8   70b4       PUSH    {R4-R6}
0xFD0CA   eaf00aef   BLX     ___save_vfp_d8_d15_regs
0xFD0CE   16af       ADD     R7, SP, #arg_38
0xFD0D0   eab0       SUB     SP, SP, #0x1A8; int
0xFD0D2   c14b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xFD0DC)
0xFD0D4   2790       STR     R0, [SP,#0x1A8+var_10C]; int
0xFD0D6   0122       MOVS    R2, #1
0xFD0D8   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xFD0DA   6846       MOV     R0, SP
0xFD0DC   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xFD0DE   4790       STR     R0, [SP,#0x1A8+var_8C]
0xFD0E0   3da8       ADD     R0, SP, #0x1A8+fctx; lpfctx
0xFD0E2   4393       STR     R3, [SP,#0x1A8+fctx.personality]
0xFD0E4   bd4b       LDR     R3, =(GCC_except_table46_0 - 0xFD0EC)
0xFD0E6   4597       STR     R7, [SP,#0x1A8+fctx.jbuf]
0xFD0E8   7b44       ADD     R3, PC; GCC_except_table46_0
0xFD0EA   4493       STR     R3, [SP,#0x1A8+fctx.lsda]
0xFD0EC   bc4b       LDR     R3, =(loc_FDE70 - 0xFD0F2)
0xFD0EE   7b44       ADD     R3, PC; loc_FDE70
0xFD0F0   1343       ORRS    R3, R2
```

**Around `0xFD38C` (cited by the Unity code)**

```
0xFD37E   77f074ff   BL      __ZN9Triniti2D5Scene12GetUIManagerEv; Triniti2D::Scene::GetUIManager(void)
0xFD382   2d4b       LDR     R3, =(__ZN9Triniti2D9UIManager13CreateControlIN7DinoCap15UILabelMoveCtrlEEEPT_ii_ptr - 0xFD38C)
0xFD384   4521       MOVS    R1, #0x45 ; 'E'
0xFD386   0522       MOVS    R2, #5
0xFD388   7b44       ADD     R3, PC; __ZN9Triniti2D9UIManager13CreateControlIN7DinoCap15UILabelMoveCtrlEEEPT_ii_ptr
0xFD38A   1b68       LDR     R3, [R3]
0xFD38C   9847       BLX     R3   <==
0xFD38E   2b4b       LDR     R3, =(__ZN7DinoCap15g_iphone_retinaE_ptr - 0xFD394)
0xFD390   7b44       ADD     R3, PC; __ZN7DinoCap15g_iphone_retinaE_ptr
0xFD392   1b68       LDR     R3, [R3]; DinoCap::g_iphone_retina ...
0xFD394   1b78       LDRB    R3, [R3]; DinoCap::g_iphone_retina
0xFD396   3390       STR     R0, [SP,#0x1A8+var_DC]
0xFD398   002b       CMP     R3, #0
```

