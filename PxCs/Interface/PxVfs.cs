using PxCs.Data;
using PxCs.Extensions;
using System;
using System.Runtime.InteropServices;

namespace PxCs.Interface
{
    public static class PxVfs
    {
        private const string DLLNAME = PxPhoenix.DLLNAME;

        public enum PxVfsOverwriteBehavior
        {
            PxVfsOverwrite_None = 0,  // Overwrite no conflicting nodes.
            PxVfsOverwrite_All = 1,   // Overwrite all conflicting nodes.
            PxVfsOverwrite_Newer = 2, // Overwrite newer conflicting nodes. (i.e. use older versions)
            PxVfsOverwrite_Older = 3  // Overwrite older conflicting nodes. (i.e. use newer versions)
        }
        
        [DllImport(DLLNAME)] public static extern IntPtr pxVfsNew();
        [DllImport(DLLNAME)] public static extern void pxVfsMountDisk(IntPtr vfs, string path, PxVfsOverwriteBehavior overwriteFlag);
        [DllImport(DLLNAME)] public static extern void pxVfsDestroy(IntPtr vfs);

        [DllImport(DLLNAME)] public static extern IntPtr pxVfsGetNodeByName(IntPtr vfs, string name);
        [DllImport(DLLNAME)] public static extern IntPtr pxVfsNodeOpenBuffer(IntPtr node);

        /// <summary>
        /// Hint: This file isn't taking care of special flags like LoadAdditive or UseNewer (for patches and mods)
        /// </summary>
        public static IntPtr LoadVfs(string[] files, PxVfsOverwriteBehavior overwriteFlag)
        {
            var vfsPtr = pxVfsNew();

            foreach (var file in files)
            {
                pxVfsMountDisk(vfsPtr, file, overwriteFlag);
            }

            return vfsPtr;
        }

        public static void DestroyVfs(IntPtr vfsPtr)
        {
            pxVfsDestroy(vfsPtr);
        }
    }
}
