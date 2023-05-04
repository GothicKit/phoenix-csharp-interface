using PxCs.Data.Model;
using System;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace PxCs.Interface
{
    public class PxModel
    {
        private const string DLLNAME = PxPhoenix.DLLNAME;



        [DllImport(DLLNAME)] public static extern IntPtr pxMdlLoad(IntPtr buffer);
        [DllImport(DLLNAME)] public static extern IntPtr pxMdlLoadFromVdf(IntPtr vdf, string name);
        [DllImport(DLLNAME)] public static extern void pxMdlDestroy(IntPtr mdl);

        [DllImport(DLLNAME)] public static extern IntPtr pxMdlGetHierarchy(IntPtr mdl);
        [DllImport(DLLNAME)] public static extern IntPtr pxMdlGetMesh(IntPtr mdl);


        public static PxModelData? LoadModelFromVdf(IntPtr vdfPtr, string name)
        {
            var mdlPtr = pxMdlLoadFromVdf(vdfPtr, name);
            if (mdlPtr == IntPtr.Zero)
                return null;

            var mdhPtr = pxMdlGetHierarchy(mdlPtr);
            var mdmPtr = pxMdlGetMesh(mdlPtr);

            var mdl = new PxModelData()
            {
                hierarchy = PxModelHierarchy.GetFromPtr(mdhPtr),
                mesh = PxModelMesh.GetFromPtr(mdmPtr)

            };

            pxMdlDestroy(mdlPtr);
            return mdl;
        }
    }
}
