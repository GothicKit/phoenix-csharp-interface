using PxCs.Data;
using PxCs.Extensions;
using System;
using System.Runtime.InteropServices;

namespace PxCs.Interface
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
        [DllImport(DLLNAME)] public static extern uint pxVdfGetRootEntryCount(IntPtr vdf);
        [DllImport(DLLNAME)] public static extern IntPtr pxVdfGetRootEntry(IntPtr vdf, uint i);

        [DllImport(DLLNAME)] public static extern IntPtr pxVdfEntryGetName(IntPtr entry);
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport(DLLNAME)] public static extern bool pxVdfEntryIsDirectory(IntPtr entry);
        [DllImport(DLLNAME)] public static extern uint pxVdfEntryGetChildCount(IntPtr entry);
        [DllImport(DLLNAME)] public static extern IntPtr pxVdfEntryGetChild(IntPtr entry, uint i);


        public static PxVdfEntryMetaData LoadEntryMetaDataList(IntPtr vdfPtr)
        {
            var count = pxVdfGetRootEntryCount(vdfPtr);
            var array = new PxVdfEntryMetaData[count];

            for (var i = 0u; i < count; i++)
            {
                var rootEntryPtr = pxVdfGetRootEntry(vdfPtr, i);
                array[i] = new PxVdfEntryMetaData()
                {
                    name = pxVdfEntryGetName(rootEntryPtr).MarshalAsString(),
                    isDirectory = pxVdfEntryIsDirectory(rootEntryPtr),
                    children = LoadEntrySubMetaDataList(rootEntryPtr)
                };
            }

            return new PxVdfEntryMetaData()
            {
                name = "/",
                isDirectory = true,
                children = array
            };
        }

        private static PxVdfEntryMetaData[] LoadEntrySubMetaDataList(IntPtr vdfMetaPtr)
        {
            var count = pxVdfEntryGetChildCount(vdfMetaPtr);
            var array = new PxVdfEntryMetaData[count];

            for (var i = 0u; i < count; i++)
            {
                var childEntryPtr = pxVdfEntryGetChild(vdfMetaPtr, i);
                array[i] = new PxVdfEntryMetaData()
                {
                    name = pxVdfEntryGetName(childEntryPtr).MarshalAsString(),
                    isDirectory = pxVdfEntryIsDirectory(childEntryPtr),
                    children = LoadEntrySubMetaDataList(childEntryPtr)
                };
            }

            return array;
        }
    }
}
