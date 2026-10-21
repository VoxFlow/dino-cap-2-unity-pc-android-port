# effectClassN::OnAnimationEnd

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-11FA80` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x11FA80` |
| RVA (EA - 0x1000) | `0x11EA80` |
| File offset | `0x11EA80` |
| Size | 104 code bytes in 1 chunk(s), 43 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap12effectClassN14OnAnimationEndEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::effectClassN::OnAnimationEnd(void)` |
| Bytes SHA-256 | `9928870cfe61ac2ea0cac3667f7f0dc5a1fb2a7b68e58c59e704939ae8735085` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameEgg.cs:7`](../../../../../Assets/Scripts/GameEgg.cs#L7) cites `0x11FA80`

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: [`selectScene::ligsawEffCallBack`](../../../functions/dossiers/selectScene/FUN-0F90DE.md), [`effectClassN::Disappear`](../../../functions/dossiers/effectClassN/FUN-11FA60.md), `Triniti2D::GameObject::GetScene`, `Triniti2D::Scene::GetCallBack`, `___dynamic_cast`

## Named data it references

- `0x22A0C0` __ZTIN9Triniti2D13SceneCallBackE_ptr
- `0x22AC10` __ZTIN7DinoCap11selectSceneE_ptr
- `0x22CFA8` `typeinfo for'DinoCap::selectScene
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22A0C8` __ZTIN7DinoCap9mainSceneE_ptr
- `0x22C868` `typeinfo for'DinoCap::mainScene

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x11FA80  90b5       PUSH    {R4,R7,LR}
0x11FA82  01af       ADD     R7, SP, #4
0x11FA84  bc235b00   MOVS    R3, #0x178
0x11FA88  041c       MOVS    R4, R0
0x11FA8A  c358       LDR     R3, [R0,R3]
0x11FA8C  022b       CMP     R3, #2
0x11FA8E  14d0       BEQ     loc_11FABA
0x11FA90  042b       CMP     R3, #4
0x11FA92  25d1       BNE     loc_11FAE0
0x11FA94  4068       LDR     R0, [R0,#4]; this
0x11FA96  4cf05bfa   BL      __ZN9Triniti2D10GameObject8GetSceneEv; Triniti2D::GameObject::GetScene(void)
0x11FA9A  55f0cafb   BL      __ZN9Triniti2D5Scene11GetCallBackEv; Triniti2D::Scene::GetCallBack(void)
0x11FA9E  0028       CMP     R0, #0
0x11FAA0  08d0       BEQ     loc_11FAB4
0x11FAA2  1149       LDR     R1, =(__ZTIN9Triniti2D13SceneCallBackE_ptr - 0x11FAAC)
0x11FAA4  114a       LDR     R2, =(__ZTIN7DinoCap11selectSceneE_ptr - 0x11FAAE)
0x11FAA6  0023       MOVS    R3, #0; s2d
0x11FAA8  7944       ADD     R1, PC; __ZTIN9Triniti2D13SceneCallBackE_ptr
0x11FAAA  7a44       ADD     R2, PC; __ZTIN7DinoCap11selectSceneE_ptr
0x11FAAC  0968       LDR     R1, [R1]; lpstype
0x11FAAE  1268       LDR     R2, [R2]; lpdtype
0x11FAB0  caf014e9   BLX     ___dynamic_cast
0x11FAB4  d9f713fb   BL      __ZN7DinoCap11selectScene17ligsawEffCallBackEv; DinoCap::selectScene::ligsawEffCallBack(void)
0x11FAB8  12e0       B       loc_11FAE0
```

