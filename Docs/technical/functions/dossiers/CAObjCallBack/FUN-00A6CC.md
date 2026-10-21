# CAObjCallBack::OnUpdate

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../../../start/disclaimer.md).*

| Field | Value |
|---|---|
| Function ID | `FUN-00A6CC` |
| Binary | [`DC2-IOS13-ARM32-SUB6`](../../../binary/ADDRESS_CONVENTIONS.md) |
| Static address (EA) | `0xA6CC` |
| RVA (EA - 0x1000) | `0x96CC` |
| File offset | `0x96CC` |
| Size | 1996 code bytes in 1 chunk(s), 864 instructions |
| Instruction set | THUMB |
| Name | `__ZN7DinoCap13CAObjCallBack8OnUpdateEf` (symbol in the binary (nlist)) |
| Demangled | `DinoCap::CAObjCallBack::OnUpdate(float)` |
| Bytes SHA-256 | `138a880b394c37e4d0122e88181aead125167f035dee4ce8a7395f9f0a67c6c0` |
| Direct code callers | 8 (virtual-table and function-pointer calls are not counted) |
| Ledger verdict / status | UNSET / TEST_EXECUTED |
| Depth | generated from the database export; interpretation limited to the cited rules and code comments |

## What it does

- Rule `dc2.anim.a2b_runtime_contract` (SUPERSEDED, NATIVE (decompiler)): CASprite::LoadFromFile@0xbb98 reads per part: u32 nameLen+name (+32), 8 bytes (x,y) at +0, 8 bytes (w,h) at +8, 8 bytes (A,B) at +16, 4 bytes C at +24, 4 bytes packedRGBA at +36, 4 bytes D at +28, int refLen (+ref string at +40). x,y,w,h are then multiplied by g_scale (iPad 1.0; iPhone 0.8, or 0.5 for load_p1 case). ref+'Frame' -> GetFrameId. D is the LAYER (SortCASpriteLayer compares it) - DC1's 'vestigial D' does n...
- Rule `dc2.animation.ca_scale_change_image.r11` (PROVEN, NATIVE): CAObjCallBack OnUpdate0xa8a0..0xa920 multiplies local x/y/w/h by callback+84 before pivot and object position. ChangeImage0xcc8a per-frame named-part lookup changes reference/frame id and multiplies existing w/h by argument; no replacement-size normalization.
- Rule `dc2.render.draw_order.r118` (PROVEN, NATIVE): RenderManager::DoRender 0x172d90 draws per layer 0..15 sprites, then point-lines, then custom draws; layers 5/10/12 are std::sorted by DrawOrder (SetLayerMode 5), others by material (texture id, blend) then AddSprite order. CAObjCallBack ctor 0xaff4: layer 10 and DrawOrder base = (DrawOrerIndex += 50); child sprite i gets base+i (OnUpdate 0xa6cc). houseClass::OnLoad sets layer 5. Dinosaur GameObjects are pooled LIFO ...
- Rule `dc2.engine.yuanli_mount_inert.r118z4` (PROVEN, NATIVE): playerClass::YuanLi 0xbf0ea mounts its effect to the hero on the first frame-11 call (offset (0, 17h) - heroPos, 0x41880000 = 17.0). The effect is created with only the container flag (no GameSprite; GameObject::Initialize 0x16c252 flag order sprite/physics/particle/pointline/container/text), effectClass::OnUpdate 0x167a6 mode 8 resets its position to the com2 point before CAObjCallBack::OnUpdate places the parts (ow...

## Where the port implements or cites it

Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.

- [`Assets/Scripts/GameBeam.cs:248`](../../../../../Assets/Scripts/GameBeam.cs#L248) cites `0xA6CC`
- [`Assets/Scripts/GameCombat.cs:53`](../../../../../Assets/Scripts/GameCombat.cs#L53) (by name)
- [`Assets/Scripts/GameHouse.cs:52`](../../../../../Assets/Scripts/GameHouse.cs#L52) (by name)
- [`Assets/Scripts/NativeCaAnimation.cs:201`](../../../../../Assets/Scripts/NativeCaAnimation.cs#L201) cites `0xA926`
- [`Assets/Scripts/NativeCaAnimation.cs:203`](../../../../../Assets/Scripts/NativeCaAnimation.cs#L203) cites `0xA6CC`
- [`Assets/Scripts/NativeCaAnimation.cs:206`](../../../../../Assets/Scripts/NativeCaAnimation.cs#L206) cites `0xA9C0`
- [`Assets/Scripts/NativeDrawOrder.cs:4`](../../../../../Assets/Scripts/NativeDrawOrder.cs#L4) cites `0xA6CC`

## Callers and callees (direct code references)

Callers: [`dinosaurClass::OnUpdate`](../../../functions/dossiers/dinosaurClass/FUN-011BA4.md), [`loadEffClass::OnUpdate`](../../../functions/dossiers/loadEffClass/FUN-015F48.md), [`effectClass::OnUpdate`](../../../functions/dossiers/effectClass/FUN-0167A6.md), [`playerClass::OnUpdate`](../../../functions/dossiers/playerClass/FUN-0BB6CC.md), [`menuObj::OnUpdate`](../../../functions/dossiers/menuObj/FUN-0CB496.md), `lifeModeDu::OnUpdate`, [`selPlayerClass::OnUpdate`](../../../functions/dossiers/selPlayerClass/FUN-10F638.md), [`jigsawPicClass::OnUpdate`](../../../functions/dossiers/jigsawPicClass/FUN-124722.md)

Callees: `Triniti2D::GameObject::Enable`, `Triniti2D::GameObject::GetPosition`, `Triniti2D::GameObject::GetRotation`, `Triniti2D::GameObject::GetCallBack`, `Triniti2D::GameObject::Sprite`, `Triniti2D::GameObject::SetRotation`, `Triniti2D::GameObject::SetSize`, `Triniti2D::GameObject::SetPosition`, `Triniti2D::GameSprite::PlayAnimation`, `Triniti2D::GameSprite::SetFrame`, `Triniti2D::TextureManager::GetFrameId`, `Triniti2D::TextureManager::GetAnimationId`, `___save_vfp_d8_d15_regs`, `___restore_vfp_d8_d15_regs`, `__Unwind_SjLj_Register`, `__Unwind_SjLj_Resume`, `__Unwind_SjLj_Unregister`, `std::string::assign`, `std::string::string`, `std::string::string`, `std::string::~string`, `___addsf3vfp`, `___dynamic_cast`, `___fixunssfsivfp`, `___floatunssisfvfp`, `___gesf2vfp`, `___gtsf2vfp`, `___ltsf2vfp`, `___mulsf3vfp`, `___subsf3vfp`, `_strcat`, `_strcmp`

## Strings it references

- `0x20117C` "Anim"
- `0x201184` "Frame"

## Named data it references

- `0x22A0B0` ___gxx_personality_sj0_ptr
- `0x241618` ___gxx_personality_sj0
- `0x2239F7` GCC_except_table7_0
- `0x22A12C` __ZNSt8_Rb_treeISsSt4pairIKSsPN7DinoCap8CASpriteEESt10_Select1stIS5_ESt4lessISsESaIS5_EE4findERS1__ptr
- `0x22A0A8` __ZTIN9Triniti2D18GameObjectCallBackE_ptr
- `0x22A128` __ZTIN7DinoCap19CAObjSpriteCallBackE_ptr
- `0x22A124` __ZN9Triniti2D15_TextureManagerE_ptr
- `0x23BBA4` Triniti2D::_TextureManager
- `0x22A0A0` __ZNKSt6vectorIPN9Triniti2D10GameObjectESaIS2_EE4sizeEv_ptr

## Native instructions (excerpts)

Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.

**Entry (first instructions)**

```
0xA6CC    f0b5       PUSH    {R4-R7,LR}
0xA6CE    5e46       MOV     R6, R11
0xA6D0    5546       MOV     R5, R10
0xA6D2    4446       MOV     R4, R8
0xA6D4    70b4       PUSH    {R4-R6}
0xA6D6    ddf104ec   BLX     ___save_vfp_d8_d15_regs
0xA6DA    5a4c       LDR     R4, =0xFFFFFCE0
0xA6DC    16af       ADD     R7, SP, #arg_38
0xA6DE    a544       ADD     SP, R4
0xA6E0    594b       LDR     R3, =(___gxx_personality_sj0_ptr - 0xA6EA)
0xA6E2    0122       MOVS    R2, #1
0xA6E4    0b90       STR     R0, [SP,#0x320+var_2F4]
0xA6E6    7b44       ADD     R3, PC; ___gxx_personality_sj0_ptr
0xA6E8    0a91       STR     R1, [SP,#0x320+var_2F8]
0xA6EA    1b68       LDR     R3, [R3]; /usr/lib/libstdc++.6.dylib
0xA6EC    6946       MOV     R1, SP
0xA6EE    31a8       ADD     R0, SP, #0x320+fctx; lpfctx
0xA6F0    3793       STR     R3, [SP,#0x320+fctx.personality]
0xA6F2    564b       LDR     R3, =(GCC_except_table7_0 - 0xA6FC)
0xA6F4    3997       STR     R7, [SP,#0x320+fctx.jbuf]
0xA6F6    3b91       STR     R1, [SP,#0x320+var_234]
0xA6F8    7b44       ADD     R3, PC; GCC_except_table7_0
0xA6FA    3893       STR     R3, [SP,#0x320+fctx.lsda]
0xA6FC    544b       LDR     R3, =(loc_ABDA - 0xA702)
```

**Around `0xA926` (cited by the Unity code)**

```
0xA91A    0b9a       LDR     R2, [SP,#0x320+var_2F4]
0xA91C    6023       MOVS    R3, #0x60 ; '`'
0xA91E    1390       STR     R0, [SP,#0x320+var_2D4]
0xA920    1190       STR     R0, [SP,#0x320+var_2DC]
0xA922    d35c       LDRB    R3, [R2,R3]
0xA924    002b       CMP     R3, #0
0xA926    15d0       BEQ     loc_A954   <==
0xA928    c09b       LDR     R3, [SP,#0x320+var_20]
0xA92A    0899       LDR     R1, [SP,#0x320+var_300]
0xA92C    80242406   MOVS    R4, #0x80000000
0xA930    1a9a       LDR     R2, [SP,#0x320+var_2B8]
0xA932    1b19       ADDS    R3, R3, R4
0xA934    c093       STR     R3, [SP,#0x320+var_20]
```

**Around `0xA9C0` (cited by the Unity code)**

```
0xA9B2    031c       MOVS    R3, R0
0xA9B4    2f98       LDR     R0, [SP,#0x320+var_264]
0xA9B6    5968       LDR     R1, [R3,#4]
0xA9B8    dff15ae9   BLX     ___addsf3vfp
0xA9BC    c190       STR     R0, [SP,#0x320+var_1C]
0xA9BE    6068       LDR     R0, [R4,#4]; this
0xA9C0    61f1c4fa   BL      __ZN9Triniti2D10GameObject11GetRotationEv; Triniti2D::GameObject::GetRotation(void)   <==
0xA9C4    1a99       LDR     R1, [SP,#0x320+var_2B8]
0xA9C6    dff154e9   BLX     ___addsf3vfp
0xA9CA    12aa       ADD     R2, SP, #0x320+var_2D8
0xA9CC    2e90       STR     R0, [SP,#0x320+var_268]
0xA9CE    a16d       LDR     R1, [R4,#0x58]
0xA9D0    1078       LDRB    R0, [R2]
```

