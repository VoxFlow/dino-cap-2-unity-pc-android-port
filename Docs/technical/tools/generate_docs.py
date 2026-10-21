"""Generate DC2-Dev/Docs/technical from the IDA export (export_ida.py), the function ledger, the source-truth registry and the Unity code.

Environment: DC2_EXPORT (export_ida.py output), DC2_DEV (the Dev folder: Evidence/, SourceTruth/), DC2_UNITY (the Unity project root), DC2_BINREC (binaries.jsonl).
Everything written goes to DC2_UNITY/Docs/technical. Links are repository-relative so they work on GitHub (file links open at the cited line).
"""
import os, re, json, bisect, hashlib, shutil, collections

EXP, DEV, UNITY, BINREC = (os.environ[k] for k in ('DC2_EXPORT', 'DC2_DEV', 'DC2_UNITY', 'DC2_BINREC'))
OUT = os.path.join(UNITY, 'Docs', 'technical')
BIN_ID = 'DC2-IOS13-ARM32-SUB6'
DISC = '> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer]({up}start/disclaimer.md).*\n'

def jl(p): return [json.loads(l) for l in open(p, encoding='utf-8') if l.strip()]
def w(rel, text):
    p = os.path.join(OUT, rel); os.makedirs(os.path.dirname(p), exist_ok=True)
    open(p, 'w', encoding='utf-8', newline='\n').write(text)
def up(rel):  # relative prefix from a page in OUT to Docs/
    return '../' * (rel.count('/') + 1)
def hx(v): return int(v, 16)

funcs = jl(os.path.join(EXP, 'functions.jsonl'))
byea = {hx(f['ea']): f for f in funcs}
# every chunk of every function (entry chunk and tails): an address belongs to the function whose chunk holds it
spans = sorted((hx(c['start']), hx(c['end']), hx(f['ea'])) for f in funcs for c in (f.get('chunks') or [{'start': f['ea'], 'end': '0x%X' % (hx(f['ea']) + f['size'])}]))
span_starts = [a for a, b, e in spans]
def containing(ea):
    i = bisect.bisect_right(span_starts, ea) - 1
    if i < 0: return None
    a, b, owner = spans[i]
    return byea[owner] if ea < b else None
def code_bytes(f): return sum(hx(c['end']) - hx(c['start']) for c in f.get('chunks') or []) or f['size']
# code IDA placed in no function, reached by a branch or fall-through from a function (export_ida.py walk_unowned)
unowned = {hx(u['reached_from']): u for u in jl(os.path.join(EXP, 'unowned_blocks.jsonl'))} if os.path.exists(os.path.join(EXP, 'unowned_blocks.jsonl')) else {}
uspans = sorted((hx(b['start']), hx(b['end']), fea) for fea, u in unowned.items() for b in u['blocks'])
def reached_unowned(ea):
    return [fea for a, b, fea in uspans if a <= ea < b]
disasm = {hx(d['ea']): d.get('instructions', []) for d in jl(os.path.join(EXP, 'core_disasm.jsonl'))}
refs = {hx(d['ea']): d for d in jl(os.path.join(EXP, 'core_refs.jsonl'))}
edges = jl(os.path.join(EXP, 'callgraph_all.jsonl'))
classes = jl(os.path.join(EXP, 'classes.jsonl'))
binary = json.load(open(os.path.join(EXP, 'binary.json')))
ledger = {hx(r['addr']): r for r in jl(os.path.join(DEV, 'Evidence', 'function_ledger.jsonl'))}
core = sorted(ea for ea, f in byea.items() if f['core'])
registry = json.load(open(os.path.join(DEV, 'SourceTruth', 'source_contract_registry.json'), encoding='utf-8'))
rules = registry['contracts']
if isinstance(rules, dict):
    rules = [dict(v, id=v.get('id', k)) if isinstance(v, dict) else {'id': k, 'proposition': str(v)} for k, v in rules.items()]

def cls_of(f):
    m = re.match(r'(?:DinoCap::)?([A-Za-z_][A-Za-z0-9_]*)::', f['demangled'] or '')
    return m.group(1) if m and f['demangled'].startswith('DinoCap::') else ('DinoCap-free' if (f['demangled'] or '').startswith('DinoCap::') else 'other')
def short(f):
    d = f['demangled'] or f['name']
    return d.replace('DinoCap::', '').split('(')[0]
