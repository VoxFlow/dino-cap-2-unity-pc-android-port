# playerClass::getDownEgg

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0B96F8` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xB96F8` |
| RVA (EA - 0x1000) | `0xB86F8` |
| File offset | `0xB86F8` |
| Size | 582 code bytes in 1 chunk(s), 227 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11playerClass10getDownEggEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerClass::getDownEgg(void)` |
| Bytes SHA-256 | `febc8a71f2edfa354ccaa97cc249080f54c8e45f711b9528069eae977819f8e1` |
| Direct code callers | 5 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameEgg.cs:8`](../../../../../Assets/Scripts/GameEgg.cs#L8) cites `0xB96F8`
- [`Assets/Scripts/GameEgg.cs:156`](../../../../../Assets/Scripts/GameEgg.cs#L156) cites `0xB96F8`
- [`Assets/Scripts/NativeCaAnimation.cs:129`](../../../../../Assets/Scripts/NativeCaAnimation.cs#L129) cites `0xB96F8`

## Callers and callees (direct code references)

Callers: [`playerClass::UpdateCamera`](../../../functions/dossiers/playerClass/FUN-0BB054.md), [`playerClass::BeAttack`](../../../functions/dossiers/playerClass/FUN-0BD166.md), [`playerClass::BeAttackByBoss`](../../../functions/dossiers/playerClass/FUN-0BD3C8.md)

Callees: [`CombineAnimation::ChangeImage`](../../../functions/dossiers/CombineAnimation/FUN-00CC8A.md), [`mainScene::CanRollDown`](../../../functions/dossiers/mainScene/FUN-094C2A.md), `Triniti2D::GameObject::GetScene`, `Triniti2D::Scene::GetCallBack`, `___switch16`, `___dynamic_cast`

## Strings it references

- `0x2012D8` "yyj4"
- `0x2013D0` "shenti0001"
- `0x20127C` "yuanjian4"
- `0x201288` "shenti0002"
- `0x201394` "com2"
- `0x2013A4` "com4"
- `0x201318` "shenti0003"
- `0x201368` "shenti0004"

## Named data it references

- `0x22A0C0` __ZTIN9Triniti2D13SceneCallBackE_ptr
- `0x22A0C8` __ZTIN7DinoCap9mainSceneE_ptr
- `0x22C868` `typeinfo for'DinoCap::mainScene
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22A2B8` __ZN7DinoCap13g_player_typeE_ptr
- `0x23B86C` DinoCap::g_player_type

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xB96F8   f0b5       PUSH    {R4-R7,LR}
0xB96FA   03af       ADD     R7, SP, #0xC
0xB96FC   041c       MOVS    R4, R0
0xB96FE   4068       LDR     R0, [R0,#4]; this
0xB9700   b2f026fc   BL      __ZN9Triniti2D10GameObject8GetSceneEv; Triniti2D::GameObject::GetScene(void)
0xB9704   bbf095fd   BL      __ZN9Triniti2D5Scene11GetCallBackEv; Triniti2D::Scene::GetCallBack(void)
0xB9708   0028       CMP     R0, #0
0xB970A   08d0       BEQ     loc_B971E
0xB970C   8c49       LDR     R1, =(__ZTIN9Triniti2D13SceneCallBackE_ptr - 0xB9716)
0xB970E   8d4a       LDR     R2, =(__ZTIN7DinoCap9mainSceneE_ptr - 0xB9718)
0xB9710   0023       MOVS    R3, #0; s2d
0xB9712   7944       ADD     R1, PC; __ZTIN9Triniti2D13SceneCallBackE_ptr
0xB9714   7a44       ADD     R2, PC; __ZTIN7DinoCap9mainSceneE_ptr
0xB9716   0968       LDR     R1, [R1]; lpstype
0xB9718   1268       LDR     R2, [R2]; lpdtype
0xB971A   30f1e0ea   BLX     ___dynamic_cast
0xB971E   dbf784fa   BL      __ZN7DinoCap9mainScene11CanRollDownEv; DinoCap::mainScene::CanRollDown(void)
0xB9722   894b       LDR     R3, =(__ZN7DinoCap13g_player_typeE_ptr - 0xB9728)
0xB9724   7b44       ADD     R3, PC; __ZN7DinoCap13g_player_typeE_ptr
0xB9726   1868       LDR     R0, [R3]; DinoCap::g_player_type ...
0xB9728   0368       LDR     R3, [R0]; DinoCap::g_player_type
0xB972A   181c       MOVS    R0, R3
0xB972C   2ef1b8eb   BLX     ___switch16; switch 5 cases
0xB973C   834e       LDR     R6, =(aYyj4 - 0xB9746); jumptable 000B972C case 0
```

