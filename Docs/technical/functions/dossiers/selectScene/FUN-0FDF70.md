# selectScene::showJigsawUI

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0FDF70` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xFDF70` |
| RVA (EA - 0x1000) | `0xFCF70` |
| File offset | `0xFCF70` |
| Size | 3256 code bytes in 1 chunk(s), 1338 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene12showJigsawUIEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::showJigsawUI(void)` |
| Bytes SHA-256 | `4cb81e2f35bff64822d8b3a670f6755b29b5a1e32b713d4d05cf7be36367f093` |
| Direct code callers | 2 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.jigsaw.onload_layout` (PROVEN, NATIVE (disasm OnLoad case 5 0x1064c6; decompile showJigsawUI 0xfdf70, showJigsawCount 0xfec34)): iPad (g_iphone==0): loadImageByConf Image/ip/jigsaw_h, jigsaw_h_1, jigeff2x; GameObject background ui_bg_allFrame sized (960+g_w_offset,640+g_h_offset) at world (0,0); cursor this+156=0; TadShow(false); showJigsawCount: ReadFile, pending list = pieces with g_jigsaw_active != g_jigsaw_get and get != 0 (ascending group,piece). Pending => label 599 (Adventure_22, RGB(0xEF,0xCD,0), align (1,1), rect (0,124,480,40) via Re...

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/SelectJigsaw.cs:8`](../../../../../Assets/Scripts/SelectJigsaw.cs#L8) cites `0xFDF70`
- [`Assets/Scripts/SelectJigsaw.cs:179`](../../../../../Assets/Scripts/SelectJigsaw.cs#L179) cites `0xFDF70`

## Callers and callees (direct code references)

Callers: [`selectScene::showJigsawUI`](../../../functions/dossiers/selectScene/FUN-0FDF70.md), [`selectScene::showJigsawCount`](../../../functions/dossiers/selectScene/FUN-0FEC34.md), [`selectScene::OnUIEvent`](../../../functions/dossiers/selectScene/FUN-10B93A.md)

Callees: [`RectScale`](../../../functions/dossiers/DinoCap/FUN-017068.md), [`RectOffset`](../../../functions/dossiers/DinoCap/FUN-0170B6.md), `jigsawGroupState::jigsawGroupState`, `jigsawPicState::jigsawPicState`, [`selectScene::showJigsawInfo`](../../../functions/dossiers/selectScene/FUN-0F47A4.md), [`selectScene::startMoveUIRight`](../../../functions/dossiers/selectScene/FUN-0F6FC8.md), [`selectScene::AlignmentLigsaw`](../../../functions/dossiers/selectScene/FUN-0F909E.md), [`selectScene::startMoveUIRight`](../../../functions/dossiers/selectScene/FUN-0FCFDC.md), [`selectScene::showJigsawUI`](../../../functions/dossiers/selectScene/FUN-0FDF70.md), `Triniti2D::GameObject::Enable`, `Triniti2D::GameObject::Sprite`, `Triniti2D::GameObject::SetPosition`, `Triniti2D::GameObject::SetSize`, `Triniti2D::GameObject::SetPosition`, `Triniti2D::GameSprite::SetFrame`, `Triniti2D::Scene::GetUIManager`, `Triniti2D::Scene::CreateGameObject`, `Triniti2D::UIImage::SetTexture`, `Triniti2D::UILabel::SetLineSpacing`, `Triniti2D::UILabel::SetAlignment`, `Triniti2D::UILabel::SetFont`, `Triniti2D::UILabel::SetColor`, `Triniti2D::UILabel::SetText`, `Triniti2D::UIManager::DeleteControl`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::assign`, `std::string::string`, `std::string::~string`, `operator delete`, `operator new`, `___addsf3vfp`, `___fixsfsivfp`, `___floatsisfvfp`, `___mulsf3vfp`, `___subsf3vfp`, `_sprintf` ... (41)

## Strings it references

- `0x20A678` "jigsaw_titleFrame"
- `0x20A68C` "ui_bg_jigsawFrame"
- `0x207B04` "jigsaw_map0%d_0%dFrame"
- `0x207B1C` "jigsaw_map0%d_%dFrame"
- `0x2065DC` "Adventure_22"
- `0x209E70` "%d/16"
- `0x2065EC` "Adventure_12"
- `0x20A6A0` "kaksdfjalsalsdkf jasdf 
asfhasd%dfakdsfasdf"
- `0x20A550` "ui_btn_backFrame"
- `0x20A564` "ui_btn_back_imgFrame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x2281B4` GCC_except_table47_0
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A6C8` __ZN9Triniti2D9UIManager13CreateControlIN7DinoCap10UIMoveCtrlEEEPT_ii_ptr
- `0x23D720` jigTitleRect
- `0x22AB78` __ZN7DinoCap15g_jigsaw_activeE_ptr
- `0x23E0F8` DinoCap::g_jigsaw_active
- `0x22A694` __ZN7DinoCap12g_jigsaw_getE_ptr
- `0x23E0A8` DinoCap::g_jigsaw_get
- `0x22A0F4` __ZNSt6vectorIN9Triniti2D7Vector2ESaIS1_EE9push_backERKS1__ptr
- `0x22ABDC` __ZNSt6vectorIPN7DinoCap14jigsawPicStateESaIS2_EE9push_backERKS2__ptr
- `0x22AC0C` __ZNSt6vectorIPN7DinoCap16jigsawGroupStateESaIS2_EE9push_backERKS2__ptr
- `0x22A654` __ZN9Triniti2D9UIManager13CreateControlIN7DinoCap15UILabelMoveCtrlEEEPT_ii_ptr
- `0x22A108` __ZNKSt6vectorIN9Triniti2D7Vector2ESaIS1_EE4sizeEv_ptr
- `0x23D700` aBackRect
- `0x23D710` aBackImgRect

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xFDF70   f0b5       PUSH    {R4-R7,LR}
0xFDF72   5e46       MOV     R6, R11
0xFDF74   5546       MOV     R5, R10
0xFDF76   4446       MOV     R4, R8
0xFDF78   70b4       PUSH    {R4-R6}
0xFDF7A   e9f0b2ef   BLX     ___save_vfp_d8_d15_regs
0xFDF7E   f04c       LDR     R4, =0xFFFFFD10
0xFDF80   16af       ADD     R7, SP, #arg_38
0xFDF82   a544       ADD     SP, R4
0xFDF84   ef4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xFDF8E)
0xFDF86   2b90       STR     R0, [SP,#0x2F0+var_244]
0xFDF88   0122       MOVS    R2, #1
0xFDF8A   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xFDF8C   6846       MOV     R0, SP
0xFDF8E   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xFDF90   5f90       STR     R0, [SP,#0x2F0+var_174]
0xFDF92   55a8       ADD     R0, SP, #0x2F0+fctx; lpfctx
0xFDF94   5b93       STR     R3, [SP,#0x2F0+fctx.personality]
0xFDF96   ec4b       LDR     R3, =(GCC_except_table47_0 - 0xFDF9E)
0xFDF98   5d97       STR     R7, [SP,#0x2F0+fctx.jbuf]
0xFDF9A   7b44       ADD     R3, PC; GCC_except_table47_0
0xFDF9C   5c93       STR     R3, [SP,#0x2F0+fctx.lsda]
0xFDF9E   eb4b       LDR     R3, =(loc_FEACC - 0xFDFA4)
0xFDFA0   7b44       ADD     R3, PC; loc_FEACC
```