def did(ea): return 'FUN-%06X' % ea
def dpath(ea): f = byea[ea]; return 'functions/dossiers/%s/%s.md' % (cls_of(f), did(ea))

# ---- rules citing each function (any 0xNNNN in the rule text that falls inside it) ----
rule_cites = collections.defaultdict(list)
for r in rules:
    t = json.dumps(r)
    hit = set()
    for m in re.finditer(r'0x([0-9a-fA-F]{4,6})\b', t):
        f = containing(int(m.group(1), 16))
        if f: hit.add(hx(f['ea']))
    for ea in hit: rule_cites[ea].append(r)

# ---- Unity code references: address citations and Class::Method names in Assets/Scripts ----
name_index = collections.defaultdict(list)
for ea, f in byea.items():
    if f['demangled'] and f['demangled'].startswith('DinoCap::'):
        name_index[short(f)].append(ea)
unity_refs = collections.defaultdict(list)   # ea -> [(path, line, cited_ea_or_None)]
unowned_cites = collections.defaultdict(list)   # reached-from ea -> [(path, line, cited_ea)] for addresses in unowned code
cited_addrs = collections.defaultdict(set)
scripts = os.path.join(UNITY, 'Assets', 'Scripts')
for fn in sorted(os.listdir(scripts)):
    if not fn.endswith('.cs'): continue
    rel = 'Assets/Scripts/' + fn
    for ln, line in enumerate(open(os.path.join(scripts, fn), encoding='utf-8', errors='replace'), 1):
        seen = set()
        for m in re.finditer(r'0x([0-9a-fA-F]{4,6})\b', line):
            a = int(m.group(1), 16)
            if a < 0x2000 or a > 0x1F0000: continue
            f = containing(a)
            if f and hx(f['ea']) not in seen:
                seen.add(hx(f['ea'])); unity_refs[hx(f['ea'])].append((rel, ln, a)); cited_addrs[hx(f['ea'])].add(a)
            elif not f:
                for fea in reached_unowned(a): unowned_cites[fea].append((rel, ln, a))
        for m in re.finditer(r'\b([A-Za-z_][A-Za-z0-9_]*::[A-Za-z_~][A-Za-z0-9_]*)\b', line):
            for ea in name_index.get(m.group(1), []):
                if ea not in seen:
                    seen.add(ea); unity_refs[ea].append((rel, ln, None))

callers = collections.defaultdict(list); callees = collections.defaultdict(list)
for c, site, t, k, *tgt in edges:
    callers[hx(t)].append((hx(c), hx(site), k)); callees[hx(c)].append((hx(t), hx(site), k))

def flink(ea, frm):
    f = byea.get(ea)
    if not f: return '`0x%X`' % ea
    if f['core']: return '[`%s`](%s%s)' % (short(f), '../' * frm.count('/'), dpath(ea))
    return '`%s`' % short(f)

