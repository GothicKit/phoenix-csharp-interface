using System;
using Xunit;

namespace Phoenix.Csharp.Interface
{
    public class VdfTest: PhoenixTest
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
    }
}