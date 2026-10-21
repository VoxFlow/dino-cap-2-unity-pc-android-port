# mainScene::LevelBGLeave

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0A38C2` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xA38C2` |
| RVA (EA - 0x1000) | `0xA28C2` |
| File offset | `0xA28C2` |
| Size | 328 code bytes in 1 chunk(s), 152 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9mainScene12LevelBGLeaveEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::mainScene::LevelBGLeave(void)` |
| Bytes SHA-256 | `07c67eb841cdafd598c2f78527a4784223c5d78eedf0aeb7dfde75e6e8b82dc2` |
| Direct code callers | 2 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | GAMEPLAY / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: r118 0xa38c2: band collapse; PORTED GameIntro.LevelBGLeave

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameIntro.cs:74`](../../../../../Assets/Scripts/GameIntro.cs#L74) cites `0xA38C2`

## Callers and callees (direct code references)

Callers: [`mainScene::OnUIEvent`](../../../functions/dossiers/mainScene/FUN-0A69AE.md), [`lifeModeScene::OnUIEvent`](../../../functions/dossiers/lifeModeScene/FUN-0E6550.md)

Callees: [`RectScale`](../../../functions/dossiers/DinoCap/FUN-017068.md), [`UIMoveCtrl::StartMove`](../../../functions/dossiers/UIMoveCtrl/FUN-0CD466.md), `Triniti2D::Scene::GetUIManager`, `Triniti2D::UIManager::GetControl`, `___addsf3vfp`, `___dynamic_cast`, `___mulsf3vfp`

## Named data it references

- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x23D240` level_bgRect
- `0x22A634` __ZN7DinoCap10g_w_offsetE_ptr
- `0x23B914` DinoCap::g_w_offset
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
0xA38C2   f0b5       PUSH    {R4-R7,LR}
0xA38C4   5e46       MOV     R6, R11
0xA38C6   5546       MOV     R5, R10
0xA38C8   4446       MOV     R4, R8
0xA38CA   70b4       PUSH    {R4-R6}
0xA38CC   06af       ADD     R7, SP, #0x18
0xA38CE   98b0       SUB     SP, SP, #0x60
0xA38D0   4e4b       LDR     R3, =(__ZN7DinoCap8g_iphoneE_ptr - 0xA38D8)
0xA38D2   041c       MOVS    R4, R0
0xA38D4   7b44       ADD     R3, PC; __ZN7DinoCap8g_iphoneE_ptr
0xA38D6   1b68       LDR     R3, [R3]; DinoCap::g_iphone ...
0xA38D8   1b78       LDRB    R3, [R3]; DinoCap::g_iphone
0xA38DA   002b       CMP     R3, #0
0xA38DC   02d1       BNE     loc_A38E4
0xA38DE   4c48       LDR     R0, =0x40000000
0xA38E0   8446       MOV     R12, R0
0xA38E2   01e0       B       loc_A38E8
0xA38E4   4b49       LDR     R1, =0x3F800000
0xA38E6   8c46       MOV     R12, R1
0xA38E8   4b4b       LDR     R3, =(__ZL12level_bgRect - 0xA38F0); level_bgRect ...
0xA38EA   11aa       ADD     R2, SP, #0x60+var_1C
0xA38EC   7b44       ADD     R3, PC; level_bgRect
0xA38EE   1d1c       MOVS    R5, R3
0xA38F0   43cd       LDM     R5!, {R0,R1,R6}; level_bgRect
```

