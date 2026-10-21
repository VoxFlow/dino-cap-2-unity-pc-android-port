# lifeModeScene::OnUpdate

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0D41BA` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xD41BA` |
| RVA (EA - 0x1000) | `0xD31BA` |
| File offset | `0xD31BA` |
| Size | 2414 code bytes in 1 chunk(s), 948 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap13lifeModeScene8OnUpdateEf` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::lifeModeScene::OnUpdate(float)` |
| Bytes SHA-256 | `8ce4cfb4ef65e93f4351be3f6d7f497bc8bb2852b327db1d95614d1f8194aa22` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / VERIFIED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.ui.image_touch_consume.r118z4` (PROVEN, NATIVE): UIManager::HandleUITouch 0x18102e tests only GetEnable (vtable +56, UIControl +0x21, default 1) - not visibility - before HandleTouch (vtable +84). UIImage::HandleTouch 0x17f1ac and UILabel::HandleTouch 0x17fd98 return UIControl::PtInRect 0x17e70c: half-open left <= x < left+w, top <= y < top+h, and inside the clip rect (+0x24) when the clip flag (+0x22) is set. So every enabled plain image or label consumes every to...

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameControls.cs:186`](../../../../../Assets/Scripts/GameControls.cs#L186) cites `0xD45B0`
- [`Assets/Scripts/GameMission.cs:303`](../../../../../Assets/Scripts/GameMission.cs#L303) cites `0xD41BA`
- [`Assets/Scripts/GameMission.cs:318`](../../../../../Assets/Scripts/GameMission.cs#L318) cites `0xD41BA`
- [`Assets/Scripts/GameMission.cs:328`](../../../../../Assets/Scripts/GameMission.cs#L328) cites `0xD47F6`
- [`Assets/Scripts/GameScene.cs:320`](../../../../../Assets/Scripts/GameScene.cs#L320) cites `0xD41BA`
- [`Assets/Scripts/GameWeather.cs:9`](../../../../../Assets/Scripts/GameWeather.cs#L9) cites `0xD41BA`
- [`Assets/Scripts/GameWeather.cs:97`](../../../../../Assets/Scripts/GameWeather.cs#L97) cites `0xD41BA`
- [`Assets/Scripts/GameWeather.cs:127`](../../../../../Assets/Scripts/GameWeather.cs#L127) (by name)

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: `Triniti2D::Camera::GetViewport`, `Triniti2D::Console::WriteLine`, `Triniti2D::GameObject::SetPosition`, `Triniti2D::Scene::GetCamera`, `Triniti2D::Scene::GetUIManager`, `Triniti2D::UIImage::SetColor`, `Triniti2D::UIImage::SetTexture`, `Triniti2D::UIManager::CreateImage`, `Triniti2D::UIManager::DeleteControl`, `Triniti2D::WebGameClient::RecvResponse`, `___switch32`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___fixunssfsivfp`, `___gtsf2vfp`, `___lesf2vfp`, `___ltsf2vfp`, `___mulsf3vfp`, `___unordsf2vfp`

## Strings it references

- `0x2074FC` "success \| requestId %d \| result %d \| name %s \| action %s \| data %s"
- `0x207540` "-- fail \| requestId %d \| result %d \| name %s \| action %s \| data %s"
- `0x2064B4` "whiteFrame"
- `0x207584` "missionfailbgFrame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x22771B` GCC_except_table21_5
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A680` __ZN7DinoCap9requestIdE_ptr
- `0x23B91C` DinoCap::requestId
- `0x22A320` __ZN9Triniti2D6ModuleINS_13WebGameClientEE10m_instanceE_ptr
- `0x22A634` __ZN7DinoCap10g_w_offsetE_ptr
- `0x23B914` DinoCap::g_w_offset
- `0x22A5F8` __ZN7DinoCap10g_h_offsetE_ptr
- `0x23B918` DinoCap::g_h_offset

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xD41BA   f0b5       PUSH    {R4-R7,LR}
0xD41BC   5e46       MOV     R6, R11
0xD41BE   5546       MOV     R5, R10
0xD41C0   4446       MOV     R4, R8
0xD41C2   70b4       PUSH    {R4-R6}
0xD41C4   13f18cee   BLX     ___save_vfp_d8_d15_regs
0xD41C8   16af       ADD     R7, SP, #arg_38
0xD41CA   f3b0       SUB     SP, SP, #0x1CC
0xD41CC   c14b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xD41D6)
0xD41CE   0122       MOVS    R2, #1
0xD41D0   1f90       STR     R0, [SP,#0x1CC+var_150]
0xD41D2   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xD41D4   1e91       STR     R1, [SP,#0x1CC+var_154]
0xD41D6   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xD41D8   6946       MOV     R1, SP
0xD41DA   3ca8       ADD     R0, SP, #0x1CC+fctx; lpfctx
0xD41DC   4293       STR     R3, [SP,#0x1CC+fctx.personality]
0xD41DE   be4b       LDR     R3, =(GCC_except_table21_5 - 0xD41E8)
0xD41E0   4497       STR     R7, [SP,#0x1CC+fctx.jbuf]
0xD41E2   4691       STR     R1, [SP,#0x1CC+var_B4]
0xD41E4   7b44       ADD     R3, PC; GCC_except_table21_5
0xD41E6   4393       STR     R3, [SP,#0x1CC+fctx.lsda]
0xD41E8   bc4b       LDR     R3, =(loc_D4A8C - 0xD41EE)
0xD41EA   7b44       ADD     R3, PC; loc_D4A8C
```

**Around `0xD45B0` (cited by the Unity code)**

```
0xD45A2   1f9c       LDR     R4, [SP,#0x1CC+var_150]
0xD45A4   86225200   MOVS    R2, #0x10C
0xD45A8   1092       STR     R2, [SP,#0x1CC+var_18C]
0xD45AA   a35c       LDRB    R3, [R4,R2]
0xD45AC   002b       CMP     R3, #0
0xD45AE   3ed0       BEQ     loc_D462E
0xD45B0   cc4b       LDR     R3, =(__ZN7DinoCap8g_iphoneE_ptr - 0xD45B6)   <==
0xD45B2   7b44       ADD     R3, PC; __ZN7DinoCap8g_iphoneE_ptr
0xD45B4   1b68       LDR     R3, [R3]; DinoCap::g_iphone ...
0xD45B6   0f93       STR     R3, [SP,#0x1CC+var_190]
0xD45B8   1b78       LDRB    R3, [R3]; DinoCap::g_iphone
0xD45BA   002b       CMP     R3, #0
0xD45BC   04d1       BNE     loc_D45C8
```

**Around `0xD47F6` (cited by the Unity code)**

```
0xD47E8   4868       LDR     R0, [R1,#4]; this
0xD47EA   0393       STR     R3, [SP,#0x1CC+var_1C0]
0xD47EC   3d93       STR     R3, [SP,#0x1CC+fctx.call_site]
0xD47EE   a0f03cfd   BL      __ZN9Triniti2D5Scene12GetUIManagerEv; Triniti2D::Scene::GetUIManager(void)
0xD47F2   0b21       MOVS    R1, #0xB; int
0xD47F4   0322       MOVS    R2, #3; int
0xD47F6   acf029fe   BL      __ZN9Triniti2D9UIManager11CreateImageEii; Triniti2D::UIManager::CreateImage(int,int)   <==
0xD47FA   494b       LDR     R3, =(__ZN7DinoCap8g_iphoneE_ptr - 0xD4804)
0xD47FC   1f9c       LDR     R4, [SP,#0x1CC+var_150]
0xD47FE   0a99       LDR     R1, [SP,#0x1CC+var_1A4]
0xD4800   7b44       ADD     R3, PC; __ZN7DinoCap8g_iphoneE_ptr
0xD4802   1b68       LDR     R3, [R3]; DinoCap::g_iphone ...
0xD4804   3090       STR     R0, [SP,#0x1CC+var_10C]
```

