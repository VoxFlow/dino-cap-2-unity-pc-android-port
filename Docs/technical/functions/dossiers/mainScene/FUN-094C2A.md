# mainScene::CanRollDown

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-094C2A` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x94C2A` |
| RVA (EA - 0x1000) | `0x93C2A` |
| File offset | `0x93C2A` |
| Size | 20 code bytes in 1 chunk(s), 9 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9mainScene11CanRollDownEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::mainScene::CanRollDown(void)` |
| Bytes SHA-256 | `3ea63a5ea5b8ac82d81cd4ee78d1bc651ac5ff48eb5726058be373ee76b1c8c4` |
| Direct code callers | 4 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.ui.roll_fill_clip.r118z4` (PROVEN, NATIVE): Roll cooldown fill: UISprite SetClip clips the 88x88 sprite (rect kept), drain dt * -20.0 * (2.0 * 0.7) per second in binary32, colour 0xB0000000 (black alpha 176), control id 39 in mainScene types 1/5 and 45 otherwise; kept with an empty clip when the clip height reaches 0; CanRollDown94c3c only hides it (r118z4 report.r5 supersedes the old deletion claim). The port resized the sprite with other constants (lifeMode_...

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameEgg.cs:108`](../../../../../Assets/Scripts/GameEgg.cs#L108) cites `0x94C2A`
- [`Assets/Scripts/GameEgg.cs:109`](../../../../../Assets/Scripts/GameEgg.cs#L109) cites `0x94C2A`

## Callers and callees (direct code references)

Callers: [`playerClass::getDownEgg`](../../../functions/dossiers/playerClass/FUN-0B96F8.md), `playerClass::OnTimeout`, [`playerNewAniClass::getDownEgg`](../../../functions/dossiers/playerNewAniClass/FUN-1270FA.md), [`playerNewAniClass::OnTimeout`](../../../functions/dossiers/playerNewAniClass/FUN-127DAC.md)

Callees: none.

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x94C2A   80b5       PUSH    {R7,LR}
0x94C2C   00af       ADD     R7, SP, #0
0x94C2E   9c235b00   MOVS    R3, #0x138
0x94C32   0021       MOVS    R1, #0
0x94C34   c058       LDR     R0, [R0,R3]
0x94C36   0368       LDR     R3, [R0]
0x94C38   5b6a       LDR     R3, [R3,#0x24]
0x94C3A   9847       BLX     R3
0x94C3C   80bd       POP     {R7,PC}
```

