# lifeModeScene::MissCompEnter

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0E3C54` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xE3C54` |
| RVA (EA - 0x1000) | `0xE2C54` |
| File offset | `0xE2C54` |
| Size | 496 code bytes in 1 chunk(s), 225 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap13lifeModeScene13MissCompEnterEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::lifeModeScene::MissCompEnter(void)` |
| Bytes SHA-256 | `cea2cb67f9318aa7e3a4a06df221ac9d7db56961573e2d7e5b714e0cecdd9b27` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameMission.cs:10`](../../../../../Assets/Scripts/GameMission.cs#L10) cites `0xE3C54`
- [`Assets/Scripts/GameMission.cs:349`](../../../../../Assets/Scripts/GameMission.cs#L349) cites `0xE3C54`
- [`Assets/Scripts/GameMission.cs:355`](../../../../../Assets/Scripts/GameMission.cs#L355) cites `0xE3C54`

## Callers and callees (direct code references)

Callers: [`lifeModeScene::OnUIEvent`](../../../functions/dossiers/lifeModeScene/FUN-0E6550.md)

Callees: [`RectScale`](../../../functions/dossiers/DinoCap/FUN-017068.md), [`RectOffset`](../../../functions/dossiers/DinoCap/FUN-0170B6.md), [`UIMoveCtrl::StartMove`](../../../functions/dossiers/UIMoveCtrl/FUN-0CD466.md), `Triniti2D::Scene::GetUIManager`, `Triniti2D::UIImage::SetTexture`, `Triniti2D::UIManager::DeleteControl`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___mulsf3vfp`

## Strings it references

- `0x207A78` "mission_completeFrame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x227AB7` GCC_except_table32_2
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A6C8` __ZN9Triniti2D9UIManager13CreateControlIN7DinoCap10UIMoveCtrlEEEPT_ii_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xE3C54   f0b5       PUSH    {R4-R7,LR}
0xE3C56   5e46       MOV     R6, R11
0xE3C58   5546       MOV     R5, R10
0xE3C5A   4446       MOV     R4, R8
0xE3C5C   70b4       PUSH    {R4-R6}
0xE3C5E   04f140e9   BLX     ___save_vfp_d8_d15_regs
0xE3C62   16af       ADD     R7, SP, #arg_38
0xE3C64   b6b0       SUB     SP, SP, #0xD8
0xE3C66   774b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xE3C70)
0xE3C68   0690       STR     R0, [SP,#0xD8+var_C0]
0xE3C6A   0122       MOVS    R2, #1
0xE3C6C   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xE3C6E   6846       MOV     R0, SP
0xE3C70   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xE3C72   1690       STR     R0, [SP,#0xD8+var_80]
0xE3C74   0ca8       ADD     R0, SP, #0xD8+fctx; lpfctx
0xE3C76   1293       STR     R3, [SP,#0xD8+fctx.personality]
0xE3C78   734b       LDR     R3, =(GCC_except_table32_2 - 0xE3C80)
0xE3C7A   1497       STR     R7, [SP,#0xD8+fctx.jbuf]
0xE3C7C   7b44       ADD     R3, PC; GCC_except_table32_2
0xE3C7E   1393       STR     R3, [SP,#0xD8+fctx.lsda]
0xE3C80   724b       LDR     R3, =(loc_E3D6C - 0xE3C86)
0xE3C82   7b44       ADD     R3, PC; loc_E3D6C
0xE3C84   1343       ORRS    R3, R2
```

