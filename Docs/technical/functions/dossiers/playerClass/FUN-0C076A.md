# playerClass::OnKeyFrameAdv

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0C076A` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xC076A` |
| RVA (EA - 0x1000) | `0xBF76A` |
| File offset | `0xBF76A` |
| Size | 470 code bytes in 1 chunk(s), 185 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11playerClass13OnKeyFrameAdvEm` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerClass::OnKeyFrameAdv(unsigned long)` |
| Bytes SHA-256 | `18cee9886f307bfaac76f12c3d803737443ae2e5daa593d432ba6903374ec8f5` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / VERIFIED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameFire.cs:6`](../../../../../Assets/Scripts/GameFire.cs#L6) cites `0xC076A`
- [`Assets/Scripts/GameFire.cs:86`](../../../../../Assets/Scripts/GameFire.cs#L86) cites `0xC076A`
- [`Assets/Scripts/GameFire.cs:100`](../../../../../Assets/Scripts/GameFire.cs#L100) cites `0xC076A`

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: [`mainScene::UpdateBullet`](../../../functions/dossiers/mainScene/FUN-09546C.md), [`playerClass::OnFireUp`](../../../functions/dossiers/playerClass/FUN-0B8BC6.md), [`playerClass::FeiBiao`](../../../functions/dossiers/playerClass/FUN-0BC868.md), [`playerClass::JuPan`](../../../functions/dossiers/playerClass/FUN-0BCA10.md), [`playerClass::PenHuoQ`](../../../functions/dossiers/playerClass/FUN-0BEB28.md), [`playerClass::JiGuangQ`](../../../functions/dossiers/playerClass/FUN-0BEDA6.md), [`playerClass::LiuDanQ`](../../../functions/dossiers/playerClass/FUN-0BFE94.md), [`playerClass::KaiQiang`](../../../functions/dossiers/playerClass/FUN-0C0336.md), `subWeaponClass::~subWeaponClass`, `Triniti2D::GameObject::GetScene`, `Triniti2D::Scene::GetCallBack`, `___switch32`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `___dynamic_cast`

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x227036` GCC_except_table30_1
- `0x22A24C` __ZN7DinoCap15g_player_vectorE_ptr
- `0x2404AC` DinoCap::g_player_vector
- `0x22A2B8` __ZN7DinoCap13g_player_typeE_ptr
- `0x23B86C` DinoCap::g_player_type
- `0x22A2F0` __ZN7DinoCap12g_b_FireDownE_ptr
- `0x23B8C3` DinoCap::g_b_FireDown
- `0x22A0C0` __ZTIN9Triniti2D13SceneCallBackE_ptr
- `0x22A0C8` __ZTIN7DinoCap9mainSceneE_ptr
- `0x22C868` `typeinfo for'DinoCap::mainScene
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xC076A   f0b5       PUSH    {R4-R7,LR}
0xC076C   5e46       MOV     R6, R11
0xC076E   5546       MOV     R5, R10
0xC0770   4446       MOV     R4, R8
0xC0772   70b4       PUSH    {R4-R6}
0xC0774   27f1b4eb   BLX     ___save_vfp_d8_d15_regs
0xC0778   16af       ADD     R7, SP, #arg_38
0xC077A   9cb0       SUB     SP, SP, #0x70 ; 'p'
0xC077C   704b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xC0786)
0xC077E   0122       MOVS    R2, #1
0xC0780   0590       STR     R0, [SP,#0x70+var_5C]
0xC0782   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xC0784   0491       STR     R1, [SP,#0x70+var_60]
0xC0786   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xC0788   6946       MOV     R1, SP
0xC078A   08a8       ADD     R0, SP, #0x70+fctx; lpfctx
0xC078C   0e93       STR     R3, [SP,#0x70+fctx.personality]
0xC078E   6d4b       LDR     R3, =(GCC_except_table30_1 - 0xC0798)
0xC0790   1097       STR     R7, [SP,#0x70+fctx.jbuf]
0xC0792   1291       STR     R1, [SP,#0x70+var_28]
0xC0794   7b44       ADD     R3, PC; GCC_except_table30_1
0xC0796   0f93       STR     R3, [SP,#0x70+fctx.lsda]
0xC0798   6b4b       LDR     R3, =(loc_C0904 - 0xC079E)
0xC079A   7b44       ADD     R3, PC; loc_C0904
```

