# selectScene::showBossPoint

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-10A2E8` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x10A2E8` |
| RVA (EA - 0x1000) | `0x1092E8` |
| File offset | `0x1092E8` |
| Size | 814 code bytes in 1 chunk(s), 375 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene13showBossPointEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::showBossPoint(void)` |
| Bytes SHA-256 | `c8949102bfa77ff5dbb98680de2c2cced7fd2e9f0271c7542244a0c05c0eaf43` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/NativeMapData.cs:111`](../../../../../Assets/Scripts/NativeMapData.cs#L111) cites `0x10A2E8`
- [`Assets/Scripts/SelectMapPoints.cs:56`](../../../../../Assets/Scripts/SelectMapPoints.cs#L56) cites `0x10A2E8`

## Callers and callees (direct code references)

Callers: [`selectScene::showMapPoint`](../../../functions/dossiers/selectScene/FUN-10A69A.md)

Callees: [`RectScale`](../../../functions/dossiers/DinoCap/FUN-017068.md), [`RectOffset`](../../../functions/dossiers/DinoCap/FUN-0170B6.md), `mapPosProp::~mapPosProp`, [`selectScene::startScaleUI`](../../../functions/dossiers/selectScene/FUN-0F77DC.md), [`selectScene::startScaleUI_Point`](../../../functions/dossiers/selectScene/FUN-10A234.md), `Triniti2D::Scene::GetUIManager`, `Triniti2D::UIImage::SetTexture`, `Triniti2D::UIManager::DeleteControl`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___mulsf3vfp`, `_sprintf`

## Strings it references

- `0x20AC70` "ui_map_point_type%dFrame"
- `0x206F30` "ui_map_point_bgFrame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x2284F1` GCC_except_table57
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A280` __ZN7DinoCap10g_map_infoE_ptr
- `0x2404EC` DinoCap::g_map_info
- `0x22AB14` __ZNKSt6vectorIN7DinoCap8mapClassESaIS1_EE4sizeEv_ptr
- `0x22AB40` __ZN7DinoCap16g_boss_map_countE_ptr
- `0x23B8F0` DinoCap::g_boss_map_count
- `0x22A2AC` __ZN9Triniti2D11RandomRangeEii_ptr
- `0x22A2DC` __ZN7DinoCap13g_prop_PlayerE_ptr
- `0x240474` DinoCap::g_prop_Player
- `0x22A2B8` __ZN7DinoCap13g_player_typeE_ptr
- `0x23B86C` DinoCap::g_player_type
- `0x22AC00` __ZN7DinoCap6isskipE_ptr
- `0x23B8A4` DinoCap::isskip
- `0x22A25C` __ZN7DinoCap14g_cur_map_dataE_ptr
- `0x23B87C` DinoCap::g_cur_map_data
- `0x22A6C8` __ZN9Triniti2D9UIManager13CreateControlIN7DinoCap10UIMoveCtrlEEEPT_ii_ptr
- `0x22AA60` __ZN7DinoCap16g_boss_point_posE_ptr
- `0x240A60` DinoCap::g_boss_point_pos

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x10A2E8  f0b5       PUSH    {R4-R7,LR}
0x10A2EA  5e46       MOV     R6, R11
0x10A2EC  5546       MOV     R5, R10
0x10A2EE  4446       MOV     R4, R8
0x10A2F0  70b4       PUSH    {R4-R6}
0x10A2F2  ddf0f6ed   BLX     ___save_vfp_d8_d15_regs
0x10A2F6  16af       ADD     R7, SP, #arg_38
0x10A2F8  bbb0       SUB     SP, SP, #0xEC
0x10A2FA  c74b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x10A304)
0x10A2FC  0c90       STR     R0, [SP,#0xEC+var_BC]
0x10A2FE  0122       MOVS    R2, #1
0x10A300  7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x10A302  6846       MOV     R0, SP
0x10A304  1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x10A306  1e90       STR     R0, [SP,#0xEC+var_74]
0x10A308  14a8       ADD     R0, SP, #0xEC+fctx; lpfctx
0x10A30A  1a93       STR     R3, [SP,#0xEC+fctx.personality]
0x10A30C  c34b       LDR     R3, =(GCC_except_table57 - 0x10A314)
0x10A30E  1c97       STR     R7, [SP,#0xEC+fctx.jbuf]
0x10A310  7b44       ADD     R3, PC; GCC_except_table57
0x10A312  1b93       STR     R3, [SP,#0xEC+fctx.lsda]
0x10A314  c24b       LDR     R3, =(loc_10A54A - 0x10A31A)
0x10A316  7b44       ADD     R3, PC; loc_10A54A
0x10A318  1343       ORRS    R3, R2
```

