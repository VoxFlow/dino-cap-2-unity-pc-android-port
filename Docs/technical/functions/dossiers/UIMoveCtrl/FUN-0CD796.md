# UIMoveCtrl::Update

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0CD796` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xCD796` |
| RVA (EA - 0x1000) | `0xCC796` |
| File offset | `0xCC796` |
| Size | 748 code bytes in 1 chunk(s), 323 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap10UIMoveCtrl6UpdateEf` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::UIMoveCtrl::Update(float)` |
| Bytes SHA-256 | `a92da42ddc6d1616ce3eedee4d83242b940e3f0f62340df3dd8004d0836efcd7` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.ui.UIMoveCtrl.tween` (PROVEN, NATIVE (ARM-verified phase timers)): UIMoveCtrl::StartMove(start,mid,end,d1,d2) @0xcd466 stores 3 UIMoveCtrlData (x,y,w,h as centre-x? + Color32) and durations d1 (+124), d2 (+128), sets active (+132) and timer 0 (+136), phase 1 (+140). Update(dt) @0xcd796: t+=dt; phase1: r=t/d1, at r>=1 -> phase 2, t-=d1, r=1; position/size/colour lerp start->mid; phase2: r=t/d2, at r>=1 clamp and stop, lerp mid->end. Drawn rect = centre minus half size. Colour channel...

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/UiControls.cs:178`](../../../../../Assets/Scripts/UiControls.cs#L178) cites `0xCD796`

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: `Triniti2D::UIImage::SetColor`, `___addsf3vfp`, `___divsf3vfp`, `___fixunssfsivfp`, `___floatsisfvfp`, `___gesf2vfp`, `___mulsf3vfp`, `___subsf3vfp`

## Named data it references

- `0x22A1F8` __ZN9Triniti2D4LerpINS_7Vector2EEET_RKS2_S4_f_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xCD796   f0b5       PUSH    {R4-R7,LR}
0xCD798   5e46       MOV     R6, R11
0xCD79A   5546       MOV     R5, R10
0xCD79C   4446       MOV     R4, R8
0xCD79E   70b4       PUSH    {R4-R6}
0xCD7A0   06af       ADD     R7, SP, #0x18
0xCD7A2   8eb0       SUB     SP, SP, #0x38
0xCD7A4   051c       MOVS    R5, R0
0xCD7A6   8420       MOVS    R0, #0x84
0xCD7A8   8046       MOV     R8, R0
0xCD7AA   2b5c       LDRB    R3, [R5,R0]
0xCD7AC   002b       CMP     R3, #0
0xCD7AE   00d1       BNE     loc_CD7B2
0xCD7B0   61e1       B       loc_CDA76
0xCD7B2   8826       MOVS    R6, #0x88
0xCD7B4   a859       LDR     R0, [R5,R6]
0xCD7B6   1cf15cea   BLX     ___addsf3vfp
0xCD7BA   0123       MOVS    R3, #1
0xCD7BC   5b42       NEGS    R3, R3
0xCD7BE   0d93       STR     R3, [SP,#0x38+var_4]
0xCD7C0   8c23       MOVS    R3, #0x8C
0xCD7C2   b049       LDR     R1, =0
0xCD7C4   9a46       MOV     R10, R3
0xCD7C6   eb58       LDR     R3, [R5,R3]
```