# ---- curated notes for deep dossiers (each points to the batch that established it) ----
DEEP = {
 0x11ab0: None,
}
DEEP_NOTES = {
 'DinoCap::dinosaurClass::OnAnimationEnd': 'State 4 (beam hold): when the current frame (CAObjCallBack +68) equals 4, playAnimation(0, 2, 0.025, 4) loops frames 2..4, so a held laser makes the dinosaur twitch and take a hit on each pass of key frame 3; otherwise the state becomes 8. The port first froze the dinosaur on frame 4 (field +68 read as a bound): corrected in batch r119k.',
 'DinoCap::dinosaurClass::DieShaoSi': 'Burn death: y = S*160*0.8 + g_ground_y, minus 70 on iPad (literals 0x43200000 = 160, 0x3F4CCCCD = 0.8, 0x428C0000 = 70); skins 1, 2, 5 get that y and scale 0.8; skin 3 only sets scale 1.0 (0x10AD4) and branches past the position write (0x10AD8 -> 0x10B2A), so it keeps its position; skin 5 also moves x by 40 against its facing. The port first used 150 and 72 (ash 18 px too low): corrected in batch r119w.',
 'DinoCap::dinosaurClass::BeAttacked': 'Hit handling for guns: damage with distance falloff measured between the dinosaur\'s and the hero\'s origins (not the head used for target selection), then a non-lethal reaction tail that branches only on state == 0 vs != 0. Review R002 corrected both points (B1, B2).',
 'DinoCap::selectScene::UpdateExp': 'Menu cash display: loads the global cash, clamps a negative value to 0 and stores it back before printing it with commas. Found by the pre-publication audit (REL-12) and fixed: the port had clamped only the displayed text.',
 'DinoCap::playerClass::OnRollDown': 'Roll gate: refused only while the hero state (+640) is 7 (rolling) or 4, or during the 3 s rollEndTime flag (+141). Mission Complete stands the hero up (state 2), so rolling after completion is allowed in every mission except the run mission, whose end writes state 4 every frame.',
 'DinoCap::playerClass::UpdateCamera': 'Camera and hero bounds per mission mode: run end (state 4 every frame, GameClear), egg delivery (count, EggFull, Stand), and the life-mode clamp of the hero to the camera bounds. Wide-view adaptations AD-002 and AD-005 build on these bounds.',
 'DinoCap::lifeModeScene::OnTouchEvent': 'All battle touches: one finger id per control, arrow takeover on slide, release-rect rules for the weapon and change buttons, pause from the stop rect only while the scene state is 4. The PC keys and mouse press these same rects.',
 'DinoCap::mainScene::GameClear': 'Mission complete: stops spawning, stands the hero, clears the held-button flags and draws the fail_line band (control 20). It does not guard against a later death: a hero killed afterwards gets GameOverStart on top (kept as an original quirk).',
 'DinoCap::mainScene::GameOverStart': 'Mission failed: the red missionfailbg image (control 11) and the fail line (control 17), scene state 7. Callers: hero death (playerClass / playerNewAniClass OnAnimationEnd) and SubTime (map type 6 only, not in the shipped Map.xml).',
 'DinoCap::playerClass::Attack': 'Gun hit scan: nearest target by head position (tou, com6 for skin 5), group weapons hit every candidate, armoured hits roll a 50 % metal sound (A_shot_tie).',
 'DinoCap::bulletClass::OnUpdate': 'Shell casing: below g_ground_y it bounces (y mirrored about the ground, vy * -0.5, spin + 2) and disappears once |vy| < 50. No timer: the life comes from the physics (5 bounces, about 1.1 s for a hero shot).',
 'DinoCap::lifeModeScene::OnLoad': 'Battle HUD creation order and control ids (weapon icon 8 on layer 2, stop icon 15, gold 43, roll 28, change 48 in life-mode scenes). The port once used id 20 for the weapon icon, which the Mission Complete band replaced: corrected in batch r119m.',
}
deep = {ea for ea in core if (byea[ea]['demangled'] or '').split('(')[0] in DEEP_NOTES}

def excerpt(ea):
    ins = disasm.get(ea, [])
    if not ins: return '_No disassembly exported._\n'
    out = ['**Entry (first instructions)**\n', '```']
    for a, b, t in ins[:24]: out.append('%-9s %-10s %s' % (a, b, t))
    out.append('```')
    idx = {hx(a): i for i, (a, b, t) in enumerate(ins)}
    for c in sorted(cited_addrs.get(ea, [])):
        near = min(idx, key=lambda x: abs(x - c)) if idx else None
        if near is None or abs(near - c) > 4 or idx[near] < 24: continue
        i = idx[near]
        out += ['', '**Around `0x%X` (cited by the Unity code)**' % c, '', '```']
        for a, b, t in ins[max(0, i - 6): i + 7]: out.append('%-9s %-10s %s%s' % (a, b, t, '   <==' if hx(a) == near else ''))
        out.append('```')
    return '\n'.join(out) + '\n'

