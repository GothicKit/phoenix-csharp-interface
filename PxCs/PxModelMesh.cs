using PxCs.Data.Mesh;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

namespace PxCs
{
    public class PxModelMesh
    {


        private const string DLLNAME = PxPhoenix.DLLNAME;

        [DllImport(DLLNAME)] public static extern IntPtr pxMdmLoad(IntPtr buffer);
        [DllImport(DLLNAME)] public static extern IntPtr pxMdmLoadFromVdf(IntPtr vdf, string name);
        [DllImport(DLLNAME)] public static extern void pxMdmDestroy(IntPtr mdm);

        [DllImport(DLLNAME)] public static extern uint pxMdmGetMeshCount(IntPtr mdm);
        [DllImport(DLLNAME)] public static extern IntPtr pxMdmGetMesh(IntPtr mdm, uint i);
        [DllImport(DLLNAME)] public static extern IntPtr pxMdmGetAttachment(IntPtr mdm, string name);
        [DllImport(DLLNAME)] public static extern uint pxMdmGetChecksum(IntPtr mdm);

        [DllImport(DLLNAME)] public static extern IntPtr pxSsmGetMesh(IntPtr ssm);
        [DllImport(DLLNAME)] public static extern uint pxSsmGetWedgeNormalsCount(IntPtr ssm);
        [DllImport(DLLNAME)] public static extern void pxSsmGetWedgeNormal(IntPtr ssm, uint i, out Vector3 normal, out uint index);
        [DllImport(DLLNAME)] public static extern uint pxSsmGetNodeCount(IntPtr ssm);
        [DllImport(DLLNAME)] public static extern uint pxSsmGetNodeWeightCount(IntPtr ssm, uint node);
        [DllImport(DLLNAME)] public static extern void pxSsmGetNodeWeight(IntPtr ssm, uint node, uint i, out float weight, out Vector3 position, out byte index);
        [DllImport(DLLNAME)] public static extern IntPtr pxSsmGetNodes(IntPtr ssm, out uint length); // ret: IntArray
    

        public static PxModelMeshData? LoadModelMeshFromVdf(IntPtr vdfPtr, string name)
        {
            var mdmPtr = pxMdmLoadFromVdf(vdfPtr, name);
            if (mdmPtr == IntPtr.Zero)
                return null;

            var data = new PxModelMeshData()
            {
                checksum = pxMdmGetChecksum(mdmPtr),
                meshes = GetMeshes(mdmPtr),
                attachments = GetAttachments(mdmPtr)
            };

            pxMdmDestroy(mdmPtr);
            return data;
        }

        public static PxSoftSkinMeshData[] GetMeshes(IntPtr mdmPtr)
        {

            return null;
        }

        public static Dictionary<string, PxProtoMeshData> GetAttachments(IntPtr mdmPtr)
        {
            return null;
        }

    }
}
