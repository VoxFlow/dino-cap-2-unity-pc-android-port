# playerClass::AfterNoBullets

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0BAEAC` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xBAEAC` |
| RVA (EA - 0x1000) | `0xB9EAC` |
| File offset | `0xB9EAC` |
| Size | 392 code bytes in 1 chunk(s), 178 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11playerClass14AfterNoBulletsEi` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerClass::AfterNoBullets(int)` |
| Bytes SHA-256 | `201a860592825997c684b3547b27752361bf39a036875c908b7b2bdd1e3241c4` |
| Direct code callers | 2 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / VERIFIED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameControls.cs:373`](../../../../../Assets/Scripts/GameControls.cs#L373) cites `0xBAEAC`
- [`Assets/Scripts/GameFire.cs:7`](../../../../../Assets/Scripts/GameFire.cs#L7) cites `0xBAEAC`
- [`Assets/Scripts/GameFire.cs:283`](../../../../../Assets/Scripts/GameFire.cs#L283) cites `0xBAEAC`

## Callers and callees (direct code references)

Callers: [`playerClass::OnFireDown`](../../../functions/dossiers/playerClass/FUN-0B9990.md), [`playerClass::AfterNoBullets`](../../../functions/dossiers/playerClass/FUN-0BAEAC.md)

Callees: `GenComAnim`, [`playerClass::getEgg`](../../../functions/dossiers/playerClass/FUN-0B937C.md), [`playerClass::ChangeWeapon`](../../../functions/dossiers/playerClass/FUN-0B9DE8.md), [`playerClass::AfterNoBullets`](../../../functions/dossiers/playerClass/FUN-0BAEAC.md), `subWeaponClass::~subWeaponClass`, `Triniti2D::GameObject::GetScene`, `Triniti2D::Scene::GetCallBack`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `___dynamic_cast`

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x226F14` GCC_except_table7_5
- `0x22A24C` __ZN7DinoCap15g_player_vectorE_ptr
- `0x2404AC` DinoCap::g_player_vector
- `0x22A2B8` __ZN7DinoCap13g_player_typeE_ptr
- `0x23B86C` DinoCap::g_player_type
- `0x22A0C0` __ZTIN9Triniti2D13SceneCallBackE_ptr
- `0x22A0C8` __ZTIN7DinoCap9mainSceneE_ptr
- `0x22C868` `typeinfo for'DinoCap::mainScene
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xBAEAC   f0b5       PUSH    {R4-R7,LR}
0xBAEAE   5e46       MOV     R6, R11
0xBAEB0   5546       MOV     R5, R10
0xBAEB2   4446       MOV     R4, R8
0xBAEB4   70b4       PUSH    {R4-R6}
0xBAEB6   2df114e8   BLX     ___save_vfp_d8_d15_regs
0xBAEBA   16af       ADD     R7, SP, #arg_38
0xBAEBC   a0b0       SUB     SP, SP, #0x80
0xBAEBE   5d4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xBAEC8)
0xBAEC0   0490       STR     R0, [SP,#0x80+var_70]
0xBAEC2   0391       STR     R1, [SP,#0x80+var_74]
0xBAEC4   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xBAEC6   6846       MOV     R0, SP
0xBAEC8   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xBAECA   0121       MOVS    R1, #1
0xBAECC   0f90       STR     R0, [SP,#0x80+var_44]
0xBAECE   0b93       STR     R3, [SP,#0x80+fctx.personality]
0xBAED0   594b       LDR     R3, =(GCC_except_table7_5 - 0xBAEDA)
0xBAED2   05a8       ADD     R0, SP, #0x80+fctx; lpfctx
0xBAED4   0d97       STR     R7, [SP,#0x80+fctx.jbuf]
0xBAED6   7b44       ADD     R3, PC; GCC_except_table7_5
0xBAED8   0c93       STR     R3, [SP,#0x80+fctx.lsda]
0xBAEDA   584b       LDR     R3, =(loc_BAFDA - 0xBAEE0)
0xBAEDC   7b44       ADD     R3, PC; loc_BAFDA
```

