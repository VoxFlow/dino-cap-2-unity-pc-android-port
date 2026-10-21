# selectScene::MissionLeave

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0FA1BA` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xFA1BA` |
| RVA (EA - 0x1000) | `0xF91BA` |
| File offset | `0xF91BA` |
| Size | 142 code bytes in 1 chunk(s), 67 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene12MissionLeaveEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::MissionLeave(void)` |
| Bytes SHA-256 | `3c794a3e22fb6ca7797cd303280f4498e3fb96cdb36a68a792b4d3b370a4f617` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: [`selectScene::OnTimeout`](../../../functions/dossiers/selectScene/FUN-0FA3D2.md)

Callees: [`UIMoveCtrl::StartMove`](../../../functions/dossiers/UIMoveCtrl/FUN-0CD466.md), `Triniti2D::Scene::GetUIManager`, `Triniti2D::UIManager::GetControl`, `___dynamic_cast`

## Named data it references

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
0xFA1BA   90b5       PUSH    {R4,R7,LR}
0xFA1BC   01af       ADD     R7, SP, #4
0xFA1BE   91b0       SUB     SP, SP, #0x44
0xFA1C0   214b       LDR     R3, =0
0xFA1C2   0122       MOVS    R2, #1
0xFA1C4   5242       NEGS    R2, R2
0xFA1C6   0c93       STR     R3, [SP,#0x44+var_14]
0xFA1C8   0d93       STR     R3, [SP,#0x44+var_10]
0xFA1CA   0e93       STR     R3, [SP,#0x44+var_C]
0xFA1CC   0f93       STR     R3, [SP,#0x44+var_8]
0xFA1CE   1092       STR     R2, [SP,#0x44+var_4]
0xFA1D0   0793       STR     R3, [SP,#0x44+var_28]
0xFA1D2   0893       STR     R3, [SP,#0x44+var_24]
0xFA1D4   0993       STR     R3, [SP,#0x44+var_20]
0xFA1D6   0a93       STR     R3, [SP,#0x44+var_1C]
0xFA1D8   0b92       STR     R2, [SP,#0x44+var_18]
0xFA1DA   0293       STR     R3, [SP,#0x44+var_3C]
0xFA1DC   0393       STR     R3, [SP,#0x44+var_38]
0xFA1DE   0493       STR     R3, [SP,#0x44+var_34]
0xFA1E0   0593       STR     R3, [SP,#0x44+var_30]
0xFA1E2   0692       STR     R2, [SP,#0x44+var_2C]
0xFA1E4   4068       LDR     R0, [R0,#4]; this
0xFA1E6   7bf040f8   BL      __ZN9Triniti2D5Scene12GetUIManagerEv; Triniti2D::Scene::GetUIManager(void)
0xFA1EA   1d21       MOVS    R1, #0x1D; int
```

