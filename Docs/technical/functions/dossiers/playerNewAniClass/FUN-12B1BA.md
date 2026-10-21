# playerNewAniClass::LiuDanQ

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-12B1BA` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x12B1BA` |
| RVA (EA - 0x1000) | `0x12A1BA` |
| File offset | `0x12A1BA` |
| Size | 1182 code bytes in 1 chunk(s), 465 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap17playerNewAniClass7LiuDanQEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerNewAniClass::LiuDanQ(void)` |
| Bytes SHA-256 | `9a0a1f33b7a1da47f523bab1066dc9bca0f7637a13bddb6185fd4902ac526b91` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameGirl.cs:398`](../../../../../Assets/Scripts/GameGirl.cs#L398) cites `0x12B1BA`
- [`Assets/Scripts/GameGirl.cs:404`](../../../../../Assets/Scripts/GameGirl.cs#L404) cites `0x12B2AA`

## Callers and callees (direct code references)

Callers: [`playerNewAniClass::OnKeyFrameAdv`](../../../functions/dossiers/playerNewAniClass/FUN-12BD44.md)

Callees: [`CAObjCallBack::playAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00A064.md), [`CAObjCallBack::setAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00AEA0.md), [`dinosaurClass::BeAttacked`](../../../functions/dossiers/dinosaurClass/FUN-014918.md), [`PlaySound`](../../../functions/dossiers/DinoCap/FUN-01A448.md), [`mainScene::GetBaozha`](../../../functions/dossiers/mainScene/FUN-098880.md), `subWeaponClass::~subWeaponClass`, `weaponClass::~weaponClass`, [`CompAnimObj::GetSprite`](../../../functions/dossiers/CompAnimObj/FUN-117E7C.md), [`bossN::BeAttacked`](../../../functions/dossiers/bossN/FUN-123B70.md), [`playerNewAniClass::Qianghuo`](../../../functions/dossiers/playerNewAniClass/FUN-12530C.md), `Triniti2D::Console::WriteLine`, `Triniti2D::GameObject::Enable`, `Triniti2D::GameObject::GetPosition`, `Triniti2D::GameObject::GetScene`, `Triniti2D::GameObject::GetCallBack`, `Triniti2D::GameObject::Name`, `Triniti2D::GameObject::SetPosition`, `Triniti2D::Scene::GetCallBack`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::substr`, `std::string::compare`, `std::string::assign`, `std::string::assign`, `std::string::~string`, `___addsf3vfp`, `___dynamic_cast`, `___extendsfdf2vfp`, `___ltsf2vfp`, `___mulsf3vfp`, `___subsf3vfp`

## Strings it references

- `0x2012E0` "yyj5"
- `0x2012F0` "com1"
- `0x20B284` "bshouqiang"
- `0x20B314` "pos x:%f y:%f"
- `0x208470` "baozhaAnim"
- `0x201F10` "dino"
- `0x201F18` "boss"
- `0x2048B8` "Attack_E2Audio"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x228F1E` GCC_except_table43_2
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A24C` __ZN7DinoCap15g_player_vectorE_ptr
- `0x2404AC` DinoCap::g_player_vector
- `0x22A2B8` __ZN7DinoCap13g_player_typeE_ptr
- `0x23B86C` DinoCap::g_player_type
- `0x22A114` __ZNSs4_Rep20_S_empty_rep_storageE_ptr
- `0x241588` std::string::_Rep::_S_empty_rep_storage
- `0x22A0C4` __ZN7DinoCap16g_manager_WeaponE_ptr
- `0x2404B8` DinoCap::g_manager_Weapon
- `0x22A2F4` __ZN7DinoCap11weaponClassC2ERKS0__ptr
- `0x22A0C0` __ZTIN9Triniti2D13SceneCallBackE_ptr
- `0x22A0C8` __ZTIN7DinoCap9mainSceneE_ptr
- `0x22C868` `typeinfo for'DinoCap::mainScene
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22A0A8` __ZTIN9Triniti2D18GameObjectCallBackE_ptr
- `0x22A748` __ZTIN7DinoCap11baozhaClassE_ptr
- `0x22C808` `typeinfo for'DinoCap::baozhaClass
- `0x22A524` __ZN7DinoCap15g_ca_baozhaDataE_ptr
- `0x23EEA8` DinoCap::g_ca_baozhaData
- `0x22A0A0` __ZNKSt6vectorIPN9Triniti2D10GameObjectESaIS2_EE4sizeEv_ptr
- `0x22A2F8` __ZTIN7DinoCap13dinosaurClassE_ptr
- `0x22C73C` `typeinfo for'DinoCap::dinosaurClass
- `0x22A2FC` __ZTIN7DinoCap5bossNE_ptr
- `0x22D290` `typeinfo for'DinoCap::bossN

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x12B1BA  f0b5       PUSH    {R4-R7,LR}
0x12B1BC  5e46       MOV     R6, R11
0x12B1BE  5546       MOV     R5, R10
0x12B1C0  4446       MOV     R4, R8
0x12B1C2  70b4       PUSH    {R4-R6}
0x12B1C4  bcf08cee   BLX     ___save_vfp_d8_d15_regs
0x12B1C8  16af       ADD     R7, SP, #arg_38
0x12B1CA  b6b0       SUB     SP, SP, #0xD8
0x12B1CC  d94b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x12B1D6)
0x12B1CE  0590       STR     R0, [SP,#0xD8+var_C4]
0x12B1D0  0122       MOVS    R2, #1
0x12B1D2  7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x12B1D4  6846       MOV     R0, SP
0x12B1D6  1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x12B1D8  1c90       STR     R0, [SP,#0xD8+var_68]
0x12B1DA  12a8       ADD     R0, SP, #0xD8+fctx; lpfctx
0x12B1DC  1893       STR     R3, [SP,#0xD8+fctx.personality]
0x12B1DE  d64b       LDR     R3, =(GCC_except_table43_2 - 0x12B1E6)
0x12B1E0  1a97       STR     R7, [SP,#0xD8+fctx.jbuf]
0x12B1E2  7b44       ADD     R3, PC; GCC_except_table43_2
0x12B1E4  1993       STR     R3, [SP,#0xD8+fctx.lsda]
0x12B1E6  d54b       LDR     R3, =(loc_12B44E - 0x12B1EC)
0x12B1E8  7b44       ADD     R3, PC; loc_12B44E
0x12B1EA  1343       ORRS    R3, R2
```

**Around `0x12B2AA` (cited by the Unity code)**

```
0x12B29E  c268       LDR     R2, [R0,#0xC]
0x12B2A0  0369       LDR     R3, [R0,#0x10]
0x12B2A2  3192       STR     R2, [SP,#0xD8+var_14]
0x12B2A4  3293       STR     R3, [SP,#0xD8+var_10]
0x12B2A6  c269       LDR     R2, [R0,#0x1C]
0x12B2A8  0598       LDR     R0, [SP,#0xD8+var_C4]
0x12B2AA  faf72ff8   BL      __ZN7DinoCap17playerNewAniClass8QianghuoERKN9Triniti2D7Vector2Ef; DinoCap::playerNewAniClass::Qianghuo(Triniti2D::Vector2 const&,float)   <==
0x12B2AE  0b98       LDR     R0, [SP,#0xD8+var_AC]
0x12B2B0  ac49       LDR     R1, =(aPosXFYF - 0x12B2BA); "pos x:%f y:%f"
0x12B2B2  c268       LDR     R2, [R0,#0xC]
0x12B2B4  0369       LDR     R3, [R0,#0x10]
0x12B2B6  7944       ADD     R1, PC; "pos x:%f y:%f"
0x12B2B8  0491       STR     R1, [SP,#0xD8+var_C8]
```

