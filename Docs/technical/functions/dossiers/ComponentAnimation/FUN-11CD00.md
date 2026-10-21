# ComponentAnimation::Clear

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-11CD00` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x11CD00` |
| RVA (EA - 0x1000) | `0x11BD00` |
| File offset | `0x11BD00` |
| Size | 80 code bytes in 1 chunk(s), 37 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap18ComponentAnimation5ClearEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::ComponentAnimation::Clear(void)` |
| Bytes SHA-256 | `426b73ecf55952b8d93214bfe5e9b6bff07b317bdc6287b6579523c4fc22f993` |
| Direct code callers | 2 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: `ComponentAnimation::~ComponentAnimation`, [`ComponentAnimation::Load`](../../../functions/dossiers/ComponentAnimation/FUN-11D3FE.md)

Callees: `CAComponent::~CAComponent`, `std::_Rb_tree_increment`, `operator delete`

## Named data it references

- `0x22B0A0` __ZNSt8_Rb_treeISsSt4pairIKSsPN7DinoCap11CAComponentEESt10_Select1stIS5_ESt4lessISsESaIS5_EE8_M_eraseEPSt13_Rb_tree_nodeIS5_E_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x11CD00  f0b5       PUSH    {R4-R7,LR}
0x11CD02  4646       MOV     R6, R8
0x11CD04  40b4       PUSH    {R6}
0x11CD06  04af       ADD     R7, SP, #0x10
0x11CD08  2023       MOVS    R3, #0x20 ; ' '
0x11CD0A  856a       LDR     R5, [R0,#0x28]
0x11CD0C  1b18       ADDS    R3, R3, R0
0x11CD0E  061c       MOVS    R6, R0
0x11CD10  9846       MOV     R8, R3
0x11CD12  0ce0       B       loc_11CD2E
0x11CD14  6c69       LDR     R4, [R5,#0x14]
0x11CD16  002c       CMP     R4, #0
0x11CD18  05d0       BEQ     loc_11CD26
0x11CD1A  201c       MOVS    R0, R4; this
0x11CD1C  fff75aff   BL      __ZN7DinoCap11CAComponentD2Ev; DinoCap::CAComponent::~CAComponent()
0x11CD20  201c       MOVS    R0, R4; void *
0x11CD22  ccf08eef   BLX     __ZdlPv; operator delete(void *)
0x11CD26  281c       MOVS    R0, R5; _Rb_tree_node_base *
0x11CD28  ccf06cef   BLX     __ZSt18_Rb_tree_incrementPSt18_Rb_tree_node_base; std::_Rb_tree_increment(std::_Rb_tree_node_base *)
0x11CD2C  051c       MOVS    R5, R0
0x11CD2E  a845       CMP     R8, R5
0x11CD30  f0d1       BNE     loc_11CD14
0x11CD32  074b       LDR     R3, =(__ZNSt8_Rb_treeISsSt4pairIKSsPN7DinoCap11CAComponentEESt10_Select1stIS5_ESt4lessISsESaIS5_EE8_M_eraseEPSt13_Rb_tree_nodeIS5_E_ptr - 0x11CD3C)
0x11CD34  301c       MOVS    R0, R6
```

