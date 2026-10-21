# CombineAnimation::LoadXml

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-00F4B0` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xF4B0` |
| RVA (EA - 0x1000) | `0xE4B0` |
| File offset | `0xE4B0` |
| Size | 494 code bytes in 1 chunk(s), 227 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap16CombineAnimation7LoadXmlEPKc` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::CombineAnimation::LoadXml(char const*)` |
| Bytes SHA-256 | `8eb583a1a7441daba295aef57704569dc8aca9c178f98beed4322be0ce40fead` |
| Direct code callers | 4 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: `sub_69DE6`, `sub_6D54A`, `sub_6D818`

Callees: `CAFrame::CAFrame`, [`CombineAnimation::Reset`](../../../functions/dossiers/CombineAnimation/FUN-00B67C.md), [`CAFrame::LoadXml`](../../../functions/dossiers/CAFrame/FUN-00F3A4.md), `getGamePath`, `TiXmlNode::NextSiblingElement`, `TiXmlNode::FirstChildElement`, `TiXmlDocument::TiXmlDocument`, `TiXmlDocument::Parse`, `Triniti2D::FileManager::ReadData`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::~string`, `operator new`, `___gtsf2vfp`, `___ltsf2vfp`

## Strings it references

- `0x201C1C` "AnimFrame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x223ACC` GCC_except_table28
- `0x22A15C` __ZStplIcSt11char_traitsIcESaIcEESbIT_T0_T1_ERKS6_PKS3__ptr
- `0x22A1A4` __ZN9Triniti2D12_FileManagerE_ptr
- `0x23BBA0` Triniti2D::_FileManager
- `0x22A148` __ZNSt6vectorIcSaIcEE9push_backERKc_ptr
- `0x22A228` __ZNSt6vectorIPN7DinoCap7CAFrameESaIS2_EE9push_backERKS2__ptr
- `0x22A13C` __ZN7DinoCap13TiXmlDocumentD2Ev_ptr
- `0x22A214` __ZNSt6vectorIcSaIcEED2Ev_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xF4B0    f0b5       PUSH    {R4-R7,LR}
0xF4B2    5e46       MOV     R6, R11
0xF4B4    5546       MOV     R5, R10
0xF4B6    4446       MOV     R4, R8
0xF4B8    70b4       PUSH    {R4-R6}
0xF4BA    d8f112ed   BLX     ___save_vfp_d8_d15_regs
0xF4BE    16af       ADD     R7, SP, #arg_38
0xF4C0    aeb0       SUB     SP, SP, #0xB8
0xF4C2    774b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xF4CC)
0xF4C4    6a46       MOV     R2, SP
0xF4C6    1292       STR     R2, [SP,#0xB8+var_70]
0xF4C8    7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xF4CA    0122       MOVS    R2, #1
0xF4CC    1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xF4CE    0290       STR     R0, [SP,#0xB8+var_B0]
0xF4D0    08a8       ADD     R0, SP, #0xB8+fctx; lpfctx
0xF4D2    0e93       STR     R3, [SP,#0xB8+fctx.personality]
0xF4D4    734b       LDR     R3, =(GCC_except_table28 - 0xF4DE)
0xF4D6    0191       STR     R1, [SP,#0xB8+var_B4]
0xF4D8    1097       STR     R7, [SP,#0xB8+fctx.jbuf]
0xF4DA    7b44       ADD     R3, PC; GCC_except_table28
0xF4DC    0f93       STR     R3, [SP,#0xB8+fctx.lsda]
0xF4DE    724b       LDR     R3, =(loc_F60A - 0xF4E6)
0xF4E0    0324       MOVS    R4, #3
```

