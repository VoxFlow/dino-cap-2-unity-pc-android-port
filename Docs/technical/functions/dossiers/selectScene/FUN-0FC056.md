# selectScene::startMoveUIDown

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0FC056` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xFC056` |
| RVA (EA - 0x1000) | `0xFB056` |
| File offset | `0xFB056` |
| Size | 194 code bytes in 1 chunk(s), 88 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene15startMoveUIDownEPNS_10UIMoveCtrlEN9Triniti2D4RectE` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::startMoveUIDown(DinoCap::UIMoveCtrl *,Triniti2D::Rect)` |
| Bytes SHA-256 | `b1fe3a866a43c0713e7c87ff9e7c0a4285b7a9dfbf0de725f9031d51e9e2e77b` |
| Direct code callers | 10 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.ui.UIMoveCtrl.tween` (PROVEN, NATIVE (ARM-verified phase timers)): UIMoveCtrl::StartMove(start,mid,end,d1,d2) @0xcd466 stores 3 UIMoveCtrlData (x,y,w,h as centre-x? + Color32) and durations d1 (+124), d2 (+128), sets active (+132) and timer 0 (+136), phase 1 (+140). Update(dt) @0xcd796: t+=dt; phase1: r=t/d1, at r>=1 -> phase 2, t-=d1, r=1; position/size/colour lerp start->mid; phase2: r=t/d2, at r>=1 clamp and stop, lerp mid->end. Drawn rect = centre minus half size. Colour channel...
- Rule `dc2.options.panel_layout` (PROVEN, NATIVE (decompiler)): selectScene::OnOptionEnter@0xfc138 builds: id78 full-screen dim = whiteFrame tinted black alpha 0xD0 (208/255) on layer 5; then layer-6 items that slide DOWN into place (startMoveUIDown): id79 op_titleFrame, id82 op_soundFrame label + id83 state icon (op_on1/op_off1 by g_fx), id80 op_musicFrame label + id81 state icon (by g_sound), id84 op_supportFrame, id85 op_reviewFrame, id87 op_reset_peofileFrame, id88 ui_btn_bac...

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/SelectScene.cs:61`](../../../../../Assets/Scripts/SelectScene.cs#L61) cites `0xFC056`

## Callers and callees (direct code references)

Callers: [`selectScene::OnOptionEnter`](../../../functions/dossiers/selectScene/FUN-0FC138.md)

Callees: [`UIMoveCtrl::StartMove`](../../../functions/dossiers/UIMoveCtrl/FUN-0CD466.md), `___addsf3vfp`, `___mulsf3vfp`

## Named data it references

- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xFC056   82b0       SUB     SP, SP, #8
0xFC058   f0b5       PUSH    {R4-R7,LR}
0xFC05A   5e46       MOV     R6, R11
0xFC05C   5546       MOV     R5, R10
0xFC05E   4446       MOV     R4, R8
0xFC060   70b4       PUSH    {R4-R6}
0xFC062   06af       ADD     R7, SP, #0x18
0xFC064   92b0       SUB     SP, SP, #0x48
0xFC066   1b93       STR     R3, [SP,#0x48+varg_r3]
0xFC068   1d9b       LDR     R3, [SP,#0x48+arg_4]
0xFC06A   1a92       STR     R2, [SP,#0x48+varg_r2]
0xFC06C   0291       STR     R1, [SP,#0x48+var_40]
0xFC06E   9846       MOV     R8, R3
0xFC070   1b9b       LDR     R3, [SP,#0x48+varg_r3]
0xFC072   1c9e       LDR     R6, [SP,#0x48+arg_0]
0xFC074   1a9c       LDR     R4, [SP,#0x48+varg_r2]
0xFC076   9b46       MOV     R11, R3
0xFC078   274b       LDR     R3, =(__ZN7DinoCap8g_iphoneE_ptr - 0xFC07E)
0xFC07A   7b44       ADD     R3, PC; __ZN7DinoCap8g_iphoneE_ptr
0xFC07C   1b68       LDR     R3, [R3]; DinoCap::g_iphone ...
0xFC07E   1b78       LDRB    R3, [R3]; DinoCap::g_iphone
0xFC080   002b       CMP     R3, #0
0xFC082   01d1       BNE     loc_FC088
0xFC084   254b       LDR     R3, =0x40000000
```

