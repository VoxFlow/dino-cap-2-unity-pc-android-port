# CAFrame::LoadXml

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-00F3A4` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xF3A4` |
| RVA (EA - 0x1000) | `0xE3A4` |
| File offset | `0xE3A4` |
| Size | 236 code bytes in 1 chunk(s), 107 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap7CAFrame7LoadXmlEPNS_9TiXmlNodeE` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::CAFrame::LoadXml(DinoCap::TiXmlNode *)` |
| Bytes SHA-256 | `7cf8ac3681498367e91b60effccc0225927445eff25d5bbcbb640318f59e1346` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.

## Callers and callees (direct code references)

Callers: [`CombineAnimation::LoadXml`](../../../functions/dossiers/CombineAnimation/FUN-00F4B0.md)

Callees: `CASprite::CASprite`, [`CASprite::LoadXml`](../../../functions/dossiers/CASprite/FUN-00BDB4.md), `TiXmlNode::NextSiblingElement`, `TiXmlNode::FirstChildElement`, `TiXmlElement::QueryIntAttribute`, `operator new`, `___addsf3vfp`, `___gtsf2vfp`, `___ltsf2vfp`, `___mulsf3vfp`, `___subsf3vfp`

## Strings it references

- `0x201C0C` "frame"
- `0x201C14` "Sprite"

## Named data it references

- `0x201C0C` sel_frame_0
- `0x22A198` __ZNSt3mapISsPN7DinoCap8CASpriteESt4lessISsESaISt4pairIKSsS2_EEEixERS6__ptr
- `0x22A1C8` __ZNSt6vectorIPN7DinoCap8CASpriteESaIS2_EE9push_backERKS2__ptr
- `0x22A144` __ZSt4sortIN9__gnu_cxx17__normal_iteratorIPPN7DinoCap8CASpriteESt6vectorIS4_SaIS4_EEEEPFbPKS3_SB_EEvT_SE_T0__ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xF3A4    f0b5       PUSH    {R4-R7,LR}
0xF3A6    5e46       MOV     R6, R11
0xF3A8    5546       MOV     R5, R10
0xF3AA    4446       MOV     R4, R8
0xF3AC    70b4       PUSH    {R4-R6}
0xF3AE    06af       ADD     R7, SP, #0x18
0xF3B0    81b0       SUB     SP, SP, #4
0xF3B2    0b68       LDR     R3, [R1]
0xF3B4    8046       MOV     R8, R0
0xF3B6    081c       MOVS    R0, R1
0xF3B8    0c1c       MOVS    R4, R1
0xF3BA    db6a       LDR     R3, [R3,#0x2C]
0xF3BC    9847       BLX     R3
0xF3BE    3449       LDR     R1, =(sel_frame_0 - 0xF3C8); "frame"
0xF3C0    4246       MOV     R2, R8
0xF3C2    2432       ADDS    R2, #0x24 ; '$'; int *
0xF3C4    7944       ADD     R1, PC; "frame"
0xF3C6    b2f0ebfe   BL      __ZNK7DinoCap12TiXmlElement17QueryIntAttributeEPKcPi; DinoCap::TiXmlElement::QueryIntAttribute(char const*,int *)
0xF3CA    3249       LDR     R1, =(aSprite - 0xF3D2); "Sprite"
0xF3CC    201c       MOVS    R0, R4; this
0xF3CE    7944       ADD     R1, PC; "Sprite"
0xF3D0    b2f09afd   BL      __ZNK7DinoCap9TiXmlNode17FirstChildElementEPKc; DinoCap::TiXmlNode::FirstChildElement(char const*)
0xF3D4    0023       MOVS    R3, #0
0xF3D6    9b46       MOV     R11, R3
```

