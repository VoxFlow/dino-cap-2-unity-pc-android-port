# UILabelMoveCtrl::Update

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0CDA98` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xCDA98` |
| RVA (EA - 0x1000) | `0xCCA98` |
| File offset | `0xCCA98` |
| Size | 348 code bytes in 1 chunk(s), 164 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap15UILabelMoveCtrl6UpdateEf` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::UILabelMoveCtrl::Update(float)` |
| Bytes SHA-256 | `b11cf045631c132b6c5c395f44977bd86de3e9dc43381f17c7113467adc306ea` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UI_FLOW / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: r118 0xcda98: same two-phase lerp as UIMoveCtrl::Update minus colour; raises owner event 1 at move end (port UiLabel has no end event; no handler uses it)

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: `___addsf3vfp`, `___divsf3vfp`, `___gesf2vfp`, `___mulsf3vfp`, `___subsf3vfp`

## Named data it references

- `0x22A1F8` __ZN9Triniti2D4LerpINS_7Vector2EEET_RKS2_S4_f_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xCDA98   f0b5       PUSH    {R4-R7,LR}
0xCDA9A   5e46       MOV     R6, R11
0xCDA9C   5546       MOV     R5, R10
0xCDA9E   4446       MOV     R4, R8
0xCDAA0   70b4       PUSH    {R4-R6}
0xCDAA2   06af       ADD     R7, SP, #0x18
0xCDAA4   8bb0       SUB     SP, SP, #0x2C
0xCDAA6   061c       MOVS    R6, R0
0xCDAA8   081c       MOVS    R0, R1
0xCDAAA   b421       MOVS    R1, #0xB4
0xCDAAC   735c       LDRB    R3, [R6,R1]
0xCDAAE   002b       CMP     R3, #0
0xCDAB0   00d1       BNE     loc_CDAB4
0xCDAB2   99e0       B       loc_CDBE8
0xCDAB4   b822       MOVS    R2, #0xB8
0xCDAB6   9246       MOV     R10, R2
0xCDAB8   b158       LDR     R1, [R6,R2]
0xCDABA   1cf1dae8   BLX     ___addsf3vfp
0xCDABE   5346       MOV     R3, R10
0xCDAC0   4c4d       LDR     R5, =0
0xCDAC2   0195       STR     R5, [SP,#0x2C+var_28]
0xCDAC4   2c1c       MOVS    R4, R5
0xCDAC6   0295       STR     R5, [SP,#0x2C+var_24]
0xCDAC8   f050       STR     R0, [R6,R3]
```

