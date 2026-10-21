# selectScene::OnTimeout

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0FA3D2` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xFA3D2` |
| RVA (EA - 0x1000) | `0xF93D2` |
| File offset | `0xF93D2` |
| Size | 358 code bytes in 1 chunk(s), 153 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene9OnTimeoutEiPv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::OnTimeout(int,void *)` |
| Bytes SHA-256 | `45d8731574603f15ef60d682ce5a31a76bc4f422143ab235454439a14c1089f7` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: [`selectScene::startJigsaw`](../../../functions/dossiers/selectScene/FUN-0F6F00.md), `selectScene::MenuBackLeave`, `selectScene::MenuBackEnter`, `selectScene::ArmoryLeave`, `selectScene::ArmoryEnter`, `selectScene::TavernLeave`, `selectScene::TavernEnter`, [`selectScene::MissionLeave`](../../../functions/dossiers/selectScene/FUN-0FA1BA.md), `selectScene::MissionEnter`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::compare`, `std::string::string`, `std::string::~string`

## Strings it references

- `0x209E38` "MissionEnter"
- `0x209E00` "MissionLeave"
- `0x209E48` "TavernEnter"
- `0x209E10` "TavernLeave"
- `0x209E54` "ArmoryEnter"
- `0x209E1C` "ArmoryLeave"
- `0x209E60` "MenuBackEnter"
- `0x209E28` "MenuBackLeave"
- `0x20A604` "startJigsaw"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x2280EC` GCC_except_table42_0

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xFA3D2   f0b5       PUSH    {R4-R7,LR}
0xFA3D4   5e46       MOV     R6, R11
0xFA3D6   5546       MOV     R5, R10
0xFA3D8   4446       MOV     R4, R8
0xFA3DA   70b4       PUSH    {R4-R6}
0xFA3DC   edf080ed   BLX     ___save_vfp_d8_d15_regs
0xFA3E0   16af       ADD     R7, SP, #arg_38
0xFA3E2   92b0       SUB     SP, SP, #0x48 ; 'H'
0xFA3E4   544b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xFA3EE)
0xFA3E6   0192       STR     R2, [SP,#0x48+var_44]
0xFA3E8   6a46       MOV     R2, SP
0xFA3EA   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xFA3EC   0d92       STR     R2, [SP,#0x48+var_14]
0xFA3EE   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xFA3F0   0122       MOVS    R2, #1
0xFA3F2   0290       STR     R0, [SP,#0x48+var_40]
0xFA3F4   0993       STR     R3, [SP,#0x48+fctx.personality]
0xFA3F6   514b       LDR     R3, =(GCC_except_table42_0 - 0xFA400)
0xFA3F8   03a8       ADD     R0, SP, #0x48+fctx; lpfctx
0xFA3FA   0b97       STR     R7, [SP,#0x48+fctx.jbuf]
0xFA3FC   7b44       ADD     R3, PC; GCC_except_table42_0
0xFA3FE   0a93       STR     R3, [SP,#0x48+fctx.lsda]
0xFA400   4f4b       LDR     R3, =(loc_FA4F6 - 0xFA406)
0xFA402   7b44       ADD     R3, PC; loc_FA4F6
```

