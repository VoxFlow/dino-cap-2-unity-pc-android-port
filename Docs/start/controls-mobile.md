# Controls for phones and tablets

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](disclaimer.md).*

> **In simple terms.** You use the same on-screen buttons as on the iPad, with real multi-touch, in landscape.

- **Touch** runs through the touch handlers and button areas recovered from the original code. In wide view the buttons are anchored to the screen edges, and you can walk and shoot at the same time.
- **Landscape only**, left or right, as in the original's `Info.plist` (`UISupportedInterfaceOrientations`).
- **Interruptions pause the game during play** (a call, the notification shade). This is the original's behaviour: `-[EngineAppDelegate applicationWillResignActive:]` calls the shell's `SystemEvent(0)`, which reaches the scene's pause handler. The handler acts only while a mission is being played. *Not yet tested on a phone.*
- **60 updates per second**, like the original's 60 Hz timer. Unity's mobile default of 30 would have halved the animation speed.
- **Phones with a notch:** the edge buttons move inward just enough to clear the cutout; the art and the touch area move together (PF-004).
- The screen does not dim while the game is open (not original). On Android the system bars are set to hidden: swipe from the edge to show them. *Immersive mode is not yet checked on a phone.*

Tested on a low-tier Android phone (32-bit system): 60 fps in battle. See the [validation matrix](../release/build-validation-matrix.md).
