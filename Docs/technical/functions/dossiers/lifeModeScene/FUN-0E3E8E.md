# lifeModeScene::FailZEnter

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0E3E8E` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xE3E8E` |
| RVA (EA - 0x1000) | `0xE2E8E` |
| File offset | `0xE2E8E` |
| Size | 508 code bytes in 1 chunk(s), 229 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap13lifeModeScene10FailZEnterEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::lifeModeScene::FailZEnter(void)` |
| Bytes SHA-256 | `cb5a339c1c1d633bc8d42a3c0a0323338c29e8f8539651798677bbca090f215f` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameMission.cs:278`](../../../../../Assets/Scripts/GameMission.cs#L278) cites `0xE3E8E`

## Callers and callees (direct code references)

Callers: [`lifeModeScene::OnUIEvent`](../../../functions/dossiers/lifeModeScene/FUN-0E6550.md)

Callees: [`RectScale`](../../../functions/dossiers/DinoCap/FUN-017068.md), [`RectOffset`](../../../functions/dossiers/DinoCap/FUN-0170B6.md), [`UIMoveCtrl::StartMove`](../../../functions/dossiers/UIMoveCtrl/FUN-0CD466.md), `Triniti2D::Scene::GetUIManager`, `Triniti2D::UIImage::SetTexture`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___mulsf3vfp`

## Strings it references

- `0x207BF8` "fail_zFrame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x227ABF` GCC_except_table33_2
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A6C8` __ZN9Triniti2D9UIManager13CreateControlIN7DinoCap10UIMoveCtrlEEEPT_ii_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xE3E8E   f0b5       PUSH    {R4-R7,LR}
0xE3E90   5e46       MOV     R6, R11
0xE3E92   5546       MOV     R5, R10
0xE3E94   4446       MOV     R4, R8
0xE3E96   70b4       PUSH    {R4-R6}
0xE3E98   04f122e8   BLX     ___save_vfp_d8_d15_regs
0xE3E9C   16af       ADD     R7, SP, #arg_38
0xE3E9E   b2b0       SUB     SP, SP, #0xC8
0xE3EA0   7a4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xE3EAA)
0xE3EA2   0690       STR     R0, [SP,#0xC8+var_B0]
0xE3EA4   0122       MOVS    R2, #1
0xE3EA6   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xE3EA8   6846       MOV     R0, SP
0xE3EAA   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xE3EAC   1690       STR     R0, [SP,#0xC8+var_70]
0xE3EAE   0ca8       ADD     R0, SP, #0xC8+fctx; lpfctx
0xE3EB0   1293       STR     R3, [SP,#0xC8+fctx.personality]
0xE3EB2   774b       LDR     R3, =(GCC_except_table33_2 - 0xE3EBA)
0xE3EB4   1497       STR     R7, [SP,#0xC8+fctx.jbuf]
0xE3EB6   7b44       ADD     R3, PC; GCC_except_table33_2
0xE3EB8   1393       STR     R3, [SP,#0xC8+fctx.lsda]
0xE3EBA   764b       LDR     R3, =(loc_E3F90 - 0xE3EC0)
0xE3EBC   7b44       ADD     R3, PC; loc_E3F90
0xE3EBE   1343       ORRS    R3, R2
```

