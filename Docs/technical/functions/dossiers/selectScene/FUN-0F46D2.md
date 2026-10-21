# selectScene::iapaddmoney

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0F46D2` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xF46D2` |
| RVA (EA - 0x1000) | `0xF36D2` |
| File offset | `0xF36D2` |
| Size | 150 code bytes in 1 chunk(s), 66 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene11iapaddmoneyEi` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::iapaddmoney(int)` |
| Bytes SHA-256 | `f3c0bbb64ef818d401a8c8f98474fab7eceddaeea1f4d6c3799cfb61e7073608` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.bank.touch_purchase_flow` (PROVEN, NATIVE (decompile selectScene::OnTouchEvent 0xf57bc, OnUpdate 0xf93c4, iapaddmoney 0xf46d2, IAPPurchase 0x5a1f0)): Touch began (0): for every cell where Rect::PtInRect (strict inequalities) contains the point, this+0x28=i; control 94 (layer 0, non-touchable) recreated at the cell rect with ui_iap_selectFrame. Touch ended (2): for each cell containing the point whose index == selection: OSShowActivityIndicator(320,480,1001), control 10 (layer 3, touchable) = sd_baiFrame image rect (0,0,960+w_off, 640+0.5*h_off+h_off=736) colour A0...

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/SelectBank.cs:9`](../../../../../Assets/Scripts/SelectBank.cs#L9) cites `0xF46D2`
- [`Assets/Scripts/SelectBank.cs:174`](../../../../../Assets/Scripts/SelectBank.cs#L174) cites `0xF46D2`

## Callers and callees (direct code references)

Callers: [`selectScene::OnUpdate`](../../../functions/dossiers/selectScene/FUN-0F93C4.md)

Callees: `GetSystemTime_Second`, `SaveFile`, `SaveMissionInfo`, [`selectScene::UpdateExp`](../../../functions/dossiers/selectScene/FUN-0F45DC.md), `___switch8`

## Named data it references

- `0x22A2DC` __ZN7DinoCap13g_prop_PlayerE_ptr
- `0x240474` DinoCap::g_prop_Player
- `0x23F580` DinoCap::g_ca_egg_che_stand
- `0x22A5EC` __ZN7DinoCap18g_max_bullet_countE_ptr
- `0x23B89C` DinoCap::g_max_bullet_count
- `0x22AB90` __ZN7DinoCap10g_iap_timeE_ptr
- `0x240A84` DinoCap::g_iap_time
- `0x22A6C0` __ZN7DinoCap12g_total_timeE_ptr
- `0x240A7C` DinoCap::g_total_time
- `0x22A700` __ZN7DinoCap14t_cur_sys_timeE_ptr
- `0x240A80` DinoCap::t_cur_sys_time

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xF46D2   90b5       PUSH    {R4,R7,LR}
0xF46D4   01af       ADD     R7, SP, #4
0xF46D6   041c       MOVS    R4, R0
0xF46D8   081c       MOVS    R0, R1
0xF46DA   f3f0d6eb   BLX     ___switch8; switch 7 cases
0xF46E6   204b       LDR     R3, =(__ZN7DinoCap13g_prop_PlayerE_ptr - 0xF46EE); jumptable 000F46DA case 0
0xF46E8   2048       LDR     R0, =off_3A98
0xF46EA   7b44       ADD     R3, PC; __ZN7DinoCap13g_prop_PlayerE_ptr
0xF46EC   1a68       LDR     R2, [R3]; DinoCap::g_prop_Player ...
0xF46EE   9368       LDR     R3, [R2,#(dword_24047C - 0x240474)]
0xF46F0   04e0       B       loc_F46FC
0xF46F2   1f4b       LDR     R3, =(__ZN7DinoCap13g_prop_PlayerE_ptr - 0xF46FA); jumptable 000F46DA case 1
0xF46F4   1f48       LDR     R0, =0xC350
0xF46F6   7b44       ADD     R3, PC; __ZN7DinoCap13g_prop_PlayerE_ptr
0xF46F8   1a68       LDR     R2, [R3]; DinoCap::g_prop_Player ...
0xF46FA   9368       LDR     R3, [R2,#(dword_24047C - 0x240474)]
0xF46FC   1b18       ADDS    R3, R3, R0
0xF46FE   9360       STR     R3, [R2,#(dword_24047C - 0x240474)]
0xF4700   16e0       B       def_F46DA; jumptable 000F46DA default case, case 6
0xF4702   1d4b       LDR     R3, =(__ZN7DinoCap13g_prop_PlayerE_ptr - 0xF470A); jumptable 000F46DA case 2
0xF4704   1d48       LDR     R0, =0x17318
0xF4706   7b44       ADD     R3, PC; __ZN7DinoCap13g_prop_PlayerE_ptr
0xF4708   1a68       LDR     R2, [R3]; DinoCap::g_prop_Player ...
0xF470A   9368       LDR     R3, [R2,#(dword_24047C - 0x240474)]
```

