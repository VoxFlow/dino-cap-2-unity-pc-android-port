# playerClass::OnFireUp

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0B8BC6` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xB8BC6` |
| RVA (EA - 0x1000) | `0xB7BC6` |
| File offset | `0xB7BC6` |
| Size | 176 code bytes in 1 chunk(s), 76 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11playerClass8OnFireUpEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerClass::OnFireUp(void)` |
| Bytes SHA-256 | `1ac5d256f055e988573782b5ea14b4a0fb3ce2131ce0655b862f7a51aedccf5c` |
| Direct code callers | 13 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameBeam.cs:272`](../../../../../Assets/Scripts/GameBeam.cs#L272) cites `0xB8BC6`
- [`Assets/Scripts/GameFire.cs:7`](../../../../../Assets/Scripts/GameFire.cs#L7) cites `0xB8BC6`
- [`Assets/Scripts/GameFire.cs:265`](../../../../../Assets/Scripts/GameFire.cs#L265) cites `0xB8BC6`

## Callers and callees (direct code references)

Callers: `sub_BA20A`, [`playerClass::SwitchWeapon`](../../../functions/dossiers/playerClass/FUN-0BAC3C.md), [`playerClass::UpdateCamera`](../../../functions/dossiers/playerClass/FUN-0BB054.md), [`playerClass::BeAttack`](../../../functions/dossiers/playerClass/FUN-0BD166.md), [`playerClass::BeAttackByBoss`](../../../functions/dossiers/playerClass/FUN-0BD3C8.md), [`playerClass::OnRollDown`](../../../functions/dossiers/playerClass/FUN-0BD668.md), [`playerClass::OnHitDown`](../../../functions/dossiers/playerClass/FUN-0BD7C8.md), [`playerClass::EnterHouse`](../../../functions/dossiers/playerClass/FUN-0BDEEC.md), [`playerClass::OnKeyFrame`](../../../functions/dossiers/playerClass/FUN-0BF406.md), [`playerClass::OnKeyFrameAdv`](../../../functions/dossiers/playerClass/FUN-0C076A.md)

Callees: `Triniti2D::GameObject::Enable`, `Triniti2D::GameObject::GetCallBack`, `___dynamic_cast`

## Named data it references

- `0x22A0A8` __ZTIN9Triniti2D18GameObjectCallBackE_ptr
- `0x22A260` __ZTIN7DinoCap11effectClassE_ptr
- `0x22C814` `typeinfo for'DinoCap::effectClass
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xB8BC6   b0b5       PUSH    {R4,R5,R7,LR}
0xB8BC8   02af       ADD     R7, SP, #8
0xB8BCA   af24a400   MOVS    R4, #0x2BC
0xB8BCE   051c       MOVS    R5, R0
0xB8BD0   0059       LDR     R0, [R0,R4]; this
0xB8BD2   0028       CMP     R0, #0
0xB8BD4   16d0       BEQ     loc_B8C04
0xB8BD6   b3f0b3f9   BL      __ZN9Triniti2D10GameObject6EnableEv; Triniti2D::GameObject::Enable(void)
0xB8BDA   0028       CMP     R0, #0
0xB8BDC   12d0       BEQ     loc_B8C04
0xB8BDE   2859       LDR     R0, [R5,R4]; this
0xB8BE0   b3f0b8f9   BL      __ZN9Triniti2D10GameObject11GetCallBackEv; Triniti2D::GameObject::GetCallBack(void)
0xB8BE4   0028       CMP     R0, #0
0xB8BE6   08d0       BEQ     loc_B8BFA
0xB8BE8   2349       LDR     R1, =(__ZTIN9Triniti2D18GameObjectCallBackE_ptr - 0xB8BF2)
0xB8BEA   244a       LDR     R2, =(__ZTIN7DinoCap11effectClassE_ptr - 0xB8BF4)
0xB8BEC   0023       MOVS    R3, #0; s2d
0xB8BEE   7944       ADD     R1, PC; __ZTIN9Triniti2D18GameObjectCallBackE_ptr
0xB8BF0   7a44       ADD     R2, PC; __ZTIN7DinoCap11effectClassE_ptr
0xB8BF2   0968       LDR     R1, [R1]; lpstype
0xB8BF4   1268       LDR     R2, [R2]; lpdtype
0xB8BF6   31f172e8   BLX     ___dynamic_cast
0xB8BFA   0023       MOVS    R3, #0
0xB8BFC   4364       STR     R3, [R0,#0x44]
```

