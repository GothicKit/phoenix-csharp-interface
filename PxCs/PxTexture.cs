using PxCs.Data;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using static PxCs.PxLogging;

namespace PxCs
{
    public class PxTexture
    {
        private const string DLLNAME = PxPhoenix.DLLNAME;

        public enum Format
        {
            tex_B8G8R8A8 = 0x0, // 32-bit ARGB pixel format with alpha, using 8 bits per channel
            tex_A8B8G8R8 = 0x2, // 32-bit ARGB pixel format with alpha, using 8 bits per channel
            tex_A8R8G8B8 = 0x3, // 32-bit ARGB pixel format with alpha, using 8 bits per channel
            tex_R8G8B8A8 = 0x1, // 32-bit ARGB pixel format with alpha, using 8 bits per channel
            tex_B8G8R8 = 0x4,   // 24-bit RGB pixel format with 8 bits per channel
            tex_R8G8B8 = 0x5,   // 24-bit RGB pixel format with 8 bits per channel
            tex_A4R4G4B4 = 0x6, // 16-bit ARGB pixel format with 4 bits for each channel
            tex_A1R5G5B5 = 0x7, // 16-bit pixel format where 5 bits are reserved for each color, and 1 bit is reserved for alpha
            tex_R5G6B5 = 0x8,   // 16-bit RGB pixel format with 5 bits for red, 6 bits for green, and 5 bits for blue
            tex_p8 = 0x9,       // 8-bit color indexed
            tex_dxt1 = 0xA,     // DXT1 compression texture format
            tex_dxt2 = 0xB,     // DXT2 compression texture format
            tex_dxt3 = 0xC,     // DXT3 compression texture format
            tex_dxt4 = 0xD,     // DXT4 compression texture format
            tex_dxt5 = 0xE      // DXT5 compression texture format
        };

        [DllImport(DLLNAME)] public static extern IntPtr pxTexLoad(IntPtr buffer);
        [DllImport(DLLNAME)] public static extern IntPtr pxTexLoadFromVdf(IntPtr vdf, string name);
        [DllImport(DLLNAME)] public static extern void pxTexDestroy(IntPtr tex);

        [DllImport(DLLNAME)] public static extern void pxTexGetMeta(IntPtr tex, out Format format, out uint width, out uint height, out uint mipmapCount, out uint averageColor);
        [DllImport(DLLNAME)] public static extern IntPtr pxTexGetMipmap(IntPtr tex, out uint length, uint level, out uint width, out uint height);


        public static PxTextureData? GetTextureFromVdf(IntPtr vdf, string name)
        {
            var texturePtr = LoadTextureFromVdf(vdf, name);

            if (texturePtr == IntPtr.Zero)
                return null;

            pxTexGetMeta(texturePtr, out Format format, out uint width, out uint height, out uint mipmapCount, out uint averageColor);
            var mipmaps = new PxTextureMipmapData[mipmapCount];

            for (var level = 0u; level < mipmapCount; level++)
            {
                mipmaps[level] = LoadMipmap(texturePtr, level);
            }

            pxTexDestroy(texturePtr);

            return new PxTextureData()
            {
                format = format,
                width = width,
                height = height,
                mipmapCount = mipmapCount,
                averageColor = averageColor,

                mipmaps= mipmaps
            };
        }

        /// <summary>
        /// Try to load texture in default name (.TGA) and compiled (-C.TEX)
        /// </summary>
        private static IntPtr LoadTextureFromVdf(IntPtr vdf, string name)
        {
            var texturePtr = pxTexLoadFromVdf(vdf, name);

            // Try another time with compiled texture.
            if (texturePtr == IntPtr.Zero)
            {
                var compiledName = name.Replace(".TGA", "-C.TEX");
                texturePtr = pxTexLoadFromVdf(vdf, compiledName);
            }

            return texturePtr;
        }

        private static PxTextureMipmapData LoadMipmap(IntPtr texturePtr, uint level)
        {
            var mipmapPtr = pxTexGetMipmap(texturePtr, out uint length, level, out uint mipmapWidth, out uint mipmapHeight);

            if (length > int.MaxValue)
                throw new ArgumentOutOfRangeException($"We can only handle int.MaxValue of elements but >{length}< was given.");

            var mipmapArray = new byte[length];
            Marshal.Copy(mipmapPtr, mipmapArray, 0, (int)length);

            return new PxTextureMipmapData()
            {
                level = level,
                width = mipmapWidth,
                height = mipmapHeight,
                mipmap = mipmapArray
            };
        }
    }
}
