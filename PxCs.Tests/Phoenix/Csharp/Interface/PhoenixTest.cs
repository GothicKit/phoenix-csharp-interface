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
        /// Convert the given path into its canonical representation for the current operating system.
        /// </summary>
        /// <param name="path">The path to normalize</param>
        /// <returns>The canonical representation of the path.</returns>
        protected static string NormalizePath(string path)
        {
            return Path.GetFullPath(path);
        }

        /// <summary>
        /// Join the given paths, appending rhs to lhs.
        /// </summary>
        /// <param name="lhs">The left-hand-side of the path</param>
        /// <param name="rhs">The right-hand-side of the path</param>
        /// <returns>The joined path in its canonical representation for the current operating system.</returns>
        protected static string JoinPaths(string lhs, string rhs)
        {
            return NormalizePath(Path.Join(lhs, rhs));
        }

        /// <summary>
        /// Get the full path to the asset with the given relative location in the Gothic game directory.
        /// </summary>
        /// <param name="relative">The relative path of the asset to the Gothic game directory root.</param>
        /// <returns>The full, normalized path to the asset.</returns>
        protected string GetAssetPath(string relative)
        {
            return JoinPaths(G1_ASSET_DIR, relative);
        }


        /// <summary>
        /// Convenient function to load Vdf file.
        /// </summary>
        /// <param name="pathSuffix"></param>
        /// <returns></returns>
        protected IntPtr LoadVdf(string pathSuffix)
        {
            string fullPath = GetAssetPath(pathSuffix);
            Assert.True(File.Exists(fullPath), "Path >" + fullPath + "< does not exist.");

            var vdfPtrMain = Vdf.pxVdfNew("main");
            var vdfPtrToLoad = Vdf.pxVdfLoadFromFile(fullPath);

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