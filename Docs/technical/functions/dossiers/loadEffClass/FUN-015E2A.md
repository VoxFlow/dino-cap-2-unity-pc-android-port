# loadEffClass::Disappear

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-015E2A` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x15E2A` |
| RVA (EA - 0x1000) | `0x14E2A` |
| File offset | `0x14E2A` |
| Size | 74 code bytes in 1 chunk(s), 32 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap12loadEffClass9DisappearEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::loadEffClass::Disappear(void)` |
| Bytes SHA-256 | `17d3a358cb2f7abe735e8c48df8d66904795a24a82eb1a1854d386e37e06f9b7` |
| Direct code callers | 2 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: [`loadEffClass::OnAnimationEnd`](../../../functions/dossiers/loadEffClass/FUN-015E80.md), [`loadEffClass::OnUpdate`](../../../functions/dossiers/loadEffClass/FUN-015F48.md)

Callees: `CAObjCallBack::setVisible`, `Triniti2D::GameObject::Enable`, `Triniti2D::GameObject::GetScene`, `Triniti2D::Scene::GetCallBack`, `___dynamic_cast`

## Named data it references

- `0x22A0C0` __ZTIN9Triniti2D13SceneCallBackE_ptr
- `0x22A2EC` __ZTIN7DinoCap9menuSceneE_ptr
- `0x22CCBC` `typeinfo for'DinoCap::menuScene
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22A11C` __ZNSt6vectorIPN9Triniti2D10GameObjectESaIS2_EE9push_backERKS2__ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x15E2A   90b5       PUSH    {R4,R7,LR}
0x15E2C   01af       ADD     R7, SP, #4
0x15E2E   0021       MOVS    R1, #0; bool
0x15E30   041c       MOVS    R4, R0
0x15E32   4068       LDR     R0, [R0,#4]; this
0x15E34   56f182f8   BL      __ZN9Triniti2D10GameObject6EnableEb; Triniti2D::GameObject::Enable(bool)
0x15E38   5023       MOVS    R3, #0x50 ; 'P'
0x15E3A   0022       MOVS    R2, #0
0x15E3C   201c       MOVS    R0, R4; this
0x15E3E   0021       MOVS    R1, #0; bool
0x15E40   e254       STRB    R2, [R4,R3]
0x15E42   f4f7fbf9   BL      __ZN7DinoCap13CAObjCallBack10setVisibleEb; DinoCap::CAObjCallBack::setVisible(bool)
0x15E46   6068       LDR     R0, [R4,#4]; this
0x15E48   56f182f8   BL      __ZN9Triniti2D10GameObject8GetSceneEv; Triniti2D::GameObject::GetScene(void)
0x15E4C   5ff1f1f9   BL      __ZN9Triniti2D5Scene11GetCallBackEv; Triniti2D::Scene::GetCallBack(void)
0x15E50   0028       CMP     R0, #0
0x15E52   08d0       BEQ     loc_15E66
0x15E54   0749       LDR     R1, =(__ZTIN9Triniti2D13SceneCallBackE_ptr - 0x15E5E)
0x15E56   084a       LDR     R2, =(__ZTIN7DinoCap9menuSceneE_ptr - 0x15E60)
0x15E58   0023       MOVS    R3, #0; s2d
0x15E5A   7944       ADD     R1, PC; __ZTIN9Triniti2D13SceneCallBackE_ptr
0x15E5C   7a44       ADD     R2, PC; __ZTIN7DinoCap9menuSceneE_ptr
0x15E5E   0968       LDR     R1, [R1]; lpstype
0x15E60   1268       LDR     R2, [R2]; lpdtype
```

