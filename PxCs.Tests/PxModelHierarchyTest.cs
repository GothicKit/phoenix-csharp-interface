using PxCs.Interface;
using Xunit;

namespace PxCs.Tests
{
    public class PxModelHierarchyTest : PxPhoenixTest
    {

        [Fact]
        public void Test_load_ModelHierarchy()
        {
            var vfsPtr = LoadVfs("Data/anims.VDF");

            var mdh = PxModelHierarchy.LoadFromVfs(vfsPtr, "HUMANS.MDH");

            Assert.NotNull(mdh);

            DestroyVfs(vfsPtr);
        }
    }
}
