# effectClass::Disappear

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-015EEA` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x15EEA` |
| RVA (EA - 0x1000) | `0x14EEA` |
| File offset | `0x14EEA` |
| Size | 78 code bytes in 1 chunk(s), 34 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11effectClass9DisappearEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::effectClass::Disappear(void)` |
| Bytes SHA-256 | `5e1e7d11882e245d74be68be39cbc413de7d2d89253f96794e26b4621cebd359` |
| Direct code callers | 2 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameFx.cs:61`](../../../../../Assets/Scripts/GameFx.cs#L61) cites `0x15EEA`

## Callers and callees (direct code references)

Callers: [`effectClass::UpdateWorld`](../../../functions/dossiers/effectClass/FUN-016248.md), [`effectClass::OnAnimationEnd`](../../../functions/dossiers/effectClass/FUN-016AF0.md)

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
0x15EEA   90b5       PUSH    {R4,R7,LR}
0x15EEC   01af       ADD     R7, SP, #4
0x15EEE   0021       MOVS    R1, #0; bool
0x15EF0   041c       MOVS    R4, R0
0x15EF2   4068       LDR     R0, [R0,#4]; this
0x15EF4   56f122f8   BL      __ZN9Triniti2D10GameObject6EnableEb; Triniti2D::GameObject::Enable(bool)
0x15EF8   5023       MOVS    R3, #0x50 ; 'P'
0x15EFA   0022       MOVS    R2, #0
0x15EFC   201c       MOVS    R0, R4; this
0x15EFE   0021       MOVS    R1, #0; bool
0x15F00   e254       STRB    R2, [R4,R3]
0x15F02   f4f79bf9   BL      __ZN7DinoCap13CAObjCallBack10setVisibleEb; DinoCap::CAObjCallBack::setVisible(bool)
0x15F06   0c4b       LDR     R3, =0x3F800000
0x15F08   6068       LDR     R0, [R4,#4]; this
0x15F0A   6365       STR     R3, [R4,#0x54]
0x15F0C   56f120f8   BL      __ZN9Triniti2D10GameObject8GetSceneEv; Triniti2D::GameObject::GetScene(void)
0x15F10   5ff18ff9   BL      __ZN9Triniti2D5Scene11GetCallBackEv; Triniti2D::Scene::GetCallBack(void)
0x15F14   0028       CMP     R0, #0
0x15F16   08d0       BEQ     loc_15F2A
0x15F18   0849       LDR     R1, =(__ZTIN9Triniti2D13SceneCallBackE_ptr - 0x15F22)
0x15F1A   094a       LDR     R2, =(__ZTIN7DinoCap9mainSceneE_ptr - 0x15F24)
0x15F1C   0023       MOVS    R3, #0; s2d
0x15F1E   7944       ADD     R1, PC; __ZTIN9Triniti2D13SceneCallBackE_ptr
0x15F20   7a44       ADD     R2, PC; __ZTIN7DinoCap9mainSceneE_ptr
```

