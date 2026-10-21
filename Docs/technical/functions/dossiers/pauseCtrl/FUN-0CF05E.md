# pauseCtrl::Initialize

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0CF05E` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xCF05E` |
| RVA (EA - 0x1000) | `0xCE05E` |
| File offset | `0xCE05E` |
| Size | 1702 code bytes in 1 chunk(s), 746 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9pauseCtrl10InitializeEPN9Triniti2D11UIContainerEii` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::pauseCtrl::Initialize(Triniti2D::UIContainer *,int,int)` |
| Bytes SHA-256 | `de7098ec7d21c69191bd307445bc50764e92648ffdee2f43f0795e8240c4091d` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameControls.cs:11`](../../../../../Assets/Scripts/GameControls.cs#L11) cites `0xCF05E`
- [`Assets/Scripts/GameControls.cs:466`](../../../../../Assets/Scripts/GameControls.cs#L466) cites `0xCF05E`

## Callers and callees (direct code references)

Callers: none found (it may be called through a virtual table or function pointer).

Callees: `UIMoveCtrl::UIMoveCtrl`, `Triniti2D::UIImage::SetColor`, `Triniti2D::UIImage::SetTexture`, `Triniti2D::UIImage::Initialize`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `operator delete`, `operator new`, `___addsf3vfp`, `___mulsf3vfp`

## Strings it references

- `0x2064B4` "whiteFrame"
- `0x2070C4` "op_titleFrame"
- `0x207040` "op_musicFrame"
- `0x2070A4` "op_soundFrame"
- `0x207050` "op_on1Frame"
- `0x206FF4` "ui_btn_resumeFrame"
- `0x206FD0` "ui_btn_retryFrame"
- `0x206FE4` "ui_btn_mapFrame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x227506` GCC_except_table7_9
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone
- `0x22A5F8` __ZN7DinoCap10g_h_offsetE_ptr
- `0x23B918` DinoCap::g_h_offset
- `0x22A634` __ZN7DinoCap10g_w_offsetE_ptr
- `0x23B914` DinoCap::g_w_offset
- `0x22AA34` __ZNSt6vectorIPN7DinoCap10UIMoveCtrlESaIS2_EE9push_backERKS2__ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xCF05E   f0b5       PUSH    {R4-R7,LR}
0xCF060   5e46       MOV     R6, R11
0xCF062   5546       MOV     R5, R10
0xCF064   4446       MOV     R4, R8
0xCF066   70b4       PUSH    {R4-R6}
0xCF068   18f13aef   BLX     ___save_vfp_d8_d15_regs
0xCF06C   16af       ADD     R7, SP, #arg_38
0xCF06E   c0b0       SUB     SP, SP, #0x100
0xCF070   0293       STR     R3, [SP,#0x100+var_F8]
0xCF072   e54b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xCF07C)
0xCF074   0392       STR     R2, [SP,#0x100+var_F4]
0xCF076   6a46       MOV     R2, SP
0xCF078   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xCF07A   2792       STR     R2, [SP,#0x100+var_64]
0xCF07C   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xCF07E   0122       MOVS    R2, #1
0xCF080   0590       STR     R0, [SP,#0x100+var_EC]
0xCF082   2393       STR     R3, [SP,#0x100+fctx.personality]
0xCF084   e14b       LDR     R3, =(GCC_except_table7_9 - 0xCF08E)
0xCF086   1da8       ADD     R0, SP, #0x100+fctx; lpfctx
0xCF088   0491       STR     R1, [SP,#0x100+var_F0]
0xCF08A   7b44       ADD     R3, PC; GCC_except_table7_9
0xCF08C   2493       STR     R3, [SP,#0x100+fctx.lsda]
0xCF08E   e04b       LDR     R3, =(loc_CF626 - 0xCF096)
```

