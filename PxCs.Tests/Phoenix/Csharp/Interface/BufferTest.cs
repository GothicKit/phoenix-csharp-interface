using System;
using Xunit;

namespace Phoenix.Csharp.Interface
{
    public class BufferTest : PhoenixTest
    {
        [Fact]
        public void Test_open_file()
        {
            var bufferPtr = Buffer.pxBufferMmap(GetAssetPath("Data/textures_bikini.VDF"));

            Assert.True(bufferPtr != IntPtr.Zero, "Buffer has no pointer and therefore no data. Does your file exist?");

            Buffer.pxBufferDestroy(bufferPtr);
        }
    }
}
