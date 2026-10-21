# CombineAnimation::LoadXmlFromFile

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-00C4DA` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xC4DA` |
| RVA (EA - 0x1000) | `0xB4DA` |
| File offset | `0xB4DA` |
| Size | 476 code bytes in 1 chunk(s), 221 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap16CombineAnimation15LoadXmlFromFileEPKc` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::CombineAnimation::LoadXmlFromFile(char const*)` |
| Bytes SHA-256 | `7091637c71aa7cd3f54b7339e6f26bbd914151e856a56ea5e2183c645434eabb` |
| Direct code callers | 64 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.asset.an2.relation` (LIKELY, NATIVE_DATA): .an2 files are XML keyframe animation text (AnimFrame/Sprite: x,y,width,height,offsetX,offsetY,rotation(deg),layer,color,image); each has a same-path .a2b with more (baked) frames. Where keyframes match, a2b A,B=offsetX,offsetY; C=rotation in radians; packedRGBA=color; D=layer (1866/1941 keyframe parts); ref=image. An2 is NOT always in sync with shipped a2b (1277/1941 full numeric match; eff_fan1 differs) so it is au...

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameCombat.cs:295`](../../../../../Assets/Scripts/GameCombat.cs#L295) cites `0xC4DA`
- [`Assets/Scripts/HeroPreview.cs:181`](../../../../../Assets/Scripts/HeroPreview.cs#L181) (by name)
- [`Assets/Scripts/NativeCaAnimation.cs:25`](../../../../../Assets/Scripts/NativeCaAnimation.cs#L25) cites `0xC60A`

## Callers and callees (direct code references)

Callers: [`loadDinoANI`](../../../functions/dossiers/DinoCap/FUN-017818.md), `sub_69DE6`, `sub_6AB6A`, `sub_6D54A`, [`menuScene::InitAnim`](../../../functions/dossiers/menuScene/FUN-0C9CE0.md)

Callees: `CAFrame::CAFrame`, `CAFrame::~CAFrame`, [`CAFrame::LoadXmlFromFile`](../../../functions/dossiers/CAFrame/FUN-00C404.md), `getGamePath`, `Triniti2D::FileManager::ReadData`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::~string`, `operator delete`, `operator new`, `___gtsf2vfp`, `___ltsf2vfp`

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x223A82` GCC_except_table20
- `0x22A120` __ZNKSt6vectorIPN7DinoCap7CAFrameESaIS2_EE4sizeEv_ptr
- `0x22A15C` __ZStplIcSt11char_traitsIcESaIcEESbIT_T0_T1_ERKS6_PKS3__ptr
- `0x22A1A4` __ZN9Triniti2D12_FileManagerE_ptr
- `0x23BBA0` Triniti2D::_FileManager
- `0x22A1E4` __ZN9Triniti2D15BlockReadBuffer4readEPcm_ptr
- `0x22A228` __ZNSt6vectorIPN7DinoCap7CAFrameESaIS2_EE9push_backERKS2__ptr
- `0x22A214` __ZNSt6vectorIcSaIcEED2Ev_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xC4DA    f0b5       PUSH    {R4-R7,LR}
0xC4DC    5e46       MOV     R6, R11
0xC4DE    5546       MOV     R5, R10
0xC4E0    4446       MOV     R4, R8
0xC4E2    70b4       PUSH    {R4-R6}
0xC4E4    dbf1fcec   BLX     ___save_vfp_d8_d15_regs
0xC4E8    16af       ADD     R7, SP, #arg_38
0xC4EA    a4b0       SUB     SP, SP, #0x90
0xC4EC    724b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xC4F6)
0xC4EE    0122       MOVS    R2, #1
0xC4F0    0490       STR     R0, [SP,#0x90+var_80]
0xC4F2    7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xC4F4    0391       STR     R1, [SP,#0x90+var_84]
0xC4F6    1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xC4F8    6946       MOV     R1, SP
0xC4FA    0da8       ADD     R0, SP, #0x90+fctx; lpfctx
0xC4FC    1393       STR     R3, [SP,#0x90+fctx.personality]
0xC4FE    6f4b       LDR     R3, =(GCC_except_table20 - 0xC508)
0xC500    1597       STR     R7, [SP,#0x90+fctx.jbuf]
0xC502    1791       STR     R1, [SP,#0x90+var_34]
0xC504    7b44       ADD     R3, PC; GCC_except_table20
0xC506    1493       STR     R3, [SP,#0x90+fctx.lsda]
0xC508    6d4b       LDR     R3, =(loc_C652 - 0xC50E)
0xC50A    7b44       ADD     R3, PC; loc_C652
```

**Around `0xC60A` (cited by the Unity code)**

```
0xC5FE    1b68       LDR     R3, [R3]
0xC600    9847       BLX     R3
0xC602    0499       LDR     R1, [SP,#0x90+var_80]
0xC604    089a       LDR     R2, [SP,#0x90+var_70]
0xC606    cb68       LDR     R3, [R1,#0xC]
0xC608    9a42       CMP     R2, R3
0xC60A    00dd       BLE     loc_C60E   <==
0xC60C    ca60       STR     R2, [R1,#0xC]
0xC60E    209b       LDR     R3, [SP,#0x90+var_10]
0xC610    db6a       LDR     R3, [R3,#0x2C]
0xC612    0693       STR     R3, [SP,#0x90+var_78]
0xC614    181c       MOVS    R0, R3
0xC616    049b       LDR     R3, [SP,#0x90+var_80]
```

