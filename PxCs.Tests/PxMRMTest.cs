using Xunit;

namespace PxCs.Tests
{
    public class PxMRMTest : PxPhoenixTest
    {

        [Fact]
        public void Test_load_MRM()
        {
            var vdfPtr = LoadVdf("Data/meshes.VDF");

            var mrm = PxMRM.GetMRMFromVdf(vdfPtr, "ITFO_PLANTS_BERRYS_01.MRM");

            Assert.NotNull(mrm);

            DestroyVdf(vdfPtr);
        }
    }
}
