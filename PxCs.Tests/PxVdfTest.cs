using System;
using System.Linq;
using PxCs.Interface;
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

        [Fact]
        public void Test_load_Vdf_Meta_Entry_list()
        {
            var vdfPtr = LoadVdf("Data/meshes.VDF");

            var entries = PxVdf.LoadEntryMetaDataList(vdfPtr);

            Assert.Single(entries.children!);
            Assert.True(entries.children![0].name == "_WORK", "Name of first folder is wrong.");
            Assert.True(entries.children![0].isDirectory == true, "_Work needs to be a directory.");
            Assert.Single(entries.children![0].children!);

            DestroyVdf(vdfPtr);
        }
    }
}