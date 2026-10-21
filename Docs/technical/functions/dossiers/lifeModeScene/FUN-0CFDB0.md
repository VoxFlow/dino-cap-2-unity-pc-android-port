# lifeModeScene::CanRollDown

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0CFDB0` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xCFDB0` |
| RVA (EA - 0x1000) | `0xCEDB0` |
| File offset | `0xCEDB0` |
| Size | 2 code bytes in 1 chunk(s), 1 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap13lifeModeScene11CanRollDownEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::lifeModeScene::CanRollDown(void)` |
| Bytes SHA-256 | `c7dfbb7d02759eacb64dbc916c1bb6f21eabaff1c1032ea5c9176abf7fd28df8` |
| Direct code callers | 2 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: `playerClass::OnTimeout`, [`playerNewAniClass::OnTimeout`](../../../functions/dossiers/playerNewAniClass/FUN-127DAC.md)

Callees: none.

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xCFDB0   7047       BX      LR
```

