using System;
using UnityEngine;

namespace Dc2Pilot
{
    /// <summary>
    /// Helpers that mirror the native DinoCap rect/scale conventions (DC2 v1.3 iPad path).
    /// Evidence: Dev/SourceTruth/source_contract_registry.json (dc2.ui.rect_tables, dc2.layout.ipad_offsets).
    /// </summary>
    public static class N
    {
        // Native helper calls return binary32 between operations. Mono may otherwise retain
        // extra precision across an expression and truncate 49.999999... to49 instead of50.
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public static float F32(double value) { return BitConverter.Int32BitsToSingle(BitConverter.SingleToInt32Bits((float)value)); }   // r119x perf: same binary32 store through the bits, without the byte[4] GetBytes allocated per call (44 % of battle garbage)
        public static float Add32(float a,float b) { return F32((double)a+(double)b); }
        public static float Mul32(float a,float b) { return F32((double)a*(double)b); }
        public static float Div32(float a,float b) { return F32((double)a/(double)b); }
        public static int HpWithJigsaw(int hp,int bonus)
        { return (int)Add32(F32(hp),Mul32(F32(unchecked(bonus*hp)),F(1008981770))); }
        /// <summary>Native floats are stored as raw IEEE bits in the decompile; keep them verbatim.</summary>
        public static float F(int bits) { return BitConverter.Int32BitsToSingle(bits); }

        /// <summary>g_iphone == 0 on iPad: RectScale factor.</summary>
        public const float S = 2f;

        // NATIVE initialized floats at 0x23b914/0x23b918. Preserve the shipped asymmetric layout.
        // Evidence: native_layout_trace.r4.json and native_offset_*r4*.json; lifetime claim is bounded.
        public const float WOffset = 64f;
        public const float HOffset = 64f;

        public const int ScreenW = 1024;
        public const int ScreenH = 768;

        /// <summary>DinoCap::RectScale(rect, s): multiplies x,y,w,h.</summary>
        public static Rect RectScale(float x, float y, float w, float h, float s = S)
        {
            return new Rect(x * s, y * s, w * s, h * s);
        }

        /// <summary>DinoCap::RectOffset on iPad: adds half the width/height offsets to x/y.</summary>
        public static Rect RectOffset(Rect r)
        {
            return new Rect(r.x + 0.5f * WOffset, r.y + 0.5f * HOffset, r.width, r.height);
        }

        /// <summary>RectScale then RectOffset, the common pattern for map/option rects.</summary>
        public static Rect Layout(float x, float y, float w, float h)
        {
            return RectOffset(RectScale(x, y, w, h));
        }
    }

    /// <summary>One animation key of UIMoveCtrlData: centre position, size and colour (native Rect+Color32).</summary>
    public struct MoveData
    {
        public float cx, cy, w, h;
        public Color32 col;
        public MoveData(float cx, float cy, float w, float h, Color32 col)
        { this.cx = cx; this.cy = cy; this.w = w; this.h = h; this.col = col; }

        public static MoveData FromRect(Rect r, Color32 col)
        { return new MoveData(r.x + r.width * 0.5f, r.y + r.height * 0.5f, r.width, r.height, col); }

        public Rect ToRect() { return new Rect(cx - w * 0.5f, cy - h * 0.5f, w, h); }
    }
}
