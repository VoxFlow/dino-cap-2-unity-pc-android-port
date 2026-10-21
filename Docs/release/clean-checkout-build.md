# Clean-checkout build check

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../start/disclaimer.md).*

**What was done (on the build machine):**
1. Exactly the files that would be committed (`git ls-files --cached --others --exclude-standard`: 1,665 files, 53 MB, with no `Library`, no caches and no ignored files) were copied into an empty folder.
2. That folder was opened with Unity 6000.3.8f1 in batch mode, from scratch.
3. Both shipped builds were made:

```powershell
& "C:\Program Files\Unity\Hub\Editor\6000.3.8f1\Editor\Unity.exe" -batchmode -projectPath "<clean folder>" -executeMethod Dc2Pilot.EditorTools.PilotBuild.BuildWindows -buildOut "<out>\Windows\Dino Cap 2.exe"
& "C:\Program Files\Unity\Hub\Editor\6000.3.8f1\Editor\Unity.exe" -batchmode -buildTarget Android -projectPath "<clean folder>" -executeMethod Dc2Pilot.EditorTools.PilotBuild.BuildAndroid -buildOut "<out>\Android\Dino Cap 2.apk"
```

In PowerShell, quote every path: the output name contains spaces, and `-buildOut` takes exactly the next argument.

**Result:**

| Target | Result |
|---|---|
| Windows x64, Mono | `BUILD Succeeded`, 0 compile errors |
| Android, IL2CPP | `BUILD Succeeded`, 0 compile errors; the APK (52,137,154 bytes) contains both `armeabi-v7a` and `arm64-v8a` libraries and the launcher icons |

**Limits:**
- A build on a different computer has not been tried.
- Android needs Unity's Android module (SDK, NDK, OpenJDK) installed through Unity Hub.
- **Not bit-reproducible:** the Windows game DLL embeds the path of the project folder it was built in, plus a build id, so two builds of the same source differ in those bytes. Build the release from a neutral folder path: the published Windows build was made from a project copy opened through a virtual drive (`X:\DC2-Dev`), so it embeds no personal path.
