# playerClass::Attack

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0BF784` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xBF784` |
| RVA (EA - 0x1000) | `0xBE784` |
| File offset | `0xBE784` |
| Size | 1710 code bytes in 1 chunk(s), 718 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11playerClass6AttackEi` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerClass::Attack(int)` |
| Bytes SHA-256 | `fe88f20cdc1bbbce7ac5e482ec23f2c9c5f34e7eacf272d159ea465bbd7523b0` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | deep: curated note below, from the batch that established it |

## What it does

Gun hit scan: nearest target by head position (tou, com6 for skin 5), group weapons hit every candidate, armoured hits roll a 50 % metal sound (A_shot_tie).

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameBoss.cs:531`](../../../../../Assets/Scripts/GameBoss.cs#L531) (by name)
- [`Assets/Scripts/GameCombat.cs:720`](../../../../../Assets/Scripts/GameCombat.cs#L720) cites `0xBF784`
- [`Assets/Scripts/GameCombat.cs:730`](../../../../../Assets/Scripts/GameCombat.cs#L730) cites `0xBF92C`
- [`Assets/Scripts/GameCombat.cs:735`](../../../../../Assets/Scripts/GameCombat.cs#L735) cites `0xBF784`
- [`Assets/Scripts/GameCombat.cs:741`](../../../../../Assets/Scripts/GameCombat.cs#L741) cites `0xBFDAE`

## Callers and callees (direct code references)

Callers: [`playerClass::KaiQiang`](../../../functions/dossiers/playerClass/FUN-0C0336.md)

Callees: [`CAObjCallBack::getSpritePos`](../../../functions/dossiers/CAObjCallBack/FUN-00A4D0.md), [`dinosaurClass::BeAttacked`](../../../functions/dossiers/dinosaurClass/FUN-014918.md), [`PlaySound`](../../../functions/dossiers/DinoCap/FUN-01A448.md), `subWeaponClass::~subWeaponClass`, `weaponClass::~weaponClass`, [`bossN::GetBossAttackOffset`](../../../functions/dossiers/bossN/FUN-120018.md), [`bossN::CheckAttackState`](../../../functions/dossiers/bossN/FUN-120C82.md), [`bossN::CheckAttackDis`](../../../functions/dossiers/bossN/FUN-120E38.md), [`bossN::BeAttacked`](../../../functions/dossiers/bossN/FUN-123B70.md), `Triniti2D::GameObject::GetPosition`, `Triniti2D::GameObject::GetScene`, `Triniti2D::GameObject::GetCallBack`, `Triniti2D::GameObject::Name`, `Triniti2D::Scene::GetCallBack`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::substr`, `std::string::compare`, `std::string::assign`, `std::string::~string`, `___addsf3vfp`, `___dynamic_cast`, `___fixsfsivfp`, `___floatsisfvfp`, `___gtsf2vfp`, `___ltsf2vfp`, `___mulsf3vfp`, `___subsf3vfp`, `_rand`

## Strings it references

