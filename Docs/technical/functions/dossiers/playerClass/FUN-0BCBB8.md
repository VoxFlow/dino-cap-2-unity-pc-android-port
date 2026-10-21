# playerClass::Danke

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0BCBB8` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xBCBB8` |
| RVA (EA - 0x1000) | `0xBBBB8` |
| File offset | `0xBBBB8` |
| Size | 486 code bytes in 1 chunk(s), 201 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11playerClass5DankeERKN9Triniti2D7Vector2ES4_PKci` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerClass::Danke(Triniti2D::Vector2 const&,Triniti2D::Vector2 const&,char const*,int)` |
| Bytes SHA-256 | `9c82e3feed98fc95b47bda917d4fe8ca82aebea4e062d96dba889247abe04363` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / VERIFIED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameFire.cs:148`](../../../../../Assets/Scripts/GameFire.cs#L148) cites `0xBCBB8`
- [`Assets/Scripts/GameFire.cs:166`](../../../../../Assets/Scripts/GameFire.cs#L166) cites `0xBCBB8`
- [`Assets/Scripts/NativeBlood.cs:174`](../../../../../Assets/Scripts/NativeBlood.cs#L174) cites `0xBCBB8`

## Callers and callees (direct code references)

Callers: [`playerClass::KaiQiang`](../../../functions/dossiers/playerClass/FUN-0C0336.md)

Callees: [`mainScene::GetBullet`](../../../functions/dossiers/mainScene/FUN-09877C.md), `Triniti2D::GameObject::GetPosition`, `Triniti2D::GameObject::GetScene`, `Triniti2D::GameObject::GetCallBack`, `Triniti2D::GameObject::Physics`, `Triniti2D::GameObject::Sprite`, `Triniti2D::GameObject::SetSize`, `Triniti2D::GameObject::SetPosition`, `Triniti2D::GamePhysics::SetLinearVelocity`, `Triniti2D::GamePhysics::SetAngularVelocity`, `Triniti2D::GamePhysics::ApplyForce`, `Triniti2D::GameSprite::SetFrame`, `Triniti2D::Scene::GetCallBack`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___dynamic_cast`, `___floatsisfvfp`, `___mulsf3vfp`, `___subsf3vfp`, `_rand`

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x226FAE` GCC_except_table17_2
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A0C0` __ZTIN9Triniti2D13SceneCallBackE_ptr
- `0x22A0C8` __ZTIN7DinoCap9mainSceneE_ptr
- `0x22C868` `typeinfo for'DinoCap::mainScene
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22A0A8` __ZTIN9Triniti2D18GameObjectCallBackE_ptr
- `0x22A770` __ZTIN7DinoCap11bulletClassE_ptr
- `0x22C674` `typeinfo for'DinoCap::bulletClass

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xBCBB8   f0b5       PUSH    {R4-R7,LR}
0xBCBBA   5e46       MOV     R6, R11
0xBCBBC   5546       MOV     R5, R10
0xBCBBE   4446       MOV     R4, R8
0xBCBC0   70b4       PUSH    {R4-R6}
0xBCBC2   2bf18ee9   BLX     ___save_vfp_d8_d15_regs
0xBCBC6   16af       ADD     R7, SP, #arg_38
0xBCBC8   a0b0       SUB     SP, SP, #0x80
0xBCBCA   0493       STR     R3, [SP,#0x80+var_70]
0xBCBCC   744b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xBCBD6)
0xBCBCE   0592       STR     R2, [SP,#0x80+var_6C]
0xBCBD0   0122       MOVS    R2, #1
0xBCBD2   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xBCBD4   0790       STR     R0, [SP,#0x80+var_64]
0xBCBD6   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xBCBD8   0691       STR     R1, [SP,#0x80+var_68]
0xBCBDA   0da8       ADD     R0, SP, #0x80+fctx; lpfctx
0xBCBDC   1393       STR     R3, [SP,#0x80+fctx.personality]
0xBCBDE   714b       LDR     R3, =(GCC_except_table17_2 - 0xBCBE8)
0xBCBE0   6946       MOV     R1, SP
0xBCBE2   1597       STR     R7, [SP,#0x80+fctx.jbuf]
0xBCBE4   7b44       ADD     R3, PC; GCC_except_table17_2
0xBCBE6   1493       STR     R3, [SP,#0x80+fctx.lsda]
0xBCBE8   6f4b       LDR     R3, =(loc_BCC7E - 0xBCBF0)
```

