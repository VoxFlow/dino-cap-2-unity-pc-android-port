# itemsClass::CloneItems

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-1155DC` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x1155DC` |
| RVA (EA - 0x1000) | `0x1145DC` |
| File offset | `0x1145DC` |
| Size | 542 code bytes in 1 chunk(s), 240 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap10itemsClass10CloneItemsEPcffff` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::itemsClass::CloneItems(char *,float,float,float,float)` |
| Bytes SHA-256 | `5a2d87681cd3602e2e7c149f5eb1e05ffe1b274aac4d1af9bf2e905340aa3ddd` |
| Direct code callers | 7 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameItems.cs:47`](../../../../../Assets/Scripts/GameItems.cs#L47) cites `0x1155DC`

## Callers and callees (direct code references)

Callers: [`itemsClass::CreatItem`](../../../functions/dossiers/itemsClass/FUN-11582A.md)

Callees: `Triniti2D::GameContainer::CreateGameObject`, `Triniti2D::GameObject::Container`, `Triniti2D::GameObject::Sprite`, `Triniti2D::GameObject::SetSize`, `Triniti2D::GameObject::SetPosition`, `Triniti2D::GameSprite::SetFrame`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___mulsf3vfp`

## Strings it references

- `0x2069C0` "L07Frame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x2287E5` GCC_except_table0_18
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x1155DC  f0b5       PUSH    {R4-R7,LR}
0x1155DE  5e46       MOV     R6, R11
0x1155E0  5546       MOV     R5, R10
0x1155E2  4446       MOV     R4, R8
0x1155E4  70b4       PUSH    {R4-R6}
0x1155E6  d2f07cec   BLX     ___save_vfp_d8_d15_regs
0x1155EA  16af       ADD     R7, SP, #arg_38
0x1155EC  a1b0       SUB     SP, SP, #0x84
0x1155EE  0893       STR     R3, [SP,#0x84+var_64]
0x1155F0  824b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x1155FA)
0x1155F2  0992       STR     R2, [SP,#0x84+var_60]
0x1155F4  0122       MOVS    R2, #1
0x1155F6  7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x1155F8  0b90       STR     R0, [SP,#0x84+var_58]
0x1155FA  1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x1155FC  0a91       STR     R1, [SP,#0x84+var_5C]
0x1155FE  10a8       ADD     R0, SP, #0x84+fctx; lpfctx
0x115600  1693       STR     R3, [SP,#0x84+fctx.personality]
0x115602  7f4b       LDR     R3, =(GCC_except_table0_18 - 0x11560C)
0x115604  6946       MOV     R1, SP
0x115606  1897       STR     R7, [SP,#0x84+fctx.jbuf]
0x115608  7b44       ADD     R3, PC; GCC_except_table0_18
0x11560A  1793       STR     R3, [SP,#0x84+fctx.lsda]
0x11560C  7d4b       LDR     R3, =(loc_11574A - 0x115614)
```

