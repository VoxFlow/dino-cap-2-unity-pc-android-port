# selectScene::OnNoMoney

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0F3F50` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xF3F50` |
| RVA (EA - 0x1000) | `0xF2F50` |
| File offset | `0xF2F50` |
| Size | 432 code bytes in 1 chunk(s), 192 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene9OnNoMoneyEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::OnNoMoney(void)` |
| Bytes SHA-256 | `497ae248dcefae261aae3eb400282261c0267effa032257f77e54b7d3c7fed77` |
| Direct code callers | 3 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UI_FLOW / PORTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.hero.locked_branch_and_hire` (PROVEN, NATIVE (decompiler)): OnSelectChange@0x100b1e builds the selection frame then, if dword_240494[cursor] <= -1 (locked), tail-calls sub_101624 and ends: this+15 (selected) is NOT updated for locked heroes, so Back (id38) commits the last UNLOCKED selection. sub_101624 deletes ids 39..46,58,57,63,90 and creates id39 'ui_btn_hiredFrame' (taHireRect 386,274,88,39, startScaleUI), id47 'ui_p<cursor+1>_infoFrame' (taPlayerInfoRect 208,119,252,181...

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: [`selectScene::UpgradeWeapon`](../../../functions/dossiers/selectScene/FUN-0F52C2.md), [`selectScene::AddBullet`](../../../functions/dossiers/selectScene/FUN-0F55B0.md), [`selectScene::OnUIEvent`](../../../functions/dossiers/selectScene/FUN-10B93A.md)

Callees: `Triniti2D::Game::SwitchScene`, `Triniti2D::OSMessageBox`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`

## Strings it references

- `0x209DC8` "You're short on cash. Want to buy some more?"
- `0x209DF8` "No"
- `0x209DFC` "Yes"
- `0x2049B4` "selectScene"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x227F18` GCC_except_table8_7
- `0x22A4AC` __ZN7DinoCap10m_map_typeE_ptr
- `0x23E0A0` DinoCap::m_map_type
- `0x22A434` __ZN9Triniti2D5_GameE_ptr
- `0x23BB94` Triniti2D::_Game

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xF3F50   f0b5       PUSH    {R4-R7,LR}
0xF3F52   5e46       MOV     R6, R11
0xF3F54   5546       MOV     R5, R10
0xF3F56   4446       MOV     R4, R8
0xF3F58   70b4       PUSH    {R4-R6}
0xF3F5A   f3f0c2ef   BLX     ___save_vfp_d8_d15_regs
0xF3F5E   16af       ADD     R7, SP, #arg_38
0xF3F60   9ab0       SUB     SP, SP, #0x68 ; 'h'
0xF3F62   674b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xF3F6C)
0xF3F64   6a46       MOV     R2, SP
0xF3F66   0f92       STR     R2, [SP,#0x68+var_2C]
0xF3F68   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xF3F6A   0122       MOVS    R2, #1
0xF3F6C   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xF3F6E   05a8       ADD     R0, SP, #0x68+fctx; lpfctx
0xF3F70   0d97       STR     R7, [SP,#0x68+fctx.jbuf]
0xF3F72   0b93       STR     R3, [SP,#0x68+fctx.personality]
0xF3F74   634b       LDR     R3, =(GCC_except_table8_7 - 0xF3F7A)
0xF3F76   7b44       ADD     R3, PC; GCC_except_table8_7
0xF3F78   0c93       STR     R3, [SP,#0x68+fctx.lsda]
0xF3F7A   634b       LDR     R3, =(loc_F40A4 - 0xF3F80)
0xF3F7C   7b44       ADD     R3, PC; loc_F40A4
0xF3F7E   1343       ORRS    R3, R2
0xF3F80   0e93       STR     R3, [SP,#0x68+var_30]
```

