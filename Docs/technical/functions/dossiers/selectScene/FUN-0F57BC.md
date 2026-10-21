# selectScene::OnTouchEvent

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0F57BC` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xF57BC` |
| RVA (EA - 0x1000) | `0xF47BC` |
| File offset | `0xF47BC` |
| Size | 1730 code bytes in 1 chunk(s), 737 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene12OnTouchEventEiiii` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::OnTouchEvent(int,int,int,int)` |
| Bytes SHA-256 | `45eec4107919d7969a28441b7df32bdc42151772a2b7730d67ed89c98e54f5e4` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.bank.touch_purchase_flow` (PROVEN, NATIVE (decompile selectScene::OnTouchEvent 0xf57bc, OnUpdate 0xf93c4, iapaddmoney 0xf46d2, IAPPurchase 0x5a1f0)): Touch began (0): for every cell where Rect::PtInRect (strict inequalities) contains the point, this+0x28=i; control 94 (layer 0, non-touchable) recreated at the cell rect with ui_iap_selectFrame. Touch ended (2): for each cell containing the point whose index == selection: OSShowActivityIndicator(320,480,1001), control 10 (layer 3, touchable) = sd_baiFrame image rect (0,0,960+w_off, 640+0.5*h_off+h_off=736) colour A0...
- Rule `dc2.jigsaw.touch_drag_settle` (PROVEN, NATIVE (decompile OnTouchEvent 0xf57bc, OnUpdate 0xf93c4)): Type-5 touch only acts when control 18 exists. Down: last=start=touch, settle flag this+151=0, startGroup=cursor. Move (only if x changed): per group k, scale = 0.8 if world x < -200*s or > 200*s else 1 - 0.001*|x|/s (computed from the OLD x); group x += (touchX - lastX); sizes scale*500 and the 4x4 pieces recomputed with the same formulas; the group with the smallest OLD |x| becomes the cursor. Up: ignored if x == s...
- Rule `dc2.ui.image_touch_consume.r118z4` (PROVEN, NATIVE): UIManager::HandleUITouch 0x18102e tests only GetEnable (vtable +56, UIControl +0x21, default 1) - not visibility - before HandleTouch (vtable +84). UIImage::HandleTouch 0x17f1ac and UILabel::HandleTouch 0x17fd98 return UIControl::PtInRect 0x17e70c: half-open left <= x < left+w, top <= y < top+h, and inside the clip rect (+0x24) when the clip flag (+0x22) is set. So every enabled plain image or label consumes every to...

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/SelectBank.cs:9`](../../../../../Assets/Scripts/SelectBank.cs#L9) cites `0xF57BC`
- [`Assets/Scripts/SelectBank.cs:114`](../../../../../Assets/Scripts/SelectBank.cs#L114) (by name)
- [`Assets/Scripts/SelectBank.cs:148`](../../../../../Assets/Scripts/SelectBank.cs#L148) cites `0xF5D46`
- [`Assets/Scripts/SelectJigsaw.cs:9`](../../../../../Assets/Scripts/SelectJigsaw.cs#L9) cites `0xF57BC`

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: [`PlaySound`](../../../functions/dossiers/DinoCap/FUN-01A448.md), [`IAPPurchase`](../../../functions/dossiers/DinoCap/FUN-05A1F0.md), [`selectScene::showJigsawInfo`](../../../functions/dossiers/selectScene/FUN-0F47A4.md), `Triniti2D::GameObject::GetPosition`, `Triniti2D::GameObject::SetPosition`, `Triniti2D::GameObject::SetSize`, `Triniti2D::GameObject::SetPosition`, `Triniti2D::OSShowActivityIndicator`, `Triniti2D::Scene::GetUIManager`, `Triniti2D::UIImage::SetColor`, `Triniti2D::UIImage::SetTexture`, `Triniti2D::UIManager::GetControl`, `Triniti2D::UIManager::CreateImage`, `Triniti2D::UIManager::DeleteControl`, `Triniti2D::IAPManager::Reset`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___divsf3vfp`, `___fixsfsivfp`, `___floatsisfvfp`, `___gtsf2vfp`, `___lesf2vfp`, `___ltsf2vfp`, `___mulsf3vfp`, `___nesf2vfp`, `___subsf3vfp`, `___unordsf2vfp`

## Strings it references

- `0x20552C` "A_jigsaw_touchend"
- `0x209E94` "ui_iap_selectFrame"
- `0x206D7C` "sd_baiFrame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x227F90` GCC_except_table27_4
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A4AC` __ZN7DinoCap10m_map_typeE_ptr
- `0x23E0A0` DinoCap::m_map_type
- `0x22AB98` __ZNKSt6vectorIPN7DinoCap16jigsawGroupStateESaIS2_EE4sizeEv_ptr
- `0x22ABE0` __ZNKSt6vectorIPN7DinoCap14jigsawPicStateESaIS2_EE4sizeEv_ptr
- `0x22AB88` __ZNKSt6vectorIN9Triniti2D4RectESaIS1_EE4sizeEv_ptr
- `0x22A2E8` __ZNK9Triniti2D4Rect8PtInRectERKNS_7Vector2E_ptr
- `0x22A5F8` __ZN7DinoCap10g_h_offsetE_ptr
- `0x23B918` DinoCap::g_h_offset
- `0x22A634` __ZN7DinoCap10g_w_offsetE_ptr
- `0x23B914` DinoCap::g_w_offset

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xF57BC   f0b5       PUSH    {R4-R7,LR}
0xF57BE   5e46       MOV     R6, R11
0xF57C0   5546       MOV     R5, R10
0xF57C2   4446       MOV     R4, R8
0xF57C4   70b4       PUSH    {R4-R6}
0xF57C6   f2f08ceb   BLX     ___save_vfp_d8_d15_regs
0xF57CA   16af       ADD     R7, SP, #arg_38
0xF57CC   d6b0       SUB     SP, SP, #0x158
0xF57CE   1493       STR     R3, [SP,#0x158+var_108]
0xF57D0   e04b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xF57DA)
0xF57D2   1690       STR     R0, [SP,#0x158+var_100]
0xF57D4   1592       STR     R2, [SP,#0x158+var_104]
0xF57D6   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xF57D8   0122       MOVS    R2, #1
0xF57DA   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xF57DC   6846       MOV     R0, SP
0xF57DE   4790       STR     R0, [SP,#0x158+var_3C]
0xF57E0   4393       STR     R3, [SP,#0x158+fctx.personality]
0xF57E2   dd4b       LDR     R3, =(GCC_except_table27_4 - 0xF57EC)
0xF57E4   3da8       ADD     R0, SP, #0x158+fctx; lpfctx
0xF57E6   4597       STR     R7, [SP,#0x158+fctx.jbuf]
0xF57E8   7b44       ADD     R3, PC; GCC_except_table27_4
0xF57EA   4493       STR     R3, [SP,#0x158+fctx.lsda]
0xF57EC   db4b       LDR     R3, =(loc_F5DD8 - 0xF57F2)
```

**Around `0xF5D46` (cited by the Unity code)**

```
0xF5D36   79f0d3fa   BL      __ZN9Triniti2D23OSShowActivityIndicatorEiii; Triniti2D::OSShowActivityIndicator(int,int,int)
0xF5D3A   169a       LDR     R2, [SP,#0x158+var_100]
0xF5D3C   5068       LDR     R0, [R2,#4]; this
0xF5D3E   7ff094fa   BL      __ZN9Triniti2D5Scene12GetUIManagerEv; Triniti2D::Scene::GetUIManager(void)
0xF5D42   0a21       MOVS    R1, #0xA; int
0xF5D44   0322       MOVS    R2, #3; int
0xF5D46   8bf081fb   BL      __ZN9Triniti2D9UIManager11CreateImageEii; Triniti2D::UIManager::CreateImage(int,int)   <==
0xF5D4A   169b       LDR     R3, [SP,#0x158+var_100]
0xF5D4C   1863       STR     R0, [R3,#0x30]
0xF5D4E   594b       LDR     R3, =0
0xF5D50   3998       LDR     R0, [SP,#0x158+var_74]
0xF5D52   3899       LDR     R1, [SP,#0x158+var_78]
0xF5D54   4b93       STR     R3, [SP,#0x158+var_2C]
```

