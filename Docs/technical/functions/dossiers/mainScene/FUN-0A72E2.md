# mainScene::SubTime

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0A72E2` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xA72E2` |
| RVA (EA - 0x1000) | `0xA62E2` |
| File offset | `0xA62E2` |
| Size | 308 code bytes in 1 chunk(s), 141 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9mainScene7SubTimeEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::mainScene::SubTime(void)` |
| Bytes SHA-256 | `4e84fd23ad6bd58e4ee1f3d8f32fd2b2b2d9a45184bedb2ef666592da52f2ff1` |
| Direct code callers | 2 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameMission.cs:81`](../../../../../Assets/Scripts/GameMission.cs#L81) (by name)

## Callers and callees (direct code references)

Callers: [`mainScene::OnLoad`](../../../functions/dossiers/mainScene/FUN-0A7434.md)

Callees: [`mainScene::StopCreateEnemy`](../../../functions/dossiers/mainScene/FUN-09560E.md), [`mainScene::GameOverStart`](../../../functions/dossiers/mainScene/FUN-0A6F70.md), `Triniti2D::UILabel::SetText`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `_sprintf`

## Strings it references

- `0x20759C` "%d%d:%d%d"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x226AC1` GCC_except_table43
- `0x22A484` __ZN7DinoCap14g_lifemodetimeE_ptr
- `0x23E080` DinoCap::g_lifemodetime

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xA72E2   f0b5       PUSH    {R4-R7,LR}
0xA72E4   5e46       MOV     R6, R11
0xA72E6   5546       MOV     R5, R10
0xA72E8   4446       MOV     R4, R8
0xA72EA   70b4       PUSH    {R4-R6}
0xA72EC   40f1f8ed   BLX     ___save_vfp_d8_d15_regs
0xA72F0   16af       ADD     R7, SP, #arg_38
0xA72F2   9cb0       SUB     SP, SP, #0x70 ; 'p'
0xA72F4   484b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xA72FE)
0xA72F6   6a46       MOV     R2, SP
0xA72F8   0f92       STR     R2, [SP,#0x70+var_34]
0xA72FA   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xA72FC   0122       MOVS    R2, #1
0xA72FE   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xA7300   0390       STR     R0, [SP,#0x70+var_64]
0xA7302   05a8       ADD     R0, SP, #0x70+fctx; lpfctx
0xA7304   0b93       STR     R3, [SP,#0x70+fctx.personality]
0xA7306   454b       LDR     R3, =(GCC_except_table43 - 0xA730E)
0xA7308   0d97       STR     R7, [SP,#0x70+fctx.jbuf]
0xA730A   7b44       ADD     R3, PC; GCC_except_table43
0xA730C   0c93       STR     R3, [SP,#0x70+fctx.lsda]
0xA730E   444b       LDR     R3, =(loc_A73A2 - 0xA7314)
0xA7310   7b44       ADD     R3, PC; loc_A73A2
0xA7312   1343       ORRS    R3, R2
```

