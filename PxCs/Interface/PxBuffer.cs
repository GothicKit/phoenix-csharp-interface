using System;
using System.Runtime.InteropServices;

namespace PxCs.Interface
{
    public class PxBuffer
    {
        private const string DLLNAME = PxPhoenix.DLLNAME;

        [DllImport(DLLNAME)] public static extern IntPtr pxBufferCreate(out IntPtr bytes, ulong size);
        [DllImport(DLLNAME)] public static extern IntPtr pxBufferMmap(string filePath);
        [DllImport(DLLNAME)] public static extern void pxBufferDestroy(IntPtr buffer);
        [DllImport(DLLNAME)] public static extern IntPtr pxBufferArray(IntPtr buffer);
        [DllImport(DLLNAME)] public static extern ulong pxBufferSize(IntPtr buffer);
    }
}
