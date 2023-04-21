using System;
using System.Runtime.InteropServices;

namespace PxCs
{
    public static class PxModelScript
    {
        private const string DLLNAME = PxPhoenix.DLLNAME;

        [DllImport(DLLNAME)] public static extern IntPtr pxMdsLoad(IntPtr buffer);
        [DllImport(DLLNAME)] public static extern IntPtr pxMdsLoadFromVdf(IntPtr vdf, string name);
        [DllImport(DLLNAME)] public static extern void pxMdsDestroy(IntPtr mdm);
    }
}
