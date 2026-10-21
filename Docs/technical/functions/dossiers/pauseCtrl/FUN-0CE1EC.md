# pauseCtrl::OnFx

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0CE1EC` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xCE1EC` |
| RVA (EA - 0x1000) | `0xCD1EC` |
| File offset | `0xCD1EC` |
| Size | 372 code bytes in 1 chunk(s), 165 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9pauseCtrl4OnFxEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::pauseCtrl::OnFx(void)` |
| Bytes SHA-256 | `b3207fb93a09b72fdf5e6ebd9b6b1e7ddc03f6955184902effe5dcdfcebcc376` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameControls.cs:532`](../../../../../Assets/Scripts/GameControls.cs#L532) cites `0xCE1EC`

## Callers and callees (direct code references)

Callers: [`pauseCtrl::SendUIEvent`](../../../functions/dossiers/pauseCtrl/FUN-0CE5C0.md)

Callees: [`UIMoveCtrl::SetRectRange`](../../../functions/dossiers/UIMoveCtrl/FUN-0CD448.md), `Triniti2D::UIImage::SetTexture`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___mulsf3vfp`

## Strings it references

- `0x207050` "op_on1Frame"
- `0x20705C` "op_off1Frame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x2274C6` GCC_except_table0_12
- `0x22A608` __ZN7DinoCap4g_fxE_ptr
- `0x23B8DA` DinoCap::g_fx
- `0x22A5FC` __ZN7DinoCap11g_ongame_fxE_ptr
- `0x23B8DC` DinoCap::g_ongame_fx

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xCE1EC   f0b5       PUSH    {R4-R7,LR}
0xCE1EE   5e46       MOV     R6, R11
0xCE1F0   5546       MOV     R5, R10
0xCE1F2   4446       MOV     R4, R8
0xCE1F4   70b4       PUSH    {R4-R6}
0xCE1F6   19f174ee   BLX     ___save_vfp_d8_d15_regs
0xCE1FA   16af       ADD     R7, SP, #arg_38
0xCE1FC   a0b0       SUB     SP, SP, #0x80
0xCE1FE   584b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xCE208)
0xCE200   6a46       MOV     R2, SP
0xCE202   1292       STR     R2, [SP,#0x80+var_38]
0xCE204   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xCE206   0122       MOVS    R2, #1
0xCE208   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xCE20A   0490       STR     R0, [SP,#0x80+var_70]
0xCE20C   08a8       ADD     R0, SP, #0x80+fctx; lpfctx
0xCE20E   0e93       STR     R3, [SP,#0x80+fctx.personality]
0xCE210   544b       LDR     R3, =(GCC_except_table0_12 - 0xCE218)
0xCE212   1097       STR     R7, [SP,#0x80+fctx.jbuf]
0xCE214   7b44       ADD     R3, PC; GCC_except_table0_12
0xCE216   0f93       STR     R3, [SP,#0x80+fctx.lsda]
0xCE218   534b       LDR     R3, =(loc_CE306 - 0xCE21E)
0xCE21A   7b44       ADD     R3, PC; loc_CE306
0xCE21C   1343       ORRS    R3, R2
```

