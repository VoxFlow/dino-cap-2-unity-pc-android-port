# lifeModeScene::StopCreateEnemy

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0D0E14` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xD0E14` |
| RVA (EA - 0x1000) | `0xCFE14` |
| File offset | `0xCFE14` |
| Size | 194 code bytes in 1 chunk(s), 72 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap13lifeModeScene15StopCreateEnemyEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::lifeModeScene::StopCreateEnemy(void)` |
| Bytes SHA-256 | `5fa6b25929e47b904d41e2dbebb2fa2fe2c8c9d10483396eb3df23e7dfcddf1c` |
| Direct code callers | 6 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameCombat.cs:173`](../../../../../Assets/Scripts/GameCombat.cs#L173) cites `0xD0E14`
- [`Assets/Scripts/GameMission.cs:9`](../../../../../Assets/Scripts/GameMission.cs#L9) cites `0xD0E14`

## Callers and callees (direct code references)

Callers: [`lifeModeScene::SubTime`](../../../functions/dossiers/lifeModeScene/FUN-0D0ED6.md), `lifeModeScene::OnUnload`, [`lifeModeScene::UpdateKillDinocap`](../../../functions/dossiers/lifeModeScene/FUN-0E1E62.md), [`lifeModeScene::GameClear`](../../../functions/dossiers/lifeModeScene/FUN-0F0FF8.md), [`bossN::BeAttacked`](../../../functions/dossiers/bossN/FUN-123B70.md)

Callees: `Triniti2D::Scene::KillTimer`

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xD0E14   f0b5       PUSH    {R4-R7,LR}
0xD0E16   03af       ADD     R7, SP, #0xC
0xD0E18   e025       MOVS    R5, #0xE0
0xD0E1A   041c       MOVS    R4, R0
0xD0E1C   6d00       LSLS    R5, R5, #1
0xD0E1E   0126       MOVS    R6, #1
0xD0E20   7642       NEGS    R6, R6
0xD0E22   6159       LDR     R1, [R4,R5]; int
0xD0E24   4068       LDR     R0, [R0,#4]; this
0xD0E26   a4f035fc   BL      __ZN9Triniti2D5Scene9KillTimerEi; Triniti2D::Scene::KillTimer(int)
0xD0E2A   6651       STR     R6, [R4,R5]
0xD0E2C   e2256d00   MOVS    R5, #0x1C4
0xD0E30   6068       LDR     R0, [R4,#4]; this
0xD0E32   6159       LDR     R1, [R4,R5]; int
0xD0E34   a4f02efc   BL      __ZN9Triniti2D5Scene9KillTimerEi; Triniti2D::Scene::KillTimer(int)
0xD0E38   6651       STR     R6, [R4,R5]
0xD0E3A   e4256d00   MOVS    R5, #0x1C8
0xD0E3E   6068       LDR     R0, [R4,#4]; this
0xD0E40   6159       LDR     R1, [R4,R5]; int
0xD0E42   a4f027fc   BL      __ZN9Triniti2D5Scene9KillTimerEi; Triniti2D::Scene::KillTimer(int)
0xD0E46   6651       STR     R6, [R4,R5]
0xD0E48   e6256d00   MOVS    R5, #0x1CC
0xD0E4C   6068       LDR     R0, [R4,#4]; this
0xD0E4E   6159       LDR     R1, [R4,R5]; int
```

