# mainScene::missionComplete

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0A26E0` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xA26E0` |
| RVA (EA - 0x1000) | `0xA16E0` |
| File offset | `0xA16E0` |
| Size | 1608 code bytes in 1 chunk(s), 670 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9mainScene15missionCompleteEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::mainScene::missionComplete(void)` |
| Bytes SHA-256 | `50a474d5c2cdce5de75ee35143bd70b2d824a843f196d825df4a32aaf4837e1e` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameMission.cs:165`](../../../../../Assets/Scripts/GameMission.cs#L165) cites `0xA2C92`
- [`Assets/Scripts/GameMission.cs:169`](../../../../../Assets/Scripts/GameMission.cs#L169) cites `0xA2CF6`

## Callers and callees (direct code references)

Callers: [`mainScene::OnUIEvent`](../../../functions/dossiers/mainScene/FUN-0A69AE.md)

Callees: `GetSystemTime_Second`, `SaveFile`, `SaveMissionInfo`, `GCSubmitScore`, `GCSubmitAchievement`, [`mainScene::AwardItem`](../../../functions/dossiers/mainScene/FUN-0963F0.md), [`mainScene::getJigsaw`](../../../functions/dossiers/mainScene/FUN-096682.md), `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___fixsfsivfp`, `___floatsisfvfp`, `___mulsf3vfp`

## Strings it references

- `0x2079C4` "dinocap2hd.l1"
- `0x2079D4` "dinocap2hd.a1"
- `0x2079E4` "dinocap2hd.a6"
- `0x2079F4` "dinocap2hd.a10"
- `0x207A04` "dinocap2hd.a2"
- `0x207A14` "dinocap2hd.a4"
- `0x207A24` "dinocap2hd.a3"
- `0x207A34` "dinocap2hd.a8"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x22699E` GCC_except_table32_0
- `0x22A2B8` __ZN7DinoCap13g_player_typeE_ptr
- `0x23B86C` DinoCap::g_player_type
- `0x22A694` __ZN7DinoCap12g_jigsaw_getE_ptr
- `0x23E0A8` DinoCap::g_jigsaw_get
- `0x22A0F4` __ZNSt6vectorIN9Triniti2D7Vector2ESaIS1_EE9push_backERKS1__ptr
- `0x22A108` __ZNKSt6vectorIN9Triniti2D7Vector2ESaIS1_EE4sizeEv_ptr
- `0x22A6E4` __ZN7DinoCap9g_missionE_ptr
- `0x23B8CC` DinoCap::g_mission
- `0x22A2AC` __ZN9Triniti2D11RandomRangeEii_ptr
- `0x22A614` __ZN7DinoCap10jigsawSeedE_ptr
- `0x23B8E0` DinoCap::jigsawSeed
- `0x22A0FC` __ZNSt6vectorIN9Triniti2D7Vector2ESaIS1_EED2Ev_ptr
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
- `0x22A30C` __ZNSt6vectorIbSaIbEEixEm_ptr
- `0x22A730` __ZN7DinoCap14g_p4_kill_dinoE_ptr
- `0x23B8F8` DinoCap::g_p4_kill_dino

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xA26E0   f0b5       PUSH    {R4-R7,LR}
0xA26E2   5e46       MOV     R6, R11
0xA26E4   5546       MOV     R5, R10
0xA26E6   4446       MOV     R4, R8
0xA26E8   70b4       PUSH    {R4-R6}
0xA26EA   45f1faeb   BLX     ___save_vfp_d8_d15_regs
0xA26EE   16af       ADD     R7, SP, #arg_38
0xA26F0   b0b0       SUB     SP, SP, #0xC0
0xA26F2   e84b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xA26FC)
0xA26F4   0e90       STR     R0, [SP,#0xC0+var_88]
0xA26F6   0122       MOVS    R2, #1
0xA26F8   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xA26FA   6846       MOV     R0, SP
0xA26FC   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xA26FE   1c90       STR     R0, [SP,#0xC0+var_50]
0xA2700   12a8       ADD     R0, SP, #0xC0+fctx; lpfctx
0xA2702   1893       STR     R3, [SP,#0xC0+fctx.personality]
0xA2704   e44b       LDR     R3, =(GCC_except_table32_0 - 0xA270C)
0xA2706   1a97       STR     R7, [SP,#0xC0+fctx.jbuf]
0xA2708   7b44       ADD     R3, PC; GCC_except_table32_0
0xA270A   1993       STR     R3, [SP,#0xC0+fctx.lsda]
0xA270C   e34b       LDR     R3, =(loc_A2BD2 - 0xA2712)
0xA270E   7b44       ADD     R3, PC; loc_A2BD2
0xA2710   1343       ORRS    R3, R2
```

**Around `0xA2C92` (cited by the Unity code)**

```
0xA2C84   1b68       LDR     R3, [R3]; DinoCap::g_prop_Player ...
0xA2C86   8850       STR     R0, [R1,R2]
0xA2C88   0493       STR     R3, [SP,#0xC0+var_B0]
0xA2C8A   47f14ce8   BLX     ___fixsfsivfp
0xA2C8E   0499       LDR     R1, [SP,#0xC0+var_B0]
0xA2C90   0e9a       LDR     R2, [SP,#0xC0+var_88]
0xA2C92   8b68       LDR     R3, [R1,#(dword_24047C - 0x240474)]   <==
0xA2C94   c018       ADDS    R0, R0, R3
0xA2C96   8860       STR     R0, [R1,#(dword_24047C - 0x240474)]
0xA2C98   0598       LDR     R0, [SP,#0xC0+var_AC]
0xA2C9A   2c4b       LDR     R3, =0
0xA2C9C   1350       STR     R3, [R2,R0]
0xA2C9E   0023       MOVS    R3, #0
```

**Around `0xA2CF6` (cited by the Unity code)**

```
0xA2CEA   1a60       STR     R2, [R3]; DinoCap::m_map_type
0xA2CEC   1c4b       LDR     R3, =(__ZN7DinoCap13g_prop_PlayerE_ptr - 0xA2CF2)
0xA2CEE   7b44       ADD     R3, PC; __ZN7DinoCap13g_prop_PlayerE_ptr
0xA2CF0   1a68       LDR     R2, [R3]; DinoCap::g_prop_Player ...
0xA2CF2   1c4b       LDR     R3, =(__ZN7DinoCap13g_player_typeE_ptr - 0xA2CF8)
0xA2CF4   7b44       ADD     R3, PC; __ZN7DinoCap13g_player_typeE_ptr
0xA2CF6   d16a       LDR     R1, [R2,#(dword_2404A0 - 0x240474)]   <==
0xA2CF8   1b68       LDR     R3, [R3]; DinoCap::g_player_type ...
0xA2CFA   1b68       LDR     R3, [R3]; DinoCap::g_player_type
0xA2CFC   9a00       LSLS    R2, R3, #2
0xA2CFE   1a4b       LDR     R3, =(__ZN7DinoCap16g_manager_WeaponE_ptr - 0xA2D04)
0xA2D00   7b44       ADD     R3, PC; __ZN7DinoCap16g_manager_WeaponE_ptr
0xA2D02   1b68       LDR     R3, [R3]; DinoCap::g_manager_Weapon ...
```

