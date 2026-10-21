# pauseCtrl::Update

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0CE84C` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xCE84C` |
| RVA (EA - 0x1000) | `0xCD84C` |
| File offset | `0xCD84C` |
| Size | 62 code bytes in 1 chunk(s), 31 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9pauseCtrl6UpdateEf` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::pauseCtrl::Update(float)` |
| Bytes SHA-256 | `8e122448d551c167ff912e1a34f0fb1a464c1e4d77a7dcf42cd111402bf2189b` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

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
0xCE84C   f0b5       PUSH    {R4-R7,LR}
0xCE84E   5646       MOV     R6, R10
0xCE850   4546       MOV     R5, R8
0xCE852   60b4       PUSH    {R5,R6}
0xCE854   05af       ADD     R7, SP, #0x14
0xCE856   0d4b       LDR     R3, =(__ZNKSt6vectorIPN7DinoCap10UIMoveCtrlESaIS2_EE4sizeEv_ptr - 0xCE860)
0xCE858   061c       MOVS    R6, R0
0xCE85A   8046       MOV     R8, R0
0xCE85C   7b44       ADD     R3, PC; __ZNKSt6vectorIPN7DinoCap10UIMoveCtrlESaIS2_EE4sizeEv_ptr
0xCE85E   8a46       MOV     R10, R1
0xCE860   1d68       LDR     R5, [R3]
0xCE862   0024       MOVS    R4, #0
0xCE864   4436       ADDS    R6, #0x44 ; 'D'
0xCE866   08e0       B       loc_CE87A
0xCE868   4346       MOV     R3, R8
0xCE86A   5146       MOV     R1, R10
0xCE86C   5a6c       LDR     R2, [R3,#0x44]
0xCE86E   a300       LSLS    R3, R4, #2
0xCE870   0134       ADDS    R4, #1
0xCE872   9858       LDR     R0, [R3,R2]
0xCE874   0368       LDR     R3, [R0]
0xCE876   9b6c       LDR     R3, [R3,#0x48]
0xCE878   9847       BLX     R3
0xCE87A   301c       MOVS    R0, R6
```

