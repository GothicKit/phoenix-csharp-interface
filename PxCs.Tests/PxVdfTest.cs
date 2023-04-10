using System;
using PxCs;
using Xunit;

namespace PxCs.Tests
{
    public class PxVdfTest : PxPhoenixTest
    {
        [Fact]
        public void Test_load_Vdf()
        {
            var vdfPtrMain = PxVdf.pxVdfNew("main");
            var vdfPtrWorlds = PxVdf.pxVdfLoadFromFile(GetAssetPath("Data/worlds.VDF"));
            var vdfPtrTextures = PxVdf.pxVdfLoadFromFile(GetAssetPath("Data/textures.VDF"));

            Assert.True(vdfPtrMain != IntPtr.Zero);
            Assert.True(vdfPtrWorlds != IntPtr.Zero);
            Assert.True(vdfPtrTextures != IntPtr.Zero);

            PxVdf.pxVdfMerge(vdfPtrMain, vdfPtrWorlds, true);
            PxVdf.pxVdfMerge(vdfPtrMain, vdfPtrTextures, true);

            PxVdf.pxVdfDestroy(vdfPtrWorlds);
            PxVdf.pxVdfDestroy(vdfPtrTextures);
            PxVdf.pxVdfDestroy(vdfPtrMain);
        }
    }
}