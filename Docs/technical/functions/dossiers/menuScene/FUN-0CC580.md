# menuScene::LogoLeave

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0CC580` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xCC580` |
| RVA (EA - 0x1000) | `0xCB580` |
| File offset | `0xCB580` |
| Size | 298 code bytes in 1 chunk(s), 138 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9menuScene9LogoLeaveEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::menuScene::LogoLeave(void)` |
| Bytes SHA-256 | `6278cb41371271fdcc2032638a908e2264616f9ba4cbea1e33b82a8e772ba86b` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.title.menuScene.flow` (PROVEN, NATIVE): Title scene = DinoCap::menuScene. startUI@0xca2a4: reads save (ReadFile), removes LogoImage texture, copies g_sound/g_fx into g_ongame_sound/g_ongame_fx, g_state=1, loads uimenu atlas (iPad/retina: Image/ip/uimenu_h.png, else Image/uimenu.png) with CODE-defined frames ui_menu_bgFrame (0,0,960,640; 1x 480x320), ui_menu_dinoFrame (0,640,846,322; 1x 0,320,423,161), ui_menu_startFrame (0,962,484,56; 1x 0,481,242,28), loa...
- Rule `dc2.title.exit_sequence` (SUPERSEDED, NATIVE (decompiler)): After the tap overlay (id23) completes (event1, state==1): InitAnim (random hero) + OnResume. OnResume@0xc9798 sets one-shot timers: LogoLeave 0.1 s, NewGameLeave 0.2 s, OnBgLeave 0.3 s. LogoLeave@0xcc580 re-tweens the dino logo (id0): start=current rect, mid x +20*s (1101004800=20), end x -480*s (Ã¢â‚¬Â¦), StartMove(0.05,0.2): nudges right then slides off left. NewGameLeave@0xcc410: same for the Tap-to-start image (...

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
0xCC580   f0b5       PUSH    {R4-R7,LR}
0xCC582   5e46       MOV     R6, R11
0xCC584   5546       MOV     R5, R10
0xCC586   4446       MOV     R4, R8
0xCC588   70b4       PUSH    {R4-R6}
0xCC58A   06af       ADD     R7, SP, #0x18
0xCC58C   99b0       SUB     SP, SP, #0x64
0xCC58E   474b       LDR     R3, =(__ZN7DinoCap8g_iphoneE_ptr - 0xCC596)
0xCC590   8346       MOV     R11, R0
0xCC592   7b44       ADD     R3, PC; __ZN7DinoCap8g_iphoneE_ptr
0xCC594   1b68       LDR     R3, [R3]; DinoCap::g_iphone ...
0xCC596   1b78       LDRB    R3, [R3]; DinoCap::g_iphone
0xCC598   002b       CMP     R3, #0
0xCC59A   02d1       BNE     loc_CC5A2
0xCC59C   4448       LDR     R0, =0x40000000
0xCC59E   8246       MOV     R10, R0
0xCC5A0   01e0       B       loc_CC5A6
0xCC5A2   4449       LDR     R1, =0x3F800000
0xCC5A4   8a46       MOV     R10, R1
0xCC5A6   444b       LDR     R3, =0x43090000
0xCC5A8   0122       MOVS    R2, #1
0xCC5AA   5242       NEGS    R2, R2
0xCC5AC   1893       STR     R3, [SP,#0x64+var_4]
0xCC5AE   434b       LDR     R3, =0x43E00000
```

