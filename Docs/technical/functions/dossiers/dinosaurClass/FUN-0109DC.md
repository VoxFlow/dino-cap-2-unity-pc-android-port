# dinosaurClass::Attack

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0109DC` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x109DC` |
| RVA (EA - 0x1000) | `0xF9DC` |
| File offset | `0xF9DC` |
| Size | 96 code bytes in 1 chunk(s), 43 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap13dinosaurClass6AttackEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::dinosaurClass::Attack(void)` |
| Bytes SHA-256 | `1f4029d4c07e412715da542868361b71cc3fce1a02ae6fd5b91c10007bf03c2f` |
| Direct code callers | 2 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameCombat.cs:597`](../../../../../Assets/Scripts/GameCombat.cs#L597) cites `0x109DC`

## Callers and callees (direct code references)

Callers: [`dinosaurClass::OnAnimationEndAdv`](../../../functions/dossiers/dinosaurClass/FUN-01170E.md), [`dinosaurClass::OnUpdate`](../../../functions/dossiers/dinosaurClass/FUN-011BA4.md)

Callees: [`CAObjCallBack::playAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00A064.md), [`CAObjCallBack::setAnimationAdv`](../../../functions/dossiers/CAObjCallBack/FUN-00A298.md), [`CAObjCallBack::setAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00AEA0.md), `Triniti2D::GameObject::Physics`, `Triniti2D::GamePhysics::SetLinearVelocity`

## Strings it references

- `0x201C34` "AttackAnim"

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x109DC   90b5       PUSH    {R4,R7,LR}
0x109DE   01af       ADD     R7, SP, #4
0x109E0   83b0       SUB     SP, SP, #0xC
0x109E2   b423       MOVS    R3, #0xB4
0x109E4   041c       MOVS    R4, R0
0x109E6   c358       LDR     R3, [R0,R3]
0x109E8   002b       CMP     R3, #0
0x109EA   03d0       BEQ     loc_109F4
0x109EC   062b       CMP     R3, #6
0x109EE   01d0       BEQ     loc_109F4
0x109F0   082b       CMP     R3, #8
0x109F2   21d1       BNE     loc_10A38
0x109F4   b423       MOVS    R3, #0xB4
0x109F6   0122       MOVS    R2, #1
0x109F8   e250       STR     R2, [R4,R3]
0x109FA   104a       LDR     R2, =(aAttackanim - 0x10A06); "AttackAnim"
0x109FC   cc23       MOVS    R3, #0xCC
0x109FE   201c       MOVS    R0, R4; this
0x10A00   e158       LDR     R1, [R4,R3]; DinoCap::CombineAnimation *
0x10A02   7a44       ADD     R2, PC; "AttackAnim"
0x10A04   faf74cfa   BL      __ZN7DinoCap13CAObjCallBack12setAnimationEPNS_16CombineAnimationEPKc; DinoCap::CAObjCallBack::setAnimation(DinoCap::CombineAnimation *,char const*)
0x10A08   0d4a       LDR     R2, =(asc_200E1C - 0x10A12); ""
0x10A0A   201c       MOVS    R0, R4; this
0x10A0C   0021       MOVS    R1, #0; DinoCap::CombineAnimation *
```

