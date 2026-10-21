# dinosaurClass::DieTouFei

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-010CBC` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x10CBC` |
| RVA (EA - 0x1000) | `0xFCBC` |
| File offset | `0xFCBC` |
| Size | 2510 code bytes in 1 chunk(s), 1024 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap13dinosaurClass9DieTouFeiEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::dinosaurClass::DieTouFei(void)` |
| Bytes SHA-256 | `042e8c0d963ceed5db2bc7a74ffcfd3ef6b5ce7e2b5fd65095434e98868a10db` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameBlood.cs:144`](../../../../../Assets/Scripts/GameBlood.cs#L144) cites `0x10CBC`
- [`Assets/Scripts/GameCombat.cs:809`](../../../../../Assets/Scripts/GameCombat.cs#L809) cites `0x10CBC`
- [`Assets/Scripts/GameCombat.cs:856`](../../../../../Assets/Scripts/GameCombat.cs#L856) cites `0x10CBC`
- [`Assets/Scripts/NativeBlood.cs:29`](../../../../../Assets/Scripts/NativeBlood.cs#L29) cites `0x10CBC`
- [`Assets/Scripts/NativeBlood.cs:292`](../../../../../Assets/Scripts/NativeBlood.cs#L292) cites `0x10CBC`

## Callers and callees (direct code references)

Callers: [`dinosaurClass::Die`](../../../functions/dossiers/dinosaurClass/FUN-0140FC.md)

Callees: [`CAObjCallBack::getSpritePos`](../../../functions/dossiers/CAObjCallBack/FUN-00A4D0.md), `sub_1168A`, [`mainScene::GetParticle`](../../../functions/dossiers/mainScene/FUN-094C3E.md), `Triniti2D::GameObject::Enable`, `Triniti2D::GameObject::GetScene`, `Triniti2D::GameObject::GetCallBack`, `Triniti2D::GameObject::Physics`, `Triniti2D::GameObject::Sprite`, `Triniti2D::GameObject::SetSize`, `Triniti2D::GameObject::SetPosition`, `Triniti2D::GamePhysics::SetLinearVelocity`, `Triniti2D::GamePhysics::SetAngularVelocity`, `Triniti2D::GamePhysics::ApplyForce`, `Triniti2D::GameSprite::SetFrame`, `Triniti2D::Scene::GetCallBack`, `___switch32`, `___save_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `std::string::string`, `std::string::~string`, `___dynamic_cast`, `___mulsf3vfp`

## Strings it references

- `0x2013B4` "com6"
- `0x2014B0` "tou"
- `0x201C4C` "k11toufeiFrame"
- `0x201C5C` "k101toufeiFrame"
- `0x201C6C` "k111toufeiFrame"
- `0x201C7C` "k3toufeiFrame"
- `0x201C8C` "k21toufeiFrame"
- `0x201C9C` "k211toufeiFrame"
- `0x201CAC` "k4toufeiFrame"
- `0x201CBC` "k31toufeiFrame"
- `0x201CCC` "k32toufeiFrame"
- `0x201CDC` "k301toufeiFrame"
- `0x201CEC` "k311toufeiFrame"
- `0x201CFC` "k321toufeiFrame"
- `0x201D0C` "k2toufeiFrame"
- `0x201D1C` "k51teshutou0003Frame"
- `0x201D34` "k52teshutou0003Frame"
- `0x201D4C` "k501teshutou0003Frame"
- `0x201D64` "k511teshutou0003Frame"
- `0x201D7C` "k521teshutou0003Frame"
- `0x201D94` "teshutou0003Frame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x223AEE` GCC_except_table3_3
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
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
0x10CBC   f0b5       PUSH    {R4-R7,LR}
0x10CBE   5e46       MOV     R6, R11
0x10CC0   5546       MOV     R5, R10
0x10CC2   4446       MOV     R4, R8
0x10CC4   70b4       PUSH    {R4-R6}
0x10CC6   d7f10ce9   BLX     ___save_vfp_d8_d15_regs
0x10CCA   16af       ADD     R7, SP, #arg_38
0x10CCC   cfb0       SUB     SP, SP, #0x13C; int
0x10CCE   dd4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x10CD8)
0x10CD0   0122       MOVS    R2, #1
0x10CD2   0190       STR     R0, [SP,#0x13C+var_138]; DinoCap::CAObjCallBack *
0x10CD4   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x10CD6   6946       MOV     R1, SP
0x10CD8   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x10CDA   1ca8       ADD     R0, SP, #0x13C+fctx; lpfctx
0x10CDC   2497       STR     R7, [SP,#0x13C+fctx.jbuf]
0x10CDE   2293       STR     R3, [SP,#0x13C+fctx.personality]
0x10CE0   d94b       LDR     R3, =(GCC_except_table3_3 - 0x10CE8)
0x10CE2   2691       STR     R1, [SP,#0x13C+var_A4]
0x10CE4   7b44       ADD     R3, PC; GCC_except_table3_3
0x10CE6   2393       STR     R3, [SP,#0x13C+fctx.lsda]
0x10CE8   d84b       LDR     R3, =(loc_114AC - 0x10CEE)
0x10CEA   7b44       ADD     R3, PC; loc_114AC
0x10CEC   1343       ORRS    R3, R2
```

