using System;
using PxCs.Data.Texture;

namespace PxCs.Data.Font
{
    [Serializable]
    public class PxFontData
    {
        public string name = default!;
        public uint height;

        public PxTextureData texture = default!;

        public PxFontGlyphData[] glyphs = default!;
    }
}
