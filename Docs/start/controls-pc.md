# Controls for PC (keyboard and mouse)

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](disclaimer.md).*

> **In simple terms.** Each key presses one of the iPad's on-screen buttons, so the game reacts exactly as it would to a finger on that button.

| Action | Keyboard | Mouse |
|---|---|---|
| Walk left / right | <kbd>A</kbd> / <kbd>D</kbd> | (none) |
| Shoot (hold for automatic weapons) | <kbd>K</kbd> | Left button |
| Melee | <kbd>J</kbd> | Right button |
| Roll / dash | <kbd>S</kbd> | (none) |
| Switch weapon | <kbd>L</kbd> | Mouse wheel (one switch per notch) |
| Pause / resume | <kbd>Esc</kbd> | Click the pause icon |
| Fullscreen | <kbd>F11</kbd> or <kbd>Alt</kbd>+<kbd>Enter</kbd> | (none) |

Menus use the mouse. In battle, a left click on a button (the pause menu, *Done*) is a normal click; anywhere else it shoots.

## How it works

Each key or mouse button is a separate "finger" that touches the centre of the matching native touch area, so the original touch handler decides everything: holding, releasing, sliding from one arrow to the other, and which button a finger belongs to (`lifeModeScene::OnTouchEvent`, see its [dossier](../technical/functions/dossiers/lifeModeScene/FUN-0D15E4.md)). Two PC conveniences are added on top:
- <kbd>Esc</kbd> also presses *Resume* when the pause menu is open;
- one wheel notch is a quick press and release of the switch button.

A release that happens while the window is not receiving input, for example during focus loss, is delivered as soon as input returns, so no button stays stuck down. The code is in `Assets/Scripts/PcPlatform.cs` (platform layer PF-001).

## Developer menu

<kbd>F1</kbd> opens a developer test menu with money, unlocks, invincibility, game speed and a mission launcher. It is not part of the original game and does nothing unless opened (DBG-001). It also holds the *wide view* switch.
