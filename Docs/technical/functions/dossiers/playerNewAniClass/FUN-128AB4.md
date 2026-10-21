# playerNewAniClass::JinAttack

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-128AB4` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x128AB4` |
| RVA (EA - 0x1000) | `0x127AB4` |
| File offset | `0x127AB4` |
| Size | 1660 code bytes in 1 chunk(s), 701 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap17playerNewAniClass9JinAttackEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerNewAniClass::JinAttack(void)` |
| Bytes SHA-256 | `87f3f0ad474564a043b1d4c080fc89e48d765ca70569b4056c18d720e50004e9` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameControls.cs:288`](../../../../../Assets/Scripts/GameControls.cs#L288) cites `0x12903A`
- [`Assets/Scripts/GameControls.cs:300`](../../../../../Assets/Scripts/GameControls.cs#L300) cites `0x129090`

## Callers and callees (direct code references)

Callers: [`playerNewAniClass::OnKeyFrame`](../../../functions/dossiers/playerNewAniClass/FUN-129180.md)

Callees: [`CAObjCallBack::getSpritePos`](../../../functions/dossiers/CAObjCallBack/FUN-00A4D0.md), [`dinosaurClass::BeJinAttacked`](../../../functions/dossiers/dinosaurClass/FUN-01442A.md), [`PlaySound`](../../../functions/dossiers/DinoCap/FUN-01A448.md), `subWeaponClass::~subWeaponClass`, `weaponClass::~weaponClass`, [`bossN::CheckAttackDis`](../../../functions/dossiers/bossN/FUN-120E38.md), [`bossN::BeAttacked`](../../../functions/dossiers/bossN/FUN-123B70.md), `Triniti2D::GameObject::GetPosition`, `Triniti2D::GameObject::GetScene`, `Triniti2D::GameObject::GetCallBack`, `Triniti2D::GameObject::Name`, `Triniti2D::Scene::GetCallBack`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::substr`, `std::string::compare`, `std::string::assign`, `std::string::~string`, `___dynamic_cast`, `___gtsf2vfp`, `___mulsf3vfp`, `___subsf3vfp`

## Strings it references

- `0x201F10` "dino"
- `0x2013B4` "com6"
- `0x2014B0` "tou"
- `0x201F18` "boss"
- `0x204FB0` "A_hit_tie_mugun"
- `0x204878` "Attack_GAudio"
- `0x20497C` "Audio_G4Audio"
- `0x204F7C` "A_hit_tie_guangjian"
- `0x2055B8` "p2hitAudio"
- `0x204FDC` "A_hit_tie_tie"
- `0x20562C` "p5hit_tieAudio"
- `0x205600` "p5hit_muAudio"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x228E6E` GCC_except_table34_5
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A24C` __ZN7DinoCap15g_player_vectorE_ptr
- `0x2404AC` DinoCap::g_player_vector
- `0x22A2B8` __ZN7DinoCap13g_player_typeE_ptr
- `0x23B86C` DinoCap::g_player_type
- `0x22A0C4` __ZN7DinoCap16g_manager_WeaponE_ptr
- `0x2404B8` DinoCap::g_manager_Weapon
- `0x22A2F4` __ZN7DinoCap11weaponClassC2ERKS0__ptr
- `0x22A0C0` __ZTIN9Triniti2D13SceneCallBackE_ptr
- `0x22A0C8` __ZTIN7DinoCap9mainSceneE_ptr
- `0x22C868` `typeinfo for'DinoCap::mainScene
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22A0A0` __ZNKSt6vectorIPN9Triniti2D10GameObjectESaIS2_EE4sizeEv_ptr
- `0x22A0A8` __ZTIN9Triniti2D18GameObjectCallBackE_ptr
- `0x22A2F8` __ZTIN7DinoCap13dinosaurClassE_ptr
- `0x22C73C` `typeinfo for'DinoCap::dinosaurClass
- `0x22A2FC` __ZTIN7DinoCap5bossNE_ptr
- `0x22D290` `typeinfo for'DinoCap::bossN

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x128AB4  f0b5       PUSH    {R4-R7,LR}
0x128AB6  5e46       MOV     R6, R11
0x128AB8  5546       MOV     R5, R10
0x128ABA  4446       MOV     R4, R8
0x128ABC  70b4       PUSH    {R4-R6}
0x128ABE  bff010ea   BLX     ___save_vfp_d8_d15_regs
0x128AC2  16af       ADD     R7, SP, #arg_38
0x128AC4  bab0       SUB     SP, SP, #0xE8
0x128AC6  da4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x128AD0)
0x128AC8  0290       STR     R0, [SP,#0xE8+var_E0]
0x128ACA  0122       MOVS    R2, #1
0x128ACC  7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x128ACE  6846       MOV     R0, SP
0x128AD0  1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x128AD2  1c90       STR     R0, [SP,#0xE8+var_78]
0x128AD4  12a8       ADD     R0, SP, #0xE8+fctx; lpfctx
0x128AD6  1893       STR     R3, [SP,#0xE8+fctx.personality]
0x128AD8  d64b       LDR     R3, =(GCC_except_table34_5 - 0x128AE0)
0x128ADA  1a97       STR     R7, [SP,#0xE8+fctx.jbuf]
0x128ADC  7b44       ADD     R3, PC; GCC_except_table34_5
0x128ADE  1993       STR     R3, [SP,#0xE8+fctx.lsda]
0x128AE0  d54b       LDR     R3, =(loc_128FFE - 0x128AE6)
0x128AE2  7b44       ADD     R3, PC; loc_128FFE
0x128AE4  1343       ORRS    R3, R2
```

**Around `0x12903A` (cited by the Unity code)**

```
0x12902C  1393       STR     R3, [SP,#0xE8+fctx.call_site]
0x12902E  9be7       B       loc_128F68
0x129030  0723       MOVS    R3, #7
0x129032  1393       STR     R3, [SP,#0xE8+fctx.call_site]
0x129034  36a8       ADD     R0, SP, #0xE8+var_10; this
0x129036  c0f0bced   BLX     __ZNSsD2Ev; std::string::~string()
0x12903A  2b9b       LDR     R3, [SP,#0xE8+var_3C]   <==
0x12903C  002b       CMP     R3, #0
0x12903E  08d1       BNE     loc_129052
0x129040  0b9d       LDR     R5, [SP,#0xE8+var_BC]
0x129042  002d       CMP     R5, #0
0x129044  02d0       BEQ     loc_12904C
0x129046  4148       LDR     R0, =(aAHitTieMugun - 0x12904C); "A_hit_tie_mugun"
```

**Around `0x129090` (cited by the Unity code)**

```
0x129084  7844       ADD     R0, PC; "Attack_GAudio"
0x129086  1ce0       B       loc_1290C2
0x129088  062b       CMP     R3, #6
0x12908A  18d1       BNE     loc_1290BE
0x12908C  364b       LDR     R3, =(__ZN7DinoCap13g_player_typeE_ptr - 0x129092)
0x12908E  7b44       ADD     R3, PC; __ZN7DinoCap13g_player_typeE_ptr
0x129090  1b68       LDR     R3, [R3]; DinoCap::g_player_type ...   <==
0x129092  1968       LDR     R1, [R3]; DinoCap::g_player_type
0x129094  354b       LDR     R3, =(__ZN7DinoCap15g_player_vectorE_ptr - 0x12909A)
0x129096  7b44       ADD     R3, PC; __ZN7DinoCap15g_player_vectorE_ptr
0x129098  1b68       LDR     R3, [R3]; DinoCap::g_player_vector ...
0x12909A  1a68       LDR     R2, [R3]; DinoCap::g_player_vector
0x12909C  0b01       LSLS    R3, R1, #4
```

