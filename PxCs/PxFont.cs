using PxCs.Data;
using PxCs.Marshaller;
using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace PxCs
{
    public static class PxFont
    {
        private const string DLLNAME = PxPhoenix.DLLNAME;

        [DllImport(DLLNAME)] public static extern IntPtr pxFntLoad(IntPtr buffer);
        [DllImport(DLLNAME)] public static extern IntPtr pxFntLoadFromVdf(IntPtr vdf, string name);
        [DllImport(DLLNAME)] public static extern void pxFntDestroy(IntPtr fnt);

        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(PxHeapStringMarshaller))]
        [DllImport(DLLNAME)] public static extern string pxFntGetName(IntPtr fnt);
        [DllImport(DLLNAME)] public static extern uint pxFntGetHeight(IntPtr fnt);
        [DllImport(DLLNAME)] public static extern uint pxFntGetGlyphCount(IntPtr fnt);
        [DllImport(DLLNAME)] public static extern void pxFntGetGlyph(IntPtr fnt, uint i, out uint width, out Vector2 upper, out Vector2 lower);


        public static PxFontData? LoadFont(IntPtr vdfPtr, string fontname)
        {
            var fontPtr = pxFntLoadFromVdf(vdfPtr, "FONT_DEFAULT.FNT");

            if (fontPtr == IntPtr.Zero)
                return null;

            var font = new PxFontData()
            {
                name = pxFntGetName(fontPtr),
                height = pxFntGetHeight(fontPtr),
            };

            var glyphCount = pxFntGetGlyphCount(fontPtr);
            var glyphs = new PxFontGlyphData[glyphCount];

            for (var i = 0u; i < glyphCount; i++)
            {
                pxFntGetGlyph(fontPtr, i, out uint width, out Vector2 upper, out Vector2 lower);

                glyphs[i] = new PxFontGlyphData()
                {
                    width = width,
                    upper = upper,
                    lower = lower
                };
            }

            font.glyphs = glyphs;

            pxFntDestroy(fontPtr);

            return font;
        }
    }
}
