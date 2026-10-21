# lifeModeScene::AddBoss

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0D105A` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xD105A` |
| RVA (EA - 0x1000) | `0xD005A` |
| File offset | `0xD005A` |
| Size | 710 code bytes in 1 chunk(s), 318 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap13lifeModeScene7AddBossEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::lifeModeScene::AddBoss(void)` |
| Bytes SHA-256 | `6d136dcb0716036ebe3da2be41858ad7e2eb6b3fee5f0b328cf7f370bfe266b5` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / VERIFIED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.engine.scene_update_order.r118z3` (PROVEN, NATIVE): Scene::Update object pass = multimap<int id> in key order, equal ids in insertion order; a pooled object keeps its first-creation slot. Ids: 0 items / bossN GetEff effectClassN pool / LevelUp effectClassN / rain; 1 hero; 2 dinosaurs; 3 bulletClass shells; 5 effects, FeiBiao/JuPan, effectClassN (GetEffectN), bossN itself (AddBoss 0xd1256), YanJiang; 6 blood; 10 house. FALSIFIED r118z: "boss body id 0 before the hero" ...

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameBoss.cs:11`](../../../../../Assets/Scripts/GameBoss.cs#L11) cites `0xD1256`
- [`Assets/Scripts/GameBoss.cs:41`](../../../../../Assets/Scripts/GameBoss.cs#L41) (by name)
- [`Assets/Scripts/GameBoss.cs:105`](../../../../../Assets/Scripts/GameBoss.cs#L105) cites `0xD105A`
- [`Assets/Scripts/GameBoss.cs:117`](../../../../../Assets/Scripts/GameBoss.cs#L117) cites `0xD105A`
- [`Assets/Scripts/GameBoss.cs:259`](../../../../../Assets/Scripts/GameBoss.cs#L259) cites `0xD1256`
- [`Assets/Scripts/GameCombat.cs:13`](../../../../../Assets/Scripts/GameCombat.cs#L13) cites `0xD1102`

## Callers and callees (direct code references)

Callers: [`lifeModeScene::OnTimeout`](../../../functions/dossiers/lifeModeScene/FUN-0E2A4C.md)

Callees: `bossPropertyVector::loadFromXml`, [`lifeModeScene::UpdateBossBlood`](../../../functions/dossiers/lifeModeScene/FUN-0D0710.md), [`bossN::OnIdle`](../../../functions/dossiers/bossN/FUN-12125E.md), `Triniti2D::GameObject::Enable`, `Triniti2D::GameObject::GetPosition`, `Triniti2D::GameObject::GetCallBack`, `Triniti2D::GameObject::SetRotation`, `Triniti2D::GameObject::SetSize`, `Triniti2D::Scene::GetCallBack`, `Triniti2D::Scene::CreateGameObject`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___dynamic_cast`, `___floatsisfvfp`, `___mulsf3vfp`

## Strings it references

- `0x208594` "boss.xml"
- `0x207320` "bossN"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x22757C` GCC_except_table13_7
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A7BC` __ZN7DinoCap6g_bossE_ptr
- `0x240468` DinoCap::g_boss
- `0x22A2DC` __ZN7DinoCap13g_prop_PlayerE_ptr
- `0x240474` DinoCap::g_prop_Player
- `0x22A2B8` __ZN7DinoCap13g_player_typeE_ptr
- `0x23B86C` DinoCap::g_player_type
- `0x22A7C8` __ZN7DinoCap11g_boss_lineE_ptr
- `0x23B870` DinoCap::g_boss_line
- `0x22A0A8` __ZTIN9Triniti2D18GameObjectCallBackE_ptr
- `0x22A2FC` __ZTIN7DinoCap5bossNE_ptr
- `0x22D290` `typeinfo for'DinoCap::bossN
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22A11C` __ZNSt6vectorIPN9Triniti2D10GameObjectESaIS2_EE9push_backERKS2__ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xD105A   f0b5       PUSH    {R4-R7,LR}
0xD105C   5e46       MOV     R6, R11
0xD105E   5546       MOV     R5, R10
0xD1060   4446       MOV     R4, R8
0xD1062   70b4       PUSH    {R4-R6}
0xD1064   16f13cef   BLX     ___save_vfp_d8_d15_regs
0xD1068   16af       ADD     R7, SP, #arg_38
0xD106A   a9b0       SUB     SP, SP, #0xA4
0xD106C   ac4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xD1076)
0xD106E   1290       STR     R0, [SP,#0xA4+var_5C]
0xD1070   0122       MOVS    R2, #1
0xD1072   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xD1074   6846       MOV     R0, SP
0xD1076   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xD1078   2390       STR     R0, [SP,#0xA4+var_18]
0xD107A   19a8       ADD     R0, SP, #0xA4+fctx; lpfctx
0xD107C   1f93       STR     R3, [SP,#0xA4+fctx.personality]
0xD107E   a94b       LDR     R3, =(GCC_except_table13_7 - 0xD1086)
0xD1080   2197       STR     R7, [SP,#0xA4+fctx.jbuf]
0xD1082   7b44       ADD     R3, PC; GCC_except_table13_7
0xD1084   2093       STR     R3, [SP,#0xA4+fctx.lsda]
0xD1086   a84b       LDR     R3, =(loc_D1262 - 0xD108C)
0xD1088   7b44       ADD     R3, PC; loc_D1262
0xD108A   1343       ORRS    R3, R2
```

**Around `0xD1102` (cited by the Unity code)**

```
0xD10F4   9b1a       SUBS    R3, R3, R2
0xD10F6   4018       ADDS    R0, R0, R1
0xD10F8   9b00       LSLS    R3, R3, #2
0xD10FA   4000       LSLS    R0, R0, #1
0xD10FC   0e93       STR     R3, [SP,#0xA4+var_6C]
0xD10FE   18f136ee   BLX     ___floatsisfvfp
0xD1102   0e9a       LDR     R2, [SP,#0xA4+var_6C]   <==
0xD1104   0f9b       LDR     R3, [SP,#0xA4+var_68]
0xD1106   011c       MOVS    R1, R0
0xD1108   d058       LDR     R0, [R2,R3]
0xD110A   18f1b2ed   BLX     ___addsf3vfp
0xD110E   1299       LDR     R1, [SP,#0xA4+var_5C]
0xD1110   109a       LDR     R2, [SP,#0xA4+var_64]
```

**Around `0xD1256` (cited by the Unity code)**

```
0xD1248   0493       STR     R3, [SP,#0xA4+var_94]; bool
0xD124A   1898       LDR     R0, [SP,#0xA4+var_44]; this
0xD124C   26a9       ADD     R1, SP, #0xA4+var_C; std::string *
0xD124E   0522       MOVS    R2, #5; int
0xD1250   a4f08af8   BL      __ZN9Triniti2D5Scene16CreateGameObjectERKSsibbbbbb; Triniti2D::Scene::CreateGameObject(std::string const&,int,bool,bool,bool,bool,bool,bool)
0xD1254   0221       MOVS    R1, #2
0xD1256   2790       STR     R0, [SP,#0xA4+var_8]   <==
0xD1258   1a91       STR     R1, [SP,#0xA4+fctx.call_site]
0xD125A   26a8       ADD     R0, SP, #0xA4+var_C; this
0xD125C   18f1a8ec   BLX     __ZNSsD2Ev; std::string::~string()
0xD1260   0fe0       B       loc_D1282
0xD1262   1b9a       LDR     R2, [SP,#0xA4+fctx.data]
0xD1264   1a9b       LDR     R3, [SP,#0xA4+fctx.call_site]
```

