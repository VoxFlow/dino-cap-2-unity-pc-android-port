# loadImageByConf

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-059AD6` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x59AD6` |
| RVA (EA - 0x1000) | `0x58AD6` |
| File offset | `0x58AD6` |
| Size | 1798 code bytes in 1 chunk(s), 754 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap15loadImageByConfEPKcb` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::loadImageByConf(char const*,bool)` |
| Bytes SHA-256 | `05a1eb6e34c1eeac204b4a7b5a01c72fa73854d6029691e5b9ad132744d60b2c` |
| Direct code callers | 49 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.asset.conf.format` (PROVEN, NATIVE): A .conf atlas file is text: '[Frame]', 'Count = N', then N lines 'FrameK = (name x y w h)' with integer x,y,w,h parsed by atoi; each becomes a frame named name+'Frame' with Rect(x,y,w,h) on texture <dir>/<base>Image? loaded as .pvr when flag set else .png. Consumer: DinoCap::loadImageByConf @0x59ad6.

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: `sub_65B54`, [`mainScene::OnLoad`](../../../functions/dossiers/mainScene/FUN-0A7434.md), `sub_B1D22`, [`menuScene::startUI`](../../../functions/dossiers/menuScene/FUN-0CA2A4.md), [`lifeModeScene::OnLoad`](../../../functions/dossiers/lifeModeScene/FUN-0E7202.md), `sub_EF248`, [`selectScene::OnLoad`](../../../functions/dossiers/selectScene/FUN-10600A.md), `sub_10897A`, `sub_109870`

Callees: `getGamePath`, `dataToString`, [`CreateFrame`](../../../functions/dossiers/DinoCap/FUN-0174DE.md), `CreateTexture`, `Triniti2D::FileManager::ReadData`, `Triniti2D::TextureManager::CheckTexture`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::find_first_of`, `std::string::find`, `std::string::rfind`, `std::string::substr`, `std::string::assign`, `std::string::assign`, `std::string::string`, `std::string::~string`, `___floatsisfvfp`, `_atoi`

## Strings it references

- `0x20473C` "Image"
- `0x204744` ".pvr"
- `0x20474C` ".png"
- `0x204754` ".conf"
- `0x20475C` "Count = "
- `0x2011BC` "
"
- `0x204768` "("
- `0x20476C` ")"
- `0x201184` "Frame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x224E09` GCC_except_table29
- `0x22A15C` __ZStplIcSt11char_traitsIcESaIcEESbIT_T0_T1_ERKS6_PKS3__ptr
- `0x22A124` __ZN9Triniti2D15_TextureManagerE_ptr
- `0x23BBA4` Triniti2D::_TextureManager
- `0x22A114` __ZNSs4_Rep20_S_empty_rep_storageE_ptr
- `0x241588` std::string::_Rep::_S_empty_rep_storage
- `0x22A300` __ZStplIcSt11char_traitsIcESaIcEESbIT_T0_T1_ERKS6_S8__ptr
- `0x22A1A4` __ZN9Triniti2D12_FileManagerE_ptr
- `0x23BBA0` Triniti2D::_FileManager
- `0x22A328` __ZNSt6vectorIcSaIcEEC2ERKS1__ptr
- `0x22A214` __ZNSt6vectorIcSaIcEED2Ev_ptr
- `0x23B905` DinoCap::g_iphone_retina

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x59AD6   f0b5       PUSH    {R4-R7,LR}
0x59AD8   5e46       MOV     R6, R11
0x59ADA   5546       MOV     R5, R10
0x59ADC   4446       MOV     R4, R8
0x59ADE   70b4       PUSH    {R4-R6}
0x59AE0   8ef1fee9   BLX     ___save_vfp_d8_d15_regs
0x59AE4   16af       ADD     R7, SP, #arg_38
0x59AE6   cab0       SUB     SP, SP, #0x128
0x59AE8   f04b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x59AF2)
0x59AEA   0122       MOVS    R2, #1
0x59AEC   c9b2       UXTB    R1, R1
0x59AEE   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x59AF0   0990       STR     R0, [SP,#0x128+var_104]
0x59AF2   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x59AF4   0891       STR     R1, [SP,#0x128+var_108]
0x59AF6   18a8       ADD     R0, SP, #0x128+fctx; lpfctx
0x59AF8   1e93       STR     R3, [SP,#0x128+fctx.personality]
0x59AFA   ed4b       LDR     R3, =(GCC_except_table29 - 0x59B04)
0x59AFC   6946       MOV     R1, SP
0x59AFE   2291       STR     R1, [SP,#0x128+var_A0]
0x59B00   7b44       ADD     R3, PC; GCC_except_table29
0x59B02   1f93       STR     R3, [SP,#0x128+fctx.lsda]
0x59B04   eb4b       LDR     R3, =(loc_5A03C - 0x59B0C)
0x59B06   2097       STR     R7, [SP,#0x128+fctx.jbuf]
```

