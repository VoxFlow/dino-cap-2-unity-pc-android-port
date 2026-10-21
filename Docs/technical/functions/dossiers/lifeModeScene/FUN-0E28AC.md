# lifeModeScene::MissCompMove

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0E28AC` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xE28AC` |
| RVA (EA - 0x1000) | `0xE18AC` |
| File offset | `0xE18AC` |
| Size | 366 code bytes in 1 chunk(s), 163 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap13lifeModeScene12MissCompMoveEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::lifeModeScene::MissCompMove(void)` |
| Bytes SHA-256 | `676459dfe2754479b4859940b87b4c56751f39022c82108fd2514229342e23e0` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameMission.cs:10`](../../../../../Assets/Scripts/GameMission.cs#L10) cites `0xE28AC`
- [`Assets/Scripts/GameMission.cs:359`](../../../../../Assets/Scripts/GameMission.cs#L359) cites `0xE28AC`
- [`Assets/Scripts/GameMission.cs:366`](../../../../../Assets/Scripts/GameMission.cs#L366) cites `0xE28AC`

## Callers and callees (direct code references)

Callers: [`lifeModeScene::OnTimeout`](../../../functions/dossiers/lifeModeScene/FUN-0E2A4C.md)

Callees: [`UIMoveCtrl::StartMove`](../../../functions/dossiers/UIMoveCtrl/FUN-0CD466.md), `Triniti2D::Scene::GetUIManager`, `Triniti2D::UIManager::GetControl`, `___addsf3vfp`, `___dynamic_cast`, `___mulsf3vfp`

## Named data it references

- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A5F4` __ZTIN9Triniti2D9UIControlE_ptr
- `0x22E468` `typeinfo for'Triniti2D::UIControl
- `0x22A650` __ZTIN7DinoCap10UIMoveCtrlE_ptr
- `0x22CDA0` `typeinfo for'DinoCap::UIMoveCtrl
- `0x2415E8` `vtable for'__cxxabiv1::__class_type_info
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xE28AC   f0b5       PUSH    {R4-R7,LR}
0xE28AE   5e46       MOV     R6, R11
0xE28B0   5546       MOV     R5, R10
0xE28B2   4446       MOV     R4, R8
0xE28B4   70b4       PUSH    {R4-R6}
0xE28B6   06af       ADD     R7, SP, #0x18
0xE28B8   92b0       SUB     SP, SP, #0x48
0xE28BA   584b       LDR     R3, =0
0xE28BC   0122       MOVS    R2, #1
0xE28BE   5242       NEGS    R2, R2
0xE28C0   0993       STR     R3, [SP,#0x48+var_24]
0xE28C2   0a93       STR     R3, [SP,#0x48+var_20]
0xE28C4   0b93       STR     R3, [SP,#0x48+var_1C]
0xE28C6   0c93       STR     R3, [SP,#0x48+var_18]
0xE28C8   0493       STR     R3, [SP,#0x48+var_38]
0xE28CA   0593       STR     R3, [SP,#0x48+var_34]
0xE28CC   0693       STR     R3, [SP,#0x48+var_30]
0xE28CE   0793       STR     R3, [SP,#0x48+var_2C]
0xE28D0   534b       LDR     R3, =(__ZN7DinoCap8g_iphoneE_ptr - 0xE28DA)
0xE28D2   8346       MOV     R11, R0
0xE28D4   0d92       STR     R2, [SP,#0x48+var_14]
0xE28D6   7b44       ADD     R3, PC; __ZN7DinoCap8g_iphoneE_ptr
0xE28D8   0892       STR     R2, [SP,#0x48+var_28]
0xE28DA   1b68       LDR     R3, [R3]; DinoCap::g_iphone ...
```

