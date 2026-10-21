# playerClass::OnUpdate

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0BB6CC` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xBB6CC` |
| RVA (EA - 0x1000) | `0xBA6CC` |
| File offset | `0xBA6CC` |
| Size | 172 code bytes in 1 chunk(s), 76 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11playerClass8OnUpdateEf` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerClass::OnUpdate(float)` |
| Bytes SHA-256 | `230ea5f17f8364d4890f8892c5c137ec3097bc5b4b416418cc5df7a20b6c32f2` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameCombat.cs:52`](../../../../../Assets/Scripts/GameCombat.cs#L52) cites `0xBB700`
- [`Assets/Scripts/GameScene.cs:117`](../../../../../Assets/Scripts/GameScene.cs#L117) cites `0xBB6E6`
- [`Assets/Scripts/GameScene.cs:326`](../../../../../Assets/Scripts/GameScene.cs#L326) cites `0xBB6CC`
- [`Assets/Scripts/GameScene.cs:328`](../../../../../Assets/Scripts/GameScene.cs#L328) cites `0xBB75C`
- [`Assets/Scripts/NativeCaAnimation.cs:204`](../../../../../Assets/Scripts/NativeCaAnimation.cs#L204) cites `0xBB6CC`

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: [`CAObjCallBack::OnUpdate`](../../../functions/dossiers/CAObjCallBack/FUN-00A6CC.md), [`playerClass::UpdateControl`](../../../functions/dossiers/playerClass/FUN-0B8A60.md), [`playerClass::UpdateCamera`](../../../functions/dossiers/playerClass/FUN-0BB054.md), `___addsf3vfp`, `___gesf2vfp`, `___lesf2vfp`, `___mulsf3vfp`

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xBB6CC   f0b5       PUSH    {R4-R7,LR}
0xBB6CE   5646       MOV     R6, R10
0xBB6D0   4546       MOV     R5, R8
0xBB6D2   60b4       PUSH    {R5,R6}
0xBB6D4   05af       ADD     R7, SP, #0x14
0xBB6D6   a5239b00   MOVS    R3, #0x294
0xBB6DA   041c       MOVS    R4, R0
0xBB6DC   0e1c       MOVS    R6, R1
0xBB6DE   e150       STR     R1, [R4,R3]
0xBB6E0   254b       LDR     R3, =0x293
0xBB6E2   c35c       LDRB    R3, [R0,R3]
0xBB6E4   002b       CMP     R3, #0
0xBB6E6   05d1       BNE     loc_BB6F4
0xBB6E8   a0239b00   MOVS    R3, #0x280
0xBB6EC   c358       LDR     R3, [R0,R3]
0xBB6EE   072b       CMP     R3, #7
0xBB6F0   3ed1       BNE     loc_BB770
0xBB6F2   39e0       B       loc_BB768
0xBB6F4   4ef7eaff   BL      __ZN7DinoCap13CAObjCallBack8OnUpdateEf; DinoCap::CAObjCallBack::OnUpdate(float)
0xBB6F8   204b       LDR     R3, =0x292
0xBB6FA   e35c       LDRB    R3, [R4,R3]
0xBB6FC   002b       CMP     R3, #0
0xBB6FE   24d0       BEQ     loc_BB74A
0xBB700   8822       MOVS    R2, #0x88
```

**Around `0xBB75C` (cited by the Unity code)**

```
0xBB750   131f       SUBS    R3, R2, #4
0xBB752   012b       CMP     R3, #1
0xBB754   08d9       BLS     loc_BB768
0xBB756   072a       CMP     R2, #7
0xBB758   06d0       BEQ     loc_BB768
0xBB75A   0d4b       LDR     R3, =0x291
0xBB75C   e35c       LDRB    R3, [R4,R3]   <==
0xBB75E   002b       CMP     R3, #0
0xBB760   02d0       BEQ     loc_BB768
0xBB762   201c       MOVS    R0, R4; this
0xBB764   fdf77cf9   BL      __ZN7DinoCap11playerClass13UpdateControlEv; DinoCap::playerClass::UpdateControl(void)
0xBB768   201c       MOVS    R0, R4; this
0xBB76A   311c       MOVS    R1, R6; float
```

