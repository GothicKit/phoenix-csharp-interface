using System;

namespace PxCs.Data.Texture
{
    [Serializable]
    public class PxTextureMipmapData
    {
        public uint level;
        public uint width;
        public uint height;

        public byte[] mipmap = new byte[0];
    }
}
