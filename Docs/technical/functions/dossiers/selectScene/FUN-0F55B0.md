# selectScene::AddBullet

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0F55B0` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xF55B0` |
| RVA (EA - 0x1000) | `0xF45B0` |
| File offset | `0xF45B0` |
| Size | 476 code bytes in 1 chunk(s), 218 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene9AddBulletEib` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::AddBullet(int,bool)` |
| Bytes SHA-256 | `a8e95cd7514973e7f984ab3b745e1602c0b1a9c464e7e8838de78bc319d48f29` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: [`selectScene::OnUIEvent`](../../../functions/dossiers/selectScene/FUN-10B93A.md)

Callees: [`PlaySound`](../../../functions/dossiers/DinoCap/FUN-01A448.md), `SaveFile`, `subWeaponClass::~subWeaponClass`, `playerTypeClass::~playerTypeClass`, [`selectScene::OnNoMoney`](../../../functions/dossiers/selectScene/FUN-0F3F50.md), [`selectScene::UpdateExp`](../../../functions/dossiers/selectScene/FUN-0F45DC.md), [`selectScene::UpdateBullet`](../../../functions/dossiers/selectScene/FUN-0F51F0.md), `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `___addsf3vfp`, `___fixsfsivfp`, `___floatsisfvfp`, `___mulsf3vfp`

## Strings it references

- `0x204850` "Audio_Button1Audio"
- `0x20483C` "Audio_button2Audio"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x227F88` GCC_except_table24_6
- `0x22A24C` __ZN7DinoCap15g_player_vectorE_ptr
- `0x2404AC` DinoCap::g_player_vector
- `0x22A2A4` __ZN7DinoCap15playerTypeClassC2ERKS0__ptr
- `0x22A5EC` __ZN7DinoCap18g_max_bullet_countE_ptr
- `0x23B89C` DinoCap::g_max_bullet_count
- `0x22A2DC` __ZN7DinoCap13g_prop_PlayerE_ptr
- `0x240474` DinoCap::g_prop_Player
- `0x22AB9C` __ZN7DinoCap15g_jigsaw3_countE_ptr
- `0x23E1D0` DinoCap::g_jigsaw3_count
- `0x22AA04` __ZNSt6vectorIiSaIiEEaSERKS1__ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xF55B0   f0b5       PUSH    {R4-R7,LR}
0xF55B2   5e46       MOV     R6, R11
0xF55B4   5546       MOV     R5, R10
0xF55B6   4446       MOV     R4, R8
0xF55B8   70b4       PUSH    {R4-R6}
0xF55BA   f2f092ec   BLX     ___save_vfp_d8_d15_regs
0xF55BE   16af       ADD     R7, SP, #arg_38
0xF55C0   bcb0       SUB     SP, SP, #0xF0
0xF55C2   724b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xF55CC)
0xF55C4   0690       STR     R0, [SP,#0xF0+var_D8]
0xF55C6   0591       STR     R1, [SP,#0xF0+var_DC]
0xF55C8   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xF55CA   0121       MOVS    R1, #1
0xF55CC   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xF55CE   6846       MOV     R0, SP
0xF55D0   1490       STR     R0, [SP,#0xF0+var_A0]
0xF55D2   1093       STR     R3, [SP,#0xF0+fctx.personality]
0xF55D4   6e4b       LDR     R3, =(GCC_except_table24_6 - 0xF55DE)
0xF55D6   0aa8       ADD     R0, SP, #0xF0+fctx; lpfctx
0xF55D8   1297       STR     R7, [SP,#0xF0+fctx.jbuf]
0xF55DA   7b44       ADD     R3, PC; GCC_except_table24_6
0xF55DC   1193       STR     R3, [SP,#0xF0+fctx.lsda]
0xF55DE   6d4b       LDR     R3, =(loc_F5732 - 0xF55E4)
0xF55E0   7b44       ADD     R3, PC; loc_F5732
```

