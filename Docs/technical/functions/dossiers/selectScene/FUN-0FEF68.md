# selectScene::updateWeaponIcon

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0FEF68` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xFEF68` |
| RVA (EA - 0x1000) | `0xFDF68` |
| File offset | `0xFDF68` |
| Size | 6068 code bytes in 1 chunk(s), 2491 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene16updateWeaponIconEi` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::updateWeaponIcon(int)` |
| Bytes SHA-256 | `a1f9d9497ed1a3eb990266e9c222ddf1ceee1ecc17425ff965de0f6daad3e562` |
| Direct code callers | 7 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: [`selectScene::updateWeaponIcon`](../../../functions/dossiers/selectScene/FUN-0FEF68.md), `sub_10479C`, [`selectScene::OnUIEvent`](../../../functions/dossiers/selectScene/FUN-10B93A.md)

Callees: [`RectScale`](../../../functions/dossiers/DinoCap/FUN-017068.md), [`RectOffset`](../../../functions/dossiers/DinoCap/FUN-0170B6.md), `GCSubmitAchievement`, `subWeaponClass::~subWeaponClass`, `weaponClass::~weaponClass`, `playerTypeClass::~playerTypeClass`, [`selectScene::startMoveUIRight`](../../../functions/dossiers/selectScene/FUN-0F6FC8.md), [`selectScene::startMoveUIRight`](../../../functions/dossiers/selectScene/FUN-0FCFDC.md), [`selectScene::updateWeaponIcon`](../../../functions/dossiers/selectScene/FUN-0FEF68.md), `Triniti2D::Scene::GetUIManager`, `Triniti2D::UIImage::SetTexture`, `Triniti2D::UILabel::SetFont`, `Triniti2D::UILabel::SetColor`, `Triniti2D::UILabel::SetText`, `Triniti2D::UIManager::DeleteControl`, `___switch32`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::compare`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___fixsfsivfp`, `___floatsisfvfp`, `___mulsf3vfp`, `___subsf3vfp`, `_sprintf`

## Strings it references

- `0x20A4D0` "ui_weapon_attriFrame"
- `0x20A6F4` "ui_img_attackFrame"
- `0x20A4E8` "ui_btn_upgradeFrame"
- `0x20111C` "%d"
- `0x20A4FC` "ui_img_jiantouFrame"
- `0x20A510` "$%d"
- `0x2065EC` "Adventure_12"
- `0x2065B8` "Adventure_16"
- `0x20A514` "MAX"
- `0x20A708` "G7"
- `0x20A70C` "dinocap2hd.a9"
- `0x20A528` "ui_img_ammoFrame"
- `0x20A53C` "ui_btn_buyammoFrame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x2281F4` GCC_except_table49
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A24C` __ZN7DinoCap15g_player_vectorE_ptr
- `0x2404AC` DinoCap::g_player_vector
- `0x22A2A4` __ZN7DinoCap15playerTypeClassC2ERKS0__ptr
- `0x22A0C4` __ZN7DinoCap16g_manager_WeaponE_ptr
- `0x2404B8` DinoCap::g_manager_Weapon
- `0x22A2F4` __ZN7DinoCap11weaponClassC2ERKS0__ptr
- `0x22A2C8` __ZN7DinoCap15g_jigsaw2_countE_ptr
- `0x23E1CC` DinoCap::g_jigsaw2_count
- `0x22A6C8` __ZN9Triniti2D9UIManager13CreateControlIN7DinoCap10UIMoveCtrlEEEPT_ii_ptr
- `0x23D6B0` aRightBgRect
- `0x23D6E0` apowerRect
- `0x23D6C0` aUpBtnRect
- `0x23D730` aLPMinRect
- `0x23D740` aLPMaxRect
- `0x23D790` aLPPriRect
- `0x22A634` __ZN7DinoCap10g_w_offsetE_ptr
- `0x23B914` DinoCap::g_w_offset
- `0x22A5EC` __ZN7DinoCap18g_max_bullet_countE_ptr
- `0x23B89C` DinoCap::g_max_bullet_count
- `0x23D750` aLBMinRect
- `0x22AB9C` __ZN7DinoCap15g_jigsaw3_countE_ptr
- `0x23E1D0` DinoCap::g_jigsaw3_count
- `0x23D760` aLBMaxRect
- `0x23D7A0` aLBPriRect
- `0x23D6D0` aAMMORect

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xFEF68   f0b5       PUSH    {R4-R7,LR}
0xFEF6A   5e46       MOV     R6, R11
0xFEF6C   5546       MOV     R5, R10
0xFEF6E   4446       MOV     R4, R8
0xFEF70   70b4       PUSH    {R4-R6}
0xFEF72   e8f0b6ef   BLX     ___save_vfp_d8_d15_regs
0xFEF76   e64c       LDR     R4, =0xFFFFFBD0
0xFEF78   16af       ADD     R7, SP, #arg_38
0xFEF7A   a544       ADD     SP, R4
0xFEF7C   e54b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xFEF86)
0xFEF7E   5790       STR     R0, [SP,#0x430+var_2D4]
0xFEF80   0122       MOVS    R2, #1
0xFEF82   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xFEF84   6846       MOV     R0, SP
0xFEF86   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xFEF88   9b90       STR     R0, [SP,#0x430+var_1C4]
0xFEF8A   91a8       ADD     R0, SP, #0x430+fctx; lpfctx
0xFEF8C   9793       STR     R3, [SP,#0x430+fctx.personality]
0xFEF8E   e24b       LDR     R3, =(GCC_except_table49 - 0xFEF98)
0xFEF90   5691       STR     R1, [SP,#0x430+var_2D8]
0xFEF92   9997       STR     R7, [SP,#0x430+fctx.jbuf]
0xFEF94   7b44       ADD     R3, PC; GCC_except_table49
0xFEF96   9893       STR     R3, [SP,#0x430+fctx.lsda]
0xFEF98   e04b       LDR     R3, =(loc_1004C0 - 0xFEF9E)
```

