# playerClass::OnLoad

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0BC6BA` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xBC6BA` |
| RVA (EA - 0x1000) | `0xBB6BA` |
| File offset | `0xBB6BA` |
| Size | 248 code bytes in 1 chunk(s), 115 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11playerClass6OnLoadEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerClass::OnLoad(void)` |
| Bytes SHA-256 | `888c0e6c5c7d2a4e57e67925f73ae0fd74967ea7c26da24e406ef2e052f9ec7e` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | GAMEPLAY / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: r118 0xbc6ba: copies the weapon record of slot +592 into the weaponClass (+596), flags +657=1 +664=0 +658=0, +136 = 4.0; init-only, covered by the port's hero setup (PILOT: field +136 meaning not traced)

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameCombat.cs:51`](../../../../../Assets/Scripts/GameCombat.cs#L51) cites `0xBC78E`

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: `subWeaponClass::~subWeaponClass`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x226FA2` GCC_except_table15_1
- `0x22A24C` __ZN7DinoCap15g_player_vectorE_ptr
- `0x2404AC` DinoCap::g_player_vector
- `0x22A2B8` __ZN7DinoCap13g_player_typeE_ptr
- `0x23B86C` DinoCap::g_player_type
- `0x22A0C4` __ZN7DinoCap16g_manager_WeaponE_ptr
- `0x2404B8` DinoCap::g_manager_Weapon
- `0x22A7E4` __ZN7DinoCap11weaponClassaSERKS0__ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xBC6BA   f0b5       PUSH    {R4-R7,LR}
0xBC6BC   5e46       MOV     R6, R11
0xBC6BE   5546       MOV     R5, R10
0xBC6C0   4446       MOV     R4, R8
0xBC6C2   70b4       PUSH    {R4-R6}
0xBC6C4   2bf10cec   BLX     ___save_vfp_d8_d15_regs
0xBC6C8   16af       ADD     R7, SP, #arg_38
0xBC6CA   96b0       SUB     SP, SP, #0x58 ; 'X'
0xBC6CC   394b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xBC6D6)
0xBC6CE   0121       MOVS    R1, #1
0xBC6D0   0190       STR     R0, [SP,#0x58+var_54]
0xBC6D2   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xBC6D4   6846       MOV     R0, SP
0xBC6D6   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xBC6D8   0c90       STR     R0, [SP,#0x58+var_28]
0xBC6DA   02a8       ADD     R0, SP, #0x58+fctx; lpfctx
0xBC6DC   0893       STR     R3, [SP,#0x58+fctx.personality]
0xBC6DE   364b       LDR     R3, =(GCC_except_table15_1 - 0xBC6E8)
0xBC6E0   0a97       STR     R7, [SP,#0x58+fctx.jbuf]
0xBC6E2   0fac       ADD     R4, SP, #0x58+var_1C
0xBC6E4   7b44       ADD     R3, PC; GCC_except_table15_1
0xBC6E6   0993       STR     R3, [SP,#0x58+fctx.lsda]
0xBC6E8   344b       LDR     R3, =(loc_BC75C - 0xBC6EE)
0xBC6EA   7b44       ADD     R3, PC; loc_BC75C
```

**Around `0xBC78E` (cited by the Unity code)**

```
0xBC780   a6239b00   MOVS    R3, #0x298
0xBC784   0022       MOVS    R2, #0
0xBC786   e254       STRB    R2, [R4,R3]
0xBC788   124b       LDR     R3, =0x292
0xBC78A   e254       STRB    R2, [R4,R3]
0xBC78C   124a       LDR     R2, =0x40800000
0xBC78E   8823       MOVS    R3, #0x88   <==
0xBC790   e250       STR     R2, [R4,R3]
0xBC792   0123       MOVS    R3, #1
0xBC794   5b42       NEGS    R3, R3
0xBC796   0393       STR     R3, [SP,#0x58+fctx.call_site]
0xBC798   08f04dff   BL      __ZN7DinoCap14subWeaponClassD1Ev; DinoCap::subWeaponClass::~subWeaponClass()
0xBC79C   02a8       ADD     R0, SP, #0x58+fctx; lpfctx
```

