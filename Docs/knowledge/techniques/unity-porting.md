# Porting recovered behaviour to Unity

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../start/disclaimer.md).*

- **Unity components are only vessels.** They must not add timing, damage, ranges, state transitions or asset meaning of their own.
- **Reproduce the host loop:** the original's tick rate, time-step cap and update order (touches → timers → physics → objects).
- **Use the original's arithmetic.** Use binary32 helpers (`N.Add32`, `N.Mul32`, `N.F32`) where the original computes in single precision; C# may otherwise evaluate in double and change a truncated result (19 instead of 20).
- **Keep the logical space,** and make screen adaptations explicit rules that reduce to the original at the native aspect (`WideView.cs`).
- **Cite addresses in comments,** so a reader and the [source-to-Unity map](../../technical/maps/SOURCE_TO_UNITY_MAP.md) can find the native code behind a line.
- **Label every non-original piece** and record it with a removal condition.
- **Performance work must be behaviour-neutral:**
  - Fix allocation in helpers, for example a rounding helper that allocated per call.
  - Prefer dirty flags over per-frame rebuilds.
  - Re-run the full suite afterwards.
