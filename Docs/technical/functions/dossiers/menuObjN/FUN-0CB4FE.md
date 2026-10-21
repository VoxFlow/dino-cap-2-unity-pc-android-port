# menuObjN::OnKeyFrame

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0CB4FE` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xCB4FE` |
| RVA (EA - 0x1000) | `0xCA4FE` |
| File offset | `0xCA4FE` |
| Size | 632 code bytes in 1 chunk(s), 266 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap8menuObjN10OnKeyFrameEm` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::menuObjN::OnKeyFrame(unsigned long)` |
| Bytes SHA-256 | `366391f90a83d3ffc644eeccdce0500f994254fdbbf67478ec4c9faabeadba74` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/LoadingAnimation.cs:83`](../../../../../Assets/Scripts/LoadingAnimation.cs#L83) (by name)

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: [`CAObjCallBack::playAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00A064.md), [`CAObjCallBack::setAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00AEA0.md), [`menuScene::GetBaozha`](../../../functions/dossiers/menuScene/FUN-0C966E.md), `Triniti2D::GameObject::Enable`, `Triniti2D::GameObject::GetScene`, `Triniti2D::GameObject::GetCallBack`, `Triniti2D::GameObject::Physics`, `Triniti2D::GameObject::SetPosition`, `Triniti2D::GamePhysics::SetLinearVelocity`, `Triniti2D::GamePhysics::WakeUp`, `Triniti2D::Scene::GetCallBack`, `___switch16`, `___addsf3vfp`, `___dynamic_cast`, `___mulsf3vfp`

## Strings it references

- `0x208470` "baozhaAnim"

## Named data it references

- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A0C0` __ZTIN9Triniti2D13SceneCallBackE_ptr
- `0x22CCBC` `typeinfo for'DinoCap::menuScene
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22A0A8` __ZTIN9Triniti2D18GameObjectCallBackE_ptr
- `0x22AA20` __ZTIN7DinoCap12loadEffClassE_ptr
- `0x22C7FC` `typeinfo for'DinoCap::loadEffClass

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xCB4FE   f0b5       PUSH    {R4-R7,LR}
0xCB500   4646       MOV     R6, R8
0xCB502   40b4       PUSH    {R6}
0xCB504   04af       ADD     R7, SP, #0x10
0xCB506   83b0       SUB     SP, SP, #0xC
0xCB508   9b4b       LDR     R3, =(__ZN7DinoCap8g_iphoneE_ptr - 0xCB50E)
0xCB50A   7b44       ADD     R3, PC; __ZN7DinoCap8g_iphoneE_ptr
0xCB50C   1b68       LDR     R3, [R3]; DinoCap::g_iphone ...
0xCB50E   1b78       LDRB    R3, [R3]; DinoCap::g_iphone
0xCB510   002b       CMP     R3, #0
0xCB512   01d1       BNE     loc_CB518
0xCB514   994e       LDR     R6, =0x40000000
0xCB516   00e0       B       loc_CB51A
0xCB518   994e       LDR     R6, =0x3F800000
0xCB51A   4068       LDR     R0, [R0,#4]; this
0xCB51C   a0f018fd   BL      __ZN9Triniti2D10GameObject8GetSceneEv; Triniti2D::GameObject::GetScene(void)
0xCB520   a9f087fe   BL      __ZN9Triniti2D5Scene11GetCallBackEv; Triniti2D::Scene::GetCallBack(void)
0xCB524   0028       CMP     R0, #0
0xCB526   02d1       BNE     loc_CB52E
0xCB528   0023       MOVS    R3, #0
0xCB52A   9846       MOV     R8, R3
0xCB52C   08e0       B       loc_CB540
0xCB52E   9549       LDR     R1, =(__ZTIN9Triniti2D13SceneCallBackE_ptr - 0xCB538)
0xCB530   954a       LDR     R2, =(__ZTIN7DinoCap9menuSceneE - 0xCB53A); `typeinfo for'DinoCap::menuScene ...
```

