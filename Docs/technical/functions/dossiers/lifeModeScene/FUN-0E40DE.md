# lifeModeScene::MissionFailEnter

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0E40DE` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xE40DE` |
| RVA (EA - 0x1000) | `0xE30DE` |
| File offset | `0xE30DE` |
| Size | 532 code bytes in 1 chunk(s), 245 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap13lifeModeScene16MissionFailEnterEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::lifeModeScene::MissionFailEnter(void)` |
| Bytes SHA-256 | `0a414014c5e49dcc0d074d1455aa7ae78e20bca7ad9c8077e3802dc974dedc96` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameMission.cs:264`](../../../../../Assets/Scripts/GameMission.cs#L264) cites `0xE40DE`
- [`Assets/Scripts/GameMission.cs:270`](../../../../../Assets/Scripts/GameMission.cs#L270) cites `0xE40DE`

## Callers and callees (direct code references)

Callers: [`lifeModeScene::OnUIEvent`](../../../functions/dossiers/lifeModeScene/FUN-0E6550.md)

Callees: [`RectScale`](../../../functions/dossiers/DinoCap/FUN-017068.md), [`RectOffset`](../../../functions/dossiers/DinoCap/FUN-0170B6.md), `GetSystemTime_Second`, `SaveMissionInfo`, [`UIMoveCtrl::StartMove`](../../../functions/dossiers/UIMoveCtrl/FUN-0CD466.md), `Triniti2D::Scene::GetUIManager`, `Triniti2D::UIImage::SetTexture`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___mulsf3vfp`, `___subsf3vfp`

## Strings it references

- `0x207C04` "mission_failedFrame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x227AC7` GCC_except_table34_1
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
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
- `0x22A6C8` __ZN9Triniti2D9UIManager13CreateControlIN7DinoCap10UIMoveCtrlEEEPT_ii_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xE40DE   f0b5       PUSH    {R4-R7,LR}
0xE40E0   5e46       MOV     R6, R11
0xE40E2   5546       MOV     R5, R10
0xE40E4   4446       MOV     R4, R8
0xE40E6   70b4       PUSH    {R4-R6}
0xE40E8   03f1faee   BLX     ___save_vfp_d8_d15_regs
0xE40EC   16af       ADD     R7, SP, #arg_38
0xE40EE   b1b0       SUB     SP, SP, #0xC4
0xE40F0   804b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xE40FA)
0xE40F2   0690       STR     R0, [SP,#0xC4+var_AC]
0xE40F4   0122       MOVS    R2, #1
0xE40F6   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xE40F8   6846       MOV     R0, SP
0xE40FA   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xE40FC   1590       STR     R0, [SP,#0xC4+var_70]
0xE40FE   0ba8       ADD     R0, SP, #0xC4+fctx; lpfctx
0xE4100   1193       STR     R3, [SP,#0xC4+fctx.personality]
0xE4102   7d4b       LDR     R3, =(GCC_except_table34_1 - 0xE410A)
0xE4104   1397       STR     R7, [SP,#0xC4+fctx.jbuf]
0xE4106   7b44       ADD     R3, PC; GCC_except_table34_1
0xE4108   1293       STR     R3, [SP,#0xC4+fctx.lsda]
0xE410A   7c4b       LDR     R3, =(loc_E422C - 0xE4110)
0xE410C   7b44       ADD     R3, PC; loc_E422C
0xE410E   1343       ORRS    R3, R2
```

