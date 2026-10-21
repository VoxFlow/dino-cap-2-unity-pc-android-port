# selectScene::startMoveUIRight

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0FCFDC` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xFCFDC` |
| RVA (EA - 0x1000) | `0xFBFDC` |
| File offset | `0xFBFDC` |
| Size | 194 code bytes in 1 chunk(s), 88 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene16startMoveUIRightEPNS_10UIMoveCtrlEN9Triniti2D4RectE` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::startMoveUIRight(DinoCap::UIMoveCtrl *,Triniti2D::Rect)` |
| Bytes SHA-256 | `9480d40171637c0385354ab057c6ec7bb8f728b9d49ec3cf7e87ff5ea1bebac1` |
| Direct code callers | 33 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/SelectScene.cs:61`](../../../../../Assets/Scripts/SelectScene.cs#L61) cites `0xFCFDC`

## Callers and callees (direct code references)

Callers: [`selectScene::showMissionInfo`](../../../functions/dossiers/selectScene/FUN-0FD0C0.md), [`selectScene::showJigsawUI`](../../../functions/dossiers/selectScene/FUN-0FDF70.md), [`selectScene::updateWeaponIcon`](../../../functions/dossiers/selectScene/FUN-0FEF68.md), [`selectScene::lockedWeapon`](../../../functions/dossiers/selectScene/FUN-10071E.md), [`selectScene::OnSelectChange`](../../../functions/dossiers/selectScene/FUN-100B1E.md), `sub_101624`, [`selectScene::showIAP`](../../../functions/dossiers/selectScene/FUN-101AD2.md), [`selectScene::showMapUI`](../../../functions/dossiers/selectScene/FUN-10218C.md), [`selectScene::showUpgrade`](../../../functions/dossiers/selectScene/FUN-10363E.md), `sub_10479C`, [`selectScene::showTavernUI`](../../../functions/dossiers/selectScene/FUN-1056DC.md), [`selectScene::resetMapUI`](../../../functions/dossiers/selectScene/FUN-10B036.md)

Callees: [`UIMoveCtrl::StartMove`](../../../functions/dossiers/UIMoveCtrl/FUN-0CD466.md), `___addsf3vfp`, `___mulsf3vfp`

## Named data it references

- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xFCFDC   82b0       SUB     SP, SP, #8
0xFCFDE   f0b5       PUSH    {R4-R7,LR}
0xFCFE0   5e46       MOV     R6, R11
0xFCFE2   5546       MOV     R5, R10
0xFCFE4   4446       MOV     R4, R8
0xFCFE6   70b4       PUSH    {R4-R6}
0xFCFE8   06af       ADD     R7, SP, #0x18
0xFCFEA   92b0       SUB     SP, SP, #0x48
0xFCFEC   1b93       STR     R3, [SP,#0x48+varg_r3]
0xFCFEE   1d9b       LDR     R3, [SP,#0x48+arg_4]
0xFCFF0   1a92       STR     R2, [SP,#0x48+varg_r2]
0xFCFF2   0291       STR     R1, [SP,#0x48+var_40]
0xFCFF4   9846       MOV     R8, R3
0xFCFF6   1b9b       LDR     R3, [SP,#0x48+varg_r3]
0xFCFF8   1c9e       LDR     R6, [SP,#0x48+arg_0]
0xFCFFA   1a9c       LDR     R4, [SP,#0x48+varg_r2]
0xFCFFC   9b46       MOV     R11, R3
0xFCFFE   284b       LDR     R3, =(__ZN7DinoCap8g_iphoneE_ptr - 0xFD004)
0xFD000   7b44       ADD     R3, PC; __ZN7DinoCap8g_iphoneE_ptr
0xFD002   1b68       LDR     R3, [R3]; DinoCap::g_iphone ...
0xFD004   1b78       LDRB    R3, [R3]; DinoCap::g_iphone
0xFD006   002b       CMP     R3, #0
0xFD008   01d1       BNE     loc_FD00E
0xFD00A   264b       LDR     R3, =0x40000000
```

