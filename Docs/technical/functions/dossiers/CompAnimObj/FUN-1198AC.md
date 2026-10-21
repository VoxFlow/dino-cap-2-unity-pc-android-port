# CompAnimObj::SetAnimation

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-1198AC` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x1198AC` |
| RVA (EA - 0x1000) | `0x1188AC` |
| File offset | `0x1188AC` |
| Size | 238 code bytes in 1 chunk(s), 109 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11CompAnimObj12SetAnimationEPNS_18ComponentAnimationERKSsb` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::CompAnimObj::SetAnimation(DinoCap::ComponentAnimation *,std::string const&,bool)` |
| Bytes SHA-256 | `083f5cdaf3e36e1ca274ae9a67f6cf1058e1dc24e75df0d9fb148afaff830a72` |
| Direct code callers | 43 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/NativeCompObj.cs:114`](../../../../../Assets/Scripts/NativeCompObj.cs#L114) cites `0x1198AC`
- [`Assets/Scripts/NativeComponentAnimation.cs:185`](../../../../../Assets/Scripts/NativeComponentAnimation.cs#L185) (by name)

## Callers and callees (direct code references)

Callers: [`mainScene::LevelUpEffect`](../../../functions/dossiers/mainScene/FUN-0957E0.md), [`mainScene::BrokenEgg`](../../../functions/dossiers/mainScene/FUN-09725A.md), [`mainScene::EggFull`](../../../functions/dossiers/mainScene/FUN-0974FA.md), [`mainScene::OnLoad`](../../../functions/dossiers/mainScene/FUN-0A7434.md), [`menuScene::InitPlayer`](../../../functions/dossiers/menuScene/FUN-0C999A.md), [`lifeModeScene::LevelUpEffect`](../../../functions/dossiers/lifeModeScene/FUN-0D0024.md), [`selectScene::AddJigsawEff`](../../../functions/dossiers/selectScene/FUN-0F4F70.md), [`bossN::YanJiang`](../../../functions/dossiers/bossN/FUN-1201A8.md), [`bossN::MoveOut`](../../../functions/dossiers/bossN/FUN-12041A.md), [`bossN::AttackRest`](../../../functions/dossiers/bossN/FUN-1205B2.md), [`bossN::BossDieOntime`](../../../functions/dossiers/bossN/FUN-120804.md), [`bossN::Attack`](../../../functions/dossiers/bossN/FUN-120AC4.md), [`bossN::OnIdle`](../../../functions/dossiers/bossN/FUN-12125E.md), [`bossN::OnUpdate`](../../../functions/dossiers/bossN/FUN-121A98.md), [`bossN::BossShao`](../../../functions/dossiers/bossN/FUN-123106.md), [`playerNewAniClass::Stand`](../../../functions/dossiers/playerNewAniClass/FUN-124F82.md), [`playerNewAniClass::Move`](../../../functions/dossiers/playerNewAniClass/FUN-125072.md), [`playerNewAniClass::Qianghuo`](../../../functions/dossiers/playerNewAniClass/FUN-12530C.md), [`playerNewAniClass::Roll`](../../../functions/dossiers/playerNewAniClass/FUN-126626.md), [`playerNewAniClass::Die`](../../../functions/dossiers/playerNewAniClass/FUN-126A36.md), [`playerNewAniClass::OnHitDown`](../../../functions/dossiers/playerNewAniClass/FUN-127954.md), [`playerNewAniClass::OnTimeout`](../../../functions/dossiers/playerNewAniClass/FUN-127DAC.md), [`playerNewAniClass::BeAttack`](../../../functions/dossiers/playerNewAniClass/FUN-1282B8.md), [`playerNewAniClass::BeAttackByBoss`](../../../functions/dossiers/playerNewAniClass/FUN-1286E4.md), [`playerNewAniClass::ChangeWeapon`](../../../functions/dossiers/playerNewAniClass/FUN-1298D0.md), `sub_12A1D6`

Callees: [`CompAnimObj::DoSetAvatar`](../../../functions/dossiers/CompAnimObj/FUN-1186E6.md), [`CompAnimObj::OnUpdateSprite`](../../../functions/dossiers/CompAnimObj/FUN-1189EC.md), `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::assign`, `std::string::~string`

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x228AEE` GCC_except_table60_0
- `0x22AF54` __ZTVN7DinoCap17CompAnimObjSpriteE_ptr
- `0x22AEBC` __ZNSt8_Rb_treeISsSt4pairIKSsSsESt10_Select1stIS2_ESt4lessISsESaIS2_EEaSERKS8__ptr
- `0x22AE74` __ZN7DinoCap17CompAnimObjSpriteC2Ev_ptr
- `0x22AE80` __ZNSt6vectorIN7DinoCap17CompAnimObjSpriteESaIS1_EE6resizeEmS1__ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x1198AC  f0b5       PUSH    {R4-R7,LR}
0x1198AE  5e46       MOV     R6, R11
0x1198B0  5546       MOV     R5, R10
0x1198B2  4446       MOV     R4, R8
0x1198B4  70b4       PUSH    {R4-R6}
0x1198B6  cef014eb   BLX     ___save_vfp_d8_d15_regs
0x1198BA  16af       ADD     R7, SP, #arg_38
0x1198BC  a8b0       SUB     SP, SP, #0xA0
0x1198BE  dbb2       UXTB    R3, R3
0x1198C0  0193       STR     R3, [SP,#0xA0+var_9C]
0x1198C2  364b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x1198CC)
0x1198C4  0124       MOVS    R4, #1
0x1198C6  0490       STR     R0, [SP,#0xA0+var_90]
0x1198C8  7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x1198CA  0292       STR     R2, [SP,#0xA0+var_98]
0x1198CC  1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x1198CE  6a46       MOV     R2, SP
0x1198D0  06a8       ADD     R0, SP, #0xA0+fctx; lpfctx
0x1198D2  0c93       STR     R3, [SP,#0xA0+fctx.personality]
0x1198D4  324b       LDR     R3, =(GCC_except_table60_0 - 0x1198DE)
0x1198D6  0391       STR     R1, [SP,#0xA0+var_94]
0x1198D8  1092       STR     R2, [SP,#0xA0+var_60]
0x1198DA  7b44       ADD     R3, PC; GCC_except_table60_0
0x1198DC  0d93       STR     R3, [SP,#0xA0+fctx.lsda]
```

