using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Csharp.Interface
{
    public class Buffer
    {
        private const string DLLNAME = Phoenix.DLLNAME;

        [DllImport(DLLNAME)] public static extern IntPtr pxBufferCreate(out IntPtr bytes, ulong size);
        [DllImport(DLLNAME)] public static extern IntPtr pxBufferMmap(string filePath);
        [DllImport(DLLNAME)] public static extern void pxBufferDestroy(IntPtr buffer);
    }
}
