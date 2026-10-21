# mainScene::CreateEnemy

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0A14B4` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xA14B4` |
| RVA (EA - 0x1000) | `0xA04B4` |
| File offset | `0xA04B4` |
| Size | 212 code bytes in 1 chunk(s), 95 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9mainScene11CreateEnemyEi` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::mainScene::CreateEnemy(int)` |
| Bytes SHA-256 | `26e70e2fe624e03a640331aa4346c3de088b9396e3e7ad409c74a1e380f3a42d` |
| Direct code callers | 19 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: `sub_A2674`, `sub_A267A`, `Triniti2D::GameObject::GetCallBack`, `___save_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `___dynamic_cast`

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x226996` GCC_except_table31_0
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A2B8` __ZN7DinoCap13g_player_typeE_ptr
- `0x23B86C` DinoCap::g_player_type
- `0x22A0A8` __ZTIN9Triniti2D18GameObjectCallBackE_ptr
- `0x22A288` __ZTIN7DinoCap17playerNewAniClassE_ptr
- `0x22D338` `typeinfo for'DinoCap::playerNewAniClass
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22A0CC` __ZTIN7DinoCap11playerClassE_ptr
- `0x22C8B0` `typeinfo for'DinoCap::playerClass

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xA14B4   f0b5       PUSH    {R4-R7,LR}
0xA14B6   5e46       MOV     R6, R11
0xA14B8   5546       MOV     R5, R10
0xA14BA   4446       MOV     R4, R8
0xA14BC   70b4       PUSH    {R4-R6}
0xA14BE   46f110ed   BLX     ___save_vfp_d8_d15_regs
0xA14C2   16af       ADD     R7, SP, #arg_38
0xA14C4   d7b0       SUB     SP, SP, #0x15C; int
0xA14C6   e64b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xA14D0)
0xA14C8   2790       STR     R0, [SP,#0x15C+var_C0]; int
0xA14CA   0122       MOVS    R2, #1
0xA14CC   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xA14CE   6846       MOV     R0, SP
0xA14D0   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xA14D2   4790       STR     R0, [SP,#0x15C+var_40]
0xA14D4   3da8       ADD     R0, SP, #0x15C+fctx; lpfctx
0xA14D6   4393       STR     R3, [SP,#0x15C+fctx.personality]
0xA14D8   e24b       LDR     R3, =(GCC_except_table31_0 - 0xA14E2)
0xA14DA   2691       STR     R1, [SP,#0x15C+var_C4]; int
0xA14DC   4597       STR     R7, [SP,#0x15C+fctx.jbuf]
0xA14DE   7b44       ADD     R3, PC; GCC_except_table31_0
0xA14E0   4493       STR     R3, [SP,#0x15C+fctx.lsda]
0xA14E2   e14b       LDR     R3, =(loc_A15EC - 0xA14E8)
0xA14E4   7b44       ADD     R3, PC; loc_A15EC
```