# ---- dossiers ----
for ea in core:
    f = byea[ea]; rel = dpath(ea); u = up(rel)
    L = ledger.get(ea, {})
    rc = rule_cites.get(ea, [])
    ur = unity_refs.get(ea, [])
    dn = (f['demangled'] or '').split('(')[0]
    lines = ['# %s' % short(f), '', DISC.format(up=u), '| Field | Value |', '|---|---|',
             '| Function ID | `%s` |' % did(ea), '| Binary | [`%s`](%sbinary/ADDRESS_CONVENTIONS.md) |' % (BIN_ID, '../' * (rel.count('/'))),
             '| Static address (EA) | `%s` |' % f['ea'], '| RVA (EA - 0x1000) | `%s` |' % f['rva'], '| File offset | `%s` |' % f['file_offset'],
             '| Size | %d code bytes in %d chunk(s), %d instructions |' % (code_bytes(f), len(f.get('chunks') or [1]), len(disasm.get(ea, []))), '| Instruction set | %s |' % f['isa'],
             '| Name | `%s` (%s) |' % (f['name'], f['name_provenance']), '| Demangled | `%s` |' % f['demangled'],
             '| Bytes SHA-256 | `%s` |' % f['bytes_sha256'], '| Direct code callers | %d (virtual-table and function-pointer calls are not counted) |' % f['direct_callers'],
             '| Ledger verdict / status | %s / %s |' % (L.get('verdict', '?'), L.get('status', '?')),
             '| Depth | %s |' % ('deep: curated note below, from the batch that established it' if ea in deep else 'generated from the database export; interpretation limited to the cited rules and code comments'), '']
    lines += ['## What it does', '']
    if ea in deep: lines += [DEEP_NOTES[dn], '']
    if rc:
        for r in rc[:6]:
            lines.append('- Rule `%s` (%s, %s): %s' % (r['id'], r.get('status'), r.get('provenance'), (r.get('proposition') or '')[:420].replace('\n', ' ') + ('...' if len(r.get('proposition') or '') > 420 else '')))
        if len(rc) > 6: lines.append('- ... %d more rules cite addresses in this function (see [claims.jsonl](%sclaims.jsonl)).' % (len(rc) - 6, '../' * rel.count('/')))
        lines.append('')
    if not rc and ea not in deep:
        lines += ['No recovered rule in the published registry cites an address inside this function. Ledger note: %s' % (L.get('reason') or 'none'), '']
    lines += ['## Where the port implements or cites it', '']
    if ur:
        lines.append('Mechanical scan of the Unity code: a comment or name that cites the function is a reference, not proof that the code implements all of it.')
        lines.append('')
        for p, ln, a in ur[:20]:
            lines.append('- [`%s:%d`](%s%s#L%d)%s' % (p, ln, '../' * (rel.count('/') + 2), p, ln, (' cites `0x%X`' % a) if a else ' (by name)'))
        if len(ur) > 20: lines.append('- ... %d more' % (len(ur) - 20))
    else:
        lines.append('**Unmapped**: no Unity code comment cites this function. Unmapped is not proof that the behaviour is absent.')
    lines += ['', '## Callers and callees (direct code references)', '']
    cs = sorted(set(c for c, s, k in callers.get(ea, [])))
    ce = sorted(set(t for t, s, k in callees.get(ea, [])))
    lines.append('Callers: ' + (', '.join(flink(c, rel) for c in cs[:30]) + (' ... (%d)' % len(cs) if len(cs) > 30 else '') if cs else 'none found (it may be called through a virtual table or function pointer).'))
    lines.append('')
    lines.append('Callees: ' + (', '.join(flink(c, rel) for c in ce[:40]) + (' ... (%d)' % len(ce) if len(ce) > 40 else '') if ce else 'none.'))
    rf = refs.get(ea, {})
    if rf.get('strings'):
        lines += ['', '## Strings it references', ''] + ['- `%s` "%s"' % (a, s.replace('|', '\\|')[:120]) for a, s in list(rf['strings'].items())[:30]]
    if rf.get('globals'):
        lines += ['', '## Named data it references', ''] + ['- `%s` %s' % (a, g) for a, g in list(rf['globals'].items())[:30]]
    if ea in unowned:
        u = unowned[ea]
        lines += ['', '## Code reached from this function outside every IDA function', '',
                  'IDA placed these blocks in no function. They are reached by a branch or fall-through from this function, which shows reachability, not ownership. They are listed here so that no reached code is silently missing.', '']
        for b in u['blocks']:
            lines.append('- `%s`..`%s` (reached from `%s`), bytes SHA-256 `%s`, %d instructions in [`unowned_blocks.jsonl`](%sgraphs/unowned_blocks.jsonl)' % (b['start'], b['end'], b['reached_from_site'], b['bytes_sha256'][:16] + '...', len(b.get('instructions', [])), '../' * rel.count('/')))
        for g in u['gaps']:
            lines.append('- Gap: `%s` -> `%s`: %s.' % (g['site'], g['target'], g['why']))
        for p, ln, a in unowned_cites.get(ea, [])[:20]:
            lines.append('- Unity code [`%s:%d`](%s%s#L%d) cites `0x%X` inside such a block.' % (p, ln, '../' * (rel.count('/') + 2), p, ln, a))
    lines += ['', '## Native instructions (excerpts)', '', 'Exact bytes and IDA 9.4 rendering of the analysed binary. Interpretation is in the rules and notes above, not in the listing.', '', excerpt(ea)]
    w(rel, '\n'.join(lines) + '\n')

