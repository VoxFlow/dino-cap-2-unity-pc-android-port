# playerTypeProperty::loadFromXml

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0C5E26` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xC5E26` |
| RVA (EA - 0x1000) | `0xC4E26` |
| File offset | `0xC4E26` |
| Size | 872 code bytes in 1 chunk(s), 399 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap18playerTypeProperty11loadFromXmlEPKc` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerTypeProperty::loadFromXml(char const*)` |
| Bytes SHA-256 | `e9a120174ca4cb237a1453cfd37d79b56115330784cbbb50aa934399c7d3b763` |
| Direct code callers | 2 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.hero.fresh_levels.r7` (PROVEN, NATIVE): playerTypeProperty::loadFromXml0xc5e26 reads five shipped Player.xml rows. Pushlevel-1 perrow0xc5fb6..c5fca and exp0, then firstlevel0 at0xc6108. Called startup0xc0d2x and ResetFile0x59642. Thus fresh/reset levels0,-1,-1,-1,-1 and five exp0. Historical pilot four zero-level slots WRONG, patched. Legacy JSON four-slot migration preserves existing values and adds locked fifth slot as PLATFORM_ADAPTER; it is not native ...
- Rule `dc2.data.player_weapon_siblings.r13` (PROVEN, NATIVE): Player XML loader0xc5ff0 starts weapon child; unfiltered NextSiblingElement0xc607c includes trailing jinweapon. Six records per shipped hero; missing ammo attributes preserve previous slot values via QueryIntAttribute no-write missing branch. Per record ammo0/level-1, first ammo200/level0. Clarification r14: level-1 in this earlier wording is ownership vector+84; upgrade vector+108 initializes1. Never conflate them.

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/Dc2GameData.cs:12`](../../../../../Assets/Scripts/Dc2GameData.cs#L12) cites `0xC5FB6`
- [`Assets/Scripts/Dc2GameData.cs:93`](../../../../../Assets/Scripts/Dc2GameData.cs#L93) cites `0xC607C`
- [`Assets/Scripts/Dc2Scene.cs:21`](../../../../../Assets/Scripts/Dc2Scene.cs#L21) cites `0xC5FB6`

## Callers and callees (direct code references)

Callers: [`ResetFile`](../../../functions/dossiers/DinoCap/FUN-05960A.md), `testApp::OnLoad`

Callees: `getGamePath`, `TiXmlNode::NextSiblingElement`, `TiXmlNode::FirstChildElement`, `TiXmlElement::QueryIntAttribute`, `TiXmlDocument::TiXmlDocument`, `TiXmlDocument::Parse`, `playerStateInfo::Reset`, `playerTypeClass::playerTypeClass`, `playerTypeClass::~playerTypeClass`, `Triniti2D::FileManager::ReadData`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::append`, `std::string::~string`, `_strlen`

## Strings it references

- `0x207A44` "Image/Conf/"
- `0x208FFC` "config"
- `0x209004` "player"
- `0x20900C` "id"
- `0x209010` "hire"
- `0x209018` "hp"
- `0x20901C` "df"
- `0x209020` "baseexp"
- `0x209028` "weapon"
- `0x209030` "type"
- `0x209038` "subtype"
- `0x209040` "curlevel"
- `0x20904C` "maxlevel"
- `0x209058` "ammoprice"
- `0x209064` "ammoadd"
- `0x20906C` "jinweapon"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x227338` GCC_except_table39_0
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A1A4` __ZN9Triniti2D12_FileManagerE_ptr
- `0x23BBA0` Triniti2D::_FileManager
- `0x22A148` __ZNSt6vectorIcSaIcEE9push_backERKc_ptr
- `0x22A9B0` __ZSt8_DestroyIPN7DinoCap15playerTypeClassEEvT_S3__ptr
- `0x22A2DC` __ZN7DinoCap13g_prop_PlayerE_ptr
- `0x240474` DinoCap::g_prop_Player
- `0x22A5E4` __ZNSt6vectorIiSaIiEE9push_backERKi_ptr
- `0x22A990` __ZNSt6vectorIN7DinoCap14subWeaponClassESaIS1_EE9push_backERKS1__ptr
- `0x22A988` __ZNSt6vectorIN7DinoCap15playerTypeClassESaIS1_EE9push_backERKS1__ptr
- `0x22A13C` __ZN7DinoCap13TiXmlDocumentD2Ev_ptr
- `0x22A214` __ZNSt6vectorIcSaIcEED2Ev_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xC5E26   f0b5       PUSH    {R4-R7,LR}
0xC5E28   5e46       MOV     R6, R11
0xC5E2A   5546       MOV     R5, R10
0xC5E2C   4446       MOV     R4, R8
0xC5E2E   70b4       PUSH    {R4-R6}
0xC5E30   22f156e8   BLX     ___save_vfp_d8_d15_regs
0xC5E34   16af       ADD     R7, SP, #arg_38
0xC5E36   dcb0       SUB     SP, SP, #0x170
0xC5E38   d54b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xC5E42)
0xC5E3A   0122       MOVS    R2, #1
0xC5E3C   0890       STR     R0, [SP,#0x170+var_150]
0xC5E3E   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xC5E40   0791       STR     R1, [SP,#0x170+__s]
0xC5E42   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xC5E44   6946       MOV     R1, SP
0xC5E46   0ea8       ADD     R0, SP, #0x170+fctx; lpfctx
0xC5E48   1493       STR     R3, [SP,#0x170+fctx.personality]
0xC5E4A   d24b       LDR     R3, =(GCC_except_table39_0 - 0xC5E54)
0xC5E4C   1891       STR     R1, [SP,#0x170+var_110]
0xC5E4E   1697       STR     R7, [SP,#0x170+fctx.jbuf]
0xC5E50   7b44       ADD     R3, PC; GCC_except_table39_0
0xC5E52   1593       STR     R3, [SP,#0x170+fctx.lsda]
0xC5E54   d04b       LDR     R3, =(loc_C60C6 - 0xC5E5A)
0xC5E56   7b44       ADD     R3, PC; loc_C60C6
```

**Around `0xC5FB6` (cited by the Unity code)**

```
0xC5FA8   fcf7faf8   BL      __ZNK7DinoCap12TiXmlElement17QueryIntAttributeEPKcPi; DinoCap::TiXmlElement::QueryIntAttribute(char const*,int *)
0xC5FAC   8a4a       LDR     R2, =(__ZN7DinoCap13g_prop_PlayerE_ptr - 0xC5FB8)
0xC5FAE   0123       MOVS    R3, #1
0xC5FB0   5b42       NEGS    R3, R3
0xC5FB2   0592       STR     R2, [SP,#0x170+var_15C]
0xC5FB4   7a44       ADD     R2, PC; __ZN7DinoCap13g_prop_PlayerE_ptr
0xC5FB6   5993       STR     R3, [SP,#0x170+var_C]   <==
0xC5FB8   1368       LDR     R3, [R2]; DinoCap::g_prop_Player ...
0xC5FBA   59a9       ADD     R1, SP, #0x170+var_C
0xC5FBC   181c       MOVS    R0, R3
0xC5FBE   0593       STR     R3, [SP,#0x170+var_15C]
0xC5FC0   864b       LDR     R3, =(__ZNSt6vectorIiSaIiEE9push_backERKi_ptr - 0xC5FC8)
0xC5FC2   2030       ADDS    R0, #0x20 ; ' '
```

**Around `0xC607C` (cited by the Unity code)**

```
0xC6070   9847       BLX     R3
0xC6072   36a8       ADD     R0, SP, #0x170+var_98
0xC6074   4da9       ADD     R1, SP, #0x170+var_3C
0xC6076   039c       LDR     R4, [SP,#0x170+var_164]
0xC6078   a047       BLX     R4
0xC607A   0a98       LDR     R0, [SP,#0x170+var_148]; this
0xC607C   fbf796fe   BL      __ZNK7DinoCap9TiXmlNode18NextSiblingElementEv; DinoCap::TiXmlNode::NextSiblingElement(void)   <==
0xC6080   0a90       STR     R0, [SP,#0x170+var_148]
0xC6082   0a99       LDR     R1, [SP,#0x170+var_148]
0xC6084   0029       CMP     R1, #0
0xC6086   b6d1       BNE     loc_C5FF6
0xC6088   339b       LDR     R3, [SP,#0x170+var_A4]
0xC608A   c822       MOVS    R2, #0xC8
```

