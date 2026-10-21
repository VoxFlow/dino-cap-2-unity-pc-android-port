# mainScene::InitFan

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0A12E0` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xA12E0` |
| RVA (EA - 0x1000) | `0xA02E0` |
| File offset | `0xA02E0` |
| Size | 40 code bytes in 1 chunk(s), 18 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9mainScene7InitFanEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::mainScene::InitFan(void)` |
| Bytes SHA-256 | `6cb31a536f0c2388d580cc452d08ef4bb297b97dd5404c09697e3fd7653dafe5` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameWeather.cs:8`](../../../../../Assets/Scripts/GameWeather.cs#L8) cites `0xA12E0`

## Callers and callees (direct code references)

Callers: `sub_B5DF0`

Callees: `Triniti2D::Scene::SetTimer`, `___floatsisfvfp`

## Strings it references

- `0x2079B0` "Yezi"

## Named data it references

- `0x22A2AC` __ZN9Triniti2D11RandomRangeEii_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xA12E0   b0b5       PUSH    {R4,R5,R7,LR}
0xA12E2   02af       ADD     R7, SP, #8
0xA12E4   084b       LDR     R3, =(__ZN9Triniti2D11RandomRangeEii_ptr - 0xA12EE)
0xA12E6   4568       LDR     R5, [R0,#4]
0xA12E8   0321       MOVS    R1, #3
0xA12EA   7b44       ADD     R3, PC; __ZN9Triniti2D11RandomRangeEii_ptr
0xA12EC   0120       MOVS    R0, #1
0xA12EE   1b68       LDR     R3, [R3]
0xA12F0   9847       BLX     R3
0xA12F2   48f13ced   BLX     ___floatsisfvfp
0xA12F6   054c       LDR     R4, =(aYezi - 0xA12FE); "Yezi"
0xA12F8   0023       MOVS    R3, #0; bool
0xA12FA   7c44       ADD     R4, PC; "Yezi"
0xA12FC   211c       MOVS    R1, R4; void *
0xA12FE   021c       MOVS    R2, R0; float
0xA1300   281c       MOVS    R0, R5; this
0xA1302   d4f0a1f9   BL      __ZN9Triniti2D5Scene8SetTimerEPvfb; Triniti2D::Scene::SetTimer(void *,float,bool)
0xA1306   b0bd       POP     {R4,R5,R7,PC}
```

