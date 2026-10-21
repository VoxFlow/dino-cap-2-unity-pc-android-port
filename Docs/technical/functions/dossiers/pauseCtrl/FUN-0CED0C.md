# pauseCtrl::Enter

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0CED0C` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xCED0C` |
| RVA (EA - 0x1000) | `0xCDD0C` |
| File offset | `0xCDD0C` |
| Size | 740 code bytes in 1 chunk(s), 345 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9pauseCtrl5EnterEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::pauseCtrl::Enter(void)` |
| Bytes SHA-256 | `ba9490b2289f596aebc4a63fb12423152d6488690452bf4f7817a8e1941f57a6` |
| Direct code callers | 4 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameControls.cs:11`](../../../../../Assets/Scripts/GameControls.cs#L11) cites `0xCED0C`
- [`Assets/Scripts/GameControls.cs:470`](../../../../../Assets/Scripts/GameControls.cs#L470) cites `0xCED0C`
- [`Assets/Scripts/GameControls.cs:471`](../../../../../Assets/Scripts/GameControls.cs#L471) cites `0xCED0C`
- [`Assets/Scripts/GameControls.cs:484`](../../../../../Assets/Scripts/GameControls.cs#L484) cites `0xCED0C`

## Callers and callees (direct code references)

Callers: [`mainScene::OnSystemEvent`](../../../functions/dossiers/mainScene/FUN-09636A.md), [`mainScene::OnTouchEvent`](../../../functions/dossiers/mainScene/FUN-096914.md), [`lifeModeScene::OnSystemEvent`](../../../functions/dossiers/lifeModeScene/FUN-0D0958.md), [`lifeModeScene::OnTouchEvent`](../../../functions/dossiers/lifeModeScene/FUN-0D15E4.md)

Callees: [`RectScale`](../../../functions/dossiers/DinoCap/FUN-017068.md), [`RectOffset`](../../../functions/dossiers/DinoCap/FUN-0170B6.md), [`UIMoveCtrl::SetRectRange`](../../../functions/dossiers/UIMoveCtrl/FUN-0CD448.md), [`UIMoveCtrl::StartMove`](../../../functions/dossiers/UIMoveCtrl/FUN-0CD466.md), `Triniti2D::UIImage::SetTexture`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___mulsf3vfp`

## Strings it references

- `0x207050` "op_on1Frame"
- `0x20705C` "op_off1Frame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x2274FA` GCC_except_table6_10
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22AA40` __ZNKSt6vectorIPN7DinoCap10UIMoveCtrlESaIS2_EE4sizeEv_ptr
- `0x22A5D4` __ZN7DinoCap7g_soundE_ptr
- `0x23B8D9` DinoCap::g_sound
- `0x22A608` __ZN7DinoCap4g_fxE_ptr
- `0x23B8DA` DinoCap::g_fx

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xCED0C   f0b5       PUSH    {R4-R7,LR}
0xCED0E   5e46       MOV     R6, R11
0xCED10   5546       MOV     R5, R10
0xCED12   4446       MOV     R4, R8
0xCED14   70b4       PUSH    {R4-R6}
0xCED16   19f1e4e8   BLX     ___save_vfp_d8_d15_regs
0xCED1A   16af       ADD     R7, SP, #arg_38
0xCED1C   bab0       SUB     SP, SP, #0xE8
0xCED1E   b44b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xCED28)
0xCED20   0790       STR     R0, [SP,#0xE8+var_CC]
0xCED22   0122       MOVS    R2, #1
0xCED24   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xCED26   6846       MOV     R0, SP
0xCED28   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xCED2A   1990       STR     R0, [SP,#0xE8+var_84]
0xCED2C   0fa8       ADD     R0, SP, #0xE8+fctx; lpfctx
0xCED2E   1593       STR     R3, [SP,#0xE8+fctx.personality]
0xCED30   b04b       LDR     R3, =(GCC_except_table6_10 - 0xCED38)
0xCED32   1797       STR     R7, [SP,#0xE8+fctx.jbuf]
0xCED34   7b44       ADD     R3, PC; GCC_except_table6_10
0xCED36   1693       STR     R3, [SP,#0xE8+fctx.lsda]
0xCED38   af4b       LDR     R3, =(loc_CEE72 - 0xCED3E)
0xCED3A   7b44       ADD     R3, PC; loc_CEE72
0xCED3C   1343       ORRS    R3, R2
```

