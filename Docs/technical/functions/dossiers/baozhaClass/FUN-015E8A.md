# baozhaClass::Disappear

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-015E8A` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x15E8A` |
| RVA (EA - 0x1000) | `0x14E8A` |
| File offset | `0x14E8A` |
| Size | 74 code bytes in 1 chunk(s), 32 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11baozhaClass9DisappearEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::baozhaClass::Disappear(void)` |
| Bytes SHA-256 | `f11ed67f0aa7ae651a05d432c1c1fb8027d8c92a2de31de5caf6563a477e72e1` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameFx.cs:61`](../../../../../Assets/Scripts/GameFx.cs#L61) cites `0x15E8A`

## Callers and callees (direct code references)

Callers: `baozhaClass::OnAnimationEnd`

Callees: `CAObjCallBack::setVisible`, `Triniti2D::GameObject::Enable`, `Triniti2D::GameObject::GetScene`, `Triniti2D::Scene::GetCallBack`, `___dynamic_cast`

## Named data it references

- `0x22A0C0` __ZTIN9Triniti2D13SceneCallBackE_ptr
- `0x22A0C8` __ZTIN7DinoCap9mainSceneE_ptr
- `0x22C868` `typeinfo for'DinoCap::mainScene
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22A11C` __ZNSt6vectorIPN9Triniti2D10GameObjectESaIS2_EE9push_backERKS2__ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x15E8A   90b5       PUSH    {R4,R7,LR}
0x15E8C   01af       ADD     R7, SP, #4
0x15E8E   0021       MOVS    R1, #0; bool
0x15E90   041c       MOVS    R4, R0
0x15E92   4068       LDR     R0, [R0,#4]; this
0x15E94   56f152f8   BL      __ZN9Triniti2D10GameObject6EnableEb; Triniti2D::GameObject::Enable(bool)
0x15E98   5023       MOVS    R3, #0x50 ; 'P'
0x15E9A   0022       MOVS    R2, #0
0x15E9C   201c       MOVS    R0, R4; this
0x15E9E   0021       MOVS    R1, #0; bool
0x15EA0   e254       STRB    R2, [R4,R3]
0x15EA2   f4f7cbf9   BL      __ZN7DinoCap13CAObjCallBack10setVisibleEb; DinoCap::CAObjCallBack::setVisible(bool)
0x15EA6   6068       LDR     R0, [R4,#4]; this
0x15EA8   56f152f8   BL      __ZN9Triniti2D10GameObject8GetSceneEv; Triniti2D::GameObject::GetScene(void)
0x15EAC   5ff1c1f9   BL      __ZN9Triniti2D5Scene11GetCallBackEv; Triniti2D::Scene::GetCallBack(void)
0x15EB0   0028       CMP     R0, #0
0x15EB2   08d0       BEQ     loc_15EC6
0x15EB4   0749       LDR     R1, =(__ZTIN9Triniti2D13SceneCallBackE_ptr - 0x15EBE)
0x15EB6   084a       LDR     R2, =(__ZTIN7DinoCap9mainSceneE_ptr - 0x15EC0)
0x15EB8   0023       MOVS    R3, #0; s2d
0x15EBA   7944       ADD     R1, PC; __ZTIN9Triniti2D13SceneCallBackE_ptr
0x15EBC   7a44       ADD     R2, PC; __ZTIN7DinoCap9mainSceneE_ptr
0x15EBE   0968       LDR     R1, [R1]; lpstype
0x15EC0   1268       LDR     R2, [R2]; lpdtype
```

