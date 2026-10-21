# lifeModeScene::AwardItem

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0D09DC` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xD09DC` |
| RVA (EA - 0x1000) | `0xCF9DC` |
| File offset | `0xCF9DC` |
| Size | 724 code bytes in 1 chunk(s), 341 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap13lifeModeScene9AwardItemEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::lifeModeScene::AwardItem(void)` |
| Bytes SHA-256 | `d0840be2a944d29d41f1f0e64f8f834b99c8e476a7a62b2576976b0138e4cbd1` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / VERIFIED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/Dc2Scene.cs:439`](../../../../../Assets/Scripts/Dc2Scene.cs#L439) cites `0xD09DC`
- [`Assets/Scripts/GameMission.cs:10`](../../../../../Assets/Scripts/GameMission.cs#L10) cites `0xD09DC`
- [`Assets/Scripts/GameMission.cs:158`](../../../../../Assets/Scripts/GameMission.cs#L158) cites `0xD09DC`
- [`Assets/Scripts/GameMission.cs:166`](../../../../../Assets/Scripts/GameMission.cs#L166) cites `0xD09DC`
- [`Assets/Scripts/GameMission.cs:174`](../../../../../Assets/Scripts/GameMission.cs#L174) cites `0xD09DC`

## Callers and callees (direct code references)

Callers: [`lifeModeScene::missionComplete`](../../../functions/dossiers/lifeModeScene/FUN-0E1704.md)

Callees: `GCSubmitAchievement`, `subWeaponClass::subWeaponClass`, `subWeaponClass::~subWeaponClass`, [`lifeModeScene::AddBullet`](../../../functions/dossiers/lifeModeScene/FUN-0CFF3A.md), `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`

## Strings it references

- `0x207470` "dinocap2hd.a5"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x22756A` GCC_except_table11_7
- `0x22A280` __ZN7DinoCap10g_map_infoE_ptr
- `0x2404EC` DinoCap::g_map_info
- `0x22A25C` __ZN7DinoCap14g_cur_map_dataE_ptr
- `0x23B87C` DinoCap::g_cur_map_data
- `0x22A270` __ZN7DinoCap10g_curMapIDE_ptr
- `0x23B878` DinoCap::g_curMapID
- `0x22A2DC` __ZN7DinoCap13g_prop_PlayerE_ptr
- `0x240474` DinoCap::g_prop_Player
- `0x22A2B8` __ZN7DinoCap13g_player_typeE_ptr
- `0x23B86C` DinoCap::g_player_type
- `0x22A24C` __ZN7DinoCap15g_player_vectorE_ptr
- `0x2404AC` DinoCap::g_player_vector

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xD09DC   f0b5       PUSH    {R4-R7,LR}
0xD09DE   5e46       MOV     R6, R11
0xD09E0   5546       MOV     R5, R10
0xD09E2   4446       MOV     R4, R8
0xD09E4   70b4       PUSH    {R4-R6}
0xD09E6   17f17cea   BLX     ___save_vfp_d8_d15_regs
0xD09EA   16af       ADD     R7, SP, #arg_38
0xD09EC   9cb0       SUB     SP, SP, #0x70 ; 'p'
0xD09EE   b04b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xD09F8)
0xD09F0   0122       MOVS    R2, #1
0xD09F2   0490       STR     R0, [SP,#0x70+var_60]
0xD09F4   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xD09F6   6846       MOV     R0, SP
0xD09F8   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xD09FA   1090       STR     R0, [SP,#0x70+var_30]
0xD09FC   06a8       ADD     R0, SP, #0x70+fctx; lpfctx
0xD09FE   0c93       STR     R3, [SP,#0x70+fctx.personality]
0xD0A00   ac4b       LDR     R3, =(GCC_except_table11_7 - 0xD0A08)
0xD0A02   0e97       STR     R7, [SP,#0x70+fctx.jbuf]
0xD0A04   7b44       ADD     R3, PC; GCC_except_table11_7
0xD0A06   0d93       STR     R3, [SP,#0x70+fctx.lsda]
0xD0A08   ab4b       LDR     R3, =(loc_D0C52 - 0xD0A0E)
0xD0A0A   7b44       ADD     R3, PC; loc_D0C52
0xD0A0C   1343       ORRS    R3, R2
```

