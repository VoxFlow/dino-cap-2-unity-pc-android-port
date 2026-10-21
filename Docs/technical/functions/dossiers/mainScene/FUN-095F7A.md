# mainScene::InitYinTian

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-095F7A` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x95F7A` |
| RVA (EA - 0x1000) | `0x94F7A` |
| File offset | `0x94F7A` |
| Size | 346 code bytes in 1 chunk(s), 153 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9mainScene11InitYinTianEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::mainScene::InitYinTian(void)` |
| Bytes SHA-256 | `fd51000c1828e6d775ba68d4efe7e84c30f9a329e2bac1d6be1e8302e0fd8aad` |
| Direct code callers | 3 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameWeather.cs:7`](../../../../../Assets/Scripts/GameWeather.cs#L7) cites `0x95F7A`

## Callers and callees (direct code references)

Callers: `sub_B5DF0`

Callees: `Triniti2D::GameObject::Sprite`, `Triniti2D::GameObject::SetSize`, `Triniti2D::GameSprite::SetFrame`, `Triniti2D::Scene::CreateGameObject`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___mulsf3vfp`

## Strings it references

- `0x206D54` "yintianFrame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x226590` GCC_except_table12_1
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x95F7A   f0b5       PUSH    {R4-R7,LR}
0x95F7C   5e46       MOV     R6, R11
0x95F7E   5546       MOV     R5, R10
0x95F80   4446       MOV     R4, R8
0x95F82   70b4       PUSH    {R4-R6}
0x95F84   51f1acef   BLX     ___save_vfp_d8_d15_regs
0x95F88   16af       ADD     R7, SP, #arg_38
0x95F8A   9db0       SUB     SP, SP, #0x74 ; 't'
0x95F8C   514b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x95F96)
0x95F8E   6a46       MOV     R2, SP
0x95F90   1792       STR     R2, [SP,#0x74+var_18]
0x95F92   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x95F94   0122       MOVS    R2, #1
0x95F96   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x95F98   0990       STR     R0, [SP,#0x74+var_50]
0x95F9A   0da8       ADD     R0, SP, #0x74+fctx; lpfctx
0x95F9C   1393       STR     R3, [SP,#0x74+fctx.personality]
0x95F9E   4e4b       LDR     R3, =(GCC_except_table12_1 - 0x95FA6)
0x95FA0   1597       STR     R7, [SP,#0x74+fctx.jbuf]
0x95FA2   7b44       ADD     R3, PC; GCC_except_table12_1
0x95FA4   1493       STR     R3, [SP,#0x74+fctx.lsda]
0x95FA6   4d4b       LDR     R3, =(loc_96072 - 0x95FAC)
0x95FA8   7b44       ADD     R3, PC; loc_96072
0x95FAA   1343       ORRS    R3, R2
```

