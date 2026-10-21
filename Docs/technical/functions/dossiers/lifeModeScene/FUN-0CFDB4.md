# lifeModeScene::InitYinTian

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0CFDB4` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xCFDB4` |
| RVA (EA - 0x1000) | `0xCEDB4` |
| File offset | `0xCEDB4` |
| Size | 346 code bytes in 1 chunk(s), 153 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap13lifeModeScene11InitYinTianEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::lifeModeScene::InitYinTian(void)` |
| Bytes SHA-256 | `cf9ad16c534a0abed8ec339d91723c9a98dbf3784a9eea3229317d40ef1a93e8` |
| Direct code callers | 3 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: `sub_EFBD6`

Callees: `Triniti2D::GameObject::Sprite`, `Triniti2D::GameObject::SetSize`, `Triniti2D::GameSprite::SetFrame`, `Triniti2D::Scene::CreateGameObject`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___mulsf3vfp`

## Strings it references

- `0x206D54` "yintianFrame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x22754A` GCC_except_table3_13
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xCFDB4   f0b5       PUSH    {R4-R7,LR}
0xCFDB6   5e46       MOV     R6, R11
0xCFDB8   5546       MOV     R5, R10
0xCFDBA   4446       MOV     R4, R8
0xCFDBC   70b4       PUSH    {R4-R6}
0xCFDBE   18f190e8   BLX     ___save_vfp_d8_d15_regs
0xCFDC2   16af       ADD     R7, SP, #arg_38
0xCFDC4   9db0       SUB     SP, SP, #0x74 ; 't'
0xCFDC6   524b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xCFDD0)
0xCFDC8   6a46       MOV     R2, SP
0xCFDCA   1792       STR     R2, [SP,#0x74+var_18]
0xCFDCC   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xCFDCE   0122       MOVS    R2, #1
0xCFDD0   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xCFDD2   0990       STR     R0, [SP,#0x74+var_50]
0xCFDD4   0da8       ADD     R0, SP, #0x74+fctx; lpfctx
0xCFDD6   1393       STR     R3, [SP,#0x74+fctx.personality]
0xCFDD8   4e4b       LDR     R3, =(GCC_except_table3_13 - 0xCFDE0)
0xCFDDA   1597       STR     R7, [SP,#0x74+fctx.jbuf]
0xCFDDC   7b44       ADD     R3, PC; GCC_except_table3_13
0xCFDDE   1493       STR     R3, [SP,#0x74+fctx.lsda]
0xCFDE0   4d4b       LDR     R3, =(loc_CFEAC - 0xCFDE6)
0xCFDE2   7b44       ADD     R3, PC; loc_CFEAC
0xCFDE4   1343       ORRS    R3, R2
```

