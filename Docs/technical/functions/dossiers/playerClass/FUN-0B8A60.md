# playerClass::UpdateControl

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0B8A60` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xB8A60` |
| RVA (EA - 0x1000) | `0xB7A60` |
| File offset | `0xB7A60` |
| Size | 312 code bytes in 1 chunk(s), 137 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11playerClass13UpdateControlEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerClass::UpdateControl(void)` |
| Bytes SHA-256 | `ab752accdb3c0a64eb70492b215085e98fb07a1bc5ad6a2d38879381b6a8d549` |
| Direct code callers | 3 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameScene.cs:10`](../../../../../Assets/Scripts/GameScene.cs#L10) cites `0xB8A60`
- [`Assets/Scripts/GameScene.cs:276`](../../../../../Assets/Scripts/GameScene.cs#L276) cites `0xB8A60`

## Callers and callees (direct code references)

Callers: [`playerClass::OnUpdate`](../../../functions/dossiers/playerClass/FUN-0BB6CC.md), [`playerClass::OnAnimationEnd`](../../../functions/dossiers/playerClass/FUN-0BD978.md)

Callees: [`playerClass::Stand`](../../../functions/dossiers/playerClass/FUN-0B88B4.md), [`playerClass::Move`](../../../functions/dossiers/playerClass/FUN-0B8900.md), [`playerClass::UpdateBG`](../../../functions/dossiers/playerClass/FUN-0B8944.md), `Triniti2D::GameObject::Physics`, `Triniti2D::GamePhysics::SetLinearVelocity`, `Triniti2D::GamePhysics::WakeUp`

## Named data it references

- `0x22A74C` __ZN7DinoCap12g_b_LeftDownE_ptr
- `0x23B8C0` DinoCap::g_b_LeftDown
- `0x22A2DC` __ZN7DinoCap13g_prop_PlayerE_ptr
- `0x240474` DinoCap::g_prop_Player
- `0x22A6EC` __ZN7DinoCap13g_b_RightDownE_ptr
- `0x23B8C1` DinoCap::g_b_RightDown

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xB8A60   90b5       PUSH    {R4,R7,LR}
0xB8A62   01af       ADD     R7, SP, #4
0xB8A64   86b0       SUB     SP, SP, #0x18
0xB8A66   4c4b       LDR     R3, =(__ZN7DinoCap12g_b_LeftDownE_ptr - 0xB8A6E)
0xB8A68   041c       MOVS    R4, R0
0xB8A6A   7b44       ADD     R3, PC; __ZN7DinoCap12g_b_LeftDownE_ptr
0xB8A6C   1b68       LDR     R3, [R3]; DinoCap::g_b_LeftDown ...
0xB8A6E   1a78       LDRB    R2, [R3]; DinoCap::g_b_LeftDown
0xB8A70   002a       CMP     R2, #0
0xB8A72   2fd0       BEQ     loc_B8AD4
0xB8A74   a0229200   MOVS    R2, #0x280
0xB8A78   8358       LDR     R3, [R0,R2]
0xB8A7A   002b       CMP     R3, #0
0xB8A7C   00d1       BNE     loc_B8A80
0xB8A7E   89e0       B       loc_B8B94
0xB8A80   032b       CMP     R3, #3
0xB8A82   00d1       BNE     loc_B8A86
0xB8A84   86e0       B       loc_B8B94
0xB8A86   072b       CMP     R3, #7
0xB8A88   00d1       BNE     loc_B8A8C
0xB8A8A   83e0       B       loc_B8B94
0xB8A8C   092b       CMP     R3, #9
0xB8A8E   00d1       BNE     loc_B8A92
0xB8A90   80e0       B       loc_B8B94
```

