# menuScene::LogoEnter

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0CD05A` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xCD05A` |
| RVA (EA - 0x1000) | `0xCC05A` |
| File offset | `0xCC05A` |
| Size | 330 code bytes in 1 chunk(s), 151 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9menuScene9LogoEnterEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::menuScene::LogoEnter(void)` |
| Bytes SHA-256 | `8abcbb18e86a06c6f6ee95d520109f2828ed7d309439edd81e0ccbb5de4afee4` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.title.menuScene.flow` (PROVEN, NATIVE): Title scene = DinoCap::menuScene. startUI@0xca2a4: reads save (ReadFile), removes LogoImage texture, copies g_sound/g_fx into g_ongame_sound/g_ongame_fx, g_state=1, loads uimenu atlas (iPad/retina: Image/ip/uimenu_h.png, else Image/uimenu.png) with CODE-defined frames ui_menu_bgFrame (0,0,960,640; 1x 480x320), ui_menu_dinoFrame (0,640,846,322; 1x 0,320,423,161), ui_menu_startFrame (0,962,484,56; 1x 0,481,242,28), loa...
- Rule `dc2.ui.RectScale_and_ipad_offset` (LIKELY, NATIVE (decompiler)): RectScale(rect, s) multiplies x,y,w,h by s. Title scene layout is authored in 480x320 (iPhone 1x) coordinates and scaled (the s argument is a 'scale' not shown in decompile; iPad = 2x with extra offset (+32,+76) on logo/start, bg offset (0,+32,+64,+64)). RectOffset 0x170b6 used by LoadingEnter.

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: [`menuScene::OnTimeout`](../../../functions/dossiers/menuScene/FUN-0CD1E2.md)

Callees: [`RectScale`](../../../functions/dossiers/DinoCap/FUN-017068.md), [`menuScene::startScaleUI`](../../../functions/dossiers/menuScene/FUN-0CCDFE.md), `Triniti2D::Scene::GetUIManager`, `Triniti2D::UIImage::SetTexture`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`

## Strings it references

- `0x209240` "ui_menu_dinoFrame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x2274B0` GCC_except_table23_5
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A6C8` __ZN9Triniti2D9UIManager13CreateControlIN7DinoCap10UIMoveCtrlEEEPT_ii_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xCD05A   f0b5       PUSH    {R4-R7,LR}
0xCD05C   5e46       MOV     R6, R11
0xCD05E   5546       MOV     R5, R10
0xCD060   4446       MOV     R4, R8
0xCD062   70b4       PUSH    {R4-R6}
0xCD064   1af13cef   BLX     ___save_vfp_d8_d15_regs
0xCD068   16af       ADD     R7, SP, #arg_38
0xCD06A   9fb0       SUB     SP, SP, #0x7C ; '|'
0xCD06C   4d4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xCD076)
0xCD06E   0690       STR     R0, [SP,#0x7C+var_64]
0xCD070   0122       MOVS    R2, #1
0xCD072   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xCD074   6846       MOV     R0, SP
0xCD076   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xCD078   1290       STR     R0, [SP,#0x7C+var_34]
0xCD07A   08a8       ADD     R0, SP, #0x7C+fctx; lpfctx
0xCD07C   0e93       STR     R3, [SP,#0x7C+fctx.personality]
0xCD07E   4a4b       LDR     R3, =(GCC_except_table23_5 - 0xCD086)
0xCD080   1097       STR     R7, [SP,#0x7C+fctx.jbuf]
0xCD082   7b44       ADD     R3, PC; GCC_except_table23_5
0xCD084   0f93       STR     R3, [SP,#0x7C+fctx.lsda]
0xCD086   494b       LDR     R3, =(loc_CD13C - 0xCD08C)
0xCD088   7b44       ADD     R3, PC; loc_CD13C
0xCD08A   1343       ORRS    R3, R2
```