# ---- functions.jsonl, address index, per-class pages ----
shutil.copy(os.path.join(EXP, 'functions.jsonl'), os.path.join(OUT, 'functions', 'functions.jsonl'))
rows = ['# Address index: core functions', '', DISC.format(up='../../'),
        'Binary `%s` (thin Mach-O, header CPU type 12 / subtype 6, ARMv6; code mostly Thumb). EA = IDA static address; RVA = EA - %s; in `__text` the file offset equals the RVA. These are the %d in-scope functions of the ledger, sorted by address; all %d functions are in [`functions.jsonl`](functions.jsonl).' % (BIN_ID, binary['imagebase'], len(core), len(funcs)),
        '', '| EA | RVA | Size | ISA | Function | Class | Rules | Unity refs | Ledger status | Dossier |', '|---|---|---:|---|---|---|---:|---:|---|---|']
for ea in core:
    f = byea[ea]
    rows.append('| `%s` | `%s` | %d | %s | `%s` | %s | %d | %d | %s | [%s](%s) |' % (f['ea'], f['rva'], code_bytes(f), f['isa'], short(f).replace('|', '\\|'), cls_of(f),
                len(rule_cites.get(ea, [])), len(unity_refs.get(ea, [])), ledger.get(ea, {}).get('status', '?'), 'deep' if ea in deep else 'auto', dpath(ea).replace('functions/', '')))
w('functions/ADDRESS_INDEX.md', '\n'.join(rows) + '\n')
bycls = collections.defaultdict(list)
for ea in core: bycls[cls_of(byea[ea])].append(ea)
sub = ['# Core functions by class', '', DISC.format(up='../../'), 'The binary keeps its C++ symbols, so functions are grouped by their real class.', '', '| Class | Core functions | Mapped in Unity | Cited by rules |', '|---|---:|---:|---:|']
for c in sorted(bycls):
    eas = bycls[c]
    sub.append('| [%s](by-class/%s.md) | %d | %d | %d |' % (c, c, len(eas), sum(1 for e in eas if unity_refs.get(e)), sum(1 for e in eas if rule_cites.get(e))))
    page = ['# %s' % c, '', DISC.format(up='../../../'), '| EA | Function | Ledger status | Unity refs | Dossier |', '|---|---|---|---:|---|']
    for e in eas:
        page.append('| `%s` | `%s` | %s | %d | [%s](../dossiers/%s/%s.md) |' % (byea[e]['ea'], short(byea[e]), ledger.get(e, {}).get('status', '?'), len(unity_refs.get(e, [])), 'deep' if e in deep else 'auto', c, did(e)))
    w('functions/by-class/%s.md' % c, '\n'.join(page) + '\n')
w('functions/CLASSES_INDEX.md', '\n'.join(sub) + '\n')

# ---- call graph ----
shutil.copy(os.path.join(EXP, 'callgraph_all.jsonl'), os.path.join(OUT, 'graphs', 'callgraph_all.jsonl')) if os.path.isdir(os.path.join(OUT, 'graphs')) else (os.makedirs(os.path.join(OUT, 'graphs')), shutil.copy(os.path.join(EXP, 'callgraph_all.jsonl'), os.path.join(OUT, 'graphs', 'callgraph_all.jsonl')))
coreset = set(core)
core_edges = [e for e in edges if hx(e[0]) in coreset and hx(e[2]) in coreset]
json.dump([e[:3] for e in core_edges], open(os.path.join(OUT, 'graphs', 'callgraph_core_edges.json'), 'w'), indent=0)
if os.path.exists(os.path.join(EXP, 'unowned_blocks.jsonl')): shutil.copy(os.path.join(EXP, 'unowned_blocks.jsonl'), os.path.join(OUT, 'graphs', 'unowned_blocks.jsonl'))
kc = collections.Counter(e[3] for e in edges)
most = collections.Counter(hx(e[2]) for e in edges).most_common(15)
cg = ['# Call graph', '', DISC.format(up='../../'),
      '- `callgraph_all.jsonl`: **%d call edges**, one `[caller, call site, callee owner, kind, target]` per line. `target` is the address the call goes to; `callee owner` is the entry of the function whose chunk holds that address (they differ in %d edges, for example a call into a tail chunk). Kind `d` (%d): the instruction\'s encoded operand is the target. Kind `r` (%d): IDA resolved the target another way (thunk or indirect); treat `r` edges as IDA-inferred. Kind `u` (%d): the call sits in code IDA placed in no function, reached from `caller` (see `unowned_blocks.jsonl`); that is reachability, not ownership.' % (len(edges), sum(1 for e in edges if len(e) > 4 and hx(e[2]) != hx(e[4]) & ~1), kc['d'], kc['r'], kc['u']),
      '- `unowned_blocks.jsonl`: per function, the code blocks reached from it that belong to no IDA function (ranges, byte hashes; instructions for core functions), and gaps where the target is not disassembled code.',
      '- `callgraph_core_edges.json`: the %d edges among the %d core functions.' % (len(core_edges), len(core)), '',
      'Limits: calls through virtual tables and function pointers are **not** edges. The game dispatches scene, player, dinosaur and UI handlers that way (see [classes](../classes/CLASSES.md)), so a function with no callers here can still run. A missing edge is not proof of absence.', '', 'Most-called functions (edges):', '']
