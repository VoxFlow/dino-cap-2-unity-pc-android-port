# CAComponent::Load

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-11CAE0` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x11CAE0` |
| RVA (EA - 0x1000) | `0x11BAE0` |
| File offset | `0x11BAE0` |
| Size | 228 code bytes in 1 chunk(s), 111 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11CAComponent4LoadERN9Triniti2D15BlockReadBufferEiPN9CALibrary7AnimLibE` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::CAComponent::Load(Triniti2D::BlockReadBuffer &,int,CALibrary::AnimLib *)` |
| Bytes SHA-256 | `a801d09f38658ae8f1d01a2c2273ecd3706aa052f965ad8f9c23d4d56840a7ad` |
| Direct code callers | 2 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: [`ComponentAnimation::Load`](../../../functions/dossiers/ComponentAnimation/FUN-11D3FE.md)

Callees: `CALibrary::AnimLib::GetAnim`, `operator new`, `___gesf2vfp`

## Named data it references

- `0x22AF4C` __ZNKSt6vectorIPN7DinoCap11CAComponent5FrameESaIS3_EE4sizeEv_ptr
- `0x22A1E4` __ZN9Triniti2D15BlockReadBuffer4readEPcm_ptr
- `0x22B0C8` __ZN7DinoCap11CAComponent5FrameC2Ev_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x11CAE0  f0b5       PUSH    {R4-R7,LR}
0x11CAE2  5e46       MOV     R6, R11
0x11CAE4  5546       MOV     R5, R10
0x11CAE6  4446       MOV     R4, R8
0x11CAE8  70b4       PUSH    {R4-R6}
0x11CAEA  06af       ADD     R7, SP, #0x18
0x11CAEC  83b0       SUB     SP, SP, #0xC
0x11CAEE  0093       STR     R3, [SP,#0xC+var_C]
0x11CAF0  344b       LDR     R3, =(__ZNKSt6vectorIPN7DinoCap11CAComponent5FrameESaIS3_EE4sizeEv_ptr - 0x11CAFA)
0x11CAF2  8246       MOV     R10, R0
0x11CAF4  1030       ADDS    R0, #0x10
0x11CAF6  7b44       ADD     R3, PC; __ZNKSt6vectorIPN7DinoCap11CAComponent5FrameESaIS3_EE4sizeEv_ptr
0x11CAF8  0d1c       MOVS    R5, R1
0x11CAFA  1b68       LDR     R3, [R3]
0x11CAFC  9847       BLX     R3
0x11CAFE  0021       MOVS    R1, #0
0x11CB00  8846       MOV     R8, R1
0x11CB02  8346       MOV     R11, R0
0x11CB04  4ce0       B       loc_11CBA0
0x11CB06  304b       LDR     R3, =(__ZN9Triniti2D15BlockReadBuffer4readEPcm_ptr - 0x11CB10)
0x11CB08  02a9       ADD     R1, SP, #0xC+var_4
0x11CB0A  281c       MOVS    R0, R5
0x11CB0C  7b44       ADD     R3, PC; __ZN9Triniti2D15BlockReadBuffer4readEPcm_ptr
0x11CB0E  0422       MOVS    R2, #4
```

