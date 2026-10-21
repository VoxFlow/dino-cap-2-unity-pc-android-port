# mainScene::SwitchWeapon

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0B7DB0` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xB7DB0` |
| RVA (EA - 0x1000) | `0xB6DB0` |
| File offset | `0xB6DB0` |
| Size | 380 code bytes in 1 chunk(s), 171 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9mainScene12SwitchWeaponEi` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::mainScene::SwitchWeapon(int)` |
| Bytes SHA-256 | `aac940c98977740352f39617825a21200c9720fa0fed757b1a634dd7b6e43daa` |
| Direct code callers | 2 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameControls.cs:380`](../../../../../Assets/Scripts/GameControls.cs#L380) cites `0xB7DB0`
- [`Assets/Scripts/GameControls.cs:387`](../../../../../Assets/Scripts/GameControls.cs#L387) cites `0xB7DB0`

## Callers and callees (direct code references)

Callers: `sub_BA20A`, [`playerNewAniClass::ChangeWeapon`](../../../functions/dossiers/playerNewAniClass/FUN-1298D0.md)

Callees: [`RectScale`](../../../functions/dossiers/DinoCap/FUN-017068.md), [`RectOffset`](../../../functions/dossiers/DinoCap/FUN-0170B6.md), [`UIMoveCtrl::StartMove`](../../../functions/dossiers/UIMoveCtrl/FUN-0CD466.md), `Triniti2D::UIImage::SetTexture`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___mulsf3vfp`, `_sprintf`

## Strings it references

- `0x2081F8` "icon_bullet%dFrame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x226EEE` GCC_except_table46
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x23D2B0` iconbulletRect

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xB7DB0   f0b5       PUSH    {R4-R7,LR}
0xB7DB2   5e46       MOV     R6, R11
0xB7DB4   5546       MOV     R5, R10
0xB7DB6   4446       MOV     R4, R8
0xB7DB8   70b4       PUSH    {R4-R6}
0xB7DBA   30f192e8   BLX     ___save_vfp_d8_d15_regs
0xB7DBE   16af       ADD     R7, SP, #arg_38
0xB7DC0   b5b0       SUB     SP, SP, #0xD4
0xB7DC2   5a4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xB7DCC)
0xB7DC4   0590       STR     R0, [SP,#0xD4+var_C0]
0xB7DC6   0122       MOVS    R2, #1
0xB7DC8   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xB7DCA   6846       MOV     R0, SP
0xB7DCC   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xB7DCE   1590       STR     R0, [SP,#0xD4+var_80]
0xB7DD0   0ba8       ADD     R0, SP, #0xD4+fctx; lpfctx
0xB7DD2   1193       STR     R3, [SP,#0xD4+fctx.personality]
0xB7DD4   564b       LDR     R3, =(GCC_except_table46 - 0xB7DDE)
0xB7DD6   0491       STR     R1, [SP,#0xD4+var_C4]
0xB7DD8   1397       STR     R7, [SP,#0xD4+fctx.jbuf]
0xB7DDA   7b44       ADD     R3, PC; GCC_except_table46
0xB7DDC   1293       STR     R3, [SP,#0xD4+fctx.lsda]
0xB7DDE   554b       LDR     R3, =(loc_B7EC8 - 0xB7DE4)
0xB7DE0   7b44       ADD     R3, PC; loc_B7EC8
```

