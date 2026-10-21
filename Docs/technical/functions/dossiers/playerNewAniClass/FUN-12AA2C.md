# playerNewAniClass::OnAnimationEnd

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-12AA2C` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x12AA2C` |
| RVA (EA - 0x1000) | `0x129A2C` |
| File offset | `0x129A2C` |
| Size | 932 code bytes in 1 chunk(s), 404 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap17playerNewAniClass14OnAnimationEndEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerNewAniClass::OnAnimationEnd(void)` |
| Bytes SHA-256 | `5a01d1d6908b6835aa1fa713cd096a2f4ffba9903d096df28642136d42cac70d` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameGirl.cs:9`](../../../../../Assets/Scripts/GameGirl.cs#L9) cites `0x12AA2C`
- [`Assets/Scripts/GameGirl.cs:185`](../../../../../Assets/Scripts/GameGirl.cs#L185) cites `0x12AA2C`

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: [`mainScene::GameOverStart`](../../../functions/dossiers/mainScene/FUN-0A6F70.md), `subWeaponClass::~subWeaponClass`, [`CompAnimObj::PlayAnimation`](../../../functions/dossiers/CompAnimObj/FUN-119010.md), [`playerNewAniClass::Stand`](../../../functions/dossiers/playerNewAniClass/FUN-124F82.md), [`playerNewAniClass::UpdateBG`](../../../functions/dossiers/playerNewAniClass/FUN-125C36.md), [`playerNewAniClass::UpdateControl`](../../../functions/dossiers/playerNewAniClass/FUN-125D54.md), [`playerNewAniClass::OnHitDown`](../../../functions/dossiers/playerNewAniClass/FUN-127954.md), [`playerNewAniClass::OnFireDown`](../../../functions/dossiers/playerNewAniClass/FUN-12A178.md), `Triniti2D::GameObject::GetScene`, `Triniti2D::GameObject::Physics`, `Triniti2D::GameObject::SetTimer`, `Triniti2D::GamePhysics::SetLinearVelocity`, `Triniti2D::GamePhysics::WakeUp`, `Triniti2D::Scene::GetCallBack`, `___switch32`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::compare`, `std::string::string`, `std::string::~string`, `___dynamic_cast`, `___mulsf3vfp`

## Strings it references

- `0x201338` "roll"
- `0x201DA8` "DieAnim"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x228F04` GCC_except_table39_5
- `0x22A2DC` __ZN7DinoCap13g_prop_PlayerE_ptr
- `0x240474` DinoCap::g_prop_Player
- `0x22A640` __ZN7DinoCap11g_b_HitDownE_ptr
- `0x23B8C2` DinoCap::g_b_HitDown
- `0x22A24C` __ZN7DinoCap15g_player_vectorE_ptr
- `0x2404AC` DinoCap::g_player_vector
- `0x22A2B8` __ZN7DinoCap13g_player_typeE_ptr
- `0x23B86C` DinoCap::g_player_type
- `0x22A0C4` __ZN7DinoCap16g_manager_WeaponE_ptr
- `0x2404B8` DinoCap::g_manager_Weapon
- `0x22A7E4` __ZN7DinoCap11weaponClassaSERKS0__ptr
- `0x22A2F0` __ZN7DinoCap12g_b_FireDownE_ptr
- `0x23B8C3` DinoCap::g_b_FireDown
- `0x22A738` __ZN7DinoCap12g_b_RollDownE_ptr
- `0x23B8C4` DinoCap::g_b_RollDown
- `0x22A0C0` __ZTIN9Triniti2D13SceneCallBackE_ptr
- `0x22A0C8` __ZTIN7DinoCap9mainSceneE_ptr
- `0x22C868` `typeinfo for'DinoCap::mainScene
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x12AA2C  f0b5       PUSH    {R4-R7,LR}
0x12AA2E  5e46       MOV     R6, R11
0x12AA30  5546       MOV     R5, R10
0x12AA32  4446       MOV     R4, R8
0x12AA34  70b4       PUSH    {R4-R6}
0x12AA36  bdf054ea   BLX     ___save_vfp_d8_d15_regs
0x12AA3A  16af       ADD     R7, SP, #arg_38
0x12AA3C  a6b0       SUB     SP, SP, #0x98
0x12AA3E  e44b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x12AA48)
0x12AA40  0a90       STR     R0, [SP,#0x98+var_70]
0x12AA42  0122       MOVS    R2, #1
0x12AA44  7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x12AA46  6846       MOV     R0, SP
0x12AA48  1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x12AA4A  1790       STR     R0, [SP,#0x98+var_3C]
0x12AA4C  0da8       ADD     R0, SP, #0x98+fctx; lpfctx
0x12AA4E  1393       STR     R3, [SP,#0x98+fctx.personality]
0x12AA50  e04b       LDR     R3, =(GCC_except_table39_5 - 0x12AA58)
0x12AA52  1597       STR     R7, [SP,#0x98+fctx.jbuf]
0x12AA54  7b44       ADD     R3, PC; GCC_except_table39_5
0x12AA56  1493       STR     R3, [SP,#0x98+fctx.lsda]
0x12AA58  df4b       LDR     R3, =(loc_12AD68 - 0x12AA5E)
0x12AA5A  7b44       ADD     R3, PC; loc_12AD68
0x12AA5C  1343       ORRS    R3, R2
```

