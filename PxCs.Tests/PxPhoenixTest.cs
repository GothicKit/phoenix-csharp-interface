using System;
using System.IO;
using Xunit;

namespace PxCs.Tests
{
    public abstract class PxPhoenixTest
    {
        private readonly string G1_ASSET_DIR;

        public PxPhoenixTest()
        {
            string? dir = Environment.GetEnvironmentVariable("GOTHIC1_ASSET_DIR");

            Assert.True(dir != null, "Please start test with dotnet test --environment GOTHIC1_ASSET_DIR=...");
            Assert.True(Directory.Exists(dir), "Path not exists");

            G1_ASSET_DIR = dir;

            PxLogging.pxLoggerSet(PxLogMessage);
        }

        public static void PxLogMessage(PxLogging.Level level, string message)
        {
            Assert.True(level != PxLogging.Level.error, "VM error logged: >" + message + "<");
        }

        /// <summary>
        /// The joined path in its canonical representation for the current operating system.
        /// </summary>
        protected string GetAssetPath(string relativeFilePath)
        {
            var joinedPath = Path.Join(G1_ASSET_DIR, relativeFilePath);
            var fullPath = Path.GetFullPath(joinedPath);

            Assert.True(File.Exists(joinedPath), "The filePath >" + fullPath + "< doesn't exist.");

            return fullPath;
        }

        protected IntPtr LoadVdf(string relativeFilePath)
        {
            string fullPath = GetAssetPath(relativeFilePath);
            Assert.True(File.Exists(fullPath), "Path >" + fullPath + "< does not exist.");

            var vdfPtrMain = PxVdf.pxVdfNew("main");
            var vdfPtrToLoad = PxVdf.pxVdfLoadFromFile(fullPath);

            PxVdf.pxVdfMerge(vdfPtrMain, vdfPtrToLoad, true);
            PxVdf.pxVdfDestroy(vdfPtrToLoad); // Data is already copied into vdfPtrMain and can therefore be destroyed.

            return vdfPtrMain;
        }
        protected void DestroyVdf(IntPtr vdf)
        {
            PxVdf.pxVdfDestroy(vdf);
        }

        protected IntPtr LoadBuffer(string relativeFilePath)
        {
            var bufferPtr = PxBuffer.pxBufferMmap(GetAssetPath(relativeFilePath));

            Assert.True(bufferPtr != IntPtr.Zero, "Buffer has no pointer and therefore no data. Does your file exist?");

            return bufferPtr;
        }

        protected void DestroyBuffer(IntPtr bufferPtr)
        {
            PxBuffer.pxBufferDestroy(bufferPtr);
        }


    }
}