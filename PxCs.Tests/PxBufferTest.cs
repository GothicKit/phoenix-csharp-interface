using System;
using PxCs.Interface;
using Xunit;

namespace PxCs.Tests
{
    public class PxBufferTest : PxPhoenixTest
    {
        [Fact]
        public void Test_open_file()
        {
            var bufferPtr = PxBuffer.pxBufferMmap(GetAssetPath("Data/textures_bikini.VDF"));

            Assert.True(bufferPtr != IntPtr.Zero, "Buffer has no pointer and therefore no data. Does your file exist?");

            PxBuffer.pxBufferDestroy(bufferPtr);
        }
    }
}
