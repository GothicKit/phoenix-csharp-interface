﻿using PxCs.Interface;
using Xunit;

namespace PxCs.Tests
{
    public class PxFontTest : PxPhoenixTest
    {
        [Fact]
        public void Test_load_Font()
        {
            var vfsPtr = LoadVfs("Data/textures.VDF");

            var font = PxFont.LoadFont(vfsPtr, "FONT_OLD_20_WHITE.FNT");

            Assert.NotNull(font);
            Assert.True(font.name == "FONT_OLD_20_WHITE.TGA", "Font name isn't set right.");
            Assert.True(font.glyphs!.Length == 256, $"It's expected to have 256 glyphs, but >{font.glyphs.Length}< given.");

            Assert.NotNull(font.texture);
            Assert.True(font.texture.width == 1024, $"Font texture expected to be 1024 but was >{font.texture.width}<.");
        }
    }
}