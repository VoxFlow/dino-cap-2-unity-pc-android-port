# CASprite::LoadXmlFromFile

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-00B8EC` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xB8EC` |
| RVA (EA - 0x1000) | `0xA8EC` |
| File offset | `0xA8EC` |
| Size | 638 code bytes in 1 chunk(s), 289 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap8CASprite15LoadXmlFromFileERN9Triniti2D15BlockReadBufferE` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::CASprite::LoadXmlFromFile(Triniti2D::BlockReadBuffer &)` |
| Bytes SHA-256 | `a782bec38c0b4729501179ec9de098412bf27079040dea45426442402ecd9f6a` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.anim.a2b_runtime_contract.r3` (PROVEN, NATIVE): Scope: ARM-backed loading playback timing and LoadXmlFromFile size branch. OnUpdate emits current frame and marked callback before accumulating dt; accumulator>=interval advances at most one frame and resets accumulator=0; finite/infinite loop handling follows unsigned frame>end. LoadXmlFromFile CASprite@0xb8ec scales x/y by g_scale; if a frame identity resolves and BOTH stored w/h are ordered <10, non-yingzi w/h mul...

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/NativeCaAnimation.cs:26`](../../../../../Assets/Scripts/NativeCaAnimation.cs#L26) (by name)
- [`Assets/Scripts/NativeCaAnimation.cs:59`](../../../../../Assets/Scripts/NativeCaAnimation.cs#L59) cites `0xB8EC`

## Callers and callees (direct code references)

Callers: [`CAFrame::LoadXmlFromFile`](../../../functions/dossiers/CAFrame/FUN-00C404.md)

Callees: `Triniti2D::TextureManager::GetFrameSize`, `Triniti2D::TextureManager::GetFrameId`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::compare`, `std::string::append`, `std::string::assign`, `std::string::string`, `std::string::~string`, `operator delete[]`, `operator new[]`, `___gesf2vfp`, `___mulsf3vfp`, `___unordsf2vfp`, `_strlen`

## Strings it references

- `0x201184` "Frame"
- `0x2011C0` "yingzi"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x223A58` GCC_except_table9_0
- `0x22A1E4` __ZN9Triniti2D15BlockReadBuffer4readEPcm_ptr
- `0x22A194` __ZN7DinoCap7g_scaleE_ptr
- `0x23B8C8` DinoCap::g_scale
- `0x22A124` __ZN9Triniti2D15_TextureManagerE_ptr
- `0x23BBA4` Triniti2D::_TextureManager

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xB8EC    f0b5       PUSH    {R4-R7,LR}
0xB8EE    5e46       MOV     R6, R11
0xB8F0    5546       MOV     R5, R10
0xB8F2    4446       MOV     R4, R8
0xB8F4    70b4       PUSH    {R4-R6}
0xB8F6    dcf1f4ea   BLX     ___save_vfp_d8_d15_regs
0xB8FA    16af       ADD     R7, SP, #arg_38
0xB8FC    a5b0       SUB     SP, SP, #0x94
0xB8FE    9b4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xB908)
0xB900    0122       MOVS    R2, #1
0xB902    0a90       STR     R0, [SP,#0x94+var_6C]
0xB904    7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xB906    0991       STR     R1, [SP,#0x94+var_70]
0xB908    1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xB90A    6946       MOV     R1, SP
0xB90C    14a8       ADD     R0, SP, #0x94+fctx; lpfctx
0xB90E    1a93       STR     R3, [SP,#0x94+fctx.personality]
0xB910    974b       LDR     R3, =(GCC_except_table9_0 - 0xB91A)
0xB912    1e91       STR     R1, [SP,#0x94+var_1C]
0xB914    1c97       STR     R7, [SP,#0x94+fctx.jbuf]
0xB916    7b44       ADD     R3, PC; GCC_except_table9_0
0xB918    1b93       STR     R3, [SP,#0x94+fctx.lsda]
0xB91A    964b       LDR     R3, =(loc_BB20 - 0xB920)
0xB91C    7b44       ADD     R3, PC; loc_BB20
```

