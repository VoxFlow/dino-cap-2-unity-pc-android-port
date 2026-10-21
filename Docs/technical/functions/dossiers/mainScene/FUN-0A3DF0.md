# mainScene::MissCompEnter

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0A3DF0` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xA3DF0` |
| RVA (EA - 0x1000) | `0xA2DF0` |
| File offset | `0xA2DF0` |
| Size | 512 code bytes in 1 chunk(s), 232 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9mainScene13MissCompEnterEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::mainScene::MissCompEnter(void)` |
| Bytes SHA-256 | `4e9707f881ce3316cbc4f642f1b1275805862b9623f7e9662cf5f2ffbdebba40` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameMission.cs:355`](../../../../../Assets/Scripts/GameMission.cs#L355) cites `0xA3EA6`

## Callers and callees (direct code references)

Callers: [`mainScene::OnUIEvent`](../../../functions/dossiers/mainScene/FUN-0A69AE.md)

Callees: [`RectScale`](../../../functions/dossiers/DinoCap/FUN-017068.md), [`RectOffset`](../../../functions/dossiers/DinoCap/FUN-0170B6.md), [`UIMoveCtrl::StartMove`](../../../functions/dossiers/UIMoveCtrl/FUN-0CD466.md), `Triniti2D::Scene::GetUIManager`, `Triniti2D::UIImage::SetTexture`, `Triniti2D::UIManager::DeleteControl`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___mulsf3vfp`

## Strings it references

- `0x207A78` "mission_completeFrame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x2269FA` GCC_except_table36
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A6C8` __ZN9Triniti2D9UIManager13CreateControlIN7DinoCap10UIMoveCtrlEEEPT_ii_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xA3DF0   f0b5       PUSH    {R4-R7,LR}
0xA3DF2   5e46       MOV     R6, R11
0xA3DF4   5546       MOV     R5, R10
0xA3DF6   4446       MOV     R4, R8
0xA3DF8   70b4       PUSH    {R4-R6}
0xA3DFA   44f172e8   BLX     ___save_vfp_d8_d15_regs
0xA3DFE   16af       ADD     R7, SP, #arg_38
0xA3E00   b2b0       SUB     SP, SP, #0xC8
0xA3E02   7b4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xA3E0C)
0xA3E04   0690       STR     R0, [SP,#0xC8+var_B0]
0xA3E06   0122       MOVS    R2, #1
0xA3E08   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xA3E0A   6846       MOV     R0, SP
0xA3E0C   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xA3E0E   1690       STR     R0, [SP,#0xC8+var_70]
0xA3E10   0ca8       ADD     R0, SP, #0xC8+fctx; lpfctx
0xA3E12   1293       STR     R3, [SP,#0xC8+fctx.personality]
0xA3E14   774b       LDR     R3, =(GCC_except_table36 - 0xA3E1C)
0xA3E16   1497       STR     R7, [SP,#0xC8+fctx.jbuf]
0xA3E18   7b44       ADD     R3, PC; GCC_except_table36
0xA3E1A   1393       STR     R3, [SP,#0xC8+fctx.lsda]
0xA3E1C   764b       LDR     R3, =(loc_A3F18 - 0xA3E22)
0xA3E1E   7b44       ADD     R3, PC; loc_A3F18
0xA3E20   1343       ORRS    R3, R2
```

**Around `0xA3EA6` (cited by the Unity code)**

```
0xA3E9A   7b44       ADD     R3, PC; __ZN7DinoCap8g_iphoneE_ptr
0xA3E9C   1b68       LDR     R3, [R3]; DinoCap::g_iphone ...
0xA3E9E   1b78       LDRB    R3, [R3]; DinoCap::g_iphone
0xA3EA0   002b       CMP     R3, #0
0xA3EA2   04d1       BNE     loc_A3EAE
0xA3EA4   2d98       LDR     R0, [SP,#0xC8+var_18.bottom]
0xA3EA6   5d49       LDR     R1, =0x42000000   <==
0xA3EA8   45f1e2ee   BLX     ___addsf3vfp
0xA3EAC   2d90       STR     R0, [SP,#0xC8+var_18.bottom]
0xA3EAE   5c4b       LDR     R3, =0
0xA3EB0   029a       LDR     R2, [SP,#0xC8+var_C0]
0xA3EB2   2793       STR     R3, [SP,#0xC8+var_2C]
0xA3EB4   2893       STR     R3, [SP,#0xC8+var_28]
```

