# IAPPurchase

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-05A1F0` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x5A1F0` |
| RVA (EA - 0x1000) | `0x591F0` |
| File offset | `0x591F0` |
| Size | 602 code bytes in 1 chunk(s), 262 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11IAPPurchaseEi` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::IAPPurchase(int)` |
| Bytes SHA-256 | `ade7c10be974cfcdfe80e8beeee83cc939fa82b5900d50089b395ab70c2a5b13` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.bank.touch_purchase_flow` (PROVEN, NATIVE (decompile selectScene::OnTouchEvent 0xf57bc, OnUpdate 0xf93c4, iapaddmoney 0xf46d2, IAPPurchase 0x5a1f0)): Touch began (0): for every cell where Rect::PtInRect (strict inequalities) contains the point, this+0x28=i; control 94 (layer 0, non-touchable) recreated at the cell rect with ui_iap_selectFrame. Touch ended (2): for each cell containing the point whose index == selection: OSShowActivityIndicator(320,480,1001), control 10 (layer 3, touchable) = sd_baiFrame image rect (0,0,960+w_off, 640+0.5*h_off+h_off=736) colour A0...

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/SelectBank.cs:10`](../../../../../Assets/Scripts/SelectBank.cs#L10) cites `0x5A1F0`

## Callers and callees (direct code references)

Callers: [`selectScene::OnTouchEvent`](../../../functions/dossiers/selectScene/FUN-0F57BC.md)

Callees: `Triniti2D::IAPManager::Purchase`, `___switch32`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::assign`, `std::string::string`, `std::string::~string`

## Strings it references

- `0x2025C0` "dinocap2hd"
- `0x204770` "com.trinitigame."
- `0x204784` ".099centsnew"
- `0x204794` ".299centsnew"
- `0x2047A4` ".499centsnew"
- `0x2047B4` ".999centsnew"
- `0x2047C4` ".1999centsnew"
- `0x2047D4` ".099cents2"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x224E43` GCC_except_table31
- `0x22A310` __ZStplIcSt11char_traitsIcESaIcEESbIT_T0_T1_EPKS3_RKS6__ptr
- `0x22A15C` __ZStplIcSt11char_traitsIcESaIcEESbIT_T0_T1_ERKS6_PKS3__ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x5A1F0   f0b5       PUSH    {R4-R7,LR}
0x5A1F2   5e46       MOV     R6, R11
0x5A1F4   5546       MOV     R5, R10
0x5A1F6   4446       MOV     R4, R8
0x5A1F8   70b4       PUSH    {R4-R6}
0x5A1FA   8df172ee   BLX     ___save_vfp_d8_d15_regs
0x5A1FE   16af       ADD     R7, SP, #arg_38
0x5A200   9ab0       SUB     SP, SP, #0x68 ; 'h'
0x5A202   924b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x5A20C)
0x5A204   6a46       MOV     R2, SP
0x5A206   0d92       STR     R2, [SP,#0x68+var_34]
0x5A208   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x5A20A   0122       MOVS    R2, #1
0x5A20C   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x5A20E   0190       STR     R0, [SP,#0x68+var_64]
0x5A210   03a8       ADD     R0, SP, #0x68+fctx; lpfctx
0x5A212   0993       STR     R3, [SP,#0x68+fctx.personality]
0x5A214   8e4b       LDR     R3, =(GCC_except_table31 - 0x5A21C)
0x5A216   0b97       STR     R7, [SP,#0x68+fctx.jbuf]
0x5A218   7b44       ADD     R3, PC; GCC_except_table31
0x5A21A   0a93       STR     R3, [SP,#0x68+fctx.lsda]
0x5A21C   8d4b       LDR     R3, =(loc_5A39C - 0x5A222)
0x5A21E   7b44       ADD     R3, PC; loc_5A39C
0x5A220   1343       ORRS    R3, R2
```

