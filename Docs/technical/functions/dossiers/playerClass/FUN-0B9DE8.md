# playerClass::ChangeWeapon

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0B9DE8` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xB9DE8` |
| RVA (EA - 0x1000) | `0xB8DE8` |
| File offset | `0xB8DE8` |
| Size | 2278 code bytes in 3 chunk(s), 839 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11playerClass12ChangeWeaponEii` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerClass::ChangeWeapon(int,int)` |
| Bytes SHA-256 | `d59958be6dad1c8a3be7df8b8596454d94c6adea632a797261b472423671579c` |
| Direct code callers | 7 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameControls.cs:387`](../../../../../Assets/Scripts/GameControls.cs#L387) (by name)

## Callers and callees (direct code references)

Callers: [`playerClass::SwitchWeapon`](../../../functions/dossiers/playerClass/FUN-0BAC3C.md), [`playerClass::AfterNoBullets`](../../../functions/dossiers/playerClass/FUN-0BAEAC.md), [`playerClass::InitType`](../../../functions/dossiers/playerClass/FUN-0BC106.md), `lifeModeTools::OnUpdate`

Callees: [`CombineAnimation::Clone`](../../../functions/dossiers/CombineAnimation/FUN-00C22A.md), [`CombineAnimation::ChangeImage`](../../../functions/dossiers/CombineAnimation/FUN-00CC8A.md), [`CombineAnimation::SetSpriteAnim`](../../../functions/dossiers/CombineAnimation/FUN-00EDBC.md), `sub_BA20A`, `sub_BABDA`, `___switchu8`, `___switch16`

## Strings it references

- `0x2012F0` "com1"
- `0x20832C` "A4"
- `0x2012E0` "yyj5"
- `0x208330` "B4"
- `0x208334` "C4"
- `0x207B60` "D4"
- `0x208338` "E4"
- `0x20833C` "yuanli20001"
- `0x20231C` "com15"
- `0x208348` "shoulijian01"
- `0x208358` "yuanjian7"
- `0x208364` "G6"
- `0x208368` "G4"
- `0x20836C` "G5"
- `0x208378` "G1"
- `0x20837C` "A1"
- `0x208380` "B1"
- `0x208384` "C1"
- `0x208388` "D1"
- `0x207B68` "E1"
- `0x20838C` "F1"
- `0x208390` "D2"
- `0x208394` "E2"
- `0x208398` "F2"
- `0x20839C` "G2"
- `0x2083A0` "A2"
- `0x2083A4` "B2"
- `0x2083A8` "C2"
- `0x2083AC` "G3"
- `0x2083B0` "A3"

## Named data it references

- `0x22A640` __ZN7DinoCap11g_b_HitDownE_ptr
- `0x23B8C2` DinoCap::g_b_HitDown
- `0x22A78C` __ZN7DinoCap14g_ca_pRollDataE_ptr
- `0x23E9BC` DinoCap::g_ca_pRollData
- `0x22A7C0` __ZN7DinoCap15g_ca_pRoll2DataE_ptr
- `0x23E9D8` DinoCap::g_ca_pRoll2Data
- `0x22A788` __ZN7DinoCap14g_ca_pMoveDataE_ptr
- `0x23E9F4` DinoCap::g_ca_pMoveData
- `0x22A7AC` __ZN7DinoCap15g_ca_pStandDataE_ptr
- `0x23EA10` DinoCap::g_ca_pStandData
- `0x22A7E0` __ZN7DinoCap14g_ca_pFireDataE_ptr
- `0x23EA2C` DinoCap::g_ca_pFireData
- `0x22A798` __ZN7DinoCap15g_ca_pBehitDataE_ptr
- `0x23EA48` DinoCap::g_ca_pBehitData
- `0x22A758` __ZN7DinoCap15g_ca_pHouseDataE_ptr
- `0x23EA64` DinoCap::g_ca_pHouseData
- `0x22A7B0` __ZN7DinoCap15g_ca_p2MoveDataE_ptr
- `0x23EA80` DinoCap::g_ca_p2MoveData
- `0x22A7D4` __ZN7DinoCap16g_ca_p2StandDataE_ptr
- `0x23EA9C` DinoCap::g_ca_p2StandData
- `0x22A76C` __ZN7DinoCap15g_ca_p2FireDataE_ptr
- `0x23EAB8` DinoCap::g_ca_p2FireData
- `0x22A7C4` __ZN7DinoCap16g_ca_p2BehitDataE_ptr
- `0x23EAD4` DinoCap::g_ca_p2BehitData
- `0x22A784` __ZN7DinoCap16g_ca_p2HouseDataE_ptr
- `0x23EAF0` DinoCap::g_ca_p2HouseData
- `0x22A7DC` __ZN7DinoCap15g_ca_p3MoveDataE_ptr
- `0x23EB0C` DinoCap::g_ca_p3MoveData
- `0x22A7E8` __ZN7DinoCap16g_ca_p3StandDataE_ptr
- `0x23EB28` DinoCap::g_ca_p3StandData

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xB9DE8   f0b5       PUSH    {R4-R7,LR}
0xB9DEA   5e46       MOV     R6, R11
0xB9DEC   5546       MOV     R5, R10
0xB9DEE   4446       MOV     R4, R8
0xB9DF0   70b4       PUSH    {R4-R6}
0xB9DF2   06af       ADD     R7, SP, #0x18
0xB9DF4   86b0       SUB     SP, SP, #0x18
0xB9DF6   061c       MOVS    R6, R0
0xB9DF8   0191       STR     R1, [SP,#0x18+var_14]; int
0xB9DFA   141c       MOVS    R4, R2
0xB9DFC   0629       CMP     R1, #6
0xB9DFE   01dd       BLE     loc_B9E04
0xB9E00   00f0ebfe   BL      sub_BABDA
0xB9E04   be4b       LDR     R3, =(__ZN7DinoCap11g_b_HitDownE_ptr - 0xB9E0E)
0xB9E06   0022       MOVS    R2, #0
0xB9E08   081c       MOVS    R0, R1
0xB9E0A   7b44       ADD     R3, PC; __ZN7DinoCap11g_b_HitDownE_ptr
0xB9E0C   1b68       LDR     R3, [R3]; DinoCap::g_b_HitDown ...
0xB9E0E   1a70       STRB    R2, [R3]; DinoCap::g_b_HitDown
0xB9E10   2ef12ee8   BLX     ___switchu8; switch 8 cases
0xB9E1E   201c       MOVS    R0, R4; jumptable 000B9E10 case 0
0xB9E20   2ef13ee8   BLX     ___switch16; switch 5 cases
0xB9E30   b44a       LDR     R2, =(aCom1 - 0xB9E38); jumptable 000B9E20 case 3
0xB9E32   b54b       LDR     R3, =(aA4 - 0xB9E3A); "A4"
```

