# lifeModeScene::InitGameBackGround

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0DCAA6` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xDCAA6` |
| RVA (EA - 0x1000) | `0xDBAA6` |
| File offset | `0xDBAA6` |
| Size | 2514 code bytes in 1 chunk(s), 1003 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap13lifeModeScene18InitGameBackGroundEi` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::lifeModeScene::InitGameBackGround(int)` |
| Bytes SHA-256 | `202ef099cad65079adf0618f7b5b34fe0c305e13f5c697542174bbb5380bcfce` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameHouse.cs:28`](../../../../../Assets/Scripts/GameHouse.cs#L28) cites `0xDCAA6`
- [`Assets/Scripts/GameScene.cs:12`](../../../../../Assets/Scripts/GameScene.cs#L12) cites `0xDCAA6`
- [`Assets/Scripts/GameScenery.cs:21`](../../../../../Assets/Scripts/GameScenery.cs#L21) (by name)
- [`Assets/Scripts/GameScenery.cs:22`](../../../../../Assets/Scripts/GameScenery.cs#L22) cites `0xDD45A`

## Callers and callees (direct code references)

Callers: `sub_DD840`, `sub_DD996`, `sub_DDC00`, `sub_DDD78`, `sub_DDEC4`, `sub_DE0B8`, `sub_DE232`, `sub_DE454`, `sub_DE5AC`, `sub_DE6CA`, `sub_DE7E0`, `sub_DE958`, `sub_DEA72`, `sub_DF352`, `sub_DFE06`, `sub_EFBD6`

Callees: `CreateAudio`, [`lifeModeScene::InitBackGround`](../../../functions/dossiers/lifeModeScene/FUN-0D6A0E.md), `sub_DF352`, `sub_E0FF6`, `Triniti2D::GameContainer::CreateGameObject`, `Triniti2D::GameObject::GetCallBack`, `Triniti2D::GameObject::Container`, `Triniti2D::GameObject::Sprite`, `Triniti2D::GameObject::SetSize`, `Triniti2D::GameObject::SetPosition`, `Triniti2D::GameSprite::SetFrame`, `Triniti2D::Scene::CreateGameObject`, `___save_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___dynamic_cast`, `___mulsf3vfp`

## Strings it references

- `0x207344` "bglayerClass"
- `0x207E1C` "beijing2_1Frame"
- `0x207E2C` "beijing2_2Frame"
- `0x207E3C` "beijing2_4Frame"
- `0x2077C4` "Music_Bg_Audio"
- `0x2080F0` "Audio/Music_Bg_Shamo.wav"
- `0x2077B4` "game_bgAudio"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x227942` GCC_except_table26_4
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A0A8` __ZTIN9Triniti2D18GameObjectCallBackE_ptr
- `0x22A5D8` __ZTIN7DinoCap12bglayerClassE_ptr
- `0x22C5B0` `typeinfo for'DinoCap::bglayerClass
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22A5F8` __ZN7DinoCap10g_h_offsetE_ptr
- `0x23B918` DinoCap::g_h_offset
- `0x22A11C` __ZNSt6vectorIPN9Triniti2D10GameObjectESaIS2_EE9push_backERKS2__ptr

## Code reached from this function outside every IDA function

IDA placed these blocks in no function. They are reached by a branch or fall-through from this function, which shows reachability, not ownership. They are listed here so that no reached code is silently missing.

- `0xDD478`..`0xDD480` (reached from `0xDD474`), bytes SHA-256 `0a773097a3acb6c3...`, 4 instructions in [`unowned_blocks.jsonl`](../../../graphs/unowned_blocks.jsonl)
- `0xDDB98`..`0xDDBA0` (reached from `0xDD47E`), bytes SHA-256 `8805893b0037f300...`, 4 instructions in [`unowned_blocks.jsonl`](../../../graphs/unowned_blocks.jsonl)
- `0xDDBA0`..`0xDDC00` (reached from `0xDDB9C`), bytes SHA-256 `99f229b2b663e53e...`, 43 instructions in [`unowned_blocks.jsonl`](../../../graphs/unowned_blocks.jsonl)
- `0xDD480`..`0xDD4E2` (reached from `0xDD47C`), bytes SHA-256 `0dbcfeea6c47b3df...`, 45 instructions in [`unowned_blocks.jsonl`](../../../graphs/unowned_blocks.jsonl)

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xDCAA6   f0b5       PUSH    {R4-R7,LR}
0xDCAA8   5e46       MOV     R6, R11
0xDCAAA   5546       MOV     R5, R10
0xDCAAC   4446       MOV     R4, R8
0xDCAAE   70b4       PUSH    {R4-R6}
0xDCAB0   0bf116ea   BLX     ___save_vfp_d8_d15_regs
0xDCAB4   e54c       LDR     R4, =0xFFFFF934
0xDCAB6   16af       ADD     R7, SP, #arg_38
0xDCAB8   a544       ADD     SP, R4
0xDCABA   e54b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xDCAC4)
0xDCABC   c891       STR     R1, [SP,#0x6CC+var_3AC]
0xDCABE   ae21       MOVS    R1, #0xAE
0xDCAC0   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xDCAC2   c900       LSLS    R1, R1, #3
0xDCAC4   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xDCAC6   6944       ADD     R1, SP
0xDCAC8   e24a       LDR     R2, =0x574
0xDCACA   c990       STR     R0, [SP,#0x6CC+var_3A8]
0xDCACC   0b60       STR     R3, [R1]
0xDCACE   e24b       LDR     R3, =(GCC_except_table26_4 - 0xDCAD8)
0xDCAD0   6a44       ADD     R2, SP
0xDCAD2   b021       MOVS    R1, #0xB0
0xDCAD4   7b44       ADD     R3, PC; GCC_except_table26_4
0xDCAD6   1360       STR     R3, [R2]
```

**Around `0xDD45A` (cited by the Unity code)**

```
0xDD44E   0c60       STR     R4, [R1]
0xDD450   042c       CMP     R4, #4
0xDD452   00d0       BEQ     loc_DD456
0xDD454   54e7       B       loc_DD300
0xDD456   c998       LDR     R0, [SP,#-0x20+arg_324]; this
0xDD458   0121       MOVS    R1, #1; int
0xDD45A   f9f7d8fa   BL      __ZN7DinoCap13lifeModeScene14InitBackGroundEi; DinoCap::lifeModeScene::InitBackGround(int)   <==
0xDD45E   aa4a       LDR     R2, =(aMusicBgAudio - 0xDD466); "Music_Bg_Audio"
0xDD460   aa49       LDR     R1, =(aAudioMusicBgSh - 0xDD46A); "Audio/Music_Bg_Shamo.wav"
0xDD462   7a44       ADD     R2, PC; "Music_Bg_Audio"
0xDD464   101c       MOVS    R0, R2; this
0xDD466   7944       ADD     R1, PC; "Audio/Music_Bg_Shamo.wav"
0xDD468   b492       STR     R2, [SP,#-0x20+arg_2D0]
```

