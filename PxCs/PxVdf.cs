using System;
using System.Runtime.InteropServices;

namespace PxCs
{
    public static class PxVdf
    {
        private const string DLLNAME = PxPhoenix.DLLNAME;

        [DllImport(DLLNAME)] public static extern IntPtr pxVdfNew(string comment);
        [DllImport(DLLNAME)] public static extern IntPtr pxVdfLoad(IntPtr buffer);
        [DllImport(DLLNAME)] public static extern IntPtr pxVdfLoadFromFile(string path);
        [DllImport(DLLNAME)] public static extern void pxVdfDestroy(IntPtr vdf);

        [DllImport(DLLNAME)] public static extern void pxVdfMerge(IntPtr vdf, IntPtr other, bool isOverride);
        [DllImport(DLLNAME)] public static extern IntPtr pxVdfGetEntryByName(IntPtr vdf, string name);
        [DllImport(DLLNAME)] public static extern IntPtr pxVdfEntryOpenBuffer(IntPtr entry);
    }
}
