# playerNewAniClass::OnKeyFrameAdv

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-12BD44` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x12BD44` |
| RVA (EA - 0x1000) | `0x12AD44` |
| File offset | `0x12AD44` |
| Size | 308 code bytes in 1 chunk(s), 123 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap17playerNewAniClass13OnKeyFrameAdvEi` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerNewAniClass::OnKeyFrameAdv(int)` |
| Bytes SHA-256 | `db2aa980f06359dcf5861142db470558585986de996ebfe73f174e4377d42fbc` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameGirl.cs:7`](../../../../../Assets/Scripts/GameGirl.cs#L7) cites `0x12BD44`
- [`Assets/Scripts/GameGirl.cs:9`](../../../../../Assets/Scripts/GameGirl.cs#L9) cites `0x12BD44`
- [`Assets/Scripts/GameGirl.cs:266`](../../../../../Assets/Scripts/GameGirl.cs#L266) cites `0x12BD44`

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: `subWeaponClass::~subWeaponClass`, [`playerNewAniClass::SubBullet`](../../../functions/dossiers/playerNewAniClass/FUN-125292.md), [`playerNewAniClass::LiuDanQ`](../../../functions/dossiers/playerNewAniClass/FUN-12B1BA.md), [`playerNewAniClass::KaiQiang`](../../../functions/dossiers/playerNewAniClass/FUN-12B65C.md), `Triniti2D::Console::WriteLine`, `___switch32`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`

## Strings it references

- `0x20B344` "m_state %d"
- `0x20B350` "on adv key frame : %d"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x228F48` GCC_except_table45_2
- `0x22A24C` __ZN7DinoCap15g_player_vectorE_ptr
- `0x2404AC` DinoCap::g_player_vector
- `0x22A2B8` __ZN7DinoCap13g_player_typeE_ptr
- `0x23B86C` DinoCap::g_player_type

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x12BD44  f0b5       PUSH    {R4-R7,LR}
0x12BD46  5e46       MOV     R6, R11
0x12BD48  5546       MOV     R5, R10
0x12BD4A  4446       MOV     R4, R8
0x12BD4C  70b4       PUSH    {R4-R6}
0x12BD4E  bcf0c8e8   BLX     ___save_vfp_d8_d15_regs
0x12BD52  16af       ADD     R7, SP, #arg_38
0x12BD54  99b0       SUB     SP, SP, #0x64 ; 'd'
0x12BD56  484b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x12BD60)
0x12BD58  0290       STR     R0, [SP,#0x64+var_5C]
0x12BD5A  0191       STR     R1, [SP,#0x64+var_60]
0x12BD5C  7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x12BD5E  0121       MOVS    R1, #1
0x12BD60  1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x12BD62  6846       MOV     R0, SP
0x12BD64  0f90       STR     R0, [SP,#0x64+var_28]
0x12BD66  0b93       STR     R3, [SP,#0x64+fctx.personality]
0x12BD68  444b       LDR     R3, =(GCC_except_table45_2 - 0x12BD72)
0x12BD6A  05a8       ADD     R0, SP, #0x64+fctx; lpfctx
0x12BD6C  0d97       STR     R7, [SP,#0x64+fctx.jbuf]
0x12BD6E  7b44       ADD     R3, PC; GCC_except_table45_2
0x12BD70  0c93       STR     R3, [SP,#0x64+fctx.lsda]
0x12BD72  434b       LDR     R3, =(loc_12BE3C - 0x12BD78)
0x12BD74  7b44       ADD     R3, PC; loc_12BE3C
```

