using System;
using System.IO;
using Xunit;

namespace Phoenix.Csharp.Interface
{
    public abstract class PhoenixTest
    {
        protected readonly string G1_ASSET_DIR;

        public PhoenixTest()
        {
            string? dir = Environment.GetEnvironmentVariable("GOTHIC1_ASSET_DIR");

            Assert.True(dir != null, "Please start test with dotnet test --environment GOTHIC1_ASSET_DIR=...");
            Assert.True(Directory.Exists(dir), "Path not exists");

            G1_ASSET_DIR = dir;
        }


        /// <summary>
        /// Convenient function to load Vdf file.
        /// </summary>
        /// <param name="pathSuffix"></param>
        /// <returns></returns>
        protected IntPtr LoadVdf(string pathSuffix)
        {
            string fullPath = G1_ASSET_DIR + pathSuffix;
            Assert.True(File.Exists(fullPath), "Path >" + fullPath + "< does not exist.");

            var vdfPtrMain = Vdf.pxVdfNew("main");
            var vdfPtrToLoad = Vdf.pxVdfLoadFromFile(G1_ASSET_DIR + pathSuffix);

            Vdf.pxVdfMerge(vdfPtrMain, vdfPtrToLoad, true);
            Vdf.pxVdfDestroy(vdfPtrToLoad); // Data is already copied into vdfPtrMain and can therefore be Destroyed.

            return vdfPtrMain;
        }

        protected void DestroyVdf(IntPtr vdf)
        {
            Vdf.pxVdfDestroy(vdf);
        }
    }
}