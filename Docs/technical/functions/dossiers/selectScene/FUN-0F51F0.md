# selectScene::UpdateBullet

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0F51F0` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xF51F0` |
| RVA (EA - 0x1000) | `0xF41F0` |
| File offset | `0xF41F0` |
| Size | 184 code bytes in 1 chunk(s), 84 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene12UpdateBulletEi` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::UpdateBullet(int)` |
| Bytes SHA-256 | `410fff2d9242e65a0d41706c1156ec216b287494055224edbe704045c9a52c0b` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UI_FLOW / PORTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: selectScene::UpdateBullet (AddBullet): armory ammo label, SelectArmory.cs

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: [`selectScene::AddBullet`](../../../functions/dossiers/selectScene/FUN-0F55B0.md)

Callees: [`PlaySound`](../../../functions/dossiers/DinoCap/FUN-01A448.md), `playerTypeClass::~playerTypeClass`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`

## Strings it references

- `0x20493C` "Audio_NobulletAudio"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x227F78` GCC_except_table22_5
- `0x22A24C` __ZN7DinoCap15g_player_vectorE_ptr
- `0x2404AC` DinoCap::g_player_vector
- `0x22A2A4` __ZN7DinoCap15playerTypeClassC2ERKS0__ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xF51F0   f0b5       PUSH    {R4-R7,LR}
0xF51F2   5e46       MOV     R6, R11
0xF51F4   5546       MOV     R5, R10
0xF51F6   4446       MOV     R4, R8
0xF51F8   70b4       PUSH    {R4-R6}
0xF51FA   f2f072ee   BLX     ___save_vfp_d8_d15_regs
0xF51FE   16af       ADD     R7, SP, #arg_38
0xF5200   aeb0       SUB     SP, SP, #0xB8
0xF5202   294b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xF520C)
0xF5204   0122       MOVS    R2, #1
0xF5206   0290       STR     R0, [SP,#0xB8+var_B0]
0xF5208   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xF520A   0191       STR     R1, [SP,#0xB8+var_B4]
0xF520C   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xF520E   6946       MOV     R1, SP
0xF5210   03a8       ADD     R0, SP, #0xB8+fctx; lpfctx
0xF5212   0993       STR     R3, [SP,#0xB8+fctx.personality]
0xF5214   254b       LDR     R3, =(GCC_except_table22_5 - 0xF521E)
0xF5216   0d91       STR     R1, [SP,#0xB8+var_84]
0xF5218   0b97       STR     R7, [SP,#0xB8+fctx.jbuf]
0xF521A   7b44       ADD     R3, PC; GCC_except_table22_5
0xF521C   0a93       STR     R3, [SP,#0xB8+fctx.lsda]
0xF521E   244b       LDR     R3, =(loc_F526C - 0xF5224)
0xF5220   7b44       ADD     R3, PC; loc_F526C
```

