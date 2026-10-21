# dinosaurProperty::LoadFromXml

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0C58FC` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xC58FC` |
| RVA (EA - 0x1000) | `0xC48FC` |
| File offset | `0xC48FC` |
| Size | 190 code bytes in 1 chunk(s), 66 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap16dinosaurProperty11LoadFromXmlEPNS_9TiXmlNodeE` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::dinosaurProperty::LoadFromXml(DinoCap::TiXmlNode *)` |
| Bytes SHA-256 | `cf480b4cf9c67cb2358c4ec824633a7b73cf2ccad879a572b9be2ad62098e900` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.data.dino_integer_fields.r13` (PROVEN, NATIVE): dinosaurProperty::LoadFromXml0xc58fc uses atoi on HP,SPD,EXP,FRQ,ATK,ADIS,SKIN,SURFACE. HP/SPD/FRQ/ATK/ADIS convert integer to float; EXP/SKIN/SURFACE int. FRQ3.2 becomes3. Pilot retains numeric values, raw skin/surface without inferring actor identity.

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/Dc2GameData.cs:123`](../../../../../Assets/Scripts/Dc2GameData.cs#L123) cites `0xC58FC`

## Callers and callees (direct code references)

Callers: `LoadDinosaurProperty`

Callees: `TiXmlNode::FirstChildElement`, `TiXmlElement::GetText`, `___floatsisfvfp`, `_atoi`

## Strings it references

- `0x208FD0` "HP"
- `0x208FD4` "SPD"
- `0x208FD8` "EXP"
- `0x208FDC` "FRQ"
- `0x208FE0` "ATK"
- `0x208FE4` "ADIS"
- `0x208FEC` "SKIN"
- `0x208FF4` "SURFACE"

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xC58FC   b0b5       PUSH    {R4,R5,R7,LR}
0xC58FE   02af       ADD     R7, SP, #8
0xC5900   0c1c       MOVS    R4, R1
0xC5902   2e49       LDR     R1, =(aHp - 0xC590C); "HP"
0xC5904   051c       MOVS    R5, R0
0xC5906   201c       MOVS    R0, R4; this
0xC5908   7944       ADD     R1, PC; "HP"
0xC590A   fcf7fdfa   BL      __ZNK7DinoCap9TiXmlNode17FirstChildElementEPKc; DinoCap::TiXmlNode::FirstChildElement(char const*)
0xC590E   fcf7b7fd   BL      __ZNK7DinoCap12TiXmlElement7GetTextEv; DinoCap::TiXmlElement::GetText(void)
0xC5912   24f134eb   BLX     _atoi
0xC5916   24f12aea   BLX     ___floatsisfvfp
0xC591A   2949       LDR     R1, =(aSpd - 0xC5920); "SPD"
0xC591C   7944       ADD     R1, PC; "SPD"
0xC591E   2860       STR     R0, [R5]
0xC5920   201c       MOVS    R0, R4; this
0xC5922   fcf7f1fa   BL      __ZNK7DinoCap9TiXmlNode17FirstChildElementEPKc; DinoCap::TiXmlNode::FirstChildElement(char const*)
0xC5926   fcf7abfd   BL      __ZNK7DinoCap12TiXmlElement7GetTextEv; DinoCap::TiXmlElement::GetText(void)
0xC592A   24f128eb   BLX     _atoi
0xC592E   24f11eea   BLX     ___floatsisfvfp
0xC5932   2449       LDR     R1, =(aExp_0 - 0xC5938); "EXP"
0xC5934   7944       ADD     R1, PC; "EXP"
0xC5936   6860       STR     R0, [R5,#4]
0xC5938   201c       MOVS    R0, R4; this
0xC593A   fcf7e5fa   BL      __ZNK7DinoCap9TiXmlNode17FirstChildElementEPKc; DinoCap::TiXmlNode::FirstChildElement(char const*)
```

