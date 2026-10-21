# playerClass::Qianghuo

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0B8C90` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xB8C90` |
| RVA (EA - 0x1000) | `0xB7C90` |
| File offset | `0xB7C90` |
| Size | 1614 code bytes in 1 chunk(s), 569 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11playerClass8QianghuoERKN9Triniti2D7Vector2Ef` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerClass::Qianghuo(Triniti2D::Vector2 const&,float)` |
| Bytes SHA-256 | `f380c14093c951e1d1317e61f63cdb3aeadf8334015824dc9db90b2d2dba14d1` |
| Direct code callers | 2 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.fx.effect_rotation.r118v` (PROVEN, NATIVE): CAObjCallBack::OnUpdate adds the GameObject rotation to each (flip-signed) part rotation, positions unrotated. Qianghuo 0xb9230 sets +-0.1 rad (0x3DCCCCCD, + when flipped); LevelUpEffect sets 0; every other GetEffect user inherits the pooled object's rotation and flip. FALSIFIES r117b 'no effect rotation'.

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameFire.cs:148`](../../../../../Assets/Scripts/GameFire.cs#L148) cites `0xB8C90`
- [`Assets/Scripts/GameFire.cs:187`](../../../../../Assets/Scripts/GameFire.cs#L187) cites `0xB8C90`
- [`Assets/Scripts/GameFire.cs:196`](../../../../../Assets/Scripts/GameFire.cs#L196) cites `0xB8C90`
- [`Assets/Scripts/GameFire.cs:209`](../../../../../Assets/Scripts/GameFire.cs#L209) cites `0xB9212`
- [`Assets/Scripts/GameFx.cs:63`](../../../../../Assets/Scripts/GameFx.cs#L63) cites `0xB9230`

## Callers and callees (direct code references)

Callers: [`playerClass::LiuDanQ`](../../../functions/dossiers/playerClass/FUN-0BFE94.md), [`playerClass::KaiQiang`](../../../functions/dossiers/playerClass/FUN-0C0336.md)

Callees: [`CAObjCallBack::playAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00A064.md), [`CAObjCallBack::setAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00AEA0.md), [`mainScene::GetEffect`](../../../functions/dossiers/mainScene/FUN-0956B6.md), `subWeaponClass::~subWeaponClass`, `Triniti2D::GameObject::Enable`, `Triniti2D::GameObject::GetScene`, `Triniti2D::GameObject::GetCallBack`, `Triniti2D::GameObject::SetRotation`, `Triniti2D::GameObject::SetPosition`, `Triniti2D::Scene::GetCallBack`, `___switch32`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `___addsf3vfp`, `___dynamic_cast`, `___fixsfsivfp`, `___floatsisfvfp`, `___mulsf3vfp`, `___subsf3vfp`

## Strings it references

- `0x208280` "qhEffectAnimation"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x226EFE` GCC_except_table3_7
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A24C` __ZN7DinoCap15g_player_vectorE_ptr
- `0x2404AC` DinoCap::g_player_vector
- `0x22A2B8` __ZN7DinoCap13g_player_typeE_ptr
- `0x23B86C` DinoCap::g_player_type
- `0x22A74C` __ZN7DinoCap12g_b_LeftDownE_ptr
- `0x23B8C0` DinoCap::g_b_LeftDown
- `0x22A6EC` __ZN7DinoCap13g_b_RightDownE_ptr
- `0x23B8C1` DinoCap::g_b_RightDown
- `0x22A0C0` __ZTIN9Triniti2D13SceneCallBackE_ptr
- `0x22A0C8` __ZTIN7DinoCap9mainSceneE_ptr
- `0x22C868` `typeinfo for'DinoCap::mainScene
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22A0A8` __ZTIN9Triniti2D18GameObjectCallBackE_ptr
- `0x22A260` __ZTIN7DinoCap11effectClassE_ptr
- `0x22C814` `typeinfo for'DinoCap::effectClass
- `0x22A3DC` __ZN7DinoCap14g_ca_qhEffDataE_ptr
- `0x23EE00` DinoCap::g_ca_qhEffData

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xB8C90   f0b5       PUSH    {R4-R7,LR}
0xB8C92   5e46       MOV     R6, R11
0xB8C94   5546       MOV     R5, R10
0xB8C96   4446       MOV     R4, R8
0xB8C98   70b4       PUSH    {R4-R6}
0xB8C9A   2ff122e9   BLX     ___save_vfp_d8_d15_regs
0xB8C9E   16af       ADD     R7, SP, #arg_38
0xB8CA0   a3b0       SUB     SP, SP, #0x8C
0xB8CA2   e44b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xB8CAC)
0xB8CA4   0490       STR     R0, [SP,#0x8C+var_7C]
0xB8CA6   0292       STR     R2, [SP,#0x8C+var_84]
0xB8CA8   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xB8CAA   6846       MOV     R0, SP
0xB8CAC   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xB8CAE   0122       MOVS    R2, #1
0xB8CB0   1790       STR     R0, [SP,#0x8C+var_30]
0xB8CB2   1393       STR     R3, [SP,#0x8C+fctx.personality]
0xB8CB4   e04b       LDR     R3, =(GCC_except_table3_7 - 0xB8CBE)
0xB8CB6   0da8       ADD     R0, SP, #0x8C+fctx; lpfctx
0xB8CB8   0391       STR     R1, [SP,#0x8C+var_80]
0xB8CBA   7b44       ADD     R3, PC; GCC_except_table3_7
0xB8CBC   1493       STR     R3, [SP,#0x8C+fctx.lsda]
0xB8CBE   df4b       LDR     R3, =(loc_B92A2 - 0xB8CC6)
0xB8CC0   1597       STR     R7, [SP,#0x8C+fctx.jbuf]
```

**Around `0xB9212` (cited by the Unity code)**

```
0xB9204   21a9       ADD     R1, SP, #0x8C+var_8
0xB9206   b2f01eff   BL      __ZN9Triniti2D10GameObject11SetPositionERKNS_7Vector2E; Triniti2D::GameObject::SetPosition(Triniti2D::Vector2 const&)
0xB920A   049e       LDR     R6, [SP,#0x8C+var_7C]
0xB920C   6023       MOVS    R3, #0x60 ; '`'
0xB920E   f35c       LDRB    R3, [R6,R3]
0xB9210   002b       CMP     R3, #0
0xB9212   05d0       BEQ     loc_B9220   <==
0xB9214   3f49       LDR     R1, =0x3DCCCCCD
0xB9216   0298       LDR     R0, [SP,#0x8C+var_84]
0xB9218   30f12aed   BLX     ___addsf3vfp
0xB921C   011c       MOVS    R1, R0
0xB921E   06e0       B       loc_B922E
0xB9220   3c49       LDR     R1, =0x3DCCCCCD
```

**Around `0xB9230` (cited by the Unity code)**

```
0xB9222   0298       LDR     R0, [SP,#0x8C+var_84]
0xB9224   30f114ee   BLX     ___subsf3vfp
0xB9228   011c       MOVS    R1, R0; float
0xB922A   0120       MOVS    R0, #1
0xB922C   0e90       STR     R0, [SP,#0x8C+fctx.call_site]
0xB922E   0898       LDR     R0, [SP,#0x8C+var_6C]; this
0xB9230   b2f0e3fe   BL      __ZN9Triniti2D10GameObject11SetRotationEf; Triniti2D::GameObject::SetRotation(float)   <==
0xB9234   0123       MOVS    R3, #1
0xB9236   0e93       STR     R3, [SP,#0x8C+fctx.call_site]
0xB9238   0898       LDR     R0, [SP,#0x8C+var_6C]; this
0xB923A   b2f08bfe   BL      __ZN9Triniti2D10GameObject11GetCallBackEv; Triniti2D::GameObject::GetCallBack(void)
0xB923E   0028       CMP     R0, #0
0xB9240   02d1       BNE     loc_B9248
```

