using Xunit;

namespace PxCs.Tests
{
    public class PxModelScriptTest : PxPhoenixTest
    {
        
        [Fact]
        public void Test_load_model()
        {
            var vdfPtr = LoadVdf("Data/anims.VDF");

            var mdsPtr = PxModelScript.pxMdsLoadFromVdf(vdfPtr, "HUMANS.MDS");

            Assert.True(mdsPtr != System.IntPtr.Zero);

            PxModelScript.pxMdsDestroy(mdsPtr);
            DestroyVdf(vdfPtr);
        }
    }
}
