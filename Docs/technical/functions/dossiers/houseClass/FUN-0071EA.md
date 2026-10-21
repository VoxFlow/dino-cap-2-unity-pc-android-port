# houseClass::OnTimeout

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0071EA` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x71EA` |
| RVA (EA - 0x1000) | `0x61EA` |
| File offset | `0x61EA` |
| Size | 202 code bytes in 1 chunk(s), 89 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap10houseClass9OnTimeoutEiPv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::houseClass::OnTimeout(int,void *)` |
| Bytes SHA-256 | `52e3af1180c5057e52592e667537fca0170de1c048324152bb976072d30f55c6` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameHouse.cs:10`](../../../../../Assets/Scripts/GameHouse.cs#L10) (by name)

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: [`itemClass::Start`](../../../functions/dossiers/itemClass/FUN-0097F8.md), [`CAObjCallBack::getSpritePos`](../../../functions/dossiers/CAObjCallBack/FUN-00A4D0.md), [`PlaySound`](../../../functions/dossiers/DinoCap/FUN-01A448.md), [`mainScene::UpdateBullet`](../../../functions/dossiers/mainScene/FUN-09546C.md), `weaponManager::GetBullet`, `Triniti2D::GameObject::GetScene`, `Triniti2D::GameObject::GetCallBack`, `Triniti2D::Scene::GetCallBack`, `std::string::assign`, `___dynamic_cast`, `_strcmp`

## Strings it references

- `0x200D54` "close"
- `0x200D7C` "_a"
- `0x200D6C` "di"
- `0x200D80` "Audio_itemAudio"

## Named data it references

- `0x22A0C4` __ZN7DinoCap16g_manager_WeaponE_ptr
- `0x2404B8` DinoCap::g_manager_Weapon
- `0x22A0C0` __ZTIN9Triniti2D13SceneCallBackE_ptr
- `0x22A0C8` __ZTIN7DinoCap9mainSceneE_ptr
- `0x22C868` `typeinfo for'DinoCap::mainScene
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22A0A8` __ZTIN9Triniti2D18GameObjectCallBackE_ptr
- `0x22A0D4` __ZTIN7DinoCap9itemClassE_ptr
- `0x22C65C` `typeinfo for'DinoCap::itemClass

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x71EA    b0b5       PUSH    {R4,R5,R7,LR}
0x71EC    02af       ADD     R7, SP, #8
0x71EE    84b0       SUB     SP, SP, #0x10
0x71F0    3049       LDR     R1, =(aClose - 0x71FA); "close"
0x71F2    051c       MOVS    R5, R0
0x71F4    101c       MOVS    R0, R2; __s1
0x71F6    7944       ADD     R1, PC; "close"
0x71F8    e3f1eee8   BLX     _strcmp
0x71FC    0028       CMP     R0, #0
0x71FE    57d1       BNE     loc_72B0
0x7200    2d49       LDR     R1, =(aA - 0x720C); "_a"
0x7202    8823       MOVS    R3, #0x88
0x7204    281c       MOVS    R0, R5
0x7206    0222       MOVS    R2, #2; unsigned int
0x7208    7944       ADD     R1, PC; "_a"
0x720A    6c30       ADDS    R0, #0x6C ; 'l'; this
0x720C    ea50       STR     R2, [R5,R3]
0x720E    e2f19aec   BLX     __ZNSs6assignEPKcm; std::string::assign(char const*,ulong)
0x7212    2a48       LDR     R0, =(__ZN7DinoCap16g_manager_WeaponE_ptr - 0x721C)
0x7214    8423       MOVS    R3, #0x84
0x7216    0122       MOVS    R2, #1
0x7218    7844       ADD     R0, PC; __ZN7DinoCap16g_manager_WeaponE_ptr
0x721A    ea54       STRB    R2, [R5,R3]
0x721C    0068       LDR     R0, [R0]; this
```

