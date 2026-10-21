# lifeModeScene::AddBullet

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0CFF3A` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xCFF3A` |
| RVA (EA - 0x1000) | `0xCEF3A` |
| File offset | `0xCEF3A` |
| Size | 216 code bytes in 1 chunk(s), 102 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap13lifeModeScene9AddBulletEi` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::lifeModeScene::AddBullet(int)` |
| Bytes SHA-256 | `51ca9fb9eee179ccac7323886b101578ed0459377abb3e4e40f2cb4cbd59c640` |
| Direct code callers | 4 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | GAMEPLAY / VERIFIED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: r118 0xcff3a (twin mainScene::AddBullet 0x95384): flat ammo for the unlocked weapon's type 0:200 1/4:15 2:60 3:30 5:50, cap g_max_bullet_count; PORTED (GameMission.AwardUnlockBullets) + test in PilotCombatVerify

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameMission.cs:173`](../../../../../Assets/Scripts/GameMission.cs#L173) cites `0xCFF3A`

## Callers and callees (direct code references)

Callers: [`lifeModeScene::AwardItem`](../../../functions/dossiers/lifeModeScene/FUN-0D09DC.md)

Callees: `subWeaponClass::~subWeaponClass`, `___switch8`

## Named data it references

- `0x22A24C` __ZN7DinoCap15g_player_vectorE_ptr
- `0x2404AC` DinoCap::g_player_vector
- `0x22A2B8` __ZN7DinoCap13g_player_typeE_ptr
- `0x23B86C` DinoCap::g_player_type
- `0x22A2B4` __ZNKSt6vectorIN7DinoCap14subWeaponClassESaIS1_EE4sizeEv_ptr
- `0x22A5EC` __ZN7DinoCap18g_max_bullet_countE_ptr
- `0x23B89C` DinoCap::g_max_bullet_count

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xCFF3A   f0b5       PUSH    {R4-R7,LR}
0xCFF3C   5e46       MOV     R6, R11
0xCFF3E   5546       MOV     R5, R10
0xCFF40   4446       MOV     R4, R8
0xCFF42   70b4       PUSH    {R4-R6}
0xCFF44   06af       ADD     R7, SP, #0x18
0xCFF46   8ab0       SUB     SP, SP, #0x28
0xCFF48   324b       LDR     R3, =(__ZN7DinoCap15g_player_vectorE_ptr - 0xCFF52)
0xCFF4A   0024       MOVS    R4, #0
0xCFF4C   0020       MOVS    R0, #0
0xCFF4E   7b44       ADD     R3, PC; __ZN7DinoCap15g_player_vectorE_ptr
0xCFF50   a046       MOV     R8, R4
0xCFF52   1b68       LDR     R3, [R3]; DinoCap::g_player_vector ...
0xCFF54   0091       STR     R1, [SP,#0x28+var_28]
0xCFF56   0290       STR     R0, [SP,#0x28+var_20]
0xCFF58   9b46       MOV     R11, R3
0xCFF5A   2f4b       LDR     R3, =(__ZN7DinoCap13g_player_typeE_ptr - 0xCFF60)
0xCFF5C   7b44       ADD     R3, PC; __ZN7DinoCap13g_player_typeE_ptr
0xCFF5E   1b68       LDR     R3, [R3]; DinoCap::g_player_type ...
0xCFF60   9a46       MOV     R10, R3
0xCFF62   2e4b       LDR     R3, =(__ZNKSt6vectorIN7DinoCap14subWeaponClassESaIS1_EE4sizeEv_ptr - 0xCFF68)
0xCFF64   7b44       ADD     R3, PC; __ZNKSt6vectorIN7DinoCap14subWeaponClassESaIS1_EE4sizeEv_ptr
0xCFF66   1b68       LDR     R3, [R3]
0xCFF68   0193       STR     R3, [SP,#0x28+var_24]
```

