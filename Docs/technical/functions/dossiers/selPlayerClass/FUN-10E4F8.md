# selPlayerClass::ChangeWeapon

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-10E4F8` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x10E4F8` |
| RVA (EA - 0x1000) | `0x10D4F8` |
| File offset | `0x10D4F8` |
| Size | 2294 code bytes in 4 chunk(s), 848 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap14selPlayerClass12ChangeWeaponEii` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selPlayerClass::ChangeWeapon(int,int)` |
| Bytes SHA-256 | `a2f465c864b8993a40a76d06e3f4e278ea7f8a895a91b1bb31f9b72a7ba9ff07` |
| Direct code callers | 2 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.anim.player_base_tables` (PROVEN, NATIVE (decompiler)): DinoCap::GenComAnim(a2,a3=hero,a4)@0x188da clones global base animation tables per hero type (g_ca_pBase*/2pBase*/3pBase*/4pBase*; Hit/Move/Stand/Fire/Behit/House/Roll/Roll2 and second-layer p2* variants) into working tables t_ca_p*Data, then per equipped-weapon class (a2) clones them into g_ca_p<N>*Data (N=1..6 weapon holders) with SetKeyFrame values: Fire 3, Behit 7, Hit 10 (0xA), 9/0xB for special weapons; SetSpri...
- Rule `dc2.anim.player_skin_swap` (PROVEN, NATIVE (decompiler+disasm)): Hero skins are made by CombineAnimation::PlayerChangeImage(id)@0xce12 / PlayerChangeHead(id)@0xd4a8 on cloned base tables (called from the startup init sub_69DE6 chunks 0x6a0c0-0x6b2a6): ChangeImage(partName, newSpriteBase, scale) replaces the sprite of named parts. Ids: 1=yuanjian*/0002; 2=yyj*/0002; 3=com1 shou0002 + yyj/0002 (+0003 hands); 4=yuanjian*/0003; 5=yyj*/0003 + roll p3move; 6=com1 shou0003+yyj/0003; 7=yu...

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: [`selPlayerClass::InitType`](../../../functions/dossiers/selPlayerClass/FUN-1102E0.md)

Callees: [`CombineAnimation::Clone`](../../../functions/dossiers/CombineAnimation/FUN-00C22A.md), [`CombineAnimation::ChangeImage`](../../../functions/dossiers/CombineAnimation/FUN-00CC8A.md), [`CombineAnimation::SetSpriteAnim`](../../../functions/dossiers/CombineAnimation/FUN-00EDBC.md), `sub_10E930`, `sub_10F286`, `___switchu8`, `___switch16`

## Strings it references

- `0x2012F0` "com1"
- `0x20832C` "A4"
- `0x2012E0` "yyj5"
- `0x208330` "B4"
- `0x208334` "C4"
- `0x207B60` "D4"
- `0x208338` "E4"
- `0x20833C` "yuanli20001"
- `0x20231C` "com15"
- `0x208348` "shoulijian01"
- `0x208358` "yuanjian7"
- `0x208364` "G6"
- `0x208368` "G4"
- `0x20836C` "G5"
- `0x208378` "G1"
- `0x20837C` "A1"
- `0x208380` "B1"
- `0x208384` "C1"
- `0x208388` "D1"
- `0x207B68` "E1"
- `0x20838C` "F1"
- `0x208390` "D2"
- `0x208394` "E2"
- `0x208398` "F2"
- `0x20839C` "G2"
- `0x2083A0` "A2"
- `0x2083A4` "B2"
- `0x2083A8` "C2"
- `0x2083B0` "A3"
- `0x207B58` "B3"

## Named data it references

- `0x22A640` __ZN7DinoCap11g_b_HitDownE_ptr
- `0x23B8C2` DinoCap::g_b_HitDown
- `0x22A78C` __ZN7DinoCap14g_ca_pRollDataE_ptr
- `0x23E9BC` DinoCap::g_ca_pRollData
- `0x22A7C0` __ZN7DinoCap15g_ca_pRoll2DataE_ptr
- `0x23E9D8` DinoCap::g_ca_pRoll2Data
- `0x22A788` __ZN7DinoCap14g_ca_pMoveDataE_ptr
- `0x23E9F4` DinoCap::g_ca_pMoveData
- `0x22A7AC` __ZN7DinoCap15g_ca_pStandDataE_ptr
- `0x23EA10` DinoCap::g_ca_pStandData
- `0x22A7E0` __ZN7DinoCap14g_ca_pFireDataE_ptr
- `0x23EA2C` DinoCap::g_ca_pFireData
- `0x22A798` __ZN7DinoCap15g_ca_pBehitDataE_ptr
- `0x23EA48` DinoCap::g_ca_pBehitData
- `0x22A758` __ZN7DinoCap15g_ca_pHouseDataE_ptr
- `0x23EA64` DinoCap::g_ca_pHouseData
- `0x22A7B0` __ZN7DinoCap15g_ca_p2MoveDataE_ptr
- `0x23EA80` DinoCap::g_ca_p2MoveData
- `0x22A7D4` __ZN7DinoCap16g_ca_p2StandDataE_ptr
- `0x23EA9C` DinoCap::g_ca_p2StandData
- `0x22A76C` __ZN7DinoCap15g_ca_p2FireDataE_ptr
- `0x23EAB8` DinoCap::g_ca_p2FireData
- `0x22A7C4` __ZN7DinoCap16g_ca_p2BehitDataE_ptr
- `0x23EAD4` DinoCap::g_ca_p2BehitData
- `0x22A784` __ZN7DinoCap16g_ca_p2HouseDataE_ptr
- `0x23EAF0` DinoCap::g_ca_p2HouseData
- `0x22A778` __ZN7DinoCap16g_ca_p3HouseDataE_ptr
- `0x23EB7C` DinoCap::g_ca_p3HouseData
- `0x22A7DC` __ZN7DinoCap15g_ca_p3MoveDataE_ptr
- `0x23EB0C` DinoCap::g_ca_p3MoveData

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x10E4F8  f0b5       PUSH    {R4-R7,LR}
0x10E4FA  5e46       MOV     R6, R11
0x10E4FC  5546       MOV     R5, R10
0x10E4FE  4446       MOV     R4, R8
0x10E500  70b4       PUSH    {R4-R6}
0x10E502  06af       ADD     R7, SP, #0x18
0x10E504  86b0       SUB     SP, SP, #0x18
0x10E506  061c       MOVS    R6, R0
0x10E508  0c1c       MOVS    R4, R1
0x10E50A  9246       MOV     R10, R2
0x10E50C  0629       CMP     R1, #6
0x10E50E  01dd       BLE     loc_10E514
0x10E510  00f0b9fe   BL      sub_10F286
0x10E514  c34b       LDR     R3, =(__ZN7DinoCap11g_b_HitDownE_ptr - 0x10E51E)
0x10E516  0022       MOVS    R2, #0
0x10E518  081c       MOVS    R0, R1
0x10E51A  7b44       ADD     R3, PC; __ZN7DinoCap11g_b_HitDownE_ptr
0x10E51C  1b68       LDR     R3, [R3]; DinoCap::g_b_HitDown ...
0x10E51E  1a70       STRB    R2, [R3]; DinoCap::g_b_HitDown
0x10E520  d9f0a6ec   BLX     ___switchu8; switch 8 cases
0x10E52E  5046       MOV     R0, R10; jumptable 0010E520 case 0
0x10E530  d9f0b6ec   BLX     ___switch16; switch 5 cases
0x10E540  b94a       LDR     R2, =(aCom1 - 0x10E548); jumptable 0010E530 case 3
0x10E542  ba4b       LDR     R3, =(aA4 - 0x10E54A); "A4"
```

