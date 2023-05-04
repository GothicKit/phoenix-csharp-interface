using PxCs.Interface;
using Xunit;

namespace PxCs.Tests
{
    public class PxModelMeshTest : PxPhoenixTest
    {
        [Fact]
        public void Test_load_ModelMesh()
        {
            var vdfPtr = LoadVdf("Data/anims.VDF");

            var modelMesh = PxModelMesh.LoadModelMeshFromVdf(vdfPtr, "HUM_BODY_NAKED0.MDM");

            Assert.NotNull(modelMesh);
            Assert.True(modelMesh.checksum == 3325331650, $"Checksum is wrong with >{modelMesh.checksum}<");
            Assert.Single(modelMesh.meshes!);

            DestroyVdf(vdfPtr);
        }
    }
}
