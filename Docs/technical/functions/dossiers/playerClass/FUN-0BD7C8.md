# playerClass::OnHitDown

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0BD7C8` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xBD7C8` |
| RVA (EA - 0x1000) | `0xBC7C8` |
| File offset | `0xBC7C8` |
| Size | 378 code bytes in 1 chunk(s), 169 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11playerClass9OnHitDownEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerClass::OnHitDown(void)` |
| Bytes SHA-256 | `5df951c801e75bf6ca70d3991c96b70f4e643da1b344f989682fa4b734ef73fd` |
| Direct code callers | 4 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameControls.cs:10`](../../../../../Assets/Scripts/GameControls.cs#L10) cites `0xBD7C8`
- [`Assets/Scripts/GameControls.cs:211`](../../../../../Assets/Scripts/GameControls.cs#L211) cites `0xBD810`

## Callers and callees (direct code references)

Callers: [`mainScene::OnTouchEvent`](../../../functions/dossiers/mainScene/FUN-096914.md), [`playerClass::OnAnimationEnd`](../../../functions/dossiers/playerClass/FUN-0BD978.md), [`lifeModeScene::OnTouchEvent`](../../../functions/dossiers/lifeModeScene/FUN-0D15E4.md)

Callees: [`CAObjCallBack::playAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00A064.md), [`CAObjCallBack::setAnimationAdv`](../../../functions/dossiers/CAObjCallBack/FUN-00A298.md), [`CAObjCallBack::setAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00AEA0.md), [`playerClass::UpdateBG`](../../../functions/dossiers/playerClass/FUN-0B8944.md), [`playerClass::OnFireUp`](../../../functions/dossiers/playerClass/FUN-0B8BC6.md), `subWeaponClass::~subWeaponClass`, `Triniti2D::GameObject::Physics`, `Triniti2D::GamePhysics::SetLinearVelocity`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::_M_mutate`

## Strings it references

- `0x208424` "playerHitAnimation"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x226FBE` GCC_except_table19_2
- `0x22A24C` __ZN7DinoCap15g_player_vectorE_ptr
- `0x2404AC` DinoCap::g_player_vector
- `0x22A2B8` __ZN7DinoCap13g_player_typeE_ptr
- `0x23B86C` DinoCap::g_player_type
- `0x22A0C4` __ZN7DinoCap16g_manager_WeaponE_ptr
- `0x2404B8` DinoCap::g_manager_Weapon
- `0x22A7E4` __ZN7DinoCap11weaponClassaSERKS0__ptr
- `0x22A640` __ZN7DinoCap11g_b_HitDownE_ptr
- `0x23B8C2` DinoCap::g_b_HitDown

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xBD7C8   f0b5       PUSH    {R4-R7,LR}
0xBD7CA   5e46       MOV     R6, R11
0xBD7CC   5546       MOV     R5, R10
0xBD7CE   4446       MOV     R4, R8
0xBD7D0   70b4       PUSH    {R4-R6}
0xBD7D2   2af186eb   BLX     ___save_vfp_d8_d15_regs
0xBD7D6   16af       ADD     R7, SP, #arg_38
0xBD7D8   9bb0       SUB     SP, SP, #0x6C ; 'l'
0xBD7DA   5a4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xBD7E4)
0xBD7DC   0490       STR     R0, [SP,#0x6C+var_5C]
0xBD7DE   0121       MOVS    R1, #1
0xBD7E0   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xBD7E2   6846       MOV     R0, SP
0xBD7E4   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xBD7E6   0f90       STR     R0, [SP,#0x6C+var_30]
0xBD7E8   05a8       ADD     R0, SP, #0x6C+fctx; lpfctx
0xBD7EA   0b93       STR     R3, [SP,#0x6C+fctx.personality]
0xBD7EC   564b       LDR     R3, =(GCC_except_table19_2 - 0xBD7F4)
0xBD7EE   0d97       STR     R7, [SP,#0x6C+fctx.jbuf]
0xBD7F0   7b44       ADD     R3, PC; GCC_except_table19_2
0xBD7F2   0c93       STR     R3, [SP,#0x6C+fctx.lsda]
0xBD7F4   554b       LDR     R3, =(loc_BD912 - 0xBD7FA)
0xBD7F6   7b44       ADD     R3, PC; loc_BD912
0xBD7F8   0b43       ORRS    R3, R1
```

**Around `0xBD810` (cited by the Unity code)**

```
0xBD802   a0229200   MOVS    R2, #0x280
0xBD806   a358       LDR     R3, [R4,R2]
0xBD808   022b       CMP     R3, #2
0xBD80A   00d9       BLS     loc_BD80E
0xBD80C   8ee0       B       loc_BD92C
0xBD80E   504b       LDR     R3, =0x291
0xBD810   e35c       LDRB    R3, [R4,R3]   <==
0xBD812   002b       CMP     R3, #0
0xBD814   00d1       BNE     loc_BD818
0xBD816   89e0       B       loc_BD92C
0xBD818   0125       MOVS    R5, #1
0xBD81A   0323       MOVS    R3, #3
0xBD81C   6068       LDR     R0, [R4,#4]; this
```

