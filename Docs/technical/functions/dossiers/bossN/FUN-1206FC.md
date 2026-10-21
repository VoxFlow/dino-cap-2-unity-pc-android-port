# bossN::HouCannotTouch

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-1206FC` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x1206FC` |
| RVA (EA - 0x1000) | `0x11F6FC` |
| File offset | `0x11F6FC` |
| Size | 224 code bytes in 1 chunk(s), 92 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap5bossN14HouCannotTouchEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::bossN::HouCannotTouch(void)` |
| Bytes SHA-256 | `b1671d88885659c5109514f73eac71441923c842c0e4c7634826a86c34244c13` |
| Direct code callers | 2 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameBoss.cs:239`](../../../../../Assets/Scripts/GameBoss.cs#L239) cites `0x1206FC`

## Callers and callees (direct code references)

Callers: [`bossN::OnUpdate`](../../../functions/dossiers/bossN/FUN-121A98.md)

Callees: [`playerClass::BeAttackByBoss`](../../../functions/dossiers/playerClass/FUN-0BD3C8.md), [`playerNewAniClass::BeAttackByBoss`](../../../functions/dossiers/playerNewAniClass/FUN-1286E4.md), `Triniti2D::GameObject::GetPosition`, `Triniti2D::GameObject::GetCallBack`, `___addsf3vfp`, `___dynamic_cast`, `___gtsf2vfp`, `___ltsf2vfp`, `___mulsf3vfp`

## Named data it references

- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A2B8` __ZN7DinoCap13g_player_typeE_ptr
- `0x23B86C` DinoCap::g_player_type
- `0x22A0A8` __ZTIN9Triniti2D18GameObjectCallBackE_ptr
- `0x22A288` __ZTIN7DinoCap17playerNewAniClassE_ptr
- `0x22D338` `typeinfo for'DinoCap::playerNewAniClass
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22A0CC` __ZTIN7DinoCap11playerClassE_ptr
- `0x22C8B0` `typeinfo for'DinoCap::playerClass

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x1206FC  f0b5       PUSH    {R4-R7,LR}
0x1206FE  4646       MOV     R6, R8
0x120700  40b4       PUSH    {R6}
0x120702  04af       ADD     R7, SP, #0x10
0x120704  354b       LDR     R3, =(__ZN7DinoCap8g_iphoneE_ptr - 0x12070C)
0x120706  041c       MOVS    R4, R0
0x120708  7b44       ADD     R3, PC; __ZN7DinoCap8g_iphoneE_ptr
0x12070A  1b68       LDR     R3, [R3]; DinoCap::g_iphone ...
0x12070C  1b78       LDRB    R3, [R3]; DinoCap::g_iphone
0x12070E  002b       CMP     R3, #0
0x120710  01d1       BNE     loc_120716
0x120712  334b       LDR     R3, =0x40000000
0x120714  00e0       B       loc_120718
0x120716  334b       LDR     R3, =0x3F800000
0x120718  9846       MOV     R8, R3
0x12071A  df23       MOVS    R3, #0xDF
0x12071C  e35c       LDRB    R3, [R4,R3]
0x12071E  002b       CMP     R3, #0
0x120720  15d1       BNE     loc_12074E
0x120722  ec235b00   MOVS    R3, #0x1D8
0x120726  e058       LDR     R0, [R4,R3]; this
0x120728  4bf00cfc   BL      __ZN9Triniti2D10GameObject11GetPositionEv; Triniti2D::GameObject::GetPosition(void)
0x12072C  0668       LDR     R6, [R0]
0x12072E  6068       LDR     R0, [R4,#4]; this
```

