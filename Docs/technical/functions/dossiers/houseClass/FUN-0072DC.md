# houseClass::OnLoad

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0072DC` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x72DC` |
| RVA (EA - 0x1000) | `0x62DC` |
| File offset | `0x62DC` |
| Size | 230 code bytes in 1 chunk(s), 98 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap10houseClass6OnLoadEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::houseClass::OnLoad(void)` |
| Bytes SHA-256 | `c36b44fe6c4b34aef69ff35b618c9514ca684f8b71cfb8f67fd4c5331e550696` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameHouse.cs:7`](../../../../../Assets/Scripts/GameHouse.cs#L7) cites `0x72DC`
- [`Assets/Scripts/GameHouse.cs:42`](../../../../../Assets/Scripts/GameHouse.cs#L42) (by name)
- [`Assets/Scripts/GameHouse.cs:52`](../../../../../Assets/Scripts/GameHouse.cs#L52) cites `0x72DC`
- [`Assets/Scripts/GameHouse.cs:56`](../../../../../Assets/Scripts/GameHouse.cs#L56) (by name)

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: [`CAObjCallBack::playAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00A064.md), [`CAObjCallBack::setAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00AEA0.md), `Triniti2D::GameObject::GetCallBack`, `Triniti2D::GameObject::SetSize`, `std::string::assign`, `___dynamic_cast`, `___fixsfsivfp`, `___floatsisfvfp`, `___ltsf2vfp`, `___mulsf3vfp`, `_rand`

## Strings it references

- `0x200D7C` "_a"
- `0x200D90` "house1"
- `0x200D98` "house2"
- `0x200DA0` "house3"

## Named data it references

- `0x22A0A8` __ZTIN9Triniti2D18GameObjectCallBackE_ptr
- `0x22A0D0` __ZTIN7DinoCap13CAObjCallBackE_ptr
- `0x22C6BC` `typeinfo for'DinoCap::CAObjCallBack
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22A0B8` __ZN7DinoCap15g_ca_house1DataE_ptr
- `0x2403C0` DinoCap::g_ca_house1Data
- `0x22A0D8` __ZN7DinoCap15g_ca_house2DataE_ptr
- `0x2403DC` DinoCap::g_ca_house2Data
- `0x22A0A4` __ZN7DinoCap15g_ca_house3DataE_ptr
- `0x2403F8` DinoCap::g_ca_house3Data

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x72DC    f0b5       PUSH    {R4-R7,LR}
0x72DE    03af       ADD     R7, SP, #0xC
0x72E0    81b0       SUB     SP, SP, #4
0x72E2    3849       LDR     R1, =(aA - 0x72F0); "_a"
0x72E4    8823       MOVS    R3, #0x88
0x72E6    0222       MOVS    R2, #2; unsigned int
0x72E8    c250       STR     R2, [R0,R3]
0x72EA    051c       MOVS    R5, R0
0x72EC    7944       ADD     R1, PC; "_a"
0x72EE    6c30       ADDS    R0, #0x6C ; 'l'; this
0x72F0    e2f128ec   BLX     __ZNSs6assignEPKcm; std::string::assign(char const*,ulong)
0x72F4    8423       MOVS    R3, #0x84
0x72F6    0122       MOVS    R2, #1
0x72F8    6868       LDR     R0, [R5,#4]; this
0x72FA    ea54       STRB    R2, [R5,R3]
0x72FC    64f12afe   BL      __ZN9Triniti2D10GameObject11GetCallBackEv; Triniti2D::GameObject::GetCallBack(void)
0x7300    0028       CMP     R0, #0
0x7302    01d1       BNE     loc_7308
0x7304    0026       MOVS    R6, #0
0x7306    09e0       B       loc_731C
0x7308    2f49       LDR     R1, =(__ZTIN9Triniti2D18GameObjectCallBackE_ptr - 0x7312)
0x730A    304a       LDR     R2, =(__ZTIN7DinoCap13CAObjCallBackE_ptr - 0x7314)
0x730C    0023       MOVS    R3, #0; s2d
0x730E    7944       ADD     R1, PC; __ZTIN9Triniti2D18GameObjectCallBackE_ptr
```