for t, n in most: cg.append('- `0x%X` %s: %d' % (t, short(byea[t]) if t in byea else '?', n))
w('graphs/CALL_GRAPH.md', '\n'.join(cg) + '\n')

# ---- classes ----
shutil.copy(os.path.join(EXP, 'classes.jsonl'), os.path.join(OUT, 'classes', 'classes.jsonl')) if os.path.isdir(os.path.join(OUT, 'classes')) else (os.makedirs(os.path.join(OUT, 'classes')), shutil.copy(os.path.join(EXP, 'classes.jsonl'), os.path.join(OUT, 'classes', 'classes.jsonl')))
cl = ['# C++ classes and virtual tables', '', DISC.format(up='../../'), '%d virtual tables (`__ZTV` symbols). Slots follow the Itanium C++ ABI used by the iOS toolchain: after the offset-to-top and RTTI words, one function pointer per slot (Thumb pointers have the low bit set; the target column shows the code address). **File word** is the 32-bit value in the executable file; where the loader writes the slot through a relocation the file holds 0 and the **resolved word** is the value IDA shows after binding (%d of %d slots). The two are kept apart: a resolved word is not a byte of the file.' % (len(classes), sum(1 for c in classes for s in c['slots'] if s.get('relocation')), sum(len(c['slots']) for c in classes)) + " Headings give the class name; `classes.jsonl` keeps the demangler's raw text, which wraps it as `` `vtable for'X `` (the backquote and apostrophe are the demangler's quoting).", '']
for c in sorted(classes, key=lambda x: x['class']):
    if not c['slots'] or 'DinoCap' not in c['class']: continue
    # c['class'] is the demangler's text "`vtable for'DinoCap::X" (its own quoting, not Markdown); the heading shows the class name only
    cname = re.sub(r"^`vtable for'", '', c['class'])
    cl += ['## %s' % cname, '', 'Virtual table at `%s`.' % c['vtable_ea'], '', '| Slot | Offset | File word | Relocation | Resolved word | Target |', '|---:|---|---|---|---|---|']
    for s in c['slots']:
        rl = ('loader binds `%s`' % s['relocation']['target_name']) if s.get('relocation') and s['relocation'].get('target_name') else ('loader relocation' if s.get('relocation') else '-')
        cl.append('| %d | `%s` | `%s` | %s | `%s` | %s |' % (s['slot'], s['offset'], s.get('file_word'), rl, s.get('resolved_word', s.get('raw')), flink(hx(s['target']), 'classes/CLASSES.md')))
    cl.append('')
w('classes/CLASSES.md', '\n'.join(cl) + '\n')

# ---- data: strings and globals referenced by core functions ----
os.makedirs(os.path.join(OUT, 'data'), exist_ok=True)
shutil.copy(os.path.join(EXP, 'strings.jsonl'), os.path.join(OUT, 'data', 'strings.jsonl'))
sref = collections.defaultdict(set); gref = collections.defaultdict(set)
for ea, d in refs.items():
    for a, s in d.get('strings', {}).items(): sref[(a, s)].add(ea)
    for a, g in d.get('globals', {}).items(): gref[(a, g)].add(ea)
