using System;
using Xunit;

namespace Phoenix.Csharp.Interface
{
    public class WorldTest : PhoenixTest
    {
        [Fact]
        public void Test_load_World_Mesh()
        {
            var vdfPtr = LoadVdf("Data/worlds.VDF");

            var worldPtr = World.pxWorldLoadFromVdf(vdfPtr, "world.zen");
            Assert.True(worldPtr != IntPtr.Zero, "World couldn't be loaded inside vdf.");

            var mesh = World.pxWorldGetMesh(worldPtr);
            var vertexCount = Mesh.pxMshGetVertexCount(mesh);
            var materialCount = Mesh.pxMshGetMaterialCount(mesh);
            var featureCount = Mesh.pxMshGetFeatureCount(mesh);

            Assert.Equal(55439u, vertexCount);
            Assert.Equal(2263u, materialCount);
            Assert.Equal(419936u, featureCount);

            World.pxWorldDestroy(worldPtr);

            DestroyVdf(vdfPtr);
        }
    }
}
