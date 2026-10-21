# lifeModeScene::InitBackGround

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0D6A0E` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xD6A0E` |
| RVA (EA - 0x1000) | `0xD5A0E` |
| File offset | `0xD5A0E` |
| Size | 762 code bytes in 1 chunk(s), 302 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap13lifeModeScene14InitBackGroundEi` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::lifeModeScene::InitBackGround(int)` |
| Bytes SHA-256 | `53f06f4a74e36dbf877f260cfd4d284cb80a58bea9b9e2c8c21fa53e0b23bda8` |
| Direct code callers | 4 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / VERIFIED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.mainscene.scenery.r118` (SUPERSEDED, NATIVE): mainScene::InitBackGround 0x9aff2 adds per-theme decorative GameObjects along run/egg missions (themes 1,3,4,5 via the four call sites in mainScene::OnLoad); group counts, templates, strides and RandomRange draws as in mainscene_scenery_r118.md; derived by bounded ARM emulation, validated 112/112. lifeModeScene::InitBackGround 0xd6a0e is an identical copy (20/20 emulation comparisons) called for variants 1/3/4/5 with...

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameScene.cs:151`](../../../../../Assets/Scripts/GameScene.cs#L151) cites `0xD6A0E`
- [`Assets/Scripts/GameScenery.cs:21`](../../../../../Assets/Scripts/GameScenery.cs#L21) cites `0xD6A0E`

## Callers and callees (direct code references)

Callers: [`lifeModeScene::InitGameBackGround`](../../../functions/dossiers/lifeModeScene/FUN-0DCAA6.md), `sub_DE9FA`, `sub_DEA72`, `sub_DF352`

Callees: `sub_D8828`, `___save_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `___addsf3vfp`, `___divsf3vfp`, `___fixsfsivfp`, `___floatsisfvfp`, `___mulsf3vfp`

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x227783` GCC_except_table25_3
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xD6A0E   f0b5       PUSH    {R4-R7,LR}
0xD6A10   5e46       MOV     R6, R11
0xD6A12   5546       MOV     R5, R10
0xD6A14   4446       MOV     R4, R8
0xD6A16   70b4       PUSH    {R4-R6}
0xD6A18   11f162ea   BLX     ___save_vfp_d8_d15_regs
0xD6A1C   fb4c       LDR     R4, =0xFFFFF568
0xD6A1E   16af       ADD     R7, SP, #arg_38
0xD6A20   a544       ADD     SP, R4
0xD6A22   fb4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xD6A2C)
0xD6A24   d991       STR     R1, [SP,#0xA98+var_734]
0xD6A26   fb49       LDR     R1, =0x87C
0xD6A28   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xD6A2A   da90       STR     R0, [SP,#0xA98+var_730]
0xD6A2C   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xD6A2E   6944       ADD     R1, SP
0xD6A30   8822       MOVS    R2, #0x88
0xD6A32   0b60       STR     R3, [R1]
0xD6A34   f84b       LDR     R3, =(GCC_except_table25_3 - 0xD6A3E)
0xD6A36   1201       LSLS    R2, R2, #4
0xD6A38   6a44       ADD     R2, SP
0xD6A3A   7b44       ADD     R3, PC; GCC_except_table25_3
0xD6A3C   1360       STR     R3, [R2]
0xD6A3E   f74b       LDR     R3, =0x884
```

