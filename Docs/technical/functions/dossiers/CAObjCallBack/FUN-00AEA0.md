# CAObjCallBack::setAnimation

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-00AEA0` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xAEA0` |
| RVA (EA - 0x1000) | `0x9EA0` |
| File offset | `0x9EA0` |
| Size | 310 code bytes in 1 chunk(s), 140 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap13CAObjCallBack12setAnimationEPNS_16CombineAnimationEPKc` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::CAObjCallBack::setAnimation(DinoCap::CombineAnimation *,char const*)` |
| Bytes SHA-256 | `9d7e7bd876e5a6b8048c0a9fa38a171bb90d73cfc7de492e4f2ff3027c5f4a58` |
| Direct code callers | 69 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameCombat.cs:758`](../../../../../Assets/Scripts/GameCombat.cs#L758) cites `0xAEA0`

## Callers and callees (direct code references)

Callers: [`houseClass::OnLoad`](../../../functions/dossiers/houseClass/FUN-0072DC.md), [`dinosaurClass::Jump`](../../../functions/dossiers/dinosaurClass/FUN-010904.md), [`dinosaurClass::Attack`](../../../functions/dossiers/dinosaurClass/FUN-0109DC.md), [`dinosaurClass::DieShaoSi`](../../../functions/dossiers/dinosaurClass/FUN-010A4C.md), `sub_1168A`, [`dinosaurClass::Move`](../../../functions/dossiers/dinosaurClass/FUN-011784.md), [`dinosaurClass::BeDamaged`](../../../functions/dossiers/dinosaurClass/FUN-011C9C.md), [`dinosaurClass::Blood`](../../../functions/dossiers/dinosaurClass/FUN-012A46.md), [`dinosaurClass::DieNormal`](../../../functions/dossiers/dinosaurClass/FUN-012CA8.md), [`dinosaurClass::BeJinAttacked`](../../../functions/dossiers/dinosaurClass/FUN-01442A.md), [`dinosaurClass::BeAttacked`](../../../functions/dossiers/dinosaurClass/FUN-014918.md), [`mainScene::LevelUpEffect`](../../../functions/dossiers/mainScene/FUN-0957E0.md), [`mainScene::AddYezi`](../../../functions/dossiers/mainScene/FUN-0A108A.md), [`mainScene::AddDian`](../../../functions/dossiers/mainScene/FUN-0A1348.md), [`playerClass::Stand`](../../../functions/dossiers/playerClass/FUN-0B88B4.md), [`playerClass::Move`](../../../functions/dossiers/playerClass/FUN-0B8900.md), [`playerClass::Qianghuo`](../../../functions/dossiers/playerClass/FUN-0B8C90.md), [`playerClass::Roll`](../../../functions/dossiers/playerClass/FUN-0B95F8.md), [`playerClass::OnFireDown`](../../../functions/dossiers/playerClass/FUN-0B9990.md), `sub_BA20A`, `playerClass::OnTimeout`, [`playerClass::FeiBiao`](../../../functions/dossiers/playerClass/FUN-0BC868.md), [`playerClass::JuPan`](../../../functions/dossiers/playerClass/FUN-0BCA10.md), [`playerClass::Die`](../../../functions/dossiers/playerClass/FUN-0BCDEA.md), [`playerClass::BeAttack`](../../../functions/dossiers/playerClass/FUN-0BD166.md), [`playerClass::BeAttackByBoss`](../../../functions/dossiers/playerClass/FUN-0BD3C8.md), [`playerClass::OnHitDown`](../../../functions/dossiers/playerClass/FUN-0BD7C8.md), [`playerClass::OnAnimationEnd`](../../../functions/dossiers/playerClass/FUN-0BD978.md), [`playerClass::EnterHouse`](../../../functions/dossiers/playerClass/FUN-0BDEEC.md), [`playerClass::PenHuoQ`](../../../functions/dossiers/playerClass/FUN-0BEB28.md) ... (48)

Callees: `Triniti2D::GameContainer::CreateGameObject`, `Triniti2D::GameObject::Enable`, `Triniti2D::GameObject::Container`, `Triniti2D::GameObject::Sprite`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::assign`, `std::string::string`, `std::string::~string`, `_strlen`

## Strings it references

- `0x20118C` "CAObjSpriteCallBack"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x223A09` GCC_except_table8
- `0x22A0A0` __ZNKSt6vectorIPN9Triniti2D10GameObjectESaIS2_EE4sizeEv_ptr
- `0x22A11C` __ZNSt6vectorIPN9Triniti2D10GameObjectESaIS2_EE9push_backERKS2__ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xAEA0    f0b5       PUSH    {R4-R7,LR}
0xAEA2    5e46       MOV     R6, R11
0xAEA4    5546       MOV     R5, R10
0xAEA6    4446       MOV     R4, R8
0xAEA8    70b4       PUSH    {R4-R6}
0xAEAA    ddf11ae8   BLX     ___save_vfp_d8_d15_regs
0xAEAE    16af       ADD     R7, SP, #arg_38
0xAEB0    9db0       SUB     SP, SP, #0x74 ; 't'
0xAEB2    494b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xAEBC)
0xAEB4    0692       STR     R2, [SP,#0x74+__s]
0xAEB6    6a46       MOV     R2, SP
0xAEB8    7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xAEBA    1792       STR     R2, [SP,#0x74+var_18]
0xAEBC    1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xAEBE    0122       MOVS    R2, #1
0xAEC0    0890       STR     R0, [SP,#0x74+var_54]
0xAEC2    1393       STR     R3, [SP,#0x74+fctx.personality]
0xAEC4    454b       LDR     R3, =(GCC_except_table8 - 0xAECE)
0xAEC6    0da8       ADD     R0, SP, #0x74+fctx; lpfctx
0xAEC8    0791       STR     R1, [SP,#0x74+var_58]
0xAECA    7b44       ADD     R3, PC; GCC_except_table8
0xAECC    1493       STR     R3, [SP,#0x74+fctx.lsda]
0xAECE    444b       LDR     R3, =(loc_AF60 - 0xAED6)
0xAED0    1597       STR     R7, [SP,#0x74+fctx.jbuf]
```

