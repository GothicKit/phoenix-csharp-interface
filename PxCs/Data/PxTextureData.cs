using static PxCs.PxTexture;

namespace PxCs.Data
{
    public class PxTextureData
    {
        public Format format;
        public uint width;
        public uint height;
        public uint mipmapCount;
        public uint averageColor;

        public PxTextureMipmapData[] mipmaps = default!;
    }
}