- `0x201F10` "dino"
- `0x2013B4` "com6"
- `0x2014B0` "tou"
- `0x201F18` "boss"
- `0x205004` "A_shot_tie"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x22700A` GCC_except_table27_1
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A24C` __ZN7DinoCap15g_player_vectorE_ptr
- `0x2404AC` DinoCap::g_player_vector
- `0x22A2B8` __ZN7DinoCap13g_player_typeE_ptr
- `0x23B86C` DinoCap::g_player_type
- `0x22A0C4` __ZN7DinoCap16g_manager_WeaponE_ptr
- `0x2404B8` DinoCap::g_manager_Weapon
- `0x22A2F4` __ZN7DinoCap11weaponClassC2ERKS0__ptr
- `0x22A0C0` __ZTIN9Triniti2D13SceneCallBackE_ptr
- `0x22A0C8` __ZTIN7DinoCap9mainSceneE_ptr
- `0x22C868` `typeinfo for'DinoCap::mainScene
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22A0A0` __ZNKSt6vectorIPN9Triniti2D10GameObjectESaIS2_EE4sizeEv_ptr
- `0x22A0A8` __ZTIN9Triniti2D18GameObjectCallBackE_ptr
- `0x22A2F8` __ZTIN7DinoCap13dinosaurClassE_ptr
- `0x22C73C` `typeinfo for'DinoCap::dinosaurClass
- `0x22A2FC` __ZTIN7DinoCap5bossNE_ptr
- `0x22D290` `typeinfo for'DinoCap::bossN

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xBF784   f0b5       PUSH    {R4-R7,LR}
0xBF786   5e46       MOV     R6, R11
0xBF788   5546       MOV     R5, R10
0xBF78A   4446       MOV     R4, R8
0xBF78C   70b4       PUSH    {R4-R6}
0xBF78E   28f1a8eb   BLX     ___save_vfp_d8_d15_regs
0xBF792   16af       ADD     R7, SP, #arg_38
0xBF794   bcb0       SUB     SP, SP, #0xF0
0xBF796   d74b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xBF7A0)
0xBF798   0390       STR     R0, [SP,#0xF0+var_E4]
0xBF79A   0122       MOVS    R2, #1
0xBF79C   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xBF79E   6846       MOV     R0, SP
0xBF7A0   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xBF7A2   1e90       STR     R0, [SP,#0xF0+var_78]
0xBF7A4   14a8       ADD     R0, SP, #0xF0+fctx; lpfctx
0xBF7A6   1a93       STR     R3, [SP,#0xF0+fctx.personality]
0xBF7A8   d34b       LDR     R3, =(GCC_except_table27_1 - 0xBF7B2)
0xBF7AA   0291       STR     R1, [SP,#0xF0+var_E8]
0xBF7AC   1c97       STR     R7, [SP,#0xF0+fctx.jbuf]
0xBF7AE   7b44       ADD     R3, PC; GCC_except_table27_1
0xBF7B0   1b93       STR     R3, [SP,#0xF0+fctx.lsda]
0xBF7B2   d24b       LDR     R3, =(loc_BFD6E - 0xBF7B8)
0xBF7B4   7b44       ADD     R3, PC; loc_BFD6E
```

**Around `0xBF92C` (cited by the Unity code)**

```
0xBF920   8549       LDR     R1, =(aCom6 - 0xBF92A); "com6"
0xBF922   0623       MOVS    R3, #6
0xBF924   101c       MOVS    R0, R2
0xBF926   7944       ADD     R1, PC; "com6"
0xBF928   1593       STR     R3, [SP,#0xF0+fctx.call_site]
0xBF92A   35aa       ADD     R2, SP, #0xF0+var_1C
0xBF92C   4af7d0fd   BL      __ZN7DinoCap13CAObjCallBack12getSpritePosEPKcRN9Triniti2D7Vector2E; DinoCap::CAObjCallBack::getSpritePos(char const*,Triniti2D::Vector2 &)   <==
0xBF930   0023       MOVS    R3, #0
0xBF932   1093       STR     R3, [SP,#0xF0+var_B0]
0xBF934   36e0       B       loc_BF9A4
0xBF936   8149       LDR     R1, =(aTou - 0xBF940); "tou"
0xBF938   0623       MOVS    R3, #6
0xBF93A   1593       STR     R3, [SP,#0xF0+fctx.call_site]
```

**Around `0xBFDAE` (cited by the Unity code)**

```
0xBFDA0   1593       STR     R3, [SP,#0xF0+fctx.call_site]
0xBFDA2   38a8       ADD     R0, SP, #0xF0+var_10; this
0xBFDA4   29f104ef   BLX     __ZNSsD2Ev; std::string::~string()
0xBFDA8   0c98       LDR     R0, [SP,#0xF0+var_C0]
0xBFDAA   0028       CMP     R0, #0
0xBFDAC   1cd0       BEQ     loc_BFDE8
0xBFDAE   2af1ccea   BLX     _rand   <==
0xBFDB2   29f1dcef   BLX     ___floatsisfvfp
0xBFDB6   3349       LDR     R1, =0x30000000
0xBFDB8   2af132e8   BLX     ___mulsf3vfp
0xBFDBC   3249       LDR     R1, =0x42C80000
0xBFDBE   2af130e8   BLX     ___mulsf3vfp
0xBFDC2   29f1b0ef   BLX     ___fixsfsivfp
```

