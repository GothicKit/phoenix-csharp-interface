using PxCs.Interface;
using Xunit;

namespace PxCs.Tests
{
    public class PxModelTest : PxPhoenixTest
    {

        [Fact]
        public void Test_load_model()
        {
            var vfsPtr = LoadVfs("Data/anims.VDF");

            var mdl = PxModel.LoadModelFromVfs(vfsPtr, "BENCH_1_OC.MDL");

            Assert.NotNull(mdl);
            Assert.NotNull(mdl.hierarchy);
            Assert.NotNull(mdl.mesh);
            Assert.True(mdl.mesh.attachments!.Count == 1, $"Attachments need to be of size 1, but >{mdl.mesh.attachments!.Count}< given.");
            Assert.True(mdl.mesh.attachments!.ContainsKey("ZM_BENCHOC01"), "There needs to be an attachment called >ZM_BENCHOC01<.");

            DestroyVfs(vfsPtr);
        }
    }
}
