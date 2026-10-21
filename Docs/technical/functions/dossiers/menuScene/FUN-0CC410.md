# menuScene::NewGameLeave

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0CC410` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xCC410` |
| RVA (EA - 0x1000) | `0xCB410` |
| File offset | `0xCB410` |
| Size | 300 code bytes in 1 chunk(s), 138 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9menuScene12NewGameLeaveEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::menuScene::NewGameLeave(void)` |
| Bytes SHA-256 | `62f94eea988917755119ffb357f1bfc9dd4c30379f7b22e10724608a62ddfdff` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.title.exit_sequence` (SUPERSEDED, NATIVE (decompiler)): After the tap overlay (id23) completes (event1, state==1): InitAnim (random hero) + OnResume. OnResume@0xc9798 sets one-shot timers: LogoLeave 0.1 s, NewGameLeave 0.2 s, OnBgLeave 0.3 s. LogoLeave@0xcc580 re-tweens the dino logo (id0): start=current rect, mid x +20*s (1101004800=20), end x -480*s (Ã¢â‚¬Â¦), StartMove(0.05,0.2): nudges right then slides off left. NewGameLeave@0xcc410: same for the Tap-to-start image (...
- Rule `dc2.title.exit_sequence.r2` (LIKELY, NATIVE): Retains the historical lifecycle as a decompiler-derived lead: tap overlay completion -> InitAnim/OnResume -> exit timers -> loading tween completion -> state 4 -> LoadResource/selectScene. The old entry's NewGameLeave mid-offset -20*s is WRONG. ARM at 0xcc4e6..0xcc50e proves mid x=startX+(-50*s) and end x=startX+(480*s). Native 0xC2480000 is IEEE754 -50. Existing MenuScene.cs NewGameLeave already computes that -50 v...

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: [`menuScene::OnTimeout`](../../../functions/dossiers/menuScene/FUN-0CD1E2.md)

Callees: [`RectScale`](../../../functions/dossiers/DinoCap/FUN-017068.md), [`UIMoveCtrl::StartMove`](../../../functions/dossiers/UIMoveCtrl/FUN-0CD466.md), `Triniti2D::Scene::GetUIManager`, `Triniti2D::UIManager::GetControl`, `___addsf3vfp`, `___dynamic_cast`, `___mulsf3vfp`

## Named data it references

- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A5F4` __ZTIN9Triniti2D9UIControlE_ptr
- `0x22E468` `typeinfo for'Triniti2D::UIControl
- `0x22A650` __ZTIN7DinoCap10UIMoveCtrlE_ptr
- `0x22CDA0` `typeinfo for'DinoCap::UIMoveCtrl
- `0x2415E8` `vtable for'__cxxabiv1::__class_type_info
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xCC410   f0b5       PUSH    {R4-R7,LR}
0xCC412   5e46       MOV     R6, R11
0xCC414   5546       MOV     R5, R10
0xCC416   4446       MOV     R4, R8
0xCC418   70b4       PUSH    {R4-R6}
0xCC41A   06af       ADD     R7, SP, #0x18
0xCC41C   9ab0       SUB     SP, SP, #0x68
0xCC41E   474b       LDR     R3, =(__ZN7DinoCap8g_iphoneE_ptr - 0xCC426)
0xCC420   0290       STR     R0, [SP,#0x68+var_60]
0xCC422   7b44       ADD     R3, PC; __ZN7DinoCap8g_iphoneE_ptr
0xCC424   1b68       LDR     R3, [R3]; DinoCap::g_iphone ...
0xCC426   1b78       LDRB    R3, [R3]; DinoCap::g_iphone
0xCC428   002b       CMP     R3, #0
0xCC42A   02d1       BNE     loc_CC432
0xCC42C   4448       LDR     R0, =0x40000000
0xCC42E   8346       MOV     R11, R0
0xCC430   01e0       B       loc_CC436
0xCC432   4449       LDR     R1, =0x3F800000
0xCC434   8b46       MOV     R11, R1
0xCC436   444b       LDR     R3, =0x41E00000
0xCC438   0122       MOVS    R2, #1
0xCC43A   5242       NEGS    R2, R2
0xCC43C   1993       STR     R3, [SP,#0x68+var_4]
0xCC43E   434b       LDR     R3, =0x43720000
```

