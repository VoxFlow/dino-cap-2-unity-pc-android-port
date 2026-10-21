# CAFrame::Clone

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-00C2DA` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xC2DA` |
| RVA (EA - 0x1000) | `0xB2DA` |
| File offset | `0xB2DA` |
| Size | 272 code bytes in 1 chunk(s), 125 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap7CAFrame5CloneEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::CAFrame::Clone(void)` |
| Bytes SHA-256 | `80a843f8453a0f52813848b3c0a3bb141a46b97c624f78a01ec2df2cf846ea8d` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/SelectBank.cs:24`](../../../../../Assets/Scripts/SelectBank.cs#L24) cites `0xC350`

## Callers and callees (direct code references)

Callers: [`CombineAnimation::SetToAdv`](../../../functions/dossiers/CombineAnimation/FUN-00EEFE.md)

Callees: `CAFrame::CAFrame`, [`CASprite::Clone`](../../../functions/dossiers/CASprite/FUN-00B6DE.md), `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `operator new`

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x223A7C` GCC_except_table19
- `0x22A154` __ZNKSt6vectorIPN7DinoCap8CASpriteESaIS2_EE4sizeEv_ptr
- `0x22A198` __ZNSt3mapISsPN7DinoCap8CASpriteESt4lessISsESaISt4pairIKSsS2_EEEixERS6__ptr
- `0x22A1C8` __ZNSt6vectorIPN7DinoCap8CASpriteESaIS2_EE9push_backERKS2__ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xC2DA    f0b5       PUSH    {R4-R7,LR}
0xC2DC    5e46       MOV     R6, R11
0xC2DE    5546       MOV     R5, R10
0xC2E0    4446       MOV     R4, R8
0xC2E2    70b4       PUSH    {R4-R6}
0xC2E4    dbf1fced   BLX     ___save_vfp_d8_d15_regs
0xC2E8    16af       ADD     R7, SP, #arg_38
0xC2EA    95b0       SUB     SP, SP, #0x54 ; 'T'
0xC2EC    3f4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xC2F6)
0xC2EE    0122       MOVS    R2, #1
0xC2F0    6946       MOV     R1, SP
0xC2F2    7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xC2F4    0290       STR     R0, [SP,#0x54+var_4C]
0xC2F6    1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xC2F8    06a8       ADD     R0, SP, #0x54+fctx; lpfctx
0xC2FA    1091       STR     R1, [SP,#0x54+var_14]
0xC2FC    0c93       STR     R3, [SP,#0x54+fctx.personality]
0xC2FE    3c4b       LDR     R3, =(GCC_except_table19 - 0xC306)
0xC300    0e97       STR     R7, [SP,#0x54+fctx.jbuf]
0xC302    7b44       ADD     R3, PC; GCC_except_table19
0xC304    0d93       STR     R3, [SP,#0x54+fctx.lsda]
0xC306    3b4b       LDR     R3, =(loc_C38C - 0xC30C)
0xC308    7b44       ADD     R3, PC; loc_C38C
0xC30A    1343       ORRS    R3, R2
```

**Around `0xC350` (cited by the Unity code)**

```
0xC344    029b       LDR     R3, [SP,#0x54+var_4C]
0xC346    0499       LDR     R1, [SP,#0x54+var_44]
0xC348    14a8       ADD     R0, SP, #0x54+var_4; this
0xC34A    9a69       LDR     R2, [R3,#0x18]
0xC34C    8b00       LSLS    R3, R1, #2
0xC34E    9b58       LDR     R3, [R3,R2]
0xC350    0593       STR     R3, [SP,#0x54+var_40]   <==
0xC352    191c       MOVS    R1, R3
0xC354    0123       MOVS    R3, #1
0xC356    5b42       NEGS    R3, R3
0xC358    2031       ADDS    R1, #0x20 ; ' '; std::string *
0xC35A    0793       STR     R3, [SP,#0x54+fctx.call_site]
0xC35C    ddf122ec   BLX     __ZNSsC1ERKSs; std::string::string(std::string const&)
```

