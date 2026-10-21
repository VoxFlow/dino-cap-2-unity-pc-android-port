# playerClass::JinAttack

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0BDFB8` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xBDFB8` |
| RVA (EA - 0x1000) | `0xBCFB8` |
| File offset | `0xBCFB8` |
| Size | 1628 code bytes in 1 chunk(s), 684 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11playerClass9JinAttackEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerClass::JinAttack(void)` |
| Bytes SHA-256 | `a05cb58fc28b1fecaf26068e407bc5a172a710531cd49e4493002affa98d26d1` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameControls.cs:10`](../../../../../Assets/Scripts/GameControls.cs#L10) cites `0xBDFB8`
- [`Assets/Scripts/GameControls.cs:235`](../../../../../Assets/Scripts/GameControls.cs#L235) cites `0xBDFB8`
- [`Assets/Scripts/GameControls.cs:236`](../../../../../Assets/Scripts/GameControls.cs#L236) cites `0xBE15A`
- [`Assets/Scripts/GameControls.cs:244`](../../../../../Assets/Scripts/GameControls.cs#L244) cites `0xBDFB8`
- [`Assets/Scripts/GameControls.cs:245`](../../../../../Assets/Scripts/GameControls.cs#L245) cites `0xBE1BC`
- [`Assets/Scripts/GameControls.cs:246`](../../../../../Assets/Scripts/GameControls.cs#L246) cites `0xBE25A`
- [`Assets/Scripts/GameControls.cs:249`](../../../../../Assets/Scripts/GameControls.cs#L249) cites `0xBE2AA`
- [`Assets/Scripts/GameControls.cs:260`](../../../../../Assets/Scripts/GameControls.cs#L260) cites `0xBE204`
- [`Assets/Scripts/GameControls.cs:263`](../../../../../Assets/Scripts/GameControls.cs#L263) cites `0xBE25A`
- [`Assets/Scripts/GameControls.cs:264`](../../../../../Assets/Scripts/GameControls.cs#L264) cites `0xBE2A0`
- [`Assets/Scripts/GameControls.cs:270`](../../../../../Assets/Scripts/GameControls.cs#L270) cites `0xBE234`
- [`Assets/Scripts/GameControls.cs:288`](../../../../../Assets/Scripts/GameControls.cs#L288) cites `0xBE542`

## Callers and callees (direct code references)

Callers: [`playerClass::OnKeyFrame`](../../../functions/dossiers/playerClass/FUN-0BF406.md)

Callees: [`CAObjCallBack::getSpritePos`](../../../functions/dossiers/CAObjCallBack/FUN-00A4D0.md), [`dinosaurClass::BeJinAttacked`](../../../functions/dossiers/dinosaurClass/FUN-01442A.md), [`PlaySound`](../../../functions/dossiers/DinoCap/FUN-01A448.md), `subWeaponClass::~subWeaponClass`, `weaponClass::~weaponClass`, [`bossN::CheckAttackDis`](../../../functions/dossiers/bossN/FUN-120E38.md), [`bossN::BeAttacked`](../../../functions/dossiers/bossN/FUN-123B70.md), `Triniti2D::GameObject::GetPosition`, `Triniti2D::GameObject::GetScene`, `Triniti2D::GameObject::GetCallBack`, `Triniti2D::GameObject::Name`, `Triniti2D::Scene::GetCallBack`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::substr`, `std::string::compare`, `std::string::assign`, `std::string::~string`, `___dynamic_cast`, `___gtsf2vfp`, `___mulsf3vfp`, `___subsf3vfp`

## Strings it references

- `0x201F10` "dino"
- `0x2013B4` "com6"
- `0x2014B0` "tou"
- `0x201F18` "boss"
- `0x204FB0` "A_hit_tie_mugun"
- `0x204878` "Attack_GAudio"
- `0x20497C` "Audio_G4Audio"
- `0x204F7C` "A_hit_tie_guangjian"
- `0x2055B8` "p2hitAudio"
- `0x204FDC` "A_hit_tie_tie"
- `0x2055DC` "p5hitAudio"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x226FCA` GCC_except_table21_2
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
0xBDFB8   f0b5       PUSH    {R4-R7,LR}
0xBDFBA   5e46       MOV     R6, R11
0xBDFBC   5546       MOV     R5, R10
0xBDFBE   4446       MOV     R4, R8
0xBDFC0   70b4       PUSH    {R4-R6}
0xBDFC2   29f18eef   BLX     ___save_vfp_d8_d15_regs
0xBDFC6   16af       ADD     R7, SP, #arg_38
0xBDFC8   bab0       SUB     SP, SP, #0xE8
0xBDFCA   da4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xBDFD4)
0xBDFCC   0290       STR     R0, [SP,#0xE8+var_E0]
0xBDFCE   0122       MOVS    R2, #1
0xBDFD0   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xBDFD2   6846       MOV     R0, SP
0xBDFD4   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xBDFD6   1c90       STR     R0, [SP,#0xE8+var_78]
0xBDFD8   12a8       ADD     R0, SP, #0xE8+fctx; lpfctx
0xBDFDA   1893       STR     R3, [SP,#0xE8+fctx.personality]
0xBDFDC   d64b       LDR     R3, =(GCC_except_table21_2 - 0xBDFE4)
0xBDFDE   1a97       STR     R7, [SP,#0xE8+fctx.jbuf]
0xBDFE0   7b44       ADD     R3, PC; GCC_except_table21_2
0xBDFE2   1993       STR     R3, [SP,#0xE8+fctx.lsda]
0xBDFE4   d54b       LDR     R3, =(loc_BE502 - 0xBDFEA)
0xBDFE6   7b44       ADD     R3, PC; loc_BE502
0xBDFE8   1343       ORRS    R3, R2
```

**Around `0xBE15A` (cited by the Unity code)**

```
0xBE14E   09d1       BNE     loc_BE164
0xBE150   8949       LDR     R1, =(aCom6 - 0xBE15A); "com6"
0xBE152   0623       MOVS    R3, #6
0xBE154   1393       STR     R3, [SP,#0xE8+fctx.call_site]
0xBE156   7944       ADD     R1, PC; "com6"
0xBE158   33aa       ADD     R2, SP, #0xE8+var_1C
0xBE15A   4cf7b9f9   BL      __ZN7DinoCap13CAObjCallBack12getSpritePosEPKcRN9Triniti2D7Vector2E; DinoCap::CAObjCallBack::getSpritePos(char const*,Triniti2D::Vector2 &)   <==
0xBE15E   0021       MOVS    R1, #0
0xBE160   0f91       STR     R1, [SP,#0xE8+var_AC]
0xBE162   33e0       B       loc_BE1CC
0xBE164   8549       LDR     R1, =(aTou - 0xBE16E); "tou"
0xBE166   0623       MOVS    R3, #6
0xBE168   1393       STR     R3, [SP,#0xE8+fctx.call_site]
```

**Around `0xBE1BC` (cited by the Unity code)**

```
0xBE1AE   1268       LDR     R2, [R2]; lpdtype
0xBE1B0   2bf194ed   BLX     ___dynamic_cast
0xBE1B4   0623       MOVS    R3, #6
0xBE1B6   0f90       STR     R0, [SP,#0xE8+var_AC]
0xBE1B8   1393       STR     R3, [SP,#0xE8+fctx.call_site]
0xBE1BA   0d98       LDR     R0, [SP,#0xE8+var_B4]; this
0xBE1BC   adf0c2fe   BL      __ZN9Triniti2D10GameObject11GetPositionEv; Triniti2D::GameObject::GetPosition(void)   <==
0xBE1C0   4168       LDR     R1, [R0,#4]
0xBE1C2   0068       LDR     R0, [R0]
0xBE1C4   3390       STR     R0, [SP,#0xE8+var_1C]
0xBE1C6   3491       STR     R1, [SP,#0xE8+var_18]
0xBE1C8   0021       MOVS    R1, #0
0xBE1CA   0e91       STR     R1, [SP,#0xE8+var_B0]
```

**Around `0xBE204` (cited by the Unity code)**

```
0xBE1F6   00d0       BEQ     loc_BE1FA
0xBE1F8   f3e1       B       loc_BE5E2
0xBE1FA   0f98       LDR     R0, [SP,#0xE8+var_AC]; this
0xBE1FC   1fa9       ADD     R1, SP, #0xE8+var_6C; DinoCap::weaponClass *
0xBE1FE   62f01bfe   BL      __ZN7DinoCap5bossN14CheckAttackDisEPNS_11weaponClassE; DinoCap::bossN::CheckAttackDis(DinoCap::weaponClass *)
0xBE202   0028       CMP     R0, #0
0xBE204   00d1       BNE     loc_BE208   <==
0xBE206   ece1       B       loc_BE5E2
0xBE208   614b       LDR     R3, =0x41200000
0xBE20A   1093       STR     R3, [SP,#0xE8+var_A8]
0xBE20C   0798       LDR     R0, [SP,#0xE8+var_CC]
0xBE20E   1099       LDR     R1, [SP,#0xE8+var_A8]
0xBE210   2bf1caed   BLX     ___gtsf2vfp
```

**Around `0xBE234` (cited by the Unity code)**

```
0xBE228   0028       CMP     R0, #0
0xBE22A   08d1       BNE     loc_BE23E
0xBE22C   0e9d       LDR     R5, [SP,#0xE8+var_B0]
0xBE22E   b423       MOVS    R3, #0xB4
0xBE230   eb58       LDR     R3, [R5,R3]
0xBE232   032b       CMP     R3, #3
0xBE234   00d1       BNE     loc_BE238   <==
0xBE236   f4e0       B       loc_BE422
0xBE238   ac23       MOVS    R3, #0xAC
0xBE23A   eb5c       LDRB    R3, [R5,R3]
0xBE23C   12e0       B       loc_BE264
0xBE23E   5649       LDR     R1, =(aBoss - 0xBE248); "boss"
0xBE240   0620       MOVS    R0, #6
```

**Around `0xBE25A` (cited by the Unity code)**

```
0xBE24C   0028       CMP     R0, #0
0xBE24E   0cd1       BNE     loc_BE26A
0xBE250   0f99       LDR     R1, [SP,#0xE8+var_AC]
0xBE252   e4235b00   MOVS    R3, #0x1C8
0xBE256   cb58       LDR     R3, [R1,R3]
0xBE258   042b       CMP     R3, #4
0xBE25A   00d1       BNE     loc_BE25E   <==
0xBE25C   e1e0       B       loc_BE422
0xBE25E   e6235b00   MOVS    R3, #0x1CC
0xBE262   cb58       LDR     R3, [R1,R3]
0xBE264   002b       CMP     R3, #0
0xBE266   00d1       BNE     loc_BE26A
0xBE268   dbe0       B       loc_BE422
```

**Around `0xBE2A0` (cited by the Unity code)**

```
0xBE294   00d1       BNE     loc_BE298
0xBE296   c4e0       B       loc_BE422
0xBE298   0623       MOVS    R3, #6
0xBE29A   1393       STR     R3, [SP,#0xE8+fctx.call_site]
0xBE29C   281c       MOVS    R0, R5; this
0xBE29E   1fa9       ADD     R1, SP, #0xE8+var_6C; DinoCap::weaponClass *
0xBE2A0   65f066fc   BL      __ZN7DinoCap5bossN10BeAttackedEPNS_11weaponClassE; DinoCap::bossN::BeAttacked(DinoCap::weaponClass *)   <==
0xBE2A4   bde0       B       loc_BE422
0xBE2A6   0998       LDR     R0, [SP,#0xE8+var_C4]
0xBE2A8   0028       CMP     R0, #0
0xBE2AA   02d1       BNE     loc_BE2B2
0xBE2AC   0d99       LDR     R1, [SP,#0xE8+var_B4]
0xBE2AE   0991       STR     R1, [SP,#0xE8+var_C4]
```

**Around `0xBE2AA` (cited by the Unity code)**

```
0xBE29C   281c       MOVS    R0, R5; this
0xBE29E   1fa9       ADD     R1, SP, #0xE8+var_6C; DinoCap::weaponClass *
0xBE2A0   65f066fc   BL      __ZN7DinoCap5bossN10BeAttackedEPNS_11weaponClassE; DinoCap::bossN::BeAttacked(DinoCap::weaponClass *)
0xBE2A4   bde0       B       loc_BE422
0xBE2A6   0998       LDR     R0, [SP,#0xE8+var_C4]
0xBE2A8   0028       CMP     R0, #0
0xBE2AA   02d1       BNE     loc_BE2B2   <==
0xBE2AC   0d99       LDR     R1, [SP,#0xE8+var_B4]
0xBE2AE   0991       STR     R1, [SP,#0xE8+var_C4]
0xBE2B0   b7e0       B       loc_BE422
0xBE2B2   2d4b       LDR     R3, =0
0xBE2B4   37a8       ADD     R0, SP, #0xE8+var_C; this
0xBE2B6   0999       LDR     R1, [SP,#0xE8+var_C4]
```

**Around `0xBE542` (cited by the Unity code)**

```
0xBE534   0723       MOVS    R3, #7
0xBE536   1393       STR     R3, [SP,#0xE8+fctx.call_site]
0xBE538   36a8       ADD     R0, SP, #0xE8+var_10; this
0xBE53A   2bf13aeb   BLX     __ZNSsD2Ev; std::string::~string()
0xBE53E   2b9b       LDR     R3, [SP,#0xE8+var_3C]
0xBE540   002b       CMP     R3, #0
0xBE542   08d1       BNE     loc_BE556   <==
0xBE544   0b9d       LDR     R5, [SP,#0xE8+var_BC]
0xBE546   002d       CMP     R5, #0
0xBE548   02d0       BEQ     loc_BE550
0xBE54A   3948       LDR     R0, =(aAHitTieMugun - 0xBE550); "A_hit_tie_mugun"
0xBE54C   7844       ADD     R0, PC; "A_hit_tie_mugun"
0xBE54E   2ae0       B       loc_BE5A6
```

