using System;
using PxCs.Interface;
using Xunit;

namespace PxCs.Tests
{
    public class PxAnimationTest : PxPhoenixTest
    {
        [Fact]
        public void Test_load_animation()
        {
            var vdfPtr = LoadVdf("Data/anims.VDF");

            var animation = PxAnimation.LoadFromVdf(vdfPtr, "Humans-t_WalkR_2_Walk.MAN");

            Assert.NotNull(animation);

            DestroyVdf(vdfPtr);
        }
    }
}
