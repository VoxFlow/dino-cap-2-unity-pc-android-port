# mainScene::AddYezi

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0A108A` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xA108A` |
| RVA (EA - 0x1000) | `0xA008A` |
| File offset | `0xA008A` |
| Size | 498 code bytes in 1 chunk(s), 217 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9mainScene7AddYeziEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::mainScene::AddYezi(void)` |
| Bytes SHA-256 | `bc2bf500d639c892fac2452582b787c1be74d963c60a17a505ac0d5a019f3aa0` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | GAMEPLAY / PORTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: twin of lifeModeScene::AddYezi 0xe1040 (same plausible literals and calls); GameWeather.AddYezi

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: [`CAObjCallBack::playAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00A064.md), [`CAObjCallBack::setAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00AEA0.md), [`mainScene::GetEffect`](../../../functions/dossiers/mainScene/FUN-0956B6.md), `Triniti2D::Camera::GetViewport`, `Triniti2D::GameObject::Enable`, `Triniti2D::GameObject::GetCallBack`, `Triniti2D::GameObject::SetSize`, `Triniti2D::GameObject::SetPosition`, `Triniti2D::Scene::GetCallBack`, `Triniti2D::Scene::GetCamera`, `Triniti2D::Scene::SetTimer`, `___switch8`, `___addsf3vfp`, `___dynamic_cast`, `___floatsisfvfp`, `___mulsf3vfp`

## Strings it references

- `0x20799C` "shandianAnimation"
- `0x2079B0` "Yezi"

## Named data it references

- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A0C0` __ZTIN9Triniti2D13SceneCallBackE_ptr
- `0x22C868` `typeinfo for'DinoCap::mainScene
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22A2AC` __ZN9Triniti2D11RandomRangeEii_ptr
- `0x22A0A8` __ZTIN9Triniti2D18GameObjectCallBackE_ptr
- `0x22A260` __ZTIN7DinoCap11effectClassE_ptr
- `0x22C814` `typeinfo for'DinoCap::effectClass
- `0x22A3E8` __ZN7DinoCap13g_ca_feng1EffE_ptr
- `0x23FD28` DinoCap::g_ca_feng1Eff
- `0x22A4CC` __ZN7DinoCap13g_ca_feng2EffE_ptr
- `0x23FD44` DinoCap::g_ca_feng2Eff
- `0x22A510` __ZN7DinoCap13g_ca_feng3EffE_ptr
- `0x23FD60` DinoCap::g_ca_feng3Eff
- `0x22A404` __ZN7DinoCap13g_ca_feng4EffE_ptr
- `0x23FD7C` DinoCap::g_ca_feng4Eff

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xA108A   f0b5       PUSH    {R4-R7,LR}
0xA108C   5e46       MOV     R6, R11
0xA108E   5546       MOV     R5, R10
0xA1090   4446       MOV     R4, R8
0xA1092   70b4       PUSH    {R4-R6}
0xA1094   06af       ADD     R7, SP, #0x18
0xA1096   8db0       SUB     SP, SP, #0x34
0xA1098   784b       LDR     R3, =(__ZN7DinoCap8g_iphoneE_ptr - 0xA10A0)
0xA109A   8346       MOV     R11, R0
0xA109C   7b44       ADD     R3, PC; __ZN7DinoCap8g_iphoneE_ptr
0xA109E   1b68       LDR     R3, [R3]; DinoCap::g_iphone ...
0xA10A0   1b78       LDRB    R3, [R3]; DinoCap::g_iphone
0xA10A2   002b       CMP     R3, #0
0xA10A4   01d1       BNE     loc_A10AA
0xA10A6   764b       LDR     R3, =0x40000000
0xA10A8   00e0       B       loc_A10AC
0xA10AA   764b       LDR     R3, flt_A1284
0xA10AC   0693       STR     R3, [SP,#0x34+var_1C]
0xA10AE   5b46       MOV     R3, R11
0xA10B0   5868       LDR     R0, [R3,#4]; this
0xA10B2   d4f0bef8   BL      __ZN9Triniti2D5Scene11GetCallBackEv; Triniti2D::Scene::GetCallBack(void)
0xA10B6   0028       CMP     R0, #0
0xA10B8   02d1       BNE     loc_A10C0
0xA10BA   0023       MOVS    R3, #0
```

