# selectScene::movePicTOLigsaw

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0F8D3E` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xF8D3E` |
| RVA (EA - 0x1000) | `0xF7D3E` |
| File offset | `0xF7D3E` |
| Size | 792 code bytes in 1 chunk(s), 351 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene15movePicTOLigsawEi` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::movePicTOLigsaw(int)` |
| Bytes SHA-256 | `0e1c52733da41bccb258018f12d68774a49d24036758b1f2ac30140ac7c0127b` |
| Direct code callers | 2 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.jigsaw.new_piece_flow` (LIKELY, NATIVE (decompile movePicTOLigsaw 0xf8d3e, jigsawPicClass::OnUpdate 0x124722, ligsawPicCallBack 0xf514e, AddJigsawEff 0xf4f70, ligsawEffCallBack 0xf90de, AlignmentLigsaw 0xf909e; jigsaw_1.animation.acb)): Pending pieces are processed from the end of the list: AlignmentLigsaw(g,j) stores j in this+164; if cursor==g movePicTOLigsaw(j) else cursor=g and settle. movePicTOLigsaw: showJigsawInfo, GameObject (jigsawPicClass) with the piece frame, size 2x piece -> piece size and colour 00FFFFFF -> FFFFFFFF, position (0, -140*s) -> the slot, linear over 1.0 s; after r>1: Disappear and ligsawPicCallBack: piece enabled, PlaySoun...

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/SelectJigsaw.cs:9`](../../../../../Assets/Scripts/SelectJigsaw.cs#L9) cites `0xF8D3E`

## Callers and callees (direct code references)

Callers: [`selectScene::AlignmentLigsaw`](../../../functions/dossiers/selectScene/FUN-0F909E.md), [`selectScene::OnUpdate`](../../../functions/dossiers/selectScene/FUN-0F93C4.md)

Callees: [`selectScene::showJigsawInfo`](../../../functions/dossiers/selectScene/FUN-0F47A4.md), [`jigsawPicClass::StartMove`](../../../functions/dossiers/jigsawPicClass/FUN-1245EE.md), `Triniti2D::GameObject::GetPosition`, `Triniti2D::GameObject::GetCallBack`, `Triniti2D::GameObject::Sprite`, `Triniti2D::GameObject::SetSize`, `Triniti2D::GameSprite::SetFrame`, `Triniti2D::Scene::CreateGameObject`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___dynamic_cast`, `___fixsfsivfp`, `___floatsisfvfp`, `___mulsf3vfp`, `___subsf3vfp`, `_sprintf`

## Strings it references

- `0x207300` "jigsawPicClass"
- `0x207B04` "jigsaw_map0%d_0%dFrame"
- `0x207B1C` "jigsaw_map0%d_%dFrame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x22809C` GCC_except_table34_2
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A0A8` __ZTIN9Triniti2D18GameObjectCallBackE_ptr
- `0x22ABE4` __ZTIN7DinoCap14jigsawPicClassE_ptr
- `0x22D2D8` `typeinfo for'DinoCap::jigsawPicClass
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xF8D3E   f0b5       PUSH    {R4-R7,LR}
0xF8D40   5e46       MOV     R6, R11
0xF8D42   5546       MOV     R5, R10
0xF8D44   4446       MOV     R4, R8
0xF8D46   70b4       PUSH    {R4-R6}
0xF8D48   eff0cae8   BLX     ___save_vfp_d8_d15_regs
0xF8D4C   16af       ADD     R7, SP, #arg_38
0xF8D4E   cdb0       SUB     SP, SP, #0x134
0xF8D50   c14b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xF8D5A)
0xF8D52   0122       MOVS    R2, #1
0xF8D54   1390       STR     R0, [SP,#0x134+var_E8]
0xF8D56   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xF8D58   1291       STR     R1, [SP,#0x134+var_EC]
0xF8D5A   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xF8D5C   6946       MOV     R1, SP
0xF8D5E   2ba8       ADD     R0, SP, #0x134+fctx; lpfctx
0xF8D60   3193       STR     R3, [SP,#0x134+fctx.personality]
0xF8D62   be4b       LDR     R3, =(GCC_except_table34_2 - 0xF8D6C)
0xF8D64   3591       STR     R1, [SP,#0x134+var_60]
0xF8D66   3397       STR     R7, [SP,#0x134+fctx.jbuf]
0xF8D68   7b44       ADD     R3, PC; GCC_except_table34_2
0xF8D6A   3293       STR     R3, [SP,#0x134+fctx.lsda]
0xF8D6C   bc4b       LDR     R3, =(loc_F8E54 - 0xF8D72)
0xF8D6E   7b44       ADD     R3, PC; loc_F8E54
```

