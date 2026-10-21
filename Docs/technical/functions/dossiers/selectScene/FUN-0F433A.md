# selectScene::OnMusic

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0F433A` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xF433A` |
| RVA (EA - 0x1000) | `0xF333A` |
| File offset | `0xF333A` |
| Size | 514 code bytes in 1 chunk(s), 225 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene7OnMusicEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::OnMusic(void)` |
| Bytes SHA-256 | `6cc817662c2a7b45ac1b66662278ed8ed40930b8d7e9e6a42bb22057028eea2e` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.audio.sound_music_flags` (LIKELY, NATIVE): Globals g_sound(0x23b8d9), g_fx(0x23b8da) are loaded by DinoCap::ReadFile (0x1995c) and mirrored to g_ongame_sound/fx at title startUI; selectScene has OnSound(0xf4128)/OnMusic(0xf433a)/OnOptionEnter(0xfc138)/OnOptionLeave(0xfb2e0). Persistence path SaveFile@0x58b9c / FileManager::WriteSave not yet traced.
- Rule `dc2.options.sound_music_toggles` (PROVEN, NATIVE): Options buttons: selectScene::OnSound@0xf4128 (control id 83) toggles g_fx (g_fx ^= 1), copies to g_ongame_fx, swaps its texture between op_on1Frame/op_off1Frame, repositions via SetRectRange (rect centre-based, shifted by -0.5? of size ... 1090519040 = -?), then calls SaveFile() immediately. selectScene::OnMusic@0xf433a (control id 81) toggles g_sound (g_sound ^= 1), copies to g_ongame_sound, swaps op_on1/op_off1 fr...

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/SelectScene.cs:299`](../../../../../Assets/Scripts/SelectScene.cs#L299) cites `0xF433A`

## Callers and callees (direct code references)

Callers: [`selectScene::OnUIEvent`](../../../functions/dossiers/selectScene/FUN-10B93A.md)

Callees: [`PlayMusic`](../../../functions/dossiers/DinoCap/FUN-01A36C.md), `SaveFile`, [`UIMoveCtrl::SetRectRange`](../../../functions/dossiers/UIMoveCtrl/FUN-0CD448.md), `Triniti2D::AudioManager::Stop`, `Triniti2D::Scene::GetUIManager`, `Triniti2D::UIImage::SetTexture`, `Triniti2D::UIManager::GetControl`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___dynamic_cast`, `___mulsf3vfp`

## Strings it references

- `0x207050` "op_on1Frame"
- `0x2047E0` "Music_menuAudio"
- `0x209304` "menu_bgAudio"
- `0x20705C` "op_off1Frame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x227F38` GCC_except_table10_7
- `0x22A5D4` __ZN7DinoCap7g_soundE_ptr
- `0x23B8D9` DinoCap::g_sound
- `0x22A658` __ZN7DinoCap14g_ongame_soundE_ptr
- `0x23B8DB` DinoCap::g_ongame_sound
- `0x22A5F4` __ZTIN9Triniti2D9UIControlE_ptr
- `0x22E468` `typeinfo for'Triniti2D::UIControl
- `0x22A650` __ZTIN7DinoCap10UIMoveCtrlE_ptr
- `0x22CDA0` `typeinfo for'DinoCap::UIMoveCtrl
- `0x2415E8` `vtable for'__cxxabiv1::__class_type_info
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22A338` __ZN9Triniti2D13_AudioManagerE_ptr
- `0x23BBAC` Triniti2D::_AudioManager

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xF433A   f0b5       PUSH    {R4-R7,LR}
0xF433C   5e46       MOV     R6, R11
0xF433E   5546       MOV     R5, R10
0xF4340   4446       MOV     R4, R8
0xF4342   70b4       PUSH    {R4-R6}
0xF4344   f3f0cced   BLX     ___save_vfp_d8_d15_regs
0xF4348   16af       ADD     R7, SP, #arg_38
0xF434A   a3b0       SUB     SP, SP, #0x8C
0xF434C   7b4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xF4356)
0xF434E   6a46       MOV     R2, SP
0xF4350   1192       STR     R2, [SP,#0x8C+var_48]
0xF4352   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xF4354   0122       MOVS    R2, #1
0xF4356   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xF4358   0190       STR     R0, [SP,#0x8C+var_88]
0xF435A   07a8       ADD     R0, SP, #0x8C+fctx; lpfctx
0xF435C   0d93       STR     R3, [SP,#0x8C+fctx.personality]
0xF435E   784b       LDR     R3, =(GCC_except_table10_7 - 0xF4366)
0xF4360   0f97       STR     R7, [SP,#0x8C+fctx.jbuf]
0xF4362   7b44       ADD     R3, PC; GCC_except_table10_7
0xF4364   0e93       STR     R3, [SP,#0x8C+fctx.lsda]
0xF4366   774b       LDR     R3, =(loc_F449C - 0xF436C)
0xF4368   7b44       ADD     R3, PC; loc_F449C
0xF436A   1343       ORRS    R3, R2
```

