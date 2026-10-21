# menuScene::OnTouchEvent

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0CC8B2` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xCC8B2` |
| RVA (EA - 0x1000) | `0xCB8B2` |
| File offset | `0xCB8B2` |
| Size | 68 code bytes in 1 chunk(s), 30 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9menuScene12OnTouchEventEiiii` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::menuScene::OnTouchEvent(int,int,int,int)` |
| Bytes SHA-256 | `c2dc9a32c8b9baec972e0c4a9a77d3ad2e6a1d463e065a9a3865e4926a5fa99d` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.title.tap_flow` (PROVEN, NATIVE): Tap handling: control id1 (Tap-to-start UIMoveCtrl) pop-in completion (event 1) while scene state(+12)==0 sets ready flag (+280). OnTouchEvent@0xcc8b2: if state not 1/4 and flag set: clear flag, ButtonEffect('ui_menu_startFrame', control id1), life_Mode=0, state=1. ButtonEffect@0xcc6f0 reuses/creates UIMoveCtrl id 23 (layer 3) with the same texture at the same rect and StartMove(d1=0.05,d2=0.2): a cloned overlay that...

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: [`menuScene::ButtonEffect`](../../../functions/dossiers/menuScene/FUN-0CC6F0.md), `Triniti2D::Scene::GetUIManager`, `Triniti2D::UIManager::GetControl`

## Strings it references

- `0x209254` "ui_menu_startFrame"

## Named data it references

- `0x22A290` __ZN7DinoCap9life_ModeE_ptr
- `0x23E09C` DinoCap::life_Mode

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xCC8B2   b0b5       PUSH    {R4,R5,R7,LR}
0xCC8B4   02af       ADD     R7, SP, #8
0xCC8B6   c368       LDR     R3, [R0,#0xC]
0xCC8B8   041c       MOVS    R4, R0
0xCC8BA   012b       CMP     R3, #1
0xCC8BC   1ad0       BEQ     locret_CC8F4
0xCC8BE   042b       CMP     R3, #4
0xCC8C0   18d0       BEQ     locret_CC8F4
0xCC8C2   8c225200   MOVS    R2, #0x118
0xCC8C6   835c       LDRB    R3, [R0,R2]
0xCC8C8   002b       CMP     R3, #0
0xCC8CA   13d0       BEQ     locret_CC8F4
0xCC8CC   0025       MOVS    R5, #0
0xCC8CE   8554       STRB    R5, [R0,R2]
0xCC8D0   4068       LDR     R0, [R0,#4]; this
0xCC8D2   a8f0cafc   BL      __ZN9Triniti2D5Scene12GetUIManagerEv; Triniti2D::Scene::GetUIManager(void)
0xCC8D6   0121       MOVS    R1, #1; int
0xCC8D8   b4f0fefb   BL      __ZN9Triniti2D9UIManager10GetControlEi; Triniti2D::UIManager::GetControl(int)
0xCC8DC   0649       LDR     R1, =(aUiMenuStartfra - 0xCC8E2); "ui_menu_startFrame"
0xCC8DE   7944       ADD     R1, PC; "ui_menu_startFrame"
0xCC8E0   021c       MOVS    R2, R0; Triniti2D::UIControl *
0xCC8E2   201c       MOVS    R0, R4; this
0xCC8E4   fff704ff   BL      __ZN7DinoCap9menuScene12ButtonEffectEPKcPN9Triniti2D9UIControlE; DinoCap::menuScene::ButtonEffect(char const*,Triniti2D::UIControl *)
0xCC8E8   044b       LDR     R3, =(__ZN7DinoCap9life_ModeE_ptr - 0xCC8EE)
```

