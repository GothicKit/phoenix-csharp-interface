
using PxCs.Interface;
using Xunit;

namespace PxCs.Tests
{
    public class PxMorphMeshTest : PxPhoenixTest
    {

        [Fact]
        public void Test_load_model()
        {
            var vdfPtr = LoadVdf("Data/anims.VDF");

            var mmb = PxMorphMesh.LoadMorphMeshFromVdf(vdfPtr, "Hum_Head_FatBald.MMB");

            Assert.NotNull(mmb);
            Assert.NotNull(mmb.mesh);
            Assert.NotNull(mmb.animations);

            DestroyVdf(vdfPtr);
        }
    }
}
