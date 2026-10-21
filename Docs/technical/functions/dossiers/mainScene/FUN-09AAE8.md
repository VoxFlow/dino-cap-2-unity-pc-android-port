# mainScene::InitHouse

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-09AAE8` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x9AAE8` |
| RVA (EA - 0x1000) | `0x99AE8` |
| File offset | `0x99AE8` |
| Size | 1258 code bytes in 1 chunk(s), 501 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9mainScene9InitHouseEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::mainScene::InitHouse(void)` |
| Bytes SHA-256 | `c2d640adf3a1b35c0f8c6c8a186ade92dba0beeb62c28b480720ec070bfd83f9` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / VERIFIED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameHouse.cs:6`](../../../../../Assets/Scripts/GameHouse.cs#L6) cites `0x9AAE8`

## Callers and callees (direct code references)

Callers: `sub_B29F6`

Callees: `Triniti2D::GameObject::GetSize`, `Triniti2D::GameObject::Sprite`, `Triniti2D::GameObject::SetSize`, `Triniti2D::GameObject::SetPosition`, `Triniti2D::GameSprite::SetFrame`, `Triniti2D::Scene::CreateGameObject`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___divsf3vfp`, `___fixsfsivfp`, `___floatsisfvfp`, `___ltsf2vfp`, `___mulsf3vfp`, `___subsf3vfp`

## Strings it references

- `0x207354` "houseClass"
- `0x20617C` "zhuangshi1Frame"
- `0x206124` "zhuangshi2Frame"
- `0x2060EC` "zhuangshi3Frame"
- `0x2072E8` "itemClass"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x2267BB` GCC_except_table29_0
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A2AC` __ZN9Triniti2D11RandomRangeEii_ptr
- `0x22A11C` __ZNSt6vectorIPN9Triniti2D10GameObjectESaIS2_EE9push_backERKS2__ptr
- `0x22A0E8` __ZN7DinoCap10g_ground_yE_ptr
- `0x23B910` DinoCap::g_ground_y

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x9AAE8   f0b5       PUSH    {R4-R7,LR}
0x9AAEA   5e46       MOV     R6, R11
0x9AAEC   5546       MOV     R5, R10
0x9AAEE   4446       MOV     R4, R8
0x9AAF0   70b4       PUSH    {R4-R6}
0x9AAF2   4df1f6e9   BLX     ___save_vfp_d8_d15_regs
0x9AAF6   16af       ADD     R7, SP, #arg_38
0x9AAF8   bcb0       SUB     SP, SP, #0xF0
0x9AAFA   bd4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x9AB04)
0x9AAFC   0122       MOVS    R2, #1
0x9AAFE   0d90       STR     R0, [SP,#0xF0+var_BC]
0x9AB00   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x9AB02   6946       MOV     R1, SP
0x9AB04   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x9AB06   26a8       ADD     R0, SP, #0xF0+fctx; lpfctx
0x9AB08   2e97       STR     R7, [SP,#0xF0+fctx.jbuf]
0x9AB0A   2c93       STR     R3, [SP,#0xF0+fctx.personality]
0x9AB0C   b94b       LDR     R3, =(GCC_except_table29_0 - 0x9AB14)
0x9AB0E   3091       STR     R1, [SP,#0xF0+var_30]
0x9AB10   7b44       ADD     R3, PC; GCC_except_table29_0
0x9AB12   2d93       STR     R3, [SP,#0xF0+fctx.lsda]
0x9AB14   b84b       LDR     R3, =(loc_9AF48 - 0x9AB1A)
0x9AB16   7b44       ADD     R3, PC; loc_9AF48
0x9AB18   1343       ORRS    R3, R2
```

