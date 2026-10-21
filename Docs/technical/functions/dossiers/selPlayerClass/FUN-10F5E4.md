# selPlayerClass::OnTimeout

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-10F5E4` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x10F5E4` |
| RVA (EA - 0x1000) | `0x10E5E4` |
| File offset | `0x10E5E4` |
| Size | 66 code bytes in 1 chunk(s), 30 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap14selPlayerClass9OnTimeoutEiPv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selPlayerClass::OnTimeout(int,void *)` |
| Bytes SHA-256 | `a7744dec0cae3b5ba556ccd29370744f2100e20e9cd867b797baebff37d81953` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UI_FLOW / TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: 0x10f5e4: 'Wudi' clears +654 and resets +88 to 1.0; 'fireup' (armed for 2.0 s by every ChangeWeapon, sub_10E930 0x10ec2e) clears the CAObj sprite-suffix string +108, which is only non-empty on the fire path: no visible effect in the tavern. Not ported (no-op).

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: `std::string::_M_mutate`, `_strcmp`

## Strings it references

- `0x2083C0` "Wudi"
- `0x208370` "fireup"

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x10F5E4  b0b5       PUSH    {R4,R5,R7,LR}
0x10F5E6  02af       ADD     R7, SP, #8
0x10F5E8  0f49       LDR     R1, =(aWudi - 0x10F5F2); "Wudi"
0x10F5EA  041c       MOVS    R4, R0
0x10F5EC  101c       MOVS    R0, R2; __s1
0x10F5EE  7944       ADD     R1, PC; "Wudi"
0x10F5F0  151c       MOVS    R5, R2
0x10F5F2  daf0f2ee   BLX     _strcmp
0x10F5F6  0028       CMP     R0, #0
0x10F5F8  04d1       BNE     loc_10F604
0x10F5FA  0c4b       LDR     R3, =0x28E
0x10F5FC  e054       STRB    R0, [R4,R3]
0x10F5FE  0c4b       LDR     R3, =0x3F800000
0x10F600  a365       STR     R3, [R4,#0x58]
0x10F602  0fe0       B       locret_10F624
0x10F604  0b49       LDR     R1, =(aFireup - 0x10F60C); "fireup"
0x10F606  281c       MOVS    R0, R5; __s1
0x10F608  7944       ADD     R1, PC; "fireup"
0x10F60A  daf0e6ee   BLX     _strcmp
0x10F60E  0028       CMP     R0, #0
0x10F610  08d1       BNE     locret_10F624
0x10F612  e26e       LDR     R2, [R4,#0x6C]
0x10F614  201c       MOVS    R0, R4
0x10F616  6c30       ADDS    R0, #0x6C ; 'l'; this
```

