# playerClass::SwitchWeapon

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0BAC3C` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xBAC3C` |
| RVA (EA - 0x1000) | `0xB9C3C` |
| File offset | `0xB9C3C` |
| Size | 572 code bytes in 1 chunk(s), 262 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11playerClass12SwitchWeaponEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerClass::SwitchWeapon(void)` |
| Bytes SHA-256 | `fc3ae3a8bbc189d633c6002e84f9bbade91447ffab8e1f8e714248d4e784429a` |
| Direct code callers | 5 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameControls.cs:10`](../../../../../Assets/Scripts/GameControls.cs#L10) cites `0xBAC3C`
- [`Assets/Scripts/GameControls.cs:382`](../../../../../Assets/Scripts/GameControls.cs#L382) cites `0xBAC3C`

## Callers and callees (direct code references)

Callers: [`mainScene::OnTouchEvent`](../../../functions/dossiers/mainScene/FUN-096914.md), [`playerClass::SwitchWeapon`](../../../functions/dossiers/playerClass/FUN-0BAC3C.md), [`lifeModeScene::OnTouchEvent`](../../../functions/dossiers/lifeModeScene/FUN-0D15E4.md)

Callees: `GenComAnim`, [`playerClass::Stand`](../../../functions/dossiers/playerClass/FUN-0B88B4.md), [`playerClass::OnFireUp`](../../../functions/dossiers/playerClass/FUN-0B8BC6.md), [`playerClass::getEgg`](../../../functions/dossiers/playerClass/FUN-0B937C.md), [`playerClass::ChangeWeapon`](../../../functions/dossiers/playerClass/FUN-0B9DE8.md), [`playerClass::SwitchWeapon`](../../../functions/dossiers/playerClass/FUN-0BAC3C.md), `subWeaponClass::~subWeaponClass`, `Triniti2D::GameObject::GetScene`, `Triniti2D::Scene::GetCallBack`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `___dynamic_cast`

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x226F0C` GCC_except_table6_5
- `0x22A640` __ZN7DinoCap11g_b_HitDownE_ptr
- `0x23B8C2` DinoCap::g_b_HitDown
- `0x22A2F0` __ZN7DinoCap12g_b_FireDownE_ptr
- `0x23B8C3` DinoCap::g_b_FireDown
- `0x22A24C` __ZN7DinoCap15g_player_vectorE_ptr
- `0x2404AC` DinoCap::g_player_vector
- `0x22A2B8` __ZN7DinoCap13g_player_typeE_ptr
- `0x23B86C` DinoCap::g_player_type
- `0x22A2B4` __ZNKSt6vectorIN7DinoCap14subWeaponClassESaIS1_EE4sizeEv_ptr
- `0x22A0C4` __ZN7DinoCap16g_manager_WeaponE_ptr
- `0x2404B8` DinoCap::g_manager_Weapon
- `0x22A7E4` __ZN7DinoCap11weaponClassaSERKS0__ptr
- `0x22A0C0` __ZTIN9Triniti2D13SceneCallBackE_ptr
- `0x22A0C8` __ZTIN7DinoCap9mainSceneE_ptr
- `0x22C868` `typeinfo for'DinoCap::mainScene
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xBAC3C   f0b5       PUSH    {R4-R7,LR}
0xBAC3E   5e46       MOV     R6, R11
0xBAC40   5546       MOV     R5, R10
0xBAC42   4446       MOV     R4, R8
0xBAC44   70b4       PUSH    {R4-R6}
0xBAC46   2df14ce9   BLX     ___save_vfp_d8_d15_regs
0xBAC4A   16af       ADD     R7, SP, #arg_38
0xBAC4C   a3b0       SUB     SP, SP, #0x8C
0xBAC4E   8a4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xBAC58)
0xBAC50   0690       STR     R0, [SP,#0x8C+var_74]
0xBAC52   0121       MOVS    R1, #1
0xBAC54   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xBAC56   6846       MOV     R0, SP
0xBAC58   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xBAC5A   1290       STR     R0, [SP,#0x8C+var_44]
0xBAC5C   08a8       ADD     R0, SP, #0x8C+fctx; lpfctx
0xBAC5E   0e93       STR     R3, [SP,#0x8C+fctx.personality]
0xBAC60   864b       LDR     R3, =(GCC_except_table6_5 - 0xBAC68)
0xBAC62   1097       STR     R7, [SP,#0x8C+fctx.jbuf]
0xBAC64   7b44       ADD     R3, PC; GCC_except_table6_5
0xBAC66   0f93       STR     R3, [SP,#0x8C+fctx.lsda]
0xBAC68   854b       LDR     R3, =(loc_BAE1C - 0xBAC6E)
0xBAC6A   7b44       ADD     R3, PC; loc_BAE1C
0xBAC6C   0b43       ORRS    R3, R1
```

