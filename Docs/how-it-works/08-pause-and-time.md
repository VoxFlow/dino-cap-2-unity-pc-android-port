# 8. Pause and the flow of time

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../start/disclaimer.md).*

> **In simple terms.** The original ticks 60 times a second, and its animations advance one frame per tick. The port ticks the same way, so animation speed does not depend on your monitor.

- **Host clock.** `EngineAppDelegate` schedules an `NSTimer` at 1/60 s. The shell measures the elapsed time, caps it at 0.1 s and updates the game once.
- **The port's clock.** It ticks at 60 Hz with `dt = 1/60`. When the device falls behind, it does one catch-up tick with the elapsed time (capped at 0.1 s), as the shell does.
- **Why this matters.** `CAObjCallBack::OnUpdate` advances an animation by **one frame per update** and drops the remainder. Ticking once per monitor frame made 144/240 Hz PCs animate faster, and Unity's mobile default of 30 fps would have halved the speed; both were port defects, now fixed and tested.
- **Order inside a tick:** queued touches, then timers, then physics, then object updates.
- **Pause** (`OnSystemEvent(0)` or the stop button, only while playing): time factor 0, the pause menu, and the held-button flags cleared. On iOS the interruption event comes from `applicationWillResignActive`; on Android the port maps focus loss to the same event. On PC this is optional and off by default.
