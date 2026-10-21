# lifeModeScene::OnTouchEvent

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0D15E4` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xD15E4` |
| RVA (EA - 0x1000) | `0xD05E4` |
| File offset | `0xD05E4` |
| Size | 2414 code bytes in 1 chunk(s), 928 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap13lifeModeScene12OnTouchEventEiiii` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::lifeModeScene::OnTouchEvent(int,int,int,int)` |
| Bytes SHA-256 | `95edf8b7df6c8b0d7c205a348aae94d8524949ea31687556e8c007577ca4d17e` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | GAMEPLAY / PORTED |
| Depth | deep: curated note below, from the batch that established it |

## What it does

All battle touches: one finger id per control, arrow takeover on slide, release-rect rules for the weapon and change buttons, pause from the stop rect only while the scene state is 4. The PC keys and mouse press these same rects.

- Rule `dc2.game.mission_intro.r118` (PROVEN, NATIVE): Every mission (mainScene run/egg and lifeModeScene) starts with state +340 = 1, opaque black cover (id 11, fades 255 -> 0 at 192/s), LevelBGEnter band (id 12), LevelEnter "dayFrame" + g_mission label (ids 13/14), 0.5 s hold, LevelLeave, LevelBGLeave, StartGame_N (state 4, spawn timers). Input is ignored until state 4 (OnTouchEvent 0xd163c).

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameControls.cs:9`](../../../../../Assets/Scripts/GameControls.cs#L9) cites `0xD15E4`
- [`Assets/Scripts/GameControls.cs:13`](../../../../../Assets/Scripts/GameControls.cs#L13) cites `0xD15E4`
- [`Assets/Scripts/GameControls.cs:39`](../../../../../Assets/Scripts/GameControls.cs#L39) cites `0xD163C`
- [`Assets/Scripts/GameControls.cs:45`](../../../../../Assets/Scripts/GameControls.cs#L45) cites `0xD1C36`
- [`Assets/Scripts/GameControls.cs:77`](../../../../../Assets/Scripts/GameControls.cs#L77) cites `0xD1CC0`
- [`Assets/Scripts/GameControls.cs:463`](../../../../../Assets/Scripts/GameControls.cs#L463) cites `0xD1BA4`
- [`Assets/Scripts/GameScene.cs:221`](../../../../../Assets/Scripts/GameScene.cs#L221) cites `0xD15E4`
- [`Assets/Scripts/GameScene.cs:226`](../../../../../Assets/Scripts/GameScene.cs#L226) cites `0xD1706`

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: [`RectScale`](../../../functions/dossiers/DinoCap/FUN-017068.md), `TadShow`, [`PlaySound`](../../../functions/dossiers/DinoCap/FUN-01A448.md), [`playerClass::OnFireDown`](../../../functions/dossiers/playerClass/FUN-0B9990.md), [`playerClass::SwitchWeapon`](../../../functions/dossiers/playerClass/FUN-0BAC3C.md), [`playerClass::OnRollDown`](../../../functions/dossiers/playerClass/FUN-0BD668.md), [`playerClass::OnHitDown`](../../../functions/dossiers/playerClass/FUN-0BD7C8.md), [`pauseCtrl::Enter`](../../../functions/dossiers/pauseCtrl/FUN-0CED0C.md), `sub_D1F52`, `mapClass::~mapClass`, [`playerNewAniClass::OnHitDown`](../../../functions/dossiers/playerNewAniClass/FUN-127954.md), [`playerNewAniClass::OnRollDown`](../../../functions/dossiers/playerNewAniClass/FUN-12815C.md), [`playerNewAniClass::OnFireDown`](../../../functions/dossiers/playerNewAniClass/FUN-12A178.md), [`playerNewAniClass::SwitchWeapon`](../../../functions/dossiers/playerNewAniClass/FUN-12AE48.md), `Triniti2D::GameObject::GetCallBack`, `Triniti2D::Scene::SetTimeFactorEx`, `Triniti2D::Scene::GetUIManager`, `___save_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `___addsf3vfp`, `___dynamic_cast`, `___floatsisfvfp`, `___gesf2vfp`, `___subsf3vfp`, `___unordsf2vfp`

## Strings it references

