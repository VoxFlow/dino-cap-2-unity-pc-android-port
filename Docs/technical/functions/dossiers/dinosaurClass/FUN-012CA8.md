# dinosaurClass::DieNormal

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-012CA8` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x12CA8` |
| RVA (EA - 0x1000) | `0x11CA8` |
| File offset | `0x11CA8` |
| Size | 366 code bytes in 1 chunk(s), 157 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap13dinosaurClass9DieNormalEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::dinosaurClass::DieNormal(void)` |
| Bytes SHA-256 | `800e9b9f56f8f870a845b6596b8d293b8109b5a6adcd0b71a0747adc9c1b9bb4` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / VERIFIED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameCombat.cs:809`](../../../../../Assets/Scripts/GameCombat.cs#L809) cites `0x12CA8`
- [`Assets/Scripts/GameCombat.cs:839`](../../../../../Assets/Scripts/GameCombat.cs#L839) cites `0x12CA8`
- [`Assets/Scripts/GameCombat.cs:856`](../../../../../Assets/Scripts/GameCombat.cs#L856) cites `0x12CA8`
- [`Assets/Scripts/NativeBlood.cs:29`](../../../../../Assets/Scripts/NativeBlood.cs#L29) cites `0x12CA8`
- [`Assets/Scripts/NativeBlood.cs:323`](../../../../../Assets/Scripts/NativeBlood.cs#L323) cites `0x12CA8`

## Callers and callees (direct code references)

Callers: [`dinosaurClass::Die`](../../../functions/dossiers/dinosaurClass/FUN-0140FC.md)

Callees: [`CAObjCallBack::setAnimationAdv`](../../../functions/dossiers/CAObjCallBack/FUN-00A298.md), [`CAObjCallBack::getSpritePos`](../../../functions/dossiers/CAObjCallBack/FUN-00A4D0.md), [`CAObjCallBack::setAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00AEA0.md), `___save_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `___mulsf3vfp`, `___subsf3vfp`

## Strings it references

- `0x201DA8` "DieAnim"
- `0x2013B4` "com6"
- `0x2014B0` "tou"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x223B80` GCC_except_table11_0
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x12CA8   f0b5       PUSH    {R4-R7,LR}
0x12CAA   5e46       MOV     R6, R11
0x12CAC   5546       MOV     R5, R10
0x12CAE   4446       MOV     R4, R8
0x12CB0   70b4       PUSH    {R4-R6}
0x12CB2   d5f116e9   BLX     ___save_vfp_d8_d15_regs
0x12CB6   ec4c       LDR     R4, =0xFFFFFD7C
0x12CB8   16af       ADD     R7, SP, #arg_38
0x12CBA   a544       ADD     SP, R4
0x12CBC   eb4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x12CC6)
0x12CBE   0122       MOVS    R2, #1
0x12CC0   0690       STR     R0, [SP,#0x284+var_26C]; int
0x12CC2   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x12CC4   6946       MOV     R1, SP
0x12CC6   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x12CC8   48a8       ADD     R0, SP, #0x284+fctx; lpfctx
0x12CCA   5097       STR     R7, [SP,#0x284+fctx.jbuf]; int
0x12CCC   4e93       STR     R3, [SP,#0x284+fctx.personality]; int
0x12CCE   e84b       LDR     R3, =(GCC_except_table11_0 - 0x12CD6)
0x12CD0   5291       STR     R1, [SP,#0x284+var_13C]; int
0x12CD2   7b44       ADD     R3, PC; GCC_except_table11_0
0x12CD4   4f93       STR     R3, [SP,#0x284+fctx.lsda]; int
0x12CD6   e74b       LDR     R3, =(loc_13D52 - 0x12CDC)
0x12CD8   7b44       ADD     R3, PC; loc_13D52
```

