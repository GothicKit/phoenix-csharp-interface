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

            Assert.NotNull(soundByteArray!.sound);
            Assert.IsType<byte[]>(soundByteArray.sound);

            Assert.True(soundByteArray.sound.Length >= 1024, "Sound needs to have more than 1kB at least");

            var soundFloatArray = PxSound.GetSoundArrayFromVDF<float>(vdfPtr, "levelup.wav");
            Assert.True(soundFloatArray!.sound.Length >= 1024, "Sound needs to have more than 1kB at least");

            DestroyVdf(vdfPtr);

        }

        [Fact]
        public void Test_Load_Dialogue()
        {
            var vdfPtr = LoadVdf("Data/speech.VDF");

            var soundFloatArray = PxSound.GetSoundArrayFromVDF<float>(vdfPtr, "svm_1_diemonster.wav");

            Assert.IsType<float[]>(soundFloatArray!.sound);
            Assert.True(soundFloatArray.sound.Length >= 1024, "Sound needs to have more than 1kB at least");

            var soundByteArray = PxSound.GetSoundArrayFromVDF<byte>(vdfPtr, "svm_1_diemonster.wav");

            Assert.NotNull(soundByteArray!.sound);
            Assert.True(soundByteArray.sound.Length >= 1024, "Sound needs to have more than 1kB at least");

            DestroyVdf(vdfPtr);
        }
    }
}
