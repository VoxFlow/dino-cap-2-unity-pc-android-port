# CASprite::LoadXml

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-00BDB4` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xBDB4` |
| RVA (EA - 0x1000) | `0xADB4` |
| File offset | `0xADB4` |
| Size | 752 code bytes in 1 chunk(s), 313 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap8CASprite7LoadXmlEPNS_9TiXmlNodeE` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::CASprite::LoadXml(DinoCap::TiXmlNode *)` |
| Bytes SHA-256 | `d3649ca9cca72f70e38ca6261e96c03f33f50f0555fa0f4b5ade5658569c52fc` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: [`CAFrame::LoadXml`](../../../functions/dossiers/CAFrame/FUN-00F3A4.md)

Callees: `TiXmlElement::Attribute`, `TiXmlNode::FirstChildElement`, `TiXmlElement::GetText`, `Triniti2D::TextureManager::GetFrameSize`, `Triniti2D::TextureManager::GetFrameId`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::compare`, `std::string::assign`, `std::string::~string`, `___gesf2vfp`, `___mulsf3vfp`, `___truncdfsf2vfp`, `___unordsf2vfp`, `_atof`, `_atoi`, `_sscanf`, `_strlen`

## Strings it references

- `0x2011C8` "name"
- `0x2011D0` "x"
- `0x2011D4` "y"
- `0x2011D8` "width"
- `0x2011E0` "height"
- `0x2011E8` "offsetX"
- `0x2011F0` "offsetY"
- `0x2011F8` "rotation"
- `0x201204` "layer"
- `0x20120C` "color"
- `0x201214` "%x"
- `0x201218` "image"
- `0x201184` "Frame"
- `0x2011C0` "yingzi"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x223A6A` GCC_except_table16
- `0x2011C8` sel_name
- `0x2011D0` sel_x
- `0x2011D4` sel_y
- `0x2011D8` sel_width
- `0x2011E0` sel_height
- `0x201204` sel_layer_0
- `0x22A194` __ZN7DinoCap7g_scaleE_ptr
- `0x23B8C8` DinoCap::g_scale
- `0x201218` sel_image
- `0x22A124` __ZN9Triniti2D15_TextureManagerE_ptr
- `0x23BBA4` Triniti2D::_TextureManager
- `0x22A15C` __ZStplIcSt11char_traitsIcESaIcEESbIT_T0_T1_ERKS6_PKS3__ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xBDB4    f0b5       PUSH    {R4-R7,LR}
0xBDB6    5e46       MOV     R6, R11
0xBDB8    5546       MOV     R5, R10
0xBDBA    4446       MOV     R4, R8
0xBDBC    70b4       PUSH    {R4-R6}
0xBDBE    dcf190e8   BLX     ___save_vfp_d8_d15_regs
0xBDC2    16af       ADD     R7, SP, #arg_38
0xBDC4    a5b0       SUB     SP, SP, #0x94
0xBDC6    b74b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xBDD0)
0xBDC8    0990       STR     R0, [SP,#0x94+var_70]
0xBDCA    0891       STR     R1, [SP,#0x94+var_74]
0xBDCC    7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xBDCE    0121       MOVS    R1, #1
0xBDD0    1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xBDD2    6846       MOV     R0, SP
0xBDD4    1e90       STR     R0, [SP,#0x94+var_1C]
0xBDD6    1a93       STR     R3, [SP,#0x94+fctx.personality]
0xBDD8    b34b       LDR     R3, =(GCC_except_table16 - 0xBDE2)
0xBDDA    14a8       ADD     R0, SP, #0x94+fctx; lpfctx
0xBDDC    1c97       STR     R7, [SP,#0x94+fctx.jbuf]
0xBDDE    7b44       ADD     R3, PC; GCC_except_table16
0xBDE0    1b93       STR     R3, [SP,#0x94+fctx.lsda]
0xBDE2    b24b       LDR     R3, =(loc_BFBE - 0xBDEC)
0xBDE4    0124       MOVS    R4, #1
```

