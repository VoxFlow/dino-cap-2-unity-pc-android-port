# playerNewAniClass::SwitchWeapon

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-12AE48` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x12AE48` |
| RVA (EA - 0x1000) | `0x129E48` |
| File offset | `0x129E48` |
| Size | 554 code bytes in 1 chunk(s), 256 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap17playerNewAniClass12SwitchWeaponEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerNewAniClass::SwitchWeapon(void)` |
| Bytes SHA-256 | `32a1191aff17eaadf200358a31b15fdd09e2a32fbb6cc74488001e507ed21edb` |
| Direct code callers | 5 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: [`mainScene::OnTouchEvent`](../../../functions/dossiers/mainScene/FUN-096914.md), [`lifeModeScene::OnTouchEvent`](../../../functions/dossiers/lifeModeScene/FUN-0D15E4.md), [`playerNewAniClass::SwitchWeapon`](../../../functions/dossiers/playerNewAniClass/FUN-12AE48.md)

Callees: `subWeaponClass::~subWeaponClass`, [`playerNewAniClass::Stand`](../../../functions/dossiers/playerNewAniClass/FUN-124F82.md), [`playerNewAniClass::ChangeWeapon`](../../../functions/dossiers/playerNewAniClass/FUN-1298D0.md), [`playerNewAniClass::SwitchWeapon`](../../../functions/dossiers/playerNewAniClass/FUN-12AE48.md), `Triniti2D::GameObject::GetScene`, `Triniti2D::Scene::GetCallBack`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `___dynamic_cast`

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x228F0E` GCC_except_table41_3
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
0x12AE48  f0b5       PUSH    {R4-R7,LR}
0x12AE4A  5e46       MOV     R6, R11
0x12AE4C  5546       MOV     R5, R10
0x12AE4E  4446       MOV     R4, R8
0x12AE50  70b4       PUSH    {R4-R6}
0x12AE52  bdf046e8   BLX     ___save_vfp_d8_d15_regs
0x12AE56  16af       ADD     R7, SP, #arg_38
0x12AE58  a3b0       SUB     SP, SP, #0x8C
0x12AE5A  864b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x12AE64)
0x12AE5C  0590       STR     R0, [SP,#0x8C+var_78]
0x12AE5E  0121       MOVS    R1, #1
0x12AE60  7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x12AE62  6846       MOV     R0, SP
0x12AE64  1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x12AE66  1290       STR     R0, [SP,#0x8C+var_44]
0x12AE68  08a8       ADD     R0, SP, #0x8C+fctx; lpfctx
0x12AE6A  0e93       STR     R3, [SP,#0x8C+fctx.personality]
0x12AE6C  824b       LDR     R3, =(GCC_except_table41_3 - 0x12AE74)
0x12AE6E  1097       STR     R7, [SP,#0x8C+fctx.jbuf]
0x12AE70  7b44       ADD     R3, PC; GCC_except_table41_3
0x12AE72  0f93       STR     R3, [SP,#0x8C+fctx.lsda]
0x12AE74  814b       LDR     R3, =(loc_12AFF2 - 0x12AE7A)
0x12AE76  7b44       ADD     R3, PC; loc_12AFF2
0x12AE78  0b43       ORRS    R3, R1
```

