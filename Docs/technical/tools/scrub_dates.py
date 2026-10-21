"""Remove calendar dates from publishable text (user request: dates are identifying information).

Usage: python scrub_dates.py <file-or-folder> [...]   (rewrites text files in place; prints changed files)
Also imported by generate_docs.py so a regenerated Docs/technical stays date-free.
Only YYYY-MM-DD dates (and "YYYY-MM-DD – MM-DD" ranges) are removed, with the punctuation that would be left dangling.
Hex addresses, revision ids (r119z) and version numbers are untouched.
"""
import os, re, sys

TEXT_EXT = ('.md', '.json', '.jsonl', '.cs', '.py', '.ps1', '.txt', '.gitignore', '.html')
D = r'20\d\d-\d\d-\d\d(?:/\d\d)*(?:\s*(?:–|-|to)\s*(?:20\d\d-)?\d\d-\d\d)?'   # also "YYYY-MM-DD/DD" shorthand (Codex R3-08)
RULES = [
    (re.compile(r'\s*\(\s*' + D + r'\s*\)'), ''),              # "(YYYY-MM-DD)" with the space before it (no "B2 ." left)
    (re.compile(r'\(\s*' + D + r'\s*,\s*'), '('),              # "(YYYY-MM-DD, on the build machine)"
    (re.compile(r'\s*,\s*' + D + r'(?=\s*[)\]"])'), ''),       # "(items 2 and 10, YYYY-MM-DD)"
    (re.compile(r'\s+' + D + r'(?=\s*[:)\]",;])'), ''),        # "user YYYY-MM-DD:" / "request YYYY-MM-DD)"
    (re.compile(r'\s+' + D + r'\b'), ''),                      # "decision YYYY-MM-DD (..."
    (re.compile(D + r'\s*[:,]?\s*'), ''),                      # leading dates
]


DATE = re.compile(D)


def scrub_line(s):
    for rx, rep in RULES:
        s = rx.sub(rep, s)
    return s


def scrub_text(s):
    # line by line, and only lines that hold a date: code names such as "OnLoad()" elsewhere stay byte-identical
    return ''.join(scrub_line(l) if DATE.search(l) else l for l in s.splitlines(True))


def scrub_file(p):
    try:
        with open(p, encoding='utf-8', newline='') as f: s = f.read()   # keep CRLF / LF as found
    except (UnicodeDecodeError, OSError):
        return False
    t = scrub_text(s)
    if t != s:
        with open(p, 'w', encoding='utf-8', newline='') as f: f.write(t)
        return True
    return False


def scrub_tree(root):
    changed = []
    if os.path.isfile(root):
        return [root] if scrub_file(root) else []
    for dp, dn, fn in os.walk(root):
        for n in fn:
            if n.endswith(TEXT_EXT) and scrub_file(os.path.join(dp, n)): changed.append(os.path.join(dp, n))
    return changed


if __name__ == '__main__':
    for a in sys.argv[1:]:
        for c in scrub_tree(a): print(c)
