# playerNewAniClass::OnTimeout

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-127DAC` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x127DAC` |
| RVA (EA - 0x1000) | `0x126DAC` |
| File offset | `0x126DAC` |
| Size | 816 code bytes in 1 chunk(s), 369 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap17playerNewAniClass9OnTimeoutEiPv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerNewAniClass::OnTimeout(int,void *)` |
| Bytes SHA-256 | `4998af3e269d433496283907e9c863deccb21700dd7ced3caeddad4e5cd82cd8` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameFire.cs:60`](../../../../../Assets/Scripts/GameFire.cs#L60) cites `0x127DAC`
- [`Assets/Scripts/GameGirl.cs:167`](../../../../../Assets/Scripts/GameGirl.cs#L167) cites `0x127F78`
- [`Assets/Scripts/GameGirl.cs:169`](../../../../../Assets/Scripts/GameGirl.cs#L169) cites `0x128024`
- [`Assets/Scripts/GameGirl.cs:424`](../../../../../Assets/Scripts/GameGirl.cs#L424) cites `0x127DAC`

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: [`PlaySound`](../../../functions/dossiers/DinoCap/FUN-01A448.md), [`mainScene::CanRollDown`](../../../functions/dossiers/mainScene/FUN-094C2A.md), [`lifeModeScene::CanRollDown`](../../../functions/dossiers/lifeModeScene/FUN-0CFDB0.md), [`CompAnimObj::SetAvatar`](../../../functions/dossiers/CompAnimObj/FUN-11888E.md), [`CompAnimObj::PlayAnimation`](../../../functions/dossiers/CompAnimObj/FUN-119010.md), [`CompAnimObj::SetAnimation`](../../../functions/dossiers/CompAnimObj/FUN-1198AC.md), [`playerNewAniClass::UpdateBG`](../../../functions/dossiers/playerNewAniClass/FUN-125C36.md), `Triniti2D::GameObject::GetScene`, `Triniti2D::GameObject::Physics`, `Triniti2D::GamePhysics::SetLinearVelocity`, `Triniti2D::Scene::GetCallBack`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::compare`, `std::string::string`, `std::string::~string`, `___dynamic_cast`, `_strcmp`

## Strings it references

- `0x2083C0` "Wudi"
- `0x208370` "fireup"
- `0x20B248` "lian"
- `0x201338` "roll"
- `0x2083D0` "playerRoll2Animation"
- `0x2077F8` "p5rollAudio"
- `0x2083E8` "rollEndTime"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x228E46` GCC_except_table31_6
- `0x22AF2C` __ZSt8_DestroyIPSt4pairISsSsEEvT_S3__ptr
- `0x22AFAC` __ZNKSt6vectorISt4pairISsSsESaIS1_EE4sizeEv_ptr
- `0x22AF68` __ZNSt4pairISsSsEC2ERKS0__ptr
- `0x22B154` __ZNSt6vectorISt4pairISsSsESaIS1_EE9push_backERKS1__ptr
- `0x22AFA8` __ZNSt4pairISsSsED2Ev_ptr
- `0x22AF80` __ZNSt6vectorISt4pairISsSsESaIS1_EEaSERKS3__ptr
- `0x22AEB0` __ZNSt6vectorISt4pairISsSsESaIS1_EED2Ev_ptr
- `0x22A2B8` __ZN7DinoCap13g_player_typeE_ptr
- `0x23B86C` DinoCap::g_player_type
- `0x22A290` __ZN7DinoCap9life_ModeE_ptr
- `0x23E09C` DinoCap::life_Mode
- `0x22A0C0` __ZTIN9Triniti2D13SceneCallBackE_ptr
- `0x22A0C8` __ZTIN7DinoCap9mainSceneE_ptr
- `0x22C868` `typeinfo for'DinoCap::mainScene
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22A274` __ZTIN7DinoCap13lifeModeSceneE_ptr
- `0x22CF10` `typeinfo for'DinoCap::lifeModeScene

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x127DAC  f0b5       PUSH    {R4-R7,LR}
0x127DAE  5e46       MOV     R6, R11
0x127DB0  5546       MOV     R5, R10
0x127DB2  4446       MOV     R4, R8
0x127DB4  70b4       PUSH    {R4-R6}
0x127DB6  c0f094e8   BLX     ___save_vfp_d8_d15_regs
0x127DBA  16af       ADD     R7, SP, #arg_38
0x127DBC  a3b0       SUB     SP, SP, #0x8C
0x127DBE  c74b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x127DC8)
0x127DC0  0692       STR     R2, [SP,#0x8C+__s1]
0x127DC2  0122       MOVS    R2, #1
0x127DC4  7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x127DC6  6946       MOV     R1, SP
0x127DC8  1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x127DCA  0790       STR     R0, [SP,#0x8C+var_70]
0x127DCC  0ca8       ADD     R0, SP, #0x8C+fctx; lpfctx
0x127DCE  1293       STR     R3, [SP,#0x8C+fctx.personality]
0x127DD0  c34b       LDR     R3, =(GCC_except_table31_6 - 0x127DDA)
0x127DD2  1691       STR     R1, [SP,#0x8C+var_34]
0x127DD4  1497       STR     R7, [SP,#0x8C+fctx.jbuf]
0x127DD6  7b44       ADD     R3, PC; GCC_except_table31_6
0x127DD8  1393       STR     R3, [SP,#0x8C+fctx.lsda]
0x127DDA  c24b       LDR     R3, =(loc_127E5A - 0x127DE0)
0x127DDC  7b44       ADD     R3, PC; loc_127E5A
```

**Around `0x127F78` (cited by the Unity code)**

```
0x127F6A  ace0       B       loc_1280C6
0x127F6C  6d49       LDR     R1, =(aRoll - 0x127F74); "roll"
0x127F6E  0698       LDR     R0, [SP,#0x8C+__s1]; __s1
0x127F70  7944       ADD     R1, PC; "roll"
0x127F72  c2f032ea   BLX     _strcmp
0x127F76  0028       CMP     R0, #0
0x127F78  5cd1       BNE     loc_128034   <==
0x127F7A  0799       LDR     R1, [SP,#0x8C+var_70]
0x127F7C  bb235b00   MOVS    R3, #0x176
0x127F80  0122       MOVS    R2, #1
0x127F82  ca54       STRB    R2, [R1,R3]
0x127F84  de23       MOVS    R3, #0xDE
0x127F86  4868       LDR     R0, [R1,#4]; this
```

**Around `0x128024` (cited by the Unity code)**

```
0x128018  042b       CMP     R3, #4
0x12801A  05d1       BNE     loc_128028
0x12801C  4748       LDR     R0, =(aP5rollaudio - 0x128026); "p5rollAudio"
0x12801E  0021       MOVS    R1, #0; char *
0x128020  0022       MOVS    R2, #0; bool
0x128022  7844       ADD     R0, PC; "p5rollAudio"
0x128024  f2f610fa   BL      __ZN7DinoCap9PlaySoundEPKcbS1_; DinoCap::PlaySound(char const*,bool,char const*)   <==
0x128028  0799       LDR     R1, [SP,#0x8C+var_70]
0x12802A  b8235b00   MOVS    R3, #0x170
0x12802E  0222       MOVS    R2, #2
0x128030  ca50       STR     R2, [R1,R3]
0x128032  48e0       B       loc_1280C6
0x128034  4249       LDR     R1, =(aRollendtime - 0x12803C); "rollEndTime"
```

