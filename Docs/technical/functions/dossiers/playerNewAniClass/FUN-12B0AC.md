# playerNewAniClass::InitType

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-12B0AC` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x12B0AC` |
| RVA (EA - 0x1000) | `0x12A0AC` |
| File offset | `0x12A0AC` |
| Size | 250 code bytes in 1 chunk(s), 113 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap17playerNewAniClass8InitTypeEi` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerNewAniClass::InitType(int)` |
| Bytes SHA-256 | `2d521e0317ed658a3e9ccdfe747e456439778237573edfb657b07a37fbabfa2d` |
| Direct code callers | 3 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | GAMEPLAY / PORTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: playerNewAniClass::InitType (scene OnLoad, InitNewCharacter): girl component rig set-up, GameGirl.cs / HeroPreview.cs

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: [`mainScene::OnLoad`](../../../functions/dossiers/mainScene/FUN-0A7434.md), [`lifeModeScene::OnLoad`](../../../functions/dossiers/lifeModeScene/FUN-0E7202.md), [`selectScene::InitNewCharacter`](../../../functions/dossiers/selectScene/FUN-0F4AF0.md)

Callees: `subWeaponClass::~subWeaponClass`, [`playerNewAniClass::ChangeWeapon`](../../../functions/dossiers/playerNewAniClass/FUN-1298D0.md), `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x228F16` GCC_except_table42_2
- `0x22A24C` __ZN7DinoCap15g_player_vectorE_ptr
- `0x2404AC` DinoCap::g_player_vector

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x12B0AC  f0b5       PUSH    {R4-R7,LR}
0x12B0AE  5e46       MOV     R6, R11
0x12B0B0  5546       MOV     R5, R10
0x12B0B2  4446       MOV     R4, R8
0x12B0B4  70b4       PUSH    {R4-R6}
0x12B0B6  bcf014ef   BLX     ___save_vfp_d8_d15_regs
0x12B0BA  16af       ADD     R7, SP, #arg_38
0x12B0BC  9eb0       SUB     SP, SP, #0x78 ; 'x'
0x12B0BE  3a4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x12B0C8)
0x12B0C0  0290       STR     R0, [SP,#0x78+var_70]
0x12B0C2  0191       STR     R1, [SP,#0x78+var_74]
0x12B0C4  7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x12B0C6  0121       MOVS    R1, #1
0x12B0C8  1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x12B0CA  6846       MOV     R0, SP
0x12B0CC  0d90       STR     R0, [SP,#0x78+var_44]
0x12B0CE  0993       STR     R3, [SP,#0x78+fctx.personality]
0x12B0D0  364b       LDR     R3, =(GCC_except_table42_2 - 0x12B0DA)
0x12B0D2  03a8       ADD     R0, SP, #0x78+fctx; lpfctx
0x12B0D4  0b97       STR     R7, [SP,#0x78+fctx.jbuf]
0x12B0D6  7b44       ADD     R3, PC; GCC_except_table42_2
0x12B0D8  0a93       STR     R3, [SP,#0x78+fctx.lsda]
0x12B0DA  354b       LDR     R3, =(loc_12B14C - 0x12B0E0)
0x12B0DC  7b44       ADD     R3, PC; loc_12B14C
```

