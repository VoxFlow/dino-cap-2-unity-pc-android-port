# playerClass::UpdateCamera

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0BB054` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xBB054` |
| RVA (EA - 0x1000) | `0xBA054` |
| File offset | `0xBA054` |
| Size | 1232 code bytes in 1 chunk(s), 497 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11playerClass12UpdateCameraEf` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerClass::UpdateCamera(float)` |
| Bytes SHA-256 | `513e526b7a48a7f649a21dec95982675eeafc35fd4569b91e99057cb8d455221` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | deep: curated note below, from the batch that established it |

## What it does

Camera and hero bounds per mission mode: run end (state 4 every frame, GameClear), egg delivery (count, EggFull, Stand), and the life-mode clamp of the hero to the camera bounds. Wide-view adaptations AD-002 and AD-005 build on these bounds.

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameEgg.cs:6`](../../../../../Assets/Scripts/GameEgg.cs#L6) cites `0xBB316`
- [`Assets/Scripts/GameEgg.cs:111`](../../../../../Assets/Scripts/GameEgg.cs#L111) cites `0xBB3C8`
- [`Assets/Scripts/GameEgg.cs:151`](../../../../../Assets/Scripts/GameEgg.cs#L151) cites `0xBB430`
- [`Assets/Scripts/GameFire.cs:12`](../../../../../Assets/Scripts/GameFire.cs#L12) cites `0xBB24C`
- [`Assets/Scripts/GameMainScene.cs:11`](../../../../../Assets/Scripts/GameMainScene.cs#L11) (by name)
- [`Assets/Scripts/GameMainScene.cs:41`](../../../../../Assets/Scripts/GameMainScene.cs#L41) (by name)
- [`Assets/Scripts/GameScene.cs:11`](../../../../../Assets/Scripts/GameScene.cs#L11) cites `0xBB054`
- [`Assets/Scripts/GameScene.cs:63`](../../../../../Assets/Scripts/GameScene.cs#L63) cites `0xBB19E`
- [`Assets/Scripts/GameScene.cs:114`](../../../../../Assets/Scripts/GameScene.cs#L114) cites `0xBB0B0`
- [`Assets/Scripts/NativeGameCamera.cs:3`](../../../../../Assets/Scripts/NativeGameCamera.cs#L3) (by name)

## Callers and callees (direct code references)

Callers: [`playerClass::OnUpdate`](../../../functions/dossiers/playerClass/FUN-0BB6CC.md)

Callees: [`mainScene::ShowEggCount`](../../../functions/dossiers/mainScene/FUN-094E8E.md), [`mainScene::StopCreateEnemy`](../../../functions/dossiers/mainScene/FUN-09560E.md), [`mainScene::initEgg`](../../../functions/dossiers/mainScene/FUN-095DC0.md), [`mainScene::EggFull`](../../../functions/dossiers/mainScene/FUN-0974FA.md), [`mainScene::AddEggOnCar`](../../../functions/dossiers/mainScene/FUN-09760A.md), [`playerClass::Stand`](../../../functions/dossiers/playerClass/FUN-0B88B4.md), [`playerClass::Move`](../../../functions/dossiers/playerClass/FUN-0B8900.md), [`playerClass::UpdateBG`](../../../functions/dossiers/playerClass/FUN-0B8944.md), [`playerClass::OnFireUp`](../../../functions/dossiers/playerClass/FUN-0B8BC6.md), [`playerClass::getDownEgg`](../../../functions/dossiers/playerClass/FUN-0B96F8.md), `Triniti2D::Camera::GetViewport`, `Triniti2D::Camera::SetViewport`, `Triniti2D::GameObject::GetPosition`, `Triniti2D::GameObject::GetScene`, `Triniti2D::GameObject::Physics`, `Triniti2D::GameObject::SetPosition`, `Triniti2D::GamePhysics::SetLinearVelocity`, `Triniti2D::GamePhysics::WakeUp`, `Triniti2D::Scene::GetCallBack`, `Triniti2D::Scene::GetCamera`, `___addsf3vfp`, `___dynamic_cast`, `___gtsf2vfp`, `___ltsf2vfp`, `___mulsf3vfp`, `___subsf3vfp`

## Named data it references

- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A768` __ZN7DinoCap18g_width_ResolutionE_ptr
- `0x23B908` DinoCap::g_width_Resolution
- `0x22A0C0` __ZTIN9Triniti2D13SceneCallBackE_ptr
- `0x22A0C8` __ZTIN7DinoCap9mainSceneE_ptr
- `0x22C868` `typeinfo for'DinoCap::mainScene
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22A2DC` __ZN7DinoCap13g_prop_PlayerE_ptr
- `0x240474` DinoCap::g_prop_Player
- `0x22A2F0` __ZN7DinoCap12g_b_FireDownE_ptr
- `0x23B8C3` DinoCap::g_b_FireDown

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xBB054   f0b5       PUSH    {R4-R7,LR}
0xBB056   5e46       MOV     R6, R11
0xBB058   5546       MOV     R5, R10
0xBB05A   4446       MOV     R4, R8
0xBB05C   70b4       PUSH    {R4-R6}
0xBB05E   06af       ADD     R7, SP, #0x18
0xBB060   97b0       SUB     SP, SP, #0x5C
0xBB062   8046       MOV     R8, R0
0xBB064   4068       LDR     R0, [R0,#4]; this
0xBB066   0e1c       MOVS    R6, R1
0xBB068   b0f06cff   BL      __ZN9Triniti2D10GameObject11GetPositionEv; Triniti2D::GameObject::GetPosition(void)
0xBB06C   4168       LDR     R1, [R0,#4]
0xBB06E   0068       LDR     R0, [R0]
0xBB070   1590       STR     R0, [SP,#0x5C+var_8]
0xBB072   1691       STR     R1, [SP,#0x5C+var_4]
0xBB074   4146       MOV     R1, R8
0xBB076   4868       LDR     R0, [R1,#4]; this
0xBB078   b0f06aff   BL      __ZN9Triniti2D10GameObject8GetSceneEv; Triniti2D::GameObject::GetScene(void)
0xBB07C   8346       MOV     R11, R0
0xBB07E   baf0f2f8   BL      __ZN9Triniti2D5Scene9GetCameraEv; Triniti2D::Scene::GetCamera(void)
0xBB082   011c       MOVS    R1, R0
0xBB084   01a8       ADD     R0, SP, #0x5C+var_58; this
0xBB086   a6f08cfd   BL      __ZN9Triniti2D6Camera11GetViewportEv; Triniti2D::Camera::GetViewport(void)
0xBB08A   b24b       LDR     R3, =(__ZN7DinoCap8g_iphoneE_ptr - 0xBB090)
```

**Around `0xBB0B0` (cited by the Unity code)**

```
0xBB0A4   cb5c       LDRB    R3, [R1,R3]
0xBB0A6   002b       CMP     R3, #0
0xBB0A8   48d0       BEQ     loc_BB13C
0xBB0AA   6023       MOVS    R3, #0x60 ; '`'
0xBB0AC   cb5c       LDRB    R3, [R1,R3]
0xBB0AE   002b       CMP     R3, #0
0xBB0B0   13d0       BEQ     loc_BB0DA   <==
0xBB0B2   ab49       LDR     R1, =(__ZN7DinoCap18g_width_ResolutionE_ptr - 0xBB0BA)
0xBB0B4   ab48       LDR     R0, =0x3F0CCCCD
0xBB0B6   7944       ADD     R1, PC; __ZN7DinoCap18g_width_ResolutionE_ptr
0xBB0B8   0968       LDR     R1, [R1]; DinoCap::g_width_Resolution ...
0xBB0BA   0968       LDR     R1, [R1]; DinoCap::g_width_Resolution
0xBB0BC   2ef1b0ee   BLX     ___mulsf3vfp
```

**Around `0xBB19E` (cited by the Unity code)**

```
0xBB190   019d       LDR     R5, [SP,#0x5C+var_58]
0xBB192   5458       LDR     R4, [R2,R1]
0xBB194   281c       MOVS    R0, R5
0xBB196   211c       MOVS    R1, R4
0xBB198   2ef11eee   BLX     ___ltsf2vfp
0xBB19C   0028       CMP     R0, #0
0xBB19E   01d0       BEQ     loc_BB1A4   <==
0xBB1A0   0194       STR     R4, [SP,#0x5C+var_58]
0xBB1A2   11e0       B       loc_BB1C8
0xBB1A4   8c20       MOVS    R0, #0x8C
0xBB1A6   5346       MOV     R3, R10
0xBB1A8   039e       LDR     R6, [SP,#0x5C+var_50]
0xBB1AA   1c58       LDR     R4, [R3,R0]
```

**Around `0xBB24C` (cited by the Unity code)**

```
0xBB23C   2ef118ed   BLX     ___addsf3vfp
0xBB240   011c       MOVS    R1, R0
0xBB242   281c       MOVS    R0, R5
0xBB244   2ef1b0ed   BLX     ___gtsf2vfp
0xBB248   0028       CMP     R0, #0
0xBB24A   3dd0       BEQ     loc_BB2C8
0xBB24C   4e4b       LDR     R3, =0x291   <==
0xBB24E   4146       MOV     R1, R8
0xBB250   5a46       MOV     R2, R11
0xBB252   5046       MOV     R0, R10; this
0xBB254   ca54       STRB    R2, [R1,R3]
0xBB256   daf7daf9   BL      __ZN7DinoCap9mainScene15StopCreateEnemyEv; DinoCap::mainScene::StopCreateEnemy(void)
0xBB25A   4146       MOV     R1, R8
```

**Around `0xBB316` (cited by the Unity code)**

```
0xBB308   1093       STR     R3, [SP,#0x5C+var_1C]
0xBB30A   b2f062fa   BL      __ZN9Triniti2D11GamePhysics17SetLinearVelocityERKNS_7Vector2E; Triniti2D::GamePhysics::SetLinearVelocity(Triniti2D::Vector2 const&)
0xBB30E   03e1       B       loc_BB518
0xBB310   042b       CMP     R3, #4
0xBB312   00d0       BEQ     loc_BB316
0xBB314   cbe0       B       loc_BB4AE
0xBB316   8821       MOVS    R1, #0x88   <==
0xBB318   5346       MOV     R3, R10
0xBB31A   159c       LDR     R4, [SP,#0x5C+var_8]
0xBB31C   5d58       LDR     R5, [R3,R1]
0xBB31E   201c       MOVS    R0, R4
0xBB320   291c       MOVS    R1, R5
0xBB322   2ef15aed   BLX     ___ltsf2vfp
```

**Around `0xBB3C8` (cited by the Unity code)**

```
0xBB3B8   5068       LDR     R0, [R2,#4]; this
0xBB3BA   b0f0edfd   BL      __ZN9Triniti2D10GameObject7PhysicsEv; Triniti2D::GameObject::Physics(void)
0xBB3BE   0ba9       ADD     R1, SP, #0x5C+var_30
0xBB3C0   0b96       STR     R6, [SP,#0x5C+var_30]
0xBB3C2   0c96       STR     R6, [SP,#0x5C+var_2C]
0xBB3C4   b2f005fa   BL      __ZN9Triniti2D11GamePhysics17SetLinearVelocityERKNS_7Vector2E; Triniti2D::GamePhysics::SetLinearVelocity(Triniti2D::Vector2 const&)
0xBB3C8   564c       LDR     R4, =0x10D   <==
0xBB3CA   0021       MOVS    R1, #0; int
0xBB3CC   4046       MOV     R0, R8; this
0xBB3CE   0022       MOVS    R2, #0; bool
0xBB3D0   fdf7b8fa   BL      __ZN7DinoCap11playerClass8UpdateBGEib; DinoCap::playerClass::UpdateBG(int,bool)
0xBB3D4   5146       MOV     R1, R10
0xBB3D6   0b5d       LDRB    R3, [R1,R4]
```

**Around `0xBB430` (cited by the Unity code)**

```
0xBB424   002b       CMP     R3, #0
0xBB426   36dc       BGT     loc_BB496
0xBB428   404b       LDR     R3, =0x291
0xBB42A   4146       MOV     R1, R8
0xBB42C   5046       MOV     R0, R10; this
0xBB42E   a424       MOVS    R4, #0xA4
0xBB430   ce54       STRB    R6, [R1,R3]   <==
0xBB432   daf7ecf8   BL      __ZN7DinoCap9mainScene15StopCreateEnemyEv; DinoCap::mainScene::StopCreateEnemy(void)
0xBB436   4046       MOV     R0, R8; this
0xBB438   fef75ef9   BL      __ZN7DinoCap11playerClass10getDownEggEv; DinoCap::playerClass::getDownEgg(void)
0xBB43C   5046       MOV     R0, R10; this
0xBB43E   daf7bffc   BL      __ZN7DinoCap9mainScene7initEggEv; DinoCap::mainScene::initEgg(void)
0xBB442   5246       MOV     R2, R10
```

