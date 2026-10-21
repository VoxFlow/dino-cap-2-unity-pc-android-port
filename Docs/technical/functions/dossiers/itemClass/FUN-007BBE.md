# itemClass::OnUpdate

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-007BBE` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x7BBE` |
| RVA (EA - 0x1000) | `0x6BBE` |
| File offset | `0x6BBE` |
| Size | 492 code bytes in 1 chunk(s), 216 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9itemClass8OnUpdateEf` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::itemClass::OnUpdate(float)` |
| Bytes SHA-256 | `aafcff2bab21b66cd71cb922e6e2a5ba6566ddd0fac6a06b0c576545417ef9e9` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | GAMEPLAY / VERIFIED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: ammo pickup popup (r118): ported in GameItems.cs StartItemPopup/UpdateItemPopup; PilotItemsVerify 20/20; Dev/Evidence/item_popup_r118.md

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameItems.cs:254`](../../../../../Assets/Scripts/GameItems.cs#L254) cites `0x7BBE`

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: `Triniti2D::GameObject::Enable`, `Triniti2D::GameObject::GetPosition`, `Triniti2D::GameObject::SetSize`, `Triniti2D::GameObject::SetPosition`, `___addsf3vfp`, `___gtsf2vfp`, `___mulsf3vfp`

## Named data it references

- `0x22A0A0` __ZNKSt6vectorIPN9Triniti2D10GameObjectESaIS2_EE4sizeEv_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x7BBE    f0b5       PUSH    {R4-R7,LR}
0x7BC0    5e46       MOV     R6, R11
0x7BC2    5546       MOV     R5, R10
0x7BC4    4446       MOV     R4, R8
0x7BC6    70b4       PUSH    {R4-R6}
0x7BC8    06af       ADD     R7, SP, #0x18
0x7BCA    96b0       SUB     SP, SP, #0x58
0x7BCC    046a       LDR     R4, [R0,#0x20]
0x7BCE    8346       MOV     R11, R0
0x7BD0    081c       MOVS    R0, R1
0x7BD2    7649       LDR     R1, =0x40A00000
0x7BD4    e2f124e9   BLX     ___mulsf3vfp
0x7BD8    011c       MOVS    R1, R0
0x7BDA    201c       MOVS    R0, R4
0x7BDC    e2f148e8   BLX     ___addsf3vfp
0x7BE0    734c       LDR     R4, =0x3F800000
0x7BE2    5946       MOV     R1, R11
0x7BE4    0862       STR     R0, [R1,#0x20]
0x7BE6    211c       MOVS    R1, R4
0x7BE8    e2f1dee8   BLX     ___gtsf2vfp
0x7BEC    0028       CMP     R0, #0
0x7BEE    01d0       BEQ     loc_7BF4
0x7BF0    5a46       MOV     R2, R11
0x7BF2    1462       STR     R4, [R2,#0x20]
```

