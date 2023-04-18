using PxCs.Data;
using PxCs.Types;
using System;
using System.Collections;
using System.Numerics;
using System.Runtime.InteropServices;

namespace PxCs
{
    /// <summary>
    /// MRM == MultiResolutionMesh
    /// </summary>
    public class PxMRM
    {
        private const string DLLNAME = PxPhoenix.DLLNAME;


        [DllImport(DLLNAME)] public static extern IntPtr pxMrmLoad(IntPtr buffer);
        [DllImport(DLLNAME)] public static extern IntPtr pxMrmLoadFromVdf(IntPtr vdf, string name);
        [DllImport(DLLNAME)] public static extern void pxMrmDestroy(IntPtr mrm);

        [DllImport(DLLNAME)] public static extern uint pxMrmGetPositionCount(IntPtr mrm);
        [DllImport(DLLNAME)] public static extern Vector3 pxMrmGetPosition(IntPtr mrm, uint i);
        [DllImport(DLLNAME)] public static extern uint pxMrmGetNormalCount(IntPtr mrm);
        [DllImport(DLLNAME)] public static extern Vector3 pxMrmGetNormal(IntPtr mrm, uint i);
        [DllImport(DLLNAME)] public static extern uint pxMrmGetMaterialCount(IntPtr mrm);
        [DllImport(DLLNAME)] public static extern IntPtr pxMrmGetMaterial(IntPtr mrm, uint i);
        [DllImport(DLLNAME)] public static extern byte pxMrmGetAlphaTest(IntPtr mrm);
        [DllImport(DLLNAME)] public static extern PxAABB pxMrmGetBbox(IntPtr mrm);

        [DllImport(DLLNAME)] public static extern uint pxMrmGetSubMeshCount(IntPtr mrm);
        [DllImport(DLLNAME)] public static extern IntPtr pxMrmGetSubMesh(IntPtr mrm, uint i);

        [DllImport(DLLNAME)] public static extern IntPtr pxMrmSubMeshGetMaterial(IntPtr sub);
        [DllImport(DLLNAME)] public static extern uint pxMrmSubMeshGetTriangleCount(IntPtr sub);
        [DllImport(DLLNAME)] public static extern void pxMrmSubMeshGetTriangle(IntPtr sub, uint i, out ushort a, out ushort b, out ushort c);
        [DllImport(DLLNAME)] public static extern uint pxMrmSubMeshGetWedgeCount(IntPtr sub);
        [DllImport(DLLNAME)] public static extern void pxMrmSubMeshGetWedge(IntPtr sub, uint i, out Vector3 normal, out Vector2 texture, out ushort index);
        [DllImport(DLLNAME)] public static extern IntPtr pxMrmSubMeshGetColors(IntPtr sub, out uint length);
        [DllImport(DLLNAME)] public static extern IntPtr pxMrmSubMeshGetTrianglePlaneIndices(IntPtr sub, out uint length);
        [DllImport(DLLNAME)] public static extern uint pxMrmSubMeshGetTrianglePlaneCount(IntPtr sub);
        [DllImport(DLLNAME)] public static extern void pxMrmSubMeshGetTrianglePlane(IntPtr sub, uint i, out float distance, out Vector3 normal);
        [DllImport(DLLNAME)] public static extern uint pxMrmSubMeshGetTriangleEdgeCount(IntPtr sub);
        [DllImport(DLLNAME)] public static extern void pxMrmSubMeshGetTriangleEdge(IntPtr sub, uint i, out ushort a, out ushort b, out ushort c);
        [DllImport(DLLNAME)] public static extern uint pxMrmSubMeshGetEdgeCount(IntPtr sub);
        [DllImport(DLLNAME)] public static extern void pxMrmSubMeshGetEdge(IntPtr sub, uint i, out ushort a, out ushort b);
        [DllImport(DLLNAME)] public static extern IntPtr pxMrmSubMeshGetEdgeScores(IntPtr sub, out uint length);
        [DllImport(DLLNAME)] public static extern IntPtr pxMrmSubMeshGetWedgeMap(IntPtr sub, out uint length);


        public static PxMRMData? GetMRMFromVdf(IntPtr vdfPtr, string name)
        {
            var mrmPtr = pxMrmLoadFromVdf(vdfPtr, "ITFO_PLANTS_BERRYS_01.MRM");

            if (mrmPtr == IntPtr.Zero)
                return null;

            var positions = GetPositions(mrmPtr);
            var normals = GetNormals(mrmPtr);

            var alphaTest = pxMrmGetAlphaTest(mrmPtr);
            var bbox = pxMrmGetBbox(mrmPtr);

            var data = new PxMRMData()
            {
                positions = positions,
                normals = normals,
                alphaTest = alphaTest,
                bbox = bbox
            };

            pxMrmDestroy(mrmPtr);

            return data;
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

        public static float[] GetSubMeshColors(IntPtr msh)
        {
            var arrayPtr = pxMrmSubMeshGetColors(msh, out uint length);
            var array = new float[length];

            if (length > int.MaxValue)
                throw new ArgumentOutOfRangeException($"We can only handle int.MaxValue of elements but >{length}< was given.");

            Marshal.Copy(arrayPtr, array, 0, (int)length);

            return array;
        }

        public static float[] GetSubMeshEdgeScores(IntPtr msh)
        {
            var arrayPtr = pxMrmSubMeshGetEdgeScores(msh, out uint length);
            var array = new float[length];

            if (length > int.MaxValue)
                throw new ArgumentOutOfRangeException($"We can only handle int.MaxValue of elements but >{length}< was given.");

            Marshal.Copy(arrayPtr, array, 0, (int)length);

            return array;
        }

        public static ushort[] GetSubMeshTrianglePlaneIndices(IntPtr msh)
        {
            var arrayPtr = pxMrmSubMeshGetTrianglePlaneIndices(msh, out uint length);
            var array = new short[length];

            if (length > int.MaxValue)
                throw new ArgumentOutOfRangeException($"We can only handle int.MaxValue of elements but >{length}< was given.");

            Marshal.Copy(arrayPtr, array, 0, (int)length);

            // If there's any negative value inside the array, at runtime it will just be "wrapped around" and no binary change will happen on the data.
            // https://www.c-sharpcorner.com/uploadfile/b942f9/how-to-convert-unsigned-integer-arrays-to-signed-arrays-and-vice-versa/
            return (ushort[])(object)array;
        }

        public static ushort[] GetSubMeshWedgeMap(IntPtr msh)
        {
            var arrayPtr = pxMrmSubMeshGetWedgeMap(msh, out uint length);
            var array = new short[length];

            if (length > int.MaxValue)
                throw new ArgumentOutOfRangeException($"We can only handle int.MaxValue of elements but >{length}< was given.");

            Marshal.Copy(arrayPtr, array, 0, (int)length);

            // If there's any negative value inside the array, at runtime it will just be "wrapped around" and no binary change will happen on the data.
            // https://www.c-sharpcorner.com/uploadfile/b942f9/how-to-convert-unsigned-integer-arrays-to-signed-arrays-and-vice-versa/
            return (ushort[])(object)array;
        }
    }
}
