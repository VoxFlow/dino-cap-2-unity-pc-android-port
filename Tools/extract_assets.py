#!/usr/bin/env python3
"""Extract the original DC2 resources the pilot needs into Unity/Assets/Resources/DC2.

Reads a verified COPY of the shipped files (never the protected originals):
  --dat  DinoCapData.dat (ZIP)           --app  folder holding Default-Landscape.png
Writes (byte-identical, except .conf -> .conf.txt so Unity treats it as a TextAsset):
  Resources/DC2/<same relative path as inside Data/>
Run:  python -B Tools/extract_assets.py --dat <DinoCapData.dat> --app <app dir> --out Unity/Assets/Resources/DC2
"""
import argparse, hashlib, json, os, shutil, sys, zipfile

WANT_GLOBS = ("Data/Image/an2/", "Data/Image/a2b/k")      # every an2 directory a2b/an2 (hero, dinosaur, effect animations)
WANT_PREFIX = (
    "Data/Image/ip/uimenu_h.png", "Data/Image/missionui.png", "Data/Image/missionui.conf", "Data/Image/ip/k1hui_h.png", "Data/Image/ip/k3hui_h.png", "Data/Image/ip/k10body_h.png", "Data/Image/ip/k11body_h.png", "Data/Image/ip/k20body_h.png", "Data/Image/ip/k21body_h.png", "Data/Image/ip/k30body_h.png", "Data/Image/ip/k31body_h.png", "Data/Image/ip/k32body_h.png", "Data/Image/ip/k101body_h.png", "Data/Image/ip/k111body_h.png", "Data/Image/ip/k211body_h.png", "Data/Image/ip/k301body_h.png", "Data/Image/ip/k311body_h.png", "Data/Image/ip/k321body_h.png", "Data/Image/ip/k501body_h.png", "Data/Image/ip/k51body_h.png", "Data/Image/ip/k52body_h.png", "Data/Image/ip/k511body_h.png", "Data/Image/ip/k521body_h.png", "Data/Image/ip/k50body_h.png", "Data/Image/ip/loadimg_h.png", "Data/Image/ip/uicontrol_h.png",
    "Data/Image/ip/uimap_h.png", "Data/Image/ip/uimap_sub_h.png", "Data/Image/ip/uimap_sub1_h.png",
    "Data/Image/ip/uimap_h.conf", "Data/Image/ip/uimap_sub_h.conf", "Data/Image/ip/uimap_sub1_h.conf",
    "Data/Image/ip/uiarmory_h.png", "Data/Image/ip/uiarmory_h.conf",
    "Data/Image/ip/uiiap_h.png", "Data/Image/ip/weapon1_h.png", "Data/Image/ip/control_h.png", "Data/Image/house.png", "Data/Image/dimian_m.png", "Data/Image/beijing_1.png", "Data/Image/beijing_2.png", "Data/Image/ip/jigsaw_h.png", "Data/Image/ip/jigsaw_h.conf", "Data/Image/ip/jigsaw_h_1.png", "Data/Image/ip/jigsaw_h_1.conf", "Data/Image/ip/jigeff2x.png", "Data/Image/ip/jigeff2x.conf", "Data/Image/acb/jigsaw_1.animation.acb", "Data/Audio/A_jigsaw_touchend.wav", "Data/Audio/A_jigsaw_moveend.wav", "Data/Image/ip/uitavern.png", "Data/Image/ip/uitavern.conf", "Data/Image/ip/uitavern_1.png", "Data/Image/ip/uitavern_1.conf",
    "Data/Audio/Music_Bg_Menu.wav", "Data/Audio/Audio_button.wav", "Data/Audio/Audio_Button1.wav",
    "Data/Audio/Audio_button2.wav",
    "Data/Font/Adventure.ttf", "Data/Image/Conf/Player.xml", "Data/Image/Conf/iPad/weapon.prop", "Data/Image/Conf/iPad/dinosaur.prop", "Data/Image/Conf/iPad/lifedinosaur.prop", "Data/Image/Conf/boss.xml", "Data/Image/Conf/raineff.xml", "Data/Image/Conf/raineff2.xml", "Data/Image/Conf/rainhua.xml",
)


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--dat", required=True)
    ap.add_argument("--app", required=True)
    ap.add_argument("--out", required=True)
    a = ap.parse_args()
    z = zipfile.ZipFile(a.dat)
    manifest = []
    names = set(z.namelist())
    extra = sorted(n for n in names if any(n.startswith(g) for g in WANT_GLOBS) and n.endswith((".a2b", ".an2")))
    for n in list(WANT_PREFIX) + extra:
        if n not in names:
            print("MISSING", n); continue
        data = z.read(n)
        rel = n[len("Data/"):]
        if rel.endswith(".conf") or rel.endswith(".xml"):
            rel += ".txt"
        elif rel.endswith(".prop") or rel.endswith(".an2"):
            rel += ".txt"
        elif rel.endswith(".a2b"):
            rel += ".bytes"
        elif rel.endswith(".acb"):
            rel += ".bytes"
        dst = os.path.join(a.out, rel)
        os.makedirs(os.path.dirname(dst), exist_ok=True)
        with open(dst, "wb") as f:
            f.write(data)
        manifest.append({"src": n, "dst": rel, "bytes": len(data), "sha256": hashlib.sha256(data).hexdigest()})
    src = os.path.join(a.app, "Default-Landscape.png")
    dst = os.path.join(a.out, "Image", "Default-Landscape.png")
    os.makedirs(os.path.dirname(dst), exist_ok=True)
    shutil.copyfile(src, dst)
    manifest.append({"src": "app/Default-Landscape.png", "dst": "Image/Default-Landscape.png",
                     "bytes": os.path.getsize(dst), "sha256": hashlib.sha256(open(dst, 'rb').read()).hexdigest()})
    with open(os.path.join(a.out, "..", "..", "..", "extract_manifest.json"), "w") as f:
        json.dump(manifest, f, indent=1)
    print("extracted", len(manifest), "files")


if __name__ == "__main__":
    sys.exit(main())

