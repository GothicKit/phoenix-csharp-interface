using System;
using Xunit;

namespace PxCs.Tests
{
    public class PxAnimationTest : PxPhoenixTest
    {
        [Fact]
        public void Test_load_animation()
        {
            var vdfPtr = LoadVdf("Data/anims.VDF");

            var animation = PxAnimation.LoadFromVdf(vdfPtr, "Hum_RunAmbient_M01.asc");

            DestroyVdf(vdfPtr);
        }
    }
}
