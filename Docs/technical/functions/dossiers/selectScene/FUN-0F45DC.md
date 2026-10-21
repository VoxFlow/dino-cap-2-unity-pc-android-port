# selectScene::UpdateExp

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0F45DC` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xF45DC` |
| RVA (EA - 0x1000) | `0xF35DC` |
| File offset | `0xF35DC` |
| Size | 220 code bytes in 1 chunk(s), 99 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11selectScene9UpdateExpEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::selectScene::UpdateExp(void)` |
| Bytes SHA-256 | `0d577e268756e4849bcac8a8e83e7eebf7b817eede244d9d0654091562585fa7` |
| Direct code callers | 8 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / PORTED_OR_TRACED |
| Depth | deep: curated note below, from the batch that established it |

## What it does

Menu cash display: loads the global cash, clamps a negative value to 0 and stores it back before printing it with commas. Found by the pre-publication audit (REL-12) and fixed: the port had clamped only the displayed text.

- Rule `dc2.hero.hire_upgrade_formulas` (LIKELY, NATIVE (decompiler; compare operators need ARM verification)): Hero screen (type 2): ids 6..9 select heroes 0..3, 10 selects hero 4 ('new character'); 36/37 prev/next wrap within 0..3; 38 confirm (commit g_player_type; if it changed dword_2404E8 := 0; SaveFile; back to map). 39 HIRE: affordable iff cash(dword_24047C) >= cost(player_vector[h]+4) [cash < cost -> OnNoMoney + sound Audio_button2Audio]; on success flag +48=1, +52=cost, cash -= cost, dword_240494[h]=0, hero 1 hired wi...
- Rule `dc2.select.map_ui_layout` (PROVEN, NATIVE (decompiler; rect constants in .data not yet read)): showMapUI@0x10218c builds the main screen (m_map_type 0): bg ui_map_bgFrame (id 56, slides in right), player-info panel ui_map_player_infoFrame (id 59, slides in left), hero head icon ui_p<type+1>_iconFrame (id 60; hero 4 has a special rect at (-?,-?,73,?)), Lv label (id 61, font Adventure_16, text = level+1 where level = dword_240494[g_player_type]), HP label (id 62, Adventure_16, text = (int)(hp + g_jigsaw1_count*h...
- Rule `dc2.select.cash_label` (PROVEN, NATIVE): UpdateExp@0xf45dc clamps cash dword_24047C to >= 0, formats %d and inserts a comma every 3 digits from the right (loop starts at len and steps by 3 while i > 3), sets text of label at selectScene+248.

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/SelectBank.cs:85`](../../../../../Assets/Scripts/SelectBank.cs#L85) (by name)
- [`Assets/Scripts/SelectHero.cs:41`](../../../../../Assets/Scripts/SelectHero.cs#L41) cites `0xF45DC`
- [`Assets/Scripts/SelectHero.cs:42`](../../../../../Assets/Scripts/SelectHero.cs#L42) cites `0xF461A`

## Callers and callees (direct code references)

Callers: [`selectScene::iapaddmoney`](../../../functions/dossiers/selectScene/FUN-0F46D2.md), [`selectScene::UpgradeWeapon`](../../../functions/dossiers/selectScene/FUN-0F52C2.md), [`selectScene::AddBullet`](../../../functions/dossiers/selectScene/FUN-0F55B0.md), [`selectScene::showIAP`](../../../functions/dossiers/selectScene/FUN-101AD2.md), [`selectScene::showUpgrade`](../../../functions/dossiers/selectScene/FUN-10363E.md), [`selectScene::showTavernUI`](../../../functions/dossiers/selectScene/FUN-1056DC.md), [`selectScene::OnUIEvent`](../../../functions/dossiers/selectScene/FUN-10B93A.md)

Callees: `Triniti2D::UILabel::SetText`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::insert`, `std::string::string`, `std::string::~string`, `_sprintf`

## Strings it references

- `0x20111C` "%d"
- `0x207598` ","

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x227F44` GCC_except_table11_8
- `0x22A2DC` __ZN7DinoCap13g_prop_PlayerE_ptr
- `0x240474` DinoCap::g_prop_Player

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xF45DC   f0b5       PUSH    {R4-R7,LR}
0xF45DE   5e46       MOV     R6, R11
0xF45E0   5546       MOV     R5, R10
0xF45E2   4446       MOV     R4, R8
0xF45E4   70b4       PUSH    {R4-R6}
0xF45E6   f3f07cec   BLX     ___save_vfp_d8_d15_regs
0xF45EA   16af       ADD     R7, SP, #arg_38
0xF45EC   96b0       SUB     SP, SP, #0x58 ; 'X'
0xF45EE   324b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xF45F8)
0xF45F0   6a46       MOV     R2, SP
0xF45F2   0d92       STR     R2, [SP,#0x58+var_24]
0xF45F4   7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xF45F6   0122       MOVS    R2, #1
0xF45F8   1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xF45FA   0190       STR     R0, [SP,#0x58+var_54]
0xF45FC   03a8       ADD     R0, SP, #0x58+fctx; lpfctx
0xF45FE   0993       STR     R3, [SP,#0x58+fctx.personality]
0xF4600   2e4b       LDR     R3, =(GCC_except_table11_8 - 0xF4608)
0xF4602   0b97       STR     R7, [SP,#0x58+fctx.jbuf]
0xF4604   7b44       ADD     R3, PC; GCC_except_table11_8
0xF4606   0a93       STR     R3, [SP,#0x58+fctx.lsda]
0xF4608   2d4b       LDR     R3, =(loc_F4676 - 0xF460E)
0xF460A   7b44       ADD     R3, PC; loc_F4676
0xF460C   1343       ORRS    R3, R2
```

**Around `0xF461A` (cited by the Unity code)**

```
0xF460C   1343       ORRS    R3, R2
0xF460E   0c93       STR     R3, [SP,#0x58+var_28]
0xF4610   f5f020ea   BLX     __Unwind_SjLj_Register
0xF4614   2b4b       LDR     R3, =(__ZN7DinoCap13g_prop_PlayerE_ptr - 0xF461A)
0xF4616   7b44       ADD     R3, PC; __ZN7DinoCap13g_prop_PlayerE_ptr
0xF4618   1b68       LDR     R3, [R3]; DinoCap::g_prop_Player ...
0xF461A   9a68       LDR     R2, [R3,#(dword_24047C - 0x240474)]   <==
0xF461C   002a       CMP     R2, #0
0xF461E   00da       BGE     loc_F4622
0xF4620   0022       MOVS    R2, #0
0xF4622   2949       LDR     R1, =(aD - 0xF462C); "%d"
0xF4624   10a8       ADD     R0, SP, #0x58+var_18; char *
0xF4626   9a60       STR     R2, [R3,#(dword_24047C - 0x240474)]
```

