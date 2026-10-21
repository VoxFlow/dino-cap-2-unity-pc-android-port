# mainScene::UpdateKillDinocap

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-094C1E` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x94C1E` |
| RVA (EA - 0x1000) | `0x93C1E` |
| File offset | `0x93C1E` |
| Size | 12 code bytes in 1 chunk(s), 5 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9mainScene17UpdateKillDinocapEi` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::mainScene::UpdateKillDinocap(int)` |
| Bytes SHA-256 | `40b63e8b8c4afde620ded62e2ead147dfc37d02eeed7f74a6463d1e9842257b8` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | GAMEPLAY / TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: 0x94c1e: scene+420 += n, called from dinosaurClass::Die 0x1438e; the lifeMode override 0xe1e62 is the ported kill counter (GameMission)

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: [`dinosaurClass::Die`](../../../functions/dossiers/dinosaurClass/FUN-0140FC.md)

Callees: none.

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x94C1E   d2225200   MOVS    R2, #0x1A4
0x94C22   8358       LDR     R3, [R0,R2]
0x94C24   c918       ADDS    R1, R1, R3
0x94C26   8150       STR     R1, [R0,R2]
0x94C28   7047       BX      LR
```

