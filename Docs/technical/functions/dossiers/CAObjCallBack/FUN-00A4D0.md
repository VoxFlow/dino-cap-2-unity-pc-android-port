# CAObjCallBack::getSpritePos

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-00A4D0` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xA4D0` |
| RVA (EA - 0x1000) | `0x94D0` |
| File offset | `0x94D0` |
| Size | 484 code bytes in 1 chunk(s), 209 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap13CAObjCallBack12getSpritePosEPKcRN9Triniti2D7Vector2E` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::CAObjCallBack::getSpritePos(char const*,Triniti2D::Vector2 &)` |
| Bytes SHA-256 | `0e5a01d2c8ab08b4d5b90bf58188c293f4b73020a7ce0ceabb565a86f7fbf750` |
| Direct code callers | 32 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameBlood.cs:75`](../../../../../Assets/Scripts/GameBlood.cs#L75) (by name)
- [`Assets/Scripts/NativeCaAnimation.cs:265`](../../../../../Assets/Scripts/NativeCaAnimation.cs#L265) (by name)
- [`Assets/Scripts/NativeSpriteAnchor.cs:5`](../../../../../Assets/Scripts/NativeSpriteAnchor.cs#L5) (by name)

## Callers and callees (direct code references)

Callers: [`houseClass::OnTimeout`](../../../functions/dossiers/houseClass/FUN-0071EA.md), [`dinosaurClass::DieTouFei`](../../../functions/dossiers/dinosaurClass/FUN-010CBC.md), [`dinosaurClass::BlastSuiRou`](../../../functions/dossiers/dinosaurClass/FUN-01226C.md), [`dinosaurClass::BlastBlood`](../../../functions/dossiers/dinosaurClass/FUN-012756.md), [`dinosaurClass::Blood`](../../../functions/dossiers/dinosaurClass/FUN-012A46.md), [`dinosaurClass::DieNormal`](../../../functions/dossiers/dinosaurClass/FUN-012CA8.md), [`effectClass::OnUpdate`](../../../functions/dossiers/effectClass/FUN-0167A6.md), [`playerClass::FeiBiao`](../../../functions/dossiers/playerClass/FUN-0BC868.md), [`playerClass::JuPan`](../../../functions/dossiers/playerClass/FUN-0BCA10.md), [`playerClass::Die`](../../../functions/dossiers/playerClass/FUN-0BCDEA.md), [`playerClass::JinAttack`](../../../functions/dossiers/playerClass/FUN-0BDFB8.md), [`playerClass::Damage`](../../../functions/dossiers/playerClass/FUN-0BE65C.md), [`playerClass::PenHuoQ`](../../../functions/dossiers/playerClass/FUN-0BEB28.md), [`playerClass::JiGuangQ`](../../../functions/dossiers/playerClass/FUN-0BEDA6.md), [`playerClass::Attack`](../../../functions/dossiers/playerClass/FUN-0BF784.md), [`playerClass::LiuDanQ`](../../../functions/dossiers/playerClass/FUN-0BFE94.md), [`playerClass::KaiQiang`](../../../functions/dossiers/playerClass/FUN-0C0336.md), `selPlayerClass::PenHuoQ`, [`playerNewAniClass::JinAttack`](../../../functions/dossiers/playerNewAniClass/FUN-128AB4.md), [`playerNewAniClass::Attack`](../../../functions/dossiers/playerNewAniClass/FUN-1291D0.md)

Callees: `Triniti2D::GameObject::Enable`, `Triniti2D::GameObject::GetPosition`, `Triniti2D::GameObject::GetRotation`, `Triniti2D::GameObject::GetCallBack`, `Triniti2D::GameObject::Sprite`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::compare`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___dynamic_cast`, `___mulsf3vfp`, `___subsf3vfp`, `_cosf`, `_sinf`

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x2239EF` GCC_except_table6_0
- `0x22A0A0` __ZNKSt6vectorIPN9Triniti2D10GameObjectESaIS2_EE4sizeEv_ptr
- `0x22A0A8` __ZTIN9Triniti2D18GameObjectCallBackE_ptr
- `0x22A128` __ZTIN7DinoCap19CAObjSpriteCallBackE_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xA4D0    f0b5       PUSH    {R4-R7,LR}
0xA4D2    5e46       MOV     R6, R11
0xA4D4    5546       MOV     R5, R10
0xA4D6    4446       MOV     R4, R8
0xA4D8    70b4       PUSH    {R4-R6}
0xA4DA    ddf102ed   BLX     ___save_vfp_d8_d15_regs
0xA4DE    16af       ADD     R7, SP, #arg_38
0xA4E0    a5b0       SUB     SP, SP, #0x94
0xA4E2    744b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xA4EC)
0xA4E4    0a92       STR     R2, [SP,#0x94+var_6C]
0xA4E6    0122       MOVS    R2, #1
0xA4E8    7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xA4EA    0c90       STR     R0, [SP,#0x94+var_64]
0xA4EC    1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xA4EE    0b91       STR     R1, [SP,#0x94+var_68]
0xA4F0    16a8       ADD     R0, SP, #0x94+fctx; lpfctx
0xA4F2    1c93       STR     R3, [SP,#0x94+fctx.personality]
0xA4F4    704b       LDR     R3, =(GCC_except_table6_0 - 0xA4FE)
0xA4F6    6946       MOV     R1, SP
0xA4F8    2091       STR     R1, [SP,#0x94+var_14]
0xA4FA    7b44       ADD     R3, PC; GCC_except_table6_0
0xA4FC    1d93       STR     R3, [SP,#0x94+fctx.lsda]
0xA4FE    6f4b       LDR     R3, =(loc_A672 - 0xA506)
0xA500    1e97       STR     R7, [SP,#0x94+fctx.jbuf]
```

