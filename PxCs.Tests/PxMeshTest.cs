using System;
using System.Numerics;
using Xunit;

namespace PxCs.Tests
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
            
            var vertices = PxMesh.GetVertices(mesh);
            Assert.Equal(55439u, vertices.LongLength);
            Assert.Equal(new Vector3(91365f, -4026.6008f, 46900f), vertices[0]); // Testing some random Vertex.

            var features = PxMesh.GetFeatures(mesh);
            Assert.Equal(419936u, features.LongLength);
            Assert.Equal(new Vector2(1.1119385f, 2.6441517f), features[0].texture); // Testing some random Feature.

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
