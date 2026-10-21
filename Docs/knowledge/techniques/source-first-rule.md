# Rule: the original's bytes and instructions come first

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../start/disclaimer.md).*

**Order of evidence:**
1. the original binary and raw asset bytes;
2. machine instructions and direct data flow;
3. callers, callees and lifecycle;
4. parsed original data files;
5. decompiler output;
6. analogues (another version, a sister game);
7. inference.

**In practice:**
- Decode every numeric literal with a tool.
- Check signedness, width, rounding and float precision on the instructions.
- A function body proves what it does *when called*, not when, how often, or in which mode. Find the caller, the trigger and the state gate.
- "Not found" is not "does not exist": absolute words (only, never, always) need exhaustive evidence.
- Keep odd original behaviour; never "normalize" it.
- Use video and screenshots to *falsify* a port or to show where to look, never to choose a value.

**When it does not apply:** platform layers with no original counterpart (window handling, PC keys) are decisions, not recoveries. Label them and keep them out of the game rules.
