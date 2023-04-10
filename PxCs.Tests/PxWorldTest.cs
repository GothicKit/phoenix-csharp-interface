using System;
using PxCs;
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

            var mesh = PxWorld.pxWorldGetMesh(worldPtr);
            var vertexCount = PxMesh.pxMshGetVertexCount(mesh);
            var materialCount = PxMesh.pxMshGetMaterialCount(mesh);
            var featureCount = PxMesh.pxMshGetFeatureCount(mesh);

            Assert.Equal(55439u, vertexCount);
            Assert.Equal(2263u, materialCount);
            Assert.Equal(419936u, featureCount);

            PxWorld.pxWorldDestroy(worldPtr);

            DestroyVdf(vdfPtr);
        }
    }
}
