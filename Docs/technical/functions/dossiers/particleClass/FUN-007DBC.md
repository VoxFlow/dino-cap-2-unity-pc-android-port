# particleClass::OnUpdate

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-007DBC` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0x7DBC` |
| RVA (EA - 0x1000) | `0x6DBC` |
| File offset | `0x6DBC` |
| Size | 2272 code bytes in 19 chunk(s), 915 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap13particleClass8OnUpdateEf` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::particleClass::OnUpdate(float)` |
| Bytes SHA-256 | `5fda9d9bf9579a1d08cd8e3e30aff5a47eeefbb93971c20b51edc788a8ed1987` |
| Direct code callers | 0 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / VERIFIED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

No recovered rule in the published registry cites an address inside this function. Ledger note: none

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/NativeBlood.cs:28`](../../../../../Assets/Scripts/NativeBlood.cs#L28) cites `0x7DBC`
- [`Assets/Scripts/NativeBlood.cs:101`](../../../../../Assets/Scripts/NativeBlood.cs#L101) cites `0x7DBC`
- [`Assets/Scripts/NativeBlood.cs:196`](../../../../../Assets/Scripts/NativeBlood.cs#L196) cites `0x826A`

## Callers and callees (direct code references)

Callers: [`particleClass::OnUpdate`](../../../functions/dossiers/particleClass/FUN-007DBC.md)

Callees: [`particleClass::OnUpdate`](../../../functions/dossiers/particleClass/FUN-007DBC.md), `sub_83EE`, `sub_844C`, `sub_84D8`, `sub_8526`, `sub_8574`, `sub_85BE`, `sub_86AC`, `sub_86FA`, `sub_8744`, `sub_87AA`, `sub_87F6`, `sub_8846`, `sub_8896`, `sub_88E6`, `sub_892E`, `sub_8996`, `sub_89E6`, `sub_8A9C`, `sub_8AEC`, `sub_8B3E`, `sub_8B88`, `sub_8C16`, `sub_8C62`, `sub_8CB0`, `sub_8CFA`, `sub_96BE`, `sub_970A`, `sub_9716`, [`mainScene::GetParticle`](../../../functions/dossiers/mainScene/FUN-094C3E.md), `Triniti2D::GameObject::Enable`, `Triniti2D::GameObject::GetPosition`, `Triniti2D::GameObject::GetScene`, `Triniti2D::GameObject::Physics`, `Triniti2D::GameObject::Sprite`, `Triniti2D::GameObject::SetSize`, `Triniti2D::GameObject::SetPosition`, `Triniti2D::GamePhysics::SetLinearVelocity`, `Triniti2D::GamePhysics::GetLinearVelocity`, `Triniti2D::GamePhysics::SetAngularVelocity` ... (55)

## Strings it references

- `0x200E30` "xue2Frame"
- `0x200E3C` "xue5Frame"
- `0x200E48` "xue4Frame"
- `0x200E54` "xue6Frame"
- `0x200E60` "xue7Frame"
- `0x20109C` "suirou1Frame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x2238F2` GCC_except_table6
- `0x22A0E4` __ZN7DinoCap8g_iphoneE_ptr
- `0x23B904` DinoCap::g_iphone

## Code reached from this function outside every IDA function

IDA placed these blocks in no function. They are reached by a branch or fall-through from this function, which shows reachability, not ownership. They are listed here so that no reached code is silently missing.

