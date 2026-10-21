# selectScene::showJigsawCount

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0FEC34` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xFEC34` |
| RVA (EA - 0x1000) | `0xFDC34` |
| File offset | `0xFDC34` |
| Size | 722 code bytes in 1 chunk(s), 336 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene15showJigsawCountEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::showJigsawCount(void)` |
| Bytes SHA-256 | `68f50b5a05bcb6c8c05003df8c0c212ed9cf63b503696d11b197c66c90183de9` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.jigsaw.onload_layout` (PROVEN, NATIVE (disasm OnLoad case 5 0x1064c6; decompile showJigsawUI 0xfdf70, showJigsawCount 0xfec34)): iPad (g_iphone==0): loadImageByConf Image/ip/jigsaw_h, jigsaw_h_1, jigeff2x; GameObject background ui_bg_allFrame sized (960+g_w_offset,640+g_h_offset) at world (0,0); cursor this+156=0; TadShow(false); showJigsawCount: ReadFile, pending list = pieces with g_jigsaw_active != g_jigsaw_get and get != 0 (ascending group,piece). Pending => label 599 (Adventure_22, RGB(0xEF,0xCD,0), align (1,1), rect (0,124,480,40) via Re...

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/SelectJigsaw.cs:8`](../../../../../Assets/Scripts/SelectJigsaw.cs#L8) cites `0xFEC34`
- [`Assets/Scripts/SelectJigsaw.cs:88`](../../../../../Assets/Scripts/SelectJigsaw.cs#L88) cites `0xFEC34`
- [`Assets/Scripts/SelectJigsaw.cs:96`](../../../../../Assets/Scripts/SelectJigsaw.cs#L96) cites `0xFED9E`
- [`Assets/Scripts/SelectJigsaw.cs:97`](../../../../../Assets/Scripts/SelectJigsaw.cs#L97) cites `0xFEC34`

## Callers and callees (direct code references)

Callers: [`selectScene::OnLoad`](../../../functions/dossiers/selectScene/FUN-10600A.md)

Callees: [`RectScale`](../../../functions/dossiers/DinoCap/FUN-017068.md), [`RectOffset`](../../../functions/dossiers/DinoCap/FUN-0170B6.md), `ReadFile`, [`selectScene::startMoveUIRight`](../../../functions/dossiers/selectScene/FUN-0F6FC8.md), [`selectScene::showJigsawUI`](../../../functions/dossiers/selectScene/FUN-0FDF70.md), `Triniti2D::Scene::GetUIManager`, `Triniti2D::Scene::SetTimer`, `Triniti2D::UILabel::SetAlignment`, `Triniti2D::UILabel::SetFont`, `Triniti2D::UILabel::SetColor`, `Triniti2D::UILabel::SetText`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___floatsisfvfp`, `_sprintf`

## Strings it references

- `0x2065DC` "Adventure_22"
- `0x20A6CC` "YOU HAVE %d JIGSAW"
- `0x20A6E0` "YOU HAVE %d JIGSAWS"
- `0x20A604` "startJigsaw"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x2281E8` GCC_except_table48
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22AB78` __ZN7DinoCap15g_jigsaw_activeE_ptr
- `0x23E0F8` DinoCap::g_jigsaw_active
- `0x22A694` __ZN7DinoCap12g_jigsaw_getE_ptr
- `0x23E0A8` DinoCap::g_jigsaw_get
- `0x22A0F4` __ZNSt6vectorIN9Triniti2D7Vector2ESaIS1_EE9push_backERKS1__ptr
- `0x22A108` __ZNKSt6vectorIN9Triniti2D7Vector2ESaIS1_EE4sizeEv_ptr
- `0x22A654` __ZN9Triniti2D9UIManager13CreateControlIN7DinoCap15UILabelMoveCtrlEEEPT_ii_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xFEC34   f0b5       PUSH    {R4-R7,LR}
0xFEC36   5e46       MOV     R6, R11
0xFEC38   5546       MOV     R5, R10
0xFEC3A   4446       MOV     R4, R8
0xFEC3C   70b4       PUSH    {R4-R6}
0xFEC3E   e9f050e9   BLX     ___save_vfp_d8_d15_regs
0xFEC42   16af       ADD     R7, SP, #arg_38
0xFEC44   b5b0       SUB     SP, SP, #0xD4
0xFEC46   b04b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xFEC50)
0xFEC48   0990       STR     R0, [SP,#0xD4+var_B0]
0xFEC4A   0122       MOVS    R2, #1
0xFEC4C   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xFEC4E   6846       MOV     R0, SP
0xFEC50   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xFEC52   1c90       STR     R0, [SP,#0xD4+var_64]
0xFEC54   12a8       ADD     R0, SP, #0xD4+fctx; lpfctx
0xFEC56   1893       STR     R3, [SP,#0xD4+fctx.personality]
0xFEC58   ac4b       LDR     R3, =(GCC_except_table48 - 0xFEC60)
0xFEC5A   1a97       STR     R7, [SP,#0xD4+fctx.jbuf]
0xFEC5C   7b44       ADD     R3, PC; GCC_except_table48
0xFEC5E   1993       STR     R3, [SP,#0xD4+fctx.lsda]
0xFEC60   ab4b       LDR     R3, =(loc_FEE60 - 0xFEC66)
0xFEC62   7b44       ADD     R3, PC; loc_FEE60
0xFEC64   1343       ORRS    R3, R2
```

**Around `0xFED9E` (cited by the Unity code)**

```
0xFED90   76f06bfa   BL      __ZN9Triniti2D5Scene12GetUIManagerEv; Triniti2D::Scene::GetUIManager(void)
0xFED94   6c4b       LDR     R3, =(__ZN9Triniti2D9UIManager13CreateControlIN7DinoCap15UILabelMoveCtrlEEEPT_ii_ptr - 0xFED9E)
0xFED96   6d49       LDR     R1, =0x257
0xFED98   0422       MOVS    R2, #4
0xFED9A   7b44       ADD     R3, PC; __ZN9Triniti2D9UIManager13CreateControlIN7DinoCap15UILabelMoveCtrlEEEPT_ii_ptr
0xFED9C   1b68       LDR     R3, [R3]
0xFED9E   9847       BLX     R3   <==
0xFEDA0   099c       LDR     R4, [SP,#0xD4+var_B0]
0xFEDA2   fc23       MOVS    R3, #0xFC
0xFEDA4   6a49       LDR     R1, =(aAdventure22 - 0xFEDAE); "Adventure_22"
0xFEDA6   6a46       MOV     R2, SP
0xFEDA8   d332       ADDS    R2, #0xD3
0xFEDAA   7944       ADD     R1, PC; "Adventure_22"
```

