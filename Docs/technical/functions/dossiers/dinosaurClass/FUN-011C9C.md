# dinosaurClass::BeDamaged

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-011C9C` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x11C9C` |
| RVA (EA - 0x1000) | `0x10C9C` |
| File offset | `0x10C9C` |
| Size | 1182 code bytes in 1 chunk(s), 443 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap13dinosaurClass9BeDamagedEPNS_11weaponClassE` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::dinosaurClass::BeDamaged(DinoCap::weaponClass *)` |
| Bytes SHA-256 | `8285117d8fdcc9c0086fc76d0d9540c90431a57d60eaf99549781b14f888b2ac` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameBeam.cs:7`](../../../../../Assets/Scripts/GameBeam.cs#L7) cites `0x11C9C`
- [`Assets/Scripts/GameBeam.cs:37`](../../../../../Assets/Scripts/GameBeam.cs#L37) cites `0x11D14`
- [`Assets/Scripts/GameBeam.cs:39`](../../../../../Assets/Scripts/GameBeam.cs#L39) cites `0x11C9C`
- [`Assets/Scripts/GameBeam.cs:42`](../../../../../Assets/Scripts/GameBeam.cs#L42) cites `0x11D14`
- [`Assets/Scripts/GameCombat.cs:392`](../../../../../Assets/Scripts/GameCombat.cs#L392) cites `0x11DF4`
- [`Assets/Scripts/GameCombat.cs:501`](../../../../../Assets/Scripts/GameCombat.cs#L501) cites `0x11D84`

## Callers and callees (direct code references)

Callers: [`playerClass::Damage`](../../../functions/dossiers/playerClass/FUN-0BE65C.md)

Callees: [`CAObjCallBack::playAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00A064.md), [`CAObjCallBack::setAnimationAdv`](../../../functions/dossiers/CAObjCallBack/FUN-00A298.md), [`CAObjCallBack::setAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00AEA0.md), `Triniti2D::Camera::GetViewport`, `Triniti2D::GameContainer::CreateGameObject`, `Triniti2D::GameObject::Enable`, `Triniti2D::GameObject::GetPosition`, `Triniti2D::GameObject::GetScene`, `Triniti2D::GameObject::GetCallBack`, `Triniti2D::GameObject::Container`, `Triniti2D::GameObject::Physics`, `Triniti2D::GameObject::SetPosition`, `Triniti2D::GamePhysics::SetLinearVelocity`, `Triniti2D::Scene::GetCamera`, `___switch32`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___dynamic_cast`, `___gtsf2vfp`, `___ltsf2vfp`, `___mulsf3vfp`, `___subsf3vfp`

## Strings it references

- `0x201DDC` "BShaoAnim"
- `0x201DE8` "effectClass"
- `0x201DF4` "DahuoAnim"
- `0x201E00` "BEAnim"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x223B56` GCC_except_table7_2
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A0A8` __ZTIN9Triniti2D18GameObjectCallBackE_ptr
- `0x22A260` __ZTIN7DinoCap11effectClassE_ptr
- `0x22C814` `typeinfo for'DinoCap::effectClass
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22A2E4` __ZN7DinoCap14g_ca_dahuoDataE_ptr
- `0x23EEC4` DinoCap::g_ca_dahuoData
- `0x22A0E8` __ZN7DinoCap10g_ground_yE_ptr
- `0x23B910` DinoCap::g_ground_y

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x11C9C   f0b5       PUSH    {R4-R7,LR}
0x11C9E   5e46       MOV     R6, R11
0x11CA0   5546       MOV     R5, R10
0x11CA2   4446       MOV     R4, R8
0x11CA4   70b4       PUSH    {R4-R6}
0x11CA6   d6f11ce9   BLX     ___save_vfp_d8_d15_regs
0x11CAA   16af       ADD     R7, SP, #arg_38
0x11CAC   aab0       SUB     SP, SP, #0xA8
0x11CAE   e94b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x11CB8)
0x11CB0   0122       MOVS    R2, #1
0x11CB2   0b90       STR     R0, [SP,#0xA8+var_7C]
0x11CB4   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x11CB6   0a91       STR     R1, [SP,#0xA8+var_80]
0x11CB8   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x11CBA   6946       MOV     R1, SP
0x11CBC   13a8       ADD     R0, SP, #0xA8+fctx; lpfctx
0x11CBE   1993       STR     R3, [SP,#0xA8+fctx.personality]
0x11CC0   e54b       LDR     R3, =(GCC_except_table7_2 - 0x11CCA)
0x11CC2   1b97       STR     R7, [SP,#0xA8+fctx.jbuf]
0x11CC4   1d91       STR     R1, [SP,#0xA8+var_34]
0x11CC6   7b44       ADD     R3, PC; GCC_except_table7_2
0x11CC8   1a93       STR     R3, [SP,#0xA8+fctx.lsda]
0x11CCA   e44b       LDR     R3, =(loc_11E00 - 0x11CD0)
0x11CCC   7b44       ADD     R3, PC; loc_11E00
```

**Around `0x11D14` (cited by the Unity code)**

```
0x11D04   2791       STR     R1, [SP,#0xA8+var_C]
0x11D06   5868       LDR     R0, [R3,#4]; this
0x11D08   5af122f9   BL      __ZN9Triniti2D10GameObject8GetSceneEv; Triniti2D::GameObject::GetScene(void)
0x11D0C   63f1abfa   BL      __ZN9Triniti2D5Scene9GetCameraEv; Triniti2D::Scene::GetCamera(void)
0x11D10   011c       MOVS    R1, R0
0x11D12   20a8       ADD     R0, SP, #0xA8+var_28; this
0x11D14   4ff145ff   BL      __ZN9Triniti2D6Camera11GetViewportEv; Triniti2D::Camera::GetViewport(void)   <==
0x11D18   0b99       LDR     R1, [SP,#0xA8+var_7C]
0x11D1A   8968       LDR     R1, [R1,#8]
0x11D1C   0c91       STR     R1, [SP,#0xA8+var_78]
0x11D1E   0029       CMP     R1, #0
0x11D20   00d1       BNE     loc_11D24
0x11D22   ffe1       B       loc_12124
```

**Around `0x11D84` (cited by the Unity code)**

```
0x11D78   c7e1       B       loc_1210A
0x11D7A   0b9a       LDR     R2, [SP,#0xA8+var_7C]
0x11D7C   e423       MOVS    R3, #0xE4
0x11D7E   0b98       LDR     R0, [SP,#0xA8+var_7C]; this
0x11D80   d158       LDR     R1, [R2,R3]; DinoCap::CombineAnimation *
0x11D82   ba4a       LDR     R2, =(aBshaoanim - 0x11D8A); "BShaoAnim"
0x11D84   069b       LDR     R3, [SP,#0xA8+var_90]   <==
0x11D86   7a44       ADD     R2, PC; "BShaoAnim"
0x11D88   1493       STR     R3, [SP,#0xA8+fctx.call_site]
0x11D8A   f9f789f8   BL      __ZN7DinoCap13CAObjCallBack12setAnimationEPNS_16CombineAnimationEPKc; DinoCap::CAObjCallBack::setAnimation(DinoCap::CombineAnimation *,char const*)
0x11D8E   b84a       LDR     R2, =(asc_200E1C - 0x11D98); ""
0x11D90   0b98       LDR     R0, [SP,#0xA8+var_7C]; this
0x11D92   0021       MOVS    R1, #0; DinoCap::CombineAnimation *
```

**Around `0x11DF4` (cited by the Unity code)**

```
0x11DE6   28a9       ADD     R1, SP, #0xA8+var_8; std::string *
0x11DE8   0023       MOVS    R3, #0; bool
0x11DEA   59f1d9fc   BL      __ZN9Triniti2D13GameContainer16CreateGameObjectERKSsibbbbbb; Triniti2D::GameContainer::CreateGameObject(std::string const&,int,bool,bool,bool,bool,bool,bool)
0x11DEE   0b9a       LDR     R2, [SP,#0xA8+var_7C]
0x11DF0   c023       MOVS    R3, #0xC0
0x11DF2   0221       MOVS    R1, #2
0x11DF4   d050       STR     R0, [R2,R3]   <==
0x11DF6   1491       STR     R1, [SP,#0xA8+fctx.call_site]
0x11DF8   28a8       ADD     R0, SP, #0xA8+var_8; this
0x11DFA   d7f1daee   BLX     __ZNSsD2Ev; std::string::~string()
0x11DFE   0fe0       B       loc_11E20
0x11E00   159a       LDR     R2, [SP,#0xA8+fctx.data]
0x11E02   149b       LDR     R3, [SP,#0xA8+fctx.call_site]
```

