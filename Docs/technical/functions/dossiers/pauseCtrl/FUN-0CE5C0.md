# pauseCtrl::SendUIEvent

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0CE5C0` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xCE5C0` |
| RVA (EA - 0x1000) | `0xCD5C0` |
| File offset | `0xCD5C0` |
| Size | 170 code bytes in 1 chunk(s), 76 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9pauseCtrl11SendUIEventEPN9Triniti2D9UIControlEiff` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::pauseCtrl::SendUIEvent(Triniti2D::UIControl *,int,float,float)` |
| Bytes SHA-256 | `a88e4c444408140f9748e88b0954750642bbcd1bb5e7907c196bf503070d6631` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameControls.cs:11`](../../../../../Assets/Scripts/GameControls.cs#L11) cites `0xCE5C0`

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: [`pauseCtrl::OnFx`](../../../functions/dossiers/pauseCtrl/FUN-0CE1EC.md), [`pauseCtrl::OnMusic`](../../../functions/dossiers/pauseCtrl/FUN-0CE384.md), `Triniti2D::UIControl::Id`, `___switch8`

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xCE5C0   b0b5       PUSH    {R4,R5,R7,LR}
0xCE5C2   02af       ADD     R7, SP, #8
0xCE5C4   81b0       SUB     SP, SP, #4
0xCE5C6   051c       MOVS    R5, R0
0xCE5C8   081c       MOVS    R0, R1; this
0xCE5CA   141c       MOVS    R4, R2
0xCE5CC   b0f047f8   BL      __ZN9Triniti2D9UIControl2IdEv; Triniti2D::UIControl::Id(void)
0xCE5D0   031c       MOVS    R3, R0
0xCE5D2   19f15aec   BLX     ___switch8; switch 9 cases
0xCE5E0   012c       CMP     R4, #1; jumptable 000CE5D2 case 0
0xCE5E2   40d1       BNE     def_CE5D2; jumptable 000CE5D2 default case, cases 1,2,8
0xCE5E4   d023       MOVS    R3, #0xD0
0xCE5E6   eb58       LDR     R3, [R5,R3]
0xCE5E8   002b       CMP     R3, #0
0xCE5EA   02d0       BEQ     loc_CE5F2
0xCE5EC   012b       CMP     R3, #1
0xCE5EE   3ad1       BNE     def_CE5D2; jumptable 000CE5D2 default case, cases 1,2,8
0xCE5F0   07e0       B       loc_CE602
0xCE5F2   6868       LDR     R0, [R5,#4]
0xCE5F4   1d4b       LDR     R3, =0
0xCE5F6   291c       MOVS    R1, R5
0xCE5F8   0268       LDR     R2, [R0]
0xCE5FA   0093       STR     R3, [SP,#4+var_4]
0xCE5FC   d469       LDR     R4, [R2,#0x1C]
```

