# selectScene::AddJigsawEff

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0F4F70` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xF4F70` |
| RVA (EA - 0x1000) | `0xF3F70` |
| File offset | `0xF3F70` |
| Size | 416 code bytes in 1 chunk(s), 187 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene12AddJigsawEffEN9Triniti2D7Vector2E` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::AddJigsawEff(Triniti2D::Vector2)` |
| Bytes SHA-256 | `5945581fd6c8c9edaf8692a1951e81e0ad96db3aaa7c4da54d634d389c81587d` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.jigsaw.new_piece_flow` (LIKELY, NATIVE (decompile movePicTOLigsaw 0xf8d3e, jigsawPicClass::OnUpdate 0x124722, ligsawPicCallBack 0xf514e, AddJigsawEff 0xf4f70, ligsawEffCallBack 0xf90de, AlignmentLigsaw 0xf909e; jigsaw_1.animation.acb)): Pending pieces are processed from the end of the list: AlignmentLigsaw(g,j) stores j in this+164; if cursor==g movePicTOLigsaw(j) else cursor=g and settle. movePicTOLigsaw: showJigsawInfo, GameObject (jigsawPicClass) with the piece frame, size 2x piece -> piece size and colour 00FFFFFF -> FFFFFFFF, position (0, -140*s) -> the slot, linear over 1.0 s; after r>1: Disappear and ligsawPicCallBack: piece enabled, PlaySoun...
- Rule `dc2.jigsaw.effect_object_scale.r13` (PROVEN, NATIVE): AddJigsawEff0xf4fb8 selects iPad2 (phone1), writes CompAnimObj+228 at0xf50f6; shared component consumer scales local geometry. Earlier PA-010 omission of effect object scale is FALSIFIED for this term.

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/SelectJigsaw.cs:10`](../../../../../Assets/Scripts/SelectJigsaw.cs#L10) cites `0xF4F70`
- [`Assets/Scripts/SelectJigsaw.cs:387`](../../../../../Assets/Scripts/SelectJigsaw.cs#L387) cites `0xF4FFA`
- [`Assets/Scripts/SelectJigsaw.cs:394`](../../../../../Assets/Scripts/SelectJigsaw.cs#L394) cites `0xF4FB8`

## Callers and callees (direct code references)

Callers: [`selectScene::ligsawPicCallBack`](../../../functions/dossiers/selectScene/FUN-0F514E.md)

Callees: [`CompAnimObj::PlayAnimation`](../../../functions/dossiers/CompAnimObj/FUN-119010.md), [`CompAnimObj::SetAnimation`](../../../functions/dossiers/CompAnimObj/FUN-1198AC.md), `ComponentAnimation::UpdateTexture`, `Triniti2D::GameObject::Enable`, `Triniti2D::GameObject::GetCallBack`, `Triniti2D::GameObject::SetPosition`, `Triniti2D::GameObject::SetSize`, `Triniti2D::Scene::CreateGameObject`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___dynamic_cast`, `___mulsf3vfp`

## Strings it references

- `0x207310` "effectClassN"
- `0x209E84` "jigeffAnimation"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x227F5E` GCC_except_table16_6
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A0A8` __ZTIN9Triniti2D18GameObjectCallBackE_ptr
- `0x22A604` __ZTIN7DinoCap12effectClassNE_ptr
- `0x22D1E8` `typeinfo for'DinoCap::effectClassN
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22A514` __ZN7DinoCap15g_ca_jigsaw_effE_ptr
- `0x23F694` DinoCap::g_ca_jigsaw_eff

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xF4F70   f0b5       PUSH    {R4-R7,LR}
0xF4F72   5e46       MOV     R6, R11
0xF4F74   5546       MOV     R5, R10
0xF4F76   4446       MOV     R4, R8
0xF4F78   70b4       PUSH    {R4-R6}
0xF4F7A   f2f0b2ef   BLX     ___save_vfp_d8_d15_regs
0xF4F7E   16af       ADD     R7, SP, #arg_38
0xF4F80   9fb0       SUB     SP, SP, #0x7C ; '|'
0xF4F82   634b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xF4F8E)
0xF4F84   1a91       STR     R1, [SP,#0x7C+var_14]
0xF4F86   1b92       STR     R2, [SP,#0x7C+var_10]
0xF4F88   0122       MOVS    R2, #1
0xF4F8A   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xF4F8C   0790       STR     R0, [SP,#0x7C+var_60]
0xF4F8E   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xF4F90   6946       MOV     R1, SP
0xF4F92   0da8       ADD     R0, SP, #0x7C+fctx; lpfctx
0xF4F94   1393       STR     R3, [SP,#0x7C+fctx.personality]
0xF4F96   5f4b       LDR     R3, =(GCC_except_table16_6 - 0xF4FA0)
0xF4F98   1597       STR     R7, [SP,#0x7C+fctx.jbuf]
0xF4F9A   1791       STR     R1, [SP,#0x7C+var_20]
0xF4F9C   7b44       ADD     R3, PC; GCC_except_table16_6
0xF4F9E   1493       STR     R3, [SP,#0x7C+fctx.lsda]
0xF4FA0   5d4b       LDR     R3, =(loc_F5096 - 0xF4FA6)
```

**Around `0xF4FB8` (cited by the Unity code)**

```
0xF4FAC   5b4b       LDR     R3, =(__ZN7DinoCap8g_iphoneE_ptr - 0xF4FB2)
0xF4FAE   7b44       ADD     R3, PC; __ZN7DinoCap8g_iphoneE_ptr
0xF4FB0   1b68       LDR     R3, [R3]; DinoCap::g_iphone ...
0xF4FB2   1b78       LDRB    R3, [R3]; DinoCap::g_iphone
0xF4FB4   002b       CMP     R3, #0
0xF4FB6   02d1       BNE     loc_F4FBE
0xF4FB8   594a       LDR     R2, =0x40000000   <==
0xF4FBA   0a92       STR     R2, [SP,#0x7C+var_54]
0xF4FBC   01e0       B       loc_F4FC2
0xF4FBE   594b       LDR     R3, =0x3F800000
0xF4FC0   0a93       STR     R3, [SP,#0x7C+var_54]
0xF4FC2   0799       LDR     R1, [SP,#0x7C+var_60]
0xF4FC4   0422       MOVS    R2, #4
```

**Around `0xF4FFA` (cited by the Unity code)**

```
0xF4FEC   0898       LDR     R0, [SP,#0x7C+var_5C]; this
0xF4FEE   1da9       ADD     R1, SP, #0x7C+var_8; std::string *
0xF4FF0   0022       MOVS    R2, #0; int
0xF4FF2   0123       MOVS    R3, #1; bool
0xF4FF4   80f0b8f9   BL      __ZN9Triniti2D5Scene16CreateGameObjectERKSsibbbbbb; Triniti2D::Scene::CreateGameObject(std::string const&,int,bool,bool,bool,bool,bool,bool)
0xF4FF8   0423       MOVS    R3, #4
0xF4FFA   0b90       STR     R0, [SP,#0x7C+var_50]   <==
0xF4FFC   0e93       STR     R3, [SP,#0x7C+fctx.call_site]
0xF4FFE   1da8       ADD     R0, SP, #0x7C+var_8; this
0xF5000   f4f0d6ed   BLX     __ZNSsD2Ev; std::string::~string()
0xF5004   03e0       B       loc_F500E
0xF5006   0023       MOVS    R3, #0
0xF5008   1da8       ADD     R0, SP, #0x7C+var_8
```

