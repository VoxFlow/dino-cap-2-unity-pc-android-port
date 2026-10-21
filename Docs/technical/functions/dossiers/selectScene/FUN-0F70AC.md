# selectScene::startMoveUILeft

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0F70AC` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xF70AC` |
| RVA (EA - 0x1000) | `0xF60AC` |
| File offset | `0xF60AC` |
| Size | 194 code bytes in 1 chunk(s), 88 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene15startMoveUILeftEPNS_15UILabelMoveCtrlEN9Triniti2D4RectE` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::startMoveUILeft(DinoCap::UILabelMoveCtrl *,Triniti2D::Rect)` |
| Bytes SHA-256 | `0888e3c3519e5f436159904d96508c70a2b8460010c217eb09ec1b6e09048a17` |
| Direct code callers | 6 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UI_FLOW / PORTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: selectScene::startMoveUILeft (showMapUI / showUpgrade slide), SelectScene.cs / SelectArmory.cs

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: [`selectScene::showMapUI`](../../../functions/dossiers/selectScene/FUN-10218C.md), [`selectScene::showUpgrade`](../../../functions/dossiers/selectScene/FUN-10363E.md)

Callees: [`UILabelMoveCtrl::StartMove`](../../../functions/dossiers/UILabelMoveCtrl/FUN-0CD4B2.md), `___addsf3vfp`, `___mulsf3vfp`

## Named data it references

- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xF70AC   82b0       SUB     SP, SP, #8
0xF70AE   f0b5       PUSH    {R4-R7,LR}
0xF70B0   5e46       MOV     R6, R11
0xF70B2   5546       MOV     R5, R10
0xF70B4   4446       MOV     R4, R8
0xF70B6   70b4       PUSH    {R4-R6}
0xF70B8   06af       ADD     R7, SP, #0x18
0xF70BA   92b0       SUB     SP, SP, #0x48
0xF70BC   1b93       STR     R3, [SP,#0x48+varg_r3]
0xF70BE   1d9b       LDR     R3, [SP,#0x48+arg_4]
0xF70C0   1a92       STR     R2, [SP,#0x48+varg_r2]
0xF70C2   0291       STR     R1, [SP,#0x48+var_40]
0xF70C4   9846       MOV     R8, R3
0xF70C6   1b9b       LDR     R3, [SP,#0x48+varg_r3]
0xF70C8   1c9e       LDR     R6, [SP,#0x48+arg_0]
0xF70CA   1a9c       LDR     R4, [SP,#0x48+varg_r2]
0xF70CC   9b46       MOV     R11, R3
0xF70CE   284b       LDR     R3, =(__ZN7DinoCap8g_iphoneE_ptr - 0xF70D4)
0xF70D0   7b44       ADD     R3, PC; __ZN7DinoCap8g_iphoneE_ptr
0xF70D2   1b68       LDR     R3, [R3]; DinoCap::g_iphone ...
0xF70D4   1b78       LDRB    R3, [R3]; DinoCap::g_iphone
0xF70D6   002b       CMP     R3, #0
0xF70D8   01d1       BNE     loc_F70DE
0xF70DA   264b       LDR     R3, =0x40000000
```

