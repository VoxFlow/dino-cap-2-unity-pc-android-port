# selectScene::AlignmentLigsaw

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0F909E` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xF909E` |
| RVA (EA - 0x1000) | `0xF809E` |
| File offset | `0xF809E` |
| Size | 64 code bytes in 1 chunk(s), 29 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene15AlignmentLigsawEN9Triniti2D7Vector2E` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::AlignmentLigsaw(Triniti2D::Vector2)` |
| Bytes SHA-256 | `009864e0a149d5bef2dc88237da23e4beb614517a9feefc2a2885c92d42f0886` |
| Direct code callers | 2 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.jigsaw.onload_layout` (PROVEN, NATIVE (disasm OnLoad case 5 0x1064c6; decompile showJigsawUI 0xfdf70, showJigsawCount 0xfec34)): iPad (g_iphone==0): loadImageByConf Image/ip/jigsaw_h, jigsaw_h_1, jigeff2x; GameObject background ui_bg_allFrame sized (960+g_w_offset,640+g_h_offset) at world (0,0); cursor this+156=0; TadShow(false); showJigsawCount: ReadFile, pending list = pieces with g_jigsaw_active != g_jigsaw_get and get != 0 (ascending group,piece). Pending => label 599 (Adventure_22, RGB(0xEF,0xCD,0), align (1,1), rect (0,124,480,40) via Re...
- Rule `dc2.jigsaw.new_piece_flow` (LIKELY, NATIVE (decompile movePicTOLigsaw 0xf8d3e, jigsawPicClass::OnUpdate 0x124722, ligsawPicCallBack 0xf514e, AddJigsawEff 0xf4f70, ligsawEffCallBack 0xf90de, AlignmentLigsaw 0xf909e; jigsaw_1.animation.acb)): Pending pieces are processed from the end of the list: AlignmentLigsaw(g,j) stores j in this+164; if cursor==g movePicTOLigsaw(j) else cursor=g and settle. movePicTOLigsaw: showJigsawInfo, GameObject (jigsawPicClass) with the piece frame, size 2x piece -> piece size and colour 00FFFFFF -> FFFFFFFF, position (0, -140*s) -> the slot, linear over 1.0 s; after r>1: Disappear and ligsawPicCallBack: piece enabled, PlaySoun...

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/SelectJigsaw.cs:9`](../../../../../Assets/Scripts/SelectJigsaw.cs#L9) cites `0xF909E`

## Callers and callees (direct code references)

Callers: [`selectScene::ligsawEffCallBack`](../../../functions/dossiers/selectScene/FUN-0F90DE.md), [`selectScene::showJigsawUI`](../../../functions/dossiers/selectScene/FUN-0FDF70.md)

Callees: [`selectScene::movePicTOLigsaw`](../../../functions/dossiers/selectScene/FUN-0F8D3E.md), `___fixsfsivfp`

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xF909E   f0b5       PUSH    {R4-R7,LR}
0xF90A0   4646       MOV     R6, R8
0xF90A2   40b4       PUSH    {R6}
0xF90A4   04af       ADD     R7, SP, #0x10
0xF90A6   061c       MOVS    R6, R0
0xF90A8   081c       MOVS    R0, R1
0xF90AA   151c       MOVS    R5, R2
0xF90AC   f0f03aee   BLX     ___fixsfsivfp
0xF90B0   8046       MOV     R8, R0
0xF90B2   281c       MOVS    R0, R5
0xF90B4   f0f036ee   BLX     ___fixsfsivfp
0xF90B8   a423       MOVS    R3, #0xA4
0xF90BA   9c22       MOVS    R2, #0x9C
0xF90BC   f050       STR     R0, [R6,R3]
0xF90BE   b358       LDR     R3, [R6,R2]
0xF90C0   011c       MOVS    R1, R0; int
0xF90C2   4345       CMP     R3, R8
0xF90C4   05d0       BEQ     loc_F90D2
0xF90C6   4346       MOV     R3, R8
0xF90C8   b350       STR     R3, [R6,R2]
0xF90CA   9723       MOVS    R3, #0x97
0xF90CC   0122       MOVS    R2, #1
0xF90CE   f254       STRB    R2, [R6,R3]
0xF90D0   02e0       B       loc_F90D8
```

