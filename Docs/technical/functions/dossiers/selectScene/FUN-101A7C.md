# selectScene::OnHire

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-101A7C` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x101A7C` |
| RVA (EA - 0x1000) | `0x100A7C` |
| File offset | `0x100A7C` |
| Size | 86 code bytes in 1 chunk(s), 30 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene6OnHireEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::OnHire(void)` |
| Bytes SHA-256 | `cd275f936636624268493ea7f2bedbf02433067b74ac2ea18c5bb9ffc59e8c2b` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.hero.locked_branch_and_hire` (PROVEN, NATIVE (decompiler)): OnSelectChange@0x100b1e builds the selection frame then, if dword_240494[cursor] <= -1 (locked), tail-calls sub_101624 and ends: this+15 (selected) is NOT updated for locked heroes, so Back (id38) commits the last UNLOCKED selection. sub_101624 deletes ids 39..46,58,57,63,90 and creates id39 'ui_btn_hiredFrame' (taHireRect 386,274,88,39, startScaleUI), id47 'ui_p<cursor+1>_infoFrame' (taPlayerInfoRect 208,119,252,181...

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: [`selectScene::OnUIEvent`](../../../functions/dossiers/selectScene/FUN-10B93A.md)

Callees: [`selectScene::OnSelectChange`](../../../functions/dossiers/selectScene/FUN-100B1E.md), `Triniti2D::Scene::GetUIManager`, `Triniti2D::UIManager::DeleteControl`

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x101A7C  90b5       PUSH    {R4,R7,LR}
0x101A7E  01af       ADD     R7, SP, #4
0x101A80  041c       MOVS    R4, R0
0x101A82  4068       LDR     R0, [R0,#4]; this
0x101A84  73f0f1fb   BL      __ZN9Triniti2D5Scene12GetUIManagerEv; Triniti2D::Scene::GetUIManager(void)
0x101A88  2821       MOVS    R1, #0x28 ; '('; int
0x101A8A  7ff0e9fc   BL      __ZN9Triniti2D9UIManager13DeleteControlEi; Triniti2D::UIManager::DeleteControl(int)
0x101A8E  6068       LDR     R0, [R4,#4]; this
0x101A90  73f0ebfb   BL      __ZN9Triniti2D5Scene12GetUIManagerEv; Triniti2D::Scene::GetUIManager(void)
0x101A94  2a21       MOVS    R1, #0x2A ; '*'; int
0x101A96  7ff0e3fc   BL      __ZN9Triniti2D9UIManager13DeleteControlEi; Triniti2D::UIManager::DeleteControl(int)
0x101A9A  6068       LDR     R0, [R4,#4]; this
0x101A9C  73f0e5fb   BL      __ZN9Triniti2D5Scene12GetUIManagerEv; Triniti2D::Scene::GetUIManager(void)
0x101AA0  2b21       MOVS    R1, #0x2B ; '+'; int
0x101AA2  7ff0ddfc   BL      __ZN9Triniti2D9UIManager13DeleteControlEi; Triniti2D::UIManager::DeleteControl(int)
0x101AA6  6068       LDR     R0, [R4,#4]; this
0x101AA8  73f0dffb   BL      __ZN9Triniti2D5Scene12GetUIManagerEv; Triniti2D::Scene::GetUIManager(void)
0x101AAC  2c21       MOVS    R1, #0x2C ; ','; int
0x101AAE  7ff0d7fc   BL      __ZN9Triniti2D9UIManager13DeleteControlEi; Triniti2D::UIManager::DeleteControl(int)
0x101AB2  6068       LDR     R0, [R4,#4]; this
0x101AB4  73f0d9fb   BL      __ZN9Triniti2D5Scene12GetUIManagerEv; Triniti2D::Scene::GetUIManager(void)
0x101AB8  2d21       MOVS    R1, #0x2D ; '-'; int
0x101ABA  7ff0d1fc   BL      __ZN9Triniti2D9UIManager13DeleteControlEi; Triniti2D::UIManager::DeleteControl(int)
0x101ABE  6068       LDR     R0, [R4,#4]; this
```

