# eggClass::OnUpdate

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-11FEB6` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x11FEB6` |
| RVA (EA - 0x1000) | `0x11EEB6` |
| File offset | `0x11EEB6` |
| Size | 184 code bytes in 1 chunk(s), 77 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap8eggClass8OnUpdateEf` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::eggClass::OnUpdate(float)` |
| Bytes SHA-256 | `db1d1b129c5611e2acbde41b1ae8f57542f60ef55cd2582fd75228f34a6596f5` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameEgg.cs:6`](../../../../../Assets/Scripts/GameEgg.cs#L6) cites `0x11FEB6`
- [`Assets/Scripts/GameEgg.cs:111`](../../../../../Assets/Scripts/GameEgg.cs#L111) cites `0x11FEB6`

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: [`mainScene::UnRollDown`](../../../functions/dossiers/mainScene/FUN-094C6C.md), [`playerClass::getEgg`](../../../functions/dossiers/playerClass/FUN-0B937C.md), [`playerNewAniClass::getEgg`](../../../functions/dossiers/playerNewAniClass/FUN-126E2E.md), `Triniti2D::Camera::GetViewport`, `Triniti2D::GameObject::GetPosition`, `Triniti2D::GameObject::GetScene`, `Triniti2D::GameObject::GetCallBack`, `Triniti2D::GameObject::Delete`, `Triniti2D::Scene::GetCamera`, `___dynamic_cast`, `___ltsf2vfp`, `___subsf3vfp`

## Named data it references

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
0x11FEB6  f0b5       PUSH    {R4-R7,LR}
0x11FEB8  03af       ADD     R7, SP, #0xC
0x11FEBA  84b0       SUB     SP, SP, #0x10
0x11FEBC  061c       MOVS    R6, R0
0x11FEBE  4068       LDR     R0, [R0,#4]; this
0x11FEC0  4cf046f8   BL      __ZN9Triniti2D10GameObject8GetSceneEv; Triniti2D::GameObject::GetScene(void)
0x11FEC4  55f0cff9   BL      __ZN9Triniti2D5Scene9GetCameraEv; Triniti2D::Scene::GetCamera(void)
0x11FEC8  011c       MOVS    R1, R0
0x11FECA  6846       MOV     R0, SP; this
0x11FECC  41f069fe   BL      __ZN9Triniti2D6Camera11GetViewportEv; Triniti2D::Camera::GetViewport(void)
0x11FED0  8423       MOVS    R3, #0x84
0x11FED2  f058       LDR     R0, [R6,R3]; this
0x11FED4  4cf036f8   BL      __ZN9Triniti2D10GameObject11GetPositionEv; Triniti2D::GameObject::GetPosition(void)
0x11FED8  0468       LDR     R4, [R0]
0x11FEDA  4568       LDR     R5, [R0,#4]
0x11FEDC  7068       LDR     R0, [R6,#4]; this
0x11FEDE  4cf031f8   BL      __ZN9Triniti2D10GameObject11GetPositionEv; Triniti2D::GameObject::GetPosition(void)
0x11FEE2  0168       LDR     R1, [R0]
0x11FEE4  4268       LDR     R2, [R0,#4]
0x11FEE6  201c       MOVS    R0, R4
0x11FEE8  c9f0b2ef   BLX     ___subsf3vfp
0x11FEEC  204b       LDR     R3, =0x7FFFFFFF
0x11FEEE  2149       LDR     R1, =0x42700000
0x11FEF0  1840       ANDS    R0, R3
```

