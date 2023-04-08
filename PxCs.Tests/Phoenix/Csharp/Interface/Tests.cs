using System;
using System.IO;
using Xunit;
using static Phoenix.Csharp.Interface.Texture;

namespace Phoenix.Csharp.Interface
{
    public class Tests: PhoenixTest
    {
        [Fact]
        public void Test_load_Vdf()
        {
            var vdfPtrMain = Vdf.pxVdfNew("main");
            var vdfPtrWorlds = Vdf.pxVdfLoadFromFile(GetAssetPath("Data/worlds.VDF"));
            var vdfPtrTextures = Vdf.pxVdfLoadFromFile(GetAssetPath("Data/textures.VDF"));

            Assert.True(vdfPtrMain != IntPtr.Zero);
            Assert.True(vdfPtrWorlds != IntPtr.Zero);
            Assert.True(vdfPtrTextures != IntPtr.Zero);

            Vdf.pxVdfMerge(vdfPtrMain, vdfPtrWorlds, true);
            Vdf.pxVdfMerge(vdfPtrMain, vdfPtrTextures, true);

            Vdf.pxVdfDestroy(vdfPtrWorlds);
            Vdf.pxVdfDestroy(vdfPtrTextures);
            Vdf.pxVdfDestroy(vdfPtrMain);
        }

        [Fact]
        public void Test_load_World_Mesh()
        {
            var vdfPtr = LoadVdf("Data/worlds.VDF");

            var worldPtr = World.pxWorldLoadFromVdf(vdfPtr, "world.zen");
            Assert.True(worldPtr != IntPtr.Zero, "World couldn't be loaded inside vdf.");

            var mesh = World.pxWorldGetMesh(worldPtr);
            var vertexCount = Mesh.pxMshGetVertexCount(mesh);
            var materialCount = Mesh.PxMshGetMaterialCount(mesh);
            var featureCount = Mesh.pxMshGetFeatureCount(mesh);

            Assert.Equal(55439u, vertexCount);
            Assert.Equal(2263u, materialCount);
            Assert.Equal(419936u, featureCount);

            World.pxWorldDestroy(worldPtr);

            DestroyVdf(vdfPtr);
        }

        [Fact]
        public void Test_load_Texture()
        {
            var vdfPtr = LoadVdf("Data/textures.VDF");

            // Textures are named uncompiled >.TGA< and compiled >-C.TEX<
            var texPtr = Texture.pxTexLoadFromVdf(vdfPtr, "OWODPAIGRASSMI-C.TEX");
            Assert.True(texPtr != IntPtr.Zero, "Texture couldn't be loaded inside vdf.");

            Texture.pxTexGetMeta(texPtr, out Format format, out uint width, out uint height, out uint mipmapCount, out uint averageColor);
            Assert.True(format == Format.tex_dxt1, "format >tex_dxt1< doesn't match.");
            Assert.True(width == 256u, "width >256u< doesn't match.");
            Assert.True(height == 256u, "height >256u< doesn't match.");
            Assert.True(mipmapCount == 6u, "mipmapCount >6u< doesn't match.");



            Texture.pxTexDestroy(texPtr);
        }
    }
}