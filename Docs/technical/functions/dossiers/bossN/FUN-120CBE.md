# bossN::MissionEnd

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-120CBE` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x120CBE` |
| RVA (EA - 0x1000) | `0x11FCBE` |
| File offset | `0x11FCBE` |
| Size | 44 code bytes in 1 chunk(s), 19 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap5bossN10MissionEndEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::bossN::MissionEnd(void)` |
| Bytes SHA-256 | `2e0bbe49c1354ef1fdce5e9514ac77b191dee0523e55fc8d08e80753d2f3cc09` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameBoss.cs:232`](../../../../../Assets/Scripts/GameBoss.cs#L232) cites `0x120CBE`

## Callers and callees (direct code references)

Callers: [`bossN::OnTimeout`](../../../functions/dossiers/bossN/FUN-1217A8.md)

Callees: `Triniti2D::GameObject::GetScene`, `Triniti2D::Scene::GetCallBack`, `___dynamic_cast`

## Named data it references

- `0x22A0C0` __ZTIN9Triniti2D13SceneCallBackE_ptr
- `0x22A274` __ZTIN7DinoCap13lifeModeSceneE_ptr
- `0x22CF10` `typeinfo for'DinoCap::lifeModeScene
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x120CBE  80b5       PUSH    {R7,LR}
0x120CC0  00af       ADD     R7, SP, #0
0x120CC2  4068       LDR     R0, [R0,#4]; this
0x120CC4  4bf044f9   BL      __ZN9Triniti2D10GameObject8GetSceneEv; Triniti2D::GameObject::GetScene(void)
0x120CC8  54f0b3fa   BL      __ZN9Triniti2D5Scene11GetCallBackEv; Triniti2D::Scene::GetCallBack(void)
0x120CCC  0028       CMP     R0, #0
0x120CCE  08d0       BEQ     loc_120CE2
0x120CD0  0649       LDR     R1, =(__ZTIN9Triniti2D13SceneCallBackE_ptr - 0x120CDA)
0x120CD2  074a       LDR     R2, =(__ZTIN7DinoCap13lifeModeSceneE_ptr - 0x120CDC)
0x120CD4  0023       MOVS    R3, #0; s2d
0x120CD6  7944       ADD     R1, PC; __ZTIN9Triniti2D13SceneCallBackE_ptr
0x120CD8  7a44       ADD     R2, PC; __ZTIN7DinoCap13lifeModeSceneE_ptr
0x120CDA  0968       LDR     R1, [R1]; lpstype
0x120CDC  1268       LDR     R2, [R2]; lpdtype
0x120CDE  c8f0feef   BLX     ___dynamic_cast
0x120CE2  0368       LDR     R3, [R0]
0x120CE4  9b6b       LDR     R3, [R3,#0x38]
0x120CE6  9847       BLX     R3
0x120CE8  80bd       POP     {R7,PC}
```

