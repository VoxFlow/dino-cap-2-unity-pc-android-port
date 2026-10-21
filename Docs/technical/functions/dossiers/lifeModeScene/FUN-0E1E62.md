# lifeModeScene::UpdateKillDinocap

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0E1E62` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xE1E62` |
| RVA (EA - 0x1000) | `0xE0E62` |
| File offset | `0xE0E62` |
| Size | 544 code bytes in 1 chunk(s), 253 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap13lifeModeScene17UpdateKillDinocapEii` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::lifeModeScene::UpdateKillDinocap(int,int)` |
| Bytes SHA-256 | `2a8b7679e3714476e191f5bdf64fefac12d56f845d7429b1d2ff67a659c5f52d` |
| Direct code callers | 2 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameMission.cs:9`](../../../../../Assets/Scripts/GameMission.cs#L9) cites `0xE1E62`
- [`Assets/Scripts/GameMission.cs:96`](../../../../../Assets/Scripts/GameMission.cs#L96) cites `0xE1E62`

## Callers and callees (direct code references)

Callers: [`dinosaurClass::Die`](../../../functions/dossiers/dinosaurClass/FUN-0140FC.md), [`lifeModeScene::OnLoad`](../../../functions/dossiers/lifeModeScene/FUN-0E7202.md)

Callees: [`lifeModeScene::StopCreateEnemy`](../../../functions/dossiers/lifeModeScene/FUN-0D0E14.md), `mapClass::~mapClass`, `Triniti2D::UILabel::SetText`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `_sprintf`

## Strings it references

- `0x207360` "%d/%d"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x227A75` GCC_except_table29_3
- `0x22A280` __ZN7DinoCap10g_map_infoE_ptr
- `0x2404EC` DinoCap::g_map_info
- `0x22A25C` __ZN7DinoCap14g_cur_map_dataE_ptr
- `0x23B87C` DinoCap::g_cur_map_data
- `0x22A270` __ZN7DinoCap10g_curMapIDE_ptr
- `0x23B878` DinoCap::g_curMapID
- `0x22A28C` __ZN7DinoCap8mapClassC2ERKS0__ptr
- `0x22A2A8` __ZNKSt6vectorIiSaIiEE4sizeEv_ptr
- `0x22A484` __ZN7DinoCap14g_lifemodetimeE_ptr
- `0x23E080` DinoCap::g_lifemodetime
- `0x22A30C` __ZNSt6vectorIbSaIbEEixEm_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xE1E62   f0b5       PUSH    {R4-R7,LR}
0xE1E64   5e46       MOV     R6, R11
0xE1E66   5546       MOV     R5, R10
0xE1E68   4446       MOV     R4, R8
0xE1E6A   70b4       PUSH    {R4-R6}
0xE1E6C   06f138e8   BLX     ___save_vfp_d8_d15_regs
0xE1E70   16af       ADD     R7, SP, #arg_38
0xE1E72   b4b0       SUB     SP, SP, #0xD0
0xE1E74   834b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xE1E7E)
0xE1E76   0392       STR     R2, [SP,#0xD0+var_C4]
0xE1E78   0122       MOVS    R2, #1
0xE1E7A   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xE1E7C   0590       STR     R0, [SP,#0xD0+var_BC]
0xE1E7E   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xE1E80   0491       STR     R1, [SP,#0xD0+var_C0]
0xE1E82   0aa8       ADD     R0, SP, #0xD0+fctx; lpfctx
0xE1E84   1093       STR     R3, [SP,#0xD0+fctx.personality]
0xE1E86   804b       LDR     R3, =(GCC_except_table29_3 - 0xE1E90)
0xE1E88   6946       MOV     R1, SP
0xE1E8A   1491       STR     R1, [SP,#0xD0+var_80]
0xE1E8C   7b44       ADD     R3, PC; GCC_except_table29_3
0xE1E8E   1193       STR     R3, [SP,#0xD0+fctx.lsda]
0xE1E90   7e4b       LDR     R3, =(loc_E2024 - 0xE1E98)
0xE1E92   1297       STR     R7, [SP,#0xD0+fctx.jbuf]
```

