# lifeModeScene::InitRain

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0E6BC4` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xE6BC4` |
| RVA (EA - 0x1000) | `0xE5BC4` |
| File offset | `0xE5BC4` |
| Size | 1524 code bytes in 1 chunk(s), 625 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap13lifeModeScene8InitRainEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::lifeModeScene::InitRain(void)` |
| Bytes SHA-256 | `9fb67710a00cb6aa44bdc303d7cd47513d1d23ed193a4ffba4dd94cf63aab445` |
| Direct code callers | 2 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | GAMEPLAY / PORTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: twin of mainScene::InitRain 0xa2e50: same emitters raineff/rainhua/raineff2, layers 15/3/7, pos (0,S*180)/(0,0)/(0,S*160), sizes (S*525,S*33)/(S*525,S*100)/(S*525,S*33); lifeMode reuses registers instead of reloading 33/160 (literal count differs, values equal). GameWeather.InitRain

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: `sub_EFBD6`

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
- `0x227B6A` GCC_except_table37_1
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
0xE6BC4   f0b5       PUSH    {R4-R7,LR}
0xE6BC6   5e46       MOV     R6, R11
0xE6BC8   5546       MOV     R5, R10
0xE6BCA   4446       MOV     R4, R8
0xE6BCC   70b4       PUSH    {R4-R6}
0xE6BCE   01f188e9   BLX     ___save_vfp_d8_d15_regs
0xE6BD2   16af       ADD     R7, SP, #arg_38
0xE6BD4   c9b0       SUB     SP, SP, #0x124
0xE6BD6   ec4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xE6BE0)
0xE6BD8   6a46       MOV     R2, SP
0xE6BDA   2492       STR     R2, [SP,#0x124+var_94]
0xE6BDC   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xE6BDE   0122       MOVS    R2, #1
0xE6BE0   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xE6BE2   1090       STR     R0, [SP,#0x124+var_E4]
0xE6BE4   1aa8       ADD     R0, SP, #0x124+fctx; lpfctx
0xE6BE6   2093       STR     R3, [SP,#0x124+fctx.personality]
0xE6BE8   e84b       LDR     R3, =(GCC_except_table37_1 - 0xE6BF0)
0xE6BEA   2297       STR     R7, [SP,#0x124+fctx.jbuf]
0xE6BEC   7b44       ADD     R3, PC; GCC_except_table37_1
0xE6BEE   2193       STR     R3, [SP,#0x124+fctx.lsda]
0xE6BF0   e74b       LDR     R3, =(loc_E70D2 - 0xE6BF6)
0xE6BF2   7b44       ADD     R3, PC; loc_E70D2
0xE6BF4   1343       ORRS    R3, R2
```

