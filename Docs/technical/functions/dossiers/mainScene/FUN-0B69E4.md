# mainScene::LevelLeave

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0B69E4` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xB69E4` |
| RVA (EA - 0x1000) | `0xB59E4` |
| File offset | `0xB59E4` |
| Size | 614 code bytes in 1 chunk(s), 277 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9mainScene10LevelLeaveEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::mainScene::LevelLeave(void)` |
| Bytes SHA-256 | `7f6825fd6f5fdd1594dd3d3189df7a1e0579455c65a916f42dbbd92f21bb9c1a` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | GAMEPLAY / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: r118 0xb69e4: ids 13/14 slide out (mid cx+50S, end cx-480S, 0.05/0.2 s), state 3; PORTED GameIntro.LevelLeave (lifeModeScene twin 0xf17e6 LIKELY identical)

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameIntro.cs:107`](../../../../../Assets/Scripts/GameIntro.cs#L107) cites `0xB69E4`

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: [`RectScale`](../../../functions/dossiers/DinoCap/FUN-017068.md), [`RectOffset`](../../../functions/dossiers/DinoCap/FUN-0170B6.md), [`UIMoveCtrl::StartMove`](../../../functions/dossiers/UIMoveCtrl/FUN-0CD466.md), [`UILabelMoveCtrl::StartMove`](../../../functions/dossiers/UILabelMoveCtrl/FUN-0CD4B2.md), `Triniti2D::Scene::GetUIManager`, `Triniti2D::UIManager::GetControl`, `___addsf3vfp`, `___dynamic_cast`, `___mulsf3vfp`, `___subsf3vfp`

## Named data it references

- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A5F4` __ZTIN9Triniti2D9UIControlE_ptr
- `0x22E468` `typeinfo for'Triniti2D::UIControl
- `0x22A650` __ZTIN7DinoCap10UIMoveCtrlE_ptr
- `0x22CDA0` `typeinfo for'DinoCap::UIMoveCtrl
- `0x2415E8` `vtable for'__cxxabiv1::__class_type_info
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x23D250` level_dayRect
- `0x22A750` __ZTIN7DinoCap15UILabelMoveCtrlE_ptr
- `0x22CD94` `typeinfo for'DinoCap::UILabelMoveCtrl
- `0x23D260` level_textRect

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xB69E4   f0b5       PUSH    {R4-R7,LR}
0xB69E6   5e46       MOV     R6, R11
0xB69E8   5546       MOV     R5, R10
0xB69EA   4446       MOV     R4, R8
0xB69EC   70b4       PUSH    {R4-R6}
0xB69EE   06af       ADD     R7, SP, #0x18
0xB69F0   a8b0       SUB     SP, SP, #0xA0
0xB69F2   964b       LDR     R3, =0
0xB69F4   0122       MOVS    R2, #1
0xB69F6   5242       NEGS    R2, R2
0xB69F8   1f93       STR     R3, [SP,#0xA0+var_24]
0xB69FA   2093       STR     R3, [SP,#0xA0+var_20]
0xB69FC   2193       STR     R3, [SP,#0xA0+var_1C]
0xB69FE   2293       STR     R3, [SP,#0xA0+var_18]
0xB6A00   1a93       STR     R3, [SP,#0xA0+var_38]
0xB6A02   1b93       STR     R3, [SP,#0xA0+var_34]
0xB6A04   1c93       STR     R3, [SP,#0xA0+var_30]
0xB6A06   1d93       STR     R3, [SP,#0xA0+var_2C]
0xB6A08   1593       STR     R3, [SP,#0xA0+var_4C]
0xB6A0A   1693       STR     R3, [SP,#0xA0+var_48]
0xB6A0C   1793       STR     R3, [SP,#0xA0+var_44]
0xB6A0E   1893       STR     R3, [SP,#0xA0+var_40]
0xB6A10   8f4b       LDR     R3, =(__ZN7DinoCap8g_iphoneE_ptr - 0xB6A1A)
0xB6A12   0290       STR     R0, [SP,#0xA0+var_98]
```

