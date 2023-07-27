
using PxCs.Interface;
using Xunit;

namespace PxCs.Tests
{
    public class PxMorphMeshTest : PxPhoenixTest
    {

        [Fact]
        public void Test_load_model()
        {
            var vfsPtr = LoadVfs("Data/anims.VDF");

            var mmb = PxMorphMesh.LoadMorphMeshFromVfs(vfsPtr, "Hum_Head_FatBald.MMB");

            Assert.NotNull(mmb);
            Assert.NotNull(mmb.mesh);
            Assert.NotNull(mmb.animations);

            DestroyVfs(vfsPtr);
        }
    }
}
