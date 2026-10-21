# playerClass::InitType

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0BC106` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xBC106` |
| RVA (EA - 0x1000) | `0xBB106` |
| File offset | `0xBB106` |
| Size | 264 code bytes in 1 chunk(s), 119 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11playerClass8InitTypeEi` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerClass::InitType(int)` |
| Bytes SHA-256 | `e404c30c32e9cdd6a9334efb774d0c1ab20e7b34f7f9c57aa419849d7eb0e26e` |
| Direct code callers | 2 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | GAMEPLAY / PORTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: playerClass::InitType (scene OnLoad): hero record selection, GameScene/HeroPreview InitType (record0/2/4)

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: [`mainScene::OnLoad`](../../../functions/dossiers/mainScene/FUN-0A7434.md), [`lifeModeScene::OnLoad`](../../../functions/dossiers/lifeModeScene/FUN-0E7202.md)

Callees: `GenComAnim`, [`playerClass::ChangeWeapon`](../../../functions/dossiers/playerClass/FUN-0B9DE8.md), `subWeaponClass::~subWeaponClass`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x226F8E` GCC_except_table12_2
- `0x22A24C` __ZN7DinoCap15g_player_vectorE_ptr
- `0x2404AC` DinoCap::g_player_vector

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xBC106   f0b5       PUSH    {R4-R7,LR}
0xBC108   5e46       MOV     R6, R11
0xBC10A   5546       MOV     R5, R10
0xBC10C   4446       MOV     R4, R8
0xBC10E   70b4       PUSH    {R4-R6}
0xBC110   2bf1e6ee   BLX     ___save_vfp_d8_d15_regs
0xBC114   16af       ADD     R7, SP, #arg_38
0xBC116   a0b0       SUB     SP, SP, #0x80
0xBC118   3d4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xBC122)
0xBC11A   0290       STR     R0, [SP,#0x80+var_78]
0xBC11C   0191       STR     R1, [SP,#0x80+var_7C]
0xBC11E   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xBC120   0121       MOVS    R1, #1
0xBC122   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xBC124   6846       MOV     R0, SP
0xBC126   0f90       STR     R0, [SP,#0x80+var_44]
0xBC128   0b93       STR     R3, [SP,#0x80+fctx.personality]
0xBC12A   3a4b       LDR     R3, =(GCC_except_table12_2 - 0xBC134)
0xBC12C   05a8       ADD     R0, SP, #0x80+fctx; lpfctx
0xBC12E   0d97       STR     R7, [SP,#0x80+fctx.jbuf]
0xBC130   7b44       ADD     R3, PC; GCC_except_table12_2
0xBC132   0c93       STR     R3, [SP,#0x80+fctx.lsda]
0xBC134   384b       LDR     R3, =(loc_BC1B4 - 0xBC13A)
0xBC136   7b44       ADD     R3, PC; loc_BC1B4
```