- `0x204850` "Audio_Button1Audio"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x22758A` GCC_except_table16_5
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x23D400` leftctrlbtnRect
- `0x23D410` rightctrlbtnRect
- `0x23D450` stopRect
- `0x22A634` __ZN7DinoCap10g_w_offsetE_ptr
- `0x23B914` DinoCap::g_w_offset
- `0x22A5F8` __ZN7DinoCap10g_h_offsetE_ptr
- `0x23B918` DinoCap::g_h_offset
- `0x23D430` firectrlbtnRect
- `0x23D420` hitctrlbtnRect
- `0x23D3F0` rotatectrlRect
- `0x22A2E8` __ZNK9Triniti2D4Rect8PtInRectERKNS_7Vector2E_ptr
- `0x23BA28` leftCtrlID
- `0x22A74C` __ZN7DinoCap12g_b_LeftDownE_ptr
- `0x23B8C0` DinoCap::g_b_LeftDown
- `0x23BA2C` rightCtrlID
- `0x22A6EC` __ZN7DinoCap13g_b_RightDownE_ptr
- `0x23B8C1` DinoCap::g_b_RightDown
- `0x23B8C2` DinoCap::g_b_HitDown
- `0x23B8C3` DinoCap::g_b_FireDown
- `0x23BA30` hitCtrlID
- `0x22A640` __ZN7DinoCap11g_b_HitDownE_ptr
- `0x22A2B8` __ZN7DinoCap13g_player_typeE_ptr
- `0x23B86C` DinoCap::g_player_type
- `0x22A0A8` __ZTIN9Triniti2D18GameObjectCallBackE_ptr
- `0x22A288` __ZTIN7DinoCap17playerNewAniClassE_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xD15E4   f0b5       PUSH    {R4-R7,LR}
0xD15E6   5e46       MOV     R6, R11
0xD15E8   5546       MOV     R5, R10
0xD15EA   4446       MOV     R4, R8
0xD15EC   70b4       PUSH    {R4-R6}
0xD15EE   16f178ec   BLX     ___save_vfp_d8_d15_regs
0xD15F2   16af       ADD     R7, SP, #arg_38
0xD15F4   d4b0       SUB     SP, SP, #0x150; int
0xD15F6   0b93       STR     R3, [SP,#0x150+var_124]; int
0xD15F8   d94b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xD1602)
0xD15FA   0c92       STR     R2, [SP,#0x150+var_120]; int
0xD15FC   0122       MOVS    R2, #1
0xD15FE   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xD1600   0e90       STR     R0, [SP,#0x150+var_118]; int
0xD1602   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xD1604   0d91       STR     R1, [SP,#0x150+var_11C]; int
0xD1606   10a8       ADD     R0, SP, #0x150+fctx; lpfctx
0xD1608   1693       STR     R3, [SP,#0x150+fctx.personality]
0xD160A   d64b       LDR     R3, =(GCC_except_table16_5 - 0xD1614)
0xD160C   6946       MOV     R1, SP
0xD160E   1a91       STR     R1, [SP,#0x150+var_E8]
0xD1610   7b44       ADD     R3, PC; GCC_except_table16_5
0xD1612   1793       STR     R3, [SP,#0x150+fctx.lsda]
0xD1614   d44b       LDR     R3, =(loc_D1C02 - 0xD161C)
```

**Around `0xD163C` (cited by the Unity code)**

```
0xD162C   18f19eeb   BLX     ___floatsisfvfp
0xD1630   0e9a       LDR     R2, [SP,#0x150+var_118]; int
0xD1632   aa235b00   MOVS    R3, #0x154
0xD1636   5390       STR     R0, [SP,#0x150+var_4]
0xD1638   d358       LDR     R3, [R2,R3]; int
0xD163A   042b       CMP     R3, #4
0xD163C   01d0       BEQ     loc_D1642   <==
0xD163E   00f088fc   BL      sub_D1F52
0xD1642   ca4b       LDR     R3, =(__ZN7DinoCap8g_iphoneE_ptr - 0xD1648)
0xD1644   7b44       ADD     R3, PC; __ZN7DinoCap8g_iphoneE_ptr
0xD1646   1b68       LDR     R3, [R3]; DinoCap::g_iphone ...
0xD1648   1b78       LDRB    R3, [R3]; DinoCap::g_iphone
0xD164A   002b       CMP     R3, #0
```

**Around `0xD1706` (cited by the Unity code)**

```
0xD16F8   4698       LDR     R0, [SP,#0x150+var_38]
0xD16FA   7944       ADD     R1, PC; __ZN7DinoCap10g_w_offsetE_ptr
0xD16FC   0968       LDR     R1, [R1]; DinoCap::g_w_offset ...
0xD16FE   0968       LDR     R1, [R1]; DinoCap::g_w_offset
0xD1700   18f1b6ea   BLX     ___addsf3vfp
0xD1704   a649       LDR     R1, =0x42000000
0xD1706   4690       STR     R0, [SP,#0x150+var_38]   <==
0xD1708   a648       LDR     R0, =(__ZN7DinoCap10g_h_offsetE_ptr - 0xD170E)
0xD170A   7844       ADD     R0, PC; __ZN7DinoCap10g_h_offsetE_ptr
0xD170C   0068       LDR     R0, [R0]; DinoCap::g_h_offset ...
0xD170E   0068       LDR     R0, [R0]; DinoCap::g_h_offset
0xD1710   18f19eeb   BLX     ___subsf3vfp
0xD1714   011c       MOVS    R1, R0
```

**Around `0xD1BA4` (cited by the Unity code)**

```
0xD1B96   00d0       BEQ     loc_D1B9A
0xD1B98   d2e1       B       loc_D1F40
0xD1B9A   0898       LDR     R0, [SP,#0x150+var_130]
0xD1B9C   9c49       LDR     R1, =0
0xD1B9E   18f1f8e8   BLX     ___gesf2vfp
0xD1BA2   0028       CMP     R0, #0
0xD1BA4   00d0       BEQ     loc_D1BA8   <==
0xD1BA6   cbe1       B       loc_D1F40
0xD1BA8   38e0       B       loc_D1C1C
0xD1BAA   0e99       LDR     R1, [SP,#0x150+var_118]; bool
0xD1BAC   aa235b00   MOVS    R3, #0x154
0xD1BB0   0622       MOVS    R2, #6
0xD1BB2   ca50       STR     R2, [R1,R3]
```

**Around `0xD1C36` (cited by the Unity code)**

```
0xD1C2A   804b       LDR     R3, =(__ZNK9Triniti2D4Rect8PtInRectERKNS_7Vector2E_ptr - 0xD1C34)
0xD1C2C   4ea8       ADD     R0, SP, #0x150+var_18
0xD1C2E   52a9       ADD     R1, SP, #0x150+var_8
0xD1C30   7b44       ADD     R3, PC; __ZNK9Triniti2D4Rect8PtInRectERKNS_7Vector2E_ptr
0xD1C32   1b68       LDR     R3, [R3]
0xD1C34   0693       STR     R3, [SP,#0x150+var_138]; int
0xD1C36   9847       BLX     R3   <==
0xD1C38   0590       STR     R0, [SP,#0x150+var_13C]; int
0xD1C3A   0028       CMP     R0, #0
0xD1C3C   16d0       BEQ     loc_D1C6C
0xD1C3E   7c4a       LDR     R2, =(__ZL11rightCtrlID_0 - 0xD1C46); rightCtrlID ...
0xD1C40   0d9c       LDR     R4, [SP,#0x150+var_11C]
0xD1C42   7a44       ADD     R2, PC; int
```

**Around `0xD1CC0` (cited by the Unity code)**

```
0xD1CB4   4de1       B       sub_D1F52
0xD1CB6   664a       LDR     R2, =(__ZL10leftCtrlID_0 - 0xD1CBE); leftCtrlID ...
0xD1CB8   0d9c       LDR     R4, [SP,#0x150+var_11C]
0xD1CBA   7a44       ADD     R2, PC; leftCtrlID
0xD1CBC   1368       LDR     R3, [R2]; leftCtrlID
0xD1CBE   9c42       CMP     R4, R3
0xD1CC0   04d1       BNE     loc_D1CCC   <==
0xD1CC2   644b       LDR     R3, =(__ZN7DinoCap12g_b_LeftDownE_ptr - 0xD1CCA)
0xD1CC4   0399       LDR     R1, [SP,#0x150+var_144]
0xD1CC6   7b44       ADD     R3, PC; __ZN7DinoCap12g_b_LeftDownE_ptr
0xD1CC8   1160       STR     R1, [R2]; leftCtrlID
0xD1CCA   28e0       B       loc_D1D1E
0xD1CCC   624a       LDR     R2, =(__ZL11rightCtrlID_0 - 0xD1CD4); rightCtrlID ...
```

