import os, re, struct
root = os.environ.get('DC2_ROOT') or os.path.abspath(os.path.join(os.path.dirname(os.path.abspath(__file__)), '..', '..'))   # project root (holds Dev/ and DC2-Dev/)
f = lambda v: struct.unpack('<f', struct.pack('<I', v & 0xffffffff))[0]
pat = re.compile(r'(-?\d{9,10})\)?\s*(?:=|->|is|:)\s*\(?(-?\d+\.?\d*)')
bad = 0; total = 0
for sub in ('Dev/Evidence', 'DC2-Dev/Assets/Scripts', 'Dev/Reports/DC2-PILOT-001/checkpoint.md'):
    base = os.path.join(root, sub)
    files = [base] if os.path.isfile(base) else [os.path.join(d, x) for d, _, fs in os.walk(base) for x in fs if x.endswith(('.md', '.json', '.jsonl', '.cs'))]
    for fp in files:
        try: txt = open(fp, encoding='utf-8', errors='ignore').read()
        except Exception: continue
        for ln, line in enumerate(txt.splitlines(), 1):
            for m in pat.finditer(line):
                iv = int(m.group(1))
                if abs(iv) < 0x3f000000 // 2 and abs(iv) < 100000000: continue
                total += 1
                got = f(iv); claimed = float(m.group(2))
                if abs(got - claimed) > 1e-4 * max(1.0, abs(got)):
                    bad += 1; print('%s:%d  %d -> %s but text says %s' % (os.path.relpath(fp, root), ln, iv, got, claimed))
print('checked', total, 'mismatches', bad)
