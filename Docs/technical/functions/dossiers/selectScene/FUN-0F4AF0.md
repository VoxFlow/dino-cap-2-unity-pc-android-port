# selectScene::InitNewCharacter

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0F4AF0` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xF4AF0` |
| RVA (EA - 0x1000) | `0xF3AF0` |
| File offset | `0xF3AF0` |
| Size | 494 code bytes in 1 chunk(s), 219 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene16InitNewCharacterEi` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::InitNewCharacter(int)` |
| Bytes SHA-256 | `e73b4080276698775e45471a089d4294e1c019da61a0c77e13f04dbe4b3b3062` |
| Direct code callers | 2 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UI_FLOW / PORTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.hero.preview_component_scale.r12` (PROVEN, NATIVE): CompAnimObj scales local x/y and scaleX/scaleY by callback+228 before world position; ARM0x118c3a..0x118c86. InitNewCharacter writes1.5 at0xf4cbc. Default scale1 retained for existing loading/jigsaw callers.

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: [`selectScene::OnUIEvent`](../../../functions/dossiers/selectScene/FUN-10B93A.md)

Callees: `subWeaponClass::subWeaponClass`, `subWeaponClass::~subWeaponClass`, `selPlayerClass::CanControlUpdata`, [`playerNewAniClass::Stand`](../../../functions/dossiers/playerNewAniClass/FUN-124F82.md), [`playerNewAniClass::InitType`](../../../functions/dossiers/playerNewAniClass/FUN-12B0AC.md), `Triniti2D::GameObject::Enable`, `Triniti2D::GameObject::GetCallBack`, `Triniti2D::GameObject::SetPosition`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `___addsf3vfp`, `___dynamic_cast`, `___mulsf3vfp`

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x227F58` GCC_except_table15_5
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A24C` __ZN7DinoCap15g_player_vectorE_ptr
- `0x2404AC` DinoCap::g_player_vector
- `0x22A0A8` __ZTIN9Triniti2D18GameObjectCallBackE_ptr
- `0x22ABF8` __ZTIN7DinoCap14selPlayerClassE_ptr
- `0x22CFF0` `typeinfo for'DinoCap::selPlayerClass
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22A288` __ZTIN7DinoCap17playerNewAniClassE_ptr
- `0x22D338` `typeinfo for'DinoCap::playerNewAniClass
- `0x22A0E8` __ZN7DinoCap10g_ground_yE_ptr
- `0x23B910` DinoCap::g_ground_y

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xF4AF0   f0b5       PUSH    {R4-R7,LR}
0xF4AF2   5e46       MOV     R6, R11
0xF4AF4   5546       MOV     R5, R10
0xF4AF6   4446       MOV     R4, R8
0xF4AF8   70b4       PUSH    {R4-R6}
0xF4AFA   f3f0f2e9   BLX     ___save_vfp_d8_d15_regs
0xF4AFE   16af       ADD     R7, SP, #arg_38
0xF4B00   a1b0       SUB     SP, SP, #0x84
0xF4B02   774b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xF4B0C)
0xF4B04   0390       STR     R0, [SP,#0x84+var_78]
0xF4B06   0122       MOVS    R2, #1
0xF4B08   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xF4B0A   6846       MOV     R0, SP
0xF4B0C   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xF4B0E   1390       STR     R0, [SP,#0x84+var_38]
0xF4B10   09a8       ADD     R0, SP, #0x84+fctx; lpfctx
0xF4B12   0f93       STR     R3, [SP,#0x84+fctx.personality]
0xF4B14   734b       LDR     R3, =(GCC_except_table15_5 - 0xF4B1E)
0xF4B16   0291       STR     R1, [SP,#0x84+var_7C]
0xF4B18   1197       STR     R7, [SP,#0x84+fctx.jbuf]
0xF4B1A   7b44       ADD     R3, PC; GCC_except_table15_5
0xF4B1C   1093       STR     R3, [SP,#0x84+fctx.lsda]
0xF4B1E   724b       LDR     R3, =(loc_F4C8E - 0xF4B24)
0xF4B20   7b44       ADD     R3, PC; loc_F4C8E
```

