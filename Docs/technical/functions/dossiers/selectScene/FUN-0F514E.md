# selectScene::ligsawPicCallBack

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0F514E` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xF514E` |
| RVA (EA - 0x1000) | `0xF414E` |
| File offset | `0xF414E` |
| Size | 150 code bytes in 1 chunk(s), 67 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene17ligsawPicCallBackEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::ligsawPicCallBack(void)` |
| Bytes SHA-256 | `a83b033d78454cbea9e784555273d3b339cff6f8dc3f4c894f53a0c43a449721` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.jigsaw.new_piece_flow` (LIKELY, NATIVE (decompile movePicTOLigsaw 0xf8d3e, jigsawPicClass::OnUpdate 0x124722, ligsawPicCallBack 0xf514e, AddJigsawEff 0xf4f70, ligsawEffCallBack 0xf90de, AlignmentLigsaw 0xf909e; jigsaw_1.animation.acb)): Pending pieces are processed from the end of the list: AlignmentLigsaw(g,j) stores j in this+164; if cursor==g movePicTOLigsaw(j) else cursor=g and settle. movePicTOLigsaw: showJigsawInfo, GameObject (jigsawPicClass) with the piece frame, size 2x piece -> piece size and colour 00FFFFFF -> FFFFFFFF, position (0, -140*s) -> the slot, linear over 1.0 s; after r>1: Disappear and ligsawPicCallBack: piece enabled, PlaySoun...

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/SelectJigsaw.cs:10`](../../../../../Assets/Scripts/SelectJigsaw.cs#L10) cites `0xF514E`
- [`Assets/Scripts/SelectJigsaw.cs:365`](../../../../../Assets/Scripts/SelectJigsaw.cs#L365) cites `0xF514E`

## Callers and callees (direct code references)

Callers: [`jigsawPicClass::OnUpdate`](../../../functions/dossiers/jigsawPicClass/FUN-124722.md)

Callees: [`PlaySound`](../../../functions/dossiers/DinoCap/FUN-01A448.md), [`selectScene::showJigsawInfo`](../../../functions/dossiers/selectScene/FUN-0F47A4.md), [`selectScene::AddJigsawEff`](../../../functions/dossiers/selectScene/FUN-0F4F70.md), `Triniti2D::GameObject::Enable`, `Triniti2D::GameObject::GetPosition`, `___fixsfsivfp`

## Strings it references

- `0x20555C` "A_jigsaw_moveend"

## Named data it references

- `0x22A108` __ZNKSt6vectorIN9Triniti2D7Vector2ESaIS1_EE4sizeEv_ptr
- `0x22AB78` __ZN7DinoCap15g_jigsaw_activeE_ptr
- `0x23E0F8` DinoCap::g_jigsaw_active

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xF514E   f0b5       PUSH    {R4-R7,LR}
0xF5150   5646       MOV     R6, R10
0xF5152   4546       MOV     R5, R8
0xF5154   60b4       PUSH    {R5,R6}
0xF5156   05af       ADD     R7, SP, #0x14
0xF5158   224b       LDR     R3, =(__ZNKSt6vectorIN9Triniti2D7Vector2ESaIS1_EE4sizeEv_ptr - 0xF5162)
0xF515A   061c       MOVS    R6, R0
0xF515C   e030       ADDS    R0, #0xE0
0xF515E   7b44       ADD     R3, PC; __ZNKSt6vectorIN9Triniti2D7Vector2ESaIS1_EE4sizeEv_ptr
0xF5160   1b68       LDR     R3, [R3]
0xF5162   9847       BLX     R3
0xF5164   e023       MOVS    R3, #0xE0
0xF5166   f358       LDR     R3, [R6,R3]
0xF5168   0138       SUBS    R0, #1
0xF516A   c000       LSLS    R0, R0, #3
0xF516C   c018       ADDS    R0, R0, R3
0xF516E   0468       LDR     R4, [R0]
0xF5170   4568       LDR     R5, [R0,#4]
0xF5172   201c       MOVS    R0, R4
0xF5174   f4f0d6ed   BLX     ___fixsfsivfp
0xF5178   8046       MOV     R8, R0
0xF517A   281c       MOVS    R0, R5
0xF517C   f4f0d2ed   BLX     ___fixsfsivfp
0xF5180   cc235b00   MOVS    R3, #0x198
```

