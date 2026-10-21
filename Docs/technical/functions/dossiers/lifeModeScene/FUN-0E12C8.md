# lifeModeScene::InitShanDian

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0E12C8` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xE12C8` |
| RVA (EA - 0x1000) | `0xE02C8` |
| File offset | `0xE02C8` |
| Size | 46 code bytes in 1 chunk(s), 21 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap13lifeModeScene12InitShanDianEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::lifeModeScene::InitShanDian(void)` |
| Bytes SHA-256 | `b1e173e631a61e47b4f50cb8083d379a99c504a6bd035c91b7fbd7d010117b66` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | GAMEPLAY / PORTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: twin of mainScene::InitShanDian 0xa1310 (same literals and calls); GameWeather.InitShanDian

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: `sub_EFBD6`

Callees: `Triniti2D::Scene::SetTimer`, `___floatsisfvfp`

## Strings it references

- `0x207464` "ShanDian"

## Named data it references

- `0x22A2AC` __ZN9Triniti2D11RandomRangeEii_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xE12C8   b0b5       PUSH    {R4,R5,R7,LR}
0xE12CA   02af       ADD     R7, SP, #8
0xE12CC   0a4b       LDR     R3, =0x28D
0xE12CE   0022       MOVS    R2, #0
0xE12D0   4568       LDR     R5, [R0,#4]
0xE12D2   0721       MOVS    R1, #7
0xE12D4   c254       STRB    R2, [R0,R3]
0xE12D6   094b       LDR     R3, =(__ZN9Triniti2D11RandomRangeEii_ptr - 0xE12E0)
0xE12D8   0420       MOVS    R0, #4
0xE12DA   094c       LDR     R4, =(aShandian - 0xE12EA); "ShanDian"
0xE12DC   7b44       ADD     R3, PC; __ZN9Triniti2D11RandomRangeEii_ptr
0xE12DE   1b68       LDR     R3, [R3]
0xE12E0   9847       BLX     R3
0xE12E2   08f144ed   BLX     ___floatsisfvfp
0xE12E6   7c44       ADD     R4, PC; "ShanDian"
0xE12E8   211c       MOVS    R1, R4; void *
0xE12EA   0023       MOVS    R3, #0; bool
0xE12EC   021c       MOVS    R2, R0; float
0xE12EE   281c       MOVS    R0, R5; this
0xE12F0   94f0aaf9   BL      __ZN9Triniti2D5Scene8SetTimerEPvfb; Triniti2D::Scene::SetTimer(void *,float,bool)
0xE12F4   b0bd       POP     {R4,R5,R7,PC}
```

