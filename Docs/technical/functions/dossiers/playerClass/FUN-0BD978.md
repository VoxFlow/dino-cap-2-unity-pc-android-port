# playerClass::OnAnimationEnd

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0BD978` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xBD978` |
| RVA (EA - 0x1000) | `0xBC978` |
| File offset | `0xBC978` |
| Size | 1356 code bytes in 1 chunk(s), 535 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11playerClass14OnAnimationEndEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerClass::OnAnimationEnd(void)` |
| Bytes SHA-256 | `99eeda03c73ee9ab39927ef95e70759af65975b2dda4b655845f77ed29228c50` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameControls.cs:9`](../../../../../Assets/Scripts/GameControls.cs#L9) cites `0xBD978`
- [`Assets/Scripts/GameControls.cs:136`](../../../../../Assets/Scripts/GameControls.cs#L136) cites `0xBDB8C`
- [`Assets/Scripts/GameFire.cs:141`](../../../../../Assets/Scripts/GameFire.cs#L141) cites `0xBDE1E`
- [`Assets/Scripts/GameFx.cs:24`](../../../../../Assets/Scripts/GameFx.cs#L24) cites `0xBDC0A`
- [`Assets/Scripts/GameMission.cs:66`](../../../../../Assets/Scripts/GameMission.cs#L66) (by name)

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: [`CAObjCallBack::playAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00A064.md), `CAObjCallBack::setVisible`, [`CAObjCallBack::setAnimationAdv`](../../../functions/dossiers/CAObjCallBack/FUN-00A298.md), [`CAObjCallBack::setAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00AEA0.md), [`mainScene::GetEffect`](../../../functions/dossiers/mainScene/FUN-0956B6.md), [`mainScene::GameOverStart`](../../../functions/dossiers/mainScene/FUN-0A6F70.md), [`playerClass::Stand`](../../../functions/dossiers/playerClass/FUN-0B88B4.md), [`playerClass::UpdateBG`](../../../functions/dossiers/playerClass/FUN-0B8944.md), [`playerClass::UpdateControl`](../../../functions/dossiers/playerClass/FUN-0B8A60.md), [`playerClass::OnFireDown`](../../../functions/dossiers/playerClass/FUN-0B9990.md), [`playerClass::OnHitDown`](../../../functions/dossiers/playerClass/FUN-0BD7C8.md), `subWeaponClass::~subWeaponClass`, `Triniti2D::GameObject::Enable`, `Triniti2D::GameObject::GetPosition`, `Triniti2D::GameObject::GetScene`, `Triniti2D::GameObject::GetCallBack`, `Triniti2D::GameObject::Physics`, `Triniti2D::GameObject::SetPosition`, `Triniti2D::GameObject::SetTimer`, `Triniti2D::GamePhysics::SetLinearVelocity`, `Triniti2D::GamePhysics::WakeUp`, `Triniti2D::Scene::GetCallBack`, `___switch32`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `___dynamic_cast`, `___mulsf3vfp`, `___subsf3vfp`

## Strings it references

- `0x208438` "rollEffectAnimation"
- `0x201338` "roll"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x226FC4` GCC_except_table20_2
- `0x22A2DC` __ZN7DinoCap13g_prop_PlayerE_ptr
- `0x240474` DinoCap::g_prop_Player
- `0x22A640` __ZN7DinoCap11g_b_HitDownE_ptr
- `0x23B8C2` DinoCap::g_b_HitDown
- `0x22A24C` __ZN7DinoCap15g_player_vectorE_ptr
- `0x2404AC` DinoCap::g_player_vector
- `0x22A2B8` __ZN7DinoCap13g_player_typeE_ptr
- `0x23B86C` DinoCap::g_player_type
- `0x22A0C4` __ZN7DinoCap16g_manager_WeaponE_ptr
- `0x2404B8` DinoCap::g_manager_Weapon
- `0x22A7E4` __ZN7DinoCap11weaponClassaSERKS0__ptr
- `0x22A2F0` __ZN7DinoCap12g_b_FireDownE_ptr
- `0x23B8C3` DinoCap::g_b_FireDown
- `0x22A0C0` __ZTIN9Triniti2D13SceneCallBackE_ptr
- `0x22A0C8` __ZTIN7DinoCap9mainSceneE_ptr
- `0x22C868` `typeinfo for'DinoCap::mainScene
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22A0A8` __ZTIN9Triniti2D18GameObjectCallBackE_ptr
- `0x22A260` __ZTIN7DinoCap11effectClassE_ptr
- `0x22C814` `typeinfo for'DinoCap::effectClass
- `0x22A3C4` __ZN7DinoCap15g_ca_p4_RollEffE_ptr
- `0x23EDE4` DinoCap::g_ca_p4_RollEff
- `0x22A738` __ZN7DinoCap12g_b_RollDownE_ptr
- `0x23B8C4` DinoCap::g_b_RollDown

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xBD978   f0b5       PUSH    {R4-R7,LR}
0xBD97A   5e46       MOV     R6, R11
0xBD97C   5546       MOV     R5, R10
0xBD97E   4446       MOV     R4, R8
0xBD980   70b4       PUSH    {R4-R6}
0xBD982   2af1aeea   BLX     ___save_vfp_d8_d15_regs
0xBD986   16af       ADD     R7, SP, #arg_38
0xBD988   a8b0       SUB     SP, SP, #0xA0
0xBD98A   e44b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xBD994)
0xBD98C   0a90       STR     R0, [SP,#0xA0+var_78]
0xBD98E   0122       MOVS    R2, #1
0xBD990   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xBD992   6846       MOV     R0, SP
0xBD994   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xBD996   1890       STR     R0, [SP,#0xA0+var_40]
0xBD998   0ea8       ADD     R0, SP, #0xA0+fctx; lpfctx
0xBD99A   1493       STR     R3, [SP,#0xA0+fctx.personality]
0xBD99C   e04b       LDR     R3, =(GCC_except_table20_2 - 0xBD9A4)
0xBD99E   1697       STR     R7, [SP,#0xA0+fctx.jbuf]
0xBD9A0   7b44       ADD     R3, PC; GCC_except_table20_2
0xBD9A2   1593       STR     R3, [SP,#0xA0+fctx.lsda]
0xBD9A4   df4b       LDR     R3, =(loc_BDAD0 - 0xBD9AA)
0xBD9A6   7b44       ADD     R3, PC; loc_BDAD0
0xBD9A8   1343       ORRS    R3, R2
```

**Around `0xBDB8C` (cited by the Unity code)**

```
0xBDB7C   0f90       STR     R0, [SP,#0xA0+fctx.call_site]
0xBDB7E   0a98       LDR     R0, [SP,#0xA0+var_78]; this
0xBDB80   fbf706ff   BL      __ZN7DinoCap11playerClass10OnFireDownEv; DinoCap::playerClass::OnFireDown(void)
0xBDB84   93e1       B       def_BD9E8; jumptable 000BD9E8 default case, cases 5,8
0xBDB86   0a99       LDR     R1, [SP,#0xA0+var_78]; jumptable 000BD9E8 case 4
0xBDB88   a1239b00   MOVS    R3, #0x284
0xBDB8C   cb58       LDR     R3, [R1,R3]   <==
0xBDB8E   0793       STR     R3, [SP,#0xA0+var_84]
0xBDB90   002b       CMP     R3, #0
0xBDB92   03d0       BEQ     loc_BDB9C
0xBDB94   022b       CMP     R3, #2
0xBDB96   00d0       BEQ     loc_BDB9A
0xBDB98   89e1       B       def_BD9E8; jumptable 000BD9E8 default case, cases 5,8
```

**Around `0xBDC0A` (cited by the Unity code)**

```
0xBDBFC   0968       LDR     R1, [R1]; lpstype
0xBDBFE   1268       LDR     R2, [R2]; lpdtype
0xBDC00   2cf16ce8   BLX     ___dynamic_cast
0xBDC04   0123       MOVS    R3, #1
0xBDC06   5b42       NEGS    R3, R3
0xBDC08   0f93       STR     R3, [SP,#0xA0+fctx.call_site]
0xBDC0A   d7f754fd   BL      __ZN7DinoCap9mainScene9GetEffectEv; DinoCap::mainScene::GetEffect(void)   <==
0xBDC0E   0121       MOVS    R1, #1; bool
0xBDC10   0c90       STR     R0, [SP,#0xA0+var_70]
0xBDC12   aef093f9   BL      __ZN9Triniti2D10GameObject6EnableEb; Triniti2D::GameObject::Enable(bool)
0xBDC16   0a9c       LDR     R4, [SP,#0xA0+var_78]
0xBDC18   6068       LDR     R0, [R4,#4]; this
0xBDC1A   aef093f9   BL      __ZN9Triniti2D10GameObject11GetPositionEv; Triniti2D::GameObject::GetPosition(void)
```

**Around `0xBDE1E` (cited by the Unity code)**

```
0xBDE12   5b42       NEGS    R3, R3
0xBDE14   0093       STR     R3, [SP,#0xA0+var_A0]; unsigned int
0xBDE16   0f93       STR     R3, [SP,#0xA0+fctx.call_site]
0xBDE18   0021       MOVS    R1, #0; unsigned int
0xBDE1A   0622       MOVS    R2, #6; int
0xBDE1C   2e4b       LDR     R3, flt_BDED8; float
0xBDE1E   4cf721f9   BL      __ZN7DinoCap13CAObjCallBack13playAnimationEmifm; DinoCap::CAObjCallBack::playAnimation(ulong,int,float,ulong)   <==
0xBDE22   0123       MOVS    R3, #1
0xBDE24   5b42       NEGS    R3, R3
0xBDE26   0093       STR     R3, [SP,#0xA0+var_A0]; unsigned int
0xBDE28   0f93       STR     R3, [SP,#0xA0+fctx.call_site]
0xBDE2A   0f22       MOVS    R2, #0xF; int
0xBDE2C   2a4b       LDR     R3, flt_BDED8; float
```

