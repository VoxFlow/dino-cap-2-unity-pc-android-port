# 2. How the original game stores its art, animation and settings

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../start/disclaimer.md).*

> **In simple terms.** The pictures are packed into big sheets with a text file saying where each picture is; animations are lists of parts that move frame by frame; settings are small text tables. The port reads all of these as they are.

- **Atlases.** A `.conf` text file lists frames as `FrameK = (name x y w h)`. Each becomes a frame named `name` + `Frame` on its texture (rule `dc2.asset.conf.format`; consumer `loadImageByConf` `0x59AD6`). The port reads the same files ([asset map](../technical/maps/ASSET_IDENTITY_MAP.md)).
- **Composite animations** (`CombineAnimation`, played by `CAObjCallBack`):
  - Each frame places named parts (sprite reference, position, rotation, scale, colour).
  - A second "advanced" layer can override parts by name (merged by `CAObjCallBack::OnUpdate`).
  - The port's `NativeCaAnimation.cs` plays them with the original's clock rules: one frame per update, the remainder dropped, clamping at the end bound, then the end callback. This rule explains why the game is frame-rate dependent ([chapter 8](08-pause-and-time.md)).
- **Component animations** (`.acb`) drive effects, the egg truck and UI pieces; `NativeComponentAnimation.cs` reads them.
- **Fonts.** Most labels use the original's bitmap fonts (`NativeFont.cs`). Rects, spacing and colours come from emulating the scenes' `OnLoad` code.
- **Configuration.** `Player.xml`, `boss.xml`, `Map.xml`, `weapon.prop`, `dinosaur.prop` and others carry stats and level data. The port reads the original files; tables the project derived from them, such as the dinosaur reaction table and the background music pairing, are marked *derived* in the asset map.
- **Field meanings need proof from a consumer.** A file name or an XML tag does not prove how the game uses a value. One recurring error class came from reading an animation-player field by its assumed meaning, the current frame read as a loop bound; it is described in [corrected claims](../history/corrected-claims.md).
