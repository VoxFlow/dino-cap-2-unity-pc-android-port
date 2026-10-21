# mainScene::GameOverStart

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0A6F70` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xA6F70` |
| RVA (EA - 0x1000) | `0xA5F70` |
| File offset | `0xA5F70` |
| Size | 774 code bytes in 1 chunk(s), 348 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9mainScene13GameOverStartEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::mainScene::GameOverStart(void)` |
| Bytes SHA-256 | `0eaabada8cae73e0af7195ac94c77a6d01f7c6959ddd67a44a5a78315978bda3` |
| Direct code callers | 3 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | deep: curated note below, from the batch that established it |

## What it does

Mission failed: the red missionfailbg image (control 11) and the fail line (control 17), scene state 7. Callers: hero death (playerClass / playerNewAniClass OnAnimationEnd) and SubTime (map type 6 only, not in the shipped Map.xml).

- Rule `dc2.ui.image_touch_consume.r118z4` (PROVEN, NATIVE): UIManager::HandleUITouch 0x18102e tests only GetEnable (vtable +56, UIControl +0x21, default 1) - not visibility - before HandleTouch (vtable +84). UIImage::HandleTouch 0x17f1ac and UILabel::HandleTouch 0x17fd98 return UIControl::PtInRect 0x17e70c: half-open left <= x < left+w, top <= y < top+h, and inside the clip rect (+0x24) when the clip flag (+0x22) is set. So every enabled plain image or label consumes every to...

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameCombat.cs:162`](../../../../../Assets/Scripts/GameCombat.cs#L162) cites `0xA6F70`
- [`Assets/Scripts/GameMission.cs:66`](../../../../../Assets/Scripts/GameMission.cs#L66) cites `0xA6F70`
- [`Assets/Scripts/GameMission.cs:235`](../../../../../Assets/Scripts/GameMission.cs#L235) cites `0xA6F70`
- [`Assets/Scripts/GameMission.cs:245`](../../../../../Assets/Scripts/GameMission.cs#L245) cites `0xA6F70`
- [`Assets/Scripts/GameMission.cs:252`](../../../../../Assets/Scripts/GameMission.cs#L252) cites `0xA703E`
- [`Assets/Scripts/GameMission.cs:254`](../../../../../Assets/Scripts/GameMission.cs#L254) cites `0xA7116`

## Callers and callees (direct code references)

Callers: [`mainScene::SubTime`](../../../functions/dossiers/mainScene/FUN-0A72E2.md), [`playerClass::OnAnimationEnd`](../../../functions/dossiers/playerClass/FUN-0BD978.md), [`playerNewAniClass::OnAnimationEnd`](../../../functions/dossiers/playerNewAniClass/FUN-12AA2C.md)

Callees: [`RectScale`](../../../functions/dossiers/DinoCap/FUN-017068.md), [`UIMoveCtrl::StartMove`](../../../functions/dossiers/UIMoveCtrl/FUN-0CD466.md), `Triniti2D::Scene::GetUIManager`, `Triniti2D::UIImage::SetColor`, `Triniti2D::UIImage::SetTexture`, `Triniti2D::UIManager::CreateImage`, `Triniti2D::UIManager::DeleteControl`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___mulsf3vfp`

## Strings it references

- `0x207584` "missionfailbgFrame"
- `0x207C38` "fail_lineFrame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x226AB5` GCC_except_table42
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A5F8` __ZN7DinoCap10g_h_offsetE_ptr
- `0x23B918` DinoCap::g_h_offset
- `0x22A634` __ZN7DinoCap10g_w_offsetE_ptr
- `0x23B914` DinoCap::g_w_offset
- `0x22A6C8` __ZN9Triniti2D9UIManager13CreateControlIN7DinoCap10UIMoveCtrlEEEPT_ii_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xA6F70   f0b5       PUSH    {R4-R7,LR}
0xA6F72   5e46       MOV     R6, R11
0xA6F74   5546       MOV     R5, R10
0xA6F76   4446       MOV     R4, R8
0xA6F78   70b4       PUSH    {R4-R6}
0xA6F7A   40f1b2ef   BLX     ___save_vfp_d8_d15_regs
0xA6F7E   16af       ADD     R7, SP, #arg_38
0xA6F80   c4b0       SUB     SP, SP, #0x110
0xA6F82   bd4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xA6F8C)
0xA6F84   0b90       STR     R0, [SP,#0x110+var_E4]
0xA6F86   0122       MOVS    R2, #1
0xA6F88   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xA6F8A   6846       MOV     R0, SP
0xA6F8C   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xA6F8E   1e90       STR     R0, [SP,#0x110+var_98]
0xA6F90   14a8       ADD     R0, SP, #0x110+fctx; lpfctx
0xA6F92   1a93       STR     R3, [SP,#0x110+fctx.personality]
0xA6F94   b94b       LDR     R3, =(GCC_except_table42 - 0xA6F9C)
0xA6F96   1c97       STR     R7, [SP,#0x110+fctx.jbuf]
0xA6F98   7b44       ADD     R3, PC; GCC_except_table42
0xA6F9A   1b93       STR     R3, [SP,#0x110+fctx.lsda]
0xA6F9C   b84b       LDR     R3, =(loc_A7182 - 0xA6FA2)
0xA6F9E   7b44       ADD     R3, PC; loc_A7182
0xA6FA0   1343       ORRS    R3, R2
```

**Around `0xA703E` (cited by the Unity code)**

```
0xA702E   daf017fa   BL      __ZN9Triniti2D9UIManager13DeleteControlEi; Triniti2D::UIManager::DeleteControl(int)
0xA7032   0b9a       LDR     R2, [SP,#0x110+var_E4]
0xA7034   5068       LDR     R0, [R2,#4]; this
0xA7036   cef018f9   BL      __ZN9Triniti2D5Scene12GetUIManagerEv; Triniti2D::Scene::GetUIManager(void)
0xA703A   0b21       MOVS    R1, #0xB; int
0xA703C   0422       MOVS    R2, #4; int
0xA703E   daf005fa   BL      __ZN9Triniti2D9UIManager11CreateImageEii; Triniti2D::UIManager::CreateImage(int,int)   <==
0xA7042   0b9c       LDR     R4, [SP,#0x110+var_E4]
0xA7044   94235b00   MOVS    R3, #0x128
0xA7048   9849       LDR     R1, =(aMissionfailbgf - 0xA7050); "missionfailbgFrame"
0xA704A   994a       LDR     R2, =0x10F
0xA704C   7944       ADD     R1, PC; "missionfailbgFrame"
0xA704E   6a44       ADD     R2, SP
```

**Around `0xA7116` (cited by the Unity code)**

```
0xA710A   1193       STR     R3, [SP,#0x110+var_CC]
0xA710C   6c4b       LDR     R3, =(__ZN7DinoCap8g_iphoneE_ptr - 0xA7116)
0xA710E   1092       STR     R2, [SP,#0x110+var_D0]
0xA7110   1390       STR     R0, [SP,#0x110+var_C4]
0xA7112   7b44       ADD     R3, PC; __ZN7DinoCap8g_iphoneE_ptr
0xA7114   1b68       LDR     R3, [R3]; DinoCap::g_iphone ...
0xA7116   1b78       LDRB    R3, [R3]; DinoCap::g_iphone   <==
0xA7118   002b       CMP     R3, #0
0xA711A   13d1       BNE     loc_A7144
0xA711C   6949       LDR     R1, =(__ZN7DinoCap10g_h_offsetE_ptr - 0xA7124)
0xA711E   6148       LDR     R0, =0x3F000000
0xA7120   7944       ADD     R1, PC; __ZN7DinoCap10g_h_offsetE_ptr
0xA7122   0968       LDR     R1, [R1]; DinoCap::g_h_offset ...
```

