# Glossary

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](disclaimer.md).*

| Term | Meaning |
|---|---|
| **Native** | Taken from the original binary or its data files |
| **ARM / Thumb** | The original's CPU instruction sets; most DC2 code is Thumb (16/32-bit instructions) |
| **EA / RVA** | IDA's static address of an instruction; the same address minus the imagebase `0x1000` ([conventions](../technical/binary/ADDRESS_CONVENTIONS.md)) |
| **Hex-Rays / pseudocode** | IDA's decompiler output; useful, but the machine instructions win when they disagree |
| **binary32** | Single-precision float arithmetic. The original computes in it, and so does the port (rounding differences change results) |
| **Rule / contract** | A recovered fact about the original, recorded with status and evidence ([registry](../technical/registry/README.md)) |
| **PROVEN / LIKELY / POSSIBLE / UNRESOLVED / FALSIFIED** | How strongly the evidence supports a rule; FALSIFIED means shown wrong (kept, marked) |
| **ADAPTATION** | A change from the original approved by the user, e.g. wide view (AD-001..007) |
| **Platform layer** | PC or Android support with no gameplay effect of its own (PF-001..004) |
| **Stand-in (PILOT_ASSUMPTION)** | A labelled placeholder for a value the original's code has not yet revealed |
| **Verifier** | One of the 46 automated test programs, run headless at 60 Hz |
| **Native oracle** | A test answer recovered independently from the original, never from the port |
| **Wide view** | Showing more of the world left and right on screens wider than 4:3 (capped at 21:9) |
| **CA / CAObjCallBack** | The original's composite-animation player; the port reimplements it |
| **Scene state** | The battle scene's mode field: 1 intro, 4 playing, 6 paused, 7 failed, ... |
| **User A/B** | The user comparing the port side by side with the real iPad |
