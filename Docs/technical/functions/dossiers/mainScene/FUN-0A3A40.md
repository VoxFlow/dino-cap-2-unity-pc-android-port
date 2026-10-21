# mainScene::LevelBGEnter

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0A3A40` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xA3A40` |
| RVA (EA - 0x1000) | `0xA2A40` |
| File offset | `0xA2A40` |
| Size | 462 code bytes in 1 chunk(s), 210 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9mainScene12LevelBGEnterEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::mainScene::LevelBGEnter(void)` |
| Bytes SHA-256 | `dd32adcadd22ed5f65fd08765e87e6824351c130ebdae87915871bee2e27961e` |
| Direct code callers | 2 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | GAMEPLAY / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: r118 0xa3a40: id 12 black band; PORTED GameIntro.LevelBGEnter

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameIntro.cs:7`](../../../../../Assets/Scripts/GameIntro.cs#L7) cites `0xA3A40`
- [`Assets/Scripts/GameIntro.cs:58`](../../../../../Assets/Scripts/GameIntro.cs#L58) cites `0xA3A40`
- [`Assets/Scripts/GameIntro.cs:62`](../../../../../Assets/Scripts/GameIntro.cs#L62) cites `0xA3A40`

## Callers and callees (direct code references)

Callers: `sub_B5DF0`, `sub_EFBD6`

Callees: [`RectScale`](../../../functions/dossiers/DinoCap/FUN-017068.md), [`UIMoveCtrl::StartMove`](../../../functions/dossiers/UIMoveCtrl/FUN-0CD466.md), `Triniti2D::Scene::GetUIManager`, `Triniti2D::UIImage::SetColor`, `Triniti2D::UIImage::SetTexture`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___mulsf3vfp`

## Strings it references

- `0x2064B4` "whiteFrame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x2269F2` GCC_except_table35_0
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x23D240` level_bgRect
- `0x22A634` __ZN7DinoCap10g_w_offsetE_ptr
- `0x23B914` DinoCap::g_w_offset
- `0x22A6C8` __ZN9Triniti2D9UIManager13CreateControlIN7DinoCap10UIMoveCtrlEEEPT_ii_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xA3A40   f0b5       PUSH    {R4-R7,LR}
0xA3A42   5e46       MOV     R6, R11
0xA3A44   5546       MOV     R5, R10
0xA3A46   4446       MOV     R4, R8
0xA3A48   70b4       PUSH    {R4-R6}
0xA3A4A   44f14aea   BLX     ___save_vfp_d8_d15_regs
0xA3A4E   16af       ADD     R7, SP, #arg_38
0xA3A50   b0b0       SUB     SP, SP, #0xC0
0xA3A52   6f4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xA3A5C)
0xA3A54   0590       STR     R0, [SP,#0xC0+var_AC]
0xA3A56   0122       MOVS    R2, #1
0xA3A58   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xA3A5A   6846       MOV     R0, SP
0xA3A5C   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xA3A5E   1490       STR     R0, [SP,#0xC0+var_70]
0xA3A60   0aa8       ADD     R0, SP, #0xC0+fctx; lpfctx
0xA3A62   1093       STR     R3, [SP,#0xC0+fctx.personality]
0xA3A64   6b4b       LDR     R3, =(GCC_except_table35_0 - 0xA3A6C)
0xA3A66   1297       STR     R7, [SP,#0xC0+fctx.jbuf]
0xA3A68   7b44       ADD     R3, PC; GCC_except_table35_0
0xA3A6A   1193       STR     R3, [SP,#0xC0+fctx.lsda]
0xA3A6C   6a4b       LDR     R3, =(loc_A3B30 - 0xA3A72)
0xA3A6E   7b44       ADD     R3, PC; loc_A3B30
0xA3A70   1343       ORRS    R3, R2
```

