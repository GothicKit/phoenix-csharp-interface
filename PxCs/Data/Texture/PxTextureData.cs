using static PxCs.Interface.PxTexture;

namespace PxCs.Data.Texture
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