ns = ['# Notable strings (referenced by core functions)', '', DISC.format(up='../../'), '| Address | String | Referenced by |', '|---|---|---|']
for (a, s), eas in sorted(sref.items(), key=lambda x: hx(x[0][0])):
    ns.append('| `%s` | `%s` | %s |' % (a, s.replace('|', '\\|').replace('`', "'")[:100], ', '.join(flink(e, 'data/NOTABLE_STRINGS.md') for e in sorted(eas)[:6])))
w('data/NOTABLE_STRINGS.md', '\n'.join(ns) + '\n')
gl = ['# Named globals (referenced by core functions)', '', DISC.format(up='../../'), '| Address | Name | Referenced by |', '|---|---|---|']
glist = []
for (a, g), eas in sorted(gref.items(), key=lambda x: hx(x[0][0])):
    gl.append('| `%s` | `%s` | %s |' % (a, g.replace('|', '\\|'), ', '.join(flink(e, 'data/GLOBALS.md') for e in sorted(eas)[:6])))
    glist.append({'ea': a, 'name': g, 'referenced_by': ['0x%X' % e for e in sorted(eas)]})
w('data/GLOBALS.md', '\n'.join(gl) + '\n')
with open(os.path.join(OUT, 'data', 'globals.jsonl'), 'w', encoding='utf-8') as fo:
    for g in glist: fo.write(json.dumps(g) + '\n')

# ---- source to Unity map ----
sm = ['# Source to Unity map', '', DISC.format(up='../../'),
      'Every place in the port\'s code (`Assets/Scripts`) that cites a native function, by address or by `Class::Method` name. **Mechanical: a citation is a reference, not proof that the code implements the whole function.** Many-to-many; a core function with no row is **unmapped**, not absent. Links open the file at the cited line.', '',
      '| Native | Function | Unity location | How cited |', '|---|---|---|---|']
nmap = 0
for ea in sorted(unity_refs):
    for p, ln, a in unity_refs[ea]:
        sm.append('| `%s` | %s | [`%s:%d`](../../../%s#L%d) | %s |' % (byea[ea]['ea'], flink(ea, 'maps/SOURCE_TO_UNITY_MAP.md'), p, ln, p, ln, ('address `0x%X`' % a) if a else 'name'))
        nmap += 1
mapped_core = sum(1 for e in core if unity_refs.get(e))
sm.insert(4, 'Totals: %d citations; %d of %d core functions are cited at least once (%d unmapped).\n' % (nmap, mapped_core, len(core), len(core) - mapped_core))
w('maps/SOURCE_TO_UNITY_MAP.md', '\n'.join(sm) + '\n')

# ---- asset identity map: every file under Assets/Resources/DC2 ----
res = os.path.join(UNITY, 'Assets', 'Resources', 'DC2')
man = {}
mp = os.path.join(UNITY, 'extract_manifest.json')
if os.path.exists(mp):
    for e in json.load(open(mp, encoding='utf-8')): man[e['dst']] = e
# Codex R3-05: the kind comes from byte identity with a member of the original DinoCapData.dat (DC2_DAT: a verified analysis copy, read only),
# never from the file extension. Files with no identical member are derived; their generator is named where it is known.
import zipfile
datidx = collections.defaultdict(list)
DAT = os.environ.get('DC2_DAT')
if not DAT or not os.path.exists(DAT): raise SystemExit('DC2_DAT (DinoCapData.dat from a verified analysis copy) is required for the asset identity map')
with zipfile.ZipFile(DAT) as z:
    for i in z.infolist():
        if not i.is_dir(): datidx[hashlib.sha256(z.read(i)).hexdigest()].append(i.filename)
GENERATORS = [('Font/native_font_', 'Dev/Tools/bake_fonts.py: FreeType bake of `Data/Font/HARLOWSI.TTF` / `Data/Font/Adventure.ttf` as the native font loader does'),
              ('Font/native_fonts.json', 'Dev/Tools/bake_fonts.py (glyph metrics of the baked fonts)')]
am = ['# Asset identity map', '', DISC.format(up='../../'),
      'Every file of the converted game data in `Assets/Resources/DC2`. **Original** = byte-identical to the member of the IPA\'s `DinoCapData.dat` shown (checked by SHA-256 against every member; `.conf`/`.xml` gain a `.txt` suffix so Unity reads them as text). **Derived** = no byte-identical member exists: a file the project generated from original data or recovered code; the generator is named where it is recorded, otherwise it is one of the research tools. Hashes are SHA-256 of the file in this repository.', '',
      '| File | Kind | Source in the original / generator | Bytes | SHA-256 |', '|---|---|---|---:|---|']
