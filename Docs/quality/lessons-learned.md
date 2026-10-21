# Lessons learned

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../start/disclaimer.md).*

1. **A field's meaning needs proof from its consumer.** The costliest repeated error was reading a value by its assumed meaning: an animation field taken for a loop bound, a control id taken from a neighbour, a literal read by eye. The fix each time was to read the code that *uses* the value.
2. **Decode constants with a tool.** `0x3D4CCCCD` is 0.05, not 0.02. One misread spread to eight places before it was caught. Now every literal is decoded with `struct`.
3. **The host loop is game code too.** The original advances animations one frame per tick. Ticking per monitor frame (PC) or at 30 fps (mobile default) changed the game speed. The shell's 60 Hz timer had to be reproduced like any other function.
4. **User reports are evidence, not instructions.** Each reported difference was traced to the code. Many were real defects; several were original quirks that had to be *kept*; a few were misobservations the user later withdrew. All three outcomes were recorded.
5. **Tests can agree with a wrong port.** Several defects lived under passing tests, because a test can encode the same misunderstanding as the code. Independent reviews (Codex) and native oracles caught what self-written tests did not.
6. **Wide screens need explicit rules.** Showing more world changes gameplay at the edges: reach, spawns, the boss tail, the egg walk. Each such case was made an explicit, user-approved adaptation that reduces to the original at 4:3.
7. **Privacy hides in binaries.** A text scan of the sources found nothing, but the Windows build had the build machine's path embedded in a DLL. Scan the built artifacts, not just the source.
8. **Coverage numbers need definitions.** "Traced" and "tested" are different claims; one counter mixing "ported or traced" overstated progress until it was split.
