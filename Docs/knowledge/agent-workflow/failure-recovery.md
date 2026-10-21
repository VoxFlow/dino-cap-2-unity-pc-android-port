# AI agents: failure and recovery

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../start/disclaimer.md).*

- **Typical failure modes seen here:**
  - reading a value by its assumed meaning;
  - copying a constant by eye;
  - a plausible explanation without a code path;
  - tests that encode the bug;
  - closing a task while rows were still open;
  - a privacy leak in a built artifact.
- **What caught them:**
  - user device comparisons;
  - independent reviewers with their own probes;
  - sweeps for the same pattern after each fix;
  - decoding tools.
- **Recovery protocol:**
  1. State the contradiction.
  2. Mark the old claim FALSIFIED or SUPERSEDED with its successor.
  3. Fix it with a discriminating test.
  4. Check the neighbours that share the mechanism.
- **Don't blame by default.** Separate the observed failure from its hypothesized cause; a long context is not proof of "context decay".
