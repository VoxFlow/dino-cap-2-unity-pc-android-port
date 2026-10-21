# Validating at runtime

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../start/disclaimer.md).*

- **Native oracles:** test answers recovered from the original, sometimes by emulating an original function on fixed inputs, never copied from the port.
- **Step at the native rate,** and also at other host rates, to catch frame-rate dependence.
- **Headless tests prove logic, not pictures.** Visual claims need captures compared with references, and device acceptance.
- **Rerun a failure alone** before calling it a regression; tell environment errors apart from game errors.
- **Run long suites in parallel** on project copies.
- **Bind device evidence to artifact hashes:** which exact build was tested, on which device and OS, with logs or screenshots kept.
- **Scan built artifacts for privacy,** not just the source: binaries can embed build-machine paths.
