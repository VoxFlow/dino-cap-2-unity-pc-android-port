# lifeModeScene::LevelLeave

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0F17E6` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xF17E6` |
| RVA (EA - 0x1000) | `0xF07E6` |
| File offset | `0xF07E6` |
| Size | 614 code bytes in 1 chunk(s), 277 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap13lifeModeScene10LevelLeaveEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::lifeModeScene::LevelLeave(void)` |
| Bytes SHA-256 | `2ebb54cf0bf0b2b91023ad174ced563288c0003c881659ede933edc336aca6f7` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | GAMEPLAY / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: r118 0xf17e6: twin of mainScene::LevelLeave, PORTED (LIKELY identical, not read in full)

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameIntro.cs:11`](../../../../../Assets/Scripts/GameIntro.cs#L11) cites `0xF17E6`
- [`Assets/Scripts/GameIntro.cs:107`](../../../../../Assets/Scripts/GameIntro.cs#L107) cites `0xF17E6`

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
- `0x23D470` level_dayRect
- `0x22A750` __ZTIN7DinoCap15UILabelMoveCtrlE_ptr
- `0x22CD94` `typeinfo for'DinoCap::UILabelMoveCtrl
- `0x23D480` level_textRect

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xF17E6   f0b5       PUSH    {R4-R7,LR}
0xF17E8   5e46       MOV     R6, R11
0xF17EA   5546       MOV     R5, R10
0xF17EC   4446       MOV     R4, R8
0xF17EE   70b4       PUSH    {R4-R6}
0xF17F0   06af       ADD     R7, SP, #0x18
0xF17F2   a8b0       SUB     SP, SP, #0xA0
0xF17F4   954b       LDR     R3, =0
0xF17F6   0122       MOVS    R2, #1
0xF17F8   5242       NEGS    R2, R2
0xF17FA   1f93       STR     R3, [SP,#0xA0+var_24]
0xF17FC   2093       STR     R3, [SP,#0xA0+var_20]
0xF17FE   2193       STR     R3, [SP,#0xA0+var_1C]
0xF1800   2293       STR     R3, [SP,#0xA0+var_18]
0xF1802   1a93       STR     R3, [SP,#0xA0+var_38]
0xF1804   1b93       STR     R3, [SP,#0xA0+var_34]
0xF1806   1c93       STR     R3, [SP,#0xA0+var_30]
0xF1808   1d93       STR     R3, [SP,#0xA0+var_2C]
0xF180A   1593       STR     R3, [SP,#0xA0+var_4C]
0xF180C   1693       STR     R3, [SP,#0xA0+var_48]
0xF180E   1793       STR     R3, [SP,#0xA0+var_44]
0xF1810   1893       STR     R3, [SP,#0xA0+var_40]
0xF1812   8f4b       LDR     R3, =(__ZN7DinoCap8g_iphoneE_ptr - 0xF181C)
0xF1814   0290       STR     R0, [SP,#0xA0+var_98]
```

