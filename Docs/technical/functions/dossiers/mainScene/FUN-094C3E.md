# mainScene::GetParticle

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-094C3E` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x94C3E` |
| RVA (EA - 0x1000) | `0x93C3E` |
| File offset | `0x93C3E` |
| Size | 42 code bytes in 1 chunk(s), 21 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9mainScene11GetParticleEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::mainScene::GetParticle(void)` |
| Bytes SHA-256 | `e44b6b5ef4700440b4ca3a8b824eaf34435efdb9d4db6bde8b8f6a0d9401f969` |
| Direct code callers | 8 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/NativeBlood.cs:28`](../../../../../Assets/Scripts/NativeBlood.cs#L28) cites `0x94C3E`
- [`Assets/Scripts/NativeBlood.cs:55`](../../../../../Assets/Scripts/NativeBlood.cs#L55) (by name)

## Callers and callees (direct code references)

Callers: [`particleClass::OnUpdate`](../../../functions/dossiers/particleClass/FUN-007DBC.md), [`dinosaurClass::DieTouFei`](../../../functions/dossiers/dinosaurClass/FUN-010CBC.md), [`dinosaurClass::BlastSuiRou`](../../../functions/dossiers/dinosaurClass/FUN-01226C.md), [`dinosaurClass::BlastBlood`](../../../functions/dossiers/dinosaurClass/FUN-012756.md), `sub_12E16`, [`playerClass::Die`](../../../functions/dossiers/playerClass/FUN-0BCDEA.md), [`bossN::BlastBlood`](../../../functions/dossiers/bossN/FUN-122E38.md), [`playerNewAniClass::Die`](../../../functions/dossiers/playerNewAniClass/FUN-126A36.md)

Callees: none.

## Named data it references

- `0x22A0A0` __ZNKSt6vectorIPN9Triniti2D10GameObjectESaIS2_EE4sizeEv_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x94C3E   f0b5       PUSH    {R4-R7,LR}
0x94C40   03af       ADD     R7, SP, #0xC
0x94C42   c26e       LDR     R2, [R0,#0x6C]
0x94C44   036e       LDR     R3, [R0,#0x60]
0x94C46   041c       MOVS    R4, R0
0x94C48   9000       LSLS    R0, R2, #2
0x94C4A   551c       ADDS    R5, R2, #1
0x94C4C   c658       LDR     R6, [R0,R3]
0x94C4E   064b       LDR     R3, =(__ZNKSt6vectorIPN9Triniti2D10GameObjectESaIS2_EE4sizeEv_ptr - 0x94C58)
0x94C50   201c       MOVS    R0, R4
0x94C52   e566       STR     R5, [R4,#0x6C]
0x94C54   7b44       ADD     R3, PC; __ZNKSt6vectorIPN9Triniti2D10GameObjectESaIS2_EE4sizeEv_ptr
0x94C56   6030       ADDS    R0, #0x60 ; '`'
0x94C58   1b68       LDR     R3, [R3]
0x94C5A   9847       BLX     R3
0x94C5C   8542       CMP     R5, R0
0x94C5E   01d3       BCC     loc_94C64
0x94C60   0023       MOVS    R3, #0
0x94C62   e366       STR     R3, [R4,#0x6C]
0x94C64   301c       MOVS    R0, R6
0x94C66   f0bd       POP     {R4-R7,PC}
```

