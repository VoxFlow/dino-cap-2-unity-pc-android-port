# bossN::OnLoad

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-1210EC` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x1210EC` |
| RVA (EA - 0x1000) | `0x1200EC` |
| File offset | `0x1200EC` |
| Size | 338 code bytes in 1 chunk(s), 149 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap5bossN6OnLoadEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::bossN::OnLoad(void)` |
| Bytes SHA-256 | `eb965cb8c136f15830831ce52ac3d21c6451822e342474f3557083b209517a61` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameBoss.cs:260`](../../../../../Assets/Scripts/GameBoss.cs#L260) cites `0x1211D2`
- [`Assets/Scripts/GameFx.cs:88`](../../../../../Assets/Scripts/GameFx.cs#L88) cites `0x1211D2`

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: `Triniti2D::Camera::GetViewport`, `Triniti2D::GameObject::Enable`, `Triniti2D::GameObject::GetScene`, `Triniti2D::GameObject::GetCallBack`, `Triniti2D::Scene::GetCamera`, `Triniti2D::Scene::CreateGameObject`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___dynamic_cast`

## Strings it references

- `0x207310` "effectClassN"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x228C94` GCC_except_table16_11
- `0x22A0A8` __ZTIN9Triniti2D18GameObjectCallBackE_ptr
- `0x22D290` `typeinfo for'DinoCap::bossN
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22A11C` __ZNSt6vectorIPN9Triniti2D10GameObjectESaIS2_EE9push_backERKS2__ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x1210EC  f0b5       PUSH    {R4-R7,LR}
0x1210EE  5e46       MOV     R6, R11
0x1210F0  5546       MOV     R5, R10
0x1210F2  4446       MOV     R4, R8
0x1210F4  70b4       PUSH    {R4-R6}
0x1210F6  c6f0f4ee   BLX     ___save_vfp_d8_d15_regs
0x1210FA  16af       ADD     R7, SP, #arg_38
0x1210FC  9fb0       SUB     SP, SP, #0x7C ; '|'
0x1210FE  504b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x121108)
0x121100  0890       STR     R0, [SP,#0x7C+var_5C]
0x121102  0122       MOVS    R2, #1
0x121104  7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x121106  6846       MOV     R0, SP
0x121108  1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x12110A  1590       STR     R0, [SP,#0x7C+var_28]
0x12110C  0ba8       ADD     R0, SP, #0x7C+fctx; lpfctx
0x12110E  1193       STR     R3, [SP,#0x7C+fctx.personality]
0x121110  4c4b       LDR     R3, =(GCC_except_table16_11 - 0x121118)
0x121112  1397       STR     R7, [SP,#0x7C+fctx.jbuf]
0x121114  7b44       ADD     R3, PC; GCC_except_table16_11
0x121116  1293       STR     R3, [SP,#0x7C+fctx.lsda]
0x121118  4b4b       LDR     R3, =(loc_1211DE - 0x12111E)
0x12111A  7b44       ADD     R3, PC; loc_1211DE
0x12111C  1343       ORRS    R3, R2
```

**Around `0x1211D2` (cited by the Unity code)**

```
0x1211C4  0998       LDR     R0, [SP,#0x7C+var_58]; this
0x1211C6  1ca9       ADD     R1, SP, #0x7C+var_C; std::string *
0x1211C8  0022       MOVS    R2, #0; int
0x1211CA  0123       MOVS    R3, #1; bool
0x1211CC  54f0ccf8   BL      __ZN9Triniti2D5Scene16CreateGameObjectERKSsibbbbbb; Triniti2D::Scene::CreateGameObject(std::string const&,int,bool,bool,bool,bool,bool,bool)
0x1211D0  0221       MOVS    R1, #2
0x1211D2  1d90       STR     R0, [SP,#0x7C+var_8]   <==
0x1211D4  0c91       STR     R1, [SP,#0x7C+fctx.call_site]
0x1211D6  1ca8       ADD     R0, SP, #0x7C+var_C; this
0x1211D8  c8f0eaec   BLX     __ZNSsD2Ev; std::string::~string()
0x1211DC  0fe0       B       loc_1211FE
0x1211DE  0d9a       LDR     R2, [SP,#0x7C+fctx.data]
0x1211E0  0c9b       LDR     R3, [SP,#0x7C+fctx.call_site]
```

