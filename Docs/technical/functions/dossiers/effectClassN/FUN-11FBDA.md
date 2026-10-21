# effectClassN::OnUpdate

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-11FBDA` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x11FBDA` |
| RVA (EA - 0x1000) | `0x11EBDA` |
| File offset | `0x11EBDA` |
| Size | 392 code bytes in 1 chunk(s), 164 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap12effectClassN8OnUpdateEf` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::effectClassN::OnUpdate(float)` |
| Bytes SHA-256 | `01645e15e1e473bff7958d3032bdfc141bcc02746ff998a0cd812d21a97e00fd` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameBoss.cs:459`](../../../../../Assets/Scripts/GameBoss.cs#L459) (by name)

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: [`PlaySound`](../../../functions/dossiers/DinoCap/FUN-01A448.md), [`playerClass::BeAttackByBoss`](../../../functions/dossiers/playerClass/FUN-0BD3C8.md), `CompAnimObj::GetCurAnimationNumber`, [`CompAnimObj::OnUpdate`](../../../functions/dossiers/CompAnimObj/FUN-1190D4.md), [`effectClassN::Disappear`](../../../functions/dossiers/effectClassN/FUN-11FA60.md), [`playerNewAniClass::BeAttackByBoss`](../../../functions/dossiers/playerNewAniClass/FUN-1286E4.md), `Triniti2D::GameObject::GetPosition`, `Triniti2D::GameObject::GetCallBack`, `Triniti2D::GameObject::SetPosition`, `___addsf3vfp`, `___dynamic_cast`, `___fixsfsivfp`, `___gesf2vfp`, `___lesf2vfp`, `___mulsf3vfp`, `___unordsf2vfp`

## Strings it references

- `0x204E84` "A_boss_yanjiang"

## Named data it references

- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A2B8` __ZN7DinoCap13g_player_typeE_ptr
- `0x23B86C` DinoCap::g_player_type
- `0x22A0A8` __ZTIN9Triniti2D18GameObjectCallBackE_ptr
- `0x22A288` __ZTIN7DinoCap17playerNewAniClassE_ptr
- `0x22D338` `typeinfo for'DinoCap::playerNewAniClass
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22A0CC` __ZTIN7DinoCap11playerClassE_ptr
- `0x22C8B0` `typeinfo for'DinoCap::playerClass

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x11FBDA  f0b5       PUSH    {R4-R7,LR}
0x11FBDC  5e46       MOV     R6, R11
0x11FBDE  5546       MOV     R5, R10
0x11FBE0  4446       MOV     R4, R8
0x11FBE2  70b4       PUSH    {R4-R6}
0x11FBE4  06af       ADD     R7, SP, #0x18
0x11FBE6  5f4b       LDR     R3, =(__ZN7DinoCap8g_iphoneE_ptr - 0x11FBF0)
0x11FBE8  061c       MOVS    R6, R0
0x11FBEA  8b46       MOV     R11, R1
0x11FBEC  7b44       ADD     R3, PC; __ZN7DinoCap8g_iphoneE_ptr
0x11FBEE  1b68       LDR     R3, [R3]; DinoCap::g_iphone ...
0x11FBF0  1b78       LDRB    R3, [R3]; DinoCap::g_iphone
0x11FBF2  002b       CMP     R3, #0
0x11FBF4  01d1       BNE     loc_11FBFA
0x11FBF6  5c4c       LDR     R4, =0x40000000
0x11FBF8  00e0       B       loc_11FBFC
0x11FBFA  5c4c       LDR     R4, =0x3F800000
0x11FBFC  bc235b00   MOVS    R3, #0x178
0x11FC00  f358       LDR     R3, [R6,R3]
0x11FC02  012b       CMP     R3, #1
0x11FC04  03d0       BEQ     loc_11FC0E
0x11FC06  052b       CMP     R3, #5
0x11FC08  00d0       BEQ     loc_11FC0C
0x11FC0A  9ae0       B       loc_11FD42
```

