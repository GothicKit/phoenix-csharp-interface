using System;
using System.IO;
using Xunit;

namespace Phoenix.Csharp.Interface
{
    public abstract class PhoenixTest
    {
        private readonly string G1_ASSET_DIR;

        public PhoenixTest()
        {
            string? dir = Environment.GetEnvironmentVariable("GOTHIC1_ASSET_DIR");

            Assert.True(dir != null, "Please start test with dotnet test --environment GOTHIC1_ASSET_DIR=...");
            Assert.True(Directory.Exists(dir), "Path not exists");

            G1_ASSET_DIR = dir;
        }

        /// <summary>
        /// The joined path in its canonical representation for the current operating system.
        /// </summary>
        protected string GetAssetPath(string relativeFilePath)
        {
            var joinedPath = Path.Join(G1_ASSET_DIR, relativeFilePath);

            return Path.GetFullPath(joinedPath);
        }


        protected IntPtr LoadVdf(string relativeFilePath)
        {
            string fullPath = GetAssetPath(relativeFilePath);
            Assert.True(File.Exists(fullPath), "Path >" + fullPath + "< does not exist.");

            var vdfPtrMain = Vdf.pxVdfNew("main");
            var vdfPtrToLoad = Vdf.pxVdfLoadFromFile(fullPath);

            Vdf.pxVdfMerge(vdfPtrMain, vdfPtrToLoad, true);
            Vdf.pxVdfDestroy(vdfPtrToLoad); // Data is already copied into vdfPtrMain and can therefore be destroyed.

            return vdfPtrMain;
        }

        protected void DestroyVdf(IntPtr vdf)
        {
            Vdf.pxVdfDestroy(vdf);
        }
    }
}