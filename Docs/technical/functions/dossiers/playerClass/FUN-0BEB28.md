# playerClass::PenHuoQ

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0BEB28` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xBEB28` |
| RVA (EA - 0x1000) | `0xBDB28` |
| File offset | `0xBDB28` |
| Size | 562 code bytes in 1 chunk(s), 249 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11playerClass7PenHuoQEi` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerClass::PenHuoQ(int)` |
| Bytes SHA-256 | `4c818a5524b9ee03a5bf6dfac66ac861aa4d55d23afd2ee6c79e3a2e5d06a4e3` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameBeam.cs:6`](../../../../../Assets/Scripts/GameBeam.cs#L6) cites `0xBEB28`
- [`Assets/Scripts/GameBeam.cs:141`](../../../../../Assets/Scripts/GameBeam.cs#L141) cites `0xBEB28`
- [`Assets/Scripts/GameBeam.cs:172`](../../../../../Assets/Scripts/GameBeam.cs#L172) cites `0xBEBFA`
- [`Assets/Scripts/GameScene.cs:347`](../../../../../Assets/Scripts/GameScene.cs#L347) cites `0xBEBFA`

## Callers and callees (direct code references)

Callers: [`playerClass::OnKeyFrameAdv`](../../../functions/dossiers/playerClass/FUN-0C076A.md)

Callees: [`CAObjCallBack::playAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00A064.md), [`CAObjCallBack::getSprite`](../../../functions/dossiers/CAObjCallBack/FUN-00A3BA.md), [`CAObjCallBack::getSpritePos`](../../../functions/dossiers/CAObjCallBack/FUN-00A4D0.md), [`CAObjCallBack::setAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00AEA0.md), [`PlaySound`](../../../functions/dossiers/DinoCap/FUN-01A448.md), [`playerClass::Damage`](../../../functions/dossiers/playerClass/FUN-0BE65C.md), `Triniti2D::GameContainer::CreateGameObject`, `Triniti2D::GameObject::Enable`, `Triniti2D::GameObject::Enable`, `Triniti2D::GameObject::GetSize`, `Triniti2D::GameObject::GetCallBack`, `Triniti2D::GameObject::Container`, `Triniti2D::GameObject::SetPosition`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___dynamic_cast`, `___mulsf3vfp`

## Strings it references

- `0x2012E0` "yyj5"
- `0x201DE8` "effectClass"
- `0x205318` "huoyanAnim"
- `0x204898` "Attack_F2Audio"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x226FEA` GCC_except_table23_2
- `0x22A0A8` __ZTIN9Triniti2D18GameObjectCallBackE_ptr
- `0x22A260` __ZTIN7DinoCap11effectClassE_ptr
- `0x22C814` `typeinfo for'DinoCap::effectClass
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22A41C` __ZN7DinoCap15g_ca_huoyanDataE_ptr
- `0x23EF18` DinoCap::g_ca_huoyanData
- `0x22A2F0` __ZN7DinoCap12g_b_FireDownE_ptr
- `0x23B8C3` DinoCap::g_b_FireDown

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xBEB28   f0b5       PUSH    {R4-R7,LR}
0xBEB2A   5e46       MOV     R6, R11
0xBEB2C   5546       MOV     R5, R10
0xBEB2E   4446       MOV     R4, R8
0xBEB30   70b4       PUSH    {R4-R6}
0xBEB32   29f1d6e9   BLX     ___save_vfp_d8_d15_regs
0xBEB36   16af       ADD     R7, SP, #arg_38
0xBEB38   a3b0       SUB     SP, SP, #0x8C
0xBEB3A   884b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xBEB44)
0xBEB3C   0122       MOVS    R2, #1
0xBEB3E   6946       MOV     R1, SP
0xBEB40   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xBEB42   0b90       STR     R0, [SP,#0x8C+var_60]
0xBEB44   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xBEB46   12a8       ADD     R0, SP, #0x8C+fctx; lpfctx
0xBEB48   1c91       STR     R1, [SP,#0x8C+var_1C]
0xBEB4A   1893       STR     R3, [SP,#0x8C+fctx.personality]
0xBEB4C   844b       LDR     R3, =(GCC_except_table23_2 - 0xBEB54)
0xBEB4E   1a97       STR     R7, [SP,#0x8C+fctx.jbuf]
0xBEB50   7b44       ADD     R3, PC; GCC_except_table23_2
0xBEB52   1993       STR     R3, [SP,#0x8C+fctx.lsda]
0xBEB54   834b       LDR     R3, =(loc_BEC10 - 0xBEB5A)
0xBEB56   7b44       ADD     R3, PC; loc_BEC10
0xBEB58   1343       ORRS    R3, R2
```

**Around `0xBEBFA` (cited by the Unity code)**

```
0xBEBEE   0491       STR     R1, [SP,#0x8C+var_7C]; bool
0xBEBF0   1392       STR     R2, [SP,#0x8C+fctx.call_site]
0xBEBF2   0e98       LDR     R0, [SP,#0x8C+var_54]; this
0xBEBF4   21a9       ADD     R1, SP, #0x8C+var_8; std::string *
0xBEBF6   6122       MOVS    R2, #0x61 ; 'a'; int
0xBEBF8   0023       MOVS    R3, #0; bool
0xBEBFA   acf0d1fd   BL      __ZN9Triniti2D13GameContainer16CreateGameObjectERKSsibbbbbb; Triniti2D::GameContainer::CreateGameObject(std::string const&,int,bool,bool,bool,bool,bool,bool)   <==
0xBEBFE   0b9b       LDR     R3, [SP,#0x8C+var_60]
0xBEC00   0999       LDR     R1, [SP,#0x8C+var_68]
0xBEC02   0222       MOVS    R2, #2
0xBEC04   5850       STR     R0, [R3,R1]
0xBEC06   1392       STR     R2, [SP,#0x8C+fctx.call_site]
0xBEC08   21a8       ADD     R0, SP, #0x8C+var_8; this
```

