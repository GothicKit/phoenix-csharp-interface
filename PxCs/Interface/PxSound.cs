using System;
using System.Runtime.InteropServices;
using System.Text;
using PxCs.Extensions;
using PxCs.Helper;


namespace PxCs.Interface
{
    public class PxSound
    {
        private const string DLLNAME = PxPhoenix.DLLNAME;

        public enum BitDepth
        {
            bit_8 = 8,
            bit_16 = 16,
        }
                name += ".WAV";
            }
            var vdfEntrySound = PxVdf.pxVdfGetEntryByName(vdfPtr, name);

            if (vdfEntrySound == IntPtr.Zero)
            {
                throw new AccessViolationException("Sound not found.");
            }

            var wavSound = PxVdf.pxVdfEntryOpenBuffer(vdfEntrySound);

            if (wavSound == IntPtr.Zero)
            {
                throw new AccessViolationException("Sound could not be opened.");
            }

            try
            {
                ulong size = PxBuffer.pxBufferSize(wavSound);

                var arrayPtr = IntPtr.Zero;
                arrayPtr = PxBuffer.pxBufferArray(wavSound);
                byte[] wavFile = new byte[size];

                Marshal.Copy(arrayPtr, wavFile, 0, (int)size);
                return wavFile;
            }
            finally
            {
                PxBuffer.pxBufferDestroy(wavSound);
            }
        }

        public static float[] GetSoundFloatArrayFromVDF(IntPtr vdfPtr, string name)
        {
            byte[] wavFile = GetSoundByteArrayFromVDF(vdfPtr, name);
            return ConvertWAVByteArrayToFloatArray(wavFile);
        }

        private static float[] ConvertWAVByteArrayToFloatArray(byte[] fileBytes)
        {
            byte[] fileBytesCopy = new byte[fileBytes.Length];
            Array.Copy(fileBytes, fileBytesCopy, fileBytes.Length);

            string riff = Encoding.ASCII.GetString(fileBytes, 0, 4);
            string wave = Encoding.ASCII.GetString(fileBytes, 8, 4);
            int subchunk1 = BitConverter.ToInt32(fileBytes, 16);
            UInt16 audioFormat = BitConverter.ToUInt16(fileBytes, 20);

            float[] data;

            string formatCode = FormatCode(audioFormat);

            UInt16 channels = BitConverter.ToUInt16(fileBytes, 22);
            int sampleRate = BitConverter.ToInt32(fileBytes, 24);
            int byteRate = BitConverter.ToInt32(fileBytes, 28);
            UInt16 blockAlign = BitConverter.ToUInt16(fileBytes, 32);
            UInt16 bitDepth = BitConverter.ToUInt16(fileBytes, 34);

            int headerOffset = 16 + 4 + subchunk1 + 4;
            int subchunk2 = BitConverter.ToInt32(fileBytes, headerOffset);

            if (formatCode == "IMA ADPCM")
            {
                var decoder = new IMAADPCMDecoder();
                return ConvertWAVByteArrayToFloatArray(decoder.Decode(fileBytes));
            }

            data = ConvertByteArrayToFloatArray(fileBytes, headerOffset, (BitDepth)bitDepth);
            return data;
        }


        private static float[] ConvertByteArrayToFloatArray(byte[] source, int headerOffset, BitDepth bit)
        {
            if (bit == BitDepth.bit_8)
            {
                int wavSize = BitConverter.ToInt32(source, headerOffset);
                headerOffset += sizeof(int);

                float[] data = new float[wavSize];

                sbyte maxValue = sbyte.MaxValue;

                for (int i = 0; i < wavSize; i++)
                    data[i] = (float)source[i] / maxValue;

                return data;

            }
            else if (bit == BitDepth.bit_16)
            {
                int bytesPerSample = sizeof(Int16); // block size = 2
                int sampleCount = source.Length / bytesPerSample;

                float[] data = new float[sampleCount];

                Int16 maxValue = Int16.MaxValue;

                for (int i = 0; i < sampleCount; i++)
                {
                    int offset = i * bytesPerSample;
                    Int16 sample = BitConverter.ToInt16(source, offset);
                    float floatSample = (float)sample / maxValue;
                    data[i] = floatSample;
                }

                return data;
            }
            else
            {
                throw new Exception(bit + " bit depth is not supported.");
            }
        }

        private static string FormatCode(UInt16 code)
        {
            switch (code)
            {
                case 1:
                    return "PCM";
                case 2:
                    return "ADPCM";
                case 3:
                    return "IEEE";
                case 7:
                    return "μ-law";
                case 17:
                    return "IMA ADPCM";
                case 65534:
                    return "WaveFormatExtensable";
                default:
                    return "";
            }
        }
    }
}

