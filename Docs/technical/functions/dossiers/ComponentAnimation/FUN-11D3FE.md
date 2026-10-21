# ComponentAnimation::Load

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-11D3FE` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x11D3FE` |
| RVA (EA - 0x1000) | `0x11C3FE` |
| File offset | `0x11C3FE` |
| Size | 882 code bytes in 1 chunk(s), 403 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap18ComponentAnimation4LoadERKSt6vectorIcSaIcEE` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::ComponentAnimation::Load(std::vector<char,std::allocator<char>> const&)` |
| Bytes SHA-256 | `b3ec098063b8b4c8743d93a7b867d35ad2f9b5b6ef297b6be59a7f8f2171e647` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/NativeComponentAnimation.cs:22`](../../../../../Assets/Scripts/NativeComponentAnimation.cs#L22) cites `0x11D3FE`

## Callers and callees (direct code references)

Callers: [`ComponentAnimation::Load`](../../../functions/dossiers/ComponentAnimation/FUN-11D7B8.md)

Callees: `CALibrary::Instance`, `ReadString`, `CALibrary::GetAnimLib`, `CAComponent::UpdateTexture`, [`CAComponent::Load`](../../../functions/dossiers/CAComponent/FUN-11CAE0.md), [`ComponentAnimation::Clear`](../../../functions/dossiers/ComponentAnimation/FUN-11CD00.md), `CAComponent::CAComponent`, `ComponentAnimation::CalcFrameRate`, `ComponentAnimation::UpdateDrawOrder`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `operator delete`, `operator new`

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x228C04` GCC_except_table38_4
- `0x22A1E4` __ZN9Triniti2D15BlockReadBuffer4readEPcm_ptr
- `0x22A300` __ZStplIcSt11char_traitsIcESaIcEESbIT_T0_T1_ERKS6_S8__ptr
- `0x22B0E8` __ZNSt3mapISsPN7DinoCap11CAComponentESt4lessISsESaISt4pairIKSsS2_EEEixERS6__ptr
- `0x22B0B0` __ZNSt4pairISsSsEC2ERKSsS2__ptr
- `0x22B0BC` __ZNSt4pairIKSsSsEC2ISsSsEERKS_IT_T0_E_ptr
- `0x22AFB4` __ZNSt8_Rb_treeISsSt4pairIKSsSsESt10_Select1stIS2_ESt4lessISsESaIS2_EE16_M_insert_uniqueERKS2__ptr
- `0x22AE94` __ZNSt4pairIKSsSsED2Ev_ptr
- `0x22AFA8` __ZNSt4pairISsSsED2Ev_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x11D3FE  f0b5       PUSH    {R4-R7,LR}
0x11D400  5e46       MOV     R6, R11
0x11D402  5546       MOV     R5, R10
0x11D404  4446       MOV     R4, R8
0x11D406  70b4       PUSH    {R4-R6}
0x11D408  caf06aed   BLX     ___save_vfp_d8_d15_regs
0x11D40C  16af       ADD     R7, SP, #arg_38
0x11D40E  acb0       SUB     SP, SP, #0xB0
0x11D410  d74b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x11D41A)
0x11D412  0122       MOVS    R2, #1
0x11D414  0390       STR     R0, [SP,#0xB0+var_A4]
0x11D416  7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x11D418  0291       STR     R1, [SP,#0xB0+var_A8]
0x11D41A  1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x11D41C  6946       MOV     R1, SP
0x11D41E  0aa8       ADD     R0, SP, #0xB0+fctx; lpfctx
0x11D420  1093       STR     R3, [SP,#0xB0+fctx.personality]
0x11D422  d44b       LDR     R3, =(GCC_except_table38_4 - 0x11D42C)
0x11D424  1491       STR     R1, [SP,#0xB0+var_60]
0x11D426  1297       STR     R7, [SP,#0xB0+fctx.jbuf]
0x11D428  7b44       ADD     R3, PC; GCC_except_table38_4
0x11D42A  1193       STR     R3, [SP,#0xB0+fctx.lsda]
0x11D42C  d24b       LDR     R3, =(loc_11D680 - 0x11D432)
0x11D42E  7b44       ADD     R3, PC; loc_11D680
```

