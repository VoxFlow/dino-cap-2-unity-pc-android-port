# menuScene::OnLoad

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0CC018` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xCC018` |
| RVA (EA - 0x1000) | `0xCB018` |
| File offset | `0xCB018` |
| Size | 890 code bytes in 1 chunk(s), 402 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9menuScene6OnLoadEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::menuScene::OnLoad(void)` |
| Bytes SHA-256 | `f767638101912292fda07165c7ffcc646ce940b14442f1705023986972f4d7f5` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.startup.logo_splash` (PROVEN, NATIVE): menuScene::OnLoad@0xcc018: ReadMissionInfo, ++g_play_time, accumulates g_total_time, SaveMissionInfo, SendStatisticsData, TadShow(1), ReviewPrompt::Popup('1.0', itunes id439203629 URL), CreatePhysicsWorld(-1000,-1000,2000,2000), layer modes 5/10/12 = 5, SetTimer('startUI', 5.0 s, one-shot). Shows splash texture 'LogoImage' from Image/Default.png (iPhone, 480x320 frame) or Image/Default-Landscape.png (iPad, 1024x768) ...
- Rule `dc2.ui.image_touch_consume.r118z4` (PROVEN, NATIVE): UIManager::HandleUITouch 0x18102e tests only GetEnable (vtable +56, UIControl +0x21, default 1) - not visibility - before HandleTouch (vtable +84). UIImage::HandleTouch 0x17f1ac and UILabel::HandleTouch 0x17fd98 return UIControl::PtInRect 0x17e70c: half-open left <= x < left+w, top <= y < top+h, and inside the clip rect (+0x24) when the clip flag (+0x22) is set. So every enabled plain image or label consumes every to...

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/MenuScene.cs:36`](../../../../../Assets/Scripts/MenuScene.cs#L36) cites `0xCC2A6`

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: [`RectScale`](../../../functions/dossiers/DinoCap/FUN-017068.md), `GetSystemTime_Second`, `CreateTexture`, `TadShow`, `ReadMissionInfo`, `SendStatisticsData`, `SaveMissionInfo`, `Triniti2D::ReviewPrompt::Popup`, `Triniti2D::Scene::GetUIManager`, `Triniti2D::Scene::SetLayerMode`, `Triniti2D::Scene::CreatePhysicsWorld`, `Triniti2D::Scene::SetTimer`, `Triniti2D::TextureManager::CreateFrame`, `Triniti2D::UIImage::SetTexture`, `Triniti2D::UIManager::CreateImage`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`

## Strings it references

- `0x209314` "1.0"
- `0x209318` "http://itunes.apple.com/us/app/dino-cap-2-hd/id439203629?mt=8"
- `0x209358` "startUI"
- `0x20920C` "LogoImage"
- `0x209360` "Image/Default-Landscape.png"
- `0x20937C` "LogoFrame"
- `0x209388` "Image/Default.png"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x227476` GCC_except_table18_5
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22AA28` __ZN7DinoCap11g_play_timeE_ptr
- `0x240A78` DinoCap::g_play_time
- `0x22A700` __ZN7DinoCap14t_cur_sys_timeE_ptr
- `0x240A80` DinoCap::t_cur_sys_time
- `0x22A6C0` __ZN7DinoCap12g_total_timeE_ptr
- `0x240A7C` DinoCap::g_total_time
- `0x22A124` __ZN9Triniti2D15_TextureManagerE_ptr
- `0x23BBA4` Triniti2D::_TextureManager

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xCC018   f0b5       PUSH    {R4-R7,LR}
0xCC01A   5e46       MOV     R6, R11
0xCC01C   5546       MOV     R5, R10
0xCC01E   4446       MOV     R4, R8
0xCC020   70b4       PUSH    {R4-R6}
0xCC022   1bf15eef   BLX     ___save_vfp_d8_d15_regs
0xCC026   16af       ADD     R7, SP, #arg_38
0xCC028   b9b0       SUB     SP, SP, #0xE4
0xCC02A   da4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xCC034)
0xCC02C   0e90       STR     R0, [SP,#0xE4+var_AC]
0xCC02E   0122       MOVS    R2, #1
0xCC030   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xCC032   6846       MOV     R0, SP
0xCC034   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xCC036   1d90       STR     R0, [SP,#0xE4+var_70]
0xCC038   13a8       ADD     R0, SP, #0xE4+fctx; lpfctx
0xCC03A   1993       STR     R3, [SP,#0xE4+fctx.personality]
0xCC03C   d64b       LDR     R3, =(GCC_except_table18_5 - 0xCC044)
0xCC03E   1b97       STR     R7, [SP,#0xE4+fctx.jbuf]
0xCC040   7b44       ADD     R3, PC; GCC_except_table18_5
0xCC042   1a93       STR     R3, [SP,#0xE4+fctx.lsda]
0xCC044   d54b       LDR     R3, =(loc_CC322 - 0xCC04A)
0xCC046   7b44       ADD     R3, PC; loc_CC322
0xCC048   1343       ORRS    R3, R2
```

**Around `0xCC2A6` (cited by the Unity code)**

```
0xCC298   5868       LDR     R0, [R3,#4]; this
0xCC29A   0594       STR     R4, [SP,#0xE4+var_D0]
0xCC29C   1494       STR     R4, [SP,#0xE4+fctx.call_site]
0xCC29E   a8f0e4ff   BL      __ZN9Triniti2D5Scene12GetUIManagerEv; Triniti2D::Scene::GetUIManager(void)
0xCC2A2   2321       MOVS    R1, #0x23 ; '#'; int
0xCC2A4   0522       MOVS    R2, #5; int
0xCC2A6   b5f0d1f8   BL      __ZN9Triniti2D9UIManager11CreateImageEii; Triniti2D::UIManager::CreateImage(int,int)   <==
0xCC2AA   564b       LDR     R3, =0x43A00000
0xCC2AC   109a       LDR     R2, [SP,#0xE4+var_A4]
0xCC2AE   20a9       ADD     R1, SP, #0xE4+var_64
0xCC2B0   2793       STR     R3, [SP,#0xE4+var_48]
0xCC2B2   534b       LDR     R3, =0x43F00000
0xCC2B4   0791       STR     R1, [SP,#0xE4+var_C8]
```

