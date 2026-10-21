# selectScene::showIAP

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-101AD2` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x101AD2` |
| RVA (EA - 0x1000) | `0x100AD2` |
| File offset | `0x100AD2` |
| Size | 1250 code bytes in 1 chunk(s), 529 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene7showIAPEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::showIAP(void)` |
| Bytes SHA-256 | `ef4e234e015731dce6711f68a0d82a7b367e109f2b53796fb14c01564bddbe57` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.bank.onload_layout` (PROVEN, NATIVE (disasm selectScene::OnLoad case 4 iPad branch 0x1066d2..0x1076fe, showIAP 0x101ad2)): m_map_type 4 (switch32 table at 0x106098: case0 0x107706, case1 0x107a22, case2 0x109a8a, case3 default, case4 0x1066aa bank, case5 0x1064c6 jigsaw, case6 0x1060bc mission start). iPad path (g_iphone==0): texture Image/ip/uiiap_h.png with CreateFrame rects ui_iap_bgFrame(0,0,960,640) ui_btn_backFrame(0,640,208,62) ui_goldFrame(960,0,46,44) ui_iap_selectFrame(208,640,300,208) ui_btn_back_imgFrame(508,640,138,54). Six ...

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/SelectArmory.cs:95`](../../../../../Assets/Scripts/SelectArmory.cs#L95) cites `0x101B44`
- [`Assets/Scripts/SelectBank.cs:8`](../../../../../Assets/Scripts/SelectBank.cs#L8) cites `0x101AD2`
- [`Assets/Scripts/SelectBank.cs:71`](../../../../../Assets/Scripts/SelectBank.cs#L71) cites `0x101AD2`
- [`Assets/Scripts/SelectBank.cs:80`](../../../../../Assets/Scripts/SelectBank.cs#L80) cites `0x101C64`
- [`Assets/Scripts/SelectBank.cs:81`](../../../../../Assets/Scripts/SelectBank.cs#L81) cites `0x101AD2`

## Callers and callees (direct code references)

Callers: [`selectScene::OnLoad`](../../../functions/dossiers/selectScene/FUN-10600A.md)

Callees: [`RectScale`](../../../functions/dossiers/DinoCap/FUN-017068.md), [`RectOffset`](../../../functions/dossiers/DinoCap/FUN-0170B6.md), [`selectScene::UpdateExp`](../../../functions/dossiers/selectScene/FUN-0F45DC.md), [`selectScene::startMoveUIRight`](../../../functions/dossiers/selectScene/FUN-0F6FC8.md), [`selectScene::startScaleUI`](../../../functions/dossiers/selectScene/FUN-0F77DC.md), [`selectScene::startMoveUIRight`](../../../functions/dossiers/selectScene/FUN-0FCFDC.md), `Triniti2D::Scene::GetUIManager`, `Triniti2D::UIImage::SetTexture`, `Triniti2D::UILabel::SetFont`, `Triniti2D::UILabel::SetColor`, `Triniti2D::UILabel::SetText`, `Triniti2D::UIManager::DeleteControl`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `_sprintf`

## Strings it references

- `0x20A798` "ui_goldFrame"
- `0x2065B8` "Adventure_16"
- `0x20111C` "%d"
- `0x20A550` "ui_btn_backFrame"
- `0x20A564` "ui_btn_back_imgFrame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x22828E` GCC_except_table52
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x23D8C0` taGoldRect1
- `0x22A6C8` __ZN9Triniti2D9UIManager13CreateControlIN7DinoCap10UIMoveCtrlEEEPT_ii_ptr
- `0x23D8E0` taGoldTxtRect1
- `0x22A654` __ZN9Triniti2D9UIManager13CreateControlIN7DinoCap15UILabelMoveCtrlEEEPT_ii_ptr
- `0x22A2DC` __ZN7DinoCap13g_prop_PlayerE_ptr
- `0x240474` DinoCap::g_prop_Player
- `0x23D700` aBackRect
- `0x23D710` aBackImgRect

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x101AD2  f0b5       PUSH    {R4-R7,LR}
0x101AD4  5e46       MOV     R6, R11
0x101AD6  5546       MOV     R5, R10
0x101AD8  4446       MOV     R4, R8
0x101ADA  70b4       PUSH    {R4-R6}
0x101ADC  e6f000ea   BLX     ___save_vfp_d8_d15_regs
0x101AE0  16af       ADD     R7, SP, #arg_38
0x101AE2  bcb0       SUB     SP, SP, #0xF0
0x101AE4  ed4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x101AEE)
0x101AE6  0d90       STR     R0, [SP,#0xF0+var_BC]
0x101AE8  0122       MOVS    R2, #1
0x101AEA  7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x101AEC  6846       MOV     R0, SP
0x101AEE  1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x101AF0  2190       STR     R0, [SP,#0xF0+var_6C]
0x101AF2  17a8       ADD     R0, SP, #0xF0+fctx; lpfctx
0x101AF4  1d93       STR     R3, [SP,#0xF0+fctx.personality]
0x101AF6  ea4b       LDR     R3, =(GCC_except_table52 - 0x101AFE)
0x101AF8  1f97       STR     R7, [SP,#0xF0+fctx.jbuf]
0x101AFA  7b44       ADD     R3, PC; GCC_except_table52
0x101AFC  1e93       STR     R3, [SP,#0xF0+fctx.lsda]
0x101AFE  e94b       LDR     R3, =(loc_101E54 - 0x101B04)
0x101B00  7b44       ADD     R3, PC; loc_101E54
0x101B02  1343       ORRS    R3, R2
```

