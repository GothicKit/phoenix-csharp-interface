using System;
using System.Runtime.InteropServices;
using Xunit;

namespace Phoenix.Csharp.Interface
{
    public class TextureTest : PhoenixTest
    {
        [Fact]
        public void Test_load_Texture()
        {
            var vdfPtr = LoadVdf("Data/textures.VDF");

            // Textures are named uncompiled >.TGA< and compiled >-C.TEX<
            var texPtr = Texture.pxTexLoadFromVdf(vdfPtr, "OWODPAIGRASSMI-C.TEX");
            Assert.True(texPtr != IntPtr.Zero, "Texture couldn't be loaded inside vdf.");

            Texture.pxTexGetMeta(texPtr, out Texture.Format format, out uint width, out uint height, out uint mipmapCount, out uint averageColor);
            Assert.True(format == Texture.Format.tex_dxt1, "format >tex_dxt1< doesn't match.");
            Assert.True(width == 256u, "width >256u< doesn't match.");
            Assert.True(height == 256u, "height >256u< doesn't match.");
            Assert.True(mipmapCount == 6u, "mipmapCount >6u< doesn't match.");

            var mipMapArrayPtr = Texture.pxTexGetMipmap(texPtr, out uint lengthMip0, 0, out uint widthMip0, out uint heightMip0);
            var mipMapArray = new byte[lengthMip0];

            Marshal.Copy(mipMapArrayPtr, mipMapArray, 0, (int)lengthMip0);

            Assert.True(mipMapArray[10] == 227, "Picked some mipmap data. It's expected to have value >227<.");

            Texture.pxTexDestroy(texPtr);
        }
    }
}
