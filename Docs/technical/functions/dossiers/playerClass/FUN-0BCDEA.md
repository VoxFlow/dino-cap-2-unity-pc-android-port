# playerClass::Die

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0BCDEA` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xBCDEA` |
| RVA (EA - 0x1000) | `0xBBDEA` |
| File offset | `0xBBDEA` |
| Size | 764 code bytes in 1 chunk(s), 313 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11playerClass3DieEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerClass::Die(void)` |
| Bytes SHA-256 | `71b5847cabe0a5e7bb7a552098fbe8b9cbfcf5f83178528597e560f412357e9e` |
| Direct code callers | 2 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameBlood.cs:166`](../../../../../Assets/Scripts/GameBlood.cs#L166) cites `0xBCDEA`
- [`Assets/Scripts/GameCombat.cs:698`](../../../../../Assets/Scripts/GameCombat.cs#L698) cites `0xBCEE4`
- [`Assets/Scripts/GameCombat.cs:699`](../../../../../Assets/Scripts/GameCombat.cs#L699) cites `0xBCEE4`
- [`Assets/Scripts/HeroPreview.cs:180`](../../../../../Assets/Scripts/HeroPreview.cs#L180) cites `0xBCEE4`
- [`Assets/Scripts/NativeBlood.cs:30`](../../../../../Assets/Scripts/NativeBlood.cs#L30) cites `0xBCDEA`
- [`Assets/Scripts/NativeBlood.cs:345`](../../../../../Assets/Scripts/NativeBlood.cs#L345) cites `0xBCDEA`

## Callers and callees (direct code references)

Callers: [`playerClass::BeAttack`](../../../functions/dossiers/playerClass/FUN-0BD166.md), [`playerClass::BeAttackByBoss`](../../../functions/dossiers/playerClass/FUN-0BD3C8.md)

Callees: [`CAObjCallBack::playAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00A064.md), [`CAObjCallBack::setAnimationAdv`](../../../functions/dossiers/CAObjCallBack/FUN-00A298.md), [`CAObjCallBack::getSpritePos`](../../../functions/dossiers/CAObjCallBack/FUN-00A4D0.md), [`CAObjCallBack::setAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00AEA0.md), [`PlaySound`](../../../functions/dossiers/DinoCap/FUN-01A448.md), [`mainScene::GetParticle`](../../../functions/dossiers/mainScene/FUN-094C3E.md), [`playerClass::UpdateBG`](../../../functions/dossiers/playerClass/FUN-0B8944.md), `Triniti2D::GameObject::Enable`, `Triniti2D::GameObject::GetPosition`, `Triniti2D::GameObject::GetScene`, `Triniti2D::GameObject::GetCallBack`, `Triniti2D::GameObject::Physics`, `Triniti2D::GameObject::Sprite`, `Triniti2D::GameObject::SetSize`, `Triniti2D::GameObject::SetPosition`, `Triniti2D::GamePhysics::SetLinearVelocity`, `Triniti2D::GamePhysics::ApplyForce`, `Triniti2D::GameSprite::SetFrame`, `Triniti2D::Scene::GetCallBack`, `___switch32`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___dynamic_cast`, `___fixsfsivfp`, `___floatsisfvfp`, `___mulsf3vfp`, `_rand`

## Strings it references

- `0x201DA8` "DieAnim"
- `0x20496C` "RoledieAudio"
- `0x2012E8` "yyj6"
- `0x201E48` "xueFrame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x226FB6` GCC_except_table18_2
- `0x22A2B8` __ZN7DinoCap13g_player_typeE_ptr
- `0x23B86C` DinoCap::g_player_type
- `0x22A478` __ZN7DinoCap13g_ca_pDieDataE_ptr
- `0x23E94C` DinoCap::g_ca_pDieData
- `0x22A4D4` __ZN7DinoCap14g_ca_2pDieDataE_ptr
- `0x23E968` DinoCap::g_ca_2pDieData
- `0x22A53C` __ZN7DinoCap14g_ca_3pDieDataE_ptr
- `0x23E984` DinoCap::g_ca_3pDieData
- `0x22A3A4` __ZN7DinoCap14g_ca_4pDieDataE_ptr
- `0x23E9A0` DinoCap::g_ca_4pDieData
- `0x22A0C0` __ZTIN9Triniti2D13SceneCallBackE_ptr
- `0x22A0C8` __ZTIN7DinoCap9mainSceneE_ptr
- `0x22C868` `typeinfo for'DinoCap::mainScene
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22A0A8` __ZTIN9Triniti2D18GameObjectCallBackE_ptr
- `0x22A268` __ZTIN7DinoCap13particleClassE_ptr
- `0x22C668` `typeinfo for'DinoCap::particleClass

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xBCDEA   f0b5       PUSH    {R4-R7,LR}
0xBCDEC   5e46       MOV     R6, R11
0xBCDEE   5546       MOV     R5, R10
0xBCDF0   4446       MOV     R4, R8
0xBCDF2   70b4       PUSH    {R4-R6}
0xBCDF4   2bf174e8   BLX     ___save_vfp_d8_d15_regs
0xBCDF8   16af       ADD     R7, SP, #arg_38
0xBCDFA   afb0       SUB     SP, SP, #0xBC
0xBCDFC   ba4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xBCE06)
0xBCDFE   0122       MOVS    R2, #1
0xBCE00   0790       STR     R0, [SP,#0xBC+var_A0]
0xBCE02   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xBCE04   6946       MOV     R1, SP
0xBCE06   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xBCE08   16a8       ADD     R0, SP, #0xBC+fctx; lpfctx
0xBCE0A   1e97       STR     R7, [SP,#0xBC+fctx.jbuf]
0xBCE0C   1c93       STR     R3, [SP,#0xBC+fctx.personality]
0xBCE0E   b74b       LDR     R3, =(GCC_except_table18_2 - 0xBCE16)
0xBCE10   2091       STR     R1, [SP,#0xBC+var_3C]
0xBCE12   7b44       ADD     R3, PC; GCC_except_table18_2
0xBCE14   1d93       STR     R3, [SP,#0xBC+fctx.lsda]
0xBCE16   b64b       LDR     R3, =(loc_BCF7C - 0xBCE1C)
0xBCE18   7b44       ADD     R3, PC; loc_BCF7C
0xBCE1A   1343       ORRS    R3, R2
```

**Around `0xBCEE4` (cited by the Unity code)**

```
0xBCED8   9b00       LSLS    R3, R3, #2; char *
0xBCEDA   0422       MOVS    R2, #4
0xBCEDC   ca50       STR     R2, [R1,R3]
0xBCEDE   7844       ADD     R0, PC; "RoledieAudio"
0xBCEE0   0021       MOVS    R1, #0; char *
0xBCEE2   0022       MOVS    R2, #0; bool
0xBCEE4   5df7b0fa   BL      __ZN7DinoCap9PlaySoundEPKcbS1_; DinoCap::PlaySound(char const*,bool,char const*)   <==
0xBCEE8   8f4b       LDR     R3, =(aYyj6 - 0xBCEF2); "yyj6"
0xBCEEA   9049       LDR     R1, =(aXueframe - 0xBCEF4); "xueFrame"
0xBCEEC   0022       MOVS    R2, #0
0xBCEEE   7b44       ADD     R3, PC; "yyj6"
0xBCEF0   7944       ADD     R1, PC; "xueFrame"
0xBCEF2   0f92       STR     R2, [SP,#0xBC+var_80]
```

