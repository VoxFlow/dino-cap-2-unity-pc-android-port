# playerNewAniClass::getDownEgg

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-1270FA` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x1270FA` |
| RVA (EA - 0x1000) | `0x1260FA` |
| File offset | `0x1260FA` |
| Size | 508 code bytes in 1 chunk(s), 236 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap17playerNewAniClass10getDownEggEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerNewAniClass::getDownEgg(void)` |
| Bytes SHA-256 | `9fa691ccfaa10235eed7e92c44090bd79f534665d3b7ae12b98f89d29c1d0111` |
| Direct code callers | 5 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameGirl.cs:385`](../../../../../Assets/Scripts/GameGirl.cs#L385) cites `0x1270FA`

## Callers and callees (direct code references)

Callers: [`playerNewAniClass::UpdateCamera`](../../../functions/dossiers/playerNewAniClass/FUN-127342.md), [`playerNewAniClass::BeAttack`](../../../functions/dossiers/playerNewAniClass/FUN-1282B8.md), [`playerNewAniClass::BeAttackByBoss`](../../../functions/dossiers/playerNewAniClass/FUN-1286E4.md)

Callees: [`mainScene::CanRollDown`](../../../functions/dossiers/mainScene/FUN-094C2A.md), [`CompAnimObj::SetAvatar`](../../../functions/dossiers/CompAnimObj/FUN-11888E.md), `Triniti2D::GameObject::GetScene`, `Triniti2D::Scene::GetCallBack`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::compare`, `std::string::string`, `std::string::~string`, `___dynamic_cast`

## Strings it references

- `0x20B250` "shenti"
- `0x20B258` "newplayer_shenti2"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x228E16` GCC_except_table29_7
- `0x22A0C0` __ZTIN9Triniti2D13SceneCallBackE_ptr
- `0x22A0C8` __ZTIN7DinoCap9mainSceneE_ptr
- `0x22C868` `typeinfo for'DinoCap::mainScene
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22AFAC` __ZNKSt6vectorISt4pairISsSsESaIS1_EE4sizeEv_ptr
- `0x22AF2C` __ZSt8_DestroyIPSt4pairISsSsEEvT_S3__ptr
- `0x22AF68` __ZNSt4pairISsSsEC2ERKS0__ptr
- `0x22B154` __ZNSt6vectorISt4pairISsSsESaIS1_EE9push_backERKS1__ptr
- `0x22AFA8` __ZNSt4pairISsSsED2Ev_ptr
- `0x22AF80` __ZNSt6vectorISt4pairISsSsESaIS1_EEaSERKS3__ptr
- `0x22AEB0` __ZNSt6vectorISt4pairISsSsESaIS1_EED2Ev_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x1270FA  f0b5       PUSH    {R4-R7,LR}
0x1270FC  5e46       MOV     R6, R11
0x1270FE  5546       MOV     R5, R10
0x127100  4446       MOV     R4, R8
0x127102  70b4       PUSH    {R4-R6}
0x127104  c0f0ecee   BLX     ___save_vfp_d8_d15_regs
0x127108  16af       ADD     R7, SP, #arg_38
0x12710A  9cb0       SUB     SP, SP, #0x70 ; 'p'
0x12710C  7a4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x127116)
0x12710E  6a46       MOV     R2, SP
0x127110  1192       STR     R2, [SP,#0x70+var_2C]
0x127112  7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x127114  0122       MOVS    R2, #1
0x127116  1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x127118  0490       STR     R0, [SP,#0x70+var_60]
0x12711A  07a8       ADD     R0, SP, #0x70+fctx; lpfctx
0x12711C  0d93       STR     R3, [SP,#0x70+fctx.personality]
0x12711E  774b       LDR     R3, =(GCC_except_table29_7 - 0x127126)
0x127120  0f97       STR     R7, [SP,#0x70+fctx.jbuf]
0x127122  7b44       ADD     R3, PC; GCC_except_table29_7
0x127124  0e93       STR     R3, [SP,#0x70+fctx.lsda]
0x127126  764b       LDR     R3, =(loc_1271BA - 0x12712C)
0x127128  7b44       ADD     R3, PC; loc_1271BA
0x12712A  1343       ORRS    R3, R2
```

