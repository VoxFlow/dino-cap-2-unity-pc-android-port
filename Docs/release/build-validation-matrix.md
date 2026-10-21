# Build and validation matrix

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../start/disclaimer.md).*

Compiling, launching, playing and matching the original are separate levels; each row says which level it reached.

| Target | Settings | Level reached | Evidence |
|---|---|---|---|
| Unity editor (batch mode) | Unity 6000.3.8f1 | 46 / 46 verifier programs pass (r119x); later fixes have targeted runs | [testing](../quality/testing-and-validation.md) |
| Windows | x64, Mono backend, no splash, product "Dino Cap 2" | Built; automated play pass at 1920×1080 (75 fps with vsync; about 2,200 fps uncapped in a development build) | per-file hash manifest ([release_manifest.json](release_manifest.json)); profiling summary |
| Android | IL2CPP, ARMv7 + ARM64 in one APK, landscape, package `com.pilot.dc2port` | Built; installed and run on a low-tier Android phone (32-bit system): 60 fps in battle, 99th-percentile frame 16.7 ms | phone run and launch screenshot ([FIG-02](../technical/maps/FIGURE_INDEX.md)) |
| Fidelity vs the iPad | User A/B, feature by feature | Accepted per batch; open items in [open questions](../technical/OPEN_QUESTIONS.md) | batch reports (private), [corrected claims](../history/corrected-claims.md) |
| Overall readiness | The project's strict gate | `NOT_READY_FOR_USER_RUNTIME_AB`: playable, not every term proven and accepted on a device | [open questions](../technical/OPEN_QUESTIONS.md) |

**Final artifacts** (after the pre-publication audit):

| File | SHA-256 |
|---|---|
| `Dino Cap 2.apk` | `34cd799400820e9ad53f1c80f448598761d503b5717d451894f71f6d3b51bd27` |
| Windows `Dino Cap 2_Data/Managed/Assembly-CSharp.dll` | `b11f6b544f83901590858ac72b45c1857143bc311fe56e24e1e6c254a1c3ec90` |
| Original IPA (not distributed) | `6cec1c949b1507a82a48997b8796adb2ab47e66524b43c3e9b3e8b5a3c940038` |

The Windows `.exe` is Unity's launcher stub (the same in every build); the whole folder's per-file hashes in [release_manifest.json](release_manifest.json) identify a Windows build. The phone performance run and FIG-02 are historical: they used an earlier APK of the same code line, built before the final rename, the splash change and the negative-cash fix. The final APK was not launched on the phone (user decision).
