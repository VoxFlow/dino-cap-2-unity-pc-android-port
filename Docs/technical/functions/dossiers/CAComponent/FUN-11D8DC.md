# CAComponent::Frame::Frame

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-11D8DC` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x11D8DC` |
| RVA (EA - 0x1000) | `0x11C8DC` |
| File offset | `0x11C8DC` |
| Size | 34 code bytes in 1 chunk(s), 17 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11CAComponent5FrameC2Ev` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::CAComponent::Frame::Frame(void)` |
| Bytes SHA-256 | `fd02054a7ee5c7ee9b8f1932b0887501d4851c83c321a859b3b9dd8292a38da9` |
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

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x11D8DC  0123       MOVS    R3, #1
0x11D8DE  5b42       NEGS    R3, R3
0x11D8E0  0362       STR     R3, [R0,#0x20]
0x11D8E2  074b       LDR     R3, =0
0x11D8E4  074a       LDR     R2, =0x3F800000
0x11D8E6  4360       STR     R3, [R0,#4]
0x11D8E8  8360       STR     R3, [R0,#8]
0x11D8EA  c360       STR     R3, [R0,#0xC]
0x11D8EC  8361       STR     R3, [R0,#0x18]
0x11D8EE  c361       STR     R3, [R0,#0x1C]
0x11D8F0  0023       MOVS    R3, #0
0x11D8F2  4362       STR     R3, [R0,#0x24]
0x11D8F4  044b       LDR     R3, =0xBF800000
0x11D8F6  0261       STR     R2, [R0,#0x10]
0x11D8F8  4261       STR     R2, [R0,#0x14]
0x11D8FA  0360       STR     R3, [R0]
0x11D8FC  7047       BX      LR
```

