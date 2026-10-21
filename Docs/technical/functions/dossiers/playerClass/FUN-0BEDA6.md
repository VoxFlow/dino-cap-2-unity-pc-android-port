# playerClass::JiGuangQ

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0BEDA6` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xBEDA6` |
| RVA (EA - 0x1000) | `0xBDDA6` |
| File offset | `0xBDDA6` |
| Size | 754 code bytes in 1 chunk(s), 336 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11playerClass8JiGuangQEi` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerClass::JiGuangQ(int)` |
| Bytes SHA-256 | `a06cfe0376f45d0dfad7a5e38473633c9c65dfa83f10b010f7b2554c472ad8aa` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameBeam.cs:6`](../../../../../Assets/Scripts/GameBeam.cs#L6) cites `0xBEDA6`
- [`Assets/Scripts/GameBeam.cs:156`](../../../../../Assets/Scripts/GameBeam.cs#L156) cites `0xBEDA6`
- [`Assets/Scripts/GameBeam.cs:172`](../../../../../Assets/Scripts/GameBeam.cs#L172) cites `0xBEE7A`
- [`Assets/Scripts/GameScene.cs:347`](../../../../../Assets/Scripts/GameScene.cs#L347) cites `0xBEE7A`

## Callers and callees (direct code references)

Callers: [`playerClass::OnKeyFrameAdv`](../../../functions/dossiers/playerClass/FUN-0C076A.md)

Callees: [`CAObjCallBack::playAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00A064.md), [`CAObjCallBack::getSprite`](../../../functions/dossiers/CAObjCallBack/FUN-00A3BA.md), [`CAObjCallBack::getSpritePos`](../../../functions/dossiers/CAObjCallBack/FUN-00A4D0.md), [`CAObjCallBack::setAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00AEA0.md), [`PlaySound`](../../../functions/dossiers/DinoCap/FUN-01A448.md), [`playerClass::Damage`](../../../functions/dossiers/playerClass/FUN-0BE65C.md), `Triniti2D::GameContainer::CreateGameObject`, `Triniti2D::GameObject::Enable`, `Triniti2D::GameObject::Enable`, `Triniti2D::GameObject::GetPosition`, `Triniti2D::GameObject::GetSize`, `Triniti2D::GameObject::GetScene`, `Triniti2D::GameObject::GetCallBack`, `Triniti2D::GameObject::Container`, `Triniti2D::GameObject::SetPosition`, `Triniti2D::Scene::Mount`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___dynamic_cast`, `___mulsf3vfp`, `___subsf3vfp`

## Strings it references

- `0x2012E0` "yyj5"
- `0x201DE8` "effectClass"
- `0x208458` "JiguangAnim"
- `0x204888` "Attack_F3Audio"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x226FF2` GCC_except_table24_2
- `0x22A0A8` __ZTIN9Triniti2D18GameObjectCallBackE_ptr
- `0x22A260` __ZTIN7DinoCap11effectClassE_ptr
- `0x22C814` `typeinfo for'DinoCap::effectClass
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22A458` __ZN7DinoCap16g_ca_jiguangDataE_ptr
- `0x23EF34` DinoCap::g_ca_jiguangData
- `0x22A2F0` __ZN7DinoCap12g_b_FireDownE_ptr
- `0x23B8C3` DinoCap::g_b_FireDown

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xBEDA6   f0b5       PUSH    {R4-R7,LR}
0xBEDA8   5e46       MOV     R6, R11
0xBEDAA   5546       MOV     R5, R10
0xBEDAC   4446       MOV     R4, R8
0xBEDAE   70b4       PUSH    {R4-R6}
0xBEDB0   29f196e8   BLX     ___save_vfp_d8_d15_regs
0xBEDB4   16af       ADD     R7, SP, #arg_38
0xBEDB6   a9b0       SUB     SP, SP, #0xA4
0xBEDB8   b74b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xBEDC2)
0xBEDBA   0d90       STR     R0, [SP,#0xA4+var_70]
0xBEDBC   0c91       STR     R1, [SP,#0xA4+var_74]
0xBEDBE   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xBEDC0   0121       MOVS    R1, #1
0xBEDC2   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xBEDC4   6846       MOV     R0, SP
0xBEDC6   2290       STR     R0, [SP,#0xA4+var_1C]
0xBEDC8   1e93       STR     R3, [SP,#0xA4+fctx.personality]
0xBEDCA   b44b       LDR     R3, =(GCC_except_table24_2 - 0xBEDD4)
0xBEDCC   18a8       ADD     R0, SP, #0xA4+fctx; lpfctx
0xBEDCE   2097       STR     R7, [SP,#0xA4+fctx.jbuf]
0xBEDD0   7b44       ADD     R3, PC; GCC_except_table24_2
0xBEDD2   1f93       STR     R3, [SP,#0xA4+fctx.lsda]
0xBEDD4   b24b       LDR     R3, =(loc_BEE90 - 0xBEDDA)
0xBEDD6   7b44       ADD     R3, PC; loc_BEE90
```

**Around `0xBEE7A` (cited by the Unity code)**

```
0xBEE6E   0493       STR     R3, [SP,#0xA4+var_94]; bool
0xBEE70   1990       STR     R0, [SP,#0xA4+fctx.call_site]
0xBEE72   27a9       ADD     R1, SP, #0xA4+var_8; std::string *
0xBEE74   1098       LDR     R0, [SP,#0xA4+var_64]; this
0xBEE76   6122       MOVS    R2, #0x61 ; 'a'; int
0xBEE78   0023       MOVS    R3, #0; bool
0xBEE7A   acf091fc   BL      __ZN9Triniti2D13GameContainer16CreateGameObjectERKSsibbbbbb; Triniti2D::GameContainer::CreateGameObject(std::string const&,int,bool,bool,bool,bool,bool,bool)   <==
0xBEE7E   0d99       LDR     R1, [SP,#0xA4+var_70]
0xBEE80   0a9a       LDR     R2, [SP,#0xA4+var_7C]
0xBEE82   0223       MOVS    R3, #2
0xBEE84   8850       STR     R0, [R1,R2]
0xBEE86   1993       STR     R3, [SP,#0xA4+fctx.call_site]
0xBEE88   27a8       ADD     R0, SP, #0xA4+var_8; this
```

