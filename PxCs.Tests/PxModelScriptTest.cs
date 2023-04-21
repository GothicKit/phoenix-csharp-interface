using Xunit;

namespace PxCs.Tests
{
    public class PxModelScriptTest : PxPhoenixTest
    {
        
        [Fact]
        public void Test_load_model()
        {
            var vdfPtr = LoadVdf("Data/anims.VDF");

            var mds = PxModelScript.GetModelScriptFromVdf(vdfPtr, "HUMANS.MDS");

            Assert.NotNull(mds);

            DestroyVdf(vdfPtr);
        }
    }
}
