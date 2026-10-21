# getJigsawPicID

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-05A6B0` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x5A6B0` |
| RVA (EA - 0x1000) | `0x596B0` |
| File offset | `0x596B0` |
| Size | 260 code bytes in 1 chunk(s), 123 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap14getJigsawPicIDEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::getJigsawPicID(void)` |
| Bytes SHA-256 | `9f534579ce362f4ab0c86deca0ca79c7a9acc3af6c126612f10ee991d734a318` |
| Direct code callers | 2 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameMission.cs:218`](../../../../../Assets/Scripts/GameMission.cs#L218) cites `0x5A6B0`

## Callers and callees (direct code references)

Callers: [`mainScene::getJigsaw`](../../../functions/dossiers/mainScene/FUN-096682.md), [`lifeModeScene::getJigsaw`](../../../functions/dossiers/lifeModeScene/FUN-0D0CF6.md)

Callees: `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `___floatsisfvfp`

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x224E6F` GCC_except_table35
- `0x23E0A8` DinoCap::g_jigsaw_get
- `0x23B86C` DinoCap::g_player_type
- `0x22A0F4` __ZNSt6vectorIN9Triniti2D7Vector2ESaIS1_EE9push_backERKS1__ptr
- `0x22A108` __ZNKSt6vectorIN9Triniti2D7Vector2ESaIS1_EE4sizeEv_ptr
- `0x22A2AC` __ZN9Triniti2D11RandomRangeEii_ptr
- `0x22A0FC` __ZNSt6vectorIN9Triniti2D7Vector2ESaIS1_EED2Ev_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x5A6B0   f0b5       PUSH    {R4-R7,LR}
0x5A6B2   5e46       MOV     R6, R11
0x5A6B4   5546       MOV     R5, R10
0x5A6B6   4446       MOV     R4, R8
0x5A6B8   70b4       PUSH    {R4-R6}
0x5A6BA   8df112ec   BLX     ___save_vfp_d8_d15_regs
0x5A6BE   16af       ADD     R7, SP, #arg_38
0x5A6C0   98b0       SUB     SP, SP, #0x60 ; '`'
0x5A6C2   3c4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x5A6CC)
0x5A6C4   6a46       MOV     R2, SP
0x5A6C6   1092       STR     R2, [SP,#0x60+var_20]
0x5A6C8   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x5A6CA   0122       MOVS    R2, #1
0x5A6CC   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x5A6CE   0490       STR     R0, [SP,#0x60+var_50]
0x5A6D0   06a8       ADD     R0, SP, #0x60+fctx; lpfctx
0x5A6D2   0c93       STR     R3, [SP,#0x60+fctx.personality]
0x5A6D4   384b       LDR     R3, =(GCC_except_table35 - 0x5A6DC)
0x5A6D6   0e97       STR     R7, [SP,#0x60+fctx.jbuf]
0x5A6D8   7b44       ADD     R3, PC; GCC_except_table35
0x5A6DA   0d93       STR     R3, [SP,#0x60+fctx.lsda]
0x5A6DC   374b       LDR     R3, =(loc_5A778 - 0x5A6E2)
0x5A6DE   7b44       ADD     R3, PC; loc_5A778
0x5A6E0   1343       ORRS    R3, R2
```

