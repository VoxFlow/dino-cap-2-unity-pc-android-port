# playerClass::getEgg

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0B937C` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xB937C` |
| RVA (EA - 0x1000) | `0xB837C` |
| File offset | `0xB837C` |
| Size | 558 code bytes in 1 chunk(s), 218 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11playerClass6getEggEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerClass::getEgg(void)` |
| Bytes SHA-256 | `5e1ba08982b0245a185a7ff09a8ff9d0f2b0c2308936e9fd0b19256f6d8b6a7f` |
| Direct code callers | 3 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameEgg.cs:8`](../../../../../Assets/Scripts/GameEgg.cs#L8) cites `0xB937C`
- [`Assets/Scripts/GameEgg.cs:121`](../../../../../Assets/Scripts/GameEgg.cs#L121) cites `0xB937C`
- [`Assets/Scripts/GameEgg.cs:159`](../../../../../Assets/Scripts/GameEgg.cs#L159) cites `0xB937C`
- [`Assets/Scripts/NativeCaAnimation.cs:129`](../../../../../Assets/Scripts/NativeCaAnimation.cs#L129) cites `0xB937C`

## Callers and callees (direct code references)

Callers: [`playerClass::SwitchWeapon`](../../../functions/dossiers/playerClass/FUN-0BAC3C.md), [`playerClass::AfterNoBullets`](../../../functions/dossiers/playerClass/FUN-0BAEAC.md), [`eggClass::OnUpdate`](../../../functions/dossiers/eggClass/FUN-11FEB6.md)

Callees: [`CombineAnimation::ChangeImage`](../../../functions/dossiers/CombineAnimation/FUN-00CC8A.md), [`PlaySound`](../../../functions/dossiers/DinoCap/FUN-01A448.md), `___switch16`

## Strings it references

- `0x2012D8` "yyj4"
- `0x208294` "shenti0001_egg"
- `0x20127C` "yuanjian4"
- `0x2082A4` "shenti0002_egg"
- `0x201394` "com2"
- `0x2013A4` "com4"
- `0x2082B4` "shenti0003_egg"
- `0x2082C4` "shenti0004_egg"
- `0x204F5C` "A_get_egg"

## Named data it references

- `0x22A2B8` __ZN7DinoCap13g_player_typeE_ptr
- `0x23B86C` DinoCap::g_player_type

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xB937C   f0b5       PUSH    {R4-R7,LR}
0xB937E   03af       ADD     R7, SP, #0xC
0xB9380   8a4b       LDR     R3, =(__ZN7DinoCap13g_player_typeE_ptr - 0xB9388)
0xB9382   041c       MOVS    R4, R0
0xB9384   7b44       ADD     R3, PC; __ZN7DinoCap13g_player_typeE_ptr
0xB9386   1868       LDR     R0, [R3]; DinoCap::g_player_type ...
0xB9388   0368       LDR     R3, [R0]; DinoCap::g_player_type
0xB938A   181c       MOVS    R0, R3
0xB938C   2ef188ed   BLX     ___switch16; switch 5 cases
0xB939C   844e       LDR     R6, =(aYyj4 - 0xB93A6); jumptable 000B938C case 0
0xB939E   854d       LDR     R5, =(aShenti0001Egg - 0xB93A8); "shenti0001_egg"
0xB93A0   c623       MOVS    R3, #0xC6
0xB93A2   7e44       ADD     R6, PC; "yyj4"
0xB93A4   7d44       ADD     R5, PC; "shenti0001_egg"
0xB93A6   5b00       LSLS    R3, R3, #1
0xB93A8   e018       ADDS    R0, R4, R3; this
0xB93AA   311c       MOVS    R1, R6; char *
0xB93AC   2a1c       MOVS    R2, R5; char *
0xB93AE   824b       LDR     R3, flt_B95B8; float
0xB93B0   53f76bfc   BL      __ZN7DinoCap16CombineAnimation11ChangeImageEPKcS2_f; DinoCap::CombineAnimation::ChangeImage(char const*,char const*,float)
0xB93B4   d4235b00   MOVS    R3, #0x1A8
0xB93B8   e018       ADDS    R0, R4, R3; this
0xB93BA   311c       MOVS    R1, R6; char *
0xB93BC   2a1c       MOVS    R2, R5; char *
```

