"""Print ledger counts by verdict / status and the coverage figure used in checkpoints (code functions only: DATA / TRIVIAL / EXCLUDED / DEAD / ENGINE_GLUE removed from the denominator)."""
import json, os, collections
ROOT = os.environ.get('DC2_ROOT') or os.path.abspath(os.path.join(os.path.dirname(os.path.abspath(__file__)), '..', '..'))   # project root (holds Dev/)
rows = [json.loads(l) for l in open(os.path.join(ROOT, 'Dev', 'Evidence', 'function_ledger.jsonl'), encoding='utf-8')]
c = collections.Counter((r['verdict'], r['status']) for r in rows)
for k, v in sorted(c.items()): print(k, v)
live = [r for r in rows if r['verdict'] in ('UNSET', 'GAMEPLAY', 'UI_FLOW')]
done = [r for r in live if r['status'] in ('VERIFIED',)]
seen = [r for r in live if r['status'] != 'UNTRACED']
print('in-scope functions', len(live), 'name/addr evidenced', len(seen), '%.1f%%' % (100 * len(seen) / max(1, len(live))), 'VERIFIED', len(done))
tested = [r for r in live if r['status'] in ('VERIFIED', 'TEST_EXECUTED')]
print('tested (VERIFIED + TEST_EXECUTED by passing verifiers, runtime coverage)', len(tested), '%.1f%%' % (100 * len(tested) / max(1, len(live))),
      '| traced/ported without test proof', sum(1 for r in live if r['status'] in ('PORTED_OR_TRACED', 'TRACED', 'PORTED')),
      '| untraced', sum(1 for r in live if r['status'] == 'UNTRACED'))
