# menuScene::InitAnim

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0C9CE0` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xC9CE0` |
| RVA (EA - 0x1000) | `0xC8CE0` |
| File offset | `0xC8CE0` |
| Size | 1300 code bytes in 1 chunk(s), 505 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9menuScene8InitAnimEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::menuScene::InitAnim(void)` |
| Bytes SHA-256 | `c36c546cf17e04061020fa66975b71b746ffdc60472442ecb1e770368506bee9` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.title.menuScene.flow` (PROVEN, NATIVE): Title scene = DinoCap::menuScene. startUI@0xca2a4: reads save (ReadFile), removes LogoImage texture, copies g_sound/g_fx into g_ongame_sound/g_ongame_fx, g_state=1, loads uimenu atlas (iPad/retina: Image/ip/uimenu_h.png, else Image/uimenu.png) with CODE-defined frames ui_menu_bgFrame (0,0,960,640; 1x 480x320), ui_menu_dinoFrame (0,640,846,322; 1x 0,320,423,161), ui_menu_startFrame (0,962,484,56; 1x 0,481,242,28), loa...
- Rule `dc2.title.hero_attack_random` (SUPERSEDED, NATIVE (ARM-verification of the rand scaling pending)): menuScene::InitAnim@0xc9ce0: k = (int)((float)rand() * 2^-31 * 4) -> 0..3 (value 4 only if the float rounds to 2^31, ~1/2^31 chance, takes the player_fire_long.acb path). Selection: 0 -> player 1 load_p1_fire + Image/a2b/baozha.a2b; 1 -> player 2 load_p2_fire + yuanli(com15) energy anim; 2 -> player 3 load_p3_fire + qh2; 3 -> player 4 p4_fire + feibiao; each plays animation 'fireAni' via CAObjCallBack::playAnimation(...
- Rule `dc2.title.hero_attack_selection.r2` (LIKELY, NATIVE): InitAnim@0xc9ce0 decompilation has five switch cases, including case 4 loading a ComponentAnimation/ACB and passing hero type 5 to InitPlayer. The earlier exclusive/uniform four-hero claim and stated probability are withdrawn: PRNG output conversion and exact case probabilities still require ARM/numeric analysis. Cases 0..3 select the four A2B hero/effect pairs recorded in the historical entry. InitPlayer position an...
- Rule `dc2.title.loading_selection_numeric` (PROVEN, NATIVE): InitAnim ARM 0xc9d18..0xc9d2c: rand -> signed int-to-float -> float multiply by 0x30000000 (2^-31) -> multiply by 0x40800000 (4) -> signed float-to-int. Default g_scale written as 0x3F0CCCCD=0.55, iPad overrides 1.0; case0 phone overrides 0.5. Prior 0.8 phone claim is WRONG. Mapping may produce case4 through float rounding; case probabilities and PRNG generator semantics are not claimed. Keyframe indices for A2B case...

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/MenuScene.cs:200`](../../../../../Assets/Scripts/MenuScene.cs#L200) cites `0xC9D18`

## Callers and callees (direct code references)

Callers: [`menuScene::OnUIEvent`](../../../functions/dossiers/menuScene/FUN-0CA20C.md)

Callees: `CombineAnimation::SetKeyFrame`, [`CombineAnimation::LoadXmlFromFile`](../../../functions/dossiers/CombineAnimation/FUN-00C4DA.md), `CombineAnimation::LoadFromFile`, [`CombineAnimation::SetSpriteAnim`](../../../functions/dossiers/CombineAnimation/FUN-00EDBC.md), `getGamePath`, [`menuScene::InitPlayer`](../../../functions/dossiers/menuScene/FUN-0C999A.md), `CALibrary::Instance`, `CALibrary::Load`, [`ComponentAnimation::Load`](../../../functions/dossiers/ComponentAnimation/FUN-11D7B8.md), `Triniti2D::FileManager::ReadData`, `Triniti2D::TextureManager::AddAnimationFrame`, `Triniti2D::TextureManager::CreateAnimation`, `___switch32`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___fixsfsivfp`, `___floatsisfvfp`, `___mulsf3vfp`, `_rand`

## Strings it references

- `0x2091A8` "Image/an2/load_p1_fire.a2b"
- `0x205F8C` "Image/a2b/baozha.a2b"
- `0x2091C4` "Image/an2/load_p2_fire.a2b"
- `0x20518C` "yuanli0001Anim"
- `0x20519C` "yuanli0001Frame"
- `0x2051AC` "yuanli0002Frame"
- `0x2051BC` "yuanli0003Frame"
- `0x205EB4` "Image/an2/yuanli.a2b"
- `0x20231C` "com15"
- `0x2091E0` "Image/an2/load_p3_fire.a2b"
- `0x205684` "Image/an2/qh2.a2b"
- `0x205EFC` "Image/an2/p4_fire.a2b"
- `0x205E9C` "Image/an2/feibiao.a2b"
- `0x2057EC` "Image/acb/Player.library.lcb"
- `0x2059C4` "Image/acb/ipad/player_fire_long.animation.acb"
- `0x205CD4` "Image/acb/player_fire_long.animation.acb"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x227386` GCC_except_table5_10
- `0x22A194` __ZN7DinoCap7g_scaleE_ptr
- `0x23B8C8` DinoCap::g_scale
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A124` __ZN9Triniti2D15_TextureManagerE_ptr
- `0x23BBA4` Triniti2D::_TextureManager
- `0x22A1A4` __ZN9Triniti2D12_FileManagerE_ptr
- `0x23BBA0` Triniti2D::_FileManager
- `0x22A15C` __ZStplIcSt11char_traitsIcESaIcEESbIT_T0_T1_ERKS6_PKS3__ptr
- `0x22A214` __ZNSt6vectorIcSaIcEED2Ev_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xC9CE0   f0b5       PUSH    {R4-R7,LR}
0xC9CE2   5e46       MOV     R6, R11
0xC9CE4   5546       MOV     R5, R10
0xC9CE6   4446       MOV     R4, R8
0xC9CE8   70b4       PUSH    {R4-R6}
0xC9CEA   1ef1fae8   BLX     ___save_vfp_d8_d15_regs
0xC9CEE   16af       ADD     R7, SP, #arg_38
0xC9CF0   abb0       SUB     SP, SP, #0xAC
0xC9CF2   e74b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xC9CFC)
0xC9CF4   6a46       MOV     R2, SP
0xC9CF6   1992       STR     R2, [SP,#0xAC+var_48]
0xC9CF8   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xC9CFA   0122       MOVS    R2, #1
0xC9CFC   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xC9CFE   0190       STR     R0, [SP,#0xAC+var_A8]
0xC9D00   0fa8       ADD     R0, SP, #0xAC+fctx; lpfctx
0xC9D02   1593       STR     R3, [SP,#0xAC+fctx.personality]
0xC9D04   e34b       LDR     R3, =(GCC_except_table5_10 - 0xC9D0C)
0xC9D06   1797       STR     R7, [SP,#0xAC+fctx.jbuf]
0xC9D08   7b44       ADD     R3, PC; GCC_except_table5_10
0xC9D0A   1693       STR     R3, [SP,#0xAC+fctx.lsda]
0xC9D0C   e24b       LDR     R3, =(loc_CA022 - 0xC9D12)
0xC9D0E   7b44       ADD     R3, PC; loc_CA022
0xC9D10   1343       ORRS    R3, R2
```

**Around `0xC9D18` (cited by the Unity code)**

```
0xC9D0A   1693       STR     R3, [SP,#0xAC+fctx.lsda]
0xC9D0C   e24b       LDR     R3, =(loc_CA022 - 0xC9D12)
0xC9D0E   7b44       ADD     R3, PC; loc_CA022
0xC9D10   1343       ORRS    R3, R2
0xC9D12   1893       STR     R3, [SP,#0xAC+var_4C]
0xC9D14   1ff19eee   BLX     __Unwind_SjLj_Register
0xC9D18   20f116eb   BLX     _rand   <==
0xC9D1C   20f126e8   BLX     ___floatsisfvfp
0xC9D20   de49       LDR     R1, =0x30000000
0xC9D22   20f17ee8   BLX     ___mulsf3vfp
0xC9D26   de49       LDR     R1, =0x40800000
0xC9D28   20f17ae8   BLX     ___mulsf3vfp
0xC9D2C   1ff1faef   BLX     ___fixsfsivfp
```

