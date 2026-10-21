# Address conventions

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../start/disclaimer.md).*

## The analysed binary (`DC2-IOS13-ARM32-SUB6`)

| Property | Value |
|---|---|
| Game | Dino Cap 2, iOS, version 1.3, bundle `com.trinitigame.dinocap2hd` |
| IPA SHA-256 | `6cec1c949b1507a82a48997b8796adb2ab47e66524b43c3e9b3e8b5a3c940038` |
| Executable SHA-256 | `0ba6bc2b2c92a045a13af1ca37af9389f81f8848ae81bcb892423e7eae01cdd8` (the analysis copy is byte-identical to the executable inside the IPA) |
| Format | thin Mach-O, 32-bit, little-endian; no fat slices |
| CPU | type 12 (ARM), subtype 6 (`CPU_SUBTYPE_ARM_V6`) as declared by the header |
| Instruction sets | mostly Thumb: 8,205 Thumb and 298 ARM functions in the analysis database |
| UUID | `4b184aee25a5d633049b3ad785b9f9dd` |
| Encryption | `cryptid 0` (not encrypted) |
| Imagebase | `0x1000` (`__TEXT` vmaddr; `__PAGEZERO` covers `0x0..0x1000`) |

The full record is [`binaries.jsonl`](binaries.jsonl) and the segment table is [`segments.json`](segments.json).

## Address kinds used in these pages

- **EA**: IDA's static virtual address in this binary, with no runtime slide. This is the number to type into IDA's *Jump to address*.
- **RVA** = EA − `0x1000`.
- **File offset**: `__TEXT` starts at file offset 0, so for code in `__text` the file offset equals the RVA. `__DATA` (vmaddr `0x22A000`) starts at file offset `0x229000`.
- **Thumb**: code addresses are shown with the low bit cleared. A function *pointer* to Thumb code has the low bit set. For example, a virtual-table slot holding `0x165B9B` points at `EngineShell::SystemEvent`, whose code starts at `0x165B9A`.
- **Ranges** are half-open `[start, end)` in bytes.

## Jumping to an address in IDA

Load **this exact binary** (check the executable SHA-256 above), let IDA analyse it with the default Mach-O loader (imagebase `0x1000`), then use *Jump to address* with the EA. **Do not use these addresses with any other version of Dino Cap 2**: a different build moves everything.
