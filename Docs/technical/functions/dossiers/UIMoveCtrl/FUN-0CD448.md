# UIMoveCtrl::SetRectRange

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0CD448` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xCD448` |
| RVA (EA - 0x1000) | `0xCC448` |
| File offset | `0xCC448` |
| Size | 30 code bytes in 1 chunk(s), 15 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap10UIMoveCtrl12SetRectRangeERKN9Triniti2D4RectE` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::UIMoveCtrl::SetRectRange(Triniti2D::Rect const&)` |
| Bytes SHA-256 | `38c1b9c2a3ffb2abf7c09196a7263c0a7be43b9362ce5ac9aea9851b418bbb6e` |
| Direct code callers | 7 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/UiControls.cs:79`](../../../../../Assets/Scripts/UiControls.cs#L79) cites `0xCD448`

## Callers and callees (direct code references)

Callers: [`pauseCtrl::OnFx`](../../../functions/dossiers/pauseCtrl/FUN-0CE1EC.md), [`pauseCtrl::OnMusic`](../../../functions/dossiers/pauseCtrl/FUN-0CE384.md), [`pauseCtrl::Enter`](../../../functions/dossiers/pauseCtrl/FUN-0CED0C.md), [`selectScene::OnSound`](../../../functions/dossiers/selectScene/FUN-0F4128.md), [`selectScene::OnMusic`](../../../functions/dossiers/selectScene/FUN-0F433A.md)

Callees: none.

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xCD448   b0b5       PUSH    {R4,R5,R7,LR}
0xCD44A   02af       ADD     R7, SP, #8
0xCD44C   0368       LDR     R3, [R0]
0xCD44E   0c1c       MOVS    R4, R1
0xCD450   051c       MOVS    R5, R0
0xCD452   db68       LDR     R3, [R3,#0xC]
0xCD454   9847       BLX     R3
0xCD456   281c       MOVS    R0, R5
0xCD458   231c       MOVS    R3, R4
0xCD45A   9430       ADDS    R0, #0x94
0xCD45C   26cb       LDM     R3!, {R1,R2,R5}
0xCD45E   26c0       STM     R0!, {R1,R2,R5}
0xCD460   e168       LDR     R1, [R4,#0xC]
0xCD462   0160       STR     R1, [R0]
0xCD464   b0bd       POP     {R4,R5,R7,PC}
```

