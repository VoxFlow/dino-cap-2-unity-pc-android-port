# playerClass::BeAttackByBoss

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0BD3C8` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xBD3C8` |
| RVA (EA - 0x1000) | `0xBC3C8` |
| File offset | `0xBC3C8` |
| Size | 586 code bytes in 1 chunk(s), 249 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11playerClass14BeAttackByBossEPN9Triniti2D10GameObjectE` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerClass::BeAttackByBoss(Triniti2D::GameObject *)` |
| Bytes SHA-256 | `d4966986d599ca42406b5007bed3b4f4caff9be015c55b6ac0c282b1aa8a04d8` |
| Direct code callers | 4 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameBoss.cs:246`](../../../../../Assets/Scripts/GameBoss.cs#L246) cites `0xBD3C8`
- [`Assets/Scripts/GameCombat.cs:630`](../../../../../Assets/Scripts/GameCombat.cs#L630) cites `0xBD3C8`
- [`Assets/Scripts/GameEgg.cs:202`](../../../../../Assets/Scripts/GameEgg.cs#L202) cites `0xBD4CA`

## Callers and callees (direct code references)

Callers: [`effectClassN::OnUpdate`](../../../functions/dossiers/effectClassN/FUN-11FBDA.md), [`bossN::HouCannotTouch`](../../../functions/dossiers/bossN/FUN-1206FC.md), [`bossN::OnUpdate`](../../../functions/dossiers/bossN/FUN-121A98.md), `sub_1228A0`

Callees: [`CAObjCallBack::playAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00A064.md), [`CAObjCallBack::setAnimationAdv`](../../../functions/dossiers/CAObjCallBack/FUN-00A298.md), [`CAObjCallBack::setAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00AEA0.md), [`PlaySound`](../../../functions/dossiers/DinoCap/FUN-01A448.md), [`mainScene::UpdateHP`](../../../functions/dossiers/mainScene/FUN-095228.md), [`mainScene::initEgg`](../../../functions/dossiers/mainScene/FUN-095DC0.md), [`mainScene::BrokenEgg`](../../../functions/dossiers/mainScene/FUN-09725A.md), [`playerClass::UpdateBG`](../../../functions/dossiers/playerClass/FUN-0B8944.md), [`playerClass::OnFireUp`](../../../functions/dossiers/playerClass/FUN-0B8BC6.md), [`playerClass::getDownEgg`](../../../functions/dossiers/playerClass/FUN-0B96F8.md), [`playerClass::Die`](../../../functions/dossiers/playerClass/FUN-0BCDEA.md), `Triniti2D::GameObject::GetPosition`, `Triniti2D::GameObject::GetScene`, `Triniti2D::GameObject::GetCallBack`, `Triniti2D::GameObject::Physics`, `Triniti2D::GameObject::SetTimer`, `Triniti2D::GamePhysics::SetLinearVelocity`, `Triniti2D::GamePhysics::WakeUp`, `Triniti2D::Scene::GetCallBack`, `std::string::_M_mutate`, `___addsf3vfp`, `___dynamic_cast`, `___fixsfsivfp`, `___floatsisfvfp`, `___lesf2vfp`, `___ltsf2vfp`, `___mulsf3vfp`, `___subsf3vfp`

## Strings it references

- `0x208418` "behitAnim"
- `0x2083C0` "Wudi"
- `0x204800` "Audio_RoleAudio"

## Named data it references

- `0x22A7BC` __ZN7DinoCap6g_bossE_ptr
- `0x240468` DinoCap::g_boss
- `0x22A2DC` __ZN7DinoCap13g_prop_PlayerE_ptr
- `0x240474` DinoCap::g_prop_Player
- `0x22A7C8` __ZN7DinoCap11g_boss_lineE_ptr
- `0x23B870` DinoCap::g_boss_line
- `0x22A75C` __ZN7DinoCap16g_boss_line_typeE_ptr
- `0x23B874` DinoCap::g_boss_line_type
- `0x22A2B8` __ZN7DinoCap13g_player_typeE_ptr
- `0x23B86C` DinoCap::g_player_type
- `0x22A0C0` __ZTIN9Triniti2D13SceneCallBackE_ptr
- `0x22A0C8` __ZTIN7DinoCap9mainSceneE_ptr
- `0x22C868` `typeinfo for'DinoCap::mainScene
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22A794` __ZN7DinoCap15g_jigsaw4_countE_ptr
- `0x23E1D4` DinoCap::g_jigsaw4_count

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xBD3C8   f0b5       PUSH    {R4-R7,LR}
0xBD3CA   5646       MOV     R6, R10
0xBD3CC   4546       MOV     R5, R8
0xBD3CE   60b4       PUSH    {R5,R6}
0xBD3D0   05af       ADD     R7, SP, #0x14
0xBD3D2   85b0       SUB     SP, SP, #0x14
0xBD3D4   8f4b       LDR     R3, =0x292
0xBD3D6   061c       MOVS    R6, R0
0xBD3D8   8a46       MOV     R10, R1
0xBD3DA   c55c       LDRB    R5, [R0,R3]
0xBD3DC   002d       CMP     R5, #0
0xBD3DE   00d0       BEQ     loc_BD3E2
0xBD3E0   12e1       B       loc_BD608
0xBD3E2   a0239b00   MOVS    R3, #0x280
0xBD3E6   c358       LDR     R3, [R0,R3]
0xBD3E8   043b       SUBS    R3, #4
0xBD3EA   032b       CMP     R3, #3
0xBD3EC   00d8       BHI     loc_BD3F0
0xBD3EE   0be1       B       loc_BD608
0xBD3F0   a424a400   MOVS    R4, #0x290
0xBD3F4   035d       LDRB    R3, [R0,R4]
0xBD3F6   002b       CMP     R3, #0
0xBD3F8   0bd0       BEQ     loc_BD412
0xBD3FA   fbf7e4fb   BL      __ZN7DinoCap11playerClass8OnFireUpEv; DinoCap::playerClass::OnFireUp(void)
```

**Around `0xBD4CA` (cited by the Unity code)**

```
0xBD4BA   281c       MOVS    R0, R5; this
0xBD4BC   2360       STR     R3, [R4]; DinoCap::g_prop_Player
0xBD4BE   d7f7b3fe   BL      __ZN7DinoCap9mainScene8UpdateHPEv; DinoCap::mainScene::UpdateHP(void)
0xBD4C2   301c       MOVS    R0, R6; this
0xBD4C4   fff791fc   BL      __ZN7DinoCap11playerClass3DieEv; DinoCap::playerClass::Die(void)
0xBD4C8   9ee0       B       loc_BD608
0xBD4CA   5c4a       LDR     R2, =0x10D   <==
0xBD4CC   ab5c       LDRB    R3, [R5,R2]
0xBD4CE   002b       CMP     R3, #0
0xBD4D0   09d0       BEQ     loc_BD4E6
0xBD4D2   a854       STRB    R0, [R5,R2]
0xBD4D4   301c       MOVS    R0, R6; this
0xBD4D6   fcf70ff9   BL      __ZN7DinoCap11playerClass10getDownEggEv; DinoCap::playerClass::getDownEgg(void)
```

