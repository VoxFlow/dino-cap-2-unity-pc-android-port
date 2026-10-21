# effectClassN::Disappear

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-11FA60` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x11FA60` |
| RVA (EA - 0x1000) | `0x11EA60` |
| File offset | `0x11EA60` |
| Size | 28 code bytes in 1 chunk(s), 12 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap12effectClassN9DisappearEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::effectClassN::Disappear(void)` |
| Bytes SHA-256 | `bc64c454ec837e2ecb458f2e160dcd93deae916e0634d1302fdecdea0dedd0fc` |
| Direct code callers | 2 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | GAMEPLAY / TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: Enable(false) + GameObject::Delete (from OnAnimationEnd 0x11fae2 / OnUpdate 0x11fd4c); port removes the CompFx object at animation end

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: [`effectClassN::OnAnimationEnd`](../../../functions/dossiers/effectClassN/FUN-11FA80.md), [`effectClassN::OnUpdate`](../../../functions/dossiers/effectClassN/FUN-11FBDA.md)

Callees: `Triniti2D::GameObject::Enable`, `Triniti2D::GameObject::Delete`

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x11FA60  90b5       PUSH    {R4,R7,LR}
0x11FA62  01af       ADD     R7, SP, #4
0x11FA64  0021       MOVS    R1, #0; bool
0x11FA66  041c       MOVS    R4, R0
0x11FA68  4068       LDR     R0, [R0,#4]; this
0x11FA6A  4cf067fa   BL      __ZN9Triniti2D10GameObject6EnableEb; Triniti2D::GameObject::Enable(bool)
0x11FA6E  034a       LDR     R2, =0x3F800000
0x11FA70  e423       MOVS    R3, #0xE4
0x11FA72  6068       LDR     R0, [R4,#4]; this
0x11FA74  e250       STR     R2, [R4,R3]
0x11FA76  4cf01fff   BL      __ZN9Triniti2D10GameObject6DeleteEv; Triniti2D::GameObject::Delete(void)
0x11FA7A  90bd       POP     {R4,R7,PC}
```

