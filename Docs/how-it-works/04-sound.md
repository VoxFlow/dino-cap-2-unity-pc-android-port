# 4. Sound

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../start/disclaimer.md).*

> **In simple terms.** Every sound plays at the same volume, each one in its own channel, so overlapping shots overlap. The port does the same.

- `DinoCap::PlayMusic` `0x1A36C` plays only when music is on and the device's own music player is idle. `PlaySound` `0x1A448` plays only when effects are on.
- `AudioManager::Play` `0x160910` creates a new session (an OpenAL source) for practically every call (the exception is a non-streamed clip longer than 8 s played looping); the session sets only its buffer, a zero velocity and looping, then plays it (`AudioSession::Play` `0x1872C6`). There is **no gain call** anywhere in the binary, so every track and effect plays at gain 1.0. Weapon sounds are never stopped.
- **Loudness differences are in the files.** The menu track is about 4.6 dB quieter (RMS) than the City battle track, and the Shamo track is quieter still. The port plays them through one 2D source at full volume, as the original does.
- **Cadence** comes from animation key frames. An automatic weapon fires on its trigger frames, which works out to about 0.117 s per shot at 60 Hz. The flame and laser repeat about every 0.2 s.
- Clips started before quitting a mission play to their end, as on the iPad: the enemy roar is a 5.6 s clip.
