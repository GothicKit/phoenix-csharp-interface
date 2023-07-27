using PxCs.Data;
using PxCs.Extensions;
using System;
using System.Runtime.InteropServices;

namespace PxCs.Interface
{
    public static class PxVfs
    {
        private const string DLLNAME = PxPhoenix.DLLNAME;

        [DllImport(DLLNAME)] public static extern IntPtr pxVfsNew();
        [DllImport(DLLNAME)] public static extern void pxVfsMountDisk(IntPtr vfs, string path);
        [DllImport(DLLNAME)] public static extern void pxVfsDestroy(IntPtr vfs);

        [DllImport(DLLNAME)] public static extern IntPtr pxVfsGetNodeByName(IntPtr vfs, string name);
        [DllImport(DLLNAME)] public static extern IntPtr pxVfsNodeOpenBuffer(IntPtr node);

        /// <summary>
        /// Hint: This file isn't taking care of special flags like LoadAdditive or UseNewer (for patches and mods)
        /// </summary>
        public static IntPtr LoadVfs(string[] files)
        {
            var vfsPtr = pxVfsNew();

            foreach (var file in files)
            {
                pxVfsMountDisk(vfsPtr, file);
            }

            return vfsPtr;
        }

        public static void DestroyVfs(IntPtr vfsPtr)
        {
            pxVfsDestroy(vfsPtr);
        }
    }
}
