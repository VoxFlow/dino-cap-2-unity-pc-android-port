# playerClass::Stand

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0B88B4` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xB88B4` |
| RVA (EA - 0x1000) | `0xB78B4` |
| File offset | `0xB78B4` |
| Size | 66 code bytes in 1 chunk(s), 28 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11playerClass5StandEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerClass::Stand(void)` |
| Bytes SHA-256 | `b0022e9eb5d4a8c7a1fb1725fc1ce9a06ff53bed78a0c817d28ecf7b4d81b13e` |
| Direct code callers | 10 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameEgg.cs:153`](../../../../../Assets/Scripts/GameEgg.cs#L153) (by name)
- [`Assets/Scripts/GameIntro.cs:27`](../../../../../Assets/Scripts/GameIntro.cs#L27) (by name)
- [`Assets/Scripts/GameMission.cs:155`](../../../../../Assets/Scripts/GameMission.cs#L155) cites `0xB88B4`

## Callers and callees (direct code references)

Callers: [`mainScene::OnLoad`](../../../functions/dossiers/mainScene/FUN-0A7434.md), [`mainScene::GameClear`](../../../functions/dossiers/mainScene/FUN-0B7F5E.md), [`playerClass::UpdateControl`](../../../functions/dossiers/playerClass/FUN-0B8A60.md), `playerClass::Realive`, [`playerClass::SwitchWeapon`](../../../functions/dossiers/playerClass/FUN-0BAC3C.md), [`playerClass::UpdateCamera`](../../../functions/dossiers/playerClass/FUN-0BB054.md), [`playerClass::OnAnimationEnd`](../../../functions/dossiers/playerClass/FUN-0BD978.md), [`lifeModeScene::OnLoad`](../../../functions/dossiers/lifeModeScene/FUN-0E7202.md), [`lifeModeScene::GameClear`](../../../functions/dossiers/lifeModeScene/FUN-0F0FF8.md)

Callees: [`CAObjCallBack::playAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00A064.md), [`CAObjCallBack::setAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00AEA0.md), `std::string::compare`

## Strings it references

- `0x208254` "playerStandAnimation"

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xB88B4   b0b5       PUSH    {R4,R5,R7,LR}
0xB88B6   02af       ADD     R7, SP, #8
0xB88B8   81b0       SUB     SP, SP, #4
0xB88BA   0f4c       LDR     R4, =(aPlayerstandani - 0xB88C4); "playerStandAnimation"
0xB88BC   051c       MOVS    R5, R0
0xB88BE   6430       ADDS    R0, #0x64 ; 'd'; this
0xB88C0   7c44       ADD     R4, PC; "playerStandAnimation"
0xB88C2   211c       MOVS    R1, R4; char *
0xB88C4   31f102e9   BLX     __ZNKSs7compareEPKc; std::string::compare(char const*)
0xB88C8   0028       CMP     R0, #0
0xB88CA   12d0       BEQ     loc_B88F2
0xB88CC   a7239b00   MOVS    R3, #0x29C
0xB88D0   281c       MOVS    R0, R5; this
0xB88D2   e958       LDR     R1, [R5,R3]; DinoCap::CombineAnimation *
0xB88D4   221c       MOVS    R2, R4; char *
0xB88D6   52f7e3fa   BL      __ZN7DinoCap13CAObjCallBack12setAnimationEPNS_16CombineAnimationEPKc; DinoCap::CAObjCallBack::setAnimation(DinoCap::CombineAnimation *,char const*)
0xB88DA   0121       MOVS    R1, #1
0xB88DC   4942       NEGS    R1, R1; unsigned int
0xB88DE   0022       MOVS    R2, #0; int
0xB88E0   064b       LDR     R3, flt_B88FC; float
0xB88E2   281c       MOVS    R0, R5; this
0xB88E4   0091       STR     R1, [SP,#4+var_4]; unsigned int
0xB88E6   51f7bdfb   BL      __ZN7DinoCap13CAObjCallBack13playAnimationEmifm; DinoCap::CAObjCallBack::playAnimation(ulong,int,float,ulong)
0xB88EA   a0239b00   MOVS    R3, #0x280
```

