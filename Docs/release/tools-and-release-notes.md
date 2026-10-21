# Tools and release notes

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../start/disclaimer.md).*

## First release

- Dino Cap 2 (iOS 1.3) for **Windows x64** (Mono) and **Android** (IL2CPP, ARMv7 + ARM64 in one APK).
- Every mission type in the shipped map; all heroes, weapons, bosses, menus, the shop, the bank (cash packs granted directly) and the jigsaw.
- **Wide view** on by default (capped at 21:9); can be switched off in the developer menu (<kbd>F1</kbd>).
- PC keyboard and mouse controls; touch on phones, with notch-aware edge buttons.
- Original icon (the IPA's 512×512 artwork); no Unity splash screen.
- Known open items: [open questions](../technical/OPEN_QUESTIONS.md).

## Tools used

| Tool | Use |
|---|---|
| Unity 6000.3.8f1 | engine and builds (`Assets/Editor/PilotBuild.cs`) |
| IDA Pro 9.4 + idalib | analysis of the original executable; [export scripts](../technical/tools/README.md) |
| Python 3 | asset extraction, emulation of original functions (Unicorn), constant decoding, documentation generation |
| adb | installing and testing on an Android phone |

## Reporting a difference

If something behaves differently from the iPad game, please open an issue with:
- the mission (type and level);
- what you did;
- what the original does, ideally with a recording;
- what the port does.

Recordings of the original are the most useful evidence: they can prove the port wrong, and they point to where in the code to look.
