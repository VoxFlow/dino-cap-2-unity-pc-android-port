# dinosaurClass::OnAnimationEnd

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-011AD6` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x11AD6` |
| RVA (EA - 0x1000) | `0x10AD6` |
| File offset | `0x10AD6` |
| Size | 102 code bytes in 1 chunk(s), 41 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap13dinosaurClass14OnAnimationEndEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::dinosaurClass::OnAnimationEnd(void)` |
| Bytes SHA-256 | `723fcc21ca0c05eb59bfa41325da4b245546c1abc62e216aa4772a1e1164093f` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | deep: curated note below, from the batch that established it |

## What it does

State 4 (beam hold): when the current frame (CAObjCallBack +68) equals 4, playAnimation(0, 2, 0.025, 4) loops frames 2..4, so a held laser makes the dinosaur twitch and take a hit on each pass of key frame 3; otherwise the state becomes 8. The port first froze the dinosaur on frame 4 (field +68 read as a bound): corrected in batch r119k.

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameCombat.cs:497`](../../../../../Assets/Scripts/GameCombat.cs#L497) cites `0x11AD6`
- [`Assets/Scripts/GameCombat.cs:500`](../../../../../Assets/Scripts/GameCombat.cs#L500) cites `0x11AD6`

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: [`CAObjCallBack::playAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00A064.md), [`dinosaurClass::CheckAttack`](../../../functions/dossiers/dinosaurClass/FUN-010B94.md), [`dinosaurClass::Move`](../../../functions/dossiers/dinosaurClass/FUN-011784.md), [`dinosaurClass::Disappear`](../../../functions/dossiers/dinosaurClass/FUN-0119F6.md), `___switch8`

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x11AD6   90b5       PUSH    {R4,R7,LR}
0x11AD8   01af       ADD     R7, SP, #4
0x11ADA   81b0       SUB     SP, SP, #4
0x11ADC   b423       MOVS    R3, #0xB4
0x11ADE   041c       MOVS    R4, R0
0x11AE0   c058       LDR     R0, [R0,R3]
0x11AE2   431e       SUBS    R3, R0, #1
0x11AE4   181c       MOVS    R0, R3
0x11AE6   d6f1d0e9   BLX     ___switch8; switch 8 cases
0x11AF4   201c       MOVS    R0, R4; jumptable 00011AE6 case 0
0x11AF6   fff74df8   BL      __ZN7DinoCap13dinosaurClass11CheckAttackEv; DinoCap::dinosaurClass::CheckAttack(void)
0x11AFA   0028       CMP     R0, #0
0x11AFC   06d0       BEQ     loc_11B0C
0x11AFE   0123       MOVS    R3, #1
0x11B00   5b42       NEGS    R3, R3
0x11B02   201c       MOVS    R0, R4
0x11B04   0021       MOVS    R1, #0
0x11B06   0022       MOVS    R2, #0
0x11B08   0093       STR     R3, [SP,#4+var_4]
0x11B0A   0ee0       B       loc_11B2A
0x11B0C   201c       MOVS    R0, R4; this
0x11B0E   fff739fe   BL      __ZN7DinoCap13dinosaurClass4MoveEv; DinoCap::dinosaurClass::Move(void)
0x11B12   11e0       B       def_11AE6; jumptable 00011AE6 default case, cases 4,7
0x11B14   201c       MOVS    R0, R4; jumptable 00011AE6 case 2
```

