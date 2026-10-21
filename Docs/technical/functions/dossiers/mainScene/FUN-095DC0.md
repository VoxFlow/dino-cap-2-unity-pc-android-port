# mainScene::initEgg

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-095DC0` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x95DC0` |
| RVA (EA - 0x1000) | `0x94DC0` |
| File offset | `0x94DC0` |
| Size | 388 code bytes in 1 chunk(s), 170 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9mainScene7initEggEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::mainScene::initEgg(void)` |
| Bytes SHA-256 | `579af81b47f7cc01e37a55c9c1b8b9b3286af31c10b390e4f70ed131197fd3ed` |
| Direct code callers | 11 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameEgg.cs:6`](../../../../../Assets/Scripts/GameEgg.cs#L6) cites `0x95DC0`
- [`Assets/Scripts/GameEgg.cs:81`](../../../../../Assets/Scripts/GameEgg.cs#L81) cites `0x95DC0`

## Callers and callees (direct code references)

Callers: `sub_ABBEE`, [`playerClass::UpdateCamera`](../../../functions/dossiers/playerClass/FUN-0BB054.md), [`playerClass::BeAttack`](../../../functions/dossiers/playerClass/FUN-0BD166.md), [`playerClass::BeAttackByBoss`](../../../functions/dossiers/playerClass/FUN-0BD3C8.md), [`playerNewAniClass::UpdateCamera`](../../../functions/dossiers/playerNewAniClass/FUN-127342.md), [`playerNewAniClass::BeAttack`](../../../functions/dossiers/playerNewAniClass/FUN-1282B8.md), [`playerNewAniClass::BeAttackByBoss`](../../../functions/dossiers/playerNewAniClass/FUN-1286E4.md)

Callees: `Triniti2D::GameObject::Sprite`, `Triniti2D::GameObject::SetSize`, `Triniti2D::GameObject::SetPosition`, `Triniti2D::GameSprite::SetFrame`, `Triniti2D::Scene::CreateGameObject`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___mulsf3vfp`

## Strings it references

- `0x2072F4` "eggClass"
- `0x2051F4` "p2move3Frame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x226584` GCC_except_table11_1
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A0E8` __ZN7DinoCap10g_ground_yE_ptr
- `0x23B910` DinoCap::g_ground_y

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x95DC0   f0b5       PUSH    {R4-R7,LR}
0x95DC2   5e46       MOV     R6, R11
0x95DC4   5546       MOV     R5, R10
0x95DC6   4446       MOV     R4, R8
0x95DC8   70b4       PUSH    {R4-R6}
0x95DCA   52f18ae8   BLX     ___save_vfp_d8_d15_regs
0x95DCE   16af       ADD     R7, SP, #arg_38
0x95DD0   9db0       SUB     SP, SP, #0x74 ; 't'
0x95DD2   5c4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x95DDC)
0x95DD4   6a46       MOV     R2, SP
0x95DD6   1792       STR     R2, [SP,#0x74+var_18]
0x95DD8   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x95DDA   0122       MOVS    R2, #1
0x95DDC   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x95DDE   0890       STR     R0, [SP,#0x74+var_54]
0x95DE0   0da8       ADD     R0, SP, #0x74+fctx; lpfctx
0x95DE2   1393       STR     R3, [SP,#0x74+fctx.personality]
0x95DE4   584b       LDR     R3, =(GCC_except_table11_1 - 0x95DEC)
0x95DE6   1597       STR     R7, [SP,#0x74+fctx.jbuf]
0x95DE8   7b44       ADD     R3, PC; GCC_except_table11_1
0x95DEA   1493       STR     R3, [SP,#0x74+fctx.lsda]
0x95DEC   574b       LDR     R3, =(loc_95E88 - 0x95DF2)
0x95DEE   7b44       ADD     R3, PC; loc_95E88
0x95DF0   1343       ORRS    R3, R2
```

