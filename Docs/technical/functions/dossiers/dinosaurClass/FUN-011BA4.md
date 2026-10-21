# dinosaurClass::OnUpdate

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-011BA4` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x11BA4` |
| RVA (EA - 0x1000) | `0x10BA4` |
| File offset | `0x10BA4` |
| Size | 208 code bytes in 1 chunk(s), 91 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap13dinosaurClass8OnUpdateEf` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::dinosaurClass::OnUpdate(float)` |
| Bytes SHA-256 | `42bd139f64be7185226ad5b5d5dc2a45ac3dce2f4bc89499aa2942b2cbff66b2` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameCombat.cs:351`](../../../../../Assets/Scripts/GameCombat.cs#L351) cites `0x11BA4`
- [`Assets/Scripts/GameCombat.cs:353`](../../../../../Assets/Scripts/GameCombat.cs#L353) cites `0x11BA4`

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: [`CAObjCallBack::OnUpdate`](../../../functions/dossiers/CAObjCallBack/FUN-00A6CC.md), [`dinosaurClass::Attack`](../../../functions/dossiers/dinosaurClass/FUN-0109DC.md), [`dinosaurClass::CheckAttack`](../../../functions/dossiers/dinosaurClass/FUN-010B94.md), [`dinosaurClass::Move`](../../../functions/dossiers/dinosaurClass/FUN-011784.md), [`dinosaurClass::UpdateWorld`](../../../functions/dossiers/dinosaurClass/FUN-011B40.md), `Triniti2D::GameObject::GetPosition`, `Triniti2D::GameObject::SetPosition`, `___addsf3vfp`, `___mulsf3vfp`, `___subsf3vfp`

## Named data it references

- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A0E8` __ZN7DinoCap10g_ground_yE_ptr
- `0x23B910` DinoCap::g_ground_y

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x11BA4   b0b5       PUSH    {R4,R5,R7,LR}
0x11BA6   02af       ADD     R7, SP, #8
0x11BA8   82b0       SUB     SP, SP, #8
0x11BAA   041c       MOVS    R4, R0
0x11BAC   f8f78efd   BL      __ZN7DinoCap13CAObjCallBack8OnUpdateEf; DinoCap::CAObjCallBack::OnUpdate(float)
0x11BB0   b423       MOVS    R3, #0xB4
0x11BB2   e258       LDR     R2, [R4,R3]
0x11BB4   d31e       SUBS    R3, R2, #3
0x11BB6   042b       CMP     R3, #4
0x11BB8   5ad9       BLS     loc_11C70
0x11BBA   2e4b       LDR     R3, =(__ZN7DinoCap8g_iphoneE_ptr - 0x11BC0)
0x11BBC   7b44       ADD     R3, PC; __ZN7DinoCap8g_iphoneE_ptr
0x11BBE   1b68       LDR     R3, [R3]; DinoCap::g_iphone ...
0x11BC0   1b78       LDRB    R3, [R3]; DinoCap::g_iphone
0x11BC2   002b       CMP     R3, #0
0x11BC4   01d1       BNE     loc_11BCA
0x11BC6   2c4d       LDR     R5, =0x40000000
0x11BC8   00e0       B       loc_11BCC
0x11BCA   2c4d       LDR     R5, =0x3F800000
0x11BCC   082a       CMP     R2, #8
0x11BCE   3cd1       BNE     loc_11C4A
0x11BD0   6068       LDR     R0, [R4,#4]; this
0x11BD2   5af1b7f9   BL      __ZN9Triniti2D10GameObject11GetPositionEv; Triniti2D::GameObject::GetPosition(void)
0x11BD6   c423       MOVS    R3, #0xC4
```

