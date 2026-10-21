# menuScene::startMoveUIUp

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0CC900` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xCC900` |
| RVA (EA - 0x1000) | `0xCB900` |
| File offset | `0xCB900` |
| Size | 196 code bytes in 1 chunk(s), 89 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9menuScene13startMoveUIUpEPNS_10UIMoveCtrlEN9Triniti2D4RectE` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::menuScene::startMoveUIUp(DinoCap::UIMoveCtrl *,Triniti2D::Rect)` |
| Bytes SHA-256 | `4f188e9a9e37f9bae1e0d12f8a182e07edece690145f8d0d60137b98ac9c65a4` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.title.exit_sequence` (SUPERSEDED, NATIVE (decompiler)): After the tap overlay (id23) completes (event1, state==1): InitAnim (random hero) + OnResume. OnResume@0xc9798 sets one-shot timers: LogoLeave 0.1 s, NewGameLeave 0.2 s, OnBgLeave 0.3 s. LogoLeave@0xcc580 re-tweens the dino logo (id0): start=current rect, mid x +20*s (1101004800=20), end x -480*s (Ã¢â‚¬Â¦), StartMove(0.05,0.2): nudges right then slides off left. NewGameLeave@0xcc410: same for the Tap-to-start image (...

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/MenuScene.cs:90`](../../../../../Assets/Scripts/MenuScene.cs#L90) cites `0xCC900`

## Callers and callees (direct code references)

Callers: [`menuScene::OnBgLeave`](../../../functions/dossiers/menuScene/FUN-0CC9E4.md)

Callees: [`UIMoveCtrl::StartMove`](../../../functions/dossiers/UIMoveCtrl/FUN-0CD466.md), `___addsf3vfp`, `___mulsf3vfp`

## Named data it references

- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xCC900   82b0       SUB     SP, SP, #8
0xCC902   f0b5       PUSH    {R4-R7,LR}
0xCC904   5e46       MOV     R6, R11
0xCC906   5546       MOV     R5, R10
0xCC908   4446       MOV     R4, R8
0xCC90A   70b4       PUSH    {R4-R6}
0xCC90C   06af       ADD     R7, SP, #0x18
0xCC90E   92b0       SUB     SP, SP, #0x48
0xCC910   1b93       STR     R3, [SP,#0x48+varg_r3]
0xCC912   1d9b       LDR     R3, [SP,#0x48+arg_4]
0xCC914   1a92       STR     R2, [SP,#0x48+varg_r2]
0xCC916   0291       STR     R1, [SP,#0x48+var_40]
0xCC918   9846       MOV     R8, R3
0xCC91A   1b9b       LDR     R3, [SP,#0x48+varg_r3]
0xCC91C   1c9e       LDR     R6, [SP,#0x48+arg_0]
0xCC91E   1a9c       LDR     R4, [SP,#0x48+varg_r2]
0xCC920   9b46       MOV     R11, R3
0xCC922   284b       LDR     R3, =(__ZN7DinoCap8g_iphoneE_ptr - 0xCC928)
0xCC924   7b44       ADD     R3, PC; __ZN7DinoCap8g_iphoneE_ptr
0xCC926   1b68       LDR     R3, [R3]; DinoCap::g_iphone ...
0xCC928   1b78       LDRB    R3, [R3]; DinoCap::g_iphone
0xCC92A   002b       CMP     R3, #0
0xCC92C   01d1       BNE     loc_CC932
0xCC92E   264b       LDR     R3, =0x40000000
```

