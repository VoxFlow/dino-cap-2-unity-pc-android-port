# PlayMusic

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-01A36C` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x1A36C` |
| RVA (EA - 0x1000) | `0x1936C` |
| File offset | `0x1936C` |
| Size | 198 code bytes in 1 chunk(s), 89 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9PlayMusicEPKcbS1_` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::PlayMusic(char const*,bool,char const*)` |
| Bytes SHA-256 | `f112b79ec441f3718697efd019c1e552e2be674ad5ba367809c6c3f6c8bb906a` |
| Direct code callers | 6 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.options.sound_music_toggles` (PROVEN, NATIVE): Options buttons: selectScene::OnSound@0xf4128 (control id 83) toggles g_fx (g_fx ^= 1), copies to g_ongame_fx, swaps its texture between op_on1Frame/op_off1Frame, repositions via SetRectRange (rect centre-based, shifted by -0.5? of size ... 1090519040 = -?), then calls SaveFile() immediately. selectScene::OnMusic@0xf433a (control id 81) toggles g_sound (g_sound ^= 1), copies to g_ongame_sound, swaps op_on1/op_off1 fr...
- Rule `dc2.audio.music_gate` (PROVEN, NATIVE): DinoCap::PlayMusic@0x1a36c(name, loop, key) plays only when g_sound != 0 AND Triniti2D::OSIsiPodMusicPlaying()==0 (the device's own iPod music is not playing). Therefore the title music honours the saved Music flag: Music OFF => no menu music at startup; and music is skipped if other music is playing. PlaySound@0x1a448 is gated by g_fx != 0 (PROVEN by decompile): effects off => no button click sounds.

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: `sub_B3BBA`, [`menuScene::startUI`](../../../functions/dossiers/menuScene/FUN-0CA2A4.md), [`pauseCtrl::OnMusic`](../../../functions/dossiers/pauseCtrl/FUN-0CE384.md), `sub_E0FF6`, [`selectScene::OnMusic`](../../../functions/dossiers/selectScene/FUN-0F433A.md), `sub_109870`

Callees: `Triniti2D::AudioManager::Play`, `Triniti2D::OSIsiPodMusicPlaying`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x223CD1` GCC_except_table16_0
- `0x23B8D9` DinoCap::g_sound
- `0x22A338` __ZN9Triniti2D13_AudioManagerE_ptr
- `0x23BBAC` Triniti2D::_AudioManager

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x1A36C   f0b5       PUSH    {R4-R7,LR}
0x1A36E   5e46       MOV     R6, R11
0x1A370   5546       MOV     R5, R10
0x1A372   4446       MOV     R4, R8
0x1A374   70b4       PUSH    {R4-R6}
0x1A376   cdf1b4ed   BLX     ___save_vfp_d8_d15_regs
0x1A37A   16af       ADD     R7, SP, #arg_38
0x1A37C   94b0       SUB     SP, SP, #0x50 ; 'P'
0x1A37E   2d4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x1A388)
0x1A380   0192       STR     R2, [SP,#0x50+var_4C]
0x1A382   6a46       MOV     R2, SP
0x1A384   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x1A386   0f92       STR     R2, [SP,#0x50+var_14]
0x1A388   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x1A38A   0122       MOVS    R2, #1
0x1A38C   0390       STR     R0, [SP,#0x50+var_44]
0x1A38E   0b93       STR     R3, [SP,#0x50+fctx.personality]
0x1A390   294b       LDR     R3, =(GCC_except_table16_0 - 0x1A39A)
0x1A392   c9b2       UXTB    R1, R1
0x1A394   05a8       ADD     R0, SP, #0x50+fctx; lpfctx
0x1A396   7b44       ADD     R3, PC; GCC_except_table16_0
0x1A398   0c93       STR     R3, [SP,#0x50+fctx.lsda]
0x1A39A   284b       LDR     R3, =(loc_1A3EE - 0x1A3A4)
0x1A39C   0291       STR     R1, [SP,#0x50+var_48]
```

