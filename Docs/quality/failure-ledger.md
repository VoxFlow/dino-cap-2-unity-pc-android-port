# Failure ledger: what went wrong, how it was found, and the fix

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../start/disclaimer.md).*

Material failures, grouped by pattern. The actor is named only where known; causes are marked as hypotheses where not verified.

| Pattern | Example | Found by | Fix | Recurrence prevention |
|---|---|---|---|---|
| Field read by assumed meaning | Animation current frame (+68) read as a loop bound; the laser froze dinosaurs | User report, then a code sweep | State-4 loop 2–4; the shock release from frame 6 | Sweep every handler that reads the same field |
| Literal read by eye | `0x3D4CCCCD` read as 0.02; burn-death 160/70 read as 150/72 | User reports, decoding | Values corrected | `check_float_claims.py` and a decode-every-literal rule |
| Wrong id from a neighbour | Weapon icon given id 20 (the Mission Complete band's id) | User report | Native ids from OnLoad emulation | Test of every HUD id |
| Wrong explanation kept briefly | Ammo-icon cause first blamed on a deleted icon | Next batch | Superseded, marked | Explanations need the code path, not a plausible story |
| Missing native write | Negative cash clamped only on screen | Codex audit | Write-back ported | Check write → store → read chains |
| Host-level timing | Animation speed tied to the monitor rate; mobile default of 30 fps | Frame-rate test, mobile review | 60 Hz host clock; 60 fps on mobile | `PilotFrameRateVerify` at non-60 Hz rates |
| Test agreed with the bug | Tests for HUD ids and wide edges encoded the port's assumption | User reports, reviews | Tests rewritten to native values | Native oracles only |
| Privacy in artifacts | The Windows DLL embedded the build machine's path; logs about to be committed | Codex audit | Neutral-path build; logs ignored | Scan built files and the exact commit set |
| User misobservation | Truck order, the map boss ring, boss shadow on the iPad | User's own follow-up | Recorded as the user's correction | Report outcome classes kept separate |
| Harness, not game | A test switching scenes without loading their assets | Test exception | Test moved to the right harness | Rerun a failure alone before calling it a regression |
