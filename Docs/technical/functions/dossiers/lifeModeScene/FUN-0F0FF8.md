# lifeModeScene::GameClear

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0F0FF8` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xF0FF8` |
| RVA (EA - 0x1000) | `0xEFFF8` |
| File offset | `0xEFFF8` |
| Size | 838 code bytes in 1 chunk(s), 377 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap13lifeModeScene9GameClearEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::lifeModeScene::GameClear(void)` |
| Bytes SHA-256 | `b75d900bb0571d1836b0285547b61a8ef3984773dad3312a512c5adc4a92b24c` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | GAMEPLAY / PORTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.mission.gameclear.r118w` (PROVEN, NATIVE): GameClear (lifeMode 0xf0ff8 / mainScene 0xb7f5e): StopCreateEnemy (timers only), hero Stand + +657=0 + velocity 0 (mainScene: not for the girl), g_b_Left/Right/Fire/RollDown=0 (HitDown kept), scene stays state 4. +657 gates UpdateControl, OnFireDown, OnHitDown, dinosaur CheckAttack/Move; OnRollDown is ungated. Supersedes the PILOT 'ignorePlayer' completion rule.

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameMission.cs:11`](../../../../../Assets/Scripts/GameMission.cs#L11) cites `0xF0FF8`
- [`Assets/Scripts/GameMission.cs:122`](../../../../../Assets/Scripts/GameMission.cs#L122) cites `0xF0FF8`
- [`Assets/Scripts/GameMission.cs:133`](../../../../../Assets/Scripts/GameMission.cs#L133) cites `0xF0FF8`
- [`Assets/Scripts/GameMission.cs:144`](../../../../../Assets/Scripts/GameMission.cs#L144) cites `0xF1078`

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: [`RectScale`](../../../functions/dossiers/DinoCap/FUN-017068.md), [`playerClass::Stand`](../../../functions/dossiers/playerClass/FUN-0B88B4.md), [`playerClass::UpdateBG`](../../../functions/dossiers/playerClass/FUN-0B8944.md), [`UIMoveCtrl::StartMove`](../../../functions/dossiers/UIMoveCtrl/FUN-0CD466.md), [`lifeModeScene::StopCreateEnemy`](../../../functions/dossiers/lifeModeScene/FUN-0D0E14.md), [`playerNewAniClass::Stand`](../../../functions/dossiers/playerNewAniClass/FUN-124F82.md), [`playerNewAniClass::UpdateBG`](../../../functions/dossiers/playerNewAniClass/FUN-125C36.md), `Triniti2D::GameObject::GetCallBack`, `Triniti2D::GameObject::Physics`, `Triniti2D::GamePhysics::SetLinearVelocity`, `Triniti2D::Scene::GetUIManager`, `Triniti2D::UIImage::SetColor`, `Triniti2D::UIImage::SetTexture`, `Triniti2D::UIManager::GetControl`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___dynamic_cast`, `___mulsf3vfp`

## Strings it references

- `0x207C38` "fail_lineFrame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x227E61` GCC_except_table39_1
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A2B8` __ZN7DinoCap13g_player_typeE_ptr
- `0x23B86C` DinoCap::g_player_type
- `0x22A0A8` __ZTIN9Triniti2D18GameObjectCallBackE_ptr
- `0x22A288` __ZTIN7DinoCap17playerNewAniClassE_ptr
- `0x22D338` `typeinfo for'DinoCap::playerNewAniClass
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22A0CC` __ZTIN7DinoCap11playerClassE_ptr
- `0x22C8B0` `typeinfo for'DinoCap::playerClass
- `0x22A74C` __ZN7DinoCap12g_b_LeftDownE_ptr
- `0x23B8C0` DinoCap::g_b_LeftDown
- `0x22A6EC` __ZN7DinoCap13g_b_RightDownE_ptr
- `0x23B8C1` DinoCap::g_b_RightDown
- `0x22A2F0` __ZN7DinoCap12g_b_FireDownE_ptr
- `0x23B8C3` DinoCap::g_b_FireDown
- `0x22A738` __ZN7DinoCap12g_b_RollDownE_ptr
- `0x23B8C4` DinoCap::g_b_RollDown
- `0x22A5F8` __ZN7DinoCap10g_h_offsetE_ptr
- `0x23B918` DinoCap::g_h_offset
- `0x22A634` __ZN7DinoCap10g_w_offsetE_ptr
- `0x23B914` DinoCap::g_w_offset
- `0x22A6C8` __ZN9Triniti2D9UIManager13CreateControlIN7DinoCap10UIMoveCtrlEEEPT_ii_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xF0FF8   f0b5       PUSH    {R4-R7,LR}
0xF0FFA   5e46       MOV     R6, R11
0xF0FFC   5546       MOV     R5, R10
0xF0FFE   4446       MOV     R4, R8
0xF1000   70b4       PUSH    {R4-R6}
0xF1002   f6f06eef   BLX     ___save_vfp_d8_d15_regs
0xF1006   16af       ADD     R7, SP, #arg_38
0xF1008   c0b0       SUB     SP, SP, #0x100
0xF100A   cd4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xF1014)
0xF100C   0122       MOVS    R2, #1
0xF100E   0890       STR     R0, [SP,#0x100+var_E0]
0xF1010   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xF1012   6846       MOV     R0, SP
0xF1014   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xF1016   1c90       STR     R0, [SP,#0x100+var_90]
0xF1018   12a8       ADD     R0, SP, #0x100+fctx; lpfctx
0xF101A   1893       STR     R3, [SP,#0x100+fctx.personality]
0xF101C   c94b       LDR     R3, =(GCC_except_table39_1 - 0xF1024)
0xF101E   1a97       STR     R7, [SP,#0x100+fctx.jbuf]
0xF1020   7b44       ADD     R3, PC; GCC_except_table39_1
0xF1022   1993       STR     R3, [SP,#0x100+fctx.lsda]
0xF1024   c84b       LDR     R3, =(loc_F1254 - 0xF102A)
0xF1026   7b44       ADD     R3, PC; loc_F1254
0xF1028   1343       ORRS    R3, R2
```

**Around `0xF1078` (cited by the Unity code)**

```
0xF106C   0a94       STR     R4, [SP,#0x100+var_D8]
0xF106E   0120       MOVS    R0, #1
0xF1070   4042       NEGS    R0, R0
0xF1072   0290       STR     R0, [SP,#0x100+var_F8]
0xF1074   1390       STR     R0, [SP,#0x100+fctx.call_site]
0xF1076   0898       LDR     R0, [SP,#0x100+var_E0]; this
0xF1078   dff7ccfe   BL      __ZN7DinoCap13lifeModeScene15StopCreateEnemyEv; DinoCap::lifeModeScene::StopCreateEnemy(void)   <==
0xF107C   b64b       LDR     R3, =(__ZN7DinoCap13g_player_typeE_ptr - 0xF1082)
0xF107E   7b44       ADD     R3, PC; __ZN7DinoCap13g_player_typeE_ptr
0xF1080   1b68       LDR     R3, [R3]; DinoCap::g_player_type ...
0xF1082   1b68       LDR     R3, [R3]; DinoCap::g_player_type
0xF1084   042b       CMP     R3, #4
0xF1086   32d1       BNE     loc_F10EE
```

