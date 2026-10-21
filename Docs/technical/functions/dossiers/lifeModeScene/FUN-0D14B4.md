# lifeModeScene::StartGame

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0D14B4` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xD14B4` |
| RVA (EA - 0x1000) | `0xD04B4` |
| File offset | `0xD04B4` |
| Size | 270 code bytes in 1 chunk(s), 116 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap13lifeModeScene9StartGameEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::lifeModeScene::StartGame(void)` |
| Bytes SHA-256 | `b5a9433cd7290ae544ad79d17704c1d85bab0535f8e5b306fe5b28ca9b920d90` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: `Triniti2D::Scene::GetUIManager`, `Triniti2D::Scene::SetTimer`, `Triniti2D::UIManager::DeleteControl`, `___switch8`

## Strings it references

- `0x20741C` "g_time_id"

## Named data it references

- `0x22A4B4` __ZN7DinoCap15g_lifeProperty1E_ptr
- `0x23DDEC` DinoCap::g_lifeProperty1
- `0x22A25C` __ZN7DinoCap14g_cur_map_dataE_ptr
- `0x23B87C` DinoCap::g_cur_map_data
- `0x22A270` __ZN7DinoCap10g_curMapIDE_ptr
- `0x23B878` DinoCap::g_curMapID
- `0x22A280` __ZN7DinoCap10g_map_infoE_ptr
- `0x2404EC` DinoCap::g_map_info
- `0x22A2A8` __ZNKSt6vectorIiSaIiEE4sizeEv_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xD14B4   f0b5       PUSH    {R4-R7,LR}
0xD14B6   4646       MOV     R6, R8
0xD14B8   40b4       PUSH    {R6}
0xD14BA   04af       ADD     R7, SP, #0x10
0xD14BC   041c       MOVS    R4, R0
0xD14BE   4068       LDR     R0, [R0,#4]; this
0xD14C0   a3f0d3fe   BL      __ZN9Triniti2D5Scene12GetUIManagerEv; Triniti2D::Scene::GetUIManager(void)
0xD14C4   0b21       MOVS    R1, #0xB; int
0xD14C6   aff0cbff   BL      __ZN9Triniti2D9UIManager13DeleteControlEi; Triniti2D::UIManager::DeleteControl(int)
0xD14CA   94235b00   MOVS    R3, #0x128
0xD14CE   0022       MOVS    R2, #0
0xD14D0   e250       STR     R2, [R4,R3]
0xD14D2   3c49       LDR     R1, =(aGTimeId - 0xD14E0); "g_time_id"
0xD14D4   aa235b00   MOVS    R3, #0x154
0xD14D8   0422       MOVS    R2, #4
0xD14DA   e250       STR     R2, [R4,R3]
0xD14DC   7944       ADD     R1, PC; "g_time_id"
0xD14DE   3a4a       LDR     R2, flt_D15C8; float
0xD14E0   0023       MOVS    R3, #0; bool
0xD14E2   6068       LDR     R0, [R4,#4]; this
0xD14E4   a4f0b0f8   BL      __ZN9Triniti2D5Scene8SetTimerEPvfb; Triniti2D::Scene::SetTimer(void *,float,bool)
0xD14E8   9f239b00   MOVS    R3, #0x27C
0xD14EC   d8225200   MOVS    R2, #0x1B0
0xD14F0   0025       MOVS    R5, #0
```

