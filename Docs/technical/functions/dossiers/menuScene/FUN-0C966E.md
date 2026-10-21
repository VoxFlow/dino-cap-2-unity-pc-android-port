# menuScene::GetBaozha

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0C966E` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xC966E` |
| RVA (EA - 0x1000) | `0xC866E` |
| File offset | `0xC866E` |
| Size | 264 code bytes in 1 chunk(s), 121 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9menuScene9GetBaozhaEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::menuScene::GetBaozha(void)` |
| Bytes SHA-256 | `2db756038abd5b793954da4fb6028563244c0a28afdb19931b30237e2ddeac4e` |
| Direct code callers | 8 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UI_FLOW / PORTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: menuScene::GetBaozha (menuObj/menuObjN OnKeyFrame): title explosion pool, MenuScene.cs / LoadingAnimation.cs

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: [`menuObj::OnKeyFrame`](../../../functions/dossiers/menuObj/FUN-0CB0EA.md), [`menuObjN::OnKeyFrame`](../../../functions/dossiers/menuObjN/FUN-0CB4FE.md)

Callees: `Triniti2D::GameObject::GetCallBack`, `Triniti2D::Scene::CreateGameObject`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::string`, `std::string::~string`, `___dynamic_cast`

## Strings it references

- `0x207334` "loadEffClass"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x227366` GCC_except_table1_9
- `0x22A0A0` __ZNKSt6vectorIPN9Triniti2D10GameObjectESaIS2_EE4sizeEv_ptr
- `0x22A6F8` __ZNSt6vectorIPN9Triniti2D10GameObjectESaIS2_EE4backEv_ptr
- `0x22A0A8` __ZTIN9Triniti2D18GameObjectCallBackE_ptr
- `0x22AA20` __ZTIN7DinoCap12loadEffClassE_ptr
- `0x22C7FC` `typeinfo for'DinoCap::loadEffClass
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xC966E   f0b5       PUSH    {R4-R7,LR}
0xC9670   5e46       MOV     R6, R11
0xC9672   5546       MOV     R5, R10
0xC9674   4446       MOV     R4, R8
0xC9676   70b4       PUSH    {R4-R6}
0xC9678   1ef132ec   BLX     ___save_vfp_d8_d15_regs
0xC967C   16af       ADD     R7, SP, #arg_38
0xC967E   9ab0       SUB     SP, SP, #0x68 ; 'h'
0xC9680   3d4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xC968A)
0xC9682   6a46       MOV     R2, SP
0xC9684   1592       STR     R2, [SP,#0x68+var_14]
0xC9686   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xC9688   0122       MOVS    R2, #1
0xC968A   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xC968C   0690       STR     R0, [SP,#0x68+var_50]
0xC968E   0ba8       ADD     R0, SP, #0x68+fctx; lpfctx
0xC9690   1193       STR     R3, [SP,#0x68+fctx.personality]
0xC9692   3a4b       LDR     R3, =(GCC_except_table1_9 - 0xC969A)
0xC9694   1397       STR     R7, [SP,#0x68+fctx.jbuf]
0xC9696   7b44       ADD     R3, PC; GCC_except_table1_9
0xC9698   1293       STR     R3, [SP,#0x68+fctx.lsda]
0xC969A   394b       LDR     R3, =(loc_C973E - 0xC96A0)
0xC969C   7b44       ADD     R3, PC; loc_C973E
0xC969E   1343       ORRS    R3, R2
```