**Around `0x101B44` (cited by the Unity code)**

```
0x101B38  3393       STR     R3, [SP,#0xF0+var_24]
0x101B3A  c368       LDR     R3, [R0,#(dword_23D8CC - 0x23D8C0)]
0x101B3C  0093       STR     R3, [SP,#0xF0+var_F0]
0x101B3E  0123       MOVS    R3, #1
0x101B40  5b42       NEGS    R3, R3
0x101B42  1893       STR     R3, [SP,#0xF0+fctx.call_site]
0x101B44  0168       LDR     R1, [R0]; Rect   <==
0x101B46  4268       LDR     R2, [R0,#(__ZL11taGoldRect1+4 - 0x23D8C0)]; taGoldRect1
0x101B48  8368       LDR     R3, [R0,#(dword_23D8C8 - 0x23D8C0)]; float
0x101B4A  201c       MOVS    R0, R4; this
0x101B4C  15f78cfa   BL      __ZN7DinoCap9RectScaleEN9Triniti2D4RectEf; DinoCap::RectScale(Triniti2D::Rect,float)
0x101B50  0c9b       LDR     R3, [SP,#0xF0+var_C0]
0x101B52  221c       MOVS    R2, R4
```

**Around `0x101C64` (cited by the Unity code)**

```
0x101C56  73f008fb   BL      __ZN9Triniti2D5Scene12GetUIManagerEv; Triniti2D::Scene::GetUIManager(void)
0x101C5A  9b4b       LDR     R3, =(__ZN9Triniti2D9UIManager13CreateControlIN7DinoCap15UILabelMoveCtrlEEEPT_ii_ptr - 0x101C64)
0x101C5C  3321       MOVS    R1, #0x33 ; '3'
0x101C5E  0022       MOVS    R2, #0
0x101C60  7b44       ADD     R3, PC; __ZN9Triniti2D9UIManager13CreateControlIN7DinoCap15UILabelMoveCtrlEEEPT_ii_ptr
0x101C62  1b68       LDR     R3, [R3]
0x101C64  9847       BLX     R3   <==
0x101C66  0d9c       LDR     R4, [SP,#0xF0+var_BC]
0x101C68  f823       MOVS    R3, #0xF8
0x101C6A  9849       LDR     R1, =(aAdventure16 - 0x101C74); "Adventure_16"
0x101C6C  6a46       MOV     R2, SP
0x101C6E  ee32       ADDS    R2, #0xEE
0x101C70  7944       ADD     R1, PC; "Adventure_16"
```

