# lifeModeScene::LevelEnter

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0F13B4` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xF13B4` |
| RVA (EA - 0x1000) | `0xF03B4` |
| File offset | `0xF03B4` |
| Size | 968 code bytes in 1 chunk(s), 438 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap13lifeModeScene10LevelEnterEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::lifeModeScene::LevelEnter(void)` |
| Bytes SHA-256 | `d304cace82224d96e5a075c0dfd2ba19038860ab8f188209af58e5e952573f7c` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | GAMEPLAY / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: r118 0xf13b4: PORTED (GameIntro.LevelEnter)

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameIntro.cs:11`](../../../../../Assets/Scripts/GameIntro.cs#L11) cites `0xF13B4`
- [`Assets/Scripts/GameIntro.cs:93`](../../../../../Assets/Scripts/GameIntro.cs#L93) cites `0xF13B4`

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: [`RectScale`](../../../functions/dossiers/DinoCap/FUN-017068.md), [`RectOffset`](../../../functions/dossiers/DinoCap/FUN-0170B6.md), [`UIMoveCtrl::StartMove`](../../../functions/dossiers/UIMoveCtrl/FUN-0CD466.md), [`UILabelMoveCtrl::StartMove`](../../../functions/dossiers/UILabelMoveCtrl/FUN-0CD4B2.md), `Triniti2D::Scene::GetUIManager`, `Triniti2D::UIImage::SetTexture`, `Triniti2D::UILabel::SetFont`, `Triniti2D::UILabel::SetColor`, `Triniti2D::UILabel::SetText`, `Triniti2D::UIManager::DeleteControl`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___mulsf3vfp`, `___subsf3vfp`, `_sprintf`

## Strings it references

- `0x206FB4` "dayFrame"
- `0x2065FC` "Adventure_33"
- `0x20111C` "%d"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x227E69` GCC_except_table40_1
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x23D470` level_dayRect
- `0x22A6C8` __ZN9Triniti2D9UIManager13CreateControlIN7DinoCap10UIMoveCtrlEEEPT_ii_ptr
- `0x23D480` level_textRect
- `0x22A654` __ZN9Triniti2D9UIManager13CreateControlIN7DinoCap15UILabelMoveCtrlEEEPT_ii_ptr
- `0x22A6E4` __ZN7DinoCap9g_missionE_ptr
- `0x23B8CC` DinoCap::g_mission

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xF13B4   f0b5       PUSH    {R4-R7,LR}
0xF13B6   5e46       MOV     R6, R11
0xF13B8   5546       MOV     R5, R10
0xF13BA   4446       MOV     R4, R8
0xF13BC   70b4       PUSH    {R4-R6}
0xF13BE   f6f090ed   BLX     ___save_vfp_d8_d15_regs
0xF13C2   16af       ADD     R7, SP, #arg_38
0xF13C4   d2b0       SUB     SP, SP, #0x148
0xF13C6   ed4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xF13D0)
0xF13C8   0122       MOVS    R2, #1
0xF13CA   0a90       STR     R0, [SP,#0x148+var_120]
0xF13CC   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xF13CE   6846       MOV     R0, SP
0xF13D0   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xF13D2   1c90       STR     R0, [SP,#0x148+var_D8]
0xF13D4   12a8       ADD     R0, SP, #0x148+fctx; lpfctx
0xF13D6   1893       STR     R3, [SP,#0x148+fctx.personality]
0xF13D8   e94b       LDR     R3, =(GCC_except_table40_1 - 0xF13E0)
0xF13DA   1a97       STR     R7, [SP,#0x148+fctx.jbuf]
0xF13DC   7b44       ADD     R3, PC; GCC_except_table40_1
0xF13DE   1993       STR     R3, [SP,#0x148+fctx.lsda]
0xF13E0   e84b       LDR     R3, =(loc_F16A6 - 0xF13E6)
0xF13E2   7b44       ADD     R3, PC; loc_F16A6
0xF13E4   1343       ORRS    R3, R2
```

