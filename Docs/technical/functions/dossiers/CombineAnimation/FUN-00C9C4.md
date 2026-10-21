# CombineAnimation::ChangeImageK5

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-00C9C4` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xC9C4` |
| RVA (EA - 0x1000) | `0xB9C4` |
| File offset | `0xB9C4` |
| Size | 646 code bytes in 1 chunk(s), 293 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap16CombineAnimation13ChangeImageK5EPKcS2_` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::CombineAnimation::ChangeImageK5(char const*,char const*)` |
| Bytes SHA-256 | `5df93d6261b558cc6bf5b28822b2a17f386f50ef41d6f6974212e6977e04d4d0` |
| Direct code callers | 10 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/NativeCaAnimation.cs:113`](../../../../../Assets/Scripts/NativeCaAnimation.cs#L113) cites `0xC9C4`
- [`Assets/Scripts/NativeDinoChangeK5.cs:9`](../../../../../Assets/Scripts/NativeDinoChangeK5.cs#L9) cites `0xC9C4`

## Callers and callees (direct code references)

Callers: `sub_E828`, `sub_E8C2`, `sub_E95C`, `sub_EBBC`, `sub_EC56`

Callees: `Triniti2D::TextureManager::GetFrameId`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::substr`, `std::string::compare`, `std::string::append`, `std::string::assign`, `std::string::string`, `std::string::~string`

## Strings it references

- `0x201220` "chibang2"
- `0x20122C` "2"
- `0x201230` "chibang1"
- `0x20123C` "1"
- `0x201240` "3"
- `0x201184` "Frame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x223A92` GCC_except_table22
- `0x22A120` __ZNKSt6vectorIPN7DinoCap7CAFrameESaIS2_EE4sizeEv_ptr
- `0x22A12C` __ZNSt8_Rb_treeISsSt4pairIKSsPN7DinoCap8CASpriteEESt10_Select1stIS5_ESt4lessISsESaIS5_EE4findERS1__ptr
- `0x22A15C` __ZStplIcSt11char_traitsIcESaIcEESbIT_T0_T1_ERKS6_PKS3__ptr
- `0x22A124` __ZN9Triniti2D15_TextureManagerE_ptr
- `0x23BBA4` Triniti2D::_TextureManager

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xC9C4    f0b5       PUSH    {R4-R7,LR}
0xC9C6    5e46       MOV     R6, R11
0xC9C8    5546       MOV     R5, R10
0xC9CA    4446       MOV     R4, R8
0xC9CC    70b4       PUSH    {R4-R6}
0xC9CE    dbf188ea   BLX     ___save_vfp_d8_d15_regs
0xC9D2    16af       ADD     R7, SP, #arg_38
0xC9D4    9fb0       SUB     SP, SP, #0x7C ; '|'
0xC9D6    9d4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xC9E0)
0xC9D8    0392       STR     R2, [SP,#0x7C+var_70]
0xC9DA    0122       MOVS    R2, #1
0xC9DC    7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xC9DE    0590       STR     R0, [SP,#0x7C+var_68]
0xC9E0    1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xC9E2    0491       STR     R1, [SP,#0x7C+var_6C]
0xC9E4    09a8       ADD     R0, SP, #0x7C+fctx; lpfctx
0xC9E6    0f93       STR     R3, [SP,#0x7C+fctx.personality]
0xC9E8    994b       LDR     R3, =(GCC_except_table22 - 0xC9F2)
0xC9EA    6946       MOV     R1, SP
0xC9EC    1391       STR     R1, [SP,#0x7C+var_30]
0xC9EE    7b44       ADD     R3, PC; GCC_except_table22
0xC9F0    1093       STR     R3, [SP,#0x7C+fctx.lsda]
0xC9F2    984b       LDR     R3, =(loc_CB6E - 0xC9FA)
0xC9F4    1197       STR     R7, [SP,#0x7C+fctx.jbuf]
```

