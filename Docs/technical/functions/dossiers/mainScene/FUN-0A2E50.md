# mainScene::InitRain

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0A2E50` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xA2E50` |
| RVA (EA - 0x1000) | `0xA1E50` |
| File offset | `0xA1E50` |
| Size | 1524 code bytes in 1 chunk(s), 625 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9mainScene8InitRainEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::mainScene::InitRain(void)` |
| Bytes SHA-256 | `414a7b625a810a09ccebd0a30109f43a9f350c452a624ff4112431d5f104db01` |
| Direct code callers | 2 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameWeather.cs:8`](../../../../../Assets/Scripts/GameWeather.cs#L8) cites `0xA2E50`

## Callers and callees (direct code references)

Callers: `sub_B5DF0`

Callees: `getGamePath`, `TiXmlDocument::TiXmlDocument`, `TiXmlDocument::Parse`, `p2dParticleEmitter::Play`, `p2dParticleEmitter::LoadXml`, `Triniti2D::FileManager::ReadData`, `Triniti2D::GameObject::GetCallBack`, `Triniti2D::GameObject::SetSize`, `Triniti2D::GameObject::SetPosition`, `Triniti2D::Scene::CreateGameObject`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::append`, `std::string::assign`, `std::string::string`, `std::string::~string`, `___dynamic_cast`, `___mulsf3vfp`

## Strings it references

- `0x20724C` "p2dParticleEmitter"
- `0x207A44` "Image/Conf/"
- `0x207A50` "raineff.xml"
- `0x207A5C` "rainhua.xml"
- `0x207A68` "raineff2.xml"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x2269C4` GCC_except_table33_0
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A0A8` __ZTIN9Triniti2D18GameObjectCallBackE_ptr
- `0x22A704` __ZTI18p2dParticleEmitter_ptr
- `0x22D05C` `typeinfo for'p2dParticleEmitter
- `0x2415F0` `vtable for'__cxxabiv1::__vmi_class_type_info
- `0x22A1A4` __ZN9Triniti2D12_FileManagerE_ptr
- `0x23BBA0` Triniti2D::_FileManager
- `0x22A214` __ZNSt6vectorIcSaIcEED2Ev_ptr
- `0x22A13C` __ZN7DinoCap13TiXmlDocumentD2Ev_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xA2E50   f0b5       PUSH    {R4-R7,LR}
0xA2E52   5e46       MOV     R6, R11
0xA2E54   5546       MOV     R5, R10
0xA2E56   4446       MOV     R4, R8
0xA2E58   70b4       PUSH    {R4-R6}
0xA2E5A   45f142e8   BLX     ___save_vfp_d8_d15_regs
0xA2E5E   16af       ADD     R7, SP, #arg_38
0xA2E60   c9b0       SUB     SP, SP, #0x124
0xA2E62   ec4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xA2E6C)
0xA2E64   6a46       MOV     R2, SP
0xA2E66   2492       STR     R2, [SP,#0x124+var_94]
0xA2E68   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xA2E6A   0122       MOVS    R2, #1
0xA2E6C   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xA2E6E   1090       STR     R0, [SP,#0x124+var_E4]
0xA2E70   1aa8       ADD     R0, SP, #0x124+fctx; lpfctx
0xA2E72   2093       STR     R3, [SP,#0x124+fctx.personality]
0xA2E74   e84b       LDR     R3, =(GCC_except_table33_0 - 0xA2E7C)
0xA2E76   2297       STR     R7, [SP,#0x124+fctx.jbuf]
0xA2E78   7b44       ADD     R3, PC; GCC_except_table33_0
0xA2E7A   2193       STR     R3, [SP,#0x124+fctx.lsda]
0xA2E7C   e74b       LDR     R3, =(loc_A335E - 0xA2E82)
0xA2E7E   7b44       ADD     R3, PC; loc_A335E
0xA2E80   1343       ORRS    R3, R2
```

