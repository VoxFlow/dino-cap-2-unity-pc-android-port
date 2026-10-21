# playerClass::LiuDanQ

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0BFE94` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xBFE94` |
| RVA (EA - 0x1000) | `0xBEE94` |
| File offset | `0xBEE94` |
| Size | 1184 code bytes in 1 chunk(s), 460 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11playerClass7LiuDanQEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerClass::LiuDanQ(void)` |
| Bytes SHA-256 | `f9eaefd1082a906b5cc90991d420571ea43bebd707d0cd83580dd7c75d16bdc9` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameFire.cs:213`](../../../../../Assets/Scripts/GameFire.cs#L213) cites `0xBFE94`
- [`Assets/Scripts/GameFire.cs:221`](../../../../../Assets/Scripts/GameFire.cs#L221) cites `0xBFF74`

## Callers and callees (direct code references)

Callers: [`playerClass::OnKeyFrameAdv`](../../../functions/dossiers/playerClass/FUN-0C076A.md)

Callees: [`CAObjCallBack::playAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00A064.md), [`CAObjCallBack::getSprite`](../../../functions/dossiers/CAObjCallBack/FUN-00A3BA.md), [`CAObjCallBack::getSpritePos`](../../../functions/dossiers/CAObjCallBack/FUN-00A4D0.md), [`CAObjCallBack::setAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00AEA0.md), [`dinosaurClass::BeAttacked`](../../../functions/dossiers/dinosaurClass/FUN-014918.md), [`PlaySound`](../../../functions/dossiers/DinoCap/FUN-01A448.md), [`mainScene::GetBaozha`](../../../functions/dossiers/mainScene/FUN-098880.md), [`playerClass::Qianghuo`](../../../functions/dossiers/playerClass/FUN-0B8C90.md), `subWeaponClass::~subWeaponClass`, `weaponClass::~weaponClass`, [`bossN::BeAttacked`](../../../functions/dossiers/bossN/FUN-123B70.md), `Triniti2D::GameObject::Enable`, `Triniti2D::GameObject::GetPosition`, `Triniti2D::GameObject::GetSize`, `Triniti2D::GameObject::GetRotation`, `Triniti2D::GameObject::GetScene`, `Triniti2D::GameObject::GetCallBack`, `Triniti2D::GameObject::Name`, `Triniti2D::GameObject::SetPosition`, `Triniti2D::Scene::GetCallBack`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::substr`, `std::string::compare`, `std::string::assign`, `std::string::assign`, `std::string::~string`, `___addsf3vfp`, `___dynamic_cast`, `___ltsf2vfp`, `___mulsf3vfp`, `___subsf3vfp`

## Strings it references

- `0x2012E0` "yyj5"
- `0x2012F0` "com1"
- `0x208470` "baozhaAnim"
- `0x201F10` "dino"
- `0x201F18` "boss"
- `0x2048B8` "Attack_E2Audio"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x22701E` GCC_except_table28_2
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
0xBFE94   f0b5       PUSH    {R4-R7,LR}
0xBFE96   5e46       MOV     R6, R11
0xBFE98   5546       MOV     R5, R10
0xBFE9A   4446       MOV     R4, R8
0xBFE9C   70b4       PUSH    {R4-R6}
0xBFE9E   28f120e8   BLX     ___save_vfp_d8_d15_regs
0xBFEA2   16af       ADD     R7, SP, #arg_38
0xBFEA4   b7b0       SUB     SP, SP, #0xDC
0xBFEA6   e44b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xBFEB0)
0xBFEA8   0290       STR     R0, [SP,#0xDC+var_D4]
0xBFEAA   0122       MOVS    R2, #1
0xBFEAC   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xBFEAE   6846       MOV     R0, SP
0xBFEB0   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xBFEB2   1d90       STR     R0, [SP,#0xDC+var_68]
0xBFEB4   13a8       ADD     R0, SP, #0xDC+fctx; lpfctx
0xBFEB6   1993       STR     R3, [SP,#0xDC+fctx.personality]
0xBFEB8   e04b       LDR     R3, =(GCC_except_table28_2 - 0xBFEC0)
0xBFEBA   1b97       STR     R7, [SP,#0xDC+fctx.jbuf]
0xBFEBC   7b44       ADD     R3, PC; GCC_except_table28_2
0xBFEBE   1a93       STR     R3, [SP,#0xDC+fctx.lsda]
0xBFEC0   df4b       LDR     R3, =(loc_C012A - 0xBFEC6)
0xBFEC2   7b44       ADD     R3, PC; loc_C012A
0xBFEC4   1343       ORRS    R3, R2
```

**Around `0xBFF74` (cited by the Unity code)**

```
0xBFF66   0692       STR     R2, [SP,#0xDC+var_C4]
0xBFF68   0599       LDR     R1, [SP,#0xDC+var_C8]
0xBFF6A   1193       STR     R3, [SP,#0xDC+var_98]
0xBFF6C   1293       STR     R3, [SP,#0xDC+var_94]
0xBFF6E   29f158ef   BLX     ___mulsf3vfp
0xBFF72   0699       LDR     R1, [SP,#0xDC+var_C4]
0xBFF74   1190       STR     R0, [SP,#0xDC+var_98]   <==
0xBFF76   bc48       LDR     R0, =0x3F000000
0xBFF78   29f152ef   BLX     ___mulsf3vfp
0xBFF7C   bb49       LDR     R1, =0x3DCCCCCD
0xBFF7E   1290       STR     R0, [SP,#0xDC+var_94]
0xBFF80   119a       LDR     R2, [SP,#0xDC+var_98]
0xBFF82   129b       LDR     R3, [SP,#0xDC+var_94]
```

