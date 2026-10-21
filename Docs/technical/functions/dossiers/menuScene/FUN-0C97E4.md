# menuScene::InitUI

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0C97E4` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xC97E4` |
| RVA (EA - 0x1000) | `0xC87E4` |
| File offset | `0xC87E4` |
| Size | 364 code bytes in 1 chunk(s), 166 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9menuScene6InitUIEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::menuScene::InitUI(void)` |
| Bytes SHA-256 | `3660a674fae5813415f9bb3e2d2af2a2321131b3be900ddc4fcf1337fc1b6811` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.title.menuScene.flow` (PROVEN, NATIVE): Title scene = DinoCap::menuScene. startUI@0xca2a4: reads save (ReadFile), removes LogoImage texture, copies g_sound/g_fx into g_ongame_sound/g_ongame_fx, g_state=1, loads uimenu atlas (iPad/retina: Image/ip/uimenu_h.png, else Image/uimenu.png) with CODE-defined frames ui_menu_bgFrame (0,0,960,640; 1x 480x320), ui_menu_dinoFrame (0,640,846,322; 1x 0,320,423,161), ui_menu_startFrame (0,962,484,56; 1x 0,481,242,28), loa...
- Rule `dc2.ui.RectScale_and_ipad_offset` (LIKELY, NATIVE (decompiler)): RectScale(rect, s) multiplies x,y,w,h by s. Title scene layout is authored in 480x320 (iPhone 1x) coordinates and scaled (the s argument is a 'scale' not shown in decompile; iPad = 2x with extra offset (+32,+76) on logo/start, bg offset (0,+32,+64,+64)). RectOffset 0x170b6 used by LoadingEnter.

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: [`menuScene::startUI`](../../../functions/dossiers/menuScene/FUN-0CA2A4.md)

Callees: [`RectScale`](../../../functions/dossiers/DinoCap/FUN-017068.md), `Triniti2D::Scene::GetUIManager`, `Triniti2D::Scene::SetTimer`, `Triniti2D::UIImage::SetTexture`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`

## Strings it references

- `0x209174` "ui_menu_bgFrame"
- `0x209184` "LogoEnter"
- `0x209190` "NewGameEnter"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x22736E` GCC_except_table3_11
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A6C8` __ZN9Triniti2D9UIManager13CreateControlIN7DinoCap10UIMoveCtrlEEEPT_ii_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xC97E4   f0b5       PUSH    {R4-R7,LR}
0xC97E6   5e46       MOV     R6, R11
0xC97E8   5546       MOV     R5, R10
0xC97EA   4446       MOV     R4, R8
0xC97EC   70b4       PUSH    {R4-R6}
0xC97EE   1ef178eb   BLX     ___save_vfp_d8_d15_regs
0xC97F2   16af       ADD     R7, SP, #arg_38
0xC97F4   a0b0       SUB     SP, SP, #0x80
0xC97F6   564b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xC9800)
0xC97F8   0690       STR     R0, [SP,#0x80+var_68]
0xC97FA   0122       MOVS    R2, #1
0xC97FC   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xC97FE   6846       MOV     R0, SP
0xC9800   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xC9802   1390       STR     R0, [SP,#0x80+var_34]
0xC9804   09a8       ADD     R0, SP, #0x80+fctx; lpfctx
0xC9806   0f93       STR     R3, [SP,#0x80+fctx.personality]
0xC9808   524b       LDR     R3, =(GCC_except_table3_11 - 0xC9810)
0xC980A   1197       STR     R7, [SP,#0x80+fctx.jbuf]
0xC980C   7b44       ADD     R3, PC; GCC_except_table3_11
0xC980E   1093       STR     R3, [SP,#0x80+fctx.lsda]
0xC9810   514b       LDR     R3, =(loc_C98DA - 0xC9816)
0xC9812   7b44       ADD     R3, PC; loc_C98DA
0xC9814   1343       ORRS    R3, R2
```

