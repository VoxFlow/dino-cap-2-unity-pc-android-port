# pauseCtrl::OnMusic

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0CE384` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xCE384` |
| RVA (EA - 0x1000) | `0xCD384` |
| File offset | `0xCD384` |
| Size | 476 code bytes in 1 chunk(s), 214 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9pauseCtrl7OnMusicEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::pauseCtrl::OnMusic(void)` |
| Bytes SHA-256 | `234715813ec0f3fc2a8ecc979e588b06a0e1aeb85f0d32242bafc073f65513c4` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameControls.cs:528`](../../../../../Assets/Scripts/GameControls.cs#L528) cites `0xCE384`

## Callers and callees (direct code references)

Callers: [`pauseCtrl::SendUIEvent`](../../../functions/dossiers/pauseCtrl/FUN-0CE5C0.md)

Callees: [`PlayMusic`](../../../functions/dossiers/DinoCap/FUN-01A36C.md), [`UIMoveCtrl::SetRectRange`](../../../functions/dossiers/UIMoveCtrl/FUN-0CD448.md), `Triniti2D::AudioManager::Stop`, `Triniti2D::UIImage::SetTexture`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___mulsf3vfp`

## Strings it references

- `0x207050` "op_on1Frame"
- `0x2077C4` "Music_Bg_Audio"
- `0x2077B4` "game_bgAudio"
- `0x20705C` "op_off1Frame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x2274D2` GCC_except_table1_11
- `0x22A5D4` __ZN7DinoCap7g_soundE_ptr
- `0x23B8D9` DinoCap::g_sound
- `0x22A658` __ZN7DinoCap14g_ongame_soundE_ptr
- `0x23B8DB` DinoCap::g_ongame_sound
- `0x22A290` __ZN7DinoCap9life_ModeE_ptr
- `0x23E09C` DinoCap::life_Mode
- `0x22A338` __ZN9Triniti2D13_AudioManagerE_ptr
- `0x23BBAC` Triniti2D::_AudioManager

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xCE384   f0b5       PUSH    {R4-R7,LR}
0xCE386   5e46       MOV     R6, R11
0xCE388   5546       MOV     R5, R10
0xCE38A   4446       MOV     R4, R8
0xCE38C   70b4       PUSH    {R4-R6}
0xCE38E   19f1a8ed   BLX     ___save_vfp_d8_d15_regs
0xCE392   16af       ADD     R7, SP, #arg_38
0xCE394   a2b0       SUB     SP, SP, #0x88
0xCE396   724b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xCE3A0)
0xCE398   6a46       MOV     R2, SP
0xCE39A   1492       STR     R2, [SP,#0x88+var_38]
0xCE39C   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xCE39E   0122       MOVS    R2, #1
0xCE3A0   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xCE3A2   0690       STR     R0, [SP,#0x88+var_70]
0xCE3A4   0aa8       ADD     R0, SP, #0x88+fctx; lpfctx
0xCE3A6   1093       STR     R3, [SP,#0x88+fctx.personality]
0xCE3A8   6e4b       LDR     R3, =(GCC_except_table1_11 - 0xCE3B0)
0xCE3AA   1297       STR     R7, [SP,#0x88+fctx.jbuf]
0xCE3AC   7b44       ADD     R3, PC; GCC_except_table1_11
0xCE3AE   1193       STR     R3, [SP,#0x88+fctx.lsda]
0xCE3B0   6d4b       LDR     R3, =(loc_CE4D6 - 0xCE3B6)
0xCE3B2   7b44       ADD     R3, PC; loc_CE4D6
0xCE3B4   1343       ORRS    R3, R2
```

