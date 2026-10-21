using UnityEditor;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    /// <summary>Keeps original DC2 pixels untouched: no compression, no mips, no NPOT resize.</summary>
    public class Dc2ImportSettings : AssetPostprocessor
    {
        public override uint GetVersion() { return 3; }   // r118z4: alphaIsTransparency off (forces a reimport of the DC2 textures)
        static readonly string[] Nearest = { "/Resources/DC2/Image/beijing_1.png", "/Resources/DC2/Image/beijing_2.png", "/Resources/DC2/Image/dimian_m.png", "/Resources/DC2/Image/ip/weapon1_h.png", "/Resources/DC2/Image/ip/huoyan2x.png" };   // huoyan2x: LoadResource stage 5 iPad branch 0x65bbc (still NEAREST)
        public static bool NearestFiltered(string path) { foreach (var n in Nearest) if (path.Replace('\\', '/').EndsWith(n)) return true; return false; }

        void OnPreprocessTexture()
        {
            if (!assetPath.Contains("/Resources/DC2/")) return;
            var ti = (TextureImporter)assetImporter;
            ti.textureType = TextureImporterType.Default;
            ti.npotScale = TextureImporterNPOTScale.None;
            ti.mipmapEnabled = false;
            ti.isReadable = false;
            // TextureManager::LoadFromPng 0x1781e0 uploads the decoded RGBA unchanged for power-of-two images (ExtendImageSize 0x177edc, the only path that
            // rewrites texels, runs only for NPOT sizes); the shipped atlases store black RGB under alpha 0, so bilinear sampling darkens sprite edges natively.
            // r118z4: alphaIsTransparency was true (Unity dilates colour into alpha-0 texels) with no native basis - port defect.
            ti.alphaIsTransparency = false;
            ti.textureCompression = TextureImporterCompression.Uncompressed;
            ti.maxTextureSize = 4096;
            // TextureManager::LoadFromPng/Jpg 0x1781e0 apply the filter selected by SetParamFilter 0x177aa0 (0 GL_NEAREST, 1 GL_LINEAR, default LINEAR) when the texture is
            // first created (CheckTexture keeps it). On iPad these four are created only inside LoadResource stages 2/3 right after SetParamFilter(0) (0x60594 / 0x60bee),
            // every other texture under LINEAR (r118z L8, Dev/Evidence/engine_render_r118z.md).
            ti.filterMode = NearestFiltered(assetPath) ? FilterMode.Point : FilterMode.Bilinear;
            // r118z4: LoadFromPng 0x1782f0/0x1782fa (and LoadFromRaw / ModifyFromRaw) set GL_TEXTURE_WRAP_S/T = GL_REPEAT (10497) on every texture; Clamp had no native basis.
            ti.wrapMode = TextureWrapMode.Repeat;
        }
    }
}
