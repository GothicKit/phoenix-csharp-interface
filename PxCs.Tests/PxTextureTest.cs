using PxCs.Interface;
using System;
using System.Runtime.InteropServices;
using Xunit;

namespace PxCs.Tests
{
    public class PxTextureTest : PxPhoenixTest
    {
        [Fact]
        public void Test_load_Texture()
        {
            var vfsPtr = LoadVfs("Data/textures.VDF");

            // Textures are named uncompiled >.TGA< and compiled >-C.TEX<
            var texPtr = PxTexture.pxTexLoadFromVfs(vfsPtr, "OWODPAIGRASSMI-C.TEX");
            Assert.True(texPtr != IntPtr.Zero, "Texture couldn't be loaded inside vfs.");

            PxTexture.pxTexGetMeta(texPtr, out PxTexture.Format format, out uint width, out uint height, out uint mipmapCount, out uint averageColor);
            Assert.True(format == PxTexture.Format.tex_dxt1, "format >tex_dxt1< doesn't match.");
            Assert.True(width == 256u, "width >256u< doesn't match.");
            Assert.True(height == 256u, "height >256u< doesn't match.");
            Assert.True(mipmapCount == 6u, "mipmapCount >6u< doesn't match.");

            var mipMapArrayPtr = PxTexture.pxTexGetMipmap(texPtr, 0, out uint lengthMip0, out uint _, out uint _);
            var mipMapArray = new byte[lengthMip0];

            Marshal.Copy(mipMapArrayPtr, mipMapArray, 0, (int)lengthMip0);

            Assert.True(mipMapArray[10] == 227, "Picked some mipmap data. It's expected to have value >227<.");

            PxTexture.pxTexDestroy(texPtr);
            DestroyVfs(vfsPtr);
        }

        [Fact]
        public void Test_load_Texture_via_wrapper()
        {
            var vfsPtr = LoadVfs("Data/textures.VDF");

            var texture = PxTexture.GetTextureFromVfs(vfsPtr, "OWODPAIGRASSMI-C.TEX", PxTexture.Format.tex_dxt1);

            Assert.NotNull(texture);

            Assert.True(texture.format == PxTexture.Format.tex_dxt1, $"format >{texture.format}< doesn't match expected >{PxTexture.Format.tex_dxt1}<.");
            Assert.True(texture.width == 256u, "width >256u< doesn't match.");
            Assert.True(texture.height == 256u, "height >256u< doesn't match.");
            Assert.True(texture.mipmapCount == 6u, "mipmapCount >6u< doesn't match.");

            Assert.True(texture.mipmaps![0].mipmap[10] == 227, "Picked some mipmap data. It's expected to have value >227<.");

            DestroyVfs(vfsPtr);
        }

        [Fact]
        public void Test_load_Texture_via_wrapper_unsupported_format()
        {
            var vfsPtr = LoadVfs("Data/textures.VDF");

            var texture = PxTexture.GetTextureFromVfs(vfsPtr, "LOG_PAPER-C.TEX");

            Assert.NotNull(texture);

            Assert.True(texture.format == PxTexture.Format.tex_B8G8R8A8, $"format >{texture.format}< doesn't match expected >{PxTexture.Format.tex_B8G8R8A8}<.");
            Assert.True(texture.width == 512u, "width >512u< doesn't match.");
            Assert.True(texture.height == 512u, "height >512u< doesn't match.");
            Assert.True(texture.mipmapCount == 1u, "mipmapCount >6u< doesn't match.");

            Assert.True(texture.mipmaps![0].mipmap.Length == 1048576, "Picked some mipmap data. It's expected to have value >1048576<.");

            DestroyVfs(vfsPtr);
        }
    }
}
