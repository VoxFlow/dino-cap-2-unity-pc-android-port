# menuScene::ButtonEffect

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0CC6F0` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xCC6F0` |
| RVA (EA - 0x1000) | `0xCB6F0` |
| File offset | `0xCB6F0` |
| Size | 402 code bytes in 1 chunk(s), 181 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9menuScene12ButtonEffectEPKcPN9Triniti2D9UIControlE` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::menuScene::ButtonEffect(char const*,Triniti2D::UIControl *)` |
| Bytes SHA-256 | `de9163b0111b2300f5ba01527b7a1a17410e2238239e555cb45fa55ec09901ef` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.title.tap_flow` (PROVEN, NATIVE): Tap handling: control id1 (Tap-to-start UIMoveCtrl) pop-in completion (event 1) while scene state(+12)==0 sets ready flag (+280). OnTouchEvent@0xcc8b2: if state not 1/4 and flag set: clear flag, ButtonEffect('ui_menu_startFrame', control id1), life_Mode=0, state=1. ButtonEffect@0xcc6f0 reuses/creates UIMoveCtrl id 23 (layer 3) with the same texture at the same rect and StartMove(d1=0.05,d2=0.2): a cloned overlay that...

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/MenuScene.cs:214`](../../../../../Assets/Scripts/MenuScene.cs#L214) cites `0xCC6F0`

## Callers and callees (direct code references)

Callers: [`menuScene::OnTouchEvent`](../../../functions/dossiers/menuScene/FUN-0CC8B2.md)

Callees: [`UIMoveCtrl::StartMove`](../../../functions/dossiers/UIMoveCtrl/FUN-0CD466.md), `Triniti2D::Scene::GetUIManager`, `Triniti2D::UIImage::SetTexture`, `Triniti2D::UIManager::GetControl`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___dynamic_cast`, `___mulsf3vfp`

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x227490` GCC_except_table19_4
- `0x22A5F4` __ZTIN9Triniti2D9UIControlE_ptr
- `0x22E468` `typeinfo for'Triniti2D::UIControl
- `0x22A650` __ZTIN7DinoCap10UIMoveCtrlE_ptr
- `0x22CDA0` `typeinfo for'DinoCap::UIMoveCtrl
- `0x2415E8` `vtable for'__cxxabiv1::__class_type_info
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22A6C8` __ZN9Triniti2D9UIManager13CreateControlIN7DinoCap10UIMoveCtrlEEEPT_ii_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xCC6F0   f0b5       PUSH    {R4-R7,LR}
0xCC6F2   5e46       MOV     R6, R11
0xCC6F4   5546       MOV     R5, R10
0xCC6F6   4446       MOV     R4, R8
0xCC6F8   70b4       PUSH    {R4-R6}
0xCC6FA   1bf1f2eb   BLX     ___save_vfp_d8_d15_regs
0xCC6FE   16af       ADD     R7, SP, #arg_38
0xCC700   afb0       SUB     SP, SP, #0xBC
0xCC702   604b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xCC70C)
0xCC704   0492       STR     R2, [SP,#0xBC+var_AC]
0xCC706   6a46       MOV     R2, SP
0xCC708   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xCC70A   1492       STR     R2, [SP,#0xBC+var_6C]
0xCC70C   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xCC70E   0122       MOVS    R2, #1
0xCC710   0690       STR     R0, [SP,#0xBC+var_A4]
0xCC712   1093       STR     R3, [SP,#0xBC+fctx.personality]
0xCC714   5c4b       LDR     R3, =(GCC_except_table19_4 - 0xCC71E)
0xCC716   0aa8       ADD     R0, SP, #0xBC+fctx; lpfctx
0xCC718   0591       STR     R1, [SP,#0xBC+var_A8]
0xCC71A   7b44       ADD     R3, PC; GCC_except_table19_4
0xCC71C   1193       STR     R3, [SP,#0xBC+fctx.lsda]
0xCC71E   5b4b       LDR     R3, =(loc_CC7AC - 0xCC726)
0xCC720   1297       STR     R7, [SP,#0xBC+fctx.jbuf]
```

