# playerClass::Roll

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0B95F8` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xB95F8` |
| RVA (EA - 0x1000) | `0xB85F8` |
| File offset | `0xB85F8` |
| Size | 216 code bytes in 1 chunk(s), 92 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11playerClass4RollEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerClass::Roll(void)` |
| Bytes SHA-256 | `fe268b9f87765d5300758e0afb17a03c9d9bad7c01c19440a2076e1e072a2169` |
| Direct code callers | 1 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.hero.two_part_roll.r118w` (PROVEN, NATIVE): Heroes 1/3 roll (playerClass::Roll 0xb95f8 else-branch): roll_1 once without velocity; at its end hide, hero 3 spawns p4_roll_eff (y-50, layer 9, flip inherited), velocity +-3x walk, SetTimer roll 0.5; timer: visible, stop, roll_2; roll_2 end: state 2, Stand. Supersedes PA-011 for the roll.
- Rule `dc2.engine.special_roll_world_phase.r118z2` (PROVEN, NATIVE): Heroes 1/3 special roll does not change the scene stage order (playerClass::Roll 0xb95f8 / OnTimeout "roll" only set body velocity). Port defect fixed: the special roll no longer runs the legacy frame (legacy dinosaur update, hero integrated after camera); hero body velocity = rolling ? rollVelocity : heroVelocityX in the physics position.

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameControls.cs:9`](../../../../../Assets/Scripts/GameControls.cs#L9) cites `0xB95F8`
- [`Assets/Scripts/GameControls.cs:117`](../../../../../Assets/Scripts/GameControls.cs#L117) cites `0xB95F8`
- [`Assets/Scripts/GameControls.cs:136`](../../../../../Assets/Scripts/GameControls.cs#L136) cites `0xB95F8`
- [`Assets/Scripts/GameScene.cs:53`](../../../../../Assets/Scripts/GameScene.cs#L53) cites `0xB95F8`

## Callers and callees (direct code references)

Callers: [`playerClass::OnRollDown`](../../../functions/dossiers/playerClass/FUN-0BD668.md)

Callees: [`CAObjCallBack::playAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00A064.md), [`CAObjCallBack::setAnimation`](../../../functions/dossiers/CAObjCallBack/FUN-00AEA0.md), `CombineAnimation::SetKeyFrame`, [`playerClass::UpdateBG`](../../../functions/dossiers/playerClass/FUN-0B8944.md), `Triniti2D::GameObject::Physics`, `Triniti2D::GamePhysics::SetLinearVelocity`, `Triniti2D::GamePhysics::WakeUp`, `std::string::compare`, `___mulsf3vfp`

## Strings it references

- `0x2082D4` "playerRollAnimation"

## Named data it references

- `0x22A2B8` __ZN7DinoCap13g_player_typeE_ptr
- `0x23B86C` DinoCap::g_player_type
- `0x22A2DC` __ZN7DinoCap13g_prop_PlayerE_ptr
- `0x240474` DinoCap::g_prop_Player

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xB95F8   b0b5       PUSH    {R4,R5,R7,LR}
0xB95FA   02af       ADD     R7, SP, #8
0xB95FC   83b0       SUB     SP, SP, #0xC
0xB95FE   3449       LDR     R1, =(aPlayerrollanim - 0xB9608); "playerRollAnimation"
0xB9600   041c       MOVS    R4, R0
0xB9602   6430       ADDS    R0, #0x64 ; 'd'; this
0xB9604   7944       ADD     R1, PC; "playerRollAnimation"
0xB9606   30f162ea   BLX     __ZNKSs7compareEPKc; std::string::compare(char const*)
0xB960A   0028       CMP     R0, #0
0xB960C   5ed0       BEQ     loc_B96CC
0xB960E   314b       LDR     R3, =(__ZN7DinoCap13g_player_typeE_ptr - 0xB9614)
0xB9610   7b44       ADD     R3, PC; __ZN7DinoCap13g_player_typeE_ptr
0xB9612   1b68       LDR     R3, [R3]; DinoCap::g_player_type ...
0xB9614   1b68       LDR     R3, [R3]; DinoCap::g_player_type
0xB9616   002b       CMP     R3, #0
0xB9618   01d0       BEQ     loc_B961E
0xB961A   022b       CMP     R3, #2
0xB961C   38d1       BNE     loc_B9690
0xB961E   ad25ad00   MOVS    R5, #0x2B4
0xB9622   0721       MOVS    R1, #7; unsigned int
0xB9624   6059       LDR     R0, [R4,R5]; this
0xB9626   51f70dff   BL      __ZN7DinoCap16CombineAnimation11SetKeyFrameEm; DinoCap::CombineAnimation::SetKeyFrame(ulong)
0xB962A   6059       LDR     R0, [R4,R5]; this
0xB962C   1221       MOVS    R1, #0x12; unsigned int
```

