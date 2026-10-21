# lifeModeScene::SubTime

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0D0ED6` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xD0ED6` |
| RVA (EA - 0x1000) | `0xCFED6` |
| File offset | `0xCFED6` |
| Size | 328 code bytes in 1 chunk(s), 152 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap13lifeModeScene7SubTimeEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::lifeModeScene::SubTime(void)` |
| Bytes SHA-256 | `3bb30dda4eb5e2d07c344f34602b1c292e4cb52e0dba8d0a41ef4feadf7c968b` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameMission.cs:9`](../../../../../Assets/Scripts/GameMission.cs#L9) cites `0xD0ED6`
- [`Assets/Scripts/GameMission.cs:79`](../../../../../Assets/Scripts/GameMission.cs#L79) cites `0xD0ED6`

## Callers and callees (direct code references)

Callers: [`lifeModeScene::OnTimeout`](../../../functions/dossiers/lifeModeScene/FUN-0E2A4C.md)

Callees: [`lifeModeScene::StopCreateEnemy`](../../../functions/dossiers/lifeModeScene/FUN-0D0E14.md), `Triniti2D::UILabel::SetText`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `_sprintf`

## Strings it references

- `0x20759C` "%d%d:%d%d"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x227574` GCC_except_table12_7
- `0x22A484` __ZN7DinoCap14g_lifemodetimeE_ptr
- `0x23E080` DinoCap::g_lifemodetime

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xD0ED6   f0b5       PUSH    {R4-R7,LR}
0xD0ED8   5e46       MOV     R6, R11
0xD0EDA   5546       MOV     R5, R10
0xD0EDC   4446       MOV     R4, R8
0xD0EDE   70b4       PUSH    {R4-R6}
0xD0EE0   16f1feef   BLX     ___save_vfp_d8_d15_regs
0xD0EE4   16af       ADD     R7, SP, #arg_38
0xD0EE6   9db0       SUB     SP, SP, #0x74 ; 't'
0xD0EE8   4d4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xD0EF2)
0xD0EEA   0122       MOVS    R2, #1
0xD0EEC   0490       STR     R0, [SP,#0x74+var_64]
0xD0EEE   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xD0EF0   6946       MOV     R1, SP
0xD0EF2   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xD0EF4   06a8       ADD     R0, SP, #0x74+fctx; lpfctx
0xD0EF6   0e97       STR     R7, [SP,#0x74+fctx.jbuf]
0xD0EF8   0c93       STR     R3, [SP,#0x74+fctx.personality]
0xD0EFA   4a4b       LDR     R3, =(GCC_except_table12_7 - 0xD0F02)
0xD0EFC   1091       STR     R1, [SP,#0x74+var_34]
0xD0EFE   7b44       ADD     R3, PC; GCC_except_table12_7
0xD0F00   0d93       STR     R3, [SP,#0x74+fctx.lsda]
0xD0F02   494b       LDR     R3, =(loc_D0F96 - 0xD0F08)
0xD0F04   7b44       ADD     R3, PC; loc_D0F96
0xD0F06   1343       ORRS    R3, R2
```

