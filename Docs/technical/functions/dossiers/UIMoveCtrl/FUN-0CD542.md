# UIMoveCtrl::HandleTouch

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0CD542` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xCD542` |
| RVA (EA - 0x1000) | `0xCC542` |
| File offset | `0xCC542` |
| Size | 260 code bytes in 1 chunk(s), 121 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap10UIMoveCtrl11HandleTouchEiiii` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::UIMoveCtrl::HandleTouch(int,int,int,int)` |
| Bytes SHA-256 | `c8aac735b12844be65add54fafa55b3b3ea58f6560bc75e9dc359ab49b8cf019` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.ui.UIMoveCtrl.touch` (PROVEN, NATIVE (decompiler)): UIMoveCtrl::HandleTouch@0xcd542 (touch type a3: 0=down,1=move,2=up) is ignored while the control's tween is active (+132!=0). Down inside stored rect (this+148): pressed flag (+144)=1 and the control rect becomes a centred 50% size (x+=0.25w, y+=0.25h, w*=0.5, h*=0.5) = the observed 'contracts on hold'. Move (type 1) outside the stored rect while pressed: pressed=0 and the full rect is restored (no click). Up (type 2...
- Rule `dc2.ui.image_touch_consume.r118z4` (PROVEN, NATIVE): UIManager::HandleUITouch 0x18102e tests only GetEnable (vtable +56, UIControl +0x21, default 1) - not visibility - before HandleTouch (vtable +84). UIImage::HandleTouch 0x17f1ac and UILabel::HandleTouch 0x17fd98 return UIControl::PtInRect 0x17e70c: half-open left <= x < left+w, top <= y < top+h, and inside the clip rect (+0x24) when the clip flag (+0x22) is set. So every enabled plain image or label consumes every to...

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameControls.cs:13`](../../../../../Assets/Scripts/GameControls.cs#L13) cites `0xCD542`
- [`Assets/Scripts/UiControls.cs:223`](../../../../../Assets/Scripts/UiControls.cs#L223) cites `0xCD542`

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: [`PlaySound`](../../../functions/dossiers/DinoCap/FUN-01A448.md), `___addsf3vfp`, `___floatsisfvfp`, `___mulsf3vfp`

## Strings it references

- `0x204864` "Audio_buttonAudio"

## Named data it references

- `0x22A2E8` __ZNK9Triniti2D4Rect8PtInRectERKNS_7Vector2E_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xCD542   f0b5       PUSH    {R4-R7,LR}
0xCD544   4646       MOV     R6, R8
0xCD546   40b4       PUSH    {R6}
0xCD548   04af       ADD     R7, SP, #0x10
0xCD54A   87b0       SUB     SP, SP, #0x1C
0xCD54C   061c       MOVS    R6, R0
0xCD54E   141c       MOVS    R4, R2
0xCD550   8422       MOVS    R2, #0x84
0xCD552   181c       MOVS    R0, R3
0xCD554   b55c       LDRB    R5, [R6,R2]
0xCD556   002d       CMP     R5, #0
0xCD558   70d1       BNE     loc_CD63C
0xCD55A   1cf108ec   BLX     ___floatsisfvfp
0xCD55E   0590       STR     R0, [SP,#0x1C+var_8]
0xCD560   0d98       LDR     R0, [SP,#0x1C+arg_0]
0xCD562   1cf104ec   BLX     ___floatsisfvfp
0xCD566   0690       STR     R0, [SP,#0x1C+var_4]
0xCD568   012c       CMP     R4, #1
0xCD56A   37d0       BEQ     loc_CD5DC
0xCD56C   022c       CMP     R4, #2
0xCD56E   4ad0       BEQ     loc_CD606
0xCD570   002c       CMP     R4, #0
0xCD572   63d1       BNE     loc_CD63C
0xCD574   344b       LDR     R3, =(__ZNK9Triniti2D4Rect8PtInRectERKNS_7Vector2E_ptr - 0xCD57E)
```

