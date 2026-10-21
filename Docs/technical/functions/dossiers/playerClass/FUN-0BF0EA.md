# playerClass::YuanLi

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0BF0EA` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xBF0EA` |
| RVA (EA - 0x1000) | `0xBE0EA` |
| File offset | `0xBE0EA` |
| Size | 716 code bytes in 1 chunk(s), 319 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11playerClass6YuanLiEi` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerClass::YuanLi(int)` |
| Bytes SHA-256 | `663c6b24bb17a26236a47e3fd376dc6b2f8a53aceb8968ff5e055768fc47b557` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.engine.yuanli_mount_inert.r118z4` (PROVEN, NATIVE): playerClass::YuanLi 0xbf0ea mounts its effect to the hero on the first frame-11 call (offset (0, 17h) - heroPos, 0x41880000 = 17.0). The effect is created with only the container flag (no GameSprite; GameObject::Initialize 0x16c252 flag order sprite/physics/particle/pointline/container/text), effectClass::OnUpdate 0x167a6 mode 8 resets its position to the com2 point before CAObjCallBack::OnUpdate places the parts (ow...

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameBeam.cs:172`](../../../../../Assets/Scripts/GameBeam.cs#L172) cites `0xBF198`
- [`Assets/Scripts/GameBeam.cs:291`](../../../../../Assets/Scripts/GameBeam.cs#L291) cites `0xBF0EA`
- [`Assets/Scripts/GameScene.cs:347`](../../../../../Assets/Scripts/GameScene.cs#L347) cites `0xBF198`

## Callers and callees (direct code references)

Callers: [`playerClass::OnKeyFrame`](../../../functions/dossiers/playerClass/FUN-0BF406.md)

Callees: [`CAObjCallBack::playAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00A064.md), [`CAObjCallBack::getSprite`](../../../functions/dossiers/CAObjCallBack/FUN-00A3BA.md), [`CAObjCallBack::setAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00AEA0.md), [`PlaySound`](../../../functions/dossiers/DinoCap/FUN-01A448.md), [`playerClass::Damage`](../../../functions/dossiers/playerClass/FUN-0BE65C.md), `Triniti2D::GameContainer::CreateGameObject`, `Triniti2D::GameObject::Enable`, `Triniti2D::GameObject::Enable`, `Triniti2D::GameObject::GetPosition`, `Triniti2D::GameObject::GetSize`, `Triniti2D::GameObject::GetScene`, `Triniti2D::GameObject::GetCallBack`, `Triniti2D::GameObject::Container`, `Triniti2D::GameObject::SetPosition`, `Triniti2D::Scene::Mount`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___dynamic_cast`, `___mulsf3vfp`, `___subsf3vfp`

## Strings it references

- `0x201394` "com2"
- `0x201DE8` "effectClass"
- `0x208464` "YuanliAnim"
- `0x204888` "Attack_F3Audio"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x226FFA` GCC_except_table25_1
- `0x22A0A8` __ZTIN9Triniti2D18GameObjectCallBackE_ptr
- `0x22A260` __ZTIN7DinoCap11effectClassE_ptr
- `0x22C814` `typeinfo for'DinoCap::effectClass
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22A4A0` __ZN7DinoCap15g_ca_yuanliDataE_ptr
- `0x23EF50` DinoCap::g_ca_yuanliData
- `0x22A2F0` __ZN7DinoCap12g_b_FireDownE_ptr
- `0x23B8C3` DinoCap::g_b_FireDown

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xBF0EA   f0b5       PUSH    {R4-R7,LR}
0xBF0EC   5e46       MOV     R6, R11
0xBF0EE   5546       MOV     R5, R10
0xBF0F0   4446       MOV     R4, R8
0xBF0F2   70b4       PUSH    {R4-R6}
0xBF0F4   28f1f4ee   BLX     ___save_vfp_d8_d15_regs
0xBF0F8   16af       ADD     R7, SP, #arg_38
0xBF0FA   a4b0       SUB     SP, SP, #0x90
0xBF0FC   ae4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xBF106)
0xBF0FE   0c90       STR     R0, [SP,#0x90+var_60]
0xBF100   0b91       STR     R1, [SP,#0x90+var_64]
0xBF102   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xBF104   0121       MOVS    R1, #1
0xBF106   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xBF108   6846       MOV     R0, SP
0xBF10A   1d90       STR     R0, [SP,#0x90+var_1C]
0xBF10C   1993       STR     R3, [SP,#0x90+fctx.personality]
0xBF10E   ab4b       LDR     R3, =(GCC_except_table25_1 - 0xBF118)
0xBF110   13a8       ADD     R0, SP, #0x90+fctx; lpfctx
0xBF112   1b97       STR     R7, [SP,#0x90+fctx.jbuf]
0xBF114   7b44       ADD     R3, PC; GCC_except_table25_1
0xBF116   1a93       STR     R3, [SP,#0x90+fctx.lsda]
0xBF118   a94b       LDR     R3, =(loc_BF1AE - 0xBF11E)
0xBF11A   7b44       ADD     R3, PC; loc_BF1AE
```

**Around `0xBF198` (cited by the Unity code)**

```
0xBF18C   0493       STR     R3, [SP,#0x90+var_80]; bool
0xBF18E   1490       STR     R0, [SP,#0x90+fctx.call_site]
0xBF190   22a9       ADD     R1, SP, #0x90+var_8; std::string *
0xBF192   0d98       LDR     R0, [SP,#0x90+var_5C]; this
0xBF194   6122       MOVS    R2, #0x61 ; 'a'; int
0xBF196   0023       MOVS    R3, #0; bool
0xBF198   acf002fb   BL      __ZN9Triniti2D13GameContainer16CreateGameObjectERKSsibbbbbb; Triniti2D::GameContainer::CreateGameObject(std::string const&,int,bool,bool,bool,bool,bool,bool)   <==
0xBF19C   0c99       LDR     R1, [SP,#0x90+var_60]
0xBF19E   0a9a       LDR     R2, [SP,#0x90+var_68]
0xBF1A0   0223       MOVS    R3, #2
0xBF1A2   8850       STR     R0, [R1,R2]
0xBF1A4   1493       STR     R3, [SP,#0x90+fctx.call_site]
0xBF1A6   22a8       ADD     R0, SP, #0x90+var_8; this
```

