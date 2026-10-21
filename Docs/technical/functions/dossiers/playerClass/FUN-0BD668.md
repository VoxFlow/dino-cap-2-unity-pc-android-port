# playerClass::OnRollDown

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-0BD668` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xBD668` |
| RVA (EA - 0x1000) | `0xBC668` |
| File offset | `0xBC668` |
| Size | 298 code bytes in 1 chunk(s), 129 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap11playerClass10OnRollDownEv` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::playerClass::OnRollDown(void)` |
| Bytes SHA-256 | `ac2d10ef9d8b892592048549ebc7849bd7b20f3db506c9e93ee898dc6272e16c` |
| Direct code callers | 2 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | deep: curated note below, from the batch that established it |

## What it does

Roll gate: refused only while the hero state (+640) is 7 (rolling) or 4, or during the 3 s rollEndTime flag (+141). Mission Complete stands the hero up (state 2), so rolling after completion is allowed in every mission except the run mission, whose end writes state 4 every frame.

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameControls.cs:9`](../../../../../Assets/Scripts/GameControls.cs#L9) cites `0xBD668`
- [`Assets/Scripts/GameControls.cs:96`](../../../../../Assets/Scripts/GameControls.cs#L96) cites `0xBD674`
- [`Assets/Scripts/GameControls.cs:101`](../../../../../Assets/Scripts/GameControls.cs#L101) cites `0xBD6EE`
- [`Assets/Scripts/GameFire.cs:268`](../../../../../Assets/Scripts/GameFire.cs#L268) cites `0xBD71C`

## Callers and callees (direct code references)

Callers: [`mainScene::OnTouchEvent`](../../../functions/dossiers/mainScene/FUN-096914.md), [`lifeModeScene::OnTouchEvent`](../../../functions/dossiers/lifeModeScene/FUN-0D15E4.md)

Callees: [`PlaySound`](../../../functions/dossiers/DinoCap/FUN-01A448.md), [`mainScene::RollDown`](../../../functions/dossiers/mainScene/FUN-094D68.md), [`playerClass::UpdateBG`](../../../functions/dossiers/playerClass/FUN-0B8944.md), [`playerClass::OnFireUp`](../../../functions/dossiers/playerClass/FUN-0B8BC6.md), [`playerClass::Roll`](../../../functions/dossiers/playerClass/FUN-0B95F8.md), [`lifeModeScene::RollDown`](../../../functions/dossiers/lifeModeScene/FUN-0D0850.md), `Triniti2D::GameObject::GetScene`, `Triniti2D::GameObject::Physics`, `Triniti2D::GameObject::SetTimer`, `Triniti2D::GamePhysics::SetLinearVelocity`, `Triniti2D::Scene::GetCallBack`, `std::string::_M_mutate`, `___dynamic_cast`

## Strings it references

- `0x2077D4` "p1rollAudio"
- `0x2077E0` "p2rollAudio"
- `0x2077EC` "p4rollAudio"
- `0x2077F8` "p5rollAudio"
- `0x2083E8` "rollEndTime"

## Named data it references

- `0x22A0C0` __ZTIN9Triniti2D13SceneCallBackE_ptr
- `0x22A0C8` __ZTIN7DinoCap9mainSceneE_ptr
- `0x22C868` `typeinfo for'DinoCap::mainScene
- `0x2415EC` `vtable for'__cxxabiv1::__si_class_type_info
- `0x22A274` __ZTIN7DinoCap13lifeModeSceneE_ptr
- `0x22CF10` `typeinfo for'DinoCap::lifeModeScene
- `0x22A2B8` __ZN7DinoCap13g_player_typeE_ptr
- `0x23B86C` DinoCap::g_player_type
- `0x22A290` __ZN7DinoCap9life_ModeE_ptr
- `0x23E09C` DinoCap::life_Mode

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xBD668   b0b5       PUSH    {R4,R5,R7,LR}
0xBD66A   02af       ADD     R7, SP, #8
0xBD66C   82b0       SUB     SP, SP, #8
0xBD66E   a0239b00   MOVS    R3, #0x280
0xBD672   041c       MOVS    R4, R0
0xBD674   c358       LDR     R3, [R0,R3]
0xBD676   072b       CMP     R3, #7
0xBD678   00d1       BNE     loc_BD67C
0xBD67A   88e0       B       loc_BD78E
0xBD67C   042b       CMP     R3, #4
0xBD67E   00d1       BNE     loc_BD682
0xBD680   85e0       B       loc_BD78E
0xBD682   8d23       MOVS    R3, #0x8D
0xBD684   c35c       LDRB    R3, [R0,R3]
0xBD686   002b       CMP     R3, #0
0xBD688   00d0       BEQ     loc_BD68C
0xBD68A   80e0       B       loc_BD78E
0xBD68C   6ee0       B       loc_BD76C
0xBD68E   6068       LDR     R0, [R4,#4]; this
0xBD690   aef05efc   BL      __ZN9Triniti2D10GameObject8GetSceneEv; Triniti2D::GameObject::GetScene(void)
0xBD694   b7f0cdfd   BL      __ZN9Triniti2D5Scene11GetCallBackEv; Triniti2D::Scene::GetCallBack(void)
0xBD698   0028       CMP     R0, #0
0xBD69A   08d0       BEQ     loc_BD6AE
0xBD69C   3d49       LDR     R1, =(__ZTIN9Triniti2D13SceneCallBackE_ptr - 0xBD6A6)
```

**Around `0xBD6EE` (cited by the Unity code)**

```
0xBD6E2   7b44       ADD     R3, PC; __ZN7DinoCap13g_player_typeE_ptr
0xBD6E4   1b68       LDR     R3, [R3]; DinoCap::g_player_type ...
0xBD6E6   1b68       LDR     R3, [R3]; char *
0xBD6E8   002b       CMP     R3, #0
0xBD6EA   01d0       BEQ     loc_BD6F0
0xBD6EC   022b       CMP     R3, #2
0xBD6EE   02d1       BNE     loc_BD6F6   <==
0xBD6F0   2d48       LDR     R0, =(aP1rollaudio - 0xBD6F6); "p1rollAudio"
0xBD6F2   7844       ADD     R0, PC; "p1rollAudio"
0xBD6F4   0de0       B       loc_BD712
0xBD6F6   012b       CMP     R3, #1
0xBD6F8   02d1       BNE     loc_BD700
0xBD6FA   2c48       LDR     R0, =(aP2rollaudio - 0xBD700); "p2rollAudio"
```

**Around `0xBD71C` (cited by the Unity code)**

```
0xBD70C   05d1       BNE     loc_BD71A
0xBD70E   2948       LDR     R0, =(aP5rollaudio - 0xBD714); "p5rollAudio"
0xBD710   7844       ADD     R0, PC; "p5rollAudio"
0xBD712   0021       MOVS    R1, #0; char *
0xBD714   0022       MOVS    R2, #0; bool
0xBD716   5cf797fe   BL      __ZN7DinoCap9PlaySoundEPKcbS1_; DinoCap::PlaySound(char const*,bool,char const*)
0xBD71A   a425ad00   MOVS    R5, #0x290   <==
0xBD71E   635d       LDRB    R3, [R4,R5]
0xBD720   002b       CMP     R3, #0
0xBD722   0cd0       BEQ     loc_BD73E
0xBD724   201c       MOVS    R0, R4; this
0xBD726   fbf74efa   BL      __ZN7DinoCap11playerClass8OnFireUpEv; DinoCap::playerClass::OnFireUp(void)
0xBD72A   e26e       LDR     R2, [R4,#0x6C]
```

