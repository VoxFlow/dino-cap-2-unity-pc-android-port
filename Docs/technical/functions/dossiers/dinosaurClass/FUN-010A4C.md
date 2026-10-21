# dinosaurClass::DieShaoSi

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-010A4C` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x10A4C` |
| RVA (EA - 0x1000) | `0xFA4C` |
| File offset | `0xFA4C` |
| Size | 262 code bytes in 1 chunk(s), 110 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap13dinosaurClass9DieShaoSiEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::dinosaurClass::DieShaoSi(void)` |
| Bytes SHA-256 | `a1726944da74b59c7b4074c45653318d93385b3ca73a037fc88aafa9b417b523` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | deep: curated note below, from the batch that established it |

## What it does

Burn death: y = S*160*0.8 + g_ground_y, minus 70 on iPad (literals 0x43200000 = 160, 0x3F4CCCCD = 0.8, 0x428C0000 = 70); skins 1, 2, 5 get that y and scale 0.8; skin 3 only sets scale 1.0 (0x10AD4) and branches past the position write (0x10AD8 -> 0x10B2A), so it keeps its position; skin 5 also moves x by 40 against its facing. The port first used 150 and 72 (ash 18 px too low): corrected in batch r119w.

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameCombat.cs:808`](../../../../../Assets/Scripts/GameCombat.cs#L808) cites `0x10A4C`
- [`Assets/Scripts/GameCombat.cs:812`](../../../../../Assets/Scripts/GameCombat.cs#L812) cites `0x10A4C`
- [`Assets/Scripts/GameCombat.cs:813`](../../../../../Assets/Scripts/GameCombat.cs#L813) cites `0x10B22`

## Callers and callees (direct code references)

Callers: [`dinosaurClass::Die`](../../../functions/dossiers/dinosaurClass/FUN-0140FC.md)

Callees: [`CAObjCallBack::setAnimationAdv`](../../../functions/dossiers/CAObjCallBack/FUN-00A298.md), [`CAObjCallBack::setAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00AEA0.md), `Triniti2D::GameObject::GetPosition`, `Triniti2D::GameObject::SetPosition`, `___switch8`, `___addsf3vfp`, `___mulsf3vfp`, `___subsf3vfp`

## Strings it references

- `0x201C40` "ShaoSiAnim"

## Named data it references

- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A0E8` __ZN7DinoCap10g_ground_yE_ptr
- `0x23B910` DinoCap::g_ground_y
- `0x22A26C` __ZN7DinoCap15g_ca_shaosiDataE_ptr
- `0x23EEE0` DinoCap::g_ca_shaosiData

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x10A4C   f0b5       PUSH    {R4-R7,LR}
0x10A4E   03af       ADD     R7, SP, #0xC
0x10A50   82b0       SUB     SP, SP, #8
0x10A52   404b       LDR     R3, =(__ZN7DinoCap8g_iphoneE_ptr - 0x10A5A)
0x10A54   041c       MOVS    R4, R0
0x10A56   7b44       ADD     R3, PC; __ZN7DinoCap8g_iphoneE_ptr
0x10A58   1b68       LDR     R3, [R3]; DinoCap::g_iphone ...
0x10A5A   1b78       LDRB    R3, [R3]; DinoCap::g_iphone
0x10A5C   002b       CMP     R3, #0
0x10A5E   01d1       BNE     loc_10A64
0x10A60   3d4d       LDR     R5, =0x40000000
0x10A62   00e0       B       loc_10A66
0x10A64   3d4d       LDR     R5, =0x3F800000
0x10A66   6068       LDR     R0, [R4,#4]; this
0x10A68   5bf16cfa   BL      __ZN9Triniti2D10GameObject11GetPositionEv; Triniti2D::GameObject::GetPosition(void)
0x10A6C   c423       MOVS    R3, #0xC4
0x10A6E   4168       LDR     R1, [R0,#4]
0x10A70   0068       LDR     R0, [R0]
0x10A72   0090       STR     R0, [SP,#8+var_8]
0x10A74   0191       STR     R1, [SP,#8+var_4]
0x10A76   e058       LDR     R0, [R4,R3]
0x10A78   431e       SUBS    R3, R0, #1
0x10A7A   181c       MOVS    R0, R3
0x10A7C   d7f104ea   BLX     ___switch8; switch 6 cases
```

**Around `0x10B22` (cited by the Unity code)**

```
0x10B16   7b44       ADD     R3, PC; __ZN7DinoCap8g_iphoneE_ptr
0x10B18   0190       STR     R0, [SP,#8+var_4]
0x10B1A   1b68       LDR     R3, [R3]; DinoCap::g_iphone ...
0x10B1C   1b78       LDRB    R3, [R3]; DinoCap::g_iphone
0x10B1E   002b       CMP     R3, #0
0x10B20   03d1       BNE     def_10A7C; jumptable 00010A7C default case, cases 3,5
0x10B22   1849       LDR     R1, =0x428C0000   <==
0x10B24   d9f194e9   BLX     ___subsf3vfp
0x10B28   0190       STR     R0, [SP,#8+var_4]
0x10B2A   1749       LDR     R1, =(__ZN7DinoCap15g_ca_shaosiDataE_ptr - 0x10B34); jumptable 00010A7C default case, cases 3,5
0x10B2C   174a       LDR     R2, =(aShaosianim - 0x10B36); "ShaoSiAnim"
0x10B2E   201c       MOVS    R0, R4; this
0x10B30   7944       ADD     R1, PC; __ZN7DinoCap15g_ca_shaosiDataE_ptr
```

