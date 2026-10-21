# selectScene::startScaleUI

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0F77DC` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xF77DC` |
| RVA (EA - 0x1000) | `0xF67DC` |
| File offset | `0xF67DC` |
| Size | 184 code bytes in 1 chunk(s), 83 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene12startScaleUIEPNS_10UIMoveCtrlEN9Triniti2D4RectE` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::startScaleUI(DinoCap::UIMoveCtrl *,Triniti2D::Rect)` |
| Bytes SHA-256 | `7143dfee33775dd10d3b5178af8cb8b8fb7509258383c6272e1a6e7cb5ad893f` |
| Direct code callers | 18 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/SelectScene.cs:61`](../../../../../Assets/Scripts/SelectScene.cs#L61) cites `0xF77DC`

## Callers and callees (direct code references)

Callers: [`selectScene::updateWeaponIconNoMove`](../../../functions/dossiers/selectScene/FUN-0F78A8.md), [`selectScene::ligsawEffCallBack`](../../../functions/dossiers/selectScene/FUN-0F90DE.md), `sub_101624`, [`selectScene::showIAP`](../../../functions/dossiers/selectScene/FUN-101AD2.md), [`selectScene::showMapUI`](../../../functions/dossiers/selectScene/FUN-10218C.md), [`selectScene::showBossPoint`](../../../functions/dossiers/selectScene/FUN-10A2E8.md), [`selectScene::showMapPoint`](../../../functions/dossiers/selectScene/FUN-10A69A.md), [`selectScene::resetMapUI`](../../../functions/dossiers/selectScene/FUN-10B036.md)

Callees: [`UIMoveCtrl::StartMove`](../../../functions/dossiers/UIMoveCtrl/FUN-0CD466.md), `___addsf3vfp`, `___mulsf3vfp`

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xF77DC   82b0       SUB     SP, SP, #8
0xF77DE   f0b5       PUSH    {R4-R7,LR}
0xF77E0   5e46       MOV     R6, R11
0xF77E2   5546       MOV     R5, R10
0xF77E4   4446       MOV     R4, R8
0xF77E6   70b4       PUSH    {R4-R6}
0xF77E8   06af       ADD     R7, SP, #0x18
0xF77EA   92b0       SUB     SP, SP, #0x48
0xF77EC   1b93       STR     R3, [SP,#0x48+varg_r3]
0xF77EE   1d9b       LDR     R3, [SP,#0x48+arg_4]
0xF77F0   1a92       STR     R2, [SP,#0x48+varg_r2]
0xF77F2   1c9e       LDR     R6, [SP,#0x48+arg_0]
0xF77F4   9a46       MOV     R10, R3
0xF77F6   1b9b       LDR     R3, [SP,#0x48+varg_r3]
0xF77F8   1a9c       LDR     R4, [SP,#0x48+varg_r2]
0xF77FA   0291       STR     R1, [SP,#0x48+var_40]
0xF77FC   9b46       MOV     R11, R3
0xF77FE   0123       MOVS    R3, #1
0xF7800   5b42       NEGS    R3, R3
0xF7802   1193       STR     R3, [SP,#0x48+var_4]
0xF7804   0c93       STR     R3, [SP,#0x48+var_18]
0xF7806   0793       STR     R3, [SP,#0x48+var_2C]
0xF7808   224b       LDR     R3, =0x3F000000
0xF780A   301c       MOVS    R0, R6
```

