# pauseCtrl::HandleTouch

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0CE890` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xCE890` |
| RVA (EA - 0x1000) | `0xCD890` |
| File offset | `0xCD890` |
| Size | 102 code bytes in 1 chunk(s), 51 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9pauseCtrl11HandleTouchEiiii` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::pauseCtrl::HandleTouch(int,int,int,int)` |
| Bytes SHA-256 | `02e78f11d48098e80f59d87f0fb719d6572e5e0d36b2b2f4d5cd2ea66c08c951` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UI_FLOW / PORTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: pauseCtrl::HandleTouch (vtable): pause children dispatch, GameControls.HandlePauseEvent

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: none.

## Named data it references

- `0x22AA40` __ZNKSt6vectorIPN7DinoCap10UIMoveCtrlESaIS2_EE4sizeEv_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xCE890   f0b5       PUSH    {R4-R7,LR}
0xCE892   5e46       MOV     R6, R11
0xCE894   5546       MOV     R5, R10
0xCE896   4446       MOV     R4, R8
0xCE898   70b4       PUSH    {R4-R6}
0xCE89A   06af       ADD     R7, SP, #0x18
0xCE89C   84b0       SUB     SP, SP, #0x10
0xCE89E   0193       STR     R3, [SP,#0x10+var_C]
0xCE8A0   154b       LDR     R3, =(__ZNKSt6vectorIPN7DinoCap10UIMoveCtrlESaIS2_EE4sizeEv_ptr - 0xCE8AA)
0xCE8A2   8346       MOV     R11, R0
0xCE8A4   0292       STR     R2, [SP,#0x10+var_8]
0xCE8A6   7b44       ADD     R3, PC; __ZNKSt6vectorIPN7DinoCap10UIMoveCtrlESaIS2_EE4sizeEv_ptr
0xCE8A8   4422       MOVS    R2, #0x44 ; 'D'
0xCE8AA   1b68       LDR     R3, [R3]
0xCE8AC   5a44       ADD     R2, R11
0xCE8AE   0025       MOVS    R5, #0
0xCE8B0   9246       MOV     R10, R2
0xCE8B2   9846       MOV     R8, R3
0xCE8B4   0391       STR     R1, [SP,#0x10+var_4]
0xCE8B6   13e0       B       loc_CE8E0
0xCE8B8   5a46       MOV     R2, R11
0xCE8BA   a800       LSLS    R0, R5, #2
0xCE8BC   536c       LDR     R3, [R2,#0x44]
0xCE8BE   c458       LDR     R4, [R0,R3]
```

