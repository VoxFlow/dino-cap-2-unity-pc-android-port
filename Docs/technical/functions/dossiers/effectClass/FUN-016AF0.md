# effectClass::OnAnimationEnd

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-016AF0` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x16AF0` |
| RVA (EA - 0x1000) | `0x15AF0` |
| File offset | `0x15AF0` |
| Size | 1222 code bytes in 1 chunk(s), 488 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11effectClass14OnAnimationEndEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::effectClass::OnAnimationEnd(void)` |
| Bytes SHA-256 | `4500e4d5e765438b436e733955203bdd0cc6428be9bdb62c41e66b955678f6a5` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameBeam.cs:8`](../../../../../Assets/Scripts/GameBeam.cs#L8) cites `0x16AF0`
- [`Assets/Scripts/GameBeam.cs:247`](../../../../../Assets/Scripts/GameBeam.cs#L247) cites `0x16AF0`
- [`Assets/Scripts/GameBeam.cs:249`](../../../../../Assets/Scripts/GameBeam.cs#L249) cites `0x16DD6`
- [`Assets/Scripts/GameBeam.cs:250`](../../../../../Assets/Scripts/GameBeam.cs#L250) cites `0x16B84`
- [`Assets/Scripts/GameBeam.cs:264`](../../../../../Assets/Scripts/GameBeam.cs#L264) cites `0x16AF0`
- [`Assets/Scripts/GameLevelUp.cs:47`](../../../../../Assets/Scripts/GameLevelUp.cs#L47) (by name)

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: [`CAObjCallBack::playAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00A064.md), `CAObjCallBack::setVisible`, [`dinosaurClass::BeDamagedOver`](../../../functions/dossiers/dinosaurClass/FUN-010954.md), [`effectClass::Disappear`](../../../functions/dossiers/effectClass/FUN-015EEA.md), `Triniti2D::GameObject::Enable`, `Triniti2D::GameObject::GetScene`, `Triniti2D::GameObject::GetCallBack`, `Triniti2D::GameObject::Name`, `Triniti2D::Scene::GetCallBack`, `Triniti2D::Scene::Unmount`, `___switch32`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::substr`, `std::string::compare`, `std::string::assign`, `std::string::~string`, `___dynamic_cast`

## Strings it references

- `0x201F10` "dino"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x223C65` GCC_except_table6_2
- `0x22A0C0` __ZTIN9Triniti2D13SceneCallBackE_ptr
- `0x22A0C8` __ZTIN7DinoCap9mainSceneE_ptr
- `0x22C868` `typeinfo for'DinoCap::mainScene
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22A0A0` __ZNKSt6vectorIPN9Triniti2D10GameObjectESaIS2_EE4sizeEv_ptr
- `0x22A0A8` __ZTIN9Triniti2D18GameObjectCallBackE_ptr
- `0x22A2F8` __ZTIN7DinoCap13dinosaurClassE_ptr
- `0x22C73C` `typeinfo for'DinoCap::dinosaurClass
- `0x22A2F0` __ZN7DinoCap12g_b_FireDownE_ptr
- `0x23B8C3` DinoCap::g_b_FireDown

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x16AF0   f0b5       PUSH    {R4-R7,LR}
0x16AF2   5e46       MOV     R6, R11
0x16AF4   5546       MOV     R5, R10
0x16AF6   4446       MOV     R4, R8
0x16AF8   70b4       PUSH    {R4-R6}
0x16AFA   d1f1f2e9   BLX     ___save_vfp_d8_d15_regs
0x16AFE   16af       ADD     R7, SP, #arg_38
0x16B00   a0b0       SUB     SP, SP, #0x80
0x16B02   de4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x16B0C)
0x16B04   0122       MOVS    R2, #1
0x16B06   0590       STR     R0, [SP,#0x80+var_6C]
0x16B08   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x16B0A   6946       MOV     R1, SP
0x16B0C   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x16B0E   0fa8       ADD     R0, SP, #0x80+fctx; lpfctx
0x16B10   1797       STR     R7, [SP,#0x80+fctx.jbuf]
0x16B12   1593       STR     R3, [SP,#0x80+fctx.personality]
0x16B14   da4b       LDR     R3, =(GCC_except_table6_2 - 0x16B1C)
0x16B16   1991       STR     R1, [SP,#0x80+var_1C]
0x16B18   7b44       ADD     R3, PC; GCC_except_table6_2
0x16B1A   1693       STR     R3, [SP,#0x80+fctx.lsda]
0x16B1C   d94b       LDR     R3, =(loc_16ED0 - 0x16B22)
0x16B1E   7b44       ADD     R3, PC; loc_16ED0
0x16B20   1343       ORRS    R3, R2
```

**Around `0x16B84` (cited by the Unity code)**

```
0x16B76   0021       MOVS    R1, #0; bool
0x16B78   f3f760fb   BL      __ZN7DinoCap13CAObjCallBack10setVisibleEb; DinoCap::CAObjCallBack::setVisible(bool)
0x16B7C   10e2       B       loc_16FA0; jumptable 00016B34 cases 1,5
0x16B7E   059a       LDR     R2, [SP,#0x80+var_6C]; jumptable 00016B34 case 2
0x16B80   536c       LDR     R3, [R2,#0x44]
0x16B82   062b       CMP     R3, #6
0x16B84   07d1       BNE     loc_16B96   <==
0x16B86   0093       STR     R3, [SP,#0x80+var_80]
0x16B88   bf4b       LDR     R3, =0x3CCCCCCD
0x16B8A   0121       MOVS    R1, #1
0x16B8C   4942       NEGS    R1, R1
0x16B8E   101c       MOVS    R0, R2
0x16B90   0422       MOVS    R2, #4
```

**Around `0x16DD6` (cited by the Unity code)**

```
0x16DCA   8342       CMP     R3, R0
0x16DCC   acd3       BCC     loc_16D28
0x16DCE   d0e0       B       loc_16F72
0x16DD0   059a       LDR     R2, [SP,#0x80+var_6C]; jumptable 00016B34 case 3
0x16DD2   536c       LDR     R3, [R2,#0x44]
0x16DD4   052b       CMP     R3, #5
0x16DD6   00d1       BNE     loc_16DDA   <==
0x16DD8   e2e0       B       loc_16FA0; jumptable 00016B34 cases 1,5
0x16DDA   0123       MOVS    R3, #1
0x16DDC   5068       LDR     R0, [R2,#4]; this
0x16DDE   5b42       NEGS    R3, R3
0x16DE0   1093       STR     R3, [SP,#0x80+fctx.call_site]
0x16DE2   55f1b5f8   BL      __ZN9Triniti2D10GameObject8GetSceneEv; Triniti2D::GameObject::GetScene(void)
```

