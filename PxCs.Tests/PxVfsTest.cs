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
            PxVfs.pxVfsMountFile(vfsPtr, GetAssetPath("Data/worlds.VDF"));
            PxVfs.pxVfsMountFile(vfsPtr, GetAssetPath("Data/textures.VDF"));

            // If we reach this far, mounting was successful. Test complete. Starting cleanup.

            PxVfs.pxVfsDestroy(vfsPtr);
        }
    }
}