# selectScene::updateWeaponIconNoMove

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0F78A8` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xF78A8` |
| RVA (EA - 0x1000) | `0xF68A8` |
| File offset | `0xF68A8` |
| Size | 5262 code bytes in 1 chunk(s), 2144 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene22updateWeaponIconNoMoveEii` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::updateWeaponIconNoMove(int,int)` |
| Bytes SHA-256 | `a3c5cc3a8097fb87efb0c3da64796481c2c79d7e54eb42e867fd1890d9dac0b0` |
| Direct code callers | 2 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/SelectArmory.cs:155`](../../../../../Assets/Scripts/SelectArmory.cs#L155) cites `0xF85D8`

## Callers and callees (direct code references)

Callers: [`selectScene::updateWeaponIconNoMove`](../../../functions/dossiers/selectScene/FUN-0F78A8.md), [`selectScene::OnUIEvent`](../../../functions/dossiers/selectScene/FUN-10B93A.md)

Callees: [`RectScale`](../../../functions/dossiers/DinoCap/FUN-017068.md), [`RectOffset`](../../../functions/dossiers/DinoCap/FUN-0170B6.md), `subWeaponClass::~subWeaponClass`, `weaponClass::~weaponClass`, `playerTypeClass::~playerTypeClass`, [`selectScene::startScaleUI`](../../../functions/dossiers/selectScene/FUN-0F77DC.md), [`selectScene::updateWeaponIconNoMove`](../../../functions/dossiers/selectScene/FUN-0F78A8.md), `Triniti2D::Scene::GetUIManager`, `Triniti2D::UIImage::SetTexture`, `Triniti2D::UILabel::SetFont`, `Triniti2D::UILabel::SetColor`, `Triniti2D::UILabel::SetText`, `Triniti2D::UIManager::GetControl`, `Triniti2D::UIManager::DeleteControl`, `___switch32`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___dynamic_cast`, `___fixsfsivfp`, `___floatsisfvfp`, `___mulsf3vfp`, `___subsf3vfp`, `_sprintf`

## Strings it references

- `0x20A4D0` "ui_weapon_attriFrame"
- `0x20A4E8` "ui_btn_upgradeFrame"
- `0x20111C` "%d"
- `0x20A4FC` "ui_img_jiantouFrame"
- `0x20A510` "$%d"
- `0x2065EC` "Adventure_12"
- `0x2065B8` "Adventure_16"
- `0x20A514` "MAX"
- `0x20A518` "icon_%s_1Frame"
- `0x207B6C` "icon_%sFrame"
- `0x20A528` "ui_img_ammoFrame"
- `0x20A53C` "ui_btn_buyammoFrame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x228052` GCC_except_table33_3
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
- `0x23D6C0` aUpBtnRect
- `0x23D790` aLPPriRect
- `0x22A634` __ZN7DinoCap10g_w_offsetE_ptr
- `0x23B914` DinoCap::g_w_offset
- `0x22A7E4` __ZN7DinoCap11weaponClassaSERKS0__ptr
- `0x22A5F4` __ZTIN9Triniti2D9UIControlE_ptr
- `0x22E468` `typeinfo for'Triniti2D::UIControl
- `0x22A650` __ZTIN7DinoCap10UIMoveCtrlE_ptr
- `0x22CDA0` `typeinfo for'DinoCap::UIMoveCtrl
- `0x2415E8` `vtable for'__cxxabiv1::__class_type_info
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22A5EC` __ZN7DinoCap18g_max_bullet_countE_ptr
- `0x23B89C` DinoCap::g_max_bullet_count
- `0x22AB9C` __ZN7DinoCap15g_jigsaw3_countE_ptr
- `0x23E1D0` DinoCap::g_jigsaw3_count

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xF78A8   f0b5       PUSH    {R4-R7,LR}
0xF78AA   5e46       MOV     R6, R11
0xF78AC   5546       MOV     R5, R10
0xF78AE   4446       MOV     R4, R8
0xF78B0   70b4       PUSH    {R4-R6}
0xF78B2   f0f016eb   BLX     ___save_vfp_d8_d15_regs
0xF78B6   e04c       LDR     R4, =0xFFFFFC30
0xF78B8   16af       ADD     R7, SP, #arg_38
0xF78BA   a544       ADD     SP, R4
0xF78BC   df4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xF78C6)
0xF78BE   4d90       STR     R0, [SP,#0x3D0+var_29C]
0xF78C0   4b92       STR     R2, [SP,#0x3D0+var_2A4]
0xF78C2   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xF78C4   6846       MOV     R0, SP
0xF78C6   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xF78C8   0122       MOVS    R2, #1
0xF78CA   8f90       STR     R0, [SP,#0x3D0+var_194]
0xF78CC   8b93       STR     R3, [SP,#0x3D0+fctx.personality]
0xF78CE   dc4b       LDR     R3, =(GCC_except_table33_3 - 0xF78D8)
0xF78D0   85a8       ADD     R0, SP, #0x3D0+fctx; lpfctx
0xF78D2   4c91       STR     R1, [SP,#0x3D0+var_2A0]
0xF78D4   7b44       ADD     R3, PC; GCC_except_table33_3
0xF78D6   8c93       STR     R3, [SP,#0x3D0+fctx.lsda]
0xF78D8   da4b       LDR     R3, =(loc_F8ABE - 0xF78E0)
```

**Around `0xF85D8` (cited by the Unity code)**

```
0xF85CC   1b68       LDR     R3, [R3]
0xF85CE   9847       BLX     R3
0xF85D0   4d9b       LDR     R3, [SP,#0x3D0+var_29C]
0xF85D2   1f9c       LDR     R4, [SP,#0x3D0+var_354]
0xF85D4   9a6b       LDR     R2, [R3,#0x38]
0xF85D6   2168       LDR     R1, [R4]; DinoCap::g_player_vector
0xF85D8   1301       LSLS    R3, R2, #4   <==
0xF85DA   9b1a       SUBS    R3, R3, R2
0xF85DC   db00       LSLS    R3, R3, #3
0xF85DE   5b18       ADDS    R3, R3, R1
0xF85E0   db6e       LDR     R3, [R3,#0x6C]
0xF85E2   5969       LDR     R1, [R3,#0x14]
0xF85E4   d29b       LDR     R3, [SP,#0x3D0+var_88]
```

