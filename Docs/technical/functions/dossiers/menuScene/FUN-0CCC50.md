# menuScene::LoadingEnter

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0CCC50` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xCCC50` |
| RVA (EA - 0x1000) | `0xCBC50` |
| File offset | `0xCBC50` |
| Size | 372 code bytes in 1 chunk(s), 170 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9menuScene12LoadingEnterEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::menuScene::LoadingEnter(void)` |
| Bytes SHA-256 | `a949dc420991ea2751da71f23fd9a574e72bb45522ad692da40aa10d4bb2992d` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.title.tap_flow` (PROVEN, NATIVE): Tap handling: control id1 (Tap-to-start UIMoveCtrl) pop-in completion (event 1) while scene state(+12)==0 sets ready flag (+280). OnTouchEvent@0xcc8b2: if state not 1/4 and flag set: clear flag, ButtonEffect('ui_menu_startFrame', control id1), life_Mode=0, state=1. ButtonEffect@0xcc6f0 reuses/creates UIMoveCtrl id 23 (layer 3) with the same texture at the same rect and StartMove(d1=0.05,d2=0.2): a cloned overlay that...
- Rule `dc2.title.exit_sequence` (SUPERSEDED, NATIVE (decompiler)): After the tap overlay (id23) completes (event1, state==1): InitAnim (random hero) + OnResume. OnResume@0xc9798 sets one-shot timers: LogoLeave 0.1 s, NewGameLeave 0.2 s, OnBgLeave 0.3 s. LogoLeave@0xcc580 re-tweens the dino logo (id0): start=current rect, mid x +20*s (1101004800=20), end x -480*s (Ã¢â‚¬Â¦), StartMove(0.05,0.2): nudges right then slides off left. NewGameLeave@0xcc410: same for the Tap-to-start image (...

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: [`menuScene::OnTimeout`](../../../functions/dossiers/menuScene/FUN-0CD1E2.md)

Callees: [`RectScale`](../../../functions/dossiers/DinoCap/FUN-017068.md), [`RectOffset`](../../../functions/dossiers/DinoCap/FUN-0170B6.md), [`menuScene::startMoveUILeft`](../../../functions/dossiers/menuScene/FUN-0CCB6E.md), `Triniti2D::Scene::GetUIManager`, `Triniti2D::UIImage::SetTexture`, `Triniti2D::UIManager::DeleteControl`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`

## Strings it references

- `0x206F7C` "loadingFrame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x2274A0` GCC_except_table21_4
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A6C8` __ZN9Triniti2D9UIManager13CreateControlIN7DinoCap10UIMoveCtrlEEEPT_ii_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xCCC50   f0b5       PUSH    {R4-R7,LR}
0xCCC52   5e46       MOV     R6, R11
0xCCC54   5546       MOV     R5, R10
0xCCC56   4446       MOV     R4, R8
0xCCC58   70b4       PUSH    {R4-R6}
0xCCC5A   1bf142e9   BLX     ___save_vfp_d8_d15_regs
0xCCC5E   16af       ADD     R7, SP, #arg_38
0xCCC60   9fb0       SUB     SP, SP, #0x7C ; '|'
0xCCC62   584b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xCCC6C)
0xCCC64   0690       STR     R0, [SP,#0x7C+var_64]
0xCCC66   0122       MOVS    R2, #1
0xCCC68   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xCCC6A   6846       MOV     R0, SP
0xCCC6C   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xCCC6E   1290       STR     R0, [SP,#0x7C+var_34]
0xCCC70   08a8       ADD     R0, SP, #0x7C+fctx; lpfctx
0xCCC72   0e93       STR     R3, [SP,#0x7C+fctx.personality]
0xCCC74   544b       LDR     R3, =(GCC_except_table21_4 - 0xCCC7C)
0xCCC76   1097       STR     R7, [SP,#0x7C+fctx.jbuf]
0xCCC78   7b44       ADD     R3, PC; GCC_except_table21_4
0xCCC7A   0f93       STR     R3, [SP,#0x7C+fctx.lsda]
0xCCC7C   534b       LDR     R3, =(loc_CCD50 - 0xCCC82)
0xCCC7E   7b44       ADD     R3, PC; loc_CCD50
0xCCC80   1343       ORRS    R3, R2
```

