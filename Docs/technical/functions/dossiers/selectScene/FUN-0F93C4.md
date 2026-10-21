# selectScene::OnUpdate

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0F93C4` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xF93C4` |
| RVA (EA - 0x1000) | `0xF83C4` |
| File offset | `0xF83C4` |
| Size | 1494 code bytes in 1 chunk(s), 593 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene8OnUpdateEf` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::OnUpdate(float)` |
| Bytes SHA-256 | `3ac2e2165a5d922ce3a98c04f6f829171b365be293f3348a09345daa8d866e77` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.bank.touch_purchase_flow` (PROVEN, NATIVE (decompile selectScene::OnTouchEvent 0xf57bc, OnUpdate 0xf93c4, iapaddmoney 0xf46d2, IAPPurchase 0x5a1f0)): Touch began (0): for every cell where Rect::PtInRect (strict inequalities) contains the point, this+0x28=i; control 94 (layer 0, non-touchable) recreated at the cell rect with ui_iap_selectFrame. Touch ended (2): for each cell containing the point whose index == selection: OSShowActivityIndicator(320,480,1001), control 10 (layer 3, touchable) = sd_baiFrame image rect (0,0,960+w_off, 640+0.5*h_off+h_off=736) colour A0...
- Rule `dc2.jigsaw.touch_drag_settle` (PROVEN, NATIVE (decompile OnTouchEvent 0xf57bc, OnUpdate 0xf93c4)): Type-5 touch only acts when control 18 exists. Down: last=start=touch, settle flag this+151=0, startGroup=cursor. Move (only if x changed): per group k, scale = 0.8 if world x < -200*s or > 200*s else 1 - 0.001*|x|/s (computed from the OLD x); group x += (touchX - lastX); sizes scale*500 and the 4x4 pieces recomputed with the same formulas; the group with the smallest OLD |x| becomes the cursor. Up: ignored if x == s...

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/SelectBank.cs:9`](../../../../../Assets/Scripts/SelectBank.cs#L9) cites `0xF93C4`
- [`Assets/Scripts/SelectJigsaw.cs:9`](../../../../../Assets/Scripts/SelectJigsaw.cs#L9) cites `0xF93C4`

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: [`selectScene::iapaddmoney`](../../../functions/dossiers/selectScene/FUN-0F46D2.md), [`selectScene::showJigsawInfo`](../../../functions/dossiers/selectScene/FUN-0F47A4.md), [`selectScene::movePicTOLigsaw`](../../../functions/dossiers/selectScene/FUN-0F8D3E.md), `Triniti2D::Console::WriteLine`, `Triniti2D::GameObject::GetPosition`, `Triniti2D::GameObject::SetPosition`, `Triniti2D::GameObject::SetSize`, `Triniti2D::GameObject::SetPosition`, `Triniti2D::OSHideActivityIndicator`, `Triniti2D::Scene::GetUIManager`, `Triniti2D::UIManager::DeleteControl`, `Triniti2D::IAPManager::GetPurchaseStatus`, `Triniti2D::WebGameClient::RecvResponse`, `___switch32`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___divsf3vfp`, `___fixsfsivfp`, `___floatsisfvfp`, `___gtsf2vfp`, `___ltsf2vfp`, `___mulsf3vfp`, `___subsf3vfp`

## Strings it references

- `0x2074FC` "success \| requestId %d \| result %d \| name %s \| action %s \| data %s"
- `0x20A57C` "-- time out \| requestId %d \| result %d \| name %s \| action %s \| data %s"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x2280B4` GCC_except_table36_2
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A680` __ZN7DinoCap9requestIdE_ptr
- `0x23B91C` DinoCap::requestId
- `0x22A320` __ZN9Triniti2D6ModuleINS_13WebGameClientEE10m_instanceE_ptr
- `0x22A4AC` __ZN7DinoCap10m_map_typeE_ptr
- `0x23E0A0` DinoCap::m_map_type
- `0x22A49C` __ZN7DinoCap15g_iphone_retinaE_ptr
- `0x23B905` DinoCap::g_iphone_retina
- `0x22AB98` __ZNKSt6vectorIPN7DinoCap16jigsawGroupStateESaIS2_EE4sizeEv_ptr
- `0x22ABE0` __ZNKSt6vectorIPN7DinoCap14jigsawPicStateESaIS2_EE4sizeEv_ptr
- `0x22A108` __ZNKSt6vectorIN9Triniti2D7Vector2ESaIS1_EE4sizeEv_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xF93C4   f0b5       PUSH    {R4-R7,LR}
0xF93C6   5e46       MOV     R6, R11
0xF93C8   5546       MOV     R5, R10
0xF93CA   4446       MOV     R4, R8
0xF93CC   70b4       PUSH    {R4-R6}
0xF93CE   eef088ed   BLX     ___save_vfp_d8_d15_regs
0xF93D2   16af       ADD     R7, SP, #arg_38
0xF93D4   c5b0       SUB     SP, SP, #0x114
0xF93D6   da4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xF93E0)
0xF93D8   0122       MOVS    R2, #1
0xF93DA   1690       STR     R0, [SP,#0x114+var_BC]
0xF93DC   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xF93DE   6946       MOV     R1, SP
0xF93E0   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xF93E2   30a8       ADD     R0, SP, #0x114+fctx; lpfctx
0xF93E4   3897       STR     R7, [SP,#0x114+fctx.jbuf]
0xF93E6   3693       STR     R3, [SP,#0x114+fctx.personality]
0xF93E8   d64b       LDR     R3, =(GCC_except_table36_2 - 0xF93F0)
0xF93EA   3a91       STR     R1, [SP,#0x114+var_2C]
0xF93EC   7b44       ADD     R3, PC; GCC_except_table36_2
0xF93EE   3793       STR     R3, [SP,#0x114+fctx.lsda]
0xF93F0   d54b       LDR     R3, =(loc_F94D6 - 0xF93F6)
0xF93F2   7b44       ADD     R3, PC; loc_F94D6
0xF93F4   1343       ORRS    R3, R2
```

