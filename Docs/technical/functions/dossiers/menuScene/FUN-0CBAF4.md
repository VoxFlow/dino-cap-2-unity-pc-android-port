# menuScene::OnUpdate

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0CBAF4` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xCBAF4` |
| RVA (EA - 0x1000) | `0xCAAF4` |
| File offset | `0xCAAF4` |
| Size | 596 code bytes in 1 chunk(s), 263 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9menuScene8OnUpdateEf` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::menuScene::OnUpdate(float)` |
| Bytes SHA-256 | `657de4949103ddd182bbc3844a55508526bbcb6123cbc5bee11c188aeced9fbf` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.title.menuScene.flow` (PROVEN, NATIVE): Title scene = DinoCap::menuScene. startUI@0xca2a4: reads save (ReadFile), removes LogoImage texture, copies g_sound/g_fx into g_ongame_sound/g_ongame_fx, g_state=1, loads uimenu atlas (iPad/retina: Image/ip/uimenu_h.png, else Image/uimenu.png) with CODE-defined frames ui_menu_bgFrame (0,0,960,640; 1x 480x320), ui_menu_dinoFrame (0,640,846,322; 1x 0,320,423,161), ui_menu_startFrame (0,962,484,56; 1x 0,481,242,28), loa...
- Rule `dc2.title.tap_to_start_pulse` (PROVEN, NATIVE (ARM-verified)): menuScene::OnUpdate(dt) @0xcbaf4: if start-image control (this+0x10C) exists, alpha float (this+0x11C) changes by dt*192.0*1.5 = 288 units/s. Direction flag (this+0x120): true=fading down (alpha += dt*192*-1.5; when alpha < 80.0 flag := false); false=fading up (alpha += dt*192*1.5; when alpha > 250.0 flag := true). Alpha is NOT clamped before the flag test, so it can overshoot the bound by up to one frame's step. App...
- Rule `dc2.title.tap_flow` (PROVEN, NATIVE): Tap handling: control id1 (Tap-to-start UIMoveCtrl) pop-in completion (event 1) while scene state(+12)==0 sets ready flag (+280). OnTouchEvent@0xcc8b2: if state not 1/4 and flag set: clear flag, ButtonEffect('ui_menu_startFrame', control id1), life_Mode=0, state=1. ButtonEffect@0xcc6f0 reuses/creates UIMoveCtrl id 23 (layer 3) with the same texture at the same rect and StartMove(d1=0.05,d2=0.2): a cloned overlay that...
- Rule `dc2.title.exit_sequence` (SUPERSEDED, NATIVE (decompiler)): After the tap overlay (id23) completes (event1, state==1): InitAnim (random hero) + OnResume. OnResume@0xc9798 sets one-shot timers: LogoLeave 0.1 s, NewGameLeave 0.2 s, OnBgLeave 0.3 s. LogoLeave@0xcc580 re-tweens the dino logo (id0): start=current rect, mid x +20*s (1101004800=20), end x -480*s (Ã¢â‚¬Â¦), StartMove(0.05,0.2): nudges right then slides off left. NewGameLeave@0xcc410: same for the Tap-to-start image (...

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/MenuScene.cs:267`](../../../../../Assets/Scripts/MenuScene.cs#L267) cites `0xCBC5C`

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: `LoadResource`, `Triniti2D::Console::WriteLine`, `Triniti2D::UIImage::SetColor`, `Triniti2D::GameCenter::LoginStatus`, `Triniti2D::WebGameClient::RecvResponse`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___fixunssfsivfp`, `___gtsf2vfp`, `___ltsf2vfp`, `___mulsf3vfp`

## Strings it references

- `0x2074FC` "success \| requestId %d \| result %d \| name %s \| action %s \| data %s"
- `0x207540` "-- fail \| requestId %d \| result %d \| name %s \| action %s \| data %s"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x227446` GCC_except_table16_4
- `0x22A680` __ZN7DinoCap9requestIdE_ptr
- `0x23B91C` DinoCap::requestId
- `0x22A320` __ZN9Triniti2D6ModuleINS_13WebGameClientEE10m_instanceE_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xCBAF4   f0b5       PUSH    {R4-R7,LR}
0xCBAF6   5e46       MOV     R6, R11
0xCBAF8   5546       MOV     R5, R10
0xCBAFA   4446       MOV     R4, R8
0xCBAFC   70b4       PUSH    {R4-R6}
0xCBAFE   1cf1f0e9   BLX     ___save_vfp_d8_d15_regs
0xCBB02   16af       ADD     R7, SP, #arg_38
0xCBB04   a1b0       SUB     SP, SP, #0x84
0xCBB06   904b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xCBB10)
0xCBB08   0122       MOVS    R2, #1
0xCBB0A   0990       STR     R0, [SP,#0x84+var_60]
0xCBB0C   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xCBB0E   0891       STR     R1, [SP,#0x84+var_64]
0xCBB10   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xCBB12   6946       MOV     R1, SP
0xCBB14   0da8       ADD     R0, SP, #0x84+fctx; lpfctx
0xCBB16   1393       STR     R3, [SP,#0x84+fctx.personality]
0xCBB18   8c4b       LDR     R3, =(GCC_except_table16_4 - 0xCBB22)
0xCBB1A   1597       STR     R7, [SP,#0x84+fctx.jbuf]
0xCBB1C   1791       STR     R1, [SP,#0x84+var_28]
0xCBB1E   7b44       ADD     R3, PC; GCC_except_table16_4
0xCBB20   1493       STR     R3, [SP,#0x84+fctx.lsda]
0xCBB22   8b4b       LDR     R3, =(loc_CBBF0 - 0xCBB28)
0xCBB24   7b44       ADD     R3, PC; loc_CBBF0
```

**Around `0xCBC5C` (cited by the Unity code)**

```
0xCBC4C   1df108ef   BLX     __Unwind_SjLj_Resume
0xCBC50   0123       MOVS    R3, #1
0xCBC52   5b42       NEGS    R3, R3
0xCBC54   1ea8       ADD     R0, SP, #0x84+var_C; this
0xCBC56   0e93       STR     R3, [SP,#0x84+fctx.call_site]
0xCBC58   1df1aaef   BLX     __ZNSsD2Ev; std::string::~string()
0xCBC5C   0999       LDR     R1, [SP,#0x84+var_60]   <==
0xCBC5E   86235b00   MOVS    R3, #0x10C
0xCBC62   cb58       LDR     R3, [R1,R3]
0xCBC64   002b       CMP     R3, #0
0xCBC66   52d0       BEQ     loc_CBD0E
0xCBC68   90225200   MOVS    R2, #0x120
0xCBC6C   0592       STR     R2, [SP,#0x84+var_70]
```

