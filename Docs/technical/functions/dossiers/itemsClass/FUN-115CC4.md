# itemsClass::OnUpdate

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-115CC4` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x115CC4` |
| RVA (EA - 0x1000) | `0x114CC4` |
| File offset | `0x114CC4` |
| Size | 1144 code bytes in 1 chunk(s), 455 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap10itemsClass8OnUpdateEf` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::itemsClass::OnUpdate(float)` |
| Bytes SHA-256 | `c4db5bbd93a8ef93002a834cc8d95d36e0c1021012b316e2b95488c219979182` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameCombat.cs:77`](../../../../../Assets/Scripts/GameCombat.cs#L77) (by name)
- [`Assets/Scripts/GameItems.cs:89`](../../../../../Assets/Scripts/GameItems.cs#L89) cites `0x115CC4`
- [`Assets/Scripts/GameItems.cs:129`](../../../../../Assets/Scripts/GameItems.cs#L129) cites `0x115CC4`
- [`Assets/Scripts/GameItems.cs:130`](../../../../../Assets/Scripts/GameItems.cs#L130) cites `0x115FD4`
- [`Assets/Scripts/GameItems.cs:159`](../../../../../Assets/Scripts/GameItems.cs#L159) cites `0x115E9E`
- [`Assets/Scripts/GameItems.cs:165`](../../../../../Assets/Scripts/GameItems.cs#L165) cites `0x1160D2`

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: [`GetPlayer4Hp`](../../../functions/dossiers/DinoCap/FUN-01946C.md), [`PlaySound`](../../../functions/dossiers/DinoCap/FUN-01A448.md), [`mainScene::UpdateHP`](../../../functions/dossiers/mainScene/FUN-095228.md), [`mainScene::UpdateBullet`](../../../functions/dossiers/mainScene/FUN-09546C.md), [`mainScene::GetPlayerHP`](../../../functions/dossiers/mainScene/FUN-09708C.md), [`itemsClass::AddBulletZhuan`](../../../functions/dossiers/itemsClass/FUN-115344.md), [`itemsClass::upDown`](../../../functions/dossiers/itemsClass/FUN-115508.md), `Triniti2D::Camera::GetViewport`, `Triniti2D::GameObject::Enable`, `Triniti2D::GameObject::GetPosition`, `Triniti2D::GameObject::GetScene`, `Triniti2D::GameObject::Physics`, `Triniti2D::GameObject::Sprite`, `Triniti2D::GameObject::SetSize`, `Triniti2D::GameObject::SetPosition`, `Triniti2D::GameObject::SetTimer`, `Triniti2D::GameObject::KillTimer`, `Triniti2D::GamePhysics::SetLinearVelocity`, `Triniti2D::GamePhysics::WakeUp`, `Triniti2D::GameSprite::SetFrame`, `Triniti2D::Scene::GetCamera`, `Triniti2D::Scene::DeleteGameObject`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___fixsfsivfp`, `___floatsisfvfp`, `___gesf2vfp`, `___gtsf2vfp`, `___ltsf2vfp`, `___mulsf3vfp`, `___subsf3vfp`

## Strings it references

- `0x200D80` "Audio_itemAudio"
- `0x209CE8` "secondtimer"
- `0x2069B4` "LaddhpFrame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x22880D` GCC_except_table4_14
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A2B8` __ZN7DinoCap13g_player_typeE_ptr
- `0x23B86C` DinoCap::g_player_type
- `0x22A24C` __ZN7DinoCap15g_player_vectorE_ptr
- `0x2404AC` DinoCap::g_player_vector
- `0x22A2DC` __ZN7DinoCap13g_prop_PlayerE_ptr
- `0x240474` DinoCap::g_prop_Player
- `0x22A684` __ZN7DinoCap15g_jigsaw1_countE_ptr
- `0x23E1C8` DinoCap::g_jigsaw1_count

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x115CC4  f0b5       PUSH    {R4-R7,LR}
0x115CC6  5e46       MOV     R6, R11
0x115CC8  5546       MOV     R5, R10
0x115CCA  4446       MOV     R4, R8
0x115CCC  70b4       PUSH    {R4-R6}
0x115CCE  d2f008e9   BLX     ___save_vfp_d8_d15_regs
0x115CD2  16af       ADD     R7, SP, #arg_38
0x115CD4  b3b0       SUB     SP, SP, #0xCC
0x115CD6  d44b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x115CE0)
0x115CD8  1090       STR     R0, [SP,#0xCC+var_8C]
0x115CDA  0122       MOVS    R2, #1
0x115CDC  7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x115CDE  6846       MOV     R0, SP
0x115CE0  1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x115CE2  2490       STR     R0, [SP,#0xCC+var_3C]
0x115CE4  1aa8       ADD     R0, SP, #0xCC+fctx; lpfctx
0x115CE6  2093       STR     R3, [SP,#0xCC+fctx.personality]
0x115CE8  d04b       LDR     R3, =(GCC_except_table4_14 - 0x115CF2)
0x115CEA  0f91       STR     R1, [SP,#0xCC+var_90]
0x115CEC  2297       STR     R7, [SP,#0xCC+fctx.jbuf]
0x115CEE  7b44       ADD     R3, PC; GCC_except_table4_14
0x115CF0  2193       STR     R3, [SP,#0xCC+fctx.lsda]
0x115CF2  cf4b       LDR     R3, =(loc_115F46 - 0x115CF8)
0x115CF4  7b44       ADD     R3, PC; loc_115F46
```

