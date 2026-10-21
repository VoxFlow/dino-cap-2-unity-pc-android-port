# itemsClass::OnTimeout

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-115AFC` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x115AFC` |
| RVA (EA - 0x1000) | `0x114AFC` |
| File offset | `0x114AFC` |
| Size | 298 code bytes in 1 chunk(s), 132 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap10itemsClass9OnTimeoutEiPv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::itemsClass::OnTimeout(int,void *)` |
| Bytes SHA-256 | `370e8cf89ccbb7968526242d06e164c2d0bc5477e1ca5663658bd1f7e0f104d6` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | GAMEPLAY / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: r118 0x115afc: timers 'firsttimer' / 'secondtimer' -> Enable(false) + DeleteGameObject; 'secondtimer' (5 s HP popup) ported as ItemActor.popupTimer; 'firsttimer' has no SetTimer site (string xrefs only in OnTimeout compares)

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameItems.cs:142`](../../../../../Assets/Scripts/GameItems.cs#L142) cites `0x115AFC`

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: `Triniti2D::GameObject::Enable`, `Triniti2D::GameObject::GetScene`, `Triniti2D::Scene::DeleteGameObject`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::compare`, `std::string::string`, `std::string::~string`

## Strings it references

- `0x209CDC` "firsttimer"
- `0x209CE8` "secondtimer"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x2287FB` GCC_except_table2_18

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x115AFC  f0b5       PUSH    {R4-R7,LR}
0x115AFE  5e46       MOV     R6, R11
0x115B00  5546       MOV     R5, R10
0x115B02  4446       MOV     R4, R8
0x115B04  70b4       PUSH    {R4-R6}
0x115B06  d2f0ece9   BLX     ___save_vfp_d8_d15_regs
0x115B0A  16af       ADD     R7, SP, #arg_38
0x115B0C  96b0       SUB     SP, SP, #0x58 ; 'X'
0x115B0E  464b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x115B18)
0x115B10  0192       STR     R2, [SP,#0x58+var_54]
0x115B12  6a46       MOV     R2, SP
0x115B14  7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x115B16  1092       STR     R2, [SP,#0x58+var_18]
0x115B18  1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x115B1A  0122       MOVS    R2, #1
0x115B1C  0290       STR     R0, [SP,#0x58+var_50]
0x115B1E  0c93       STR     R3, [SP,#0x58+fctx.personality]
0x115B20  424b       LDR     R3, =(GCC_except_table2_18 - 0x115B2A)
0x115B22  06a8       ADD     R0, SP, #0x58+fctx; lpfctx
0x115B24  0e97       STR     R7, [SP,#0x58+fctx.jbuf]
0x115B26  7b44       ADD     R3, PC; GCC_except_table2_18
0x115B28  0d93       STR     R3, [SP,#0x58+fctx.lsda]
0x115B2A  414b       LDR     R3, =(loc_115BC2 - 0x115B30)
0x115B2C  7b44       ADD     R3, PC; loc_115BC2
```

