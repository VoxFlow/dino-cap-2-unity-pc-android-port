# CompAnimObj::OnUpdateSprite

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-1189EC` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x1189EC` |
| RVA (EA - 0x1000) | `0x1179EC` |
| File offset | `0x1179EC` |
| Size | 1350 code bytes in 1 chunk(s), 590 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11CompAnimObj14OnUpdateSpriteEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::CompAnimObj::OnUpdateSprite(void)` |
| Bytes SHA-256 | `35d3f1ee9b9ad08ce00074eb6f75951069b2aa84433eb2d2000d8b018c010fb1` |
| Direct code callers | 5 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.hero.preview_component_scale.r12` (PROVEN, NATIVE): CompAnimObj scales local x/y and scaleX/scaleY by callback+228 before world position; ARM0x118c3a..0x118c86. InitNewCharacter writes1.5 at0xf4cbc. Default scale1 retained for existing loading/jigsaw callers.

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/NativeCompObj.cs:62`](../../../../../Assets/Scripts/NativeCompObj.cs#L62) cites `0x1189EC`
- [`Assets/Scripts/NativeCompObj.cs:70`](../../../../../Assets/Scripts/NativeCompObj.cs#L70) cites `0x1189EC`
- [`Assets/Scripts/NativeCompObj.cs:306`](../../../../../Assets/Scripts/NativeCompObj.cs#L306) cites `0x1189EC`
- [`Assets/Scripts/NativeComponentAnimation.cs:111`](../../../../../Assets/Scripts/NativeComponentAnimation.cs#L111) cites `0x118C8C`
- [`Assets/Scripts/NativeComponentAnimation.cs:159`](../../../../../Assets/Scripts/NativeComponentAnimation.cs#L159) cites `0x118C3A`
- [`Assets/Scripts/NativeComponentAnimation.cs:180`](../../../../../Assets/Scripts/NativeComponentAnimation.cs#L180) cites `0x118C8C`

## Callers and callees (direct code references)

Callers: [`CompAnimObj::PlayAnimationAdv`](../../../functions/dossiers/CompAnimObj/FUN-118F4C.md), [`CompAnimObj::PlayAnimation`](../../../functions/dossiers/CompAnimObj/FUN-119010.md), [`CompAnimObj::OnUpdate`](../../../functions/dossiers/CompAnimObj/FUN-1190D4.md), [`CompAnimObj::SetAnimationAdv`](../../../functions/dossiers/CompAnimObj/FUN-119102.md), [`CompAnimObj::SetAnimation`](../../../functions/dossiers/CompAnimObj/FUN-1198AC.md)

Callees: `ModPi`, `CompAnimObj::GetRenderTextureId`, `CAComponent::GetFrame`, `ComponentAnimation::GetComponent`, `Triniti2D::GameObject::GetPosition`, `Triniti2D::GameObject::GetRotation`, `Triniti2D::TextureManager::GetFrame`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::assign`, `std::string::~string`, `___addsf3vfp`, `___divsf3vfp`, `___fixunssfsivfp`, `___floatsisfvfp`, `___floatunssisfvfp`, `___gtsf2vfp`, `___ltsf2vfp`, `___mulsf3vfp`, `___nesf2vfp`, `___subsf3vfp`, `_cosf`, `_sinf`

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x228AB6` GCC_except_table54_0
- `0x22A2A8` __ZNKSt6vectorIiSaIiEE4sizeEv_ptr
- `0x22AEDC` __ZNSt6vectorIPN7DinoCap11CAComponentESaIS2_EEC2ERKS4__ptr
- `0x22A300` __ZStplIcSt11char_traitsIcESaIcEESbIT_T0_T1_ERKS6_S8__ptr
- `0x22A124` __ZN9Triniti2D15_TextureManagerE_ptr
- `0x23BBA4` Triniti2D::_TextureManager
- `0x22AC58` __ZNSt6vectorIPN9Triniti2D6SpriteESaIS2_EE9push_backERKS2__ptr
- `0x22AF8C` __ZNSt6vectorIPN7DinoCap11CAComponentESaIS2_EED2Ev_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x1189EC  f0b5       PUSH    {R4-R7,LR}
0x1189EE  5e46       MOV     R6, R11
0x1189F0  5546       MOV     R5, R10
0x1189F2  4446       MOV     R4, R8
0x1189F4  70b4       PUSH    {R4-R6}
0x1189F6  cff074ea   BLX     ___save_vfp_d8_d15_regs
0x1189FA  16af       ADD     R7, SP, #arg_38
0x1189FC  cbb0       SUB     SP, SP, #0x12C
0x1189FE  844b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x118A08)
0x118A00  0122       MOVS    R2, #1
0x118A02  0990       STR     R0, [SP,#0x12C+var_108]
0x118A04  7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x118A06  6846       MOV     R0, SP
0x118A08  1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x118A0A  3b90       STR     R0, [SP,#0x12C+var_40]
0x118A0C  31a8       ADD     R0, SP, #0x12C+fctx; lpfctx
0x118A0E  3793       STR     R3, [SP,#0x12C+fctx.personality]
0x118A10  804b       LDR     R3, =(GCC_except_table54_0 - 0x118A18)
0x118A12  3997       STR     R7, [SP,#0x12C+fctx.jbuf]
0x118A14  7b44       ADD     R3, PC; GCC_except_table54_0
0x118A16  3893       STR     R3, [SP,#0x12C+fctx.lsda]
0x118A18  7f4b       LDR     R3, =(loc_118B48 - 0x118A1E)
0x118A1A  7b44       ADD     R3, PC; loc_118B48
0x118A1C  1343       ORRS    R3, R2
```

