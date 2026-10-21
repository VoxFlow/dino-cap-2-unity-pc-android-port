# bossN::Attack

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-120AC4` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x120AC4` |
| RVA (EA - 0x1000) | `0x11FAC4` |
| File offset | `0x11FAC4` |
| Size | 396 code bytes in 1 chunk(s), 176 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap5bossN6AttackEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::bossN::Attack(void)` |
| Bytes SHA-256 | `bc9c91aee1d772cac326295c0fcf363206084244d332bb2f9e36630eee18f609` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameBoss.cs:194`](../../../../../Assets/Scripts/GameBoss.cs#L194) cites `0x120AC4`

## Callers and callees (direct code references)

Callers: [`bossN::OnTimeout`](../../../functions/dossiers/bossN/FUN-1217A8.md)

Callees: [`CompAnimObj::PlayAnimation`](../../../functions/dossiers/CompAnimObj/FUN-119010.md), [`CompAnimObj::SetAnimation`](../../../functions/dossiers/CompAnimObj/FUN-1198AC.md), `Triniti2D::GameObject::Physics`, `Triniti2D::GamePhysics::SetLinearVelocity`, `Triniti2D::GamePhysics::WakeUp`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`

## Strings it references

- `0x20B0D4` "HouAni"
- `0x20B0DC` "WeiAni"
- `0x20B0E4` "JiaoAni"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x228C76` GCC_except_table8_12
- `0x22AA78` __ZN7DinoCap15g_ca_boss_hou02E_ptr
- `0x23F078` DinoCap::g_ca_boss_hou02
- `0x22AAF4` __ZN7DinoCap15g_ca_boss_wei03E_ptr
- `0x23F244` DinoCap::g_ca_boss_wei03
- `0x22AAE8` __ZN7DinoCap16g_ca_boss_jiao02E_ptr
- `0x23F46C` DinoCap::g_ca_boss_jiao02

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x120AC4  f0b5       PUSH    {R4-R7,LR}
0x120AC6  5e46       MOV     R6, R11
0x120AC8  5546       MOV     R5, R10
0x120ACA  4446       MOV     R4, R8
0x120ACC  70b4       PUSH    {R4-R6}
0x120ACE  c7f008ea   BLX     ___save_vfp_d8_d15_regs
0x120AD2  16af       ADD     R7, SP, #arg_38
0x120AD4  98b0       SUB     SP, SP, #0x60 ; '`'
0x120AD6  5e4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x120AE0)
0x120AD8  0122       MOVS    R2, #1
0x120ADA  6946       MOV     R1, SP
0x120ADC  7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x120ADE  0490       STR     R0, [SP,#0x60+var_50]
0x120AE0  1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x120AE2  05a8       ADD     R0, SP, #0x60+fctx; lpfctx
0x120AE4  0f91       STR     R1, [SP,#0x60+var_24]
0x120AE6  0b93       STR     R3, [SP,#0x60+fctx.personality]
0x120AE8  5a4b       LDR     R3, =(GCC_except_table8_12 - 0x120AF0)
0x120AEA  0d97       STR     R7, [SP,#0x60+fctx.jbuf]
0x120AEC  7b44       ADD     R3, PC; GCC_except_table8_12
0x120AEE  0c93       STR     R3, [SP,#0x60+fctx.lsda]
0x120AF0  594b       LDR     R3, =(loc_120BB4 - 0x120AF6)
0x120AF2  7b44       ADD     R3, PC; loc_120BB4
0x120AF4  1343       ORRS    R3, R2
```

