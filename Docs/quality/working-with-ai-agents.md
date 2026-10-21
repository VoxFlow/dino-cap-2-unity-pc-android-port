# Working with AI agents on a project like this

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../start/disclaimer.md).*

- **Recommended: one main agent, with helpers.** One agent owns the project and its decisions; it hands bounded jobs (searches, reviews, long test runs) to helper agents and checks what they return. Two agents never write the same files at once. In this project builder and reviewer ran sequentially, and work was handed over through files, not memory.
- **Rules in one file, facts in records.** The working rules live in one short rule file; facts, evidence and history live in registries and reports. A new rule had to say which mistake it prevents.
- **Evidence labels everywhere.** NATIVE, OBSERVED (user or video), PILOT_ASSUMPTION, ADAPTATION; and PROVEN, LIKELY, POSSIBLE, UNRESOLVED, FALSIFIED. Agents were not allowed to upgrade an inference to a fact.
- **No premature "done".** Implemented, test passed, build passed and accepted on a device are separate states, and a parent task stays open until every row is closed or explicitly accepted.
- **Contradictions are said out loud.** When a user observation contradicted the code, or the code contradicted an earlier claim, the agent had to state it, record it and keep the old entry marked rather than rewrite it.
- **Independent review catches what the builder cannot.** Codex reviews found real defects under passing tests: hit reactions, a shotgun distance, a level-up comparison, a cash cap, a cash write-back, and privacy leaks in a build.
- **User decisions stay with the user.** Adaptations, workarounds and publication choices were asked as explicit questions, then recorded with the user's words.

For the failures behind these rules, see the [failure ledger](failure-ledger.md) and the [policy history](policy-history.md).
