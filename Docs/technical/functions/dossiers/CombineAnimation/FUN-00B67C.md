# CombineAnimation::Reset

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-00B67C` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xB67C` |
| RVA (EA - 0x1000) | `0xA67C` |
| File offset | `0xA67C` |
| Size | 80 code bytes in 1 chunk(s), 38 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap16CombineAnimation5ResetEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::CombineAnimation::Reset(void)` |
| Bytes SHA-256 | `811e26a99b6c336c07cc1d42432a2359542f7a900cfdab408ce1c2d689b3012a` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: [`CombineAnimation::LoadXml`](../../../functions/dossiers/CombineAnimation/FUN-00F4B0.md)

Callees: `CAFrame::~CAFrame`, `operator delete`

## Named data it references

- `0x22A120` __ZNKSt6vectorIPN7DinoCap7CAFrameESaIS2_EE4sizeEv_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xB67C    f0b5       PUSH    {R4-R7,LR}
0xB67E    4646       MOV     R6, R8
0xB680    40b4       PUSH    {R6}
0xB682    04af       ADD     R7, SP, #0x10
0xB684    114b       LDR     R3, =(__ZNKSt6vectorIPN7DinoCap7CAFrameESaIS2_EE4sizeEv_ptr - 0xB68E)
0xB686    051c       MOVS    R5, R0
0xB688    0026       MOVS    R6, #0
0xB68A    7b44       ADD     R3, PC; __ZNKSt6vectorIPN7DinoCap7CAFrameESaIS2_EE4sizeEv_ptr
0xB68C    1b68       LDR     R3, [R3]
0xB68E    9846       MOV     R8, R3
0xB690    0be0       B       loc_B6AA
0xB692    2b68       LDR     R3, [R5]
0xB694    b000       LSLS    R0, R6, #2
0xB696    c458       LDR     R4, [R0,R3]
0xB698    002c       CMP     R4, #0
0xB69A    05d0       BEQ     loc_B6A8
0xB69C    201c       MOVS    R0, R4; this
0xB69E    fff7e6fe   BL      __ZN7DinoCap7CAFrameD2Ev; DinoCap::CAFrame::~CAFrame()
0xB6A2    201c       MOVS    R0, R4; void *
0xB6A4    def1ccea   BLX     __ZdlPv; operator delete(void *)
0xB6A8    0136       ADDS    R6, #1
0xB6AA    281c       MOVS    R0, R5
0xB6AC    c047       BLX     R8
0xB6AE    8642       CMP     R6, R0
```

