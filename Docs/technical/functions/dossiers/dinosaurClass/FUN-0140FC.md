# dinosaurClass::Die

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0140FC` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x140FC` |
| RVA (EA - 0x1000) | `0x130FC` |
| File offset | `0x130FC` |
| Size | 724 code bytes in 1 chunk(s), 309 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap13dinosaurClass3DieEPNS_11weaponClassE` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::dinosaurClass::Die(DinoCap::weaponClass *)` |
| Bytes SHA-256 | `1bdb981141c4f56164b1a6efae34a24815af095f10acf10d015e67b9c0a6806e` |
| Direct code callers | 2 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / VERIFIED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/DebugMenu.cs:40`](../../../../../Assets/Scripts/DebugMenu.cs#L40) (by name)
- [`Assets/Scripts/GameBlood.cs:123`](../../../../../Assets/Scripts/GameBlood.cs#L123) cites `0x140FC`
- [`Assets/Scripts/GameCombat.cs:808`](../../../../../Assets/Scripts/GameCombat.cs#L808) cites `0x140FC`
- [`Assets/Scripts/GameCombat.cs:831`](../../../../../Assets/Scripts/GameCombat.cs#L831) cites `0x140FC`
- [`Assets/Scripts/GameCombat.cs:848`](../../../../../Assets/Scripts/GameCombat.cs#L848) cites `0x140FC`
- [`Assets/Scripts/GameCombat.cs:856`](../../../../../Assets/Scripts/GameCombat.cs#L856) cites `0x140FC`
- [`Assets/Scripts/GameControls.cs:333`](../../../../../Assets/Scripts/GameControls.cs#L333) (by name)
- [`Assets/Scripts/GameItems.cs:22`](../../../../../Assets/Scripts/GameItems.cs#L22) cites `0x140FC`
- [`Assets/Scripts/GameMission.cs:101`](../../../../../Assets/Scripts/GameMission.cs#L101) (by name)

## Callers and callees (direct code references)

Callers: [`dinosaurClass::BeJinAttacked`](../../../functions/dossiers/dinosaurClass/FUN-01442A.md), [`dinosaurClass::BeAttacked`](../../../functions/dossiers/dinosaurClass/FUN-014918.md)

Callees: [`CAObjCallBack::playAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00A064.md), [`dinosaurClass::DieShaoSi`](../../../functions/dossiers/dinosaurClass/FUN-010A4C.md), [`dinosaurClass::DieTouFei`](../../../functions/dossiers/dinosaurClass/FUN-010CBC.md), [`dinosaurClass::BlastSuiRou`](../../../functions/dossiers/dinosaurClass/FUN-01226C.md), [`dinosaurClass::BlastBlood`](../../../functions/dossiers/dinosaurClass/FUN-012756.md), [`dinosaurClass::DieNormal`](../../../functions/dossiers/dinosaurClass/FUN-012CA8.md), [`mainScene::UpdateKillDinocap`](../../../functions/dossiers/mainScene/FUN-094C1E.md), [`lifeModeScene::DropItem`](../../../functions/dossiers/lifeModeScene/FUN-0E1470.md), [`lifeModeScene::UpdateKillDinocap`](../../../functions/dossiers/lifeModeScene/FUN-0E1E62.md), `mapClass::~mapClass`, `Triniti2D::GameObject::Enable`, `Triniti2D::GameObject::GetPosition`, `Triniti2D::GameObject::GetScene`, `Triniti2D::GameObject::Physics`, `Triniti2D::GameObject::SetTimer`, `Triniti2D::GamePhysics::SetLinearVelocity`, `Triniti2D::Scene::GetCallBack`, `___switch32`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `___addsf3vfp`, `___dynamic_cast`, `___floatsisfvfp`, `___modsi3`

## Strings it references

- `0x201EA0` "Die"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x223C25` GCC_except_table12
- `0x22A290` __ZN7DinoCap9life_ModeE_ptr
- `0x23E09C` DinoCap::life_Mode
- `0x22A0C0` __ZTIN9Triniti2D13SceneCallBackE_ptr
- `0x22A274` __ZTIN7DinoCap13lifeModeSceneE_ptr
- `0x22CF10` `typeinfo for'DinoCap::lifeModeScene
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22A280` __ZN7DinoCap10g_map_infoE_ptr
- `0x2404EC` DinoCap::g_map_info
- `0x22A25C` __ZN7DinoCap14g_cur_map_dataE_ptr
- `0x23B87C` DinoCap::g_cur_map_data
- `0x22A270` __ZN7DinoCap10g_curMapIDE_ptr
- `0x23B878` DinoCap::g_curMapID
- `0x22A28C` __ZN7DinoCap8mapClassC2ERKS0__ptr
- `0x22A2AC` __ZN9Triniti2D11RandomRangeEii_ptr
- `0x22A2B8` __ZN7DinoCap13g_player_typeE_ptr
- `0x23B86C` DinoCap::g_player_type
- `0x22A27C` __ZN7DinoCap14g_p1_kill_dinoE_ptr
- `0x23B8FC` DinoCap::g_p1_kill_dino
- `0x22A2DC` __ZN7DinoCap13g_prop_PlayerE_ptr
- `0x240474` DinoCap::g_prop_Player
- `0x22A0C8` __ZTIN7DinoCap9mainSceneE_ptr
- `0x22C868` `typeinfo for'DinoCap::mainScene

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x140FC   f0b5       PUSH    {R4-R7,LR}
0x140FE   5e46       MOV     R6, R11
0x14100   5546       MOV     R5, R10
0x14102   4446       MOV     R4, R8
0x14104   70b4       PUSH    {R4-R6}
0x14106   d3f1ecee   BLX     ___save_vfp_d8_d15_regs
0x1410A   16af       ADD     R7, SP, #arg_38
0x1410C   acb0       SUB     SP, SP, #0xB0
0x1410E   b04b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x14118)
0x14110   0122       MOVS    R2, #1
0x14112   0590       STR     R0, [SP,#0xB0+var_9C]
0x14114   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x14116   0491       STR     R1, [SP,#0xB0+var_A0]
0x14118   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x1411A   6946       MOV     R1, SP
0x1411C   08a8       ADD     R0, SP, #0xB0+fctx; lpfctx
0x1411E   0e93       STR     R3, [SP,#0xB0+fctx.personality]
0x14120   ac4b       LDR     R3, =(GCC_except_table12 - 0x1412A)
0x14122   1097       STR     R7, [SP,#0xB0+fctx.jbuf]
0x14124   1291       STR     R1, [SP,#0xB0+var_68]
0x14126   7b44       ADD     R3, PC; GCC_except_table12
0x14128   0f93       STR     R3, [SP,#0xB0+fctx.lsda]
0x1412A   ab4b       LDR     R3, =(loc_141E6 - 0x14130)
0x1412C   7b44       ADD     R3, PC; loc_141E6
```

