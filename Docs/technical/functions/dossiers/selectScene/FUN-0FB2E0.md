# selectScene::OnOptionLeave

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0FB2E0` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xFB2E0` |
| RVA (EA - 0x1000) | `0xFA2E0` |
| File offset | `0xFA2E0` |
| Size | 3406 code bytes in 1 chunk(s), 1399 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene13OnOptionLeaveEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::OnOptionLeave(void)` |
| Bytes SHA-256 | `f57f527e004f9c6c85ae3bcf3f863063782d2320c92b33401762f3c1cd93d7e4` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.audio.sound_music_flags` (LIKELY, NATIVE): Globals g_sound(0x23b8d9), g_fx(0x23b8da) are loaded by DinoCap::ReadFile (0x1995c) and mirrored to g_ongame_sound/fx at title startUI; selectScene has OnSound(0xf4128)/OnMusic(0xf433a)/OnOptionEnter(0xfc138)/OnOptionLeave(0xfb2e0). Persistence path SaveFile@0x58b9c / FileManager::WriteSave not yet traced.
- Rule `dc2.options.panel_layout` (PROVEN, NATIVE (decompiler)): selectScene::OnOptionEnter@0xfc138 builds: id78 full-screen dim = whiteFrame tinted black alpha 0xD0 (208/255) on layer 5; then layer-6 items that slide DOWN into place (startMoveUIDown): id79 op_titleFrame, id82 op_soundFrame label + id83 state icon (op_on1/op_off1 by g_fx), id80 op_musicFrame label + id81 state icon (by g_sound), id84 op_supportFrame, id85 op_reviewFrame, id87 op_reset_peofileFrame, id88 ui_btn_bac...

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/SelectScene.cs:210`](../../../../../Assets/Scripts/SelectScene.cs#L210) cites `0xFB2E0`

## Callers and callees (direct code references)

Callers: [`selectScene::OnOptionLeave`](../../../functions/dossiers/selectScene/FUN-0FB2E0.md), [`selectScene::OnUIEvent`](../../../functions/dossiers/selectScene/FUN-10B93A.md)

Callees: [`RectScale`](../../../functions/dossiers/DinoCap/FUN-017068.md), [`RectOffset`](../../../functions/dossiers/DinoCap/FUN-0170B6.md), [`selectScene::startMoveUIUp`](../../../functions/dossiers/selectScene/FUN-0FB1FC.md), [`selectScene::OnOptionLeave`](../../../functions/dossiers/selectScene/FUN-0FB2E0.md), `Triniti2D::Scene::GetUIManager`, `Triniti2D::UIImage::SetColor`, `Triniti2D::UIImage::SetTexture`, `Triniti2D::UIManager::CreateImage`, `Triniti2D::UIManager::DeleteControl`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___mulsf3vfp`

## Strings it references

- `0x2064B4` "whiteFrame"
- `0x2070C4` "op_titleFrame"
- `0x2070A4` "op_soundFrame"
- `0x207050` "op_on1Frame"
- `0x20705C` "op_off1Frame"
- `0x207040` "op_musicFrame"
- `0x2070B4` "op_supportFrame"
- `0x207094` "op_reviewFrame"
- `0x20707C` "op_reset_peofileFrame"
- `0x20A550` "ui_btn_backFrame"
- `0x20A564` "ui_btn_back_imgFrame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x228118` GCC_except_table44_0
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A5F8` __ZN7DinoCap10g_h_offsetE_ptr
- `0x23B918` DinoCap::g_h_offset
- `0x22A634` __ZN7DinoCap10g_w_offsetE_ptr
- `0x23B914` DinoCap::g_w_offset
- `0x22A6C8` __ZN9Triniti2D9UIManager13CreateControlIN7DinoCap10UIMoveCtrlEEEPT_ii_ptr
- `0x22A608` __ZN7DinoCap4g_fxE_ptr
- `0x23B8DA` DinoCap::g_fx
- `0x22A5D4` __ZN7DinoCap7g_soundE_ptr
- `0x23B8D9` DinoCap::g_sound
- `0x23D700` aBackRect
- `0x23D710` aBackImgRect

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xFB2E0   f0b5       PUSH    {R4-R7,LR}
0xFB2E2   5e46       MOV     R6, R11
0xFB2E4   5546       MOV     R5, R10
0xFB2E6   4446       MOV     R4, R8
0xFB2E8   70b4       PUSH    {R4-R6}
0xFB2EA   ecf0faed   BLX     ___save_vfp_d8_d15_regs
0xFB2EE   16af       ADD     R7, SP, #arg_38
0xFB2F0   dbb0       SUB     SP, SP, #0x16C
0xFB2F2   c54b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xFB2FC)
0xFB2F4   2590       STR     R0, [SP,#0x16C+var_D8]
0xFB2F6   0122       MOVS    R2, #1
0xFB2F8   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xFB2FA   6846       MOV     R0, SP
0xFB2FC   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xFB2FE   3e90       STR     R0, [SP,#0x16C+var_74]
0xFB300   34a8       ADD     R0, SP, #0x16C+fctx; lpfctx
0xFB302   3a93       STR     R3, [SP,#0x16C+fctx.personality]
0xFB304   c14b       LDR     R3, =(GCC_except_table44_0 - 0xFB30C)
0xFB306   3c97       STR     R7, [SP,#0x16C+fctx.jbuf]
0xFB308   7b44       ADD     R3, PC; GCC_except_table44_0
0xFB30A   3b93       STR     R3, [SP,#0x16C+fctx.lsda]
0xFB30C   c04b       LDR     R3, =(loc_FBEC8 - 0xFB312)
0xFB30E   7b44       ADD     R3, PC; loc_FBEC8
0xFB310   1343       ORRS    R3, R2
```

