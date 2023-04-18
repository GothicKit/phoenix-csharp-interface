namespace PxCs.Data
{
    public class PxFontData
    {
        public string name = default!;
        public uint height;

        public PxTextureData? texture;

        public PxFontGlyphData[] glyphs = default!;
    }
}
