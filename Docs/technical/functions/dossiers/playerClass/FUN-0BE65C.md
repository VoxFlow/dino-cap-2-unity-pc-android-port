# playerClass::Damage

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0BE65C` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xBE65C` |
| RVA (EA - 0x1000) | `0xBD65C` |
| File offset | `0xBD65C` |
| Size | 1208 code bytes in 1 chunk(s), 494 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11playerClass6DamageEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerClass::Damage(void)` |
| Bytes SHA-256 | `1dd71ad790c165d8dfc107722bb915dff9a9011170dd5e5569177ab42dee58f1` |
| Direct code callers | 3 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameBeam.cs:7`](../../../../../Assets/Scripts/GameBeam.cs#L7) cites `0xBE65C`
- [`Assets/Scripts/GameBeam.cs:95`](../../../../../Assets/Scripts/GameBeam.cs#L95) cites `0xBE65C`

## Callers and callees (direct code references)

Callers: [`playerClass::PenHuoQ`](../../../functions/dossiers/playerClass/FUN-0BEB28.md), [`playerClass::JiGuangQ`](../../../functions/dossiers/playerClass/FUN-0BEDA6.md), [`playerClass::YuanLi`](../../../functions/dossiers/playerClass/FUN-0BF0EA.md)

Callees: [`CAObjCallBack::getSpritePos`](../../../functions/dossiers/CAObjCallBack/FUN-00A4D0.md), [`dinosaurClass::BeDamagedOver`](../../../functions/dossiers/dinosaurClass/FUN-010954.md), [`dinosaurClass::BeDamaged`](../../../functions/dossiers/dinosaurClass/FUN-011C9C.md), `subWeaponClass::~subWeaponClass`, `weaponClass::~weaponClass`, [`bossN::GetBossAttackOffset`](../../../functions/dossiers/bossN/FUN-120018.md), [`bossN::CheckAttackDis`](../../../functions/dossiers/bossN/FUN-120E38.md), [`bossN::BeAttacked`](../../../functions/dossiers/bossN/FUN-123B70.md), `Triniti2D::GameObject::GetPosition`, `Triniti2D::GameObject::GetScene`, `Triniti2D::GameObject::GetCallBack`, `Triniti2D::GameObject::Name`, `Triniti2D::Scene::GetCallBack`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::substr`, `std::string::compare`, `std::string::assign`, `std::string::~string`, `___addsf3vfp`, `___dynamic_cast`, `___gesf2vfp`, `___lesf2vfp`, `___mulsf3vfp`, `___subsf3vfp`, `___unordsf2vfp`

## Strings it references

- `0x201F10` "dino"
- `0x2013B4` "com6"
- `0x2014B0` "tou"
- `0x201F18` "boss"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x226FDE` GCC_except_table22_0
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
0xBE65C   f0b5       PUSH    {R4-R7,LR}
0xBE65E   5e46       MOV     R6, R11
0xBE660   5546       MOV     R5, R10
0xBE662   4446       MOV     R4, R8
0xBE664   70b4       PUSH    {R4-R6}
0xBE666   29f13cec   BLX     ___save_vfp_d8_d15_regs
0xBE66A   16af       ADD     R7, SP, #arg_38
0xBE66C   b4b0       SUB     SP, SP, #0xD0
0xBE66E   d94b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xBE678)
0xBE670   0290       STR     R0, [SP,#0xD0+var_C8]
0xBE672   0122       MOVS    R2, #1
0xBE674   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xBE676   6846       MOV     R0, SP
0xBE678   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xBE67A   1b90       STR     R0, [SP,#0xD0+var_64]
0xBE67C   11a8       ADD     R0, SP, #0xD0+fctx; lpfctx
0xBE67E   1793       STR     R3, [SP,#0xD0+fctx.personality]
0xBE680   d54b       LDR     R3, =(GCC_except_table22_0 - 0xBE688)
0xBE682   1997       STR     R7, [SP,#0xD0+fctx.jbuf]
0xBE684   7b44       ADD     R3, PC; GCC_except_table22_0
0xBE686   1893       STR     R3, [SP,#0xD0+fctx.lsda]
0xBE688   d44b       LDR     R3, =(loc_BE7BA - 0xBE68E)
0xBE68A   7b44       ADD     R3, PC; loc_BE7BA
0xBE68C   1343       ORRS    R3, R2
```

