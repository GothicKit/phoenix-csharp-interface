using System;
using System.Collections.Generic;
using PxCs.Interface;
using Xunit;

namespace PxCs.Tests
{
    public class PxSoundTest : PxPhoenixTest
    {
        [Fact]
        public void Test_Load_SFX()
        {
            var vdfPtr = LoadVdf("Data/sound.VDF");

            var soundByteArray = PxSound.GetSoundArrayFromVDF<byte>(vdfPtr, "levelup.wav");

            Assert.NotNull(soundByteArray.sound);
            Assert.IsType<byte[]>(soundByteArray.sound);

            Assert.True(soundByteArray.sound.Length == 102440, "Wrong number of bytes " + soundByteArray.sound.Length);

            var soundFloatArray = PxSound.GetSoundArrayFromVDF<float>(vdfPtr, "levelup.wav");
            Assert.True(soundFloatArray.sound.Length == 204880, "Wrong number of converted bytes " + soundFloatArray.sound.Length);

            DestroyVdf(vdfPtr);

        }

        [Fact]
        public void Test_Load_Dialogue()
        {
            var vdfPtr = LoadVdf("Data/speech.VDF");

            var soundFloatArray = PxSound.GetSoundArrayFromVDF<float>(vdfPtr, "svm_1_diemonster.wav");

            Assert.IsType<float[]>(soundFloatArray.sound);
            Assert.True(soundFloatArray.sound.Length == 73462, "Wrong number of converted bytes " + soundFloatArray.sound.Length);

            var soundByteArray = PxSound.GetSoundArrayFromVDF<byte>(vdfPtr, "svm_1_diemonster.wav");

            Assert.NotNull(soundByteArray.sound);
            Assert.True(soundByteArray.sound.Length == 36924, "Wrong number of bytes " + soundByteArray.sound.Length);

            DestroyVdf(vdfPtr);
        }
    }
}
