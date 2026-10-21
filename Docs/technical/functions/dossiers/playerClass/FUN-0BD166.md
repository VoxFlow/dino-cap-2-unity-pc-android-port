# playerClass::BeAttack

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0BD166` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xBD166` |
| RVA (EA - 0x1000) | `0xBC166` |
| File offset | `0xBC166` |
| Size | 540 code bytes in 1 chunk(s), 228 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11playerClass8BeAttackEPN9Triniti2D10GameObjectE` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerClass::BeAttack(Triniti2D::GameObject *)` |
| Bytes SHA-256 | `fa874e0c4d99eac99126430ef4f105b42276235ad8501ab6032d2e7bb65eb789` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameCombat.cs:627`](../../../../../Assets/Scripts/GameCombat.cs#L627) cites `0xBD166`
- [`Assets/Scripts/GameCombat.cs:630`](../../../../../Assets/Scripts/GameCombat.cs#L630) cites `0xBD166`
- [`Assets/Scripts/GameCombat.cs:640`](../../../../../Assets/Scripts/GameCombat.cs#L640) cites `0xBD23A`
- [`Assets/Scripts/GameEgg.cs:202`](../../../../../Assets/Scripts/GameEgg.cs#L202) cites `0xBD23A`
- [`Assets/Scripts/GameFire.cs:268`](../../../../../Assets/Scripts/GameFire.cs#L268) cites `0xBD196`

## Callers and callees (direct code references)

Callers: [`dinosaurClass::OnKeyFrame`](../../../functions/dossiers/dinosaurClass/FUN-0156D4.md)

Callees: [`CAObjCallBack::playAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00A064.md), [`CAObjCallBack::setAnimationAdv`](../../../functions/dossiers/CAObjCallBack/FUN-00A298.md), [`CAObjCallBack::setAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00AEA0.md), [`PlaySound`](../../../functions/dossiers/DinoCap/FUN-01A448.md), [`mainScene::UpdateHP`](../../../functions/dossiers/mainScene/FUN-095228.md), [`mainScene::initEgg`](../../../functions/dossiers/mainScene/FUN-095DC0.md), [`mainScene::BrokenEgg`](../../../functions/dossiers/mainScene/FUN-09725A.md), [`playerClass::UpdateBG`](../../../functions/dossiers/playerClass/FUN-0B8944.md), [`playerClass::OnFireUp`](../../../functions/dossiers/playerClass/FUN-0B8BC6.md), [`playerClass::getDownEgg`](../../../functions/dossiers/playerClass/FUN-0B96F8.md), [`playerClass::Die`](../../../functions/dossiers/playerClass/FUN-0BCDEA.md), `Triniti2D::GameObject::GetPosition`, `Triniti2D::GameObject::GetScene`, `Triniti2D::GameObject::GetCallBack`, `Triniti2D::GameObject::Physics`, `Triniti2D::GameObject::SetTimer`, `Triniti2D::GamePhysics::SetLinearVelocity`, `Triniti2D::GamePhysics::WakeUp`, `Triniti2D::Scene::GetCallBack`, `std::string::_M_mutate`, `___dynamic_cast`, `___fixsfsivfp`, `___floatsisfvfp`, `___lesf2vfp`, `___ltsf2vfp`, `___subsf3vfp`

## Strings it references

- `0x208418` "behitAnim"
- `0x2083C0` "Wudi"
- `0x204800` "Audio_RoleAudio"

## Named data it references

- `0x22A0A8` __ZTIN9Triniti2D18GameObjectCallBackE_ptr
- `0x22A2F8` __ZTIN7DinoCap13dinosaurClassE_ptr
- `0x22C73C` `typeinfo for'DinoCap::dinosaurClass
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22A2DC` __ZN7DinoCap13g_prop_PlayerE_ptr
- `0x240474` DinoCap::g_prop_Player
- `0x22A0C0` __ZTIN9Triniti2D13SceneCallBackE_ptr
- `0x22A0C8` __ZTIN7DinoCap9mainSceneE_ptr
- `0x22C868` `typeinfo for'DinoCap::mainScene

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xBD166   f0b5       PUSH    {R4-R7,LR}
0xBD168   5646       MOV     R6, R10
0xBD16A   4546       MOV     R5, R8
0xBD16C   60b4       PUSH    {R5,R6}
0xBD16E   05af       ADD     R7, SP, #0x14
0xBD170   85b0       SUB     SP, SP, #0x14
0xBD172   844b       LDR     R3, =0x292
0xBD174   061c       MOVS    R6, R0
0xBD176   8a46       MOV     R10, R1
0xBD178   c55c       LDRB    R5, [R0,R3]
0xBD17A   002d       CMP     R5, #0
0xBD17C   00d0       BEQ     loc_BD180
0xBD17E   fbe0       B       loc_BD378
0xBD180   a0239b00   MOVS    R3, #0x280
0xBD184   c358       LDR     R3, [R0,R3]
0xBD186   043b       SUBS    R3, #4
0xBD188   032b       CMP     R3, #3
0xBD18A   00d8       BHI     loc_BD18E
0xBD18C   f4e0       B       loc_BD378
0xBD18E   a424a400   MOVS    R4, #0x290
0xBD192   035d       LDRB    R3, [R0,R4]
0xBD194   002b       CMP     R3, #0
0xBD196   0bd0       BEQ     loc_BD1B0
0xBD198   fbf715fd   BL      __ZN7DinoCap11playerClass8OnFireUpEv; DinoCap::playerClass::OnFireUp(void)
```

**Around `0xBD23A` (cited by the Unity code)**

```
0xBD22A   4346       MOV     R3, R8
0xBD22C   281c       MOVS    R0, R5; this
0xBD22E   2360       STR     R3, [R4]; DinoCap::g_prop_Player
0xBD230   d7f7faff   BL      __ZN7DinoCap9mainScene8UpdateHPEv; DinoCap::mainScene::UpdateHP(void)
0xBD234   301c       MOVS    R0, R6; this
0xBD236   fff7d8fd   BL      __ZN7DinoCap11playerClass3DieEv; DinoCap::playerClass::Die(void)
0xBD23A   5a4a       LDR     R2, =0x10D   <==
0xBD23C   ab5c       LDRB    R3, [R5,R2]
0xBD23E   002b       CMP     R3, #0
0xBD240   00d1       BNE     loc_BD244
0xBD242   99e0       B       loc_BD378
0xBD244   0023       MOVS    R3, #0
0xBD246   ab54       STRB    R3, [R5,R2]
```

