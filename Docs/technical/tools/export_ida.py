"""DC2 documentation export (read-only). Produces the technical record that Docs/technical is generated from.

Needs IDA Pro (idalib) and the analysis database of the shipped Dino Cap 2 1.3 executable. It opens the database with auto-analysis off
(open_database(path, False)) and closes it without saving (close_database(False)). That is a safeguard, not an enforced read-only mode.

Environment:
  DC2_DB      path of the IDA database (the project keeps exactly one central analysis database; this script never writes it)
  DC2_BIN     path of the analysed executable (a verified copy): symbol table and file words are read from it directly, never from IDA
  DC2_OUT     output folder (write a new folder per export revision; never overwrite an earlier export)
  DC2_LEDGER  path of Dev/Evidence/function_ledger.jsonl (defines the 418 core functions: verdict UNSET / GAMEPLAY / UI_FLOW)
Outputs (DC2_OUT):
  functions.jsonl        every function: EA, RVA, file offset, segment, size of the entry chunk, every chunk (start, end, bytes SHA-256), ISA,
                         raw / demangled name, name provenance, direct callers, entry-chunk bytes SHA-256, exits (branches or fall-through
                         leaving the function's chunks, with the owner of the target or "unowned")
  callgraph_all.jsonl    [caller_ea, site_ea, callee_owner_ea, kind, target_ea]: kind d = the instruction's encoded operand is the target,
                         r = IDA resolved it another way (thunk / indirect), u = the call sits in unowned code reached from caller_ea
                         (see unowned_blocks.jsonl); callee_owner_ea is the entry of the function that contains target_ea
  core_disasm.jsonl      every instruction of every chunk of every core function: [ea, bytes_hex, text]
  unowned_blocks.jsonl   code IDA disassembled but placed in no function, reached by a branch or fall-through from a function; per block:
                         start, end, bytes SHA-256, reached_from (function EA and site), instructions (core functions only). Reached-from is
                         reachability, not ownership. Targets that are not disassembled code are listed as gaps.
  classes.jsonl          C++ virtual tables (__ZTV symbols): per slot the file word (bytes of the executable), whether the loader writes the
                         word through a relocation (the symbol it binds), the resolved word IDA shows, and the target EA / ISA / name
                         (target ISA from the target's T segment register, as for functions; not from the word's low bit)
  strings.jsonl          IDA string list: ea, text
  core_refs.jsonl        per core function: referenced strings and named data (globals)
  binary.json            imagebase, segment table (name, start, end, file offset), input file SHA-256 as recorded by IDA
"""
import os, sys, json, hashlib, struct

import idapro  # the idalib package of the IDA install that owns the database
idapro.enable_console_messages(False)
DB, BIN, OUT, LEDGER = os.environ['DC2_DB'], os.environ['DC2_BIN'], os.environ['DC2_OUT'], os.environ['DC2_LEDGER']
os.makedirs(OUT, exist_ok=True)


def macho_symbols(path):
    """nlist names of the ARM slice (thin or fat Mach-O, 32-bit). Returns {name} for defined and undefined symbols, plus the raw image bytes."""
    data = open(path, 'rb').read()
    base = 0
    if struct.unpack('>I', data[:4])[0] == 0xCAFEBABE:             # fat: take the first ARM (cputype 12) slice
        n = struct.unpack('>I', data[4:8])[0]
        for i in range(n):
            cpu, sub, off, size, al = struct.unpack('>5I', data[8 + 20 * i: 28 + 20 * i])
            if cpu == 12: base = off; break
    magic = struct.unpack('<I', data[base:base + 4])[0]
    if magic != 0xFEEDFACE: raise SystemExit('not a 32-bit Mach-O: 0x%X' % magic)
    ncmds = struct.unpack('<I', data[base + 16: base + 20])[0]
    p = base + 28; names = set()
    for _ in range(ncmds):
        cmd, size = struct.unpack('<II', data[p:p + 8])
        if cmd == 2:                                                   # LC_SYMTAB
            symoff, nsyms, stroff, strsize = struct.unpack('<4I', data[p + 8:p + 24])
            for i in range(nsyms):
                strx = struct.unpack('<I', data[base + symoff + 12 * i: base + symoff + 12 * i + 4])[0]
                if strx:
                    e = data.index(b'\0', base + stroff + strx)
                    names.add(data[base + stroff + strx: e].decode('latin-1'))
        p += size
    return names, data, base


