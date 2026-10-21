# mainScene::FailZEnter

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0A64CE` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xA64CE` |
| RVA (EA - 0x1000) | `0xA54CE` |
| File offset | `0xA54CE` |
| Size | 508 code bytes in 1 chunk(s), 229 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9mainScene10FailZEnterEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::mainScene::FailZEnter(void)` |
| Bytes SHA-256 | `4780776ef026961c49fb74b69ed2c8d513ba6ba8f96b5e6f751f59769d755718` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | GAMEPLAY / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: r118 0xa64ce: constants identical to lifeModeScene::FailZEnter (0.1/0.05 s); ported (GameMission.FailZEnter)

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: [`mainScene::OnUIEvent`](../../../functions/dossiers/mainScene/FUN-0A69AE.md)

Callees: [`RectScale`](../../../functions/dossiers/DinoCap/FUN-017068.md), [`RectOffset`](../../../functions/dossiers/DinoCap/FUN-0170B6.md), [`UIMoveCtrl::StartMove`](../../../functions/dossiers/UIMoveCtrl/FUN-0CD466.md), `Triniti2D::Scene::GetUIManager`, `Triniti2D::UIImage::SetTexture`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___mulsf3vfp`

## Strings it references

- `0x207BF8` "fail_zFrame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x226A95` GCC_except_table39
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A6C8` __ZN9Triniti2D9UIManager13CreateControlIN7DinoCap10UIMoveCtrlEEEPT_ii_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xA64CE   f0b5       PUSH    {R4-R7,LR}
0xA64D0   5e46       MOV     R6, R11
0xA64D2   5546       MOV     R5, R10
0xA64D4   4446       MOV     R4, R8
0xA64D6   70b4       PUSH    {R4-R6}
0xA64D8   41f102ed   BLX     ___save_vfp_d8_d15_regs
0xA64DC   16af       ADD     R7, SP, #arg_38
0xA64DE   b2b0       SUB     SP, SP, #0xC8
0xA64E0   7a4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xA64EA)
0xA64E2   0690       STR     R0, [SP,#0xC8+var_B0]
0xA64E4   0122       MOVS    R2, #1
0xA64E6   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xA64E8   6846       MOV     R0, SP
0xA64EA   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xA64EC   1690       STR     R0, [SP,#0xC8+var_70]
0xA64EE   0ca8       ADD     R0, SP, #0xC8+fctx; lpfctx
0xA64F0   1293       STR     R3, [SP,#0xC8+fctx.personality]
0xA64F2   774b       LDR     R3, =(GCC_except_table39 - 0xA64FA)
0xA64F4   1497       STR     R7, [SP,#0xC8+fctx.jbuf]
0xA64F6   7b44       ADD     R3, PC; GCC_except_table39
0xA64F8   1393       STR     R3, [SP,#0xC8+fctx.lsda]
0xA64FA   764b       LDR     R3, =(loc_A65D0 - 0xA6500)
0xA64FC   7b44       ADD     R3, PC; loc_A65D0
0xA64FE   1343       ORRS    R3, R2
```

