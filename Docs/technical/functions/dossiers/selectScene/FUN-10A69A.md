# selectScene::showMapPoint

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-10A69A` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x10A69A` |
| RVA (EA - 0x1000) | `0x10969A` |
| File offset | `0x10969A` |
| Size | 2440 code bytes in 1 chunk(s), 1023 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene12showMapPointEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::showMapPoint(void)` |
| Bytes SHA-256 | `46922a6180202458cee2439f9918bef389c55b0283b0cb6e8a81ee78abcb3636` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/NativeMapData.cs:111`](../../../../../Assets/Scripts/NativeMapData.cs#L111) cites `0x10A69A`

## Callers and callees (direct code references)

Callers: [`selectScene::OnUIEvent`](../../../functions/dossiers/selectScene/FUN-10B93A.md)

Callees: [`RectScale`](../../../functions/dossiers/DinoCap/FUN-017068.md), [`RectOffset`](../../../functions/dossiers/DinoCap/FUN-0170B6.md), `BossPointReadFile`, `mapPosProp::~mapPosProp`, `mapPosProp::Init`, `mapPosClass::~mapPosClass`, [`selectScene::getBossPoint`](../../../functions/dossiers/selectScene/FUN-0F4992.md), [`selectScene::startScaleUI`](../../../functions/dossiers/selectScene/FUN-0F77DC.md), [`selectScene::startScaleUI_Point`](../../../functions/dossiers/selectScene/FUN-10A234.md), [`selectScene::showBossPoint`](../../../functions/dossiers/selectScene/FUN-10A2E8.md), `Triniti2D::Console::WriteLine`, `Triniti2D::Scene::GetUIManager`, `Triniti2D::UIImage::SetTexture`, `Triniti2D::UIManager::DeleteControl`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___modsi3`, `___mulsf3vfp`, `___nesf2vfp`, `_sprintf`

## Strings it references

- `0x20AC70` "ui_map_point_type%dFrame"
- `0x20AC8C` "map bg id : %d"
- `0x206F30` "ui_map_point_bgFrame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x2284FF` GCC_except_table58
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A834` __ZN7DinoCap16g_map_pos_vectorE_ptr
- `0x24050C` DinoCap::g_map_pos_vector
- `0x22AB6C` __ZNSt6vectorIN7DinoCap10mapPosPropESaIS1_EEC2ERKS3__ptr
- `0x22AA60` __ZN7DinoCap16g_boss_point_posE_ptr
- `0x240A60` DinoCap::g_boss_point_pos
- `0x22AA54` __ZN7DinoCap16g_boss_point_keyE_ptr
- `0x23B8EC` DinoCap::g_boss_point_key
- `0x22AAF0` __ZNKSt6vectorIN7DinoCap10mapPosPropESaIS1_EE4sizeEv_ptr
- `0x22AB60` __ZNSt6vectorIN7DinoCap10mapPosPropESaIS1_EEaSERKS3__ptr
- `0x22A6E4` __ZN7DinoCap9g_missionE_ptr
- `0x23B8CC` DinoCap::g_mission
- `0x22AAB0` __ZN7DinoCap11g_boss_seedE_ptr
- `0x23B8E4` DinoCap::g_boss_seed
- `0x22AB10` __ZNSt6vectorIN7DinoCap10mapPosPropESaIS1_EE9push_backERKS1__ptr
- `0x22A2AC` __ZN9Triniti2D11RandomRangeEii_ptr
- `0x22A280` __ZN7DinoCap10g_map_infoE_ptr
- `0x2404EC` DinoCap::g_map_info
- `0x22AB14` __ZNKSt6vectorIN7DinoCap8mapClassESaIS1_EE4sizeEv_ptr
- `0x22AB40` __ZN7DinoCap16g_boss_map_countE_ptr
- `0x23B8F0` DinoCap::g_boss_map_count
- `0x22A2DC` __ZN7DinoCap13g_prop_PlayerE_ptr
- `0x240474` DinoCap::g_prop_Player
- `0x22A2B8` __ZN7DinoCap13g_player_typeE_ptr
- `0x23B86C` DinoCap::g_player_type
- `0x22AC00` __ZN7DinoCap6isskipE_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x10A69A  f0b5       PUSH    {R4-R7,LR}
0x10A69C  5e46       MOV     R6, R11
0x10A69E  5546       MOV     R5, R10
0x10A6A0  4446       MOV     R4, R8
0x10A6A2  70b4       PUSH    {R4-R6}
0x10A6A4  ddf01cec   BLX     ___save_vfp_d8_d15_regs
0x10A6A8  16af       ADD     R7, SP, #arg_38
0x10A6AA  f0b0       SUB     SP, SP, #0x1C0
0x10A6AC  e14b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x10A6B6)
0x10A6AE  2190       STR     R0, [SP,#0x1C0+var_13C]
0x10A6B0  0122       MOVS    R2, #1
0x10A6B2  7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x10A6B4  6846       MOV     R0, SP
0x10A6B6  1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x10A6B8  4d90       STR     R0, [SP,#0x1C0+var_8C]
0x10A6BA  43a8       ADD     R0, SP, #0x1C0+fctx; lpfctx
0x10A6BC  4993       STR     R3, [SP,#0x1C0+fctx.personality]
0x10A6BE  de4b       LDR     R3, =(GCC_except_table58 - 0x10A6C6)
0x10A6C0  4b97       STR     R7, [SP,#0x1C0+fctx.jbuf]
0x10A6C2  7b44       ADD     R3, PC; GCC_except_table58
0x10A6C4  4a93       STR     R3, [SP,#0x1C0+fctx.lsda]
0x10A6C6  dd4b       LDR     R3, =(loc_10AF06 - 0x10A6CC)
0x10A6C8  7b44       ADD     R3, PC; loc_10AF06
0x10A6CA  1343       ORRS    R3, R2
```

