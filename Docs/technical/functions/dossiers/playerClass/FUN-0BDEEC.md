# playerClass::EnterHouse

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0BDEEC` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xBDEEC` |
| RVA (EA - 0x1000) | `0xBCEEC` |
| File offset | `0xBCEEC` |
| Size | 178 code bytes in 1 chunk(s), 76 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11playerClass10EnterHouseEb` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerClass::EnterHouse(bool)` |
| Bytes SHA-256 | `18539b9fedbc31f007218bfb12475e4603dc0e9dac0ec9cdb82003c8d82a9162` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: [`houseClass::OnKeyFrame`](../../../functions/dossiers/houseClass/FUN-006E1C.md)

Callees: [`CAObjCallBack::playAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00A064.md), [`CAObjCallBack::setAnimationAdv`](../../../functions/dossiers/CAObjCallBack/FUN-00A298.md), [`CAObjCallBack::setAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00AEA0.md), [`playerClass::UpdateBG`](../../../functions/dossiers/playerClass/FUN-0B8944.md), [`playerClass::OnFireUp`](../../../functions/dossiers/playerClass/FUN-0B8BC6.md), `Triniti2D::GameObject::Physics`, `Triniti2D::GamePhysics::SetLinearVelocity`, `std::string::_M_mutate`

## Strings it references

- `0x20844C` "houseAnim"

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xBDEEC   f0b5       PUSH    {R4-R7,LR}
0xBDEEE   03af       ADD     R7, SP, #0xC
0xBDEF0   83b0       SUB     SP, SP, #0xC
0xBDEF2   cdb2       UXTB    R5, R1
0xBDEF4   041c       MOVS    R4, R0
0xBDEF6   002d       CMP     R5, #0
0xBDEF8   41d0       BEQ     loc_BDF7E
0xBDEFA   a0239b00   MOVS    R3, #0x280
0xBDEFE   0622       MOVS    R2, #6
0xBDF00   c250       STR     R2, [R0,R3]
0xBDF02   274b       LDR     R3, =0x291
0xBDF04   a425ad00   MOVS    R5, #0x290
0xBDF08   0026       MOVS    R6, #0
0xBDF0A   c654       STRB    R6, [R0,R3]
0xBDF0C   435d       LDRB    R3, [R0,R5]
0xBDF0E   002b       CMP     R3, #0
0xBDF10   0bd0       BEQ     loc_BDF2A
0xBDF12   faf758fe   BL      __ZN7DinoCap11playerClass8OnFireUpEv; DinoCap::playerClass::OnFireUp(void)
0xBDF16   e26e       LDR     R2, [R4,#0x6C]
0xBDF18   201c       MOVS    R0, R4
0xBDF1A   6655       STRB    R6, [R4,R5]
0xBDF1C   0c3a       SUBS    R2, #0xC
0xBDF1E   6c30       ADDS    R0, #0x6C ; 'l'; this
0xBDF20   1268       LDR     R2, [R2]; unsigned int
```

