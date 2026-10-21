# pauseCtrl::Leave

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0CE93C` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xCE93C` |
| RVA (EA - 0x1000) | `0xCD93C` |
| File offset | `0xCD93C` |
| Size | 292 code bytes in 1 chunk(s), 137 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9pauseCtrl5LeaveEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::pauseCtrl::Leave(void)` |
| Bytes SHA-256 | `14fab2f8cf1fdd1a8466abaa9b91c5589f0afde8b04dda219e9f550d00bd867d` |
| Direct code callers | 2 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / VERIFIED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameControls.cs:499`](../../../../../Assets/Scripts/GameControls.cs#L499) cites `0xCE93C`

## Callers and callees (direct code references)

Callers: [`mainScene::OnUIEvent`](../../../functions/dossiers/mainScene/FUN-0A69AE.md), [`lifeModeScene::OnUIEvent`](../../../functions/dossiers/lifeModeScene/FUN-0E6550.md)

Callees: [`UIMoveCtrl::StartMove`](../../../functions/dossiers/UIMoveCtrl/FUN-0CD466.md), `___addsf3vfp`, `___mulsf3vfp`

## Named data it references

- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22AA40` __ZNKSt6vectorIPN7DinoCap10UIMoveCtrlESaIS2_EE4sizeEv_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xCE93C   f0b5       PUSH    {R4-R7,LR}
0xCE93E   5e46       MOV     R6, R11
0xCE940   5546       MOV     R5, R10
0xCE942   4446       MOV     R4, R8
0xCE944   70b4       PUSH    {R4-R6}
0xCE946   06af       ADD     R7, SP, #0x18
0xCE948   9bb0       SUB     SP, SP, #0x6C
0xCE94A   0123       MOVS    R3, #1
0xCE94C   5b42       NEGS    R3, R3
0xCE94E   1693       STR     R3, [SP,#0x6C+var_14]
0xCE950   1193       STR     R3, [SP,#0x6C+var_28]
0xCE952   0c93       STR     R3, [SP,#0x6C+var_3C]
0xCE954   424b       LDR     R3, =(__ZN7DinoCap8g_iphoneE_ptr - 0xCE95C)
0xCE956   0490       STR     R0, [SP,#0x6C+var_5C]
0xCE958   7b44       ADD     R3, PC; __ZN7DinoCap8g_iphoneE_ptr
0xCE95A   1b68       LDR     R3, [R3]; DinoCap::g_iphone ...
0xCE95C   1b78       LDRB    R3, [R3]; DinoCap::g_iphone
0xCE95E   002b       CMP     R3, #0
0xCE960   01d1       BNE     loc_CE966
0xCE962   404c       LDR     R4, =0x40000000
0xCE964   00e0       B       loc_CE968
0xCE966   404c       LDR     R4, =0x3F800000
0xCE968   404b       LDR     R3, =0
0xCE96A   4149       LDR     R1, =0x42400000
```

