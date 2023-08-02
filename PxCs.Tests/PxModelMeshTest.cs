using PxCs.Interface;
using Xunit;

namespace PxCs.Tests
{
    public class PxModelMeshTest : PxPhoenixTest
    {
        [Fact]
        public void Test_load_ModelMesh()
        {
            var vfsPtr = LoadVfs("Data/anims.VDF");

            var modelMesh = PxModelMesh.LoadModelMeshFromVfs(vfsPtr, "HUM_BODY_NAKED0.MDM");

            Assert.NotNull(modelMesh);
            Assert.True(modelMesh.checksum == 3325331650, $"Checksum is wrong with >{modelMesh.checksum}<");
            Assert.Single(modelMesh.meshes!);

            DestroyVfs(vfsPtr);
        }

        [Fact]
        public void Test_load_ModelMesh_with_Attachments()
        {
            var vfsPtr = LoadVfs("Data/anims.VDF");

            var modelMesh = PxModelMesh.LoadModelMeshFromVfs(vfsPtr, "chestbig_occhestlarge.mdm",
                "BIP01 CHEST_BIG_0", "BIP01 CHEST_BIG_1", "BIP01 CHESTLOCK", "ZS_POS0");

            Assert.NotNull(modelMesh);
            Assert.True(modelMesh.checksum == 0, $"Checksum needs to be zero for Chest.");
            Assert.True(modelMesh.meshes!.Length == 0, "Meshes should be empty for Chest.");
            Assert.True(modelMesh.attachments!.Count == 2, "There need to be 2 attachments loaded from 4 requested.");

            DestroyVfs(vfsPtr);
        }
    }
}
