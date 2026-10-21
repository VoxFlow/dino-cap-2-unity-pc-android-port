# houseClass::OnKeyFrame

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-006E1C` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x6E1C` |
| RVA (EA - 0x1000) | `0x5E1C` |
| File offset | `0x5E1C` |
| Size | 94 code bytes in 1 chunk(s), 40 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap10houseClass10OnKeyFrameEm` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::houseClass::OnKeyFrame(unsigned long)` |
| Bytes SHA-256 | `7251f6e4378f035ebbe1a6386980c00f1521fd8ae0c7bbabe47547b20f705db4` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameCombat.cs:77`](../../../../../Assets/Scripts/GameCombat.cs#L77) (by name)
- [`Assets/Scripts/GameHouse.cs:8`](../../../../../Assets/Scripts/GameHouse.cs#L8) (by name)

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: [`playerClass::EnterHouse`](../../../functions/dossiers/playerClass/FUN-0BDEEC.md), `Triniti2D::GameObject::GetScene`, `Triniti2D::GameObject::GetCallBack`, `Triniti2D::GameObject::KillTimer`, `Triniti2D::Scene::GetCallBack`, `___dynamic_cast`

## Named data it references

- `0x22A0C0` __ZTIN9Triniti2D13SceneCallBackE_ptr
- `0x22A0C8` __ZTIN7DinoCap9mainSceneE_ptr
- `0x22C868` `typeinfo for'DinoCap::mainScene
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22A0A8` __ZTIN9Triniti2D18GameObjectCallBackE_ptr
- `0x22A0CC` __ZTIN7DinoCap11playerClassE_ptr
- `0x22C8B0` `typeinfo for'DinoCap::playerClass

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x6E1C    f0b5       PUSH    {R4-R7,LR}
0x6E1E    03af       ADD     R7, SP, #0xC
0x6E20    051c       MOVS    R5, R0
0x6E22    4068       LDR     R0, [R0,#4]; this
0x6E24    65f194f8   BL      __ZN9Triniti2D10GameObject8GetSceneEv; Triniti2D::GameObject::GetScene(void)
0x6E28    6ef103fa   BL      __ZN9Triniti2D5Scene11GetCallBackEv; Triniti2D::Scene::GetCallBack(void)
0x6E2C    0028       CMP     R0, #0
0x6E2E    08d0       BEQ     loc_6E42
0x6E30    1249       LDR     R1, =(__ZTIN9Triniti2D13SceneCallBackE_ptr - 0x6E3A)
0x6E32    134a       LDR     R2, =(__ZTIN7DinoCap9mainSceneE_ptr - 0x6E3C)
0x6E34    0023       MOVS    R3, #0; s2d
0x6E36    7944       ADD     R1, PC; __ZTIN9Triniti2D13SceneCallBackE_ptr
0x6E38    7a44       ADD     R2, PC; __ZTIN7DinoCap9mainSceneE_ptr
0x6E3A    0968       LDR     R1, [R1]; lpstype
0x6E3C    1268       LDR     R2, [R2]; lpdtype
0x6E3E    e2f14eef   BLX     ___dynamic_cast
0x6E42    c06a       LDR     R0, [R0,#0x2C]; this
0x6E44    65f186f8   BL      __ZN9Triniti2D10GameObject11GetCallBackEv; Triniti2D::GameObject::GetCallBack(void)
0x6E48    0028       CMP     R0, #0
0x6E4A    08d0       BEQ     loc_6E5E
0x6E4C    0d49       LDR     R1, =(__ZTIN9Triniti2D18GameObjectCallBackE_ptr - 0x6E56)
0x6E4E    0e4a       LDR     R2, =(__ZTIN7DinoCap11playerClassE_ptr - 0x6E58)
0x6E50    0023       MOVS    R3, #0; s2d
0x6E52    7944       ADD     R1, PC; __ZTIN9Triniti2D18GameObjectCallBackE_ptr
```

