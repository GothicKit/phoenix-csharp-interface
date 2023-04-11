using System;
using System.Runtime.InteropServices;
using Xunit;

namespace PxCs.Tests
{
    public class PXTextureTest : PxPhoenixTest
    {
        [Fact]
        public void Test_load_Texture()
        {
            var vdfPtr = LoadVdf("Data/textures.VDF");

            // Textures are named uncompiled >.TGA< and compiled >-C.TEX<
            var texPtr = PxTexture.pxTexLoadFromVdf(vdfPtr, "OWODPAIGRASSMI-C.TEX");
            Assert.True(texPtr != IntPtr.Zero, "Texture couldn't be loaded inside vdf.");

            PxTexture.pxTexGetMeta(texPtr, out PxTexture.Format format, out uint width, out uint height, out uint mipmapCount, out uint averageColor);
            Assert.True(format == PxTexture.Format.tex_dxt1, "format >tex_dxt1< doesn't match.");
            Assert.True(width == 256u, "width >256u< doesn't match.");
            Assert.True(height == 256u, "height >256u< doesn't match.");
            Assert.True(mipmapCount == 6u, "mipmapCount >6u< doesn't match.");

            var mipMapArrayPtr = PxTexture.pxTexGetMipmap(texPtr, out uint lengthMip0, 0, out uint widthMip0, out uint heightMip0);
            var mipMapArray = new byte[lengthMip0];

            Marshal.Copy(mipMapArrayPtr, mipMapArray, 0, (int)lengthMip0);

            Assert.True(mipMapArray[10] == 227, "Picked some mipmap data. It's expected to have value >227<.");

            PxTexture.pxTexDestroy(texPtr);
        }

        [Fact]
        public void Test_load_Texture_via_wrapper()
        {
            var vdfPtr = LoadVdf("Data/textures.VDF");

            var texture = PxTexture.GetTextureFromVdf(vdfPtr, "OWODPAIGRASSMI-C.TEX");

            Assert.True(texture.format == PxTexture.Format.tex_dxt1, "format >tex_dxt1< doesn't match.");
            Assert.True(texture.width == 256u, "width >256u< doesn't match.");
            Assert.True(texture.height == 256u, "height >256u< doesn't match.");
            Assert.True(texture.mipmapCount == 6u, "mipmapCount >6u< doesn't match.");

            Assert.True(texture.mipmaps[0].mipmap[10] == 227, "Picked some mipmap data. It's expected to have value >227<.");
        }
    }
}
