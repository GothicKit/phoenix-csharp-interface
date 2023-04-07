using System;
using Xunit;

namespace Phoenix.Csharp.Interface
{
    public class VdfTest: PhoenixTest
    {
        [Fact]
        public void Is_Vdf_loadable()
        {
            var vdfPtr = Vdf.pxVdfLoadFromFile(G_ASSET_DIR + "/worlds.VDF");
            Assert.True(vdfPtr != IntPtr.Zero);

            Vdf.pxVdfDestroy(vdfPtr);
        }
    }
}