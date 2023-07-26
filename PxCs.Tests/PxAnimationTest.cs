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
            var vfsPtr = LoadVfs("Data/anims.VDF");

            var animation = PxAnimation.LoadFromVfs(vfsPtr, "Humans-t_WalkR_2_Walk.MAN");

            Assert.NotNull(animation);

            DestroyVfs(vfsPtr);
        }
    }
}
