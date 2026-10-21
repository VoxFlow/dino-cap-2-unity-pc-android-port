# selectScene::startMoveUIRightOut

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0F6E24` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xF6E24` |
| RVA (EA - 0x1000) | `0xF5E24` |
| File offset | `0xF5E24` |
| Size | 188 code bytes in 1 chunk(s), 85 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene19startMoveUIRightOutEPNS_15UILabelMoveCtrlEN9Triniti2D4RectE` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::startMoveUIRightOut(DinoCap::UILabelMoveCtrl *,Triniti2D::Rect)` |
| Bytes SHA-256 | `82b80747145caae84eb26c22532600d321c14912d048d307b11b35cefe4d6d47` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UI_FLOW / PORTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: 0xf6e24 from startJigsaw 0xf6f00: label exit keys (cx + 20S, cy) -> (cx - 480S) twice, 0.2 s / 0.05 s, white; rect RectScale(0,124,480,40)+32 on iPad (0x42000000) while the entering banner used +22: shipped 10 px quirk. SelectJigsaw.StartJigsaw matches.

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: [`selectScene::startJigsaw`](../../../functions/dossiers/selectScene/FUN-0F6F00.md)

Callees: [`UILabelMoveCtrl::StartMove`](../../../functions/dossiers/UILabelMoveCtrl/FUN-0CD4B2.md), `___addsf3vfp`, `___mulsf3vfp`

## Named data it references

- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xF6E24   82b0       SUB     SP, SP, #8
0xF6E26   f0b5       PUSH    {R4-R7,LR}
0xF6E28   5e46       MOV     R6, R11
0xF6E2A   5546       MOV     R5, R10
0xF6E2C   4446       MOV     R4, R8
0xF6E2E   70b4       PUSH    {R4-R6}
0xF6E30   06af       ADD     R7, SP, #0x18
0xF6E32   92b0       SUB     SP, SP, #0x48
0xF6E34   1b93       STR     R3, [SP,#0x48+varg_r3]
0xF6E36   1b9b       LDR     R3, [SP,#0x48+varg_r3]
0xF6E38   1a92       STR     R2, [SP,#0x48+varg_r2]
0xF6E3A   0291       STR     R1, [SP,#0x48+var_40]
0xF6E3C   9b46       MOV     R11, R3
0xF6E3E   284b       LDR     R3, =(__ZN7DinoCap8g_iphoneE_ptr - 0xF6E48)
0xF6E40   1d9e       LDR     R6, [SP,#0x48+arg_4]
0xF6E42   1c9d       LDR     R5, [SP,#0x48+arg_0]
0xF6E44   7b44       ADD     R3, PC; __ZN7DinoCap8g_iphoneE_ptr
0xF6E46   1a9c       LDR     R4, [SP,#0x48+varg_r2]
0xF6E48   1b68       LDR     R3, [R3]; DinoCap::g_iphone ...
0xF6E4A   1b78       LDRB    R3, [R3]; DinoCap::g_iphone
0xF6E4C   002b       CMP     R3, #0
0xF6E4E   01d1       BNE     loc_F6E54
0xF6E50   244b       LDR     R3, =0x40000000
0xF6E52   00e0       B       loc_F6E56
```

