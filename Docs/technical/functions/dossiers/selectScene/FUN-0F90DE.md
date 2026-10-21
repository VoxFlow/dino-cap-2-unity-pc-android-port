# selectScene::ligsawEffCallBack

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0F90DE` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xF90DE` |
| RVA (EA - 0x1000) | `0xF80DE` |
| File offset | `0xF80DE` |
| Size | 672 code bytes in 1 chunk(s), 308 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene17ligsawEffCallBackEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::ligsawEffCallBack(void)` |
| Bytes SHA-256 | `735c982da7f3ae9185f62aa9c04407d277fe90f64e564cd967edba504fc23a87` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.jigsaw.new_piece_flow` (LIKELY, NATIVE (decompile movePicTOLigsaw 0xf8d3e, jigsawPicClass::OnUpdate 0x124722, ligsawPicCallBack 0xf514e, AddJigsawEff 0xf4f70, ligsawEffCallBack 0xf90de, AlignmentLigsaw 0xf909e; jigsaw_1.animation.acb)): Pending pieces are processed from the end of the list: AlignmentLigsaw(g,j) stores j in this+164; if cursor==g movePicTOLigsaw(j) else cursor=g and settle. movePicTOLigsaw: showJigsawInfo, GameObject (jigsawPicClass) with the piece frame, size 2x piece -> piece size and colour 00FFFFFF -> FFFFFFFF, position (0, -140*s) -> the slot, linear over 1.0 s; after r>1: Disappear and ligsawPicCallBack: piece enabled, PlaySoun...

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/SelectJigsaw.cs:10`](../../../../../Assets/Scripts/SelectJigsaw.cs#L10) cites `0xF90DE`
- [`Assets/Scripts/SelectJigsaw.cs:408`](../../../../../Assets/Scripts/SelectJigsaw.cs#L408) cites `0xF90DE`

## Callers and callees (direct code references)

Callers: [`effectClassN::OnAnimationEnd`](../../../functions/dossiers/effectClassN/FUN-11FA80.md)

Callees: [`RectScale`](../../../functions/dossiers/DinoCap/FUN-017068.md), [`RectOffset`](../../../functions/dossiers/DinoCap/FUN-0170B6.md), `SaveFile`, [`selectScene::startScaleUI`](../../../functions/dossiers/selectScene/FUN-0F77DC.md), [`selectScene::AlignmentLigsaw`](../../../functions/dossiers/selectScene/FUN-0F909E.md), `Triniti2D::Scene::GetUIManager`, `Triniti2D::UIImage::SetTexture`, `Triniti2D::UIManager::DeleteControl`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`

## Strings it references

- `0x20A550` "ui_btn_backFrame"
- `0x20A564` "ui_btn_back_imgFrame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x2280A8` GCC_except_table35_3
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A108` __ZNKSt6vectorIN9Triniti2D7Vector2ESaIS1_EE4sizeEv_ptr
- `0x22A6C8` __ZN9Triniti2D9UIManager13CreateControlIN7DinoCap10UIMoveCtrlEEEPT_ii_ptr
- `0x23D700` aBackRect
- `0x23D710` aBackImgRect

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xF90DE   f0b5       PUSH    {R4-R7,LR}
0xF90E0   5e46       MOV     R6, R11
0xF90E2   5546       MOV     R5, R10
0xF90E4   4446       MOV     R4, R8
0xF90E6   70b4       PUSH    {R4-R6}
0xF90E8   eef0faee   BLX     ___save_vfp_d8_d15_regs
0xF90EC   16af       ADD     R7, SP, #arg_38
0xF90EE   a9b0       SUB     SP, SP, #0xA4
0xF90F0   a34b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xF90FA)
0xF90F2   0a90       STR     R0, [SP,#0xA4+var_7C]
0xF90F4   0122       MOVS    R2, #1
0xF90F6   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xF90F8   6846       MOV     R0, SP
0xF90FA   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xF90FC   1b90       STR     R0, [SP,#0xA4+var_38]
0xF90FE   11a8       ADD     R0, SP, #0xA4+fctx; lpfctx
0xF9100   1793       STR     R3, [SP,#0xA4+fctx.personality]
0xF9102   a04b       LDR     R3, =(GCC_except_table35_3 - 0xF910A)
0xF9104   1997       STR     R7, [SP,#0xA4+fctx.jbuf]
0xF9106   7b44       ADD     R3, PC; GCC_except_table35_3
0xF9108   1893       STR     R3, [SP,#0xA4+fctx.lsda]
0xF910A   9f4b       LDR     R3, =(loc_F929C - 0xF9110)
0xF910C   7b44       ADD     R3, PC; loc_F929C
0xF910E   1343       ORRS    R3, R2
```

