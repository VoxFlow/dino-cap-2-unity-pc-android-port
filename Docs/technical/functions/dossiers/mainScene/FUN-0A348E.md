# mainScene::LevelEnter

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0A348E` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xA348E` |
| RVA (EA - 0x1000) | `0xA248E` |
| File offset | `0xA248E` |
| Size | 968 code bytes in 1 chunk(s), 438 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9mainScene10LevelEnterEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::mainScene::LevelEnter(void)` |
| Bytes SHA-256 | `7e9b633d117bfe218c55820b267c6217c17c77936b1e248fa16c638c6a60ef01` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | GAMEPLAY / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: r118 0xa348e (and lifeModeScene 0xf13b4, identical): dayFrame + Adventure_33 label; PORTED GameIntro.LevelEnter

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameIntro.cs:86`](../../../../../Assets/Scripts/GameIntro.cs#L86) cites `0xA348E`
- [`Assets/Scripts/GameIntro.cs:93`](../../../../../Assets/Scripts/GameIntro.cs#L93) cites `0xA348E`

## Callers and callees (direct code references)

Callers: [`mainScene::OnUIEvent`](../../../functions/dossiers/mainScene/FUN-0A69AE.md)

Callees: [`RectScale`](../../../functions/dossiers/DinoCap/FUN-017068.md), [`RectOffset`](../../../functions/dossiers/DinoCap/FUN-0170B6.md), [`UIMoveCtrl::StartMove`](../../../functions/dossiers/UIMoveCtrl/FUN-0CD466.md), [`UILabelMoveCtrl::StartMove`](../../../functions/dossiers/UILabelMoveCtrl/FUN-0CD4B2.md), `Triniti2D::Scene::GetUIManager`, `Triniti2D::UIImage::SetTexture`, `Triniti2D::UILabel::SetFont`, `Triniti2D::UILabel::SetColor`, `Triniti2D::UILabel::SetText`, `Triniti2D::UIManager::DeleteControl`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___mulsf3vfp`, `___subsf3vfp`, `_sprintf`

## Strings it references

- `0x206FB4` "dayFrame"
- `0x2065FC` "Adventure_33"
- `0x20111C` "%d"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x2269E2` GCC_except_table34_0
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x23D250` level_dayRect
- `0x22A6C8` __ZN9Triniti2D9UIManager13CreateControlIN7DinoCap10UIMoveCtrlEEEPT_ii_ptr
- `0x23D260` level_textRect
- `0x22A654` __ZN9Triniti2D9UIManager13CreateControlIN7DinoCap15UILabelMoveCtrlEEEPT_ii_ptr
- `0x22A6E4` __ZN7DinoCap9g_missionE_ptr
- `0x23B8CC` DinoCap::g_mission

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xA348E   f0b5       PUSH    {R4-R7,LR}
0xA3490   5e46       MOV     R6, R11
0xA3492   5546       MOV     R5, R10
0xA3494   4446       MOV     R4, R8
0xA3496   70b4       PUSH    {R4-R6}
0xA3498   44f122ed   BLX     ___save_vfp_d8_d15_regs
0xA349C   16af       ADD     R7, SP, #arg_38
0xA349E   d2b0       SUB     SP, SP, #0x148
0xA34A0   ed4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xA34AA)
0xA34A2   0122       MOVS    R2, #1
0xA34A4   0a90       STR     R0, [SP,#0x148+var_120]
0xA34A6   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xA34A8   6846       MOV     R0, SP
0xA34AA   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xA34AC   1c90       STR     R0, [SP,#0x148+var_D8]
0xA34AE   12a8       ADD     R0, SP, #0x148+fctx; lpfctx
0xA34B0   1893       STR     R3, [SP,#0x148+fctx.personality]
0xA34B2   ea4b       LDR     R3, =(GCC_except_table34_0 - 0xA34BA)
0xA34B4   1a97       STR     R7, [SP,#0x148+fctx.jbuf]
0xA34B6   7b44       ADD     R3, PC; GCC_except_table34_0
0xA34B8   1993       STR     R3, [SP,#0x148+fctx.lsda]
0xA34BA   e94b       LDR     R3, =(loc_A3780 - 0xA34C0)
0xA34BC   7b44       ADD     R3, PC; loc_A3780
0xA34BE   1343       ORRS    R3, R2
```

