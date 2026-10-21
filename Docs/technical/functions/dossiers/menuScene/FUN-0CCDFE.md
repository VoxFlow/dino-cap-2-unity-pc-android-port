# menuScene::startScaleUI

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0CCDFE` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xCCDFE` |
| RVA (EA - 0x1000) | `0xCBDFE` |
| File offset | `0xCBDFE` |
| Size | 184 code bytes in 1 chunk(s), 83 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9menuScene12startScaleUIEPNS_10UIMoveCtrlEN9Triniti2D4RectE` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::menuScene::startScaleUI(DinoCap::UIMoveCtrl *,Triniti2D::Rect)` |
| Bytes SHA-256 | `4ee47669d4069872ca7c8a88762a3b03cc9cdb25c8b39b1c8c486f79a877bcb5` |
| Direct code callers | 2 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.title.menuScene.flow` (PROVEN, NATIVE): Title scene = DinoCap::menuScene. startUI@0xca2a4: reads save (ReadFile), removes LogoImage texture, copies g_sound/g_fx into g_ongame_sound/g_ongame_fx, g_state=1, loads uimenu atlas (iPad/retina: Image/ip/uimenu_h.png, else Image/uimenu.png) with CODE-defined frames ui_menu_bgFrame (0,0,960,640; 1x 480x320), ui_menu_dinoFrame (0,640,846,322; 1x 0,320,423,161), ui_menu_startFrame (0,962,484,56; 1x 0,481,242,28), loa...
- Rule `dc2.ui.UIMoveCtrl.tween` (PROVEN, NATIVE (ARM-verified phase timers)): UIMoveCtrl::StartMove(start,mid,end,d1,d2) @0xcd466 stores 3 UIMoveCtrlData (x,y,w,h as centre-x? + Color32) and durations d1 (+124), d2 (+128), sets active (+132) and timer 0 (+136), phase 1 (+140). Update(dt) @0xcd796: t+=dt; phase1: r=t/d1, at r>=1 -> phase 2, t-=d1, r=1; position/size/colour lerp start->mid; phase2: r=t/d2, at r>=1 clamp and stop, lerp mid->end. Drawn rect = centre minus half size. Colour channel...

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/MenuScene.cs:80`](../../../../../Assets/Scripts/MenuScene.cs#L80) cites `0xCCDFE`

## Callers and callees (direct code references)

Callers: [`menuScene::NewGameEnter`](../../../functions/dossiers/menuScene/FUN-0CCECC.md), [`menuScene::LogoEnter`](../../../functions/dossiers/menuScene/FUN-0CD05A.md)

Callees: [`UIMoveCtrl::StartMove`](../../../functions/dossiers/UIMoveCtrl/FUN-0CD466.md), `___addsf3vfp`, `___mulsf3vfp`

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xCCDFE   82b0       SUB     SP, SP, #8
0xCCE00   f0b5       PUSH    {R4-R7,LR}
0xCCE02   5e46       MOV     R6, R11
0xCCE04   5546       MOV     R5, R10
0xCCE06   4446       MOV     R4, R8
0xCCE08   70b4       PUSH    {R4-R6}
0xCCE0A   06af       ADD     R7, SP, #0x18
0xCCE0C   92b0       SUB     SP, SP, #0x48
0xCCE0E   1b93       STR     R3, [SP,#0x48+varg_r3]
0xCCE10   1d9b       LDR     R3, [SP,#0x48+arg_4]
0xCCE12   1a92       STR     R2, [SP,#0x48+varg_r2]
0xCCE14   1c9e       LDR     R6, [SP,#0x48+arg_0]
0xCCE16   9a46       MOV     R10, R3
0xCCE18   1b9b       LDR     R3, [SP,#0x48+varg_r3]
0xCCE1A   1a9c       LDR     R4, [SP,#0x48+varg_r2]
0xCCE1C   0291       STR     R1, [SP,#0x48+var_40]
0xCCE1E   9b46       MOV     R11, R3
0xCCE20   0123       MOVS    R3, #1
0xCCE22   5b42       NEGS    R3, R3
0xCCE24   1193       STR     R3, [SP,#0x48+var_4]
0xCCE26   0c93       STR     R3, [SP,#0x48+var_18]
0xCCE28   0793       STR     R3, [SP,#0x48+var_2C]
0xCCE2A   234b       LDR     R3, =0x3F000000
0xCCE2C   301c       MOVS    R0, R6
```

