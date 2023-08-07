using System;
using System.Linq;
using PxCs.Interface;
using Xunit;

namespace PxCs.Tests
{
    public class PxVfsTest : PxPhoenixTest
    {
        [Fact]
        public void Test_load_Vfs()
        {
            var vfsPtr = PxVfs.pxVfsNew();
            PxVfs.pxVfsMountDisk(vfsPtr, GetAssetPath("Data/worlds.VDF"), PxVfs.PxVfsOverwriteBehavior.PxVfsOverwrite_Older);
            PxVfs.pxVfsMountDisk(vfsPtr, GetAssetPath("Data/textures.VDF"), PxVfs.PxVfsOverwriteBehavior.PxVfsOverwrite_Older);

            // If we reach this far, mounting was successful. Test complete. Starting cleanup.

            PxVfs.pxVfsDestroy(vfsPtr);
        }
    }
}