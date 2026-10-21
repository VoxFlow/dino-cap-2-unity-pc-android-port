# menuScene::OnBgLeave

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0CC9E4` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xCC9E4` |
| RVA (EA - 0x1000) | `0xCB9E4` |
| File offset | `0xCB9E4` |
| Size | 340 code bytes in 1 chunk(s), 154 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9menuScene9OnBgLeaveEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::menuScene::OnBgLeave(void)` |
| Bytes SHA-256 | `a5245b7642d4a71fb8310eeafbbadda6c5315bb498ae4e362f4f6d8a98624b44` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.title.exit_sequence` (SUPERSEDED, NATIVE (decompiler)): After the tap overlay (id23) completes (event1, state==1): InitAnim (random hero) + OnResume. OnResume@0xc9798 sets one-shot timers: LogoLeave 0.1 s, NewGameLeave 0.2 s, OnBgLeave 0.3 s. LogoLeave@0xcc580 re-tweens the dino logo (id0): start=current rect, mid x +20*s (1101004800=20), end x -480*s (Ã¢â‚¬Â¦), StartMove(0.05,0.2): nudges right then slides off left. NewGameLeave@0xcc410: same for the Tap-to-start image (...

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: [`menuScene::OnTimeout`](../../../functions/dossiers/menuScene/FUN-0CD1E2.md)

Callees: [`RectScale`](../../../functions/dossiers/DinoCap/FUN-017068.md), [`menuScene::startMoveUIUp`](../../../functions/dossiers/menuScene/FUN-0CC900.md), `Triniti2D::Scene::GetUIManager`, `Triniti2D::UIImage::SetTexture`, `Triniti2D::UIManager::DeleteControl`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`

## Strings it references

- `0x209174` "ui_menu_bgFrame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x227498` GCC_except_table20_5
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A6C8` __ZN9Triniti2D9UIManager13CreateControlIN7DinoCap10UIMoveCtrlEEEPT_ii_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xCC9E4   f0b5       PUSH    {R4-R7,LR}
0xCC9E6   5e46       MOV     R6, R11
0xCC9E8   5546       MOV     R5, R10
0xCC9EA   4446       MOV     R4, R8
0xCC9EC   70b4       PUSH    {R4-R6}
0xCC9EE   1bf178ea   BLX     ___save_vfp_d8_d15_regs
0xCC9F2   16af       ADD     R7, SP, #arg_38
0xCC9F4   9fb0       SUB     SP, SP, #0x7C ; '|'
0xCC9F6   504b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xCCA00)
0xCC9F8   0690       STR     R0, [SP,#0x7C+var_64]
0xCC9FA   0122       MOVS    R2, #1
0xCC9FC   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xCC9FE   6846       MOV     R0, SP
0xCCA00   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xCCA02   1290       STR     R0, [SP,#0x7C+var_34]
0xCCA04   08a8       ADD     R0, SP, #0x7C+fctx; lpfctx
0xCCA06   0e93       STR     R3, [SP,#0x7C+fctx.personality]
0xCCA08   4c4b       LDR     R3, =(GCC_except_table20_5 - 0xCCA10)
0xCCA0A   1097       STR     R7, [SP,#0x7C+fctx.jbuf]
0xCCA0C   7b44       ADD     R3, PC; GCC_except_table20_5
0xCCA0E   0f93       STR     R3, [SP,#0x7C+fctx.lsda]
0xCCA10   4b4b       LDR     R3, =(loc_CCADA - 0xCCA16)
0xCCA12   7b44       ADD     R3, PC; loc_CCADA
0xCCA14   1343       ORRS    R3, R2
```