if idapro.open_database(DB, False) != 0:
    sys.exit('open_database failed')
import ida_funcs, ida_ida, idautils, ida_idp, ida_segregs, ida_bytes, ida_name, ida_xref, ida_segment, ida_loader, ida_nalt, idc, ida_lines, ida_fixup

try:
    IMAGEBASE = ida_nalt.get_imagebase()
    TREG = ida_idp.str2reg('T')
    SYMS, BINDATA, SLICE = macho_symbols(BIN)

    def isa(ea):
        try:
            return 'THUMB' if ida_segregs.get_sreg(ea, TREG) == 1 else 'ARM'
        except Exception:
            return 'UNKNOWN'

    def segname(ea):
        s = ida_segment.getseg(ea)
        return ida_segment.get_segm_name(s) if s else None

    def fileoff(ea):
        o = ida_loader.get_fileregion_offset(ea)
        return None if o is None or o < 0 else o

    def demangle(n):
        d = ida_name.demangle_name(n, 0) if n else None
        return d or n

    def name_provenance(ea, name):
        if name in SYMS: return 'symbol in the binary (nlist)'
        if ida_bytes.has_dummy_name(ida_bytes.get_flags(ea)): return 'IDA automatic name'
        stem = name.rsplit('_', 1)[0]
        if stem != name and stem in SYMS and name.rsplit('_', 1)[1].isdigit(): return 'IDA duplicate-name suffix of a binary symbol'
        return 'name not in the symbol table (IDA, loader or Objective-C metadata)'

    def chunks(f):
        return sorted((s, e) for s, e in idautils.Chunks(f.start_ea))   # (start, end) per chunk: the entry chunk and every tail

    def in_chunks(ea, cs):
        return any(s <= ea < e for s, e in cs)

    segs = []
    for s in idautils.Segments():
        seg = ida_segment.getseg(s)
        segs.append({'name': ida_segment.get_segm_name(seg), 'start': '0x%X' % seg.start_ea, 'end': '0x%X' % seg.end_ea,
                     'file_offset': ('0x%X' % fileoff(seg.start_ea)) if fileoff(seg.start_ea) is not None else None})
    sha = ida_nalt.retrieve_input_file_sha256()
    json.dump({'imagebase': '0x%X' % IMAGEBASE, 'segments': segs,
               'input_sha256': sha.hex() if isinstance(sha, (bytes, bytearray)) else sha,
               'input_file': os.path.basename(ida_nalt.get_input_file_path() or ''),
               'bin_sha256': hashlib.sha256(BINDATA).hexdigest(),
               'note': 'EA = IDA static address; RVA = EA - imagebase; file offset from IDA\'s loader map; Thumb code shown without the low bit'},
              open(os.path.join(OUT, 'binary.json'), 'w'), indent=1)

    ledger = [json.loads(l) for l in open(LEDGER, encoding='utf-8')]
    core = {int(r['addr'], 16) for r in ledger if r['verdict'] in ('UNSET', 'GAMEPLAY', 'UI_FLOW')}

    CODE_REFS = (ida_xref.fl_CN, ida_xref.fl_CF, ida_xref.fl_JN, ida_xref.fl_JF)
    JUMPS = (ida_xref.fl_JN, ida_xref.fl_JF)

    def insn_row(head):
        n = ida_bytes.get_item_size(head)
        return ['0x%X' % head, (ida_bytes.get_bytes(head, n) or b'').hex(), ida_lines.tag_remove(idc.generate_disasm_line(head, 0) or '')]

    def call_rows(owner_ea, head, kind_override=None):
        rows = []
        for x in idautils.XrefsFrom(head, 0):
            if x.type in (ida_xref.fl_CN, ida_xref.fl_CF):
                callee = ida_funcs.get_func(x.to)
                if not callee:
                    continue
                enc = idc.get_operand_value(head, 0)
                kind = kind_override or ('d' if (enc & ~1) == x.to or enc == x.to else 'r')
                rows.append(['0x%X' % owner_ea, '0x%X' % head, '0x%X' % callee.start_ea, kind, '0x%X' % x.to])
        return rows

    def walk_unowned(starts, limit=4000):
        """Code in no function, reached from the given (site, target) pairs. Follows fall-through and jumps; stops at owned code."""
        blocks, gaps, seen, work, total = [], [], set(), list(starts), 0
        while work and total < limit:
            site, s = work.pop()
            if s in seen or ida_funcs.get_func(s): continue
            if not ida_bytes.is_code(ida_bytes.get_flags(s)):
                gaps.append({'site': '0x%X' % site, 'target': '0x%X' % s, 'why': 'target bytes are not disassembled code in the database'}); continue
            ea, ins, heads = s, [], []
            while total < limit and ea not in seen and not ida_funcs.get_func(ea) and ida_bytes.is_code(ida_bytes.get_flags(ea)):
                seen.add(ea); heads.append(ea); ins.append(insn_row(ea)); total += 1
                flow = False
                for x in idautils.XrefsFrom(ea, 0):
                    if x.type == ida_xref.fl_F: flow = True
                    elif x.type in JUMPS and not ida_funcs.get_func(x.to): work.append((ea, x.to))
                if not flow: break
                ea += ida_bytes.get_item_size(ea)
            if heads:
                end = heads[-1] + ida_bytes.get_item_size(heads[-1])
                blocks.append({'start': '0x%X' % s, 'end': '0x%X' % end, 'reached_from_site': '0x%X' % site,
                               'bytes_sha256': hashlib.sha256(ida_bytes.get_bytes(s, end - s) or b'').hexdigest(), 'heads': heads, 'instructions': ins})
        return blocks, gaps

    nf = 0
    unowned_by_func = {}
    with open(os.path.join(OUT, 'functions.jsonl'), 'w', encoding='utf-8') as fo, \
         open(os.path.join(OUT, 'callgraph_all.jsonl'), 'w', encoding='utf-8') as fg, \
         open(os.path.join(OUT, 'unowned_blocks.jsonl'), 'w', encoding='utf-8') as fu:
        for fea in idautils.Functions():
            f = ida_funcs.get_func(fea)
            if not f:
                continue
            cs = chunks(f)
            size = f.end_ea - f.start_ea
            raw = ida_bytes.get_bytes(f.start_ea, size) or b''
            callers = sum(1 for x in idautils.XrefsTo(fea, 0) if x.type in CODE_REFS)
            name = ida_funcs.get_func_name(fea) or ''
            exits, starts = [], []
            for cs_, ce_ in cs:
                for head in idautils.Heads(cs_, ce_):
                    if not ida_bytes.is_code(ida_bytes.get_flags(head)):
                        continue
                    for row in call_rows(fea, head): fg.write(json.dumps(row) + '\n')
                    for x in idautils.XrefsFrom(head, 0):
                        if x.type in JUMPS + (ida_xref.fl_F,) and not in_chunks(x.to, cs):
                            o = ida_funcs.get_func(x.to)
                            exits.append({'site': '0x%X' % head, 'target': '0x%X' % x.to, 'flow': 'fall-through' if x.type == ida_xref.fl_F else 'branch',
                                          'target_owner': ('0x%X' % o.start_ea) if o else 'unowned'})
                            if not o: starts.append((head, x.to))
            blocks, gaps = walk_unowned(starts) if starts else ([], [])
            for b in blocks:
                for h in b['heads']:
                    for row in call_rows(fea, h, 'u'): fg.write(json.dumps(row) + '\n')
            if blocks or gaps:
                fu.write(json.dumps({'reached_from': '0x%X' % fea, 'blocks': [{k: v for k, v in b.items() if k != 'heads' and (fea in core or k != 'instructions')} for b in blocks],
                                     'gaps': gaps}) + '\n')
            fo.write(json.dumps({'id': 'FUN-%06X' % fea, 'ea': '0x%X' % fea, 'rva': '0x%X' % (fea - IMAGEBASE),
                                 'file_offset': ('0x%X' % fileoff(fea)) if fileoff(fea) is not None else None,
                                 'segment': segname(fea), 'size': size, 'isa': isa(fea), 'name': name, 'demangled': demangle(name),
                                 'chunks': [{'start': '0x%X' % s, 'end': '0x%X' % e, 'bytes_sha256': hashlib.sha256(ida_bytes.get_bytes(s, e - s) or b'').hexdigest()} for s, e in cs],
                                 'exits': exits,
                                 'name_provenance': name_provenance(fea, name),
                                 'direct_callers': callers, 'bytes_sha256': hashlib.sha256(raw).hexdigest(), 'core': fea in core}) + '\n')
            nf += 1

    with open(os.path.join(OUT, 'core_disasm.jsonl'), 'w', encoding='utf-8') as fd, \
         open(os.path.join(OUT, 'core_refs.jsonl'), 'w', encoding='utf-8') as fr:
        for fea in sorted(core):
            f = ida_funcs.get_func(fea)
            if not f:
                fd.write(json.dumps({'ea': '0x%X' % fea, 'missing': True}) + '\n'); continue
            ins, strs, globs = [], {}, {}
            for cs_, ce_ in chunks(f):
                for head in idautils.Heads(cs_, ce_):
                    if not ida_bytes.is_code(ida_bytes.get_flags(head)):
                        continue
                    ins.append(insn_row(head))
                    for x in idautils.DataRefsFrom(head):
                        for y in [x] + list(idautils.DataRefsFrom(x)):
                            st = idc.get_str_type(y)
                            if st is not None and st >= 0:
                                s = idc.get_strlit_contents(y, -1, st)
                                if s:
                                    strs['0x%X' % y] = s.decode('utf-8', 'replace')
                            nm = ida_name.get_name(y)
                            if nm and not nm.startswith(('off_', 'unk_', 'loc_', 'a', 'dword_', 'byte_', 'word_', 'qword_', 'flt_', 'dbl_', 'stru_', 'asc_')) and segname(y) not in ('__text',):
                                globs['0x%X' % y] = demangle(nm)
            fd.write(json.dumps({'ea': '0x%X' % fea, 'instructions': ins}) + '\n')
            fr.write(json.dumps({'ea': '0x%X' % fea, 'strings': strs, 'globals': globs}) + '\n')

    fx = ida_fixup.fixup_data_t()
    with open(os.path.join(OUT, 'classes.jsonl'), 'w', encoding='utf-8') as fc:
        for ea, nm in idautils.Names():
            if not nm.startswith('__ZTV'):
                continue
            slots, p, i = [], ea + 8, 0          # Itanium ABI: offset-to-top, RTTI, then slots
            while i < 400:
                v = ida_bytes.get_dword(p)
                t = v & ~1
                fn = ida_funcs.get_func(t)
                if not fn or fn.start_ea != t:
                    if v == 0 and i == 0:
                        p += 4; i += 1; continue
                    break
                fo_ = fileoff(p)
                file_word = struct.unpack('<I', BINDATA[SLICE + fo_: SLICE + fo_ + 4])[0] if fo_ is not None else None
                reloc = None
                if ida_fixup.get_fixup(fx, p):
                    reloc = {'type': fx.get_type(), 'target_name': ida_name.get_name(fx.get_base() + fx.off) or None}
                slots.append({'slot': i, 'offset': '0x%X' % (i * 4),
                              'file_word': ('0x%X' % file_word) if file_word is not None else None,
                              'relocation': reloc,
                              'resolved_word': '0x%X' % v,
                              # target ISA from the target's own segment register (as for functions), never from the word's low bit:
                              # a loader-resolved word is an address, not a tagged runtime pointer (Codex R3-07 confirmation)
                              'target': '0x%X' % t, 'target_isa': isa(t), 'name': demangle(ida_funcs.get_func_name(t))})
                p += 4; i += 1
            fc.write(json.dumps({'vtable_ea': '0x%X' % ea, 'symbol': nm, 'class': demangle(nm), 'slots': slots}) + '\n')

    with open(os.path.join(OUT, 'strings.jsonl'), 'w', encoding='utf-8') as fs:
        for s in idautils.Strings():
            try:
                fs.write(json.dumps({'ea': '0x%X' % s.ea, 'text': str(s)}) + '\n')
            except Exception:
                pass
    print('functions', nf, 'core', len(core))
finally:
    idapro.close_database(False)
