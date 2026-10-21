# menuScene::startUI

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0CA2A4` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xCA2A4` |
| RVA (EA - 0x1000) | `0xC92A4` |
| File offset | `0xC92A4` |
| Size | 2890 code bytes in 1 chunk(s), 1115 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9menuScene7startUIEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::menuScene::startUI(void)` |
| Bytes SHA-256 | `c04d0cea17cf9831b36d94108321d8856ebb7eeab669fa33a4eebf565636b7c5` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.title.menuScene.flow` (PROVEN, NATIVE): Title scene = DinoCap::menuScene. startUI@0xca2a4: reads save (ReadFile), removes LogoImage texture, copies g_sound/g_fx into g_ongame_sound/g_ongame_fx, g_state=1, loads uimenu atlas (iPad/retina: Image/ip/uimenu_h.png, else Image/uimenu.png) with CODE-defined frames ui_menu_bgFrame (0,0,960,640; 1x 480x320), ui_menu_dinoFrame (0,640,846,322; 1x 0,320,423,161), ui_menu_startFrame (0,962,484,56; 1x 0,481,242,28), loa...
- Rule `dc2.startup.logo_splash` (PROVEN, NATIVE): menuScene::OnLoad@0xcc018: ReadMissionInfo, ++g_play_time, accumulates g_total_time, SaveMissionInfo, SendStatisticsData, TadShow(1), ReviewPrompt::Popup('1.0', itunes id439203629 URL), CreatePhysicsWorld(-1000,-1000,2000,2000), layer modes 5/10/12 = 5, SetTimer('startUI', 5.0 s, one-shot). Shows splash texture 'LogoImage' from Image/Default.png (iPhone, 480x320 frame) or Image/Default-Landscape.png (iPad, 1024x768) ...

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/Dc2Scene.cs:42`](../../../../../Assets/Scripts/Dc2Scene.cs#L42) (by name)

## Callers and callees (direct code references)

Callers: [`menuScene::startUI`](../../../functions/dossiers/menuScene/FUN-0CA2A4.md), [`menuScene::OnTimeout`](../../../functions/dossiers/menuScene/FUN-0CD1E2.md)

Callees: `CreateTexture`, `ReadFile`, [`PlayMusic`](../../../functions/dossiers/DinoCap/FUN-01A36C.md), [`loadImageByConf`](../../../functions/dossiers/DinoCap/FUN-059AD6.md), [`menuScene::InitUI`](../../../functions/dossiers/menuScene/FUN-0C97E4.md), [`menuScene::startUI`](../../../functions/dossiers/menuScene/FUN-0CA2A4.md), `Triniti2D::GameObject::Enable`, `Triniti2D::Scene::GetUIManager`, `Triniti2D::Scene::CreateGameObject`, `Triniti2D::TextureManager::CreateFrame`, `Triniti2D::TextureManager::DeleteTexture`, `Triniti2D::UIManager::DeleteControl`, `Triniti2D::GameCenter::IsSupported`, `Triniti2D::GameCenter::Login`, `Triniti2D::GameCenter::IsLogin`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`

## Strings it references

- `0x20920C` "LogoImage"
- `0x209218` "uimenu_hImage"
- `0x209228` "Image/ip/uimenu_h.png"
- `0x209174` "ui_menu_bgFrame"
- `0x209240` "ui_menu_dinoFrame"
- `0x209254` "ui_menu_startFrame"
- `0x209268` "loadimg_hImage"
- `0x209278` "Image/ip/loadimg_h.png"
- `0x209290` "yuanliFrame"
- `0x20929C` "wuqi0007Frame"
- `0x2092AC` "danshou0003Frame"
- `0x207C80` "Image/ip/Player2x"
- `0x2092C0` "uimenuImage"
- `0x2092CC` "Image/uimenu.png"
- `0x2092E0` "loadimgImage"
- `0x2092F0` "Image/loadimg.png"
- `0x207CB4` "Image/Player1x"
- `0x207334` "loadEffClass"
- `0x2047E0` "Music_menuAudio"
- `0x209304` "menu_bgAudio"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x2273AA` GCC_except_table6_9
- `0x22A2DC` __ZN7DinoCap13g_prop_PlayerE_ptr
- `0x240474` DinoCap::g_prop_Player
- `0x22A124` __ZN9Triniti2D15_TextureManagerE_ptr
- `0x23BBA4` Triniti2D::_TextureManager
- `0x22A658` __ZN7DinoCap14g_ongame_soundE_ptr
- `0x23B8DB` DinoCap::g_ongame_sound
- `0x22A5D4` __ZN7DinoCap7g_soundE_ptr
- `0x23B8D9` DinoCap::g_sound
- `0x22A5FC` __ZN7DinoCap11g_ongame_fxE_ptr
- `0x23B8DC` DinoCap::g_ongame_fx
- `0x22A608` __ZN7DinoCap4g_fxE_ptr
- `0x23B8DA` DinoCap::g_fx
- `0x23B9D4` DinoCap::menuScene::count
- `0x22A6D0` __ZN7DinoCap7g_stateE_ptr
- `0x23B900` DinoCap::g_state
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A49C` __ZN7DinoCap15g_iphone_retinaE_ptr
- `0x23B905` DinoCap::g_iphone_retina
- `0x22A11C` __ZNSt6vectorIPN9Triniti2D10GameObjectESaIS2_EE9push_backERKS2__ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xCA2A4   f0b5       PUSH    {R4-R7,LR}
0xCA2A6   5e46       MOV     R6, R11
0xCA2A8   5546       MOV     R5, R10
0xCA2AA   4446       MOV     R4, R8
0xCA2AC   70b4       PUSH    {R4-R6}
0xCA2AE   1df118ee   BLX     ___save_vfp_d8_d15_regs
0xCA2B2   ef4c       LDR     R4, =0xFFFFFDD4
0xCA2B4   16af       ADD     R7, SP, #arg_38
0xCA2B6   a544       ADD     SP, R4
0xCA2B8   ee4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xCA2C2)
0xCA2BA   0122       MOVS    R2, #1
0xCA2BC   1c90       STR     R0, [SP,#0x22C+var_1BC]
0xCA2BE   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xCA2C0   6946       MOV     R1, SP
0xCA2C2   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xCA2C4   2ca8       ADD     R0, SP, #0x22C+fctx; lpfctx
0xCA2C6   3497       STR     R7, [SP,#0x22C+fctx.jbuf]
0xCA2C8   3293       STR     R3, [SP,#0x22C+fctx.personality]
0xCA2CA   eb4b       LDR     R3, =(GCC_except_table6_9 - 0xCA2D2)
0xCA2CC   3691       STR     R1, [SP,#0x22C+var_154]
0xCA2CE   7b44       ADD     R3, PC; GCC_except_table6_9
0xCA2D0   3393       STR     R3, [SP,#0x22C+fctx.lsda]
0xCA2D2   ea4b       LDR     R3, =(loc_CAC8E - 0xCA2D8)
0xCA2D4   7b44       ADD     R3, PC; loc_CAC8E
```

