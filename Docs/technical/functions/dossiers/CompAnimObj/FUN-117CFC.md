# CompAnimObj::ClearAvatar

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-117CFC` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x117CFC` |
| RVA (EA - 0x1000) | `0x116CFC` |
| File offset | `0x116CFC` |
| Size | 154 code bytes in 1 chunk(s), 69 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11CompAnimObj11ClearAvatarEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::CompAnimObj::ClearAvatar(void)` |
| Bytes SHA-256 | `e723976af79e7135e3600b8c05fd203d9c9d91433bc8e65343a0b6cd6b5eca23` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | GAMEPLAY / PORTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: 0x117cfc: clears the avatar pairs +308 and resets the selection map +40 to the base animation defaults (+24); only caller playerNewAniClass::Die 0x126a9c, before SetAnimation(g_player_die). Port was missing it (girl died with her egg/weapon avatars): fixed r118s, NativeCompObj.ClearAvatar + GameGirl.GirlDie; PilotGirlVerify check (avatar active before, cleared after).

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/NativeCompObj.cs:159`](../../../../../Assets/Scripts/NativeCompObj.cs#L159) cites `0x117CFC`

## Callers and callees (direct code references)

Callers: [`playerNewAniClass::Die`](../../../functions/dossiers/playerNewAniClass/FUN-126A36.md)

Callees: `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x2289B4` GCC_except_table33_4
- `0x22AF2C` __ZSt8_DestroyIPSt4pairISsSsEEvT_S3__ptr
- `0x22AEBC` __ZNSt8_Rb_treeISsSt4pairIKSsSsESt10_Select1stIS2_ESt4lessISsESaIS2_EEaSERKS8__ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x117CFC  f0b5       PUSH    {R4-R7,LR}
0x117CFE  5e46       MOV     R6, R11
0x117D00  5546       MOV     R5, R10
0x117D02  4446       MOV     R4, R8
0x117D04  70b4       PUSH    {R4-R6}
0x117D06  d0f0ece8   BLX     ___save_vfp_d8_d15_regs
0x117D0A  16af       ADD     R7, SP, #arg_38
0x117D0C  8fb0       SUB     SP, SP, #0x3C ; '<'
0x117D0E  224b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x117D18)
0x117D10  0122       MOVS    R2, #1
0x117D12  6946       MOV     R1, SP
0x117D14  7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x117D16  0090       STR     R0, [SP,#0x3C+var_3C]
0x117D18  1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x117D1A  02a8       ADD     R0, SP, #0x3C+fctx; lpfctx
0x117D1C  0c91       STR     R1, [SP,#0x3C+var_C]
0x117D1E  0893       STR     R3, [SP,#0x3C+fctx.personality]
0x117D20  1e4b       LDR     R3, =(GCC_except_table33_4 - 0x117D28)
0x117D22  0a97       STR     R7, [SP,#0x3C+fctx.jbuf]
0x117D24  7b44       ADD     R3, PC; GCC_except_table33_4
0x117D26  0993       STR     R3, [SP,#0x3C+fctx.lsda]
0x117D28  1d4b       LDR     R3, =(loc_117D54 - 0x117D2E)
0x117D2A  7b44       ADD     R3, PC; loc_117D54
0x117D2C  1343       ORRS    R3, R2
```

