# ComponentAnimation::Load

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-11D7B8` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x11D7B8` |
| RVA (EA - 0x1000) | `0x11C7B8` |
| File offset | `0x11C7B8` |
| Size | 260 code bytes in 1 chunk(s), 118 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap18ComponentAnimation4LoadEPKc` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::ComponentAnimation::Load(char const*)` |
| Bytes SHA-256 | `5797032060194a3bd43cf7b858ca6bdcb6e6f84e5b965218cf7952c585e2206a` |
| Direct code callers | 71 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/NativeComponentAnimation.cs:22`](../../../../../Assets/Scripts/NativeComponentAnimation.cs#L22) (by name)

## Callers and callees (direct code references)

Callers: `sub_6AB6A`, [`menuScene::InitAnim`](../../../functions/dossiers/menuScene/FUN-0C9CE0.md), [`lifeModeScene::OnLoad`](../../../functions/dossiers/lifeModeScene/FUN-0E7202.md)

Callees: `getGamePath`, [`ComponentAnimation::Load`](../../../functions/dossiers/ComponentAnimation/FUN-11D3FE.md), `Triniti2D::FileManager::ReadData`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::~string`

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x228C20` GCC_except_table39_4
- `0x22A15C` __ZStplIcSt11char_traitsIcESaIcEESbIT_T0_T1_ERKS6_PKS3__ptr
- `0x22A1A4` __ZN9Triniti2D12_FileManagerE_ptr
- `0x23BBA0` Triniti2D::_FileManager
- `0x22A214` __ZNSt6vectorIcSaIcEED2Ev_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x11D7B8  f0b5       PUSH    {R4-R7,LR}
0x11D7BA  5e46       MOV     R6, R11
0x11D7BC  5546       MOV     R5, R10
0x11D7BE  4446       MOV     R4, R8
0x11D7C0  70b4       PUSH    {R4-R6}
0x11D7C2  caf08eeb   BLX     ___save_vfp_d8_d15_regs
0x11D7C6  16af       ADD     R7, SP, #arg_38
0x11D7C8  95b0       SUB     SP, SP, #0x54 ; 'T'
0x11D7CA  3c4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x11D7D4)
0x11D7CC  6a46       MOV     R2, SP
0x11D7CE  0d92       STR     R2, [SP,#0x54+var_20]
0x11D7D0  7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x11D7D2  0122       MOVS    R2, #1
0x11D7D4  1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x11D7D6  0290       STR     R0, [SP,#0x54+var_4C]
0x11D7D8  03a8       ADD     R0, SP, #0x54+fctx; lpfctx
0x11D7DA  0993       STR     R3, [SP,#0x54+fctx.personality]
0x11D7DC  384b       LDR     R3, =(GCC_except_table39_4 - 0x11D7E6)
0x11D7DE  0191       STR     R1, [SP,#0x54+var_50]
0x11D7E0  0b97       STR     R7, [SP,#0x54+fctx.jbuf]
0x11D7E2  7b44       ADD     R3, PC; GCC_except_table39_4
0x11D7E4  0a93       STR     R3, [SP,#0x54+fctx.lsda]
0x11D7E6  374b       LDR     R3, =(loc_11D84C - 0x11D7EC)
0x11D7E8  7b44       ADD     R3, PC; loc_11D84C
```

