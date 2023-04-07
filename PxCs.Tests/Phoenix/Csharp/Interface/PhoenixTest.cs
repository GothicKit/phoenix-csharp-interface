using System;
using System.IO;
using Xunit;

namespace Phoenix.Csharp.Interface
{
    public abstract class PhoenixTest
    {
        protected readonly string G_ASSET_DIR;

        public PhoenixTest()
        {
            string? dir = Environment.GetEnvironmentVariable("GOTHIC_ASSET_DIR");

            Assert.True(dir != null, "Please start test with dotnet test --environment GOTHIC_ASSET_DIR=...");
            Assert.True(Directory.Exists(dir), "Path not exists");

            G_ASSET_DIR = dir;
        }
    }
}