# mainScene::GameClear

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0B7F5E` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xB7F5E` |
| RVA (EA - 0x1000) | `0xB6F5E` |
| File offset | `0xB6F5E` |
| Size | 728 code bytes in 1 chunk(s), 331 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9mainScene9GameClearEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::mainScene::GameClear(void)` |
| Bytes SHA-256 | `8b7d73a938ac8f80286ff05b0544c5fbb5399c84bd8a6d3261f9db326239a3bb` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | GAMEPLAY / PORTED |
| Depth | deep: curated note below, from the batch that established it |

## What it does

Mission complete: stops spawning, stands the hero, clears the held-button flags and draws the fail_line band (control 20). It does not guard against a later death: a hero killed afterwards gets GameOverStart on top (kept as an original quirk).

- Rule `dc2.mission.gameclear.r118w` (PROVEN, NATIVE): GameClear (lifeMode 0xf0ff8 / mainScene 0xb7f5e): StopCreateEnemy (timers only), hero Stand + +657=0 + velocity 0 (mainScene: not for the girl), g_b_Left/Right/Fire/RollDown=0 (HitDown kept), scene stays state 4. +657 gates UpdateControl, OnFireDown, OnHitDown, dinosaur CheckAttack/Move; OnRollDown is ungated. Supersedes the PILOT 'ignorePlayer' completion rule.

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameMainScene.cs:73`](../../../../../Assets/Scripts/GameMainScene.cs#L73) cites `0xB7F5E`
- [`Assets/Scripts/GameMission.cs:11`](../../../../../Assets/Scripts/GameMission.cs#L11) cites `0xB7F5E`
- [`Assets/Scripts/GameMission.cs:133`](../../../../../Assets/Scripts/GameMission.cs#L133) cites `0xB7F5E`
- [`Assets/Scripts/GameMission.cs:144`](../../../../../Assets/Scripts/GameMission.cs#L144) cites `0xB7FDC`
- [`Assets/Scripts/GameMission.cs:145`](../../../../../Assets/Scripts/GameMission.cs#L145) cites `0xB7FEA`

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: [`RectScale`](../../../functions/dossiers/DinoCap/FUN-017068.md), [`mainScene::StopCreateEnemy`](../../../functions/dossiers/mainScene/FUN-09560E.md), [`playerClass::Stand`](../../../functions/dossiers/playerClass/FUN-0B88B4.md), [`playerClass::UpdateBG`](../../../functions/dossiers/playerClass/FUN-0B8944.md), [`UIMoveCtrl::StartMove`](../../../functions/dossiers/UIMoveCtrl/FUN-0CD466.md), `Triniti2D::GameObject::GetCallBack`, `Triniti2D::GameObject::Physics`, `Triniti2D::GamePhysics::SetLinearVelocity`, `Triniti2D::Scene::GetUIManager`, `Triniti2D::UIImage::SetColor`, `Triniti2D::UIImage::SetTexture`, `Triniti2D::UIManager::GetControl`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___dynamic_cast`, `___mulsf3vfp`

## Strings it references

- `0x207C38` "fail_lineFrame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x226EF6` GCC_except_table47
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A2B8` __ZN7DinoCap13g_player_typeE_ptr
- `0x23B86C` DinoCap::g_player_type
- `0x22A0A8` __ZTIN9Triniti2D18GameObjectCallBackE_ptr
- `0x22A0CC` __ZTIN7DinoCap11playerClassE_ptr
- `0x22C8B0` `typeinfo for'DinoCap::playerClass
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
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
0xB7F5E   f0b5       PUSH    {R4-R7,LR}
0xB7F60   5e46       MOV     R6, R11
0xB7F62   5546       MOV     R5, R10
0xB7F64   4446       MOV     R4, R8
0xB7F66   70b4       PUSH    {R4-R6}
0xB7F68   2ff1baef   BLX     ___save_vfp_d8_d15_regs
0xB7F6C   16af       ADD     R7, SP, #arg_38
0xB7F6E   bcb0       SUB     SP, SP, #0xF0
0xB7F70   b14b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xB7F7A)
0xB7F72   0122       MOVS    R2, #1
0xB7F74   0790       STR     R0, [SP,#0xF0+var_D4]
0xB7F76   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xB7F78   6846       MOV     R0, SP
0xB7F7A   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xB7F7C   1a90       STR     R0, [SP,#0xF0+var_88]
0xB7F7E   10a8       ADD     R0, SP, #0xF0+fctx; lpfctx
0xB7F80   1693       STR     R3, [SP,#0xF0+fctx.personality]
0xB7F82   ae4b       LDR     R3, =(GCC_except_table47 - 0xB7F8A)
0xB7F84   1897       STR     R7, [SP,#0xF0+fctx.jbuf]
0xB7F86   7b44       ADD     R3, PC; GCC_except_table47
0xB7F88   1793       STR     R3, [SP,#0xF0+fctx.lsda]
0xB7F8A   ad4b       LDR     R3, =(loc_B814E - 0xB7F90)
0xB7F8C   7b44       ADD     R3, PC; loc_B814E
0xB7F8E   1343       ORRS    R3, R2
```

**Around `0xB7FDC` (cited by the Unity code)**

```
0xB7FD0   9e4c       LDR     R4, =0x3F800000
0xB7FD2   0994       STR     R4, [SP,#0xF0+var_CC]
0xB7FD4   0123       MOVS    R3, #1
0xB7FD6   5b42       NEGS    R3, R3
0xB7FD8   0798       LDR     R0, [SP,#0xF0+var_D4]; this
0xB7FDA   1193       STR     R3, [SP,#0xF0+fctx.call_site]
0xB7FDC   ddf717fb   BL      __ZN7DinoCap9mainScene15StopCreateEnemyEv; DinoCap::mainScene::StopCreateEnemy(void)   <==
0xB7FE0   9b4b       LDR     R3, =(__ZN7DinoCap13g_player_typeE_ptr - 0xB7FE6)
0xB7FE2   7b44       ADD     R3, PC; __ZN7DinoCap13g_player_typeE_ptr
0xB7FE4   1b68       LDR     R3, [R3]; DinoCap::g_player_type ...
0xB7FE6   1b68       LDR     R3, [R3]; DinoCap::g_player_type
0xB7FE8   042b       CMP     R3, #4
0xB7FEA   30d0       BEQ     loc_B804E
```

**Around `0xB7FEA` (cited by the Unity code)**

```
0xB7FDC   ddf717fb   BL      __ZN7DinoCap9mainScene15StopCreateEnemyEv; DinoCap::mainScene::StopCreateEnemy(void)
0xB7FE0   9b4b       LDR     R3, =(__ZN7DinoCap13g_player_typeE_ptr - 0xB7FE6)
0xB7FE2   7b44       ADD     R3, PC; __ZN7DinoCap13g_player_typeE_ptr
0xB7FE4   1b68       LDR     R3, [R3]; DinoCap::g_player_type ...
0xB7FE6   1b68       LDR     R3, [R3]; DinoCap::g_player_type
0xB7FE8   042b       CMP     R3, #4
0xB7FEA   30d0       BEQ     loc_B804E   <==
0xB7FEC   0799       LDR     R1, [SP,#0xF0+var_D4]
0xB7FEE   c86a       LDR     R0, [R1,#0x2C]; this
0xB7FF0   b3f0b0ff   BL      __ZN9Triniti2D10GameObject11GetCallBackEv; Triniti2D::GameObject::GetCallBack(void)
0xB7FF4   0028       CMP     R0, #0
0xB7FF6   02d1       BNE     loc_B7FFE
0xB7FF8   0022       MOVS    R2, #0
```

