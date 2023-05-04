using PxCs.Interface;
using Xunit;

namespace PxCs.Tests
{
    public class PxModelHierarchyTest : PxPhoenixTest
    {

        [Fact]
        public void Test_load_ModelHierarchy()
        {
            var vdfPtr = LoadVdf("Data/anims.VDF");

            var mdh = PxModelHierarchy.LoadFromVdf(vdfPtr, "HUMANS.MDH");

            Assert.NotNull(mdh);

            DestroyVdf(vdfPtr);
        }
    }
}
