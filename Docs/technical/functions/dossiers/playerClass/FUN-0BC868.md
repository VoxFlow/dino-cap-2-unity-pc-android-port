# playerClass::FeiBiao

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0BC868` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xBC868` |
| RVA (EA - 0x1000) | `0xBB868` |
| File offset | `0xBB868` |
| Size | 352 code bytes in 1 chunk(s), 151 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11playerClass7FeiBiaoEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerClass::FeiBiao(void)` |
| Bytes SHA-256 | `6220a20454d38aba6131264df14539dec51bed691afe96257a9ba9e0fc29f791` |
| Direct code callers | 2 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameBeam.cs:327`](../../../../../Assets/Scripts/GameBeam.cs#L327) cites `0xBC868`

## Callers and callees (direct code references)

Callers: [`playerClass::OnKeyFrame`](../../../functions/dossiers/playerClass/FUN-0BF406.md), [`playerClass::OnKeyFrameAdv`](../../../functions/dossiers/playerClass/FUN-0C076A.md)

Callees: [`CAObjCallBack::playAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00A064.md), [`CAObjCallBack::getSprite`](../../../functions/dossiers/CAObjCallBack/FUN-00A3BA.md), [`CAObjCallBack::getSpritePos`](../../../functions/dossiers/CAObjCallBack/FUN-00A4D0.md), [`CAObjCallBack::setAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00AEA0.md), [`PlaySound`](../../../functions/dossiers/DinoCap/FUN-01A448.md), [`mainScene::GetEffect`](../../../functions/dossiers/mainScene/FUN-0956B6.md), `Triniti2D::GameObject::Enable`, `Triniti2D::GameObject::GetSize`, `Triniti2D::GameObject::GetScene`, `Triniti2D::GameObject::GetCallBack`, `Triniti2D::GameObject::Physics`, `Triniti2D::GameObject::SetPosition`, `Triniti2D::GamePhysics::SetLinearVelocity`, `Triniti2D::GamePhysics::WakeUp`, `Triniti2D::Scene::GetCallBack`, `___dynamic_cast`, `___mulsf3vfp`

## Strings it references

- `0x20231C` "com15"
- `0x2083F4` "feibiaoAnimation"
- `0x2048A8` "Attack_F1Audio"

## Named data it references

- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A0C0` __ZTIN9Triniti2D13SceneCallBackE_ptr
- `0x22A0C8` __ZTIN7DinoCap9mainSceneE_ptr
- `0x22C868` `typeinfo for'DinoCap::mainScene
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22A0A8` __ZTIN9Triniti2D18GameObjectCallBackE_ptr
- `0x22A260` __ZTIN7DinoCap11effectClassE_ptr
- `0x22C814` `typeinfo for'DinoCap::effectClass
- `0x22A4E0` __ZN7DinoCap16g_ca_feibiaoDataE_ptr
- `0x23EE8C` DinoCap::g_ca_feibiaoData

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xBC868   f0b5       PUSH    {R4-R7,LR}
0xBC86A   5646       MOV     R6, R10
0xBC86C   4546       MOV     R5, R8
0xBC86E   60b4       PUSH    {R5,R6}
0xBC870   05af       ADD     R7, SP, #0x14
0xBC872   89b0       SUB     SP, SP, #0x24
0xBC874   544b       LDR     R3, =(__ZN7DinoCap8g_iphoneE_ptr - 0xBC87C)
0xBC876   8046       MOV     R8, R0
0xBC878   7b44       ADD     R3, PC; __ZN7DinoCap8g_iphoneE_ptr
0xBC87A   1b68       LDR     R3, [R3]; DinoCap::g_iphone ...
0xBC87C   1b78       LDRB    R3, [R3]; DinoCap::g_iphone
0xBC87E   002b       CMP     R3, #0
0xBC880   02d1       BNE     loc_BC888
0xBC882   524a       LDR     R2, =0x40000000
0xBC884   9246       MOV     R10, R2
0xBC886   01e0       B       loc_BC88C
0xBC888   514b       LDR     R3, =0x3F800000
0xBC88A   9a46       MOV     R10, R3
0xBC88C   514e       LDR     R6, =(aCom15 - 0xBC894); "com15"
0xBC88E   4046       MOV     R0, R8; this
0xBC890   7e44       ADD     R6, PC; "com15"
0xBC892   311c       MOVS    R1, R6; char *
0xBC894   4df791fd   BL      __ZN7DinoCap13CAObjCallBack9getSpriteEPKc; DinoCap::CAObjCallBack::getSprite(char const*)
0xBC898   aff056fb   BL      __ZN9Triniti2D10GameObject7GetSizeEv; Triniti2D::GameObject::GetSize(void)
```

