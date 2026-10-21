# UILabelMoveCtrl::StartMove

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0CD4B2` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xCD4B2` |
| RVA (EA - 0x1000) | `0xCC4B2` |
| File offset | `0xCC4B2` |
| Size | 72 code bytes in 1 chunk(s), 36 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap15UILabelMoveCtrl9StartMoveERKNS_19UILabelMoveCtrlDataES3_S3_ff` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::UILabelMoveCtrl::StartMove(DinoCap::UILabelMoveCtrlData const&,DinoCap::UILabelMoveCtrlData const&,DinoCap::UILabelMoveCtrlData const&,float,float)` |
| Bytes SHA-256 | `8a216d3509a29318339d7da0a227877acac8639cdc98e7ca44ea4721d10e3e32` |
| Direct code callers | 10 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: [`mainScene::startMoveUIRight`](../../../functions/dossiers/mainScene/FUN-0A2D6E.md), [`mainScene::LevelEnter`](../../../functions/dossiers/mainScene/FUN-0A348E.md), [`mainScene::LevelLeave`](../../../functions/dossiers/mainScene/FUN-0B69E4.md), [`lifeModeScene::startMoveUIRight`](../../../functions/dossiers/lifeModeScene/FUN-0D5224.md), [`lifeModeScene::LevelEnter`](../../../functions/dossiers/lifeModeScene/FUN-0F13B4.md), [`lifeModeScene::LevelLeave`](../../../functions/dossiers/lifeModeScene/FUN-0F17E6.md), [`selectScene::startReturnUIRight`](../../../functions/dossiers/selectScene/FUN-0F6D42.md), [`selectScene::startMoveUIRightOut`](../../../functions/dossiers/selectScene/FUN-0F6E24.md), [`selectScene::startMoveUIRight`](../../../functions/dossiers/selectScene/FUN-0F6FC8.md), [`selectScene::startMoveUILeft`](../../../functions/dossiers/selectScene/FUN-0F70AC.md)

Callees: none.

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xCD4B2   70b5       PUSH    {R4-R6,LR}
0xCD4B4   041c       MOVS    R4, R0
0xCD4B6   7034       ADDS    R4, #0x70 ; 'p'
0xCD4B8   9c46       MOV     R12, R3
0xCD4BA   68c9       LDM     R1!, {R3,R5,R6}
0xCD4BC   68c4       STM     R4!, {R3,R5,R6}
0xCD4BE   28c9       LDM     R1!, {R3,R5}
0xCD4C0   28c4       STM     R4!, {R3,R5}
0xCD4C2   011c       MOVS    R1, R0
0xCD4C4   9831       ADDS    R1, #0x98
0xCD4C6   58ca       LDM     R2!, {R3,R4,R6}
0xCD4C8   58c1       STM     R1!, {R3,R4,R6}
0xCD4CA   6346       MOV     R3, R12
0xCD4CC   60ca       LDM     R2!, {R5,R6}
0xCD4CE   60c1       STM     R1!, {R5,R6}
0xCD4D0   021c       MOVS    R2, R0
0xCD4D2   8432       ADDS    R2, #0x84
0xCD4D4   32cb       LDM     R3!, {R1,R4,R5}
0xCD4D6   32c2       STM     R2!, {R1,R4,R5}
0xCD4D8   42cb       LDM     R3!, {R1,R6}
0xCD4DA   42c2       STM     R2!, {R1,R6}
0xCD4DC   049a       LDR     R2, [SP,#arg_0]
0xCD4DE   ac23       MOVS    R3, #0xAC
0xCD4E0   059c       LDR     R4, [SP,#arg_4]
```

