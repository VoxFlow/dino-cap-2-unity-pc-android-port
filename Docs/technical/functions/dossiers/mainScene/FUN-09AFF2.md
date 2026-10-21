# mainScene::InitBackGround

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-09AFF2` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x9AFF2` |
| RVA (EA - 0x1000) | `0x99FF2` |
| File offset | `0x99FF2` |
| Size | 762 code bytes in 1 chunk(s), 302 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap9mainScene14InitBackGroundEi` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::mainScene::InitBackGround(int)` |
| Bytes SHA-256 | `5418431316a373585cbffdecedefb7275f8ca2d39059625990014285a144a21a` |
| Direct code callers | 4 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | GAMEPLAY / VERIFIED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.mainscene.scenery.r118` (SUPERSEDED, NATIVE): mainScene::InitBackGround 0x9aff2 adds per-theme decorative GameObjects along run/egg missions (themes 1,3,4,5 via the four call sites in mainScene::OnLoad); group counts, templates, strides and RandomRange draws as in mainscene_scenery_r118.md; derived by bounded ARM emulation, validated 112/112. lifeModeScene::InitBackGround 0xd6a0e is an identical copy (20/20 emulation comparisons) called for variants 1/3/4/5 with...

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameScene.cs:151`](../../../../../Assets/Scripts/GameScene.cs#L151) cites `0x9AFF2`
- [`Assets/Scripts/GameScenery.cs:17`](../../../../../Assets/Scripts/GameScenery.cs#L17) cites `0x9AFF2`
- [`Assets/Scripts/GameScenery.cs:20`](../../../../../Assets/Scripts/GameScenery.cs#L20) cites `0x9AFF2`

## Callers and callees (direct code references)

Callers: `sub_B1D9A`, `sub_B3BE0`

Callees: `sub_9CE0C`, `___save_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `___addsf3vfp`, `___divsf3vfp`, `___fixsfsivfp`, `___floatsisfvfp`, `___mulsf3vfp`

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x2267D7` GCC_except_table30_0
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x9AFF2   f0b5       PUSH    {R4-R7,LR}
0x9AFF4   5e46       MOV     R6, R11
0x9AFF6   5546       MOV     R5, R10
0x9AFF8   4446       MOV     R4, R8
0x9AFFA   70b4       PUSH    {R4-R6}
0x9AFFC   4cf170ef   BLX     ___save_vfp_d8_d15_regs
0x9B000   fb4c       LDR     R4, =0xFFFFF568
0x9B002   16af       ADD     R7, SP, #arg_38
0x9B004   a544       ADD     SP, R4
0x9B006   fb4b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x9B010)
0x9B008   d991       STR     R1, [SP,#0xA98+var_734]
0x9B00A   fb49       LDR     R1, =0x87C
0x9B00C   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x9B00E   da90       STR     R0, [SP,#0xA98+var_730]
0x9B010   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x9B012   6944       ADD     R1, SP
0x9B014   8822       MOVS    R2, #0x88
0x9B016   0b60       STR     R3, [R1]
0x9B018   f84b       LDR     R3, =(GCC_except_table30_0 - 0x9B022)
0x9B01A   1201       LSLS    R2, R2, #4
0x9B01C   6a44       ADD     R2, SP
0x9B01E   7b44       ADD     R3, PC; GCC_except_table30_0
0x9B020   1360       STR     R3, [R2]
0x9B022   f74b       LDR     R3, =0x884
```

