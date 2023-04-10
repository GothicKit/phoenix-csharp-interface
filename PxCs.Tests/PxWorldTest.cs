using System;
using Xunit;

namespace PxCs.Tests
{
    public class PxWorldTest : PxPhoenixTest
    {
        [Fact]
        public void Test_load_World_Mesh()
        {
            var vdfPtr = LoadVdf("Data/worlds.VDF");

            var worldPtr = PxWorld.pxWorldLoadFromVdf(vdfPtr, "world.zen");
            Assert.True(worldPtr != IntPtr.Zero, "World couldn't be loaded inside vdf.");

            PxWorld.pxWorldDestroy(worldPtr);
            DestroyVdf(vdfPtr);
        }
    }
}
