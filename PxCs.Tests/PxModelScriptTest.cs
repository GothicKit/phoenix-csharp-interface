using PxCs.Interface;
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

            Assert.True(mds.animations.Length == 864, $"Animations length is wrong with >{mds.animations.Length}<");
            Assert.True(mds.animations[0].model == "Hum_RunAmbient_M01.asc", $"AnimationModel[0] is wrong with >{mds.animations[0].model}<");
            Assert.True(mds.disabledAnimations.Length == 9, $"DisabledAnimations length is wrong with >{mds.disabledAnimations.Length}<");
            Assert.True(mds.meshes.Length == 50, $"Meshes length is wrong with >{mds.meshes.Length}<");
            Assert.True(mds.meshes[0] == "Hum_Body_Naked0.ASC", $"Mesh[0] is wrong with >{mds.meshes[0]}<");

            DestroyVdf(vdfPtr);
        }
    }
}
