using System;
using System.IO;
using Xunit;

namespace Phoenix.Csharp.Interface
{
    public abstract class PhoenixTest
    {
        protected readonly string G1_ASSET_DIR;

        public PhoenixTest()
        {
            string? dir = Environment.GetEnvironmentVariable("GOTHIC1_ASSET_DIR");

            Assert.True(dir != null, "Please start test with dotnet test --environment GOTHIC1_ASSET_DIR=...");
            Assert.True(Directory.Exists(dir), "Path not exists");

            G1_ASSET_DIR = dir;
        }
    }
}