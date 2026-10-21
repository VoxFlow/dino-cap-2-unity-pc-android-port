# mainScene::MissCompMove

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0A3C52` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xA3C52` |
| RVA (EA - 0x1000) | `0xA2C52` |
| File offset | `0xA2C52` |
| Size | 364 code bytes in 1 chunk(s), 163 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9mainScene12MissCompMoveEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::mainScene::MissCompMove(void)` |
| Bytes SHA-256 | `d714e3fbe20bcaa110e2cd3384e94b4e66cb469c97f7056762bcd9a66f6f0cbf` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameMission.cs:366`](../../../../../Assets/Scripts/GameMission.cs#L366) cites `0xA3C52`

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

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
0xA3C52   f0b5       PUSH    {R4-R7,LR}
0xA3C54   5e46       MOV     R6, R11
0xA3C56   5546       MOV     R5, R10
0xA3C58   4446       MOV     R4, R8
0xA3C5A   70b4       PUSH    {R4-R6}
0xA3C5C   06af       ADD     R7, SP, #0x18
0xA3C5E   92b0       SUB     SP, SP, #0x48
0xA3C60   574b       LDR     R3, =0
0xA3C62   0122       MOVS    R2, #1
0xA3C64   5242       NEGS    R2, R2
0xA3C66   0993       STR     R3, [SP,#0x48+var_24]
0xA3C68   0a93       STR     R3, [SP,#0x48+var_20]
0xA3C6A   0b93       STR     R3, [SP,#0x48+var_1C]
0xA3C6C   0c93       STR     R3, [SP,#0x48+var_18]
0xA3C6E   0493       STR     R3, [SP,#0x48+var_38]
0xA3C70   0593       STR     R3, [SP,#0x48+var_34]
0xA3C72   0693       STR     R3, [SP,#0x48+var_30]
0xA3C74   0793       STR     R3, [SP,#0x48+var_2C]
0xA3C76   534b       LDR     R3, =(__ZN7DinoCap8g_iphoneE_ptr - 0xA3C80)
0xA3C78   8346       MOV     R11, R0
0xA3C7A   0d92       STR     R2, [SP,#0x48+var_14]
0xA3C7C   7b44       ADD     R3, PC; __ZN7DinoCap8g_iphoneE_ptr
0xA3C7E   0892       STR     R2, [SP,#0x48+var_28]
0xA3C80   1b68       LDR     R3, [R3]; DinoCap::g_iphone ...
```

