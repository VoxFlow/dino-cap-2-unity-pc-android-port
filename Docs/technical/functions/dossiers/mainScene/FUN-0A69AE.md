# mainScene::OnUIEvent

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0A69AE` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xA69AE` |
| RVA (EA - 0x1000) | `0xA59AE` |
| File offset | `0xA59AE` |
| Size | 1380 code bytes in 1 chunk(s), 527 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9mainScene9OnUIEventEPN9Triniti2D9UIControlEiff` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::mainScene::OnUIEvent(Triniti2D::UIControl *,int,float,float)` |
| Bytes SHA-256 | `9f1bb47b8517b5691e78169dc438e371f04e1062138024269662e228cc8bbfcd` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameControls.cs:398`](../../../../../Assets/Scripts/GameControls.cs#L398) cites `0xA6A9E`
- [`Assets/Scripts/GameIntro.cs:28`](../../../../../Assets/Scripts/GameIntro.cs#L28) cites `0xA6AB8`
- [`Assets/Scripts/GameIntro.cs:131`](../../../../../Assets/Scripts/GameIntro.cs#L131) cites `0xA69AE`
- [`Assets/Scripts/GameIntro.cs:159`](../../../../../Assets/Scripts/GameIntro.cs#L159) cites `0xA6A9E`

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: `GetSystemTime_Second`, `TadHide`, `SaveMissionInfo`, [`mainScene::StartGame_N`](../../../functions/dossiers/mainScene/FUN-094F68.md), [`mainScene::missionComplete`](../../../functions/dossiers/mainScene/FUN-0A26E0.md), [`mainScene::LevelEnter`](../../../functions/dossiers/mainScene/FUN-0A348E.md), [`mainScene::LevelBGLeave`](../../../functions/dossiers/mainScene/FUN-0A38C2.md), [`mainScene::MissCompEnter`](../../../functions/dossiers/mainScene/FUN-0A3DF0.md), [`mainScene::MissCompEnd`](../../../functions/dossiers/mainScene/FUN-0A4124.md), `mainScene::GameOverEnd`, [`mainScene::FailZEnter`](../../../functions/dossiers/mainScene/FUN-0A64CE.md), [`mainScene::MissionFailEnter`](../../../functions/dossiers/mainScene/FUN-0A671E.md), [`pauseCtrl::Leave`](../../../functions/dossiers/pauseCtrl/FUN-0CE93C.md), `Triniti2D::AudioManager::Stop`, `Triniti2D::Game::SwitchScene`, `Triniti2D::GameObject::GetPosition`, `Triniti2D::GameObject::SetPosition`, `Triniti2D::Scene::SetTimeFactor`, `Triniti2D::Scene::GetUIManager`, `Triniti2D::Scene::SetTimer`, `Triniti2D::UIControl::Id`, `Triniti2D::UIManager::DeleteControl`, `___switch32`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___dynamic_cast`

## Strings it references

