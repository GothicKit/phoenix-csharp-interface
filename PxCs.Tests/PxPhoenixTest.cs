using System;
using System.IO;
using PxCs.Interface;
using Xunit;

namespace PxCs.Tests
{
    public abstract class PxPhoenixTest
    {
        private readonly string G1_ASSET_DIR;

        public PxPhoenixTest()
        {
            string? dir = Environment.GetEnvironmentVariable("GOTHIC1_ASSET_DIR");

            Assert.True(dir != null, "Please start test with dotnet test --environment GOTHIC1_ASSET_DIR=... or configure ./.runsettings to be used by your IDE runner.");
            Assert.True(Directory.Exists(dir), "Path not exists");

            G1_ASSET_DIR = dir;

            PxLogging.pxLoggerSet(PxLogMessage);
        }

        public static void PxLogMessage(PxLogging.Level level, string message)
        {
            // Hint: Phoenix also throws error logs when e.g. a file isn't found. Therefore we can't use it for every test.
            // But we can leave it here for specific debugging purposes.

            //Assert.True(level != PxLogging.Level.error, "VM error logged: >" + message + "<");
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

        protected IntPtr LoadVfs(string relativeFilePath)
        {
            string fullPath = GetAssetPath(relativeFilePath);
            Assert.True(File.Exists(fullPath), "Path >" + fullPath + "< does not exist.");

            var vfsPtr = PxVfs.pxVfsNew();
            PxVfs.pxVfsMountFile(vfsPtr, fullPath);
            
            return vfsPtr;
        }
        protected void DestroyVfs(IntPtr vfs)
        {
            PxVfs.pxVfsDestroy(vfs);
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