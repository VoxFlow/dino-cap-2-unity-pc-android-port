# CASprite::Expand

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-00B73C` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xB73C` |
| RVA (EA - 0x1000) | `0xA73C` |
| File offset | `0xA73C` |
| Size | 410 code bytes in 1 chunk(s), 175 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap8CASprite6ExpandEPS0_S1_f` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::CASprite::Expand(DinoCap::CASprite*,DinoCap::CASprite*,float)` |
| Bytes SHA-256 | `ad08d8552ee94bc911a43ffe85975eac0123d5b3f6b4453e0bb5e2bc9801eb95` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: [`CAFrame::Expand`](../../../functions/dossiers/CAFrame/FUN-00F014.md)

Callees: `std::string::assign`, `___addsf3vfp`, `___fixunssfsivfp`, `___floatsisfvfp`, `___gtsf2vfp`, `___ltsf2vfp`, `___mulsf3vfp`, `___subsf3vfp`

## Named data it references

- `0x22A1F8` __ZN9Triniti2D4LerpINS_7Vector2EEET_RKS2_S4_f_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xB73C    f0b5       PUSH    {R4-R7,LR}
0xB73E    5e46       MOV     R6, R11
0xB740    5546       MOV     R5, R10
0xB742    4446       MOV     R4, R8
0xB744    70b4       PUSH    {R4-R6}
0xB746    06af       ADD     R7, SP, #0x18
0xB748    83b0       SUB     SP, SP, #0xC
0xB74A    0093       STR     R3, [SP,#0xC+var_C]
0xB74C    cb69       LDR     R3, [R1,#0x1C]
0xB74E    0d1c       MOVS    R5, R1
0xB750    061c       MOVS    R6, R0
0xB752    c361       STR     R3, [R0,#0x1C]
0xB754    2031       ADDS    R1, #0x20 ; ' '; std::string *
0xB756    2030       ADDS    R0, #0x20 ; ' '; this
0xB758    9046       MOV     R8, R2
0xB75A    5f4c       LDR     R4, =(__ZN9Triniti2D4LerpINS_7Vector2EEET_RKS2_S4_f_ptr - 0xB772)
0xB75C    def1f8e9   BLX     __ZNSs6assignERKSs; std::string::assign(std::string const&)
0xB760    291c       MOVS    R1, R5
0xB762    301c       MOVS    R0, R6
0xB764    2831       ADDS    R1, #0x28 ; '('; std::string *
0xB766    2830       ADDS    R0, #0x28 ; '('; this
0xB768    def1f2e9   BLX     __ZNSs6assignERKSs; std::string::assign(std::string const&)
0xB76C    2b6b       LDR     R3, [R5,#0x30]
0xB76E    7c44       ADD     R4, PC; __ZN9Triniti2D4LerpINS_7Vector2EEET_RKS2_S4_f_ptr
```

