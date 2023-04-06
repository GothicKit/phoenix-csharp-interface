using Xunit;
using Xunit.Abstractions;
using System;
using PxCs.Tests.Phoenix.Csharp.Interface;

namespace Phoenix.Csharp.Interface
{
    public class VdfTest: PhoenixTest
    {
        [Fact]
        public void Is_Vdf_loadable()
        {
            Assert.Equal("234", gothicAssetDir);
        }
    }
}