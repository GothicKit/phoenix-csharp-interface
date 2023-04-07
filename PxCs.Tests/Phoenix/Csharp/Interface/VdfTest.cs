using System;
using Xunit;

namespace Phoenix.Csharp.Interface
{
    public class VdfTest: PhoenixTest
    {
        [Fact]
        public void Is_Vdf_loadable()
        {
            var vdfPtrMain = Vdf.pxVdfNew("main");
            var vdfPtrWorlds = Vdf.pxVdfLoadFromFile(G1_ASSET_DIR + "/Data/worlds.VDF");
            var vdfPtrTextures = Vdf.pxVdfLoadFromFile(G1_ASSET_DIR + "/Data/textures.VDF");

            Assert.True(vdfPtrMain != IntPtr.Zero);
            Assert.True(vdfPtrWorlds != IntPtr.Zero);
            Assert.True(vdfPtrTextures != IntPtr.Zero);

            Vdf.pxVdfMerge(vdfPtrMain, vdfPtrWorlds, true);
            Vdf.pxVdfMerge(vdfPtrMain, vdfPtrTextures, true);

            // Destroy early to check that data is stored on vdfPtrMain (as expected)
            Vdf.pxVdfDestroy(vdfPtrWorlds);
            Vdf.pxVdfDestroy(vdfPtrTextures);
            Vdf.pxVdfDestroy(vdfPtrMain);
        }
    }
}