arec = []
for dp, dn, fs in os.walk(res):
    for fn in sorted(fs):
        if fn.endswith('.meta'): continue
        p = os.path.join(dp, fn); r = os.path.relpath(p, res).replace(os.sep, '/')
        b = open(p, 'rb').read(); h = hashlib.sha256(b).hexdigest()
        members = sorted(datidx.get(h, []))
        if members: kind, source, gen = 'original', members[0] if len(members) == 1 else ' = '.join(members), None
        else: kind, source, gen = 'derived', None, next((g for pre, g in GENERATORS if r.startswith(pre)), None)
        arec.append({'path': 'Assets/Resources/DC2/' + r, 'kind': kind, 'source': source, 'generator': gen, 'bytes': len(b), 'sha256': h})
print('assets: original %d, derived %d (generator named %d)' % (sum(a['kind'] == 'original' for a in arec), sum(a['kind'] == 'derived' for a in arec), sum(bool(a['generator']) for a in arec)))
for a in sorted(arec, key=lambda x: x['path']):
    am.append('| `%s` | %s | %s | %d | `%s` |' % (a['path'].replace('Assets/Resources/DC2/', ''), a['kind'], ('`%s`' % a['source']) if a['source'] else (a['generator'] or '-'), a['bytes'], a['sha256'][:16] + '...'))
w('maps/ASSET_IDENTITY_MAP.md', '\n'.join(am) + '\n')
with open(os.path.join(OUT, 'maps', 'asset_identity_map.jsonl'), 'w', encoding='utf-8') as fo:
    for a in sorted(arec, key=lambda x: x['path']): fo.write(json.dumps(a) + '\n')

# ---- registry copy, claims, binary record ----
os.makedirs(os.path.join(OUT, 'registry'), exist_ok=True)
for fn in ('source_contract_registry.json', 'pilot_assumptions.json', 'disarmed_heuristics.json'):
    shutil.copy(os.path.join(DEV, 'SourceTruth', fn), os.path.join(OUT, 'registry', fn))
with open(os.path.join(OUT, 'claims.jsonl'), 'w', encoding='utf-8') as fo:
    for r in rules:
        fo.write(json.dumps({'id': r['id'], 'status': r.get('status'), 'provenance': r.get('provenance'), 'statement': r.get('proposition'), 'evidence': r.get('evidence'),
                             'functions': sorted({'0x%X' % hx(containing(int(m, 16))['ea']) for m in re.findall(r'0x([0-9a-fA-F]{4,6})\b', json.dumps(r)) if containing(int(m, 16))}),
                             'supersedes': r.get('supersedes'), 'superseded_by': r.get('superseded_by')}) + '\n')
os.makedirs(os.path.join(OUT, 'binary'), exist_ok=True)
shutil.copy(BINREC, os.path.join(OUT, 'binary', 'binaries.jsonl'))
json.dump(binary, open(os.path.join(OUT, 'binary', 'segments.json'), 'w'), indent=1)
stats = {'functions': len(funcs), 'core': len(core), 'core_with_rules': sum(1 for e in core if rule_cites.get(e)), 'core_mapped': mapped_core,
         'deep': len(deep), 'edges': len(edges), 'core_edges': len(core_edges), 'vtables': len(classes), 'unity_citations': nmap, 'assets': len(arec),
         'ledger': collections.Counter(ledger[e]['status'] for e in core if e in ledger)}
json.dump(stats, open(os.path.join(OUT, 'binary', 'coverage_stats.json'), 'w'), indent=1)
print(json.dumps(stats, indent=1))

# ---- no calendar dates in the published tree (user request: dates are identifying information) ----
import sys
sys.path.insert(0, os.path.dirname(os.path.abspath(__file__)))
import scrub_dates
print('dates scrubbed in %d files' % len(scrub_dates.scrub_tree(OUT)))

# ---- published tool copies: byte-identical to the working tools, copied after the scrub so code is never rewritten (Codex R3-01) ----
os.makedirs(os.path.join(OUT, 'tools'), exist_ok=True)
for src in ('Tools/docs_export/export_ida.py', 'Tools/docs_export/generate_docs.py', 'Tools/docs_export/scrub_dates.py', 'Tools/ledger_report.py', 'Tools/check_float_claims.py'):
    shutil.copy(os.path.join(DEV, src), os.path.join(OUT, 'tools', os.path.basename(src)))
