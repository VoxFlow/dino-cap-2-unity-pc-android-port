# lifeModeScene::AddShanDian

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0E20C0` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xE20C0` |
| RVA (EA - 0x1000) | `0xE10C0` |
| File offset | `0xE10C0` |
| Size | 538 code bytes in 1 chunk(s), 239 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap13lifeModeScene11AddShanDianEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::lifeModeScene::AddShanDian(void)` |
| Bytes SHA-256 | `0dc90a0044f47038b26a4291a5931e9298c4f429dd1977a5406454681329185c` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameWeather.cs:153`](../../../../../Assets/Scripts/GameWeather.cs#L153) cites `0xE20C0`

## Callers and callees (direct code references)

Callers: [`lifeModeScene::OnTimeout`](../../../functions/dossiers/lifeModeScene/FUN-0E2A4C.md)

Callees: [`RectScale`](../../../functions/dossiers/DinoCap/FUN-017068.md), `Triniti2D::Scene::GetUIManager`, `Triniti2D::Scene::SetTimer`, `Triniti2D::UIImage::SetColor`, `Triniti2D::UIImage::SetTexture`, `Triniti2D::UIManager::CreateImage`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___floatsisfvfp`, `___mulsf3vfp`, `___subsf3vfp`, `_rand`

## Strings it references

- `0x206D7C` "sd_baiFrame"
- `0x207464` "ShanDian"
- `0x207458` "maskWhite"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x227A7F` GCC_except_table30_3
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A5F8` __ZN7DinoCap10g_h_offsetE_ptr
- `0x23B918` DinoCap::g_h_offset
- `0x22A634` __ZN7DinoCap10g_w_offsetE_ptr
- `0x23B914` DinoCap::g_w_offset

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xE20C0   f0b5       PUSH    {R4-R7,LR}
0xE20C2   5e46       MOV     R6, R11
0xE20C4   5546       MOV     R5, R10
0xE20C6   4446       MOV     R4, R8
0xE20C8   70b4       PUSH    {R4-R6}
0xE20CA   05f10aef   BLX     ___save_vfp_d8_d15_regs
0xE20CE   16af       ADD     R7, SP, #arg_38
0xE20D0   aab0       SUB     SP, SP, #0xA8
0xE20D2   824b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xE20DC)
0xE20D4   0c90       STR     R0, [SP,#0xA8+var_78]
0xE20D6   0122       MOVS    R2, #1
0xE20D8   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xE20DA   6846       MOV     R0, SP
0xE20DC   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xE20DE   1b90       STR     R0, [SP,#0xA8+var_3C]
0xE20E0   11a8       ADD     R0, SP, #0xA8+fctx; lpfctx
0xE20E2   1793       STR     R3, [SP,#0xA8+fctx.personality]
0xE20E4   7e4b       LDR     R3, =(GCC_except_table30_3 - 0xE20EC)
0xE20E6   1997       STR     R7, [SP,#0xA8+fctx.jbuf]
0xE20E8   7b44       ADD     R3, PC; GCC_except_table30_3
0xE20EA   1893       STR     R3, [SP,#0xA8+fctx.lsda]
0xE20EC   7d4b       LDR     R3, =(loc_E21F6 - 0xE20F2)
0xE20EE   7b44       ADD     R3, PC; loc_E21F6
0xE20F0   1343       ORRS    R3, R2
```

