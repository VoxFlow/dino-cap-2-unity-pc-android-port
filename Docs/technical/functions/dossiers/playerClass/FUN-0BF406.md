# playerClass::OnKeyFrame

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0BF406` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xBF406` |
| RVA (EA - 0x1000) | `0xBE406` |
| File offset | `0xBE406` |
| Size | 800 code bytes in 1 chunk(s), 363 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11playerClass10OnKeyFrameEm` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerClass::OnKeyFrame(unsigned long)` |
| Bytes SHA-256 | `4cc7362f31465cae3cce4ceb1dec992001448ffd5b0ff890d3631d4a29a5a5e5` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameControls.cs:9`](../../../../../Assets/Scripts/GameControls.cs#L9) cites `0xBF406`

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: [`mainScene::UpdateBullet`](../../../functions/dossiers/mainScene/FUN-09546C.md), [`playerClass::UpdateBG`](../../../functions/dossiers/playerClass/FUN-0B8944.md), [`playerClass::OnFireUp`](../../../functions/dossiers/playerClass/FUN-0B8BC6.md), [`playerClass::FeiBiao`](../../../functions/dossiers/playerClass/FUN-0BC868.md), [`playerClass::JinAttack`](../../../functions/dossiers/playerClass/FUN-0BDFB8.md), [`playerClass::YuanLi`](../../../functions/dossiers/playerClass/FUN-0BF0EA.md), `subWeaponClass::~subWeaponClass`, `Triniti2D::GameObject::GetScene`, `Triniti2D::GameObject::Physics`, `Triniti2D::GamePhysics::SetLinearVelocity`, `Triniti2D::GamePhysics::WakeUp`, `Triniti2D::Scene::GetCallBack`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `___dynamic_cast`, `___mulsf3vfp`

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x227002` GCC_except_table26_2
- `0x22A24C` __ZN7DinoCap15g_player_vectorE_ptr
- `0x2404AC` DinoCap::g_player_vector
- `0x22A2B8` __ZN7DinoCap13g_player_typeE_ptr
- `0x23B86C` DinoCap::g_player_type
- `0x22A2F0` __ZN7DinoCap12g_b_FireDownE_ptr
- `0x23B8C3` DinoCap::g_b_FireDown
- `0x22A0C0` __ZTIN9Triniti2D13SceneCallBackE_ptr
- `0x22A0C8` __ZTIN7DinoCap9mainSceneE_ptr
- `0x22C868` `typeinfo for'DinoCap::mainScene
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22A2DC` __ZN7DinoCap13g_prop_PlayerE_ptr
- `0x240474` DinoCap::g_prop_Player

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xBF406   f0b5       PUSH    {R4-R7,LR}
0xBF408   5e46       MOV     R6, R11
0xBF40A   5546       MOV     R5, R10
0xBF40C   4446       MOV     R4, R8
0xBF40E   70b4       PUSH    {R4-R6}
0xBF410   28f166ed   BLX     ___save_vfp_d8_d15_regs
0xBF414   16af       ADD     R7, SP, #arg_38
0xBF416   a5b0       SUB     SP, SP, #0x94
0xBF418   c34b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xBF422)
0xBF41A   0890       STR     R0, [SP,#0x94+var_74]
0xBF41C   0791       STR     R1, [SP,#0x94+var_78]
0xBF41E   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xBF420   6846       MOV     R0, SP
0xBF422   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xBF424   0121       MOVS    R1, #1
0xBF426   1590       STR     R0, [SP,#0x94+var_40]
0xBF428   1193       STR     R3, [SP,#0x94+fctx.personality]
0xBF42A   c04b       LDR     R3, =(GCC_except_table26_2 - 0xBF434)
0xBF42C   0ba8       ADD     R0, SP, #0x94+fctx; lpfctx
0xBF42E   1397       STR     R7, [SP,#0x94+fctx.jbuf]
0xBF430   7b44       ADD     R3, PC; GCC_except_table26_2
0xBF432   1293       STR     R3, [SP,#0x94+fctx.lsda]
0xBF434   be4b       LDR     R3, =(loc_BF60A - 0xBF43A)
0xBF436   7b44       ADD     R3, PC; loc_BF60A
```

