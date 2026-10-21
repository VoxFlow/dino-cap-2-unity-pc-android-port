# mainScene::MissionFailEnter

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0A671E` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xA671E` |
| RVA (EA - 0x1000) | `0xA571E` |
| File offset | `0xA571E` |
| Size | 558 code bytes in 1 chunk(s), 256 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9mainScene16MissionFailEnterEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::mainScene::MissionFailEnter(void)` |
| Bytes SHA-256 | `9cc8105bc1afd34997b05d86866f42f4bf592068a0bfae4a470641038272be07` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | GAMEPLAY / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: r118 0xa671e: banner offsets use S*480 / S*(-20) (lifeMode: 480 / -20); FIXED in GameMission.cs

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameMission.cs:270`](../../../../../Assets/Scripts/GameMission.cs#L270) cites `0xA671E`

## Callers and callees (direct code references)

Callers: [`mainScene::OnUIEvent`](../../../functions/dossiers/mainScene/FUN-0A69AE.md)

Callees: [`RectScale`](../../../functions/dossiers/DinoCap/FUN-017068.md), [`RectOffset`](../../../functions/dossiers/DinoCap/FUN-0170B6.md), `GetSystemTime_Second`, `SaveMissionInfo`, [`UIMoveCtrl::StartMove`](../../../functions/dossiers/UIMoveCtrl/FUN-0CD466.md), `Triniti2D::Scene::GetUIManager`, `Triniti2D::UIImage::SetTexture`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___mulsf3vfp`

## Strings it references

- `0x207C04` "mission_failedFrame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x226A9D` GCC_except_table40
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
0xA671E   f0b5       PUSH    {R4-R7,LR}
0xA6720   5e46       MOV     R6, R11
0xA6722   5546       MOV     R5, R10
0xA6724   4446       MOV     R4, R8
0xA6726   70b4       PUSH    {R4-R6}
0xA6728   41f1daeb   BLX     ___save_vfp_d8_d15_regs
0xA672C   16af       ADD     R7, SP, #arg_38
0xA672E   b2b0       SUB     SP, SP, #0xC8
0xA6730   864b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xA673A)
0xA6732   0690       STR     R0, [SP,#0xC8+var_B0]
0xA6734   0122       MOVS    R2, #1
0xA6736   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xA6738   6846       MOV     R0, SP
0xA673A   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xA673C   1690       STR     R0, [SP,#0xC8+var_70]
0xA673E   0ca8       ADD     R0, SP, #0xC8+fctx; lpfctx
0xA6740   1293       STR     R3, [SP,#0xC8+fctx.personality]
0xA6742   834b       LDR     R3, =(GCC_except_table40 - 0xA674A)
0xA6744   1497       STR     R7, [SP,#0xC8+fctx.jbuf]
0xA6746   7b44       ADD     R3, PC; GCC_except_table40
0xA6748   1393       STR     R3, [SP,#0xC8+fctx.lsda]
0xA674A   824b       LDR     R3, =(loc_A6872 - 0xA6750)
0xA674C   7b44       ADD     R3, PC; loc_A6872
0xA674E   1343       ORRS    R3, R2
```

