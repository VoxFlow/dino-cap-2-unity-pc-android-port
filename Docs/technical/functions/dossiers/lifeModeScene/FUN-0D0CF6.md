# lifeModeScene::getJigsaw

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0D0CF6` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xD0CF6` |
| RVA (EA - 0x1000) | `0xCFCF6` |
| File offset | `0xCFCF6` |
| Size | 246 code bytes in 1 chunk(s), 112 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap13lifeModeScene9getJigsawEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::lifeModeScene::getJigsaw(void)` |
| Bytes SHA-256 | `040b0e84d79a82522e5a27dbc0a03c0982e891fbae53bf03aa8ffe7469939675` |
| Direct code callers | 2 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.jigsaw.new_piece_flow` (LIKELY, NATIVE (decompile movePicTOLigsaw 0xf8d3e, jigsawPicClass::OnUpdate 0x124722, ligsawPicCallBack 0xf514e, AddJigsawEff 0xf4f70, ligsawEffCallBack 0xf90de, AlignmentLigsaw 0xf909e; jigsaw_1.animation.acb)): Pending pieces are processed from the end of the list: AlignmentLigsaw(g,j) stores j in this+164; if cursor==g movePicTOLigsaw(j) else cursor=g and settle. movePicTOLigsaw: showJigsawInfo, GameObject (jigsawPicClass) with the piece frame, size 2x piece -> piece size and colour 00FFFFFF -> FFFFFFFF, position (0, -140*s) -> the slot, linear over 1.0 s; after r>1: Disappear and ligsawPicCallBack: piece enabled, PlaySoun...
- Rule `dc2.constants_audit.r118` (PROVEN, NATIVE): getJigsaw lowers the seed when the mean level > 4.5 (0x40900000 at 0xd0db6) and playerClass::JuPan y factor is 0.04 (0x3D23D70A at 0xbca6c); earlier 5.5 / 0.043 were wrong.

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameMission.cs:218`](../../../../../Assets/Scripts/GameMission.cs#L218) cites `0xD0CF6`
- [`Assets/Scripts/GameMission.cs:226`](../../../../../Assets/Scripts/GameMission.cs#L226) cites `0xD0CF6`
- [`Assets/Scripts/GameMission.cs:228`](../../../../../Assets/Scripts/GameMission.cs#L228) cites `0xD0DB6`

## Callers and callees (direct code references)

Callers: [`lifeModeScene::missionComplete`](../../../functions/dossiers/lifeModeScene/FUN-0E1704.md)

Callees: [`getJigsawPicID`](../../../functions/dossiers/DinoCap/FUN-05A6B0.md), `___addsf3vfp`, `___divsf3vfp`, `___fixsfsivfp`, `___floatsisfvfp`, `___gtsf2vfp`, `___ltsf2vfp`, `___nesf2vfp`

## Named data it references

- `0x22A694` __ZN7DinoCap12g_jigsaw_getE_ptr
- `0x23E0A8` DinoCap::g_jigsaw_get
- `0x22A5C8` __ZN7DinoCap15g_jigsaw_levelsE_ptr
- `0x240A6C` DinoCap::g_jigsaw_levels
- `0x22A2A8` __ZNKSt6vectorIiSaIiEE4sizeEv_ptr
- `0x22A614` __ZN7DinoCap10jigsawSeedE_ptr
- `0x23B8E0` DinoCap::jigsawSeed

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xD0CF6   f0b5       PUSH    {R4-R7,LR}
0xD0CF8   5e46       MOV     R6, R11
0xD0CFA   5546       MOV     R5, R10
0xD0CFC   4446       MOV     R4, R8
0xD0CFE   70b4       PUSH    {R4-R6}
0xD0D00   06af       ADD     R7, SP, #0x18
0xD0D02   85b0       SUB     SP, SP, #0x14
0xD0D04   0090       STR     R0, [SP,#0x14+var_14]
0xD0D06   03a8       ADD     R0, SP, #0x14+var_8; this
0xD0D08   89f7d2fc   BL      __ZN7DinoCap14getJigsawPicIDEv; DinoCap::getJigsawPicID(void)
0xD0D0C   0399       LDR     R1, [SP,#0x14+var_8]
0xD0D0E   049a       LDR     R2, [SP,#0x14+var_4]
0xD0D10   0191       STR     R1, [SP,#0x14+var_10]
0xD0D12   0292       STR     R2, [SP,#0x14+var_C]
0xD0D14   019d       LDR     R5, [SP,#0x14+var_10]
0xD0D16   3549       LDR     R1, =0xBF800000
0xD0D18   281c       MOVS    R0, R5
0xD0D1A   19f188e8   BLX     ___nesf2vfp
0xD0D1E   0028       CMP     R0, #0
0xD0D20   5ed0       BEQ     loc_D0DE0
0xD0D22   029c       LDR     R4, [SP,#0x14+var_C]
0xD0D24   3149       LDR     R1, =0xBF800000
0xD0D26   201c       MOVS    R0, R4
0xD0D28   19f180e8   BLX     ___nesf2vfp
```

**Around `0xD0DB6` (cited by the Unity code)**

```
0xD0DAA   7b44       ADD     R3, PC; __ZN7DinoCap10jigsawSeedE_ptr
0xD0DAC   1a68       LDR     R2, [R3]; DinoCap::jigsawSeed ...
0xD0DAE   1368       LDR     R3, [R2]; DinoCap::jigsawSeed
0xD0DB0   0133       ADDS    R3, #1
0xD0DB2   0ae0       B       loc_D0DCA
0xD0DB4   201c       MOVS    R0, R4
0xD0DB6   1549       LDR     R1, =0x40900000   <==
0xD0DB8   18f1f6ef   BLX     ___gtsf2vfp
0xD0DBC   0028       CMP     R0, #0
0xD0DBE   05d0       BEQ     loc_D0DCC
0xD0DC0   134b       LDR     R3, =(__ZN7DinoCap10jigsawSeedE_ptr - 0xD0DC6)
0xD0DC2   7b44       ADD     R3, PC; __ZN7DinoCap10jigsawSeedE_ptr
0xD0DC4   1a68       LDR     R2, [R3]; DinoCap::jigsawSeed ...
```

