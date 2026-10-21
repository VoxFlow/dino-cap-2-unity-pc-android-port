# selectScene::OnUIEvent

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-10B93A` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x10B93A` |
| RVA (EA - 0x1000) | `0x10A93A` |
| File offset | `0x10A93A` |
| Size | 7380 code bytes in 1 chunk(s), 2635 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene9OnUIEventEPN9Triniti2D9UIControlEiff` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::OnUIEvent(Triniti2D::UIControl *,int,float,float)` |
| Bytes SHA-256 | `8c205f2f2992a63e0c593ee613fec752fa021b018797a84604bc6cfe2b002722` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.options.items_strings` (PROVEN, NATIVE_DATA): Option-screen frame names exist: op_on1Frame, op_off1Frame, op_reset_peofileFrame (sic), op_reviewFrame, op_supportFrame. Support address string support@trinitigame.com, mail format 'mailto:%@&subject=%@&body=%@'. Review prompt: Triniti2D::ReviewPrompt with '.ReviewPrompt.dat' and 'Rate this 5 stars!' text. Engine also embeds Flurry analytics/ads, GameKit, StoreKit IAP (IAPManager::Reset); these are third-party/netwo...
- Rule `dc2.select.scene_modes` (PROVEN, NATIVE (decompiler; control-id->frame mapping LIKELY)): selectScene is ONE scene reloaded with global DinoCap::m_map_type selecting the screen: 0=main map/menu, 1=armory, 2=hero select/tavern, 3=top menu bar (ids 29 mission->type0, 30 tavern->type2, 31 armory->type1, 32 back->menuScene; frames missionbtn/tavernbtn/armorybtn/backbtnFrame), 4=BANK (bankFrame, id 93; any event0 returns to t_last_map; also reachable via id 49 from armory (t_last_map=1) and hero (t_last_map=2)...
- Rule `dc2.mission.types_dispatch` (PROVEN, NATIVE (decompiler)): Mission start (m_map_type 6, control 597 event 1): mission type t = g_map_info[84*g_cur_map_data[g_curMapID]+8]; pushes level id (g_map_info+16) into g_jigsaw_levels (keeps <=3 newest, inserts at front when size>2), ++g_mission_state[4*t-4], updates g_total_time, SaveMissionInfo. Scene by type: 1 -> mainScene (life_Mode=0); 2,3,4 -> lifeModeScene (life_Mode=1, weapon.prop loaded, lifeModeTime::initTime); 5 -> mainSce...
- Rule `dc2.hero.hire_upgrade_formulas` (LIKELY, NATIVE (decompiler; compare operators need ARM verification)): Hero screen (type 2): ids 6..9 select heroes 0..3, 10 selects hero 4 ('new character'); 36/37 prev/next wrap within 0..3; 38 confirm (commit g_player_type; if it changed dword_2404E8 := 0; SaveFile; back to map). 39 HIRE: affordable iff cash(dword_24047C) >= cost(player_vector[h]+4) [cash < cost -> OnNoMoney + sound Audio_button2Audio]; on success flag +48=1, +52=cost, cash -= cost, dword_240494[h]=0, hero 1 hired wi...
- Rule `dc2.options.panel_layout` (PROVEN, NATIVE (decompiler)): selectScene::OnOptionEnter@0xfc138 builds: id78 full-screen dim = whiteFrame tinted black alpha 0xD0 (208/255) on layer 5; then layer-6 items that slide DOWN into place (startMoveUIDown): id79 op_titleFrame, id82 op_soundFrame label + id83 state icon (op_on1/op_off1 by g_fx), id80 op_musicFrame label + id81 state icon (by g_sound), id84 op_supportFrame, id85 op_reviewFrame, id87 op_reset_peofileFrame, id88 ui_btn_bac...
- Rule `dc2.save.reset_defaults` (PROVEN, NATIVE (decompiler)): DinoCap::ResetFile@0x5960a (Reset Profile confirm=Yes): g_max_mission=0, g_max_mission_rand=0, g_mission=1, cash=0, g_prop_Player=100.0 (0x42C80000), dword_240480=0, g_player_type=0, reloads Player.xml into g_player_vector, clears hired flags (vector<bool> unk_2404F8) and dword_2404A0[], g_p4_kill_dino=g_p1_kill_dino=0, g_max_bullet_count=500, clears g_jigsaw_active/get (5x16), jigsawSeed and jigsaw1..5 counts, BossP...
- ... 4 more rules cite addresses in this function (see [claims.jsonl](../../../claims.jsonl)).

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/Dc2Scene.cs:431`](../../../../../Assets/Scripts/Dc2Scene.cs#L431) cites `0x10B93A`
- [`Assets/Scripts/SelectBank.cs:10`](../../../../../Assets/Scripts/SelectBank.cs#L10) cites `0x10BF04`
- [`Assets/Scripts/SelectHero.cs:10`](../../../../../Assets/Scripts/SelectHero.cs#L10) cites `0x10CE02`
- [`Assets/Scripts/SelectJigsaw.cs:11`](../../../../../Assets/Scripts/SelectJigsaw.cs#L11) cites `0x10BE6C`
- [`Assets/Scripts/SelectLaunch.cs:7`](../../../../../Assets/Scripts/SelectLaunch.cs#L7) cites `0x10B9B8`
- [`Assets/Scripts/SelectLaunch.cs:51`](../../../../../Assets/Scripts/SelectLaunch.cs#L51) cites `0x10B9F2`
- [`Assets/Scripts/SelectMissionInfo.cs:101`](../../../../../Assets/Scripts/SelectMissionInfo.cs#L101) cites `0x10C6F0`

## Callers and callees (direct code references)

Callers: [`selectScene::OnUIEvent`](../../../functions/dossiers/selectScene/FUN-10B93A.md)

Callees: `GetSystemTime_Second`, `GCOpenAchievement`, `TadHide`, `TadShow`, [`PlaySound`](../../../functions/dossiers/DinoCap/FUN-01A448.md), `SaveFile`, [`ResetFile`](../../../functions/dossiers/DinoCap/FUN-05960A.md), `SaveMissionInfo`, `GCOpenLeaderboard`, `GCSubmitAchievement`, [`lifeModeTime::initTime`](../../../functions/dossiers/lifeModeTime/FUN-0C5804.md), [`weaponManager::LoadFromXml`](../../../functions/dossiers/weaponManager/FUN-0C6232.md), `sendEmail`, [`selectScene::OnNoMoney`](../../../functions/dossiers/selectScene/FUN-0F3F50.md), [`selectScene::OnSound`](../../../functions/dossiers/selectScene/FUN-0F4128.md), [`selectScene::OnMusic`](../../../functions/dossiers/selectScene/FUN-0F433A.md), `selectScene::RemoveMenu`, [`selectScene::UpdateExp`](../../../functions/dossiers/selectScene/FUN-0F45DC.md), [`selectScene::InitNewCharacter`](../../../functions/dossiers/selectScene/FUN-0F4AF0.md), [`selectScene::InitCharacter`](../../../functions/dossiers/selectScene/FUN-0F4D30.md), [`selectScene::UpgradeWeapon`](../../../functions/dossiers/selectScene/FUN-0F52C2.md), [`selectScene::AddBullet`](../../../functions/dossiers/selectScene/FUN-0F55B0.md), [`selectScene::updateWeaponIconNoMove`](../../../functions/dossiers/selectScene/FUN-0F78A8.md), `selectScene::ButtonEffect`, [`selectScene::removeMissionInfo`](../../../functions/dossiers/selectScene/FUN-0FA64C.md), [`selectScene::OnOptionLeave`](../../../functions/dossiers/selectScene/FUN-0FB2E0.md), [`selectScene::OnOptionEnter`](../../../functions/dossiers/selectScene/FUN-0FC138.md), [`selectScene::showMissionInfo`](../../../functions/dossiers/selectScene/FUN-0FD0C0.md), [`selectScene::showJigsawUI`](../../../functions/dossiers/selectScene/FUN-0FDF70.md), [`selectScene::updateWeaponIcon`](../../../functions/dossiers/selectScene/FUN-0FEF68.md), [`selectScene::lockedWeapon`](../../../functions/dossiers/selectScene/FUN-10071E.md), [`selectScene::OnSelectChange`](../../../functions/dossiers/selectScene/FUN-100B1E.md), [`selectScene::OnHire`](../../../functions/dossiers/selectScene/FUN-101A7C.md), [`selectScene::startScaleUI_Point`](../../../functions/dossiers/selectScene/FUN-10A234.md), [`selectScene::showMapPoint`](../../../functions/dossiers/selectScene/FUN-10A69A.md), [`selectScene::resetMapUI`](../../../functions/dossiers/selectScene/FUN-10B036.md), [`selectScene::OnUIEvent`](../../../functions/dossiers/selectScene/FUN-10B93A.md), `Triniti2D::AudioManager::Stop`, `Triniti2D::Console::WriteLine`, `Triniti2D::Game::SwitchScene` ... (60)

## Strings it references

- `0x207260` "mainScene"
- `0x204998` "lifeModeScene"
- `0x2049A8` "weapon.prop"
- `0x209304` "menu_bgAudio"
- `0x2049B4` "selectScene"
- `0x20A5F4` "missionbtnFrame"
- `0x20A5E4` "tavernbtnFrame"
- `0x20A5D4` "armorybtnFrame"
- `0x20A5C4` "backbtnFrame"
- `0x20726C` "menuScene"
- `0x2079C4` "dinocap2hd.l1"
- `0x209134` "support@trinitigame.com"
- `0x20AC9C` "Dino Cap 2 HD"
- `0x209318` "http://itunes.apple.com/us/app/dino-cap-2-hd/id439203629?mt=8"
- `0x20ACAC` "ARE YOU SURE YOU WANT TO DELETE ALL SAVED DATA ?"
- `0x209DF8` "No"
- `0x209DFC` "Yes"
- `0x20ACE0` "weapon3"
- `0x2072A8` "playerNewAniClass"
- `0x2072BC` "selPlayerClass"
- `0x20A7A8` "dinocap2hd.a7"
- `0x20483C` "Audio_button2Audio"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x22853D` GCC_except_table60
- `0x22A4AC` __ZN7DinoCap10m_map_typeE_ptr
- `0x23E0A0` DinoCap::m_map_type
- `0x22A280` __ZN7DinoCap10g_map_infoE_ptr
- `0x2404EC` DinoCap::g_map_info
- `0x22A25C` __ZN7DinoCap14g_cur_map_dataE_ptr
- `0x23B87C` DinoCap::g_cur_map_data
- `0x22A270` __ZN7DinoCap10g_curMapIDE_ptr
- `0x23B878` DinoCap::g_curMapID
- `0x22A5C8` __ZN7DinoCap15g_jigsaw_levelsE_ptr
- `0x240A6C` DinoCap::g_jigsaw_levels
- `0x22A2A8` __ZNKSt6vectorIiSaIiEE4sizeEv_ptr
- `0x22A5E4` __ZNSt6vectorIiSaIiEE9push_backERKi_ptr
- `0x22AB94` __ZNSt6vectorIiSaIiEE6insertEN9__gnu_cxx17__normal_iteratorIPiS1_EERKi_ptr
- `0x22A5CC` __ZN7DinoCap15g_mission_stateE_ptr
- `0x23E148` DinoCap::g_mission_state
- `0x22A6C0` __ZN7DinoCap12g_total_timeE_ptr
- `0x240A7C` DinoCap::g_total_time
- `0x22A700` __ZN7DinoCap14t_cur_sys_timeE_ptr
- `0x240A80` DinoCap::t_cur_sys_time
- `0x22A290` __ZN7DinoCap9life_ModeE_ptr
- `0x23E09C` DinoCap::life_Mode
- `0x22A434` __ZN9Triniti2D5_GameE_ptr
- `0x23BB94` Triniti2D::_Game
- `0x22A0C4` __ZN7DinoCap16g_manager_WeaponE_ptr
- `0x2404B8` DinoCap::g_manager_Weapon
- `0x22A484` __ZN7DinoCap14g_lifemodetimeE_ptr
- `0x23E080` DinoCap::g_lifemodetime

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x10B93A  f0b5       PUSH    {R4-R7,LR}
0x10B93C  5e46       MOV     R6, R11
0x10B93E  5546       MOV     R5, R10
0x10B940  4446       MOV     R4, R8
0x10B942  70b4       PUSH    {R4-R6}
0x10B944  dcf0ccea   BLX     ___save_vfp_d8_d15_regs
0x10B948  16af       ADD     R7, SP, #arg_38
0x10B94A  f1b0       SUB     SP, SP, #0x1C4
0x10B94C  f14b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x10B956)
0x10B94E  1090       STR     R0, [SP,#0x1C4+var_184]
0x10B950  0e92       STR     R2, [SP,#0x1C4+var_18C]
0x10B952  7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x10B954  0122       MOVS    R2, #1
0x10B956  1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x10B958  6846       MOV     R0, SP
0x10B95A  3d90       STR     R0, [SP,#0x1C4+var_D0]
0x10B95C  3993       STR     R3, [SP,#0x1C4+fctx.personality]
0x10B95E  ee4b       LDR     R3, =(GCC_except_table60 - 0x10B968)
0x10B960  33a8       ADD     R0, SP, #0x1C4+fctx; lpfctx
0x10B962  0f91       STR     R1, [SP,#0x1C4+var_188]
0x10B964  7b44       ADD     R3, PC; GCC_except_table60
0x10B966  3a93       STR     R3, [SP,#0x1C4+fctx.lsda]
0x10B968  ec4b       LDR     R3, =(loc_10D354 - 0x10B970)
0x10B96A  3b97       STR     R7, [SP,#0x1C4+fctx.jbuf]
```

**Around `0x10B9B8` (cited by the Unity code)**

```
0x10B986  1390       STR     R0, [SP,#0x1C4+var_178]
0x10B988  1868       LDR     R0, [R3]; DinoCap::m_map_type ...
0x10B98A  0368       LDR     R3, [R0]; DinoCap::m_map_type
0x10B98C  181c       MOVS    R0, R3
0x10B98E  c046       NOP
0x10B990  dcf096ea   BLX     ___switch32; switch 8 cases
0x10B9B8  da4b       LDR     R3, =0x255; jumptable 0010B990 case 6   <==
0x10B9BA  1399       LDR     R1, [SP,#0x1C4+var_178]
0x10B9BC  9942       CMP     R1, R3
0x10B9BE  01d0       BEQ     loc_10B9C4
0x10B9C0  01f01afe   BL      def_10B990; jumptable 0010B990 default case, case 7
0x10B9C4  0e9a       LDR     R2, [SP,#0x1C4+var_18C]
0x10B9C6  012a       CMP     R2, #1
```

**Around `0x10B9F2` (cited by the Unity code)**

```
0x10B9E6  5423       MOVS    R3, #0x54 ; 'T'
0x10B9E8  5a43       MULS    R2, R3
0x10B9EA  0b68       LDR     R3, [R1]; DinoCap::g_map_info
0x10B9EC  d318       ADDS    R3, R2, R3
0x10B9EE  9d68       LDR     R5, [R3,#8]
0x10B9F0  1695       STR     R5, [SP,#0x1C4+var_16C]
0x10B9F2  1b69       LDR     R3, [R3,#0x10]   <==
0x10B9F4  6793       STR     R3, [SP,#0x1C4+var_28]
0x10B9F6  cf4b       LDR     R3, =(__ZN7DinoCap15g_jigsaw_levelsE_ptr - 0x10B9FC)
0x10B9F8  7b44       ADD     R3, PC; __ZN7DinoCap15g_jigsaw_levelsE_ptr
0x10B9FA  1b68       LDR     R3, [R3]; DinoCap::g_jigsaw_levels ...
0x10B9FC  0d93       STR     R3, [SP,#0x1C4+var_190]
0x10B9FE  181c       MOVS    R0, R3
```

**Around `0x10BE6C` (cited by the Unity code)**

```
0x10BE5C  0068       LDR     R0, [R0]; Triniti2D::_AudioManager ...
0x10BE5E  7944       ADD     R1, PC; "menu_bgAudio"
0x10BE60  0068       LDR     R0, [R0]; this
0x10BE62  3493       STR     R3, [SP,#0x1C4+fctx.call_site]
0x10BE64  54f0c0fc   BL      __ZN9Triniti2D12AudioManager4StopEPKc; Triniti2D::AudioManager::Stop(char const*)
0x10BE68  01f0c6fb   BL      def_10B990; jumptable 0010B990 default case, case 7
0x10BE6C  139b       LDR     R3, [SP,#0x1C4+var_178]; jumptable 0010B990 case 5   <==
0x10BE6E  112b       CMP     R3, #0x11
0x10BE70  06d0       BEQ     loc_10BE80
0x10BE72  d74b       LDR     R3, =0x257
0x10BE74  139d       LDR     R5, [SP,#0x1C4+var_178]
0x10BE76  9d42       CMP     R5, R3
0x10BE78  01d0       BEQ     loc_10BE7E
```

**Around `0x10BF04` (cited by the Unity code)**

```
0x10BEF4  0123       MOVS    R3, #1
0x10BEF6  5b42       NEGS    R3, R3
0x10BEF8  1098       LDR     R0, [SP,#0x1C4+var_184]; this
0x10BEFA  3493       STR     R3, [SP,#0x1C4+fctx.call_site]
0x10BEFC  f2f738f8   BL      __ZN7DinoCap11selectScene12showJigsawUIEv; DinoCap::selectScene::showJigsawUI(void)
0x10BF00  01f07afb   BL      def_10B990; jumptable 0010B990 default case, case 7
0x10BF04  0e99       LDR     R1, [SP,#0x1C4+var_18C]; jumptable 0010B990 case 4   <==
0x10BF06  0029       CMP     R1, #0
0x10BF08  01d0       BEQ     loc_10BF0E
0x10BF0A  01f075fb   BL      def_10B990; jumptable 0010B990 default case, case 7
0x10BF0E  b54b       LDR     R3, =(__ZN7DinoCap6isskipE_ptr - 0x10BF16)
0x10BF10  5da8       ADD     R0, SP, #0x1C4+var_50
0x10BF12  7b44       ADD     R3, PC; __ZN7DinoCap6isskipE_ptr
```

**Around `0x10C6F0` (cited by the Unity code)**

```
0x10C6DE  74f0bffe   BL      __ZN9Triniti2D9UIManager13DeleteControlEi; Triniti2D::UIManager::DeleteControl(int)
0x10C6E2  6868       LDR     R0, [R5,#4]; this
0x10C6E4  68f0c1fd   BL      __ZN9Triniti2D5Scene12GetUIManagerEv; Triniti2D::Scene::GetUIManager(void)
0x10C6E8  cd49       LDR     R1, =0x1DD; int
0x10C6EA  74f0b9fe   BL      __ZN9Triniti2D9UIManager13DeleteControlEi; Triniti2D::UIManager::DeleteControl(int)
0x10C6EE  1098       LDR     R0, [SP,#0x1C4+var_184]; this
0x10C6F0  fef7a1fc   BL      __ZN7DinoCap11selectScene10resetMapUIEv; DinoCap::selectScene::resetMapUI(void)   <==
0x10C6F4  00f080ff   BL      def_10B990; jumptable 0010B990 default case, case 7
0x10C6F8  0e98       LDR     R0, [SP,#0x1C4+var_18C]
0x10C6FA  0128       CMP     R0, #1
0x10C6FC  01d0       BEQ     loc_10C702
0x10C6FE  00f07bff   BL      def_10B990; jumptable 0010B990 default case, case 7
0x10C702  1099       LDR     R1, [SP,#0x1C4+var_184]
```

**Around `0x10CE02` (cited by the Unity code)**

```
0x10CDF4  7ce3       B       loc_10D4F0
0x10CDF6  0e23       MOVS    R3, #0xE
0x10CDF8  3493       STR     R3, [SP,#0x1C4+fctx.call_site]
0x10CDFA  4aa8       ADD     R0, SP, #0x1C4+var_9C; this
0x10CDFC  dcf0d8ee   BLX     __ZNSsD2Ev; std::string::~string()
0x10CE00  fae3       B       def_10B990; jumptable 0010B990 default case, case 7
0x10CE02  139b       LDR     R3, [SP,#0x1C4+var_178]; jumptable 0010B990 case 2   <==
0x10CE04  063b       SUBS    R3, #6
0x10CE06  181c       MOVS    R0, R3
0x10CE08  dbf05ae8   BLX     ___switch32; switch 45 cases
0x10CEC4  0e9b       LDR     R3, [SP,#0x1C4+var_18C]; jumptable 0010CE08 case 0
0x10CEC6  002b       CMP     R3, #0
0x10CEC8  00d0       BEQ     loc_10CECC
```

