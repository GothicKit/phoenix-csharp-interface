using System;
using Xunit;

namespace PxCs.Tests
{
    public class PxFontTest : PxPhoenixTest
    {
        [Fact]
        public void Test_load_Font()
        {
            var vdfPtr = LoadVdf("Data/textures.VDF");

            var font = PxFont.LoadFont(vdfPtr, "FONT_DEFAULT.FNT");

            Assert.NotNull(font);
            Assert.True(font.name == "FONT_DEFAULT.TGA", "Font name isn't set right.");
            Assert.True(font.glyphs.Length == 256, $"It's expected to have 256 glyphs, but >{font.glyphs.Length}< given.");
        }
    }
}