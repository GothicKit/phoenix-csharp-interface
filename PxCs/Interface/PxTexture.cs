﻿using PxCs.Data.Texture;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace PxCs.Interface
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
        [DllImport(DLLNAME)] public static extern IntPtr pxTexLoadFromVfs(IntPtr vfs, string name);
        [DllImport(DLLNAME)] public static extern void pxTexDestroy(IntPtr tex);

        [DllImport(DLLNAME)] public static extern void pxTexGetMeta(IntPtr tex, out Format format, out uint width, out uint height, out uint mipmapCount, out uint averageColor);
        [DllImport(DLLNAME)] public static extern IntPtr pxTexGetMipmap(IntPtr tex, uint level, out uint size, out uint width, out uint height);
        [DllImport(DLLNAME)] public static extern IntPtr pxTexGetDecompressedMipmap(IntPtr tex, uint level, out uint size, out uint width, out uint height);
        [DllImport(DLLNAME)] public static extern void pxTexFreeDecompressedMipmap(IntPtr data);


        public static PxTextureData? GetTextureFromVfs(IntPtr vfs, string name, params Format[] supportedFormats)
        {
            var texturePtr = LoadTextureFromVfs(vfs, name);

            if (texturePtr == IntPtr.Zero)
                return null;

            pxTexGetMeta(texturePtr, out Format format, out uint width, out uint height, out uint mipmapCount, out uint averageColor);
            var mipmaps = new PxTextureMipmapData[mipmapCount];

            for (var level = 0u; level < mipmapCount; level++)
            {
                // a) it's already uncompressed or b) the texture format is a supported one from the caller.
                if (supportedFormats.Contains(Format.tex_B8G8R8A8) || supportedFormats.Contains(format))
                    mipmaps[level] = LoadMipmap(texturePtr, level);
                else
                    mipmaps[level] = LoadMipmapUncompressed(texturePtr, level);
            }

            pxTexDestroy(texturePtr);

            // If we loaded the uncompressed value, we need to change the format.
            if (!supportedFormats.Contains(format))
                format = Format.tex_B8G8R8A8;

            return new PxTextureData()
            {
                format = format,
                width = width,
                height = height,
                mipmapCount = mipmapCount,
                averageColor = averageColor,

                mipmaps = mipmaps
            };
        }

        /// <summary>
        /// Try to load texture in default name (.TGA) and compiled (-C.TEX)
        /// </summary>
        private static IntPtr LoadTextureFromVfs(IntPtr vfs, string name)
        {
            IntPtr texturePtr;

            // Based on experience, textures are compiled most of the time.
            // Therefore start with the -C.TEX version to load.
            if (name.EndsWith(".TGA"))
            {
                var compiledName = name.Replace(".TGA", "-C.TEX");
                texturePtr = pxTexLoadFromVfs(vfs, compiledName);

                if (texturePtr != IntPtr.Zero)
                    return texturePtr;
            }

            texturePtr = pxTexLoadFromVfs(vfs, name);

            return texturePtr;
        }

        private static PxTextureMipmapData LoadMipmap(IntPtr texturePtr, uint level)
        {
            var mipmapPtr = pxTexGetMipmap(texturePtr, level, out uint size, out uint mipmapWidth, out uint mipmapHeight);

            if (size > int.MaxValue)
                throw new ArgumentOutOfRangeException($"We can only handle int.MaxValue of elements but >{size}< was given.");

            var mipmapArray = new byte[size];
            Marshal.Copy(mipmapPtr, mipmapArray, 0, (int)size);

            return new PxTextureMipmapData()
            {
                level = level,
                width = mipmapWidth,
                height = mipmapHeight,
                mipmap = mipmapArray
            };
        }

        public static PxTextureMipmapData LoadMipmapUncompressed(IntPtr texturePtr, uint level)
        {
            var mipmapPtr = pxTexGetDecompressedMipmap(texturePtr, level, out uint size, out uint mipmapWidth, out uint mipmapHeight);

            if (size > int.MaxValue)
                throw new ArgumentOutOfRangeException($"We can only handle int.MaxValue of elements but >{size}< was given.");

            var mipmapArray = new byte[size];
            Marshal.Copy(mipmapPtr, mipmapArray, 0, (int)size);

            pxTexFreeDecompressedMipmap(mipmapPtr);

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
