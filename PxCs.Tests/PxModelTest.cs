using PxCs.Interface;
using Xunit;

namespace PxCs.Tests
{
    public class PxModelTest : PxPhoenixTest
    {

        [Fact]
        public void Test_load_model()
        {
            var vdfPtr = LoadVdf("Data/anims.VDF");

            var mdl = PxModel.LoadModelFromVdf(vdfPtr, "BENCH_1_OC.MDL");

            Assert.NotNull(mdl);
            Assert.NotNull(mdl.hierarchy);
            Assert.NotNull(mdl.mesh);

            // FIXME - The mesh itself has no data. Seems as internally it isn't found. Need to find out why!

            DestroyVdf(vdfPtr);
        }
    }
}
