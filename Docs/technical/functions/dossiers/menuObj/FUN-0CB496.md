# menuObj::OnUpdate

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0CB496` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xCB496` |
| RVA (EA - 0x1000) | `0xCA496` |
| File offset | `0xCA496` |
| Size | 10 code bytes in 1 chunk(s), 4 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap7menuObj8OnUpdateEf` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::menuObj::OnUpdate(float)` |
| Bytes SHA-256 | `4d46884b9aa5de35c7d85610605d498c267b5fc722a4fabc61b5de588844ec8f` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: [`CAObjCallBack::OnUpdate`](../../../functions/dossiers/CAObjCallBack/FUN-00A6CC.md)

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xCB496   80b5       PUSH    {R7,LR}
0xCB498   00af       ADD     R7, SP, #0
0xCB49A   3ff717f9   BL      __ZN7DinoCap13CAObjCallBack8OnUpdateEf; DinoCap::CAObjCallBack::OnUpdate(float)
0xCB49E   80bd       POP     {R7,PC}
```

