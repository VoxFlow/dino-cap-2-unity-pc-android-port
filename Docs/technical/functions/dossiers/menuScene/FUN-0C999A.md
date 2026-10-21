# menuScene::InitPlayer

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0C999A` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xC999A` |
| RVA (EA - 0x1000) | `0xC899A` |
| File offset | `0xC899A` |
| Size | 740 code bytes in 1 chunk(s), 323 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9menuScene10InitPlayerEi` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::menuScene::InitPlayer(int)` |
| Bytes SHA-256 | `804192a35819888067a21db082ef6d33d48f7ccd55501bd92454d3c7313b769a` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UI_FLOW / PORTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.title.hero_attack_random` (SUPERSEDED, NATIVE (ARM-verification of the rand scaling pending)): menuScene::InitAnim@0xc9ce0: k = (int)((float)rand() * 2^-31 * 4) -> 0..3 (value 4 only if the float rounds to 2^31, ~1/2^31 chance, takes the player_fire_long.acb path). Selection: 0 -> player 1 load_p1_fire + Image/a2b/baozha.a2b; 1 -> player 2 load_p2_fire + yuanli(com15) energy anim; 2 -> player 3 load_p3_fire + qh2; 3 -> player 4 p4_fire + feibiao; each plays animation 'fireAni' via CAObjCallBack::playAnimation(...
- Rule `dc2.title.loading_hero_position` (PROVEN, NATIVE): InitPlayer@0xc999a uses local scale 1.0 when g_iphone!=0, else 2.0. For hero types other than 5, SetPosition arguments are scale*float(0xC2480000) and scale*float(0x42480000), i.e. (-50*scale,+50*scale), iPad (-100,+100). Type 5 uses (-50*scale,-60*scale). The handoff's +48*scale and historical tentative -20 x are WRONG. Scope is the SetPosition call, not projected screen placement.

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: [`menuScene::InitAnim`](../../../functions/dossiers/menuScene/FUN-0C9CE0.md)

Callees: [`CAObjCallBack::playAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00A064.md), [`CAObjCallBack::setAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00AEA0.md), [`CompAnimObj::PlayAnimation`](../../../functions/dossiers/CompAnimObj/FUN-119010.md), [`CompAnimObj::SetAnimation`](../../../functions/dossiers/CompAnimObj/FUN-1198AC.md), `Triniti2D::GameObject::Enable`, `Triniti2D::GameObject::Enable`, `Triniti2D::GameObject::GetCallBack`, `Triniti2D::GameObject::SetPosition`, `Triniti2D::Scene::CreateGameObject`, `___switch32`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___dynamic_cast`, `___mulsf3vfp`

## Strings it references

- `0x207290` "menuObjN"
- `0x207288` "menuObj"
- `0x2091A0` "fireAni"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x227376` GCC_except_table4_8
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A0A8` __ZTIN9Triniti2D18GameObjectCallBackE_ptr
- `0x22AA24` __ZTIN7DinoCap11CompAnimObjE_ptr
- `0x22D154` `typeinfo for'DinoCap::CompAnimObj
- `0x2415F0` `vtable for'__cxxabiv1::__vmi_class_type_info
- `0x22A0D0` __ZTIN7DinoCap13CAObjCallBackE_ptr
- `0x22C6BC` `typeinfo for'DinoCap::CAObjCallBack
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xC999A   f0b5       PUSH    {R4-R7,LR}
0xC999C   5e46       MOV     R6, R11
0xC999E   5546       MOV     R5, R10
0xC99A0   4446       MOV     R4, R8
0xC99A2   70b4       PUSH    {R4-R6}
0xC99A4   1ef19cea   BLX     ___save_vfp_d8_d15_regs
0xC99A8   16af       ADD     R7, SP, #arg_38
0xC99AA   a7b0       SUB     SP, SP, #0x9C
0xC99AC   b44b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xC99B6)
0xC99AE   6a46       MOV     R2, SP
0xC99B0   2092       STR     R2, [SP,#0x9C+var_1C]
0xC99B2   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xC99B4   0122       MOVS    R2, #1
0xC99B6   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xC99B8   0f90       STR     R0, [SP,#0x9C+var_60]
0xC99BA   16a8       ADD     R0, SP, #0x9C+fctx; lpfctx
0xC99BC   1c93       STR     R3, [SP,#0x9C+fctx.personality]
0xC99BE   b14b       LDR     R3, =(GCC_except_table4_8 - 0xC99C8)
0xC99C0   0e91       STR     R1, [SP,#0x9C+var_64]
0xC99C2   1e97       STR     R7, [SP,#0x9C+fctx.jbuf]
0xC99C4   7b44       ADD     R3, PC; GCC_except_table4_8
0xC99C6   1d93       STR     R3, [SP,#0x9C+fctx.lsda]
0xC99C8   af4b       LDR     R3, =(loc_C9B58 - 0xC99CE)
0xC99CA   7b44       ADD     R3, PC; loc_C9B58
```