- `0x8338`..`0x83A4` (reached from `0x8336`), bytes SHA-256 `1ca811d784c8f20c...`, 44 instructions in [`unowned_blocks.jsonl`](../../../graphs/unowned_blocks.jsonl)
- `0x8BB2`..`0x8BC0` (reached from `0x83A0`), bytes SHA-256 `39cad6f30e4efbd1...`, 6 instructions in [`unowned_blocks.jsonl`](../../../graphs/unowned_blocks.jsonl)
- `0x9054`..`0x908C` (reached from `0x8BBC`), bytes SHA-256 `f7136f6dfe4f7875...`, 25 instructions in [`unowned_blocks.jsonl`](../../../graphs/unowned_blocks.jsonl)
- `0x9094`..`0x90A2` (reached from `0x908A`), bytes SHA-256 `0e813f3304a8131a...`, 6 instructions in [`unowned_blocks.jsonl`](../../../graphs/unowned_blocks.jsonl)
- `0x9280`..`0x9292` (reached from `0x90A0`), bytes SHA-256 `33b914032a103406...`, 9 instructions in [`unowned_blocks.jsonl`](../../../graphs/unowned_blocks.jsonl)
- `0x90A2`..`0x90D8` (reached from `0x905A`), bytes SHA-256 `7aa64ddb600c3b9b...`, 24 instructions in [`unowned_blocks.jsonl`](../../../graphs/unowned_blocks.jsonl)
- `0x90E0`..`0x90EE` (reached from `0x90D6`), bytes SHA-256 `13cec78ad1cdf845...`, 6 instructions in [`unowned_blocks.jsonl`](../../../graphs/unowned_blocks.jsonl)
- `0x90EE`..`0x9126` (reached from `0x90A4`), bytes SHA-256 `976757e1a75fa1a2...`, 25 instructions in [`unowned_blocks.jsonl`](../../../graphs/unowned_blocks.jsonl)
- `0x912E`..`0x913C` (reached from `0x9124`), bytes SHA-256 `3e4b9113415df88e...`, 6 instructions in [`unowned_blocks.jsonl`](../../../graphs/unowned_blocks.jsonl)
- `0x913C`..`0x9174` (reached from `0x90F2`), bytes SHA-256 `81806e8e303dd0d4...`, 25 instructions in [`unowned_blocks.jsonl`](../../../graphs/unowned_blocks.jsonl)
- `0x917C`..`0x918A` (reached from `0x9172`), bytes SHA-256 `bc603963a4f2e8c9...`, 6 instructions in [`unowned_blocks.jsonl`](../../../graphs/unowned_blocks.jsonl)
- `0x918A`..`0x91C0` (reached from `0x9140`), bytes SHA-256 `ed895f2556bdd2dc...`, 24 instructions in [`unowned_blocks.jsonl`](../../../graphs/unowned_blocks.jsonl)
- `0x91C8`..`0x91D6` (reached from `0x91BE`), bytes SHA-256 `7f2fa100738b677f...`, 6 instructions in [`unowned_blocks.jsonl`](../../../graphs/unowned_blocks.jsonl)
- `0x91D6`..`0x9208` (reached from `0x918E`), bytes SHA-256 `30664c536eb6f9c9...`, 22 instructions in [`unowned_blocks.jsonl`](../../../graphs/unowned_blocks.jsonl)
- `0x9274`..`0x9280` (reached from `0x9206`), bytes SHA-256 `ce8971bf0d80c4bb...`, 5 instructions in [`unowned_blocks.jsonl`](../../../graphs/unowned_blocks.jsonl)
- `0x8E1E`..`0x8E58` (reached from `0x8BBC`), bytes SHA-256 `856b1e5989f7eea6...`, 26 instructions in [`unowned_blocks.jsonl`](../../../graphs/unowned_blocks.jsonl)
- `0x8EB8`..`0x8EC6` (reached from `0x8E56`), bytes SHA-256 `d1c1e91c61455bdf...`, 6 instructions in [`unowned_blocks.jsonl`](../../../graphs/unowned_blocks.jsonl)
- `0x9040`..`0x9054` (reached from `0x8EC4`), bytes SHA-256 `5ec102a27f31fb8d...`, 10 instructions in [`unowned_blocks.jsonl`](../../../graphs/unowned_blocks.jsonl)
- `0x8EC6`..`0x8EFC` (reached from `0x8E24`), bytes SHA-256 `502366f728aa2c76...`, 24 instructions in [`unowned_blocks.jsonl`](../../../graphs/unowned_blocks.jsonl)
- `0x8F04`..`0x8F12` (reached from `0x8EFA`), bytes SHA-256 `76d3a00451a6974d...`, 6 instructions in [`unowned_blocks.jsonl`](../../../graphs/unowned_blocks.jsonl)
- `0x8F12`..`0x8F48` (reached from `0x8EC8`), bytes SHA-256 `94f98350195b282d...`, 24 instructions in [`unowned_blocks.jsonl`](../../../graphs/unowned_blocks.jsonl)
- `0x8F50`..`0x8F5E` (reached from `0x8F46`), bytes SHA-256 `23b01707f962b581...`, 6 instructions in [`unowned_blocks.jsonl`](../../../graphs/unowned_blocks.jsonl)
- `0x8F5E`..`0x8F96` (reached from `0x8F16`), bytes SHA-256 `d14a19b95411675e...`, 25 instructions in [`unowned_blocks.jsonl`](../../../graphs/unowned_blocks.jsonl)
- `0x8F9E`..`0x8FAC` (reached from `0x8F94`), bytes SHA-256 `1ece17048b1c8941...`, 6 instructions in [`unowned_blocks.jsonl`](../../../graphs/unowned_blocks.jsonl)
- `0x8FAC`..`0x8FE4` (reached from `0x8F62`), bytes SHA-256 `ba5bb9e8b3c95240...`, 25 instructions in [`unowned_blocks.jsonl`](../../../graphs/unowned_blocks.jsonl)
- `0x8FEC`..`0x8FFA` (reached from `0x8FE2`), bytes SHA-256 `209299c925d76c28...`, 6 instructions in [`unowned_blocks.jsonl`](../../../graphs/unowned_blocks.jsonl)
- `0x8FFA`..`0x902C` (reached from `0x8FB0`), bytes SHA-256 `90a46392841a762b...`, 22 instructions in [`unowned_blocks.jsonl`](../../../graphs/unowned_blocks.jsonl)
- `0x9034`..`0x9040` (reached from `0x902A`), bytes SHA-256 `1f213bd57ed16646...`, 5 instructions in [`unowned_blocks.jsonl`](../../../graphs/unowned_blocks.jsonl)
- `0x8D24`..`0x8D5E` (reached from `0x8BBC`), bytes SHA-256 `2c052677e14b9541...`, 26 instructions in [`unowned_blocks.jsonl`](../../../graphs/unowned_blocks.jsonl)
- `0x8D76`..`0x8DAA` (reached from `0x8D2A`), bytes SHA-256 `24c58d57346a7237...`, 23 instructions in [`unowned_blocks.jsonl`](../../../graphs/unowned_blocks.jsonl)
- `0x8DC2`..`0x8DF4` (reached from `0x8D78`), bytes SHA-256 `e24d91a1d35f13a6...`, 22 instructions in [`unowned_blocks.jsonl`](../../../graphs/unowned_blocks.jsonl)
- `0x8BDC`..`0x8C16` (reached from `0x8BBC`), bytes SHA-256 `481d55dd0fe82465...`, 26 instructions in [`unowned_blocks.jsonl`](../../../graphs/unowned_blocks.jsonl)
- `0x8C2E`..`0x8C62` (reached from `0x8BE2`), bytes SHA-256 `72bd835c3115f766...`, 23 instructions in [`unowned_blocks.jsonl`](../../../graphs/unowned_blocks.jsonl)
- `0x8C7A`..`0x8CB0` (reached from `0x8C30`), bytes SHA-256 `7c69cccbb317d564...`, 24 instructions in [`unowned_blocks.jsonl`](../../../graphs/unowned_blocks.jsonl)
- `0x8CC8`..`0x8CFA` (reached from `0x8C7C`), bytes SHA-256 `97420bfceaca9c32...`, 22 instructions in [`unowned_blocks.jsonl`](../../../graphs/unowned_blocks.jsonl)
- `0x83BC`..`0x83EE` (reached from `0x83A0`), bytes SHA-256 `555cadbb9197ff4f...`, 22 instructions in [`unowned_blocks.jsonl`](../../../graphs/unowned_blocks.jsonl)

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0x7DBC    f0b5       PUSH    {R4-R7,LR}
0x7DBE    5e46       MOV     R6, R11
0x7DC0    5546       MOV     R5, R10
0x7DC2    4446       MOV     R4, R8
0x7DC4    70b4       PUSH    {R4-R6}
0x7DC6    e0f18ce8   BLX     ___save_vfp_d8_d15_regs
0x7DCA    f64c       LDR     R4, =0xFFFFFC64
0x7DCC    16af       ADD     R7, SP, #arg_38
0x7DCE    a544       ADD     SP, R4; int
0x7DD0    f54b       LDR     R3, =(___gxx_personality_sj0_ptr - 0x7DDA)
0x7DD2    6a46       MOV     R2, SP
0x7DD4    7292       STR     R2, [SP,#0x39C+var_1D4]
0x7DD6    7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0x7DD8    0122       MOVS    R2, #1
0x7DDA    1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0x7DDC    1590       STR     R0, [SP,#0x39C+var_348]; int
0x7DDE    68a8       ADD     R0, SP, #0x39C+fctx; lpfctx
0x7DE0    6e93       STR     R3, [SP,#0x39C+fctx.personality]
0x7DE2    f24b       LDR     R3, =(GCC_except_table6 - 0x7DEA)
0x7DE4    7097       STR     R7, [SP,#0x39C+fctx.jbuf]
0x7DE6    7b44       ADD     R3, PC; GCC_except_table6
0x7DE8    6f93       STR     R3, [SP,#0x39C+fctx.lsda]
0x7DEA    f14b       LDR     R3, =(loc_947C - 0x7DF0)
0x7DEC    7b44       ADD     R3, PC; loc_947C
```

**Around `0x826A` (cited by the Unity code)**

```
0x824A    0123       MOVS    R3, #1
0x824C    5b42       NEGS    R3, R3
0x824E    5068       LDR     R0, [R2,#4]
0x8250    6993       STR     R3, [SP,#0x39C+fctx.call_site]
0x8266    e449       LDR     R1, =0x42200000; jumptable 00007EA0 case 2
0x8268    4a98       LDR     R0, [SP,#0x39C+var_274]
0x826A    e1f1daed   BLX     ___mulsf3vfp   <==
0x826E    e349       LDR     R1, =0x42040000
0x8270    4e90       STR     R0, [SP,#0x39C+var_264]
0x8272    4a98       LDR     R0, [SP,#0x39C+var_274]
0x8274    e1f1d4ed   BLX     ___mulsf3vfp
0x8278    e149       LDR     R1, =0x41E00000
0x827A    4d90       STR     R0, [SP,#0x39C+var_268]
```

