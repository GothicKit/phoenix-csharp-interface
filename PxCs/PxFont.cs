using PxCs.Data;
using System;
using System.Numerics;
using System.Runtime.InteropServices;
using static PxCs.PxTexture;

namespace PxCs
{
    public static class PxFont
    {
        private const string DLLNAME = PxPhoenix.DLLNAME;

        [DllImport(DLLNAME)] public static extern IntPtr pxFntLoad(IntPtr buffer);
        [DllImport(DLLNAME)] public static extern IntPtr pxFntLoadFromVdf(IntPtr vdf, string name);
        [DllImport(DLLNAME)] public static extern void pxFntDestroy(IntPtr fnt);

        [DllImport(DLLNAME)] public static extern IntPtr pxFntGetName(IntPtr fnt);
        [DllImport(DLLNAME)] public static extern uint pxFntGetHeight(IntPtr fnt);
        [DllImport(DLLNAME)] public static extern uint pxFntGetGlyphCount(IntPtr fnt);
        [DllImport(DLLNAME)] public static extern void pxFntGetGlyph(IntPtr fnt, uint i, out uint width, out Vector2 upper, out Vector2 lower);


        public static PxFontData? LoadFont(IntPtr vdfPtr, string fontname, params Format[] supportedTextureFormats)
        {
            var fontPtr = pxFntLoadFromVdf(vdfPtr, "FONT_DEFAULT.FNT");

            if (fontPtr == IntPtr.Zero)
                return null;

            var fontName = PxPhoenix.MarshalString(pxFntGetName(fontPtr));
            var font = new PxFontData()
            {
                name = fontName,
                height = pxFntGetHeight(fontPtr),
                glyphs = GetGlyphs(fontPtr),
                texture = PxTexture.GetTextureFromVdf(vdfPtr, fontName, supportedTextureFormats)
            };

            pxFntDestroy(fontPtr);

            return font;
        }

        private static PxFontGlyphData[] GetGlyphs(IntPtr fontPtr)
        {
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

            return glyphs;
        }
    }
}
