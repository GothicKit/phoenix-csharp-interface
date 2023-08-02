using PxCs.Data.Mesh;
using PxCs.Data.Struct;
using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace PxCs.Interface
{
    /// <summary>
    /// MRM == MultiResolutionMesh
    /// </summary>
    public class PxMultiResolutionMesh
    {
        private const string DLLNAME = PxPhoenix.DLLNAME;


        [DllImport(DLLNAME)] public static extern IntPtr pxMrmLoad(IntPtr buffer);
        [DllImport(DLLNAME)] public static extern IntPtr pxMrmLoadFromVfs(IntPtr vfs, string name);
        [DllImport(DLLNAME)] public static extern void pxMrmDestroy(IntPtr mrm);

        [DllImport(DLLNAME)] public static extern uint pxMrmGetPositionCount(IntPtr mrm);
        [DllImport(DLLNAME)] public static extern Vector3 pxMrmGetPosition(IntPtr mrm, uint i);
        [DllImport(DLLNAME)] public static extern uint pxMrmGetNormalCount(IntPtr mrm);
        [DllImport(DLLNAME)] public static extern Vector3 pxMrmGetNormal(IntPtr mrm, uint i);
        [DllImport(DLLNAME)] public static extern uint pxMrmGetMaterialCount(IntPtr mrm);
        [DllImport(DLLNAME)] public static extern IntPtr pxMrmGetMaterial(IntPtr mrm, uint i);
        [DllImport(DLLNAME)] public static extern byte pxMrmGetAlphaTest(IntPtr mrm);
        [DllImport(DLLNAME)] public static extern PxAABBData pxMrmGetBbox(IntPtr mrm);


        public static PxMultiResolutionMeshData? GetMRMFromVfs(IntPtr vfsPtr, string name)
        {
            var mrmPtr = pxMrmLoadFromVfs(vfsPtr, name);
            if (mrmPtr == IntPtr.Zero)
                return null;

            var data = GetMRMFromPtr(mrmPtr);

            pxMrmDestroy(mrmPtr);
            return data;
        }

        public static PxMultiResolutionMeshData GetMRMFromPtr(IntPtr mrmPtr)
        {
            var positions = GetPositions(mrmPtr);
            var normals = GetNormals(mrmPtr);

            var alphaTest = pxMrmGetAlphaTest(mrmPtr);
            var bbox = pxMrmGetBbox(mrmPtr);

            var materials = GetMaterials(mrmPtr);
            var subMeshes = PxMRMSubMesh.GetSubMeshes(mrmPtr);

            return new PxMultiResolutionMeshData()
            {
                positions = positions,
                normals = normals,
                alphaTest = alphaTest,
                bbox = bbox,

                materials = materials,
                subMeshes = subMeshes
            };
        }

        public static Vector3[] GetPositions(IntPtr mrmPtr)
        {
            var count = pxMrmGetPositionCount(mrmPtr);
            var array = new Vector3[count];

            for (var i = 0u; i < count; i++)
                array[i] = pxMrmGetPosition(mrmPtr, i);

            return array;
        }

        public static Vector3[] GetNormals(IntPtr mrmPtr)
        {
            var count = pxMrmGetNormalCount(mrmPtr);
            var array = new Vector3[count];

            for (var i = 0u; i < count; i++)
                array[i] = pxMrmGetNormal(mrmPtr, i);

            return array;
        }

        public static PxMaterialData[] GetMaterials(IntPtr mrmPtr)
        {
            var count = pxMrmGetMaterialCount(mrmPtr);
            var array = new PxMaterialData[count];

            for (var i = 0u; i < count; i++)
            {
                var materialPtr = pxMrmGetMaterial(mrmPtr, i);
                array[i] = PxMaterial.GetMaterial(materialPtr);
            }

            return array;
        }
    }
}
