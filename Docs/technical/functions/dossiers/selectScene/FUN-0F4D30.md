# selectScene::InitCharacter

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0F4D30` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xF4D30` |
| RVA (EA - 0x1000) | `0xF3D30` |
| File offset | `0xF3D30` |
| Size | 486 code bytes in 1 chunk(s), 209 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene13InitCharacterEi` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::InitCharacter(int)` |
| Bytes SHA-256 | `11ae676ec19046d92b5404945439a7670ef432ecd4a181843441e6c5625c9144` |
| Direct code callers | 7 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UI_FLOW / PORTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.anim.player_base_tables` (PROVEN, NATIVE (decompiler)): DinoCap::GenComAnim(a2,a3=hero,a4)@0x188da clones global base animation tables per hero type (g_ca_pBase*/2pBase*/3pBase*/4pBase*; Hit/Move/Stand/Fire/Behit/House/Roll/Roll2 and second-layer p2* variants) into working tables t_ca_p*Data, then per equipped-weapon class (a2) clones them into g_ca_p<N>*Data (N=1..6 weapon holders) with SetKeyFrame values: Fire 3, Behit 7, Hit 10 (0xA), 9/0xB for special weapons; SetSpri...

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: [`selectScene::OnUIEvent`](../../../functions/dossiers/selectScene/FUN-10B93A.md)

Callees: [`CAObjCallBack::playAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00A064.md), [`CAObjCallBack::setAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00AEA0.md), `selPlayerClass::CanControlUpdata`, [`selPlayerClass::Stand`](../../../functions/dossiers/selPlayerClass/FUN-10E0D4.md), [`selPlayerClass::InitType`](../../../functions/dossiers/selPlayerClass/FUN-1102E0.md), `Triniti2D::GameObject::Enable`, `Triniti2D::GameObject::GetCallBack`, `Triniti2D::GameObject::SetPosition`, `___addsf3vfp`, `___dynamic_cast`, `___mulsf3vfp`

## Strings it references

- `0x209E78` "standAni"

## Named data it references

- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A0A8` __ZTIN9Triniti2D18GameObjectCallBackE_ptr
- `0x22A288` __ZTIN7DinoCap17playerNewAniClassE_ptr
- `0x22D338` `typeinfo for'DinoCap::playerNewAniClass
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22ABF8` __ZTIN7DinoCap14selPlayerClassE_ptr
- `0x22CFF0` `typeinfo for'DinoCap::selPlayerClass
- `0x22A0E8` __ZN7DinoCap10g_ground_yE_ptr
- `0x23B910` DinoCap::g_ground_y
- `0x22A3C0` __ZN7DinoCap16t_ca_2pStandDataE_ptr
- `0x23E348` DinoCap::t_ca_2pStandData

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xF4D30   f0b5       PUSH    {R4-R7,LR}
0xF4D32   5646       MOV     R6, R10
0xF4D34   4546       MOV     R5, R8
0xF4D36   60b4       PUSH    {R5,R6}
0xF4D38   05af       ADD     R7, SP, #0x14
0xF4D3A   89b0       SUB     SP, SP, #0x24
0xF4D3C   764b       LDR     R3, =(__ZN7DinoCap8g_iphoneE_ptr - 0xF4D46)
0xF4D3E   041c       MOVS    R4, R0
0xF4D40   0e1c       MOVS    R6, R1
0xF4D42   7b44       ADD     R3, PC; __ZN7DinoCap8g_iphoneE_ptr
0xF4D44   1b68       LDR     R3, [R3]; DinoCap::g_iphone ...
0xF4D46   1b78       LDRB    R3, [R3]; DinoCap::g_iphone
0xF4D48   002b       CMP     R3, #0
0xF4D4A   01d1       BNE     loc_F4D50
0xF4D4C   734b       LDR     R3, =0x40000000
0xF4D4E   00e0       B       loc_F4D52
0xF4D50   734b       LDR     R3, =0x3F800000
0xF4D52   a069       LDR     R0, [R4,#0x18]; this
0xF4D54   9846       MOV     R8, R3
0xF4D56   77f0fdf8   BL      __ZN9Triniti2D10GameObject11GetCallBackEv; Triniti2D::GameObject::GetCallBack(void)
0xF4D5A   0028       CMP     R0, #0
0xF4D5C   08d0       BEQ     loc_F4D70
0xF4D5E   7149       LDR     R1, =(__ZTIN9Triniti2D18GameObjectCallBackE_ptr - 0xF4D68)
0xF4D60   714a       LDR     R2, =(__ZTIN7DinoCap17playerNewAniClassE_ptr - 0xF4D6A)
```

