# CombineAnimation::Expand

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-00F14E` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xF14E` |
| RVA (EA - 0x1000) | `0xE14E` |
| File offset | `0xE14E` |
| Size | 546 code bytes in 1 chunk(s), 247 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap16CombineAnimation6ExpandEf` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::CombineAnimation::Expand(float)` |
| Bytes SHA-256 | `e3be8cbfd752f66ac6a07fde63a434f4ceb5d82e2c453ae4e405a6de28ce8ddd` |
| Direct code callers | 4 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: `sub_69DE6`, `sub_6D54A`, `sub_6D818`

Callees: `CAFrame::CAFrame`, `CAFrame::~CAFrame`, [`CAFrame::Expand`](../../../functions/dossiers/CAFrame/FUN-00F014.md), `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `operator delete`, `operator new`, `___addsf3vfp`, `___divsf3vfp`, `___floatsisfvfp`, `___gtsf2vfp`, `___ltsf2vfp`, `___mulsf3vfp`, `___subsf3vfp`

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x223AC6` GCC_except_table27
- `0x22A188` __ZNKSt6vectorIPN7DinoCap7CAFrameESaIS2_EE5emptyEv_ptr
- `0x22A120` __ZNKSt6vectorIPN7DinoCap7CAFrameESaIS2_EE4sizeEv_ptr
- `0x22A234` __ZNSt6vectorIPN7DinoCap7CAFrameESaIS2_EE4backEv_ptr
- `0x22A228` __ZNSt6vectorIPN7DinoCap7CAFrameESaIS2_EE9push_backERKS2__ptr
- `0x22A1EC` __ZNSt6vectorIPN7DinoCap7CAFrameESaIS2_EEaSERKS4__ptr
- `0x22A158` __ZNSt6vectorIPN7DinoCap7CAFrameESaIS2_EED2Ev_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xF14E    f0b5       PUSH    {R4-R7,LR}
0xF150    5e46       MOV     R6, R11
0xF152    5546       MOV     R5, R10
0xF154    4446       MOV     R4, R8
0xF156    70b4       PUSH    {R4-R6}
0xF158    d8f1c2ee   BLX     ___save_vfp_d8_d15_regs
0xF15C    16af       ADD     R7, SP, #arg_38
0xF15E    a8b0       SUB     SP, SP, #0xA0
0xF160    834b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xF16A)
0xF162    0122       MOVS    R2, #1
0xF164    0790       STR     R0, [SP,#0xA0+var_84]
0xF166    7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xF168    0691       STR     R1, [SP,#0xA0+var_88]
0xF16A    1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xF16C    6946       MOV     R1, SP
0xF16E    17a8       ADD     R0, SP, #0xA0+fctx; lpfctx
0xF170    1d93       STR     R3, [SP,#0xA0+fctx.personality]
0xF172    804b       LDR     R3, =(GCC_except_table27 - 0xF17C)
0xF174    2191       STR     R1, [SP,#0xA0+var_1C]
0xF176    1f97       STR     R7, [SP,#0xA0+fctx.jbuf]
0xF178    7b44       ADD     R3, PC; GCC_except_table27
0xF17A    1e93       STR     R3, [SP,#0xA0+fctx.lsda]
0xF17C    7e4b       LDR     R3, =(loc_F336 - 0xF182)
0xF17E    7b44       ADD     R3, PC; loc_F336
```

