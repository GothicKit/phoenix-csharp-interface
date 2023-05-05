using PxCs.Data.Model;
using System;
using System.Linq;
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
            var hierarchy = PxModelHierarchy.GetFromPtr(mdhPtr);

            var mdmPtr = pxMdlGetMesh(mdlPtr);
            var attachmentKeys = hierarchy!.nodes.Select(i => i.name).ToArray();
            var mesh = PxModelMesh.GetFromPtr(mdmPtr, attachmentKeys!);

            var mdl = new PxModelData()
            {
                hierarchy = hierarchy,
                mesh = mesh
            };

            pxMdlDestroy(mdlPtr);
            return mdl;
        }
    }
}
