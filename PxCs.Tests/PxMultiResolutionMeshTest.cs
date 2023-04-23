using System.Linq;
using Xunit;

namespace PxCs.Tests
{
    public class PxMultiResolutionMeshTest : PxPhoenixTest
    {

        [Fact]
        public void Test_load_MRM()
        {
            var vdfPtr = LoadVdf("Data/meshes.VDF");

            var mrm = PxMultiResolutionMesh.GetMRMFromVdf(vdfPtr, "ITFO_PLANTS_BERRYS_01.MRM");

            Assert.NotNull(mrm);
            Assert.Single(mrm.materials);
            Assert.Equal("ITFOOD", mrm.materials.First().name);
            Assert.Equal(30, mrm.positions.Length);
            Assert.Single(mrm.subMeshes);
            Assert.Equal(54, mrm.subMeshes.First().triangles.Length);
            
            DestroyVdf(vdfPtr);
        }
    }
}
