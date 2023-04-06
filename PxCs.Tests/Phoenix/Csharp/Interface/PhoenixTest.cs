using System;
using Xunit;

namespace PxCs.Tests.Phoenix.Csharp.Interface
{
    public abstract class PhoenixTest
    {
        protected readonly string gothicAssetDir;

        public PhoenixTest()
        {
            string? dir = Environment.GetEnvironmentVariable("GOTHIC_ASSET_DIR");
            Assert.True(dir != null, "Please start test with dotnet test --environment GOTHIC_ASSET_DIR=...");

            gothicAssetDir = dir;
        }
    }
}