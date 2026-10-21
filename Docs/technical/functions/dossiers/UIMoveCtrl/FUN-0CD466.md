# UIMoveCtrl::StartMove

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0CD466` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xCD466` |
| RVA (EA - 0x1000) | `0xCC466` |
| File offset | `0xCC466` |
| Size | 70 code bytes in 1 chunk(s), 35 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap10UIMoveCtrl9StartMoveERKNS_14UIMoveCtrlDataES3_S3_ff` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::UIMoveCtrl::StartMove(DinoCap::UIMoveCtrlData const&,DinoCap::UIMoveCtrlData const&,DinoCap::UIMoveCtrlData const&,float,float)` |
| Bytes SHA-256 | `791f0c9413852a49e0c7e482004bebbf31c52a90cd44647be4663c2eee733be3` |
| Direct code callers | 50 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.title.menuScene.flow` (PROVEN, NATIVE): Title scene = DinoCap::menuScene. startUI@0xca2a4: reads save (ReadFile), removes LogoImage texture, copies g_sound/g_fx into g_ongame_sound/g_ongame_fx, g_state=1, loads uimenu atlas (iPad/retina: Image/ip/uimenu_h.png, else Image/uimenu.png) with CODE-defined frames ui_menu_bgFrame (0,0,960,640; 1x 480x320), ui_menu_dinoFrame (0,640,846,322; 1x 0,320,423,161), ui_menu_startFrame (0,962,484,56; 1x 0,481,242,28), loa...
- Rule `dc2.ui.UIMoveCtrl.tween` (PROVEN, NATIVE (ARM-verified phase timers)): UIMoveCtrl::StartMove(start,mid,end,d1,d2) @0xcd466 stores 3 UIMoveCtrlData (x,y,w,h as centre-x? + Color32) and durations d1 (+124), d2 (+128), sets active (+132) and timer 0 (+136), phase 1 (+140). Update(dt) @0xcd796: t+=dt; phase1: r=t/d1, at r>=1 -> phase 2, t-=d1, r=1; position/size/colour lerp start->mid; phase2: r=t/d2, at r>=1 clamp and stop, lerp mid->end. Drawn rect = centre minus half size. Colour channel...

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/UiControls.cs:178`](../../../../../Assets/Scripts/UiControls.cs#L178) cites `0xCD466`

## Callers and callees (direct code references)

Callers: [`mainScene::LevelEnter`](../../../functions/dossiers/mainScene/FUN-0A348E.md), [`mainScene::LevelBGLeave`](../../../functions/dossiers/mainScene/FUN-0A38C2.md), [`mainScene::LevelBGEnter`](../../../functions/dossiers/mainScene/FUN-0A3A40.md), [`mainScene::MissCompMove`](../../../functions/dossiers/mainScene/FUN-0A3C52.md), [`mainScene::MissCompEnter`](../../../functions/dossiers/mainScene/FUN-0A3DF0.md), [`mainScene::startMoveUIRight`](../../../functions/dossiers/mainScene/FUN-0A4042.md), `mainScene::GameOverEnd`, [`mainScene::FailZEnter`](../../../functions/dossiers/mainScene/FUN-0A64CE.md), [`mainScene::MissionFailEnter`](../../../functions/dossiers/mainScene/FUN-0A671E.md), [`mainScene::GameOverStart`](../../../functions/dossiers/mainScene/FUN-0A6F70.md), [`mainScene::LevelLeave`](../../../functions/dossiers/mainScene/FUN-0B69E4.md), [`mainScene::SwitchWeapon`](../../../functions/dossiers/mainScene/FUN-0B7DB0.md), [`mainScene::GameClear`](../../../functions/dossiers/mainScene/FUN-0B7F5E.md), [`menuScene::NewGameLeave`](../../../functions/dossiers/menuScene/FUN-0CC410.md), [`menuScene::LogoLeave`](../../../functions/dossiers/menuScene/FUN-0CC580.md), [`menuScene::ButtonEffect`](../../../functions/dossiers/menuScene/FUN-0CC6F0.md), [`menuScene::startMoveUIUp`](../../../functions/dossiers/menuScene/FUN-0CC900.md), [`menuScene::startMoveUILeft`](../../../functions/dossiers/menuScene/FUN-0CCB6E.md), [`menuScene::startScaleUI`](../../../functions/dossiers/menuScene/FUN-0CCDFE.md), [`pauseCtrl::Leave`](../../../functions/dossiers/pauseCtrl/FUN-0CE93C.md), [`pauseCtrl::Enter`](../../../functions/dossiers/pauseCtrl/FUN-0CED0C.md), [`lifeModeScene::MissCompMove`](../../../functions/dossiers/lifeModeScene/FUN-0E28AC.md), [`lifeModeScene::MissCompEnter`](../../../functions/dossiers/lifeModeScene/FUN-0E3C54.md), [`lifeModeScene::FailZEnter`](../../../functions/dossiers/lifeModeScene/FUN-0E3E8E.md), [`lifeModeScene::MissionFailEnter`](../../../functions/dossiers/lifeModeScene/FUN-0E40DE.md), [`lifeModeScene::startMoveUIRight`](../../../functions/dossiers/lifeModeScene/FUN-0E4356.md), [`lifeModeScene::GameClear`](../../../functions/dossiers/lifeModeScene/FUN-0F0FF8.md), [`lifeModeScene::LevelEnter`](../../../functions/dossiers/lifeModeScene/FUN-0F13B4.md), [`lifeModeScene::LevelLeave`](../../../functions/dossiers/lifeModeScene/FUN-0F17E6.md), [`selectScene::startScaleUI`](../../../functions/dossiers/selectScene/FUN-0F77DC.md) ... (47)

Callees: none.

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xCD466   70b5       PUSH    {R4-R6,LR}
0xCD468   041c       MOVS    R4, R0
0xCD46A   4034       ADDS    R4, #0x40 ; '@'
0xCD46C   9c46       MOV     R12, R3
0xCD46E   68c9       LDM     R1!, {R3,R5,R6}
0xCD470   68c4       STM     R4!, {R3,R5,R6}
0xCD472   28c9       LDM     R1!, {R3,R5}
0xCD474   28c4       STM     R4!, {R3,R5}
0xCD476   011c       MOVS    R1, R0
0xCD478   6831       ADDS    R1, #0x68 ; 'h'
0xCD47A   58ca       LDM     R2!, {R3,R4,R6}
0xCD47C   58c1       STM     R1!, {R3,R4,R6}
0xCD47E   6346       MOV     R3, R12
0xCD480   60ca       LDM     R2!, {R5,R6}
0xCD482   60c1       STM     R1!, {R5,R6}
0xCD484   021c       MOVS    R2, R0
0xCD486   5432       ADDS    R2, #0x54 ; 'T'
0xCD488   32cb       LDM     R3!, {R1,R4,R5}
0xCD48A   32c2       STM     R2!, {R1,R4,R5}
0xCD48C   42cb       LDM     R3!, {R1,R6}
0xCD48E   42c2       STM     R2!, {R1,R6}
0xCD490   049a       LDR     R2, [SP,#arg_0]
0xCD492   059c       LDR     R4, [SP,#arg_4]
0xCD494   8023       MOVS    R3, #0x80
```

