using PxCs.Tests;
using System;
using Xunit;

namespace PxCs
{
    public class PxMeshTest : PxPhoenixTest
    {
        [Fact]
        public void Test_load_World_Mesh()
        {
            var vdfPtr = LoadVdf("Data/worlds.VDF");

            var worldPtr = PxWorld.pxWorldLoadFromVdf(vdfPtr, "world.zen");
            Assert.True(worldPtr != IntPtr.Zero, "World couldn't be loaded inside vdf.");

            var mesh = PxWorld.pxWorldGetMesh(worldPtr);
            var vertexCount = PxMesh.pxMshGetVertexCount(mesh);
            var featureCount = PxMesh.pxMshGetFeatureCount(mesh);
            Assert.Equal(55439u, vertexCount);
            Assert.Equal(419936u, featureCount);

            // Check indices
            var vertexIndices = PxMesh.GetPolygonVertexIndices(mesh);
            Assert.Equal(320166, vertexIndices.Length);
            var materialIndices = PxMesh.GetPolygonMaterialIndices(mesh);
            Assert.Equal(106722, materialIndices.Length);
            var featureIndices = PxMesh.GetPolygonFeatureIndices(mesh);
            Assert.Equal(320166, featureIndices.Length);

            PxWorld.pxWorldDestroy(worldPtr);

            DestroyVdf(vdfPtr);
        }
    }
}
