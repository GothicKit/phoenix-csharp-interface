using PxCs.Data.Texture;

namespace PxCs.Data.Font
{
    public class PxFontData
    {
        public string? name;
        public uint height;

        public PxTextureData? texture;

        public PxFontGlyphData[]? glyphs;
    }
}
