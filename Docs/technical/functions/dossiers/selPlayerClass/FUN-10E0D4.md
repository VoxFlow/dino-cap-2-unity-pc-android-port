# selPlayerClass::Stand

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-10E0D4` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x10E0D4` |
| RVA (EA - 0x1000) | `0x10D0D4` |
| File offset | `0x10D0D4` |
| Size | 66 code bytes in 1 chunk(s), 28 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap14selPlayerClass5StandEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selPlayerClass::Stand(void)` |
| Bytes SHA-256 | `a05752fd11879a4079d3744e4b82b78c8397e29d86e4a11d466b53fb25fae2a7` |
| Direct code callers | 3 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.anim.player_base_tables` (PROVEN, NATIVE (decompiler)): DinoCap::GenComAnim(a2,a3=hero,a4)@0x188da clones global base animation tables per hero type (g_ca_pBase*/2pBase*/3pBase*/4pBase*; Hit/Move/Stand/Fire/Behit/House/Roll/Roll2 and second-layer p2* variants) into working tables t_ca_p*Data, then per equipped-weapon class (a2) clones them into g_ca_p<N>*Data (N=1..6 weapon holders) with SetKeyFrame values: Fire 3, Behit 7, Hit 10 (0xA), 9/0xB for special weapons; SetSpri...

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: [`selectScene::InitCharacter`](../../../functions/dossiers/selectScene/FUN-0F4D30.md), `selPlayerClass::UpdateControl`, [`selPlayerClass::OnAnimationEnd`](../../../functions/dossiers/selPlayerClass/FUN-110838.md)

Callees: [`CAObjCallBack::playAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00A064.md), [`CAObjCallBack::setAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00AEA0.md), `std::string::compare`

## Strings it references

- `0x208254` "playerStandAnimation"

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x10E0D4  b0b5       PUSH    {R4,R5,R7,LR}
0x10E0D6  02af       ADD     R7, SP, #8
0x10E0D8  81b0       SUB     SP, SP, #4
0x10E0DA  0f4c       LDR     R4, =(aPlayerstandani - 0x10E0E4); "playerStandAnimation"
0x10E0DC  051c       MOVS    R5, R0
0x10E0DE  6430       ADDS    R0, #0x64 ; 'd'; this
0x10E0E0  7c44       ADD     R4, PC; "playerStandAnimation"
0x10E0E2  211c       MOVS    R1, R4; char *
0x10E0E4  dbf0f2ec   BLX     __ZNKSs7compareEPKc; std::string::compare(char const*)
0x10E0E8  0028       CMP     R0, #0
0x10E0EA  12d0       BEQ     loc_10E112
0x10E0EC  a4239b00   MOVS    R3, #0x290
0x10E0F0  281c       MOVS    R0, R5; this
0x10E0F2  e958       LDR     R1, [R5,R3]; DinoCap::CombineAnimation *
0x10E0F4  221c       MOVS    R2, R4; char *
0x10E0F6  fcf6d3fe   BL      __ZN7DinoCap13CAObjCallBack12setAnimationEPNS_16CombineAnimationEPKc; DinoCap::CAObjCallBack::setAnimation(DinoCap::CombineAnimation *,char const*)
0x10E0FA  0121       MOVS    R1, #1
0x10E0FC  4942       NEGS    R1, R1; unsigned int
0x10E0FE  0022       MOVS    R2, #0; int
0x10E100  064b       LDR     R3, flt_10E11C; float
0x10E102  281c       MOVS    R0, R5; this
0x10E104  0091       STR     R1, [SP,#4+var_4]; unsigned int
0x10E106  fbf6adff   BL      __ZN7DinoCap13CAObjCallBack13playAnimationEmifm; DinoCap::CAObjCallBack::playAnimation(ulong,int,float,ulong)
0x10E10A  a0239b00   MOVS    R3, #0x280
```

