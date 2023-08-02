using PxCs.Interface;
using Xunit;

namespace PxCs.Tests
{
    public class PxSoundTest : PxPhoenixTest
    {
        [Fact]
        public void Test_Load_SFX()
        {
            var vfsPtr = LoadVfs("Data/sound.VDF");

            var soundByteArray = PxSound.GetSoundArrayFromVfs<byte>(vfsPtr, "levelup.wav");

            Assert.NotNull(soundByteArray!.sound);
            Assert.IsType<byte[]>(soundByteArray.sound);

            Assert.True(soundByteArray.sound.Length >= 1024, "Sound needs to have more than 1kB at least");

            var soundFloatArray = PxSound.GetSoundArrayFromVfs<float>(vfsPtr, "levelup.wav");
            Assert.True(soundFloatArray!.sound.Length >= 1024, "Sound needs to have more than 1kB at least");

            DestroyVfs(vfsPtr);

        }

        [Fact]
        public void Test_Load_Dialogue()
        {
            var vfsPtr = LoadVfs("Data/speech.VDF");

            var soundFloatArray = PxSound.GetSoundArrayFromVfs<float>(vfsPtr, "svm_1_diemonster.wav");

            Assert.IsType<float[]>(soundFloatArray!.sound);
            Assert.True(soundFloatArray.sound.Length >= 1024, "Sound needs to have more than 1kB at least");

            var soundByteArray = PxSound.GetSoundArrayFromVfs<byte>(vfsPtr, "svm_1_diemonster.wav");

            Assert.NotNull(soundByteArray!.sound);
            Assert.True(soundByteArray.sound.Length >= 1024, "Sound needs to have more than 1kB at least");

            DestroyVfs(vfsPtr);
        }
    }
}