- `0x207C18` "LevelLeave"
- `0x207C24` "DieEnd"
- `0x2077B4` "game_bgAudio"
- `0x2049B4` "selectScene"
- `0x207C2C` "CompText"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x226AA5` GCC_except_table41
- `0x22A6EC` __ZN7DinoCap13g_b_RightDownE_ptr
- `0x23B8C1` DinoCap::g_b_RightDown
- `0x22A338` __ZN9Triniti2D13_AudioManagerE_ptr
- `0x23BBAC` Triniti2D::_AudioManager
- `0x22A4AC` __ZN7DinoCap10m_map_typeE_ptr
- `0x23E0A0` DinoCap::m_map_type
- `0x22A434` __ZN9Triniti2D5_GameE_ptr
- `0x23BB94` Triniti2D::_Game
- `0x22A5F4` __ZTIN9Triniti2D9UIControlE_ptr
- `0x22E468` `typeinfo for'Triniti2D::UIControl
- `0x22A6E8` __ZTIN7DinoCap9pauseCtrlE_ptr
- `0x22CE48` `typeinfo for'DinoCap::pauseCtrl
- `0x2415E8` `vtable for'__cxxabiv1::__class_type_info
- `0x2415F0` `vtable for'__cxxabiv1::__vmi_class_type_info
- `0x22A280` __ZN7DinoCap10g_map_infoE_ptr
- `0x2404EC` DinoCap::g_map_info
- `0x22A25C` __ZN7DinoCap14g_cur_map_dataE_ptr
- `0x23B87C` DinoCap::g_cur_map_data
- `0x22A270` __ZN7DinoCap10g_curMapIDE_ptr
- `0x23B878` DinoCap::g_curMapID
- `0x22A5CC` __ZN7DinoCap15g_mission_stateE_ptr
- `0x23E148` DinoCap::g_mission_state
- `0x22A6C0` __ZN7DinoCap12g_total_timeE_ptr
- `0x240A7C` DinoCap::g_total_time
- `0x22A700` __ZN7DinoCap14t_cur_sys_timeE_ptr
- `0x240A80` DinoCap::t_cur_sys_time

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xA69AE   f0b5       PUSH    {R4-R7,LR}
0xA69B0   5e46       MOV     R6, R11
0xA69B2   5546       MOV     R5, R10
0xA69B4   4446       MOV     R4, R8
0xA69B6   70b4       PUSH    {R4-R6}
0xA69B8   41f192ea   BLX     ___save_vfp_d8_d15_regs
0xA69BC   16af       ADD     R7, SP, #arg_38
0xA69BE   9bb0       SUB     SP, SP, #0x6C ; 'l'
0xA69C0   c94b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xA69CA)
0xA69C2   0292       STR     R2, [SP,#0x6C+var_64]
0xA69C4   0122       MOVS    R2, #1
0xA69C6   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xA69C8   0490       STR     R0, [SP,#0x6C+var_5C]
0xA69CA   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xA69CC   0391       STR     R1, [SP,#0x6C+lpsrc]
0xA69CE   08a8       ADD     R0, SP, #0x6C+fctx; lpfctx
0xA69D0   0e93       STR     R3, [SP,#0x6C+fctx.personality]
0xA69D2   c64b       LDR     R3, =(GCC_except_table41 - 0xA69DC)
0xA69D4   6946       MOV     R1, SP
0xA69D6   1291       STR     R1, [SP,#0x6C+var_24]
0xA69D8   7b44       ADD     R3, PC; GCC_except_table41
0xA69DA   0f93       STR     R3, [SP,#0x6C+fctx.lsda]
0xA69DC   c44b       LDR     R3, =(loc_A6EBC - 0xA69E4)
0xA69DE   1097       STR     R7, [SP,#0x6C+fctx.jbuf]
```

**Around `0xA6A9E` (cited by the Unity code)**

```
0xA6A8E   0123       MOVS    R3, #1
0xA6A90   5b42       NEGS    R3, R3
0xA6A92   0993       STR     R3, [SP,#0x6C+fctx.call_site]
0xA6A94   cef0e9fb   BL      __ZN9Triniti2D5Scene12GetUIManagerEv; Triniti2D::Scene::GetUIManager(void)
0xA6A98   0c21       MOVS    R1, #0xC; int
0xA6A9A   daf0e1fc   BL      __ZN9Triniti2D9UIManager13DeleteControlEi; Triniti2D::UIManager::DeleteControl(int)
0xA6A9E   0499       LDR     R1, [SP,#0x6C+var_5C]   <==
0xA6AA0   b423       MOVS    R3, #0xB4
0xA6AA2   0122       MOVS    R2, #1
0xA6AA4   c86a       LDR     R0, [R1,#0x2C]; this
0xA6AA6   ca54       STRB    R2, [R1,R3]
0xA6AA8   c5f04cfa   BL      __ZN9Triniti2D10GameObject11GetPositionEv; Triniti2D::GameObject::GetPosition(void)
0xA6AAC   914b       LDR     R3, =(__ZN7DinoCap13g_b_RightDownE_ptr - 0xA6AB4)
```

**Around `0xA6AB8` (cited by the Unity code)**

```
0xA6AAC   914b       LDR     R3, =(__ZN7DinoCap13g_b_RightDownE_ptr - 0xA6AB4)
0xA6AAE   0022       MOVS    R2, #0
0xA6AB0   7b44       ADD     R3, PC; __ZN7DinoCap13g_b_RightDownE_ptr
0xA6AB2   1b68       LDR     R3, [R3]; DinoCap::g_b_RightDown ...
0xA6AB4   4168       LDR     R1, [R0,#4]
0xA6AB6   0068       LDR     R0, [R0]
0xA6AB8   1a70       STRB    R2, [R3]; DinoCap::g_b_RightDown   <==
0xA6ABA   049a       LDR     R2, [SP,#0x6C+var_5C]
0xA6ABC   1590       STR     R0, [SP,#0x6C+var_18]
0xA6ABE   1691       STR     R1, [SP,#0x6C+var_14]
0xA6AC0   15a9       ADD     R1, SP, #0x6C+var_18
0xA6AC2   d06a       LDR     R0, [R2,#0x2C]
0xA6AC4   c5f0bffa   BL      __ZN9Triniti2D10GameObject11SetPositionERKNS_7Vector2E; Triniti2D::GameObject::SetPosition(Triniti2D::Vector2 const&)
```

