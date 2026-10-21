# lifeModeScene::OnTimeout

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0E2A4C` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xE2A4C` |
| RVA (EA - 0x1000) | `0xE1A4C` |
| File offset | `0xE1A4C` |
| Size | 1350 code bytes in 1 chunk(s), 552 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap13lifeModeScene9OnTimeoutEiPv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::lifeModeScene::OnTimeout(int,void *)` |
| Bytes SHA-256 | `fcee1d2284efe428fd66a6ef98c4c4de7a8b013b68f615e04f475af4964754f3` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameBoss.cs:131`](../../../../../Assets/Scripts/GameBoss.cs#L131) (by name)
- [`Assets/Scripts/GameMission.cs:79`](../../../../../Assets/Scripts/GameMission.cs#L79) cites `0xE2F44`
- [`Assets/Scripts/GameMission.cs:297`](../../../../../Assets/Scripts/GameMission.cs#L297) (by name)
- [`Assets/Scripts/GameWeather.cs:20`](../../../../../Assets/Scripts/GameWeather.cs#L20) cites `0xE2A4C`
- [`Assets/Scripts/GameWeather.cs:165`](../../../../../Assets/Scripts/GameWeather.cs#L165) cites `0xE2BF2`

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: [`lifeModeScene::SubTime`](../../../functions/dossiers/lifeModeScene/FUN-0D0ED6.md), [`lifeModeScene::AddBoss`](../../../functions/dossiers/lifeModeScene/FUN-0D105A.md), [`lifeModeScene::AddYezi`](../../../functions/dossiers/lifeModeScene/FUN-0E1040.md), [`lifeModeScene::AddDian`](../../../functions/dossiers/lifeModeScene/FUN-0E1304.md), [`lifeModeScene::AddShanDian`](../../../functions/dossiers/lifeModeScene/FUN-0E20C0.md), [`lifeModeScene::MissCompMove`](../../../functions/dossiers/lifeModeScene/FUN-0E28AC.md), `sub_E3BDC`, `sub_E3BF8`, `Triniti2D::AudioManager::Stop`, `Triniti2D::Game::SwitchScene`, `Triniti2D::Scene::GetUIManager`, `Triniti2D::UIImage::SetColor`, `Triniti2D::UIImage::SetTexture`, `Triniti2D::UIManager::CreateImage`, `Triniti2D::UIManager::DeleteControl`, `___save_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `std::string::compare`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___mulsf3vfp`, `___subsf3vfp`

## Strings it references

- `0x207C18` "LevelLeave"
- `0x2096AC` "CompTextEnter"
- `0x2079B0` "Yezi"
- `0x2096BC` "AddBoss"
- `0x207458` "maskWhite"
- `0x206D44` "shandian02Frame"
- `0x2079B8` "AddShanDian"
- `0x207464` "ShanDian"
- `0x207C24` "DieEnd"
- `0x2077B4` "game_bgAudio"
- `0x2049B4` "selectScene"
- `0x20741C` "g_time_id"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x227A87` GCC_except_table31_2
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A338` __ZN9Triniti2D13_AudioManagerE_ptr
- `0x23BBAC` Triniti2D::_AudioManager
- `0x22A4AC` __ZN7DinoCap10m_map_typeE_ptr
- `0x23E0A0` DinoCap::m_map_type
- `0x22A434` __ZN9Triniti2D5_GameE_ptr
- `0x23BB94` Triniti2D::_Game
- `0x22A5F0` __ZN7DinoCap6g_timeE_ptr
- `0x23E098` DinoCap::g_time
- `0x22A280` __ZN7DinoCap10g_map_infoE_ptr
- `0x2404EC` DinoCap::g_map_info
- `0x22A25C` __ZN7DinoCap14g_cur_map_dataE_ptr
- `0x23B87C` DinoCap::g_cur_map_data
- `0x22A270` __ZN7DinoCap10g_curMapIDE_ptr
- `0x23B878` DinoCap::g_curMapID
- `0x22A484` __ZN7DinoCap14g_lifemodetimeE_ptr
- `0x23E080` DinoCap::g_lifemodetime

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xE2A4C   f0b5       PUSH    {R4-R7,LR}
0xE2A4E   5e46       MOV     R6, R11
0xE2A50   5546       MOV     R5, R10
0xE2A52   4446       MOV     R4, R8
0xE2A54   70b4       PUSH    {R4-R6}
0xE2A56   05f144ea   BLX     ___save_vfp_d8_d15_regs
0xE2A5A   16af       ADD     R7, SP, #arg_38
0xE2A5C   c6b0       SUB     SP, SP, #0x118; int
0xE2A5E   d64b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xE2A68)
0xE2A60   0192       STR     R2, [SP,#0x118+var_114]; int
0xE2A62   0122       MOVS    R2, #1
0xE2A64   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xE2A66   0390       STR     R0, [SP,#0x118+var_10C]; int
0xE2A68   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xE2A6A   0291       STR     R1, [SP,#0x118+var_110]; int
0xE2A6C   26a8       ADD     R0, SP, #0x118+fctx; lpfctx
0xE2A6E   2c93       STR     R3, [SP,#0x118+fctx.personality]
0xE2A70   d24b       LDR     R3, =(GCC_except_table31_2 - 0xE2A7A)
0xE2A72   6946       MOV     R1, SP
0xE2A74   2e97       STR     R7, [SP,#0x118+fctx.jbuf]
0xE2A76   7b44       ADD     R3, PC; GCC_except_table31_2
0xE2A78   2d93       STR     R3, [SP,#0x118+fctx.lsda]
0xE2A7A   d14b       LDR     R3, =(loc_E2EB8 - 0xE2A82)
0xE2A7C   3091       STR     R1, [SP,#0x118+var_58]
```

**Around `0xE2BF2` (cited by the Unity code)**

```
0xE2BE4   0d23       MOVS    R3, #0xD
0xE2BE6   2793       STR     R3, [SP,#0x118+fctx.call_site]
0xE2BE8   7944       ADD     R1, PC; "maskWhite"
0xE2BEA   3ea8       ADD     R0, SP, #0x118+var_20; this
0xE2BEC   06f16eef   BLX     __ZNKSs7compareEPKc; std::string::compare(char const*)
0xE2BF0   0e21       MOVS    R1, #0xE
0xE2BF2   0990       STR     R0, [SP,#0x118+var_F4]; int   <==
0xE2BF4   2791       STR     R1, [SP,#0x118+fctx.call_site]
0xE2BF6   3ea8       ADD     R0, SP, #0x118+var_20; this
0xE2BF8   06f1daef   BLX     __ZNSsD2Ev; std::string::~string()
0xE2BFC   03e0       B       loc_E2C06
0xE2BFE   0023       MOVS    R3, #0
0xE2C00   3ea8       ADD     R0, SP, #0x118+var_20
```

**Around `0xE2F44` (cited by the Unity code)**

```
0xE2F36   06f194ed   BLX     __Unwind_SjLj_Resume
0xE2F3A   0f9b       LDR     R3, [SP,#0x118+var_DC]
0xE2F3C   002b       CMP     R3, #0
0xE2F3E   3cd1       BNE     loc_E2FBA
0xE2F40   964b       LDR     R3, =(__ZN7DinoCap6g_timeE_ptr - 0xE2F46)
0xE2F42   7b44       ADD     R3, PC; __ZN7DinoCap6g_timeE_ptr
0xE2F44   1a68       LDR     R2, [R3]; DinoCap::g_time ...   <==
0xE2F46   1368       LDR     R3, [R2]; DinoCap::g_time
0xE2F48   0133       ADDS    R3, #1
0xE2F4A   1360       STR     R3, [R2]; DinoCap::g_time
0xE2F4C   944b       LDR     R3, =(__ZN7DinoCap10g_map_infoE_ptr - 0xE2F52)
0xE2F4E   7b44       ADD     R3, PC; __ZN7DinoCap10g_map_infoE_ptr
0xE2F50   1968       LDR     R1, [R3]; DinoCap::g_map_info ...
```

