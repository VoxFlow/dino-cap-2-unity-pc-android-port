# loadDinoBMP

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-03B8AC` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x3B8AC` |
| RVA (EA - 0x1000) | `0x3A8AC` |
| File offset | `0x3A8AC` |
| Size | 4008 code bytes in 1 chunk(s), 1401 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11loadDinoBMPEi` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::loadDinoBMP(int)` |
| Bytes SHA-256 | `78c200af4752472b81fb160e6981c8697be7ad243cc1ff1f27561122ac810776` |
| Direct code callers | 2 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/Atlas.cs:41`](../../../../../Assets/Scripts/Atlas.cs#L41) cites `0x3B8AC`

## Callers and callees (direct code references)

Callers: `sub_578A2`, `mainScene::loadDinoTexture`, `lifeModeScene::loadDinoTexture`

Callees: [`CreateFrame`](../../../functions/dossiers/DinoCap/FUN-0174DE.md), `CreateTexture`, `sub_3C854`, `sub_3D764`, `sub_3D76C`, `sub_44F7E`, `sub_58854`, `Triniti2D::TextureManager::CheckTexture`, `___save_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `std::string::string`, `std::string::~string`

## Strings it references

- `0x203B10` "k1hui_hImage"
- `0x203B20` "Image/ip/k1hui_h.png"
- `0x202664` "k3gtxiabaFrame"
- `0x20277C` "k3gtzuaFrame"
- `0x203B38` "hk3shenFrame"
- `0x203B48` "hk3t1Frame"
- `0x203B54` "hk3t2Frame"
- `0x203B60` "hk3t3Frame"
- `0x203B6C` "hk3t4Frame"
- `0x203B78` "hk3t5Frame"
- `0x200D70` "Data"
- `0x203B84` "hk3t6Frame"
- `0x203B90` "hk3tou2Frame"
- `0x203BA0` "hk3xiabaFrame"
- `0x203BB0` "hk3zhuaFrame"
- `0x202610` "k3gtshentiFrame"
- `0x202638` "k3gttouFrame"
- `0x20268C` "k3gttui1Frame"
- `0x2026B4` "k3gttui2Frame"
- `0x2026DC` "k3gttui3Frame"
- `0x202704` "k3gttui4Frame"
- `0x20272C` "k3gttui5Frame"
- `0x202754` "k3gttui6Frame"
- `0x203BC0` "k10body_hImage"
- `0x203BD0` "Image/ip/k10body_h.png"
- `0x200EA0` "k3s1Frame"
- `0x200FB0` "k3s2Frame"
- `0x203BE8` "k3shen2Frame"
- `0x202604` "k3shenFrame"
- `0x202680` "k3t1Frame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x224CEC` GCC_except_table24_0
- `0x23B878` DinoCap::g_curMapID
- `0x23B87C` DinoCap::g_cur_map_data
- `0x2404EC` DinoCap::g_map_info
- `0x22A28C` __ZN7DinoCap8mapClassC2ERKS0__ptr
- `0x23B905` DinoCap::g_iphone_retina
- `0x23B904` DinoCap::g_iphone
- `0x22A124` __ZN9Triniti2D15_TextureManagerE_ptr
- `0x23BBA4` Triniti2D::_TextureManager
- `0x2264D6` GCC_except_table1_5
- `0x224E7B` GCC_except_table2_6
- `0x23D114` __ZN7DinoCapL6g_PathE_5
- `0x240A2C` DinoCap::g_s_G4
- `0x240A60` DinoCap::g_boss_point_pos
- `0x2409C4` DinoCap::g_s_G2
- `0x2409F8` DinoCap::g_s_G3
- `0x24095C` DinoCap::g_s_G0
- `0x240990` DinoCap::g_s_G1

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x3B8AC   f0b5       PUSH    {R4-R7,LR}
0x3B8AE   5e46       MOV     R6, R11
0x3B8B0   5546       MOV     R5, R10
0x3B8B2   4446       MOV     R4, R8
0x3B8B4   70b4       PUSH    {R4-R6}
0x3B8B6   acf114eb   BLX     ___save_vfp_d8_d15_regs
0x3B8BA   3d4c       LDR     R4, =0xFFFFB5B8
0x3B8BC   16af       ADD     R7, SP, #arg_38
0x3B8BE   a544       ADD     SP, R4; int
0x3B8C0   3c4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x3B8CA)
0x3B8C2   9421c900   MOVS    R1, #0x4A0
0x3B8C6   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x3B8C8   6944       ADD     R1, SP
0x3B8CA   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x3B8CC   3a4a       LDR     R2, =0x4A4
0x3B8CE   a590       STR     R0, [SP,#0x4A48+var_47B4]; int
0x3B8D0   0b60       STR     R3, [R1]
0x3B8D2   3a4b       LDR     R3, =(GCC_except_table24_0 - 0x3B8DC)
0x3B8D4   6a44       ADD     R2, SP
0x3B8D6   9621       MOVS    R1, #0x96
0x3B8D8   7b44       ADD     R3, PC; GCC_except_table24_0
0x3B8DA   1360       STR     R3, [R2]
0x3B8DC   9523db00   MOVS    R3, #0x4A8
0x3B8E0   6b44       ADD     R3, SP
```

