# playerClass::JuPan

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0BCA10` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xBCA10` |
| RVA (EA - 0x1000) | `0xBBA10` |
| File offset | `0xBBA10` |
| Size | 352 code bytes in 1 chunk(s), 151 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11playerClass5JuPanEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerClass::JuPan(void)` |
| Bytes SHA-256 | `6ff78355c622ebc97de354b3fd2851bd1c8c20a9d5550108c44797229372671e` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.constants_audit.r118` (PROVEN, NATIVE): getJigsaw lowers the seed when the mean level > 4.5 (0x40900000 at 0xd0db6) and playerClass::JuPan y factor is 0.04 (0x3D23D70A at 0xbca6c); earlier 5.5 / 0.043 were wrong.

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameBeam.cs:319`](../../../../../Assets/Scripts/GameBeam.cs#L319) cites `0xBCA10`

## Callers and callees (direct code references)

Callers: [`playerClass::OnKeyFrameAdv`](../../../functions/dossiers/playerClass/FUN-0C076A.md)

Callees: [`CAObjCallBack::playAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00A064.md), [`CAObjCallBack::getSprite`](../../../functions/dossiers/CAObjCallBack/FUN-00A3BA.md), [`CAObjCallBack::getSpritePos`](../../../functions/dossiers/CAObjCallBack/FUN-00A4D0.md), [`CAObjCallBack::setAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00AEA0.md), [`PlaySound`](../../../functions/dossiers/DinoCap/FUN-01A448.md), [`mainScene::GetEffect`](../../../functions/dossiers/mainScene/FUN-0956B6.md), `Triniti2D::GameObject::Enable`, `Triniti2D::GameObject::GetSize`, `Triniti2D::GameObject::GetScene`, `Triniti2D::GameObject::GetCallBack`, `Triniti2D::GameObject::Physics`, `Triniti2D::GameObject::SetPosition`, `Triniti2D::GamePhysics::SetLinearVelocity`, `Triniti2D::GamePhysics::WakeUp`, `Triniti2D::Scene::GetCallBack`, `___dynamic_cast`, `___mulsf3vfp`

## Strings it references

- `0x2012E0` "yyj5"
- `0x208408` "jupanAnimation"
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
- `0x22A52C` __ZN7DinoCap14g_ca_jupanDataE_ptr
- `0x23EE70` DinoCap::g_ca_jupanData

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xBCA10   f0b5       PUSH    {R4-R7,LR}
0xBCA12   5646       MOV     R6, R10
0xBCA14   4546       MOV     R5, R8
0xBCA16   60b4       PUSH    {R5,R6}
0xBCA18   05af       ADD     R7, SP, #0x14
0xBCA1A   89b0       SUB     SP, SP, #0x24
0xBCA1C   544b       LDR     R3, =(__ZN7DinoCap8g_iphoneE_ptr - 0xBCA24)
0xBCA1E   8046       MOV     R8, R0
0xBCA20   7b44       ADD     R3, PC; __ZN7DinoCap8g_iphoneE_ptr
0xBCA22   1b68       LDR     R3, [R3]; DinoCap::g_iphone ...
0xBCA24   1b78       LDRB    R3, [R3]; DinoCap::g_iphone
0xBCA26   002b       CMP     R3, #0
0xBCA28   02d1       BNE     loc_BCA30
0xBCA2A   524a       LDR     R2, =0x40000000
0xBCA2C   9246       MOV     R10, R2
0xBCA2E   01e0       B       loc_BCA34
0xBCA30   514b       LDR     R3, =0x3F800000
0xBCA32   9a46       MOV     R10, R3
0xBCA34   514e       LDR     R6, =(aYyj5 - 0xBCA3C); "yyj5"
0xBCA36   4046       MOV     R0, R8; this
0xBCA38   7e44       ADD     R6, PC; "yyj5"
0xBCA3A   311c       MOVS    R1, R6; char *
0xBCA3C   4df7bdfc   BL      __ZN7DinoCap13CAObjCallBack9getSpriteEPKc; DinoCap::CAObjCallBack::getSprite(char const*)
0xBCA40   aff082fa   BL      __ZN9Triniti2D10GameObject7GetSizeEv; Triniti2D::GameObject::GetSize(void)
```

