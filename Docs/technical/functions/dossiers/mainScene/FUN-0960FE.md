# mainScene::AddShanDian

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0960FE` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x960FE` |
| RVA (EA - 0x1000) | `0x950FE` |
| File offset | `0x950FE` |
| Size | 538 code bytes in 1 chunk(s), 239 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9mainScene11AddShanDianEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::mainScene::AddShanDian(void)` |
| Bytes SHA-256 | `723cf299593e2fd9ee71061eede1ed6b0550fd8d648ac94737f5e5464a78756f` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: [`RectScale`](../../../functions/dossiers/DinoCap/FUN-017068.md), `Triniti2D::Scene::GetUIManager`, `Triniti2D::Scene::SetTimer`, `Triniti2D::UIImage::SetColor`, `Triniti2D::UIImage::SetTexture`, `Triniti2D::UIManager::CreateImage`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___floatsisfvfp`, `___mulsf3vfp`, `___subsf3vfp`, `_rand`

## Strings it references

- `0x206D7C` "sd_baiFrame"
- `0x207464` "ShanDian"
- `0x207458` "maskWhite"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x22659C` GCC_except_table13_1
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A5F8` __ZN7DinoCap10g_h_offsetE_ptr
- `0x23B918` DinoCap::g_h_offset
- `0x22A634` __ZN7DinoCap10g_w_offsetE_ptr
- `0x23B914` DinoCap::g_w_offset

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x960FE   f0b5       PUSH    {R4-R7,LR}
0x96100   5e46       MOV     R6, R11
0x96102   5546       MOV     R5, R10
0x96104   4446       MOV     R4, R8
0x96106   70b4       PUSH    {R4-R6}
0x96108   51f1eaee   BLX     ___save_vfp_d8_d15_regs
0x9610C   16af       ADD     R7, SP, #arg_38
0x9610E   aab0       SUB     SP, SP, #0xA8
0x96110   814b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x9611A)
0x96112   0c90       STR     R0, [SP,#0xA8+var_78]
0x96114   0122       MOVS    R2, #1
0x96116   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x96118   6846       MOV     R0, SP
0x9611A   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x9611C   1b90       STR     R0, [SP,#0xA8+var_3C]
0x9611E   11a8       ADD     R0, SP, #0xA8+fctx; lpfctx
0x96120   1793       STR     R3, [SP,#0xA8+fctx.personality]
0x96122   7e4b       LDR     R3, =(GCC_except_table13_1 - 0x9612A)
0x96124   1997       STR     R7, [SP,#0xA8+fctx.jbuf]
0x96126   7b44       ADD     R3, PC; GCC_except_table13_1
0x96128   1893       STR     R3, [SP,#0xA8+fctx.lsda]
0x9612A   7d4b       LDR     R3, =(loc_96234 - 0x96130)
0x9612C   7b44       ADD     R3, PC; loc_96234
0x9612E   1343       ORRS    R3, R2
```

