# menuScene::OnTimeout

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0CD1E2` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xCD1E2` |
| RVA (EA - 0x1000) | `0xCC1E2` |
| File offset | `0xCC1E2` |
| Size | 322 code bytes in 1 chunk(s), 139 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9menuScene9OnTimeoutEiPv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::menuScene::OnTimeout(int,void *)` |
| Bytes SHA-256 | `d75641efc5dfb2615edc45ddc27585bf86320452b0dded1d2e503b17cb600a42` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.title.menuScene.flow` (PROVEN, NATIVE): Title scene = DinoCap::menuScene. startUI@0xca2a4: reads save (ReadFile), removes LogoImage texture, copies g_sound/g_fx into g_ongame_sound/g_ongame_fx, g_state=1, loads uimenu atlas (iPad/retina: Image/ip/uimenu_h.png, else Image/uimenu.png) with CODE-defined frames ui_menu_bgFrame (0,0,960,640; 1x 480x320), ui_menu_dinoFrame (0,640,846,322; 1x 0,320,423,161), ui_menu_startFrame (0,962,484,56; 1x 0,481,242,28), loa...

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: [`menuScene::startUI`](../../../functions/dossiers/menuScene/FUN-0CA2A4.md), [`menuScene::NewGameLeave`](../../../functions/dossiers/menuScene/FUN-0CC410.md), [`menuScene::LogoLeave`](../../../functions/dossiers/menuScene/FUN-0CC580.md), [`menuScene::OnBgLeave`](../../../functions/dossiers/menuScene/FUN-0CC9E4.md), [`menuScene::LoadingEnter`](../../../functions/dossiers/menuScene/FUN-0CCC50.md), [`menuScene::NewGameEnter`](../../../functions/dossiers/menuScene/FUN-0CCECC.md), [`menuScene::LogoEnter`](../../../functions/dossiers/menuScene/FUN-0CD05A.md), `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::compare`, `std::string::string`, `std::string::~string`

## Strings it references

- `0x209190` "NewGameEnter"
- `0x209184` "LogoEnter"
- `0x209158` "NewGameLeave"
- `0x209168` "OnBgLeave"
- `0x20914C` "LogoLeave"
- `0x2091FC` "LoadingEnter"
- `0x209358` "startUI"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x2274B8` GCC_except_table24_4

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xCD1E2   f0b5       PUSH    {R4-R7,LR}
0xCD1E4   5e46       MOV     R6, R11
0xCD1E6   5546       MOV     R5, R10
0xCD1E8   4446       MOV     R4, R8
0xCD1EA   70b4       PUSH    {R4-R6}
0xCD1EC   1af178ee   BLX     ___save_vfp_d8_d15_regs
0xCD1F0   16af       ADD     R7, SP, #arg_38
0xCD1F2   92b0       SUB     SP, SP, #0x48 ; 'H'
0xCD1F4   4b4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xCD1FE)
0xCD1F6   0192       STR     R2, [SP,#0x48+var_44]
0xCD1F8   6a46       MOV     R2, SP
0xCD1FA   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xCD1FC   0d92       STR     R2, [SP,#0x48+var_14]
0xCD1FE   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xCD200   0122       MOVS    R2, #1
0xCD202   0290       STR     R0, [SP,#0x48+var_40]
0xCD204   0993       STR     R3, [SP,#0x48+fctx.personality]
0xCD206   484b       LDR     R3, =(GCC_except_table24_4 - 0xCD210)
0xCD208   03a8       ADD     R0, SP, #0x48+fctx; lpfctx
0xCD20A   0b97       STR     R7, [SP,#0x48+fctx.jbuf]
0xCD20C   7b44       ADD     R3, PC; GCC_except_table24_4
0xCD20E   0a93       STR     R3, [SP,#0x48+fctx.lsda]
0xCD210   464b       LDR     R3, =(loc_CD2E2 - 0xCD216)
0xCD212   7b44       ADD     R3, PC; loc_CD2E2
```

