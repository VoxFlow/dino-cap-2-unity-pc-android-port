# selectScene::OnOptionEnter

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0FC138` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xFC138` |
| RVA (EA - 0x1000) | `0xFB138` |
| File offset | `0xFB138` |
| Size | 3484 code bytes in 1 chunk(s), 1423 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene13OnOptionEnterEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::OnOptionEnter(void)` |
| Bytes SHA-256 | `2e675dacf1f86e2f2a7207e52a11c90cfdddffbfae8aae7f731e87618c494ab8` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.audio.sound_music_flags` (LIKELY, NATIVE): Globals g_sound(0x23b8d9), g_fx(0x23b8da) are loaded by DinoCap::ReadFile (0x1995c) and mirrored to g_ongame_sound/fx at title startUI; selectScene has OnSound(0xf4128)/OnMusic(0xf433a)/OnOptionEnter(0xfc138)/OnOptionLeave(0xfb2e0). Persistence path SaveFile@0x58b9c / FileManager::WriteSave not yet traced.
- Rule `dc2.options.items_strings` (PROVEN, NATIVE_DATA): Option-screen frame names exist: op_on1Frame, op_off1Frame, op_reset_peofileFrame (sic), op_reviewFrame, op_supportFrame. Support address string support@trinitigame.com, mail format 'mailto:%@&subject=%@&body=%@'. Review prompt: Triniti2D::ReviewPrompt with '.ReviewPrompt.dat' and 'Rate this 5 stars!' text. Engine also embeds Flurry analytics/ads, GameKit, StoreKit IAP (IAPManager::Reset); these are third-party/netwo...
- Rule `dc2.options.panel_layout` (PROVEN, NATIVE (decompiler)): selectScene::OnOptionEnter@0xfc138 builds: id78 full-screen dim = whiteFrame tinted black alpha 0xD0 (208/255) on layer 5; then layer-6 items that slide DOWN into place (startMoveUIDown): id79 op_titleFrame, id82 op_soundFrame label + id83 state icon (op_on1/op_off1 by g_fx), id80 op_musicFrame label + id81 state icon (by g_sound), id84 op_supportFrame, id85 op_reviewFrame, id87 op_reset_peofileFrame, id88 ui_btn_bac...

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/SelectScene.cs:210`](../../../../../Assets/Scripts/SelectScene.cs#L210) cites `0xFC138`
- [`Assets/Scripts/SelectScene.cs:241`](../../../../../Assets/Scripts/SelectScene.cs#L241) cites `0xFC1C0`

## Callers and callees (direct code references)

Callers: [`selectScene::OnOptionEnter`](../../../functions/dossiers/selectScene/FUN-0FC138.md), [`selectScene::OnUIEvent`](../../../functions/dossiers/selectScene/FUN-10B93A.md)

Callees: [`RectScale`](../../../functions/dossiers/DinoCap/FUN-017068.md), [`RectOffset`](../../../functions/dossiers/DinoCap/FUN-0170B6.md), `TadShow`, [`selectScene::startMoveUIDown`](../../../functions/dossiers/selectScene/FUN-0FC056.md), [`selectScene::OnOptionEnter`](../../../functions/dossiers/selectScene/FUN-0FC138.md), `Triniti2D::Scene::GetUIManager`, `Triniti2D::UIImage::SetColor`, `Triniti2D::UIImage::SetTexture`, `Triniti2D::UIManager::CreateImage`, `Triniti2D::UIManager::DeleteControl`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___mulsf3vfp`, `___subsf3vfp`

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
- `0x228150` GCC_except_table45_0
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
0xFC138   f0b5       PUSH    {R4-R7,LR}
0xFC13A   5e46       MOV     R6, R11
0xFC13C   5546       MOV     R5, R10
0xFC13E   4446       MOV     R4, R8
0xFC140   70b4       PUSH    {R4-R6}
0xFC142   ebf0ceee   BLX     ___save_vfp_d8_d15_regs
0xFC146   16af       ADD     R7, SP, #arg_38
0xFC148   dcb0       SUB     SP, SP, #0x170
0xFC14A   c84b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xFC154)
0xFC14C   2690       STR     R0, [SP,#0x170+var_D8]
0xFC14E   0122       MOVS    R2, #1
0xFC150   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xFC152   6846       MOV     R0, SP
0xFC154   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xFC156   3f90       STR     R0, [SP,#0x170+var_74]
0xFC158   35a8       ADD     R0, SP, #0x170+fctx; lpfctx
0xFC15A   3b93       STR     R3, [SP,#0x170+fctx.personality]
0xFC15C   c44b       LDR     R3, =(GCC_except_table45_0 - 0xFC164)
0xFC15E   3d97       STR     R7, [SP,#0x170+fctx.jbuf]
0xFC160   7b44       ADD     R3, PC; GCC_except_table45_0
0xFC162   3c93       STR     R3, [SP,#0x170+fctx.lsda]
0xFC164   c34b       LDR     R3, =(loc_FCD70 - 0xFC16A)
0xFC166   7b44       ADD     R3, PC; loc_FCD70
0xFC168   1343       ORRS    R3, R2
```

**Around `0xFC1C0` (cited by the Unity code)**

```
0xFC1B0   85f056f9   BL      __ZN9Triniti2D9UIManager13DeleteControlEi; Triniti2D::UIManager::DeleteControl(int)
0xFC1B4   269a       LDR     R2, [SP,#0x170+var_D8]
0xFC1B6   5068       LDR     R0, [R2,#4]; this
0xFC1B8   79f057f8   BL      __ZN9Triniti2D5Scene12GetUIManagerEv; Triniti2D::Scene::GetUIManager(void)
0xFC1BC   4e21       MOVS    R1, #0x4E ; 'N'; int
0xFC1BE   0522       MOVS    R2, #5; int
0xFC1C0   85f044f9   BL      __ZN9Triniti2D9UIManager11CreateImageEii; Triniti2D::UIManager::CreateImage(int,int)   <==
0xFC1C4   b04b       LDR     R3, =0x43A00000
0xFC1C6   4794       STR     R4, [SP,#0x170+var_58.bottom]
0xFC1C8   4694       STR     R4, [SP,#0x170+var_58]
0xFC1CA   4993       STR     R3, [SP,#0x170+var_4C]
0xFC1CC   af4b       LDR     R3, =0x43F00000
0xFC1CE   299c       LDR     R4, [SP,#0x170+var_CC]
```

