using PxCs.Data.Mesh;
using PxCs.Data.Model;
using PxCs.Extensions;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;

namespace PxCs.Interface
{
    public class PxModelMesh
    {


        private const string DLLNAME = PxPhoenix.DLLNAME;

        [DllImport(DLLNAME)] public static extern IntPtr pxMdmLoad(IntPtr buffer);
        [DllImport(DLLNAME)] public static extern IntPtr pxMdmLoadFromVfs(IntPtr vfs, string name);
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


        public static PxModelMeshData? LoadModelMeshFromVfs(IntPtr vfsPtr, string name, params string[] attachmentKeys)
        {
            var mdmPtr = pxMdmLoadFromVfs(vfsPtr, name);
            var data = GetFromPtr(mdmPtr, attachmentKeys);

            pxMdmDestroy(mdmPtr);
            return data;
        }

        public static PxModelMeshData? GetFromPtr(IntPtr mdmPtr, params string[] attachmentKeys)
        {
            if (mdmPtr == IntPtr.Zero)
                return null;

            return new PxModelMeshData()
            {
                checksum = pxMdmGetChecksum(mdmPtr),
                meshes = GetMeshes(mdmPtr),
                attachments = GetAttachments(mdmPtr, attachmentKeys)
            };
        }

        public static PxSoftSkinMeshData[] GetMeshes(IntPtr mdmPtr)
        {
            var count = pxMdmGetMeshCount(mdmPtr);
            var array = new PxSoftSkinMeshData[count];

            for (var i = 0u; i < count; i++)
            {
                array[i] = GetSoftSkinMesh(mdmPtr, i);
            }

            return array;
        }

        public static PxSoftSkinMeshData GetSoftSkinMesh(IntPtr mdmPtr, uint i)
        {
            var softSkinMeshPtr = pxMdmGetMesh(mdmPtr, i);
            var multiResolutionMeshPtr = pxSsmGetMesh(softSkinMeshPtr);

            return new PxSoftSkinMeshData()
            {
                mesh = PxMultiResolutionMesh.GetMRMFromPtr(multiResolutionMeshPtr),
                wedgeNormals = GetSoftSkinMeshWedgeNormals(softSkinMeshPtr),
                nodes = pxSsmGetNodes(softSkinMeshPtr, out uint length).MarshalAsArray<int>(length),
                weights = GetSoftSkinMeshWeights(softSkinMeshPtr)
            };
        }

        public static PxWedgeNormalData[] GetSoftSkinMeshWedgeNormals(IntPtr softSkinMeshPtr)
        {
            var count = pxSsmGetWedgeNormalsCount(softSkinMeshPtr);
            var array = new PxWedgeNormalData[count];

            for (var i = 0u; i < count; i++)
            {
                pxSsmGetWedgeNormal(softSkinMeshPtr, i, out Vector3 normal, out uint index);
                array[i] = new PxWedgeNormalData()
                {
                    normal = normal,
                    index = index
                };
            }

            return array;
        }

        public static PxWeightEntryData[][] GetSoftSkinMeshWeights(IntPtr softSkinMeshPtr)
        {
            var nodeCount = pxSsmGetNodeCount(softSkinMeshPtr);
            var array = new PxWeightEntryData[nodeCount][];

            for (var i = 0u; i < nodeCount; i++)
            {
                var weightCount = pxSsmGetNodeWeightCount(softSkinMeshPtr, i);
                array[i] = new PxWeightEntryData[weightCount];

                for (var ii = 0u; ii < weightCount; ii++)
                {
                    pxSsmGetNodeWeight(softSkinMeshPtr, i, ii, out float weight, out Vector3 position, out byte index);
                    array[i][ii] = new PxWeightEntryData()
                    {
                        weight = weight,
                        position = position,
                        nodeIndex = index
                    };
                }
            }

            return array;
        }

        public static Dictionary<string, PxMultiResolutionMeshData> GetAttachments(IntPtr mdmPtr, params string[] attachmentKeys)
        {
            var data = new Dictionary<string, PxMultiResolutionMeshData>();

            foreach (var key in attachmentKeys)
            {
                var attachmentPtr = pxMdmGetAttachment(mdmPtr, key);
                if (attachmentPtr == IntPtr.Zero)
                    continue;

                var mrm = PxMultiResolutionMesh.GetMRMFromPtr(attachmentPtr);
                data[key] = mrm;
            }

            return data;
        }

    }
}
