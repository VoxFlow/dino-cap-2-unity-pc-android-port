# CombineAnimation::DinoChangeImage

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-00DB2C` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xDB2C` |
| RVA (EA - 0x1000) | `0xCB2C` |
| File offset | `0xCB2C` |
| Size | 2858 code bytes in 1 chunk(s), 891 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap16CombineAnimation15DinoChangeImageEibbbb` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::CombineAnimation::DinoChangeImage(int,bool,bool,bool,bool)` |
| Bytes SHA-256 | `3ef1be446beadf54a58200c7da21afb9311ac3b59581a4f6a45926b467437c0e` |
| Direct code callers | 32 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/NativeDinoChangeK5.cs:8`](../../../../../Assets/Scripts/NativeDinoChangeK5.cs#L8) cites `0xDB2C`

## Callers and callees (direct code references)

Callers: [`loadDinoANI`](../../../functions/dossiers/DinoCap/FUN-017818.md)

Callees: [`CombineAnimation::ChangeImage`](../../../functions/dossiers/CombineAnimation/FUN-00CC8A.md), `sub_E656`, `sub_E73E`, `sub_E824`, `sub_E828`, `sub_E8C2`, `sub_E95C`, `sub_EBBC`, `sub_EC56`, `sub_ECF8`

## Strings it references

- `0x201438` "k101zhua"
- `0x201434` "z1"
- `0x201444` "z2"
- `0x201448` "t1"
- `0x20144C` "k101t1"
- `0x201454` "t2"
- `0x201458` "k101t2"
- `0x201460` "t3"
- `0x201464` "k101t3"
- `0x20146C` "t4"
- `0x201470` "k101t4"
- `0x201478` "t5"
- `0x20147C` "k101t5"
- `0x201484` "t6"
- `0x201488` "k101t6"
- `0x201490` "shen"
- `0x201498` "k101shen2"
- `0x2014A4` "k101shen"
- `0x2014B0` "tou"
- `0x2014B4` "k101tou3"
- `0x2014C0` "k101tou2"
- `0x2014CC` "k101tou"
- `0x2014D4` "xiaba"
- `0x2014DC` "k101xiaba"
- `0x2014E8` "k11zhua"
- `0x2014F0` "k11t1"
- `0x2014F8` "k11t2"
- `0x201500` "k11t3"
- `0x201508` "k11t4"
- `0x201510` "k11t5"

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xDB2C    f0b5       PUSH    {R4-R7,LR}
0xDB2E    5e46       MOV     R6, R11
0xDB30    5546       MOV     R5, R10
0xDB32    4446       MOV     R4, R8
0xDB34    70b4       PUSH    {R4-R6}
0xDB36    06af       ADD     R7, SP, #0x18
0xDB38    051c       MOVS    R5, R0
0xDB3A    0898       LDR     R0, [SP,#arg_0]
0xDB3C    099c       LDR     R4, [SP,#arg_4]
0xDB3E    d2b2       UXTB    R2, R2
0xDB40    dbb2       UXTB    R3, R3
0xDB42    c0b2       UXTB    R0, R0
0xDB44    9046       MOV     R8, R2
0xDB46    9a46       MOV     R10, R3
0xDB48    8346       MOV     R11, R0
0xDB4A    e6b2       UXTB    R6, R4
0xDB4C    6f29       CMP     R1, #0x6F ; 'o'
0xDB4E    00d1       BNE     loc_DB52
0xDB50    1fe1       B       loc_DD92
0xDB52    6f29       CMP     R1, #0x6F ; 'o'
0xDB54    1cdc       BGT     loc_DB90
0xDB56    2029       CMP     R1, #0x20 ; ' '
0xDB58    01d1       BNE     loc_DB5E
0xDB5A    00f07cfd   BL      sub_E656
```

