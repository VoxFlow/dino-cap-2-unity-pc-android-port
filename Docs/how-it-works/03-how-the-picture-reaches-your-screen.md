# 3. How the picture reaches your screen

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../start/disclaimer.md).*

> **In simple terms.** The game draws a 1024×768 iPad screen in layers. The port draws the same layers in the same order; on wider screens it shows more world to the sides instead of black bars.

- **Logical screen.** The game's code works in a 1024×768 space with `S = 2` (the iPad scale of iPhone units), and `g_ground_y = -110` on iPad. The port keeps that space for every rule.
- **Layers and order.** The original renderer draws layers 0..15 in ascending order: each layer's sprites, then its custom draws. Insertion order decides order inside a layer. The port reproduces this, including the HUD's creation order from the scenes' `OnLoad` ([lifeModeScene::OnLoad](../technical/functions/dossiers/lifeModeScene/FUN-0E7202.md)).
- **Filtering.** The original sets bilinear filtering (`GL_LINEAR`) on its textures. Some HUD icons show faint edge pixels at non-integer window scales; that is kept.
- **Wide view** (ADAPTATION AD-001, on by default):
  - The camera keeps the native height and shows `ExtraHalf = 384 × aspect − 512` extra logical pixels per side, capped at 21:9 (3440/1440). Wider screens get black bars.
  - HUD elements anchor to the screen edge of the half their centre is in.
  - Background chains repeat at their own period to fill the extra width.
  - Every gameplay use of the extra width is written to reduce to the original rule at 4:3; the gameplay-affecting cases are listed in [divergences](../fidelity/shipped-quirks-and-divergences.md).
- **Bars.** When the picture is letter- or pillar-boxed, a clear-only camera repaints the bars every frame (platform layer PF-002). Without it, a closed debug menu stayed visible in the bars.
- **Menus** keep the native height, with the red background art widened over the extra width.
