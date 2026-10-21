# Open questions

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../start/disclaimer.md).*

What is not known, or not yet checked. Each item says what would close it.

| Topic | State | What would close it |
|---|---|---|
| Casing lifetime | The user remembers ejected casings disappearing sooner on the iPad. The port uses the original's casing constants and bounce rule (`bulletClass::OnUpdate` `0x77A4`: 5 bounces, about 1.1 s), and an independent simulation agrees. | A frame-by-frame recording of a single shot on the iPad. |
| Hit reactions in dinosaur states 4–6 | The "no write" rows are proven on ARM. The hit-animation rows are LIKELY, inferred because the sampled rows for states 1, 7 and 8 are identical. Twelve ARM cases for weapon types 4 and 6 in state 5 matched in the pre-publication audit. | Exhaustive ARM coverage of every weapon type and skin. |
| Girl invincibility flash start value (PA-013) | Not written by her constructor or setup code, so the start value before her first hit is unknown. The port uses 1.0 as a labelled stand-in. | Tracing the object's allocation and zero-fill lifecycle. |
| Early pilot stand-ins (PA-005, PA-011) | Still bounded or open: some UI update-order details, and residue from hero/girl placement and physics. | Each entry's removal condition in [`pilot_assumptions.json`](registry/pilot_assumptions.json). |
| 142 traced-but-untested core functions | Some port mappings in this group need reconciling (the audit sampled 10). | A dedicated check per function, or a recorded reason why none is needed. |
| Two ledger symbols without a function | `LoadImageHight` `0x6F89C` and `mainScene::OnTimeout` `0xB6C94` are not defined as functions in the analysis database. | Defining them in a disposable copy and exporting their dossiers. |
| Device checks not yet done | Interruption-to-pause, Android immersive mode and the notch inset have source implementations but no recorded device test. Physical keyboard and focus-loss scenarios are not certified. | A recorded device session for each. |
| Overall readiness | The project's strict gate is still `NOT_READY_FOR_USER_RUNTIME_AB`: playable is not the same as every term proven and accepted on a device. | Every open row above closed or accepted. |
