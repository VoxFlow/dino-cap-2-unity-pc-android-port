# playerClass::OnAnimationEndAdv

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0BC222` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xBC222` |
| RVA (EA - 0x1000) | `0xBB222` |
| File offset | `0xBB222` |
| Size | 1166 code bytes in 1 chunk(s), 462 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11playerClass17OnAnimationEndAdvEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerClass::OnAnimationEndAdv(void)` |
| Bytes SHA-256 | `e075bf65229723b2901f550d6cd92086d588c1d95c31c0c9ce68c0c56f16b5d5` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / VERIFIED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameBeam.cs:280`](../../../../../Assets/Scripts/GameBeam.cs#L280) cites `0xBC382`
- [`Assets/Scripts/GameBeam.cs:286`](../../../../../Assets/Scripts/GameBeam.cs#L286) cites `0xBC3CA`
- [`Assets/Scripts/GameFire.cs:7`](../../../../../Assets/Scripts/GameFire.cs#L7) cites `0xBC222`
- [`Assets/Scripts/GameFire.cs:243`](../../../../../Assets/Scripts/GameFire.cs#L243) cites `0xBC222`

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: `CAObjCallBack::playAnimationAdv`, [`CAObjCallBack::playAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00A064.md), [`CAObjCallBack::setAnimationAdv`](../../../functions/dossiers/CAObjCallBack/FUN-00A298.md), `subWeaponClass::~subWeaponClass`, `Triniti2D::GameObject::GetCallBack`, `Triniti2D::GameObject::SetTimer`, `___switch32`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `___dynamic_cast`

## Strings it references

- `0x208370` "fireup"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x226F96` GCC_except_table13_2
- `0x22A24C` __ZN7DinoCap15g_player_vectorE_ptr
- `0x2404AC` DinoCap::g_player_vector
- `0x22A2B8` __ZN7DinoCap13g_player_typeE_ptr
- `0x23B86C` DinoCap::g_player_type
- `0x22A0C4` __ZN7DinoCap16g_manager_WeaponE_ptr
- `0x2404B8` DinoCap::g_manager_Weapon
- `0x22A2F0` __ZN7DinoCap12g_b_FireDownE_ptr
- `0x23B8C3` DinoCap::g_b_FireDown
- `0x22A0A8` __ZTIN9Triniti2D18GameObjectCallBackE_ptr
- `0x22A260` __ZTIN7DinoCap11effectClassE_ptr
- `0x22C814` `typeinfo for'DinoCap::effectClass
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xBC222   f0b5       PUSH    {R4-R7,LR}
0xBC224   5e46       MOV     R6, R11
0xBC226   5546       MOV     R5, R10
0xBC228   4446       MOV     R4, R8
0xBC22A   70b4       PUSH    {R4-R6}
0xBC22C   2bf158ee   BLX     ___save_vfp_d8_d15_regs
0xBC230   16af       ADD     R7, SP, #arg_38
0xBC232   9fb0       SUB     SP, SP, #0x7C ; '|'
0xBC234   e64b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xBC23E)
0xBC236   0121       MOVS    R1, #1
0xBC238   0a90       STR     R0, [SP,#0x7C+var_54]
0xBC23A   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xBC23C   6846       MOV     R0, SP
0xBC23E   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xBC240   1590       STR     R0, [SP,#0x7C+var_28]
0xBC242   0ba8       ADD     R0, SP, #0x7C+fctx; lpfctx
0xBC244   1193       STR     R3, [SP,#0x7C+fctx.personality]
0xBC246   e34b       LDR     R3, =(GCC_except_table13_2 - 0xBC250)
0xBC248   1397       STR     R7, [SP,#0x7C+fctx.jbuf]
0xBC24A   18ac       ADD     R4, SP, #0x7C+var_1C
0xBC24C   7b44       ADD     R3, PC; GCC_except_table13_2
0xBC24E   1293       STR     R3, [SP,#0x7C+fctx.lsda]
0xBC250   e14b       LDR     R3, =(loc_BC674 - 0xBC256)
0xBC252   7b44       ADD     R3, PC; loc_BC674
```

**Around `0xBC382` (cited by the Unity code)**

```
0xBC376   7ae1       B       loc_BC66E
0xBC378   a34b       LDR     R3, =(__ZN7DinoCap12g_b_FireDownE_ptr - 0xBC37E); jumptable 000BC338 case 1
0xBC37A   7b44       ADD     R3, PC; __ZN7DinoCap12g_b_FireDownE_ptr
0xBC37C   1b68       LDR     R3, [R3]; DinoCap::g_b_FireDown ...
0xBC37E   0793       STR     R3, [SP,#0x7C+var_60]
0xBC380   1d78       LDRB    R5, [R3]; DinoCap::g_b_FireDown
0xBC382   0695       STR     R5, [SP,#0x7C+var_64]   <==
0xBC384   002d       CMP     R5, #0
0xBC386   51d1       BNE     loc_BC42C
0xBC388   0a98       LDR     R0, [SP,#0x7C+var_54]
0xBC38A   836c       LDR     R3, [R0,#0x48]
0xBC38C   042b       CMP     R3, #4
0xBC38E   31d1       BNE     loc_BC3F4
```

**Around `0xBC3CA` (cited by the Unity code)**

```
0xBC3BE   426c       LDR     R2, [R0,#0x44]; int
0xBC3C0   0093       STR     R3, [SP,#0x7C+var_7C]; unsigned int
0xBC3C2   0123       MOVS    R3, #1
0xBC3C4   0c93       STR     R3, [SP,#0x7C+fctx.call_site]
0xBC3C6   0021       MOVS    R1, #0; unsigned int
0xBC3C8   894b       LDR     R3, flt_BC5F0; float
0xBC3CA   4df74bfe   BL      __ZN7DinoCap13CAObjCallBack13playAnimationEmifm; DinoCap::CAObjCallBack::playAnimation(ulong,int,float,ulong)   <==
0xBC3CE   0123       MOVS    R3, #1
0xBC3D0   5b42       NEGS    R3, R3
0xBC3D2   0093       STR     R3, [SP,#0x7C+var_7C]; unsigned int
0xBC3D4   0123       MOVS    R3, #1
0xBC3D6   0c93       STR     R3, [SP,#0x7C+fctx.call_site]
0xBC3D8   0a98       LDR     R0, [SP,#0x7C+var_54]; this
```

