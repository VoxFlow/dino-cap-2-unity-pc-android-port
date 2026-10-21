# CASprite::Clone

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-00B6DE` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xB6DE` |
| RVA (EA - 0x1000) | `0xA6DE` |
| File offset | `0xA6DE` |
| Size | 92 code bytes in 1 chunk(s), 42 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap8CASprite5CloneEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::CASprite::Clone(void)` |
| Bytes SHA-256 | `8bc6544a332bccf39af2a77a93564cf48a056828793f3426284534c7285757cd` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: [`CAFrame::Clone`](../../../functions/dossiers/CAFrame/FUN-00C2DA.md)

Callees: `CASprite::CASprite`, `std::string::assign`, `operator new`

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xB6DE    b0b5       PUSH    {R4,R5,R7,LR}
0xB6E0    02af       ADD     R7, SP, #8
0xB6E2    051c       MOVS    R5, R0
0xB6E4    3420       MOVS    R0, #0x34 ; '4'; unsigned int
0xB6E6    def1b8ea   BLX     __Znwm; operator new(ulong)
0xB6EA    041c       MOVS    R4, R0
0xB6EC    f6f75dfe   BL      __ZN7DinoCap8CASpriteC2Ev; DinoCap::CASprite::CASprite(void)
0xB6F0    2a68       LDR     R2, [R5]
0xB6F2    6b68       LDR     R3, [R5,#4]
0xB6F4    201c       MOVS    R0, R4
0xB6F6    291c       MOVS    R1, R5
0xB6F8    2031       ADDS    R1, #0x20 ; ' '; std::string *
0xB6FA    2260       STR     R2, [R4]
0xB6FC    6360       STR     R3, [R4,#4]
0xB6FE    aa68       LDR     R2, [R5,#8]
0xB700    eb68       LDR     R3, [R5,#0xC]
0xB702    2030       ADDS    R0, #0x20 ; ' '; this
0xB704    a260       STR     R2, [R4,#8]
0xB706    e360       STR     R3, [R4,#0xC]
0xB708    2a69       LDR     R2, [R5,#0x10]
0xB70A    6b69       LDR     R3, [R5,#0x14]
0xB70C    2261       STR     R2, [R4,#0x10]
0xB70E    6361       STR     R3, [R4,#0x14]
0xB710    ab69       LDR     R3, [R5,#0x18]
```