**Around `0x115E9E` (cited by the Unity code)**

```
0x115E92  1a01       LSLS    R2, R3, #4
0x115E94  d31a       SUBS    R3, R2, R3
0x115E96  c018       ADDS    R0, R0, R3; this
0x115E98  1890       STR     R0, [SP,#0xCC+var_6C]
0x115E9A  0429       CMP     R1, #4
0x115E9C  02d1       BNE     loc_115EA4
0x115E9E  03f7e5fa   BL      __ZN7DinoCap12GetPlayer4HpEi; DinoCap::GetPlayer4Hp(int)   <==
0x115EA2  1890       STR     R0, [SP,#0xCC+var_6C]
0x115EA4  1898       LDR     R0, [SP,#0xCC+var_6C]
0x115EA6  d3f062ef   BLX     ___floatsisfvfp
0x115EAA  189a       LDR     R2, [SP,#0xCC+var_6C]
0x115EAC  0890       STR     R0, [SP,#0xCC+var_AC]
0x115EAE  6d48       LDR     R0, =(__ZN7DinoCap15g_jigsaw1_countE_ptr - 0x115EB4)
```

**Around `0x115FD4` (cited by the Unity code)**

```
0x115FC6  1590       STR     R0, [SP,#0xCC+var_78]
0x115FC8  1798       LDR     R0, [SP,#0xCC+var_70]
0x115FCA  d3f02aef   BLX     ___mulsf3vfp
0x115FCE  2da9       ADD     R1, SP, #0xCC+var_18
0x115FD0  2e90       STR     R0, [SP,#0xCC+var_14]
0x115FD2  1598       LDR     R0, [SP,#0xCC+var_78]
0x115FD4  57f0fdfb   BL      __ZN9Triniti2D11GamePhysics17SetLinearVelocityERKNS_7Vector2E; Triniti2D::GamePhysics::SetLinearVelocity(Triniti2D::Vector2 const&)   <==
0x115FD8  109a       LDR     R2, [SP,#0xCC+var_8C]
0x115FDA  bc23       MOVS    R3, #0xBC
0x115FDC  0021       MOVS    R1, #0; bool
0x115FDE  d058       LDR     R0, [R2,R3]; this
0x115FE0  55f0acff   BL      __ZN9Triniti2D10GameObject6EnableEb; Triniti2D::GameObject::Enable(bool)
0x115FE4  109b       LDR     R3, [SP,#0xCC+var_8C]
```

**Around `0x1160D2` (cited by the Unity code)**

```
0x1160C6  1098       LDR     R0, [SP,#0xCC+var_8C]; int
0x1160C8  1099       LDR     R1, [SP,#0xCC+var_8C]
0x1160CA  8358       LDR     R3, [R0,R2]
0x1160CC  9431       ADDS    R1, #0x94; int
0x1160CE  2baa       ADD     R2, SP, #0xCC+var_20; int
0x1160D0  0093       STR     R3, [SP,#0xCC+var_CC]; DinoCap::mainScene *
0x1160D2  0123       MOVS    R3, #1; int   <==
0x1160D4  fff736f9   BL      __ZN7DinoCap10itemsClass14AddBulletZhuanERiRKN9Triniti2D7Vector2EbPNS_9mainSceneE; DinoCap::itemsClass::AddBulletZhuan(int &,Triniti2D::Vector2 const&,bool,DinoCap::mainScene *)
0x1160D8  109b       LDR     R3, [SP,#0xCC+var_8C]
0x1160DA  b421       MOVS    R1, #0xB4
0x1160DC  5858       LDR     R0, [R3,R1]; this
0x1160DE  7ff7c5f9   BL      __ZN7DinoCap9mainScene12UpdateBulletEv; DinoCap::mainScene::UpdateBullet(void)
0x1160E2  109a       LDR     R2, [SP,#0xCC+var_8C]
```

