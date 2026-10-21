# CAObjCallBack::setAnimationDelta

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-00A00C` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xA00C` |
| RVA (EA - 0x1000) | `0x900C` |
| File offset | `0x900C` |
| Size | 4 code bytes in 1 chunk(s), 2 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap13CAObjCallBack17setAnimationDeltaEf` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::CAObjCallBack::setAnimationDelta(float)` |
| Bytes SHA-256 | `f1b6e557411f6fa5f3eb0b11ae498fb7f5d7331f94de486cb28dc296eb5fc124` |
| Direct code callers | 6 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/NativeCaAnimation.cs:245`](../../../../../Assets/Scripts/NativeCaAnimation.cs#L245) (by name)

## Callers and callees (direct code references)

Callers: [`dinosaurClass::OnAnimationEndAdv`](../../../functions/dossiers/dinosaurClass/FUN-01170E.md), [`dinosaurClass::BeJinAttacked`](../../../functions/dossiers/dinosaurClass/FUN-01442A.md), [`dinosaurClass::BeAttacked`](../../../functions/dossiers/dinosaurClass/FUN-014918.md)

Callees: none.

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xA00C    8161       STR     R1, [R0,#0x18]
0xA00E    7047       BX      LR
```

