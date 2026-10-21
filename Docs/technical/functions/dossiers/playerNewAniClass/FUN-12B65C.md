# playerNewAniClass::KaiQiang

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-12B65C` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x12B65C` |
| RVA (EA - 0x1000) | `0x12A65C` |
| File offset | `0x12A65C` |
| Size | 1708 code bytes in 1 chunk(s), 670 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap17playerNewAniClass8KaiQiangEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerNewAniClass::KaiQiang(void)` |
| Bytes SHA-256 | `4670626cb7876bf2a3a0786d6b2ecf20fa86a64107c850b8069e3ea63729e337` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameBlood.cs:60`](../../../../../Assets/Scripts/GameBlood.cs#L60) cites `0x12B8A0`
- [`Assets/Scripts/GameGirl.cs:325`](../../../../../Assets/Scripts/GameGirl.cs#L325) cites `0x12B65C`
- [`Assets/Scripts/GameGirl.cs:352`](../../../../../Assets/Scripts/GameGirl.cs#L352) cites `0x12BBB4`
- [`Assets/Scripts/GameGirl.cs:355`](../../../../../Assets/Scripts/GameGirl.cs#L355) cites `0x12BC42`

## Callers and callees (direct code references)

Callers: [`playerNewAniClass::OnKeyFrameAdv`](../../../functions/dossiers/playerNewAniClass/FUN-12BD44.md)

Callees: [`PlaySound`](../../../functions/dossiers/DinoCap/FUN-01A448.md), `subWeaponClass::~subWeaponClass`, [`CompAnimObj::GetSprite`](../../../functions/dossiers/CompAnimObj/FUN-117E7C.md), [`CompAnimObj::SetAvatar`](../../../functions/dossiers/CompAnimObj/FUN-11888E.md), [`playerNewAniClass::Qianghuo`](../../../functions/dossiers/playerNewAniClass/FUN-12530C.md), [`playerNewAniClass::Danke`](../../../functions/dossiers/playerNewAniClass/FUN-126804.md), [`playerNewAniClass::Attack`](../../../functions/dossiers/playerNewAniClass/FUN-1291D0.md), `___switch32`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::compare`, `std::string::assign`, `std::string::string`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___mulsf3vfp`

## Strings it references

- `0x20B248` "lian"
- `0x20B284` "bshouqiang"
- `0x204D58` "danke1Frame"
- `0x204908` "Attack_AAudio"
- `0x204D64` "danke2Frame"
- `0x2048F8` "Attack_BAudio"
- `0x2048D8` "Attack_DAudio"
- `0x2048C8` "Attack_E1Audio"
- `0x20B324` "newplayer_lian4"
- `0x20B334` "newplayer_lian3"
- `0x2048E8` "Attack_CAudio"
- `0x20634C` "Attack_F6Audio"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x228F2C` GCC_except_table44_2
- `0x22A114` __ZNSs4_Rep20_S_empty_rep_storageE_ptr
- `0x241588` std::string::_Rep::_S_empty_rep_storage
- `0x22A24C` __ZN7DinoCap15g_player_vectorE_ptr
- `0x2404AC` DinoCap::g_player_vector
- `0x22A2B8` __ZN7DinoCap13g_player_typeE_ptr
- `0x23B86C` DinoCap::g_player_type
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22AF2C` __ZSt8_DestroyIPSt4pairISsSsEEvT_S3__ptr
- `0x22AFAC` __ZNKSt6vectorISt4pairISsSsESaIS1_EE4sizeEv_ptr
- `0x22AF68` __ZNSt4pairISsSsEC2ERKS0__ptr
- `0x22B154` __ZNSt6vectorISt4pairISsSsESaIS1_EE9push_backERKS1__ptr
- `0x22AFA8` __ZNSt4pairISsSsED2Ev_ptr
- `0x22AF80` __ZNSt6vectorISt4pairISsSsESaIS1_EEaSERKS3__ptr
- `0x22B0B0` __ZNSt4pairISsSsEC2ERKSsS2__ptr
- `0x22AEB0` __ZNSt6vectorISt4pairISsSsESaIS1_EED2Ev_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x12B65C  f0b5       PUSH    {R4-R7,LR}
0x12B65E  5e46       MOV     R6, R11
0x12B660  5546       MOV     R5, R10
0x12B662  4446       MOV     R4, R8
0x12B664  70b4       PUSH    {R4-R6}
0x12B666  bcf03cec   BLX     ___save_vfp_d8_d15_regs
0x12B66A  16af       ADD     R7, SP, #arg_38
0x12B66C  b8b0       SUB     SP, SP, #0xE0
0x12B66E  dd4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x12B678)
0x12B670  0122       MOVS    R2, #1
0x12B672  0690       STR     R0, [SP,#0xE0+var_C8]
0x12B674  7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x12B676  6846       MOV     R0, SP
0x12B678  1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x12B67A  1c90       STR     R0, [SP,#0xE0+var_70]
0x12B67C  12a8       ADD     R0, SP, #0xE0+fctx; lpfctx
0x12B67E  1893       STR     R3, [SP,#0xE0+fctx.personality]
0x12B680  d94b       LDR     R3, =(GCC_except_table44_2 - 0x12B68A)
0x12B682  1a97       STR     R7, [SP,#0xE0+fctx.jbuf]
0x12B684  1fac       ADD     R4, SP, #0xE0+var_64
0x12B686  7b44       ADD     R3, PC; GCC_except_table44_2
0x12B688  1993       STR     R3, [SP,#0xE0+fctx.lsda]
0x12B68A  d84b       LDR     R3, =(loc_12B97E - 0x12B690)
0x12B68C  7b44       ADD     R3, PC; loc_12B97E
```

**Around `0x12B8A0` (cited by the Unity code)**

```
0x12B892  0f22       MOVS    R2, #0xF; unsigned int
0x12B894  bef056e9   BLX     __ZNSs6assignEPKcm; std::string::assign(char const*,ulong)
0x12B898  64e0       B       def_12B718; jumptable 0012B718 default case, cases 4,6
0x12B89A  7049       LDR     R1, =(aBshouqiang - 0x12B8A4); jumptable 0012B718 case 3
0x12B89C  0725       MOVS    R5, #7
0x12B89E  1395       STR     R5, [SP,#0xE0+fctx.call_site]
0x12B8A0  7944       ADD     R1, PC; "bshouqiang"   <==
0x12B8A2  36a8       ADD     R0, SP, #0xE0+var_8; this
0x12B8A4  0a22       MOVS    R2, #0xA; unsigned int
0x12B8A6  bef04ee9   BLX     __ZNSs6assignEPKcm; std::string::assign(char const*,ulong)
0x12B8AA  6d49       LDR     R1, =(aDanke1frame - 0x12B8B4); "danke1Frame"
0x12B8AC  35a8       ADD     R0, SP, #0xE0+var_C; this
0x12B8AE  0b22       MOVS    R2, #0xB; unsigned int
```

**Around `0x12BBB4` (cited by the Unity code)**

```
0x12BBA4  b4235b00   MOVS    R3, #0x168
0x12BBA8  281c       MOVS    R0, R5; this
0x12BBAA  e958       LDR     R1, [R5,R3]; int
0x12BBAC  fdf710fb   BL      __ZN7DinoCap17playerNewAniClass6AttackEi; DinoCap::playerNewAniClass::Attack(int)
0x12BBB0  0698       LDR     R0, [SP,#0xE0+var_C8]; this
0x12BBB2  36a9       ADD     R1, SP, #0xE0+var_8; std::string *
0x12BBB4  ecf762f9   BL      __ZN7DinoCap11CompAnimObj9GetSpriteERKSs; DinoCap::CompAnimObj::GetSprite(std::string const&)   <==
0x12BBB8  0b90       STR     R0, [SP,#0xE0+var_B4]
0x12BBBA  0028       CMP     R0, #0
0x12BBBC  4cd0       BEQ     loc_12BC58
0x12BBBE  4269       LDR     R2, [R0,#0x14]
0x12BBC0  8369       LDR     R3, [R0,#0x18]
0x12BBC2  5b48       LDR     R0, =0
```

**Around `0x12BC42` (cited by the Unity code)**

```
0x12BC36  119d       LDR     R5, [SP,#0xE0+var_9C]
0x12BC38  0698       LDR     R0, [SP,#0xE0+var_C8]
0x12BC3A  2994       STR     R4, [SP,#0xE0+var_3C]
0x12BC3C  2a95       STR     R5, [SP,#0xE0+var_38]
0x12BC3E  0b9d       LDR     R5, [SP,#0xE0+var_B4]
0x12BC40  ea69       LDR     R2, [R5,#0x1C]
0x12BC42  f9f763fb   BL      __ZN7DinoCap17playerNewAniClass8QianghuoERKN9Triniti2D7Vector2Ef; DinoCap::playerNewAniClass::Qianghuo(Triniti2D::Vector2 const&,float)   <==
0x12BC46  0be0       B       loc_12BC60
0x12BC48  3b4b       LDR     R3, =(__ZNSt6vectorISt4pairISsSsESaIS1_EED2Ev_ptr - 0x12BC52)
0x12BC4A  0022       MOVS    R2, #0
0x12BC4C  26a8       ADD     R0, SP, #0xE0+var_48
0x12BC4E  7b44       ADD     R3, PC; __ZNSt6vectorISt4pairISsSsESaIS1_EED2Ev_ptr
0x12BC50  1392       STR     R2, [SP,#0xE0+fctx.call_site]
```