**Around `0x118C3A` (cited by the Unity code)**

```
0x118C08  fff752fe   BL      __ZN7DinoCap11CompAnimObj18GetRenderTextureIdEPNS_11CAComponentEPNS1_5FrameE; DinoCap::CompAnimObj::GetRenderTextureId(DinoCap::CAComponent *,DinoCap::CAComponent::Frame *)
0x118C0C  2190       STR     R0, [SP,#0x12C+var_A8]
0x118C0E  14e0       B       loc_118C3A
0x118C34  0122       MOVS    R2, #1
0x118C36  5242       NEGS    R2, R2
0x118C38  2192       STR     R2, [SP,#0x12C+var_A8]
0x118C3A  be4b       LDR     R3, =0   <==
0x118C3C  099c       LDR     R4, [SP,#0x12C+var_108]
0x118C3E  0f99       LDR     R1, [SP,#0x12C+var_F0]
0x118C40  2793       STR     R3, [SP,#0x12C+var_90]
0x118C42  2893       STR     R3, [SP,#0x12C+var_8C]
0x118C44  e423       MOVS    R3, #0xE4
0x118C46  e358       LDR     R3, [R4,R3]
```

**Around `0x118C8C` (cited by the Unity code)**

```
0x118C80  2b9b       LDR     R3, [SP,#0x12C+var_80]
0x118C82  df20       MOVS    R0, #0xDF
0x118C84  0d92       STR     R2, [SP,#0x12C+var_F8]
0x118C86  0e93       STR     R3, [SP,#0x12C+var_F4]
0x118C88  235c       LDRB    R3, [R4,R0]
0x118C8A  002b       CMP     R3, #0
0x118C8C  0dd0       BEQ     loc_118CAA   <==
0x118C8E  0f99       LDR     R1, [SP,#0x12C+var_F0]
0x118C90  80221206   MOVS    R2, #0x80000000
0x118C94  8b18       ADDS    R3, R1, R2
0x118C96  0f93       STR     R3, [SP,#0x12C+var_F0]
0x118C98  4a9b       LDR     R3, [SP,#0x12C+var_4]
0x118C9A  0b9c       LDR     R4, [SP,#0x12C+var_100]
```

