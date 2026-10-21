# selectScene::removeMissionInfo

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0FA64C` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xFA64C` |
| RVA (EA - 0x1000) | `0xF964C` |
| File offset | `0xF964C` |
| Size | 2918 code bytes in 1 chunk(s), 1227 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene17removeMissionInfoEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::removeMissionInfo(void)` |
| Bytes SHA-256 | `3117d9fc83f1f4e8ddf21085b44f28cb4453b55dec3bd5f1595a7dbb87b43bfc` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/SelectMissionInfo.cs:89`](../../../../../Assets/Scripts/SelectMissionInfo.cs#L89) cites `0xFA934`
- [`Assets/Scripts/SelectMissionInfo.cs:93`](../../../../../Assets/Scripts/SelectMissionInfo.cs#L93) cites `0xFAEBC`
- [`Assets/Scripts/SelectMissionInfo.cs:98`](../../../../../Assets/Scripts/SelectMissionInfo.cs#L98) cites `0xFB0CE`

## Callers and callees (direct code references)

Callers: [`selectScene::removeMissionInfo`](../../../functions/dossiers/selectScene/FUN-0FA64C.md), [`selectScene::OnUIEvent`](../../../functions/dossiers/selectScene/FUN-10B93A.md)

Callees: [`RectScale`](../../../functions/dossiers/DinoCap/FUN-017068.md), [`RectOffset`](../../../functions/dossiers/DinoCap/FUN-0170B6.md), `TadHide`, `TadShow`, [`selectScene::startReturnUIRight`](../../../functions/dossiers/selectScene/FUN-0F6D42.md), [`selectScene::startReturnUIRight`](../../../functions/dossiers/selectScene/FUN-0FA56A.md), [`selectScene::removeMissionInfo`](../../../functions/dossiers/selectScene/FUN-0FA64C.md), `sub_FB1B2`, `Triniti2D::Scene::GetUIManager`, `Triniti2D::UIImage::SetTexture`, `Triniti2D::UILabel::SetFont`, `Triniti2D::UILabel::SetText`, `Triniti2D::UIManager::DeleteControl`, `___switch32`, `___save_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___fixsfsivfp`, `___floatsisfvfp`, `_sprintf`

## Strings it references

- `0x20A610` "ui_map_popbgFrame"
- `0x20A624` "ui_maptype%dFrame"
- `0x2065B8` "Adventure_16"
- `0x20111C` "%d"
- `0x20A638` "ui_maptype%d_endFrame"
- `0x20A650` "ui_img_levelFrame"
- `0x20A550` "ui_btn_backFrame"
- `0x20A664` "ui_btn_startFrame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x2280F4` GCC_except_table43_0
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A280` __ZN7DinoCap10g_map_infoE_ptr
- `0x2404EC` DinoCap::g_map_info
- `0x22A25C` __ZN7DinoCap14g_cur_map_dataE_ptr
- `0x23B87C` DinoCap::g_cur_map_data
- `0x22A270` __ZN7DinoCap10g_curMapIDE_ptr
- `0x23B878` DinoCap::g_curMapID
- `0x22A6C8` __ZN9Triniti2D9UIManager13CreateControlIN7DinoCap10UIMoveCtrlEEEPT_ii_ptr
- `0x23D980` mappopBgRect
- `0x22A634` __ZN7DinoCap10g_w_offsetE_ptr
- `0x23B914` DinoCap::g_w_offset
- `0x22A5F8` __ZN7DinoCap10g_h_offsetE_ptr
- `0x23B918` DinoCap::g_h_offset
- `0x23D990` mappopInfoRect
- `0x22A654` __ZN9Triniti2D9UIManager13CreateControlIN7DinoCap15UILabelMoveCtrlEEEPT_ii_ptr
- `0x23D9C0` mappopLevelRect
- `0x23D9A0` mappopBackRect
- `0x23D9B0` mappopStartRect

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xFA64C   f0b5       PUSH    {R4-R7,LR}
0xFA64E   5e46       MOV     R6, R11
0xFA650   5546       MOV     R5, R10
0xFA652   4446       MOV     R4, R8
0xFA654   70b4       PUSH    {R4-R6}
0xFA656   edf044ec   BLX     ___save_vfp_d8_d15_regs
0xFA65A   16af       ADD     R7, SP, #arg_38
0xFA65C   e2b0       SUB     SP, SP, #0x188; int
0xFA65E   f34b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xFA668)
0xFA660   2390       STR     R0, [SP,#0x188+var_FC]; int
0xFA662   0122       MOVS    R2, #1
0xFA664   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xFA666   6846       MOV     R0, SP
0xFA668   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xFA66A   3d90       STR     R0, [SP,#0x188+var_94]
0xFA66C   33a8       ADD     R0, SP, #0x188+fctx; lpfctx
0xFA66E   3993       STR     R3, [SP,#0x188+fctx.personality]
0xFA670   ef4b       LDR     R3, =(GCC_except_table43_0 - 0xFA678)
0xFA672   3b97       STR     R7, [SP,#0x188+fctx.jbuf]
0xFA674   7b44       ADD     R3, PC; GCC_except_table43_0
0xFA676   3a93       STR     R3, [SP,#0x188+fctx.lsda]
0xFA678   ee4b       LDR     R3, =(loc_FB0EE - 0xFA67E)
0xFA67A   7b44       ADD     R3, PC; loc_FB0EE
0xFA67C   1343       ORRS    R3, R2
```

**Around `0xFA934` (cited by the Unity code)**

```
0xFA926   7af0a0fc   BL      __ZN9Triniti2D5Scene12GetUIManagerEv; Triniti2D::Scene::GetUIManager(void)
0xFA92A   564b       LDR     R3, =(__ZN9Triniti2D9UIManager13CreateControlIN7DinoCap15UILabelMoveCtrlEEEPT_ii_ptr - 0xFA934)
0xFA92C   0422       MOVS    R2, #4
0xFA92E   4521       MOVS    R1, #0x45 ; 'E'
0xFA930   7b44       ADD     R3, PC; __ZN9Triniti2D9UIManager13CreateControlIN7DinoCap15UILabelMoveCtrlEEEPT_ii_ptr
0xFA932   1b68       LDR     R3, [R3]
0xFA934   9847       BLX     R3   <==
0xFA936   239b       LDR     R3, [SP,#0x188+var_FC]
0xFA938   2c90       STR     R0, [SP,#0x188+var_D8]
0xFA93A   5868       LDR     R0, [R3,#4]; this
0xFA93C   7af095fc   BL      __ZN9Triniti2D5Scene12GetUIManagerEv; Triniti2D::Scene::GetUIManager(void)
0xFA940   4621       MOVS    R1, #0x46 ; 'F'; int
0xFA942   86f08dfd   BL      __ZN9Triniti2D9UIManager13DeleteControlEi; Triniti2D::UIManager::DeleteControl(int)
```

**Around `0xFAEBC` (cited by the Unity code)**

```
0xFAEB0   559a       LDR     R2, [SP,#0x188+var_38.bottom]
0xFAEB2   4f93       STR     R3, [SP,#0x188+var_4C]
0xFAEB4   579b       LDR     R3, [SP,#0x188+var_2C]
0xFAEB6   5499       LDR     R1, [SP,#0x188+var_38]; Rect
0xFAEB8   0093       STR     R3, [SP,#0x188+var_188]
0xFAEBA   569b       LDR     R3, [SP,#0x188+var_30]
0xFAEBC   1cf7fbf8   BL      __ZN7DinoCap10RectOffsetEN9Triniti2D4RectE; DinoCap::RectOffset(Triniti2D::Rect)   <==
0xFAEC0   0b9b       LDR     R3, [SP,#0x188+var_15C]
0xFAEC2   0c9a       LDR     R2, [SP,#0x188+var_158]
0xFAEC4   31ca       LDM     R2!, {R0,R4,R5}
0xFAEC6   31c3       STM     R3!, {R0,R4,R5}
0xFAEC8   3199       LDR     R1, [SP,#0x188+var_C4]
0xFAECA   439b       LDR     R3, [SP,#0x188+var_7C]
```

**Around `0xFB0CE` (cited by the Unity code)**

```
0xFB0C2   439b       LDR     R3, [SP,#0x188+var_7C]
0xFB0C4   081c       MOVS    R0, R1
0xFB0C6   5793       STR     R3, [SP,#0x188+var_2C]
0xFB0C8   0b68       LDR     R3, [R1]
0xFB0CA   0699       LDR     R1, [SP,#0x188+var_170]
0xFB0CC   db68       LDR     R3, [R3,#0xC]
0xFB0CE   9847       BLX     R3   <==
0xFB0D0   4849       LDR     R1, =(aUiBtnStartfram - 0xFB0DA); "ui_btn_startFrame"
0xFB0D2   0223       MOVS    R3, #2
0xFB0D4   3493       STR     R3, [SP,#0x188+fctx.call_site]
0xFB0D6   7944       ADD     R1, PC; "ui_btn_startFrame"
0xFB0D8   58a8       ADD     R0, SP, #0x188+var_28
0xFB0DA   60aa       ADD     R2, SP, #0x188+var_8
```

