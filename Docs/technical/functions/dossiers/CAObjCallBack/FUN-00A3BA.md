# CAObjCallBack::getSprite

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-00A3BA` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xA3BA` |
| RVA (EA - 0x1000) | `0x93BA` |
| File offset | `0x93BA` |
| Size | 252 code bytes in 1 chunk(s), 114 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap13CAObjCallBack9getSpriteEPKc` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::CAObjCallBack::getSprite(char const*)` |
| Bytes SHA-256 | `6212de5926c54728dabe786601032ee9e35e077ca3e0a915f51105345f8c1205` |
| Direct code callers | 12 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameFire.cs:174`](../../../../../Assets/Scripts/GameFire.cs#L174) (by name)

## Callers and callees (direct code references)

Callers: [`dinosaurClass::Blood`](../../../functions/dossiers/dinosaurClass/FUN-012A46.md), [`effectClass::OnUpdate`](../../../functions/dossiers/effectClass/FUN-0167A6.md), [`playerClass::FeiBiao`](../../../functions/dossiers/playerClass/FUN-0BC868.md), [`playerClass::JuPan`](../../../functions/dossiers/playerClass/FUN-0BCA10.md), [`playerClass::PenHuoQ`](../../../functions/dossiers/playerClass/FUN-0BEB28.md), [`playerClass::JiGuangQ`](../../../functions/dossiers/playerClass/FUN-0BEDA6.md), [`playerClass::YuanLi`](../../../functions/dossiers/playerClass/FUN-0BF0EA.md), [`playerClass::LiuDanQ`](../../../functions/dossiers/playerClass/FUN-0BFE94.md), [`playerClass::KaiQiang`](../../../functions/dossiers/playerClass/FUN-0C0336.md), `selPlayerClass::PenHuoQ`

Callees: `Triniti2D::GameObject::Enable`, `Triniti2D::GameObject::GetCallBack`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::compare`, `std::string::string`, `std::string::~string`, `___dynamic_cast`

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x2239E7` GCC_except_table5_0
- `0x22A0A0` __ZNKSt6vectorIPN9Triniti2D10GameObjectESaIS2_EE4sizeEv_ptr
- `0x22A0A8` __ZTIN9Triniti2D18GameObjectCallBackE_ptr
- `0x22A128` __ZTIN7DinoCap19CAObjSpriteCallBackE_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xA3BA    f0b5       PUSH    {R4-R7,LR}
0xA3BC    5e46       MOV     R6, R11
0xA3BE    5546       MOV     R5, R10
0xA3C0    4446       MOV     R4, R8
0xA3C2    70b4       PUSH    {R4-R6}
0xA3C4    ddf18ced   BLX     ___save_vfp_d8_d15_regs
0xA3C8    16af       ADD     R7, SP, #arg_38
0xA3CA    95b0       SUB     SP, SP, #0x54 ; 'T'
0xA3CC    3a4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xA3D6)
0xA3CE    0122       MOVS    R2, #1
0xA3D0    0390       STR     R0, [SP,#0x54+var_48]
0xA3D2    7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xA3D4    0291       STR     R1, [SP,#0x54+var_4C]
0xA3D6    1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xA3D8    6946       MOV     R1, SP
0xA3DA    06a8       ADD     R0, SP, #0x54+fctx; lpfctx
0xA3DC    0c93       STR     R3, [SP,#0x54+fctx.personality]
0xA3DE    374b       LDR     R3, =(GCC_except_table5_0 - 0xA3E8)
0xA3E0    1091       STR     R1, [SP,#0x54+var_14]
0xA3E2    0e97       STR     R7, [SP,#0x54+fctx.jbuf]
0xA3E4    7b44       ADD     R3, PC; GCC_except_table5_0
0xA3E6    0d93       STR     R3, [SP,#0x54+fctx.lsda]
0xA3E8    354b       LDR     R3, =(loc_A46E - 0xA3EE)
0xA3EA    7b44       ADD     R3, PC; loc_A46E
```

