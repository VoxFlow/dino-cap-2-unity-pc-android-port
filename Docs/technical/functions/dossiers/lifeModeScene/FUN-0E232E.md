# lifeModeScene::StartGame_N

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0E232E` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xE232E` |
| RVA (EA - 0x1000) | `0xE132E` |
| File offset | `0xE132E` |
| Size | 1382 code bytes in 1 chunk(s), 502 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap13lifeModeScene11StartGame_NEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::lifeModeScene::StartGame_N(void)` |
| Bytes SHA-256 | `8ad8cc40370431e7fc9e9cd269b19ab96c9b012a4d509d760c9f73350fcf7100` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | GAMEPLAY / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: r118 0xe232e: PORTED (GameIntro.StartGame)

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameCombat.cs:123`](../../../../../Assets/Scripts/GameCombat.cs#L123) cites `0xE232E`
- [`Assets/Scripts/GameCombat.cs:124`](../../../../../Assets/Scripts/GameCombat.cs#L124) cites `0xE232E`
- [`Assets/Scripts/GameCombat.cs:130`](../../../../../Assets/Scripts/GameCombat.cs#L130) cites `0xE232E`
- [`Assets/Scripts/GameIntro.cs:8`](../../../../../Assets/Scripts/GameIntro.cs#L8) cites `0xE232E`
- [`Assets/Scripts/GameIntro.cs:154`](../../../../../Assets/Scripts/GameIntro.cs#L154) cites `0xE232E`
- [`Assets/Scripts/GameMission.cs:70`](../../../../../Assets/Scripts/GameMission.cs#L70) cites `0xE232E`

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: `Triniti2D::Scene::GetUIManager`, `Triniti2D::Scene::SetTimer`, `Triniti2D::UIManager::DeleteControl`, `___switch16`, `___addsf3vfp`, `___floatsisfvfp`, `___mulsf3vfp`, `_rand`

## Strings it references

- `0x20741C` "g_time_id"

## Named data it references

- `0x22A25C` __ZN7DinoCap14g_cur_map_dataE_ptr
- `0x23B87C` DinoCap::g_cur_map_data
- `0x22A270` __ZN7DinoCap10g_curMapIDE_ptr
- `0x23B878` DinoCap::g_curMapID
- `0x22A280` __ZN7DinoCap10g_map_infoE_ptr
- `0x2404EC` DinoCap::g_map_info
- `0x22A2A8` __ZNKSt6vectorIiSaIiEE4sizeEv_ptr
- `0x22A3D8` __ZN7DinoCap9g_prop_k1E_ptr
- `0x23DB8C` DinoCap::g_prop_k1
- `0x22A35C` __ZN7DinoCap9g_prop_k2E_ptr
- `0x23DBAC` DinoCap::g_prop_k2
- `0x22A464` __ZN7DinoCap9g_prop_k3E_ptr
- `0x23DBCC` DinoCap::g_prop_k3
- `0x22A358` __ZN7DinoCap9g_prop_k4E_ptr
- `0x23DBEC` DinoCap::g_prop_k4
- `0x22A500` __ZN7DinoCap9g_prop_k5E_ptr
- `0x23DC0C` DinoCap::g_prop_k5
- `0x22A3FC` __ZN7DinoCap9g_prop_k6E_ptr
- `0x23DC2C` DinoCap::g_prop_k6
- `0x22A394` __ZN7DinoCap9g_prop_k7E_ptr
- `0x23DC4C` DinoCap::g_prop_k7
- `0x22A494` __ZN7DinoCap9g_prop_k8E_ptr
- `0x23DC6C` DinoCap::g_prop_k8
- `0x22A390` __ZN7DinoCap9g_prop_k9E_ptr
- `0x23DC8C` DinoCap::g_prop_k9
- `0x22A46C` __ZN7DinoCap10g_prop_k10E_ptr
- `0x23DCAC` DinoCap::g_prop_k10
- `0x22A364` __ZN7DinoCap10g_prop_k11E_ptr
- `0x23DCCC` DinoCap::g_prop_k11
- `0x22A508` __ZN7DinoCap10g_prop_k12E_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xE232E   f0b5       PUSH    {R4-R7,LR}
0xE2330   5e46       MOV     R6, R11
0xE2332   5546       MOV     R5, R10
0xE2334   4446       MOV     R4, R8
0xE2336   70b4       PUSH    {R4-R6}
0xE2338   06af       ADD     R7, SP, #0x18
0xE233A   81b0       SUB     SP, SP, #4
0xE233C   051c       MOVS    R5, R0
0xE233E   4068       LDR     R0, [R0,#4]; this
0xE2340   92f093ff   BL      __ZN9Triniti2D5Scene12GetUIManagerEv; Triniti2D::Scene::GetUIManager(void)
0xE2344   0b21       MOVS    R1, #0xB; int
0xE2346   9ff08bf8   BL      __ZN9Triniti2D9UIManager13DeleteControlEi; Triniti2D::UIManager::DeleteControl(int)
0xE234A   94235b00   MOVS    R3, #0x128
0xE234E   0022       MOVS    R2, #0
0xE2350   ea50       STR     R2, [R5,R3]
0xE2352   aa235b00   MOVS    R3, #0x154
0xE2356   0422       MOVS    R2, #4
0xE2358   ea50       STR     R2, [R5,R3]
0xE235A   a3239b00   MOVS    R3, #0x28C
0xE235E   0122       MOVS    R2, #1
0xE2360   ea54       STRB    R2, [R5,R3]
0xE2362   e94b       LDR     R3, =(__ZN7DinoCap14g_cur_map_dataE_ptr - 0xE236A)
0xE2364   5420       MOVS    R0, #0x54 ; 'T'
0xE2366   7b44       ADD     R3, PC; __ZN7DinoCap14g_cur_map_dataE_ptr
```

