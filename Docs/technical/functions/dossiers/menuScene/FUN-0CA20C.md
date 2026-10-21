# menuScene::OnUIEvent

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0CA20C` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xCA20C` |
| RVA (EA - 0x1000) | `0xC920C` |
| File offset | `0xC920C` |
| Size | 144 code bytes in 1 chunk(s), 53 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9menuScene9OnUIEventEPN9Triniti2D9UIControlEiff` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::menuScene::OnUIEvent(Triniti2D::UIControl *,int,float,float)` |
| Bytes SHA-256 | `d01aa2ffabbd642dd4a8a2058412fc19dda57a430b6139caaa897d935c4359a8` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.title.menuScene.flow` (PROVEN, NATIVE): Title scene = DinoCap::menuScene. startUI@0xca2a4: reads save (ReadFile), removes LogoImage texture, copies g_sound/g_fx into g_ongame_sound/g_ongame_fx, g_state=1, loads uimenu atlas (iPad/retina: Image/ip/uimenu_h.png, else Image/uimenu.png) with CODE-defined frames ui_menu_bgFrame (0,0,960,640; 1x 480x320), ui_menu_dinoFrame (0,640,846,322; 1x 0,320,423,161), ui_menu_startFrame (0,962,484,56; 1x 0,481,242,28), loa...
- Rule `dc2.title.tap_flow` (PROVEN, NATIVE): Tap handling: control id1 (Tap-to-start UIMoveCtrl) pop-in completion (event 1) while scene state(+12)==0 sets ready flag (+280). OnTouchEvent@0xcc8b2: if state not 1/4 and flag set: clear flag, ButtonEffect('ui_menu_startFrame', control id1), life_Mode=0, state=1. ButtonEffect@0xcc6f0 reuses/creates UIMoveCtrl id 23 (layer 3) with the same texture at the same rect and StartMove(d1=0.05,d2=0.2): a cloned overlay that...
- Rule `dc2.title.exit_sequence` (SUPERSEDED, NATIVE (decompiler)): After the tap overlay (id23) completes (event1, state==1): InitAnim (random hero) + OnResume. OnResume@0xc9798 sets one-shot timers: LogoLeave 0.1 s, NewGameLeave 0.2 s, OnBgLeave 0.3 s. LogoLeave@0xcc580 re-tweens the dino logo (id0): start=current rect, mid x +20*s (1101004800=20), end x -480*s (Ã¢â‚¬Â¦), StartMove(0.05,0.2): nudges right then slides off left. NewGameLeave@0xcc410: same for the Tap-to-start image (...

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: [`menuScene::OnResume`](../../../functions/dossiers/menuScene/FUN-0C9798.md), [`menuScene::InitAnim`](../../../functions/dossiers/menuScene/FUN-0C9CE0.md), `Triniti2D::Scene::SetTimer`, `Triniti2D::UIControl::Id`, `___switch8`

## Strings it references

- `0x2091FC` "LoadingEnter"

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xCA20C   f0b5       PUSH    {R4-R7,LR}
0xCA20E   03af       ADD     R7, SP, #0xC
0xCA210   061c       MOVS    R6, R0
0xCA212   081c       MOVS    R0, R1; this
0xCA214   0c1c       MOVS    R4, R1
0xCA216   151c       MOVS    R5, R2
0xCA218   b4f021fa   BL      __ZN9Triniti2D9UIControl2IdEv; Triniti2D::UIControl::Id(void)
0xCA21C   431e       SUBS    R3, R0, #1
0xCA21E   181c       MOVS    R0, R3
0xCA220   1df132ee   BLX     ___switch8; switch 24 cases
0xCA23E   012d       CMP     R5, #1; jumptable 000CA220 case 0
0xCA240   2bd1       BNE     def_CA220; jumptable 000CA220 default case, cases 1-6,8-12,14-21,23
0xCA242   f368       LDR     R3, [R6,#0xC]
0xCA244   002b       CMP     R3, #0
0xCA246   28d1       BNE     def_CA220; jumptable 000CA220 default case, cases 1-6,8-12,14-21,23
0xCA248   8c235b00   MOVS    R3, #0x118
0xCA24C   f554       STRB    R5, [R6,R3]
0xCA24E   24e0       B       def_CA220; jumptable 000CA220 default case, cases 1-6,8-12,14-21,23
0xCA250   012d       CMP     R5, #1; jumptable 000CA220 case 7
0xCA252   22d1       BNE     def_CA220; jumptable 000CA220 default case, cases 1-6,8-12,14-21,23
0xCA254   f368       LDR     R3, [R6,#0xC]
0xCA256   012b       CMP     R3, #1
0xCA258   1fd1       BNE     def_CA220; jumptable 000CA220 default case, cases 1-6,8-12,14-21,23
0xCA25A   1049       LDR     R1, =(aLoadingenter - 0xCA264); "LoadingEnter"
```

